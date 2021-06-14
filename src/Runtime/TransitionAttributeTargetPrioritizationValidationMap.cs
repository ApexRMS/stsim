// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;
using System.Diagnostics;

namespace SyncroSim.STSim
{
    internal class TransitionAttributeTargetPrioritizationValidationMap : STSimMapBase6<TransitionAttributeTargetPrioritization>
    {
        public TransitionAttributeTargetPrioritizationValidationMap(Scenario scenario, TransitionAttributeTargetPrioritizationCollection collection) : base(scenario)
        {
            foreach (TransitionAttributeTargetPrioritization Item in collection)
            {
                this.TryAddItem(Item);
            }
        }

        private void TryAddItem(TransitionAttributeTargetPrioritization item)
        {
            try
            {
                this.AddItem(item.TransitionAttributeTypeId, item.StratumId, item.SecondaryStratumId, item.TertiaryStratumId, 
                    item.TransitionGroupId, item.StateClassId, item.Iteration, item.Timestep, item);
            }
            catch (STSimMapDuplicateItemException)
            {
                string template =
                    "A duplicate transition attribute target prioritization was detected: More information:" +
                    Environment.NewLine +
                    "Transition Attribute Type={0}, {1}={2}, {3}={4}, {5}={6}, Transition Group={7}, State Class={8}, Iteration={9}, Timestep={10}.";

                ExceptionUtils.ThrowArgumentException(
                    template,
                    this.GetTransitionAttributeTypeName(item.TransitionAttributeTypeId),
                    this.PrimaryStratumLabel,
                    this.GetStratumName(item.StratumId),
                    this.SecondaryStratumLabel,
                    this.GetSecondaryStratumName(item.SecondaryStratumId),
                    this.TertiaryStratumLabel,
                    this.GetTertiaryStratumName(item.TertiaryStratumId),
                    this.GetTransitionGroupName(item.TransitionGroupId),
                    this.GetStateClassName(item.StateClassId),
                    STSimMapBase.FormatValue(item.Iteration),
                    STSimMapBase.FormatValue(item.Timestep));
            }

            Debug.Assert(this.HasItems);
        }
    }
}
