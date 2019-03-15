// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Data;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    internal class ProbabilisticTransitionDataSheet : DataSheet
    {
        protected override void OnDataSheetChanged(DataSheetMonitorEventArgs e)
        {
            base.OnDataSheetChanged(e);

            string Primary = null;
            string Secondary = null;
            string Tertiary = null;

            TerminologyUtilities.GetStratumLabelTerminology(e.DataSheet, ref Primary, ref Secondary, ref Tertiary);

            this.Columns[Strings.DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME].DisplayName = Primary;
            this.Columns[Strings.DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME].DisplayName = "To " + Primary;
        }

        public override void Validate(DataRow proposedRow, DataTransferMethod transferMethod)
        {
            base.Validate(proposedRow, transferMethod);

            DataTable dt = this.GetDataSheet(Strings.DATASHEET_DT_NAME).GetData();
            DTAnalyzer Analyzer = new DTAnalyzer(dt, this.Project);
            int? StratumIdSource = null;
            int StateClassIdSource = 0;
            int? StratumIdDest = null;
            int? StateClassIdDest = null;

            DTAnalyzer.GetPTFieldValues(proposedRow, ref StratumIdSource, ref StateClassIdSource, ref StratumIdDest, ref StateClassIdDest);

            if (!Analyzer.CanResolveStateClass(StratumIdSource, StratumIdSource, StateClassIdSource))
            {
                Analyzer.ThrowDataException(StateClassIdSource, false);
            }

            if (StateClassIdDest.HasValue)
            {
                if (!Analyzer.CanResolveStateClass(StratumIdSource, StratumIdDest, StateClassIdDest.Value))
                {
                    Analyzer.ThrowDataException(StateClassIdDest.Value, true);
                }
            }
        }

        public override void Validate(System.Data.DataTable proposedData, DataTransferMethod transferMethod)
        {
            base.Validate(proposedData, transferMethod);

            DataSheet DeterministicSheet = this.GetDataSheet(Strings.DATASHEET_DT_NAME);
            DTAnalyzer Analyzer = new DTAnalyzer(DeterministicSheet.GetData(), this.Project);

            const string IMPORT_ERROR = "Error importing transitions." + "\r\n" + "\r\n" + "Note that each probabilistic transition's source and destination state class must exist in " + "this scenario's deterministic transition records.   More information:" + "\r\n" + "\r\n";

            try
            {
                foreach (DataRow dr in proposedData.Rows)
                {
                    int? StratumIdSource = null;
                    int StateClassIdSource = 0;
                    int? StratumIdDest = null;
                    int? StateClassIdDest = null;

                    DTAnalyzer.GetPTFieldValues(dr, ref StratumIdSource, ref StateClassIdSource, ref StratumIdDest, ref StateClassIdDest);

                    if (!Analyzer.CanResolveStateClass(StratumIdSource, StratumIdSource, StateClassIdSource))
                    {
                        Analyzer.ThrowDataException(StateClassIdSource, false);
                    }

                    if (StateClassIdDest.HasValue)
                    {
                        if (!Analyzer.CanResolveStateClass(StratumIdSource, StratumIdDest, StateClassIdDest.Value))
                        {
                            Analyzer.ThrowDataException(StateClassIdDest.Value, true);
                        }
                    }
                }
            }
            catch (DataException ex)
            {
                throw new DataException(IMPORT_ERROR + ex.Message);
            }
        }
    }
}
