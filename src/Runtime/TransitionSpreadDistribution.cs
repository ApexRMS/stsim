// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal class TransitionSpreadDistribution
    {
        private int m_TransitionSpreadDistributionId;
        private int? m_StratumId;
        private int? m_Iteration;
        private int? m_Timestep;
        private int m_TransitionGroupId;
        private int m_StateClassId;
        private double m_MinimumDistance;
        private double m_MaximumDistance;
        private double m_RelativeAmount;
        private double m_Proportion;

        public TransitionSpreadDistribution(
            int transitionSpreadDistributionId, int? stratumId, int? iteration, int? timestep, 
            int transitionGroupId, int stateClassId, double maximumDistance, double relativeAmount)
        {
            this.m_TransitionSpreadDistributionId = transitionSpreadDistributionId;
            this.m_StratumId = stratumId;
            this.m_Iteration = iteration;
            this.m_Timestep = timestep;
            this.m_TransitionGroupId = transitionGroupId;
            this.m_StateClassId = stateClassId;
            this.m_MaximumDistance = maximumDistance;
            this.m_RelativeAmount = relativeAmount;
        }

        public int TransitionSpreadDistributionId
        {
            get
            {
                return this.m_TransitionSpreadDistributionId;
            }
        }

        public int? StratumId
        {
            get
            {
                return this.m_StratumId;
            }
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

        public int TransitionGroupId
        {
            get
            {
                return this.m_TransitionGroupId;
            }
        }

        public int StateClassId
        {
            get
            {
                return this.m_StateClassId;
            }
        }

        internal double MinimumDistance
        {
            get
            {
                return this.m_MinimumDistance;
            }
            set
            {
                this.m_MinimumDistance = value;
            }
        }

        public double MaximumDistance
        {
            get
            {
                return this.m_MaximumDistance;
            }
        }

        public double RelativeAmount
        {
            get
            {
                return this.m_RelativeAmount;
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
    }
}
