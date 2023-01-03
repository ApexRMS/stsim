// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Collections.ObjectModel;

namespace SyncroSim.STSim
{
    public class TstCollection : KeyedCollection<int, Tst>
    {
        protected override int GetKeyForItem(Tst item)
        {
            return item.TransitionGroupId;
        }
    }
}
