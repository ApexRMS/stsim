'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Module MessageStrings

    'Confirmation
    Public Const CONFIRM_DIAGRAM_PASTE_OVERWRITE As String = "One or more of the state classes being pasted already exists in the diagram. Overwrite ages and append transitions?"
    Public Const CONFIRM_DIAGRAM_DELETE As String = "Are you sure you want to delete the selected state classes?  Note that associated transitions may be changed or deleted if you continue."

    'Errors
    Public Const ERROR_INVALID_CELL_ADDRESS As String = "The value must be a valid cell address (a valid cell address is a letter from 'A' to 'Z' followed by a number from 1 to 255.  Example: 'A25')"
    Public Const ERROR_DIAGRAM_STATE_CLASS_EXISTS As String = "The specified state class already exists in the diagram."
    Public Const ERROR_DIAGRAM_NO_MORE_LOCATIONS As String = "There are no more available locations on the diagram."
    Public Const ERROR_DIAGRAM_CANNOT_PASTE_SPECIFIC_LOCATION As String = "Cannot paste state classes to the specified location."
    Public Const ERROR_DIAGRAM_CANNOT_PASTE_ANY_LOCATION As String = "Cannot paste state classes into any location."
    Public Const ERROR_DIAGRAM_NO_STATE_LABEL_X_VALUES As String = "There are no State Label X values.  Cannot continue."
    Public Const ERROR_DIAGRAM_NO_STATE_LABEL_Y_VALUES As String = "There are no State Label Y values.  Cannot continue."
    Public Const ERROR_SPATIAL_FILE_NOT_DEFINED As String = "Both the State Class and Stratum Initial Conditions files must be defined when running spatially."
    Public Const ERROR_SPATIAL_PRIMARY_STRATUM_FILE_NOT_DEFINED As String = "The Primary Stratum Initial Conditions file must be defined when running spatially."
    Public Const ERROR_SPATIAL_FILE_MISMATCHED_METADATA As String = "The Raster file '{0}' metadata did not match that of the Initial Condition Primary Stratum raster file. {1}."
    Public Const ERROR_SPATIAL_NO_CELL_AREA = "The Spatial Initial Conditions does not contain a valid non-zero Cell Area."
    Public Const ERROR_NO_INITIAL_CONDITIONS_DISTRIBUTION_RECORDS As String = "No Initial Conditions Distribution values have been defined.  Cannot continue."
    Public Const ERROR_NO_INITIAL_CONDITIONS_SPATIAL_RECORDS As String = "No Initial Conditions Spatial values have been defined.  Cannot continue."
    Public Const ERROR_NO_APPLICABLE_INITIAL_CONDITIONS_SPATIAL_RECORDS As String = "No applicable Initial Conditions Spatial values have been defined for the starting iteraration. Cannot continue."
    Public Const ERROR_AGE_DATA_MISSING As String = "Age classes have been defined but the scenario '{0}' does not contain age data.  You may need to re-run this scenario."
    Public Const ERROR_AGE_CLASS_DATA_MISMATCH As String = "Your age types and age groups are inconsistent. You can either modify the groups and/or modify the types. If the types are modified you will need to rerun your model." & vbCrLf & vbCrLf & "Scenario is: {0}."
    Public Const ERROR_NO_RESULT_SCENARIOS_FOR_REPORT As String = "There must be at least one selected ressult scenario create this report."

    'Status Messages
    Public Const STATUS_USING_DEFAULT_MAX_TIMESTEP_WARNING As String = "The number of {0}s was not specified.  Using default."
    Public Const STATUS_USING_DEFAULT_MAX_ITERATIONS_WARNING As String = "The number of iterations was not specified.  Using default."
    Public Const STATUS_USING_DEFAULT_NUM_CELLS_WARNING As String = "The number of cells was not specified.  Using default."
    Public Const STATUS_USING_DEFAULT_TOTAL_AMOUNT_WARNING As String = "The total amount was not specified.  Using default."
    Public Const STATUS_NO_OUTPUT_OPTIONS_WARNIING As String = "No output options specified.  Using defaults."
    Public Const STATUS_STATE_ATTRIBUTE_VALUES_EXIST_WARNING As String = "State attribute values exist but state attribute output is not selected."
    Public Const STATUS_TRANSITION_ATTRIBUTE_VALUES_EXIST_WARNING As String = "Transition attribute values exist but transition attribute output is not selected."
    Public Const STATUS_NO_INITIAL_CONDITIONS_WARNING As String = "No Initial Conditions Distribution values provided - model assuming equal area in all state classes"
    Public Const STATUS_SPATIAL_FILE_MISSING_PROJECTION_WARNING As String = "The Primary Stratum Raster file '{0}' does not have an associated Projection."
    Public Const STATUS_SPATIAL_FILE_TSM_METADATA_WARNING As String = "The Transition Spatial Multiplier file '{0}' number of rows and/or columns did not match that of the Initial Condition Primary Stratum raster file, and will be ignored."
    Public Const STATUS_SPATIAL_FILE_TSM_METADATA_INFO As String = "The Transition Spatial Multiplier file '{0}' metadata did not match that of the Initial Condition Primary Stratum raster file, but differences will be ignored. {1}."
    Public Const STATUS_SPATIAL_FILE_TSIM_METADATA_WARNING As String = "The Transition Spatial Initiation Multiplier file '{0}' number of rows and/or columns did not match that of the Initial Condition Primary Stratum raster file, and will be ignored."
    Public Const STATUS_SPATIAL_FILE_TSIM_METADATA_INFO As String = "The Transition Spatial Initiation Multiplier file '{0}' metadata did not match that of the Initial Condition Primary Stratum raster file, but differences will be ignored. {1}."
    Public Const STATUS_SPATIAL_FILE_MISMATCHED_METADATA_INFO As String = "The Raster file '{0}' metadata did not match that of the Initial Condition Primary Stratum raster file, but differences will be ignored. {1}."
    Public Const STATUS_SPATIAL_RUN_USING_NONSPATIAL_IC As String = "No Spatial Initial Conditions Primary Stratum file specified. Spatial Run performed using Non-Spatial Initial Conditions."
    Public Const STATUS_SPATIAL_RUN_USING_COMBINED_IC As String = "Spatial Initial Conditions partially specified. The missing Spatial Initial Conditions files will be generated based on Non-Spatial Initial Conditions."
    Public Const STATUS_SPATIAL_RUN_USING_COMBINED_IC_MISSING_ICD As String = "Spatial Initial Conditions partially specified, and insufficient detail in Non-Spatial Initial Conditions to properly complete iteration {0}."
    Public Const STATUS_SPATIAL_RUN_NO_PROPERTIES_DEFINED As String = "The Raster file metadata has not been configured (possible cause is running from command-line), and will be derived from available Primary Stratum file."


End Module
