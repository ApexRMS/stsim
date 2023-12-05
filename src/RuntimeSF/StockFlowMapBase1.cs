// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using SyncroSim.Apex;

namespace SyncroSim.STSim
{
	internal abstract class StockFlowMapBase1<T> : StockFlowMapBase
	{
		private readonly MultiLevelKeyMap1<SortedKeyMap2<T>> m_map = new MultiLevelKeyMap1<SortedKeyMap2<T>>();

		protected StockFlowMapBase1(Scenario scenario) : base(scenario)
		{
		}

		protected void AddItem(int? k1, int? iteration, int? timestep, T item)
		{
			SortedKeyMap2<T> m = this.m_map.GetItemExact(k1);

			if (m == null)
			{
				m = new SortedKeyMap2<T>(SearchMode.ExactPrev);
				this.m_map.AddItem(k1, m);
			}

			T v = m.GetItemExact(iteration, timestep);

			if (v != null)
			{
				ThrowDuplicateItemException();
			}

			m.AddItem(iteration, timestep, item);
			this.SetHasItems();
		}

		protected T GetItem(int? k1, int? iteration, int? timestep)
		{
			if (!this.HasItems)
			{
				return default(T);
			}

			SortedKeyMap2<T> p = this.m_map.GetItem(k1);

			if (p == null)
			{
				return default(T);
			}

			return p.GetItem(iteration, timestep);
		}
	}
}