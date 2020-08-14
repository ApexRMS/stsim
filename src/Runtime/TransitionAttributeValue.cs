// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.StochasticTime;

namespace SyncroSim.STSim
{
    internal class TransitionAttributeValue : STSimDistributionBase
    {
        private int m_TransitionAttributeTypeId;
        private int m_TransitionGroupId;
        private int? m_StateClassId;
        private int m_MinimumAge;
        private int m_MaximumAge;

        public TransitionAttributeValue(
            int transitionAttributeTypeId, 
            int? stratumId, 
            int? secondaryStratumId, 
            int? tertiaryStratumId, 
            int? iteration, 
            int? timestep, 
            int transitionGroupId, 
            int? stateClassId, 
            int minimumAge, 
            int maximumAge,
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
            this.m_TransitionAttributeTypeId = transitionAttributeTypeId;
            this.m_TransitionGroupId = transitionGroupId;
            this.m_StateClassId = stateClassId;
            this.m_MinimumAge = minimumAge;
            this.m_MaximumAge = maximumAge;
        }

        public int TransitionAttributeTypeId
        {
            get
            {
                return this.m_TransitionAttributeTypeId;
            }
        }

        public int TransitionGroupId
        {
            get
            {
                return this.m_TransitionGroupId;
            }
        }

        public int? StateClassId
        {
            get
            {
                return this.m_StateClassId;
            }
        }

        public int MinimumAge
        {
            get
            {
                return this.m_MinimumAge;
            }
        }

        public int MaximumAge
        {
            get
            {
                return this.m_MaximumAge;
            }
        }

        public override STSimDistributionBase Clone()
        {
            return new TransitionAttributeValue(
                this.TransitionAttributeTypeId,
                this.StratumId,
                this.SecondaryStratumId,
                this.TertiaryStratumId,
                this.Iteration,
                this.Timestep,
                this.TransitionGroupId,
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
