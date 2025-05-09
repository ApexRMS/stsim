﻿// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
	internal class FlowGroup : StockFlowType
	{
        private readonly FlowTypeLinkageCollection m_FlowTypeLinkages = new FlowTypeLinkageCollection();

        public FlowGroup(int id, string name) : base(id, name)
        {
        }

        internal FlowTypeLinkageCollection FlowTypeLinkages
        {
            get
            {
                return this.m_FlowTypeLinkages;
            }
        }
	}
}