// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Drawing;

namespace SyncroSim.STSim
{
    internal static class Strings
    {
        //General
        public const string AGE_CLASS_VALIDATION_TABLE_NAME = "stsim_AgeClass";
        public const string TST_CLASS_VALIDATION_TABLE_NAME = "stsim_TSTClass";
        public const string STOCHASTIC_TIME_CHART_ANALYZER_TABLE_DATA_KEY = "StochasticTimeChartTableData";
        public const string CLIPBOARD_FORMAT_TRANSITION_DIAGRAM = "DataSheetTransitionDiagram";

        //Data Store Names
        public const string SQLITE_DATASTORE_NAME = "SQLite";

        //All Strata Diagram Tab
        public const string DIAGRAM_ALL_STRATA_DISPLAY_NAME = "All Strata";

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
        public const string DATASHEET_MAPID_COLUMN_NAME = "Id";
        public const string DATASHEET_DESCRIPTION_COLUMN_NAME = "Description";
        public const string DATASHEET_SCENARIOID_COLUMN_NAME = "ScenarioId";
        public const string DATASHEET_STRATUM_ID_COLUMN_NAME = "StratumId";
        public const string DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME = "SecondaryStratumId";
        public const string DATASHEET_TERTIARY_STRATUM_ID_COLUMN_NAME = "TertiaryStratumId";
        public const string DATASHEET_ITERATION_COLUMN_NAME = "Iteration";
        public const string DATASHEET_TIMESTEP_COLUMN_NAME = "Timestep";
        public const string DATASHEET_STATECLASS_ID_COLUMN_NAME = "StateClassId";
        public const string DATASHEET_END_STATECLASS_ID_COLUMN_NAME = "EndStateClassId";
        public const string DATASHEET_TRANSITION_TYPE_ID_COLUMN_NAME = "TransitionTypeId";
        public const string DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME = "TransitionGroupId";
        public const string DATASHEET_TRANSITION_MULTIPLIER_TYPE_ID_COLUMN_NAME = "TransitionMultiplierTypeId";
        public const string DATASHEET_STATE_ATTRIBUTE_TYPE_ID_COLUMN_NAME = "StateAttributeTypeId";
        public const string DATASHEET_TRANSITION_ATTRIBUTE_TYPE_ID_COLUMN_NAME = "TransitionAttributeTypeId";
        public const string DATASHEET_TST_GROUP_ID_COLUMN_NAME = "TSTGroupId";
        public const string DATASHEET_AGE_MIN_COLUMN_NAME = "AgeMin";
        public const string DATASHEET_AGE_MAX_COLUMN_NAME = "AgeMax";
        public const string DATASHEET_AGE_CLASS_COLUMN_NAME = "AgeClass";
        public const string DATASHEET_TST_MIN_COLUMN_NAME = "TSTMin";
        public const string DATASHEET_TST_MAX_COLUMN_NAME = "TSTMax";
        public const string DATASHEET_TST_CLASS_COLUMN_NAME = "TSTClass";
        public const string DATASHEET_EVENT_ID_COLUMN_NAME = "EventId";
        public const string DATASHEET_SIZE_CLASS_ID_COLUMN_NAME = "SizeClassId";
        public const string DATASHEET_COLOR_COLUMN_NAME = "Color";
        public const string DATASHEET_LEGEND_COLUMN_NAME = "Legend";
        public const string DATASHEET_AMOUNT_COLUMN_NAME = "Amount";
        public const string DATASHEET_MULTIPLIER_COLUMN_NAME = "Multiplier";
        public const string DATASHEET_MULTIPLIER_FILE_COLUMN_NAME = "MultiplierFilename";
        public const string DATASHEET_VALUE_COLUMN_NAME = "Value";
        public const string DATASHEET_ATTRIBUTE_GROUP_ID_COLUMN_NAME = "AttributeGroupId";
        public const string DATASHEET_DISTRIBUTIONTYPE_COLUMN_NAME = "DistributionType";
        public const string DATASHEET_DISTRIBUTION_FREQUENCY_COLUMN_NAME = "DistributionFrequencyId";
        public const string DATASHEET_DISTRIBUTIONSD_COLUMN_NAME = "DistributionSD";
        public const string DATASHEET_DISTRIBUTIONMIN_COLUMN_NAME = "DistributionMin";
        public const string DATASHEET_DISTRIBUTIONMAX_COLUMN_NAME = "DistributionMax";
        public const string DATASHEET_SUMOFAMOUNT_COLUMN_NAME = "SumOfAmount";
        public const string DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN = "Filename";
        public const string VALUE_MEMBER_COLUMN_NAME = "Value";
        public const string DISPLAY_MEMBER_COLUMN_NAME = "Display";
        public const string IS_AUTO_COLUMN_NAME = "IsAuto";
        public const string AUTO_COLUMN_SUFFIX = "[Type]";

