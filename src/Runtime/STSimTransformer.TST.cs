// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using SyncroSim.StochasticTime;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    public partial class STSimTransformer
    {
        private bool AnyWildTSTGroupSpecified()
        { 
            foreach (InitialConditionsDistribution t in this.m_InitialConditionsDistributions)
            {
                if ((!t.TSTGroupId.HasValue) && (t.TSTMin.HasValue || t.TSTMax.HasValue)) { return true; }
            }

            foreach (TransitionMultiplierValue t in this.m_TransitionMultiplierValues)
            {
                if (!t.TSTGroupId.HasValue) { return true; }
            }

            foreach (InitialTSTSpatial t in this.m_InitialTSTSpatialRecords)
            {
                if (!t.TSTGroupId.HasValue) { return true; }
            }

            foreach (StateAttributeValue t in this.m_StateAttributeValues)
            {
                if ((!t.TSTGroupId.HasValue) && (t.TSTMin.HasValue || t.TSTMax.HasValue)) { return true; }
            }

            foreach (TransitionAttributeValue t in this.m_TransitionAttributeValues)
            {
                if ((!t.TSTGroupId.HasValue) && (t.TSTMin.HasValue || t.TSTMax.HasValue)) { return true; }
            }

            return false;
        }

        private List<int> GetTSTTransitionGroupIds()
        {

#if DEBUG
        Debug.Assert(TRANSITION_GROUPS_FILLED);
        Debug.Assert(IC_DISTRIBUTIONS_FILLED);
        Debug.Assert(TRANSITION_MULTIPLIERS_FILLED);
        Debug.Assert(STATE_ATTRIBUTES_FILLED);
        Debug.Assert(TRANSITION_ATTRIBUTES_FILLED);
        Debug.Assert(TST_TRANSITION_GROUPS_FILLED);
        Debug.Assert(TST_RANDOMIZE_FILLED);

        if (this.m_IsSpatial)
        {
            Debug.Assert(INITIAL_TST_SPATIAL_FILLED);
        }
#endif

            List<int> Groups = new List<int>();

            //If there are any wild card specified then just add all groups

            if (this.AnyWildTSTGroupSpecified())
            {
                foreach (TransitionGroup g in this.m_TransitionGroups)
                {
                    Groups.Add(g.TransitionGroupId);
                }

                return Groups;
            }

            //Otherwise, go through each applicable collection and all the explicitly specified groups

            Dictionary<int, bool> dict = new Dictionary<int, bool>();

            foreach (InitialConditionsDistribution t in this.m_InitialConditionsDistributions)
            {
                if (t.TSTGroupId.HasValue && !dict.ContainsKey(t.TSTGroupId.Value)) { dict.Add(t.TSTGroupId.Value, true); }
            }

            foreach (TransitionMultiplierValue t in this.m_TransitionMultiplierValues)
            {
                if (t.TSTGroupId.HasValue && !dict.ContainsKey(t.TSTGroupId.Value)) { dict.Add(t.TSTGroupId.Value, true); }
            }

            foreach (InitialTSTSpatial t in this.m_InitialTSTSpatialRecords)
            {
                if (t.TSTGroupId.HasValue && !dict.ContainsKey(t.TSTGroupId.Value)) { dict.Add(t.TSTGroupId.Value, true); }
            }

            foreach (StateAttributeValue t in this.m_StateAttributeValues)
            {
                if (t.TSTGroupId.HasValue && !dict.ContainsKey(t.TSTGroupId.Value)) { dict.Add(t.TSTGroupId.Value, true); }
            }

            foreach (TransitionAttributeValue t in this.m_TransitionAttributeValues)
            {
                if (t.TSTGroupId.HasValue && !dict.ContainsKey(t.TSTGroupId.Value)) { dict.Add(t.TSTGroupId.Value, true); }
            }

            foreach (TstTransitionGroup t in this.m_TSTTransitionGroups)
            {
                if (!dict.ContainsKey(t.TSTGroupId)) { dict.Add(t.TSTGroupId, true); }
            }

            foreach (int id in dict.Keys)
            {
                 Groups.Add(id);
            }

            return Groups;
        }

        private void FillCellTSTCollections()
        {
            List<int> GroupIds = this.GetTSTTransitionGroupIds();

            if (GroupIds.Count == 0)
            {
                return;
            }

            foreach (Cell c in this.m_Cells)
            {
                foreach (int id in GroupIds)
                {
                    c.TstValues.Add(new Tst(id));
                }
            }
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
            this.InitTSTFromICDistribution(simulationCell, iteration, icd);

            if (IsSpatial)
            {
                this.InitTSTFromRaster(simulationCell, iteration);
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
            if (icd == null)
            {
                return;
            }

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
            Tst cellTst = simulationCell.TstValues[tstgroup.TSTGroupId];

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
                    int diff = int.MaxValue - celltst.TstValue;

                    if (diff >= tr.TstRelative)
                    {
                        celltst.TstValue += tr.TstRelative;
                    }

                    if (celltst.TstValue < 0)
                    {
                        celltst.TstValue = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Initializes the TST reporting helpers
        /// </summary>
        /// <remarks></remarks>
        private void InitializeTSTReportingHelpers()
        {
            Debug.Assert(this.m_TSTReportingHelper == null);
            this.m_TSTReportingHelper = new ClassBinHelper(false, 0, 0);

            //If not reporting TST then all TST helpers are disabled
            //-----------------------------------------------------------

            if (!this.m_CreateSummaryTSTOutput)
            {
                return;
            }

            //If TST is not configured then all TST helpers are disabled
            //------------------------------------------------------------

            DataRow dr = this.Project.GetDataSheet(Strings.DATASHEET_TST_TYPE_NAME).GetDataRow();

            if (dr == null)
            {
                return;
            }

            //If the TST configuration is invalid then all TST helpers are disabled
            //---------------------------------------------------------------------

            if (dr[Strings.DATASHEET_TST_TYPE_FREQUENCY_COLUMN_NAME] != DBNull.Value)
            {
                if (dr[Strings.DATASHEET_TST_TYPE_MAXIMUM_COLUMN_NAME] == DBNull.Value)
                {
                    this.RecordStatus(StatusType.Warning,
                        "TST reporting freqency set without TST reporting maximum.  Not reporting TST.");

                    return;
                }
            }

            if (dr[Strings.DATASHEET_TST_TYPE_MAXIMUM_COLUMN_NAME] != DBNull.Value)
            {
                if (dr[Strings.DATASHEET_TST_TYPE_FREQUENCY_COLUMN_NAME] == DBNull.Value)
                {
                    this.RecordStatus(StatusType.Warning,
                        "TST reporting maximum set without TST reporting frequency.  Not reporting TST.");

                    return;
                }
            }

            int f = Convert.ToInt32(dr[Strings.DATASHEET_TST_TYPE_FREQUENCY_COLUMN_NAME], CultureInfo.InvariantCulture);
            int m = Convert.ToInt32(dr[Strings.DATASHEET_TST_TYPE_MAXIMUM_COLUMN_NAME], CultureInfo.InvariantCulture);

            if (m < f)
            {
                this.RecordStatus(StatusType.Warning,
                    "TST reporting maximum is less than TST reporting frequency.  Not reporting TST.");

                return;
            }

            //Only enable a TST helper if that type of output is enabled

            if (this.m_CreateSummaryTSTOutput)
            {
                this.m_TSTReportingHelper = new ClassBinHelper(true, f, m);
            }
        }
    }
}
