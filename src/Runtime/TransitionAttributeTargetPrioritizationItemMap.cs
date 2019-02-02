// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    internal class TransitionAttributeTargetPrioritizationItemMap : STSimMapBase5<TransitionAttributeTargetPrioritization>
    {
        public TransitionAttributeTargetPrioritizationItemMap(
            List<TransitionAttributeTargetPrioritization> collection, 
            Scenario scenario) : base(scenario)
        {
            foreach (TransitionAttributeTargetPrioritization item in collection)
            {
                this.AddItem(
                    item.StratumId,
                    item.SecondaryStratumId,
                    item.TertiaryStratumId,
                    item.TransitionGroupId,
                    item.StateClassId,
                    item.Iteration,
                    item.Timestep,
                    item);
            }
        }

        public TransitionAttributeTargetPrioritization GetPrioritization(
            int? stratumId,
            int? secondaryStratumId,
            int? tertiaryStratumId,
            int? transitionGroupId,
            int? stateClassId,
            int? iteration,
            int? timestep)
        {
            return this.GetItem(
                stratumId, 
                secondaryStratumId, 
                tertiaryStratumId, 
                transitionGroupId, 
                stateClassId, 
                iteration, 
                timestep);
        }
    }
}
