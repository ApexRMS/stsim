// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System.Collections.ObjectModel;

namespace SyncroSim.STSim
{
    internal class TransitionSpatialMultiplierCollection : KeyedCollection<int, TransitionSpatialMultiplier>
    {
        protected override int GetKeyForItem(TransitionSpatialMultiplier item)
        {
            return item.TransitionSpatialMultiplierId;
        }
    }
}
