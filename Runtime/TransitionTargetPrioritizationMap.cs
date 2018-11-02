// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;
using SyncroSim.Common;
using System.Globalization;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    class TransitionTargetPrioritizationMap : STSimMapBase1<List<TransitionTargetPrioritization>>
    {
        private List<List<TransitionTargetPrioritization>> m_AllLists = new List<List<TransitionTargetPrioritization>>();

        private Dictionary<List<TransitionTargetPrioritization>, MultiLevelKeyMap4<TransitionTargetPrioritization>> m_PropMaps = 
            new Dictionary<List<TransitionTargetPrioritization>, MultiLevelKeyMap4<TransitionTargetPrioritization>>();
         
        public TransitionTargetPrioritizationMap(Scenario scenario, TransitionTargetPrioritizationCollection collection) : base(scenario)
        {
            foreach (TransitionTargetPrioritization Item in collection)
            {
                List<TransitionTargetPrioritization> l = base.GetItemExact(Item.TransitionGroupId, Item.Iteration, Item.Timestep);

                if (l == null)
                {
                    l = new List<TransitionTargetPrioritization>();
                    this.AddItem(Item.TransitionGroupId, Item.Iteration, Item.Timestep, l);

                    if (!this.m_AllLists.Contains(l))
                    {
                        this.m_AllLists.Add(l);
                        this.m_PropMaps.Add(l, new MultiLevelKeyMap4<TransitionTargetPrioritization>());
                    }
                }

                l.Add(Item);
            }

            foreach(List<TransitionTargetPrioritization> lst in this.m_AllLists)
            {
                lst.Sort((TransitionTargetPrioritization p1, TransitionTargetPrioritization p2) =>
                {
                    return p1.Priority.CompareTo(p2.Priority);
                });      
            }

            foreach (List<TransitionTargetPrioritization> lst in this.m_AllLists)
            {
                MultiLevelKeyMap4<TransitionTargetPrioritization> map = this.m_PropMaps[lst];

                foreach (TransitionTargetPrioritization pri in lst)
                {
                    TransitionTargetPrioritization props = map.GetItemExact(
                        pri.StratumId, pri.SecondaryStratumId, pri.TertiaryStratumId, pri.StateClassId);
 
                    if (props == null)
                    {
                        map.AddItem(
                            pri.StratumId, 
                            pri.SecondaryStratumId, 
                            pri.TertiaryStratumId, 
                            pri.StateClassId, 
                            pri);
                    }
                    else
                    {
                        string msg = string.Format(CultureInfo.InvariantCulture,
                            "A duplicate Transition Target Prioritization exists for: {0}: {1}, {2}: {3}, {4}: {5}, {6}: {7}, {8}: {9}",
                            "Transition Group", this.GetTransitionGroupName(pri.TransitionGroupId),
                            this.PrimaryStratumLabel, this.GetStratumName(pri.StratumId), 
                            this.SecondaryStratumLabel, this.GetSecondaryStratumName(pri.SecondaryStratumId), 
                            this.TertiaryStratumLabel, this.GetTertiaryStratumName(pri.TertiaryStratumId), 
                            "State Class", this.GetStateClassName(pri.StateClassId));

                        throw new ArgumentException(msg);
                    }
                }
            }
        }

        public List<TransitionTargetPrioritization> GetPrioritizations(
            int transitionGroupId, 
            int iteration, 
            int timestep)
        {
            return this.GetItem(transitionGroupId, iteration, timestep);
        }

        public TransitionTargetPrioritization GetSinglePrioritization(
            List<TransitionTargetPrioritization> prioritizations, 
            int stratumId, 
            int? secondaryStratumId, 
            int? tertiaryStratumId, 
            int stateClassId)
        {
            return this.m_PropMaps[prioritizations].GetItem(
                stratumId, secondaryStratumId, tertiaryStratumId, stateClassId);
        }
    }
}
