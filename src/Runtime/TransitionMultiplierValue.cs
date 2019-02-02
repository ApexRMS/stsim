// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.StochasticTime;

namespace SyncroSim.STSim
{
    internal class TransitionMultiplierValue : STSimDistributionBase
    {
        private int m_TransitionGroupId;
        private int? m_StateClassId;
        private int? m_TransitionMultiplierTypeId;

        public TransitionMultiplierValue(
            int transitionGroupId, int? iteration, int? timestep, int? stratumId, int? secondaryStratumId, int? tertiaryStratumId, int? stateClassId, 
            int? transitionMultiplierTypeId, double? multiplierValue, int? distributionTypeId, DistributionFrequency? distributionFrequency, 
            double? distributionSD, double? distributionMin, double? distributionMax) : base(iteration, timestep, stratumId, secondaryStratumId, 
                tertiaryStratumId, multiplierValue, distributionTypeId, distributionFrequency, distributionSD, distributionMin, distributionMax)
        {

            this.m_TransitionGroupId = transitionGroupId;
            this.m_StateClassId = stateClassId;
            this.m_TransitionMultiplierTypeId = transitionMultiplierTypeId;
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

        public int? TransitionMultiplierTypeId
        {
            get
            {
                return this.m_TransitionMultiplierTypeId;
            }
        }

        public override STSimDistributionBase Clone()
        {
            return new TransitionMultiplierValue(
                this.TransitionGroupId, this.Iteration, this.Timestep, this.StratumId, this.SecondaryStratumId, this.TertiaryStratumId, 
                this.StateClassId, this.TransitionMultiplierTypeId, this.DistributionValue, this.DistributionTypeId, this.DistributionFrequency, 
                this.DistributionSD, this.DistributionMin, this.DistributionMax);
        }
    }
}
