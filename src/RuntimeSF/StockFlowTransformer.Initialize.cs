// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    internal partial class StockFlowTransformer
    {
        /// <summary>
        /// Sets whether or not this is a spatial model run
        /// </summary>
        /// <remarks></remarks>
        private void InitializeSpatialRunFlag()
        {
            DataRow drrc = this.ResultScenario.GetDataSheet(Strings.DATASHEET_RUN_CONTROL_NAME).GetDataRow();
            this.m_IsSpatial = DataTableUtilities.GetDataBool(drrc[Strings.RUN_CONTROL_IS_SPATIAL_COLUMN_NAME]);
        }

        /// <summary>
        /// Sets the Flow Order Options
        /// </summary>
        /// <remarks></remarks>
        private void InitializeFlowOrderOptions()
        {
            DataRow dr = this.ResultScenario.GetDataSheet(Strings.DATASHEET_FLOW_ORDER_OPTIONS).GetDataRow();

            if (dr != null)
            {
                this.m_ApplyBeforeTransitions = DataTableUtilities.GetDataBool(dr, Strings.DATASHEET_FLOW_ORDER_OPTIONS_ABT_COLUMN_NAME);
                this.m_ApplyEquallyRankedSimultaneously = DataTableUtilities.GetDataBool(dr, Strings.DATASHEET_FLOW_ORDER_OPTIONS_AERS_COLUMN_NAME);
            }
        }

        /// <summary>
        /// Initializes the output options
        /// </summary>
        /// <remarks></remarks>
        private void InitializeOutputOptions()
        {
            DataRow droo = this.ResultScenario.GetDataSheet(Strings.DATASHEET_STOCKFLOW_OO_NAME).GetDataRow();

            Func<object, int> SafeInt = (object o) =>
            {
                if (o == DBNull.Value)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(o, CultureInfo.InvariantCulture);
                }
            };

            this.m_CreateSummaryStockOutput = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_ST_COLUMN_NAME]);
            this.m_SummaryStockOutputTimesteps = SafeInt(droo[Strings.DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_ST_TIMESTEPS_COLUMN_NAME]);
            this.m_STSummaryOmitSecondaryStrata = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_ST_OMIT_SS_COLUMN_NAME]);
            this.m_STSummaryOmitTertiaryStrata = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_ST_OMIT_TS_COLUMN_NAME]);
            this.m_STSummaryOmitStateClass = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_ST_OMIT_SC_COLUMN_NAME]);

            this.m_CreateSummaryFlowOutput = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_FL_COLUMN_NAME]);
            this.m_SummaryFlowOutputTimesteps = SafeInt(droo[Strings.DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_FL_TIMESTEPS_COLUMN_NAME]);
            this.m_FLSummaryOmitSecondaryStrata = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_FL_OMIT_SS_COLUMN_NAME]);
            this.m_FLSummaryOmitTertiaryStrata = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_FL_OMIT_TS_COLUMN_NAME]);
            this.m_FLSummaryOmitFromStateClass = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_FL_OMIT_FROM_SC_COLUMN_NAME]);
            this.m_FLSummaryOmitFromStockType = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_FL_OMIT_FROM_ST_COLUMN_NAME]);
            this.m_FLSummaryOmitTransitionType = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_FL_OMIT_TT_COLUMN_NAME]);
            this.m_FLSummaryOmitToStateClass = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_FL_OMIT_TO_SC_COLUMN_NAME]);
            this.m_FLSummaryOmitToStockType = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_FL_OMIT_TO_ST_COLUMN_NAME]);

            this.m_CreateSpatialStockOutput = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_STOCKFLOW_OO_SPATIAL_OUTPUT_ST_COLUMN_NAME]);
            this.m_SpatialStockOutputTimesteps = SafeInt(droo[Strings.DATASHEET_STOCKFLOW_OO_SPATIAL_OUTPUT_ST_TIMESTEPS_COLUMN_NAME]);
            this.m_CreateSpatialFlowOutput = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_STOCKFLOW_OO_SPATIAL_OUTPUT_FL_COLUMN_NAME]);
            this.m_SpatialFlowOutputTimesteps = SafeInt(droo[Strings.DATASHEET_STOCKFLOW_OO_SPATIAL_OUTPUT_FL_TIMESTEPS_COLUMN_NAME]);
            this.m_CreateLateralFlowOutput = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_STOCKFLOW_OO_LATERAL_OUTPUT_FL_COLUMN_NAME]);
            this.m_LateralFlowOutputTimesteps = SafeInt(droo[Strings.DATASHEET_STOCKFLOW_OO_LATERAL_OUTPUT_FL_TIMESTEPS_COLUMN_NAME]);
            this.m_CreateAvgSpatialStockOutput = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_STOCKFLOW_OO_AVG_SPATIAL_OUTPUT_ST_COLUMN_NAME]);
            this.m_AvgSpatialStockOutputTimesteps = SafeInt(droo[Strings.DATASHEET_STOCKFLOW_OO_AVG_SPATIAL_OUTPUT_ST_TIMESTEPS_COLUMN_NAME]);
            this.m_AvgSpatialStockOutputAcrossTimesteps = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_STOCKFLOW_OO_AVG_SPATIAL_OUTPUT_ST_ACROSS_TIMESTEPS_COLUMN_NAME]);
            this.m_CreateAvgSpatialFlowOutput = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_STOCKFLOW_OO_AVG_SPATIAL_OUTPUT_FL_COLUMN_NAME]);
            this.m_AvgSpatialFlowOutputTimesteps = SafeInt(droo[Strings.DATASHEET_STOCKFLOW_OO_AVG_SPATIAL_OUTPUT_FL_TIMESTEPS_COLUMN_NAME]);
            this.m_AvgSpatialFlowOutputAcrossTimesteps = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_STOCKFLOW_OO_AVG_SPATIAL_OUTPUT_FL_ACROSS_TIMESTEPS_COLUMN_NAME]);
            this.m_CreateAvgSpatialLateralFlowOutput = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_STOCKFLOW_OO_AVG_SPATIAL_OUTPUT_LFL_COLUMN_NAME]);
            this.m_AvgSpatialLateralFlowOutputTimesteps = SafeInt(droo[Strings.DATASHEET_STOCKFLOW_OO_AVG_SPATIAL_OUTPUT_LFL_TIMESTEPS_COLUMN_NAME]);
            this.m_AvgSpatialLateralFlowOutputAcrossTimesteps = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_STOCKFLOW_OO_AVG_SPATIAL_OUTPUT_LFL_ACROSS_TIMESTEPS_COLUMN_NAME]);
        }

        /// <summary>
        /// Initializes all distribution values
        /// </summary>
        private void InitializeDistributionValues()
        {
            this.InitializeFlowMultiplierDistributionValues();
            this.InitializeStockTransitionMultiplierDistributionValues();
            this.InitializeFlowMultiplierByStockDistributionValues();
        }

        /// <summary>
        /// Initializes distribution values for the flow multipliers
        /// </summary>
        internal void InitializeFlowMultiplierDistributionValues()
        {
            try
            {
                foreach (FlowMultiplier t in this.m_FlowMultipliers)
                {
                    if (!t.IsDisabled)
                    {
                        t.Initialize(
                            this.m_STSimTransformer.MinimumIteration,
                            this.m_STSimTransformer.MinimumTimestep,
                            this.m_STSimTransformer.DistributionProvider);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Flow Multipliers" + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Initializes distribution values for the stock flow multipliers
        /// </summary>
        internal void InitializeFlowMultiplierByStockDistributionValues()
        {
            try
            {
                foreach (FlowMultiplierByStock t in this.m_FlowMultipliersByStock)
                {
                    t.Initialize(
                        this.m_STSimTransformer.MinimumIteration,
                        this.m_STSimTransformer.MinimumTimestep,
                        this.m_STSimTransformer.DistributionProvider);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Stock Flow Multipliers" + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Initializes distribution values for the stock transition multipliers
        /// </summary>
        internal void InitializeStockTransitionMultiplierDistributionValues()
        {
            try
            {
                foreach (StockTransitionMultiplier t in this.m_StockTransitionMultipliers)
                {
                    t.Initialize(
                        this.m_STSimTransformer.MinimumIteration,
                        this.m_STSimTransformer.MinimumTimestep,
                        this.m_STSimTransformer.DistributionProvider);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Stock Transition Multipliers" + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Initializes a separate list of Flow Types that can be shuffled
        /// </summary>
        /// <remarks>
        /// The main list is keyed and cannot be shuffled, but we need a shuffled list for doing raster simulations
        /// </remarks>
        private void InitializeShufflableFlowTypes()
        {
            Debug.Assert(this.m_ShufflableFlowTypes.Count == 0);

            foreach (FlowType ft in this.m_FlowTypes)
            {
                this.m_ShufflableFlowTypes.Add(ft);
            }
        }

        /// <summary>
        /// Adds automatic stock type/group linkages
        /// </summary>
        private void AddAutoStockTypeLinkages()
        {
            DataSheet ds = this.ResultScenario.GetDataSheet(Strings.DATASHEET_STOCK_TYPE_GROUP_MEMBERSHIP_NAME);
            DataTable dt = ds.GetData();

            foreach (StockType t in this.m_StockTypes)
            {
                DataRow NewRow = dt.NewRow();

                NewRow[Strings.STOCK_TYPE_ID_COLUMN_NAME] = t.Id;
                NewRow[Strings.STOCK_GROUP_ID_COLUMN_NAME] = this.GetAutoGeneratedStockGroup(t).Id;

                dt.Rows.Add(NewRow);
            }
#if DEBUG
            this.m_AutoStockLinkagesAdded = true;
#endif
        }

        /// <summary>
        /// Adds automatic flow type/group linkages
        /// </summary>
        private void AddAutoFlowTypeLinkages()
        {
            DataSheet ds = this.ResultScenario.GetDataSheet(Strings.DATASHEET_FLOW_TYPE_GROUP_MEMBERSHIP_NAME);
            DataTable dt = ds.GetData();

            foreach (FlowType t in this.m_FlowTypes)
            {
                DataRow NewRow = dt.NewRow();

                NewRow[Strings.FLOW_TYPE_ID_COLUMN_NAME] = t.Id;
                NewRow[Strings.FLOW_GROUP_ID_COLUMN_NAME] = this.GetAutoGeneratedFlowGroup(t).Id;

                dt.Rows.Add(NewRow);
            }

#if DEBUG
            this.m_AutoFlowLinkagesAdded = true;
#endif
        }

        private StockGroup GetAutoGeneratedStockGroup(StockType t)
        {
            string n = DataTableUtilities.GetAutoGeneratedGroupName(t.Name);

            foreach (StockGroup g in this.m_StockGroups)
            {
                if (g.Name == n)
                {
                    return g;
                }
            }

            throw new ArgumentException("Auto-generated group not found for stock type: " + t.Name);
        }

        private FlowGroup GetAutoGeneratedFlowGroup(FlowType t)
        {
            string n = DataTableUtilities.GetAutoGeneratedGroupName(t.Name);

            foreach (FlowGroup g in this.m_FlowGroups)
            {
                if (g.Name == n)
                {
                    return g;
                }
            }

            throw new ArgumentException("Auto-generated group not found for flow type: " + t.Name);
        }

        private void InitializeAverageStockMap()
        {
            Debug.Assert(this.STSimTransformer.IsSpatial);
            Debug.Assert(this.STSimTransformer.MinimumTimestep > 0);

            if (!this.m_CreateAvgSpatialStockOutput)
            {
                return;
            }

            foreach (StockGroup sg in this.m_StockGroups)
            {
                if (!sg.OutputFilter.HasFlag(OutputFilter.AvgSpatial))
                {
                    continue;
                }

                Dictionary<int, float[]> dict = new Dictionary<int, float[]>();

                for (var timestep = this.STSimTransformer.MinimumTimestep; timestep <= this.STSimTransformer.MaximumTimestep; timestep++)
                {
                    if (this.m_STSimTransformer.IsOutputTimestepAverage(
                        timestep,
                        this.m_AvgSpatialStockOutputTimesteps,
                        this.m_CreateAvgSpatialStockOutput))
                    {
                        float[] values = new float[this.STSimTransformer.Cells.Count];

                        for (var i = 0; i < this.STSimTransformer.Cells.Count; i++)
                        {
                            values[i] = 0;
                        }

                        dict.Add(timestep, values);
                    }
                }

                if (!dict.ContainsKey(this.STSimTransformer.TimestepZero))
                {
                    float[] values = new float[this.STSimTransformer.Cells.Count];

                    for (var i = 0; i < this.STSimTransformer.Cells.Count; i++)
                    {
                        values[i] = 0;
                    }

                    dict.Add(this.STSimTransformer.TimestepZero, values);
                }

                this.m_AvgStockMap.Add(sg.Id, dict);
            }
        }

        private void InitializeAverageFlowMap()
        {
            Debug.Assert(this.STSimTransformer.IsSpatial);
            Debug.Assert(this.STSimTransformer.MinimumTimestep > 0);

            if (!this.m_CreateAvgSpatialFlowOutput)
            {
                return;
            }

            foreach (FlowGroup fg in this.m_FlowGroups)
            {
                if (!fg.OutputFilter.HasFlag(OutputFilter.AvgSpatial))
                {
                    continue;
                }

                Dictionary<int, float[]> dict = new Dictionary<int, float[]>();

                for (var timestep = this.STSimTransformer.MinimumTimestep; timestep <= this.STSimTransformer.MaximumTimestep; timestep++)
                {
                    if (this.m_STSimTransformer.IsOutputTimestepAverage(
                        timestep,
                        this.m_AvgSpatialFlowOutputTimesteps,
                        this.m_CreateAvgSpatialFlowOutput))
                    {
                        float[] values = new float[this.STSimTransformer.Cells.Count];

                        for (var i = 0; i < this.STSimTransformer.Cells.Count; i++)
                        {
                            values[i] = 0;
                        }

                        dict.Add(timestep, values);
                    }
                }

                this.m_AvgFlowMap.Add(fg.Id, dict);
            }
        }

        private void InitializeAverageLateralFlowMap()
        {
            Debug.Assert(this.STSimTransformer.IsSpatial);
            Debug.Assert(this.STSimTransformer.MinimumTimestep > 0);

            if (!this.m_CreateAvgSpatialLateralFlowOutput)
            {
                return;
            }

            foreach (FlowGroup fg in this.m_FlowGroups)
            {
                if (!fg.OutputFilter.HasFlag(OutputFilter.AvgSpatial))
                {
                    continue;
                }

                Dictionary<int, float[]> dict = new Dictionary<int, float[]>();

                for (var timestep = this.STSimTransformer.MinimumTimestep; timestep <= this.STSimTransformer.MaximumTimestep; timestep++)
                {
                    if (this.m_STSimTransformer.IsOutputTimestepAverage(
                        timestep,
                        this.m_AvgSpatialLateralFlowOutputTimesteps,
                        this.m_CreateAvgSpatialLateralFlowOutput))
                    {
                        float[] values = new float[this.STSimTransformer.Cells.Count];

                        for (var i = 0; i < this.STSimTransformer.Cells.Count; i++)
                        {
                            values[i] = 0;
                        }

                        dict.Add(timestep, values);
                    }
                }

                this.m_AvgLateralFlowMap.Add(fg.Id, dict);
            }
        }
    }
}