        public const string STOCK_VALUE_COLUMN_NAME = "StockValue";
        public const string STOCK_MIN_COLUMN_NAME = "StockMinimum";
        public const string STOCK_MAX_COLUMN_NAME = "StockMaximum";
        public const string STOCK_TYPE_ID_COLUMN_NAME = "StockTypeId";
        public const string STOCK_GROUP_ID_COLUMN_NAME = "StockGroupId";
        public const string FLOW_TYPE_ID_COLUMN_NAME = "FlowTypeId";
        public const string FLOW_GROUP_ID_COLUMN_NAME = "FlowGroupId";
        public const string FLOW_MULTIPLIER_TYPE_ID_COLUMN_NAME = "FlowMultiplierTypeId";
        public const string FROM_STRATUM_ID_COLUMN_NAME = "FromStratumId";
        public const string FROM_SECONDARY_STRATUM_ID_COLUMN_NAME = "FromSecondaryStratumId";
        public const string FROM_TERTIARY_STRATUM_ID_COLUMN_NAME = "FromTertiaryStratumId";
        public const string FROM_STATECLASS_ID_COLUMN_NAME = "FromStateClassId";
        public const string FROM_MIN_AGE_COLUMN_NAME = "FromAgeMin";
        public const string FROM_STOCK_TYPE_ID_COLUMN_NAME = "FromStockTypeId";
        public const string TO_STRATUM_ID_COLUMN_NAME = "ToStratumId";
        public const string TO_STATECLASS_ID_COLUMN_NAME = "ToStateClassId";
        public const string TO_MIN_AGE_COLUMN_NAME = "ToAgeMin";
        public const string TO_STOCK_TYPE_ID_COLUMN_NAME = "ToStockTypeId";
        public const string TARGET_TYPE = "TargetType";

        public const string TRANSFER_TO_STRATUM_ID_COLUMN_NAME = "TransferToStratumId";
        public const string TRANSFER_TO_SECONDARY_STRATUM_ID_COLUMN_NAME = "TransferToSecondaryStratumId";
        public const string TRANSFER_TO_TERTIARY_STRATUM_ID_COLUMN_NAME = "TransferToTertiaryStratumId";
        public const string TRANSFER_TO_STATECLASS_ID_COLUMN_NAME = "TransferToStateClassId";
        public const string TRANSFER_TO_MIN_AGE_COLUMN_NAME = "TransferToAgeMin";

        public const string END_STRATUM_ID_COLUMN_NAME = "EndStratumId";
        public const string END_SECONDARY_STRATUM_ID_COLUMN_NAME = "EndSecondaryStratumId";
        public const string END_TERTIARY_STRATUM_ID_COLUMN_NAME = "EndTertiaryStratumId";
        public const string END_STATECLASS_ID_COLUMN_NAME = "EndStateClassId";
        public const string END_MIN_AGE_COLUMN_NAME = "EndMinAge";

        public const string OUTPUT_SUMMARY_COLUMN_NAME = "Summary";
        public const string OUTPUT_SPATIAL_COLUMN_NAME = "Spatial";
        public const string OUTPUT_AVG_SPATIAL_COLUMN_NAME = "AvgSpatial";

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
        public const string DATASHEET_STATECLASS_STATE_LABEL_X_ID_COLUMN_NAME = "StateLabelXId";
        public const string DATASHEET_STATECLASS_STATE_LABEL_Y_ID_COLUMN_NAME = "StateLabelYId";
        public const string DATASHEET_IS_AUTO_NAME_COLUMN_NAME = "IsAutoName";

        //Transition type
        public const string DATASHEET_TRANSITION_TYPE_NAME = "stsim_TransitionType";

        //Transition group
        public const string DATASHEET_TRANSITION_GROUP_NAME = "stsim_TransitionGroup";

        //Transition Simulation Group
        public const string DATASHEET_TRANSITION_SIMULATION_GROUP_NAME = "stsim_TransitionSimulationGroup";

