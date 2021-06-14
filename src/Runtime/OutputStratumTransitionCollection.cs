// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Common;
using System.Collections.ObjectModel;

namespace SyncroSim.STSim
{
    internal class OutputStratumTransitionCollection : KeyedCollection<EightIntegerLookupKey, OutputStratumTransition>
    {
        public OutputStratumTransitionCollection() : base(new EightIntegerLookupKeyEqualityComparer())
        {
        }

        protected override EightIntegerLookupKey GetKeyForItem(OutputStratumTransition item)
        {
            return new EightIntegerLookupKey(
                item.StratumId, 
                LookupKeyUtils.GetOutputCollectionKey(item.SecondaryStratumId), 
                LookupKeyUtils.GetOutputCollectionKey(item.TertiaryStratumId), 
                item.Iteration, 
                item.Timestep, 
                item.TransitionGroupId, 
                item.AgeKey, 
                item.EventIdKey);
        }
    }
}
