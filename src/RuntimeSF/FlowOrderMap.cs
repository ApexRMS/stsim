// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Apex;
using System.Diagnostics;

namespace SyncroSim.STSim
{
	internal class FlowOrderMap
	{
		private bool m_HasItems;
		private readonly SortedKeyMap2<FlowOrderCollection> m_Map = new SortedKeyMap2<FlowOrderCollection>(SearchMode.ExactPrev);

		public FlowOrderMap(FlowOrderCollection orders)
		{
			foreach (FlowOrder o in orders)
			{
				this.AddOrder(o);
			}
		}

		private void AddOrder(FlowOrder order)
		{
			FlowOrderCollection l = this.m_Map.GetItemExact(order.Iteration, order.Timestep);

			if (l == null)
			{
				l = new FlowOrderCollection();
				this.m_Map.AddItem(order.Iteration, order.Timestep, l);
			}

			l.Add(order);
			this.m_HasItems = true;
		}

		public FlowOrderCollection GetOrders(int iteration, int timestep)
		{
			if (!this.m_HasItems)
			{
				return null;
			}

			FlowOrderCollection l = this.m_Map.GetItem(iteration, timestep);

			if (l == null)
			{
				return null;
			}

			Debug.Assert(l.Count > 0);
			return l;
		}
	}
}