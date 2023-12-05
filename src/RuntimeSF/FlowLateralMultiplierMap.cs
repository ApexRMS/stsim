// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    internal class FlowLateralMultiplierMap : StockFlowMapBase1<FlowLateralMultiplier>
    {
        public FlowLateralMultiplierMap(Scenario scenario, FlowLateralMultiplierCollection items) : base(scenario)
        {
            foreach (FlowLateralMultiplier Item in items)
            {
                this.TryAddItem(Item);
            }
        }

        public FlowLateralMultiplier GetFlowLateralMultiplier(int flowGroupId, int iteration, int timestep)
        {
            return base.GetItem(flowGroupId, iteration, timestep);
        }

        private void TryAddItem(FlowLateralMultiplier item)
        {
            try
            {
                this.AddItem(item.FlowGroupId, item.Iteration, item.Timestep, item);
            }
            catch (STSimMapDuplicateItemException)
            {
                string template = "A duplicate flow lateral multiplier was detected: More information:" + Environment.NewLine + "Flow Group={0}, Iteration={1}, Timestep={2}";
                ExceptionUtils.ThrowArgumentException(template, this.GetFlowGroupName(item.FlowGroupId), StockFlowMapBase.FormatValue(item.Iteration), StockFlowMapBase.FormatValue(item.Timestep));
            }
        }
    }
}
