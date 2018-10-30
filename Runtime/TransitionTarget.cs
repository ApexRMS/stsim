// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.StochasticTime;

namespace SyncroSim.STSim
{
    internal class TransitionTarget : STSimDistributionBase
    {
        private int m_TransitionGroupId;
        private double m_ExpectedAmount;
        private double m_Multiplier = 1.0;

        public TransitionTarget(
            int? iteration, int? timestep, int? stratumId, int? secondaryStratumId, int? tertiaryStratumId, 
            int transitionGroupId, double? targetAmount, int? distributionTypeId, DistributionFrequency? distributionFrequency,
            double? distributionSD, double? distributionMin, double? distributionMax) : base(iteration, timestep, stratumId, secondaryStratumId, 
                tertiaryStratumId, targetAmount, distributionTypeId, distributionFrequency, distributionSD, distributionMin, distributionMax)
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

        public double ExpectedAmount
        {
            get
            {
                return this.m_ExpectedAmount;
            }
            set
            {
                this.m_ExpectedAmount = value;
            }
        }

        public double Multiplier
        {
            get
            {
                return this.m_Multiplier;
            }
            set
            {
                this.m_Multiplier = value;
            }
        }

        public override STSimDistributionBase Clone()
        {
            TransitionTarget t = new TransitionTarget(
                this.Iteration, this.Timestep, this.StratumId, this.SecondaryStratumId, this.TertiaryStratumId, this.TransitionGroupId, 
                this.DistributionValue, this.DistributionTypeId, this.DistributionFrequency, this.DistributionSD, this.DistributionMin, 
                this.DistributionMax);

            t.ExpectedAmount = this.ExpectedAmount;
            t.Multiplier = this.Multiplier;

            return t;
        }
    }
}
