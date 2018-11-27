// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System.Drawing;

namespace SyncroSim.STSim
{
    internal static class Constants
    {
        //General
        public const int AGE_KEY_NO_AGES = -1;
        public const int OUTPUT_COLLECTION_WILDCARD_KEY = 0;
        public const double DEFAULT_TRANSITION_ORDER = double.MinValue;
        public const string AGE_QUERY_CACHE_TAG = "-STSIM-AGE-QUERY";
        public const string AGECLASS_UPDATE_REQUIRED_TAG = "STSimAgeClassUpdateRequired";

        //Run Control
        public const int DATASHEET_RUN_CONTROL_NON_SPATIAL_NUM_CELLS_DEFAULT = 1000;
        public const int DATASHEET_RUN_CONTROL_SPATIAL_NUM_CELLS_DEFAULT = 1024; // Default when Spatial Run with Non-Spatial IC
        public const int DATASHEET_RUN_CONTROL_TOTAL_AMOUNT_DEFAULT = 100;

        //Raster
        public const double SPATIAL_DIAGONAL_PROBABILITY = 0.25;
        public const int SPATIAL_FILE_DATA_SIZE = 255;
        public const int SPATIAL_SRS_DATA_SIZE = 1024;
        public const int SPATIAL_CELL_SIZE_UNITS_DATA_SIZE = 20;

        //Transition diagram
        public const int TRANSITION_DIAGRAM_MAX_ROWS = 256;
        public const int TRANSITION_DIAGRAM_MAX_COLUMNS = 26;
        public const int TRANSITION_DIAGRAM_ITEM_HEIGHT = 25;
        public const int TRANSITION_DIAGRAM_TITLE_BAR_HEIGHT = 25;
        public const int TRANSITION_DIAGRAM_SHAPE_PADDING = 44;
        public const int TRANSITION_DIAGRAM_SHAPE_SIZE = (TRANSITION_DIAGRAM_TITLE_BAR_HEIGHT + (2 * TRANSITION_DIAGRAM_ITEM_HEIGHT));
        public const int TRANSITION_DIAGRAM_SPACE_BETWEEN_SHAPES = 2 * TRANSITION_DIAGRAM_SHAPE_PADDING;
        public const int TRANSITION_DIAGRAM_NUM_VERTICAL_CONNECTORS = 9;
        public const int TRANSITION_DIAGRAM_NUM_HORIZONTAL_CONNECTORS = 9;
        public const int TRANSITION_DIAGRAM_OFF_STRATUM_CUE_SIZE = 14;
        public const int TRANSITION_DIAGRAM_LANES_BETWEEN_SHAPES = 11;
        public static Color TRANSITION_DIAGRAM_TEXT_COLOR = Color.Black;
        public static Color TRANSITION_DIAGRAM_READONLY_TEXT_COLOR = Color.Gray;
        public static Color TRANSITION_DIAGRAM_SELECTED_TEXT_COLOR = TRANSITION_DIAGRAM_TEXT_COLOR;
        public static Color TRANSITION_DIAGRAM_SHAPE_BACKGROUND_COLOR = Color.FromArgb(255, 240, 240, 240);
        public static Color TRANSITION_DIAGRAM_SHAPE_BORDER_COLOR = Color.Gray;
        public static Color TRANSITION_DIAGRAM_SHAPE_READONLY_BORDER_COLOR = Color.LightGray;
        public static Color TRANSITION_SELECTED_LINE_COLOR = Color.DarkOrange;
        public static Color DETERMINISTIC_TRANSITION_LINE_COLOR = Color.Green;
        public static Color PROBABILISTIC_TRANSITION_LINE_COLOR = Color.CornflowerBlue;

        //Spatial
        public const string SPATIAL_MAP_EXPORT_STATE_CLASS_VARIABLE_NAME = "SClass";
        public const string SPATIAL_MAP_STATE_CLASS_VARIABLE_NAME = "sc";
        public const string SPATIAL_MAP_EXPORT_STRATUM_VARIABLE_NAME = "Stratum";
        public const string SPATIAL_MAP_STRATUM_VARIABLE_NAME = "str";
        public const string SPATIAL_MAP_SECONDARY_STRATUM_VARIABLE_NAME = "secstr";
        public const string SPATIAL_MAP_TERTIARY_STRATUM_VARIABLE_NAME = "terstr";
        public const string SPATIAL_MAP_EXPORT_AGE_VARIABLE_NAME = "Age";
        public const string SPATIAL_MAP_AGE_VARIABLE_NAME = "age";
        public const string SPATIAL_MAP_EXPORT_TRANSITION_GROUP_VARIABLE_PREFIX = "Tg";
        public const string SPATIAL_MAP_TRANSITION_GROUP_VARIABLE_PREFIX = "tg";
        public const string SPATIAL_MAP_EXPORT_TST_VARIABLE_NAME = "Tst";
        public const string SPATIAL_MAP_TST_VARIABLE_NAME = "tst";
        public const string SPATIAL_MAP_EXPORT_STATE_ATTRIBUTE_VARIABLE_PREFIX = "Sa";
        public const string SPATIAL_MAP_STATE_ATTRIBUTE_VARIABLE_PREFIX = "sa";
        public const string SPATIAL_MAP_EXPORT_TRANSITION_ATTRIBUTE_VARIABLE_PREFIX = "Ta";
        public const string SPATIAL_MAP_TRANSITION_ATTRIBUTE_VARIABLE_PREFIX = "ta";
        public const string SPATIAL_MAP_EXPORT_AVG_ANNUAL_TRANSITION_PROBABILITY_VARIABLE_PREFIX = "Tgap";
        public const string SPATIAL_MAP_AVG_ANNUAL_TRANSITION_PROBABILITY_VARIABLE_PREFIX = "tgap";

        //Spatial Output Datasheets
        public const string DATASHEET_OUTPUT_SPATIAL_STRATUM = "STSim_OutputSpatialStratum";
        public const string DATASHEET_OUTPUT_SPATIAL_STATE_CLASS = "STSim_OutputSpatialState";
        public const string DATASHEET_OUTPUT_SPATIAL_AGE = "STSim_OutputSpatialAge";
        public const string DATASHEET_OUTPUT_SPATIAL_TRANSITION = "STSim_OutputSpatialTransition";
        public const string DATASHEET_OUTPUT_SPATIAL_STATE_ATTRIBUTE = "STSim_OutputSpatialStateAttribute";
        public const string DATASHEET_OUTPUT_SPATIAL_TRANSITION_ATTRIBUTE = "STSim_OutputSpatialTransitionAttribute";
        public const string DATASHEET_OUTPUT_SPATIAL_TST = "STSim_OutputSpatialTST";
        public const string DATASHEET_OUTPUT_SPATIAL_AVERAGE_TRANSITION_PROBABILITY = "STSim_OutputSpatialAverageTransitionProbability";

        //Spatial Output Datasheet Common Column Names
        public const string DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN = "Filename";

        // Legend Map Common Strings
        public const string LEGEND_MAP_BLANK_LEGEND_ITEM = "[blank]";

        //Spatial Map file naming Regex filter, containing 1 ID value
        public const string FILE_FILTER_ID_REGEX = "^(.*){0}-([\\d]*)\\.(tif|vrt)$";
    }
}
