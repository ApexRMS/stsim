// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal class TransitionStratumStateClass
    {
        private int m_StratumId;
        private int m_StateClassId;
        private TransitionCollection m_Transitions = new TransitionCollection();

        public TransitionStratumStateClass(int stratumId, int stateClassId)
        {
            this.m_StratumId = stratumId;
            this.m_StateClassId = stateClassId;
        }

        public int StratumId
        {
            get
            {
                return this.m_StratumId;
            }
        }

        public int StateClassId
        {
            get
            {
                return this.m_StateClassId;
            }
        }

        public TransitionCollection Transitions
        {
            get
            {
                return this.m_Transitions;
            }
        }
    }
}
