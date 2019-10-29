// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.StochasticTime;

namespace SyncroSim.STSim
{
    internal class TransitionAttributeValue : AttributeValueBase
    {
        private int m_TransitionGroupId;

        public TransitionAttributeValue(
            int transitionAttributeTypeId, 
            int? stratumId, 
            int? secondaryStratumId, 
            int? tertiaryStratumId, 
            int? iteration, 
            int? timestep, 
            int transitionGroupId, 
            int? stateClassId, 
            int? minimumAge, 
            int? maximumAge,
            double? value,
            int? distributionTypeId,
            DistributionFrequency? distributionFrequency,
            double? distributionSD,
            double? distributionMin,
            double? distributionMax) : base(
                transitionAttributeTypeId,
                iteration, timestep, stratumId, secondaryStratumId, tertiaryStratumId,
                stateClassId, minimumAge, maximumAge,
                value,
                distributionTypeId, distributionFrequency, distributionSD, distributionMin, distributionMax)
        {
            this.m_TransitionGroupId = transitionGroupId;
        }

        public int TransitionGroupId
        {
            get
            {
                return this.m_TransitionGroupId;
            }
        }

        public override STSimDistributionBase Clone()
        {
            return new TransitionAttributeValue(
                this.AttributeTypeId,
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
