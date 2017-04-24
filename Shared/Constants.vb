'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Module Constants

    'General
    Public Const AGE_KEY_NO_AGES As Integer = -1
    Public Const SECONDARY_STRATUM_ID_WILDCARD_VALUE As Integer = 0
    Public Const DEFAULT_TRANSITION_ORDER As Double = Double.MinValue

    'Run Control
    Public Const DATASHEET_RUN_CONTROL_NON_SPATIAL_NUM_CELLS_DEFAULT As Integer = 1000
    Public Const DATASHEET_RUN_CONTROL_SPATIAL_NUM_CELLS_DEFAULT As Integer = 1024      ' Default when Spatial Run with Non-Spatial IC
    Public Const DATASHEET_RUN_CONTROL_TOTAL_AMOUNT_DEFAULT As Integer = 100

    'Raster
    Public Const SPATIAL_DIAGONAL_PROBABILITY As Double = 0.25
    Public Const SPATIAL_FILE_DATA_SIZE As Integer = 255
    Public Const SPATIAL_SRS_DATA_SIZE As Integer = 1024
    Public Const SPATIAL_CELL_SIZE_UNITS_DATA_SIZE = 20

    'Transition diagram
    Public Const TRANSITION_DIAGRAM_MAX_ROWS As Integer = 256
    Public Const TRANSITION_DIAGRAM_MAX_COLUMNS As Integer = 26
    Public Const TRANSITION_DIAGRAM_ITEM_HEIGHT As Integer = 25
    Public Const TRANSITION_DIAGRAM_TITLE_BAR_HEIGHT As Integer = 25
    Public Const TRANSITION_DIAGRAM_SHAPE_PADDING As Integer = 44
    Public Const TRANSITION_DIAGRAM_SHAPE_SIZE As Integer = (TRANSITION_DIAGRAM_TITLE_BAR_HEIGHT + (2 * TRANSITION_DIAGRAM_ITEM_HEIGHT))
    Public Const TRANSITION_DIAGRAM_SPACE_BETWEEN_SHAPES As Integer = 2 * TRANSITION_DIAGRAM_SHAPE_PADDING
    Public Const TRANSITION_DIAGRAM_NUM_VERTICAL_CONNECTORS As Integer = 9
    Public Const TRANSITION_DIAGRAM_NUM_HORIZONTAL_CONNECTORS As Integer = 9
    Public Const TRANSITION_DIAGRAM_OFF_STRATUM_CUE_SIZE As Integer = 14
    Public Const TRANSITION_DIAGRAM_LANES_BETWEEN_SHAPES As Integer = 11
    Public TRANSITION_DIAGRAM_TEXT_COLOR As System.Drawing.Color = Drawing.Color.Black
    Public TRANSITION_DIAGRAM_READONLY_TEXT_COLOR As System.Drawing.Color = Drawing.Color.Gray
    Public TRANSITION_DIAGRAM_SELECTED_TEXT_COLOR As System.Drawing.Color = TRANSITION_DIAGRAM_TEXT_COLOR
    Public TRANSITION_DIAGRAM_SHAPE_BACKGROUND_COLOR As System.Drawing.Color = System.Drawing.Color.FromArgb(255, 240, 240, 240)
    Public TRANSITION_DIAGRAM_SHAPE_BORDER_COLOR As System.Drawing.Color = Drawing.Color.Gray
    Public TRANSITION_DIAGRAM_SHAPE_READONLY_BORDER_COLOR As System.Drawing.Color = Drawing.Color.LightGray
    Public TRANSITION_SELECTED_LINE_COLOR As System.Drawing.Color = Drawing.Color.Red
    Public DETERMINISTIC_TRANSITION_LINE_COLOR As System.Drawing.Color = Drawing.Color.Green
    Public PROBABILISTIC_TRANSITION_LINE_COLOR As System.Drawing.Color = Drawing.Color.CornflowerBlue

    'Spatial
    Public Const SPATIAL_MAP_EXPORT_STATE_CLASS_VARIABLE_NAME As String = "SClass"
    Public Const SPATIAL_MAP_STATE_CLASS_VARIABLE_NAME As String = "sc"
    Public Const SPATIAL_MAP_EXPORT_STRATUM_VARIABLE_NAME As String = "Stratum"
    Public Const SPATIAL_MAP_STRATUM_VARIABLE_NAME As String = "str"
    Public Const SPATIAL_MAP_SECONDARY_STRATUM_VARIABLE_NAME As String = "secstr"
    Public Const SPATIAL_MAP_EXPORT_AGE_VARIABLE_NAME As String = "Age"
    Public Const SPATIAL_MAP_AGE_VARIABLE_NAME As String = "age"
    Public Const SPATIAL_MAP_EXPORT_TRANSITION_GROUP_VARIABLE_PREFIX As String = "Tg"
    Public Const SPATIAL_MAP_TRANSITION_GROUP_VARIABLE_PREFIX As String = "tg"
    Public Const SPATIAL_MAP_EXPORT_TST_VARIABLE_NAME As String = "Tst"
    Public Const SPATIAL_MAP_TST_VARIABLE_NAME As String = "tst"
    Public Const SPATIAL_MAP_EXPORT_STATE_ATTRIBUTE_VARIABLE_PREFIX As String = "Sa"
    Public Const SPATIAL_MAP_STATE_ATTRIBUTE_VARIABLE_PREFIX As String = "sa"
    Public Const SPATIAL_MAP_EXPORT_TRANSITION_ATTRIBUTE_VARIABLE_PREFIX As String = "Ta"
    Public Const SPATIAL_MAP_TRANSITION_ATTRIBUTE_VARIABLE_PREFIX As String = "ta"
    Public Const SPATIAL_MAP_EXPORT_AVG_ANNUAL_TRANSITION_PROBABILITY_VARIABLE_PREFIX As String = "Tgap"
    Public Const SPATIAL_MAP_AVG_ANNUAL_TRANSITION_PROBABILITY_VARIABLE_PREFIX As String = "tgap"

    'Spatial Map file naming Regex filter, containing 1 ID value
    Public Const FILE_FILTER_ID_REGEX = "^(.*){0}-([\d]*)\.(tif|vrt)$"



End Module
