// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    /// <summary>
    /// Transition Attribute Type
    /// </summary>
    /// <remarks></remarks>
    internal class TransitionAttributeType
    {
        private int m_TransitionAttributeId;

        public TransitionAttributeType(int transitionAttributeId)
        {
            this.m_TransitionAttributeId = transitionAttributeId;
        }

        public int TransitionAttributeId
        {
            get
            {
                return this.m_TransitionAttributeId;
            }
        }
    }
}
