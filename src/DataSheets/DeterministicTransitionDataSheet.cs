// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using SyncroSim.Core;
using System.Diagnostics;

namespace SyncroSim.STSim
{
    internal class DeterministicTransitionDataSheet : DataSheet
    {
        protected override void OnDataSheetChanged(DataSheetMonitorEventArgs e)
        {
            base.OnDataSheetChanged(e);

            string Primary = null;
            string Secondary = null;
            string Tertiary = null;

            TerminologyUtilities.GetStratumLabelTerminology(e.DataSheet, ref Primary, ref Secondary, ref Tertiary);

            this.Columns[Strings.DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME].DisplayName = Primary;
            this.Columns[Strings.DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME].DisplayName = "To " + Primary;
        }

        public override void Validate(object proposedValue, string columnName)
        {
            base.Validate(proposedValue, columnName);

            if (columnName == Strings.DATASHEET_DT_LOCATION_COLUMN_NAME)
            {
                if (!DTAnalyzer.IsValidLocation(proposedValue))
                {
                    throw new DataException(MessageStrings.ERROR_INVALID_CELL_ADDRESS);
                }
            }
        }

        public override void Validate(DataRow proposedRow, DataTransferMethod transferMethod)
        {
            base.Validate(proposedRow, transferMethod);

            int? StratumIdSource = null;
            int StateClassIdSource = 0;
            int? StratumIdDest = null;
            int? StateClassIdDest = null;

            DTAnalyzer.GetDTFieldValues(proposedRow, ref StratumIdSource, ref StateClassIdSource, ref StratumIdDest, ref StateClassIdDest);

            if (!StateClassIdDest.HasValue)
            {
                return;
            }

            if (StateClassIdDest.Value == StateClassIdSource)
            {
                if (!StratumIdDest.HasValue)
                {
                    return;
                }

                if (NullableUtilities.NullableIdsEqual(StratumIdSource, StratumIdDest))
                {
                    return;
                }
            }

            DTAnalyzer Analyzer = new DTAnalyzer(this.GetData(), this.Project);

            if (!Analyzer.CanResolveStateClass(StratumIdSource, StratumIdDest, StateClassIdDest.Value))
            {
                Analyzer.ThrowDataException(StateClassIdDest.Value, true);
            }
        }

        /// <summary>
        /// Overrides Validate
        /// </summary>
        /// <param name="data"></param>
        /// <param name="transferMethod"></param>
        /// <returns></returns>
        /// <remarks>
        /// While the incoming state class Ids can themselves be valid, they must also be valid within the context of the union of the
        /// incoming data and the existing data.  In other words, each destination state class must point to a source state class within
        /// the same stratum.
        /// </remarks>
        public override void Validate(System.Data.DataTable proposedData, DataTransferMethod transferMethod)
        {
            base.Validate(proposedData, transferMethod);

            DataTable dtdata = this.GetData();
            DTAnalyzer AnalyzerExisting = new DTAnalyzer(dtdata, this.Project);
            DTAnalyzer AnalyzerProposed = new DTAnalyzer(proposedData, this.Project);

            foreach (DataRow ProposedRow in proposedData.Rows)
            {
                int? StratumIdSource = null;
                int StateClassIdSource = 0;
                int? StratumIdDest = null;
                int? StateClassIdDest = null;

                DTAnalyzer.GetDTFieldValues(ProposedRow, ref StratumIdSource, ref StateClassIdSource, ref StratumIdDest, ref StateClassIdDest);

                if (!StateClassIdDest.HasValue)
                {
                    return;
                }

                //If the state class is not part of the incoming data then we need to see if it is part of the existing data, and 
                //if it isn't then we can't continue.  Note that if the import option is 'Overwrite' then the state class
                //will not appear in the existing data!

                bool ClassInClip = AnalyzerProposed.CanResolveStateClass(StratumIdSource, StratumIdDest, StateClassIdDest.Value);
                bool ClassInExisting = AnalyzerExisting.CanResolveStateClass(StratumIdSource, StratumIdDest, StateClassIdDest.Value);
                bool IsOverwrite = (transferMethod == SyncroSim.Core.DataTransferMethod.Overwrite);

                if (!ClassInClip)
                {
                    if (IsOverwrite || (!ClassInExisting))
                    {
                        AnalyzerExisting.ThrowDataException(StateClassIdDest.Value, true);
                    }
                }

                //Also validate the location

                if (!DTAnalyzer.IsValidLocation(ProposedRow[Strings.DATASHEET_DT_LOCATION_COLUMN_NAME]))
                {
                    throw new DataException(MessageStrings.ERROR_INVALID_CELL_ADDRESS);
                }
            }
        }

        /// <summary>
        /// Overrides OnAfterRowsDeleted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// If deterministic transitions have been deleted then it's possible that some transitions no longer reference
        /// valid state classes.  If it is a deterministic transition then we just need to fix up its destination state
        /// class and stratum to be the same as the source state class and stratum.  If it's a probabilistic transition, 
        /// however, we need to delete it.
        /// <remarks></remarks>
        protected override void OnRowsDeleted(object sender, SyncroSim.Core.DataSheetRowEventArgs e)
        {
            DTAnalyzer Analyzer = new DTAnalyzer(this.GetData(), this.Project);

            if (this.ResolveDeterministicTransitions(Analyzer))
            {
                this.Changes.Add(new ChangeRecord(this, "DT OnRowsDeleted Modified DT Rows"));
            }

            if (this.ResolveProbabilisticTransitions(Analyzer))
            {
                this.GetDataSheet(Strings.DATASHEET_PT_NAME).Changes.Add(new ChangeRecord(this, "DT OnRowsDeleted Deleted PT Rows"));
            }

            base.OnRowsDeleted(sender, e);

#if DEBUG
            this.VALIDATE_DETERMINISTIC_TRANSITIONS();
            this.VALIDATE_PROBABILISTIC_TRANSITIONS();
#endif
        }

