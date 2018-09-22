// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Common;
using System.Collections.ObjectModel;

namespace SyncroSim.STSim
{
    internal class OutputTransitionAttributeCollection : KeyedCollection<SevenIntegerLookupKey, OutputTransitionAttribute>
    {
        public OutputTransitionAttributeCollection() : base(new SevenIntegerLookupKeyEqualityComparer())
        {
        }

        protected override SevenIntegerLookupKey GetKeyForItem(OutputTransitionAttribute item)
        {
            return new SevenIntegerLookupKey(
                item.StratumId,
                LookupKeyUtils.GetOutputCollectionKey(item.SecondaryStratumId), 
                LookupKeyUtils.GetOutputCollectionKey(item.TertiaryStratumId), 
                item.Iteration, item.Timestep, item.TransitionAttributeTypeId, item.AgeKey);
        }
    }
}
