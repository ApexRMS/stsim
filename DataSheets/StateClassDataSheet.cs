// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using SyncroSim.Core;
using System.Globalization;
using System.Collections.Generic;
using System.Diagnostics;

namespace SyncroSim.STSim
{
    internal class StateClassDataSheet : DataSheet
    {
        private DataSheet m_SlxSheet;
        private DataSheet m_SlySheet;

        protected override void OnDataFeedsRefreshed()
        {
            base.OnDataFeedsRefreshed();

            this.m_SlxSheet = this.Project.GetDataSheet(Strings.DATASHEET_STATE_LABEL_X_NAME);
            this.m_SlySheet = this.Project.GetDataSheet(Strings.DATASHEET_STATE_LABEL_Y_NAME);
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

            if (proposedRow[Strings.DATASHEET_NAME_COLUMN_NAME] == DBNull.Value && proposedRow.RowState == DataRowState.Detached)
            {
                proposedRow[Strings.DATASHEET_NAME_COLUMN_NAME] = "__SYNCROSIM_TEMP_VALUE__";
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

        protected override void OnRowsAdded(object sender, DataSheetRowEventArgs e)
        {
            base.OnRowsAdded(sender, e);

            Dictionary<string, bool> d = new Dictionary<string, bool>();

            //Create a dictionary of the existing names

            foreach (DataRow dr in this.GetData().Rows)
            {
                if (dr.RowState == DataRowState.Deleted)
                {
                    continue;
                }

                if (dr[Strings.DATASHEET_NAME_COLUMN_NAME] != DBNull.Value)
                {
                    d.Add(Convert.ToString(dr[Strings.DATASHEET_NAME_COLUMN_NAME], CultureInfo.InvariantCulture), true);
                }
            }

            //If any name is NULL then add the name, checking for duplicates as we go

            foreach (DataRow dr in this.GetData().Rows)
            {
                if (dr.RowState == DataRowState.Deleted)
                {
                    continue;
                }

                if (dr[Strings.DATASHEET_NAME_COLUMN_NAME] == DBNull.Value)
                {
                    DataTable slxdata = this.m_SlxSheet.GetData();
                    DataTable slydata = this.m_SlySheet.GetData();
                    int slxid = Convert.ToInt32(dr[Strings.DATASHEET_STATECLASS_STATE_LABEL_X_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                    int slyid = Convert.ToInt32(dr[Strings.DATASHEET_STATECLASS_STATE_LABEL_Y_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                    string slxname = Convert.ToString(DataTableUtilities.GetTableValue(slxdata, this.m_SlxSheet.ValueMember, slxid, Strings.DATASHEET_NAME_COLUMN_NAME), CultureInfo.InvariantCulture);
                    string slyname = Convert.ToString(DataTableUtilities.GetTableValue(slydata, this.m_SlySheet.ValueMember, slyid, Strings.DATASHEET_NAME_COLUMN_NAME), CultureInfo.InvariantCulture);

                    string n = GetNextName(slxname + ":" + slyname, d);
                    dr[Strings.DATASHEET_NAME_COLUMN_NAME] = n;

                    d.Add(n, true);
                }
            }
        }

        protected override void BeforeImportData(DataTable proposedData)
        {
            base.BeforeImportData(proposedData);

            //We require the State Label X and Y values but they are not validated 
            //until after the call to BeforeImportData() so validate them now.

            foreach (DataRow dr in proposedData.Rows)
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

            //Create a dictionary of the existing names

            Dictionary<string, bool> d = new Dictionary<string, bool>();

            foreach (DataRow dr in proposedData.Rows)
            {
                if (dr[Strings.DATASHEET_NAME_COLUMN_NAME] != DBNull.Value)
                {
                    d.Add(Convert.ToString(dr[Strings.DATASHEET_NAME_COLUMN_NAME], CultureInfo.InvariantCulture), true);
                }
            }

            //If any name is NULL then add the name, checking for duplicates as we go

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

                    d.Add(FinalName, true);
                }
            }
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
    }
}
