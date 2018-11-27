// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Common;
using System.Collections.ObjectModel;

namespace SyncroSim.STSim
{
    internal class OutputStateAttributeCollection : KeyedCollection<SevenIntegerLookupKey, OutputStateAttribute>
    {
        public OutputStateAttributeCollection() : base(new SevenIntegerLookupKeyEqualityComparer())
        {
        }

        protected override SevenIntegerLookupKey GetKeyForItem(OutputStateAttribute item)
        {
            return new SevenIntegerLookupKey(
                item.StratumId,
                LookupKeyUtils.GetOutputCollectionKey(item.SecondaryStratumId), 
                LookupKeyUtils.GetOutputCollectionKey(item.TertiaryStratumId), 
                item.Iteration, 
                item.Timestep, 
                item.StateAttributeTypeId, 
                item.AgeKey);
        }
    }
}
