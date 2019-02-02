// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    public partial class STSimTransformer
    {
        /// <summary>
        /// Initializes all simulations cells in Non-Raster mode
        /// </summary>
        /// <param name="iteration">The current iteration</param>
        /// <remarks></remarks>
        private void InitializeCellsNonRaster(int iteration)
        {
            if (this.m_CalcNumCellsFromDist)
            {
                this.InitializeCellsNonRasterCalcFromDist(iteration);
            }
            else
            {
                this.InitializeCellsNonRasterNoCalcFromDist(iteration);
            }
        }

        /// <summary>
        /// Initializes a simulation cell from a initial conditions distribution record
        /// </summary>
        /// <param name="simulationCell">The cell to initialize</param>
        /// <param name="icd">The initial conditions distribution record to use</param>
        /// <remarks></remarks>
        private void InitializeCellNonRaster(ref Cell simulationCell, InitialConditionsDistribution icd, int iteration)
        {
            Debug.Assert(!this.IsSpatial);

            int sisagemin = Math.Min(icd.AgeMin, icd.AgeMax);
            int sisagemax = Math.Max(icd.AgeMin, icd.AgeMax);

            this.InitializeCellAge(simulationCell, icd.StratumId, icd.StateClassId, sisagemin, sisagemax, iteration, this.m_TimestepZero);

            simulationCell.StratumId = icd.StratumId;
            simulationCell.StateClassId = icd.StateClassId;
            simulationCell.SecondaryStratumId = icd.SecondaryStratumId;
            simulationCell.TertiaryStratumId = icd.TertiaryStratumId;

            this.InitializeCellTstValues(simulationCell, iteration);

#if DEBUG
            this.VALIDATE_INITIALIZED_CELL(simulationCell, iteration, this.m_TimestepZero);
#endif
        }

        /// <summary>
        /// Performs all non initial conditions distribution initialization for the specified cell
        /// </summary>
        /// <param name="c"></param>
        /// <param name="iteration"></param>
        /// <remarks></remarks>
        private void PostInitializeCellNonRaster(Cell c, int iteration)
        {
            this.m_ProportionAccumulatorMap.AddOrIncrement(c.StratumId, c.SecondaryStratumId, c.TertiaryStratumId);

            this.OnSummaryStateClassOutput(c, iteration, this.m_TimestepZero);
            this.OnSummaryStateAttributeOutput(c, iteration, this.m_TimestepZero);

            CellInitialized?.Invoke(this, new CellEventArgs(c, iteration, this.m_TimestepZero));
        }

        /// <summary>
        /// Initializes all simulations cells in Non-Raster mode
        /// </summary>
        /// <param name="iteration">The current iteration</param>
        /// <remarks></remarks>
        private void InitializeCellsNonRasterCalcFromDist(int iteration)
        {
            // Fetch the number of cells from the NS IC setting
            DataRow drrc = this.ResultScenario.GetDataSheet(Strings.DATASHEET_NSIC_NAME).GetDataRow();
            int numCells = Convert.ToInt32(drrc[Strings.DATASHEET_NSIC_NUM_CELLS_COLUMN_NAME], CultureInfo.InvariantCulture);

            Debug.Assert(!this.IsSpatial);
            Debug.Assert(this.m_Cells.Count > 0);

            InitialConditionsDistributionCollection icds = this.m_InitialConditionsDistributionMap.GetICDs(iteration);
            double sumOfRelativeAmountForIteration = CalcSumOfRelativeAmount(iteration);

            int CellIndex = 0;

#if DEBUG
            Dictionary<int, Cell> dict = new Dictionary<int, Cell>();
#endif

            foreach (InitialConditionsDistribution icd in icds)
            {
                // DEVNOTE:To support multiple iterations, use relativeAmount / sum For Iteration as scale of total number of cells. Number of cells determined by 1st iteration specified. 
                // Otherwise, there's too much likelyhood that Number of cells will vary per iteration, which we cant/wont support.
                int numCellsForICD = Convert.ToInt32(Math.Round(icd.RelativeAmount / sumOfRelativeAmountForIteration * numCells));
                for (int i = 0; i < numCellsForICD; i++)
                {
                    Cell c = this.Cells[CellIndex];

#if DEBUG
                    dict.Add(c.CellId, c);
#endif

                    this.InitializeCellNonRaster(ref c, icd, iteration);
                    this.PostInitializeCellNonRaster(c, iteration);

                    CellIndex += 1;
                }
            }

#if DEBUG
            Debug.Assert(dict.Count == this.m_Cells.Count);
            Debug.Assert(CellIndex == this.Cells.Count);
#endif

            CellsInitialized?.Invoke(this, new CellEventArgs(null, iteration, this.m_TimestepZero));
        }

        /// <summary>
        /// Initializes all simulations cells in Non-Raster mode
        /// </summary>
        /// <param name="iteration">The current iteration</param>
        /// <remarks></remarks>
        private void InitializeCellsNonRasterNoCalcFromDist(int iteration)
        {
            Debug.Assert(!this.IsSpatial);
            Debug.Assert(this.m_Cells.Count > 0);

#if DEBUG
            Dictionary<int, Cell> dict = new Dictionary<int, Cell>();
#endif

            InitialConditionsDistributionCollection icds = this.m_InitialConditionsDistributionMap.GetICDs(iteration);
            double sumOfRelativeAmountForIteration = CalcSumOfRelativeAmount(iteration);

            foreach (Cell c in this.m_Cells)
            {
                double Rand = this.m_RandomGenerator.GetNextDouble();
                double CumulativeProportion = 0.0;

                foreach (InitialConditionsDistribution icd in icds)
                {
                    CumulativeProportion += (icd.RelativeAmount / sumOfRelativeAmountForIteration);

                    if (Rand < CumulativeProportion)
                    {
#if DEBUG
                        dict.Add(c.CellId, c);
#endif

                        Cell tempVar = c;
                        this.InitializeCellNonRaster(ref tempVar, icd, iteration);
                        this.PostInitializeCellNonRaster(c, iteration);

                        break;
                    }
                }
            }

#if DEBUG
            Debug.Assert(dict.Count == this.m_Cells.Count);
#endif

            CellsInitialized?.Invoke(this, new CellEventArgs(null, iteration, this.m_TimestepZero));
        }
    }
}
