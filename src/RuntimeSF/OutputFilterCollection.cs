// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Collections.Generic;

namespace SyncroSim.STSim
{
    class OutputFilterCollection
    {
        readonly Dictionary<int, OutputFilterBase> m_Items = new Dictionary<int, OutputFilterBase>();

		public bool HasItems
		{
			get
			{
				return (this.m_Items.Count > 0);
			}
		}

		public void Add(OutputFilterBase item)
        {
			this.m_Items.Add(item.Id, item);
        }

		public OutputFilterBase Get(int id)
        {
			if (this.m_Items.ContainsKey(id))
            {
				return this.m_Items[id];
            }

			return null;
        }
	}
}
