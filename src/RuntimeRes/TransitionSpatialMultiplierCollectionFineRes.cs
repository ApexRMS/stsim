﻿// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Collections.ObjectModel;

namespace SyncroSim.STSim
{
    internal class TransitionSpatialMultiplierCollectionFineRes : KeyedCollection<int, TransitionSpatialMultiplierFineRes>
    {
        protected override int GetKeyForItem(TransitionSpatialMultiplierFineRes item)
        {
            return item.TransitionSpatialMultiplierId;
        }
    }
}