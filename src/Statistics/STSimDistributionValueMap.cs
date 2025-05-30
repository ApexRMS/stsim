﻿// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Apex;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    internal class STSimDistributionValueMap
    {
        private MultiLevelKeyMap3<SortedKeyMap2<DistributionValueCollection>> m_Map = new MultiLevelKeyMap3<SortedKeyMap2<DistributionValueCollection>>();

        public void AddValue(STSimDistributionValue value)
        {
            SortedKeyMap2<DistributionValueCollection> m = this.m_Map.GetItemExact(value.StratumId, value.SecondaryStratumId, value.DistributionTypeId);

            if (m == null)
            {
                m = new SortedKeyMap2<DistributionValueCollection>(SearchMode.ExactPrevNext);
                this.m_Map.AddItem(value.StratumId, value.SecondaryStratumId, value.DistributionTypeId, m);
            }

            DistributionValueCollection c = m.GetItemExact(value.Iteration, value.Timestep);

            if (c == null)
            {
                c = new DistributionValueCollection();
                m.AddItem(value.Iteration, value.Timestep, c);
            }

            c.Add(value);
        }

        public DistributionValueCollection GetValues(int distributionTypeId, int iteration, int timestep, int? stratumId, int? secondaryStratumId)
        {
            SortedKeyMap2<DistributionValueCollection> m = this.m_Map.GetItem(stratumId, secondaryStratumId, distributionTypeId);

            if (m == null)
            {
                return null;
            }

            DistributionValueCollection c = m.GetItem(iteration, timestep);

            if (c == null)
            {
                return null;
            }

            return c;
        }
    }
}
