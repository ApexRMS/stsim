// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Globalization;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    public class TransitionGroup
    {
        private int m_TransitionGroupId;
        private string m_DisplayName;
        private string m_ToStringName;
        private TransitionTypeCollection m_TransitionTypes = new TransitionTypeCollection();
        private TransitionTypeCollection m_PrimaryTransitionTypes = new TransitionTypeCollection();
        private Dictionary<int, Cell> m_TransitionSpreadCells = new Dictionary<int, Cell>();
        private TransitionSpreadDistributionMap m_TransitionSpreadDistributionMap = new TransitionSpreadDistributionMap();
        private bool m_HasSizeDistribution;
        private PatchPrioritization m_PatchPrioritization;
        private double m_Order = Constants.DEFAULT_TRANSITION_ORDER;
        private OutputFilterFlagTransitionGroup m_OutputFilter;
        private bool m_IsAuto;

        public TransitionGroup(int transitionGroupId, string transitionGroupName, bool isAuto)
        {
            this.m_TransitionGroupId = transitionGroupId;
            this.m_DisplayName = transitionGroupName;
            this.m_IsAuto = isAuto;

            this.m_ToStringName = string.Format(CultureInfo.InvariantCulture,
                "{0}-{1}-{2}",
                this.m_TransitionGroupId, this.m_DisplayName, this.m_IsAuto ? "(IsAuto=True)" : "(IsAuto=False)");
        }

        public override string ToString()
        {
            return this.m_ToStringName;
        }

        public int TransitionGroupId
        {
            get
            {
                return this.m_TransitionGroupId;
            }
        }

        public string DisplayName
        {
            get
            {
                return m_DisplayName;
            }
        }

        internal TransitionTypeCollection TransitionTypes
        {
            get
            {
                return this.m_TransitionTypes;
            }
        }

        internal TransitionTypeCollection PrimaryTransitionTypes
        {
            get
            {
                return this.m_PrimaryTransitionTypes;
            }
        }

        internal Dictionary<int, Cell> TransitionSpreadCells
        {
            get
            {
                return this.m_TransitionSpreadCells;
            }
        }

        internal TransitionSpreadDistributionMap TransitionSpreadDistributionMap
        {
            get
            {
                return this.m_TransitionSpreadDistributionMap;
            }
        }

        internal PatchPrioritization PatchPrioritization
        {
            get
            {
                return this.m_PatchPrioritization;
            }
            set
            {
                this.m_PatchPrioritization = value;
            }
        }

        public bool HasSizeDistribution
        {
            get
            {
                return this.m_HasSizeDistribution;
            }
            set
            {
                this.m_HasSizeDistribution = value;
            }
        }

        public double Order
        {
            get
            {
                return this.m_Order;
            }
            set
            {
                this.m_Order = value;
            }
        }

        internal OutputFilterFlagTransitionGroup OutputFilter
        {
            get
            {
                return this.m_OutputFilter;
            }
            set
            {
                this.m_OutputFilter = value;
            }
        }

        public bool IsAuto
        {
            get
            {
                return this.m_IsAuto;
            }
        }
    }
}
