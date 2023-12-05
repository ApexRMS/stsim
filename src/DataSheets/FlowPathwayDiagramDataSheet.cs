// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Reflection;
using SyncroSim.Core;
using System.Globalization;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
	[ObfuscationAttribute(Exclude=true, ApplyToMembers=false)]
	internal class FlowPathwayDiagramDataSheet : DataSheet
	{
		public const string ERROR_INVALID_CELL_ADDRESS = "The value must be a valid cell address (a valid cell address is a letter from 'A' to 'Z' followed by a number from 1 to 255.  Example: 'A25')";

		public override void Validate(object proposedValue, string columnName)
		{
			base.Validate(proposedValue, columnName);

			if (columnName == Strings.LOCATION_COLUMN_NAME)
			{
				if (!IsValidLocation(proposedValue))
				{
					throw new DataException(ERROR_INVALID_CELL_ADDRESS);
				}
			}
		}

		public override string GetDeleteRowsConfirmation()
		{
			string m = base.GetDeleteRowsConfirmation();

			if (string.IsNullOrWhiteSpace(m))
			{
				m = "Associated flows will also be deleted.  Continue?";
			}

			return m;
		}

		protected override void OnRowsDeleted(object sender, SyncroSim.Core.DataSheetRowEventArgs e)
		{
			bool DeletedRows = false;
			Dictionary<int, bool> RemainingStockTypes = LookupKeyUtils.CreateRecordLookup(this, Strings.STOCK_TYPE_ID_COLUMN_NAME);
			DataSheet FlowPathwaySheet = this.GetDataSheet(Strings.DATASHEET_FLOW_PATHWAY_NAME);
			DataTable FlowPathwayData = FlowPathwaySheet.GetData();

			for (int i = FlowPathwayData.Rows.Count - 1; i >= 0; i--)
			{
				DataRow dr = FlowPathwayData.Rows[i];

				if (dr.RowState == DataRowState.Deleted)
				{
					continue;
				}

				int? FromStockTypeId = null;
				if (dr[Strings.FROM_STOCK_TYPE_ID_COLUMN_NAME] != DBNull.Value)
                {
					FromStockTypeId = Convert.ToInt32(dr[Strings.FROM_STOCK_TYPE_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
				}

				int? ToStockTypeId = null;
				if (dr[Strings.TO_STOCK_TYPE_ID_COLUMN_NAME] != DBNull.Value)
                {
					ToStockTypeId = Convert.ToInt32(dr[Strings.TO_STOCK_TYPE_ID_COLUMN_NAME], CultureInfo.InvariantCulture);

				}

				if (FromStockTypeId.HasValue && ToStockTypeId.HasValue)
                {
					if ((!RemainingStockTypes.ContainsKey(FromStockTypeId.Value)) | (!RemainingStockTypes.ContainsKey(ToStockTypeId.Value)))
					{
						DataTableUtilities.DeleteTableRow(FlowPathwayData, dr);
						DeletedRows = true;
					}
				}
			}

			if (DeletedRows)
			{
				FlowPathwaySheet.Changes.Add(new ChangeRecord(this, "Diagram data deleted rows"));
			}

			base.OnRowsDeleted(sender, e);
		}

		private static bool IsValidLocation(object proposedLocation)
		{
			if (proposedLocation == null)
			{
				return false;
			}

			string Location = Convert.ToString(proposedLocation, CultureInfo.InvariantCulture);

			if (string.IsNullOrEmpty(Location))
			{
				return false;
			}

			string LocUpper = Location.ToUpper(CultureInfo.InvariantCulture).Trim();

			if (string.IsNullOrEmpty(LocUpper))
			{
				return false;
			}

			if (LocUpper.Length < 2)
			{
				return false;
			}

			string CharPart = LocUpper.Substring(0, 1);
			string NumPart = LocUpper.Substring(1, LocUpper.Length - 1);

			if (string.IsNullOrEmpty(CharPart) | string.IsNullOrEmpty(NumPart))
			{
				return false;
			}

			if (CharPart[0] < 'A' || CharPart[0] > 'Z')
			{
				return false;
			}

			int n = 0;
			if (!int.TryParse(NumPart, out n))
			{
				return false;
			}

			if (n <= 0 || n > 256)
			{
				return false;
			}

			return true;
		}
	}
}