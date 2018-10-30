// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System.Collections.Generic;

namespace SyncroSim.STSim
{
    internal class TransitionFilterCriteria
    {
        public bool IncludeDeterministic = true;
        public bool IncludeProbabilistic = true;
        public Dictionary<int, bool> TransitionGroups = new Dictionary<int, bool>();
    }
}
