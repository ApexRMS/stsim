// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Reflection;
using System.Globalization;
using SyncroSim.Core;
using SyncroSim.Core.Forms;

namespace SyncroSim.STSim
{
	[ObfuscationAttribute(Exclude=true, ApplyToMembers=false)]
	internal class SummaryStockReport : ExportTransformer
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
				this.ExportToExcel(location, columns, this.CreateReportQuery(false), "Stocks");
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
			DataSheet dsterm = this.Project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME);
			string StockUnits = TerminologyUtilities.GetStockUnits(dsterm);
			string TimestepLabel = TerminologyUtilities.GetTimestepUnits(this.Project);
			string PrimaryStratumLabel = null;
			string SecondaryStratumLabel = null;
			string TertiaryStratumLabel = null;

			TerminologyUtilities.GetStratumLabelTerminology(dsterm, ref PrimaryStratumLabel, ref SecondaryStratumLabel, ref TertiaryStratumLabel);
			string TotalValue = string.Format(CultureInfo.InvariantCulture, "Total Value ({0})", StockUnits);

			c.Add(new ExportColumn("ScenarioId", "Scenario Id"));
			c.Add(new ExportColumn("ScenarioName", "Scenario"));
			c.Add(new ExportColumn("Iteration", "Iteration"));
			c.Add(new ExportColumn("Timestep", TimestepLabel));
			c.Add(new ExportColumn("Stratum", PrimaryStratumLabel));
			c.Add(new ExportColumn("SecondaryStratum", SecondaryStratumLabel));
			c.Add(new ExportColumn("TertiaryStratum", TertiaryStratumLabel));
			c.Add(new ExportColumn("StateClass", "State Class"));
			c.Add(new ExportColumn("StockGroup", "Stock Group"));
			c.Add(new ExportColumn("Amount", TotalValue));

			c["Amount"].DecimalPlaces = 2;
			c["Amount"].Alignment = Core.ColumnAlignment.Right;

			return c;
		}

		private string CreateReportQuery(bool isCSV)
		{
			string ScenFilter = this.ExportCreateActiveResultScenarioFilter();

            string Query =
                "SELECT " +
                "stsim_OutputStock.ScenarioId, ";
           
            if (!isCSV)
            {
                Query += "core_Scenario.Name AS ScenarioName, ";
            }

            Query += string.Format(CultureInfo.InvariantCulture,
                "stsim_OutputStock.Iteration, " +
                "stsim_OutputStock.Timestep, " +
                "ST1.Name AS Stratum, " +
                "ST2.Name AS SecondaryStratum, " +
                "ST3.Name AS TertiaryStratum, " +
                "SC1.Name AS StateClass, " +
                "stsim_StockGroup.Name as StockGroup, " +
                "stsim_OutputStock.Amount " +
                "FROM stsim_OutputStock " +
                "INNER JOIN core_Scenario ON core_Scenario.ScenarioId = stsim_OutputStock.ScenarioId " +
                "INNER JOIN stsim_Stratum AS ST1 ON ST1.StratumId = stsim_OutputStock.StratumId " +
                "LEFT JOIN stsim_SecondaryStratum AS ST2 ON ST2.SecondaryStratumId = stsim_OutputStock.SecondaryStratumId " +
                "LEFT JOIN stsim_TertiaryStratum AS ST3 ON ST3.TertiaryStratumId = stsim_OutputStock.TertiaryStratumId " +
                "INNER JOIN stsim_StateClass AS SC1 ON SC1.StateClassId = stsim_OutputStock.StateClassId " +
                "INNER JOIN stsim_StockGroup ON stsim_StockGroup.StockGroupId = stsim_OutputStock.StockGroupId " +
                "WHERE stsim_OutputStock.ScenarioId IN ({0}) " +
                "ORDER BY " +
                "stsim_OutputStock.ScenarioId, ", 
                ScenFilter);

            if (!isCSV)
            {
                Query += "core_Scenario.Name, ";
            }

            Query += string.Format(CultureInfo.InvariantCulture, 
                "stsim_OutputStock.Iteration, " + 
                "stsim_OutputStock.Timestep, " + 
                "ST1.Name, " + 
                "ST2.Name, " + 
                "ST3.Name, " + 
                "SC1.Name, " + 
                "stsim_StockGroup.Name", 
                ScenFilter);

            return Query;
		}
	}
}
