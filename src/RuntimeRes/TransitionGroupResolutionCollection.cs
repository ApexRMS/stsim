﻿// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Collections.ObjectModel;

namespace SyncroSim.STSim
{
    class TransitionGroupResolutionCollection : KeyedCollection<int, TransitionGroupResolution>
    {
        protected override int GetKeyForItem(TransitionGroupResolution item)
        {
            return item.Id;
        }
    }
}
