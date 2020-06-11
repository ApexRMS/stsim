// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    class StateAttributeType
    {
        private int m_Id;

        public StateAttributeType(int id)
        {
            this.m_Id = id;
        }

        public int Id
        {
            get
            {
                return this.m_Id;
            }
        }
    }
}
