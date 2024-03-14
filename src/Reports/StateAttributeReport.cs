// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using SyncroSim.Core.Forms;
using System.Globalization;

namespace SyncroSim.STSim
{
    internal class StateAttributeReport : ExportTransformer
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
                this.ExportToExcel(location, columns, this.CreateReportQuery(false), "State Based Attributes");
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
            DataSheet dsterm = this.Project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME);
            string TimestepLabel = TerminologyUtilities.GetTimestepUnits(this.Project);

            TerminologyUtilities.GetStratumLabelTerminology(dsterm, ref PrimaryStratumLabel, ref SecondaryStratumLabel, ref TertiaryStratumLabel);

            c.Add(new ExportColumn("ScenarioId", "Scenario Id"));
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
            string ScenFilter = this.ExportCreateActiveResultScenarioFilter();

            if (isCSV)
            {
                return string.Format(CultureInfo.InvariantCulture, 
                    "SELECT " + "stsim_OutputStateAttribute.ScenarioId, " + "stsim_OutputStateAttribute.Iteration,  " + 
                    "stsim_OutputStateAttribute.Timestep,  " + "stsim_Stratum.Name AS Stratum,  " + "stsim_SecondaryStratum.Name AS SecondaryStratum,  " +
                    "stsim_TertiaryStratum.Name AS TertiaryStratum,  " + "stsim_StateAttributeType.Name as AttributeType, " + 
                    "stsim_OutputStateAttribute.AgeMin, " + "stsim_OutputStateAttribute.AgeMax, " + "stsim_OutputStateAttribute.Amount " + 
                    "FROM stsim_OutputStateAttribute " + "INNER JOIN stsim_Stratum ON stsim_Stratum.StratumId = stsim_OutputStateAttribute.StratumId " + 
                    "LEFT JOIN stsim_SecondaryStratum ON stsim_SecondaryStratum.SecondaryStratumId = stsim_OutputStateAttribute.SecondaryStratumId " +
                    "LEFT JOIN stsim_TertiaryStratum ON stsim_TertiaryStratum.TertiaryStratumId = stsim_OutputStateAttribute.TertiaryStratumId " +
                    "INNER JOIN stsim_StateAttributeType ON stsim_StateAttributeType.StateAttributeTypeId = stsim_OutputStateAttribute.StateAttributeTypeId " + "WHERE stsim_OutputStateAttribute.ScenarioId IN ({0})  " + "ORDER BY " + 
                    "stsim_OutputStateAttribute.ScenarioId, " + "stsim_OutputStateAttribute.Iteration, " + "stsim_OutputStateAttribute.Timestep, " + 
                    "stsim_Stratum.Name, " + "stsim_SecondaryStratum.Name, " + "stsim_TertiaryStratum.Name, " + "stsim_StateAttributeType.Name, " +
                    "AgeMin, " + "AgeMax", ScenFilter);
            }
            else
            {
                return string.Format(CultureInfo.InvariantCulture, 
                    "SELECT " + "stsim_OutputStateAttribute.ScenarioId, " + "core_Scenario.Name AS ScenarioName,  " + "stsim_OutputStateAttribute.Iteration,  " + 
                    "stsim_OutputStateAttribute.Timestep,  " + "stsim_Stratum.Name AS Stratum,  " + "stsim_SecondaryStratum.Name AS SecondaryStratum,  " + 
                    "stsim_TertiaryStratum.Name AS TertiaryStratum,  " + "stsim_StateAttributeType.Name as AttributeType, " + "stsim_OutputStateAttribute.AgeMin, " + 
                    "stsim_OutputStateAttribute.AgeMax, " + "stsim_OutputStateAttribute.Amount " + "FROM stsim_OutputStateAttribute " + 
                    "INNER JOIN core_Scenario ON core_Scenario.ScenarioId = stsim_OutputStateAttribute.ScenarioId " + 
                    "INNER JOIN stsim_Stratum ON stsim_Stratum.StratumId = stsim_OutputStateAttribute.StratumId " + 
                    "LEFT JOIN stsim_SecondaryStratum ON stsim_SecondaryStratum.SecondaryStratumId = stsim_OutputStateAttribute.SecondaryStratumId " + 
                    "LEFT JOIN stsim_TertiaryStratum ON stsim_TertiaryStratum.TertiaryStratumId = stsim_OutputStateAttribute.TertiaryStratumId " + 
                    "INNER JOIN stsim_StateAttributeType ON stsim_StateAttributeType.StateAttributeTypeId = stsim_OutputStateAttribute.StateAttributeTypeId " + 
                    "WHERE stsim_OutputStateAttribute.ScenarioId IN ({0})  " + "ORDER BY " + "stsim_OutputStateAttribute.ScenarioId, " + "core_Scenario.Name, " +
                    "stsim_OutputStateAttribute.Iteration, " + "stsim_OutputStateAttribute.Timestep, " + "stsim_Stratum.Name, " + "stsim_SecondaryStratum.Name, " +
                    "stsim_TertiaryStratum.Name, " + "stsim_StateAttributeType.Name, " + "AgeMin, " + "AgeMax", ScenFilter);
            }
        }
    }
}
