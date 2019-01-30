// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal static class MessageStrings
    {
        //Confirmation
        public const string CONFIRM_DIAGRAM_PASTE_OVERWRITE = "One or more of the state classes being pasted already exists in the diagram. Overwrite ages and append transitions?";
        public const string CONFIRM_DIAGRAM_DELETE = "Are you sure you want to delete the selected state classes?  Note that associated transitions may be changed or deleted if you continue.";

        //Errors
        public const string ERROR_INVALID_CELL_ADDRESS = "The value must be a valid cell address (a valid cell address is a letter from 'A' to 'Z' followed by a number from 1 to 255.  Example: 'A25')";
        public const string ERROR_DIAGRAM_STATE_CLASS_EXISTS = "The specified state class already exists in the diagram.";
        public const string ERROR_DIAGRAM_NO_MORE_LOCATIONS = "There are no more available locations on the diagram.";
        public const string ERROR_DIAGRAM_CANNOT_PASTE_SPECIFIC_LOCATION = "Cannot paste state classes to the specified location.";
        public const string ERROR_DIAGRAM_CANNOT_PASTE_ANY_LOCATION = "Cannot paste state classes into any location.";
        public const string ERROR_DIAGRAM_NO_STATE_LABEL_X_VALUES = "There are no State Label X values.  Cannot continue.";
        public const string ERROR_DIAGRAM_NO_STATE_LABEL_Y_VALUES = "There are no State Label Y values.  Cannot continue.";
        public const string ERROR_SPATIAL_FILE_NOT_DEFINED = "Both the State Class and Stratum Initial Conditions files must be defined when running spatially.";
        public const string ERROR_SPATIAL_PRIMARY_STRATUM_FILE_NOT_DEFINED = "The Primary Stratum Initial Conditions file must be defined when running spatially.";
        public const string ERROR_SPATIAL_FILE_MISMATCHED_METADATA = "The Raster file '{0}' metadata did not match that of the Initial Condition Primary Stratum raster file. {1}.";
        public const string ERROR_SPATIAL_NO_CELL_AREA = "The Spatial Initial Conditions does not contain a valid non-zero Cell Area.";
        public const string ERROR_NO_INITIAL_CONDITIONS_DISTRIBUTION_RECORDS = "No Initial Conditions Distribution values have been defined.  Cannot continue.";
        public const string ERROR_NO_INITIAL_CONDITIONS_SPATIAL_RECORDS = "No Initial Conditions Spatial values have been defined.  Cannot continue.";
        public const string ERROR_NO_APPLICABLE_INITIAL_CONDITIONS_SPATIAL_RECORDS = "No applicable Initial Conditions Spatial values have been defined for the starting iteraration. Cannot continue.";
        public const string ERROR_AGE_DATA_MISSING = "Age classes have been defined but the scenario '{0}' does not contain age data.  You may need to re-run this scenario.";
        public const string ERROR_NO_RESULT_SCENARIOS_FOR_REPORT = "There must be at least one selected ressult scenario create this report.";

        public const string PROMPT_AGE_TYPE_CHANGE = 
            "Changing your Age Type data will automatically save this library the next time you refresh your charts.  " +
            "This can take several minutes because all result scenarios for the current project will also be updated.\n\n" + 
            "Are you sure you want to continue?";

        public const string PROMPT_AGE_GROUP_CHANGE =
            "Changing your Age Group data will automatically save this library the next time you refresh your charts.  " +
            "This can take several minutes because all result scenarios for the current project will also be updated.\n\n" +
            "Are you sure you want to continue?";

        //Status Messages
        public const string STATUS_USING_DEFAULT_MAX_TIMESTEP_WARNING = "The number of {0}s was not specified.  Using default.";
        public const string STATUS_USING_DEFAULT_MAX_ITERATIONS_WARNING = "The number of iterations was not specified.  Using default.";
        public const string STATUS_USING_DEFAULT_NUM_CELLS_WARNING = "The number of cells was not specified.  Using default.";
        public const string STATUS_USING_DEFAULT_TOTAL_AMOUNT_WARNING = "The total amount was not specified.  Using default.";
        public const string STATUS_NO_OUTPUT_OPTIONS_WARNIING = "No output options specified.  Using defaults.";
        public const string STATUS_STATE_ATTRIBUTE_VALUES_EXIST_WARNING = "State attribute values exist but state attribute output is not selected.";
        public const string STATUS_TRANSITION_ATTRIBUTE_VALUES_EXIST_WARNING = "Transition attribute values exist but transition attribute output is not selected.";
        public const string STATUS_NO_INITIAL_CONDITIONS_WARNING = "No Initial Conditions Distribution values provided - model assuming equal area in all state classes";
        public const string STATUS_SPATIAL_FILE_MISSING_PROJECTION_WARNING = "The Primary Stratum Raster file '{0}' does not have an associated Projection.";
        public const string STATUS_SPATIAL_FILE_TSM_METADATA_WARNING = "The Transition Spatial Multiplier file '{0}' number of rows and/or columns did not match that of the Initial Condition Primary Stratum raster file, and will be ignored.";
        public const string STATUS_SPATIAL_FILE_TSM_METADATA_INFO = "The Transition Spatial Multiplier file '{0}' metadata did not match that of the Initial Condition Primary Stratum raster file, but differences will be ignored. {1}.";
        public const string STATUS_SPATIAL_FILE_TSIM_METADATA_WARNING = "The Transition Spatial Initiation Multiplier file '{0}' number of rows and/or columns did not match that of the Initial Condition Primary Stratum raster file, and will be ignored.";
        public const string STATUS_SPATIAL_FILE_TSIM_METADATA_INFO = "The Transition Spatial Initiation Multiplier file '{0}' metadata did not match that of the Initial Condition Primary Stratum raster file, but differences will be ignored. {1}.";
        public const string STATUS_SPATIAL_FILE_MISMATCHED_METADATA_INFO = "The Raster file '{0}' metadata did not match that of the Initial Condition Primary Stratum raster file, but differences will be ignored. {1}.";
        public const string STATUS_SPATIAL_RUN_USING_NONSPATIAL_IC = "No Spatial Initial Conditions Primary Stratum file specified. Spatial Run performed using Non-Spatial Initial Conditions.";
        public const string STATUS_SPATIAL_RUN_USING_COMBINED_IC = "Spatial Initial Conditions partially specified. The missing Spatial Initial Conditions files will be generated based on Non-Spatial Initial Conditions.";
        public const string STATUS_SPATIAL_RUN_USING_COMBINED_IC_MISSING_ICD = "Spatial Initial Conditions partially specified, and insufficient detail in Non-Spatial Initial Conditions to properly complete iteration {0}.";
        public const string STATUS_SPATIAL_RUN_NO_PROPERTIES_DEFINED = "The Raster file metadata has not been configured (possible cause is running from command-line), and will be derived from available Primary Stratum file.";
    }
}
