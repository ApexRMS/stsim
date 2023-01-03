// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    internal class TransitionPatchPrioritizationMap : STSimMapBase1<TransitionPatchPrioritization>
    {
        public TransitionPatchPrioritizationMap(Scenario scenario, TransitionPatchPrioritizationCollection items) : base(scenario)
        {
            foreach (TransitionPatchPrioritization Item in items)
            {
                this.TryAddItem(Item);
            }
        }

        public TransitionPatchPrioritization GetPatchPrioritization(int transitionGroupId, int iteration, int timestep)
        {
            return base.GetItem(transitionGroupId, iteration, timestep);
        }

        private void TryAddItem(TransitionPatchPrioritization item)
        {
            try
            {
                this.AddItem(item.TransitionGroupId, item.Iteration, item.Timestep, item);
            }
            catch (STSimMapDuplicateItemException)
            {
                string template = "A duplicate transition patch prioritization was detected: More information:" + Environment.NewLine + "Transition Group={0}, Iteration={1}, Timestep={2}";
                ExceptionUtils.ThrowArgumentException(template, this.GetTransitionGroupName(item.TransitionGroupId), STSimMapBase.FormatValue(item.Iteration), STSimMapBase.FormatValue(item.Timestep));
            }
        }
    }
}
