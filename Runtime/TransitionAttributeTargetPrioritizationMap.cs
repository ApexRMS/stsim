// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    class TransitionAttributeTargetPrioritizationMap : STSimMapBase1<List<TransitionAttributeTargetPrioritization>>
    {
        private List<List<TransitionAttributeTargetPrioritization>> m_Lists = new List<List<TransitionAttributeTargetPrioritization>>();

        public TransitionAttributeTargetPrioritizationMap(Scenario scenario, TransitionAttributeTargetPrioritizationCollection collection) : base(scenario)
        {
            foreach (TransitionAttributeTargetPrioritization Item in collection)
            {
                List<TransitionAttributeTargetPrioritization> l = base.GetItemExact(Item.TransitionAttributeTypeId, Item.Iteration, Item.Timestep);

                if (l == null)
                {
                    l = new List<TransitionAttributeTargetPrioritization>();
                    this.AddItem(Item.TransitionAttributeTypeId, Item.Iteration, Item.Timestep, l);
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

        public List<TransitionAttributeTargetPrioritization> GetPrioritizationList(
            int transitionAttributeTypeId,
            int? iteration,
            int? timestep)
        {
            return this.GetItem(transitionAttributeTypeId, iteration, timestep);
        }
    }
}
