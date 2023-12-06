// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal class StateClass
    {
        private int m_Id;
        private int m_StateLabelXId;
        private int m_StateLabelYId;
        private string m_DisplayName;

        public StateClass(
            int id, 
            int stateLabelXId, 
            int stateLabelYId, 
            string displayName)
        {
            this.m_Id = id;
            this.m_StateLabelXId = stateLabelXId;
            this.m_StateLabelYId = stateLabelYId;
            this.m_DisplayName = displayName;
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
                return this.m_StateLabelXId;
            }
        }

        public int StateLabelYID
        {
            get
            {
                return this.m_StateLabelYId;
            }
        }

        public string DisplayName
        {
            get
            {
                return this.m_DisplayName;
            }
        }
    }
}
