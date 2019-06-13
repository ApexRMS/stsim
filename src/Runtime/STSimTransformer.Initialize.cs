// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

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
        /// Initializes the model
        /// </summary>
        /// <remarks>
        /// This function must be called once the model has been configured
        /// </remarks>
        private void InitializeModel()
        {
            if (this.m_Cells.Count == 0)
            {
                ExceptionUtils.ThrowArgumentException("You must have at least one cell to run the simulation.");
            }

            if (!this.IsSpatial)
            {
                if (this.m_InitialConditionsDistributionMap.GetICDs(this.MinimumIteration).Count == 0)
                {
                    ExceptionUtils.ThrowArgumentException("The initial conditions distribution collection cannot be empty.");
                }
            }

            Debug.Assert(this.MinimumTimestep > 0);
            Debug.Assert(this.MinimumIteration > 0);

            this.InitializeCellArea();
            this.InitializeCollectionMaps();
            this.InitializeIntervalMeanTimestepMap();
            this.InitializeStateAttributes();
            this.InitializeTransitionAttributes();
            this.InitializeShufflableTransitionGroups();
            this.InitializeTransitionTargetPrioritizations();
            this.InitializeTransitionAttributeTargetPrioritizations();

            if (this.IsSpatial)
            {
                this.InitializeTransitionSpreadGroups();
            }

            Debug.Assert(this.m_SummaryStratumStateResults.Count == 0);
            Debug.Assert(this.m_SummaryStratumTransitionStateResults.Count == 0);
        }

        /// <summary>
        /// Configures the lower case version of the timestep units
        /// </summary>
        private void ConfigureTimestepUnits()
        {
            this.TimestepUnits = TerminologyUtilities.GetTimestepUnits(this.Project);
            this.m_TimestepUnitsLower = this.TimestepUnits.ToLower(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Initializes the IsSpatial run flag
        /// </summary>
        /// <remarks></remarks>
        private void ConfigureIsSpatialRunFlag()
        {
            DataRow dr = this.ResultScenario.GetDataSheet(Strings.DATASHEET_RUN_CONTROL_NAME).GetDataRow();

            if (dr != null)
            {
                this.m_IsSpatial = DataTableUtilities.GetDataBool(dr[Strings.RUN_CONTROL_IS_SPATIAL_COLUMN_NAME]);
            }
        }

        /// <summary>
        /// Configures the timesteps and iterations for this model run
        /// </summary>
        /// <remarks></remarks>
        private void ConfigureTimestepsAndIterations()
        {
            DataRow dr = this.ResultScenario.GetDataSheet(Strings.DATASHEET_RUN_CONTROL_NAME).GetDataRow();

            this.MinimumIteration = Convert.ToInt32(dr["MinimumIteration"], CultureInfo.InvariantCulture);
            this.MaximumIteration = Convert.ToInt32(dr["MaximumIteration"], CultureInfo.InvariantCulture);
            this.MinimumTimestep = Convert.ToInt32(dr["MinimumTimestep"], CultureInfo.InvariantCulture);
            this.MaximumTimestep = Convert.ToInt32(dr["MaximumTimestep"], CultureInfo.InvariantCulture);

            //We want run control to have the minimum timestep that it is configured with, but we don't want
            //to run this timestep.  Instead, we want to set TimestepZero to the minimum timestep and run the
            //model starting at MinimumTimestep + 1.  We need to configure these values before initializing the
            //rest of the model because some of the initialization routines depend on these values being set.

            if (this.MinimumTimestep == this.MaximumTimestep)
            {
                ExceptionUtils.ThrowArgumentException(
                    "ST-Sim: The start {0} and end {1} cannot be the same.", 
                    this.m_TimestepUnitsLower, this.m_TimestepUnitsLower);
            }

            this.m_TimestepZero = this.MinimumTimestep;
            this.MinimumTimestep = this.MinimumTimestep + 1;
        }

        /// <summary>
        /// Initializes the output data tables
        /// </summary>
        /// <remarks></remarks>
        private void InitializeOutputDataTables()
        {
            Debug.Assert(this.m_OutputStratumStateTable == null);

            this.m_OutputStratumAmountTable = this.ResultScenario.GetDataSheet(Strings.DATASHEET_OUTPUT_STRATUM_NAME).GetData();
            this.m_OutputStratumStateTable = this.ResultScenario.GetDataSheet(Strings.DATASHEET_OUTPUT_STRATUM_STATE_NAME).GetData();
            this.m_OutputStratumTransitionTable = this.ResultScenario.GetDataSheet(Strings.DATASHEET_OUTPUT_STRATUM_TRANSITION_NAME).GetData();
            this.m_OutputStratumTransitionStateTable = this.ResultScenario.GetDataSheet(Strings.DATASHEET_OUTPUT_STRATUM_TRANSITION_STATE_NAME).GetData();
            this.m_OutputStateAttributeTable = this.ResultScenario.GetDataSheet(Strings.DATASHEET_OUTPUT_STATE_ATTRIBUTE_NAME).GetData();
            this.m_OutputTransitionAttributeTable = this.ResultScenario.GetDataSheet(Strings.DATASHEET_OUTPUT_TRANSITION_ATTRIBUTE_NAME).GetData();

            Debug.Assert(m_OutputStratumAmountTable.Rows.Count == 0);
            Debug.Assert(m_OutputStratumStateTable.Rows.Count == 0);
            Debug.Assert(m_OutputStratumTransitionTable.Rows.Count == 0);
            Debug.Assert(m_OutputStratumTransitionStateTable.Rows.Count == 0);
            Debug.Assert(m_OutputStateAttributeTable.Rows.Count == 0);
            Debug.Assert(m_OutputTransitionAttributeTable.Rows.Count == 0);
        }

        /// <summary>
        /// Initializes the amount per cell
        /// </summary>
        /// <remarks></remarks>
        private void InitializeCellArea()
        {
            if (!this.IsSpatial)
            {
                DataRow drta = this.ResultScenario.GetDataSheet(Strings.DATASHEET_NSIC_NAME).GetDataRow();

                this.m_TotalAmount = Convert.ToDouble(drta[Strings.DATASHEET_NSIC_TOTAL_AMOUNT_COLUMN_NAME], CultureInfo.InvariantCulture);
                this.m_CalcNumCellsFromDist = DataTableUtilities.GetDataBool(drta, Strings.DATASHEET_NSIC_CALC_FROM_DIST_COLUMN_NAME);
            }
            else
            {
                DataRow drics = this.ResultScenario.GetDataSheet(Strings.DATASHEET_SPPIC_NAME).GetDataRow();
                double cellAreaTU = DataTableUtilities.GetDataDbl(drics[Strings.DATASHEET_SPPIC_CELL_AREA_COLUMN_NAME]);

                if (cellAreaTU.Equals(0))
                {
                    throw new STSimException(MessageStrings.ERROR_SPATIAL_NO_CELL_AREA);
                }

                this.m_TotalAmount = cellAreaTU * this.m_Cells.Count;
                DataRow drISC = this.ResultScenario.GetDataSheet(Strings.DATASHEET_SPPIC_NAME).GetDataRow();

                //Save the Number of Cells count, now that we have a potentially more accurate value than at config time.

                if (Convert.ToInt32(drISC[Strings.DATASHEET_SPPIC_NUM_CELLS_COLUMN_NAME], CultureInfo.InvariantCulture) != this.m_Cells.Count)
                {
                    drISC[Strings.DATASHEET_SPPIC_NUM_CELLS_COLUMN_NAME] = this.m_Cells.Count;
                }
            }

            this.m_AmountPerCell = (this.m_TotalAmount / Convert.ToDouble(this.m_Cells.Count, CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Initializes output options
        /// </summary>
        /// <remarks></remarks>
        private void InitializeOutputOptions()
        {
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

            DataRow droo = this.ResultScenario.GetDataSheet(Strings.DATASHEET_OO_NAME).GetDataRow();

            if (this.IsSpatial)
            {
                this.m_CreateRasterStateClassOutput = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_OO_RASTER_OUTPUT_SC_COLUMN_NAME]);
                this.m_RasterStateClassOutputTimesteps = SafeInt(droo[Strings.DATASHEET_OO_RASTER_OUTPUT_SC_TIMESTEPS_COLUMN_NAME]);
                this.m_CreateRasterTransitionOutput = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_OO_RASTER_OUTPUT_TR_COLUMN_NAME]);
                this.m_RasterTransitionOutputTimesteps = SafeInt(droo[Strings.DATASHEET_OO_RASTER_OUTPUT_TR_TIMESTEPS_COLUMN_NAME]);
                this.m_CreateRasterAgeOutput = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_OO_RASTER_OUTPUT_AGE_COLUMN_NAME]);
                this.m_RasterAgeOutputTimesteps = SafeInt(droo[Strings.DATASHEET_OO_RASTER_OUTPUT_AGE_TIMESTEPS_COLUMN_NAME]);
                this.m_CreateRasterTstOutput = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_OO_RASTER_OUTPUT_TST_COLUMN_NAME]);
                this.m_RasterTstOutputTimesteps = SafeInt(droo[Strings.DATASHEET_OO_RASTER_OUTPUT_TST_TIMESTEPS_COLUMN_NAME]);
                this.m_CreateRasterStratumOutput = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_OO_RASTER_OUTPUT_ST_COLUMN_NAME]);
                this.m_RasterStratumOutputTimesteps = SafeInt(droo[Strings.DATASHEET_OO_RASTER_OUTPUT_ST_TIMESTEPS_COLUMN_NAME]);
                this.m_CreateRasterStateAttributeOutput = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_OO_RASTER_OUTPUT_SA_COLUMN_NAME]);
                this.m_RasterStateAttributeOutputTimesteps = SafeInt(droo[Strings.DATASHEET_OO_RASTER_OUTPUT_SA_TIMESTEPS_COLUMN_NAME]);
                this.m_CreateRasterTransitionAttributeOutput = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_OO_RASTER_OUTPUT_TA_COLUMN_NAME]);
                this.m_RasterTransitionAttributeOutputTimesteps = SafeInt(droo[Strings.DATASHEET_OO_RASTER_OUTPUT_TA_TIMESTEPS_COLUMN_NAME]);
                this.m_CreateRasterAATPOutput = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_OO_RASTER_OUTPUT_AATP_COLUMN_NAME]);
                this.m_RasterAATPTimesteps = SafeInt(droo[Strings.DATASHEET_OO_RASTER_OUTPUT_AATP_TIMESTEPS_COLUMN_NAME]);
                this.m_CreateRasterSizeClassOutput = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_OO_RASTER_OUTPUT_SIZE_CLASS_COLUMN_NAME]);
                this.m_RasterSizeClassTimesteps = SafeInt(droo[Strings.DATASHEET_OO_RASTER_OUTPUT_SIZE_CLASS_TIMESTEPS_COLUMN_NAME]);
            }

            this.m_CreateSummaryStateClassOutput = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_OO_SUMMARY_OUTPUT_SC_COLUMN_NAME]);
            this.m_SummaryStateClassOutputTimesteps = SafeInt(droo[Strings.DATASHEET_OO_SUMMARY_OUTPUT_SC_TIMESTEPS_COLUMN_NAME]);
            this.m_SummaryStateClassOutputAges = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_OO_SUMMARY_OUTPUT_SC_AGES_COLUMN_NAME]);
            this.m_SummaryStateClassZeroValues = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_OO_SUMMARY_OUTPUT_SC_ZERO_VALUES_COLUMN_NAME]);
            this.m_CreateSummaryTransitionOutput = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_OO_SUMMARY_OUTPUT_TR_COLUMN_NAME]);
            this.m_SummaryTransitionOutputTimesteps = SafeInt(droo[Strings.DATASHEET_OO_SUMMARY_OUTPUT_TR_TIMESTEPS_COLUMN_NAME]);
            this.m_SummaryTransitionOutputAges = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_OO_SUMMARY_OUTPUT_TR_AGES_COLUMN_NAME]);
            this.m_SummaryTransitionOutputAsIntervalMean = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_OO_SUMMARY_OUTPUT_TR_INTERVAL_MEAN_COLUMN_NAME]);
            this.m_CreateSummaryTransitionByStateClassOutput = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_OO_SUMMARY_OUTPUT_TRSC_COLUMN_NAME]);
            this.m_SummaryTransitionByStateClassOutputTimesteps = SafeInt(droo[Strings.DATASHEET_OO_SUMMARY_OUTPUT_TRSC_TIMESTEPS_COLUMN_NAME]);
            this.m_CreateSummaryStateAttributeOutput = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_OO_SUMMARY_OUTPUT_SA_COLUMN_NAME]);
            this.m_SummaryStateAttributeOutputTimesteps = SafeInt(droo[Strings.DATASHEET_OO_SUMMARY_OUTPUT_SA_TIMESTEPS_COLUMN_NAME]);
            this.m_SummaryStateAttributeOutputAges = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_OO_SUMMARY_OUTPUT_SA_AGES_COLUMN_NAME]);
            this.m_CreateSummaryTransitionAttributeOutput = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_OO_SUMMARY_OUTPUT_TA_COLUMN_NAME]);
            this.m_SummaryTransitionAttributeOutputTimesteps = SafeInt(droo[Strings.DATASHEET_OO_SUMMARY_OUTPUT_TA_TIMESTEPS_COLUMN_NAME]);
            this.m_SummaryTransitionAttributeOutputAges = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_OO_SUMMARY_OUTPUT_TA_AGES_COLUMN_NAME]);
            this.m_SummaryOmitSecondaryStrata = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_OO_SUMMARY_OUTPUT_OMIT_SS_COLUMN_NAME]);
            this.m_SummaryOmitTertiaryStrata = DataTableUtilities.GetDataBool(droo[Strings.DATASHEET_OO_SUMMARY_OUTPUT_OMIT_TS_COLUMN_NAME]);
        }

        /// <summary>
        /// Initializes the model collections from the data in the input data feeds
        /// </summary>
        /// <remarks></remarks>
        private void InitializeModelCollections()
        {
            this.FillCellCollection();
            this.FillStratumCollection();
            this.FillSecondaryStratumCollection();
            this.FillTertiaryStratumCollection();
            this.FillStateClassCollection();
            this.FillTransitionGroupCollection();
            this.FillTransitionTypeCollection();
            this.FillTransitionSimulationGroupCollection();
            this.FillTypesForTransitionGroups();
            this.FillGroupsForTransitionTypes();
            this.FillTransitionMultiplierTypeCollection();
            this.FillTransitionAttributeTypeCollection();
            this.FillDeterministicTransitionsCollection();
            this.FillProbabilisticTransitionsCollection();
            this.FillStateAttributeValueCollection();
            this.FillTransitionAttributeValueCollection();
            this.FillTransitionOrderCollection();
            this.FillTransitionTargetCollection();
            this.FillTransitionTargetPrioritizationCollection();
            this.FillTransitionAttributeTargetCollection();
            this.FillTransitionAttributeTargetPrioritizationCollection();
            this.FillTstTransitionGroupCollection();
            this.FillTstRandomizeCollection();
            this.FillTransitionMultiplierValueCollection();

            if (this.IsSpatial)
            {
                this.FillPatchPrioritizationCollection();
                this.FillTransitionSpatialMultiplierCollection();
                this.FillTransitionSpatialInitiationMultiplierCollection();
                this.FillTransitionSizeDistributionCollection();
                this.FillTransitionSpreadDistributionCollection();
                this.FillTransitionPatchPrioritizationCollection();
                this.FillTransitionSizePrioritizationCollection();
                this.FillTransitionDirectionMultiplierCollection();
                this.FillTransitionSlopeMultiplierCollection();
                this.FillTransitionAdjacencySettingCollection();
                this.FillTransitionAdjacencyMultiplierCollection();
                this.FillTransitionPathwayAutoCorrelationCollection();

                this.ValidateSpatialPrimaryGroups();
            }
        }

        /// <summary>
        /// Creates a dictionary that maps all timesteps to another 'aggregator' timestep
        /// </summary>
        /// <remarks>This function exists to support the 'calculate as interval mean values' feature for summary transition output.</remarks>
        internal void InitializeIntervalMeanTimestepMap()
        {
            Debug.Assert(this.m_IntervalMeanTimestepMap == null);

            if (!this.m_SummaryTransitionOutputAsIntervalMean)
            {
                return;
            }

            Debug.Assert(this.MinimumTimestep > 0);

            this.m_IntervalMeanTimestepMap = new IntervalMeanTimestepMap(this.MinimumTimestep, this.MaximumTimestep, this.m_TimestepZero, this.m_SummaryTransitionOutputTimesteps);
        }

        /// <summary>
        /// Initializes the state attributes map and collections of active attribute type Ids
        /// </summary>
        /// <remarks></remarks>
        private void InitializeStateAttributes()
        {
            Debug.Assert(this.m_StateAttributeTypeIdsAges == null);
            Debug.Assert(this.m_StateAttributeTypeIdsNoAges == null);

            this.m_StateAttributeTypeIdsAges = new Dictionary<int, bool>();
            this.m_StateAttributeTypeIdsNoAges = new Dictionary<int, bool>();

            StateAttributeValueCollection AgesColl = new StateAttributeValueCollection();
            StateAttributeValueCollection NoAgesColl = new StateAttributeValueCollection();

            foreach (StateAttributeValue attr in this.m_StateAttributeValues)
            {
                if (attr.MinimumAge.HasValue || attr.MaximumAge.HasValue)
                {
                    AgesColl.Add(attr);

                    if (!this.m_StateAttributeTypeIdsAges.ContainsKey(attr.AttributeTypeId))
                    {
                        this.m_StateAttributeTypeIdsAges.Add(attr.AttributeTypeId, true);
                    }
                }
                else
                {
                    NoAgesColl.Add(attr);

                    if (!this.m_StateAttributeTypeIdsNoAges.ContainsKey(attr.AttributeTypeId))
                    {
                        this.m_StateAttributeTypeIdsNoAges.Add(attr.AttributeTypeId, true);
                    }
                }
            }

            Debug.Assert(this.m_StateAttributeValueMapAges == null);
            Debug.Assert(this.m_StateAttributeValueMapNoAges == null);
            Debug.Assert(AgesColl.Count + NoAgesColl.Count == this.m_StateAttributeValues.Count);

            this.m_StateAttributeValueMapAges = new StateAttributeValueMap(this.ResultScenario, AgesColl);
            this.m_StateAttributeValueMapNoAges = new StateAttributeValueMap(this.ResultScenario, NoAgesColl);
        }

        /// <summary>
        /// Initializes the transition attribute map and collection of active attribute type Ids
        /// </summary>
        /// <remarks></remarks>
        private void InitializeTransitionAttributes()
        {
            Debug.Assert(this.m_TransitionAttributeTypeIds == null);
            this.m_TransitionAttributeTypeIds = new Dictionary<int, bool>();

            foreach (TransitionAttributeValue attr in this.m_TransitionAttributeValues)
            {
                if (!this.m_TransitionAttributeTypeIds.ContainsKey(attr.AttributeTypeId))
                {
                    this.m_TransitionAttributeTypeIds.Add(attr.AttributeTypeId, true);
                }
            }

            Debug.Assert(this.m_TransitionAttributeValueMap == null);
            this.m_TransitionAttributeValueMap = new TransitionAttributeValueMap(this.ResultScenario, this.m_TransitionAttributeValues);
        }

        /// <summary>
        /// Initializes a separate list of transition groups that can be shuffled
        /// </summary>
        /// <remarks>
        /// The main list is keyed and cannot be shuffled, but we need a shuffled list for doing raster simulations
        /// </remarks>
        private void InitializeShufflableTransitionGroups()
        {
            Debug.Assert(this.m_ShufflableTransitionGroups.Count == 0);

            foreach (TransitionGroup tg in this.m_TransitionGroups)
            {
                this.m_ShufflableTransitionGroups.Add(tg);
            }
        }

        /// <summary>
        /// Assigns Target Prioritizations to each target
        /// </summary>
        private void InitializeTransitionTargetPrioritizations()
        {
            if (this.m_TransitionTargets.Count == 0 || this.m_TransitionTargetPrioritizations.Count == 0)
            {
                return;
            }

            foreach (TransitionTarget t in this.m_TransitionTargets)
            {
                if (!t.IsDisabled)
                {
                    List<TransitionTargetPrioritization> l = 
                        this.m_TransitionTargetPrioritizationKeyMap.GetPrioritizationList(t.TransitionGroupId);

                    if (l != null)
                    {
                        t.SetPrioritizations(l);
                    }
                }
            }
        }

        /// <summary>
        /// Assigns Target Prioritizations to each attribute target
        /// </summary>
        private void InitializeTransitionAttributeTargetPrioritizations()
        {
            if (this.m_TransitionAttributeTargets.Count == 0 || this.m_TransitionAttributeTargetPrioritizations.Count == 0)
            {
                return;
            }

            foreach (TransitionAttributeTarget t in this.m_TransitionAttributeTargets)
            {
                if (!t.IsDisabled)
                {
                    List<TransitionAttributeTargetPrioritization> l = 
                        this.m_TransitionAttributeTargetPrioritizationKeyMap.GetPrioritizationList(t.TransitionAttributeTypeId);

                    if (l != null)
                    {
                        t.SetPrioritizations(l);
                    }
                }
            }
        }

        /// <summary>
        /// Calculates the sum of the Non Spatial Initial Conditions relative amount
        /// </summary>
        /// <remarks></remarks>
        private double CalcSumOfRelativeAmount(int? iteration)
        {
            double sumOfRelativeAmount = 0.0;

            InitialConditionsDistributionCollection icds = this.m_InitialConditionsDistributionMap.GetICDs(iteration);

            foreach (InitialConditionsDistribution sis in icds)
            {
                sumOfRelativeAmount += sis.RelativeAmount;
            }

            if (sumOfRelativeAmount <= 0.0)
            {
                ExceptionUtils.ThrowArgumentException("The sum of the relative amount cannot be zero.");
            }

            return sumOfRelativeAmount;
        }
    }
}
