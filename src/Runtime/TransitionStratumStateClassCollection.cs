// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Common;
using System.Collections.ObjectModel;

namespace SyncroSim.STSim
{
    internal class TransitionStratumStateClassCollection : KeyedCollection<TwoIntegerLookupKey, TransitionStratumStateClass>
    {
        public TransitionStratumStateClassCollection() : base(new TwoIntegerLookupKeyEqualityComparer())
        {
        }

        protected override TwoIntegerLookupKey GetKeyForItem(TransitionStratumStateClass item)
        {
            return new TwoIntegerLookupKey(item.StratumId, item.StateClassId);
        }
    }
}