        //Transition type group
        public const string DATASHEET_TRANSITION_TYPE_GROUP_NAME = "stsim_TransitionTypeGroup";
        public const string DATASHEET_TRANSITION_TYPE_GROUP_TYPE_COLUMN_NAME = "TransitionTypeId";
        public const string DATASHEET_TRANSITION_TYPE_GROUP_GROUP_COLUMN_NAME = "TransitionGroupId";

        //Age Type
        public const string DATASHEET_AGE_TYPE_NAME = "stsim_AgeType";
        public const string DATASHEET_AGE_TYPE_FREQUENCY_COLUMN_NAME = "Frequency";
        public const string DATASHEET_AGE_TYPE_MAXIMUM_COLUMN_NAME = "MaximumAge";

        //Age Group
        public const string DATASHEET_AGE_GROUP_NAME = "stsim_AgeGroup";
        public const string DATASHEET_AGE_GROUP_MAXIMUM_COLUMN_NAME = "MaximumAge";

        //TST Type
        public const string DATASHEET_TST_TYPE_NAME = "stsim_TSTType";
        public const string DATASHEET_TST_TYPE_FREQUENCY_COLUMN_NAME = "Frequency";
        public const string DATASHEET_TST_TYPE_MAXIMUM_COLUMN_NAME = "MaximumTST";

        //TST Group
        public const string DATASHEET_TST_GROUP_NAME = "stsim_TSTGroup";
        public const string DATASHEET_TST_GROUP_MAXIMUM_COLUMN_NAME = "MaximumTST";

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
        public const string DATASHEET_STATE_ATTRIBUTE_TYPE_GROUP_COLUMN_NAME = "AttributeGroupId";
        public const string DATASHEET_STATE_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME = "Units";

        //Transition Attribute Type
        public const string DATASHEET_TRANSITION_ATTRIBUTE_TYPE_NAME = "stsim_TransitionAttributeType";
        public const string DATASHEET_TRANSITION_ATTRIBUTE_TYPE_GROUP_COLUMN_NAME = "AttributeGroupId";
        public const string DATASHEET_TRANSITION_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME = "Units";

        //Run control
        public const string DATASHEET_RUN_CONTROL_NAME = "stsim_RunControl";
        public const string RUN_CONTROL_MIN_ITERATION_COLUMN_NAME = "MinimumIteration";
        public const string RUN_CONTROL_MAX_ITERATION_COLUMN_NAME = "MaximumIteration";
        public const string RUN_CONTROL_MIN_TIMESTEP_COLUMN_NAME = "MinimumTimestep";
        public const string RUN_CONTROL_MAX_TIMESTEP_COLUMN_NAME = "MaximumTimestep";
        public const string RUN_CONTROL_IS_SPATIAL_COLUMN_NAME = "IsSpatial";

        //Non-Spatial Initial conditions properties
        public const string DATASHEET_NSIC_NAME = "stsim_InitialConditionsNonSpatialProperties";
        public const string DATASHEET_NSIC_TOTAL_AMOUNT_COLUMN_NAME = "TotalAmount";
        public const string DATASHEET_NSIC_NUM_CELLS_COLUMN_NAME = "NumCells";
        public const string DATASHEET_NSIC_CALC_FROM_DIST_COLUMN_NAME = "CalcFromDist";

        //Non-Spatial Initial conditions distribution
        public const string DATASHEET_NSIC_DISTRIBUTION_NAME = "stsim_InitialConditionsNonSpatialDistribution";
        public const string DATASHEET_NSIC_DISTRIBUTION_DISPLAY_NAME = "Initial Conditions Distribution";
        public const string DATASHEET_NSIC_DISTRIBUTION_RELATIVE_AMOUNT_COLUMN_NAME = "RelativeAmount";

        //Spatial Initial Conditions Properties
        public const string DATASHEET_SPPIC_NAME = "stsim_InitialConditionsSpatialProperty";
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

        //Spatial Initial Conditions Raster
        public const string DATASHEET_SPIC_NAME = "stsim_InitialConditionsSpatialRaster";
        public const string DATASHEET_SPIC_STRATUM_FILE_COLUMN_NAME = "StratumFileName";
        public const string DATASHEET_SPIC_SECONDARY_STRATUM_FILE_COLUMN_NAME = "SecondaryStratumFileName";
        public const string DATASHEET_SPIC_TERTIARY_STRATUM_FILE_COLUMN_NAME = "TertiaryStratumFileName";
        public const string DATASHEET_SPIC_STATE_CLASS_FILE_COLUMN_NAME = "StateClassFileName";
        public const string DATASHEET_SPIC_AGE_FILE_COLUMN_NAME = "AgeFileName";

