// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using SyncroSim.Core;

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
        /// <remarks>
        /// When initializing the cell TST use the following order of priority:
        /// 
        /// If the value for a TST group is specified in a raster use this      
        /// If the value for a TST group is specified in the initial condition distribution, use this
        /// If the value for a TST group is specified based on TST randomize data sheet, use this
        /// If the value for a TST group is not specified set it to Integer Max Value.
        /// </remarks>
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

            this.TryInitTSTFromRamdomize(simulationCell, iteration);

            if (IsSpatial)
            {
                this.TryInitTSTFromRaster(simulationCell, iteration);
            }
            else
            {
                this.TryInitTSTFromICDistribution(simulationCell, iteration, icd);
            }
        }

        private bool TryInitTSTFromRaster(Cell simulationCell, int iteration)
        {
            if (this.m_InputRasters.InitialTSTRaster == null)
            {
                return false;
            }

            bool SetValue = false;
            bool IsWild = (!this.m_InputRasters.InitialTSTRasterTransitionGroupId.HasValue);

            foreach (Tst tst in simulationCell.TstValues)
            {
                if (IsWild)
                {
                    SetValue = true;
                }
                else
                {
                    int v = this.m_InputRasters.InitialTSTRasterTransitionGroupId.Value;

                    if (tst.TransitionGroupId == v)
                    {
                        SetValue = true;
                    }
                }

                if (SetValue)
                {
                    tst.TstValue = this.m_InputRasters.InitialTSTCells[simulationCell.CellId];
                }
            }

            return SetValue;
        }

        private bool TryInitTSTFromICDistribution(Cell simulationCell, int iteration, InitialConditionsDistribution icd)
        {
            bool RetVal = false;

            if (icd.TSTGroupId.HasValue)
            {
                foreach (Tst tst in simulationCell.TstValues)
                {
                    if (tst.TransitionGroupId == icd.TSTGroupId.Value)
                    {
                        int min = icd.TSTMin.HasValue ? icd.TSTMin.Value : 0;
                        int max = icd.TSTMax.HasValue ? icd.TSTMax.Value : int.MaxValue;

                        tst.TstValue = this.m_RandomGenerator.GetNextInteger(min, max);
                        RetVal = true;
                    }
                }
            }
            else
            {
                if (icd.TSTMin.HasValue || icd.TSTMax.HasValue)
                {
                    foreach (Tst tst in simulationCell.TstValues)
                    {
                        int min = icd.TSTMin.HasValue ? icd.TSTMin.Value : 0;
                        int max = icd.TSTMax.HasValue ? icd.TSTMax.Value : int.MaxValue;

                        tst.TstValue = this.m_RandomGenerator.GetNextInteger(min, max);
                        RetVal = true;
                    }
                }
            }

            return RetVal;
        }

        private bool TryInitTSTFromRamdomize(Cell simulationCell, int iteration)
        {
            bool RetVal = false;

            foreach (TransitionGroup tg in this.TransitionGroups)
            {
                if (simulationCell.TstValues.Contains(tg.TransitionGroupId))
                {
                    TstRandomize TstRand = this.m_TstRandomizeMap.GetTstRandomize(
                        tg.TransitionGroupId, simulationCell.StratumId, simulationCell.SecondaryStratumId,
                        simulationCell.TertiaryStratumId, simulationCell.StateClassId, iteration);

                    int TstMaxRandValue = 0;
                    int TstMinRandValue = 0;

                    if (TstRand != null)
                    {
                        TstMinRandValue = TstRand.MinInitialTst;
                        TstMaxRandValue = TstRand.MaxInitialTst;
                    }

                    if (TstMaxRandValue == int.MaxValue)
                    {
                        TstMaxRandValue = int.MaxValue - 1;
                    }

                    int r = this.m_RandomGenerator.GetNextInteger(TstMinRandValue, TstMaxRandValue + 1);
                    Tst cellTst = simulationCell.TstValues[tg.TransitionGroupId];

                    cellTst.TstValue = r;
                    RetVal = true;
                }
            }

            return RetVal;
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
                tr.TransitionTypeId, simulationCell.StratumId, simulationCell.SecondaryStratumId, simulationCell.TertiaryStratumId);

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
