// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.StochasticTime;

namespace SyncroSim.STSim
{
    internal class TransitionMultiplierValue : STSimDistributionBase
    {
        private int m_TransitionGroupId;
        private int? m_StateClassId;
        private int m_AgeMin = 0;
        private int m_AgeMax = int.MaxValue;
        private int? m_TSTGroupId;
        private int m_TSTMin = 0;
        private int m_TSTMax = int.MaxValue;
        private int? m_TransitionMultiplierTypeId;

        public TransitionMultiplierValue(
            int transitionGroupId, 
            int? iteration, 
            int? timestep, 
            int? stratumId, 
            int? secondaryStratumId, 
            int? tertiaryStratumId, 
            int? stateClassId, 
            int ageMin,
            int ageMax,
            int? tstGroupId,
            int tstMin,
            int tstMax,
            int? transitionMultiplierTypeId, 
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

            this.m_TransitionGroupId = transitionGroupId;
            this.m_StateClassId = stateClassId;
            this.m_AgeMin = ageMin;
            this.m_AgeMax = ageMax;
            this.m_TSTGroupId = tstGroupId;
            this.m_TSTMin = tstMin;
            this.m_TSTMax = tstMax;
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

        public int? TSTGroupId
        {
            get
            {
                return this.m_TSTGroupId;
            }
        }

        public int TSTMin
        {
            get
            {
                return this.m_TSTMin;
            }
        }

        public int TSTMax
        {
            get
            {
                return this.m_TSTMax;
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
                this.TransitionGroupId, 
                this.Iteration, 
                this.Timestep, 
                this.StratumId, 
                this.SecondaryStratumId, 
                this.TertiaryStratumId, 
                this.StateClassId, 
                this.AgeMin,
                this.AgeMax,
                this.TSTGroupId,
                this.TSTMin,
                this.TSTMax,
                this.TransitionMultiplierTypeId, 
                this.DistributionValue,
                this.DistributionTypeId, 
                this.DistributionFrequency, 
                this.DistributionSD, 
                this.DistributionMin, 
                this.DistributionMax);
        }
    }
}