        protected override void OnRowsModified(object sender, DataSheetRowEventArgs e)
        {
            base.OnRowsModified(sender, e);

            DTAnalyzer Analyzer = new DTAnalyzer(this.GetData(), this.Project);

            if (this.ResolveDeterministicTransitions(Analyzer))
            {
                this.Changes.Add(new ChangeRecord(this, "DT OnRowsModified Modified DT Rows"));
            }

            if (this.ResolveProbabilisticTransitions(Analyzer))
            {
                this.GetDataSheet(Strings.DATASHEET_PT_NAME).Changes.Add(new ChangeRecord(this, "DT OnRowsModified Deleted PT Rows"));
            }

#if DEBUG
            this.VALIDATE_DETERMINISTIC_TRANSITIONS();
            this.VALIDATE_PROBABILISTIC_TRANSITIONS();
#endif
        }

        private bool ResolveDeterministicTransitions(DTAnalyzer analyzer)
        {
            bool HasChanges = false;
            DataTable dt = this.GetData();

            for (int Index = dt.Rows.Count - 1; Index >= 0; Index--)
            {
                DataRow dr = dt.Rows[Index];

                if (dr.RowState == DataRowState.Deleted)
                {
                    continue;
                }

                int? StratumIdSource = null;
                int StateClassIdSource = 0;
                int? StratumIdDest = null;
                int? StateClassIdDest = null;

                DTAnalyzer.GetDTFieldValues(dr, ref StratumIdSource, ref StateClassIdSource, ref StratumIdDest, ref StateClassIdDest);

                if (!StateClassIdDest.HasValue)
                {
                    continue;
                }

                if (!analyzer.CanResolveStateClass(StratumIdSource, StratumIdDest, StateClassIdDest.Value))
                {
                    dr[Strings.DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME] = DBNull.Value;
                    dr[Strings.DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME] = DBNull.Value;

                    HasChanges = true;
                }
            }

            return HasChanges;
        }

        private bool ResolveProbabilisticTransitions(DTAnalyzer analyzer)
        {
            bool HasChanges = false;
            DataTable dt = this.GetDataSheet(Strings.DATASHEET_PT_NAME).GetData();

            for (int Index = dt.Rows.Count - 1; Index >= 0; Index--)
            {
                DataRow dr = dt.Rows[Index];

                if (dr.RowState == DataRowState.Deleted)
                {
                    continue;
                }

                int? StratumIdSource = null;
                int StateClassIdSource = 0;
                int? StratumIdDest = null;
                int? StateClassIdDest = null;

                DTAnalyzer.GetDTFieldValues(dr, ref StratumIdSource, ref StateClassIdSource, ref StratumIdDest, ref StateClassIdDest);

                if (!analyzer.StateClassExists(StratumIdSource, StateClassIdSource))
                {
                    DataTableUtilities.DeleteTableRow(dt, dr);
                    HasChanges = true;

                    continue;
                }

                if (!StateClassIdDest.HasValue)
                {
                    continue;
                }

                if (!analyzer.CanResolveStateClass(StratumIdSource, StratumIdDest, StateClassIdDest.Value))
                {
                    DataTableUtilities.DeleteTableRow(dt, dr);
                    HasChanges = true;
                }
            }

            return HasChanges;
        }

#if DEBUG

        private void VALIDATE_DETERMINISTIC_TRANSITIONS()
        {
            DataTable dtdata = this.GetData();
            DTAnalyzer Analyzer = new DTAnalyzer(dtdata, this.Project);

            foreach (DataRow dr in dtdata.Rows)
            {
                if (dr.RowState == DataRowState.Deleted)
                {
                    continue;
                }

                int? StratumIdSource = null;
                int StateClassIdSource = 0;
                int? StratumIdDest = null;
                int? StateClassIdDest = null;

                DTAnalyzer.GetDTFieldValues(dr, ref StratumIdSource, ref StateClassIdSource, ref StratumIdDest, ref StateClassIdDest);

                if (StateClassIdDest.HasValue)
                {
                    Debug.Assert(Analyzer.CanResolveStateClass(StratumIdSource, StratumIdDest, StateClassIdDest.Value));
                }
            }
        }

        private void VALIDATE_PROBABILISTIC_TRANSITIONS()
        {
            DTAnalyzer Analyzer = new DTAnalyzer(this.GetDataSheet(Strings.DATASHEET_DT_NAME).GetData(), this.Project);
            DataTable ptdata = this.GetDataSheet(Strings.DATASHEET_PT_NAME).GetData();

            foreach (DataRow dr in ptdata.Rows)
            {
                if (dr.RowState == DataRowState.Deleted)
                {
                    continue;
                }

                int? StratumIdSource = null;
                int StateClassIdSource = 0;
                int? StratumIdDest = null;
                int? StateClassIdDest = null;

                DTAnalyzer.GetPTFieldValues(dr, ref StratumIdSource, ref StateClassIdSource, ref StratumIdDest, ref StateClassIdDest);
                Debug.Assert(Analyzer.CanResolveStateClass(StratumIdSource, StratumIdSource, StateClassIdSource));

                if (StateClassIdDest.HasValue)
                {
                    Debug.Assert(Analyzer.CanResolveStateClass(StratumIdSource, StratumIdDest, StateClassIdDest.Value));
                }
            }
        }

#endif
    }
}
