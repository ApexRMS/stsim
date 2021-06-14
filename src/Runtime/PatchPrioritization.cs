// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Collections.Generic;

namespace SyncroSim.STSim
{
    /// <summary>
    /// Patch Prioritization
    /// </summary>
    /// <remarks></remarks>
    internal class PatchPrioritization
    {
        private int m_PatchPrioritizationId;
        private PatchPrioritizationType m_PatchPrioritizationType;
        private List<TransitionPatch> m_TransitionPatches = new List<TransitionPatch>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="prioritizationId"></param>
        /// <param name="prioritizationType"></param>
        /// <remarks></remarks>
        public PatchPrioritization(int prioritizationId, PatchPrioritizationType prioritizationType)
        {
            this.m_PatchPrioritizationId = prioritizationId;
            this.m_PatchPrioritizationType = prioritizationType;
        }

        /// <summary>
        /// Gets the Id for this patch prioritization
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int PatchPrioritizationId
        {
            get
            {
                return this.m_PatchPrioritizationId;
            }
        }

        /// <summary>
        /// Gets the prioritization type for this patch prioritization
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public PatchPrioritizationType PatchPrioritizationType
        {
            get
            {
                return this.m_PatchPrioritizationType;
            }
        }

        /// <summary>
        /// Gets the transition patches collection
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        internal List<TransitionPatch> TransitionPatches
        {
            get
            {
                return this.m_TransitionPatches;
            }
        }
    }
}
