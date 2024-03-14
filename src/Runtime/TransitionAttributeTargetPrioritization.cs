// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    class TransitionAttributeTargetPrioritization
    {
        private int? m_Iteration;
        private int? m_Timestep;
        private int m_TransitionAttributeTypeId;
        private int? m_StratumId;
        private int? m_SecondaryStratumId;
        private int? m_TertiaryStratumId;
        private int? m_TransitionGroupId;
        private int? m_StateClassId;
        private double m_Priority;
        public double PossibleAmount;
        public double ExpectedAmount;
        public double CumulativePossibleAmount;
        public double? DesiredAmount;
        public double? ProbabilityOverride;
        public double ProbabilityMultiplier = 1.0;

        public TransitionAttributeTargetPrioritization(
            int? iteration,
            int? timestep,
            int transitionAttributeTypeId,
            int? stratumId,
            int? secondaryStratumId,
            int? tertiaryStratumId,
            int? transitionGroupId,
            int? stateClassId,
            double priority)
        {
            this.m_Iteration = iteration;
            this.m_Timestep = timestep;
            this.m_TransitionAttributeTypeId = transitionAttributeTypeId;
            this.m_StratumId = stratumId;
            this.m_SecondaryStratumId = secondaryStratumId;
            this.m_TertiaryStratumId = tertiaryStratumId;
            this.m_TransitionGroupId = transitionGroupId;
            this.m_StateClassId = stateClassId;
            this.m_Priority = priority;
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

        public int TransitionAttributeTypeId
        {
            get
            {
                return this.m_TransitionAttributeTypeId;
            }
        }

        public int? StratumId
        {
            get
            {
                return this.m_StratumId;
            }
        }

        public int? SecondaryStratumId
        {
            get
            {
                return this.m_SecondaryStratumId;
            }
        }

        public int? TertiaryStratumId
        {
            get
            {
                return this.m_TertiaryStratumId;
            }
        }

        public int? TransitionGroupId
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

        public double Priority
        {
            get
            {
                return this.m_Priority;
            }
        }
    }
}
