// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal class TransitionSizeDistribution
    {
        private int m_TransitionSizeDistributionId;
        private int? m_StratumId;
        private int? m_Iteration;
        private int? m_Timestep;
        private int m_TransitionGroupId;
        private double m_MinimumSize;
        private double m_MaximumSize;
        private double m_RelativeAmount;
        private double m_Proportion;

        public TransitionSizeDistribution(
            int transitionSizeDistributionId, int? stratumId, int? iteration, int? timestep, 
            int transitionGroupId, double maximumSize, double relativeAmount)
        {
            this.m_TransitionSizeDistributionId = transitionSizeDistributionId;
            this.m_StratumId = stratumId;
            this.m_Iteration = iteration;
            this.m_Timestep = timestep;
            this.m_TransitionGroupId = transitionGroupId;
            this.m_MaximumSize = maximumSize;
            this.m_RelativeAmount = relativeAmount;
        }

        public int TransitionSizeDistributionId
        {
            get
            {
                return this.m_TransitionSizeDistributionId;
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

        public double MinimumSize
        {
            get
            {
                return this.m_MinimumSize;
            }
            set
            {
                this.m_MinimumSize = value;
            }
        }

        public double MaximumSize
        {
            get
            {
                return this.m_MaximumSize;
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
