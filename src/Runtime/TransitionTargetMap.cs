// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;
using System.Diagnostics;

namespace SyncroSim.STSim
{
    internal class TransitionTargetMap : STSimMapBase4<TransitionTarget>
    {
        public TransitionTargetMap(Scenario scenario, TransitionTargetCollection collection) : base(scenario)
        {
            foreach (TransitionTarget Item in collection)
            {
                this.TryAddItem(Item);
            }
        }

        public TransitionTarget GetTransitionTarget(int transitionGroupId, int stratumId, int? secondaryStratumId, int? tertiaryStratumId, int iteration, int timestep)
        {
            return base.GetItem(transitionGroupId, stratumId, secondaryStratumId, tertiaryStratumId, iteration, timestep);
        }

        private void TryAddItem(TransitionTarget item)
        {
            try
            {
                this.AddItem(item.TransitionGroupId, item.StratumId, item.SecondaryStratumId, item.TertiaryStratumId, item.Iteration, item.Timestep, item);
            }
            catch (STSimMapDuplicateItemException)
            {
                string template = "A duplicate transition target was detected: More information:" + Environment.NewLine + "Transition Group={0}, {1}={2}, {3}={4}, {5}={6}, Iteration={7}, Timestep={8}." + Environment.NewLine + "NOTE: A user defined distribution can result in additional transition targets when the model is run.";
                ExceptionUtils.ThrowArgumentException(template, this.GetTransitionGroupName(item.TransitionGroupId), this.PrimaryStratumLabel, this.GetStratumName(item.StratumId), this.SecondaryStratumLabel, this.GetSecondaryStratumName(item.SecondaryStratumId), this.TertiaryStratumLabel, this.GetTertiaryStratumName(item.TertiaryStratumId), STSimMapBase.FormatValue(item.Iteration), STSimMapBase.FormatValue(item.Timestep));
            }

            Debug.Assert(this.HasItems);
        }
    }
}
