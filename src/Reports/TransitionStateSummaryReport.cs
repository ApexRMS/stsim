// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

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

            c.Add(new ExportColumn("ScenarioID", "Scenario ID"));
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
                    "SELECT " + "stsim__OutputStratumTransitionState.ScenarioID, " + "stsim__OutputStratumTransitionState.Iteration,  " + 
                    "stsim__OutputStratumTransitionState.Timestep,  " + "ST1.Name AS Stratum,  " + "ST2.Name AS SecondaryStratum,  " + 
                    "ST3.Name AS TertiaryStratum,  " + "stsim__TransitionType.Name as TransitionType, " + "SC1.Name AS StateClass, " + 
                    "SC2.Name AS EndStateClass, " + "stsim__OutputStratumTransitionState.Amount " + "FROM stsim__OutputStratumTransitionState " + 
                    "INNER JOIN stsim__Stratum AS ST1 ON ST1.StratumID = stsim__OutputStratumTransitionState.StratumID " + 
                    "LEFT JOIN stsim__SecondaryStratum AS ST2 ON ST2.SecondaryStratumID = stsim__OutputStratumTransitionState.SecondaryStratumID " + 
                    "LEFT JOIN stsim__TertiaryStratum AS ST3 ON ST3.TertiaryStratumID = stsim__OutputStratumTransitionState.TertiaryStratumID " +
                    "INNER JOIN stsim__StateClass as SC1 ON SC1.StateClassID = stsim__OutputStratumTransitionState.StateClassID " +
                    "INNER JOIN stsim__StateClass as SC2 ON SC2.StateClassID = stsim__OutputStratumTransitionState.EndStateClassID " + 
                    "INNER JOIN stsim__TransitionType ON stsim__TransitionType.TransitionTypeID = stsim__OutputStratumTransitionState.TransitionTypeID " + 
                    "WHERE stsim__OutputStratumTransitionState.ScenarioID IN ({0})  " + "ORDER BY " + "stsim__OutputStratumTransitionState.ScenarioID, " + 
                    "stsim__OutputStratumTransitionState.Iteration, " + "stsim__OutputStratumTransitionState.Timestep, " + "ST1.Name, " + "ST2.Name, " + 
                    "ST3.Name, " + "SC1.Name, " + "SC2.Name, " + "stsim__TransitionType.Name", ScenFilter);
            }
            else
            {
                return string.Format(CultureInfo.InvariantCulture, 
                    "SELECT " + "stsim__OutputStratumTransitionState.ScenarioID, " + 
                    "system__Scenario.Name AS ScenarioName,  " + "stsim__OutputStratumTransitionState.Iteration,  " + 
                    "stsim__OutputStratumTransitionState.Timestep,  " + "ST1.Name AS Stratum,  " + "ST2.Name AS SecondaryStratum,  " + 
                    "ST3.Name AS TertiaryStratum,  " + "stsim__TransitionType.Name as TransitionType, " + "SC1.Name AS StateClass, " + 
                    "SC2.Name AS EndStateClass, " + "stsim__OutputStratumTransitionState.Amount " + "FROM stsim__OutputStratumTransitionState " +
                    "INNER JOIN system__Scenario ON system__Scenario.ScenarioID = stsim__OutputStratumTransitionState.ScenarioID " + 
                    "INNER JOIN stsim__Stratum AS ST1 ON ST1.StratumID = stsim__OutputStratumTransitionState.StratumID " + 
                    "LEFT JOIN stsim__SecondaryStratum AS ST2 ON ST2.SecondaryStratumID = stsim__OutputStratumTransitionState.SecondaryStratumID " +
                    "LEFT JOIN stsim__TertiaryStratum AS ST3 ON ST3.TertiaryStratumID = stsim__OutputStratumTransitionState.TertiaryStratumID " + 
                    "INNER JOIN stsim__StateClass as SC1 ON SC1.StateClassID = stsim__OutputStratumTransitionState.StateClassID " +
                    "INNER JOIN stsim__StateClass as SC2 ON SC2.StateClassID = stsim__OutputStratumTransitionState.EndStateClassID " + 
                    "INNER JOIN stsim__TransitionType ON stsim__TransitionType.TransitionTypeID = stsim__OutputStratumTransitionState.TransitionTypeID " +
                    "WHERE stsim__OutputStratumTransitionState.ScenarioID IN ({0})  " + "ORDER BY " + "stsim__OutputStratumTransitionState.ScenarioID, " +
                    "system__Scenario.Name, " + "stsim__OutputStratumTransitionState.Iteration, " + "stsim__OutputStratumTransitionState.Timestep, " + 
                    "ST1.Name, " + "ST2.Name, " + "ST3.Name, " + "SC1.Name, " + "SC2.Name, " + "stsim__TransitionType.Name", ScenFilter);
            }
        }
    }
}
