// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Collections.Generic;

namespace SyncroSim.STSim
{
    class OutputFilterTransitionGroupCollection
    {
		Dictionary<int, OutputFilterTransitionGroup> m_Items = new Dictionary<int, OutputFilterTransitionGroup>();

		public bool HasItems
		{
			get
			{
				return (this.m_Items.Count > 0);
			}
		}

		public void Add(OutputFilterTransitionGroup item)
		{
			this.m_Items.Add(item.Id, item);
		}

		public OutputFilterTransitionGroup Get(int id)
		{
			if (this.m_Items.ContainsKey(id))
			{
				return this.m_Items[id];
			}

			return null;
		}
	}
}
