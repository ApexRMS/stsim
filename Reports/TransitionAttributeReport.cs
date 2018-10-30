// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using SyncroSim.Core.Forms;
using System.Reflection;
using System.Globalization;

namespace SyncroSim.STSim
{
    [ObfuscationAttribute(Exclude=true, ApplyToMembers=false)]
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
                    "SELECT " + "STSim_OutputTransitionAttribute.ScenarioID, " + "STSim_OutputTransitionAttribute.Iteration,  " + 
                    "STSim_OutputTransitionAttribute.Timestep,  " + "STSim_Stratum.Name AS Stratum,  " + "STSim_SecondaryStratum.Name AS SecondaryStratum,  " +
                    "STSim_TertiaryStratum.Name AS TertiaryStratum,  " + "STSim_TransitionAttributeType.Name as AttributeType, " + "STSim_OutputTransitionAttribute.AgeMin, " + 
                    "STSim_OutputTransitionAttribute.AgeMax, " + "STSim_OutputTransitionAttribute.Amount " + "FROM STSim_OutputTransitionAttribute " + 
                    "INNER JOIN STSim_Stratum ON STSim_Stratum.StratumID = STSim_OutputTransitionAttribute.StratumID " +
                    "LEFT JOIN STSim_SecondaryStratum ON STSim_SecondaryStratum.SecondaryStratumID = STSim_OutputTransitionAttribute.SecondaryStratumID " + 
                    "LEFT JOIN STSim_TertiaryStratum ON STSim_TertiaryStratum.TertiaryStratumID = STSim_OutputTransitionAttribute.TertiaryStratumID " + 
                    "INNER JOIN STSim_TransitionAttributeType ON STSim_TransitionAttributeType.TransitionAttributeTypeID = STSim_OutputTransitionAttribute.TransitionAttributeTypeID " +
                    "WHERE STSim_OutputTransitionAttribute.ScenarioID IN ({0})  " + "ORDER BY " + "STSim_OutputTransitionAttribute.ScenarioID, " + 
                    "STSim_OutputTransitionAttribute.Iteration, " + "STSim_OutputTransitionAttribute.Timestep, " + "STSim_Stratum.Name, " + "STSim_SecondaryStratum.Name, " +
                    "STSim_TertiaryStratum.Name, " + "STSim_TransitionAttributeType.Name, " + "AgeMin, " + "AgeMax", ScenFilter);
            }
            else
            {
                return string.Format(CultureInfo.InvariantCulture, 
                    "SELECT " + "STSim_OutputTransitionAttribute.ScenarioID, " + "SSim_Scenario.Name AS ScenarioName,  " + 
                    "STSim_OutputTransitionAttribute.Iteration,  " + "STSim_OutputTransitionAttribute.Timestep,  " + "STSim_Stratum.Name AS Stratum,  " + 
                    "STSim_SecondaryStratum.Name AS SecondaryStratum,  " + "STSim_TertiaryStratum.Name AS TertiaryStratum,  " + "STSim_TransitionAttributeType.Name as AttributeType, " +
                    "STSim_OutputTransitionAttribute.AgeMin, " + "STSim_OutputTransitionAttribute.AgeMax, " + "STSim_OutputTransitionAttribute.Amount " + 
                    "FROM STSim_OutputTransitionAttribute " + "INNER JOIN SSim_Scenario ON SSim_Scenario.ScenarioID = STSim_OutputTransitionAttribute.ScenarioID " +
                    "INNER JOIN STSim_Stratum ON STSim_Stratum.StratumID = STSim_OutputTransitionAttribute.StratumID " + 
                    "LEFT JOIN STSim_SecondaryStratum ON STSim_SecondaryStratum.SecondaryStratumID = STSim_OutputTransitionAttribute.SecondaryStratumID " + 
                    "LEFT JOIN STSim_TertiaryStratum ON STSim_TertiaryStratum.TertiaryStratumID = STSim_OutputTransitionAttribute.TertiaryStratumID " + 
                    "INNER JOIN STSim_TransitionAttributeType ON STSim_TransitionAttributeType.TransitionAttributeTypeID = STSim_OutputTransitionAttribute.TransitionAttributeTypeID " + 
                    "WHERE STSim_OutputTransitionAttribute.ScenarioID IN ({0})  " + "ORDER BY " + "STSim_OutputTransitionAttribute.ScenarioID, " + "SSim_Scenario.Name, " + 
                    "STSim_OutputTransitionAttribute.Iteration, " + "STSim_OutputTransitionAttribute.Timestep, " + "STSim_Stratum.Name, " + "STSim_SecondaryStratum.Name, " +
                    "STSim_TertiaryStratum.Name, " + "STSim_TransitionAttributeType.Name, " + "AgeMin, " + "AgeMax", ScenFilter);
            }
        }
    }
}
