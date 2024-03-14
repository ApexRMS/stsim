// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    class SpatialOutputFlowRecord
    {
        public int FlowTypeId;
        public float[] Data;
        public bool HasOutputData;
    }
}
