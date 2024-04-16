﻿// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    partial class ResolutionTransformer : Transformer
    {
        private STSimTransformer m_STSimTransformer;
        private TransitionGroupResolutionCollection m_ResolutionGroups;
        private Dictionary<int, List<int>> m_BaseToFineDictionary;
        private string m_MultiResFilename;
        private string m_STSimFilename;
        private bool m_CanDoMultiResolution;

        /// <summary>
        /// Gets or sets the ST-Sim Transformer
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
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

            if (this.m_CanDoMultiResolution)
            {
                //QUESTION FOR KATIE: Do we need this?
                //this.NormalizeOutputOptions();

                //These events must be subscribed locally since they are for merging
                this.m_STSimTransformer.BeginNormalSpatialMerge += this.OnSTSimBeginNormalSpatialMerge;
                this.m_STSimTransformer.NormalSpatialMergeComplete += this.OnSTSimNormalSpatialMergeComplete;
            }
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

                DataSheet STSimSpatialProperties = this.ResultScenario.GetDataSheet(Constants.DATASHEET_STSIM_SPPIC_NAME);
                DataSheet STSimICS = this.ResultScenario.GetDataSheet(Constants.DATASHEET_STSIM_SPIC_NAME);
                InitialConditionsSpatialCollection STSimColl = CreateSPICCollection(this.ResultScenario, Constants.DATASHEET_STSIM_SPIC_NAME);
                InitialConditionsSpatial RefSTSimColl = STSimColl.First();
                InitialConditionsSpatialCollection MultiResColl = CreateSPICCollection(this.ResultScenario, Constants.DATASHEET_SPIC_NAME);
                InitialConditionsSpatial RefMultiResColl = MultiResColl.First();
                DataSheet MultiResDataSheet = this.ResultScenario.GetDataSheet(Constants.DATASHEET_SPIC_NAME);

                //QUESTION FOR KATIE: How do we rectify this in Syncrosim 3? Are we using stochastic time?
                //this.m_MultiResFilename = Spatial.GetSpatialInputFileName(MultiResDataSheet, RefMultiResColl.PrimaryStratumFileName, false);
                //this.m_STSimFilename = Spatial.GetSpatialInputFileName(STSimICS, RefSTSimColl.PrimaryStratumFileName, false);
                //StochasticTimeRaster MRRaster = new StochasticTimeRaster(this.m_MultiResFilename, RasterDataType.DTInteger);
                //StochasticTimeRaster STSimRaster = new StochasticTimeRaster(this.m_STSimFilename, RasterDataType.DTInteger);

                //this.m_BaseToFineDictionary = CreateBaseToFineDictionary(STSimRaster, MRRaster);
            }
        }

        //QUESTION FOR KATIE: Should the spatial merges for multi-resolution be exactly the same as for stocks and flows
        private void OnSTSimBeginNormalSpatialMerge(object sender, EventArgs e)
        {
            //this.ProcessAveragedStockGroupOutputFiles();
            //this.ProcessAveragedFlowGroupOutputFiles();
            //this.ProcessAveragedLateralFlowGroupOutputFiles();
        }

        private void OnSTSimNormalSpatialMergeComplete(object sender, EventArgs e)
        {
            //this.ProcessAveragedStockGroupDatasheet();
            //this.ProcessAveragedFlowGroupDatasheet();
            //this.ProcessAveragedLateralFlowGroupDatasheet();
        }

        //QUESTION FOR KATIE: How do we rectify this in Syncrosim 3? Are we using stochastic time?
        // key will be base cell ID, value will be list of fine cell IDs
        // Arguments will be the coarse resolution raster and fine resolution raster
        //public static Dictionary<int, List<int>> CreateBaseToFineDictionary(StochasticTimeRaster STSimRaster, StochasticTimeRaster MRRaster)
        //{
        //    int numBaseCells = STSimRaster.TotalCells;
        //    int fineHeight = MRRaster.Height;
        //    int fineWidth = MRRaster.Width;
        //    int baseWidth = STSimRaster.Width;
        //    int baseHeight = STSimRaster.Height;
        //    int heightRatio = MRRaster.Height / STSimRaster.Height;
        //    int widthRatio = MRRaster.Width / STSimRaster.Width;
        //    Dictionary<int, List<int>> BaseToFineDict = new Dictionary<int, List<int>>();

        //    for (int baseCellId = 0; baseCellId < numBaseCells; baseCellId++)
        //    {
        //        List<int> fineCellIds = new List<int>();
        //        int widthPosition = baseCellId % baseWidth; // here
        //        double heightDouble = baseCellId / baseWidth;
        //        int heightPosition = Convert.ToInt32(Math.Floor(heightDouble));
        //        // int verticalCellStep = baseWidth * heightRatio;
        //        int ul = ((widthPosition * widthRatio) + (heightPosition * fineWidth * heightRatio));
                

        //        for (int horizFineCellId = ul; horizFineCellId < ul + widthRatio; horizFineCellId++)
        //        {
        //            for (int verticalFineCellId = horizFineCellId; verticalFineCellId < horizFineCellId + (fineWidth * heightRatio); verticalFineCellId += fineWidth)
        //            {
        //                fineCellIds.Add(verticalFineCellId);
        //            }
        //        }

        //        BaseToFineDict.Add(baseCellId, fineCellIds);
        //    }

        //    return BaseToFineDict;
        //}

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
                }

                this.m_STSimTransformer = null;
            }

            base.Dispose(disposing);      
        }

        private void OnSTSimBeginModelRun(object sender, EventArgs e)
        {
            ////QUESTION FOR KATIE: Do we need this? I'm assuming not?
            ////Create the parallel result scenario
            //this.m_ParallelScenario = new ParallelScenario(this.m_STSimTransformer);

            ////Create (and initialize) the parallel transformer
            //this.m_STSimController = (STSimController) 
            //    this.m_ParallelScenario.CreateTransformer(Constants.STSIM_CONTROLLER_TRANSFORMER_NAME);

            this.ValidateRaster(this.m_MultiResFilename, this.m_STSimFilename);
        }

        private void OnSTSimModelRunComplete(object sender, EventArgs e)
        {
            this.SetStatusMessage(Constants.FINALIZING_DATA);
            this.EndModelRun();

            ////QUESTION FOR KATIE: Do we need this? I'm assuming not?
            //this.m_ParallelScenario.Complete(Constants.APPEND_TITLE);
        }

        private void OnSTSimIterationStarted(object sender, IterationEventArgs e)
        {
            this.PerformIteration(e.Iteration);
        }

        private void OnSTSimIterationCompleted(object sender, IterationEventArgs e)
        {
            this.FineForcesBaseCells.Clear();
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
                        if (!this.m_STSimTransformer.Cells.Contains(fineCellId))
                        {
                            continue;
                        }

                        Cell fineCell = this.m_STSimTransformer.Cells[fineCellId];
                        this.m_STSimTransformer.FillProbabilisticTransitionsForCell(fineCell, e.Iteration, e.Timestep);
                        Transition forcedFineTransition = this.m_STSimTransformer.SelectTransitionPathway(fineCell, e.TransitionGroup.TransitionGroupId, e.Iteration, e.Timestep, true);

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
    }
}