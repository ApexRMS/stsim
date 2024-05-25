// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    partial class ResolutionTransformer : STSimTransformer
    {
        private STSimTransformer m_STSimTransformer; // base ST-Sim transformer
        private TransitionGroupResolutionCollection m_ResolutionGroups;
        private Dictionary<int, List<int>> m_BaseToFineDictionary;
        private Dictionary<int, int> m_FineToBaseDictionary;
        private string m_MultiResFilename;
        private string m_STSimFilename;
        private bool m_CanDoMultiResolution;

        public STSimTransformer STSimTransformer
        {
            get
            {
                return this.m_STSimTransformer;
            }
            set
            {
                this.m_STSimTransformer = value;
            }
        }

        public override void Configure()
        {
            base.Configure();

            this.m_CanDoMultiResolution = CanDoMultiResolution(this.ResultScenario);
        }

        public override void Initialize()
        {
            if(this.m_CanDoMultiResolution)
            {
                AuxillarySetup();
                base.Initialize();

                this.m_STSimTransformer.IterationStarted += OnSTSimIterationStarted;
                this.m_STSimTransformer.IterationCompleted += OnSTSimIterationCompleted;
                this.m_STSimTransformer.TimestepStarted += OnSTSimTimestepStarted;
                this.m_STSimTransformer.TimestepCompleted += OnSTSimTimestepCompleted;
                this.m_STSimTransformer.BeginModelRun += OnSTSimBeginModelRun;
                this.m_STSimTransformer.ModelRunComplete += OnSTSimModelRunComplete;
                this.m_STSimTransformer.ApplySpatialTransition += OnSTSimApplySpatialTransition;
                this.m_STSimTransformer.ApplySpatialTransitionGroup += OnSTSimApplySpatialTransitionGroup;
                this.m_ResolutionGroups = CreateResolutionGroupCollection(this.ResultScenario);

                DataSheet STSimICS = this.ResultScenario.GetDataSheet(Strings.DATASHEET_SPIC_NAME);
                InitialConditionsFineSpatialCollection MultiResColl = CreateSPICFCollection(this.ResultScenario, Strings.DATASHEET_SPICF_NAME);
                InitialConditionsFineSpatial RefMultiResColl = MultiResColl.First();
                DataSheet MultiResDataSheet = this.ResultScenario.GetDataSheet(Strings.DATASHEET_SPICF_NAME);

                this.m_MultiResFilename = Spatial.GetSpatialDataFileName(MultiResDataSheet, RefMultiResColl.PrimaryStratumFileName, false);
                this.m_STSimFilename = Spatial.GetSpatialDataFileName(STSimICS, this.m_STSimTransformer.InputRasters.PrimaryStratumName, false);
                SyncroSimRaster MRRaster = new SyncroSimRaster(this.m_MultiResFilename, RasterDataType.DTInteger);
                SyncroSimRaster STSimRaster = new SyncroSimRaster(this.m_STSimFilename, RasterDataType.DTInteger);

                (this.m_BaseToFineDictionary, this.m_FineToBaseDictionary) = CreateBaseToFineDictionary(STSimRaster, MRRaster);

                // Autogenerate age raster based on base raster values
                //this.CreateSpatialICFromCombinedIC(true);
            }
        }

        // key will be base cell ID, value will be list of fine cell IDs
        // Arguments will be the coarse resolution raster and fine resolution raster
        public static Tuple<Dictionary<int, List<int>>, Dictionary<int, int>> CreateBaseToFineDictionary(SyncroSimRaster STSimRaster, SyncroSimRaster MRRaster)
        {
            int numBaseCells = STSimRaster.TotalCells;
            int fineHeight = MRRaster.Height;
            int fineWidth = MRRaster.Width;
            int baseWidth = STSimRaster.Width;
            int baseHeight = STSimRaster.Height;
            int heightRatio = MRRaster.Height / STSimRaster.Height;
            int widthRatio = MRRaster.Width / STSimRaster.Width;
            Dictionary<int, List<int>> BaseToFineDict = new Dictionary<int, List<int>>();
            Dictionary<int, int> FineToBaseDict = new Dictionary<int, int>();

            for (int baseCellId = 0; baseCellId < numBaseCells; baseCellId++)
            {
                List<int> fineCellIds = new List<int>();
                int widthPosition = baseCellId % baseWidth; // here
                double heightDouble = baseCellId / baseWidth;
                int heightPosition = Convert.ToInt32(Math.Floor(heightDouble));
                // int verticalCellStep = baseWidth * heightRatio;
                int ul = ((widthPosition * widthRatio) + (heightPosition * fineWidth * heightRatio));

                for (int horizFineCellId = ul; horizFineCellId < ul + widthRatio; horizFineCellId++)
                {
                    for (int verticalFineCellId = horizFineCellId; verticalFineCellId < horizFineCellId + (fineWidth * heightRatio); verticalFineCellId += fineWidth)
                    {
                        fineCellIds.Add(verticalFineCellId);
                        FineToBaseDict.Add(verticalFineCellId, baseCellId);
                    }
                }

                BaseToFineDict.Add(baseCellId, fineCellIds);
            }

            return Tuple.Create(BaseToFineDict, FineToBaseDict);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.IsDisposed)
            {
                if (this.m_STSimTransformer != null)
                {
                    this.m_STSimTransformer.IterationStarted -= OnSTSimIterationStarted;
                    this.m_STSimTransformer.IterationCompleted -= OnSTSimIterationCompleted;
                    this.m_STSimTransformer.TimestepStarted -= OnSTSimTimestepStarted;
                    this.m_STSimTransformer.TimestepCompleted -= OnSTSimTimestepCompleted;
                    this.m_STSimTransformer.BeginModelRun -= OnSTSimBeginModelRun;
                    this.m_STSimTransformer.ModelRunComplete -= OnSTSimModelRunComplete;
                    this.m_STSimTransformer.ApplySpatialTransition -= OnSTSimApplySpatialTransition;
                    this.m_STSimTransformer.ApplySpatialTransitionGroup -= OnSTSimApplySpatialTransitionGroup;
                }

                this.m_STSimTransformer = null;
            }

            base.Dispose(disposing);      
        }

        private void OnSTSimBeginModelRun(object sender, EventArgs e)
        {
            this.ValidateRaster(this.m_MultiResFilename, this.m_STSimFilename);
        }

        private void OnSTSimModelRunComplete(object sender, EventArgs e)
        {
            //this.SetStatusMessage(Constants.FINALIZING_DATA);
            this.EndModelRun();
        }

        private void OnSTSimIterationStarted(object sender, IterationEventArgs e)
        {
            
        }

        private void OnSTSimIterationCompleted(object sender, IterationEventArgs e)
        {
            this.FineForcesBaseCells.Clear();
            this.PerformIteration(e.Iteration);
        }

        private void OnSTSimTimestepStarted(object sender, TimestepEventArgs e)
        {
            this.BaseForcesFineCells.Clear();
            this.FineTransitionDictionary.Clear();
        }

        private void OnSTSimTimestepCompleted(object sender, TimestepEventArgs e)
        {
            this.FineForcesBaseCells.Clear();
            this.PerformTimestep(e.Iteration, e.Timestep);
        }

        private void OnSTSimApplySpatialTransition(object sender, SpatialTransitionEventArgsEx e)
        {
            if (this.m_ResolutionGroups.Contains(e.TransitionGroup.TransitionGroupId))
            {
                TransitionGroupResolution tgr = this.m_ResolutionGroups[e.TransitionGroup.TransitionGroupId];

                if (tgr.Resolution == Resolution.BaseForcesFine)
                {
                    List<int> forcedFineCells = this.m_BaseToFineDictionary[e.SimulationCell.CellId];

                    foreach (int fineCellId in forcedFineCells)
                    {
                        // If fine cell is NA, then won't exist in collection of fine cells, so continue
                        if (!this.Cells.Contains(fineCellId))
                        {
                            continue;
                        }

                        Cell fineCell = this.Cells[fineCellId];
                        this.FillProbabilisticTransitionsForCell(fineCell, e.Iteration, e.Timestep);
                        Transition forcedFineTransition = this.SelectTransitionPathway(fineCell, e.TransitionGroup.TransitionGroupId, e.Iteration, e.Timestep, true);

                        if (forcedFineTransition != null)
                        {
                            if (!this.BaseForcesFineCells.ContainsKey(e.TransitionGroup.TransitionGroupId))
                            {
                                this.BaseForcesFineCells.Add(e.TransitionGroup.TransitionGroupId, new CellCollection());
                            }
                            this.BaseForcesFineCells[e.TransitionGroup.TransitionGroupId].Add(fineCell);
                            this.FineTransitionDictionary.Add((fineCellId, e.TransitionGroup.TransitionGroupId), forcedFineTransition);
                        }
                    }

                    // find corresponding cells and add to cell collection
                    // this.m_STSimTransformer.Cells corresponds to the fine cells
                    // e.SimulationCell corresponds to the base cell that's forcing the fine transition
                    // find the fine cells within e.SimulationCell
                }
            }
        }

        private void OnSTSimApplySpatialTransitionGroup(object sender, SpatialTransitionGroupEventArgs e)
        {
            this.ValidateGroups(e.TransitionGroup.TransitionGroupId);

            if (this.m_ResolutionGroups.Contains(e.TransitionGroup.TransitionGroupId))
            {
                TransitionGroupResolution tgr = this.m_ResolutionGroups[e.TransitionGroup.TransitionGroupId];

                if (tgr.Resolution == Resolution.FineOnly)
                {
                    e.Cancel = true;
                }
                else if (tgr.Resolution == Resolution.FineForcesBase)
                {
                    // For each ffb cell, find corresponding base resolution cells
                    foreach (KeyValuePair<int, List<int>> entry in this.m_BaseToFineDictionary)
                    {
                        int baseCellId = entry.Key;

                        // check if base cell exists
                        if (!this.m_STSimTransformer.Cells.Contains(baseCellId))
                        {
                            continue;
                        }

                        Cell baseCell = this.m_STSimTransformer.Cells[baseCellId];
                        List<int> fineCellIds = entry.Value;
                        Transition baseTransition = null;
                        int fineCellCount = 0;
                        int counter = 1;

                        if (this.FineForcesBaseCells.ContainsKey(e.TransitionGroup.TransitionGroupId))
                        {
                            foreach (Cell fineCell in this.FineForcesBaseCells[e.TransitionGroup.TransitionGroupId])
                            // find corresponding base cells by reversing the dictionary
                            {
                                if (fineCellIds.Contains(fineCell.CellId))
                                {
                                    fineCellCount++;

                                    if (counter == 1)
                                    {
                                        baseTransition = this.m_STSimTransformer.SelectTransitionPathway(baseCell, e.TransitionGroup.TransitionGroupId, e.Iteration, e.Timestep);
                                        counter++;
                                    }
                                }
                            }

                            // Calculate probability of transition as proportion of fine cells in a base cell
                            double transitionProb = (double)fineCellCount / fineCellIds.Count;

                            // Invoke transition with given probability
                            if (baseTransition != null)
                            {
                                baseTransition.Probability = transitionProb;
                                this.m_STSimTransformer.ApplyProbabilisticTransitionsByCell(baseCell, e.Iteration, e.Timestep, baseTransition, e.TransitionGroup, e.TransitionedPixels, e.RasterTransitionAttrValues);
                            }
                        }
                    }

                    e.Cancel = true;
                }
            }
        }

        //private bool UseBaseCellValues(Cell fineCell, int minimumAge, int maximumAge, int? iteration)
        //{
        //    int baseCellId = this.m_FineToBaseDictionary[fineCell.CellId];

        //    // Could have a situation where base cell does not exist for the given fine cell
        //    if (!this.m_STSimTransformer.Cells.Contains(baseCellId))
        //    {
        //        return false;    
        //    }

        //    // Load the base cells from the initial conditions spatial datasheet
        //    InitialConditionsSpatial baseICS = this.m_STSimTransformer.m_InitialConditionsSpatialMap.GetICS(iteration);
        //    string ageName = baseICS.AgeFileName;

        //    Debug.Assert(!string.IsNullOrEmpty(ageName));

        //    DataSheet dsIC = this.ResultScenario.GetDataSheet(Strings.DATASHEET_SPIC_NAME);
        //    string fullFileName = Spatial.GetSpatialDataFileName(dsIC, ageName, false);
        //    SyncroSimRaster BaseAgeRaster = new SyncroSimRaster(fullFileName, RasterDataType.DTInteger);
        //    int baseCellAge = BaseAgeRaster.IntCells[baseCellId];

        //    // if base age falls within age range of fine resolution raster, use base raster value
        //    // this also handles case when no age ranges specified for fine resolution raster 
        //    if ((baseCellAge >= icd.AgeMin) && (baseCellAge <= icd.AgeMax))
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        internal override void InitializeCellAge(Cell simulationCell, int stratumId, int stateClassId, int minimumAge, int maximumAge, int iteration, int timestep)
        {
            int baseCellId = this.m_FineToBaseDictionary[simulationCell.CellId];

            // Could have a situation where base cell does not exist for the given fine cell
            if (!this.m_STSimTransformer.Cells.Contains(baseCellId))
            {
                base.InitializeCellAge(simulationCell, stratumId, stateClassId, minimumAge, maximumAge, iteration, timestep);
                return;
            }

            Cell baseCell = this.m_STSimTransformer.Cells[baseCellId];

            DeterministicTransition dt = this.GetDeterministicTransition(simulationCell, iteration, timestep);

            if (baseCell.Age >= dt.AgeMinimum && baseCell.Age <= dt.AgeMaximum)
            {
                simulationCell.Age = baseCell.Age;
            }
            else
            {
                base.InitializeCellAge(simulationCell, stratumId, stateClassId, minimumAge, maximumAge, iteration, timestep);
            }
        }

        //override internal void FillICSpatialCells(CellCollection cells, InitialConditionsDistributionCollection icds, bool AgeDefined, int? iteration)
        //{
        //    if (this.m_FineToBaseDictionary == null)
        //    {
        //        return;
        //    }

        //    foreach (Cell c in cells)
        //    {
        //        if (c.StratumId != 0)
        //        {
        //            // Now lets filter the ICDs by Primary Stratum, and optionally Age, StateClass, and Secondary Stratum 
        //            InitialConditionsDistributionCollection filteredICDs = icds.GetFiltered(c);

        //            var sumOfRelativeAmount = filteredICDs.CalcSumOfRelativeAmount();

        //            double Rand = this.m_RandomGenerator.GetNextDouble();
        //            double CumulativeProportion = 0.0;

        //            foreach (InitialConditionsDistribution icd in filteredICDs)
        //            {
        //                CumulativeProportion += (icd.RelativeAmount / sumOfRelativeAmount);

        //                if (Rand < CumulativeProportion)
        //                {
        //                    if (!AgeDefined)
        //                    {
        //                        if (this.UseBaseCellValues(c, icd, iteration)) 
        //                        {
        //                            int baseCellId = this.m_FineToBaseDictionary[c.CellId];
        //                            Cell baseCell = this.m_STSimTransformer.Cells[baseCellId];
        //                            c.Age = baseCell.Age;
        //                        } 
        //                        else
        //                        {
        //                            int sisagemin = Math.Min(icd.AgeMin, icd.AgeMax);
        //                            int sisagemax = Math.Max(icd.AgeMin, icd.AgeMax);

        //                            int Iter = this.MinimumIteration;

        //                            if (iteration.HasValue)
        //                            {
        //                                Iter = iteration.Value;
        //                            }

        //                            this.InitializeCellAge(
        //                                c, icd.StratumId, icd.StateClassId,
        //                                sisagemin, sisagemax,
        //                                Iter, this.m_TimestepZero);
        //                        }
        //                    }

        //                    c.StratumId = icd.StratumId;
        //                    c.StateClassId = icd.StateClassId;
        //                    c.SecondaryStratumId = icd.SecondaryStratumId;
        //                    c.TertiaryStratumId = icd.TertiaryStratumId;

        //                    break;
        //                }
        //            }
        //        }
        //    }
        // }
    }
}
