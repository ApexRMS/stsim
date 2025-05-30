﻿// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using SyncroSim.Core;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    class StockTypeDataSheet : DataSheet
    {
        private DataTable m_StockTypeDataTable;
        private DataSheet m_StockGroupDataSheet;
        private DataTable m_StockGroupDataTable;
        private readonly Dictionary<int, string> m_PrevNames = new Dictionary<int, string>();

        protected override void OnDataFeedsRefreshed(DataStore store)
        {
            base.OnDataFeedsRefreshed(store);

            this.m_StockTypeDataTable = this.GetData(store);
            this.m_StockGroupDataSheet = this.Project.GetDataSheet(Strings.DATASHEET_STOCK_GROUP_NAME );
            this.m_StockGroupDataTable = this.m_StockGroupDataSheet.GetData(store);
        }

        protected override void OnRowsAdded(object sender, DataSheetRowEventArgs e)
        {
            Dictionary<int, string> AutoGroups = new Dictionary<int, string>();
            Dictionary<int, int> AutoTypeGroups = new Dictionary<int, int>();
            string IdColName = this.PrimaryKeyColumn.Name;

            using (DataStore store = this.Library.CreateDataStore())
            {
                foreach (DataRow dr in this.m_StockTypeDataTable.Rows)
                {
                    if (dr.RowState != DataRowState.Added)
                    {
                        continue;
                    }

                    int ThisId = Convert.ToInt32(dr[IdColName], CultureInfo.InvariantCulture);
                    string AutoGroupName = GetAutoGeneratedGroupName(dr);

                    if (this.m_StockGroupDataSheet.ValidationTable.ContainsValue(AutoGroupName))
                    {
                        continue;
                    }

                    int AutoGroupId = Library.GetNextSequenceId(store);

                    AutoGroups.Add(AutoGroupId, AutoGroupName);
                    AutoTypeGroups.Add(ThisId, AutoGroupId);
                }
            }

            Debug.Assert(AutoTypeGroups.Count == AutoGroups.Count);

            if (AutoGroups.Count > 0)
            {
                this.m_StockGroupDataSheet.BeginAddRows();

                foreach (int gid in AutoGroups.Keys)
                {
                    this.CreateStockGroup(gid, AutoGroups[gid]);
                }

                this.m_StockGroupDataSheet.EndAddRows();
            }

            base.OnRowsAdded(sender, e);
        }

        public override void DeleteRows(IEnumerable<DataRow> rows)
        {
            List<DataRow> DeleteRows = new List<DataRow>();
            Dictionary<string, DataRow> GroupRows = this.CreateStockGroupRowDictionary();

            foreach (DataRow dr in rows)
            {
                string AutoGroupName = GetAutoGeneratedGroupName(dr);

                if (!GroupRows.ContainsKey(AutoGroupName))
                {
                    continue;
                }

                Debug.Assert(DataTableUtilities.GetDataBool(GroupRows[AutoGroupName], Strings.IS_AUTO_COLUMN_NAME));
                DeleteRows.Add(GroupRows[AutoGroupName]);
            }

            if (DeleteRows.Count > 0)
            {
                ((StockGroupDataSheet)this.m_StockGroupDataSheet).DeleteAutoGeneratedRows(DeleteRows);
            }

            base.DeleteRows(rows);
        }

        protected override void OnModifyingRows(object sender, DataSheetRowEventArgs e)
        {
            this.m_PrevNames.Clear();

            string IdColName = this.PrimaryKeyColumn.Name;

            foreach (DataRow dr in this.m_StockTypeDataTable.Rows)
            {
                if (dr.RowState == DataRowState.Deleted)
                {
                    continue;
                }

                int TypeId = Convert.ToInt32(dr[IdColName], CultureInfo.InvariantCulture);
                string TypeName = Convert.ToString(dr[Strings.DATASHEET_NAME_COLUMN_NAME, DataRowVersion.Current], CultureInfo.InvariantCulture);

                this.m_PrevNames.Add(TypeId, TypeName);
            }

            base.OnModifyingRows(sender, e);
        }

        protected override void OnRowsModified(object sender, DataSheetRowEventArgs e)
        {
            List<DataRow> ModifyRows = new List<DataRow>();
            string IdColName = this.PrimaryKeyColumn.Name;
            Dictionary<string, DataRow> GroupRows = this.CreateStockGroupRowDictionary();
            Dictionary<string, bool> ExistingNames = new Dictionary<string, bool>();

            foreach (string k in GroupRows.Keys)
            {
                ExistingNames.Add(k, true);
            }

            foreach (DataRow dr in this.m_StockTypeDataTable.Rows)
            {
                if (dr.RowState == DataRowState.Deleted)
                {
                    continue;
                }

                int id = Convert.ToInt32(dr[IdColName], CultureInfo.InvariantCulture);

                if (!this.m_PrevNames.ContainsKey(id))
                {
                    continue;
                }

                string OldName = this.m_PrevNames[id];
                string OldAutoGroupName = GetAutoGeneratedGroupName(OldName);

                if (!GroupRows.ContainsKey(OldAutoGroupName))
                {
                    continue;
                }

                string NewName = Convert.ToString(dr[Strings.DATASHEET_NAME_COLUMN_NAME], CultureInfo.InvariantCulture);

                Debug.Assert(DataTableUtilities.GetDataBool(GroupRows[OldAutoGroupName], Strings.IS_AUTO_COLUMN_NAME));

                if (OldName != NewName)
                {
                    ModifyRows.Add(dr);
                }
            }

            if (ModifyRows.Count > 0)
            {
                this.m_StockGroupDataSheet.BeginModifyRows();

                foreach (DataRow dr in ModifyRows)
                {
                    string OldName = this.m_PrevNames[Convert.ToInt32(dr[IdColName], CultureInfo.InvariantCulture)];
                    string NewName = Convert.ToString(dr[Strings.DATASHEET_NAME_COLUMN_NAME], CultureInfo.InvariantCulture);

                    Debug.Assert(OldName != NewName);
                    Debug.Assert(!GroupRows.ContainsKey(GetAutoGeneratedGroupName(NewName)));

                    string OldAutoGroupName = GetAutoGeneratedGroupName(OldName);
                    string NewAutoGroupName = GetAutoGeneratedGroupName(NewName);

                    GroupRows[OldAutoGroupName][Strings.DATASHEET_NAME_COLUMN_NAME] = NewAutoGroupName;
                }

                this.m_StockGroupDataSheet.EndModifyRows();
            }

            base.OnRowsModified(sender, e);
        }

        private Dictionary<string, DataRow> CreateStockGroupRowDictionary()
        {
            Dictionary<string, DataRow> d = new Dictionary<string, DataRow>();

            foreach (DataRow dr in this.m_StockGroupDataTable.Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    d.Add(Convert.ToString(dr[Strings.DATASHEET_NAME_COLUMN_NAME], CultureInfo.InvariantCulture), dr);
                }
            }

            return d;
        }

        private void CreateStockGroup(int id, string name)
        {
            DataRow dr = this.m_StockGroupDataTable.NewRow();

            dr[this.m_StockGroupDataSheet.PrimaryKeyColumn.Name] = id;
            dr[Strings.DATASHEET_NAME_COLUMN_NAME] = name;
            dr[Strings.IS_AUTO_COLUMN_NAME] = Booleans.BoolToInt(true);

            this.m_StockGroupDataTable.Rows.Add(dr);
        }

        private static string GetAutoGeneratedGroupName(DataRow dr)
        {
            return GetAutoGeneratedGroupName(Convert.ToString(dr[Strings.DATASHEET_NAME_COLUMN_NAME], CultureInfo.InvariantCulture));
        }

        private static string GetAutoGeneratedGroupName(string typeName)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0} {1}", typeName, Strings.AUTO_COLUMN_SUFFIX);
        }
    }
}
