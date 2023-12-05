// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using SyncroSim.Core;
using System.Reflection;
using System.Globalization;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
	[ObfuscationAttribute(Exclude=true, ApplyToMembers=false)]
	internal class FlowPathwayDataSheet : DataSheet
	{
		protected override void OnDataSheetChanged(DataSheetMonitorEventArgs e)
		{
			base.OnDataSheetChanged(e);

			string psl = e.GetValue("PrimaryStratumLabel", "Stratum");
			string ssl = e.GetValue("SecondaryStratumLabel", "Secondary Stratum");
			string tsl = e.GetValue("TertiaryStratumLabel", "Tertiary Stratum");

			this.Columns[Strings.FROM_STRATUM_ID_COLUMN_NAME].DisplayName = string.Format(CultureInfo.InvariantCulture, "From {0}", psl);
			this.Columns[Strings.FROM_SECONDARY_STRATUM_ID_COLUMN_NAME].DisplayName = string.Format(CultureInfo.InvariantCulture, "From {0}", ssl);
			this.Columns[Strings.FROM_TERTIARY_STRATUM_ID_COLUMN_NAME].DisplayName = string.Format(CultureInfo.InvariantCulture, "From {0}", tsl);

			this.Columns[Strings.TO_STRATUM_ID_COLUMN_NAME].DisplayName = string.Format(CultureInfo.InvariantCulture, "To {0}", psl);

			this.Columns[Strings.TRANSFER_TO_STRATUM_ID_COLUMN_NAME].DisplayName = string.Format(CultureInfo.InvariantCulture, "Transfer to {0}", psl);
			this.Columns[Strings.TRANSFER_TO_SECONDARY_STRATUM_ID_COLUMN_NAME].DisplayName = string.Format(CultureInfo.InvariantCulture, "Transfer to {0}", ssl);
			this.Columns[Strings.TRANSFER_TO_TERTIARY_STRATUM_ID_COLUMN_NAME].DisplayName = string.Format(CultureInfo.InvariantCulture, "Transfer to {0}", tsl);
		}

		public override void Validate(DataRow proposedRow, DataTransferMethod transferMethod)
		{
			base.Validate(proposedRow, transferMethod);

			DataSheet DiagramSheet = this.GetDataSheet(Strings.DATASHEET_FLOW_PATHWAY_DIAGRAM_NAME);
			Dictionary<int, bool> StockTypes = LookupKeyUtils.CreateRecordLookup(DiagramSheet, Strings.STOCK_TYPE_ID_COLUMN_NAME);

			if (proposedRow[Strings.FROM_STOCK_TYPE_ID_COLUMN_NAME] != DBNull.Value)
            {
				int FromStockTypeId = Convert.ToInt32(proposedRow[Strings.FROM_STOCK_TYPE_ID_COLUMN_NAME], CultureInfo.InvariantCulture);

				if (!StockTypes.ContainsKey(FromStockTypeId))
				{
					throw new DataException("The 'From Stock' does not exist for this scenario.");
				}
			}

			if (proposedRow[Strings.TO_STOCK_TYPE_ID_COLUMN_NAME] != DBNull.Value)
            {
				int ToStockTypeId = Convert.ToInt32(proposedRow[Strings.TO_STOCK_TYPE_ID_COLUMN_NAME], CultureInfo.InvariantCulture);

				if (!StockTypes.ContainsKey(ToStockTypeId))
				{
					throw new DataException("The 'To Stock' does not exist for this scenario.");
				}
			}
		}

		public override void Validate(DataTable proposedData, DataTransferMethod transferMethod)
		{
			base.Validate(proposedData, transferMethod);

			DataSheet StockTypeSheet = this.Project.GetDataSheet(Strings.DATASHEET_STOCK_TYPE_NAME);
			DataSheet DiagramSheet = this.GetDataSheet(Strings.DATASHEET_FLOW_PATHWAY_DIAGRAM_NAME);
			Dictionary<int, bool> StockTypes = LookupKeyUtils.CreateRecordLookup(DiagramSheet, Strings.STOCK_TYPE_ID_COLUMN_NAME);

			foreach (DataRow dr in proposedData.Rows)
			{
				if (dr[Strings.FROM_STOCK_TYPE_ID_COLUMN_NAME] != DBNull.Value)
                {
					int FromStockTypeId = Convert.ToInt32(dr[Strings.FROM_STOCK_TYPE_ID_COLUMN_NAME], CultureInfo.InvariantCulture);

					if (!StockTypes.ContainsKey(FromStockTypeId))
					{
						string StockTypeName = Convert.ToString(DataTableUtilities.GetTableValue(StockTypeSheet.GetData(), StockTypeSheet.ValueMember, FromStockTypeId, StockTypeSheet.DisplayMember), CultureInfo.InvariantCulture);
						throw new DataException(string.Format(CultureInfo.InvariantCulture, "Cannot import flow pathways because the 'From Stock' does not exist in this scenario: {0}", StockTypeName));
					}
				}

				if (dr[Strings.TO_STOCK_TYPE_ID_COLUMN_NAME] != DBNull.Value)
                {
					int ToStockTypeId = Convert.ToInt32(dr[Strings.TO_STOCK_TYPE_ID_COLUMN_NAME], CultureInfo.InvariantCulture);

					if (!StockTypes.ContainsKey(ToStockTypeId))
					{
						string StockTypeName = Convert.ToString(DataTableUtilities.GetTableValue(StockTypeSheet.GetData(), StockTypeSheet.ValueMember, ToStockTypeId, StockTypeSheet.DisplayMember), CultureInfo.InvariantCulture);
						throw new DataException(string.Format(CultureInfo.InvariantCulture, "Cannot import flow pathways because the 'To Stock' does not exist in this scenario: {0}", StockTypeName));
					}
				}
			}
		}
	}
}