        //Deterministic Transitions
        public const string DATASHEET_DT_NAME = "stsim_DeterministicTransition";
        public const string DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME = "StratumIdSource";
        public const string DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME = "StateClassIdSource";
        public const string DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME = "StratumIdDest";
        public const string DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME = "StateClassIdDest";
        public const string DATASHEET_DT_LOCATION_COLUMN_NAME = "Location";

        //Probabilisitic Transitions
        public const string DATASHEET_PT_NAME = "stsim_Transition";
        public const string DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME = "StratumIdSource";
        public const string DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME = "StateClassIdSource";
        public const string DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME = "StratumIdDest";
        public const string DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME = "StateClassIdDest";
        public const string DATASHEET_PT_TRANSITION_TYPE_COLUMN_NAME = "TransitionTypeId";
        public const string DATASHEET_PT_AGE_RELATIVE_COLUMN_NAME = "AgeRelative";
        public const string DATASHEET_PT_AGE_RESET_COLUMN_NAME = "AgeReset";
        public const string DATASHEET_PT_TST_MIN_COLUMN_NAME = "TSTMin";
        public const string DATASHEET_PT_TST_MAX_COLUMN_NAME = "TSTMax";
        public const string DATASHEET_PT_TST_RELATIVE_COLUMN_NAME = "TSTRelative";
        public const string DATASHEET_PT_PROPORTION_COLUMN_NAME = "Proportion";
        public const string DATASHEET_PT_PROBABILITY_COLUMN_NAME = "Probability";
        public const string DATASHEET_PT_PROBXPROPN_COLUMN_NAME = "ProbXPropn";

        //Transition Group Output Filters
        public const string DATASHEET_OUTPUT_FILTER_TRANSITION_GROUPS = "stsim_OutputFilterTransitionGroups";
        public const string DATASHEET_OUTPUT_FILTER_TRANSITION_GROUPS_SUMMARY_COLUMN_NAME = "Summary";
        public const string DATASHEET_OUTPUT_FILTER_TRANSITION_GROUPS_SUMMARY_BY_STATE_CLASS_COLUMN_NAME = "SummaryByStateClass";
        public const string DATASHEET_OUTPUT_FILTER_TRANSITION_GROUPS_TST_COLUMN_NAME = "TimeSinceTransition";
        public const string DATASHEET_OUTPUT_FILTER_TRANSITION_GROUPS_SPATIAL_COLUMN_NAME = "Spatial";
        public const string DATASHEET_OUTPUT_FILTER_TRANSITION_GROUPS_SPATIAL_EVENTS_COLUMN_NAME = "SpatialEvents";
        public const string DATASHEET_OUTPUT_FILTER_TRANSITION_GROUPS_SPATIAL_TST_COLUMN_NAME = "SpatialTimeSinceTransition";
        public const string DATASHEET_OUTPUT_FILTER_TRANSITION_GROUPS_SPATIAL_PROB_COLUMN_NAME = "SpatialProbability";
        public const string DATASHEET_OUTPUT_FILTER_TRANSITION_GROUPS_AVG_SPATIAL_TST_COLUMN_NAME = "AvgSpatialTimeSinceTransition";

        //State Attribute Output Filters
        public const string DATASHEET_OUTPUT_FILTER_STATE_ATTRIBUTES = "stsim_OutputFilterStateAttributes";
        public const string DATASHEET_OUTPUT_FILTER_STATE_ATTRIBUTES_SUMMARY_COLUMN_NAME = "Summary";
        public const string DATASHEET_OUTPUT_FILTER_STATE_ATTRIBUTES_SPATIAL_COLUMN_NAME = "Spatial";
        public const string DATASHEET_OUTPUT_FILTER_STATE_ATTRIBUTES_AVG_SPATIAL_COLUMN_NAME = "AvgSpatial";

