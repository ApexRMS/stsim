// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal static class Strings
    {
        //General
        public const string AGE_VALIDATION_TABLE_NAME = "stsim_AgeClass";
        public const string STOCHASTIC_TIME_CHART_ANALYZER_TABLE_DATA_KEY = "StochasticTimeChartTableData";
        public const string CLIPBOARD_FORMAT_TRANSITION_DIAGRAM = "DataSheetTransitionDiagram";

        //Data Store Names
        public const string SQLITE_DATASTORE_NAME = "SQLite";

        //All Strata Diagram Tab
        public const string DIAGRAM_ALL_STRATA_DISPLAY_NAME = "All Strata";

        //Report names
        public const string STATECLASS_SUMMARY_REPORT_NAME = "stateclass-summary";
        public const string TRANSITION_SUMMARY_REPORT_NAME = "transition-summary";
        public const string TRANSITION_STATECLASS_SUMMARY_REPORT_NAME = "transition-stateclass-summary";
        public const string STATE_ATTRIBUTE_REPORT_NAME = "state-attributes";
        public const string TRANSITION_ATTRIBUTE_REPORT_NAME = "transition-attributes";

        //Commands
        public const string COMMAND_STRING_OPEN = "Open";
        public const string COMMAND_STRING_CUT = "Cut";
        public const string COMMAND_STRING_COPY = "Copy";
        public const string COMMAND_STRING_PASTE = "Paste";
        public const string COMMAND_STRING_PASTE_SPECIAL = "Paste Special...";
        public const string COMMAND_STRING_SELECT_ALL = "Select All";
        public const string COMMAND_STRING_DELETE = "Delete";
        public const string COMMAND_STRING_SHOW_GRID = "Show Grid";
        public const string COMMAND_STRING_SHOW_TOOLTIPS = "Show Tooltips";
        public const string COMMAND_STRING_ADD_STATECLASS = "Add State Class...";
        public const string COMMAND_STRING_EDIT_STATECLASS = "Edit State Class...";
        public const string COMMAND_STRING_FILTER_TRANSITIONS = "Filter Transitions...";
        public const string COMMAND_STRING_SELECT_STRATUM = "Select Stratum...";
        public const string COMMAND_STRING_SEPARATOR = "separator";

        //Columns
        public const string DATASHEET_NAME_COLUMN_NAME = "Name";
        public const string DATASHEET_MAPID_COLUMN_NAME = "ID";
        public const string DATASHEET_DESCRIPTION_COLUMN_NAME = "Description";
        public const string DATASHEET_SCENARIOID_COLUMN_NAME = "ScenarioID";
        public const string DATASHEET_STRATUM_ID_COLUMN_NAME = "StratumID";
        public const string DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME = "SecondaryStratumID";
        public const string DATASHEET_TERTIARY_STRATUM_ID_COLUMN_NAME = "TertiaryStratumID";
        public const string DATASHEET_ITERATION_COLUMN_NAME = "Iteration";
        public const string DATASHEET_TIMESTEP_COLUMN_NAME = "Timestep";
        public const string DATASHEET_STATECLASS_ID_COLUMN_NAME = "StateClassID";
        public const string DATASHEET_END_STATECLASS_ID_COLUMN_NAME = "EndStateClassID";
        public const string DATASHEET_TRANSITION_TYPE_ID_COLUMN_NAME = "TransitionTypeID";
        public const string DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME = "TransitionGroupID";
        public const string DATASHEET_TRANSITION_MULTIPLIER_TYPE_ID_COLUMN_NAME = "TransitionMultiplierTypeID";
        public const string DATASHEET_STATE_ATTRIBUTE_TYPE_ID_COLUMN_NAME = "StateAttributeTypeID";
        public const string DATASHEET_TRANSITION_ATTRIBUTE_TYPE_ID_COLUMN_NAME = "TransitionAttributeTypeID";
        public const string DATASHEET_AGE_MIN_COLUMN_NAME = "AgeMin";
        public const string DATASHEET_AGE_MAX_COLUMN_NAME = "AgeMax";
        public const string DATASHEET_AGE_CLASS_COLUMN_NAME = "AgeClass";
        public const string DATASHEET_EVENT_ID_COLUMN_NAME = "EventID";
        public const string DATASHEET_SIZE_CLASS_ID_COLUMN_NAME = "SizeClassID";
        public const string DATASHEET_COLOR_COLUMN_NAME = "Color";
        public const string DATASHEET_LEGEND_COLUMN_NAME = "Legend";
        public const string DATASHEET_AMOUNT_COLUMN_NAME = "Amount";
        public const string DATASHEET_ATTRIBUTE_GROUP_ID_COLUMN_NAME = "AttributeGroupID";
        public const string DATASHEET_DISTRIBUTIONTYPE_COLUMN_NAME = "DistributionType";
        public const string DATASHEET_DISTRIBUTION_FREQUENCY_COLUMN_NAME = "DistributionFrequencyID";
        public const string DATASHEET_DISTRIBUTIONSD_COLUMN_NAME = "DistributionSD";
        public const string DATASHEET_DISTRIBUTIONMIN_COLUMN_NAME = "DistributionMin";
        public const string DATASHEET_DISTRIBUTIONMAX_COLUMN_NAME = "DistributionMax";
        public const string DATASHEET_SUMOFAMOUNT_COLUMN_NAME = "SumOfAmount";
        public const string VALUE_MEMBER_COLUMN_NAME = "Value";
        public const string DISPLAY_MEMBER_COLUMN_NAME = "Display";
        public const string IS_AUTO_COLUMN_NAME = "IsAuto";
        public const string AUTO_COLUMN_SUFFIX = "[Type]";

        //State Label X
        public const string DATASHEET_STATE_LABEL_X_NAME = "stsim_StateLabelX";

        //State label Y
        public const string DATASHEET_STATE_LABEL_Y_NAME = "stsim_StateLabelY";

        //Stratum
        public const string DATASHEET_STRATA_NAME = "stsim_Stratum";

        //Secondary Stratum
        public const string DATASHEET_SECONDARY_STRATA_NAME = "stsim_SecondaryStratum";

        //Tertiary Stratum
        public const string DATASHEET_TERTIARY_STRATA_NAME = "stsim_TertiaryStratum";

        //State class
        public const string DATASHEET_STATECLASS_NAME = "stsim_StateClass";
        public const string DATASHEET_STATECLASS_STATE_LABEL_X_ID_COLUMN_NAME = "StateLabelXID";
        public const string DATASHEET_STATECLASS_STATE_LABEL_Y_ID_COLUMN_NAME = "StateLabelYID";
        public const string DATASHEET_IS_AUTO_NAME_COLUMN_NAME = "IsAutoName";

        //Transition type
        public const string DATASHEET_TRANSITION_TYPE_NAME = "stsim_TransitionType";

        //Transition group
        public const string DATASHEET_TRANSITION_GROUP_NAME = "stsim_TransitionGroup";

        //Transition Simulation Group
        public const string DATASHEET_TRANSITION_SIMULATION_GROUP_NAME = "stsim_TransitionSimulationGroup";

        //Transition type group
        public const string DATASHEET_TRANSITION_TYPE_GROUP_NAME = "stsim_TransitionTypeGroup";
        public const string DATASHEET_TRANSITION_TYPE_GROUP_TYPE_COLUMN_NAME = "TransitionTypeID";
        public const string DATASHEET_TRANSITION_TYPE_GROUP_GROUP_COLUMN_NAME = "TransitionGroupID";

        //Age Type
        public const string DATASHEET_AGE_TYPE_NAME = "stsim_AgeType";
        public const string DATASHEET_AGE_TYPE_FREQUENCY_COLUMN_NAME = "Frequency";
        public const string DATASHEET_AGE_TYPE_MAXIMUM_COLUMN_NAME = "MaximumAge";

        //Age Group
        public const string DATASHEET_AGE_GROUP_NAME = "stsim_AgeGroup";
        public const string DATASHEET_AGE_GROUP_MAXIMUM_COLUMN_NAME = "MaximumAge";

        //Size Class
        public const string DATASHEET_SIZE_CLASS_NAME = "stsim_SizeClass";
        public const string DATASHEET_SIZE_CLASS_MAXIMUM_SIZE_COLUMN_NAME = "MaximumSize";

        //Transition multiplier type
        public const string DATASHEET_TRANSITION_MULTIPLIER_TYPE_NAME = "stsim_TransitionMultiplierType";

        //Patch Prioritization
        public const string DATASHEET_PATCH_PRIORITIZATION_NAME = "stsim_PatchPrioritization";
        public const string PATCH_PRIORITIZATION_SMALLEST = "Smallest";
        public const string PATCH_PRIORITIZATION_SMALLEST_EDGES_ONLY = "Smallest (transition edges only)";
        public const string PATCH_PRIORITIZATION_LARGEST = "Largest";
        public const string PATCH_PRIORITIZATION_LARGEST_EDGES_ONLY = "Largest (transition edges only)";

        //Attribute Group
        public const string DATASHEET_ATTRIBUTE_GROUP_NAME = "stsim_AttributeGroup";

        //State Attribute Type
        public const string DATASHEET_STATE_ATTRIBUTE_TYPE_NAME = "stsim_StateAttributeType";
        public const string DATASHEET_STATE_ATTRIBUTE_TYPE_GROUP_COLUMN_NAME = "AttributeGroupID";
        public const string DATASHEET_STATE_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME = "Units";

        //Transition Attribute Type
        public const string DATASHEET_TRANSITION_ATTRIBUTE_TYPE_NAME = "stsim_TransitionAttributeType";
        public const string DATASHEET_TRANSITION_ATTRIBUTE_TYPE_GROUP_COLUMN_NAME = "AttributeGroupID";
        public const string DATASHEET_TRANSITION_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME = "Units";

        //Run control
        public const string DATASHEET_RUN_CONTROL_NAME = "stsim_RunControl";
        public const string RUN_CONTROL_MIN_ITERATION_COLUMN_NAME = "MinimumIteration";
        public const string RUN_CONTROL_MAX_ITERATION_COLUMN_NAME = "MaximumIteration";
        public const string RUN_CONTROL_MIN_TIMESTEP_COLUMN_NAME = "MinimumTimestep";
        public const string RUN_CONTROL_MAX_TIMESTEP_COLUMN_NAME = "MaximumTimestep";
        public const string RUN_CONTROL_IS_SPATIAL_COLUMN_NAME = "IsSpatial";

        //Non-Spatial Initial conditions
        public const string DATASHEET_NSIC_NAME = "stsim_InitialConditionsNonSpatial";
        public const string DATASHEET_NSIC_TOTAL_AMOUNT_COLUMN_NAME = "TotalAmount";
        public const string DATASHEET_NSIC_NUM_CELLS_COLUMN_NAME = "NumCells";
        public const string DATASHEET_NSIC_CALC_FROM_DIST_COLUMN_NAME = "CalcFromDist";
        public const string DATASHEET_NSIC_DISTRIBUTION_NAME = "stsim_InitialConditionsNonSpatialDistribution";
        public const string DATASHEET_NSIC_DISTRIBUTION_DISPLAY_NAME = "Initial Conditions Distribution";
        public const string DATASHEET_NSIC_DISTRIBUTION_RELATIVE_AMOUNT_COLUMN_NAME = "RelativeAmount";

        //Spatial Initial Conditions Properties
        public const string DATASHEET_SPPIC_NAME = "stsim_InitialConditionsSpatialProperties";
        public const string DATASHEET_SPPIC_NUM_COLUMNS_COLUMN_NAME = "NumColumns";
        public const string DATASHEET_SPPIC_NUM_ROWS_COLUMN_NAME = "NumRows";
        public const string DATASHEET_SPPIC_NUM_CELLS_COLUMN_NAME = "NumCells";
        public const string DATASHEET_SPPIC_XLLCORNER_COLUMN_NAME = "XLLCorner";
        public const string DATASHEET_SPPIC_YLLCORNER_COLUMN_NAME = "YLLCorner";
        public const string DATASHEET_SPPIC_CELL_SIZE_COLUMN_NAME = "CellSize";
        public const string DATASHEET_SPPIC_CELL_SIZE_UNITS_COLUMN_NAME = "CellSizeUnits";
        public const string DATASHEET_SPPIC_CELL_AREA_COLUMN_NAME = "CellArea";
        public const string DATASHEET_SPPIC_CELL_AREA_OVERRIDE_COLUMN_NAME = "CellAreaOverride";
        public const string DATASHEET_SPPIC_SRS_COLUMN_NAME = "SRS";

        //Spatial Initial Conditions Files
        public const string DATASHEET_SPIC_NAME = "stsim_InitialConditionsSpatial";
        public const string DATASHEET_SPIC_STRATUM_FILE_COLUMN_NAME = "StratumFileName";
        public const string DATASHEET_SPIC_SECONDARY_STRATUM_FILE_COLUMN_NAME = "SecondaryStratumFileName";
        public const string DATASHEET_SPIC_TERTIARY_STRATUM_FILE_COLUMN_NAME = "TertiaryStratumFileName";
        public const string DATASHEET_SPIC_STATE_CLASS_FILE_COLUMN_NAME = "StateClassFileName";
        public const string DATASHEET_SPIC_AGE_FILE_COLUMN_NAME = "AgeFileName";

        //Deterministic Transitions
        public const string DATASHEET_DT_NAME = "stsim_DeterministicTransition";
        public const string DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME = "StratumIDSource";
        public const string DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME = "StateClassIDSource";
        public const string DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME = "StratumIDDest";
        public const string DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME = "StateClassIDDest";
        public const string DATASHEET_DT_LOCATION_COLUMN_NAME = "Location";

        //Probabilisitic Transitions
        public const string DATASHEET_PT_NAME = "stsim_Transition";
        public const string DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME = "StratumIDSource";
        public const string DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME = "StateClassIDSource";
        public const string DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME = "StratumIDDest";
        public const string DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME = "StateClassIDDest";
        public const string DATASHEET_PT_TRANSITION_TYPE_COLUMN_NAME = "TransitionTypeID";
        public const string DATASHEET_PT_AGE_RELATIVE_COLUMN_NAME = "AgeRelative";
        public const string DATASHEET_PT_AGE_RESET_COLUMN_NAME = "AgeReset";
        public const string DATASHEET_PT_TST_MIN_COLUMN_NAME = "TSTMin";
        public const string DATASHEET_PT_TST_MAX_COLUMN_NAME = "TSTMax";
        public const string DATASHEET_PT_TST_RELATIVE_COLUMN_NAME = "TSTRelative";
        public const string DATASHEET_PT_PROPORTION_COLUMN_NAME = "Proportion";
        public const string DATASHEET_PT_PROBABILITY_COLUMN_NAME = "Probability";
        public const string DATASHEET_PT_PROBXPROPN_COLUMN_NAME = "ProbXPropn";

        //Transition Target
        public const string DATASHEET_TRANSITION_TARGET_NAME = "stsim_TransitionTarget";

        //Transition Target Prioritization
        public const string DATASHEET_TRANSITION_TARGET_PRIORITIZATION_NAME = "stsim_TransitionTargetPrioritization";
        public const string DATASHEET_TRANSITION_TARGET_PRIORITIZATION_PRIORITY_COLUMN_NAME = "Priority";

        //Transition Attribute Target
        public const string DATASHEET_TRANSITION_ATTRIBUTE_TARGET_NAME = "stsim_TransitionAttributeTarget";

        //Transition Attribute Target Prioritization
        public const string DATASHEET_TRANSITION_ATTRIBUTE_TARGET_PRIORITIZATION_NAME = "stsim_TransitionAttributeTargetPrioritization";
        public const string DATASHEET_TRANSITION_ATTRIBUTE_TARGET_PRIORITIZATION_PRIORITY_COLUMN_NAME = "Priority";

        //Transition Order
        public const string DATASHEET_TRANSITION_ORDER_NAME = "stsim_TransitionOrder";
        public const string DATASHEET_TRANSITION_ORDER_ORDER_COLUMN_NAME = "Order";

        //Time Since Transition Group
        public const string DATASHEET_TST_GROUP_NAME = "stsim_TimeSinceTransitionGroup";

        //Time Since Transition Randomize
        public const string DATASHEET_TST_RANDOMIZE_NAME = "stsim_TimeSinceTransitionRandomize";
        public const string DATASHEET_TST_RANDOMIZE_MIN_INITIAL_TST_COLUMN_NAME = "MinInitialTST";
        public const string DATASHEET_TST_RANDOMIZE_MAX_INITIAL_TST_COLUMN_NAME = "MaxInitialTST";

        //Transition multiplier value
        public const string DATASHEET_TRANSITION_MULTIPLIER_VALUE_NAME = "stsim_TransitionMultiplierValue";
        public const string DATASHEET_TRANSITION_MULTIPLIER_VALUE_TST_GROUP_COLUMN_NAME = "TSTGroupID";
        public const string DATASHEET_TRANSITION_MULTIPLIER_VALUE_TST_MIN_COLUMN_NAME = "TSTMin";
        public const string DATASHEET_TRANSITION_MULTIPLIER_VALUE_TST_MAX_COLUMN_NAME = "TSTMax";
        public const string DATASHEET_TRANSITION_MULTIPLIER_VALUE_MULTIPLIER_TYPE_COLUMN_NAME = "TransitionMultiplierTypeID";

        //Transition spatial multiplier
        public const string DATASHEET_TRANSITION_SPATIAL_MULTIPLIER_NAME = "stsim_TransitionSpatialMultiplier";
        public const string DATASHEET_TRANSITION_SPATIAL_MULTIPLIER_MULTIPLIER_TYPE_COLUMN_NAME = "TransitionMultiplierTypeID";
        public const string DATASHEET_TRANSITION_SPATIAL_MULTIPLIER_FILE_COLUMN_NAME = "MultiplierFilename";

        //Transition spatial initiation multiplier
        public const string DATASHEET_TRANSITION_SPATIAL_INITIATION_MULTIPLIER_NAME = "stsim_TransitionSpatialInitiationMultiplier";
        public const string DATASHEET_TRANSITION_SPATIAL_INITIATION_MULTIPLIER_FILE_COLUMN_NAME = "MultiplierFilename";

        //Transition Size Distribution
        public const string DATASHEET_TRANSITION_SIZE_DISTRIBUTION_NAME = "stsim_TransitionSizeDistribution";
        public const string DATASHEET_TRANSITION_SIZE_DISTRIBUTION_MAXIMUM_AREA_COLUMN_NAME = "MaximumArea";
        public const string DATASHEET_TRANSITION_SIZE_DISTRIBUTION_RELATIVE_AMOUNT_COLUMN_NAME = "RelativeAmount";

        //Transition Spread Distribution
        public const string DATASHEET_TRANSITION_SPREAD_DISTRIBUTION_NAME = "stsim_TransitionSpreadDistribution";
        public const string DATASHEET_TRANSITION_SPREAD_DISTRIBUTION_MAXIMUM_DISTANCE_COLUMN_NAME = "MaximumDistance";
        public const string DATASHEET_TRANSITION_SPREAD_DISTRIBUTION_RELATIVE_AMOUNT_COLUMN_NAME = "RelativeAmount";

        //Transition Direction Multiplier
        public const string DATASHEET_TRANSITION_DIRECTION_MULTIPLER_NAME = "stsim_TransitionDirectionMultiplier";
        public const string DATASHEET_TRANSITION_DIRECTION_MULTIPLER_CARDINAL_DIRECTION_COLUMN_NAME = "CardinalDirection";

        //Transition Slope Multiplier
        public const string DATAFEED_TRANSITION_SLOPE_MULTIPLIER_NAME = "stsim_TransitionSlopeMultiplierDataFeed";
        public const string DATASHEET_DIGITAL_ELEVATION_MODEL_NAME = "stsim_DigitalElevationModel";
        public const string DATASHEET_DIGITAL_ELEVATION_MODEL_FILE_NAME_COLUMN_NAME = "DigitalElevationModelFilename";
        public const string DATASHEET_TRANSITION_SLOPE_MULTIPLIER_NAME = "stsim_TransitionSlopeMultiplier";
        public const string DATASHEET_TRANSITION_SLOPE_MULTIPLIER_SLOPE_COLUMN_NAME = "Slope";

        //Transition Adjacency Setting

        public const string DATASHEET_TRANSITION_ADJACENCY_SETTING_NAME = "stsim_TransitionAdjacencySetting";
        public const string DATASHEET_TRANSITION_ADJACENCY_SETTING_NBR_COLUMN_NAME = "NeighborhoodRadius";
        public const string DATASHEET_TRANSITION_ADJACENCY_SETTING_UF_COLUMN_NAME = "UpdateFrequency";

        //Transition Adjacency Multiplier
        public const string DATAFEED_TRANSITION_ADJACENCY_MULTIPLIER_NAME = "stsim_TransitionAdjacencyMultiplierDataFeed";
        public const string DATASHEET_TRANSITION_ADJACENCY_MULTIPLIER_NAME = "stsim_TransitionAdjacencyMultiplier";
        public const string DATASHEET_TRANSITION_ADJACENCY_MULTIPLIER_ATTRIBUTE_VALUE_COLUMN_NAME = "AttributeValue";

        //Transition Patch Prioritization
        public const string DATASHEET_TRANSITION_PATCH_PRIORITIZATION_NAME = "stsim_TransitionPatchPrioritization";
        public const string DATASHEET_TRANSITION_PATCH_PRIORITIZATION_PP_COLUMN_NAME = "PatchPrioritizationID";

        //Transition Size Prioritization
        public const string DATASHEET_TRANSITION_SIZE_PRIORITIZATION_NAME = "stsim_TransitionSizePrioritization";
        public const string DATASHEET_TRANSITION_SIZE_PRIORITIZATION_PRIORITY_TYPE_COLUMN_NAME = "Priority";
        public const string DATASHEET_TRANSITION_SIZE_PRIORITIZATION_MFDIST_COLUMN_NAME = "MaximizeFidelityToDistribution";
        public const string DATASHEET_TRANSITION_SIZE_PRIORITIZATION_MFAREA_COLUMN_NAME = "MaximizeFidelityToTotalArea";

        //Transition Pathway Auto-Correlation
        public const string DATASHEET_TRANSITION_PATHWAY_AUTO_CORRELATION_NAME = "stsim_TransitionPathwayAutoCorrelation";
        public const string DATASHEET_TRANSITION_PATHWAY_AUTO_CORRELATION_COLUMN_NAME = "AutoCorrelation";
        public const string DATASHEET_TRANSITION_PATHWAY_SPREAD_TO_COLUMN_NAME = "SpreadTo";

        //State Attribute Value
        public const string DATASHEET_STATE_ATTRIBUTE_VALUE_NAME = "stsim_StateAttributeValue";
        public const string DATASHEET_STATE_ATTRIBUTE_VALUE_VALUE_COLUMN_NAME = "Value";

        //Transition Attribute Value
        public const string DATASHEET_TRANSITION_ATTRIBUTE_VALUE_NAME = "stsim_TransitionAttributeValue";
        public const string DATASHEET_TRANSITION_ATTRIBUTE_VALUE_VALUE_COLUMN_NAME = "Value";

        //Terminology
        public const string DATASHEET_TERMINOLOGY_NAME = "stsim_Terminology";
        public const string DATASHEET_TERMINOLOGY_AMOUNT_LABEL_COLUMN_NAME = "AmountLabel";
        public const string DATASHEET_TERMINOLOGY_AMOUNT_UNITS_COLUMN_NAME = "AmountUnits";
        public const string DATASHEET_TERMINOLOGY_STATELABELX_COLUMN_NAME = "StateLabelX";
        public const string DATASHEET_TERMINOLOGY_STATELABELY_COLUMN_NAME = "StateLabelY";
        public const string DATASHEET_TERMINOLOGY_PRIMARY_STRATUM_LABEL_COLUMN_NAME = "PrimaryStratumLabel";
        public const string DATASHEET_TERMINOLOGY_SECONDARY_STRATUM_LABEL_COLUMN_NAME = "SecondaryStratumLabel";
        public const string DATASHEET_TERMINOLOGY_TERTIARY_STRATUM_LABEL_COLUMN_NAME = "TertiaryStratumLabel";

        //Output Options
        public const string DATASHEET_OO_TABULAR_NAME = "stsim_OutputOptions";
        public const string DATASHEET_OO_SUMMARY_OUTPUT_SC_COLUMN_NAME = "SummaryOutputSC";
        public const string DATASHEET_OO_SUMMARY_OUTPUT_SC_TIMESTEPS_COLUMN_NAME = "SummaryOutputSCTimesteps";
        public const string DATASHEET_OO_SUMMARY_OUTPUT_SC_AGES_COLUMN_NAME = "SummaryOutputSCAges";
        public const string DATASHEET_OO_SUMMARY_OUTPUT_SC_ZERO_VALUES_COLUMN_NAME = "SummaryOutputSCZeroValues";
        public const string DATASHEET_OO_SUMMARY_OUTPUT_TR_COLUMN_NAME = "SummaryOutputTR";
        public const string DATASHEET_OO_SUMMARY_OUTPUT_TR_TIMESTEPS_COLUMN_NAME = "SummaryOutputTRTimesteps";
        public const string DATASHEET_OO_SUMMARY_OUTPUT_TR_AGES_COLUMN_NAME = "SummaryOutputTRAges";
        public const string DATASHEET_OO_SUMMARY_OUTPUT_TR_INTERVAL_MEAN_COLUMN_NAME = "SummaryOutputTRIntervalMean";
        public const string DATASHEET_OO_SUMMARY_OUTPUT_TRSC_COLUMN_NAME = "SummaryOutputTRSC";
        public const string DATASHEET_OO_SUMMARY_OUTPUT_TRSC_TIMESTEPS_COLUMN_NAME = "SummaryOutputTRSCTimesteps";
        public const string DATASHEET_OO_SUMMARY_OUTPUT_SA_COLUMN_NAME = "SummaryOutputSA";
        public const string DATASHEET_OO_SUMMARY_OUTPUT_SA_TIMESTEPS_COLUMN_NAME = "SummaryOutputSATimesteps";
        public const string DATASHEET_OO_SUMMARY_OUTPUT_SA_AGES_COLUMN_NAME = "SummaryOutputSAAges";
        public const string DATASHEET_OO_SUMMARY_OUTPUT_TA_COLUMN_NAME = "SummaryOutputTA";
        public const string DATASHEET_OO_SUMMARY_OUTPUT_TA_TIMESTEPS_COLUMN_NAME = "SummaryOutputTATimesteps";
        public const string DATASHEET_OO_SUMMARY_OUTPUT_TA_AGES_COLUMN_NAME = "SummaryOutputTAAges";
        public const string DATASHEET_OO_SUMMARY_OUTPUT_OMIT_SS_COLUMN_NAME = "SummaryOutputOmitSS";
        public const string DATASHEET_OO_SUMMARY_OUTPUT_OMIT_TS_COLUMN_NAME = "SummaryOutputOmitTS";

        //Output Options Spatial
        public const string DATASHEET_OO_SPATIAL_NAME = "stsim_OutputOptionsSpatial";
        public const string DATASHEET_OO_SPATIAL_RASTER_OUTPUT_SC_COLUMN_NAME = "RasterOutputSC";
        public const string DATASHEET_OO_SPATIAL_RASTER_OUTPUT_SC_TIMESTEPS_COLUMN_NAME = "RasterOutputSCTimesteps";
        public const string DATASHEET_OO_SPATIAL_RASTER_OUTPUT_AGE_COLUMN_NAME = "RasterOutputAge";
        public const string DATASHEET_OO_SPATIAL_RASTER_OUTPUT_AGE_TIMESTEPS_COLUMN_NAME = "RasterOutputAgeTimesteps";
        public const string DATASHEET_OO_SPATIAL_RASTER_OUTPUT_ST_COLUMN_NAME = "RasterOutputST";
        public const string DATASHEET_OO_SPATIAL_RASTER_OUTPUT_ST_TIMESTEPS_COLUMN_NAME = "RasterOutputSTTimesteps";
        public const string DATASHEET_OO_SPATIAL_RASTER_OUTPUT_TR_COLUMN_NAME = "RasterOutputTR";
        public const string DATASHEET_OO_SPATIAL_RASTER_OUTPUT_TR_TIMESTEPS_COLUMN_NAME = "RasterOutputTRTimesteps";
        public const string DATASHEET_OO_SPATIAL_RASTER_OUTPUT_TE_COLUMN_NAME = "RasterOutputTransitionEvents";
        public const string DATASHEET_OO_SPATIAL_RASTER_OUTPUT_TE_TIMESTEPS_COLUMN_NAME = "RasterOutputTransitionEventTimesteps";
        public const string DATASHEET_OO_SPATIAL_RASTER_OUTPUT_TST_COLUMN_NAME = "RasterOutputTST";
        public const string DATASHEET_OO_SPATIAL_RASTER_OUTPUT_TST_TIMESTEPS_COLUMN_NAME = "RasterOutputTSTTimesteps";
        public const string DATASHEET_OO_SPATIAL_RASTER_OUTPUT_SA_COLUMN_NAME = "RasterOutputSA";
        public const string DATASHEET_OO_SPATIAL_RASTER_OUTPUT_SA_TIMESTEPS_COLUMN_NAME = "RasterOutputSATimesteps";
        public const string DATASHEET_OO_SPATIAL_RASTER_OUTPUT_TA_COLUMN_NAME = "RasterOutputTA";
        public const string DATASHEET_OO_SPATIAL_RASTER_OUTPUT_TA_TIMESTEPS_COLUMN_NAME = "RasterOutputTATimesteps";

        //Output Options Spatial Average
        public const string DATASHEET_OO_SPATIAL_AVERAGE_NAME = "stsim_OutputOptionsSpatialAverage";
        public const string DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_SC_COLUMN_NAME = "AvgRasterOutputSC";
        public const string DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_SC_TIMESTEPS_COLUMN_NAME = "AvgRasterOutputSCTimesteps";
        public const string DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_SC_CUMULATIVE_COLUMN_NAME = "AvgRasterOutputSCCumulative";
        public const string DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_AGE_COLUMN_NAME = "AvgRasterOutputAge";
        public const string DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_AGE_TIMESTEPS_COLUMN_NAME = "AvgRasterOutputAgeTimesteps";
        public const string DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_AGE_CUMULATIVE_COLUMN_NAME = "AvgRasterOutputAgeCumulative";
        public const string DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_ST_COLUMN_NAME = "AvgRasterOutputST";
        public const string DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_ST_TIMESTEPS_COLUMN_NAME = "AvgRasterOutputSTTimesteps";
        public const string DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_ST_CUMULATIVE_COLUMN_NAME = "AvgRasterOutputSTCumulative";
        public const string DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_TP_COLUMN_NAME = "AvgRasterOutputTP";
        public const string DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_TP_TIMESTEPS_COLUMN_NAME = "AvgRasterOutputTPTimesteps";
        public const string DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_TP_CUMULATIVE_COLUMN_NAME = "AvgRasterOutputTPCumulative";
        public const string DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_TST_COLUMN_NAME = "AvgRasterOutputTST";
        public const string DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_TST_TIMESTEPS_COLUMN_NAME = "AvgRasterOutputTSTTimesteps";
        public const string DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_TST_CUMULATIVE_COLUMN_NAME = "AvgRasterOutputTSTCumulative";
        public const string DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_SA_COLUMN_NAME = "AvgRasterOutputSA";
        public const string DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_SA_TIMESTEPS_COLUMN_NAME = "AvgRasterOutputSATimesteps";
        public const string DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_SA_CUMULATIVE_COLUMN_NAME = "AvgRasterOutputSACumulative";
        public const string DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_TA_COLUMN_NAME = "AvgRasterOutputTA";
        public const string DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_TA_TIMESTEPS_COLUMN_NAME = "AvgRasterOutputTATimesteps";
        public const string DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_TA_CUMULATIVE_COLUMN_NAME = "AvgRasterOutputTACumulative";

        //Distribution Type Data Feed
        public const string DISTRIBUTION_TYPE_DATASHEET_NAME = "corestime_DistributionType";
        public const string DISTRIBUTION_TYPE_IS_INTERNAL_COLUMN_NAME = "IsInternal";
        public const string DISTRIBUTION_TYPE_NAME_UNIFORM_INTEGER = "Uniform Integer";

        //Distribution Value Data Feed
        public const string DISTRIBUTION_VALUE_DATASHEET_NAME = "stsim_DistributionValue";
        public const string DISTRIBUTION_VALUE_DIST_TYPE_ID_COLUMN_NAME = "DistributionTypeID";
        public const string DISTRIBUTION_VALUE_EXTVAR_TYPE_ID_COLUMN_NAME = "ExternalVariableTypeID";
        public const string DISTRIBUTION_VALUE_EXTVAR_MIN_COLUMN_NAME = "ExternalVariableMin";
        public const string DISTRIBUTION_VALUE_EXTVAR_MAX_COLUMN_NAME = "ExternalVariableMax";
        public const string DISTRIBUTION_VALUE_DIST_VALUE_COLUMN_NAME = "Value";
        public const string DISTRIBUTION_VALUE_VALUE_DIST_TYPE_ID_COLUMN_NAME = "ValueDistributionTypeID";
        public const string DISTRIBUTION_VALUE_VALUE_DIST_FREQUENCY_COLUMN_NAME = "ValueDistributionFrequency";
        public const string DISTRIBUTION_VALUE_VALUE_DIST_SD_COLUMN_NAME = "ValueDistributionSD";
        public const string DISTRIBUTION_VALUE_VALUE_DIST_MIN_COLUMN_NAME = "ValueDistributionMin";
        public const string DISTRIBUTION_VALUE_VALUE_DIST_MAX_COLUMN_NAME = "ValueDistributionMax";
        public const string DISTRIBUTION_VALUE_VALUE_DIST_RELATIVE_FREQUENCY_COLUMN_NAME = "ValueDistributionRelativeFrequency";

        //Multiprocessing
        public const string DATASHEET_MULTI_PROCESSING_NAME = "stsim_Multiprocessing";
        public const string DATASHEET_MULTI_PROCESSING_SPLIT_BY_SS_COLUMN_NAME = "SplitBySecondaryStrata";

        //Output Stratum Amount
        public const string DATASHEET_OUTPUT_STRATUM_NAME = "stsim_OutputStratum";

        //Output Stratum State
        public const string DATASHEET_OUTPUT_STRATUM_STATE_NAME = "stsim_OutputStratumState";

        //Output Stratum Transition
        public const string DATASHEET_OUTPUT_STRATUM_TRANSITION_NAME = "stsim_OutputStratumTransition";

        //OutputStratumTransitionState
        public const string DATASHEET_OUTPUT_STRATUM_TRANSITION_STATE_NAME = "stsim_OutputStratumTransitionState";

        //OutputStateAttribute
        public const string DATASHEET_OUTPUT_STATE_ATTRIBUTE_NAME = "stsim_OutputStateAttribute";

        //OutputTransitionAttribute
        public const string DATASHEET_OUTPUT_TRANSITION_ATTRIBUTE_NAME = "stsim_OutputTransitionAttribute";

        //Output External Variable Value Data Feed
        public const string OUTPUT_EXTERNAL_VARIABLE_VALUE_DATASHEET_NAME = "stsim_OutputExternalVariableValue";
        public const string OUTPUT_EXTERNAL_VARIABLE_VALUE_TYPE_ID_COLUMN_NAME = "ExternalVariableTypeID";
        public const string OUTPUT_EXTERNAL_VARIABLE_VALUE_VALUE_COLUMN_NAME = "Value";

        public const string CORESTIME_EXTERNAL_VAR_TYPE_DATASHEET_NAME = "corestime_ExternalVariableType";

        //Charting Variables
        public const string STATE_CLASS_AMOUNT_VARIABLE_NAME = "stsim_StateClassNormalVariable";
        public const string STATE_CLASS_PROPORTION_VARIABLE_NAME = "stsim_StateClassProportionVariable";
        public const string TRANSITION_AMOUNT_VARIABLE_NAME = "stsim_TransitionNormalVariable";
        public const string TRANSITION_PROPORTION_VARIABLE_NAME = "stsim_TransitionProportionVariable";
    }
}
