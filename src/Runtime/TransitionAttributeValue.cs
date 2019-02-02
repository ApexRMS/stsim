// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

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
