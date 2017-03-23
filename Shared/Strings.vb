'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Module Strings

    'General
    Public Const AGE_VALIDATION_TABLE_NAME As String = "STSim_AgeClass"
    Public Const STOCHASTIC_TIME_CHART_ANALYZER_TABLE_DATA_KEY As String = "StochasticTimeChartTableData"
    Public Const CLIPBOARD_FORMAT_TRANSITION_DIAGRAM As String = "DataSheetTransitionDiagram"

    'Data Store Names
    Public Const SQLITE_DATASTORE_NAME As String = "SQLite"

    'All Strata Diagram Tab
    Public Const DIAGRAM_ALL_STRATA_DISPLAY_NAME As String = "All Strata"

    'Report names
    Public Const STATECLASS_SUMMARY_REPORT_NAME As String = "stateclass-summary"
    Public Const TRANSITION_SUMMARY_REPORT_NAME As String = "transition-summary"
    Public Const TRANSITION_STATECLASS_SUMMARY_REPORT_NAME As String = "transition-stateclass-summary"
    Public Const STATE_ATTRIBUTE_REPORT_NAME As String = "state-attributes"
    Public Const TRANSITION_ATTRIBUTE_REPORT_NAME As String = "transition-attributes"

    'Commands
    Public Const COMMAND_STRING_OPEN As String = "Open"
    Public Const COMMAND_STRING_CUT As String = "Cut"
    Public Const COMMAND_STRING_COPY As String = "Copy"
    Public Const COMMAND_STRING_PASTE As String = "Paste"
    Public Const COMMAND_STRING_PASTE_SPECIAL As String = "Paste Special..."
    Public Const COMMAND_STRING_SELECT_ALL As String = "Select All"
    Public Const COMMAND_STRING_DELETE As String = "Delete"
    Public Const COMMAND_STRING_SHOW_GRID As String = "Show Grid"
    Public Const COMMAND_STRING_SHOW_TOOLTIPS As String = "Show Tooltips"
    Public Const COMMAND_STRING_ADD_STATECLASS As String = "Add State Class..."
    Public Const COMMAND_STRING_EDIT_STATECLASS As String = "Edit State Class..."
    Public Const COMMAND_STRING_FILTER_TRANSITIONS As String = "Filter Transitions..."
    Public Const COMMAND_STRING_SELECT_STRATUM As String = "Select Stratum..."
    Public Const COMMAND_STRING_SEPARATOR As String = "separator"

    'Columns
    Public Const DATASHEET_NAME_COLUMN_NAME As String = "Name"
    Public Const DATASHEET_MAPID_COLUMN_NAME As String = "ID"
    Public Const DATASHEET_DESCRIPTION_COLUMN_NAME As String = "Description"
    Public Const DATASHEET_SCENARIOID_COLUMN_NAME As String = "ScenarioID"
    Public Const DATASHEET_STRATUM_ID_COLUMN_NAME As String = "StratumID"
    Public Const DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME As String = "SecondaryStratumID"
    Public Const DATASHEET_ITERATION_COLUMN_NAME As String = "Iteration"
    Public Const DATASHEET_TIMESTEP_COLUMN_NAME As String = "Timestep"
    Public Const DATASHEET_STATECLASS_ID_COLUMN_NAME As String = "StateClassID"
    Public Const DATASHEET_END_STATECLASS_ID_COLUMN_NAME As String = "EndStateClassID"
    Public Const DATASHEET_TRANSITION_TYPE_ID_COLUMN_NAME As String = "TransitionTypeID"
    Public Const DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME As String = "TransitionGroupID"
    Public Const DATASHEET_TRANSITION_MULTIPLIER_TYPE_ID_COLUMN_NAME As String = "TransitionMultiplierTypeID"
    Public Const DATASHEET_STATE_ATTRIBUTE_TYPE_ID_COLUMN_NAME As String = "StateAttributeTypeID"
    Public Const DATASHEET_TRANSITION_ATTRIBUTE_TYPE_ID_COLUMN_NAME As String = "TransitionAttributeTypeID"
    Public Const DATASHEET_AGE_MIN_COLUMN_NAME As String = "AgeMin"
    Public Const DATASHEET_AGE_MAX_COLUMN_NAME As String = "AgeMax"
    Public Const DATASHEET_AGE_CLASS_COLUMN_NAME As String = "AgeClass"
    Public Const DATASHEET_COLOR_COLUMN_NAME As String = "Color"
    Public Const DATASHEET_AMOUNT_COLUMN_NAME As String = "Amount"
    Public Const DATASHEET_ATTRIBUTE_GROUP_ID_COLUMN_NAME As String = "AttributeGroupID"
    Public Const DATASHEET_DISTRIBUTIONTYPE_COLUMN_NAME As String = "DistributionType"
    Public Const DATASHEET_DISTRIBUTION_FREQUENCY_COLUMN_NAME As String = "DistributionFrequencyID"
    Public Const DATASHEET_DISTRIBUTIONSD_COLUMN_NAME As String = "DistributionSD"
    Public Const DATASHEET_DISTRIBUTIONMIN_COLUMN_NAME As String = "DistributionMin"
    Public Const DATASHEET_DISTRIBUTIONMAX_COLUMN_NAME As String = "DistributionMax"
    Public Const VALUE_MEMBER_COLUMN_NAME As String = "Value"
    Public Const DISPLAY_MEMBER_COLUMN_NAME As String = "Display"
    Public Const IS_AUTO_COLUMN_NAME As String = "IsAuto"
    Public Const AUTO_COLUMN_SUFFIX As String = "[Type]"

    'State Label X
    Public Const DATASHEET_STATE_LABEL_X_NAME As String = "STSim_StateLabelX"

    'State label Y
    Public Const DATASHEET_STATE_LABEL_Y_NAME As String = "STSim_StateLabelY"

    'Stratum
    Public Const DATASHEET_STRATA_NAME As String = "STSim_Stratum"

    'Secondary Stratum
    Public Const DATASHEET_SECONDARY_STRATA_NAME As String = "STSim_SecondaryStratum"

    'State class
    Public Const DATASHEET_STATECLASS_NAME As String = "STSim_StateClass"
    Public Const DATASHEET_STATECLASS_STATE_LABEL_X_ID_COLUMN_NAME As String = "StateLabelXID"
    Public Const DATASHEET_STATECLASS_STATE_LABEL_Y_ID_COLUMN_NAME As String = "StateLabelYID"

    'Transition type
    Public Const DATASHEET_TRANSITION_TYPE_NAME As String = "STSim_TransitionType"

    'Transition group
    Public Const DATASHEET_TRANSITION_GROUP_NAME As String = "STSim_TransitionGroup"

    'Transition type group
    Public Const DATASHEET_TRANSITION_TYPE_GROUP_NAME As String = "STSim_TransitionTypeGroup"
    Public Const DATASHEET_TRANSITION_TYPE_GROUP_TYPE_COLUMN_NAME As String = "TransitionTypeID"
    Public Const DATASHEET_TRANSITION_TYPE_GROUP_GROUP_COLUMN_NAME As String = "TransitionGroupID"
    Public Const DATASHEET_TRANSITION_TYPE_GROUP_PRIMARY_COLUMN_NAME As String = "IsPrimary"

    'Age Type
    Public Const DATASHEET_AGE_TYPE_NAME As String = "STSim_AgeType"
    Public Const DATASHEET_AGE_TYPE_FREQUENCY_COLUMN_NAME As String = "Frequency"
    Public Const DATASHEET_AGE_TYPE_MAXIMUM_COLUMN_NAME As String = "MaximumAge"

    'Age Group
    Public Const DATASHEET_AGE_GROUP_NAME As String = "STSim_AgeGroup"
    Public Const DATASHEET_AGE_GROUP_MAXIMUM_COLUMN_NAME As String = "MaximumAge"

    'Transition multiplier type
    Public Const DATASHEET_TRANSITION_MULTIPLIER_TYPE_NAME As String = "STSim_TransitionMultiplierType"

    'Patch Prioritization
    Public Const DATASHEET_PATCH_PRIORITIZATION_NAME As String = "STSim_PatchPrioritization"
    Public Const PATCH_PRIORITIZATION_SMALLEST As String = "Smallest"
    Public Const PATCH_PRIORITIZATION_SMALLEST_EDGES_ONLY As String = "Smallest (transition edges only)"
    Public Const PATCH_PRIORITIZATION_LARGEST As String = "Largest"
    Public Const PATCH_PRIORITIZATION_LARGEST_EDGES_ONLY As String = "Largest (transition edges only)"

    'Attribute Group
    Public Const DATASHEET_ATTRIBUTE_GROUP_NAME As String = "STSim_AttributeGroup"

    'State Attribute Type
    Public Const DATASHEET_STATE_ATTRIBUTE_TYPE_NAME As String = "STSim_StateAttributeType"
    Public Const DATASHEET_STATE_ATTRIBUTE_TYPE_GROUP_COLUMN_NAME As String = "AttributeGroupID"
    Public Const DATASHEET_STATE_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME As String = "Units"

    'Transition Attribute Type
    Public Const DATASHEET_TRANSITION_ATTRIBUTE_TYPE_NAME As String = "STSim_TransitionAttributeType"
    Public Const DATASHEET_TRANSITION_ATTRIBUTE_TYPE_GROUP_COLUMN_NAME As String = "AttributeGroupID"
    Public Const DATASHEET_TRANSITION_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME As String = "Units"

    'Run control
    Public Const DATASHEET_RUN_CONTROL_NAME As String = "STSim_RunControl"
    Public Const RUN_CONTROL_MIN_ITERATION_COLUMN_NAME As String = "MinimumIteration"
    Public Const RUN_CONTROL_MAX_ITERATION_COLUMN_NAME As String = "MaximumIteration"
    Public Const RUN_CONTROL_MIN_TIMESTEP_COLUMN_NAME As String = "MinimumTimestep"
    Public Const RUN_CONTROL_MAX_TIMESTEP_COLUMN_NAME As String = "MaximumTimestep"
    Public Const RUN_CONTROL_IS_SPATIAL_COLUMN_NAME As String = "IsSpatial"

    'Non-Spatial Initial conditions
    Public Const DATASHEET_NSIC_NAME As String = "STSim_InitialConditionsNonSpatial"
    Public Const DATASHEET_NSIC_TOTAL_AMOUNT_COLUMN_NAME As String = "TotalAmount"
    Public Const DATASHEET_NSIC_NUM_CELLS_COLUMN_NAME As String = "NumCells"
    Public Const DATASHEET_NSIC_CALC_FROM_DIST_COLUMN_NAME As String = "CalcFromDist"
    Public Const DATASHEET_NSIC_DISTRIBUTION_NAME As String = "STSim_InitialConditionsNonSpatialDistribution"
    Public Const DATASHEET_NSIC_DISTRIBUTION_DISPLAY_NAME As String = "Initial Conditions Distribution"
    Public Const DATASHEET_NSIC_DISTRIBUTION_RELATIVE_AMOUNT_COLUMN_NAME As String = "RelativeAmount"

    'Spatial Initial Conditions Properties
    Public Const DATASHEET_SPPIC_NAME As String = "STSim_InitialConditionsSpatialProperties"
    Public Const DATASHEET_SPPIC_NUM_COLUMNS_COLUMN_NAME As String = "NumColumns"
    Public Const DATASHEET_SPPIC_NUM_ROWS_COLUMN_NAME As String = "NumRows"
    Public Const DATASHEET_SPPIC_NUM_CELLS_COLUMN_NAME As String = "NumCells"
    Public Const DATASHEET_SPPIC_XLLCORNER_COLUMN_NAME As String = "XLLCorner"
    Public Const DATASHEET_SPPIC_YLLCORNER_COLUMN_NAME As String = "YLLCorner"
    Public Const DATASHEET_SPPIC_CELL_SIZE_COLUMN_NAME As String = "CellSize"
    Public Const DATASHEET_SPPIC_CELL_SIZE_UNITS_COLUMN_NAME As String = "CellSizeUnits"
    Public Const DATASHEET_SPPIC_CELL_AREA_COLUMN_NAME As String = "CellArea"
    Public Const DATASHEET_SPPIC_CELL_AREA_OVERRIDE_COLUMN_NAME As String = "CellAreaOverride"
    Public Const DATASHEET_SPPIC_SRS_COLUMN_NAME As String = "SRS"

    'Spatial Initial Conditions Files
    Public Const DATASHEET_SPIC_NAME As String = "STSim_InitialConditionsSpatial"
    Public Const DATASHEET_SPIC_STRATUM_FILE_COLUMN_NAME As String = "StratumFilename"
    Public Const DATASHEET_SPIC_SECONDARY_STRATUM_FILE_COLUMN_NAME As String = "SecondaryStratumFilename"
    Public Const DATASHEET_SPIC_STATE_CLASS_FILE_COLUMN_NAME As String = "StateClassFilename"
    Public Const DATASHEET_SPIC_AGE_FILE_COLUMN_NAME As String = "AgeFilename"

    'Deterministic Transitions
    Public Const DATASHEET_DT_NAME As String = "STSim_DeterministicTransition"
    Public Const DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME As String = "StratumIDSource"
    Public Const DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME As String = "StateClassIDSource"
    Public Const DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME As String = "StratumIDDest"
    Public Const DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME As String = "StateClassIDDest"
    Public Const DATASHEET_DT_LOCATION_COLUMN_NAME As String = "Location"

    'Probabilisitic Transitions
    Public Const DATASHEET_PT_NAME As String = "STSim_Transition"
    Public Const DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME As String = "StratumIDSource"
    Public Const DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME As String = "StateClassIDSource"
    Public Const DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME As String = "StratumIDDest"
    Public Const DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME As String = "StateClassIDDest"
    Public Const DATASHEET_PT_TRANSITION_TYPE_COLUMN_NAME As String = "TransitionTypeID"
    Public Const DATASHEET_PT_AGE_RELATIVE_COLUMN_NAME As String = "AgeRelative"
    Public Const DATASHEET_PT_AGE_RESET_COLUMN_NAME As String = "AgeReset"
    Public Const DATASHEET_PT_TST_MIN_COLUMN_NAME As String = "TSTMin"
    Public Const DATASHEET_PT_TST_MAX_COLUMN_NAME As String = "TSTMax"
    Public Const DATASHEET_PT_TST_RELATIVE_COLUMN_NAME As String = "TSTRelative"
    Public Const DATASHEET_PT_PROPORTION_COLUMN_NAME As String = "Proportion"
    Public Const DATASHEET_PT_PROBABILITY_COLUMN_NAME As String = "Probability"
    Public Const DATASHEET_PT_PROBXPROPN_COLUMN_NAME As String = "ProbXPropn"

    'Transition Target
    Public Const DATASHEET_TRANSITION_TARGET_NAME As String = "STSim_TransitionTarget"

    'Transition Order
    Public Const DATASHEET_TRANSITION_ORDER_NAME As String = "STSim_TransitionOrder"
    Public Const DATASHEET_TRANSITION_ORDER_ORDER_COLUMN_NAME As String = "Order"

    'Time Since Transition Group
    Public Const DATASHEET_TST_GROUP_NAME As String = "STSim_TimeSinceTransitionGroup"

    'Time Since Transition Randomize
    Public Const DATASHEET_TST_RANDOMIZE_NAME As String = "STSim_TimeSinceTransitionRandomize"
    Public Const DATASHEET_TST_RANDOMIZE_MIN_INITIAL_TST_COLUMN_NAME As String = "MinInitialTST"
    Public Const DATASHEET_TST_RANDOMIZE_MAX_INITIAL_TST_COLUMN_NAME As String = "MaxInitialTST"

    'Transition multiplier value
    Public Const DATASHEET_TRANSITION_MULTIPLIER_VALUE_NAME As String = "STSim_TransitionMultiplierValue"
    Public Const DATASHEET_TRANSITION_MULTIPLIER_VALUE_MULTIPLIER_TYPE_COLUMN_NAME As String = "TransitionMultiplierTypeID"

    'Transition spatial multiplier
    Public Const DATASHEET_TRANSITION_SPATIAL_MULTIPLIER_NAME As String = "STSim_TransitionSpatialMultiplier"
    Public Const DATASHEET_TRANSITION_SPATIAL_MULTIPLIER_MULTIPLIER_TYPE_COLUMN_NAME As String = "TransitionMultiplierTypeID"
    Public Const DATASHEET_TRANSITION_SPATIAL_MULTIPLIER_FILE_COLUMN_NAME As String = "MultiplierFilename"

    'Transition spatial initiation multiplier
    Public Const DATASHEET_TRANSITION_SPATIAL_INITIATION_MULTIPLIER_NAME As String = "STSim_TransitionSpatialInitiationMultiplier"
    Public Const DATASHEET_TRANSITION_SPATIAL_INITIATION_MULTIPLIER_FILE_COLUMN_NAME As String = "MultiplierFilename"

    'Transition Size Distribution
    Public Const DATASHEET_TRANSITION_SIZE_DISTRIBUTION_NAME As String = "STSim_TransitionSizeDistribution"
    Public Const DATASHEET_TRANSITION_SIZE_DISTRIBUTION_MAXIMUM_AREA_COLUMN_NAME As String = "MaximumArea"
    Public Const DATASHEET_TRANSITION_SIZE_DISTRIBUTION_RELATIVE_AMOUNT_COLUMN_NAME As String = "RelativeAmount"

    'Transition Spread Distribution
    Public Const DATASHEET_TRANSITION_SPREAD_DISTRIBUTION_NAME As String = "STSim_TransitionSpreadDistribution"
    Public Const DATASHEET_TRANSITION_SPREAD_DISTRIBUTION_MAXIMUM_DISTANCE_COLUMN_NAME As String = "MaximumDistance"
    Public Const DATASHEET_TRANSITION_SPREAD_DISTRIBUTION_RELATIVE_AMOUNT_COLUMN_NAME As String = "RelativeAmount"

    'Transition Direction Multiplier
    Public Const DATASHEET_TRANSITION_DIRECTION_MULTIPLER_NAME As String = "STSim_TransitionDirectionMultiplier"
    Public Const DATASHEET_TRANSITION_DIRECTION_MULTIPLER_CARDINAL_DIRECTION_COLUMN_NAME As String = "CardinalDirection"

    'Transition Slope Multiplier
    Public Const DATAFEED_TRANSITION_SLOPE_MULTIPLIER_NAME As String = "STSim_TransitionSlopeMultiplierDataFeed"
    Public Const DATASHEET_DIGITAL_ELEVATION_MODEL_NAME As String = "STSim_DigitalElevationModel"
    Public Const DATASHEET_DIGITAL_ELEVATION_MODEL_FILE_NAME_COLUMN_NAME As String = "DigitalElevationModelFilename"
    Public Const DATASHEET_TRANSITION_SLOPE_MULTIPLIER_NAME As String = "STSim_TransitionSlopeMultiplier"
    Public Const DATASHEET_TRANSITION_SLOPE_MULTIPLIER_SLOPE_COLUMN_NAME As String = "Slope"

    'Transition Adjacency Multiplier
    Public Const DATAFEED_TRANSITION_ADJACENCY_MULTIPLIER_NAME As String = "STSim_TransitionAdjacencyMultiplierDataFeed"
    Public Const DATASHEET_TRANSITION_ADJACENCY_MULTIPLIER_NAME As String = "STSim_TransitionAdjacencyMultiplier"
    Public Const DATASHEET_TRANSITION_ADJACENCY_ATTRIBUTE_VALUE_COLUMN_NAME As String = "AttributeValue"
    Public Const DATASHEET_TRANSITION_ADJACENCY_SETTING_NAME As String = "STSim_TransitionAdjacencySetting"
    Public Const DATASHEET_TRANSITION_ADJACENCY_SETTING_NR_COLUMN_NAME As String = "NeighborhoodRadius"
    Public Const DATASHEET_TRANSITION_ADJACENCY_SETTING_UF_COLUMN_NAME As String = "UpdateFrequency"

    'Transition Patch Prioritization
    Public Const DATASHEET_TRANSITION_PATCH_PRIORITIZATION_NAME As String = "STSim_TransitionPatchPrioritization"
    Public Const DATASHEET_TRANSITION_PATCH_PRIORITIZATION_PP_COLUMN_NAME As String = "PatchPrioritizationID"

    'Transition Size Prioritization
    Public Const DATASHEET_TRANSITION_SIZE_PRIORITIZATION_NAME As String = "STSim_TransitionSizePrioritization"
    Public Const DATASHEET_TRANSITION_SIZE_PRIORITIZATION_PRIORITY_TYPE_COLUMN_NAME As String = "Priority"
    Public Const DATASHEET_TRANSITION_SIZE_PRIORITIZATION_MFDIST_COLUMN_NAME As String = "MaximizeFidelityToDistribution"
    Public Const DATASHEET_TRANSITION_SIZE_PRIORITIZATION_MFAREA_COLUMN_NAME As String = "MaximizeFidelityToTotalArea"

    'Transition Pathway Auto-Correlation
    Public Const DATASHEET_TRANSITION_PATHWAY_AUTO_CORRELATION_NAME As String = "STSim_TransitionPathwayAutoCorrelation"
    Public Const DATASHEET_TRANSITION_PATHWAY_AUTO_CORRELATION_FACTOR_COLUMN_NAME As String = "AutoCorrelationFactor"
    Public Const DATASHEET_TRANSITION_PATHWAY_SPREAD_ONLY_TO_LIKE_COLUMN_NAME As String = "SpreadOnlyToLike"

    'State Attribute Value
    Public Const DATASHEET_STATE_ATTRIBUTE_VALUE_NAME As String = "STSim_StateAttributeValue"
    Public Const DATASHEET_STATE_ATTRIBUTE_VALUE_VALUE_COLUMN_NAME As String = "Value"

    'Transition Attribute Value
    Public Const DATASHEET_TRANSITION_ATTRIBUTE_VALUE_NAME As String = "STSim_TransitionAttributeValue"
    Public Const DATASHEET_TRANSITION_ATTRIBUTE_VALUE_VALUE_COLUMN_NAME As String = "Value"

    'Transition Attribute Target
    Public Const DATASHEET_TRANSITION_ATTRIBUTE_TARGET_NAME As String = "STSim_TransitionAttributeTarget"

    'Terminology
    Public Const DATASHEET_TERMINOLOGY_NAME As String = "STSim_Terminology"
    Public Const DATASHEET_TERMINOLOGY_AMOUNT_LABEL_COLUMN_NAME As String = "AmountLabel"
    Public Const DATASHEET_TERMINOLOGY_AMOUNT_UNITS_COLUMN_NAME As String = "AmountUnits"
    Public Const DATASHEET_TERMINOLOGY_STATELABELX_COLUMN_NAME As String = "StateLabelX"
    Public Const DATASHEET_TERMINOLOGY_STATELABELY_COLUMN_NAME As String = "StateLabelY"
    Public Const DATASHEET_TERMINOLOGY_PRIMARY_STRATUM_LABEL_COLUMN_NAME As String = "PrimaryStratumLabel"
    Public Const DATASHEET_TERMINOLOGY_SECONDARY_STRATUM_LABEL_COLUMN_NAME As String = "SecondaryStratumLabel"

    'Output Options
    Public Const DATASHEET_OO_NAME As String = "STSim_OutputOptions"
    Public Const DATASHEET_OO_SUMMARY_OUTPUT_SC_COLUMN_NAME As String = "SummaryOutputSC"
    Public Const DATASHEET_OO_SUMMARY_OUTPUT_SC_TIMESTEPS_COLUMN_NAME As String = "SummaryOutputSCTimesteps"
    Public Const DATASHEET_OO_SUMMARY_OUTPUT_SC_ZERO_VALUES_COLUMN_NAME As String = "SummaryOutputSCZeroValues"
    Public Const DATASHEET_OO_SUMMARY_OUTPUT_TR_COLUMN_NAME As String = "SummaryOutputTR"
    Public Const DATASHEET_OO_SUMMARY_OUTPUT_TR_TIMESTEPS_COLUMN_NAME As String = "SummaryOutputTRTimesteps"
    Public Const DATASHEET_OO_SUMMARY_OUTPUT_TR_INTERVAL_MEAN_COLUMN_NAME As String = "SummaryOutputTRIntervalMean"
    Public Const DATASHEET_OO_SUMMARY_OUTPUT_TRSC_COLUMN_NAME As String = "SummaryOutputTRSC"
    Public Const DATASHEET_OO_SUMMARY_OUTPUT_TRSC_TIMESTEPS_COLUMN_NAME As String = "SummaryOutputTRSCTimesteps"
    Public Const DATASHEET_OO_SUMMARY_OUTPUT_SA_COLUMN_NAME As String = "SummaryOutputSA"
    Public Const DATASHEET_OO_SUMMARY_OUTPUT_SA_TIMESTEPS_COLUMN_NAME As String = "SummaryOutputSATimesteps"
    Public Const DATASHEET_OO_SUMMARY_OUTPUT_TA_COLUMN_NAME As String = "SummaryOutputTA"
    Public Const DATASHEET_OO_SUMMARY_OUTPUT_TA_TIMESTEPS_COLUMN_NAME As String = "SummaryOutputTATimesteps"
    Public Const DATASHEET_OO_RASTER_OUTPUT_SC_COLUMN_NAME As String = "RasterOutputSC"
    Public Const DATASHEET_OO_RASTER_OUTPUT_SC_TIMESTEPS_COLUMN_NAME As String = "RasterOutputSCTimesteps"
    Public Const DATASHEET_OO_RASTER_OUTPUT_TR_COLUMN_NAME As String = "RasterOutputTR"
    Public Const DATASHEET_OO_RASTER_OUTPUT_TR_TIMESTEPS_COLUMN_NAME As String = "RasterOutputTRTimesteps"
    Public Const DATASHEET_OO_RASTER_OUTPUT_AGE_COLUMN_NAME As String = "RasterOutputAge"
    Public Const DATASHEET_OO_RASTER_OUTPUT_AGE_TIMESTEPS_COLUMN_NAME As String = "RasterOutputAgeTimesteps"
    Public Const DATASHEET_OO_RASTER_OUTPUT_TST_COLUMN_NAME As String = "RasterOutputTST"
    Public Const DATASHEET_OO_RASTER_OUTPUT_TST_TIMESTEPS_COLUMN_NAME As String = "RasterOutputTSTTimesteps"
    Public Const DATASHEET_OO_RASTER_OUTPUT_ST_COLUMN_NAME As String = "RasterOutputST"
    Public Const DATASHEET_OO_RASTER_OUTPUT_ST_TIMESTEPS_COLUMN_NAME As String = "RasterOutputSTTimesteps"
    Public Const DATASHEET_OO_RASTER_OUTPUT_SA_COLUMN_NAME As String = "RasterOutputSA"
    Public Const DATASHEET_OO_RASTER_OUTPUT_SA_TIMESTEPS_COLUMN_NAME As String = "RasterOutputSATimesteps"
    Public Const DATASHEET_OO_RASTER_OUTPUT_TA_COLUMN_NAME As String = "RasterOutputTA"
    Public Const DATASHEET_OO_RASTER_OUTPUT_TA_TIMESTEPS_COLUMN_NAME As String = "RasterOutputTATimesteps"
    Public Const DATASHEET_OO_RASTER_OUTPUT_AATP_COLUMN_NAME As String = "RasterOutputAATP"
    Public Const DATASHEET_OO_RASTER_OUTPUT_AATP_TIMESTEPS_COLUMN_NAME As String = "RasterOutputAATPTimesteps"

    'Distribution Type Data Feed
    Public Const DISTRIBUTION_TYPE_DATASHEET_NAME As String = "STime_DistributionType"
    Public Const DISTRIBUTION_TYPE_IS_INTERNAL_COLUMN_NAME As String = "IsInternal"
    Public Const DISTRIBUTION_TYPE_NAME_UNIFORM_INTEGER As String = "Uniform Integer"

    'Distribution Value Data Feed
    Public Const DISTRIBUTION_VALUE_DATASHEET_NAME As String = "STSim_DistributionValue"
    Public Const DISTRIBUTION_VALUE_DIST_TYPE_ID_COLUMN_NAME As String = "DistributionTypeID"
    Public Const DISTRIBUTION_VALUE_EXTVAR_TYPE_ID_COLUMN_NAME As String = "ExternalVariableTypeID"
    Public Const DISTRIBUTION_VALUE_EXTVAR_MIN_COLUMN_NAME As String = "ExternalVariableMin"
    Public Const DISTRIBUTION_VALUE_EXTVAR_MAX_COLUMN_NAME As String = "ExternalVariableMax"
    Public Const DISTRIBUTION_VALUE_DIST_VALUE_COLUMN_NAME As String = "Value"
    Public Const DISTRIBUTION_VALUE_VALUE_DIST_TYPE_ID_COLUMN_NAME As String = "ValueDistributionTypeID"
    Public Const DISTRIBUTION_VALUE_VALUE_DIST_FREQUENCY_COLUMN_NAME As String = "ValueDistributionFrequency"
    Public Const DISTRIBUTION_VALUE_VALUE_DIST_SD_COLUMN_NAME As String = "ValueDistributionSD"
    Public Const DISTRIBUTION_VALUE_VALUE_DIST_MIN_COLUMN_NAME As String = "ValueDistributionMin"
    Public Const DISTRIBUTION_VALUE_VALUE_DIST_MAX_COLUMN_NAME As String = "ValueDistributionMax"
    Public Const DISTRIBUTION_VALUE_VALUE_DIST_RELATIVE_FREQUENCY_COLUMN_NAME As String = "ValueDistributionRelativeFrequency"

    'Processing
    Public Const DATASHEET_PROCESSING_NAME As String = "STSim_Processing"
    Public Const DATASHEET_PROCESSING_SPLIT_BY_SS_COLUMN_NAME As String = "SplitBySecondaryStrata"

    'Outpu Stratum Amount
    Public Const DATASHEET_OUTPUT_STRATUM_NAME As String = "STSim_OutputStratum"

    'Output Stratum State
    Public Const DATASHEET_OUTPUT_STRATUM_STATE_NAME As String = "STSim_OutputStratumState"

    'Output Stratum Transition
    Public Const DATASHEET_OUTPUT_STRATUM_TRANSITION_NAME As String = "STSim_OutputStratumTransition"

    'OutputStratumTransitionState
    Public Const DATASHEET_OUTPUT_STRATUM_TRANSITION_STATE_NAME As String = "STSim_OutputStratumTransitionState"

    'OutputStateAttribute
    Public Const DATASHEET_OUTPUT_STATE_ATTRIBUTE_NAME As String = "STSim_OutputStateAttribute"

    'OutputTransitionAttribute
    Public Const DATASHEET_OUTPUT_TRANSITION_ATTRIBUTE_NAME As String = "STSim_OutputTransitionAttribute"

    'Processing
    Public Const DATASHEET_STSIM_PROCESSING_NAME As String = "STSim_Processing"
    Public Const DATASHEET_STSIM_PROCESSING_JOBS_BY_STRATA_COLUMN_NAME As String = "JobsByStrata"

    'Charting Variables
    Public Const STATE_CLASS_AMOUNT_VARIABLE_NAME As String = "STSimStateClassNormalVariable"
    Public Const STATE_CLASS_PROPORTION_VARIABLE_NAME As String = "STSimStateClassProportionVariable"
    Public Const TRANSITION_AMOUNT_VARIABLE_NAME As String = "STSimTransitionNormalVariable"
    Public Const TRANSITION_PROPORTION_VARIABLE_NAME As String = "STSimTransitionProportionVariable"

End Module
