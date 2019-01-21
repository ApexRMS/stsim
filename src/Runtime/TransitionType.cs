// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.Globalization;

namespace SyncroSim.STSim
{
    public class TransitionType
    {
        private int m_TransitionTypeId;
        private string m_DisplayName;
        private string m_ToStringName;
        private int? m_MapId;
        private TransitionGroupCollection m_TransitionGroups = new TransitionGroupCollection();
        private TransitionGroupCollection m_PrimaryTransitionGroups = new TransitionGroupCollection();

        public TransitionType(int transitionTypeId, string displayName, int? mapId)
        {
            this.m_TransitionTypeId = transitionTypeId;
            this.m_DisplayName = displayName;
            this.m_MapId = mapId;

            string MapIDString = "NULL";

            if (this.m_MapId.HasValue)
            {
                MapIDString = Convert.ToString(this.m_MapId.Value, CultureInfo.InvariantCulture);
            }

            this.m_ToStringName = string.Format(CultureInfo.InvariantCulture,
                "{0}-{1}-(MapID={2})",
                this.m_TransitionTypeId, this.m_DisplayName, MapIDString);
        }

        public override string ToString()
        {
            return this.m_ToStringName;
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
