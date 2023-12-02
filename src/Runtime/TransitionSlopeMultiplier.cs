// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;

namespace SyncroSim.STSim
{
    internal class TransitionSlopeMultiplier : STSimDistributionBase
    {
        private int m_TransitionGroupId;
        private int m_Slope;

        public TransitionSlopeMultiplier(
            int transitionGroupId, int? iteration, int? timestep, int? stratumId, int? secondaryStratumId, int? tertiaryStratumId, 
            int slope, double? multiplierAmount, int? distributionTypeId, DistributionFrequency? distributionFrequency, double? distributionSD, 
            double? distributionMin, double? distributionMax) : base(iteration, timestep, stratumId, secondaryStratumId, tertiaryStratumId, 
                multiplierAmount, distributionTypeId, distributionFrequency, distributionSD, distributionMin, distributionMax)
        {

            this.m_TransitionGroupId = transitionGroupId;
            this.m_Slope = slope;
        }

        public int TransitionGroupId
        {
            get
            {
                return this.m_TransitionGroupId;
            }
        }

        public int Slope
        {
            get
            {
                return this.m_Slope;
            }
        }

        public override STSimDistributionBase Clone()
        {
            return new TransitionSlopeMultiplier
                (this.TransitionGroupId, this.Iteration, this.Timestep, this.StratumId, this.SecondaryStratumId, this.TertiaryStratumId,
                this.Slope, this.DistributionValue, this.DistributionTypeId, this.DistributionFrequency, this.DistributionSD, 
                this.DistributionMin, this.DistributionMax);
        }
    }
}
