// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal class TransitionAdjacencySetting
    {
        private int m_TransitionGroupId;
        private int m_StateAttributeTypeId;
        private double m_NeighborhoodRadius;
        private int m_UpdateFrequency;

        public TransitionAdjacencySetting(int transitionGroupId, int stateAttributeTypeId, double neighborhoodRadius, int updateFrequency)
        {
            this.m_TransitionGroupId = transitionGroupId;
            this.m_StateAttributeTypeId = stateAttributeTypeId;
            this.m_NeighborhoodRadius = neighborhoodRadius;
            this.m_UpdateFrequency = updateFrequency;
        }

        public int TransitionGroupId
        {
            get
            {
                return this.m_TransitionGroupId;
            }
        }

        public int StateAttributeTypeId
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
