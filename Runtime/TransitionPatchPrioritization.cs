// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal class TransitionPatchPrioritization
    {
        private int m_TransitionPatchPrioritizationId;
        private int? m_Iteration;
        private int? m_Timestep;
        private int m_TransitionGroupId;
        private int m_PatchPrioritizationId;

        public TransitionPatchPrioritization(
            int transitionPatchPrioritizationId, int? iteration, int? timestep, 
            int transitionGroupId, int patchPrioritizationId)
        {
            this.m_TransitionPatchPrioritizationId = transitionPatchPrioritizationId;
            this.m_Iteration = iteration;
            this.m_Timestep = timestep;
            this.m_TransitionGroupId = transitionGroupId;
            this.m_PatchPrioritizationId = patchPrioritizationId;
        }

        public int TransitionPatchPrioritizationId
        {
            get
            {
                return this.m_TransitionPatchPrioritizationId;
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

        public int PatchPrioritizationId
        {
            get
            {
                return this.m_PatchPrioritizationId;
            }
        }
    }
}
