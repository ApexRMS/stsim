// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    class InitialTSTSpatial
    {
        private int? m_Iteration;
        private int? m_TSTGroupId;
        private string m_FileName;

        public InitialTSTSpatial(int? iteration, int? tstGroupId, string fileName)
        {
            this.m_Iteration = iteration;
            this.m_TSTGroupId = tstGroupId;
            this.m_FileName = fileName;
        }

        public int? Iteration
        {
            get
            {
                return this.m_Iteration;
            }
        }

        public int? TSTGroupId
        {
            get
            {
                return this.m_TSTGroupId;
            }
        }

        public string FileName
        {
            get
            {
                return this.m_FileName;
            }
        }
    }
}
