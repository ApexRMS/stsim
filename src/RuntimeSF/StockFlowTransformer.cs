// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Globalization;
using System.Collections.Generic;
using SyncroSim.Core;
using SyncroSim.Apex;

namespace SyncroSim.STSim
{
    [ObfuscationAttribute(Exclude = true, ApplyToMembers = false)]
    internal partial class StockFlowTransformer : Transformer
    {
        private bool m_IsSpatial;
        private bool m_ApplyBeforeTransitions;
        private bool m_ApplyEquallyRankedSimultaneously;
        private bool m_SummaryOmitSecondaryStrata;
        private bool m_SummaryOmitTertiaryStrata;
        private STSimTransformer m_STSimTransformer;
        private bool m_CanComputeStocksAndFlows;
        private bool m_StockLimitsOnSourceAndTarget;
        private readonly RandomGenerator m_RandomGenerator = new RandomGenerator();
        private readonly List<FlowType> m_ShufflableFlowTypes = new List<FlowType>();
        private LateralFlowAmountMap m_LateralFlowAmountMap;
        private int m_TotalIterations;

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

        /// <summary>
        /// Overrides Configure
        /// </summary>
        /// <remarks></remarks>
        public override void Configure()
        {
            base.Configure();

            this.m_CanComputeStocksAndFlows = this.CanComputeStocksAndFlows();
            this.m_StockLimitsOnSourceAndTarget = this.IsStockLimitsOnSourceAndTarget();

            if (this.m_CanComputeStocksAndFlows)
            {
                this.NormalizeOutputOptions();

                //These events must be subscribed locally since they are for merging
                this.STSimTransformer.BeginNormalSpatialMerge += this.OnSTSimBeginNormalSpatialMerge;
                this.STSimTransformer.NormalSpatialMergeComplete += this.OnSTSimNormalSpatialMergeComplete;
            }
        }

        /// <summary>
        /// Overrides Initialize
        /// </summary>
        public override void Initialize()
        {
            this.m_CanComputeStocksAndFlows = this.CanComputeStocksAndFlows();
            this.m_StockLimitsOnSourceAndTarget = this.IsStockLimitsOnSourceAndTarget();

            if (!this.m_CanComputeStocksAndFlows)
            {
                return;
            }

            this.InitializeSpatialRunFlag();
            this.Initialize_SS_TS_Flags();
            this.InitializeFlowOrderOptions();
            this.InitializeOutputOptions();
            this.InitializeOutputDataTables();
            this.FillStockTypes();
            this.FillStockGroups();
            this.FillInitialStocksNonSpatial();
            this.FillStockLimits();
            this.FillStockTransitionMultipliers();
            this.FillFlowGroups();
            this.FillFlowTypes();
            this.FillFlowMultiplierTypes();
            this.FillFlowPathways();
            this.FillFlowMultipliers();
            this.FillFlowMultipliersByStock();
            this.FillFlowOrders();
            this.AddAutoStockTypeLinkages();
            this.AddAutoFlowTypeLinkages();
            this.FillStockGroupLinkages();
            this.FillStockTypeLinkages();
            this.FillFlowGroupLinkages();
            this.FillFlowTypeLinkages();
            this.FillOutputFilterStocks();
            this.FillOutputFilterFlows();

            if (this.m_IsSpatial)
            {
                this.FillInitialStocksSpatial();
                this.FillFlowSpatialMultipliers();
                this.FillFlowLateralMultipliers();
                this.ValidateFlowSpatialMultipliers();
                this.ValidateFlowLateralMultipliers();
            }

            this.NormalizeForUserDistributions();
            this.InitializeDistributionValues();
            this.InitializeShufflableFlowTypes();
            this.CreateStockLimitMap();
            this.CreateStockTransitionMultiplierMap();
            this.CreateFlowPathwayMap();
            this.CreateMultiplierTypeMaps();
            this.CreateFlowOrderMap();

            if (this.m_IsSpatial)
            {
                this.CreateInitialStockSpatialMap();
            }

            this.m_TotalIterations = (
                this.STSimTransformer.MaximumIteration -
                this.STSimTransformer.MinimumIteration + 1);

            this.STSimTransformer.CellInitialized += this.OnSTSimCellInitialized;
            this.STSimTransformer.CellsInitialized += this.OnSTSimAfterCellsInitialized;
            this.STSimTransformer.ChangingCellProbabilistic += this.OnSTSimBeforeChangeCellProbabilistic;
            this.STSimTransformer.ChangingCellDeterministic += this.OnSTSimBeforeChangeCellDeterministic;
            this.STSimTransformer.CellBeforeTransitions += this.OnSTSimCellBeforeTransitions;
            this.STSimTransformer.IterationStarted += this.OnSTSimBeforeIteration;
            this.STSimTransformer.TimestepStarted += this.OnSTSimBeforeTimestep;
            this.STSimTransformer.TimestepCompleted += this.OnSTSimAfterTimestep;
            this.STSimTransformer.ModelRunComplete += this.OnSTSimModelRunComplete;

            if (this.m_StockTransitionMultipliers.Count > 0)
            {
                this.STSimTransformer.ApplyingTransitionMultipliers += this.OnApplyingTransitionMultipliers;
            }

#if DEBUG
            foreach (StockType t in this.m_StockTypes) { Debug.Assert(t.StockGroupLinkages.Count > 0); }
            foreach (FlowType t in this.m_FlowTypes) { Debug.Assert(t.FlowGroupLinkages.Count > 0); }
#endif
        }

        /// <summary>
        /// Disposes this instance
        /// </summary>
        /// <param name="disposing"></param>
        /// <remarks></remarks>
        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.IsDisposed)
            {
                if (this.m_CanComputeStocksAndFlows)
                {
                    this.STSimTransformer.CellInitialized -= this.OnSTSimCellInitialized;
                    this.STSimTransformer.CellsInitialized -= this.OnSTSimAfterCellsInitialized;
                    this.STSimTransformer.ChangingCellProbabilistic -= this.OnSTSimBeforeChangeCellProbabilistic;
                    this.STSimTransformer.ChangingCellDeterministic -= this.OnSTSimBeforeChangeCellDeterministic;
                    this.STSimTransformer.IterationStarted -= this.OnSTSimBeforeIteration;
                    this.STSimTransformer.TimestepStarted -= this.OnSTSimBeforeTimestep;
                    this.STSimTransformer.TimestepCompleted -= this.OnSTSimAfterTimestep;
                    this.STSimTransformer.ModelRunComplete -= this.OnSTSimModelRunComplete;
                    this.STSimTransformer.BeginNormalSpatialMerge -= this.OnSTSimBeginNormalSpatialMerge;
                    this.STSimTransformer.NormalSpatialMergeComplete -= this.OnSTSimNormalSpatialMergeComplete;

                    if (this.m_StockTransitionMultipliers.Count > 0)
                    {
                        this.STSimTransformer.ApplyingTransitionMultipliers -= this.OnApplyingTransitionMultipliers;
                    }
                }
            }

