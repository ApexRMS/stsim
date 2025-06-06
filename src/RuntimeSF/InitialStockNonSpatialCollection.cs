﻿// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Collections.ObjectModel;

namespace SyncroSim.STSim
{
	internal class InitialStockNonSpatialCollection : KeyedCollection<int, InitialStockNonSpatial>
	{
		protected override int GetKeyForItem(InitialStockNonSpatial item)
		{
			return item.Id;
		}
	}
}