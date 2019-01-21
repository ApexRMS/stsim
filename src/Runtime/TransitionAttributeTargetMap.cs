// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    internal class TransitionAttributeTargetMap : STSimMapBase4<TransitionAttributeTarget>
    {
        public TransitionAttributeTargetMap(Scenario scenario, TransitionAttributeTargetCollection collection) : base(scenario)
        {
            foreach (TransitionAttributeTarget Item in collection)
            {
                this.TryAddItem(Item);
            }
        }

        public TransitionAttributeTarget GetAttributeTarget(int transitionAttributeTypeId, int stratumId, int? secondaryStratumId, int? tertiaryStratumId, int iteration, int timestep)
        {
            return base.GetItem(transitionAttributeTypeId, stratumId, secondaryStratumId, tertiaryStratumId, iteration, timestep);
        }

        private void TryAddItem(TransitionAttributeTarget item)
        {
            try
            {
                base.AddItem(item.TransitionAttributeTypeId, item.StratumId, item.SecondaryStratumId, item.TertiaryStratumId, item.Iteration, item.Timestep, item);
            }
            catch (STSimMapDuplicateItemException)
            {
                string template = "A duplicate transition attribute target was detected: More information:" + Environment.NewLine + "Transition Attribute Type={0}, {1}={2}, {3}={4}, {5}={6}, Iteration={7}, Timestep={8}." + Environment.NewLine + "NOTE: A user defined distribution can result in additional transition attribute targets when the model is run.";
                ExceptionUtils.ThrowArgumentException(template, this.GetTransitionAttributeTypeName(item.TransitionAttributeTypeId), this.PrimaryStratumLabel, this.GetStratumName(item.StratumId), this.SecondaryStratumLabel, this.GetSecondaryStratumName(item.SecondaryStratumId), this.TertiaryStratumLabel, this.GetTertiaryStratumName(item.TertiaryStratumId), STSimMapBase.FormatValue(item.Iteration), STSimMapBase.FormatValue(item.Timestep));
            }
        }
    }
}
