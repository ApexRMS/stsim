// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

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
        public const string TST_QUERY_CACHE_TAG = "-STSIM-TST-QUERY";
        public const string AGECLASS_UPDATE_REQUIRED_TAG = "STSimAgeClassUpdateRequired";
        public const string TSTCLASS_UPDATE_REQUIRED_TAG = "STSimTSTClassUpdateRequired";
        public const int STARTING_TRANSITION_EVENT_ID = 0;
        public const double EIGHT_DIV_NINE = 8.0 / 9.0;
        public const int INCLUDE_DATA_NULL_ID = -9999;
        public const string STSIMRESOLUTION_SCENARIO_NAME = "[1] Partial";
        public const double DEFAULT_FLOW_ORDER = double.MinValue;
        public const int NULL_FROM_STOCK_TYPE_ID = 0;

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

        //Spatial file prefixes
        public const string SPATIAL_MAP_STATE_CLASS_FILEPREFIX = "sc";
        public const string SPATIAL_MAP_AGE_FILEPREFIX = "age";
        public const string SPATIAL_MAP_STRATUM_FILEPREFIX = "str";
        public const string SPATIAL_MAP_SECONDARY_STRATUM_FILEPREFIX = "secstr";
        public const string SPATIAL_MAP_TERTIARY_STRATUM_FILEPREFIX = "terstr";
        public const string SPATIAL_MAP_TRANSITION_GROUP_FILEPREFIX = "tg";
        public const string SPATIAL_MAP_TRANSITION_EVENT_FILEPREFIX = "tge";
        public const string SPATIAL_MAP_TST_FILEPREFIX = "tst";
        public const string SPATIAL_MAP_STATE_ATTRIBUTE_FILEPREFIX = "sa";
        public const string SPATIAL_MAP_TRANSITION_ATTRIBUTE_FILEPREFIX = "ta";

        public const string SPATIAL_MAP_AVG_STATE_CLASS_FILEPREFIX = "avgsc";
        public const string SPATIAL_MAP_AVG_AGE_FILEPREFIX = "avgage";
        public const string SPATIAL_MAP_AVG_STRATUM_FILEPREFIX = "avgstr";
        public const string SPATIAL_MAP_AVG_TRANSITION_PROBABILITY_FILEPREFIX = "avgtp";
        public const string SPATIAL_MAP_AVG_TST_FILEPREFIX = "avgtst";
        public const string SPATIAL_MAP_AVG_STATE_ATTRIBUTE_FILEPREFIX = "avgsa";
        public const string SPATIAL_MAP_AVG_TRANSITION_ATTRIBUTE_FILEPREFIX = "avgta";

        //Spatial map variables
        public const string SPATIAL_MAP_STATE_CLASS_VARIABLE_NAME = "stsim_StateClass";
        public const string SPATIAL_MAP_AGE_VARIABLE_NAME = "stsim_Age";
        public const string SPATIAL_MAP_STRATUM_VARIABLE_NAME = "stsim_Stratum";
        public const string SPATIAL_MAP_TRANSITION_GROUP_VARIABLE_NAME = "stsim_TransitionGroup";
        public const string SPATIAL_MAP_TRANSITION_GROUP_EVENT_VARIABLE_NAME = "stsim_TransitionEvent";
        public const string SPATIAL_MAP_TST_VARIABLE_NAME = "stsim_TST";
        public const string SPATIAL_MAP_STATE_ATTRIBUTE_VARIABLE_NAME = "stsim_StateAttribute";
        public const string SPATIAL_MAP_TRANSITION_ATTRIBUTE_VARIABLE_NAME = "stsim_TransitionAttribute";

        public const string SPATIAL_MAP_AVG_STATE_CLASS_VARIABLE_NAME = "stsim_StateClassProb";
        public const string SPATIAL_MAP_AVG_AGE_VARIABLE_NAME = "stsim_AgeProb";
        public const string SPATIAL_MAP_AVG_STRATUM_VARIABLE_NAME = "stsim_StratumProb";
        public const string SPATIAL_MAP_AVG_TRANSITION_PROBABILITY_VARIABLE_NAME = "stsim_TransitionProb";
        public const string SPATIAL_MAP_AVG_TST_VARIABLE_NAME = "stsim_TSTProb";
        public const string SPATIAL_MAP_AVG_STATE_ATTRIBUTE_VARIABLE_NAME = "stsim_StateAttributeProb";
        public const string SPATIAL_MAP_AVG_TRANSITION_ATTRIBUTE_VARIABLE_NAME = "stsim_TransitionAttributeProb";

        //Spatial Output Datasheets
        public const string DATASHEET_OUTPUT_SPATIAL_STATE_CLASS = "stsim_OutputSpatialState";
        public const string DATASHEET_OUTPUT_SPATIAL_AGE = "stsim_OutputSpatialAge";
        public const string DATASHEET_OUTPUT_SPATIAL_STRATUM = "stsim_OutputSpatialStratum";
        public const string DATASHEET_OUTPUT_SPATIAL_TRANSITION = "stsim_OutputSpatialTransition";
        public const string DATASHEET_OUTPUT_SPATIAL_TRANSITION_EVENT = "stsim_OutputSpatialTransitionEvent";
        public const string DATASHEET_OUTPUT_SPATIAL_TST = "stsim_OutputSpatialTST";
        public const string DATASHEET_OUTPUT_SPATIAL_STATE_ATTRIBUTE = "stsim_OutputSpatialStateAttribute";
        public const string DATASHEET_OUTPUT_SPATIAL_TRANSITION_ATTRIBUTE = "stsim_OutputSpatialTransitionAttribute";

        public const string DATASHEET_OUTPUT_AVG_SPATIAL_STATE_CLASS = "stsim_OutputSpatialAverageStateClass";
        public const string DATASHEET_OUTPUT_AVG_SPATIAL_AGE = "stsim_OutputSpatialAverageAge";
        public const string DATASHEET_OUTPUT_AVG_SPATIAL_STRATUM = "stsim_OutputSpatialAverageStratum";
        public const string DATASHEET_OUTPUT_AVG_SPATIAL_TRANSITION_PROBABILITY = "stsim_OutputSpatialAverageTransitionProbability";
        public const string DATASHEET_OUTPUT_AVG_SPATIAL_TST = "stsim_OutputSpatialAverageTST";
        public const string DATASHEET_OUTPUT_AVG_SPATIAL_STATE_ATTRIBUTE = "stsim_OutputSpatialAverageStateAttribute";
        public const string DATASHEET_OUTPUT_AVG_SPATIAL_TRANSITION_ATTRIBUTE = "stsim_OutputSpatialAverageTransitionAttribute";

        //Spatial Output Datasheet Common Column Names
        public const string DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN = "Filename";

        // Legend Map Common Strings
        public const string LEGEND_MAP_BLANK_LEGEND_ITEM = "[blank]";

        //Spatial Map file naming Regex filter, containing 1 Id value
        public const string FILE_FILTER_ID_REGEX = "^(.*){0}-([\\d]*)\\.(tif|vrt)$";

        //General colors
        public static Color READONLY_COLUMN_COLOR = Color.FromArgb(232, 232, 232);

        //Stock Flow Diagram
        public const int DIAGRAM_MAX_ROWS = 256;
        public const int DIAGRAM_MAX_COLUMNS = 26;
        public const int DIAGRAM_ITEM_HEIGHT = 25;
        public const int DIAGRAM_TITLE_BAR_HEIGHT = 25;
        public const int DIAGRAM_SHAPE_PADDING = 44;
        public const int DIAGRAM_SHAPE_SIZE = (DIAGRAM_TITLE_BAR_HEIGHT + (2 * DIAGRAM_ITEM_HEIGHT));
        public const int DIAGRAM_SPACE_BETWEEN_SHAPES = 2 * DIAGRAM_SHAPE_PADDING;
        public const int DIAGRAM_NUM_VERTICAL_CONNECTORS = 9;
        public const int DIAGRAM_NUM_HORIZONTAL_CONNECTORS = 9;
        public const int DIAGRAM_OFF_STRATUM_CUE_SIZE = 14;
        public const int DIAGRAM_LANES_BETWEEN_SHAPES = 11;
        public const int ZOOM_SAFE_PEN_WIDTH = -1;

        public static Color DIAGRAM_SHAPE_TEXT_COLOR = Color.Black;
        public static Color DIAGRAM_SHAPE_READONLY_TEXT_COLOR = Color.Gray;
        public static Color DIAGRAM_SHAPE_BORDER_COLOR = Color.Gray;
        public static Color DIAGRAM_SHAPE_READONLY_BORDER_COLOR = Color.LightGray;
        public static Color DIAGRAM_SHAPE_BACKGROUND_COLOR = Color.FromArgb(255, 240, 240, 240);
        public static Color DIAGRAM_FLOW_PATHWAY_LINE_COLOR = Color.CornflowerBlue;
        public static Font DIAGRAM_SHAPE_NORMAL_FONT = new Font("Segoe UI", 9.0F, FontStyle.Regular);
    }
}
