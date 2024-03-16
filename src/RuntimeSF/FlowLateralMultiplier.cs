// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;

namespace SyncroSim.STSim
{
    internal class FlowLateralMultiplier
    {
        private readonly int m_FlowGroupId;
        private readonly int? m_FlowMultiplierTypeId;
        private readonly int? m_Iteration;
        private readonly int? m_Timestep;
        private readonly string m_Filename;

        public FlowLateralMultiplier(
            int flowGroupId,
            int? flowMultiplierTypeId,
            int? iteration,
            int? timestep,
            string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentException("The filename parameter cannot be Null.");
            }

            this.m_FlowGroupId = flowGroupId;
            this.m_FlowMultiplierTypeId = flowMultiplierTypeId;
            this.m_Iteration = iteration;
            this.m_Timestep = timestep;
            this.m_Filename = fileName;
        }

        public int FlowGroupId
        {
            get
            {
                return this.m_FlowGroupId;
            }
        }

        public int? FlowMultiplierTypeId
        {
            get
            {
                return this.m_FlowMultiplierTypeId;
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

        public string FileName
        {
            get
            {
                return this.m_Filename;
            }
        }
    }
}