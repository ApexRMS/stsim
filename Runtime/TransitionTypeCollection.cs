// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System.Collections.ObjectModel;

namespace SyncroSim.STSim
{
    public class TransitionTypeCollection : KeyedCollection<int, TransitionType>
    {
        internal TransitionTypeCollection()
        {
            return;
        }

        protected override int GetKeyForItem(TransitionType item)
        {
            return item.TransitionTypeId;
        }
    }
}
