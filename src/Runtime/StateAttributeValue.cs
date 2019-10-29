// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.StochasticTime;

namespace SyncroSim.STSim
{
    internal class StateAttributeValue : AttributeValueBase
    {
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
                stateAttributeTypeId, 
                iteration, timestep, stratumId, secondaryStratumId, tertiaryStratumId, 
                stateClassId, minimumAge, maximumAge,
                value, 
                distributionTypeId, distributionFrequency, distributionSD, distributionMin, distributionMax)
        {
        }

        public override STSimDistributionBase Clone()
        {
            return new StateAttributeValue(
                this.AttributeTypeId,
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
