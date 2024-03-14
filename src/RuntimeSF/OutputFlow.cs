// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
	internal class OutputFlow
	{
		private readonly int m_FromStratumId;
		private readonly int? m_FromSecondaryStratumId;
		private readonly int? m_FromTertiaryStratumId;
		private readonly int m_FromStateClassId;
		private readonly int? m_FromStockTypeId;
		private readonly int? m_transitionTypeId;
		private readonly int m_ToStratumId;
		private readonly int m_ToStateClassId;
		private readonly int? m_ToStockTypeId;
		private readonly int m_FlowGroupId;
        private readonly int? m_TransferToStratumId;
        private readonly int? m_TransferToSecondaryStratumId;
        private readonly int? m_TransferToTertiaryStratumId;
        private readonly int? m_TransferToStateClassId;
        private readonly int? m_TransferToMinimumAge;
		private double m_Amount;

		public OutputFlow(
            int fromStratumId, 
            int? fromSecondaryStratumId, 
            int? fromTertiaryStratumId, 
            int fromStateClassId, 
            int? fromStockTypeId, 
            int? transitionTypeId, 
            int toStratumId, 
            int toStateClassId, 
            int? toStockTypeId, 
            int flowGroupId, 
            int? transferToStratumId, 
            int? transferToSecondaryStratumId, 
            int? transferToTertiaryStratumId, 
            int? transferToStateClassId, 
            int? transferToMinimumAge,
            double amount)
		{
			this.m_FromStratumId = fromStratumId;
			this.m_FromSecondaryStratumId = fromSecondaryStratumId;
			this.m_FromTertiaryStratumId = fromTertiaryStratumId;
			this.m_FromStateClassId = fromStateClassId;
			this.m_FromStockTypeId = fromStockTypeId;
			this.m_transitionTypeId = transitionTypeId;
			this.m_ToStratumId = toStratumId;
			this.m_ToStateClassId = toStateClassId;
			this.m_ToStockTypeId = toStockTypeId;
			this.m_FlowGroupId = flowGroupId;
            this.m_TransferToStratumId = transferToStratumId;
            this.m_TransferToSecondaryStratumId = transferToSecondaryStratumId;
            this.m_TransferToTertiaryStratumId = transferToTertiaryStratumId;
            this.m_TransferToStateClassId = transferToStateClassId;
            this.m_TransferToMinimumAge = transferToMinimumAge;

			this.m_Amount = amount;
		}

		public int FromStratumId
		{
			get
			{
				return this.m_FromStratumId;
			}
		}

		public int? FromSecondaryStratumId
		{
			get
			{
				return this.m_FromSecondaryStratumId;
			}
		}

		public int? FromTertiaryStratumId
		{
			get
			{
				return this.m_FromTertiaryStratumId;
			}
		}

		public int FromStateClassId
		{
			get
			{
				return this.m_FromStateClassId;
			}
		}

		public int? FromStockTypeId
		{
			get
			{
				return this.m_FromStockTypeId;
			}
		}

		public int? TransitionTypeId
		{
			get
			{
				return this.m_transitionTypeId;
			}
		}

		public int ToStratumId
		{
			get
			{
				return this.m_ToStratumId;
			}
		}

		public int ToStateClassId
		{
			get
			{
				return this.m_ToStateClassId;
			}
		}

		public int? ToStockTypeId
		{
			get
			{
				return this.m_ToStockTypeId;
			}
		}

		public int FlowGroupId
		{
			get
			{
				return this.m_FlowGroupId;
			}
		}

        public int? TransferToStratumId
        {
            get
            {
                return this.m_TransferToStratumId;
            }
        }

        public int? TransferToSecondaryStratumId
        {
            get
            {
                return this.m_TransferToSecondaryStratumId;
            }
        }

        public int? TransferToTertiaryStratumId
        {
            get
            {
                return this.m_TransferToTertiaryStratumId;
            }
        }

        public int? TransferToStateClassId
        {
            get
            {
                return this.m_TransferToStateClassId;
            }
        }

        public int? TransferToMinimumAge
        {
            get
            {
                return this.m_TransferToMinimumAge;
            }
        }

        public double Amount
        {
            get
            {
                return this.m_Amount;
            }
            set
            {
                this.m_Amount = value;
            }
        }
    }
}
