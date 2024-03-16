// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Diagnostics;

namespace SyncroSim.STSim
{
    internal static class NullableUtilities
    {
        public static bool NullableIdsEqual(int? value1, int? value2)
        {
            int s1 = 0;
            int s2 = 0;

            if (value1.HasValue)
            {
                s1 = value1.Value;
                Debug.Assert(s1 > 0);
            }

            if (value2.HasValue)
            {
                s2 = value2.Value;
                Debug.Assert(s2 > 0);
            }

            return (s1 == s2);
        }
    }
}
