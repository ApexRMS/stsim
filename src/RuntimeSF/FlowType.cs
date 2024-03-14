// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
	internal class FlowType : StockFlowType
	{
        private readonly FlowGroupLinkageCollection m_FlowGroupLinkages = new FlowGroupLinkageCollection();
        private double m_Order = Constants.DEFAULT_FLOW_ORDER;

		public FlowType(int id, string name) : base(id, name)
		{
		}

        internal FlowGroupLinkageCollection FlowGroupLinkages
        {
            get
            {
                return this.m_FlowGroupLinkages;
            }
        }

        public double Order
		{
			get
			{
				return m_Order;
			}
			set
			{
				m_Order = value;
			}
		}
	}
}