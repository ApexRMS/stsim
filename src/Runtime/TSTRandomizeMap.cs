// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;
using SyncroSim.Common;

namespace SyncroSim.STSim
{
    internal class TstRandomizeMap : STSimMapBase
    {
        private MultiLevelKeyMap5<SortedKeyMap1<TstRandomize>> m_map = new MultiLevelKeyMap5<SortedKeyMap1<TstRandomize>>();

        public TstRandomizeMap(Scenario scenario) : base(scenario)
        {
        }

        public TstRandomize GetTstRandomize(
            int? transitionGroupId, int? stratumId, int? secondaryStratumId, 
            int? tertiaryStratumId, int? stateClassId, int? iteration)
        {
            if (!this.HasItems)
            {
                return null;
            }

            SortedKeyMap1<TstRandomize> m = this.m_map.GetItem(
                transitionGroupId, stratumId, secondaryStratumId, tertiaryStratumId, stateClassId);

            if (m == null)
            {
                return null;
            }

            return m.GetItem(iteration);
        }

        public void AddTstRandomize
            (int? transitionGroupId, int? stratumId, int? secondaryStratumId, int? tertiaryStratumId, 
            int? stateClassId, int? iteration, TstRandomize tstRandomize)
        {
            SortedKeyMap1<TstRandomize> m = this.m_map.GetItemExact(
                transitionGroupId, stratumId, secondaryStratumId, tertiaryStratumId, stateClassId);

            if (m == null)
            {
                m = new SortedKeyMap1<TstRandomize>(SearchMode.ExactPrev);
                this.m_map.AddItem(transitionGroupId, stratumId, secondaryStratumId, tertiaryStratumId, stateClassId, m);
            }

            TstRandomize v = m.GetItemExact(iteration);

            if (v != null)
            {
                string template = "A duplicate Time-Since-Transition Randomize value was detected: More information:" + Environment.NewLine + "Transition Group={0}, {1}={2}, {3}={4}, {5}={6}, State Class={7}, Iteration={8}.";
                ExceptionUtils.ThrowArgumentException(template, this.GetTransitionGroupName(transitionGroupId), this.PrimaryStratumLabel, this.GetStratumName(stratumId), this.SecondaryStratumLabel, this.GetSecondaryStratumName(secondaryStratumId), this.TertiaryStratumLabel, this.GetTertiaryStratumName(tertiaryStratumId), this.GetStateClassName(stateClassId), STSimMapBase.FormatValue(iteration));
            }

            m.AddItem(iteration, tstRandomize);
            this.SetHasItems();
        }
    }
}
