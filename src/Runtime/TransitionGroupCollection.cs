// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Collections.ObjectModel;

namespace SyncroSim.STSim
{
    public class TransitionGroupCollection : KeyedCollection<int, TransitionGroup>
    {
        internal TransitionGroupCollection()
        {
            return;
        }

        protected override int GetKeyForItem(TransitionGroup item)
        {
            return item.TransitionGroupId;
        }
    }
}
