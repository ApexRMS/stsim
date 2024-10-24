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
    class FlowTypeDataSheet : DataSheet
    {
        private DataTable m_FlowTypeDataTable;
        private DataSheet m_FlowGroupDataSheet;
        private DataTable m_FlowGroupDataTable;
        private readonly Dictionary<int, string> m_PrevNames = new Dictionary<int, string>();

        protected override void OnDataFeedsRefreshed(DataStore store)
        {
            base.OnDataFeedsRefreshed(store);

            this.m_FlowTypeDataTable = this.GetData(store);
            this.m_FlowGroupDataSheet = this.Project.GetDataSheet(Strings.DATASHEET_FLOW_GROUP_NAME);
            this.m_FlowGroupDataTable = this.m_FlowGroupDataSheet.GetData(store);
        }

        protected override void OnRowsAdded(object sender, DataSheetRowEventArgs e)
        {
            Dictionary<int, string> AutoGroups = new Dictionary<int, string>();
            Dictionary<int, int> AutoTypeGroups = new Dictionary<int, int>();
            string IdColName = this.PrimaryKeyColumn.Name;

            using (DataStore store = this.Library.CreateDataStore())
            {
                foreach (DataRow dr in this.m_FlowTypeDataTable.Rows)
                {
                    if (dr.RowState != DataRowState.Added)
                    {
                        continue;
                    }

                    int ThisId = Convert.ToInt32(dr[IdColName], CultureInfo.InvariantCulture);
                    string AutoGroupName = GetAutoGeneratedGroupName(dr);

                    if (this.m_FlowGroupDataSheet.ValidationTable.ContainsValue(AutoGroupName))
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
                this.m_FlowGroupDataSheet.BeginAddRows();

                foreach (int gid in AutoGroups.Keys)
                {
                    this.CreateFlowGroup(gid, AutoGroups[gid]);
                }

                this.m_FlowGroupDataSheet.EndAddRows();
            }

            base.OnRowsAdded(sender, e);
        }

        public override void DeleteRows(IEnumerable<DataRow> rows)
        {
            List<DataRow> DeleteRows = new List<DataRow>();
            Dictionary<string, DataRow> GroupRows = this.CreateFlowGroupRowDictionary();

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
                ((FlowGroupDataSheet)this.m_FlowGroupDataSheet).DeleteAutoGeneratedRows(DeleteRows);
            }

            base.DeleteRows(rows);
        }

        protected override void OnModifyingRows(object sender, DataSheetRowEventArgs e)
        {
            this.m_PrevNames.Clear();

            string IdColName = this.PrimaryKeyColumn.Name;

            foreach (DataRow dr in this.m_FlowTypeDataTable.Rows)
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
            Dictionary<string, DataRow> GroupRows = this.CreateFlowGroupRowDictionary();
            Dictionary<string, bool> ExistingNames = new Dictionary<string, bool>();

            foreach (string k in GroupRows.Keys)
            {
                ExistingNames.Add(k, true);
            }

            foreach (DataRow dr in this.m_FlowTypeDataTable.Rows)
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
                this.m_FlowGroupDataSheet.BeginModifyRows();

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

                this.m_FlowGroupDataSheet.EndModifyRows();
            }

            base.OnRowsModified(sender, e);
        }

        private Dictionary<string, DataRow> CreateFlowGroupRowDictionary()
        {
            Dictionary<string, DataRow> d = new Dictionary<string, DataRow>();

            foreach (DataRow dr in this.m_FlowGroupDataTable.Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    d.Add(Convert.ToString(dr[Strings.DATASHEET_NAME_COLUMN_NAME], CultureInfo.InvariantCulture), dr);
                }
            }

            return d;
        }

        private void CreateFlowGroup(int id, string name)
        {
            DataRow dr = this.m_FlowGroupDataTable.NewRow();

            dr[this.m_FlowGroupDataSheet.PrimaryKeyColumn.Name] = id;
            dr[Strings.DATASHEET_NAME_COLUMN_NAME] = name;
            dr[Strings.IS_AUTO_COLUMN_NAME] = Booleans.BoolToInt(true);

            this.m_FlowGroupDataTable.Rows.Add(dr);
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
