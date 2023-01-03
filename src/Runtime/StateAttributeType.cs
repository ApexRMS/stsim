// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    class StateAttributeType
    {
        private int m_Id;
        private OutputFilterFlagAttribute m_OutputFilter;

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

        public OutputFilterFlagAttribute OutputFilter
        {
            get
            {
                return this.m_OutputFilter;
            }
            set
            {
                this.m_OutputFilter = value;
            }
        }
    }
}
