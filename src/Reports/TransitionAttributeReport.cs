// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using SyncroSim.Core.Forms;
using System.Globalization;

namespace SyncroSim.STSim
{
    internal class TransitionAttributeReport : ExportTransformer
    {
        protected override void Export(string location, ExportType exportType)
        {
            this.InternalExport(location, exportType, true);
        }

        internal void InternalExport(string location, ExportType exportType, bool showMessage)
        {
            ExportColumnCollection columns = this.CreateColumnCollection();

            if (exportType ==ExportType.ExcelFile)
            {
                this.ExcelExport(location, columns, this.CreateReportQuery(false), "Transition Based Attributes");
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

            string PrimaryStratumLabel = null;
            string SecondaryStratumLabel = null;
            string TertiaryStratumLabel = null;
            DataSheet dsterm = this.Project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME);
            string TimestepLabel = TerminologyUtilities.GetTimestepUnits(this.Project);

            TerminologyUtilities.GetStratumLabelTerminology(
                dsterm, ref PrimaryStratumLabel, ref SecondaryStratumLabel, ref TertiaryStratumLabel);

            c.Add(new ExportColumn("ScenarioID", "Scenario ID"));
            c.Add(new ExportColumn("ScenarioName", "Scenario"));
            c.Add(new ExportColumn("Iteration", "Iteration"));
            c.Add(new ExportColumn("Timestep", TimestepLabel));
            c.Add(new ExportColumn("Stratum", PrimaryStratumLabel));
            c.Add(new ExportColumn("SecondaryStratum", SecondaryStratumLabel));
            c.Add(new ExportColumn("TertiaryStratum", TertiaryStratumLabel));
            c.Add(new ExportColumn("AttributeType", "Attribute"));
            c.Add(new ExportColumn("AgeMin", "Age Min"));
            c.Add(new ExportColumn("AgeMax", "Age Max"));
            c.Add(new ExportColumn("Amount", "Total Value"));

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
                    "SELECT " + "stsim_OutputTransitionAttribute.ScenarioID, " + "stsim_OutputTransitionAttribute.Iteration,  " + 
                    "stsim_OutputTransitionAttribute.Timestep,  " + "stsim_Stratum.Name AS Stratum,  " + "stsim_SecondaryStratum.Name AS SecondaryStratum,  " +
                    "stsim_TertiaryStratum.Name AS TertiaryStratum,  " + "stsim_TransitionAttributeType.Name as AttributeType, " + "stsim_OutputTransitionAttribute.AgeMin, " + 
                    "stsim_OutputTransitionAttribute.AgeMax, " + "stsim_OutputTransitionAttribute.Amount " + "FROM stsim_OutputTransitionAttribute " + 
                    "INNER JOIN stsim_Stratum ON stsim_Stratum.StratumID = stsim_OutputTransitionAttribute.StratumID " +
                    "LEFT JOIN stsim_SecondaryStratum ON stsim_SecondaryStratum.SecondaryStratumID = stsim_OutputTransitionAttribute.SecondaryStratumID " + 
                    "LEFT JOIN stsim_TertiaryStratum ON stsim_TertiaryStratum.TertiaryStratumID = stsim_OutputTransitionAttribute.TertiaryStratumID " + 
                    "INNER JOIN stsim_TransitionAttributeType ON stsim_TransitionAttributeType.TransitionAttributeTypeID = stsim_OutputTransitionAttribute.TransitionAttributeTypeID " +
                    "WHERE stsim_OutputTransitionAttribute.ScenarioID IN ({0})  " + "ORDER BY " + "stsim_OutputTransitionAttribute.ScenarioID, " + 
                    "stsim_OutputTransitionAttribute.Iteration, " + "stsim_OutputTransitionAttribute.Timestep, " + "stsim_Stratum.Name, " + "stsim_SecondaryStratum.Name, " +
                    "stsim_TertiaryStratum.Name, " + "stsim_TransitionAttributeType.Name, " + "AgeMin, " + "AgeMax", ScenFilter);
            }
            else
            {
                return string.Format(CultureInfo.InvariantCulture, 
                    "SELECT " + "stsim_OutputTransitionAttribute.ScenarioID, " + "core_Scenario.Name AS ScenarioName,  " + 
                    "stsim_OutputTransitionAttribute.Iteration,  " + "stsim_OutputTransitionAttribute.Timestep,  " + "stsim_Stratum.Name AS Stratum,  " + 
                    "stsim_SecondaryStratum.Name AS SecondaryStratum,  " + "stsim_TertiaryStratum.Name AS TertiaryStratum,  " + "stsim_TransitionAttributeType.Name as AttributeType, " +
                    "stsim_OutputTransitionAttribute.AgeMin, " + "stsim_OutputTransitionAttribute.AgeMax, " + "stsim_OutputTransitionAttribute.Amount " + 
                    "FROM stsim_OutputTransitionAttribute " + "INNER JOIN core_Scenario ON core_Scenario.ScenarioID = stsim_OutputTransitionAttribute.ScenarioID " +
                    "INNER JOIN stsim_Stratum ON stsim_Stratum.StratumID = stsim_OutputTransitionAttribute.StratumID " + 
                    "LEFT JOIN stsim_SecondaryStratum ON stsim_SecondaryStratum.SecondaryStratumID = stsim_OutputTransitionAttribute.SecondaryStratumID " + 
                    "LEFT JOIN stsim_TertiaryStratum ON stsim_TertiaryStratum.TertiaryStratumID = stsim_OutputTransitionAttribute.TertiaryStratumID " + 
                    "INNER JOIN stsim_TransitionAttributeType ON stsim_TransitionAttributeType.TransitionAttributeTypeID = stsim_OutputTransitionAttribute.TransitionAttributeTypeID " + 
                    "WHERE stsim_OutputTransitionAttribute.ScenarioID IN ({0})  " + "ORDER BY " + "stsim_OutputTransitionAttribute.ScenarioID, " + "core_Scenario.Name, " + 
                    "stsim_OutputTransitionAttribute.Iteration, " + "stsim_OutputTransitionAttribute.Timestep, " + "stsim_Stratum.Name, " + "stsim_SecondaryStratum.Name, " +
                    "stsim_TertiaryStratum.Name, " + "stsim_TransitionAttributeType.Name, " + "AgeMin, " + "AgeMax", ScenFilter);
            }
        }
    }
}
