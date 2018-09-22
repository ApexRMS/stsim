// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal class StateAttributeValue : AttributeValueBase
    {
        public StateAttributeValue(
            int stateAttributeTypeId, int? stratumId, int? secondaryStratumId, int? tertiaryStratumId, 
            int? iteration, int? timestep, int? stateClassId, int? minimumAge, int? maximumAge, double value) : 
            base(stateAttributeTypeId, stratumId, secondaryStratumId, tertiaryStratumId, iteration, 
                timestep, stateClassId, minimumAge, maximumAge, value)
        {
        }
    }
}
