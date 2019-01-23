﻿// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System.Collections.ObjectModel;

namespace SyncroSim.STSim
{
    internal class StratumCollection : KeyedCollection<int, Stratum>
    {
        protected override int GetKeyForItem(Stratum item)
        {
            return item.StratumId;
        }
    }
}