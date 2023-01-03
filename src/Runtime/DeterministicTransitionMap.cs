// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;
using System.Diagnostics;

namespace SyncroSim.STSim
{
    internal class DeterministicTransitionMap : STSimMapBase2<DeterministicTransition>
    {
        public DeterministicTransitionMap(Scenario scenario, DeterministicTransitionCollection transitions) : base(scenario)
        {

            foreach (DeterministicTransition t in transitions)
            {
                AddTransition(t);
            }
        }

        public DeterministicTransition GetDeterministicTransition(int? stratumId, int stateClassId, int iteration, int timestep)
        {
            return this.GetItem(stratumId, stateClassId, iteration, timestep);
        }

        private void AddTransition(DeterministicTransition t)
        {
            try
            {
                this.AddItem(t.StratumIdSource, t.StateClassIdSource, t.Iteration, t.Timestep, t);
            }
            catch (STSimMapDuplicateItemException)
            {
                string template = "A duplicate deterministic transition was detected: More information:" + Environment.NewLine + "Source {0}={1}, Source State Class={2}, Iteration={3}, Timestep={4}.";
                ExceptionUtils.ThrowArgumentException(template, this.PrimaryStratumLabel, this.GetStratumName(t.StratumIdSource), this.GetStateClassName(t.StateClassIdSource), STSimMapBase.FormatValue(t.Iteration), STSimMapBase.FormatValue(t.Timestep));
            }

            Debug.Assert(this.HasItems);
        }
    }
}
