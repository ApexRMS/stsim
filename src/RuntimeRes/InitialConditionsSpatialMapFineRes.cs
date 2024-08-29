// stsimresolution: SyncroSim Add-On Package (to stsim) that enables multiple raster resolutions for spatial simulations.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Apex;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    internal class InitialConditionsSpatialMapFineRes
    {
        private List<InitialConditionsSpatialFineRes> m_AllItems = new List<InitialConditionsSpatialFineRes>();
        private SortedKeyMap1<InitialConditionsSpatialFineRes> m_Map = new SortedKeyMap1<InitialConditionsSpatialFineRes>(SearchMode.ExactPrev);

        public List<InitialConditionsSpatialFineRes> AllItems
        {
            get
            {
                return this.m_AllItems;
            }
        }

        public InitialConditionsSpatialMapFineRes(InitialConditionsSpatialCollectionFineRes ics)
        {
            foreach (InitialConditionsSpatialFineRes t in ics)
            {
                this.AddICS(t);
            }
        }

        private void AddICS(InitialConditionsSpatialFineRes ics)
        {
            this.m_Map.AddItem(ics.Iteration, ics);
            this.m_AllItems.Add(ics);
        }

        public InitialConditionsSpatialFineRes GetICS(int? iteration)
        {
            if (this.m_AllItems.Count == 0)
            {
                return null;
            }

            InitialConditionsSpatialFineRes l = this.m_Map.GetItem(iteration);
            return l;
        }
    }

}
