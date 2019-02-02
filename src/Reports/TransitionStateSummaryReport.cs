// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

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
                    "SELECT " + "STSim_OutputStratumTransitionState.ScenarioID, " + "STSim_OutputStratumTransitionState.Iteration,  " + 
                    "STSim_OutputStratumTransitionState.Timestep,  " + "ST1.Name AS Stratum,  " + "ST2.Name AS SecondaryStratum,  " + 
                    "ST3.Name AS TertiaryStratum,  " + "STSim_TransitionType.Name as TransitionType, " + "SC1.Name AS StateClass, " + 
                    "SC2.Name AS EndStateClass, " + "STSim_OutputStratumTransitionState.Amount " + "FROM STSim_OutputStratumTransitionState " + 
                    "INNER JOIN STSim_Stratum AS ST1 ON ST1.StratumID = STSim_OutputStratumTransitionState.StratumID " + 
                    "LEFT JOIN STSim_SecondaryStratum AS ST2 ON ST2.SecondaryStratumID = STSim_OutputStratumTransitionState.SecondaryStratumID " + 
                    "LEFT JOIN STSim_TertiaryStratum AS ST3 ON ST3.TertiaryStratumID = STSim_OutputStratumTransitionState.TertiaryStratumID " +
                    "INNER JOIN STSim_StateClass as SC1 ON SC1.StateClassID = STSim_OutputStratumTransitionState.StateClassID " +
                    "INNER JOIN STSim_StateClass as SC2 ON SC2.StateClassID = STSim_OutputStratumTransitionState.EndStateClassID " + 
                    "INNER JOIN STSim_TransitionType ON STSim_TransitionType.TransitionTypeID = STSim_OutputStratumTransitionState.TransitionTypeID " + 
                    "WHERE STSim_OutputStratumTransitionState.ScenarioID IN ({0})  " + "ORDER BY " + "STSim_OutputStratumTransitionState.ScenarioID, " + 
                    "STSim_OutputStratumTransitionState.Iteration, " + "STSim_OutputStratumTransitionState.Timestep, " + "ST1.Name, " + "ST2.Name, " + 
                    "ST3.Name, " + "SC1.Name, " + "SC2.Name, " + "STSim_TransitionType.Name", ScenFilter);
            }
            else
            {
                return string.Format(CultureInfo.InvariantCulture, 
                    "SELECT " + "STSim_OutputStratumTransitionState.ScenarioID, " + 
                    "SSim_Scenario.Name AS ScenarioName,  " + "STSim_OutputStratumTransitionState.Iteration,  " + 
                    "STSim_OutputStratumTransitionState.Timestep,  " + "ST1.Name AS Stratum,  " + "ST2.Name AS SecondaryStratum,  " + 
                    "ST3.Name AS TertiaryStratum,  " + "STSim_TransitionType.Name as TransitionType, " + "SC1.Name AS StateClass, " + 
                    "SC2.Name AS EndStateClass, " + "STSim_OutputStratumTransitionState.Amount " + "FROM STSim_OutputStratumTransitionState " +
                    "INNER JOIN SSim_Scenario ON SSim_Scenario.ScenarioID = STSim_OutputStratumTransitionState.ScenarioID " + 
                    "INNER JOIN STSim_Stratum AS ST1 ON ST1.StratumID = STSim_OutputStratumTransitionState.StratumID " + 
                    "LEFT JOIN STSim_SecondaryStratum AS ST2 ON ST2.SecondaryStratumID = STSim_OutputStratumTransitionState.SecondaryStratumID " +
                    "LEFT JOIN STSim_TertiaryStratum AS ST3 ON ST3.TertiaryStratumID = STSim_OutputStratumTransitionState.TertiaryStratumID " + 
                    "INNER JOIN STSim_StateClass as SC1 ON SC1.StateClassID = STSim_OutputStratumTransitionState.StateClassID " +
                    "INNER JOIN STSim_StateClass as SC2 ON SC2.StateClassID = STSim_OutputStratumTransitionState.EndStateClassID " + 
                    "INNER JOIN STSim_TransitionType ON STSim_TransitionType.TransitionTypeID = STSim_OutputStratumTransitionState.TransitionTypeID " +
                    "WHERE STSim_OutputStratumTransitionState.ScenarioID IN ({0})  " + "ORDER BY " + "STSim_OutputStratumTransitionState.ScenarioID, " +
                    "SSim_Scenario.Name, " + "STSim_OutputStratumTransitionState.Iteration, " + "STSim_OutputStratumTransitionState.Timestep, " + 
                    "ST1.Name, " + "ST2.Name, " + "ST3.Name, " + "SC1.Name, " + "SC2.Name, " + "STSim_TransitionType.Name", ScenFilter);
            }
        }
    }
}
