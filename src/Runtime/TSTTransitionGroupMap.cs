// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;
using SyncroSim.Common;

namespace SyncroSim.STSim
{
    internal class TstTransitionGroupMap : STSimMapBase
    {
        private MultiLevelKeyMap4<TstTransitionGroup> m_Map = new MultiLevelKeyMap4<TstTransitionGroup>();

        public TstTransitionGroupMap(Scenario scenario) : base(scenario)
        {
        }

        public TstTransitionGroup GetGroup(int transitionTypeId, int stratumId, int? secondaryStratumId, int? tertiaryStratumId)
        {
            return this.m_Map.GetItem(transitionTypeId, stratumId, secondaryStratumId, tertiaryStratumId);
        }

        public void AddGroup(int transitionTypeId, int? stratumId, int? secondaryStratumId, int? tertiaryStratumId, TstTransitionGroup item)
        {
            TstTransitionGroup v = this.m_Map.GetItemExact(transitionTypeId, stratumId, secondaryStratumId, tertiaryStratumId);

            if (v != null)
            {
                string template = "A duplicate Time-Since-Transition Group was detected: More information:" + Environment.NewLine + "Transition Type={0}, {1}={2}, {3}={4}, {5}={6}.";
                ExceptionUtils.ThrowArgumentException(template, this.GetTransitionTypeName(transitionTypeId), this.PrimaryStratumLabel, this.GetStratumName(stratumId), this.SecondaryStratumLabel, this.GetSecondaryStratumName(secondaryStratumId), this.TertiaryStratumLabel, this.GetTertiaryStratumName(tertiaryStratumId));
            }

            this.m_Map.AddItem(transitionTypeId, stratumId, secondaryStratumId, tertiaryStratumId, item);
            this.SetHasItems();
        }
    }
}
