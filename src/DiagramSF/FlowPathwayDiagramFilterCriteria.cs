// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Collections.Generic;

namespace SyncroSim.STSim
{
	internal class FlowPathwayDiagramFilterCriteria
	{
		private readonly Dictionary<int, bool> m_FlowTypes = new Dictionary<int, bool>();

		public Dictionary<int, bool> FlowTypes
		{
			get
			{
				return this.m_FlowTypes;
			}
		}
	}
}