// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    internal partial class StockFlowTransformer
    {
        private readonly StockTypeCollection m_StockTypes = new StockTypeCollection();
        private readonly StockGroupCollection m_StockGroups = new StockGroupCollection();
        private readonly FlowTypeCollection m_FlowTypes = new FlowTypeCollection();
        private readonly FlowGroupCollection m_FlowGroups = new FlowGroupCollection();
        private readonly FlowMultiplierTypeCollection m_FlowMultiplierTypes = new FlowMultiplierTypeCollection();
        private readonly InitialStockNonSpatialCollection m_InitialStocksNonSpatial = new InitialStockNonSpatialCollection();
        private readonly InitialStockSpatialCollection m_InitialStocksSpatial = new InitialStockSpatialCollection();
        private readonly Dictionary<string, SyncroSimRaster> m_InitialStockSpatialRasters = new Dictionary<string, SyncroSimRaster>();
        private readonly StockLimitCollection m_StockLimits = new StockLimitCollection();
        private readonly FlowMultiplierByStockCollection m_FlowMultipliersByStock = new FlowMultiplierByStockCollection();
        private readonly StockTransitionMultiplierCollection m_StockTransitionMultipliers = new StockTransitionMultiplierCollection();
        private readonly FlowPathwayCollection m_FlowPathways = new FlowPathwayCollection();
        private readonly FlowMultiplierCollection m_FlowMultipliers = new FlowMultiplierCollection();
        private readonly FlowSpatialMultiplierCollection m_FlowSpatialMultipliers = new FlowSpatialMultiplierCollection();
        private readonly Dictionary<string, SyncroSimRaster> m_FlowSpatialMultiplierRasters = new Dictionary<string, SyncroSimRaster>();
        private readonly FlowLateralMultiplierCollection m_FlowLateralMultipliers = new FlowLateralMultiplierCollection();
        private readonly Dictionary<string, SyncroSimRaster> m_FlowLateralMultiplierRasters = new Dictionary<string, SyncroSimRaster>();
        private readonly OutputFilterCollection m_OutputFilterStocks = new OutputFilterCollection();
        private readonly OutputFilterCollection m_OutputFilterFlows = new OutputFilterCollection();
        private readonly FlowOrderCollection m_FlowOrders = new FlowOrderCollection();

#if DEBUG
        private bool m_AutoStockLinkagesAdded;
        private bool m_StockTypesFilled;
        private bool m_StockGroupsFilled;
        private bool m_StockTypeLinkagesAdded;
        private bool m_StockGroupLinkagesAdded;

        private bool m_AutoFlowLinkagesAdded;
        private bool m_FlowTypesFilled;
        private bool m_FlowGroupsFilled;
        private bool m_FlowTypeLinkagesAdded;
        private bool m_FlowGroupLinkagesAdded;
#endif

        private void FillStockTypes()
        {
            Debug.Assert(this.m_StockTypes.Count == 0);
            DataSheet ds = this.Project.GetDataSheet(Strings.DATASHEET_STOCK_TYPE_NAME);

            foreach (DataRow dr in ds.GetData().Rows)
            {
                this.m_StockTypes.Add(
                    new StockType(
                        Convert.ToInt32(dr[ds.PrimaryKeyColumn.Name], CultureInfo.InvariantCulture),
                        Convert.ToString(dr[ds.DisplayMember], CultureInfo.InvariantCulture)));
            }

#if DEBUG
            this.m_StockTypesFilled = true;
#endif
        }

        private void FillStockGroups()
        {
            Debug.Assert(this.m_StockGroups.Count == 0);
            DataSheet ds = this.Project.GetDataSheet(Strings.DATASHEET_STOCK_GROUP_NAME);

            foreach (DataRow dr in ds.GetData().Rows)
            {
                this.m_StockGroups.Add(
                    new StockGroup(
                        Convert.ToInt32(dr[ds.PrimaryKeyColumn.Name], CultureInfo.InvariantCulture),
                        Convert.ToString(dr[ds.DisplayMember], CultureInfo.InvariantCulture)));
            }

#if DEBUG
            this.m_StockGroupsFilled = true;
#endif
        }

        private void FillFlowTypes()
        {
            Debug.Assert(this.m_FlowTypes.Count == 0);
            DataSheet ds = this.Project.GetDataSheet(Strings.DATASHEET_FLOW_TYPE_NAME);

            foreach (DataRow dr in ds.GetData().Rows)
            {
                this.m_FlowTypes.Add(
                    new FlowType(
                        Convert.ToInt32(dr[ds.PrimaryKeyColumn.Name], CultureInfo.InvariantCulture),
                        Convert.ToString(dr[ds.DisplayMember], CultureInfo.InvariantCulture)));
            }

#if DEBUG
            this.m_FlowTypesFilled = true;
#endif

        }

        private void FillFlowGroups()
        {
            Debug.Assert(this.m_FlowGroups.Count == 0);
            DataSheet ds = this.Project.GetDataSheet(Strings.DATASHEET_FLOW_GROUP_NAME);

            foreach (DataRow dr in ds.GetData().Rows)
            {
                this.m_FlowGroups.Add(
                            new FlowGroup(
                                Convert.ToInt32(dr[ds.PrimaryKeyColumn.Name], CultureInfo.InvariantCulture),
                                Convert.ToString(dr[ds.DisplayMember], CultureInfo.InvariantCulture)));
            }

#if DEBUG
            this.m_FlowGroupsFilled = true;
#endif
        }

        private void FillStockGroupLinkages()
        {

#if DEBUG
            Debug.Assert(this.m_AutoStockLinkagesAdded);
            Debug.Assert(this.m_StockTypesFilled);
            Debug.Assert(this.m_StockGroupsFilled);
#endif

            DataTable dt = this.ResultScenario.GetDataSheet(Strings.DATASHEET_STOCK_TYPE_GROUP_MEMBERSHIP_NAME).GetData();

            foreach (StockType st in this.m_StockTypes)
            {
                Debug.Assert(st.StockGroupLinkages.Count == 0);

                string q = string.Format(CultureInfo.InvariantCulture, "{0}={1}", Strings.STOCK_TYPE_ID_COLUMN_NAME, st.Id);
                DataRow[] rows = dt.Select(q, null);

                foreach (DataRow dr in rows)
                {
                    int gid = Convert.ToInt32(dr[Strings.STOCK_GROUP_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                    double value = 1.0;

                    if (dr[Strings.DATASHEET_VALUE_COLUMN_NAME] != DBNull.Value)
                    {
                        value = (double)dr[Strings.DATASHEET_VALUE_COLUMN_NAME];
                    }

                    StockGroupLinkage linkage = new StockGroupLinkage(this.m_StockGroups[gid], value);
                    st.StockGroupLinkages.Add(linkage);
                }
            }
#if DEBUG
            this.m_StockGroupLinkagesAdded = true;
#endif
        }

        private void FillStockTypeLinkages()
        {

#if DEBUG
            Debug.Assert(this.m_AutoStockLinkagesAdded);
            Debug.Assert(this.m_StockTypesFilled);
            Debug.Assert(this.m_StockGroupsFilled);
#endif

            DataTable dt = this.ResultScenario.GetDataSheet(Strings.DATASHEET_STOCK_TYPE_GROUP_MEMBERSHIP_NAME).GetData();

            foreach (StockGroup sg in this.m_StockGroups)
            {
                Debug.Assert(sg.StockTypeLinkages.Count == 0);

                string q = string.Format(CultureInfo.InvariantCulture, "{0}={1}", Strings.STOCK_GROUP_ID_COLUMN_NAME, sg.Id);
                DataRow[] rows = dt.Select(q, null);

                foreach (DataRow dr in rows)
                {
                    int tid = Convert.ToInt32(dr[Strings.STOCK_TYPE_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                    float value = 1.0F;

                    if (dr[Strings.DATASHEET_VALUE_COLUMN_NAME] != DBNull.Value)
                    {
                        value = Convert.ToSingle(dr[Strings.DATASHEET_VALUE_COLUMN_NAME]);
                    }

                    StockTypeLinkage linkage = new StockTypeLinkage(this.m_StockTypes[tid], value);
                    sg.StockTypeLinkages.Add(linkage);
                }
            }

#if DEBUG
            this.m_StockTypeLinkagesAdded = true;
#endif
        }

        private void FillFlowGroupLinkages()
        {

#if DEBUG
            Debug.Assert(this.m_AutoFlowLinkagesAdded);
            Debug.Assert(this.m_FlowTypesFilled);
            Debug.Assert(this.m_FlowGroupsFilled);
#endif

            DataTable dt = this.ResultScenario.GetDataSheet(Strings.DATASHEET_FLOW_TYPE_GROUP_MEMBERSHIP_NAME).GetData();

            foreach (FlowType ft in this.m_FlowTypes)
            {
                Debug.Assert(ft.FlowGroupLinkages.Count == 0);

                string q = string.Format(CultureInfo.InvariantCulture, "{0}={1}", Strings.FLOW_TYPE_ID_COLUMN_NAME, ft.Id);
                DataRow[] rows = dt.Select(q, null);

                foreach (DataRow dr in rows)
                {
                    int gid = Convert.ToInt32(dr[Strings.FLOW_GROUP_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                    double value = 1.0;

                    if (dr[Strings.DATASHEET_VALUE_COLUMN_NAME] != DBNull.Value)
                    {
                        value = (double)dr[Strings.DATASHEET_VALUE_COLUMN_NAME];
                    }

                    FlowGroupLinkage linkage = new FlowGroupLinkage(this.m_FlowGroups[gid], value);
                    ft.FlowGroupLinkages.Add(linkage);
                }
            }

#if DEBUG
            this.m_FlowGroupLinkagesAdded = true;
#endif
        }

        private void FillFlowTypeLinkages()
        {

#if DEBUG
            Debug.Assert(this.m_AutoFlowLinkagesAdded);
            Debug.Assert(this.m_FlowTypesFilled);
            Debug.Assert(this.m_FlowGroupsFilled);
#endif

            DataTable dt = this.ResultScenario.GetDataSheet(Strings.DATASHEET_FLOW_TYPE_GROUP_MEMBERSHIP_NAME).GetData();

            foreach (FlowGroup fg in this.m_FlowGroups)
            {
                Debug.Assert(fg.FlowTypeLinkages.Count == 0);

                string q = string.Format(CultureInfo.InvariantCulture, "{0}={1}", Strings.FLOW_GROUP_ID_COLUMN_NAME, fg.Id);
                DataRow[] rows = dt.Select(q, null);

                foreach (DataRow dr in rows)
                {
                    int tid = Convert.ToInt32(dr[Strings.FLOW_TYPE_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                    float value = 1.0F;

                    if (dr[Strings.DATASHEET_VALUE_COLUMN_NAME] != DBNull.Value)
                    {
                        value = Convert.ToSingle(dr[Strings.DATASHEET_VALUE_COLUMN_NAME]);
                    }

                    FlowTypeLinkage linkage = new FlowTypeLinkage(this.m_FlowTypes[tid], value);
                    fg.FlowTypeLinkages.Add(linkage);
                }
            }

#if DEBUG
            this.m_FlowTypeLinkagesAdded = true;
#endif
        }

        private void FillFlowMultiplierTypes()
        {
            Debug.Assert(this.m_FlowMultiplierTypes.Count == 0);
            DataSheet ds = this.Project.GetDataSheet(Strings.DATASHEET_FLOW_MULTIPLIER_TYPE_NAME);

            //Always add type with a Null Id because flow multipliers can have null types.
            this.m_FlowMultiplierTypes.Add(new FlowMultiplierType(null, this.ResultScenario, this.m_STSimTransformer.DistributionProvider));

            foreach (DataRow dr in ds.GetData().Rows)
            {
                int FlowMultiplierTypeId = Convert.ToInt32(dr[ds.PrimaryKeyColumn.Name], CultureInfo.InvariantCulture);

                this.m_FlowMultiplierTypes.Add(new FlowMultiplierType
                    (FlowMultiplierTypeId, this.ResultScenario, this.m_STSimTransformer.DistributionProvider));
            }
        }

        private void FillInitialStocksNonSpatial()
        {
            Debug.Assert(this.m_InitialStocksNonSpatial.Count == 0);
            DataSheet ds = this.ResultScenario.GetDataSheet(Strings.DATASHEET_INITIAL_STOCK_NON_SPATIAL);

            foreach (DataRow dr in ds.GetData().Rows)
            {
                this.m_InitialStocksNonSpatial.Add(
                            new InitialStockNonSpatial(
                                Convert.ToInt32(dr[ds.PrimaryKeyColumn.Name], CultureInfo.InvariantCulture),
                                Convert.ToInt32(dr[Strings.STOCK_TYPE_ID_COLUMN_NAME], CultureInfo.InvariantCulture),
                                Convert.ToInt32(dr[Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_ID_COLUMN_NAME], CultureInfo.InvariantCulture)));
            }
        }

        private void FillInitialStocksSpatial()
        {
            Debug.Assert(this.m_IsSpatial);
            Debug.Assert(this.m_InitialStocksSpatial.Count == 0);
            DataSheet ds = this.ResultScenario.GetDataSheet(Strings.DATASHEET_INITIAL_STOCK_SPATIAL);

            foreach (DataRow dr in ds.GetData().Rows)
            {
                string stockFileName = Convert.ToString(dr[Strings.RASTER_FILE_COLUMN_NAME], CultureInfo.InvariantCulture);
                string fullFilename = Spatial.GetSpatialDataFileName(ds, stockFileName, false);
                int? Iteration = null;

                if (dr[Strings.DATASHEET_ITERATION_COLUMN_NAME] != DBNull.Value)
                {
                    Iteration = Convert.ToInt32(dr[Strings.DATASHEET_ITERATION_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                //Load Initial Stock raster file
                SyncroSimRaster raster;

                try
                {
                    raster = new SyncroSimRaster(fullFilename, RasterDataType.DTDouble);
                }
                catch (Exception)
                {
                    string message = string.Format(CultureInfo.InvariantCulture, Strings.SPATIAL_FILE_STOCK_LOAD_WARNING, stockFileName);
                    throw new ArgumentException(message);
                }

                //Compare the Stock raster metadata to standard to make rasters match
                string cmpMsg = "";
                var cmpResult = this.STSimTransformer.InputRasters.CompareMetadata(raster, ref cmpMsg);

                if (cmpResult == STSim.CompareMetadataResult.RowColumnMismatch)
                {
                    string message = string.Format(CultureInfo.InvariantCulture, Strings.SPATIAL_FILE_STOCK_ROW_COLUMN_MISMATCH, stockFileName, cmpMsg);
                    ExceptionUtils.ThrowArgumentException(message);
                }
                else
                {
                    if (cmpResult == STSim.CompareMetadataResult.UnimportantDifferences)
                    {
                        string message = string.Format(CultureInfo.InvariantCulture, Strings.SPATIAL_FILE_STOCK_METADATA_INFO, stockFileName, cmpMsg);
                        this.RecordStatus(StatusType.Information, message);
                    }

                    this.m_InitialStocksSpatial.Add(
                                  new InitialStockSpatial(
                                      Iteration,
                                      Convert.ToInt32(dr[Strings.STOCK_TYPE_ID_COLUMN_NAME], CultureInfo.InvariantCulture),
                                      stockFileName));

                    //Only loading single instance of a particular raster, as a way to conserve memory

                    if (!m_InitialStockSpatialRasters.ContainsKey(stockFileName))
                    {
                        this.m_InitialStockSpatialRasters.Add(stockFileName, raster);
                    }
                }
            }
        }

        private void FillStockLimits()
        {
            Debug.Assert(this.m_StockLimits.Count == 0);
            DataSheet ds = this.ResultScenario.GetDataSheet(Strings.DATASHEET_STOCK_LIMIT_NAME);

            foreach (DataRow dr in ds.GetData().Rows)
            {
                int? Iteration = null;
                int? Timestep = null;
                int? StratumId = null;
                int? SecondaryStratumId = null;
                int? TertiaryStratumId = null;
                int? StateClassId = null;
                float StockMin = float.MinValue;
                float StockMax = float.MaxValue;

                if (dr[Strings.DATASHEET_ITERATION_COLUMN_NAME] != DBNull.Value)
                {
                    Iteration = Convert.ToInt32(dr[Strings.DATASHEET_ITERATION_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME] != DBNull.Value)
                {
                    Timestep = Convert.ToInt32(dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                int StockTypeId = Convert.ToInt32(dr[Strings.STOCK_TYPE_ID_COLUMN_NAME], CultureInfo.InvariantCulture);

                if (dr[Strings.DATASHEET_STRATUM_ID_COLUMN_NAME] != DBNull.Value)
                {
                    StratumId = Convert.ToInt32(dr[Strings.DATASHEET_STRATUM_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME] != DBNull.Value)
                {
                    SecondaryStratumId = Convert.ToInt32(dr[Strings.DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_TERTIARY_STRATUM_ID_COLUMN_NAME] != DBNull.Value)
                {
                    TertiaryStratumId = Convert.ToInt32(dr[Strings.DATASHEET_TERTIARY_STRATUM_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_STATECLASS_ID_COLUMN_NAME] != DBNull.Value)
                {
                    StateClassId = Convert.ToInt32(dr[Strings.DATASHEET_STATECLASS_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.STOCK_MIN_COLUMN_NAME] != DBNull.Value)
                {
                    StockMin = Convert.ToSingle(dr[Strings.STOCK_MIN_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.STOCK_MAX_COLUMN_NAME] != DBNull.Value)
                {
                    StockMax = Convert.ToSingle(dr[Strings.STOCK_MAX_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                this.m_StockLimits.Add(new StockLimit(
                            Iteration, Timestep, StockTypeId, StratumId,
                            SecondaryStratumId, TertiaryStratumId, StateClassId, StockMin, StockMax));
            }
        }

        private void FillFlowMultipliersByStock()
        {
            Debug.Assert(this.m_FlowMultipliersByStock.Count == 0);
            DataSheet ds = this.ResultScenario.GetDataSheet(Strings.DATASHEET_FLOW_MULTIPLIER_BY_STOCK_NAME);

            foreach (DataRow dr in ds.GetData().Rows)
            {
                int? Iteration = null;
                int? Timestep = null;
                int? StratumId = null;
                int? SecondaryStratumId = null;
                int? TertiaryStratumId = null;
                int? StateClassId = null;
                int? FlowMultiplierTypeId = null;
                int FlowGroupId = Convert.ToInt32(dr[Strings.FLOW_GROUP_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                int StockGroupId = Convert.ToInt32(dr[Strings.STOCK_GROUP_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                double StockValue = Convert.ToDouble(dr[Strings.STOCK_VALUE_COLUMN_NAME], CultureInfo.InvariantCulture);
                double? Multiplier = null;
                int? DistributionTypeId = null;
                DistributionFrequency? DistributionFrequency = null;
                double? DistributionSD = null;
                double? DistributionMin = null;
                double? DistributionMax = null;

                if (dr[Strings.DATASHEET_ITERATION_COLUMN_NAME] != DBNull.Value)
                {
                    Iteration = Convert.ToInt32(dr[Strings.DATASHEET_ITERATION_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME] != DBNull.Value)
                {
                    Timestep = Convert.ToInt32(dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_STRATUM_ID_COLUMN_NAME] != DBNull.Value)
                {
                    StratumId = Convert.ToInt32(dr[Strings.DATASHEET_STRATUM_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME] != DBNull.Value)
                {
                    SecondaryStratumId = Convert.ToInt32(dr[Strings.DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_TERTIARY_STRATUM_ID_COLUMN_NAME] != DBNull.Value)
                {
                    TertiaryStratumId = Convert.ToInt32(dr[Strings.DATASHEET_TERTIARY_STRATUM_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_STATECLASS_ID_COLUMN_NAME] != DBNull.Value)
                {
                    StateClassId = Convert.ToInt32(dr[Strings.DATASHEET_STATECLASS_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.FLOW_MULTIPLIER_TYPE_ID_COLUMN_NAME] != DBNull.Value)
                {
                    FlowMultiplierTypeId = Convert.ToInt32(dr[Strings.FLOW_MULTIPLIER_TYPE_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_MULTIPLIER_COLUMN_NAME] != DBNull.Value)
                {
                    Multiplier = Convert.ToDouble(dr[Strings.DATASHEET_MULTIPLIER_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_DISTRIBUTIONTYPE_COLUMN_NAME] != DBNull.Value)
                {
                    DistributionTypeId = Convert.ToInt32(dr[Strings.DATASHEET_DISTRIBUTIONTYPE_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_DISTRIBUTION_FREQUENCY_COLUMN_NAME] != DBNull.Value)
                {
                    DistributionFrequency = (DistributionFrequency)(long)dr[Strings.DATASHEET_DISTRIBUTION_FREQUENCY_COLUMN_NAME];
                }

                if (dr[Strings.DATASHEET_DISTRIBUTIONSD_COLUMN_NAME] != DBNull.Value)
                {
                    DistributionSD = Convert.ToDouble(dr[Strings.DATASHEET_DISTRIBUTIONSD_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_DISTRIBUTIONMIN_COLUMN_NAME] != DBNull.Value)
                {
                    DistributionMin = Convert.ToDouble(dr[Strings.DATASHEET_DISTRIBUTIONMIN_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_DISTRIBUTIONMAX_COLUMN_NAME] != DBNull.Value)
                {
                    DistributionMax = Convert.ToDouble(dr[Strings.DATASHEET_DISTRIBUTIONMAX_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                try
                {
                    FlowMultiplierByStock Item = new FlowMultiplierByStock(
                                  Iteration, Timestep, StratumId, SecondaryStratumId, TertiaryStratumId, StateClassId,
                                  FlowMultiplierTypeId, FlowGroupId, StockGroupId, StockValue, Multiplier, 
                                  DistributionTypeId, DistributionFrequency, DistributionSD, DistributionMin, DistributionMax);

                    this.m_STSimTransformer.DistributionProvider.Validate(
                                  Item.DistributionTypeId, Item.DistributionValue, Item.DistributionSD,
                                  Item.DistributionMin, Item.DistributionMax);

                    this.m_FlowMultipliersByStock.Add(Item);

                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ds.DisplayName + " -> " + ex.Message);
                }
            }
        }

        private void FillStockTransitionMultipliers()
        {
            Debug.Assert(this.m_StockTransitionMultipliers.Count == 0);
            DataSheet ds = this.ResultScenario.GetDataSheet(Strings.DATASHEET_STOCK_TRANSITION_MULTIPLIER_NAME);

            foreach (DataRow dr in ds.GetData().Rows)
            {
                int? Iteration = null;
                int? Timestep = null;
                int? StratumId = null;
                int? SecondaryStratumId = null;
                int? TertiaryStratumId = null;
                int? StateClassId = null;
                int TransitionGroupId = Convert.ToInt32(dr[Strings.DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                int StockGroupId = Convert.ToInt32(dr[Strings.STOCK_GROUP_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                double StockValue = Convert.ToDouble(dr[Strings.STOCK_VALUE_COLUMN_NAME], CultureInfo.InvariantCulture);
                double? Multiplier = null;
                int? DistributionTypeId = null;
                DistributionFrequency? DistributionFrequency = null;
                double? DistributionSD = null;
                double? DistributionMin = null;
                double? DistributionMax = null;

                if (dr[Strings.DATASHEET_ITERATION_COLUMN_NAME] != DBNull.Value)
                {
                    Iteration = Convert.ToInt32(dr[Strings.DATASHEET_ITERATION_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME] != DBNull.Value)
                {
                    Timestep = Convert.ToInt32(dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_STRATUM_ID_COLUMN_NAME] != DBNull.Value)
                {
                    StratumId = Convert.ToInt32(dr[Strings.DATASHEET_STRATUM_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME] != DBNull.Value)
                {
                    SecondaryStratumId = Convert.ToInt32(dr[Strings.DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_TERTIARY_STRATUM_ID_COLUMN_NAME] != DBNull.Value)
                {
                    TertiaryStratumId = Convert.ToInt32(dr[Strings.DATASHEET_TERTIARY_STRATUM_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_STATECLASS_ID_COLUMN_NAME] != DBNull.Value)
                {
                    StateClassId = Convert.ToInt32(dr[Strings.DATASHEET_STATECLASS_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_MULTIPLIER_COLUMN_NAME] != DBNull.Value)
                {
                    Multiplier = Convert.ToDouble(dr[Strings.DATASHEET_MULTIPLIER_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_DISTRIBUTIONTYPE_COLUMN_NAME] != DBNull.Value)
                {
                    DistributionTypeId = Convert.ToInt32(dr[Strings.DATASHEET_DISTRIBUTIONTYPE_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_DISTRIBUTION_FREQUENCY_COLUMN_NAME] != DBNull.Value)
                {
                    DistributionFrequency = (DistributionFrequency)(long)dr[Strings.DATASHEET_DISTRIBUTION_FREQUENCY_COLUMN_NAME];
                }

                if (dr[Strings.DATASHEET_DISTRIBUTIONSD_COLUMN_NAME] != DBNull.Value)
                {
                    DistributionSD = Convert.ToDouble(dr[Strings.DATASHEET_DISTRIBUTIONSD_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_DISTRIBUTIONMIN_COLUMN_NAME] != DBNull.Value)
                {
                    DistributionMin = Convert.ToDouble(dr[Strings.DATASHEET_DISTRIBUTIONMIN_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_DISTRIBUTIONMAX_COLUMN_NAME] != DBNull.Value)
                {
                    DistributionMax = Convert.ToDouble(dr[Strings.DATASHEET_DISTRIBUTIONMAX_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                try
                {
                    StockTransitionMultiplier Item = new StockTransitionMultiplier(
                                  Iteration, Timestep, StratumId, SecondaryStratumId, TertiaryStratumId, StateClassId,
                                  TransitionGroupId, StockGroupId, StockValue, Multiplier, DistributionTypeId, DistributionFrequency,
                                  DistributionSD, DistributionMin, DistributionMax);

                    this.m_STSimTransformer.DistributionProvider.Validate(
                                  Item.DistributionTypeId, Item.DistributionValue, Item.DistributionSD,
                                  Item.DistributionMin, Item.DistributionMax);

                    this.m_StockTransitionMultipliers.Add(Item);

                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ds.DisplayName + " -> " + ex.Message);
                }
            }
        }

        private void FillFlowPathways()
        {
            Debug.Assert(this.m_FlowPathways.Count == 0);
            Debug.Assert(this.m_LateralFlowCoupletMap == null);

            this.m_LateralFlowCoupletMap = new LateralFlowCoupletMap();
            DataSheet ds = this.ResultScenario.GetDataSheet(Strings.DATASHEET_FLOW_PATHWAY_NAME);

            foreach (DataRow dr in ds.GetData().Rows)
            {
                FlowPathway fp = DataTableUtilities.CreateFlowPathway(dr);
                this.m_FlowPathways.Add(fp);

                if (fp.IsLateral)
                {
                    this.m_LateralFlowCoupletMap.AddCouplet(fp.ToStockTypeId, fp.FlowTypeId);
                }
            }
        }

        private void FillFlowMultipliers()
        {
            Debug.Assert(this.m_FlowMultipliers.Count == 0);
            DataSheet ds = this.ResultScenario.GetDataSheet(Strings.DATASHEET_FLOW_MULTIPLIER_NAME);

            foreach (DataRow dr in ds.GetData().Rows)
            {
                int? Iteration = null;
                int? Timestep = null;
                int? StratumId = null;
                int? SecondaryStratumId = null;
                int? TertiaryStratumId = null;
                int? StateClassId = null;
                int AgeMinimum = 0;
                int AgeMaximum = int.MaxValue;
                int? FlowMultiplierTypeId = null;
                int FlowGroupId = 0;
                double? MultiplierAmount = null;
                int? DistributionTypeId = null;
                DistributionFrequency? DistributionFrequency = null;
                double? DistributionSD = null;
                double? DistributionMin = null;
                double? DistributionMax = null;

                if (dr[Strings.DATASHEET_ITERATION_COLUMN_NAME] != DBNull.Value)
                {
                    Iteration = Convert.ToInt32(dr[Strings.DATASHEET_ITERATION_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME] != DBNull.Value)
                {
                    Timestep = Convert.ToInt32(dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_STRATUM_ID_COLUMN_NAME] != DBNull.Value)
                {
                    StratumId = Convert.ToInt32(dr[Strings.DATASHEET_STRATUM_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME] != DBNull.Value)
                {
                    SecondaryStratumId = Convert.ToInt32(dr[Strings.DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_TERTIARY_STRATUM_ID_COLUMN_NAME] != DBNull.Value)
                {
                    TertiaryStratumId = Convert.ToInt32(dr[Strings.DATASHEET_TERTIARY_STRATUM_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_STATECLASS_ID_COLUMN_NAME] != DBNull.Value)
                {
                    StateClassId = Convert.ToInt32(dr[Strings.DATASHEET_STATECLASS_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_AGE_MIN_COLUMN_NAME] != DBNull.Value)
                {
                    AgeMinimum = Convert.ToInt32(dr[Strings.DATASHEET_AGE_MIN_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_AGE_MAX_COLUMN_NAME] != DBNull.Value)
                {
                    AgeMaximum = Convert.ToInt32(dr[Strings.DATASHEET_AGE_MAX_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.FLOW_MULTIPLIER_TYPE_ID_COLUMN_NAME] != DBNull.Value)
                {
                    FlowMultiplierTypeId = Convert.ToInt32(dr[Strings.FLOW_MULTIPLIER_TYPE_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                FlowGroupId = Convert.ToInt32(dr[Strings.FLOW_GROUP_ID_COLUMN_NAME], CultureInfo.InvariantCulture);

                if (dr[Strings.DATASHEET_VALUE_COLUMN_NAME] != DBNull.Value)
                {
                    MultiplierAmount = Convert.ToDouble(dr[Strings.DATASHEET_VALUE_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_DISTRIBUTIONTYPE_COLUMN_NAME] != DBNull.Value)
                {
                    DistributionTypeId = Convert.ToInt32(dr[Strings.DATASHEET_DISTRIBUTIONTYPE_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_DISTRIBUTION_FREQUENCY_COLUMN_NAME] != DBNull.Value)
                {
                    DistributionFrequency = (DistributionFrequency)(long)dr[Strings.DATASHEET_DISTRIBUTION_FREQUENCY_COLUMN_NAME];
                }

                if (dr[Strings.DATASHEET_DISTRIBUTIONSD_COLUMN_NAME] != DBNull.Value)
                {
                    DistributionSD = Convert.ToDouble(dr[Strings.DATASHEET_DISTRIBUTIONSD_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_DISTRIBUTIONMIN_COLUMN_NAME] != DBNull.Value)
                {
                    DistributionMin = Convert.ToDouble(dr[Strings.DATASHEET_DISTRIBUTIONMIN_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_DISTRIBUTIONMAX_COLUMN_NAME] != DBNull.Value)
                {
                    DistributionMax = Convert.ToDouble(dr[Strings.DATASHEET_DISTRIBUTIONMAX_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                try
                {
                    FlowMultiplier Item = new FlowMultiplier(
                                  Iteration, Timestep,
                                  StratumId, SecondaryStratumId, TertiaryStratumId,
                                  StateClassId, AgeMinimum, AgeMaximum,
                                  FlowMultiplierTypeId, FlowGroupId, MultiplierAmount,
                                  DistributionTypeId, DistributionFrequency, DistributionSD, DistributionMin, DistributionMax);

                    Item.IsDisabled = (!Item.DistributionValue.HasValue && !Item.DistributionTypeId.HasValue);

                    if (!Item.IsDisabled)
                    {
                        this.m_STSimTransformer.DistributionProvider.Validate(
                            Item.DistributionTypeId, Item.DistributionValue, Item.DistributionSD,
                            Item.DistributionMin, Item.DistributionMax);
                    }

                    this.m_FlowMultipliers.Add(Item);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ds.DisplayName + " -> " + ex.Message);
                }
            }
        }

        private void FillOutputFilterStocks()
        {
#if DEBUG
            Debug.Assert(this.m_StockTypeLinkagesAdded);
            Debug.Assert(this.m_StockGroupLinkagesAdded);
            Debug.Assert(!this.m_OutputFilterStocks.HasItems);
#endif
            DataSheet ds = this.ResultScenario.GetDataSheet(Strings.DATASHEET_OUTPUT_FILTER_STOCKS);

            foreach (DataRow dr in ds.GetData().Rows)
            {
                this.m_OutputFilterStocks.Add(
                  new OutputFilterStocks(
                    Convert.ToInt32(dr[Strings.STOCK_GROUP_ID_COLUMN_NAME], CultureInfo.InvariantCulture),
                    Booleans.BoolFromValue(dr[Strings.OUTPUT_SUMMARY_COLUMN_NAME]),
                    Booleans.BoolFromValue(dr[Strings.OUTPUT_SPATIAL_COLUMN_NAME]),
                    Booleans.BoolFromValue(dr[Strings.OUTPUT_AVG_SPATIAL_COLUMN_NAME])));
            }

            foreach (StockGroup g in this.m_StockGroups)
            {
                OutputFilter f = OutputFilter.None;

                if (this.FilterIncludesTabularDataForStockGroup(g.Id)) f |= OutputFilter.Tabular;
                if (this.FilterIncludesSpatialDataForStockGroup(g.Id)) f |= OutputFilter.Spatial;
                if (this.FilterIncludesAvgSpatialDataForStockGroup(g.Id)) f |= OutputFilter.AvgSpatial;

                g.OutputFilter = f;
            }
        }

        private void FillOutputFilterFlows()
        {
#if DEBUG
            Debug.Assert(this.m_FlowTypeLinkagesAdded);
            Debug.Assert(this.m_FlowGroupLinkagesAdded);
            Debug.Assert(!this.m_OutputFilterFlows.HasItems);
#endif
            DataSheet ds = this.ResultScenario.GetDataSheet(Strings.DATASHEET_OUTPUT_FILTER_FLOWS);

            foreach (DataRow dr in ds.GetData().Rows)
            {
                this.m_OutputFilterFlows.Add(
                  new OutputFilterFlows(
                    Convert.ToInt32(dr[Strings.FLOW_GROUP_ID_COLUMN_NAME], CultureInfo.InvariantCulture),
                    Booleans.BoolFromValue(dr[Strings.OUTPUT_SUMMARY_COLUMN_NAME]),
                    Booleans.BoolFromValue(dr[Strings.OUTPUT_SPATIAL_COLUMN_NAME]),
                    Booleans.BoolFromValue(dr[Strings.OUTPUT_AVG_SPATIAL_COLUMN_NAME])));
            }

            foreach (FlowGroup g in this.m_FlowGroups)
            {
                OutputFilter f = OutputFilter.None;

                if (this.FilterIncludesTabularDataForFlowGroup(g.Id)) f |= OutputFilter.Tabular;
                if (this.FilterIncludesSpatialDataForFlowGroup(g.Id)) f |= OutputFilter.Spatial;
                if (this.FilterIncludesAvgSpatialDataForFlowGroup(g.Id)) f |= OutputFilter.AvgSpatial;

                g.OutputFilter = f;
            }

            foreach (FlowType t in this.m_FlowTypes)
            {
                OutputFilter f = OutputFilter.None;

                if (this.FilterIncludesSpatialDataForFlowType(t.Id)) f |= OutputFilter.Spatial;
                if (this.FilterIncludesAvgSpatialDataForFlowType(t.Id)) f |= OutputFilter.AvgSpatial;

                t.OutputFilter = f;
            }
        }

        private void FillFlowOrders()
        {
            Debug.Assert(this.m_FlowOrders.Count == 0);
            DataSheet ds = this.ResultScenario.GetDataSheet(Strings.DATASHEET_FLOW_ORDER);

            foreach (DataRow dr in ds.GetData().Rows)
            {
                int? Iteration = null;
                int? Timestep = null;
                int FlowTypeId = 0;
                double? Order = null;

                if (dr[Strings.DATASHEET_ITERATION_COLUMN_NAME] != DBNull.Value)
                {
                    Iteration = Convert.ToInt32(dr[Strings.DATASHEET_ITERATION_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME] != DBNull.Value)
                {
                    Timestep = Convert.ToInt32(dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                FlowTypeId = Convert.ToInt32(dr[Strings.FLOW_TYPE_ID_COLUMN_NAME], CultureInfo.InvariantCulture);

                if (dr[Strings.DATASHEET_FLOW_ORDER_ORDER_COLUMN_NAME] != DBNull.Value)
                {
                    Order = Convert.ToDouble(dr[Strings.DATASHEET_FLOW_ORDER_ORDER_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                try
                {
                    FlowOrder Item = new FlowOrder(Iteration, Timestep, FlowTypeId, Order);
                    this.m_FlowOrders.Add(Item);
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(ds.DisplayName + " -> " + ex.Message);
                }
            }
        }

        private void FillFlowSpatialMultipliers()
        {
            Debug.Assert(this.m_IsSpatial);
            Debug.Assert(this.m_FlowSpatialMultipliers.Count == 0);
            Debug.Assert(this.m_FlowSpatialMultiplierRasters.Count == 0);
            DataSheet ds = this.ResultScenario.GetDataSheet(Strings.DATASHEET_FLOW_SPATIAL_MULTIPLIER_NAME);

            foreach (DataRow dr in ds.GetData().Rows)
            {
                int FlowGroupId = Convert.ToInt32(dr[Strings.FLOW_GROUP_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                int? FlowMultiplierTypeId = null;
                int? Iteration = null;
                int? Timestep = null;
                string FileName = Convert.ToString(dr[Strings.DATASHEET_MULTIPLIER_FILE_COLUMN_NAME], CultureInfo.InvariantCulture);

                if (dr[Strings.FLOW_MULTIPLIER_TYPE_ID_COLUMN_NAME] != DBNull.Value)
                {
                    FlowMultiplierTypeId = Convert.ToInt32(dr[Strings.FLOW_MULTIPLIER_TYPE_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_ITERATION_COLUMN_NAME] != DBNull.Value)
                {
                    Iteration = Convert.ToInt32(dr[Strings.DATASHEET_ITERATION_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME] != DBNull.Value)
                {
                    Timestep = Convert.ToInt32(dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                FlowSpatialMultiplier Multiplier = new FlowSpatialMultiplier(FlowGroupId, FlowMultiplierTypeId, Iteration, Timestep, FileName);
                string FullFilename = Spatial.GetSpatialDataFileName(ds, FileName, false);
                SyncroSimRaster MultiplierRaster;

                try
                {
                    MultiplierRaster = new SyncroSimRaster(FullFilename, RasterDataType.DTDouble);
                }
                catch (Exception)
                {
                    string msg = string.Format(CultureInfo.InvariantCulture, Strings.SPATIAL_PROCESS_WARNING, FullFilename);
                    throw new ArgumentException(msg);
                }

                this.m_FlowSpatialMultipliers.Add(Multiplier);

                //Only load a single instance of a each unique filename to conserve memory

                if (!this.m_FlowSpatialMultiplierRasters.ContainsKey(FileName))
                {
                    this.STSimTransformer.CompressRasterForCellCollection(MultiplierRaster);
                    this.m_FlowSpatialMultiplierRasters.Add(FileName, MultiplierRaster);
                }
            }
        }

        private void FillFlowLateralMultipliers()
        {
            Debug.Assert(this.m_IsSpatial);
            Debug.Assert(this.m_FlowLateralMultipliers.Count == 0);
            Debug.Assert(this.m_FlowLateralMultiplierRasters.Count == 0);
            DataSheet ds = this.ResultScenario.GetDataSheet(Strings.DATASHEET_FLOW_LATERAL_MULTIPLIER_NAME);

            foreach (DataRow dr in ds.GetData().Rows)
            {
                int FlowGroupId = Convert.ToInt32(dr[Strings.FLOW_GROUP_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                int? FlowMultiplierTypeId = null;
                int? Iteration = null;
                int? Timestep = null;
                string FileName = Convert.ToString(dr[Strings.DATASHEET_MULTIPLIER_FILE_COLUMN_NAME], CultureInfo.InvariantCulture);

                if (dr[Strings.FLOW_MULTIPLIER_TYPE_ID_COLUMN_NAME] != DBNull.Value)
                {
                    FlowMultiplierTypeId = Convert.ToInt32(dr[Strings.FLOW_MULTIPLIER_TYPE_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_ITERATION_COLUMN_NAME] != DBNull.Value)
                {
                    Iteration = Convert.ToInt32(dr[Strings.DATASHEET_ITERATION_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                if (dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME] != DBNull.Value)
                {
                    Timestep = Convert.ToInt32(dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                FlowLateralMultiplier Multiplier = new FlowLateralMultiplier(FlowGroupId, FlowMultiplierTypeId, Iteration, Timestep, FileName);
                string FullFilename = Spatial.GetSpatialDataFileName(ds, FileName, false);
                SyncroSimRaster MultiplierRaster;

                try
                {
                    MultiplierRaster = new SyncroSimRaster(FullFilename, RasterDataType.DTDouble);
                }
                catch (Exception)
                {
                    string msg = string.Format(CultureInfo.InvariantCulture, Strings.SPATIAL_PROCESS_WARNING, FullFilename);
                    throw new ArgumentException(msg);
                }

                this.m_FlowLateralMultipliers.Add(Multiplier);

                //Only load a single instance of a each unique filename to conserve memory

                if (!this.m_FlowLateralMultiplierRasters.ContainsKey(FileName))
                {
                    this.STSimTransformer.CompressRasterForCellCollection(MultiplierRaster);
                    this.m_FlowLateralMultiplierRasters.Add(FileName, MultiplierRaster);
                }
            }
        }
    }
}
