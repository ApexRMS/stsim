// stsimresolution: SyncroSim Add-On Package (to stsim) that enables multiple raster resolutions for spatial simulations.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Apex;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    internal class InitialConditionsFineSpatialMap
    {
        private List<InitialConditionsSpatial> m_AllItems = new List<InitialConditionsSpatial>();
        private SortedKeyMap1<InitialConditionsSpatial> m_Map = new SortedKeyMap1<InitialConditionsSpatial>(SearchMode.ExactPrev);

        public List<InitialConditionsSpatial> AllItems
        {
            get
            {
                return this.m_AllItems;
            }
        }

        public InitialConditionsFineSpatialMap(InitialConditionsSpatialCollection ics)
        {
            foreach (InitialConditionsSpatial t in ics)
            {
                this.AddICS(t);
            }
        }

        private void AddICS(InitialConditionsSpatial ics)
        {
            this.m_Map.AddItem(ics.Iteration, ics);
            this.m_AllItems.Add(ics);
        }

        public InitialConditionsSpatial GetICS(int? iteration)
        {
            if (this.m_AllItems.Count == 0)
            {
                return null;
            }

            InitialConditionsSpatial l = this.m_Map.GetItem(iteration);
            return l;
        }
    }

}
