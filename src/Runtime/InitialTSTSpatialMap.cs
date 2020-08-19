// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using SyncroSim.Common;

namespace SyncroSim.STSim
{
    class InitialTSTSpatialMap : STSimMapBase
    {
        private bool m_HasItems;

        private MultiLevelKeyMap1<SortedKeyMap1<string>> m_map = 
            new MultiLevelKeyMap1<SortedKeyMap1<string>>();

        public InitialTSTSpatialMap(Scenario scenario) : base(scenario)
        {
        }

        public void AddFile(int? transitionGroupId, int? iteration, string fileName)
        {
            SortedKeyMap1<string> m = this.m_map.GetItemExact(transitionGroupId);

            if (m == null)
            {
                m = new SortedKeyMap1<string>(SearchMode.ExactPrev);
                this.m_map.AddItem(transitionGroupId, m);
            }

            m.AddItem(iteration, fileName);
            this.m_HasItems = true;
        }

        public string GetFile(int transitionGroupId, int iteration)
        {
            if (!this.m_HasItems)
            {
                return null;
            }

            SortedKeyMap1<string> m = this.m_map.GetItem(transitionGroupId);

            if (m == null)
            {
                return null;
            }

            return m.GetItem(iteration);
        }
    }
}
