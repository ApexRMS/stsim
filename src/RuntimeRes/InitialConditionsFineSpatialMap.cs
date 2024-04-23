// stsimresolution: SyncroSim Add-On Package (to stsim) that enables multiple raster resolutions for spatial simulations.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Apex;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    internal class InitialConditionsFineSpatialMap
    {
        private List<InitialConditionsFineSpatial> m_AllItems = new List<InitialConditionsFineSpatial>();
        private SortedKeyMap1<InitialConditionsFineSpatial> m_Map = new SortedKeyMap1<InitialConditionsFineSpatial>(SearchMode.ExactPrev);

        public List<InitialConditionsFineSpatial> AllItems
        {
            get
            {
                return this.m_AllItems;
            }
        }

        public InitialConditionsFineSpatialMap(InitialConditionsFineSpatialCollection ics)
        {
            foreach (InitialConditionsFineSpatial t in ics)
            {
                this.AddICS(t);
            }
        }

        private void AddICS(InitialConditionsFineSpatial ics)
        {
            this.m_Map.AddItem(ics.Iteration, ics);
            this.m_AllItems.Add(ics);
        }

        public InitialConditionsFineSpatial GetICS(int? iteration)
        {
            if (this.m_AllItems.Count == 0)
            {
                return null;
            }

            InitialConditionsFineSpatial l = this.m_Map.GetItem(iteration);
            return l;
        }
    }

}
