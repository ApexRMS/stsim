// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Apex;
using System.Collections.ObjectModel;

namespace SyncroSim.STSim
{
	internal class OutputFlowCollection : KeyedCollection<FifteenIntegerLookupKey, OutputFlow>
	{
		public OutputFlowCollection() : base(new FifteenIntegerLookupKeyEqualityComparer())
		{
		}

		protected override FifteenIntegerLookupKey GetKeyForItem(OutputFlow item)
		{
			return new FifteenIntegerLookupKey(
                item.FromStratumId, 
                LookupKeyUtils.GetOutputCollectionKey(item.FromSecondaryStratumId), 
                LookupKeyUtils.GetOutputCollectionKey(item.FromTertiaryStratumId), 
                LookupKeyUtils.GetOutputCollectionKey(item.FromStateClassId),
                LookupKeyUtils.GetOutputCollectionKey(item.FromStockTypeId), 
                LookupKeyUtils.GetOutputCollectionKey(item.TransitionTypeId), 
                item.ToStratumId,
                LookupKeyUtils.GetOutputCollectionKey(item.ToStateClassId),
                LookupKeyUtils.GetOutputCollectionKey(item.ToStockTypeId), 
                item.FlowGroupId,
                LookupKeyUtils.GetOutputCollectionKey(item.TransferToStratumId),
                LookupKeyUtils.GetOutputCollectionKey(item.TransferToSecondaryStratumId),
                LookupKeyUtils.GetOutputCollectionKey(item.TransferToTertiaryStratumId),
                LookupKeyUtils.GetOutputCollectionKey(item.TransferToStateClassId),
                LookupKeyUtils.GetOutputCollectionKey(item.TransferToMinimumAge));
		}
	}
}
