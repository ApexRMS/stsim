// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
	internal class StockLimit
	{
		private readonly int? m_Iteration;
		private readonly int? m_Timestep;
		private readonly int m_StockTypeId;
		private readonly int? m_StratumId;
		private readonly int? m_SecondaryStratumId;
		private readonly int? m_TertiaryStratumId;
		private readonly int? m_StateClassId;
		private readonly double m_StockMinimum;
		private readonly double m_StockMaximum;

		public StockLimit(
            int? iteration, int? timestep, int stockTypeId, int? stratumId, int? secondaryStratumId, int? tertiaryStratumId, 
            int? stateClassId, float stockMinimum, float stockMaximum)
		{
			this.m_Iteration = iteration;
			this.m_Timestep = timestep;
			this.m_StockTypeId = stockTypeId;
			this.m_StratumId = stratumId;
			this.m_SecondaryStratumId = secondaryStratumId;
			this.m_TertiaryStratumId = tertiaryStratumId;
			this.m_StateClassId = stateClassId;
			this.m_StockMinimum = stockMinimum;
			this.m_StockMaximum = stockMaximum;
		}

		public int? Iteration
		{
			get
			{
				return this.m_Iteration;
			}
		}

		public int? Timestep
		{
			get
			{
				return this.m_Timestep;
			}
		}

		public int StockTypeId
		{
			get
			{
				return this.m_StockTypeId;
			}
		}

		public int? StratumId
		{
			get
			{
				return this.m_StratumId;
			}
		}

		public int? SecondaryStratumId
		{
			get
			{
				return this.m_SecondaryStratumId;
			}
		}

		public int? TertiaryStratumId
		{
			get
			{
				return this.m_TertiaryStratumId;
			}
		}

		public int? StateClassId
		{
			get
			{
				return this.m_StateClassId;
			}
		}

		public double StockMinimum
		{
			get
			{
				return this.m_StockMinimum;
			}
		}

		public double StockMaximum
		{
			get
			{
				return this.m_StockMaximum;
			}
		}
	}
}