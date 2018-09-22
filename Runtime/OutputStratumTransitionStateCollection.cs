// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

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
