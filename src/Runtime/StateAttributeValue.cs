// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;

namespace SyncroSim.STSim
{
    internal class StateAttributeValue : STSimDistributionBase
    {
        private int m_StateAttributeTypeId;
        private int? m_StateClassId;
        private int m_MinimumAge;
        private int m_MaximumAge;
        private int? m_TSTGroupId;
        private int? m_TSTMin;
        private int? m_TSTMax;

        public StateAttributeValue(
            int stateAttributeTypeId, 
            int? stratumId, 
            int? secondaryStratumId, 
            int? tertiaryStratumId, 
            int? iteration, 
            int? timestep, 
            int? stateClassId, 
            int minimumAge, 
            int maximumAge, 
            int? tstGroupId,
            int? tstMin,
            int? tstMax,
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
            this.m_StateAttributeTypeId = stateAttributeTypeId;
            this.m_StateClassId = stateClassId;
            this.m_MinimumAge = minimumAge;
            this.m_MaximumAge = maximumAge;
            this.m_TSTGroupId = tstGroupId;
            this.m_TSTMin = tstMin;
            this.m_TSTMax = tstMax;
        }

        public int StateAttributeTypeId
        {
            get
            {
                return this.m_StateAttributeTypeId;
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

        public int? TSTGroupId
        {
            get
            {
                return this.m_TSTGroupId;
            }
        }

        public int? TSTMin
        {
            get
            {
                return this.m_TSTMin;
            }
        }

        public int? TSTMax
        {
            get
            {
                return this.m_TSTMax;
            }
        }

        public override STSimDistributionBase Clone()
        {
            return new StateAttributeValue(
                this.StateAttributeTypeId,
                this.StratumId,
                this.SecondaryStratumId,
                this.TertiaryStratumId,
                this.Iteration,
                this.Timestep,
                this.StateClassId,
                this.MinimumAge,
                this.MaximumAge,
                this.TSTGroupId, 
                this.TSTMin, 
                this.TSTMax,
                this.DistributionValue,
                this.DistributionTypeId,
                this.DistributionFrequency,
                this.DistributionSD,
                this.DistributionMin,
                this.DistributionMax);
        }
    }
}
