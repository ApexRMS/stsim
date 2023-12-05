// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
	internal class InitialStockNonSpatial
	{
		private readonly int m_Id;
		private readonly int m_StockTypeId;
		private readonly int m_StateAttributeTypeId;

		public InitialStockNonSpatial(int id, int stockTypeId, int stateAttributeTypeId)
		{
			this.m_Id = id;
			this.m_StockTypeId = stockTypeId;
			this.m_StateAttributeTypeId = stateAttributeTypeId;
		}

		public int Id
		{
			get
			{
				return this.m_Id;
			}
		}

		public int StockTypeId
		{
			get
			{
				return this.m_StockTypeId;
			}
		}

		public int StateAttributeTypeId
		{
			get
			{
				return this.m_StateAttributeTypeId;
			}
		}
	}

}