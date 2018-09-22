// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using System.Diagnostics;

namespace SyncroSim.STSim
{
    internal class TransitionMap : STSimMapBase2<TransitionCollection>
    {
        public TransitionMap(Scenario scenario, TransitionCollection transitions) : base(scenario)
        {
            foreach (Transition t in transitions)
            {
                AddTransition(t);
            }
        }

        public TransitionCollection GetTransitions(int stratumId, int stateClassId, int iteration, int timestep)
        {
            return this.GetItem(stratumId, stateClassId, iteration, timestep);
        }

        private void AddTransition(Transition t)
        {
            TransitionCollection c = this.GetItemExact(t.StratumIdSource, t.StateClassIdSource, t.Iteration, t.Timestep);

            if (c == null)
            {
                c = new TransitionCollection();
                this.AddItem(t.StratumIdSource, t.StateClassIdSource, t.Iteration, t.Timestep, c);
            }

            c.Add(t);
            Debug.Assert(this.HasItems);
        }
    }
}
