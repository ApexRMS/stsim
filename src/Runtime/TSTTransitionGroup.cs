// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal class TstTransitionGroup
    {
        private int m_TSTGroupId;

        public TstTransitionGroup(int tstGroupId)
        {
            this.m_TSTGroupId = tstGroupId;
        }

        public int TSTGroupId
        {
            get
            {
                return this.m_TSTGroupId;
            }
        }
    }
}
