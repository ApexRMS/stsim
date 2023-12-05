// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
	internal class InitialStockSpatial
	{
		private readonly int? m_Iteration = null;
		private readonly int m_StockTypeId;
		private readonly string m_filename;

		public InitialStockSpatial(int? iteration, int stockTypeId, string filename)
		{
			this.m_Iteration = iteration;
			this.m_StockTypeId = stockTypeId;
			this.m_filename = filename;
		}

		public int? Iteration
		{
			get
			{
				return m_Iteration;
			}
		}

		public int StockTypeId
		{
			get
			{
				return this.m_StockTypeId;
			}
		}

		public string Filename
		{
			get
			{
				return m_filename;
			}
		}
	}
}