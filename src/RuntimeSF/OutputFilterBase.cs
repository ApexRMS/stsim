// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    class OutputFilterBase
    {
        private int m_Id;
        private bool m_OutputTabularData;
        private bool m_OutputSpatialData;
        private bool m_OutputAvgSpatialData;

        public OutputFilterBase(int id, bool outputTabularData, bool outputSpatialData, bool outputAvgSpatialData)
        {
            this.Id = id;
            this.m_OutputTabularData = outputTabularData;
            this.m_OutputSpatialData = outputSpatialData;
            this.m_OutputAvgSpatialData = outputAvgSpatialData;
        }

        public int Id { get => m_Id; set => m_Id = value; }
        public bool OutputTabularData { get => m_OutputTabularData; set => m_OutputTabularData = value; }
        public bool OutputSpatialData { get => m_OutputSpatialData; set => m_OutputSpatialData = value; }
        public bool OutputAvgSpatialData { get => m_OutputAvgSpatialData; set => m_OutputAvgSpatialData = value; }
    }
}