        //Transition Attribute Output Filters
        public const string DATASHEET_OUTPUT_FILTER_TRANSITION_ATTRIBUTES = "stsim_OutputFilterTransitionAttributes";
        public const string DATASHEET_OUTPUT_FILTER_TRANSITION_ATTRIBUTES_SUMMARY_COLUMN_NAME = "Summary";
        public const string DATASHEET_OUTPUT_FILTER_TRANSITION_ATTRIBUTES_SPATIAL_COLUMN_NAME = "Spatial";
        public const string DATASHEET_OUTPUT_FILTER_TRANSITION_ATTRIBUTES_AVG_SPATIAL_COLUMN_NAME = "AvgSpatial";

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
        public const string DATASHEET_TST_GROUP_VALUE_NAME = "stsim_TimeSinceTransitionGroup";

        //Time Since Transition Randomize
        public const string DATASHEET_TST_RANDOMIZE_NAME = "stsim_TimeSinceTransitionRandomize";
        public const string DATASHEET_TST_RANDOMIZE_MIN_INITIAL_TST_COLUMN_NAME = "MinInitialTST";
        public const string DATASHEET_TST_RANDOMIZE_MAX_INITIAL_TST_COLUMN_NAME = "MaxInitialTST";

        //Initial TST Spatial
        public const string DATASHEET_INITIAL_TST_SPATIAL_NAME = "stsim_InitialTSTSpatial";
        public const string DATASHEET_INITIAL_TST_SPATIAL_FILE_COLUMN_NAME = "TSTFileName";

        //Transition multiplier value
        public const string DATASHEET_TRANSITION_MULTIPLIER_VALUE_NAME = "stsim_TransitionMultiplierValue";
        public const string DATASHEET_TRANSITION_MULTIPLIER_VALUE_TST_GROUP_COLUMN_NAME = "TSTGroupId";
        public const string DATASHEET_TRANSITION_MULTIPLIER_VALUE_TST_MIN_COLUMN_NAME = "TSTMin";
        public const string DATASHEET_TRANSITION_MULTIPLIER_VALUE_TST_MAX_COLUMN_NAME = "TSTMax";
        public const string DATASHEET_TRANSITION_MULTIPLIER_VALUE_MULTIPLIER_TYPE_COLUMN_NAME = "TransitionMultiplierTypeId";

        //Transition spatial multiplier
        public const string DATASHEET_TRANSITION_SPATIAL_MULTIPLIER_NAME = "stsim_TransitionSpatialMultiplier";
        public const string DATASHEET_TRANSITION_SPATIAL_MULTIPLIER_MULTIPLIER_TYPE_COLUMN_NAME = "TransitionMultiplierTypeId";
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
        public const string DATASHEET_TRANSITION_PATCH_PRIORITIZATION_PP_COLUMN_NAME = "PatchPrioritizationId";

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
        public const string DATASHEET_OO_SUMMARY_OUTPUT_EV_COLUMN_NAME = "SummaryOutputEV";
        public const string DATASHEET_OO_SUMMARY_OUTPUT_EV_TIMESTEPS_COLUMN_NAME = "SummaryOutputEVTimesteps";
        public const string DATASHEET_OO_SUMMARY_OUTPUT_TST_COLUMN_NAME = "SummaryOutputTST";
        public const string DATASHEET_OO_SUMMARY_OUTPUT_TST_TIMESTEPS_COLUMN_NAME = "SummaryOutputTSTTimesteps";
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
        public const string DISTRIBUTION_TYPE_DATASHEET_NAME = "core_DistributionType";
        public const string DISTRIBUTION_TYPE_IS_INTERNAL_COLUMN_NAME = "IsInternal";
        public const string DISTRIBUTION_TYPE_NAME_UNIFORM_INTEGER = "Uniform Integer";

        //Distribution Value Data Feed
        public const string DISTRIBUTION_VALUE_DATASHEET_NAME = "stsim_DistributionValue";
        public const string DISTRIBUTION_VALUE_DIST_TYPE_ID_COLUMN_NAME = "DistributionTypeId";
        public const string DISTRIBUTION_VALUE_EXTVAR_TYPE_ID_COLUMN_NAME = "ExternalVariableTypeId";
        public const string DISTRIBUTION_VALUE_EXTVAR_MIN_COLUMN_NAME = "ExternalVariableMin";
        public const string DISTRIBUTION_VALUE_EXTVAR_MAX_COLUMN_NAME = "ExternalVariableMax";
        public const string DISTRIBUTION_VALUE_DIST_VALUE_COLUMN_NAME = "Value";
        public const string DISTRIBUTION_VALUE_VALUE_DIST_TYPE_ID_COLUMN_NAME = "ValueDistributionTypeId";
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

