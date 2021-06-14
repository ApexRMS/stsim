// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal class InitialConditionsDistribution
    {
        private int m_StratumId;
        private int? m_Iteration;
        private int? m_SecondaryStratumId;
        private int? m_TertiaryStratumId;
        private int m_StateClassId;
        private int m_AgeMin;
        private int m_AgeMax;
        private int? m_TSTGroupId;
        private int? m_TSTMin;
        private int? m_TSTMax;
        private double m_RelativeAmount;

        public InitialConditionsDistribution(
            int stratumId, 
            int? iteration, 
            int? secondaryStratumId, 
            int? tertiaryStratumId, 
            int stateClassId, 
            int ageMin, 
            int ageMax,
            int? tstGroupId,
            int? tstMin,
            int? tstMax,
            double relativeAmount)
        {
            this.m_StratumId = stratumId;
            this.m_Iteration = iteration;
            this.m_SecondaryStratumId = secondaryStratumId;
            this.m_TertiaryStratumId = tertiaryStratumId;
            this.m_StateClassId = stateClassId;
            this.m_AgeMin = ageMin;
            this.m_AgeMax = ageMax;
            this.m_TSTGroupId = tstGroupId;
            this.m_TSTMin = tstMin;
            this.m_TSTMax = tstMax;
            this.m_RelativeAmount = relativeAmount;
        }

        /// <summary>
        /// Gets the stratum Id
        /// </summary>
        public int StratumId
        {
            get
            {
                return this.m_StratumId;
            }
        }

        /// <summary>
        /// Gets the Iteration
        /// </summary>
        public int? Iteration
        {
            get
            {
                return m_Iteration;
            }
        }

        /// <summary>
        /// Gets the Secondary Stratum Id
        /// </summary>
        public int? SecondaryStratumId
        {
            get
            {
                return this.m_SecondaryStratumId;
            }
        }

        /// <summary>
        /// Gets the Tertiary Stratum Id
        /// </summary>
        public int? TertiaryStratumId
        {
            get
            {
                return this.m_TertiaryStratumId;
            }
        }

        /// <summary>
        /// StateClass Id for the cell
        /// </summary>
        public int StateClassId
        {
            get
            {
                return this.m_StateClassId;
            }
        }

        /// <summary>
        /// Minimum age for the pathway
        /// </summary>
        /// <remarks></remarks>
        public int AgeMin
        {
            get
            {
                return this.m_AgeMin;
            }
        }

        /// <summary>
        /// Maximum age for the pathway
        /// </summary>
        /// <remarks></remarks>
        public int AgeMax
        {
            get
            {
                return this.m_AgeMax;
            }
        }

        /// <summary>
        /// Gets the TST Transition Group ID
        /// </summary>
        public int? TSTGroupId
        {
            get
            {
                return this.m_TSTGroupId;
            }
        }

        /// <summary>
        /// Gets the TST Min
        /// </summary>
        public int? TSTMin
        {
            get
            {
                return this.m_TSTMin;
            }
        }

        /// <summary>
        /// Gets the TST Max
        /// </summary>
        public int? TSTMax
        {
            get
            {
                return this.m_TSTMax;
            }
        }

        /// <summary>
        /// Total amount (e.g. area) for the simulation
        /// </summary>
        public double RelativeAmount
        {
            get
            {
                return this.m_RelativeAmount;
            }
        }
    }
}
