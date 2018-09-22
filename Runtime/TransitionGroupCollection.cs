// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

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
