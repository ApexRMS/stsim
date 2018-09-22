// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal enum AutoCorrelationSpread
    {
        ToAnyCell = 0,
        ToSamePathway = 1,
        ToSamePrimaryStratum = 2,
        ToSameSecondaryStratum = 3,
        ToSameTertiaryStratum = 4
    }

    internal class TransitionPathwayAutoCorrelation
    {
        private int? m_Iteration;
        private int? m_Timestep;
        private int? m_StratumId;
        private int? m_SecondaryStratumId;
        private int? m_TertiaryStratumId;
        private int? m_TransitionGroupId;
        private bool m_AutoCorrelation;
        private AutoCorrelationSpread m_SpreadTo = AutoCorrelationSpread.ToAnyCell;

        public TransitionPathwayAutoCorrelation(
            int? iteration, int? timestep, int? stratumId, int? secondaryStratumId, int? tertiaryStratumId, 
            int? transitionGroupId, bool autoCorrelation, AutoCorrelationSpread spreadTo)
        {
            this.m_Iteration = iteration;
            this.m_Timestep = timestep;
            this.m_StratumId = stratumId;
            this.m_SecondaryStratumId = secondaryStratumId;
            this.m_TertiaryStratumId = tertiaryStratumId;
            this.m_TransitionGroupId = transitionGroupId;
            this.m_AutoCorrelation = autoCorrelation;
            this.m_SpreadTo = spreadTo;
        }

        public int? Iteration
        {
            get
            {
                return this.m_Iteration;
            }
        }

        public int? Timestep
        {
            get
            {
                return this.m_Timestep;
            }
        }

        public int? StratumId
        {
            get
            {
                return this.m_StratumId;
            }
        }

        public int? SecondaryStratumId
        {
            get
            {
                return this.m_SecondaryStratumId;
            }
        }

        public int? TertiaryStratumId
        {
            get
            {
                return this.m_TertiaryStratumId;
            }
        }

        public int? TransitionGroupId
        {
            get
            {
                return this.m_TransitionGroupId;
            }
        }

        public bool AutoCorrelation
        {
            get
            {
                return this.m_AutoCorrelation;
            }
        }

        public AutoCorrelationSpread SpreadTo
        {
            get
            {
                return this.m_SpreadTo;
            }
        }
    }
}
