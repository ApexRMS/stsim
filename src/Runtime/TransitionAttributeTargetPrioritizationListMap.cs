// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Common;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    class TransitionAttributeTargetPrioritizationListMap
    {
        SortedKeyMap2<List<TransitionAttributeTargetPrioritization>> m_Map = new SortedKeyMap2<List<TransitionAttributeTargetPrioritization>>(SearchMode.ExactPrev);
        private List<List<TransitionAttributeTargetPrioritization>> m_Lists = new List<List<TransitionAttributeTargetPrioritization>>();

        public TransitionAttributeTargetPrioritizationListMap(
            List<TransitionAttributeTargetPrioritization> collection, 
            int transitionAttributeTypeId)
        {
            foreach (TransitionAttributeTargetPrioritization item in collection)
            {
                List<TransitionAttributeTargetPrioritization> l = this.m_Map.GetItemExact(item.Iteration, item.Timestep);

                if (l == null)
                {
                    l = new List<TransitionAttributeTargetPrioritization>();

                    l.Add(new TransitionAttributeTargetPrioritization(
                        item.Iteration, item.Timestep, transitionAttributeTypeId, null, null, null, null, null, double.MaxValue));

                    this.m_Map.AddItem(item.Iteration, item.Timestep, l);

                    this.m_Lists.Add(l);
                }

                l.Add(item);
            }
           
            foreach (List<TransitionAttributeTargetPrioritization> lst in this.m_Lists)
            {
                lst.Sort((TransitionAttributeTargetPrioritization p1, TransitionAttributeTargetPrioritization p2) =>
                {
                    return p1.Priority.CompareTo(p2.Priority);
                });
            }
        }

        public List<TransitionAttributeTargetPrioritization> GetPrioritizations(int iteration, int timestep)
        {
            return this.m_Map.GetItem(iteration, timestep);
        }
    }
}
