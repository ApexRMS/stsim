// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal class StateClass
    {
        private int m_Id;
        private int m_StateLabelXID;
        private int m_StateLabelYID;

        public StateClass(int id, int stateLabelXID, int stateLabelYID)
        {
            this.m_Id = id;

            this.m_StateLabelXID = stateLabelXID;
            this.m_StateLabelYID = stateLabelYID;
        }

        public int Id
        {
            get
            {
                return this.m_Id;
            }
        }

        public int StateLabelXID
        {
            get
            {
                return this.m_StateLabelXID;
            }
        }

        public int StateLabelYID
        {
            get
            {
                return this.m_StateLabelYID;
            }
        }
    }
}
