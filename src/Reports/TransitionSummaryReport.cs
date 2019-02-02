// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using SyncroSim.Core.Forms;
using System.Globalization;

namespace SyncroSim.STSim
{
    internal class TransitionSummaryReport : ExportTransformer
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

            if (exportType == ExportType.ExcelFile)
            {
                string WorksheetName = string.Format(CultureInfo.InvariantCulture, "{0} by Transition Group", AmountLabel);
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
            c.Add(new ExportColumn("TransitionGroup", "Transition Group"));
            c.Add(new ExportColumn("AgeMin", "Age Min"));
            c.Add(new ExportColumn("AgeMax", "Age Max"));
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
                    "SELECT " + "STSim_OutputStratumTransition.ScenarioID, " + "STSim_OutputStratumTransition.Iteration,  " + 
                    "STSim_OutputStratumTransition.Timestep,  " + "STSim_Stratum.Name AS Stratum,  " + "STSim_SecondaryStratum.Name AS SecondaryStratum,  " + 
                    "STSim_TertiaryStratum.Name AS TertiaryStratum,  " + "STSim_TransitionGroup.Name as TransitionGroup, " + "STSim_OutputStratumTransition.AgeMin, " + 
                    "STSim_OutputStratumTransition.AgeMax, " + "STSim_OutputStratumTransition.Amount " + "FROM STSim_OutputStratumTransition " + 
                    "INNER JOIN STSim_Stratum ON STSim_Stratum.StratumID = STSim_OutputStratumTransition.StratumID " +
                    "LEFT JOIN STSim_SecondaryStratum ON STSim_SecondaryStratum.SecondaryStratumID = STSim_OutputStratumTransition.SecondaryStratumID " +
                    "LEFT JOIN STSim_TertiaryStratum ON STSim_TertiaryStratum.TertiaryStratumID = STSim_OutputStratumTransition.TertiaryStratumID " + 
                    "INNER JOIN STSim_TransitionGroup ON STSim_TransitionGroup.TransitionGroupID = STSim_OutputStratumTransition.TransitionGroupID " + 
                    "WHERE STSim_OutputStratumTransition.ScenarioID IN ({0})  " + "ORDER BY " + "STSim_OutputStratumTransition.ScenarioID, " + 
                    "STSim_OutputStratumTransition.Iteration, " + "STSim_OutputStratumTransition.Timestep, " + "STSim_Stratum.Name, " + 
                    "STSim_SecondaryStratum.Name, " + "STSim_TertiaryStratum.Name, " + "STSim_TransitionGroup.Name, " + "AgeMin, " + "AgeMax", ScenFilter);
            }
            else
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "SELECT " + "STSim_OutputStratumTransition.ScenarioID, " +
                    "SSim_Scenario.Name AS ScenarioName,  " + "STSim_OutputStratumTransition.Iteration,  " + "STSim_OutputStratumTransition.Timestep,  " + 
                    "STSim_Stratum.Name AS Stratum,  " + "STSim_SecondaryStratum.Name AS SecondaryStratum,  " + "STSim_TertiaryStratum.Name AS TertiaryStratum,  " + 
                    "STSim_TransitionGroup.Name as TransitionGroup, " + "STSim_OutputStratumTransition.AgeMin, " + "STSim_OutputStratumTransition.AgeMax, " + 
                    "STSim_OutputStratumTransition.Amount " + "FROM STSim_OutputStratumTransition " +
                    "INNER JOIN SSim_Scenario ON SSim_Scenario.ScenarioID = STSim_OutputStratumTransition.ScenarioID " + 
                    "INNER JOIN STSim_Stratum ON STSim_Stratum.StratumID = STSim_OutputStratumTransition.StratumID " + 
                    "LEFT JOIN STSim_SecondaryStratum ON STSim_SecondaryStratum.SecondaryStratumID = STSim_OutputStratumTransition.SecondaryStratumID " + 
                    "LEFT JOIN STSim_TertiaryStratum ON STSim_TertiaryStratum.TertiaryStratumID = STSim_OutputStratumTransition.TertiaryStratumID " + 
                    "INNER JOIN STSim_TransitionGroup ON STSim_TransitionGroup.TransitionGroupID = STSim_OutputStratumTransition.TransitionGroupID " +
                    "WHERE STSim_OutputStratumTransition.ScenarioID IN ({0})  " + "ORDER BY " + "STSim_OutputStratumTransition.ScenarioID, " + 
                    "SSim_Scenario.Name, " + "STSim_OutputStratumTransition.Iteration, " + "STSim_OutputStratumTransition.Timestep, " +
                    "STSim_Stratum.Name, " + "STSim_SecondaryStratum.Name, " + "STSim_TertiaryStratum.Name, " + "STSim_TransitionGroup.Name, " + 
                    "AgeMin, " + "AgeMax", ScenFilter);
            }
        }
    }
}
