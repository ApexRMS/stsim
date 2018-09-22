// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System.Collections.ObjectModel;

namespace SyncroSim.STSim
{
    internal class TransitionSizeDistributionCollection : KeyedCollection<int, TransitionSizeDistribution>
    {
        protected override int GetKeyForItem(TransitionSizeDistribution item)
        {
            return item.TransitionSizeDistributionId;
        }
    }
}
