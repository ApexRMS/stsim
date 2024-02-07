// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

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
                this.ExportToExcel(location, columns, this.CreateReportQuery(false), WorksheetName);
            }
            else
            {
                columns.Remove("ScenarioName");
                this.ExportToCSVFile(location, columns, this.CreateReportQuery(true));

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
            string ScenFilter = this.ExportCreateActiveResultScenarioFilter();

            if (isCSV)
            {
                return string.Format(CultureInfo.InvariantCulture, 
                    "SELECT " + "stsim_OutputStratumTransition.ScenarioId, " + "stsim_OutputStratumTransition.Iteration,  " + 
                    "stsim_OutputStratumTransition.Timestep,  " + "stsim_Stratum.Name AS Stratum,  " + "stsim_SecondaryStratum.Name AS SecondaryStratum,  " + 
                    "stsim_TertiaryStratum.Name AS TertiaryStratum,  " + "stsim_TransitionGroup.Name as TransitionGroup, " + "stsim_OutputStratumTransition.AgeMin, " + 
                    "stsim_OutputStratumTransition.AgeMax, " + "stsim_OutputStratumTransition.Amount " + "FROM stsim_OutputStratumTransition " + 
                    "INNER JOIN stsim_Stratum ON stsim_Stratum.StratumId = stsim_OutputStratumTransition.StratumId " +
                    "LEFT JOIN stsim_SecondaryStratum ON stsim_SecondaryStratum.SecondaryStratumId = stsim_OutputStratumTransition.SecondaryStratumId " +
                    "LEFT JOIN stsim_TertiaryStratum ON stsim_TertiaryStratum.TertiaryStratumId = stsim_OutputStratumTransition.TertiaryStratumId " + 
                    "INNER JOIN stsim_TransitionGroup ON stsim_TransitionGroup.TransitionGroupId = stsim_OutputStratumTransition.TransitionGroupId " + 
                    "WHERE stsim_OutputStratumTransition.ScenarioId IN ({0})  " + "ORDER BY " + "stsim_OutputStratumTransition.ScenarioId, " + 
                    "stsim_OutputStratumTransition.Iteration, " + "stsim_OutputStratumTransition.Timestep, " + "stsim_Stratum.Name, " + 
                    "stsim_SecondaryStratum.Name, " + "stsim_TertiaryStratum.Name, " + "stsim_TransitionGroup.Name, " + "AgeMin, " + "AgeMax", ScenFilter);
            }
            else
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "SELECT " + "stsim_OutputStratumTransition.ScenarioId, " +
                    "core_Scenario.Name AS ScenarioName,  " + "stsim_OutputStratumTransition.Iteration,  " + "stsim_OutputStratumTransition.Timestep,  " + 
                    "stsim_Stratum.Name AS Stratum,  " + "stsim_SecondaryStratum.Name AS SecondaryStratum,  " + "stsim_TertiaryStratum.Name AS TertiaryStratum,  " + 
                    "stsim_TransitionGroup.Name as TransitionGroup, " + "stsim_OutputStratumTransition.AgeMin, " + "stsim_OutputStratumTransition.AgeMax, " + 
                    "stsim_OutputStratumTransition.Amount " + "FROM stsim_OutputStratumTransition " +
                    "INNER JOIN core_Scenario ON core_Scenario.ScenarioId = stsim_OutputStratumTransition.ScenarioId " + 
                    "INNER JOIN stsim_Stratum ON stsim_Stratum.StratumId = stsim_OutputStratumTransition.StratumId " + 
                    "LEFT JOIN stsim_SecondaryStratum ON stsim_SecondaryStratum.SecondaryStratumId = stsim_OutputStratumTransition.SecondaryStratumId " + 
                    "LEFT JOIN stsim_TertiaryStratum ON stsim_TertiaryStratum.TertiaryStratumId = stsim_OutputStratumTransition.TertiaryStratumId " + 
                    "INNER JOIN stsim_TransitionGroup ON stsim_TransitionGroup.TransitionGroupId = stsim_OutputStratumTransition.TransitionGroupId " +
                    "WHERE stsim_OutputStratumTransition.ScenarioId IN ({0})  " + "ORDER BY " + "stsim_OutputStratumTransition.ScenarioId, " + 
                    "core_Scenario.Name, " + "stsim_OutputStratumTransition.Iteration, " + "stsim_OutputStratumTransition.Timestep, " +
                    "stsim_Stratum.Name, " + "stsim_SecondaryStratum.Name, " + "stsim_TertiaryStratum.Name, " + "stsim_TransitionGroup.Name, " + 
                    "AgeMin, " + "AgeMax", ScenFilter);
            }
        }
    }
}
