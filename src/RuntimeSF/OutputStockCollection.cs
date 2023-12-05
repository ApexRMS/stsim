// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Apex;
using System.Collections.ObjectModel;

namespace SyncroSim.STSim
{
	internal class OutputStockCollection : KeyedCollection<FiveIntegerLookupKey, OutputStock>
	{
		public OutputStockCollection() : base(new FiveIntegerLookupKeyEqualityComparer())
		{
		}
		protected override FiveIntegerLookupKey GetKeyForItem(OutputStock item)
		{
			return new FiveIntegerLookupKey(
                item.StratumId,
                LookupKeyUtils.GetOutputCollectionKey(item.SecondaryStratumId),
                LookupKeyUtils.GetOutputCollectionKey(item.TertiaryStratumId), 
                item.StateClassId, 
                item.StockGroupId);
		}
	}
}