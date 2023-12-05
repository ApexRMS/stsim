// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    class FlowTypeLinkage
    {
        private readonly FlowType m_FlowType;
        private readonly float m_Value;

        public FlowTypeLinkage(FlowType flowType, float value)
        {
            this.m_FlowType = flowType;
            this.m_Value = value;
        }

        internal FlowType FlowType
        {
            get
            {
                return m_FlowType;
            }
        }

        public float Value
        {
            get
            {
                return m_Value;
            }
        }
    }
}
