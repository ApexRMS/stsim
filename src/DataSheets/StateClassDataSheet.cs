// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using SyncroSim.Core;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    internal class StateClassDataSheet : DataSheet
    {
        private DataSheet m_SlxSheet;
        private DataSheet m_SlySheet;
        private bool m_AllowRowChangeEvents = true;
        private const string TEMP_NAME_VALUE = "__SYNCROSIM_TEMP_VALUE__";

        protected override void OnDataFeedsRefreshed()
        {
            base.OnDataFeedsRefreshed();

            this.m_SlxSheet = this.Project.GetDataSheet(Strings.DATASHEET_STATE_LABEL_X_NAME);
            this.m_SlySheet = this.Project.GetDataSheet(Strings.DATASHEET_STATE_LABEL_Y_NAME);

            this.SubscribeChildSheets();
            this.SubscribeDataTable();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.IsDisposed)
            {
                this.UnsubscribeChildSheets();
                UnsubscribeDataTable();
            }

            base.Dispose(disposing);
        }

        public override void Validate(DataRow proposedRow, DataTransferMethod transferMethod)
        {
            bool ResetNull = false;

            //It's not possible to have NULL for a display member, but we want the user to be able to
            //have this name be auto-generated.  To avoid a validation failure because of a NULL here
            //we are going to fix up the Name with a default value until the validation is done and then
            //set it back to NULL.  The OnRowsAdded() override will auto-generate the name.  Note that
            //this should only be done for detached rows (i.e. rows that are being editing by a control
            //such as the DataGridView...)

            if (proposedRow[Strings.DATASHEET_NAME_COLUMN_NAME] == DBNull.Value && 
                proposedRow.RowState == DataRowState.Detached)
            {
                proposedRow[Strings.DATASHEET_NAME_COLUMN_NAME] = TEMP_NAME_VALUE;
                ResetNull = true;
            }

            try
            {
                base.Validate(proposedRow, transferMethod);
            }
            finally
            {
                if (ResetNull)
                {
                    proposedRow[Strings.DATASHEET_NAME_COLUMN_NAME] = DBNull.Value;
                }
            }
        }
      
        private void OnColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            if (e.Row.RowState == DataRowState.Detached)
            {
                return;
            }

            if (!this.m_AllowRowChangeEvents)
            {
                return;
            }

            DataTable dt = this.GetData();
            Dictionary<string, bool> d = CreateExistingNamesDictionary(dt);

            this.m_AllowRowChangeEvents = false;

            if (e.Column.ColumnName == Strings.DATASHEET_NAME_COLUMN_NAME)
            {
                if (e.Row[Strings.DATASHEET_NAME_COLUMN_NAME] != DBNull.Value && 
                    Convert.ToString(e.Row[Strings.DATASHEET_NAME_COLUMN_NAME], CultureInfo.InvariantCulture) != TEMP_NAME_VALUE)
                {
                    e.Row[Strings.DATASHEET_IS_AUTO_NAME_COLUMN_NAME] = Booleans.BoolToInt(false);
                } 
            }
            else if (
                e.Column.ColumnName == Strings.DATASHEET_STATECLASS_STATE_LABEL_X_ID_COLUMN_NAME ||
                e.Column.ColumnName == Strings.DATASHEET_STATECLASS_STATE_LABEL_Y_ID_COLUMN_NAME)
            {
                bool IsAutoName = DataTableUtilities.GetDataBool(e.Row, Strings.DATASHEET_IS_AUTO_NAME_COLUMN_NAME);

                if (IsAutoName)
                {
                    string slxname = this.GetSlxName(e.Row);
                    string slyname = this.GetSlyName(e.Row);
                    string CurrentName = Convert.ToString(e.Row[Strings.DATASHEET_NAME_COLUMN_NAME], CultureInfo.InvariantCulture);
                    string ProposedName = slxname + ":" + slyname;

                    if (CurrentName != ProposedName)
                    {
                        ProposedName = GetNextName(ProposedName, d);
                        e.Row[Strings.DATASHEET_NAME_COLUMN_NAME] = ProposedName;
                    }
                }
            }

            this.m_AllowRowChangeEvents = true;
        }

        protected override void OnRowsAdded(object sender, DataSheetRowEventArgs e)
        {
            base.OnRowsAdded(sender, e);
            this.m_AllowRowChangeEvents = false;

            DataTable dt = this.GetData();
            Dictionary<string, bool> d = CreateExistingNamesDictionary(dt);

            foreach (DataRow dr in dt.Rows)
            {
                if (dr.RowState == DataRowState.Deleted)
                {
                    continue;
                }

                if (dr[Strings.DATASHEET_NAME_COLUMN_NAME] == DBNull.Value)
                {
                    string slxname = this.GetSlxName(dr);
                    string slyname = this.GetSlyName(dr);
                    string n = GetNextName(slxname + ":" + slyname, d);

                    dr[Strings.DATASHEET_NAME_COLUMN_NAME] = n;
                    dr[Strings.DATASHEET_IS_AUTO_NAME_COLUMN_NAME] = Booleans.BoolToInt(true);

                    d.Add(n, true);
                }
            }

            this.m_AllowRowChangeEvents = true;
        }

        protected override void BeforeImportData(DataTable proposedData, string importFileName)
        {
            base.BeforeImportData(proposedData, importFileName);

            this.ValidateSlxSly(proposedData);
            EnsureIsAutoNameColumn(proposedData);

            Dictionary<string, bool> d = CreateExistingNamesDictionary(proposedData);

            foreach (DataRow dr in proposedData.Rows)
            {
                if (dr[Strings.DATASHEET_NAME_COLUMN_NAME] == DBNull.Value)
                {
                    string InitialName = Convert.ToString(
                        dr[Strings.DATASHEET_STATECLASS_STATE_LABEL_X_ID_COLUMN_NAME], CultureInfo.InvariantCulture) + 
                        ":" + 
                        Convert.ToString(dr[Strings.DATASHEET_STATECLASS_STATE_LABEL_Y_ID_COLUMN_NAME], CultureInfo.InvariantCulture);

                    string FinalName = GetNextName(InitialName, d);

                    dr[Strings.DATASHEET_NAME_COLUMN_NAME] = FinalName;
                    dr[Strings.DATASHEET_IS_AUTO_NAME_COLUMN_NAME] = "Yes";

                    d.Add(FinalName, true);
                }
            }
        }

        private void OnSLXSLYChanged(object sender, EventArgs e)
        {
            DataTable dt = this.GetData();
            Dictionary<string, bool> d = CreateExistingNamesDictionary(dt);
            this.m_AllowRowChangeEvents = false;

            foreach (DataRow dr in dt.Rows)
            {
                if (dr.RowState == DataRowState.Deleted)
                {
                    continue;
                }
 
                bool IsAutoName = DataTableUtilities.GetDataBool(dr, Strings.DATASHEET_IS_AUTO_NAME_COLUMN_NAME);

                if (IsAutoName)
                {
                    string slxname = this.GetSlxName(dr);
                    string slyname = this.GetSlyName(dr);
                    string CurrentName = Convert.ToString(dr[Strings.DATASHEET_NAME_COLUMN_NAME], CultureInfo.InvariantCulture);

                    string CurrentNamePart1 = CurrentName;
                    string CurrentNamePart2 = null;

                    if (CurrentName.Contains("("))
                    {
                        string[] s = CurrentName.Split(' ');

                        CurrentNamePart1 = s[0];
                        CurrentNamePart2 = s[1];
                    }

                    string[] Split = CurrentNamePart1.Split(':');

                    if (Split[0] == slxname && Split[1] == slyname)
                    {
                        string n = slxname + ":" + slyname;

                        if (CurrentNamePart2 != null)
                        {
                            n += " " + CurrentNamePart2;
                        }

                        dr[Strings.DATASHEET_NAME_COLUMN_NAME] = n;                   
                    }
                    else
                    {
                        string n = GetNextName(slxname + ":" + slyname, d);

                        dr[Strings.DATASHEET_NAME_COLUMN_NAME] = n;
                        d.Add(n, true);
                    }
                }
            }

            this.m_AllowRowChangeEvents = true;
        }

        private static string GetNextName(string proposedName, Dictionary<string, bool> existingNames)
        {
            Debug.Assert(!string.IsNullOrEmpty(proposedName));
            string NewProposedName = proposedName;

            int NextNum = 1;
            string NextName = NewProposedName;

            while (existingNames.ContainsKey(NextName))
            {
                NextNum += 1;
                NextName = string.Format(CultureInfo.InvariantCulture, "{0} ({1})", NewProposedName, NextNum);
                Debug.Assert(NextNum < 500);
            }

            return NextName;
        }

        private void SubscribeDataTable()
        {
            this.UnsubscribeDataTable();

            DataTable dt = this.GetData();
            dt.ColumnChanged += this.OnColumnChanged;
        }

        private void UnsubscribeDataTable()
        {
            DataTable dt = this.GetData();
            dt.ColumnChanged -= this.OnColumnChanged;
        }

        private void SubscribeChildSheets()
        {
            this.UnsubscribeChildSheets();

            if (this.m_SlxSheet != null)
            {
                this.m_SlxSheet.RowsModified += this.OnSLXSLYChanged;
                this.m_SlySheet.RowsModified += this.OnSLXSLYChanged;
            }
        }

        private void UnsubscribeChildSheets()
        {
            if (this.m_SlxSheet != null)
            {
                this.m_SlxSheet.RowsModified -= this.OnSLXSLYChanged;
                this.m_SlySheet.RowsModified -= this.OnSLXSLYChanged;
            }
        }

        private static void EnsureIsAutoNameColumn(DataTable dt)
        {
            if (!dt.Columns.Contains(Strings.DATASHEET_IS_AUTO_NAME_COLUMN_NAME))
            {
                dt.Columns.Add(new DataColumn(Strings.DATASHEET_IS_AUTO_NAME_COLUMN_NAME, typeof(object)));
            }
        }

        private string GetSlxName(DataRow dr)
        {
            DataTable slxdata = this.m_SlxSheet.GetData();
            int slxid = Convert.ToInt32(dr[Strings.DATASHEET_STATECLASS_STATE_LABEL_X_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
            string slxname = Convert.ToString(DataTableUtilities.GetTableValue(slxdata, this.m_SlxSheet.ValueMember, slxid, Strings.DATASHEET_NAME_COLUMN_NAME), CultureInfo.InvariantCulture);

            return slxname;
        }

        private string GetSlyName(DataRow dr)
        {
            DataTable slydata = this.m_SlySheet.GetData();
            int slyid = Convert.ToInt32(dr[Strings.DATASHEET_STATECLASS_STATE_LABEL_Y_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
            string slyname = Convert.ToString(DataTableUtilities.GetTableValue(slydata, this.m_SlySheet.ValueMember, slyid, Strings.DATASHEET_NAME_COLUMN_NAME), CultureInfo.InvariantCulture);

            return slyname;
        }

        private static Dictionary<string, bool> CreateExistingNamesDictionary(DataTable dt)
        {
            Dictionary<string, bool> d = new Dictionary<string, bool>();

            foreach (DataRow dr in dt.Rows)
            {
                if (dr.RowState == DataRowState.Deleted)
                {
                    continue;
                }

                if (dr[Strings.DATASHEET_NAME_COLUMN_NAME] == DBNull.Value)
                {
                    continue;
                }

                string n = Convert.ToString(dr[Strings.DATASHEET_NAME_COLUMN_NAME], CultureInfo.InvariantCulture);

                if (d.ContainsKey(n))
                {
                    continue;
                }

                d.Add(n, true);
            }

            return d;
        }

        private void ValidateSlxSly(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr[Strings.DATASHEET_STATECLASS_STATE_LABEL_X_ID_COLUMN_NAME] == DBNull.Value)
                {
                    ExceptionUtils.ThrowArgumentException(
                        "The data contains a NULL for '{0}'.",
                        this.Columns[Strings.DATASHEET_STATECLASS_STATE_LABEL_X_ID_COLUMN_NAME].DisplayName);
                }

                if (dr[Strings.DATASHEET_STATECLASS_STATE_LABEL_Y_ID_COLUMN_NAME] == DBNull.Value)
                {
                    ExceptionUtils.ThrowArgumentException(
                        "The data contains a NULL for '{0}'.",
                        this.Columns[Strings.DATASHEET_STATECLASS_STATE_LABEL_Y_ID_COLUMN_NAME].DisplayName);
                }
            }
        }
    }
}
