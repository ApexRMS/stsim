// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;
using SyncroSim.Common;
using System.Globalization;

namespace SyncroSim.STSim
{
    class InitialTSTSpatialMap : STSimMapBase
    {
        private bool m_HasItems;
        private SortedKeyMap1<InitialTSTSpatial> m_Map = 
            new SortedKeyMap1<InitialTSTSpatial>(SearchMode.ExactPrev);

        public InitialTSTSpatialMap(InitialTSTSpatialCollection items, Scenario scenario) : base(scenario)
        {
            foreach (InitialTSTSpatial item in items)
            {
                this.AddItem(item);
            }
        }

        private void AddItem(InitialTSTSpatial item)
        {
            InitialTSTSpatial v = this.m_Map.GetItemExact(item.Iteration);

            if (v != null)
            {
                string msg = string.Format(CultureInfo.InvariantCulture,
                    "A record already exists for iteration={0}.",
                    STSimMapBase.FormatValue(item.Iteration));

                throw new ArgumentException(msg);
            }

            this.m_Map.AddItem(item.Iteration, item);
            this.m_HasItems = true;
        }

        public InitialTSTSpatial GetItem(int iteration)
        {
            if (!this.m_HasItems)
            {
                return null;
            }

            return this.m_Map.GetItem(iteration);
        }
    }
}
