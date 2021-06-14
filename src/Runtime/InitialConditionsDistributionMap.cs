// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Common;
using System.Diagnostics;

namespace SyncroSim.STSim
{
    internal class InitialConditionsDistributionMap
    {
        private bool m_HasItems;
        private SortedKeyMap1<InitialConditionsDistributionCollection> m_Map = 
            new SortedKeyMap1<InitialConditionsDistributionCollection>(SearchMode.ExactPrev);

        public InitialConditionsDistributionMap(InitialConditionsDistributionCollection icd)
        {
            foreach (InitialConditionsDistribution t in icd)
            {
                this.AddICD(t);
            }
        }

        private void AddICD(InitialConditionsDistribution order)
        {
            InitialConditionsDistributionCollection l = this.m_Map.GetItemExact(order.Iteration);

            if (l == null)
            {
                l = new InitialConditionsDistributionCollection();
                this.m_Map.AddItem(order.Iteration, l);
            }

            l.Add(order);

            this.m_HasItems = true;
        }

        public InitialConditionsDistributionCollection GetICDs(int? iteration)
        {
            if (!this.m_HasItems)
            {
                return null;
            }

            InitialConditionsDistributionCollection l = this.m_Map.GetItem(iteration);

            if (l == null)
            {
                return null;
            }

            Debug.Assert(l.Count > 0);
            return l;
        }
    }
}
