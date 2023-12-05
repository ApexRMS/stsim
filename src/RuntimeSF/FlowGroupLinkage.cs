// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    class FlowGroupLinkage
    {
        private readonly FlowGroup m_FlowGroup;
        private readonly double m_Value;

        public FlowGroupLinkage(FlowGroup flowGroup, double value)
        {
            this.m_FlowGroup = flowGroup;
            this.m_Value = value;
        }

        internal FlowGroup FlowGroup
        {
            get
            {
                return m_FlowGroup;
            }
        }

        public double Value
        {
            get
            {
                return m_Value;
            }
        }
    }
}
