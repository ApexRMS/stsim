// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

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
