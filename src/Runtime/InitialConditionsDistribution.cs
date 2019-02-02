// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

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
        private double m_RelativeAmount;

        public InitialConditionsDistribution(
            int stratumId, int? iteration, int? secondaryStratumId, int? tertiaryStratumId, 
            int stateClassId, int ageMin, int ageMax, double relativeAmount)
        {
            this.m_StratumId = stratumId;
            this.m_Iteration = iteration;
            this.m_SecondaryStratumId = secondaryStratumId;
            this.m_TertiaryStratumId = tertiaryStratumId;
            this.m_StateClassId = stateClassId;
            this.m_AgeMin = ageMin;
            this.m_AgeMax = ageMax;
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
