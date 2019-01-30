// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.StochasticTime;

namespace SyncroSim.STSim
{
    internal class TransitionAdjacencyMultiplier : STSimDistributionBase
    {
        private int m_TransitionGroupId;
        private double m_AttributeValue;

        public TransitionAdjacencyMultiplier(
            int transitionGroupId, int? iteration, int? timestep, int? stratumId, int? secondaryStratumId, int? tertiaryStratumId, 
            double attributeValue, double? multiplierAmount, int? distributionTypeId, DistributionFrequency? distributionFrequency, 
            double? distributionSD, double? distributionMin, double? distributionMax) : base(iteration, timestep, stratumId, 
                secondaryStratumId, tertiaryStratumId, multiplierAmount, distributionTypeId, distributionFrequency, 
                distributionSD, distributionMin, distributionMax)
        {

            this.m_TransitionGroupId = transitionGroupId;
            this.m_AttributeValue = attributeValue;
        }

        public int TransitionGroupId
        {
            get
            {
                return this.m_TransitionGroupId;
            }
        }

        public double AttributeValue
        {
            get
            {
                return this.m_AttributeValue;
            }
        }

        public override STSimDistributionBase Clone()
        {
            return new TransitionAdjacencyMultiplier(
                this.TransitionGroupId, this.Iteration, this.Timestep, this.StratumId, this.SecondaryStratumId, this.TertiaryStratumId, 
                this.AttributeValue, this.DistributionValue, this.DistributionTypeId, this.DistributionFrequency, this.DistributionSD, 
                this.DistributionMin, this.DistributionMax);
        }
    }
}
