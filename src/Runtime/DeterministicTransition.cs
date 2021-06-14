// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    public class DeterministicTransition
    {
        private int? m_Iteration;
        private int? m_Timestep;
        private int? m_StratumIdSource;
        private int m_StateClassIdSource;
        private int? m_StratumIdDestination;
        private int? m_StateClassIdDestination;
        private int m_AgeMinimum;
        private int m_AgeMaximum;

        public DeterministicTransition(
            int? iteration, int? timestep, int? stratumIdSource, int stateClassIdSource, int? stratumIdDestination, 
            int? stateClassIdDestination, int ageMinimum, int ageMaximum)
        {
            this.m_Iteration = iteration;
            this.m_Timestep = timestep;
            this.m_StratumIdSource = stratumIdSource;
            this.m_StateClassIdSource = stateClassIdSource;
            this.m_StratumIdDestination = stratumIdDestination;
            this.m_StateClassIdDestination = stateClassIdDestination;
            this.m_AgeMinimum = ageMinimum;
            this.m_AgeMaximum = ageMaximum;
        }

        public int? Iteration
        {
            get
            {
                return this.m_Iteration;
            }
        }

        public int? Timestep
        {
            get
            {
                return this.m_Timestep;
            }
        }

        public int? StratumIdSource
        {
            get
            {
                return this.m_StratumIdSource;
            }
            set
            {
                this.m_StratumIdSource = value;
            }
        }

        public int StateClassIdSource
        {
            get
            {
                return this.m_StateClassIdSource;
            }
            set
            {
                this.m_StateClassIdSource = value;
            }
        }

        public int? StratumIdDestination
        {
            get
            {
                return this.m_StratumIdDestination;
            }
            set
            {
                this.m_StratumIdDestination = value;
            }
        }

        public int? StateClassIdDestination
        {
            get
            {
                return this.m_StateClassIdDestination;
            }
            set
            {
                this.m_StateClassIdDestination = value;
            }
        }

        public int AgeMinimum
        {
            get
            {
                return this.m_AgeMinimum;
            }
            set
            {
                this.m_AgeMinimum = value;
            }
        }

        public int AgeMaximum
        {
            get
            {
                return this.m_AgeMaximum;
            }
            set
            {
                this.m_AgeMaximum = value;
            }
        }
    }
}
