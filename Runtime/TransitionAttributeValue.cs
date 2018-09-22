// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal class TransitionAttributeValue : AttributeValueBase
    {
        private int m_TransitionGroupId;

        public TransitionAttributeValue(
            int transitionAttributeTypeId, int? stratumId, int? secondaryStratumId, int? tertiaryStratumId, int? iteration, int? timestep, 
            int transitionGroupId, int? stateClassId, int? minimumAge, int? maximumAge, double value) : base(transitionAttributeTypeId, stratumId, 
                secondaryStratumId, tertiaryStratumId, iteration, timestep, stateClassId, minimumAge, maximumAge, value)
        {
            this.m_TransitionGroupId = transitionGroupId;
        }

        public int TransitionGroupId
        {
            get
            {
                return this.m_TransitionGroupId;
            }
        }
    }
}
