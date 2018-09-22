// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
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
