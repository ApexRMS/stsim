// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;
using System.Diagnostics;

namespace SyncroSim.STSim
{
    internal class TransitionTargetPrioritizationValidationMap : STSimMapBase5<TransitionTargetPrioritization>
    {
        public TransitionTargetPrioritizationValidationMap(Scenario scenario, TransitionTargetPrioritizationCollection collection) : base(scenario)
        {
            foreach (TransitionTargetPrioritization Item in collection)
            {
                this.TryAddItem(Item);
            }
        }

        private void TryAddItem(TransitionTargetPrioritization item)
        {
            try
            {
                this.AddItem(item.TransitionGroupId, item.StratumId, item.SecondaryStratumId, item.TertiaryStratumId, item.StateClassId, item.Iteration, item.Timestep, item);
            }
            catch (STSimMapDuplicateItemException)
            {
                string template =
                    "A duplicate transition target prioritization was detected: More information:" +
                    Environment.NewLine +
                    "Transition Group={0}, {1}={2}, {3}={4}, {5}={6}, State Class={7}, Iteration={8}, Timestep={9}.";

                ExceptionUtils.ThrowArgumentException(
                    template,
                    this.GetTransitionGroupName(item.TransitionGroupId),
                    this.PrimaryStratumLabel,
                    this.GetStratumName(item.StratumId),
                    this.SecondaryStratumLabel,
                    this.GetSecondaryStratumName(item.SecondaryStratumId),
                    this.TertiaryStratumLabel,
                    this.GetTertiaryStratumName(item.TertiaryStratumId),
                    this.GetStateClassName(item.StateClassId),
                    STSimMapBase.FormatValue(item.Iteration), 
                    STSimMapBase.FormatValue(item.Timestep));
            }

            Debug.Assert(this.HasItems);
        }
    }
}
