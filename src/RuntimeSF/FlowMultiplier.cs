// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;

namespace SyncroSim.STSim
{
    internal class FlowMultiplier : STSimDistributionBase
    {
        private readonly int? m_StateClassId;
        private readonly int m_AgeMin;
        private readonly int m_AgeMax = int.MaxValue;
        private readonly int? m_FlowMultiplierTypeId;
        private readonly int m_FlowGroupId;

        public FlowMultiplier(
                int? iteration,
                int? timestep,
                int? stratumId,
                int? secondaryStratumId,
                int? tertiaryStratumId,
                int? stateClassId,
                int ageMin,
                int ageMax,
                int? flowMultiplierTypeId,
                int flowGroupId,
                double? multiplierValue,
                int? distributionTypeId,
                DistributionFrequency? distributionFrequency,
                double? distributionSD,
                double? distributionMin,
                double? distributionMax) : base(
                    iteration, timestep, stratumId, secondaryStratumId,
                    tertiaryStratumId, multiplierValue, distributionTypeId,
                    distributionFrequency, distributionSD, distributionMin, distributionMax)
        {
            this.m_StateClassId = stateClassId;
            this.m_AgeMin = ageMin;
            this.m_AgeMax = ageMax;
            this.m_FlowMultiplierTypeId = flowMultiplierTypeId;
            this.m_FlowGroupId = flowGroupId;
        }

        public int? StateClassId
        {
            get
            {
                return this.m_StateClassId;
            }
        }

        public int AgeMin
        {
            get
            {
                return this.m_AgeMin;
            }
        }

        public int AgeMax
        {
            get
            {
                return this.m_AgeMax;
            }
        }

        public int? FlowMultiplierTypeId
        {
            get
            {
                return this.m_FlowMultiplierTypeId;
            }
        }

        public int FlowGroupId
        {
            get
            {
                return this.m_FlowGroupId;
            }
        }

        public override STSimDistributionBase Clone()
        {
            return new FlowMultiplier(
                      this.Iteration, this.Timestep,
                      this.StratumId, this.SecondaryStratumId, this.TertiaryStratumId,
                      this.StateClassId, this.AgeMin, this.AgeMax, this.FlowMultiplierTypeId, this.FlowGroupId,
                      this.DistributionValue, this.DistributionTypeId, this.DistributionFrequency,
                      this.DistributionSD, this.DistributionMin, this.DistributionMax);
        }
    }
}