            base.Dispose(disposing);
        }

        //DEVTODO-3.0

        ///// <summary>
        ///// Overrides External Data Ready
        ///// </summary>
        ///// <param name="dataSheet"></param>
        //protected override void OnExternalDataReady(DataSheet dataSheet)
        //{
        //    if (!this.m_CanComputeStocksAndFlows)
        //    {
        //        return;
        //    }

        //    if (dataSheet.Name == Constants.DATASHEET_FLOW_PATHWAY_NAME)
        //    {
        //        this.m_FlowPathways.Clear();
        //        this.FillFlowPathways();
        //        this.m_FlowPathwayMap = null;
        //        this.CreateFlowPathwayMap();
        //    }
        //    else if (dataSheet.Name == Constants.DATASHEET_FLOW_MULTIPLIER_NAME)
        //    {
        //        this.m_FlowMultipliers.Clear();
        //        this.FillFlowMultipliers();
        //        this.InitializeFlowMultiplierDistributionValues();

        //        foreach (FlowMultiplierType tmt in this.m_FlowMultiplierTypes)
        //        {
        //            tmt.ClearFlowMultiplierMap();
        //        }

        //        foreach (FlowMultiplier sm in this.m_FlowMultipliers)
        //        {
        //            FlowMultiplierType mt = this.GetFlowMultiplierType(sm.FlowMultiplierTypeId);
        //            mt.AddFlowMultiplier(sm);
        //        }

        //        foreach (FlowMultiplierType tmt in this.m_FlowMultiplierTypes)
        //        {
        //            tmt.CreateFlowMultiplierMap();
        //        }
        //    }
        //    else if (dataSheet.Name == Constants.DATASHEET_FLOW_MULTIPLIER_BY_STOCK_NAME)
        //    {
        //        this.m_FlowMultipliersByStock.Clear();
        //        this.FillFlowMultipliersByStock();
        //        this.InitializeFlowMultiplierByStockDistributionValues();

        //        foreach (FlowMultiplierType tmt in this.m_FlowMultiplierTypes)
        //        {
        //            tmt.ClearFlowMultiplierByStockMap();
        //        }

        //        foreach (FlowMultiplierByStock sm in this.m_FlowMultipliersByStock)
        //        {
        //            FlowMultiplierType mt = this.GetFlowMultiplierType(sm.FlowMultiplierTypeId);
        //            mt.AddFlowMultiplierByStock(sm);
        //        }

        //        foreach (FlowMultiplierType tmt in this.m_FlowMultiplierTypes)
        //        {
        //            tmt.CreateFlowMultiplierByStockMap();
        //        }
        //    }
        //    else if (dataSheet.Name == Constants.DATASHEET_FLOW_SPATIAL_MULTIPLIER_NAME)
        //    {
        //        if (this.m_IsSpatial)
        //        {
        //            this.m_FlowSpatialMultipliers.Clear();
        //            this.m_FlowSpatialMultiplierRasters.Clear();
        //            this.FillFlowSpatialMultipliers();
        //            this.ValidateFlowSpatialMultipliers();

        //            foreach (FlowMultiplierType tmt in this.m_FlowMultiplierTypes)
        //            {
        //                tmt.ClearFlowSpatialMultiplierMap();
        //            }

        //            foreach (FlowSpatialMultiplier sm in this.m_FlowSpatialMultipliers)
        //            {
        //                FlowMultiplierType mt = this.GetFlowMultiplierType(sm.FlowMultiplierTypeId);
        //                mt.AddFlowSpatialMultiplier(sm);
        //            }

        //            foreach (FlowMultiplierType tmt in this.m_FlowMultiplierTypes)
        //            {
        //                tmt.CreateSpatialFlowMultiplierMap();
        //            }
        //        }
        //    }
        //    else if (dataSheet.Name == Constants.DATASHEET_FLOW_LATERAL_MULTIPLIER_NAME)
        //    {
        //        if (this.m_IsSpatial)
        //        {
        //            this.m_FlowLateralMultipliers.Clear();
        //            this.m_FlowLateralMultiplierRasters.Clear();
        //            this.FillFlowLateralMultipliers();
        //            this.ValidateFlowLateralMultipliers();

        //            foreach (FlowMultiplierType tmt in this.m_FlowMultiplierTypes)
        //            {
        //                tmt.ClearFlowLateralMultiplierMap();
        //            }

        //            foreach (FlowLateralMultiplier lm in this.m_FlowLateralMultipliers)
        //            {
        //                FlowMultiplierType mt = this.GetFlowMultiplierType(lm.FlowMultiplierTypeId);
        //                mt.AddFlowLateralMultiplier(lm);
        //            }

        //            foreach (FlowMultiplierType tmt in this.m_FlowMultiplierTypes)
        //            {
        //                tmt.CreateLateralFlowMultiplierMap();
        //            }
        //        }
        //    }
        //    else if (dataSheet.Name == Constants.DATASHEET_FLOW_ORDER)
        //    {
        //        this.m_FlowOrders.Clear();
        //        this.FillFlowOrders();
        //        this.m_FlowOrderMap = null;
        //        this.CreateFlowOrderMap();
        //    }
        //    else if (dataSheet.Name == Constants.DATASHEET_STOCK_LIMIT_NAME)
        //    {
        //        this.m_StockLimits.Clear();
        //        this.FillStockLimits();
        //        this.m_StockLimitMap = null;
        //        this.CreateStockLimitMap();
        //    }
        //    else if (dataSheet.Name == Constants.DATASHEET_STOCK_TRANSITION_MULTIPLIER_NAME)
        //    {
        //        this.m_StockTransitionMultipliers.Clear();
        //        this.FillStockTransitionMultipliers();
        //        this.m_StockTransitionMultiplierMap = null;
        //        this.CreateStockTransitionMultiplierMap();
        //    }
        //    else
        //    {
        //        string msg = string.Format(CultureInfo.InvariantCulture, "External data is not supported for: {0}", dataSheet.Name);
        //        throw new TransformerFailedException(msg);
        //    }
        //}

        /// <summary>
        /// Handles the BeforeIteration event. We run this raster verification code here
        /// as it depends of the STSim rasters having been loaded (it's a timing thing).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void OnSTSimBeforeIteration(object sender, IterationEventArgs e)
        {
            this.ResampleFlowMultiplierValues(
                      e.Iteration,
                      this.m_STSimTransformer.MinimumTimestep,
                      DistributionFrequency.Iteration);

            this.ResampleFlowMultiplierByStockValues(
                      e.Iteration,
                      this.m_STSimTransformer.MinimumTimestep,
                      DistributionFrequency.Iteration);
        }

        /// <summary>
        /// Handles the BeforeTimestep event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void OnSTSimBeforeTimestep(object sender, TimestepEventArgs e)
        {
            //Is it spatial flow output timestep?  If so, then iterate over flow types and initialize an output raster 
            //for each flow type Initialize to DEFAULT_NODATA_VALUE.  Note that we need to do this for lateral rasters also.
            //Note also that we need to set each SpatialOutputFlowRecord.HasOutputData to FALSE before each timestep.

            if (this.m_STSimTransformer.IsOutputTimestep(
                e.Timestep,
                this.m_SpatialFlowOutputTimesteps,
                this.m_CreateSpatialFlowOutput))
            {
                foreach (FlowType ft in this.m_FlowTypes)
                {
                    //Spatial
                    if (this.GetSpatialOutputFlowDictionary().ContainsKey(ft.Id))
                    {
                        SpatialOutputFlowRecord rec = GetSpatialOutputFlowDictionary()[ft.Id];
                        Debug.Assert(rec.FlowTypeId == ft.Id);

                        for (var i = 0; i <= rec.Data.GetUpperBound(0); i++)
                        {
                            rec.Data[i] = Spatial.DefaultNoDataValue;
                        }

                        rec.HasOutputData = false;
                    }

                    //Lateral
                    if (this.GetLateralOutputFlowDictionary().ContainsKey(ft.Id))
                    {
                        SpatialOutputFlowRecord rec = GetLateralOutputFlowDictionary()[ft.Id];
                        Debug.Assert(rec.FlowTypeId == ft.Id);

                        for (var i = 0; i <= rec.Data.GetUpperBound(0); i++)
                        {
                            rec.Data[i] = Spatial.DefaultNoDataValue;
                        }

                        rec.HasOutputData = false;
                    }
                }
#if DEBUG
                VALIDATE_SPATIAL_OUTPUT_RECORDS(GetSpatialOutputFlowDictionary());
                VALIDATE_SPATIAL_OUTPUT_RECORDS(GetLateralOutputFlowDictionary());
#endif
            }

            //Resample the multiplier values
            this.ResampleFlowMultiplierValues(e.Iteration, e.Timestep, DistributionFrequency.Timestep);
            this.ResampleFlowMultiplierByStockValues(e.Iteration, e.Timestep, DistributionFrequency.Timestep);

            //Clear the lateral flow amount map
            Debug.Assert(this.m_LateralFlowAmountMap == null);
            this.m_LateralFlowAmountMap = new LateralFlowAmountMap();
        }

