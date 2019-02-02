// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Common;

namespace SyncroSim.STSim
{
    internal class TransitionSpatialInitiationMultiplierMap
    {
        private MultiLevelKeyMap1<SortedKeyMap2<TransitionSpatialInitiationMultiplier>> m_Map = 
            new MultiLevelKeyMap1<SortedKeyMap2<TransitionSpatialInitiationMultiplier>>();

        public TransitionSpatialInitiationMultiplierMap(TransitionSpatialInitiationMultiplierCollection transitionSpatialInitiationMultipliers)
        {
            foreach (TransitionSpatialInitiationMultiplier m in transitionSpatialInitiationMultipliers)
            {
                this.AddMultiplierRaster(m);
            }
        }

        public TransitionSpatialInitiationMultiplier GetMultiplierRaster(int transitionGroupId, int iteration, int timestep)
        {
            SortedKeyMap2<TransitionSpatialInitiationMultiplier> m = this.m_Map.GetItem(transitionGroupId);

            if (m == null)
            {
                return null;
            }

            return m.GetItem(iteration, timestep);
        }

        private void AddMultiplierRaster(TransitionSpatialInitiationMultiplier multiplier)
        {
            SortedKeyMap2<TransitionSpatialInitiationMultiplier> m = this.m_Map.GetItemExact(multiplier.TransitionGroupId);

            if (m == null)
            {
                m = new SortedKeyMap2<TransitionSpatialInitiationMultiplier>(SearchMode.ExactPrev);
                this.m_Map.AddItem(multiplier.TransitionGroupId, m);
            }

            m.AddItem(multiplier.Iteration, multiplier.Timestep, multiplier);
        }
    }
}
