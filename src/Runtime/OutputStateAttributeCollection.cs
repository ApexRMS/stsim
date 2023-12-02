// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Apex;
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
