// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    public class TransitionType
    {
        private int m_TransitionTypeId;
        private string m_DisplayName;
        private int? m_MapId;
        private TransitionGroupCollection m_TransitionGroups = new TransitionGroupCollection();
        private TransitionGroupCollection m_PrimaryTransitionGroups = new TransitionGroupCollection();

        public TransitionType(int transitionTypeId, string displayName, int? mapId)
        {
            this.m_TransitionTypeId = transitionTypeId;
            this.m_DisplayName = displayName;
            this.m_MapId = mapId;
        }

        public int TransitionTypeId
        {
            get
            {
                return this.m_TransitionTypeId;
            }
        }

        public string DisplayName
        {
            get
            {
                return this.m_DisplayName;
            }
        }

        public int? MapId
        {
            get
            {
                return this.m_MapId;
            }
        }

        public TransitionGroupCollection TransitionGroups
        {
            get
            {
                return this.m_TransitionGroups;
            }
        }

        public TransitionGroupCollection PrimaryTransitionGroups
        {
            get
            {
                return this.m_PrimaryTransitionGroups;
            }
        }
    }
}
