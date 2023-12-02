// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Apex;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    class TransitionTargetPrioritizationListMap
    {
        SortedKeyMap2<List<TransitionTargetPrioritization>> m_Map = new SortedKeyMap2<List<TransitionTargetPrioritization>>(SearchMode.ExactPrev);
        private List<List<TransitionTargetPrioritization>> m_Lists = new List<List<TransitionTargetPrioritization>>();

        public TransitionTargetPrioritizationListMap(
            List<TransitionTargetPrioritization> collection, 
            int transitionGroupId)
        {
            foreach (TransitionTargetPrioritization item in collection)
            {
                List<TransitionTargetPrioritization> l = this.m_Map.GetItemExact(item.Iteration, item.Timestep);

                if (l == null)
                {
                    l = new List<TransitionTargetPrioritization>();

                    l.Add(new TransitionTargetPrioritization(
                        item.Iteration, item.Timestep, transitionGroupId, null, null, null, null, double.MaxValue));

                    this.m_Map.AddItem(item.Iteration, item.Timestep, l);
                    this.m_Lists.Add(l);
                }

                l.Add(item);
            }

            foreach (List<TransitionTargetPrioritization> lst in this.m_Lists)
            {
                lst.Sort((TransitionTargetPrioritization p1, TransitionTargetPrioritization p2) =>
                {
                    return p1.Priority.CompareTo(p2.Priority);
                });
            }
        }

        public List<TransitionTargetPrioritization> GetPrioritizations(int iteration, int timestep)
        {
            return this.m_Map.GetItem(iteration, timestep);
        }
    }
}