        //OutputTST
        public const string DATASHEET_OUTPUT_TST_NAME = "stsim_OutputTST";

        //Output External Variable Value Data Feed
        public const string OUTPUT_EXTERNAL_VARIABLE_VALUE_DATASHEET_NAME = "stsim_OutputExternalVariableValue";
        public const string OUTPUT_EXTERNAL_VARIABLE_VALUE_TYPE_ID_COLUMN_NAME = "ExternalVariableTypeId";
        public const string OUTPUT_EXTERNAL_VARIABLE_VALUE_VALUE_COLUMN_NAME = "Value";

        public const string CORE_EXTERNAL_VAR_TYPE_DATASHEET_NAME = "core_ExternalVariableType";

        //Initial Stock
        public const string RASTER_FILE_COLUMN_NAME = "RasterFileName";

        //Flow Pathway
        public const string STOCK_TYPE_ID_SOURCE_COLUMN_NAME = "StockTypeIdSource";
        public const string STOCK_TYPE_ID_DEST_COLUMN_NAME = "StockTypeIdDest";

        //Flow Diagram
        public const string LOCATION_COLUMN_NAME = "Location";
        public const int FLOW_PATHWAY_NULL_STOCK_TYPE_CUE_SIZE = 14;
        public static Color FLOW_PATHWAY_CUE_COLOR = Color.Green;

        //Flow Order
        public const string DATASHEET_FLOW_ORDER_OPTIONS_ABT_COLUMN_NAME = "ApplyBeforeTransitions";
        public const string DATASHEET_FLOW_ORDER_OPTIONS_AERS_COLUMN_NAME = "ApplyEquallyRankedSimultaneously";
        public const string DATASHEET_FLOW_ORDER_ORDER_COLUMN_NAME = "Order";

        //Stock Flow Output Options
        public const string DATASHEET_STOCKFLOW_OO_NAME = "stsim_OutputOptionsStockFlow";
        public const string DATASHEET_STOCKFLOW_OO_DISPLAY_NAME = "Stock Flow Output Options";
        public const string DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_ST_COLUMN_NAME = "SummaryOutputST";
        public const string DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_ST_TIMESTEPS_COLUMN_NAME = "SummaryOutputSTTimesteps";
        public const string DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_FL_COLUMN_NAME = "SummaryOutputFL";
        public const string DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_FL_TIMESTEPS_COLUMN_NAME = "SummaryOutputFLTimesteps";
        public const string DATASHEET_STOCKFLOW_OO_SPATIAL_OUTPUT_ST_COLUMN_NAME = "SpatialOutputST";
        public const string DATASHEET_STOCKFLOW_OO_SPATIAL_OUTPUT_ST_TIMESTEPS_COLUMN_NAME = "SpatialOutputSTTimesteps";
        public const string DATASHEET_STOCKFLOW_OO_SPATIAL_OUTPUT_FL_COLUMN_NAME = "SpatialOutputFL";
        public const string DATASHEET_STOCKFLOW_OO_SPATIAL_OUTPUT_FL_TIMESTEPS_COLUMN_NAME = "SpatialOutputFLTimesteps";
        public const string DATASHEET_STOCKFLOW_OO_LATERAL_OUTPUT_FL_COLUMN_NAME = "LateralOutputFL";
        public const string DATASHEET_STOCKFLOW_OO_LATERAL_OUTPUT_FL_TIMESTEPS_COLUMN_NAME = "LateralOutputFLTimesteps";
        public const string DATASHEET_STOCKFLOW_OO_AVG_SPATIAL_OUTPUT_ST_COLUMN_NAME = "AvgSpatialOutputST";
        public const string DATASHEET_STOCKFLOW_OO_AVG_SPATIAL_OUTPUT_ST_TIMESTEPS_COLUMN_NAME = "AvgSpatialOutputSTTimesteps";
        public const string DATASHEET_STOCKFLOW_OO_AVG_SPATIAL_OUTPUT_ST_ACROSS_TIMESTEPS_COLUMN_NAME = "AvgSpatialOutputSTAcrossTimesteps";
        public const string DATASHEET_STOCKFLOW_OO_AVG_SPATIAL_OUTPUT_FL_COLUMN_NAME = "AvgSpatialOutputFL";
        public const string DATASHEET_STOCKFLOW_OO_AVG_SPATIAL_OUTPUT_FL_TIMESTEPS_COLUMN_NAME = "AvgSpatialOutputFLTimesteps";
        public const string DATASHEET_STOCKFLOW_OO_AVG_SPATIAL_OUTPUT_FL_ACROSS_TIMESTEPS_COLUMN_NAME = "AvgSpatialOutputFLAcrossTimesteps";
        public const string DATASHEET_STOCKFLOW_OO_AVG_SPATIAL_OUTPUT_LFL_COLUMN_NAME = "AvgSpatialOutputLFL";
        public const string DATASHEET_STOCKFLOW_OO_AVG_SPATIAL_OUTPUT_LFL_TIMESTEPS_COLUMN_NAME = "AvgSpatialOutputLFLTimesteps";
        public const string DATASHEET_STOCKFLOW_OO_AVG_SPATIAL_OUTPUT_LFL_ACROSS_TIMESTEPS_COLUMN_NAME = "AvgSpatialOutputLFLAcrossTimesteps";

