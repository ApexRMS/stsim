// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using SyncroSim.Core.Forms;
using System.Globalization;

namespace SyncroSim.STSim
{
    internal class TransitionStateSummaryReport : ExportTransformer
    {
        protected override void Export(string location, ExportType exportType)
        {
            this.InternalExport(location, exportType, true);
        }

        internal void InternalExport(string location, ExportType exportType, bool showMessage)
        {
            string AmountLabel = null;
            TerminologyUnit TermUnit = 0;
            DataSheet dsterm = this.Project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME);
            ExportColumnCollection columns = this.CreateColumnCollection();

            TerminologyUtilities.GetAmountLabelTerminology(dsterm, ref AmountLabel, ref TermUnit);

            string WorksheetName = string.Format(CultureInfo.InvariantCulture, "{0} by Transition and State", AmountLabel);

            if (exportType == ExportType.ExcelFile)
            {
                this.ExcelExport(location, columns, this.CreateReportQuery(false), WorksheetName);
            }
            else
            {
                columns.Remove("ScenarioName");
                this.CSVExport(location, columns, this.CreateReportQuery(true));

                if (showMessage)
                {
                    FormsUtilities.InformationMessageBox("Data saved to '{0}'.", location);
                }
            }
        }

        private ExportColumnCollection CreateColumnCollection()
        {
            ExportColumnCollection c = new ExportColumnCollection();

            string AmountLabel = null;
            string UnitsLabel = null;
            TerminologyUnit TermUnit = 0;
            string PrimaryStratumLabel = null;
            string SecondaryStratumLabel = null;
            string TertiaryStratumLabel = null;
            DataSheet dsterm = this.Project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME);
            string TimestepLabel = TerminologyUtilities.GetTimestepUnits(this.Project);

            TerminologyUtilities.GetAmountLabelTerminology(dsterm, ref AmountLabel, ref TermUnit);
            TerminologyUtilities.GetStratumLabelTerminology(dsterm, ref PrimaryStratumLabel, ref SecondaryStratumLabel, ref TertiaryStratumLabel);
            UnitsLabel = TerminologyUtilities.TerminologyUnitToString(TermUnit);

            string AmountTitle = string.Format(CultureInfo.InvariantCulture, "{0} ({1})", AmountLabel, UnitsLabel);

            c.Add(new ExportColumn("ScenarioId", "Scenario Id"));
            c.Add(new ExportColumn("ScenarioName", "Scenario"));
            c.Add(new ExportColumn("Iteration", "Iteration"));
            c.Add(new ExportColumn("Timestep", TimestepLabel));
            c.Add(new ExportColumn("Stratum", PrimaryStratumLabel));
            c.Add(new ExportColumn("SecondaryStratum", SecondaryStratumLabel));
            c.Add(new ExportColumn("TertiaryStratum", TertiaryStratumLabel));
            c.Add(new ExportColumn("TransitionType", "Transition Type"));
            c.Add(new ExportColumn("StateClass", "State Class"));
            c.Add(new ExportColumn("EndStateClass", "End State Class"));
            c.Add(new ExportColumn("Amount", AmountTitle));

            c["Amount"].DecimalPlaces = 2;
            c["Amount"].Alignment = ColumnAlignment.Right;

