// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    class TransitionAttributeTargetPrioritizationKeyMap
    {
        private Dictionary<int, List<TransitionAttributeTargetPrioritization>> m_Map = new Dictionary<int, List<TransitionAttributeTargetPrioritization>>();
        private List<List<TransitionAttributeTargetPrioritization>> m_Lists = new List<List<TransitionAttributeTargetPrioritization>>();

        public TransitionAttributeTargetPrioritizationKeyMap(TransitionAttributeTargetPrioritizationCollection collection)
        {
            foreach (TransitionAttributeTargetPrioritization Item in collection)
            {
                List<TransitionAttributeTargetPrioritization> l;

                if (this.m_Map.ContainsKey(Item.TransitionAttributeTypeId))
                {
                    l = this.m_Map[Item.TransitionAttributeTypeId];
                }
                else
                {
                    l = new List<TransitionAttributeTargetPrioritization>();
                    this.m_Map.Add(Item.TransitionAttributeTypeId, l);
                    this.m_Lists.Add(l);
                }

                l.Add(Item);
            }

            foreach (List<TransitionAttributeTargetPrioritization> lst in this.m_Lists)
            {
                lst.Sort((TransitionAttributeTargetPrioritization p1, TransitionAttributeTargetPrioritization p2) =>
                {
                    return p1.Priority.CompareTo(p2.Priority);
                });
            }
        }

        public List<TransitionAttributeTargetPrioritization> GetPrioritizationList(int transitionGroupId)
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