#if DEBUG
        private static void VALIDATE_SPATIAL_OUTPUT_RECORDS(Dictionary<int, SpatialOutputFlowRecord> recs)
        {
            foreach (KeyValuePair<int, SpatialOutputFlowRecord> kvp in recs)
            {
                Debug.Assert(kvp.Key != 0);

                if (kvp.Value.Data.Length > 0)
                {
                    Debug.Assert(kvp.Value.Data[0] == Spatial.DefaultNoDataValue);
                    Debug.Assert(kvp.Value.Data[kvp.Value.Data.Length - 1] == Spatial.DefaultNoDataValue);
                }

                Debug.Assert(kvp.Value.HasOutputData == false);
            }
        }
#endif

        /// <summary>
        /// Handles the AfterTimestep event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void OnSTSimAfterTimestep(object sender, TimestepEventArgs e)
        {
            this.DistributeLateralFlows(e.Iteration, e.Timestep);

            if (this.m_STSimTransformer.IsOutputTimestep(e.Timestep, this.m_SummaryStockOutputTimesteps, this.m_CreateSummaryStockOutput))
            {
                this.RecordSummaryStockOutputData();
                this.WriteTabularSummaryStockOutput(e.Iteration, e.Timestep);
            }

            if (this.m_STSimTransformer.IsOutputTimestep(e.Timestep, this.m_SummaryFlowOutputTimesteps, this.m_CreateSummaryFlowOutput))
            {
                this.WriteTabularSummaryFlowOutputData(e.Iteration, e.Timestep);
            }

            if (this.m_IsSpatial)
            {
                if (this.m_STSimTransformer.IsOutputTimestep(e.Timestep, this.m_SpatialStockOutputTimesteps, this.m_CreateSpatialStockOutput))
                {
                    this.WriteStockGroupRasters(e.Iteration, e.Timestep);
                }

                if (this.m_STSimTransformer.IsOutputTimestep(e.Timestep, this.m_SpatialFlowOutputTimesteps, this.m_CreateSpatialFlowOutput))
                {
                    this.WriteFlowGroupRasters(e.Iteration, e.Timestep);
                }

                if (this.m_STSimTransformer.IsOutputTimestep(e.Timestep, this.m_LateralFlowOutputTimesteps, this.m_CreateLateralFlowOutput))
                {
                    this.WriteLateralFlowRasters(e.Iteration, e.Timestep);
                }

                this.RecordAverageStockValues(e.Timestep);
                this.RecordAverageFlowValues(e.Timestep);
                this.RecordAverageLateralFlowValues(e.Timestep);
            }

            this.m_LateralFlowAmountMap = null;
        }

        private void OnSTSimModelRunComplete(object sender, EventArgs e)
        {
            if (!this.STSimTransformer.IsSpatial)
            {
                return;
            }

            if (this.m_CreateAvgSpatialStockOutput)
            {
                this.WriteAverageStockRasters();
            }

            if (this.m_CreateAvgSpatialFlowOutput)
            {
                this.WriteAverageFlowRasters();
            }

            if (this.m_CreateAvgSpatialLateralFlowOutput)
            {
                this.WriteAverageLateralFlowRasters();
            }
        }

        private void OnSTSimBeginNormalSpatialMerge(object sender, EventArgs e)
        {
            this.ProcessAveragedStockGroupOutputFiles();
            this.ProcessAveragedFlowGroupOutputFiles();
            this.ProcessAveragedLateralFlowGroupOutputFiles();
        }

        private void OnSTSimNormalSpatialMergeComplete(object sender, EventArgs e)
        {
            this.ProcessAveragedStockGroupDatasheet();
            this.ProcessAveragedFlowGroupDatasheet();
            this.ProcessAveragedLateralFlowGroupDatasheet();
        }

        /// <summary>
        /// Called when (non-spatial) multipliers are being applied
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnApplyingTransitionMultipliers(object sender, MultiplierEventArgs e)
        {
            Debug.Assert(this.m_StockTransitionMultipliers.Count > 0);

            double Multiplier = 1.0;
            DataSheet Groups = this.Project.GetDataSheet(Strings.DATASHEET_STOCK_GROUP_NAME);
            DataSheet TGMembership = this.ResultScenario.GetDataSheet(Strings.DATASHEET_STOCK_TYPE_GROUP_MEMBERSHIP_NAME);
            Dictionary<int, float> StockAmounts = GetStockAmountDictionary(e.SimulationCell);
            var dtgroups = Groups.GetData();
            var dtmembership = TGMembership.GetData();

            foreach (DataRow dr in dtgroups.Rows)
            {
                float StockGroupValue = 0.0F;
                int StockGroupId = Convert.ToInt32(dr[Groups.ValueMember], CultureInfo.InvariantCulture);
                string query = string.Format(CultureInfo.InvariantCulture, "{0}={1}", Strings.STOCK_GROUP_ID_COLUMN_NAME, StockGroupId);
                DataRow[] rows = dtmembership.Select(query);

                foreach (DataRow r in rows)
                {
                    float ValueMultiplier = 1.0F;
                    int StockTypeId = Convert.ToInt32(r[Strings.STOCK_TYPE_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                    float StockTypeAmount = 0.0F;

                    if (StockAmounts.ContainsKey(StockTypeId))
                    {
                        StockTypeAmount = StockAmounts[StockTypeId];
                    }

                    if (!Convert.IsDBNull(r[Strings.DATASHEET_VALUE_COLUMN_NAME]))
                    {
                        ValueMultiplier = Convert.ToSingle(r[Strings.DATASHEET_VALUE_COLUMN_NAME], CultureInfo.InvariantCulture);
                    }

                    StockGroupValue += ((StockTypeAmount * ValueMultiplier) / Convert.ToSingle(this.m_STSimTransformer.AmountPerCell));
                }

                Multiplier *= this.m_StockTransitionMultiplierMap.GetStockTransitionMultiplier(StockGroupId, e.SimulationCell.StratumId, e.SimulationCell.SecondaryStratumId, e.SimulationCell.TertiaryStratumId, e.SimulationCell.StateClassId, e.TransitionGroupId, e.Iteration, e.Timestep, StockGroupValue);
            }

            e.ApplyMultiplier(Multiplier);
        }

        /// <summary>
        /// Called when a cell has been initialized
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void OnSTSimCellInitialized(object sender, CellEventArgs e)
        {
            Dictionary<int, float> StockAmounts = GetStockAmountDictionary(e.SimulationCell);

            foreach (StockType s in this.m_StockTypes)
            {
                if (StockAmounts.ContainsKey(s.Id))
                {
                    StockAmounts[s.Id] = 0.0F;
                }
                else
                {
                    StockAmounts.Add(s.Id, 0.0F);
                }
            }

            foreach (InitialStockNonSpatial s in this.m_InitialStocksNonSpatial)
            {
                StockLimit lim = this.m_StockLimitMap.GetStockLimit(
                    s.StockTypeId, e.SimulationCell.StratumId, e.SimulationCell.SecondaryStratumId, e.SimulationCell.TertiaryStratumId,
                    e.SimulationCell.StateClassId, e.Iteration, e.Timestep);

                double val = this.GetAttributeValue(
                    s.StateAttributeTypeId, e.SimulationCell.StratumId, e.SimulationCell.SecondaryStratumId, e.SimulationCell.TertiaryStratumId,
                    e.SimulationCell.StateClassId, e.Iteration, e.Timestep, e.SimulationCell.Age, e.SimulationCell.TstValues);

                double v = val * this.m_STSimTransformer.AmountPerCell;
                v = GetLimitBasedInitialStock(Convert.ToSingle(v), lim);

                StockAmounts[s.StockTypeId] = Convert.ToSingle(v);
            }

            if (this.m_InitialStockSpatialMap != null)
            {
                InitialStockSpatialCollection l = this.m_InitialStockSpatialMap.GetItem(e.Iteration);

                if (l != null)
                {
                    foreach (InitialStockSpatial s in l)
                    {
                        StockLimit lim = this.m_StockLimitMap.GetStockLimit(
                            s.StockTypeId, e.SimulationCell.StratumId, e.SimulationCell.SecondaryStratumId, e.SimulationCell.TertiaryStratumId,
                            e.SimulationCell.StateClassId, e.Iteration, e.Timestep);

                        //The spatial value should take precedence over the non-spatial value.  Note that
                        //we assume that raster values are the total amount not the density and don't need conversion.

                        if (this.m_InitialStockSpatialRasters.ContainsKey(s.Filename))
                        {
                            double v = this.m_InitialStockSpatialRasters[s.Filename].DblCells[e.SimulationCell.CellId];

                            //If a cell is a no data cell or if there is a -INF value for a cell, initialize the stock value to zero

                            if (Math.Abs(v - this.m_InitialStockSpatialRasters[s.Filename].NoDataValue) < double.Epsilon | double.IsNegativeInfinity(v))
                            {
                                v = 0.0;
                            }
                            else if (Math.Abs((float)v - (float)this.m_InitialStockSpatialRasters[s.Filename].NoDataValue) < float.Epsilon | float.IsNegativeInfinity((float)v))
                            {
                                v = 0.0;
                            }

                            v = GetLimitBasedInitialStock(Convert.ToSingle(v), lim);
                            StockAmounts[s.StockTypeId] = Convert.ToSingle(v * this.m_STSimTransformer.AmountPerCell);
                        }
                        else
                        {
                            Debug.Assert(false, "Where's the raster object ?");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Called after all cells have been initialized
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void OnSTSimAfterCellsInitialized(object sender, CellEventArgs e)
        {
            this.RecordSummaryStockOutputData();
            this.WriteTabularSummaryStockOutput(e.Iteration, e.Timestep);

            if (this.m_IsSpatial)
            {
                if (this.m_STSimTransformer.IsOutputTimestep(
                            e.Timestep,
                            this.m_SpatialStockOutputTimesteps,
                            this.m_CreateSpatialStockOutput))
                {
                    this.WriteStockGroupRasters(e.Iteration, e.Timestep);
                }

                if (e.Iteration == this.m_STSimTransformer.MinimumIteration)
                {
                    this.InitializeAverageStockMap();
                    this.InitializeAverageFlowMap();
                    this.InitializeAverageLateralFlowMap();
                }

                this.RecordAverageStockValuesTimestepZero();
            }
        }

        /// <summary>
        /// Called before a cell changes for a probabilistic transition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void OnSTSimBeforeChangeCellProbabilistic(object sender, CellChangeEventArgs e)
        {
            if (!this.m_FlowPathwayMap.HasRecords)
            {
                return;
            }

            this.ReorderShufflableFlowTypes(e.Iteration, e.Timestep);
            List<List<FlowType>> flowTypeLists = this.CreateListOfFlowTypeLists();

            foreach (List<FlowType> l in flowTypeLists)
            {
                foreach (StockType st in this.m_StockTypes)
                {
                    this.ApplyTransitionFlows(l, st.Id, e.SimulationCell, e.Iteration, e.Timestep, null, e.ProbabilisticPathway);
                }

                this.ApplyTransitionFlows(l, Constants.NULL_FROM_STOCK_TYPE_ID, e.SimulationCell, e.Iteration, e.Timestep, null, e.ProbabilisticPathway);
            }
        }

        /// <summary>
        /// Called before a cell changes for a deterministic transition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void OnSTSimBeforeChangeCellDeterministic(object sender, CellChangeEventArgs e)
        {
            if (this.m_ApplyBeforeTransitions == false)
            {
                ApplyAutomaticFlows(e);
            }
        }

        /// <summary>
        /// Called before a cell changes for a deterministic transition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void OnSTSimCellBeforeTransitions(object sender, CellChangeEventArgs e)
        {
            if (this.m_ApplyBeforeTransitions == true)
            {
                ApplyAutomaticFlows(e);
            }
        }

        private void ApplyAutomaticFlows(CellChangeEventArgs e)
        {
            if (!this.m_FlowPathwayMap.HasRecords)
            {
                return;
            }

            this.ReorderShufflableFlowTypes(e.Iteration, e.Timestep);
            List<List<FlowType>> flowTypeLists = this.CreateListOfFlowTypeLists();

            foreach (List<FlowType> l in flowTypeLists)
            {
                foreach (StockType st in this.m_StockTypes)
                {
                    this.ApplyTransitionFlows(l, st.Id, e.SimulationCell, e.Iteration, e.Timestep, e.DeterministicPathway, null);
                }

                this.ApplyTransitionFlows(l, Constants.NULL_FROM_STOCK_TYPE_ID, e.SimulationCell, e.Iteration, e.Timestep, e.DeterministicPathway, null);
            }
        }

        private void ApplyTransitionFlows(
                List<FlowType> ftList,
                int stockTypeId,
                Cell cell,
                int iteration,
                int timestep,
                DeterministicTransition dtPathway,
                Transition ptPathway)
        {
            Debug.Assert(this.m_FlowPathwayMap.HasRecords);
            List<int> TGIds = new List<int>();


            int DestStrat;
            int DestStateClass;
            int ToAge;
            if (ptPathway != null)
            {
                if (ptPathway.StratumIdDestination.HasValue)
                {
                    DestStrat = ptPathway.StratumIdDestination.Value;
                }
                else
                {
                    DestStrat = cell.StratumId;
                }

                if (ptPathway.StateClassIdDestination.HasValue)
                {
                    DestStateClass = ptPathway.StateClassIdDestination.Value;
                }
                else
                {
                    DestStateClass = cell.StateClassId;
                }

                ToAge = this.m_STSimTransformer.DetermineTargetAgeProbabilistic(cell.Age, DestStrat, DestStateClass, iteration, timestep, ptPathway);

                foreach (TransitionGroup tg in this.m_STSimTransformer.TransitionTypes[ptPathway.TransitionTypeId].TransitionGroups)
                {
                    TGIds.Add(tg.TransitionGroupId);
                }
            }
            else
            {
                if (dtPathway == null)
                {
                    DestStrat = cell.StratumId;
                    DestStateClass = cell.StateClassId;
                    ToAge = cell.Age + 1;
                }
                else
                {
                    if (dtPathway.StratumIdDestination.HasValue)
                    {
                        DestStrat = dtPathway.StratumIdDestination.Value;
                    }
                    else
                    {
                        DestStrat = cell.StratumId;
                    }

                    if (dtPathway.StateClassIdDestination.HasValue)
                    {
                        DestStateClass = dtPathway.StateClassIdDestination.Value;
                    }
                    else
                    {
                        DestStateClass = cell.StateClassId;
                    }

                    ToAge = cell.Age + 1;

                }

                TGIds.Add(0);
            }

            foreach (int TransitionGroupId in TGIds)
            {
                List<FlowPathway> allFlowPathways = new List<FlowPathway>();

                foreach (FlowType ft in ftList)
                {
                    List<FlowPathway> l = this.m_FlowPathwayMap.GetFlowPathwayList(
                                  iteration, timestep, cell.StratumId, cell.SecondaryStratumId, cell.TertiaryStratumId, cell.StateClassId, stockTypeId, cell.Age,
                                  DestStrat, DestStateClass, TransitionGroupId, ft.Id, ToAge);

                    if (l != null)
                    {
                        foreach (FlowPathway fp in l)
                        {
                            allFlowPathways.Add(fp);
                        }
                    }
                }

                foreach (FlowPathway fp in allFlowPathways)
                {
                    fp.FlowAmount = this.CalculateFlowAmount(fp, cell, iteration, timestep);
                }

                foreach (FlowPathway fp in allFlowPathways)
                {
                    Dictionary<int, float> d = GetStockAmountDictionary(cell);
                    StockLimit limsrc = this.m_StockLimitMap.GetStockLimit(fp.FromStockTypeId, cell.StratumId, cell.SecondaryStratumId, cell.TertiaryStratumId, cell.StateClassId, iteration, timestep);
                    StockLimit limdst = this.m_StockLimitMap.GetStockLimit(fp.ToStockTypeId, cell.StratumId, cell.SecondaryStratumId, cell.TertiaryStratumId, cell.StateClassId, iteration, timestep);

                    if (fp.FromStockTypeId.HasValue && !d.ContainsKey(fp.FromStockTypeId.Value))
                    {
                        float val = GetLimitBasedInitialStock(0.0F, limsrc);
                        d.Add(fp.FromStockTypeId.Value, val);
                    }

                    if (fp.ToStockTypeId.HasValue && !d.ContainsKey(fp.ToStockTypeId.Value))
                    {
                        float val = GetLimitBasedInitialStock(0.0F, limdst);
                        d.Add(fp.ToStockTypeId.Value, val);
                    }

                    float fa = fp.FlowAmount;

                    if (limsrc != null)
                    {
                        if (fp.FromStockTypeId.HasValue)
                        {
                            double srcStkDensity = (d[fp.FromStockTypeId.Value] / this.m_STSimTransformer.AmountPerCell);
                            fa = this.CalculateFlowAmountWithStockLimits(srcStkDensity, limsrc, fa, true);
                        }
                    }

                    if (limdst != null)
                    {
                        if (!fp.IsLateral && fp.ToStockTypeId.HasValue)
                        {
                            double dstStkDensity = (d[fp.ToStockTypeId.Value] / this.m_STSimTransformer.AmountPerCell);
                            fa = this.CalculateFlowAmountWithStockLimits(dstStkDensity, limdst, fa, false);
                        }
                    }

                    if (fp.FromStockTypeId.HasValue)
                    {
                        d[fp.FromStockTypeId.Value] -= Convert.ToSingle(fa);
                        if (MathUtilities.CompareDoublesEqual(d[fp.FromStockTypeId.Value], 0.0, 0.00000001))
                        {
                            d[fp.FromStockTypeId.Value] = 0.0F;
                        }
                    }

                    if (fp.IsLateral)
                    {
                        this.AccumulateLateralFlowAmounts(fp, fa);
                    }
                    else
                    {
                        if (fp.ToStockTypeId.HasValue)
                        {
                            d[fp.ToStockTypeId.Value] += Convert.ToSingle(fa);
                        }
                    }

                    this.RecordSummaryFlowOutputData(timestep, cell, dtPathway, ptPathway, fp, fa);
                    this.RecordSpatialFlowOutputData(timestep, cell, fp.FlowTypeId, fa);

                    if (fp.IsLateral)
                    {
                        this.RecordSpatialLateralFlowOutputData(timestep, cell, fp.FlowTypeId, -fa);
                    }
                }
            }
        }

        private float CalculateFlowAmountWithStockLimits(double StkDensity, StockLimit lim, float fa, bool src)
        {
            double faDensity = fa / this.m_STSimTransformer.AmountPerCell;

            if (src)
            {
                if ((StkDensity - faDensity) < lim.StockMinimum)
                {
                    fa = Convert.ToSingle((StkDensity - lim.StockMinimum) * this.m_STSimTransformer.AmountPerCell);
                }
                else if ((StkDensity - faDensity) > lim.StockMaximum)
                {
                    fa = Convert.ToSingle((lim.StockMaximum - StkDensity) * this.m_STSimTransformer.AmountPerCell);
                }
            }
            else
            {
                if ((StkDensity + faDensity) > lim.StockMaximum)
                {
                    fa = Convert.ToSingle((lim.StockMaximum - StkDensity) * this.m_STSimTransformer.AmountPerCell);
                }
                else if ((StkDensity + faDensity) < lim.StockMinimum)
                {
                    fa = -(Convert.ToSingle((StkDensity - lim.StockMinimum) * this.m_STSimTransformer.AmountPerCell));
                }
            }

            return fa;
        }

        private void ResampleFlowMultiplierValues(int iteration, int timestep, DistributionFrequency frequency)
        {
            try
            {
                foreach (FlowMultiplier t in this.m_FlowMultipliers)
                {
                    if (!t.IsDisabled)
                    {
                        t.Sample(iteration, timestep, this.m_STSimTransformer.DistributionProvider, frequency);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Flow Multipliers" + " -> " + ex.Message);
            }
        }

        private void ResampleFlowMultiplierByStockValues(int iteration, int timestep, DistributionFrequency frequency)
        {
            try
            {
                foreach (FlowMultiplierByStock t in this.m_FlowMultipliersByStock)
                {
                    if (!t.IsDisabled)
                    {
                        t.Sample(iteration, timestep, this.m_STSimTransformer.DistributionProvider, frequency);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Stock Flow Multipliers" + " -> " + ex.Message);
            }
        }

        private void AccumulateLateralFlowAmounts(FlowPathway flowPathway, float flowAmount)
        {
            this.m_LateralFlowAmountMap.AddOrUpdate(
                flowPathway.ToStockTypeId,
                flowPathway.FlowTypeId,
                flowPathway.TransferToStratumId,
                flowPathway.TransferToSecondaryStratumId,
                flowPathway.TransferToTertiaryStratumId,
                flowPathway.TransferToStateClassId,
                flowPathway.TransferToMinimumAge,
                flowAmount);
        }

        private void DistributeLateralFlows(int iteration, int timestep)
        {
            foreach (LateralFlowCouplet couplet in this.m_LateralFlowCoupletMap.Couplets)
            {
                foreach (Cell cell in this.m_STSimTransformer.Cells)
                {
                    LateralFlowAmountRecord rec = this.m_LateralFlowAmountMap.GetRecord(
                        couplet.StockTypeId,
                        couplet.FlowTypeId,
                        cell.StratumId,
                        cell.SecondaryStratumId,
                        cell.TertiaryStratumId,
                        cell.StateClassId,
                        cell.Age);

                    if (rec != null)
                    {
                        double InvMult = this.GetFlowLateralMultiplier(couplet.FlowTypeId, cell, iteration, timestep);

                        if (InvMult > 0.0)
                        {
                            rec.Cells.Add(cell);
                            rec.InverseMultiplier += InvMult;
                        }
                    }
                }
            }

            foreach (LateralFlowAmountRecord rec in this.m_LateralFlowAmountMap.AllRecords)
            {
                foreach (Cell RecCell in rec.Cells)
                {
                    Dictionary<int, float> d = GetStockAmountDictionary(RecCell);

                    double LateralFlowMultiplier = this.GetFlowLateralMultiplier(rec.FlowTypeId, RecCell, iteration, timestep);
                    float FlowAmount = Convert.ToSingle((LateralFlowMultiplier / rec.InverseMultiplier) * rec.StockAmount);

                    if (rec.StockTypeId.HasValue)
                    {
                        d[rec.StockTypeId.Value] += FlowAmount;
                    }
                    this.RecordSpatialLateralFlowOutputData(timestep, RecCell, rec.FlowTypeId, FlowAmount);
                }
            }
        }

        private static float GetLimitBasedInitialStock(float value, StockLimit limit)
        {
            float v = value;

            if (limit != null)
            {
                if (v < limit.StockMinimum)
                {
                    v = Convert.ToSingle(limit.StockMinimum);
                }
                else if (v > limit.StockMaximum)
                {
                    v = Convert.ToSingle(limit.StockMinimum);
                }
            }

            return v;
        }

        private float CalculateFlowAmount(FlowPathway fp, Cell cell, int iteration, int timestep)
        {
            if (fp.TargetType == TargetType.Flow)
            {
                return Convert.ToSingle(this.CalculateFlowAmountTargetTypeFlow(fp, cell, iteration, timestep));
            }
            else if (fp.TargetType == TargetType.FromStock)
            {
                return this.CalculateFlowAmountTargetTypeFromStock(fp, cell, iteration, timestep);
            }
            else
            {
                return this.CalculateFlowAmountTargetTypeToStock(fp, cell, iteration, timestep);
            }
        }

        private double CalculateFlowAmountTargetTypeFlow(FlowPathway fp, Cell cell, int iteration, int timestep)
        {
            float FlowAmount = 0.0F;

            if (fp.StateAttributeTypeId.HasValue)
            {
                FlowAmount = this.GetAttributeValue(
                            fp.StateAttributeTypeId.Value, cell.StratumId, cell.SecondaryStratumId,
                            cell.TertiaryStratumId, cell.StateClassId, iteration, timestep, cell.Age, cell.TstValues);

                FlowAmount *= Convert.ToSingle(this.m_STSimTransformer.AmountPerCell);
            }
            else
            {
                Dictionary<int, float> d = GetStockAmountDictionary(cell);

                if (fp.FromStockTypeId.HasValue && !d.ContainsKey(fp.FromStockTypeId.Value))
                {
                    d.Add(fp.FromStockTypeId.Value, 0.0F);
                }

                if (fp.FromStockTypeId.HasValue)
                {
                    FlowAmount = d[fp.FromStockTypeId.Value];
                }
            }

            return this.ApplyFlowMultipliers(cell, fp, iteration, timestep, FlowAmount);
        }

        private float CalculateFlowAmountTargetTypeFromStock(FlowPathway fp, Cell cell, int iteration, int timestep)
        {
            if (!fp.FromStockTypeId.HasValue)
            {
                return 0.0F;
            }

            if (this.DisabledFlowMultiplierExists(cell, fp, iteration, timestep))
            {
                return 0.0F;
            }

            Dictionary<int, float> d = GetStockAmountDictionary(cell);

            if (!d.ContainsKey(fp.FromStockTypeId.Value))
            {
                d.Add(fp.FromStockTypeId.Value, 0.0F);
            }

            float StockTargetAmount;
            float FromStockAmount = d[fp.FromStockTypeId.Value];

            if (fp.StateAttributeTypeId.HasValue)
            {
                double AttrVal = this.GetAttributeValue(
                    fp.StateAttributeTypeId.Value, cell.StratumId, cell.SecondaryStratumId,
                    cell.TertiaryStratumId, cell.StateClassId, iteration, timestep, cell.Age, cell.TstValues);

                AttrVal *= this.m_STSimTransformer.AmountPerCell;
                StockTargetAmount = Convert.ToSingle(this.ApplyFlowMultipliers(cell, fp, iteration, timestep, AttrVal));
            }
            else
            {
                StockTargetAmount = Convert.ToSingle(this.ApplyFlowMultipliers(cell, fp, iteration, timestep, FromStockAmount));
            }

            return (FromStockAmount - StockTargetAmount);
        }

        private float CalculateFlowAmountTargetTypeToStock(FlowPathway fp, Cell cell, int iteration, int timestep)
        {
            if (!fp.ToStockTypeId.HasValue)
            {
                return 0.0F;
            }

            if (this.DisabledFlowMultiplierExists(cell, fp, iteration, timestep))
            {
                return 0.0F;
            }

            Dictionary<int, float> d = GetStockAmountDictionary(cell);

            if (!d.ContainsKey(fp.ToStockTypeId.Value))
            {
                d.Add(fp.ToStockTypeId.Value, 0.0F);
            }

            float StockTargetAmount;
            float ToStockAmount = d[fp.ToStockTypeId.Value];

            if (fp.StateAttributeTypeId.HasValue)
            {
                double AttrVal = this.GetAttributeValue(
                    fp.StateAttributeTypeId.Value, cell.StratumId, cell.SecondaryStratumId,
                    cell.TertiaryStratumId, cell.StateClassId, iteration, timestep, cell.Age, cell.TstValues);

                AttrVal *= this.m_STSimTransformer.AmountPerCell;
                StockTargetAmount = Convert.ToSingle(this.ApplyFlowMultipliers(cell, fp, iteration, timestep, AttrVal));
            }
            else
            {
                StockTargetAmount = Convert.ToSingle(this.ApplyFlowMultipliers(cell, fp, iteration, timestep, ToStockAmount));
            }

            return (StockTargetAmount - ToStockAmount);
        }

        private bool DisabledFlowMultiplierExists(Cell cell, FlowPathway fp, int iteration, int timestep)
        {
            FlowType ft = this.m_FlowTypes[fp.FlowTypeId];

            foreach (FlowMultiplierType mt in this.m_FlowMultiplierTypes)
            {
                foreach (FlowGroupLinkage fgl in ft.FlowGroupLinkages)
                {
                    if (mt.FlowMultiplierMap != null)
                    {
                        FlowMultiplier fm = mt.FlowMultiplierMap.GetFlowMultiplierClassInstance(
                            fgl.FlowGroup.Id, cell.StratumId, cell.SecondaryStratumId, cell.TertiaryStratumId,
                            cell.StateClassId, iteration, timestep, cell.Age);

                        if (fm != null && fm.IsDisabled)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private double ApplyFlowMultipliers(Cell cell, FlowPathway fp, int iteration, int timestep, double value)
        {
            value *= fp.Multiplier;

            FlowType ft = this.m_FlowTypes[fp.FlowTypeId];

            foreach (FlowMultiplierType mt in this.m_FlowMultiplierTypes)
            {
                foreach (FlowGroupLinkage fgl in ft.FlowGroupLinkages)
                {
                    if (mt.FlowMultiplierMap != null)
                    {
                        value *= mt.FlowMultiplierMap.GetFlowMultiplier(
                            fgl.FlowGroup.Id, cell.StratumId, cell.SecondaryStratumId, cell.TertiaryStratumId,
                            cell.StateClassId, iteration, timestep, cell.Age);
                    }

                    if (this.m_IsSpatial && mt.FlowSpatialMultiplierMap != null)
                    {
                        value *= this.GetFlowSpatialMultiplier(
                            cell, mt.FlowSpatialMultiplierMap, fgl.FlowGroup.Id, iteration, timestep);
                    }

                    if (mt.FlowMultiplierByStockMap != null)
                    {
                        value *= this.GetFlowMultiplierByStock(
                            fgl.FlowGroup.Id, mt.FlowMultiplierByStockMap, iteration, timestep, cell);
                    }
                }
            }

            return value;
        }

        /// <summary>
        /// Reorders the list of shufflable flow types
        /// </summary>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <remarks></remarks>
        private void ReorderShufflableFlowTypes(int iteration, int timestep)
        {
            FlowOrderCollection orders = this.m_FlowOrderMap.GetOrders(iteration, timestep);

            if (orders == null)
            {
                ShuffleUtilities.ShuffleList(this.m_ShufflableFlowTypes, this.m_RandomGenerator.Random);
            }
            else
            {
                this.ReorderShufflableFlowTypes(orders);
            }
        }

        /// <summary>
        /// Reorders the list of shufflable Flow Types
        /// </summary>
        /// <param name="orders"></param>
        /// <remarks></remarks>
        private void ReorderShufflableFlowTypes(FlowOrderCollection orders)
        {
            //If there are less than two Flow Types there is no reason to continue

            if (this.m_ShufflableFlowTypes.Count <= 1)
            {
                return;
            }

            //Reset all Flow Type order values

            foreach (FlowType ft in this.m_ShufflableFlowTypes)
            {
                ft.Order = Constants.DEFAULT_FLOW_ORDER;
            }

            //Apply the new ordering from the order collection

            Debug.Assert(this.m_FlowTypes.Count == this.m_ShufflableFlowTypes.Count);

            foreach (FlowOrder order in orders)
            {
                if (this.m_FlowTypes.Contains(order.FlowTypeId))
                {
                    Debug.Assert(this.m_ShufflableFlowTypes.Contains(this.m_FlowTypes[order.FlowTypeId]));
                    this.m_FlowTypes[order.FlowTypeId].Order = order.Order;
                }
            }

            //Sort by the Flow Types by the order value

            this.m_ShufflableFlowTypes.Sort((FlowType t1, FlowType t2) =>
            {
                return (t1.Order.CompareTo(t2.Order));
            });

            //Find the number of times each order appears.  If it appears more than
            //once then shuffle the subset of transtion groups with this order.

            Dictionary<double, int> OrderCounts = new Dictionary<double, int>();

            foreach (FlowOrder o in orders)
            {
                if (!OrderCounts.ContainsKey(o.Order))
                {
                    OrderCounts.Add(o.Order, 1);
                }
                else
                {
                    OrderCounts[o.Order] += 1;
                }
            }

            //If any order appears more than once then it is a subset
            //that we need to shuffle.  Note that there may be a subset
            //for the default order.

            foreach (double d in OrderCounts.Keys)
            {
                if (OrderCounts[d] > 1)
                {
                    ShuffleUtilities.ShuffleSubList(
                                  this.m_ShufflableFlowTypes,
                                  this.GetMinOrderIndex(d),
                                  this.GetMaxOrderIndex(d),
                                  this.m_RandomGenerator.Random);
                }
            }

            if (this.DefaultOrderHasSubset())
            {
                ShuffleUtilities.ShuffleSubList(
                            this.m_ShufflableFlowTypes,
                            this.GetMinOrderIndex(
                            Constants.DEFAULT_FLOW_ORDER),
                            this.GetMaxOrderIndex(Constants.DEFAULT_FLOW_ORDER),
                            this.m_RandomGenerator.Random);
            }

        }

        private bool DefaultOrderHasSubset()
        {
            int c = 0;

            foreach (FlowType tg in this.m_ShufflableFlowTypes)
            {
                if (tg.Order == Constants.DEFAULT_FLOW_ORDER)
                {
                    c += 1;

                    if (c == 2)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private int GetMinOrderIndex(double order)
        {
            for (int Index = 0; Index < this.m_ShufflableFlowTypes.Count; Index++)
            {
                FlowType tg = this.m_ShufflableFlowTypes[Index];

                if (tg.Order == order)
                {
                    return Index;
                }
            }

            throw new InvalidOperationException("Cannot find minimum Flow order!");
        }

        private int GetMaxOrderIndex(double order)
        {
            for (int Index = this.m_ShufflableFlowTypes.Count - 1; Index >= 0; Index--)
            {
                FlowType tg = this.m_ShufflableFlowTypes[Index];

                if (tg.Order == order)
                {
                    return Index;
                }
            }

            throw new InvalidOperationException("Cannot find maximum Flow order!");
        }
    }
}