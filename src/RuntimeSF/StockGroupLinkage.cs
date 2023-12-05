// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    class StockGroupLinkage
    {
        private readonly StockGroup m_StockGroup;
        private readonly double m_Value;

        public StockGroupLinkage(StockGroup stockGroup, double value)
        {
            this.m_StockGroup = stockGroup;
            this.m_Value = value;
        }

        internal StockGroup StockGroup
        {
            get
            {
                return m_StockGroup;
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
