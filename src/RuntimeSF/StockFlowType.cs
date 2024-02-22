// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Diagnostics;

namespace SyncroSim.STSim
{
	internal class StockFlowType
	{
		private readonly int m_Id;
        private readonly string m_Name;
        private OutputFilter m_OutputFilter = OutputFilter.None;

        public StockFlowType(int id, string name)
		{
			this.m_Id = id;
            this.m_Name = name;
		}

		public int Id
		{
			get
			{
				return this.m_Id;
			}
		}

        public string Name
        {
            get
            {
                return this.m_Name;
            }
        }

        internal OutputFilter OutputFilter
        {
            get
            {
                return this.m_OutputFilter;
            }
            set
            {
                this.m_OutputFilter = value;
            }
        }
    }
}
