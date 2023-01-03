// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Collections.ObjectModel;

namespace SyncroSim.STSim
{
    internal class PatchPrioritizationCollection : KeyedCollection<int, PatchPrioritization>
    {
        protected override int GetKeyForItem(PatchPrioritization item)
        {
            return item.PatchPrioritizationId;
        }
    }
}
