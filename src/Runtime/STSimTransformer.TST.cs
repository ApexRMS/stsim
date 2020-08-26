// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Globalization;
using System.Collections.Generic;
using SyncroSim.Core;
using SyncroSim.StochasticTime;

namespace SyncroSim.STSim
{
    public partial class STSimTransformer
    {
        private List<int> GetTSTTransitionGroupIds()
        {
            List<int> Groups = new List<int>();
            Dictionary<int, bool> dict = new Dictionary<int, bool>();

            //From the TST Group datafeed
            DataSheet dstst = this.ResultScenario.GetDataSheet(Strings.DATASHEET_TST_GROUP_NAME);

            foreach (DataRow dr in dstst.GetData().Rows)
            {
                int id = Convert.ToInt32(
                    dr[Strings.DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME], 
                    CultureInfo.InvariantCulture);

                if (!dict.ContainsKey(id))
                {
                    dict.Add(id, true);
                }
            }

            //From the Transition Multiplier Value datafeed
            DataSheet dstmv = this.ResultScenario.GetDataSheet(Strings.DATASHEET_TRANSITION_MULTIPLIER_VALUE_NAME);

            foreach (DataRow dr in dstmv.GetData().Rows)
            {
                if (dr[Strings.DATASHEET_TRANSITION_MULTIPLIER_VALUE_TST_GROUP_COLUMN_NAME] != DBNull.Value)
                {
                    int id = Convert.ToInt32(
                        dr[Strings.DATASHEET_TRANSITION_MULTIPLIER_VALUE_TST_GROUP_COLUMN_NAME], 
                        CultureInfo.InvariantCulture);

                    if (!dict.ContainsKey(id))
                    {
                        dict.Add(id, true);
                    }
                }
            }

            foreach (int id in dict.Keys)
            {
                 Groups.Add(id);
            }

            return Groups;
        }

        /// <summary>
        /// Initializes the specified cell's Tst values
        /// </summary>
        /// <param name="simulationCell"></param>
        /// <param name="iteration"></param>
        /// <param name="icd"></param>
        /// <param name="isSpatial"></param>
        private void InitializeCellTstValues(
            Cell simulationCell, 
            int iteration, 
            InitialConditionsDistribution icd, 
            bool isSpatial)
        {
            if (simulationCell.TstValues.Count == 0)
            {
                return;
            }

            foreach (Tst tst in simulationCell.TstValues)
            {
                tst.TstValue = int.MaxValue;
            }

            this.InitTSTFromRamdomize(simulationCell, iteration);

            if (IsSpatial)
            {
                this.InitTSTFromRaster(simulationCell, iteration);
            }
            else
            {
                this.InitTSTFromICDistribution(simulationCell, iteration, icd);
            }
        }

        private void InitTSTFromRamdomize(Cell simulationCell, int iteration)
        {
            if (!this.m_TstRandomizeMap.HasItems)
            {
                return;
            }

            foreach (TransitionGroup tg in this.TransitionGroups)
            {
                if (simulationCell.TstValues.Contains(tg.TransitionGroupId))
                {
                    TstRandomize TstRand = this.m_TstRandomizeMap.GetTstRandomize(
                        tg.TransitionGroupId, simulationCell.StratumId, simulationCell.SecondaryStratumId,
                        simulationCell.TertiaryStratumId, simulationCell.StateClassId, iteration);

                    if (TstRand != null)
                    {
                        int r = this.m_RandomGenerator.GetNextInteger(TstRand.MinInitialTst, TstRand.MaxInitialTst);
                        Tst cellTst = simulationCell.TstValues[tg.TransitionGroupId];

                        cellTst.TstValue = r;
                    }
                }
            }
        }

        private void InitTSTFromRaster(Cell simulationCell, int iteration)
        {
            if (!this.m_InitialTstSpatialMap.HasItems)
            {
                return;
            }

            foreach (Tst tst in simulationCell.TstValues)
            {
                StochasticTimeRaster r = this.m_InitialTstSpatialMap.GetRaster(tst.TransitionGroupId, iteration);

                if (r != null)
                {
                    tst.TstValue = r.IntCells[simulationCell.CellId];
                }
            }
        }

        private void InitTSTFromICDistribution(Cell simulationCell, int iteration, InitialConditionsDistribution icd)
        {
            if (!icd.TSTGroupId.HasValue && !icd.TSTMin.HasValue && !icd.TSTMax.HasValue)
            {
                return;
            }

            foreach (Tst tst in simulationCell.TstValues)
            {
                if (icd.TSTGroupId.HasValue && icd.TSTGroupId.Value != tst.TransitionGroupId)
                {
                    continue;
                }

                int min = icd.TSTMin.HasValue ? icd.TSTMin.Value : 0;
                int max = icd.TSTMax.HasValue ? icd.TSTMax.Value : int.MaxValue;

                tst.TstValue = this.m_RandomGenerator.GetNextInteger(min, max);
            }
        }

        /// <summary>
        /// Compares the simulation cell's Tst value to the transitions Tst min and max
        /// </summary>
        /// <param name="simulationCell">The simulation cell</param>
        /// <param name="tr">The transition</param>
        /// <returns>TRUE if the cell's Tst is in range and FALSE if not.</returns>
        /// <remarks></remarks>
        private bool CompareTstValues(Cell simulationCell, Transition tr)
        {
            //If the transition's Transition Type doesn't have an associated Transition Group in 
            //Time-Since-Transition groups then return True.

            TstTransitionGroup tstgroup = this.m_TstTransitionGroupMap.GetGroup(
                tr.TransitionTypeId, simulationCell.StratumId, 
                simulationCell.SecondaryStratumId, simulationCell.TertiaryStratumId);

            if (tstgroup == null)
            {
                return true;
            }

            //Get the matching Tst for the simulation cell
            Tst cellTst = simulationCell.TstValues[tstgroup.GroupId];

            //If the cell Tst value is within the Transition's TstMin and TstMax range then return TRUE
            if (cellTst.TstValue >= tr.TstMinimum && cellTst.TstValue <= tr.TstMaximum)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Changes a simulation cell's Tst value for a probabilistic transition
        /// </summary>
        /// <param name="simulationCell">The simulation cell to change</param>
        /// <param name="tr">The probabilistic transition</param>
        /// <remarks></remarks>
        private void ChangeCellTstForProbabilisticTransition(Cell simulationCell, Transition tr)
        {
            if (simulationCell.TstValues.Count == 0)
            {
                return;
            }

            TransitionType tt = this.m_TransitionTypes[tr.TransitionTypeId];

            foreach (TransitionGroup tg in tt.TransitionGroups)
            {
                if (simulationCell.TstValues.Contains(tg.TransitionGroupId))
                {
                    Tst celltst = simulationCell.TstValues[tg.TransitionGroupId];
                    celltst.TstValue += tr.TstRelative;

                    if (celltst.TstValue < 0)
                    {
                        celltst.TstValue = 0;
                    }
                }
            }
        }
    }
}
