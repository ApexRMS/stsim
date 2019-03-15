// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    class TransitionTargetPrioritizationKeyMap
    {
        private Dictionary<int, List<TransitionTargetPrioritization>> m_Map = new Dictionary<int, List<TransitionTargetPrioritization>>();
        private List<List<TransitionTargetPrioritization>> m_Lists = new List<List<TransitionTargetPrioritization>>();

        public TransitionTargetPrioritizationKeyMap(TransitionTargetPrioritizationCollection collection)
        {
            foreach (TransitionTargetPrioritization Item in collection)
            {
                List<TransitionTargetPrioritization> l;

                if (this.m_Map.ContainsKey(Item.TransitionGroupId))
                {
                    l = this.m_Map[Item.TransitionGroupId];
                }
                else
                { 
                    l = new List<TransitionTargetPrioritization>();
                    this.m_Map.Add(Item.TransitionGroupId, l);
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

        public List<TransitionTargetPrioritization> GetPrioritizationList(int transitionGroupId)
        {
            if (this.m_Map.ContainsKey(transitionGroupId))
            {
                return this.m_Map[transitionGroupId];
            }
            else
            {
                return null;
            }
        }
    }
}
