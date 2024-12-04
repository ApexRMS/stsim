// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using SyncroSim.Core.Forms;
using System.Globalization;

namespace SyncroSim.STSim
{
    internal class TSTReport : ExportTransformer
    {
        protected override void Export(string location, ExportType exportType)
        {
            this.InternalExport(location, exportType, true);
        }

        internal void InternalExport(string location, ExportType exportType, bool showMessage)
        {
            ExportColumnCollection columns = this.CreateColumnCollection();

            if (exportType == ExportType.ExcelFile)
            {
                this.ExportToExcel(location, columns, this.CreateReportQuery(false),"Time-Since-Transition");
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

            string PrimaryStratumLabel = null;
            string SecondaryStratumLabel = null;
            string TertiaryStratumLabel = null;
            string TimestepLabel = TerminologyUtilities.GetTimestepUnits(this.Project);

            TerminologyUtilities.GetStratumLabelTerminology(
                this.Project, ref PrimaryStratumLabel, ref SecondaryStratumLabel, ref TertiaryStratumLabel);

            c.Add(new ExportColumn("ScenarioId", "Scenario Id"));
            c.Add(new ExportColumn("ScenarioName", "Scenario"));
            c.Add(new ExportColumn("Iteration", "Iteration"));
            c.Add(new ExportColumn("Timestep", TimestepLabel));
            c.Add(new ExportColumn("Stratum", PrimaryStratumLabel));
            c.Add(new ExportColumn("SecondaryStratum", SecondaryStratumLabel));
            c.Add(new ExportColumn("TertiaryStratum", TertiaryStratumLabel));
            c.Add(new ExportColumn("TransitionGroup", "Transition Type/Group"));
            c.Add(new ExportColumn("TSTMin", "TST Min"));
            c.Add(new ExportColumn("TSTMax", "TST Max"));
            c.Add(new ExportColumn("Amount", "Total Value"));

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
                    "SELECT " + 
                    "stsim_OutputTST.ScenarioId, " +
                    "stsim_OutputTST.Iteration,  " +
                    "stsim_OutputTST.Timestep,  " +
                    "stsim_Stratum.Name AS Stratum,  " + 
                    "stsim_SecondaryStratum.Name AS SecondaryStratum,  " +
                    "stsim_TertiaryStratum.Name AS TertiaryStratum,  " + 
                    "stsim_TransitionGroup.Name as TransitionGroup, " +
                    "stsim_OutputTST.TSTMin, " + 
                    "stsim_OutputTST.TSTMax, " +
                    "stsim_OutputTST.Amount " +
                    "FROM stsim_OutputTST " + 
                    "INNER JOIN stsim_Stratum ON stsim_Stratum.StratumId = stsim_OutputTST.StratumId " +
                    "LEFT JOIN stsim_SecondaryStratum ON stsim_SecondaryStratum.SecondaryStratumId = stsim_OutputTST.SecondaryStratumId " +
                    "LEFT JOIN stsim_TertiaryStratum ON stsim_TertiaryStratum.TertiaryStratumId = stsim_OutputTST.TertiaryStratumId " +
                    "INNER JOIN stsim_TransitionGroup ON stsim_TransitionGroup.TransitionGroupId = stsim_OutputTST.TransitionGroupId " + 
                    "WHERE stsim_OutputTST.ScenarioId IN ({0})  " + 
                    "ORDER BY " +
                    "stsim_OutputTST.ScenarioId, " + 
                    "stsim_OutputTST.Iteration, " + 
                    "stsim_OutputTST.Timestep, " +
                    "stsim_Stratum.Name, " + 
                    "stsim_SecondaryStratum.Name, " + 
                    "stsim_TertiaryStratum.Name, " +
                    "stsim_TransitionGroup.Name, " +
                    "TSTMin, " + 
                    "TSTMax", 
                    ScenFilter);
            }
            else
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "SELECT " + 
                    "stsim_OutputTST.ScenarioId, " +
                    "core_Scenario.Name AS ScenarioName,  " + 
                    "stsim_OutputTST.Iteration,  " +
                    "stsim_OutputTST.Timestep,  " + 
                    "stsim_Stratum.Name AS Stratum,  " + 
                    "stsim_SecondaryStratum.Name AS SecondaryStratum,  " +
                    "stsim_TertiaryStratum.Name AS TertiaryStratum,  " + 
                    "stsim_TransitionGroup.Name as TransitionGroup, " + 
                    "stsim_OutputTST.TSTMin, " +
                    "stsim_OutputTST.TSTMax, " + 
                    "stsim_OutputTST.Amount " + 
                    "FROM stsim_OutputTST " +
                    "INNER JOIN core_Scenario ON core_Scenario.ScenarioId = stsim_OutputTST.ScenarioId " +
                    "INNER JOIN stsim_Stratum ON stsim_Stratum.StratumId = stsim_OutputTST.StratumId " +
                    "LEFT JOIN stsim_SecondaryStratum ON stsim_SecondaryStratum.SecondaryStratumId = stsim_OutputTST.SecondaryStratumId " +
                    "LEFT JOIN stsim_TertiaryStratum ON stsim_TertiaryStratum.TertiaryStratumId = stsim_OutputTST.TertiaryStratumId " +
                    "INNER JOIN stsim_TransitionGroup ON stsim_TransitionGroup.TransitionGroupId = stsim_OutputTST.TransitionGroupId " +
                    "WHERE stsim_OutputTST.ScenarioId IN ({0})  " + 
                    "ORDER BY " + "stsim_OutputTST.ScenarioId, " +
                    "core_Scenario.Name, " +
                    "stsim_OutputTST.Iteration, " + 
                    "stsim_OutputTST.Timestep, " + 
                    "stsim_Stratum.Name, " + 
                    "stsim_SecondaryStratum.Name, " +
                    "stsim_TertiaryStratum.Name, " + 
                    "stsim_TransitionGroup.Name, " +
                    "TSTMin, " +
                    "TSTMax", 
                    ScenFilter);
            }
        }
    }
}
