// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal static class LookupKeyUtils
    {
        public static int GetOutputCollectionKey(int? stratumId)
        {
            if (stratumId.HasValue)
            {
                return stratumId.Value;
            }
            else
            {
                return Constants.OUTPUT_COLLECTION_WILDCARD_KEY;
            }
        }
    }
}