        //Stock Flow Datasheets (general)
        public const string DATASHEET_STOCK_TYPE_NAME = "stsim_StockType";
        public const string DATASHEET_STOCK_GROUP_NAME = "stsim_StockGroup";
        public const string DATASHEET_FLOW_TYPE_NAME = "stsim_FlowType";
        public const string DATASHEET_FLOW_MULTIPLIER_TYPE_NAME = "stsim_FlowMultiplierType";
        public const string DATASHEET_FLOW_GROUP_NAME = "stsim_FlowGroup";
        public const string DATASHEET_INITIAL_STOCK_NON_SPATIAL = "stsim_InitialStockNonSpatial";
        public const string DATASHEET_INITIAL_STOCK_SPATIAL = "stsim_InitialStockSpatial";
        public const string DATASHEET_STOCK_LIMIT_NAME = "stsim_StockLimit";
        public const string DATASHEET_FLOW_MULTIPLIER_BY_STOCK_NAME = "stsim_FlowMultiplierByStock";
        public const string DATASHEET_STOCK_TRANSITION_MULTIPLIER_NAME = "stsim_StockTransitionMultiplier";
        public const string DATASHEET_STOCK_TYPE_GROUP_MEMBERSHIP_NAME = "stsim_StockTypeGroupMembership";
        public const string DATASHEET_FLOW_PATHWAY_NAME = "stsim_FlowPathway";
        public const string DATASHEET_FLOW_PATHWAY_DIAGRAM_NAME = "stsim_FlowPathwayDiagram";
        public const string DATASHEET_FLOW_MULTIPLIER_NAME = "stsim_FlowMultiplier";
        public const string DATASHEET_FLOW_SPATIAL_MULTIPLIER_NAME = "stsim_FlowSpatialMultiplier";
        public const string DATASHEET_FLOW_LATERAL_MULTIPLIER_NAME = "stsim_FlowLateralMultiplier";
        public const string DATASHEET_FLOW_TYPE_GROUP_MEMBERSHIP_NAME = "stsim_FlowTypeGroupMembership";
        public const string DATASHEET_FLOW_ORDER = "stsim_FlowOrder";
        public const string DATASHEET_FLOW_ORDER_OPTIONS = "stsim_FlowOrderOptions";
        public const string DATASHEET_OUTPUT_FLOW_NAME = "stsim_OutputFlow";
        public const string DATASHEET_OUTPUT_STOCK_NAME = "stsim_OutputStock";
        public const string DATASHEET_OUTPUT_SPATIAL_STOCK_GROUP = "stsim_OutputSpatialStockGroup";
        public const string DATASHEET_OUTPUT_SPATIAL_FLOW_GROUP = "stsim_OutputSpatialFlowGroup";
        public const string DATASHEET_OUTPUT_LATERAL_FLOW_GROUP = "stsim_OutputLateralFlowGroup";
        public const string DATASHEET_OUTPUT_AVG_SPATIAL_STOCK_GROUP = "stsim_OutputAverageSpatialStockGroup";
        public const string DATASHEET_OUTPUT_AVG_SPATIAL_FLOW_GROUP = "stsim_OutputAverageSpatialFlowGroup";
        public const string DATASHEET_OUTPUT_AVG_SPATIAL_LATERAL_FLOW_GROUP = "stsim_OutputAverageLateralFlowGroup";
        public const string DATASHEET_OUTPUT_FILTER_STOCKS = "stsim_OutputFilterStocks";
        public const string DATASHEET_OUTPUT_FILTER_FLOWS = "stsim_OutputFilterFlows";

