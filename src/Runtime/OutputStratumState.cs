// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal class OutputStratumState
    {
        private int m_StratumId;
        private int? m_SecondaryStratumId;
        private int? m_TertiaryStratumId;
        private int m_Iteration;
        private int m_Timestep;
        private int m_StateClassId;
        private int? m_AgeMin;
        private int? m_AgeMax;
        private int m_AgeKey;
        private double m_Amount;

        public OutputStratumState(
            int stratumId, int? secondaryStratumId, int? tertiaryStratumId, int iteration, int timestep, 
            int stateClassId, int? ageMin, int? ageMax, int ageKey, double amount)
        {
            this.m_StratumId = stratumId;
            this.m_SecondaryStratumId = secondaryStratumId;
            this.m_TertiaryStratumId = tertiaryStratumId;
            this.m_Iteration = iteration;
            this.m_Timestep = timestep;
            this.m_StateClassId = stateClassId;
            this.m_AgeMin = ageMin;
            this.m_AgeMax = ageMax;
            this.m_AgeKey = ageKey;
            this.m_Amount = amount;
        }

        /// <summary>
        /// The output Stratum Id
        /// </summary>
        public int StratumId
        {
            get
            {
                return this.m_StratumId;
            }
        }

        /// <summary>
        /// Gets the secondary stratum Id
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int? SecondaryStratumId
        {
            get
            {
                return this.m_SecondaryStratumId;
            }
        }

        /// <summary>
        /// Gets the tertiary stratum Id
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int? TertiaryStratumId
        {
            get
            {
                return this.m_TertiaryStratumId;
            }
        }

        /// <summary>
        /// The output iteration
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int Iteration
        {
            get
            {
                return this.m_Iteration;
            }
        }

        /// <summary>
        /// The output timestep
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int Timestep
        {
            get
            {
                return this.m_Timestep;
            }
        }

        /// <summary>
        /// The output StateClass Id
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int StateClassId
        {
            get
            {
                return this.m_StateClassId;
            }
        }

        /// <summary>
        /// Gets the minimum age
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int? AgeMin
        {
            get
            {
                return this.m_AgeMin;
            }
        }

        /// <summary>
        /// Gets the maximum age
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int? AgeMax
        {
            get
            {
                return this.m_AgeMax;
            }
        }

        /// <summary>
        /// Gets the age key
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int AgeKey
        {
            get
            {
                return this.m_AgeKey;
            }
        }

        /// <summary>
        /// The output amount
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public double Amount
        {
            get
            {
                return this.m_Amount;
            }
            set
            {
                this.m_Amount = value;
            }
        }
    }
}
