// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;
using System.Diagnostics;

namespace SyncroSim.STSim
{
    internal class TransitionSpatialMultiplierMap : STSimMapBase1<TransitionSpatialMultiplier>
    {
        public TransitionSpatialMultiplierMap(Scenario scenario, TransitionSpatialMultiplierCollection collection) : base(scenario)
        {
            foreach (TransitionSpatialMultiplier Item in collection)
            {
                this.TryAddItem(Item);
            }
        }

        public TransitionSpatialMultiplier GetMultiplier(int transitionGroupId, int iteration, int timestep)
        {
            return base.GetItem(transitionGroupId, iteration, timestep);
        }

        private void TryAddItem(TransitionSpatialMultiplier item)
        {
            try
            {
                this.AddItem(item.TransitionGroupId, item.Iteration, item.Timestep, item);
            }
            catch (STSimMapDuplicateItemException)
            {
                string template = 
                    "A duplicate transition spatial multiplier was detected: More information:" 
                    + Environment.NewLine
                    + "Transition Group={0}, Iteration={1}, Timestep={2}";

                ExceptionUtils.ThrowArgumentException(template, this.GetTransitionGroupName(item.TransitionGroupId), STSimMapBase.FormatValue(item.Iteration), STSimMapBase.FormatValue(item.Timestep));
            }

            Debug.Assert(this.HasItems);
        }
    }
}
