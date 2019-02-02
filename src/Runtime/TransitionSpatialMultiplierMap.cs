// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Common;

namespace SyncroSim.STSim
{
    internal class TransitionSpatialMultiplierMap
    {
        private MultiLevelKeyMap1<SortedKeyMap2<TransitionSpatialMultiplier>> m_Map = 
            new MultiLevelKeyMap1<SortedKeyMap2<TransitionSpatialMultiplier>>();

        public TransitionSpatialMultiplierMap(TransitionSpatialMultiplierCollection transitionSpatialMultipliers)
        {
            foreach (TransitionSpatialMultiplier m in transitionSpatialMultipliers)
            {
                this.AddMultiplierRaster(m);
            }
        }

        public TransitionSpatialMultiplier GetMultiplierRaster(int transitionGroupId, int iteration, int timestep)
        {
            SortedKeyMap2<TransitionSpatialMultiplier> m = this.m_Map.GetItem(transitionGroupId);

            if (m == null)
            {
                return null;
            }

            return m.GetItem(iteration, timestep);
        }

        private void AddMultiplierRaster(TransitionSpatialMultiplier multiplier)
        {
            SortedKeyMap2<TransitionSpatialMultiplier> m = this.m_Map.GetItemExact(multiplier.TransitionGroupId);

            if (m == null)
            {
                m = new SortedKeyMap2<TransitionSpatialMultiplier>(SearchMode.ExactPrev);
                this.m_Map.AddItem(multiplier.TransitionGroupId, m);
            }

            m.AddItem(multiplier.Iteration, multiplier.Timestep, multiplier);
        }
    }
}
