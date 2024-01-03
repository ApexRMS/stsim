// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Common;
using System.Collections.ObjectModel;

namespace SyncroSim.STSim
{
    internal class OutputStratumTransitionStateCollection : KeyedCollection<EightIntegerLookupKey, OutputStratumTransitionState>
    {
        public OutputStratumTransitionStateCollection() : base(new EightIntegerLookupKeyEqualityComparer())
        {
        }

        protected override EightIntegerLookupKey GetKeyForItem(OutputStratumTransitionState item)
        {
            return new EightIntegerLookupKey(
                item.StratumId, 
                LookupKeyUtils.GetOutputCollectionKey(item.SecondaryStratumId), 
                LookupKeyUtils.GetOutputCollectionKey(item.TertiaryStratumId), 
                item.Iteration, item.Timestep, item.TransitionTypeId, item.StateClassId, item.EndStateClassId);
        }
    }
}
