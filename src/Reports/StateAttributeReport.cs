// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

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
                this.ExcelExport(location, columns, this.CreateReportQuery(false), "State Based Attributes");
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

            TerminologyUtilities.GetStratumLabelTerminology(dsterm, ref PrimaryStratumLabel, ref SecondaryStratumLabel, ref TertiaryStratumLabel);

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
                    "SELECT " + "stsim__OutputStateAttribute.ScenarioID, " + "stsim__OutputStateAttribute.Iteration,  " + 
                    "stsim__OutputStateAttribute.Timestep,  " + "stsim__Stratum.Name AS Stratum,  " + "stsim__SecondaryStratum.Name AS SecondaryStratum,  " +
                    "stsim__TertiaryStratum.Name AS TertiaryStratum,  " + "stsim__StateAttributeType.Name as AttributeType, " + 
                    "stsim__OutputStateAttribute.AgeMin, " + "stsim__OutputStateAttribute.AgeMax, " + "stsim__OutputStateAttribute.Amount " + 
                    "FROM stsim__OutputStateAttribute " + "INNER JOIN stsim__Stratum ON stsim__Stratum.StratumID = stsim__OutputStateAttribute.StratumID " + 
                    "LEFT JOIN stsim__SecondaryStratum ON stsim__SecondaryStratum.SecondaryStratumID = stsim__OutputStateAttribute.SecondaryStratumID " +
                    "LEFT JOIN stsim__TertiaryStratum ON stsim__TertiaryStratum.TertiaryStratumID = stsim__OutputStateAttribute.TertiaryStratumID " +
                    "INNER JOIN stsim__StateAttributeType ON stsim__StateAttributeType.StateAttributeTypeID = stsim__OutputStateAttribute.StateAttributeTypeID " + "WHERE stsim__OutputStateAttribute.ScenarioID IN ({0})  " + "ORDER BY " + 
                    "stsim__OutputStateAttribute.ScenarioID, " + "stsim__OutputStateAttribute.Iteration, " + "stsim__OutputStateAttribute.Timestep, " + 
                    "stsim__Stratum.Name, " + "stsim__SecondaryStratum.Name, " + "stsim__TertiaryStratum.Name, " + "stsim__StateAttributeType.Name, " +
                    "AgeMin, " + "AgeMax", ScenFilter);
            }
            else
            {
                return string.Format(CultureInfo.InvariantCulture, 
                    "SELECT " + "stsim__OutputStateAttribute.ScenarioID, " + "system__Scenario.Name AS ScenarioName,  " + "stsim__OutputStateAttribute.Iteration,  " + 
                    "stsim__OutputStateAttribute.Timestep,  " + "stsim__Stratum.Name AS Stratum,  " + "stsim__SecondaryStratum.Name AS SecondaryStratum,  " + 
                    "stsim__TertiaryStratum.Name AS TertiaryStratum,  " + "stsim__StateAttributeType.Name as AttributeType, " + "stsim__OutputStateAttribute.AgeMin, " + 
                    "stsim__OutputStateAttribute.AgeMax, " + "stsim__OutputStateAttribute.Amount " + "FROM stsim__OutputStateAttribute " + 
                    "INNER JOIN system__Scenario ON system__Scenario.ScenarioID = stsim__OutputStateAttribute.ScenarioID " + 
                    "INNER JOIN stsim__Stratum ON stsim__Stratum.StratumID = stsim__OutputStateAttribute.StratumID " + 
                    "LEFT JOIN stsim__SecondaryStratum ON stsim__SecondaryStratum.SecondaryStratumID = stsim__OutputStateAttribute.SecondaryStratumID " + 
                    "LEFT JOIN stsim__TertiaryStratum ON stsim__TertiaryStratum.TertiaryStratumID = stsim__OutputStateAttribute.TertiaryStratumID " + 
                    "INNER JOIN stsim__StateAttributeType ON stsim__StateAttributeType.StateAttributeTypeID = stsim__OutputStateAttribute.StateAttributeTypeID " + 
                    "WHERE stsim__OutputStateAttribute.ScenarioID IN ({0})  " + "ORDER BY " + "stsim__OutputStateAttribute.ScenarioID, " + "system__Scenario.Name, " +
                    "stsim__OutputStateAttribute.Iteration, " + "stsim__OutputStateAttribute.Timestep, " + "stsim__Stratum.Name, " + "stsim__SecondaryStratum.Name, " +
                    "stsim__TertiaryStratum.Name, " + "stsim__StateAttributeType.Name, " + "AgeMin, " + "AgeMax", ScenFilter);
            }
        }
    }
}
