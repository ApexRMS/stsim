// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System.Collections.ObjectModel;

namespace SyncroSim.STSim
{
    internal class TransitionSpatialInitiationMultiplierCollection : KeyedCollection<int, TransitionSpatialInitiationMultiplier>
    {
        protected override int GetKeyForItem(TransitionSpatialInitiationMultiplier item)
        {
            return item.TransitionSpatialInitiationMultiplierId;
        }
    }
}
