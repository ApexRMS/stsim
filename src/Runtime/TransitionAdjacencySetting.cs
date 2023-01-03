// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal class TransitionAdjacencySetting
    {
        private int m_TransitionGroupId;
        private int? m_StateClassId;
        private int? m_StateAttributeTypeId;
        private double m_NeighborhoodRadius;
        private int m_UpdateFrequency = 1;

        public TransitionAdjacencySetting(
            int transitionGroupId, 
            int? stateClassId,
            int? stateAttributeTypeId, 
            double neighborhoodRadius, 
            int? updateFrequency)
        {
            this.m_TransitionGroupId = transitionGroupId;
            this.m_StateClassId = stateClassId;
            this.m_StateAttributeTypeId = stateAttributeTypeId;
            this.m_NeighborhoodRadius = neighborhoodRadius;

            if (updateFrequency.HasValue)
            {
                this.m_UpdateFrequency = updateFrequency.Value;
            }
        }

        public int TransitionGroupId
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

        public int? StateAttributeTypeId
        {
            get
            {
                return this.m_StateAttributeTypeId;
            }
        }

        public double NeighborhoodRadius
        {
            get
            {
                return this.m_NeighborhoodRadius;
            }
        }

        public int UpdateFrequency
        {
            get
            {
                return this.m_UpdateFrequency;
            }
        }
    }
}
