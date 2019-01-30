// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

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
