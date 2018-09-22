// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System.Collections.ObjectModel;

namespace SyncroSim.STSim
{
    internal class TstCollection : KeyedCollection<int, Tst>
    {
        protected override int GetKeyForItem(Tst item)
        {
            return item.TransitionGroupId;
        }
    }
}
