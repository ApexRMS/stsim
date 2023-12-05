// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
	internal class FlowSpatialMultiplierMap : StockFlowMapBase1<FlowSpatialMultiplier>
	{
		public FlowSpatialMultiplierMap(Scenario scenario, FlowSpatialMultiplierCollection items) : base(scenario)
		{
			foreach (FlowSpatialMultiplier Item in items)
			{
				this.TryAddItem(Item);
			}
		}

		public FlowSpatialMultiplier GetFlowSpatialMultiplier(int flowGroupId, int iteration, int timestep)
		{
			return base.GetItem(flowGroupId, iteration, timestep);
		}

		private void TryAddItem(FlowSpatialMultiplier item)
		{
			try
			{
				this.AddItem(item.FlowGroupId, item.Iteration, item.Timestep, item);
			}
			catch (STSimMapDuplicateItemException)
			{
				string template = "A duplicate flow spatial multiplier was detected: More information:" + Environment.NewLine + "Flow Group={0}, Iteration={1}, Timestep={2}";
				ExceptionUtils.ThrowArgumentException(template, this.GetFlowGroupName(item.FlowGroupId), StockFlowMapBase.FormatValue(item.Iteration), StockFlowMapBase.FormatValue(item.Timestep));
			}
		}
	}
}