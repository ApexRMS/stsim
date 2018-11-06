// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    class TransitionTargetPrioritizationMap : STSimMapBase1<List<TransitionTargetPrioritization>>
    {
        private List<List<TransitionTargetPrioritization>> m_Lists = new List<List<TransitionTargetPrioritization>>();

        public TransitionTargetPrioritizationMap(Scenario scenario, TransitionTargetPrioritizationCollection collection) : base(scenario)
        {
            foreach (TransitionTargetPrioritization Item in collection)
            {
                List<TransitionTargetPrioritization> l = base.GetItemExact(Item.TransitionGroupId, Item.Iteration, Item.Timestep);

                if (l == null)
                {
                    l = new List<TransitionTargetPrioritization>();
                    this.AddItem(Item.TransitionGroupId, Item.Iteration, Item.Timestep, l);
                    this.m_Lists.Add(l);
                }

                l.Add(Item);
            }

            foreach(List<TransitionTargetPrioritization> lst in this.m_Lists)
            {
                lst.Sort((TransitionTargetPrioritization p1, TransitionTargetPrioritization p2) =>
                {
                    return p1.Priority.CompareTo(p2.Priority);
                });      
            }
        }

        public List<TransitionTargetPrioritization> GetPrioritizationList(
            int transitionGroupId, 
            int? iteration, 
            int? timestep)
        {
            return this.GetItem(transitionGroupId, iteration, timestep);
        }
    }
}
