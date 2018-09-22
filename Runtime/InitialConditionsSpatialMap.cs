// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Common;

namespace SyncroSim.STSim
{
    internal class InitialConditionsSpatialMap
    {
        private bool m_HasItems;
        private SortedKeyMap1<InitialConditionsSpatial> m_Map = new SortedKeyMap1<InitialConditionsSpatial>(SearchMode.ExactPrev);

        public InitialConditionsSpatialMap(InitialConditionsSpatialCollection ics)
        {
            foreach (InitialConditionsSpatial t in ics)
            {
                this.AddICS(t);
            }
        }

        private void AddICS(InitialConditionsSpatial ics)
        {
            this.m_Map.AddItem(ics.Iteration, ics);

            this.m_HasItems = true;
        }

        public InitialConditionsSpatial GetICS(int? iteration)
        {
            if (!this.m_HasItems)
            {
                return null;
            }

            InitialConditionsSpatial l = this.m_Map.GetItem(iteration);

            return l;
        }
    }
}
