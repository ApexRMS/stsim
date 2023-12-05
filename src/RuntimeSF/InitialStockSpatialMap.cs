// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Apex;
using System.Diagnostics;

namespace SyncroSim.STSim
{
	internal class InitialStockSpatialMap
	{
		private bool m_HasItems;
		private readonly SortedKeyMap1<InitialStockSpatialCollection> m_Map = new SortedKeyMap1<InitialStockSpatialCollection>(SearchMode.ExactPrev);

		public InitialStockSpatialMap(InitialStockSpatialCollection icd)
		{
			foreach (InitialStockSpatial t in icd)
			{
				this.AddISS(t);
			}
		}

		private void AddISS(InitialStockSpatial iss)
		{
			InitialStockSpatialCollection l = this.m_Map.GetItemExact(iss.Iteration);

			if (l == null)
			{
				l = new InitialStockSpatialCollection();
				this.m_Map.AddItem(iss.Iteration, l);
			}

			l.Add(iss);
			this.m_HasItems = true;
		}

		public InitialStockSpatialCollection GetItem(int? iteration)
		{
			if (!this.m_HasItems)
			{
				return null;
			}

			InitialStockSpatialCollection l = this.m_Map.GetItem(iteration);

			if (l == null)
			{
				return null;
			}

			Debug.Assert(l.Count > 0);
			return l;
		}
	}
}