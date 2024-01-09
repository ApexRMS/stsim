// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    /// <summary>
    /// Transition Attribute Type
    /// </summary>
    /// <remarks></remarks>
    internal class TransitionAttributeType
    {
        private int m_TransitionAttributeId;
        private OutputFilterFlagAttribute m_OutputFilter;

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
