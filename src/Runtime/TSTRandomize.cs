// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal class TstRandomize
    {
        private int m_MaxInitialTst;
        private int m_MinInitialTst;
        private int? m_TransitionGroupId;

        public TstRandomize(int minInitialTst, int maxInitialTst, int? transitionGroupId)
        {
            this.m_MinInitialTst = minInitialTst;
            this.m_MaxInitialTst = maxInitialTst;
            this.m_TransitionGroupId = transitionGroupId;
        }

        public int MaxInitialTst
        {
            get
            {
                return this.m_MaxInitialTst;
            }
        }

        public int MinInitialTst
        {
            get
            {
                return m_MinInitialTst;
            }
        }

        public int? TransitionGroupId
        {
            get
            {
                return this.m_TransitionGroupId;
            }
        }
    }
}
