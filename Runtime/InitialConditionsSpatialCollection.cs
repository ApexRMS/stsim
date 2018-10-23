﻿// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SyncroSim.STSim
{
    /// <summary>
    /// Initial Conditions Distirbution collection
    /// </summary>
    internal class InitialConditionsSpatialCollection : Collection<InitialConditionsSpatial>
    {
        /// <summary>
        /// Get a Sorted List of Iterations contained in this collection
        /// </summary>
        /// <returns>A list of iterations</returns>
        /// <remarks></remarks>
        public List<int?> GetSortedIterationList()
        {
            List<int?> lstIterations = new List<int?>();
            foreach (InitialConditionsSpatial icd in this)
            {
                var iteration = icd.Iteration;
                if (!lstIterations.Contains(iteration))
                {
                    lstIterations.Add(iteration);
                }
            }

            //Sort Ascending with Null at start
            lstIterations.Sort();
            return lstIterations;
        }
    }
}