            return c;
        }

        private string CreateReportQuery(bool isCSV)
        {
            string ScenFilter = this.CreateActiveResultScenarioFilter();

            if (isCSV)
            {
                return string.Format(CultureInfo.InvariantCulture, 
                    "SELECT " + "stsim_OutputStratumTransitionState.ScenarioId, " + "stsim_OutputStratumTransitionState.Iteration,  " + 
                    "stsim_OutputStratumTransitionState.Timestep,  " + "ST1.Name AS Stratum,  " + "ST2.Name AS SecondaryStratum,  " + 
                    "ST3.Name AS TertiaryStratum,  " + "stsim_TransitionType.Name as TransitionType, " + "SC1.Name AS StateClass, " + 
                    "SC2.Name AS EndStateClass, " + "stsim_OutputStratumTransitionState.Amount " + "FROM stsim_OutputStratumTransitionState " + 
                    "INNER JOIN stsim_Stratum AS ST1 ON ST1.StratumId = stsim_OutputStratumTransitionState.StratumId " + 
                    "LEFT JOIN stsim_SecondaryStratum AS ST2 ON ST2.SecondaryStratumId = stsim_OutputStratumTransitionState.SecondaryStratumId " + 
                    "LEFT JOIN stsim_TertiaryStratum AS ST3 ON ST3.TertiaryStratumId = stsim_OutputStratumTransitionState.TertiaryStratumId " +
                    "INNER JOIN stsim_StateClass as SC1 ON SC1.StateClassId = stsim_OutputStratumTransitionState.StateClassId " +
                    "INNER JOIN stsim_StateClass as SC2 ON SC2.StateClassId = stsim_OutputStratumTransitionState.EndStateClassId " + 
                    "INNER JOIN stsim_TransitionType ON stsim_TransitionType.TransitionTypeId = stsim_OutputStratumTransitionState.TransitionTypeId " + 
                    "WHERE stsim_OutputStratumTransitionState.ScenarioId IN ({0})  " + "ORDER BY " + "stsim_OutputStratumTransitionState.ScenarioId, " + 
                    "stsim_OutputStratumTransitionState.Iteration, " + "stsim_OutputStratumTransitionState.Timestep, " + "ST1.Name, " + "ST2.Name, " + 
                    "ST3.Name, " + "SC1.Name, " + "SC2.Name, " + "stsim_TransitionType.Name", ScenFilter);
            }
            else
            {
                return string.Format(CultureInfo.InvariantCulture, 
                    "SELECT " + "stsim_OutputStratumTransitionState.ScenarioId, " + 
                    "core_Scenario.Name AS ScenarioName,  " + "stsim_OutputStratumTransitionState.Iteration,  " + 
                    "stsim_OutputStratumTransitionState.Timestep,  " + "ST1.Name AS Stratum,  " + "ST2.Name AS SecondaryStratum,  " + 
                    "ST3.Name AS TertiaryStratum,  " + "stsim_TransitionType.Name as TransitionType, " + "SC1.Name AS StateClass, " + 
                    "SC2.Name AS EndStateClass, " + "stsim_OutputStratumTransitionState.Amount " + "FROM stsim_OutputStratumTransitionState " +
                    "INNER JOIN core_Scenario ON core_Scenario.ScenarioId = stsim_OutputStratumTransitionState.ScenarioId " + 
                    "INNER JOIN stsim_Stratum AS ST1 ON ST1.StratumId = stsim_OutputStratumTransitionState.StratumId " + 
                    "LEFT JOIN stsim_SecondaryStratum AS ST2 ON ST2.SecondaryStratumId = stsim_OutputStratumTransitionState.SecondaryStratumId " +
                    "LEFT JOIN stsim_TertiaryStratum AS ST3 ON ST3.TertiaryStratumId = stsim_OutputStratumTransitionState.TertiaryStratumId " + 
                    "INNER JOIN stsim_StateClass as SC1 ON SC1.StateClassId = stsim_OutputStratumTransitionState.StateClassId " +
                    "INNER JOIN stsim_StateClass as SC2 ON SC2.StateClassId = stsim_OutputStratumTransitionState.EndStateClassId " + 
                    "INNER JOIN stsim_TransitionType ON stsim_TransitionType.TransitionTypeId = stsim_OutputStratumTransitionState.TransitionTypeId " +
                    "WHERE stsim_OutputStratumTransitionState.ScenarioId IN ({0})  " + "ORDER BY " + "stsim_OutputStratumTransitionState.ScenarioId, " +
                    "core_Scenario.Name, " + "stsim_OutputStratumTransitionState.Iteration, " + "stsim_OutputStratumTransitionState.Timestep, " + 
                    "ST1.Name, " + "ST2.Name, " + "ST3.Name, " + "SC1.Name, " + "SC2.Name, " + "stsim_TransitionType.Name", ScenFilter);
            }
        }
    }
}