        //Chart Variable Names
        public const string DENSITY_GROUP_NAME = "stsim_DensityGroup";
        public const string STATE_CLASS_VARIABLE_NAME = "stsim_StateClass";
        public const string STATE_CLASS_PROPORTION_VARIABLE_NAME = "stsim_StateClassProportion";
        public const string TRANSITION_VARIABLE_NAME = "stsim_Transition";
        public const string TRANSITION_PROPORTION_VARIABLE_NAME = "stsim_TransitionProportion";
        public const string TST_VARIABLE_NAME = "stsim_TST";
        public const string STATE_ATTRIBUTE_VARIABLE_NAME = "stsim_StateAttribute";
        public const string STATE_ATTRIBUTE_DENSITY_VARIABLE_NAME = "stsim_StateAttributeDensity";
        public const string TRANSITION_ATTRIBUTE_VARIABLE_NAME = "stsim_TransitionAttribute";
        public const string TRANSITION_ATTRIBUTE_DENSITY_VARIABLE_NAME = "stsim_TransitionAttributeDensity";
        public const string STOCK_GROUP_VAR_NAME = "stsim_StockGroup";
        public const string STOCK_GROUP_DENSITY_VAR_NAME = "stsim_StockGroupDensity";
        public const string FLOW_GROUP_VAR_NAME = "stsim_FlowGroup";
        public const string FLOW_GROUP_DENSITY_VAR_NAME = "stsim_FlowGroupDensity";

        //Stock-Flow Messages
        public const string NO_SUMMARY_OUTPUT_OPTIONS_INFORMATION = "No summary output options specified for stocks and flows.  Using defaults.";
        public const string REPORT_EXCEL_TOO_MANY_ROWS = "There are too many rows for Excel.  Please try another format for your report.";
        public const string SPATIAL_FILE_STOCK_LOAD_WARNING = "The Initial Stocks Raster file '{0}' did not load, and will be ignored.";
        public const string SPATIAL_FILE_STOCK_ROW_COLUMN_MISMATCH = "The Initial Stocks Raster file '{0}' metadata did not match that of the Initial Condition Primary Stratum raster file. {1}.";
        public const string SPATIAL_FILE_STOCK_METADATA_INFO = "The Initial Stocks Raster file '{0}' metadata did not match that of the Initial Condition Primary Stratum raster file, but differences will be ignored. {1}.";
        public const string ERROR_NO_RESULT_SCENARIOS_FOR_REPORT = "There must be at least one selected result scenario to create this report.";
        public const string ERROR_FLOW_MULTIPLIERS_BETA_DISTRIBUTION_INVALID = "Flow Multipliers: The beta distribution parameters are invalid.  The multiplier will be used for all iterations.";
        public const string SPATIAL_METADATA_ROW_COLUMN_MISMATCH = "The Flow Spatial Multiplier file '{0}' number of rows and/or columns did not match that of ST-Sim's Initial Condition Primary Stratum raster file.";
        public const string SPATIAL_METADATA_INFO = "The Flow Spatial Multiplier file '{0}' metadata did not match that of ST-Sim's Initial Condition Primary Stratum raster file, but differences will be ignored. {1}.";
        public const string SPATIAL_PROCESS_WARNING = "The Flow Spatial Multiplier file '{0}' did not load, and will be ignored.";

        //Spatial Map file naming constants - Stock and Flow Group
        public const string SPATIAL_MAP_STOCK_GROUP_VARIABLE_PREFIX = "stkg";
        public const string SPATIAL_MAP_FLOW_GROUP_VARIABLE_PREFIX = "flog";
        public const string SPATIAL_MAP_LATERAL_FLOW_GROUP_VARIABLE_PREFIX = "lflog";
        public const string SPATIAL_MAP_AVG_STOCK_GROUP_VARIABLE_PREFIX = "avgstkg";
        public const string SPATIAL_MAP_AVG_FLOW_GROUP_VARIABLE_PREFIX = "avgflog";
        public const string SPATIAL_MAP_AVG_LATERAL_FLOW_GROUP_VARIABLE_PREFIX = "avglflog";
    }
}
