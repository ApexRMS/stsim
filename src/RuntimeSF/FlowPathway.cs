// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
	internal class FlowPathway
	{
		private readonly int? m_Iteration;
		private readonly int? m_Timestep;
		private readonly int? m_FromStratumId;
		private readonly int? m_FromSecondaryStratumId;
		private readonly int? m_FromTertiaryStratumId;
		private readonly int? m_FromStateClassId;
		private readonly int? m_FromMinimumAge;
		private readonly int? m_FromStockTypeId;
		private readonly int? m_ToStratumId;
		private readonly int? m_ToStateClassId;
		private readonly int? m_ToMinimumAge;
		private readonly int? m_ToStockTypeId;
		private readonly int m_TransitionGroupId;
		private readonly int? m_StateAttributeTypeId;
		private readonly int m_FlowTypeId;
		private readonly TargetType m_TargetType = STSim.TargetType.Flow;
        private readonly double m_Multiplier;
        private readonly int? m_TransferToStratumId;
        private readonly int? m_TransferToSecondaryStratumId;
        private readonly int? m_TransferToTertiaryStratumId;
        private readonly int? m_TransferToStateClassId;
        private readonly int? m_TransferToMinimumAge;
        private float m_FlowAmount;
        private readonly bool m_IsLateral;

		public FlowPathway(
            int? iteration, 
            int? timestep, 
            int? fromStratumId,
            int? fromSecondaryStratumId,
            int? fromTertiaryStratumId, 
            int? fromStateClassId, 
            int? fromMinimumAge, 
            int? fromStockTypeId, 
            int? toStratumId, 
            int? toStateClassId, 
            int? toMinimumAge, 
            int? toStockTypeId, 
            int transitionGroupId, 
            int? stateAttributeTypeId, 
            int flowTypeId, 
            int? targetType,
            double multiplier,
            int? transferToStratumId,
            int? transferToSecondaryStratumId,
            int? transferToTertiaryStratumId,
            int? transferToStateClassId,
            int? transferToMinimumAge)
		{
			this.m_Iteration = iteration;
			this.m_Timestep = timestep;
			this.m_FromStratumId = fromStratumId;
            this.m_FromSecondaryStratumId = fromSecondaryStratumId;
            this.m_FromTertiaryStratumId = fromTertiaryStratumId;
			this.m_FromStateClassId = fromStateClassId;
			this.m_FromMinimumAge = fromMinimumAge;
			this.m_FromStockTypeId = fromStockTypeId;
			this.m_ToStratumId = toStratumId;
			this.m_ToStateClassId = toStateClassId;
			this.m_ToMinimumAge = toMinimumAge;
			this.m_ToStockTypeId = toStockTypeId;
			this.m_TransitionGroupId = transitionGroupId;
			this.m_StateAttributeTypeId = stateAttributeTypeId;
			this.m_FlowTypeId = flowTypeId;
			this.m_Multiplier = multiplier;
            this.m_TransferToStratumId = transferToStratumId;
            this.m_TransferToSecondaryStratumId = transferToSecondaryStratumId;
            this.m_TransferToTertiaryStratumId = transferToTertiaryStratumId;
            this.m_TransferToStateClassId = transferToStateClassId;
            this.m_TransferToMinimumAge = transferToMinimumAge;

            if (targetType.HasValue)
            {
                this.m_TargetType = (TargetType)targetType;
            }

            if (this.m_TransferToStratumId.HasValue ||
                this.m_TransferToSecondaryStratumId.HasValue ||
                this.m_TransferToTertiaryStratumId.HasValue ||
                this.m_TransferToStateClassId.HasValue ||
                this.m_TransferToMinimumAge.HasValue)
            {
                this.m_IsLateral = true;
            }
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

		public int? FromStratumId
		{
			get
			{
				return this.m_FromStratumId;
			}
		}

		public int? FromStateClassId
		{
			get
			{
				return this.m_FromStateClassId;
			}
		}

		public int? FromMinimumAge
		{
			get
			{
				return this.m_FromMinimumAge;
			}
		}

		public int? FromStockTypeId
		{
			get
			{
				return this.m_FromStockTypeId;
			}
		}

		public int? ToStratumId
		{
			get
			{
				return this.m_ToStratumId;
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

        public int? ToStateClassId
		{
			get
			{
				return this.m_ToStateClassId;
			}
		}

		public int? ToMinimumAge
		{
			get
			{
				return this.m_ToMinimumAge;
			}
		}

		public int? ToStockTypeId
		{
			get
			{
				return this.m_ToStockTypeId;
			}
		}

		public int TransitionGroupId
		{
			get
			{
				return this.m_TransitionGroupId;
			}
		}

		public int? StateAttributeTypeId
		{
			get
			{
				return this.m_StateAttributeTypeId;
			}
		}

		public int FlowTypeId
		{
			get
			{
				return this.m_FlowTypeId;
			}
		}

        public TargetType TargetType
        {
            get
            {
                return this.m_TargetType;
            }
        }

        public double Multiplier
		{
			get
			{
				return this.m_Multiplier;
			}
		}

		public float FlowAmount
		{
			get
			{
				return this.m_FlowAmount;
			}
			set
			{
				this.m_FlowAmount = value;
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

        public bool IsLateral
        {
            get
            {
                return m_IsLateral;
            }
        }
    }
}
