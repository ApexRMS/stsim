// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Common;
using System.Collections.ObjectModel;

namespace SyncroSim.STSim
{
    internal class OutputStratumStateCollection : KeyedCollection<SevenIntegerLookupKey, OutputStratumState>
    {
        public OutputStratumStateCollection() : base(new SevenIntegerLookupKeyEqualityComparer())
        {
        }

        protected override SevenIntegerLookupKey GetKeyForItem(OutputStratumState item)
        {
            return new SevenIntegerLookupKey(
                item.StratumId, 
                LookupKeyUtils.GetOutputCollectionKey(item.SecondaryStratumId), 
                LookupKeyUtils.GetOutputCollectionKey(item.TertiaryStratumId), 
                item.Iteration, item.Timestep, item.StateClassId, item.AgeKey);
        }
    }

    internal class OutputStratumStateCollectionZeroValues : KeyedCollection<SixIntegerLookupKey, OutputStratumState>
    {
        public OutputStratumStateCollectionZeroValues() : base(new SixIntegerLookupKeyEqualityComparer())
        {
        }

        protected override SixIntegerLookupKey GetKeyForItem(OutputStratumState item)
        {
            return new SixIntegerLookupKey(
                item.StratumId, 
                LookupKeyUtils.GetOutputCollectionKey(item.SecondaryStratumId), 
                LookupKeyUtils.GetOutputCollectionKey(item.TertiaryStratumId), 
                item.Iteration, item.Timestep, item.StateClassId);
        }
    }
}
