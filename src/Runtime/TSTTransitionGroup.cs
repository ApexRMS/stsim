// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal class TstTransitionGroup
    {
        private int m_GroupId;

        public TstTransitionGroup(int groupId)
        {
            this.m_GroupId = groupId;
        }

        public int GroupId
        {
            get
            {
                return this.m_GroupId;
            }
        }
    }
}
