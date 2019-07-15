// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

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
                    "SELECT " + "stsim__OutputStratumTransition.ScenarioID, " + "stsim__OutputStratumTransition.Iteration,  " + 
                    "stsim__OutputStratumTransition.Timestep,  " + "stsim__Stratum.Name AS Stratum,  " + "stsim__SecondaryStratum.Name AS SecondaryStratum,  " + 
                    "stsim__TertiaryStratum.Name AS TertiaryStratum,  " + "stsim__TransitionGroup.Name as TransitionGroup, " + "stsim__OutputStratumTransition.AgeMin, " + 
                    "stsim__OutputStratumTransition.AgeMax, " + "stsim__OutputStratumTransition.Amount " + "FROM stsim__OutputStratumTransition " + 
                    "INNER JOIN stsim__Stratum ON stsim__Stratum.StratumID = stsim__OutputStratumTransition.StratumID " +
                    "LEFT JOIN stsim__SecondaryStratum ON stsim__SecondaryStratum.SecondaryStratumID = stsim__OutputStratumTransition.SecondaryStratumID " +
                    "LEFT JOIN stsim__TertiaryStratum ON stsim__TertiaryStratum.TertiaryStratumID = stsim__OutputStratumTransition.TertiaryStratumID " + 
                    "INNER JOIN stsim__TransitionGroup ON stsim__TransitionGroup.TransitionGroupID = stsim__OutputStratumTransition.TransitionGroupID " + 
                    "WHERE stsim__OutputStratumTransition.ScenarioID IN ({0})  " + "ORDER BY " + "stsim__OutputStratumTransition.ScenarioID, " + 
                    "stsim__OutputStratumTransition.Iteration, " + "stsim__OutputStratumTransition.Timestep, " + "stsim__Stratum.Name, " + 
                    "stsim__SecondaryStratum.Name, " + "stsim__TertiaryStratum.Name, " + "stsim__TransitionGroup.Name, " + "AgeMin, " + "AgeMax", ScenFilter);
            }
            else
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "SELECT " + "stsim__OutputStratumTransition.ScenarioID, " +
                    "system__Scenario.Name AS ScenarioName,  " + "stsim__OutputStratumTransition.Iteration,  " + "stsim__OutputStratumTransition.Timestep,  " + 
                    "stsim__Stratum.Name AS Stratum,  " + "stsim__SecondaryStratum.Name AS SecondaryStratum,  " + "stsim__TertiaryStratum.Name AS TertiaryStratum,  " + 
                    "stsim__TransitionGroup.Name as TransitionGroup, " + "stsim__OutputStratumTransition.AgeMin, " + "stsim__OutputStratumTransition.AgeMax, " + 
                    "stsim__OutputStratumTransition.Amount " + "FROM stsim__OutputStratumTransition " +
                    "INNER JOIN system__Scenario ON system__Scenario.ScenarioID = stsim__OutputStratumTransition.ScenarioID " + 
                    "INNER JOIN stsim__Stratum ON stsim__Stratum.StratumID = stsim__OutputStratumTransition.StratumID " + 
                    "LEFT JOIN stsim__SecondaryStratum ON stsim__SecondaryStratum.SecondaryStratumID = stsim__OutputStratumTransition.SecondaryStratumID " + 
                    "LEFT JOIN stsim__TertiaryStratum ON stsim__TertiaryStratum.TertiaryStratumID = stsim__OutputStratumTransition.TertiaryStratumID " + 
                    "INNER JOIN stsim__TransitionGroup ON stsim__TransitionGroup.TransitionGroupID = stsim__OutputStratumTransition.TransitionGroupID " +
                    "WHERE stsim__OutputStratumTransition.ScenarioID IN ({0})  " + "ORDER BY " + "stsim__OutputStratumTransition.ScenarioID, " + 
                    "system__Scenario.Name, " + "stsim__OutputStratumTransition.Iteration, " + "stsim__OutputStratumTransition.Timestep, " +
                    "stsim__Stratum.Name, " + "stsim__SecondaryStratum.Name, " + "stsim__TertiaryStratum.Name, " + "stsim__TransitionGroup.Name, " + 
                    "AgeMin, " + "AgeMax", ScenFilter);
            }
        }
    }
}
