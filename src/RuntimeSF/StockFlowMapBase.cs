// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;
using System.Globalization;

namespace SyncroSim.STSim
{
	internal abstract class StockFlowMapBase
	{
		private readonly Scenario m_Scenario;
		private readonly string m_PrimaryStratumLabel;
		private readonly string m_SecondaryStratumLabel;
		private readonly string m_TertiaryStratumLabel;
		private bool m_HasItems;

		protected StockFlowMapBase(Scenario scenario)
		{
            this.m_Scenario = scenario;

			TerminologyUtilities.GetStratumLabelTerminology(
                scenario.Project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME), 
                ref this.m_PrimaryStratumLabel, 
                ref this.m_SecondaryStratumLabel, 
                ref this.m_TertiaryStratumLabel);
		}

		protected string PrimaryStratumLabel
		{
			get
			{
				return this.m_PrimaryStratumLabel;
			}
		}

		protected string SecondaryStratumLabel
		{
			get
			{
				return this.m_SecondaryStratumLabel;
			}
		}

		protected string TertiaryStratumLabel
		{
			get
			{
				return this.m_TertiaryStratumLabel;
			}
		}

		protected void SetHasItems()
		{
			this.m_HasItems = true;
		}

		public bool HasItems
		{
			get
			{
				return this.m_HasItems;
			}
		}

		protected static void ThrowDuplicateItemException()
		{
			throw new STSimMapDuplicateItemException("An item with the same keys has already been added.");
		}

		protected static string FormatValue(int? value)
		{
			if (!value.HasValue)
			{
				return "NULL";
			}
			else
			{
				return Convert.ToString(value, CultureInfo.InvariantCulture);
			}
		}

		protected string GetStratumName(int? id)
		{
			return this.GetProjectItemName(Strings.DATASHEET_STRATA_NAME, id);
		}

		protected string GetSecondaryStratumName(int? id)
		{
			return this.GetProjectItemName(Strings.DATASHEET_SECONDARY_STRATA_NAME, id);
		}

		protected string GetTertiaryStratumName(int? id)
		{
			return this.GetProjectItemName(Strings.DATASHEET_TERTIARY_STRATA_NAME, id);
		}

		protected string GetStateClassName(int? id)
		{
			return this.GetProjectItemName(Strings.DATASHEET_STATECLASS_NAME, id);
		}

		protected string GetStockTypeName(int? id)
		{
			return this.GetProjectItemName(Strings.DATASHEET_STOCK_TYPE_NAME, id);
		}

		protected string GetFlowGroupName(int? id)
		{
			return this.GetProjectItemName(Strings.DATASHEET_FLOW_GROUP_NAME, id);
		}

		protected string GetProjectItemName(string dataSheetName, int? id)
		{
			if (!id.HasValue)
			{
				return "NULL";
			}
			else
			{
				DataSheet ds = this.m_Scenario.Project.GetDataSheet(dataSheetName);
				return ds.ValidationTable.GetDisplayName(id.Value);
			}
		}
	}
}