// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    class OutputFilterFlows : OutputFilterBase
    {
        public OutputFilterFlows(
            int id, 
            bool outputSummary, 
            bool outputSpatial, 
            bool outputAvgSpatial) : base(id, outputSummary, outputSpatial, outputAvgSpatial)
        {
        }
    }
}
