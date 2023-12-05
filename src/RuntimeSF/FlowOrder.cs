// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
	internal class FlowOrder
	{
		private readonly int? m_iteration;
		private readonly int? m_timestep;
		private readonly int m_flowTypeId;
		private readonly double m_Order = Constants.DEFAULT_FLOW_ORDER;

		public FlowOrder(int? iteration, int? timestep, int flowTypeId, double? order)
		{
			this.m_iteration = iteration;
			this.m_timestep = timestep;
			this.m_flowTypeId = flowTypeId;

			if (order.HasValue)
			{
				this.m_Order = order.Value;
			}
		}

		public int? Iteration
		{
			get
			{
				return m_iteration;
			}
		}

		public int? Timestep
		{
			get
			{
				return m_timestep;
			}
		}

		public int FlowTypeId
		{
			get
			{
				return m_flowTypeId;
			}
		}

		public double Order
		{
			get
			{
				return m_Order;
			}
		}
	}
}