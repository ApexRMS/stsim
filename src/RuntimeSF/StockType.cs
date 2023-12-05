// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
	internal class StockType : StockFlowType
	{
        private readonly StockGroupLinkageCollection m_StockGroupLinkages = new StockGroupLinkageCollection();

		public StockType(int id, string name) : base(id, name)
		{
		}

        internal StockGroupLinkageCollection StockGroupLinkages
        {
            get
            {
                return this.m_StockGroupLinkages;
            }
        }
    }
}
