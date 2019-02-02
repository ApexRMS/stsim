// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    internal class TransitionSizePrioritizationMap : STSimMapBase2<TransitionSizePrioritization>
    {
        public TransitionSizePrioritizationMap(Scenario scenario, TransitionSizePrioritizationCollection collection) : base(scenario)
        {
            foreach (TransitionSizePrioritization Item in collection)
            {
                this.TryAddItem(Item);
            }
        }

        public TransitionSizePrioritization GetSizePrioritization(int transitionGroupId, int iteration, int timestep, int stratumId)
        {
            return base.GetItem(transitionGroupId, stratumId, iteration, timestep);
        }

        private void TryAddItem(TransitionSizePrioritization item)
        {
            try
            {
                this.AddItem(item.TransitionGroupId, item.StratumId, item.Iteration, item.Timestep, item);
            }
            catch (STSimMapDuplicateItemException)
            {
                string template = "A duplicate transition size prioritization was detected: More information:" + Environment.NewLine + "Transition Group={0}, {1}={2}, Iteration={3}, Timestep={4}";
                ExceptionUtils.ThrowArgumentException(template, this.GetTransitionGroupName(item.TransitionGroupId), this.PrimaryStratumLabel, this.GetStratumName(item.StratumId), STSimMapBase.FormatValue(item.Iteration), STSimMapBase.FormatValue(item.Timestep));
            }
        }
    }
}
