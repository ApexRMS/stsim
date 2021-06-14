﻿// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Collections.ObjectModel;

namespace SyncroSim.STSim
{
    internal class StateClassCollection : KeyedCollection<int, StateClass>
    {
        protected override int GetKeyForItem(StateClass item)
        {
            return item.Id;
        }
    }
}
