// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.StochasticTime;

namespace SyncroSim.STSim
{
    internal class TransitionDirectionMultiplier : STSimDistributionBase
    {
        private int m_TransitionGroupId;
        private CardinalDirection m_CardinalDirection;

        public TransitionDirectionMultiplier(
            int transitionGroupId, int? iteration, int? timestep, int? stratumId, int? secondaryStratumId, int? tertiaryStratumId, 
            CardinalDirection cardinalDirection, double? multiplierAmount, int? distributionTypeId, DistributionFrequency? distributionFrequency, 
            double? distributionSD, double? distributionMin, double? distributionMax) : base(iteration, timestep, stratumId, secondaryStratumId, 
                tertiaryStratumId, multiplierAmount, distributionTypeId, distributionFrequency, distributionSD, distributionMin, distributionMax)
        {
            this.m_TransitionGroupId = transitionGroupId;
            this.m_CardinalDirection = cardinalDirection;
        }

        public int TransitionGroupId
        {
            get
            {
                return this.m_TransitionGroupId;
            }
        }

        public CardinalDirection CardinalDirection
        {
            get
            {
                return this.m_CardinalDirection;
            }
        }

        public override STSimDistributionBase Clone()
        {
            return new TransitionDirectionMultiplier(
                this.TransitionGroupId, this.Iteration, this.Timestep, this.StratumId, this.SecondaryStratumId, this.TertiaryStratumId, 
                this.CardinalDirection, this.DistributionValue, this.DistributionTypeId, this.DistributionFrequency, this.DistributionSD, 
                this.DistributionMin, this.DistributionMax);
        }
    }
}
