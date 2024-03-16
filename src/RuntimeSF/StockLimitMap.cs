// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    internal class StockLimitMap : StockFlowMapBase5<StockLimit>
    {
        public StockLimitMap(Scenario scenario, StockLimitCollection items) : base(scenario)
        {
            foreach (StockLimit item in items)
            {
                this.TryAddLimit(item);
            }
        }

        public StockLimit GetStockLimit(int? stockTypeId, int stratumId, int? secondaryStratumId, int? tertiaryStratumId, int stateClassId, int iteration, int timestep)
        {
            return base.GetItem(stockTypeId, stratumId, secondaryStratumId, tertiaryStratumId, stateClassId, iteration, timestep);
        }

        private void TryAddLimit(StockLimit item)
        {
            try
            {
                base.AddItem(item.StockTypeId, item.StratumId, item.SecondaryStratumId, item.TertiaryStratumId, item.StateClassId, item.Iteration, item.Timestep, item);
            }
            catch (STSimMapDuplicateItemException)
            {
                string template = "A duplicate stock limit was detected: More information:" + Environment.NewLine + "Stock Type={0}, {1}={2}, {3}={4}, {5}={6}, State Class={7}, Iteration={8}, Timestep={9}.";
                ExceptionUtils.ThrowArgumentException(template, this.GetStockTypeName(item.StockTypeId), this.PrimaryStratumLabel, this.GetStratumName(item.StratumId), this.SecondaryStratumLabel, this.GetSecondaryStratumName(item.SecondaryStratumId), this.TertiaryStratumLabel, this.GetTertiaryStratumName(item.TertiaryStratumId), this.GetStateClassName(item.StateClassId), StockFlowMapBase.FormatValue(item.Iteration), StockFlowMapBase.FormatValue(item.Timestep));
            }
        }
    }
}