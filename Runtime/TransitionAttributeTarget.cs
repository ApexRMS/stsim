// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.StochasticTime;

namespace SyncroSim.STSim
{
    internal class TransitionAttributeTarget : STSimDistributionBase
    {
        private int m_TransitionAttributeTargetId;
        private int m_TransitionAttributeTypeId;
        private double m_TargetRemaining;
        private double m_ExpectedAmount;
        private double m_Multiplier = 1.0;

        public TransitionAttributeTarget(
            int transitionAttributeTargetId, int? iteration, int? timestep, int? stratumId, int? secondaryStratumId, int? tertiaryStratumId, 
            int transitionAttributeTypeId, double? targetAmount, int? distributionTypeId, DistributionFrequency? distributionFrequency, 
            double? distributionSD, double? distributionMin, double? distributionMax) : base(iteration, timestep, stratumId, secondaryStratumId,
                tertiaryStratumId, targetAmount, distributionTypeId, distributionFrequency, distributionSD, distributionMin, distributionMax)
        {

            this.m_TransitionAttributeTargetId = transitionAttributeTargetId;
            this.m_TransitionAttributeTypeId = transitionAttributeTypeId;
        }

        public int TransitionAttributeTargetId
        {
            get
            {
                return this.m_TransitionAttributeTargetId;
            }
        }

        public int TransitionAttributeTypeId
        {
            get
            {
                return this.m_TransitionAttributeTypeId;
            }
        }

        public double TargetRemaining
        {
            get
            {
                return this.m_TargetRemaining;
            }
            set
            {
                this.m_TargetRemaining = value;
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

        public override STSimDistributionBase Clone()
        {
            TransitionAttributeTarget t = new TransitionAttributeTarget(
                this.TransitionAttributeTargetId, this.Iteration, this.Timestep, this.StratumId, this.SecondaryStratumId, this.TertiaryStratumId, 
                this.TransitionAttributeTypeId, this.DistributionValue, this.DistributionTypeId, this.DistributionFrequency, this.DistributionSD, 
                this.DistributionMin, this.DistributionMax);

            t.TargetRemaining = this.TargetRemaining;
            t.Multiplier = this.Multiplier;
            t.ExpectedAmount = this.ExpectedAmount;

            return t;
        }
    }
}
