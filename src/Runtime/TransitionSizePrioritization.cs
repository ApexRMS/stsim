// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal class TransitionSizePrioritization
    {
        private int m_TransitionSizePrioritizationId;
        private int? m_Iteration;
        private int? m_Timestep;
        private int? m_StratumId;
        private int? m_TransitionGroupId;
        private SizePrioritization m_SizePrioritization;
        private bool m_MaximizeFidelityToDistribution;
        private bool m_MaximizeFidelityToTotalArea;

        public TransitionSizePrioritization(
            int transitionSizePrioritizationId, int? iteration, int? timestep, int? stratumId, int? transitionGroupId, 
            SizePrioritization sizePrioritization, bool maximizeFidelityToDistribution, bool maximizeFidelityToTotalArea)
        {
            this.m_TransitionSizePrioritizationId = transitionSizePrioritizationId;
            this.m_Iteration = iteration;
            this.m_Timestep = timestep;
            this.m_StratumId = stratumId;
            this.m_TransitionGroupId = transitionGroupId;
            this.m_SizePrioritization = sizePrioritization;
            this.m_MaximizeFidelityToDistribution = maximizeFidelityToDistribution;
            this.m_MaximizeFidelityToTotalArea = maximizeFidelityToTotalArea;
        }

        public int TransitionSizePrioritizationId
        {
            get
            {
                return this.m_TransitionSizePrioritizationId;
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

        public int? StratumId
        {
            get
            {
                return this.m_StratumId;
            }
        }

        public int? TransitionGroupId
        {
            get
            {
                return this.m_TransitionGroupId;
            }
        }

        public SizePrioritization SizePrioritization
        {
            get
            {
                return this.m_SizePrioritization;
            }
        }

        public bool MaximizeFidelityToDistribution
        {
            get
            {
                return this.m_MaximizeFidelityToDistribution;
            }
        }

        public bool MaximizeFidelityToTotalArea
        {
            get
            {
                return this.m_MaximizeFidelityToTotalArea;
            }
        }
    }
}
