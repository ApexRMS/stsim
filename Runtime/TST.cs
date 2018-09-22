// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal class Tst
    {
        private int m_TstValue;
        private int m_TransitionGroupId;

        public Tst(int transitionGroupId)
        {
            this.m_TransitionGroupId = transitionGroupId;
        }

        public int TstValue
        {
            get
            {
                return this.m_TstValue;
            }
            set
            {
                this.m_TstValue = value;
            }
        }

        public int TransitionGroupId
        {
            get
            {
                return this.m_TransitionGroupId;
            }
        }
    }
}
