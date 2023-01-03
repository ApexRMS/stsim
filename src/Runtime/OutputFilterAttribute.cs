// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    class OutputFilterAttribute
    {
        private int m_Id;
        private bool m_OutputSummaryData;
        private bool m_OutputSpatialData;
        private bool m_OutputAvgSpatialData;

        public OutputFilterAttribute(int id, bool outputSummaryData, bool outputSpatialData, bool outputAvgSpatialData)
        {
            this.Id = id;
            this.m_OutputSummaryData = outputSummaryData;
            this.m_OutputSpatialData = outputSpatialData;
            this.m_OutputAvgSpatialData = outputAvgSpatialData;
        }

        public int Id { get => m_Id; set => m_Id = value; }
        public bool OutputSummaryData { get => m_OutputSummaryData; set => m_OutputSummaryData = value; }
        public bool OutputSpatialData { get => m_OutputSpatialData; set => m_OutputSpatialData = value; }
        public bool OutputAvgSpatialData { get => m_OutputAvgSpatialData; set => m_OutputAvgSpatialData = value; }
    }
}
