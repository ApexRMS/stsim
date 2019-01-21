// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    public class Transition
    {
        private int? m_Iteration;
        private int? m_Timestep;
        private int? m_StratumIdSource;
        private int m_StateClassIdSource;
        private int? m_StratumIdDestination;
        private int? m_StateClassIdDestination;
        private int? m_SecondaryStratumId;
        private int? m_TertiaryStratumId;
        private int m_TransitionTypeId;
        private double m_Probability;
        private double m_Proportion;
        private int m_AgeMinimum;
        private int m_AgeMaximum;
        private int m_AgeRelative;
        private bool m_AgeReset;
        private int m_TstMinimum;
        private int m_TstMaximum;
        private int m_TstRelative;

        //Internal use only
        internal bool PropnWasNull;
        internal bool AgeMinWasNull;
        internal bool AgeMaxWasNull;
        internal bool AgeRelativeWasNull;
        internal bool AgeResetWasNull;
        internal bool TstMinimumWasNull;
        internal bool TstMaximumWasNull;
        internal bool TstRelativeWasNull;

        public Transition(
            int? iteration, int? timestep, int? stratumIdSource, int stateClassIdSource, int? stratumIdDestination, int? stateClassIdDestination, 
            int? secondaryStratumId, int? tertiaryStratumId, int transitionTypeId, double probability, double proportion, int ageMinimum, 
            int ageMaximum, int ageRelative, bool ageReset, int tstMinimum, int tstMaximum, int tstRelative)
        {
            this.m_Iteration = iteration;
            this.m_Timestep = timestep;
            this.m_StratumIdSource = stratumIdSource;
            this.m_StateClassIdSource = stateClassIdSource;
            this.m_StratumIdDestination = stratumIdDestination;
            this.m_StateClassIdDestination = stateClassIdDestination;
            this.m_SecondaryStratumId = secondaryStratumId;
            this.m_TertiaryStratumId = tertiaryStratumId;
            this.m_TransitionTypeId = transitionTypeId;
            this.m_Probability = probability;
            this.m_Proportion = proportion;
            this.m_AgeMinimum = ageMinimum;
            this.m_AgeMaximum = ageMaximum;
            this.m_AgeRelative = ageRelative;
            this.m_AgeReset = ageReset;
            this.m_TstMinimum = tstMinimum;
            this.m_TstMaximum = tstMaximum;
            this.m_TstRelative = tstRelative;
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

        public int? SecondaryStratumId
        {
            get
            {
                return this.m_SecondaryStratumId;
            }
            set
            {
                this.m_SecondaryStratumId = value;
            }
        }

        public int? TertiaryStratumId
        {
            get
            {
                return this.m_TertiaryStratumId;
            }
            set
            {
                this.m_TertiaryStratumId = value;
            }
        }

        public int TransitionTypeId
        {
            get
            {
                return this.m_TransitionTypeId;
            }
            set
            {
                this.m_TransitionTypeId = value;
            }
        }

        public double Probability
        {
            get
            {
                return this.m_Probability;
            }
            set
            {
                this.m_Probability = value;
            }
        }

        public double Proportion
        {
            get
            {
                return this.m_Proportion;
            }
            set
            {
                this.m_Proportion = value;
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

        public int AgeRelative
        {
            get
            {
                return this.m_AgeRelative;
            }
            set
            {
                this.m_AgeRelative = value;
            }
        }

        public bool AgeReset
        {
            get
            {
                return this.m_AgeReset;
            }
            set
            {
                this.m_AgeReset = value;
            }
        }

        public int TstMinimum
        {
            get
            {
                return this.m_TstMinimum;
            }
            set
            {
                this.m_TstMinimum = value;
            }
        }

        public int TstMaximum
        {
            get
            {
                return this.m_TstMaximum;
            }
            set
            {
                this.m_TstMaximum = value;
            }
        }

        public int TstRelative
        {
            get
            {
                return this.m_TstRelative;
            }
            set
            {
                this.m_TstRelative = value;
            }
        }
    }
}
