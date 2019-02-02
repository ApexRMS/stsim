// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    /// <summary>
    /// The Output Stratum Transition State class
    /// </summary>
    /// <remarks></remarks>
    internal class OutputStratumTransitionState
    {
        private int m_StratumId;
        private int? m_SecondaryStratumId;
        private int? m_TertiaryStratumId;
        private int m_Iteration;
        private int m_Timestep;
        private int m_TransitionTypeId;
        private int m_StateClassId;
        private int m_EndStateClassId;
        private double m_Amount;

        public OutputStratumTransitionState(
            int stratumId, int? secondaryStratumId, int? tertiaryStratumId, int iteration, int timestep, 
            int transitionTypeId, int stateClassId, int endStateClassId, double amount)
        {
            this.m_StratumId = stratumId;
            this.m_SecondaryStratumId = secondaryStratumId;
            this.m_TertiaryStratumId = tertiaryStratumId;
            this.m_Iteration = iteration;
            this.m_Timestep = timestep;
            this.m_TransitionTypeId = transitionTypeId;
            this.m_StateClassId = stateClassId;
            this.m_EndStateClassId = endStateClassId;
            this.m_Amount = amount;
        }

        /// <summary>
        /// The output Stratum ID
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
        /// Gets the output transition type
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int TransitionTypeId
        {
            get
            {
                return this.m_TransitionTypeId;
            }
        }

        /// <summary>
        /// The output State Class Id
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
        /// The output End State Class Id
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int EndStateClassId
        {
            get
            {
                return this.m_EndStateClassId;
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
