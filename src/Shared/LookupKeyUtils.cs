// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal static class LookupKeyUtils
    {
        public static int GetOutputCollectionKey(int? value)
        {
            if (value.HasValue)
            {
                return value.Value;
            }
            else
            {
                return Constants.OUTPUT_COLLECTION_WILDCARD_KEY;
            }
        }
    }
}
