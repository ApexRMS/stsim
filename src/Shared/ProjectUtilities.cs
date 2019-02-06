// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using System.Diagnostics;

namespace SyncroSim.STSim
{
    static class ProjectUtilities
    {
        public static bool ProjectHasResults(Project project)
        {
            Debug.Assert(!project.IsDeleted);
            Debug.Assert(!project.IsDisposed);

            foreach (Scenario s in project.Library.Scenarios)
            {
                if (s.IsDeleted)
                {
                    continue;
                }

                if (!s.IsResult)
                {
                    continue;
                }

                if (s.Project == project)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
