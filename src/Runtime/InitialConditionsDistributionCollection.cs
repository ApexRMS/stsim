// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.StochasticTime;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SyncroSim.STSim
{
    /// <summary>
    /// Initial Conditions Distirbution collection
    /// </summary>
    internal class InitialConditionsDistributionCollection : Collection<InitialConditionsDistribution>
    {
        /// <summary>
        /// Get a collection of InitialConditionDistribution objects for the specified Iteration
        /// </summary>
        /// <param name="iteration"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public InitialConditionsDistributionCollection GetForIteration(int? iteration)
        {
            InitialConditionsDistributionCollection icds = new InitialConditionsDistributionCollection();

            foreach (InitialConditionsDistribution icd in this)
            {
                if (Nullable.Equals(icd.Iteration, iteration))
                {
                    icds.Add(icd);
                }
            }

            return icds;
        }

        /// <summary>
        /// Get a Sorted List of Iterations contained in this collection
        /// </summary>
        /// <returns>A list of iterations</returns>
        /// <remarks></remarks>
        public List<int?> GetSortedIterationList()
        {
            List<int?> Iterations = new List<int?>();

            foreach (InitialConditionsDistribution icd in this)
            {
                var iteration = icd.Iteration;

                if (!Iterations.Contains(iteration))
                {
                    Iterations.Add(iteration);
                }
            }

            //Sort Ascending with Null at start
            Iterations.Sort();

            return Iterations;
        }

        /// <summary>
        /// Get a Filtered Collection of InitialConditionsDistribution for specified parameter
        /// </summary>
        /// <returns>A Collection of InitialConditionsDistribution</returns>
        /// <remarks></remarks>
        public InitialConditionsDistributionCollection GetFiltered(Cell cell)
        {
            InitialConditionsDistributionCollection ICDCollection = new InitialConditionsDistributionCollection();

            foreach (InitialConditionsDistribution icd in this)
            {
                if (cell.StratumId != icd.StratumId)
                {
                    continue;
                }

                if (cell.StateClassId != Spatial.DefaultNoDataValue)
                {
                    if (cell.StateClassId != icd.StateClassId)
                    {
                        continue;
                    }
                }

                if (cell.SecondaryStratumId != Spatial.DefaultNoDataValue)
                {
                    if (cell.SecondaryStratumId != icd.SecondaryStratumId)
                    {
                        continue;
                    }
                }

                if (cell.TertiaryStratumId != Spatial.DefaultNoDataValue)
                {
                    if (cell.TertiaryStratumId != icd.TertiaryStratumId)
                    {
                        continue;
                    }
                }

                if (cell.Age != Spatial.DefaultNoDataValue)
                {
                    if (cell.Age < icd.AgeMin || cell.Age > icd.AgeMax)
                    {
                        continue;
                    }
                }

                // Passed all the tests, so we'll take this one
                ICDCollection.Add(icd);
            }

            return ICDCollection;
        }

        public double CalcSumOfRelativeAmount()
        {
            double sumOfRelativeAmount = 0.0;

            foreach (InitialConditionsDistribution sis in this)
            {
                sumOfRelativeAmount += sis.RelativeAmount;
            }

            return sumOfRelativeAmount;
        }
    }
}
