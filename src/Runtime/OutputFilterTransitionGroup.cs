// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    class OutputFilterTransitionGroup
    {
        private int m_TransitionGroupId;
        private bool m_OutputSummary;
        private bool m_OutputSummaryByStateClass;
        private bool m_OutputTimeSinceTransition;
        private bool m_OutputSpatial;
        private bool m_OutputSpatialEvents;
        private bool m_OutputSpatialTimeSinceTransition;
        private bool m_OutputSpatialProbability;
        private bool m_OutputAvgSpatialTimeSinceTransition;

        public OutputFilterTransitionGroup(
            int transitionGroupId, 
            bool outputSummary, 
            bool outputSummaryByStateClass, 
            bool outputTimeSinceTransition, 
            bool outputSpatial,
            bool outputSpatialEvents,
            bool outputSpatialTimeSinceTransition,
            bool outputSpatialProbabilty, 
            bool outputAvgSpatialTimeSinceTransition)
        {
            this.Id = transitionGroupId;
            this.m_OutputSummary = outputSummary;
            this.m_OutputSummaryByStateClass = outputSummaryByStateClass;
            this.m_OutputTimeSinceTransition =  outputTimeSinceTransition;
            this.m_OutputSpatial = outputSpatial;
            this.m_OutputSpatialEvents = outputSpatialEvents;
            this.m_OutputSpatialTimeSinceTransition = outputSpatialTimeSinceTransition;
            this.m_OutputSpatialProbability = outputSpatialProbabilty;
            this.m_OutputAvgSpatialTimeSinceTransition = outputAvgSpatialTimeSinceTransition;
        }

        public int Id { get => m_TransitionGroupId; set => m_TransitionGroupId = value; }
        public bool OutputSummary { get => m_OutputSummary; set => m_OutputSummary = value; }
        public bool OutputSummaryByStateClass { get => m_OutputSummaryByStateClass; set => m_OutputSummaryByStateClass = value; }
        public bool OutputTimeSinceTransition { get => m_OutputTimeSinceTransition; set => m_OutputTimeSinceTransition = value; }
        public bool OutputSpatial { get => m_OutputSpatial; set => m_OutputSpatial = value; }
        public bool OutputSpatialEvents { get => m_OutputSpatialEvents; set => m_OutputSpatialEvents = value; }
        public bool OutputSpatialTimeSinceTransition { get => m_OutputSpatialTimeSinceTransition; set => m_OutputSpatialTimeSinceTransition = value; }
        public bool OutputSpatialProbability { get => m_OutputSpatialProbability; set => m_OutputSpatialProbability = value; }
        public bool OutputAvgSpatialTimeSinceTransition { get => m_OutputAvgSpatialTimeSinceTransition; set => m_OutputAvgSpatialTimeSinceTransition = value; }
    }
}
