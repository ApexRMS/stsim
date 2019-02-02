// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System.Collections.ObjectModel;

namespace SyncroSim.STSim
{
    internal class TransitionAttributeTypeCollection : KeyedCollection<int, TransitionAttributeType>
    {
        protected override int GetKeyForItem(TransitionAttributeType item)
        {
            return item.TransitionAttributeId;
        }
    }
}
