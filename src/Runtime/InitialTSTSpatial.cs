// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    class InitialTSTSpatial
    {
        private int? m_Iteration;
        private int? m_TransitionGroupId;
        private string m_FileName;

        public InitialTSTSpatial(int? iteration, int? transitionGroupId, string fileName)
        {
            this.m_Iteration = iteration;
            this.m_TransitionGroupId = transitionGroupId;
            this.m_FileName = fileName;
        }

        public int? Iteration
        {
            get
            {
                return this.m_Iteration;
            }
        }

        public int? TransitionGroupId
        {
            get
            {
                return this.m_TransitionGroupId;
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
