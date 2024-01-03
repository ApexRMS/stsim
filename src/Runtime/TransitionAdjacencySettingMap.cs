// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Collections.Generic;

namespace SyncroSim.STSim
{
    internal class TransitionAdjacencySettingMap
    {
        private Dictionary<int, TransitionAdjacencySetting> m_Map = new Dictionary<int, TransitionAdjacencySetting>();

        public TransitionAdjacencySettingMap(TransitionAdjacencySettingCollection settings)
        {
            foreach (TransitionAdjacencySetting s in settings)
            {
                this.AddItem(s);
            }
        }

        public void AddItem(TransitionAdjacencySetting setting)
        {
            if (!this.m_Map.ContainsKey(setting.TransitionGroupId))
            {
                this.m_Map.Add(setting.TransitionGroupId, setting);
            }
        }

        public TransitionAdjacencySetting GetItem(int transitionGroupId)
        {
            if (this.m_Map.ContainsKey(transitionGroupId))
            {
                return this.m_Map[transitionGroupId];
            }
            else
            {
                return null;
            }
        }
    }
}
