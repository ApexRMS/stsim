// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Common;
using System.Collections.ObjectModel;

namespace SyncroSim.STSim
{
    internal class OutputTSTCollection : KeyedCollection<SevenIntegerLookupKey, OutputTST>
    {
        public OutputTSTCollection() : base(new SevenIntegerLookupKeyEqualityComparer())
        {
        }

        protected override SevenIntegerLookupKey GetKeyForItem(OutputTST item)
        {
            return new SevenIntegerLookupKey(
                item.StratumId,
                LookupKeyUtils.GetOutputCollectionKey(item.SecondaryStratumId),
                LookupKeyUtils.GetOutputCollectionKey(item.TertiaryStratumId),
                item.Iteration, item.Timestep, item.TransitionGroupId, item.TSTKey);
        }
    }
}
