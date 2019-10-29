// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.StochasticTime;

namespace SyncroSim.STSim
{
    internal class StateAttributeValue : STSimDistributionBase
    {
        private int m_StateAttributeTypeId;
        private int? m_StateClassId;
        private int? m_MinimumAge;
        private int? m_MaximumAge;

        public StateAttributeValue(
            int stateAttributeTypeId, 
            int? stratumId, 
            int? secondaryStratumId, 
            int? tertiaryStratumId, 
            int? iteration, 
            int? timestep, 
            int? stateClassId, 
            int? minimumAge, 
            int? maximumAge, 
            double? value,
            int? distributionTypeId,
            DistributionFrequency? distributionFrequency,
            double? distributionSD,
            double? distributionMin,
            double? distributionMax) : base(
                iteration, timestep, stratumId, secondaryStratumId, tertiaryStratumId,
                value,
                distributionTypeId, distributionFrequency, distributionSD, distributionMin, distributionMax)
        {
            this.m_StateAttributeTypeId = stateAttributeTypeId;
            this.m_StateClassId = stateClassId;
            this.m_MinimumAge = minimumAge;
            this.m_MaximumAge = maximumAge;
        }

        public int StateAttributeTypeId
        {
            get
            {
                return this.m_StateAttributeTypeId;
            }
        }

        public int? StateClassId
        {
            get
            {
                return this.m_StateClassId;
            }
        }

        public int? MinimumAge
        {
            get
            {
                return this.m_MinimumAge;
            }
        }

        public int? MaximumAge
        {
            get
            {
                return this.m_MaximumAge;
            }
        }

        public override STSimDistributionBase Clone()
        {
            return new StateAttributeValue(
                this.StateAttributeTypeId,
                this.StratumId,
                this.SecondaryStratumId,
                this.TertiaryStratumId,
                this.Iteration,
                this.Timestep,
                this.StateClassId,
                this.MinimumAge,
                this.MaximumAge,
                this.DistributionValue,
                this.DistributionTypeId,
                this.DistributionFrequency,
                this.DistributionSD,
                this.DistributionMin,
                this.DistributionMax);
        }
    }
}
