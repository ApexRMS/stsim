// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.IO;
using System.Data;
using System.Text;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    internal partial class STSimUpdates : UpdateProvider
    {
        public override void PerformUpdate(DataStore store, int currentSchemaVersion)
        {
            PerformUpdateInternal(store, currentSchemaVersion);

#if DEBUG

            //Verify that all expected indexes exist after the update because it is easy to forget to recreate them after 
            //adding a column to an existing table (which requires the table to be recreated if you want to preserve column order.)

            ASSERT_INDEX_EXISTS(store, "STSim_Transition");
            ASSERT_INDEX_EXISTS(store, "STSim_InitialConditionsNonSpatialDistribution");
            ASSERT_INDEX_EXISTS(store, "STSim_TransitionTarget");
            ASSERT_INDEX_EXISTS(store, "STSim_TransitionMultiplierValue");
            ASSERT_INDEX_EXISTS(store, "STSim_StateAttributeValue");
            ASSERT_INDEX_EXISTS(store, "STSim_TransitionAttributeValue");
            ASSERT_INDEX_EXISTS(store, "STSim_TransitionAttributeTarget");
            ASSERT_INDEX_EXISTS(store, "STSim_DistributionValue");
            ASSERT_INDEX_EXISTS(store, "STSim_OutputStratum");
            ASSERT_INDEX_EXISTS(store, "STSim_OutputStratumState");
            ASSERT_INDEX_EXISTS(store, "STSim_OutputStratumTransition");
            ASSERT_INDEX_EXISTS(store, "STSim_OutputStratumTransitionState");
            ASSERT_INDEX_EXISTS(store, "STSim_OutputStateAttribute");
            ASSERT_INDEX_EXISTS(store, "STSim_OutputTransitionAttribute");

#endif
        }

        private static void PerformUpdateInternal(DataStore store, int currentSchemaVersion)
        {
            if (currentSchemaVersion < 1)
            {
                STSIM0000001(store);
                STSIM0000002(store);
            }

            if (currentSchemaVersion < 2)
            {
                STSIM0000003(store);
                STSIM0000004(store);
            }

            if (currentSchemaVersion < 3)
            {
                STSIM0000005(store);
                STSIM0000006(store);
            }

            if (currentSchemaVersion < 4)
            {
                STSIM0000007(store);
            }

            if (currentSchemaVersion < 5)
            {
                STSIM0000008(store);
            }

            if (currentSchemaVersion < 6)
            {
                STSIM0000009(store);
            }

            if (currentSchemaVersion < 7)
            {
                STSIM0000010(store);
            }

            if (currentSchemaVersion < 8)
            {
                STSIM0000011(store);
            }

            if (currentSchemaVersion < 9)
            {
                STSIM0000012(store);
            }

            if (currentSchemaVersion < 10)
            {
                STSIM0000013(store);
            }

            if (currentSchemaVersion < 11)
            {
                STSIM0000014(store);
            }

            if (currentSchemaVersion < 12)
            {
                STSIM0000015(store);
            }

            if (currentSchemaVersion < 13)
            {
                STSIM0000016(store);
            }

            if (currentSchemaVersion < 14)
            {
                STSIM0000017(store);
            }

            if (currentSchemaVersion < 15)
            {
                STSIM0000018(store);
            }

            if (currentSchemaVersion < 16)
            {
                STSIM0000019(store);
            }

            if (currentSchemaVersion < 17)
            {
                STSIM0000020(store);
            }

            if (currentSchemaVersion < 18)
            {
                STSIM0000021(store);
            }

            if (currentSchemaVersion < 19)
            {
                STSIM0000022(store);
            }

            if (currentSchemaVersion < 20)
            {
                STSIM0000023(store);
            }

            if (currentSchemaVersion < 21)
            {
                STSIM0000024(store);
            }

            if (currentSchemaVersion < 22)
            {
                STSIM0000025(store);
            }

            if (currentSchemaVersion < 23)
            {
                STSIM0000026(store);
            }

            if (currentSchemaVersion < 24)
            {
                STSIM0000027(store);
            }

            if (currentSchemaVersion < 25)
            {
                STSIM0000028(store);
            }

            if (currentSchemaVersion < 26)
            {
                STSIM0000029(store);
            }

            if (currentSchemaVersion < 27)
            {
                STSIM0000030(store);
            }

            if (currentSchemaVersion < 28)
            {
                STSIM0000031(store);
            }

            if (currentSchemaVersion < 29)
            {
                STSIM0000032(store);
            }

            if (currentSchemaVersion < 30)
            {
                UpdateSTSimTables_SSIM_V_1(store);
                UpdateStockFlowTables_SSIM_V_1(store);
                UpdateEcologicalDepartureTables_SSIM_V_1(store);
                UpdateTransitionProbabilityEstimatorTables_SSIM_V_1(store);
                UpdateDynamicMultiplierTables_SSIM_V_1(store);
                RenameInputDirectories_SSIM_V_1(store);
            }

            //At this point, the library has been updated to be compatible with
            //SyncroSim version 1.

            if (currentSchemaVersion < 31)
            {
                STSIM0000033(store);
            }

            if (currentSchemaVersion < 32)
            {
                STSIM0000034(store);
            }

            //Due to legacy mistakes, the update function names have departed from the version number.
            //We are going to fix that here with a dummy update for clarity.  From this point on, the
            //ST-Sim schema version number will be 40 or greater.

            if (currentSchemaVersion < 40)
            {
                STSIM0000040(store);
            }

            if (currentSchemaVersion < 41)
            {
                STSIM0000041(store);
            }

            if (currentSchemaVersion < 42)
            {
                STSIM0000042(store);
            }

            if (currentSchemaVersion < 43)
            {
                STSIM0000043(store);
            }

            if (currentSchemaVersion < 44)
            {
                STSIM0000044(store);
            }

            if (currentSchemaVersion < 45)
            {
                STSIM0000045(store);
            }

            if (currentSchemaVersion < 46)
            {
                STSIM0000046(store);
            }

            if (currentSchemaVersion < 47)
            {
                STSIM0000047(store);
            }

            if (currentSchemaVersion < 48)
            {
                STSIM0000048(store);
            }

            if (currentSchemaVersion < 49)
            {
                STSIM0000049(store);
            }

            if (currentSchemaVersion < 50)
            {
                STSIM0000050(store);
            }

            if (currentSchemaVersion < 51)
            {
                STSIM0000051(store);
            }

            if (currentSchemaVersion < 52)
            {
                STSIM0000052(store);
            }

            if (currentSchemaVersion < 53)
            {
                STSIM0000053(store);
            }

            if (currentSchemaVersion < 54)
            {
                STSIM0000054(store);
            }

            if (currentSchemaVersion < 55)
            {
                STSIM0000055(store);
            }

            if (currentSchemaVersion < 56)
            {
                STSIM0000056(store);
            }

            if (currentSchemaVersion < 57)
            {
                STSIM0000057(store);
            }

            if (currentSchemaVersion < 58)
            {
                STSIM0000058(store);
            }

            if (currentSchemaVersion < 59)
            {
                STSIM0000059(store);
            }

            if (currentSchemaVersion < 60)
            {
                STSIM0000060(store);
            }

            if (currentSchemaVersion < 61)
            {
                STSIM0000061(store);
            }

            if (currentSchemaVersion < 62)
            {
                STSIM0000062(store);
            }

            if (currentSchemaVersion < 63)
            {
                STSIM0000063(store);
            }

            if (currentSchemaVersion < 64)
            {
                STSIM0000064(store);
            }

            if (currentSchemaVersion < 65)
            {
                STSIM0000065(store);
            }

            if (currentSchemaVersion < 66)
            {
                STSIM0000066(store);
            }

            if (currentSchemaVersion < 67)
            {
                STSIM0000067(store);
            }

            if (currentSchemaVersion < 68)
            {
                STSIM0000068(store);
            }
        }

        /// <summary>
        /// STSIM0000001
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// In this update, we want to change the TransitionTypeID column to TransitionGroupID.  Since this column
        /// has its AllowDBNull property set to False, we can't just change the name and delete the values.  Instead
        /// we need to delete the entire table which will cause the system to recreate it as the library loads.
        /// </remarks>
        private static void STSIM0000001(DataStore store)
        {
            store.ExecuteNonQuery("DROP TABLE ST_TransitionMultiplierValues");
        }

        /// <summary>
        /// STSIM0000002
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// In this update, we want to change the TransitionTypeID column to TransitionGroupID and we want to delete
        /// the MultiplierType column.  Since the TransitionTypeID column has its AllowDBNull property set to False, 
        /// we can't just change the name and delete the values.  Instead we need to delete the entire table which 
        /// will cause the system to recreate it as the library loads.
        /// </remarks>
        private static void STSIM0000002(DataStore store)
        {
            if (store.TableExists("ST_TransitionSpatialMultiplier"))
            {
                store.ExecuteNonQuery("DROP TABLE ST_TransitionSpatialMultiplier");
            }
        }

        /// <summary>
        /// STSIM0000003
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// In this update we will add support for transition attribute types as follows:
        /// 
        /// (1.) Rename the table ST_AttributeType to ST_StateAttributeType
        /// (2.) Rename the column ST_AttributeTypeID ST_StateAttributeTypeID in the table ST_StateAttributeType
        /// (3.) Rename the AttributeTypeID column to StateAttributeTypeID in the table ST_OutputStratumStateAttribute
        /// 
        /// Note that some databases such as SQLite don't support column renaming, so we need to 
        /// select the values from the old table into the new, and then drop the old.
        /// 
        /// Also note that the required transition attribute type tables will be created automatically by the system
        /// </remarks>
        private static void STSIM0000003(DataStore store)
        {
            //(1.) and (2.) above
            //-------------------
            store.ExecuteNonQuery("CREATE TABLE ST_StateAttributeType(ST_StateAttributeTypeID INTEGER, ProjectID INTEGER, [Name] TEXT(255), AttributeGroupID INTEGER, Units TEXT(50), Description TEXT(255))");
            store.ExecuteNonQuery("INSERT INTO ST_StateAttributeType(ST_StateAttributeTypeID, ProjectID, [Name], AttributeGroupID, Units, Description) SELECT ST_AttributeTypeID, ProjectID, [Name], AttributeGroupID, Units, Description FROM ST_AttributeType");
            store.ExecuteNonQuery("DROP TABLE ST_AttributeType");

            //(3.) above
            //-------------------
            store.ExecuteNonQuery("DROP INDEX ST_OutputStratumStateAttribute_Index");
            store.ExecuteNonQuery("ALTER TABLE ST_OutputStratumStateAttribute RENAME TO ST_OutputStratumStateAttributeTEMP");
            store.ExecuteNonQuery("CREATE TABLE ST_OutputStratumStateAttribute(ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, StateAttributeTypeID INTEGER, Amount DOUBLE)");
            store.ExecuteNonQuery("INSERT INTO ST_OutputStratumStateAttribute(ScenarioID, Iteration, Timestep, StratumID, StateAttributeTypeID, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, AttributeTypeID, Amount FROM ST_OutputStratumStateAttributeTEMP");
            store.ExecuteNonQuery("DROP TABLE ST_OutputStratumStateAttributeTEMP");
            store.ExecuteNonQuery("CREATE INDEX ST_OutputStratumStateAttribute_Index ON ST_OutputStratumStateAttribute(ScenarioID, Iteration, Timestep)");
        }

        /// <summary>
        /// STSIM0000004
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// In this update we will rename the following tables so their names respect the established naming conventions.
        /// 
        /// ST_TransitionMultiplierValues -> ST_TransitionMultiplierValue
        /// ST_StateAttributeValues       -> ST_StateAttributeValue
        /// 
        /// Note that some databases such as SQLite don't support column renaming, so we need to 
        /// select the values from the old table into the new, and then drop the old.
        /// 
        /// Also note that in STSIM0000001 we drop the table ST_TransitionMultiplierValues to force an automatic recreation.
        /// Because of this, the table will not exist if STSIM0000001 is run in conjunction with STSIM0000004.  If the table
        /// doesn't exist then we don't need to create the new table since it will be created by the system.
        /// </remarks>
        private static void STSIM0000004(DataStore store)
        {
            //ST_TransitionMultiplierValues -> ST_TransitionMultiplierValue
            //-------------------------------------------------------------
            if (store.TableExists("ST_TransitionMultiplierValues"))
            {
                store.ExecuteNonQuery("CREATE TABLE ST_TransitionMultiplierValue(ST_TransitionMultiplierValueID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, TransitionGroupID INTEGER, StratumID INTEGER, TransitionMultiplierTypeID INTEGER, Iteration INTEGER, Timestep INTEGER, Mean DOUBLE, SD DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO ST_TransitionMultiplierValue(ProjectID, ScenarioID, TransitionGroupID, StratumID, TransitionMultiplierTypeID, Iteration, Timestep, Mean, SD) SELECT ProjectID, ScenarioID, TransitionGroupID, StratumID, TransitionMultiplierTypeID, Iteration, Timestep, Mean, SD FROM ST_TransitionMultiplierValues");
                store.ExecuteNonQuery("DROP TABLE ST_TransitionMultiplierValues");
            }

            //ST_StateAttributeValues       -> ST_StateAttributeValue
            //-------------------------------------------------------
            store.ExecuteNonQuery("CREATE TABLE ST_StateAttributeValue(ST_StateAttributeValueID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, StateAttributeTypeID INTEGER, StratumID INTEGER, StateClassID INTEGER, AgeMin INTEGER, AgeMax INTEGER, Value DOUBLE)");
            store.ExecuteNonQuery("INSERT INTO ST_StateAttributeValue(ProjectID, ScenarioID, StateAttributeTypeID, StratumID, StateClassID, AgeMin, AgeMax, Value) SELECT ProjectID, ScenarioID, AttributeTypeID, StratumID, StateClassID, AgeMin, AgeMax, Value FROM ST_StateAttributeValues");
            store.ExecuteNonQuery("DROP TABLE ST_StateAttributeValues");
        }

        /// <summary>
        /// STSIM0000005
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update adds transition attribute support to Output Options
        /// </remarks>
        private static void STSIM0000005(DataStore store)
        {
            store.ExecuteNonQuery("ALTER TABLE ST_OutputOptions RENAME TO ST_OutputOptionsTEMP");

            store.ExecuteNonQuery("CREATE TABLE ST_OutputOptions ( " + "ST_OutputOptionsID          INTEGER PRIMARY KEY AUTOINCREMENT, " + "ProjectID                   INTEGER, " + "ScenarioID                  INTEGER, " + "SummaryOutputSC             INTEGER, " + "SummaryOutputSCTimesteps    INTEGER, " + "SummaryOutputTR             INTEGER, " + "SummaryOutputTRTimesteps    INTEGER, " + "SummaryOutputTRIntervalMean INTEGER, " + "SummaryOutputTRSC           INTEGER, " + "SummaryOutputTRSCTimesteps  INTEGER, " + "SummaryOutputSA              INTEGER, " + "SummaryOutputSATimesteps     INTEGER, " + "SummaryOutputTA              INTEGER, " + "SummaryOutputTATimesteps     INTEGER, " + "RasterOutputSC              INTEGER, " + "RasterOutputSCTimesteps     INTEGER, " + "RasterOutputTR              INTEGER, " + "RasterOutputTRTimesteps     INTEGER, " + "RasterOutputAge             INTEGER, " + "RasterOutputAgeTimesteps    INTEGER, " + "RasterOutputTST             INTEGER, " + "RasterOutputTSTTimesteps    INTEGER, " + "RasterOutputST              INTEGER, " + "RasterOutputSTTimesteps     INTEGER)");

            store.ExecuteNonQuery("INSERT INTO ST_OutputOptions (" + "ProjectID," + "ScenarioID," + "SummaryOutputSC," + "SummaryOutputSCTimesteps," + "SummaryOutputTR," + "SummaryOutputTRTimesteps," + "SummaryOutputTRIntervalMean," + "SummaryOutputTRSC," + "SummaryOutputTRSCTimesteps," + "SummaryOutputSA," + "SummaryOutputSATimesteps," + "RasterOutputSC," + "RasterOutputSCTimesteps," + "RasterOutputTR," + "RasterOutputTRTimesteps," + "RasterOutputAge," + "RasterOutputAgeTimesteps," + "RasterOutputTST," + "RasterOutputTSTTimesteps," + "RasterOutputST," + "RasterOutputSTTimesteps)" + " SELECT " + "ProjectID," + "ScenarioID," + "SummaryOutputSC," + "SummaryOutputSCTimesteps," + "SummaryOutputTR," + "SummaryOutputTRTimesteps," + "SummaryOutputTRIntervalMean," + "SummaryOutputTRSC, " + "SummaryOutputTRSCTimesteps," + "SummaryOutputSA," + "SummaryOutputSATimesteps," + "RasterOutputSC," + "RasterOutputSCTimesteps," + "RasterOutputTR," + "RasterOutputTRTimesteps," + "RasterOutputAge," + "RasterOutputAgeTimesteps," + "RasterOutputTST," + "RasterOutputTSTTimesteps," + "RasterOutputST," + "RasterOutputSTTimesteps" + " FROM ST_OutputOptionsTEMP");

            store.ExecuteNonQuery("DROP TABLE ST_OutputOptionsTEMP");
        }

        /// <summary>
        /// STSIM0000006
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update renames the Output?Attribute tables so that they do not contain the word 'Stratum'.
        /// Note that if this update may be applied to libraries that do not yet have a transition attribute table.
        /// </remarks>
        private static void STSIM0000006(DataStore store)
        {
            store.ExecuteNonQuery("DROP INDEX ST_OutputStratumStateAttribute_Index");
            store.ExecuteNonQuery("ALTER TABLE ST_OutputStratumStateAttribute RENAME TO ST_OutputStateAttribute");
            store.ExecuteNonQuery("CREATE INDEX ST_OutputStateAttribute_Index ON ST_OutputStateAttribute(ScenarioID, Iteration, Timestep)");

            if (store.TableExists("ST_OutputStratumTransitionAttribute"))
            {
                store.ExecuteNonQuery("DROP INDEX ST_OutputStratumTransitionAttribute_Index");
                store.ExecuteNonQuery("ALTER TABLE ST_OutputStratumTransitionAttribute RENAME TO ST_OutputTransitionAttribute");
                store.ExecuteNonQuery("CREATE INDEX ST_OutputTransitionAttribute_Index ON ST_OutputTransitionAttribute(ScenarioID, Iteration, Timestep)");
            }
        }

        /// <summary>
        /// STSIM0000007
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update will add Distribution support to Transition Targets.  Specifically, it will 
        /// add the folliwng columns to the Transition Target table:
        /// 
        /// SD           : DOUBLE     //Standard Deviation
        /// Distribution : Integer
        /// MinimumValue : Integer
        /// MaximumValue : Integer
        /// </remarks>
        private static void STSIM0000007(DataStore store)
        {
            store.ExecuteNonQuery("ALTER TABLE ST_TransitionTarget RENAME TO ST_TransitionTargetTEMP");
            store.ExecuteNonQuery("CREATE TABLE ST_TransitionTarget (ST_TransitionTargetID  INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, StratumID INTEGER, Timestep INTEGER, TransitionGroupID INTEGER, Amount DOUBLE, MinimumValue DOUBLE, MaximumValue DOUBLE, SD DOUBLE, Distribution INTEGER)");
            store.ExecuteNonQuery("INSERT INTO ST_TransitionTarget(ProjectID, ScenarioID, StratumID, Timestep, TransitionGroupID, Amount) SELECT ProjectID, ScenarioID, StratumID, Timestep, TransitionGroupID, Amount FROM ST_TransitionTargetTEMP");
            store.ExecuteNonQuery("DROP TABLE ST_TransitionTargetTEMP");
        }

        /// <summary>
        /// STSIM0000008
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update will add Modeling Zone support to ST-Sim as follows:
        /// 
        /// (1.)  Add primary and secondary stratum label columns to the Terminology table
        /// (2.)  Update all projects so that their terminology table contains the new values
        /// (3.)  Add a SecondaryStratum column to the Initial Conditions Distribution table
        /// (4.)  Add a SecondaryStratum column to the Transition Targets table
        /// (5.)  Add a SecondaryStratum column to the Transition Attribute Targets table
        /// (6.)  Add a SecondaryStratum column to the Transition Multiplier Values table
        /// (7.)  Add a SecondaryStratumFilename column to the Initial Conditions Spatial table
        /// (8.)  Add a SecondaryStratum column to the Output Stratum State table
        /// (9.)  Add a SecondaryStratum column to the Output Stratum Transition table
        /// (10.) Add a SecondaryStratum column to the Output Stratum Transition State table
        /// (11.) Add a SecondaryStratum column to the Output State Attribute table
        /// (12.) Add a SecondaryStratum column to the Output Transition Attribute table
        /// 
        /// NOTE: The SecondaryStratum table does not need to be created here because the system will auto create missing tables.
        /// NOTE: The Transition Attribute Target table may not yet exist.  If so, we can skip the alteration below.
        /// NOTE: The Output Transition Attribute table may not yet exist.  If so, we can skip the alteration below.
        /// NOTE: The Output Transition Multiplier Value table may not yet exist.  If so, we can skip the alteration below.
        /// NOTE: The indexes on the output tables need to be recreated if the table is recreated.
        /// 
        /// </remarks>
        private static void STSIM0000008(DataStore store)
        {
            //(1.) and (2.) above
            store.ExecuteNonQuery("ALTER TABLE ST_Terminology RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_Terminology (ST_TerminologyID INTEGER PRIMARY KEY, ProjectID INTEGER, AmountLabel TEXT( 50 ), AmountUnits INTEGER, StateLabelX TEXT( 50 ), StateLabelY TEXT( 50 ), PrimaryStratumLabel TEXT( 50 ), SecondaryStratumLabel Text(50))");
            store.ExecuteNonQuery("INSERT INTO ST_Terminology (ST_TerminologyID, ProjectID, AmountLabel, AmountUnits, StateLabelX, StateLabelY, PrimaryStratumLabel, SecondaryStratumLabel) SELECT ST_TerminologyID, ProjectID, AmountLabel, AmountUnits, StateLabelX, StateLabelY, 'Vegetation Type', 'Planning Zone' FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

            //(3.) Above
            store.ExecuteNonQuery("ALTER TABLE ST_InitialConditionsNonSpatialDistribution RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_InitialConditionsNonSpatialDistribution (ST_InitialConditionsNonSpatialDistributionID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, StateClassID INTEGER, AgeMin INTEGER, AgeMax INTEGER, RelativeAmount DOUBLE)");
            store.ExecuteNonQuery("INSERT INTO ST_InitialConditionsNonSpatialDistribution(ProjectID, ScenarioID, StratumID, StateClassID, AgeMin, AgeMax, RelativeAmount) SELECT ProjectID, ScenarioID, StratumID, StateClassID, AgeMin, AgeMax, RelativeAmount FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

            //(4.) Above
            store.ExecuteNonQuery("ALTER TABLE ST_TransitionTarget RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_TransitionTarget (ST_TransitionTargetID  INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, Timestep INTEGER, TransitionGroupID INTEGER, Amount DOUBLE, MinimumValue DOUBLE, MaximumValue DOUBLE, SD DOUBLE, Distribution INTEGER)");
            store.ExecuteNonQuery("INSERT INTO ST_TransitionTarget(ProjectID, ScenarioID, StratumID, Timestep, TransitionGroupID, Amount, MinimumValue, MaximumValue, SD, Distribution) SELECT ProjectID, ScenarioID, StratumID, Timestep, TransitionGroupID, Amount, MinimumValue, MaximumValue, SD, Distribution FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

            //(5.) Above
            if (store.TableExists("ST_TransitionAttributeTarget"))
            {
                store.ExecuteNonQuery("ALTER TABLE ST_TransitionAttributeTarget RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE ST_TransitionAttributeTarget (ST_TransitionAttributeTargetID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, Timestep INTEGER, TransitionAttributeTypeID INTEGER, Amount DOUBLE, MinimumValue DOUBLE, MaximumValue DOUBLE, SD DOUBLE, Distribution INTEGER)");
                store.ExecuteNonQuery("INSERT INTO ST_TransitionAttributeTarget(ProjectID, ScenarioID, StratumID, Timestep, TransitionAttributeTypeID, Amount, MinimumValue, MaximumValue, SD, Distribution) SELECT ProjectID, ScenarioID, StratumID, Timestep, TransitionAttributeTypeID, Amount, MinimumValue, MaximumValue, SD, Distribution FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            //(6.) Above
            if (store.TableExists("ST_TransitionMultiplierValue"))
            {
                store.ExecuteNonQuery("ALTER TABLE ST_TransitionMultiplierValue RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE ST_TransitionMultiplierValue (ST_TransitionMultiplierValueID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, TransitionGroupID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TransitionMultiplierTypeID INTEGER, Iteration INTEGER, Timestep INTEGER, Mean DOUBLE, SD DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO ST_TransitionMultiplierValue(ProjectID, ScenarioID, TransitionGroupID, StratumID, TransitionMultiplierTypeID, Iteration, Timestep, Mean, SD) SELECT ProjectID, ScenarioID, TransitionGroupID, StratumID, TransitionMultiplierTypeID, Iteration, Timestep, Mean, SD FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            //(7.) Above
            store.ExecuteNonQuery("ALTER TABLE ST_InitialConditionsSpatial RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_InitialConditionsSpatial (ST_InitialConditionsSpatialID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, NumRows INTEGER, NumColumns INTEGER, CellSize SINGLE, CellSizeUnits TEXT( 20 ), CellArea DOUBLE, CellAreaOverride INTEGER, XLLCorner SINGLE, YLLCorner SINGLE, SRS TEXT(1024), StratumFileName TEXT(255), SecondaryStratumFilename TEXT(255), StateClassFilename TEXT(255), AgeFilename TEXT(255))");
            store.ExecuteNonQuery("INSERT INTO ST_InitialConditionsSpatial(ProjectID, ScenarioID, NumRows, NumColumns, CellSize, CellSizeUnits, CellArea, CellAreaOverride, XLLCorner, YLLCorner, SRS, StratumFileName, StateClassFileName, AgeFileName) SELECT ProjectID, ScenarioID, NumRows, NumColumns, CellSize, CellSizeUnits, CellArea, CellAreaOverride, XLLCorner, YLLCorner, SRS, StratumFilename, StateClassFilename, AgeFilename FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

            //(8.) Above
            store.ExecuteNonQuery("DROP INDEX ST_OutputStratumState_Index");
            store.ExecuteNonQuery("ALTER TABLE ST_OutputStratumState RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_OutputStratumState (ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, StateClassID INTEGER, Amount DOUBLE)");
            store.ExecuteNonQuery("INSERT INTO ST_OutputStratumState(ScenarioID, Iteration, Timestep, StratumID, StateClassID, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, StateClassID, Amount FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            store.ExecuteNonQuery("CREATE INDEX ST_OutputStratumState_Index ON ST_OutputStratumState(ScenarioID, Iteration, Timestep)");

            //(9.) Above
            store.ExecuteNonQuery("DROP INDEX ST_OutputStratumTransition_Index");
            store.ExecuteNonQuery("ALTER TABLE ST_OutputStratumTransition RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_OutputStratumTransition (ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TransitionGroupID INTEGER, Amount DOUBLE)");
            store.ExecuteNonQuery("INSERT INTO ST_OutputStratumTransition(ScenarioID, Iteration, Timestep, StratumID, TransitionGroupID, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, TransitionGroupID, Amount FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            store.ExecuteNonQuery("CREATE INDEX ST_OutputStratumTransition_Index ON ST_OutputStratumTransition(ScenarioID, Iteration, Timestep)");

            //(10.) Above
            store.ExecuteNonQuery("DROP INDEX ST_OutputStratumTransitionState_Index");
            store.ExecuteNonQuery("ALTER TABLE ST_OutputStratumTransitionState RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_OutputStratumTransitionState (ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TransitionTypeID INTEGER, StateClassID INTEGER, EndStateClassID INTEGER, Amount DOUBLE)");
            store.ExecuteNonQuery("INSERT INTO ST_OutputStratumTransitionState(ScenarioID, Iteration, Timestep, StratumID, TransitionTypeID, StateClassID, EndStateClassID, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, TransitionTypeID, StateClassID, EndStateClassID, Amount FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            store.ExecuteNonQuery("CREATE INDEX ST_OutputStratumTransitionState_Index ON ST_OutputStratumTransitionState(ScenarioID, Iteration, Timestep)");

            //(11.) Above
            store.ExecuteNonQuery("DROP INDEX ST_OutputStateAttribute_Index");
            store.ExecuteNonQuery("ALTER TABLE ST_OutputStateAttribute RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_OutputStateAttribute (ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, StateAttributeTypeID INTEGER, Amount DOUBLE)");
            store.ExecuteNonQuery("INSERT INTO ST_OutputStateAttribute(ScenarioID, Iteration, Timestep, StratumID, StateAttributeTypeID, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, StateAttributeTypeID, Amount FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            store.ExecuteNonQuery("CREATE INDEX ST_OutputStateAttribute_Index ON ST_OutputStateAttribute(ScenarioID, Iteration, Timestep)");

            //(12.) Above
            if (store.TableExists("ST_OutputTransitionAttribute"))
            {
                store.ExecuteNonQuery("DROP INDEX ST_OutputTransitionAttribute_Index");
                store.ExecuteNonQuery("ALTER TABLE ST_OutputTransitionAttribute RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE ST_OutputTransitionAttribute (ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TransitionAttributeTypeID INTEGER, Amount DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO ST_OutputTransitionAttribute(ScenarioID, Iteration, Timestep, StratumID, TransitionAttributeTypeID, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, TransitionAttributeTypeID, Amount FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
                store.ExecuteNonQuery("CREATE INDEX ST_OutputTransitionAttribute_Index ON ST_OutputTransitionAttribute(ScenarioID, Iteration, Timestep)");
            }
        }

        /// <summary>
        /// STSIM0000009
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// A bug in the schema generation code was causing each project record to be created with an AUTOINCRMENT primary key.  Although this does not
        /// create problems with the application, the AUTOINCREMENT constraint is incorrect and we are going to correct all affected tables here.
        /// </remarks>
        private static void STSIM0000009(DataStore store)
        {
            //Attribute Group
            store.ExecuteNonQuery("ALTER TABLE ST_AttributeGroup RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_AttributeGroup(ST_AttributeGroupID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT(255), Description TEXT(255))");
            store.ExecuteNonQuery("INSERT INTO ST_AttributeGroup(ST_AttributeGroupID, ProjectID, Name, Description) SELECT ST_AttributeGroupID, ProjectID, Name, Description FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

            //Patch Prioritization
            store.ExecuteNonQuery("ALTER TABLE ST_PatchPrioritization RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_PatchPrioritization(ST_PatchPrioritizationID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT(255))");
            store.ExecuteNonQuery("INSERT INTO ST_PatchPrioritization(ST_PatchPrioritizationID, ProjectID, Name) SELECT ST_PatchPrioritizationID, ProjectID, Name FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

            //State Attribute Type
            store.ExecuteNonQuery("ALTER TABLE ST_StateAttributeType RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_StateAttributeType (ST_StateAttributeTypeID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT( 255 ), AttributeGroupID INTEGER, Units TEXT( 50 ), Description Text(255))");
            store.ExecuteNonQuery("INSERT INTO ST_StateAttributeType(ST_StateAttributeTypeID, ProjectID, Name, AttributeGroupID, Units, Description) SELECT ST_StateAttributeTypeID, ProjectID, Name, AttributeGroupID, Units, Description FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

            //State Class
            store.ExecuteNonQuery("ALTER TABLE ST_StateClass RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_StateClass (ST_StateClassID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT( 255 ), StateLabelXID INTEGER, StateLabelYID INTEGER, ID INTEGER, Description TEXT( 255 ))");
            store.ExecuteNonQuery("INSERT INTO ST_StateClass(ST_StateClassID, ProjectID, Name, StateLabelXID, StateLabelYID, ID, Description) SELECT ST_StateClassID, ProjectID, Name, StateLabelXID, StateLabelYID, ID, Description FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

            //State Label X
            store.ExecuteNonQuery("ALTER TABLE ST_StateLabelX RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_StateLabelX(ST_StateLabelXID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT(255), Description TEXT(255))");
            store.ExecuteNonQuery("INSERT INTO ST_StateLabelX(ST_StateLabelXID, ProjectID, Name, Description) SELECT ST_StateLabelXID, ProjectID, Name, Description FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

            //State Label Y
            store.ExecuteNonQuery("ALTER TABLE ST_StateLabelY RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_StateLabelY(ST_StateLabelYID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT(255), Description TEXT(255))");
            store.ExecuteNonQuery("INSERT INTO ST_StateLabelY(ST_StateLabelYID, ProjectID, Name, Description) SELECT ST_StateLabelYID, ProjectID, Name, Description FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

            //Stratum
            store.ExecuteNonQuery("ALTER TABLE ST_Stratum RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_Stratum(ST_StratumID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT(255), ID INTEGER, Description TEXT(255))");
            store.ExecuteNonQuery("INSERT INTO ST_Stratum(ST_StratumID, ProjectID, Name, ID, Description) SELECT ST_StratumID, ProjectID, Name, ID, Description FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

            //Transition Group
            store.ExecuteNonQuery("ALTER TABLE ST_TransitionGroup RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_TransitionGroup(ST_TransitionGroupID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT(255), Description TEXT(255))");
            store.ExecuteNonQuery("INSERT INTO ST_TransitionGroup(ST_TransitionGroupID, ProjectID, Name, Description) SELECT ST_TransitionGroupID, ProjectID, Name, Description FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

            //Transition Multiplier Type
            store.ExecuteNonQuery("ALTER TABLE ST_TransitionMultiplierType RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_TransitionMultiplierType(ST_TransitionMultiplierTypeID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT(255))");
            store.ExecuteNonQuery("INSERT INTO ST_TransitionMultiplierType(ST_TransitionMultiplierTypeID, ProjectID, Name) SELECT ST_TransitionMultiplierTypeID, ProjectID, Name FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

            //Transition Type
            store.ExecuteNonQuery("ALTER TABLE ST_TransitionType RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_TransitionType(ST_TransitionTypeID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT(255), ID INTEGER, Description TEXT(255))");
            store.ExecuteNonQuery("INSERT INTO ST_TransitionType(ST_TransitionTypeID, ProjectID, Name, ID, Description) SELECT ST_TransitionTypeID, ProjectID, Name, ID, Description FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

            //Transition Type Group
            store.ExecuteNonQuery("ALTER TABLE ST_TransitionTypeGroup RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_TransitionTypeGroup(ST_TransitionTypeGroupID INTEGER PRIMARY KEY, ProjectID INTEGER, TransitionTypeID INTEGER, TransitionGroupID INTEGER, IsSecondary INTEGER)");
            store.ExecuteNonQuery("INSERT INTO ST_TransitionTypeGroup(ST_TransitionTypeGroupID, ProjectID, TransitionTypeID, TransitionGroupID, IsSecondary) SELECT ST_TransitionTypeGroupID, ProjectID, TransitionTypeID, TransitionGroupID, IsSecondary FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
        }

        /// <summary>
        /// STSIM0000010
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update will add color columns to the State Class and Transition Type tables
        /// </remarks>
        private static void STSIM0000010(DataStore store)
        {
            //State Class
            store.ExecuteNonQuery("ALTER TABLE ST_StateClass RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_StateClass (ST_StateClassID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT( 255 ), StateLabelXID INTEGER, StateLabelYID INTEGER, ID INTEGER, Color TEXT(20), Description TEXT( 255 ))");
            store.ExecuteNonQuery("INSERT INTO ST_StateClass(ST_StateClassID, ProjectID, Name, StateLabelXID, StateLabelYID, ID, Description) SELECT ST_StateClassID, ProjectID, Name, StateLabelXID, StateLabelYID, ID, Description FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

            //Transition Type
            store.ExecuteNonQuery("ALTER TABLE ST_TransitionType RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_TransitionType(ST_TransitionTypeID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT(255), ID INTEGER, Color TEXT(20), Description TEXT(255))");
            store.ExecuteNonQuery("INSERT INTO ST_TransitionType(ST_TransitionTypeID, ProjectID, Name, ID, Description) SELECT ST_TransitionTypeID, ProjectID, Name, ID, Description FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
        }

        /// <summary>
        /// STSIM0000011
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update adds raster attribute support to Output Options
        /// </remarks>
        private static void STSIM0000011(DataStore store)
        {
            store.ExecuteNonQuery("ALTER TABLE ST_OutputOptions RENAME TO ST_OutputOptionsTEMP");

            store.ExecuteNonQuery("CREATE TABLE ST_OutputOptions ( " + "ST_OutputOptionsID          INTEGER PRIMARY KEY AUTOINCREMENT, " + "ProjectID                   INTEGER, " + "ScenarioID                  INTEGER, " + "SummaryOutputSC             INTEGER, " + "SummaryOutputSCTimesteps    INTEGER, " + "SummaryOutputTR             INTEGER, " + "SummaryOutputTRTimesteps    INTEGER, " + "SummaryOutputTRIntervalMean INTEGER, " + "SummaryOutputTRSC           INTEGER, " + "SummaryOutputTRSCTimesteps  INTEGER, " + "SummaryOutputSA             INTEGER, " + "SummaryOutputSATimesteps    INTEGER, " + "SummaryOutputTA             INTEGER, " + "SummaryOutputTATimesteps    INTEGER, " + "RasterOutputSC              INTEGER, " + "RasterOutputSCTimesteps     INTEGER, " + "RasterOutputTR              INTEGER, " + "RasterOutputTRTimesteps     INTEGER, " + "RasterOutputAge             INTEGER, " + "RasterOutputAgeTimesteps    INTEGER, " + "RasterOutputTST             INTEGER, " + "RasterOutputTSTTimesteps    INTEGER, " + "RasterOutputST              INTEGER, " + "RasterOutputSTTimesteps     INTEGER, " + "RasterOutputSA              INTEGER, " + "RasterOutputSATimesteps     INTEGER, " + "RasterOutputTA              INTEGER, " + "RasterOutputTATimesteps     INTEGER)");

            store.ExecuteNonQuery("INSERT INTO ST_OutputOptions (" + "ProjectID," + "ScenarioID," + "SummaryOutputSC," + "SummaryOutputSCTimesteps," + "SummaryOutputTR," + "SummaryOutputTRTimesteps," + "SummaryOutputTRIntervalMean," + "SummaryOutputTRSC," + "SummaryOutputTRSCTimesteps," + "SummaryOutputSA," + "SummaryOutputSATimesteps," + "SummaryOutputTA," + "SummaryOutputTATimesteps," + "RasterOutputSC," + "RasterOutputSCTimesteps," + "RasterOutputTR," + "RasterOutputTRTimesteps," + "RasterOutputAge," + "RasterOutputAgeTimesteps," + "RasterOutputTST," + "RasterOutputTSTTimesteps," + "RasterOutputST," + "RasterOutputSTTimesteps)" + " SELECT " + "ProjectID," + "ScenarioID," + "SummaryOutputSC," + "SummaryOutputSCTimesteps," + "SummaryOutputTR," + "SummaryOutputTRTimesteps," + "SummaryOutputTRIntervalMean," + "SummaryOutputTRSC, " + "SummaryOutputTRSCTimesteps," + "SummaryOutputSA," + "SummaryOutputSATimesteps," + "SummaryOutputTA," + "SummaryOutputTATimesteps," + "RasterOutputSC," + "RasterOutputSCTimesteps," + "RasterOutputTR," + "RasterOutputTRTimesteps," + "RasterOutputAge," + "RasterOutputAgeTimesteps," + "RasterOutputTST," + "RasterOutputTSTTimesteps," + "RasterOutputST," + "RasterOutputSTTimesteps" + " FROM ST_OutputOptionsTEMP");

            store.ExecuteNonQuery("DROP TABLE ST_OutputOptionsTEMP");
        }

        /// <summary>
        /// STSIM0000012
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update will add a timestep column to ST_StateAttributeValue and ST_TransitionAttributeValue
        /// </remarks>
        private static void STSIM0000012(DataStore store)
        {
            //State Attribute Value
            store.ExecuteNonQuery("ALTER TABLE ST_StateAttributeValue RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_StateAttributeValue (ST_StateAttributeValueID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, StateAttributeTypeID INTEGER, Timestep INTEGER, StratumID INTEGER, StateClassID INTEGER, AgeMin INTEGER, AgeMax INTEGER, Value DOUBLE)");
            store.ExecuteNonQuery("INSERT INTO ST_StateAttributeValue(ProjectID, ScenarioID, StateAttributeTypeID, StratumID, StateClassID, AgeMin, AgeMax, Value) SELECT ProjectID, ScenarioID, StateAttributeTypeID, StratumID, StateClassID, AgeMin, AgeMax, Value FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

            //Transition Attribute Value
            if (store.TableExists("ST_TransitionAttributeValue"))
            {
                store.ExecuteNonQuery("ALTER TABLE ST_TransitionAttributeValue RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE ST_TransitionAttributeValue (ST_TransitionAttributeValueID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, TransitionAttributeTypeID INTEGER, Timestep INTEGER, StratumID INTEGER, TransitionGroupID INTEGER, StateClassID INTEGER, AgeMin INTEGER, AgeMax INTEGER, Value DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO ST_TransitionAttributeValue(ProjectID, ScenarioID, TransitionAttributeTypeID, StratumID, TransitionGroupID, StateClassID, AgeMin, AgeMax, Value) SELECT ProjectID, ScenarioID, TransitionAttributeTypeID, StratumID, TransitionGroupID, StateClassID, AgeMin, AgeMax, Value FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }
        }

        /// <summary>
        /// STSIM0000013
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update will create a new schema for the following tables:
        /// 
        /// TransitionTarget
        /// TransitionAttributeTarget
        /// TransitionMultiplierValue
        /// TransitionSlopeMultiplier
        /// TransitionDirectionMultiplier
        /// 
        /// The distribution, sd, min, and max fields will appear as follows:
        /// 
        /// DistributionType
        /// DistributionSD
        /// DistributionMin 
        /// DistributionMax 
        /// 
        /// And finally, the TransitionMultiplierDistribution table will be dropped because its fields will be included in the TransitionMultiplierValue table.
        /// However, before it is dropped, its values must be copied over to the new TransitionMultiplierValue table.
        /// 
        /// </remarks>
        private static void STSIM0000013(DataStore store)
        {
            store.ExecuteNonQuery("ALTER TABLE ST_TransitionTarget RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_TransitionTarget (ST_TransitionTargetID  INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, Timestep INTEGER, TransitionGroupID INTEGER, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE)");
            store.ExecuteNonQuery("INSERT INTO ST_TransitionTarget(ProjectID, ScenarioID, StratumID, SecondaryStratumID, Timestep, TransitionGroupID, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax) SELECT ProjectID, ScenarioID, StratumID, SecondaryStratumID, Timestep, TransitionGroupID, Amount, Distribution, SD, MinimumValue, MaximumValue FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

            if (store.TableExists("ST_TransitionAttributeTarget"))
            {
                store.ExecuteNonQuery("ALTER TABLE ST_TransitionAttributeTarget RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE ST_TransitionAttributeTarget (ST_TransitionAttributeTargetID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, Timestep INTEGER, TransitionAttributeTypeID INTEGER, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO ST_TransitionAttributeTarget(ProjectID, ScenarioID, StratumID, SecondaryStratumID, Timestep, TransitionAttributeTypeID, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax) SELECT ProjectID, ScenarioID, StratumID, SecondaryStratumID, Timestep, TransitionAttributeTypeID, Amount, Distribution, SD, MinimumValue, MaximumValue FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("ST_TransitionMultiplierValue"))
            {
                store.ExecuteNonQuery("ALTER TABLE ST_TransitionMultiplierValue RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE ST_TransitionMultiplierValue (ST_TransitionMultiplierValueID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, TransitionGroupID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, Iteration INTEGER, Timestep INTEGER, TransitionMultiplierTypeID INTEGER, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO ST_TransitionMultiplierValue(ProjectID, ScenarioID, TransitionGroupID, StratumID, SecondaryStratumID, Iteration, Timestep, TransitionMultiplierTypeID, Amount, DistributionSD) SELECT ProjectID, ScenarioID, TransitionGroupID, StratumID, SecondaryStratumID, Iteration, Timestep, TransitionMultiplierTypeID, Mean, SD FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("ST_TransitionSlopeMultiplier"))
            {
                store.ExecuteNonQuery("ALTER TABLE ST_TransitionSlopeMultiplier RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE ST_TransitionSlopeMultiplier (ST_TransitionSlopeMultiplierID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, TransitionGroupID INTEGER, StratumID INTEGER, Iteration INTEGER, Timestep INTEGER, Slope INTEGER, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO ST_TransitionSlopeMultiplier(ProjectID, ScenarioID, TransitionGroupID, StratumID, Iteration, Timestep, Slope, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax) SELECT ProjectID, ScenarioID, TransitionGroupID, StratumID, Iteration, Timestep, Slope, Multiplier, Distribution, SD, MinimumValue, MaximumValue FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("ST_TransitionDirectionMultiplier"))
            {
                store.ExecuteNonQuery("ALTER TABLE ST_TransitionDirectionMultiplier RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE ST_TransitionDirectionMultiplier (ST_TransitionDirectionMultiplierID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, TransitionGroupID INTEGER, StratumID INTEGER, Iteration INTEGER, Timestep INTEGER, CardinalDirection INTEGER, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO ST_TransitionDirectionMultiplier(ProjectID, ScenarioID, TransitionGroupID, StratumID, Iteration, Timestep, CardinalDirection, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax) SELECT ProjectID, ScenarioID, TransitionGroupID, StratumID, Iteration, Timestep, CardinalDirection, Multiplier, Distribution, SD, MinimumValue, MaximumValue FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("ST_TransitionMultiplierValue") & store.TableExists("ST_TransitionMultiplierDistribution"))
            {
                store.ExecuteNonQuery("UPDATE ST_TransitionMultiplierValue SET DistributionType=(SELECT ST_TransitionMultiplierDistribution.Distribution FROM ST_TransitionMultiplierDistribution WHERE ST_TransitionMultiplierValue.ScenarioID=ST_TransitionMultiplierDistribution.ScenarioID)");
                store.ExecuteNonQuery("UPDATE ST_TransitionMultiplierValue SET DistributionMin=(SELECT ST_TransitionMultiplierDistribution.MinimumValue FROM ST_TransitionMultiplierDistribution WHERE ST_TransitionMultiplierValue.ScenarioID=ST_TransitionMultiplierDistribution.ScenarioID)");
                store.ExecuteNonQuery("UPDATE ST_TransitionMultiplierValue SET DistributionMax=(SELECT ST_TransitionMultiplierDistribution.MaximumValue FROM ST_TransitionMultiplierDistribution WHERE ST_TransitionMultiplierValue.ScenarioID=ST_TransitionMultiplierDistribution.ScenarioID)");

                store.ExecuteNonQuery("DROP TABLE ST_TransitionMultiplierDistribution");
            }
        }

        /// <summary>
        /// STSIM0000014
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update will add average annual transition probability support to the output options table.
        /// </remarks>
        private static void STSIM0000014(DataStore store)
        {
            store.ExecuteNonQuery("ALTER TABLE ST_OutputOptions ADD COLUMN RasterOutputAATP INTEGER");
            store.ExecuteNonQuery("ALTER TABLE ST_OutputOptions ADD COLUMN RasterOutputAATPTimesteps INTEGER");
        }

        /// <summary>
        /// STSIM0000015
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update will perform minor maintenance on the chart variables table.  The new table has a VariableName column and we
        /// need to update it with the correct varaible name for ST-Sim.
        /// </remarks>
        private static void STSIM0000015(DataStore store)
        {
            if (!store.TableExists("STime_ChartVariable"))
            {
                return;
            }

            //We don't want to do this if the DataSheetName column is no longer there.  This will happen after Stochastic Time's STIME0000009
            //update has been applied.  See commments for that function.

            DataTable dtcv = store.CreateDataTable("STime_ChartVariable");

            if (!dtcv.Columns.Contains("DataFeedName"))
            {
                return;
            }

            store.ExecuteNonQuery("UPDATE STime_ChartVariable SET VariableName='StateClassAmountVariable' WHERE DataSheetName='ST_OutputStratumState'");
            store.ExecuteNonQuery("UPDATE STime_ChartVariable SET VariableName='TransitionAmountVariable' WHERE DataSheetName='ST_OutputStratumTransition'");

            DataTable dt = store.CreateDataTableFromQuery("SELECT * FROM STime_ChartVariable WHERE DataFeedName='ST_OutputStateAttribute'", "Table");

            foreach (DataRow dr in dt.Rows)
            {
                string dsname = Convert.ToString(dr["DataSheetName"], CultureInfo.InvariantCulture);
                string q = string.Format(CultureInfo.InvariantCulture, "UPDATE STime_ChartVariable SET VariableName='{0}' WHERE DataSheetName='{1}'", dsname, dsname);

                store.ExecuteNonQuery(q);
            }

            dt = store.CreateDataTableFromQuery("SELECT * FROM STime_ChartVariable WHERE DataFeedName='ST_OutputTransitionAttribute'", "Table");

            foreach (DataRow dr in dt.Rows)
            {
                string dsname = Convert.ToString(dr["DataSheetName"], CultureInfo.InvariantCulture);
                string q = string.Format(CultureInfo.InvariantCulture, "UPDATE STime_ChartVariable SET VariableName='{0}' WHERE DataSheetName='{1}'", dsname, dsname);

                store.ExecuteNonQuery(q);
            }
        }

        /// <summary>
        /// STSIM0000016
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update will add a secondary stratum column to ST_StateAttributeValue and ST_TransitionAttributeValue
        /// </remarks>
        private static void STSIM0000016(DataStore store)
        {
            //State Attribute Value
            store.ExecuteNonQuery("ALTER TABLE ST_StateAttributeValue RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_StateAttributeValue (ST_StateAttributeValueID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, StateAttributeTypeID INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, StateClassID INTEGER, AgeMin INTEGER, AgeMax INTEGER, Value DOUBLE)");
            store.ExecuteNonQuery("INSERT INTO ST_StateAttributeValue(ProjectID, ScenarioID, StateAttributeTypeID, Timestep, StratumID, StateClassID, AgeMin, AgeMax, Value) SELECT ProjectID, ScenarioID, StateAttributeTypeID, Timestep, StratumID, StateClassID, AgeMin, AgeMax, Value FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

            //Transition Attribute Value
            if (store.TableExists("ST_TransitionAttributeValue"))
            {
                store.ExecuteNonQuery("ALTER TABLE ST_TransitionAttributeValue RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE ST_TransitionAttributeValue (ST_TransitionAttributeValueID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, TransitionAttributeTypeID INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TransitionGroupID INTEGER, StateClassID INTEGER, AgeMin INTEGER, AgeMax INTEGER, Value DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO ST_TransitionAttributeValue(ProjectID, ScenarioID, TransitionAttributeTypeID, Timestep, StratumID, TransitionGroupID, StateClassID, AgeMin, AgeMax, Value) SELECT ProjectID, ScenarioID, TransitionAttributeTypeID, Timestep, StratumID, TransitionGroupID, StateClassID, AgeMin, AgeMax, Value FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }
        }

        /// <summary>
        /// STSIM0000017
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update will add secondary stratum/iteration/timestep columns to the following tables:
        /// 
        /// (1.)  ST_TransitionTarget              - Iteration
        /// (2.)  ST_TransitionSizeDistribution    - Iteration, Timestep
        /// (3.)  ST_TransitionSpreadDistribution  - Iteration, Timestep
        /// (4.)  ST_TransitionPatchPrioritization - Iteration, Timestep
        /// (5.)  ST_TransitionSpatialMultiplier   - Iteration
        /// (6.)  ST_StateAttributeValue           - Iteration
        /// (7.)  ST_TransitionAttributeValue      - Iteration
        /// (8.)  ST_TransitionAttributeTarget     - Iteration
        /// (9.)  ST_TimeSinceTransitionGroup      - SecondaryStratum
        /// (10.) ST_TimeSinceTransitionRandomize  - SecondaryStratum
        /// (11.) ST_TransitionSlopeMultiplier     - SecondaryStratum
        /// (12.) ST_TransitionDirectionMultiplier - SecondaryStratum
        /// (13.) ST_TransitionAdjacencyMultiplier - SecondaryStratum
        /// 
        /// </remarks>
        private static void STSIM0000017(DataStore store)
        {
            //(1.) above
            store.ExecuteNonQuery("ALTER TABLE ST_TransitionTarget RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_TransitionTarget (ST_TransitionTargetID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, Iteration INTEGER, Timestep INTEGER, TransitionGroupID INTEGER, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE)");
            store.ExecuteNonQuery("INSERT INTO ST_TransitionTarget(ProjectID, ScenarioID, StratumID, SecondaryStratumID, Timestep, TransitionGroupID, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax) SELECT ProjectID, ScenarioID, StratumID, SecondaryStratumID, Timestep, TransitionGroupID, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

            //(2.) above
            store.ExecuteNonQuery("ALTER TABLE ST_TransitionSizeDistribution RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_TransitionSizeDistribution (ST_TransitionSizeDistributionID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, StratumID INTEGER, Iteration INTEGER, Timestep INTEGER, TransitionGroupID INTEGER, MaximumArea DOUBLE, RelativeAmount DOUBLE)");
            store.ExecuteNonQuery("INSERT INTO ST_TransitionSizeDistribution(ProjectID, ScenarioID, StratumID, TransitionGroupID, MaximumArea, RelativeAmount) SELECT ProjectID, ScenarioID, StratumID, TransitionGroupID, MaximumArea, RelativeAmount FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

            //(3.) above
            store.ExecuteNonQuery("ALTER TABLE ST_TransitionSpreadDistribution RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_TransitionSpreadDistribution (ST_TransitionSpreadDistributionID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, StratumID INTEGER, Iteration INTEGER, Timestep INTEGER, TransitionGroupID INTEGER, StateClassID INTEGER, MaximumDistance DOUBLE, RelativeAmount DOUBLE)");
            store.ExecuteNonQuery("INSERT INTO ST_TransitionSpreadDistribution(ProjectID, ScenarioID, StratumID, TransitionGroupID, StateClassID, MaximumDistance, RelativeAmount) SELECT ProjectID, ScenarioID, StratumID, TransitionGroupID, StateClassID, MaximumDistance, RelativeAmount FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

            //(4.) above
            store.ExecuteNonQuery("ALTER TABLE ST_TransitionPatchPrioritization RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_TransitionPatchPrioritization (ST_TransitionPatchPrioritizationID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, TransitionGroupID INTEGER, PatchPrioritization INTEGER)");
            store.ExecuteNonQuery("INSERT INTO ST_TransitionPatchPrioritization(ProjectID, ScenarioID, TransitionGroupID, PatchPrioritization) SELECT ProjectID, ScenarioID, TransitionGroupID, PatchPrioritization FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

            //(5.) above
            if (store.TableExists("ST_TransitionSpatialMultiplier"))
            {
                store.ExecuteNonQuery("ALTER TABLE ST_TransitionSpatialMultiplier RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE ST_TransitionSpatialMultiplier (ST_TransitionSpatialMultiplierID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, TransitionGroupID INTEGER, MultiplierFileName TEXT(255))");
                store.ExecuteNonQuery("INSERT INTO ST_TransitionSpatialMultiplier(ProjectID, ScenarioID, Timestep, TransitionGroupID, MultiplierFileName) SELECT ProjectID, ScenarioID, Timestep, TransitionGroupID, TransitionSpatialMultiplierFilename FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            //(6.) above
            store.ExecuteNonQuery("ALTER TABLE ST_StateAttributeValue RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_StateAttributeValue (ST_StateAttributeValueID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, StateAttributeTypeID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, Iteration INTEGER, Timestep INTEGER, StateClassID INTEGER, AgeMin INTEGER, AgeMax INTEGER, Value DOUBLE)");
            store.ExecuteNonQuery("INSERT INTO ST_StateAttributeValue(ProjectID, ScenarioID, StateAttributeTypeID, StratumID, SecondaryStratumID, Timestep, StateClassID, AgeMin, AgeMax, Value) SELECT ProjectID, ScenarioID, StateAttributeTypeID, StratumID, SecondaryStratumID, Timestep, StateClassID, AgeMin, AgeMax, Value FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

            //(7.) above
            if (store.TableExists("ST_TransitionAttributeValue"))
            {
                store.ExecuteNonQuery("ALTER TABLE ST_TransitionAttributeValue RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE ST_TransitionAttributeValue (ST_TransitionAttributeValueID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, TransitionAttributeTypeID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, Iteration INTEGER, Timestep INTEGER, TransitionGroupID INTEGER, StateClassID INTEGER, AgeMin INTEGER, AgeMax INTEGER, Value DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO ST_TransitionAttributeValue(ProjectID, ScenarioID, TransitionAttributeTypeID, StratumID, SecondaryStratumID, Timestep, TransitionGroupID, StateClassID, AgeMin, AgeMax, Value) SELECT ProjectID, ScenarioID, TransitionAttributeTypeID, StratumID, SecondaryStratumID, Timestep, TransitionGroupID, StateClassID, AgeMin, AgeMax, Value FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            //(8.) above
            if (store.TableExists("ST_TransitionAttributeTarget"))
            {
                store.ExecuteNonQuery("ALTER TABLE ST_TransitionAttributeTarget RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE ST_TransitionAttributeTarget (ST_TransitionAttributeTargetID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, Iteration INTEGER, Timestep INTEGER, TransitionAttributeTypeID INTEGER, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO ST_TransitionAttributeTarget(ProjectID, ScenarioID, StratumID, SecondaryStratumID, Timestep, TransitionAttributeTypeID, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax) SELECT ProjectID, ScenarioID, StratumID, SecondaryStratumID, Timestep, TransitionAttributeTypeID, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            //(9.) above
            store.ExecuteNonQuery("ALTER TABLE ST_TimeSinceTransitionGroup RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_TimeSinceTransitionGroup (ST_TimeSinceTransitionGroupID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TransitionTypeID INTEGER, TransitionGroupID INTEGER)");
            store.ExecuteNonQuery("INSERT INTO ST_TimeSinceTransitionGroup(ProjectID, ScenarioID, StratumID, TransitionTypeID, TransitionGroupID) SELECT ProjectID, ScenarioID, StratumID, TransitionTypeID, TransitionGroupID FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

            //(10.) above
            store.ExecuteNonQuery("ALTER TABLE ST_TimeSinceTransitionRandomize RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_TimeSinceTransitionRandomize (ST_TimeSinceTransitionRandomizeID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, MaxInitialTST INTEGER)");
            store.ExecuteNonQuery("INSERT INTO ST_TimeSinceTransitionRandomize(ProjectID, ScenarioID, StratumID, MaxInitialTST) SELECT ProjectID, ScenarioID, StratumID, MaxInitialTST FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

            //(11.) above
            if (store.TableExists("ST_TransitionSlopeMultiplier"))
            {
                store.ExecuteNonQuery("ALTER TABLE ST_TransitionSlopeMultiplier RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE ST_TransitionSlopeMultiplier (ST_TransitionSlopeMultiplierID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, TransitionGroupID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, Iteration INTEGER, Timestep INTEGER, Slope INTEGER, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO ST_TransitionSlopeMultiplier(ProjectID, ScenarioID, TransitionGroupID, StratumID, Iteration, Timestep, Slope, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax) SELECT ProjectID, ScenarioID, TransitionGroupID, StratumID, Iteration, Timestep, Slope, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            //(12.) above
            if (store.TableExists("ST_TransitionDirectionMultiplier"))
            {
                store.ExecuteNonQuery("ALTER TABLE ST_TransitionDirectionMultiplier RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE ST_TransitionDirectionMultiplier (ST_TransitionDirectionMultiplierID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, TransitionGroupID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, Iteration INTEGER, Timestep INTEGER, CardinalDirection INTEGER, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO ST_TransitionDirectionMultiplier(ProjectID, ScenarioID, TransitionGroupID, StratumID, Iteration, Timestep, CardinalDirection, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax) SELECT ProjectID, ScenarioID, TransitionGroupID, StratumID, Iteration, Timestep, CardinalDirection, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            //(13.) above
            if (store.TableExists("ST_TransitionAdjacencyMultiplier"))
            {
                store.ExecuteNonQuery("ALTER TABLE ST_TransitionAdjacencyMultiplier RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE ST_TransitionAdjacencyMultiplier (ST_TransitionAdjacencyMultiplierID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, TransitionGroupID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, Iteration INTEGER, Timestep INTEGER, AttributeValue DOUBLE, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO ST_TransitionAdjacencyMultiplier(ProjectID, ScenarioID, TransitionGroupID, StratumID, Iteration, Timestep, AttributeValue, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax) SELECT ProjectID, ScenarioID, TransitionGroupID, StratumID, Iteration, Timestep, AttributeValue, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }
        }

        /// <summary>
        /// STSIM0000018
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update will update the ST_TransitionTypeGroup table as follows:
        /// 
        /// (1.) Change the IsSecondary field to IsPrimary
        /// (2.) Toggle the values in the IsPrimary field as follows:
        /// 
        ///      IsSecondary(True) -> IsPrimary(False) // -1 == True
        ///      IsSecondary(False) -> IsPrimary(True) // 0 or NULL == False
        /// 
        /// Also, set any IsPrimary(True) values to NULL which means True for this particular column
        /// 
        /// </remarks>
        private static void STSIM0000018(DataStore store)
        {
            //(1.) above
            store.ExecuteNonQuery("ALTER TABLE ST_TransitionTypeGroup RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_TransitionTypeGroup(ST_TransitionTypeGroupID INTEGER PRIMARY KEY, ProjectID INTEGER, TransitionTypeID INTEGER, TransitionGroupID INTEGER, IsPrimary INTEGER)");
            store.ExecuteNonQuery("INSERT INTO ST_TransitionTypeGroup(ST_TransitionTypeGroupID, ProjectID, TransitionTypeID, TransitionGroupID, IsPrimary) SELECT ST_TransitionTypeGroupID, ProjectID, TransitionTypeID, TransitionGroupID, IsSecondary FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

            //(2.) above
            store.ExecuteNonQuery("UPDATE ST_TransitionTypeGroup SET IsPrimary=2 WHERE IsPrimary=-1");
            store.ExecuteNonQuery("UPDATE ST_TransitionTypeGroup SET IsPrimary=-1 WHERE IsPrimary=0 or IsPrimary IS NULL");
            store.ExecuteNonQuery("UPDATE ST_TransitionTypeGroup SET IsPrimary=0 WHERE IsPrimary=2");
            store.ExecuteNonQuery("UPDATE ST_TransitionTypeGroup SET IsPrimary=NULL WHERE IsPrimary=-1");
        }

        /// <summary>
        /// STSIM0000019
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update will update will append a CalcFromDist column to the ST_InitialConditionsNonSpatial table
        /// 
        /// </remarks>
        private static void STSIM0000019(DataStore store)
        {
            store.ExecuteNonQuery("ALTER TABLE ST_InitialConditionsNonSpatial ADD COLUMN CalcFromDist INTEGER");
        }

        /// <summary>
        /// STSIM0000020
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update will add support for Transition Group, State Class, and Minimum Initial TST Value 
        /// to the ST_TimeSinceTransitionRandomize table.
        /// 
        /// </remarks>
        private static void STSIM0000020(DataStore store)
        {
            store.ExecuteNonQuery("ALTER TABLE ST_TimeSinceTransitionRandomize ADD COLUMN TransitionGroupID INTEGER");
            store.ExecuteNonQuery("ALTER TABLE ST_TimeSinceTransitionRandomize ADD COLUMN StateClassID INTEGER");
            store.ExecuteNonQuery("ALTER TABLE ST_TimeSinceTransitionRandomize ADD COLUMN MinInitialTST INTEGER");
        }

        /// <summary>
        /// STSIM0000021
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// "This update will add minimum and maximum iteration fields to the STSim Run Control table.
        /// </remarks>
        private static void STSIM0000021(DataStore store)
        {
            store.ExecuteNonQuery("ALTER TABLE ST_RunControl RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_RunControl(ST_RunControlID INTEGER PRIMARY KEY, ProjectID INTEGER, ScenarioID INTEGER, MinimumIteration INTEGER, MaximumIteration INTEGER, MinimumTimestep INTEGER, MaximumTimestep INTEGER, RunSpatially INTEGER)");
            store.ExecuteNonQuery("INSERT INTO ST_RunControl(ProjectID, ScenarioID, MinimumIteration, MaximumIteration, MinimumTimestep, MaximumTimestep, RunSpatially) SELECT ProjectID, ScenarioID, 1, Iteration, 0, Timestep, RunSpatially FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
        }

        /// <summary>
        /// STSIM0000022
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// "This update will fix incorrect boolean values written to the CellAreaOverride table.
        /// </remarks>
        private static void STSIM0000022(DataStore store)
        {
            store.ExecuteNonQuery("UPDATE ST_InitialConditionsSpatial SET CellAreaOverride=-1 WHERE CellAreaOverride=1");
        }

        /// <summary>
        /// STSIM0000023
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update will add the "Zero Values" field to the ST_OutputOptions table
        /// </remarks>
        private static void STSIM0000023(DataStore store)
        {
            store.ExecuteNonQuery("ALTER TABLE ST_OutputOptions RENAME TO ST_OutputOptionsTEMP");

            store.ExecuteNonQuery("CREATE TABLE ST_OutputOptions ( " + "ST_OutputOptionsID          INTEGER PRIMARY KEY AUTOINCREMENT, " + "ProjectID                   INTEGER, " + "ScenarioID                  INTEGER, " + "SummaryOutputSC             INTEGER, " + "SummaryOutputSCTimesteps    INTEGER, " + "SummaryOutputSCZeroValues   INTEGER, " + "SummaryOutputTR             INTEGER, " + "SummaryOutputTRTimesteps    INTEGER, " + "SummaryOutputTRIntervalMean INTEGER, " + "SummaryOutputTRSC           INTEGER, " + "SummaryOutputTRSCTimesteps  INTEGER, " + "SummaryOutputSA             INTEGER, " + "SummaryOutputSATimesteps    INTEGER, " + "SummaryOutputTA             INTEGER, " + "SummaryOutputTATimesteps    INTEGER, " + "RasterOutputSC              INTEGER, " + "RasterOutputSCTimesteps     INTEGER, " + "RasterOutputTR              INTEGER, " + "RasterOutputTRTimesteps     INTEGER, " + "RasterOutputAge             INTEGER, " + "RasterOutputAgeTimesteps    INTEGER, " + "RasterOutputTST             INTEGER, " + "RasterOutputTSTTimesteps    INTEGER, " + "RasterOutputST              INTEGER, " + "RasterOutputSTTimesteps     INTEGER, " + "RasterOutputSA              INTEGER, " + "RasterOutputSATimesteps     INTEGER, " + "RasterOutputTA              INTEGER, " + "RasterOutputTATimesteps     INTEGER, " + "RasterOutputAATP            INTEGER, " + "RasterOutputAATPTimesteps   INTEGER)");

            store.ExecuteNonQuery("INSERT INTO ST_OutputOptions (" + "ProjectID," + "ScenarioID," + "SummaryOutputSC," + "SummaryOutputSCTimesteps," + "SummaryOutputTR," + "SummaryOutputTRTimesteps," + "SummaryOutputTRIntervalMean," + "SummaryOutputTRSC," + "SummaryOutputTRSCTimesteps," + "SummaryOutputSA," + "SummaryOutputSATimesteps," + "SummaryOutputTA," + "SummaryOutputTATimesteps," + "RasterOutputSC," + "RasterOutputSCTimesteps," + "RasterOutputTR," + "RasterOutputTRTimesteps," + "RasterOutputAge," + "RasterOutputAgeTimesteps," + "RasterOutputTST," + "RasterOutputTSTTimesteps," + "RasterOutputST," + "RasterOutputSTTimesteps," + "RasterOutputSA," + "RasterOutputSATimesteps," + "RasterOutputTA," + "RasterOutputTATimesteps," + "RasterOutputAATP," + "RasterOutputAATPTimesteps)" + " SELECT " + "ProjectID," + "ScenarioID," + "SummaryOutputSC," + "SummaryOutputSCTimesteps," + "SummaryOutputTR," + "SummaryOutputTRTimesteps," + "SummaryOutputTRIntervalMean," + "SummaryOutputTRSC, " + "SummaryOutputTRSCTimesteps," + "SummaryOutputSA," + "SummaryOutputSATimesteps," + "SummaryOutputTA," + "SummaryOutputTATimesteps," + "RasterOutputSC," + "RasterOutputSCTimesteps," + "RasterOutputTR," + "RasterOutputTRTimesteps," + "RasterOutputAge," + "RasterOutputAgeTimesteps," + "RasterOutputTST," + "RasterOutputTSTTimesteps," + "RasterOutputST," + "RasterOutputSTTimesteps," + "RasterOutputSA, " + "RasterOutputSATimesteps," + "RasterOutputTA, " + "RasterOutputTATimesteps," + "RasterOutputAATP," + "RasterOutputAATPTimesteps" + " FROM ST_OutputOptionsTEMP");

            store.ExecuteNonQuery("DROP TABLE ST_OutputOptionsTEMP");
        }

        /// <summary>
        /// STSIM0000024
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update will set the Run Control table's minimum timestep values to 0 instead of the 
        /// incorrect value of 0 which was set by a previous update.
        /// </remarks>
        private static void STSIM0000024(DataStore store)
        {
            store.ExecuteNonQuery("UPDATE ST_RunControl SET MinimumTimestep=0");
        }

        /// <summary>
        /// STSIM0000025
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update will:
        /// 
        /// (1.) Add a Timestep Units field to the Terminology table.
        /// (2.) Update the table with a default value for this field.
        /// </remarks>
        private static void STSIM0000025(DataStore store)
        {
            store.ExecuteNonQuery("ALTER TABLE ST_Terminology RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_Terminology (ST_TerminologyID INTEGER PRIMARY KEY, ProjectID INTEGER, TimestepUnits TEXT( 50 ), AmountLabel TEXT( 50 ), AmountUnits INTEGER, StateLabelX TEXT( 50 ), StateLabelY TEXT( 50 ), PrimaryStratumLabel TEXT( 50 ), SecondaryStratumLabel Text(50))");
            store.ExecuteNonQuery("INSERT INTO ST_Terminology (ST_TerminologyID, ProjectID, TimestepUnits, AmountLabel, AmountUnits, StateLabelX, StateLabelY, PrimaryStratumLabel, SecondaryStratumLabel) SELECT ST_TerminologyID, ProjectID, 'Timestep', AmountLabel, AmountUnits, StateLabelX, StateLabelY, PrimaryStratumLabel, SecondaryStratumLabel FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
        }

        /// <summary>
        /// STSIM0000026
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update adds age tracking support to the summary state class output table.
        /// It also adds State Label X/Y columns so that disaggregation with these columns is possible.
        /// </remarks>
        private static void STSIM0000026(DataStore store)
        {
            store.ExecuteNonQuery("DROP INDEX ST_OutputStratumState_Index");
            store.ExecuteNonQuery("ALTER TABLE ST_OutputStratumState RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_OutputStratumState(ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, StateClassID INTEGER, StateLabelXID INTEGER, StateLabelYID INTEGER, AgeMin INTEGER, AgeMax INTEGER, AgeClass INTEGER, Amount DOUBLE)");
            store.ExecuteNonQuery("INSERT INTO ST_OutputStratumState(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, StateClassID, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, StateClassID, Amount FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            store.ExecuteNonQuery("CREATE INDEX ST_OutputStratumState_Index ON ST_OutputStratumState(ScenarioID, Iteration, Timestep)");
        }

        /// <summary>
        /// STSIM0000027
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update adds NumCells support to the Initial Conditions Spatial table.
        /// </remarks>
        private static void STSIM0000027(DataStore store)
        {
            store.ExecuteNonQuery("ALTER TABLE ST_InitialConditionsSpatial ADD COLUMN  NumCells INTEGER");
            // Set NumCells value = Num Rows * Num Cols, which is a starting point. Will only be 100% if Primary
            // Stratum raster has no NO_DATA_VALUE cells.
            store.ExecuteNonQuery("UPDATE ST_InitialConditionsSpatial set NumCells = NumRows * NumColumns");
        }

        /// <summary>
        /// STSIM0000028
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update adds age tracking support to the transition summary table and both attribute tables.
        /// </remarks>
        private static void STSIM0000028(DataStore store)
        {
            //ST_OutputStratumTransition
            store.ExecuteNonQuery("DROP INDEX ST_OutputStratumTransition_Index");
            store.ExecuteNonQuery("ALTER TABLE ST_OutputStratumTransition RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_OutputStratumTransition(ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TransitionGroupID INTEGER, AgeMin INTEGER, AgeMax INTEGER, AgeClass INTEGER, Amount DOUBLE)");
            store.ExecuteNonQuery("INSERT INTO ST_OutputStratumTransition(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, Amount FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            store.ExecuteNonQuery("CREATE INDEX ST_OutputStratumTransition_Index ON ST_OutputStratumTransition(ScenarioID, Iteration, Timestep)");

            //ST_OutputStateAttribute
            store.ExecuteNonQuery("DROP INDEX ST_OutputStateAttribute_Index");
            store.ExecuteNonQuery("ALTER TABLE ST_OutputStateAttribute RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_OutputStateAttribute(ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, StateAttributeTypeID INTEGER, AgeMin INTEGER, AgeMax INTEGER, AgeClass INTEGER, Amount DOUBLE)");
            store.ExecuteNonQuery("INSERT INTO ST_OutputStateAttribute(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, StateAttributeTypeID, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, StateAttributeTypeID, Amount FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            store.ExecuteNonQuery("CREATE INDEX ST_OutputStateAttribute_Index ON ST_OutputStateAttribute(ScenarioID, Iteration, Timestep)");

            //ST_OutputTransitionAttribute
            store.ExecuteNonQuery("DROP INDEX ST_OutputTransitionAttribute_Index");
            store.ExecuteNonQuery("ALTER TABLE ST_OutputTransitionAttribute RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_OutputTransitionAttribute(ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TransitionAttributeTypeID INTEGER, AgeMin INTEGER, AgeMax INTEGER, AgeClass INTEGER, Amount DOUBLE)");
            store.ExecuteNonQuery("INSERT INTO ST_OutputTransitionAttribute(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionAttributeTypeID, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionAttributeTypeID, Amount FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            store.ExecuteNonQuery("CREATE INDEX ST_OutputTransitionAttribute_Index ON ST_OutputTransitionAttribute(ScenarioID, Iteration, Timestep)");
        }

        /// <summary>
        /// STSIM0000029
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update will remove the embedded ID value from STime_ChartVariable.VariableName.
        /// </remarks>
        private static void STSIM0000029(DataStore store)
        {
            if (store.TableExists("STime_ChartVariable"))
            {
                store.ExecuteNonQuery("UPDATE STime_ChartVariable SET VariableName = rtrim(VariableName, '0123456789') WHERE DataSheetName = 'ST_OutputStateAttribute'");
                store.ExecuteNonQuery("UPDATE STime_ChartVariable SET VariableName = rtrim(VariableName, '0123456789') WHERE DataSheetName = 'ST_OutputTransitionAttribute'");
                store.ExecuteNonQuery("UPDATE STime_ChartVariable SET VariableName = rtrim(VariableName, '-') WHERE DataSheetName = 'ST_OutputStateAttribute'");
                store.ExecuteNonQuery("UPDATE STime_ChartVariable SET VariableName = rtrim(VariableName, '-') WHERE DataSheetName = 'ST_OutputTransitionAttribute'");
            }
        }

        /// <summary>
        /// STSIM0000030
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update will remove the Name column from the age tables. It will also remove
        /// the Description column from the age groups table.
        /// </remarks>
        private static void STSIM0000030(DataStore store)
        {
            if (store.TableExists("ST_AgeType"))
            {
                store.ExecuteNonQuery("ALTER TABLE ST_AgeType RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE ST_AgeType (ST_AgeTypeID INTEGER PRIMARY KEY, ProjectID INTEGER, Frequency INTEGER, MaximumAge INTEGER)");
                store.ExecuteNonQuery("INSERT INTO ST_AgeType (ST_AgeTypeID, ProjectID, Frequency, MaximumAge) SELECT ST_AgeTypeID, ProjectID, Frequency, MaximumAge FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("ST_AgeGroup"))
            {
                store.ExecuteNonQuery("ALTER TABLE ST_AgeGroup RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE ST_AgeGroup (ST_AgeGroupID INTEGER PRIMARY KEY, ProjectID INTEGER, MaximumAge INTEGER, ID INTEGER, Color TEXT(20))");
                store.ExecuteNonQuery("INSERT INTO ST_AgeGroup (ST_AgeGroupID, ProjectID, MaximumAge, ID, Color) SELECT ST_AgeGroupID, ProjectID, MaximumAge, ID, Color FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }
        }

        /// <summary>
        /// STSIM0000031
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update adds a StateClassID column to the ST_TransitionMultiplierValue column.
        /// </remarks>
        private static void STSIM0000031(DataStore store)
        {
            if (store.TableExists("ST_TransitionMultiplierValue"))
            {
                store.ExecuteNonQuery("ALTER TABLE ST_TransitionMultiplierValue RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE ST_TransitionMultiplierValue (ST_TransitionMultiplierValueID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, TransitionGroupID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, StateClassID INTEGER, Iteration INTEGER, Timestep INTEGER, TransitionMultiplierTypeID INTEGER, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO ST_TransitionMultiplierValue(ProjectID, ScenarioID, TransitionGroupID, StratumID, SecondaryStratumID, Iteration, Timestep, TransitionMultiplierTypeID, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax) SELECT ProjectID, ScenarioID, TransitionGroupID, StratumID, SecondaryStratumID, Iteration, Timestep, TransitionMultiplierTypeID, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }
        }

        /// <summary>
        /// STSIM0000032
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update will change ST_RunControl as follows:
        /// 
        /// (1.) Update "MinimumIteration" to have a "1" everywhere to fix a problem where NULL values
        ///      were erroneously written to this field at some point.
        /// 
        /// (2.) Change the order of the columns in ST_RunControl so that it matches the order in the code.
        /// </remarks>
        private static void STSIM0000032(DataStore store)
        {
            store.ExecuteNonQuery("ALTER TABLE ST_RunControl RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE ST_RunControl (ST_RunControlID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, MinimumIteration INTEGER, MaximumIteration INTEGER, MinimumTimestep INTEGER, MaximumTimestep INTEGER, RunSpatially INTEGER)");
            store.ExecuteNonQuery("INSERT INTO ST_RunControl(ProjectID, ScenarioID, MinimumIteration, MaximumIteration, MinimumTimestep, MaximumTimestep, RunSpatially) SELECT ProjectID, ScenarioID, MinimumIteration, MaximumIteration, MinimumTimestep, MaximumTimestep, RunSpatially FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

            store.ExecuteNonQuery("UPDATE ST_RunControl SET MinimumIteration=1 WHERE MinimumIteration IS NULL");
        }

        /// <summary>
        /// STSIM0000033
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>Cretaes new indexes for output tables</remarks>
        private static void STSIM0000033(DataStore store)
        {
            //Very old libraries did not create the schema until the tables were actually needed so if, for example, 
            //STSim_OutputStratumState does not exist we don't want to try to do anything.

            if (!store.TableExists("STSim_OutputStratumState"))
            {
                return;
            }

            if (store.TableExists("ST_OutputStratumAmount"))
            {
                store.ExecuteNonQuery("DROP INDEX ST_OutputStratumAmount_Index");
                store.ExecuteNonQuery("CREATE INDEX STSim_OutputStratum_Index ON STSim_OutputStratum (ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID)");
            }

            store.ExecuteNonQuery("DROP INDEX ST_OutputStratumState_Index");
            store.ExecuteNonQuery("CREATE INDEX STSim_OutputStratumState_Index ON STSim_OutputStratumState (ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, StateClassID, StateLabelXID, StateLabelYID, AgeClass)");

            store.ExecuteNonQuery("DROP INDEX ST_OutputStratumTransition_Index");
            store.ExecuteNonQuery("CREATE INDEX STSim_OutputStratumTransition_Index ON STSim_OutputStratumTransition (ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, AgeClass)");

            store.ExecuteNonQuery("DROP INDEX ST_OutputStratumTransitionState_Index");
            store.ExecuteNonQuery("CREATE INDEX STSim_OutputStratumTransitionState_Index ON STSim_OutputStratumTransitionState (ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionTypeID, StateClassID, EndStateClassID)");

            store.ExecuteNonQuery("DROP INDEX ST_OutputStateAttribute_Index");
            store.ExecuteNonQuery("CREATE INDEX STSim_OutputStateAttribute_Index ON STSim_OutputStateAttribute (ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, StateAttributeTypeID, AgeClass)");

            store.ExecuteNonQuery("DROP INDEX ST_OutputTransitionAttribute_Index");
            store.ExecuteNonQuery("CREATE INDEX STSim_OutputTransitionAttribute_Index ON STSim_OutputTransitionAttribute (ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionAttributeTypeID, AgeClass)");
        }

        /// <summary>
        /// STSIM0000034
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>Adds a color field to the stratum table</remarks>
        private static void STSIM0000034(DataStore store)
        {
            if (store.TableExists("STSim_Stratum"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_Stratum RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE STSim_Stratum(StratumID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT, ID INTEGER, Color TEXT(20), Description TEXT)");
                store.ExecuteNonQuery("INSERT INTO STSim_Stratum(StratumID, ProjectID, Name, ID, Description) SELECT StratumID, ProjectID, Name, ID, Description FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }
        }

        /// <summary>
        /// STSIM0000040
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>See comments in caller function for this dummy routine...</remarks>
        private static void STSIM0000040(DataStore store)
        {
            return;
        }

        /// <summary>
        /// STSIM0000041
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// Removes invalid NULL entries for TransitionTypeID TransitionGroupID in the table STSim_TransitionTypeGroup
        /// </remarks>
        private static void STSIM0000041(DataStore store)
        {
            if (store.TableExists("STSim_TransitionTypeGroup"))
            {
                store.ExecuteNonQuery("DELETE FROM STSim_TransitionTypeGroup WHERE TransitionTypeID IS NULL");
                store.ExecuteNonQuery("DELETE FROM STSim_TransitionTypeGroup WHERE TransitionGroupID IS NULL");
            }
        }

        /// <summary>
        /// STSIM0000042
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// Changes table name 'STSim_OutputStratumAmount' to 'STSim_OutputStratum'
        /// </remarks>
        private static void STSIM0000042(DataStore store)
        {
            if (store.TableExists("STSim_OutputStratumAmount"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_OutputStratumAmount RENAME to STSim_OutputStratum");
            }
        }

        /// <summary>
        /// STSIM0000043
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// Data feeds with distributions now get their distribution types from Stats_DistributionType instead
        /// of a hard coded list.  This means that we must update every table so that it uses the new project
        /// specific primary key IDs.
        /// </remarks>
        private static void STSIM0000043(DataStore store)
        {
            DataTable Projects = store.CreateDataTable("SSim_Project");
            DataTable Scenarios = store.CreateDataTable("SSim_Scenario");
            Dictionary<int, DataTable> DistTables = new Dictionary<int, DataTable>();

            foreach (DataRow ProjectRow in Projects.Rows)
            {
                int ProjectId = Convert.ToInt32(ProjectRow["ProjectID"], CultureInfo.InvariantCulture);
                DataTable DistributionTypes = store.CreateDataTableFromQuery(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM STime_DistributionType WHERE ProjectID={0}", ProjectId), "DistributionTypes");
                Debug.Assert(DistributionTypes.Rows.Count == 4);
                DistTables.Add(ProjectId, DistributionTypes);
            }

            UpdateDistributions("STSim_TransitionTarget", store, Scenarios, DistTables);
            UpdateDistributions("STSim_TransitionMultiplierValue", store, Scenarios, DistTables);
            UpdateDistributions("STSim_TransitionAttributeTarget", store, Scenarios, DistTables);
            UpdateDistributions("STSim_TransitionDirectionMultiplier", store, Scenarios, DistTables);
            UpdateDistributions("STSim_TransitionSlopeMultiplier", store, Scenarios, DistTables);
            UpdateDistributions("STSim_TransitionAdjacencyMultiplier", store, Scenarios, DistTables);
        }

        private static void UpdateDistributions(string tableName, DataStore store, DataTable scenarios, Dictionary<int, DataTable> distTables)
        {
            if (!store.TableExists(tableName))
            {
                return;
            }

            store.ExecuteNonQuery(string.Format(CultureInfo.InvariantCulture, "UPDATE {0} SET DistributionType=-1 WHERE DistributionType=0", tableName)); //Normal
            store.ExecuteNonQuery(string.Format(CultureInfo.InvariantCulture, "UPDATE {0} SET DistributionType=-2 WHERE DistributionType=1", tableName)); //Beta
            store.ExecuteNonQuery(string.Format(CultureInfo.InvariantCulture, "UPDATE {0} SET DistributionType=-3 WHERE DistributionType=2", tableName)); //Uniform

            foreach (DataRow ScenarioRow in scenarios.Rows)
            {
                int ScenarioId = Convert.ToInt32(ScenarioRow["ScenarioID"], CultureInfo.InvariantCulture);
                int ProjectId = Convert.ToInt32(ScenarioRow["ProjectID"], CultureInfo.InvariantCulture);
                DataTable DistributionTypes = distTables[ProjectId];

                foreach (DataRow DistTypeRow in DistributionTypes.Rows)
                {
                    int DistTypeId = Convert.ToInt32(DistTypeRow["DistributionTypeID"], CultureInfo.InvariantCulture);
                    string DistTypeName = Convert.ToString(DistTypeRow["Name"], CultureInfo.InvariantCulture);
                    int TempDistId = 0;
                    bool DoUpdate = true;

                    if (DistTypeName == "Normal")
                    {
                        TempDistId = -1;
                    }
                    else if (DistTypeName == "Beta")
                    {
                        TempDistId = -2;
                    }
                    else if (DistTypeName == "Uniform")
                    {
                        TempDistId = -3;
                    }
                    else
                    {
                        DoUpdate = false;
                        Debug.Assert(DistTypeName == "Uniform Integer");
                    }

                    if (DoUpdate)
                    {
                        store.ExecuteNonQuery(string.Format(
                            CultureInfo.InvariantCulture, 
                            "UPDATE {0} SET DistributionType={1} WHERE DistributionType={2} AND ScenarioID={3}", 
                            tableName, DistTypeId, TempDistId, ScenarioId));
                    }
                }
            }
        }

        /// <summary>
        /// STSIM0000044
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update adds a TransitionMultiplierTypeID column to the STSim_TransitionSpatialMultiplier table
        /// </remarks>
        private static void STSIM0000044(DataStore store)
        {
            if (store.TableExists("STSim_TransitionSpatialMultiplier"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_TransitionSpatialMultiplier RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE STSim_TransitionSpatialMultiplier(TransitionSpatialMultiplierID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, TransitionGroupID INTEGER, TransitionMultiplierTypeID INTEGER, MultiplierFileName TEXT)");
                store.ExecuteNonQuery("INSERT INTO STSim_TransitionSpatialMultiplier(ScenarioID, Iteration, Timestep, TransitionGroupID, MultiplierFileName) SELECT ScenarioID, Iteration, Timestep, TransitionGroupID, MultiplierFileName FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }
        }

        /// <summary>
        /// STSIM0000045
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update adds a single Transition Size Prioritization record to any scenario that
        /// has Transition Size Distribution records.  
        /// 
        /// Tables are:
        /// 
        /// STSim_TransitionSizeDistribution
        /// STSim_TransitionSizePrioritization
        /// </remarks>
        private static void STSIM0000045(DataStore store)
        {
            if (!store.TableExists("STSim_TransitionSizeDistribution"))
            {
                return;
            }

            if (!store.TableExists("STSim_TransitionSizePrioritization"))
            {
                store.ExecuteNonQuery("CREATE TABLE STSim_TransitionSizePrioritization(TransitionSizePrioritizationID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, TransitionGroupID INTEGER, Priority INTEGER)");
            }

            DataTable dt = store.CreateDataTable("SSim_Scenario");

            foreach (DataRow dr in dt.Rows)
            {
                int ScenarioId = Convert.ToInt32(dr["ScenarioID"], CultureInfo.InvariantCulture);
                string SizeDistQuery = string.Format(CultureInfo.InvariantCulture, "SELECT COUNT(ScenarioID) FROM STSim_TransitionSizeDistribution WHERE ScenarioID={0}", ScenarioId);
                bool HasSizeDist = (Convert.ToInt32(store.ExecuteScalar(SizeDistQuery), CultureInfo.InvariantCulture) > 0);

                if (HasSizeDist)
                {
                    store.ExecuteNonQuery(string.Format(CultureInfo.InvariantCulture, 
                        "INSERT INTO STSim_TransitionSizePrioritization(ScenarioID, Priority) VALUES({0},{1})", 
                        ScenarioId, Convert.ToInt32(SizePrioritization.Largest, CultureInfo.InvariantCulture)));
                }
            }
        }

        /// <summary>
        /// STSIM0000046
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update changes the CellSize field type from SINGLE to DOUBLE because this type is no longer supported by SyncroSim
        /// </remarks>
        private static void STSIM0000046(DataStore store)
        {
            if (store.TableExists("STSim_InitialConditionsSpatial"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_InitialConditionsSpatial RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE STSim_InitialConditionsSpatial(InitialConditionsSpatialID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, NumRows INTEGER, NumColumns INTEGER, NumCells INTEGER, CellSize DOUBLE, CellSizeUnits TEXT, CellArea DOUBLE, CellAreaOverride INTEGER, XLLCorner DOUBLE, YLLCorner DOUBLE, SRS TEXT, StratumFileName TEXT, SecondaryStratumFileName TEXT, StateClassFileName TEXT, AgeFileName TEXT)");
                store.ExecuteNonQuery("INSERT INTO STSim_InitialConditionsSpatial(ScenarioID, NumRows, NumColumns, NumCells, CellSize, CellSizeUnits, CellArea, CellAreaOverride, XLLCorner, YLLCorner, SRS, StratumFileName, SecondaryStratumFileName, StateClassFileName, AgeFileName) SELECT ScenarioID, NumRows, NumColumns, NumCells, CellSize, CellSizeUnits, CellArea, CellAreaOverride, XLLCorner, YLLCorner, SRS, StratumFileName, SecondaryStratumFileName, StateClassFileName, AgeFileName FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }
        }

        /// <summary>
        /// STSIM0000047
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update adds model names and display names to all existing charts
        /// </remarks>
        private static void STSIM0000047(DataStore store)
        {
            if (store.TableExists("STime_Chart"))
            {
                store.ExecuteNonQuery("UPDATE STime_Chart SET Model='stsim-model-transformer', ModelDisplayName='ST-Sim State and Transition'");
            }

            if (store.TableExists("STime_Map"))
            {
                store.ExecuteNonQuery("UPDATE STime_Map SET Model='stsim-model-transformer', ModelDisplayName='ST-Sim State and Transition'");
            }
        }

        /// <summary>
        /// This update adds a DistributionFrequency column to the relevant ST-Sim tables
        /// </summary>
        /// <param name="store"></param>
        /// <remarks></remarks>
        private static void STSIM0000048(DataStore store)
        {
            if (store.TableExists("STSim_TransitionTarget"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_TransitionTarget ADD COLUMN DistributionFrequencyID INTEGER");
                store.ExecuteNonQuery(string.Format(CultureInfo.InvariantCulture, "UPDATE STSim_TransitionTarget SET DistributionFrequencyID={0} WHERE DistributionType IS NOT NULL", Convert.ToInt32(StochasticTime.DistributionFrequency.Iteration, CultureInfo.InvariantCulture)));
            }

            if (store.TableExists("STSim_TransitionMultiplierValue"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_TransitionMultiplierValue ADD COLUMN DistributionFrequencyID INTEGER");
                store.ExecuteNonQuery(string.Format(CultureInfo.InvariantCulture, "UPDATE STSim_TransitionMultiplierValue SET DistributionFrequencyID={0} WHERE DistributionType IS NOT NULL", Convert.ToInt32(StochasticTime.DistributionFrequency.Iteration, CultureInfo.InvariantCulture)));
            }

            if (store.TableExists("STSim_TransitionAttributeTarget"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_TransitionAttributeTarget ADD COLUMN DistributionFrequencyID INTEGER");
                store.ExecuteNonQuery(string.Format(CultureInfo.InvariantCulture, "UPDATE STSim_TransitionAttributeTarget SET DistributionFrequencyID={0} WHERE DistributionType IS NOT NULL", Convert.ToInt32(StochasticTime.DistributionFrequency.Iteration, CultureInfo.InvariantCulture)));
            }

            if (store.TableExists("STSim_TransitionDirectionMultiplier"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_TransitionDirectionMultiplier ADD COLUMN DistributionFrequencyID INTEGER");
            }

            if (store.TableExists("STSim_TransitionSlopeMultiplier"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_TransitionSlopeMultiplier ADD COLUMN DistributionFrequencyID INTEGER");
            }

            if (store.TableExists("STSim_TransitionAdjacencyMultiplier"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_TransitionAdjacencyMultiplier ADD COLUMN DistributionFrequencyID INTEGER");
            }
        }

        /// <summary>
        /// STSIM0000049
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// We need to copy the data from the STime_DistribtionValue table to the STSim_DistributionValue table
        /// because the STSim_DistributionValue table is what ST-Sim's customized distribution support uses.
        /// 
        /// Also, the STime_DistributionValue table now has a distribution field which needs to have the ID for a  
        /// Uniform Distribution because before that's how it was being sampled.
        /// 
        /// Steps:
        /// 
        /// 1.  Create the STSim_DistributionValue table
        /// 2.  Copy the data from STime_DistribtionValue to STSim_DistributionValue
        /// 3.  Update STSim_DistributionValue.ValueDistributionTypeID with the ID for a Uniform Distribution
        /// 4.  Delete the contents of the STime_DistribtionValue table
        /// </remarks>
        private static void STSIM0000049(DataStore store)
        {
            //If neither of these tables exist then there is nothing to do

            if (!store.TableExists("STime_DistributionType") | !store.TableExists("STime_DistributionValue"))
            {
                return;
            }

            //1. Above
            store.ExecuteNonQuery("CREATE TABLE STSim_DistributionValue(DistributionValueID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, DistributionTypeID INTEGER, ExternalVariableTypeID INTEGER, ExternalVariableMin DOUBLE, ExternalVariableMax DOUBLE, Value DOUBLE, ValueDistributionTypeID INTEGER, ValueDistributionFrequency INTEGER, ValueDistributionSD DOUBLE, ValueDistributionMin DOUBLE, ValueDistributionMax DOUBLE, ValueDistributionRelativeFrequency DOUBLE)");

            //2. Above
            store.ExecuteNonQuery("INSERT INTO STSim_DistributionValue(ScenarioID, Iteration, Timestep, DistributionTypeID, ExternalVariableTypeID, ExternalVariableMin, ExternalVariableMax, ValueDistributionMin, ValueDistributionMax, ValueDistributionRelativeFrequency) SELECT ScenarioID, Iteration, Timestep, DistributionTypeID, ExternalVariableTypeID, ExternalVariableMin, ExternalVariableMax, ValueDistributionMin, ValueDistributionMax, ValueDistributionRelativeFrequency FROM STime_DistributionValue");

            //3. Above
            DataTable Projects = store.CreateDataTable("SSim_Project");
            DataTable Scenarios = store.CreateDataTable("SSim_Scenario");
            Dictionary<int, int> IDLookup = new Dictionary<int, int>();

            foreach (DataRow ProjectRow in Projects.Rows)
            {
                int ProjectId = Convert.ToInt32(ProjectRow["ProjectID"], CultureInfo.InvariantCulture);
                DataTable DistributionTypes = store.CreateDataTableFromQuery(string.Format(CultureInfo.InvariantCulture, "SELECT * FROM STime_DistributionType WHERE ProjectID={0} AND Name='Uniform'", ProjectId), "DistributionTypes");
                Debug.Assert(DistributionTypes.Rows.Count == 1);

                if (DistributionTypes.Rows.Count == 1)
                {
                    IDLookup.Add(
                        ProjectId, Convert.
                        ToInt32(DistributionTypes.Rows[0]["DistributionTypeID"], CultureInfo.InvariantCulture));
                }
            }

            foreach (DataRow ScenarioRow in Scenarios.Rows)
            {
                int ScenarioId = Convert.ToInt32(ScenarioRow["ScenarioID"], CultureInfo.InvariantCulture);
                int ProjectId = Convert.ToInt32(ScenarioRow["ProjectID"], CultureInfo.InvariantCulture);
                Debug.Assert(IDLookup.ContainsKey(ProjectId));

                if (IDLookup.ContainsKey(ProjectId))
                {
                    store.ExecuteNonQuery(string.Format(
                        CultureInfo.InvariantCulture, 
                        "UPDATE STSim_DistributionValue SET ValueDistributionTypeID={0} WHERE ScenarioID={1}", 
                        IDLookup[ProjectId], ScenarioId));
                }
            }

            //4. Above
            store.ExecuteNonQuery("DROP TABLE STime_DistributionValue");
        }

        /// <summary>
        /// STSIM0000050
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update adds indexes to various tables to improve performance with large data sets
        /// </remarks>
        private static void STSIM0000050(DataStore store)
        {
            if (store.TableExists("STSim_InitialConditionsNonSpatialDistribution"))
            {
                store.ExecuteNonQuery("CREATE INDEX STSim_InitialConditionsNonSpatialDistribution_Index ON STSim_InitialConditionsNonSpatialDistribution (ScenarioID)");
            }

            if (store.TableExists("STSim_StateAttributeValue"))
            {
                store.ExecuteNonQuery("CREATE INDEX STSim_StateAttributeValue_Index ON STSim_StateAttributeValue (ScenarioID)");
            }

            if (store.TableExists("STSim_Transition"))
            {
                store.ExecuteNonQuery("CREATE INDEX STSim_Transition_Index ON STSim_Transition (ScenarioID)");
            }

            if (store.TableExists("STSim_TransitionAttributeTarget"))
            {
                store.ExecuteNonQuery("CREATE INDEX STSim_TransitionAttributeTarget_Index ON STSim_TransitionAttributeTarget (ScenarioID)");
            }

            if (store.TableExists("STSim_TransitionAttributeValue"))
            {
                store.ExecuteNonQuery("CREATE INDEX STSim_TransitionAttributeValue_Index ON STSim_TransitionAttributeValue (ScenarioID)");
            }

            if (store.TableExists("STSim_TransitionMultiplierValue"))
            {
                store.ExecuteNonQuery("CREATE INDEX STSim_TransitionMultiplierValue_Index ON STSim_TransitionMultiplierValue (ScenarioID)");
            }

            if (store.TableExists("STSim_TransitionTarget"))
            {
                store.ExecuteNonQuery("CREATE INDEX STSim_TransitionTarget_Index ON STSim_TransitionTarget (ScenarioID)");
            }
        }

        /// <summary>
        /// STSIM0000050
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update adds Iteration to the Initial Condition data sets, and splits the IC Spatial table into 2.
        /// </remarks>
        private static void STSIM0000051(DataStore store)
        {
            if (store.TableExists("STSim_InitialConditionsNonSpatialDistribution"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_InitialConditionsNonSpatialDistribution RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE STSim_InitialConditionsNonSpatialDistribution(InitialConditionsNonSpatialDistributionID INTEGER PRIMARY KEY AUTOINCREMENT,ScenarioID INTEGER,Iteration INTEGER, StratumID INTEGER,SecondaryStratumID INTEGER,StateClassID INTEGER,AgeMin INTEGER,AgeMax INTEGER,RelativeAmount DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO STSim_InitialConditionsNonSpatialDistribution(ScenarioID,StratumID,SecondaryStratumID,StateClassID,AgeMin,AgeMax,RelativeAmount) SELECT ScenarioID,StratumID,SecondaryStratumID,StateClassID,AgeMin,AgeMax,RelativeAmount FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            //Copy the raster metadata over to new table
            if (store.TableExists("STSim_InitialConditionsSpatial"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_InitialConditionsSpatial RENAME TO TEMP_TABLE");

                string sSQL = "CREATE TABLE STSim_InitialConditionsSpatialProperties (InitialConditionsSpatialPropertiesID INTEGER PRIMARY KEY AUTOINCREMENT,ScenarioID INTEGER,NumRows INTEGER,NumColumns INTEGER,NumCells INTEGER,CellSize DOUBLE,CellSizeUnits TEXT,CellArea DOUBLE,CellAreaOverride INTEGER,XLLCorner DOUBLE,YLLCorner DOUBLE,SRS TEXT)";
                store.ExecuteNonQuery(sSQL);

                sSQL = "insert into STSim_InitialConditionsSpatialProperties(ScenarioID,NumRows,NumColumns,NumCells,CellSize,CellSizeUnits,CellArea,CellAreaOverride,XLLCorner,YLLCorner,SRS) " + "select ScenarioID,NumRows,NumColumns,NumCells,CellSize,CellSizeUnits,CellArea,CellAreaOverride,XLLCorner,YLLCorner,SRS from TEMP_TABLE";
                store.ExecuteNonQuery(sSQL);

                // Drop columns transfered to Properties table, and add Iteration
                sSQL = "CREATE TABLE STSim_InitialConditionsSpatial (InitialConditionsSpatialID INTEGER PRIMARY KEY AUTOINCREMENT,ScenarioID INTEGER,Iteration INTEGER, StratumFileName TEXT,SecondaryStratumFileName TEXT,StateClassFileName TEXT,AgeFileName TEXT)";
                store.ExecuteNonQuery(sSQL);
                sSQL = "insert into STSim_InitialConditionsSpatial(ScenarioID,StratumFileName,SecondaryStratumFileName,StateClassFileName,AgeFileName) select ScenarioID,StratumFileName,SecondaryStratumFileName,StateClassFileName,AgeFileName from TEMP_TABLE";
                store.ExecuteNonQuery(sSQL);

                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }
        }

        /// <summary>
        /// STSIM0000052
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update adds an Iteration column to the STSim_TimeSinceTransitionRandomize table
        /// </remarks>
        private static void STSIM0000052(DataStore store)
        {
            if (store.TableExists("STSim_TimeSinceTransitionRandomize"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_TimeSinceTransitionRandomize ADD COLUMN Iteration INTEGER");
            }
        }

        /// <summary>
        /// STSIM0000053
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>This update adds a MaximizeFidelityToDistribution field to STSim_TransitionSizePrioritization</remarks>
        private static void STSIM0000053(DataStore store)
        {
            if (store.TableExists("STSim_TransitionSizePrioritization"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_TransitionSizePrioritization ADD COLUMN MaximizeFidelityToDistribution INTEGER");
            }
        }

        /// <summary>
        /// STSIM0000054
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>This update adds a MaximizeFidelityToTotalArea to STSim_TransitionSizePrioritization</remarks>
        private static void STSIM0000054(DataStore store)
        {
            if (store.TableExists("STSim_TransitionSizePrioritization"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_TransitionSizePrioritization ADD COLUMN MaximizeFidelityToTotalArea INTEGER");
            }
        }

        /// <summary>
        /// STSIM0000055
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update adds iteration and timestep fields to the following tables:
        /// 
        /// STSim_Transition
        /// STSim_DeterministicTransition
        /// </remarks>
        private static void STSIM0000055(DataStore store)
        {
            if (store.TableExists("STSim_Transition"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_Transition RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE STSim_Transition(TransitionID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumIDSource INTEGER, StateClassIDSource INTEGER, StratumIDDest INTEGER, StateClassIDDest INTEGER, TransitionTypeID INTEGER, Probability DOUBLE, Proportion DOUBLE, AgeMin INTEGER, AgeMax INTEGER, AgeRelative INTEGER, AgeReset INTEGER, TSTMin INTEGER, TSTMax INTEGER, TSTRelative INTEGER)");
                store.ExecuteNonQuery("INSERT INTO STSim_Transition(ScenarioID, StratumIDSource, StateClassIDSource, StratumIDDest, StateClassIDDest, TransitionTypeID, Probability, Proportion, AgeMin, AgeMax, AgeRelative, AgeReset, TSTMin, TSTMax, TSTRelative) SELECT ScenarioID, StratumIDSource, StateClassIDSource, StratumIDDest, StateClassIDDest, TransitionTypeID, Probability, Proportion, AgeMin, AgeMax, AgeRelative, AgeReset, TSTMin, TSTMax, TSTRelative FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("STSim_DeterministicTransition"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_DeterministicTransition RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE STSim_DeterministicTransition(DeterministicTransitionID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumIDSource INTEGER, StateClassIDSource INTEGER, StratumIDDest INTEGER, StateClassIDDest INTEGER, AgeMin INTEGER, AgeMax INTEGER, Location TEXT)");
                store.ExecuteNonQuery("INSERT INTO STSim_DeterministicTransition(ScenarioID, StratumIDSource, StateClassIDSource, StratumIDDest, StateClassIDDest, AgeMin, AgeMax, Location) SELECT ScenarioID, StratumIDSource, StateClassIDSource, StratumIDDest, StateClassIDDest, AgeMin, AgeMax, Location FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }
        }

        /// <summary>
        /// STSIM0000056
        /// </summary>
        /// <param name="store"></param>
        /// <remarks></remarks>
        private static void STSIM0000056(DataStore store)
        {
            if (store.TableExists("STSim_TransitionGroup"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_TransitionGroup ADD COLUMN IsAuto INTEGER");
            }

            if (store.TableExists("STSim_TransitionTypeGroup"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_TransitionTypeGroup ADD COLUMN IsAuto INTEGER");
            }
        }

        /// <summary>
        /// STSIM0000057
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// A bug in the transition diagram was causing invalid boolean values to be written to the AgeReset
        /// column which caused diagram editing to fail.
        /// </remarks>
        private static void STSIM0000057(DataStore store)
        {
            if (store.TableExists("STSim_Transition"))
            {
                store.ExecuteNonQuery("UPDATE STSim_Transition SET AgeReset=-1 WHERE AgeReset=1");
            }
        }

        /// <summary>
        /// STSIM0000058
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// Move the Spatial Output files to the new Datasheet based locations
        /// </remarks>
        private static void STSIM0000058(DataStore store)
        {
            // Loop thru all the results scenarios in the library
            DataTable dtScenarios = store.CreateDataTable("SSim_Scenario");

            string[,] outputDatasheets =
            {
                {"STSim_OutputSpatialTST", "tg-*-tst"},
                {"STSim_OutputSpatialStratum", "str"},
                {"STSim_OutputSpatialState", "sc"},
                {"STSim_OutputSpatialAge", "age"},
                {"STSim_OutputSpatialTransition", "tg-*"},
                {"STSim_OutputSpatialStateAttribute", "sa-*"},
                {"STSim_OutputSpatialTransitionAttribute", "ta-*"},
                {"STSim_OutputSpatialAverageTransitionProbability", "tgap-*"},
                {"SF_OutputSpatialStockType", "stk-*"},
                {"SF_OutputSpatialStockGroup", "stkg-*"},
                {"SF_OutputSpatialFlowType", "flo-*"},
                {"SF_OutputSpatialFlowGroup", "flog-*"}
            };

            foreach (DataRow row in dtScenarios.Rows)
            {
                int scenarioId = Convert.ToInt32(row["ScenarioID"], CultureInfo.InvariantCulture);

                for (int i = 0; i <= outputDatasheets.GetUpperBound(0); i++)
                {
                    string dsName = outputDatasheets[i, 0];
                    string fileFilter = outputDatasheets[i, 1];

                    string oldLocation = GetLegacyOutputFolder(store, scenarioId);
                    string newLocation = GetDatasheetOutputFolder(store, scenarioId, dsName);

                    if (Directory.Exists(oldLocation))
                    {
                        var files = Directory.GetFiles(oldLocation, "*" + fileFilter + ".*");

                        foreach (var oldFilename in files)
                        {
                            if (!Directory.Exists(newLocation))
                            {
                                Directory.CreateDirectory(newLocation);
                            }

                            string newFilename = null;
                            if (dsName == "STSim_OutputSpatialTST")
                            {
                                // This is a special case, because we want to rename to a generic form
                                newFilename = Path.GetFileName(oldFilename);
                                newFilename = newFilename.Replace("-tst", "").Replace("tg-", "tst-");
                                newFilename = Path.Combine(newLocation, newFilename);
                            }
                            else
                            {
                                newFilename = Path.Combine(newLocation, Path.GetFileName(oldFilename));
                            }

                            if (!File.Exists(newFilename))
                            {
                                File.Move(oldFilename, newFilename);
                                Debug.Print(string.Format(CultureInfo.InvariantCulture, "Moving spatial output file '{0}' to {1}", oldFilename, newLocation));
                            }
                        }
                    }
                }
            }

            // Rename any TST files already records in the STSim_OutputSpatialTST datasheet, to "tst-123".
            if (store.TableExists("STSim_OutputSpatialTST"))
            {
                store.ExecuteNonQuery("update STSim_OutputSpatialTST set filename = Replace(Replace(filename,'tg-','tst-'),'-tst.','.')");
            }
        }

        /// <summary>
        /// STSIM0000059
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// Map criteria used to be in the following form:
        /// 
        ///     sc|      //variable with no item id
        ///     tg-342   //variable with item id
        /// 
        /// Now it is in the form:
        /// 
        ///     sc|
        ///     tg-342-itemid-342-itemsrc-STSim_TransitionGroup
        ///     
        /// This update must convert all map criteria to the new form.  This update is specific to ST-Sim and
        /// StockFlow since the data sheet names must be known in order for a conversion to take place.
        /// </remarks>
        private static void STSIM0000059(DataStore store)
        {
            if (!store.TableExists("STime_Map"))
            {
                return;
            }

            DataTable dt = store.CreateDataTable("STime_Map");

            foreach (DataRow dr in dt.Rows)
            {
                if (dr["Criteria"] != DBNull.Value)
                {
                    int id = Convert.ToInt32(dr["MapID"], CultureInfo.InvariantCulture);
                    string cr = Convert.ToString(dr["Criteria"], CultureInfo.InvariantCulture);
                    string[] sp = cr.Split('|');
                    StringBuilder sb = new StringBuilder();

                    foreach (string s in sp)
                    {
                        sb.Append(ExpandMapCriteriaFrom1x(s));
                        sb.Append("|");
                    }

                    string newcr = sb.ToString().TrimEnd('|');
                    string query = string.Format(CultureInfo.InvariantCulture, "UPDATE STime_Map SET Criteria='{0}' WHERE MapID={1}", newcr, id);

                    store.ExecuteNonQuery(query);
                }
            }
        }

        /// <summary>
        /// STSIM0000060
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// Add Legend Column to Primary Stratum, State Class, and Transition Type, to aid in Map 
        /// Criteria legend definition.
        /// </remarks>
        private static void STSIM0000060(DataStore store)
        {
            if (store.TableExists("STSim_Stratum"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_Stratum ADD COLUMN Legend TEXT");
            }

            if (store.TableExists("STSim_StateClass"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_StateClass ADD COLUMN Legend TEXT");
            }

            if (store.TableExists("STSim_TransitionType"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_TransitionType ADD COLUMN Legend TEXT");
            }
        }

        /// <summary>
        /// SF0000061
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>Add missing index on STSim_DistributionValue and if missing drop</remarks>
        private static void STSIM0000061(DataStore store)
        {
            if (store.TableExists("STSim_DistributionValue"))
            {
                store.ExecuteNonQuery("DROP INDEX IF EXISTS STSim_DistributionValue_Index");
                store.ExecuteNonQuery("CREATE INDEX STSim_DistributionValue_Index ON STSim_DistributionValue(ScenarioID)");
            }
        }

        /// <summary>
        /// SF0000062
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>This update adds a tertiary stratum to all applicable tables</remarks>
        private static void STSIM0000062(DataStore store)
        {
            if (store.TableExists("STSim_Terminology"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_Terminology RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE STSim_Terminology(TerminologyID Integer PRIMARY KEY AUTOINCREMENT, ProjectID Integer, AmountLabel TEXT, AmountUnits Integer, StateLabelX TEXT, StateLabelY TEXT, PrimaryStratumLabel TEXT, SecondaryStratumLabel TEXT, TertiaryStratumLabel TEXT, TimestepUnits TEXT)");
                store.ExecuteNonQuery("INSERT INTO STSim_Terminology(ProjectID, AmountLabel, AmountUnits, StateLabelX, StateLabelY, PrimaryStratumLabel, SecondaryStratumLabel, TertiaryStratumLabel, TimestepUnits) SELECT ProjectID, AmountLabel, AmountUnits, StateLabelX, StateLabelY, PrimaryStratumLabel, SecondaryStratumLabel, 'Tertiary Stratum', TimestepUnits FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("STSim_Transition"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_Transition RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE STSim_Transition(TransitionID Integer PRIMARY KEY AUTOINCREMENT, ScenarioID Integer, Iteration Integer, Timestep Integer, StratumIDSource Integer, StateClassIDSource Integer, StratumIDDest Integer, StateClassIDDest Integer, SecondaryStratumID Integer, TertiaryStratumID Integer, TransitionTypeID Integer, Probability Double, Proportion Double, AgeMin Integer, AgeMax Integer, AgeRelative Integer, AgeReset Integer, TSTMin Integer, TSTMax Integer, TSTRelative Integer)");
                store.ExecuteNonQuery("INSERT INTO STSim_Transition(ScenarioID, Iteration, Timestep, StratumIDSource, StateClassIDSource, StratumIDDest, StateClassIDDest, TransitionTypeID, Probability, Proportion, AgeMin, AgeMax, AgeRelative, AgeReset, TSTMin, TSTMax, TSTRelative) SELECT ScenarioID, Iteration, Timestep, StratumIDSource, StateClassIDSource, StratumIDDest, StateClassIDDest, TransitionTypeID, Probability, Proportion, AgeMin, AgeMax, AgeRelative, AgeReset, TSTMin, TSTMax, TSTRelative FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("STSim_InitialConditionsNonSpatialDistribution"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_InitialConditionsNonSpatialDistribution RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE STSim_InitialConditionsNonSpatialDistribution(InitialConditionsNonSpatialDistributionID Integer PRIMARY KEY AUTOINCREMENT, ScenarioID Integer, Iteration Integer, StratumID Integer, SecondaryStratumID Integer, TertiaryStratumID Integer, StateClassID Integer, AgeMin Integer, AgeMax Integer, RelativeAmount DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO STSim_InitialConditionsNonSpatialDistribution(ScenarioID, Iteration, StratumID, SecondaryStratumID, StateClassID, AgeMin, AgeMax, RelativeAmount) SELECT ScenarioID, Iteration, StratumID, SecondaryStratumID, StateClassID, AgeMin, AgeMax, RelativeAmount FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("STSim_InitialConditionsSpatial"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_InitialConditionsSpatial RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE STSim_InitialConditionsSpatial(InitialConditionsSpatialID Integer PRIMARY KEY AUTOINCREMENT, ScenarioID Integer, Iteration Integer, StratumFileName TEXT, SecondaryStratumFileName TEXT, TertiaryStratumFileName TEXT, StateClassFileName TEXT, AgeFileName TEXT)");
                store.ExecuteNonQuery("INSERT INTO STSim_InitialConditionsSpatial(ScenarioID, Iteration, StratumFileName, SecondaryStratumFileName, StateClassFileName, AgeFileName) SELECT ScenarioID, Iteration, StratumFileName, SecondaryStratumFileName, StateClassFileName, AgeFileName FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("STSim_TransitionTarget"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_TransitionTarget RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE STSim_TransitionTarget(TransitionTargetID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, TransitionGroupID INTEGER, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE, DistributionFrequencyID INTEGER)");
                store.ExecuteNonQuery("INSERT INTO STSim_TransitionTarget(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax, DistributionFrequencyID) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax, DistributionFrequencyID FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("STSim_TransitionMultiplierValue"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_TransitionMultiplierValue RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE STSim_TransitionMultiplierValue(TransitionMultiplierValueID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, StateClassID INTEGER, TransitionGroupID INTEGER, TransitionMultiplierTypeID  INTEGER, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE, DistributionFrequencyID INTEGER)");
                store.ExecuteNonQuery("INSERT INTO STSim_TransitionMultiplierValue(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, StateClassID, TransitionGroupID, TransitionMultiplierTypeID, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax, DistributionFrequencyID) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, StateClassID, TransitionGroupID, TransitionMultiplierTypeID, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax, DistributionFrequencyID FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("STSim_TimeSinceTransitionGroup"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_TimeSinceTransitionGroup RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE STSim_TimeSinceTransitionGroup(TimeSinceTransitionGroupID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, TransitionTypeID INTEGER, TransitionGroupID INTEGER)");
                store.ExecuteNonQuery("INSERT INTO STSim_TimeSinceTransitionGroup(ScenarioID, StratumID, SecondaryStratumID, TransitionTypeID, TransitionGroupID) SELECT ScenarioID, StratumID, SecondaryStratumID, TransitionTypeID, TransitionGroupID FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("STSim_TimeSinceTransitionRandomize"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_TimeSinceTransitionRandomize RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE STSim_TimeSinceTransitionRandomize(TimeSinceTransitionRandomizeID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, TransitionGroupID INTEGER, StateClassID INTEGER, MinInitialTST INTEGER, MaxInitialTST INTEGER, Iteration INTEGER)");
                store.ExecuteNonQuery("INSERT INTO STSim_TimeSinceTransitionRandomize(ScenarioID, StratumID, SecondaryStratumID, TransitionGroupID, StateClassID, MinInitialTST, MaxInitialTST, Iteration) SELECT ScenarioID, StratumID, SecondaryStratumID, TransitionGroupID, StateClassID, MinInitialTST, MaxInitialTST, Iteration FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("STSim_StateAttributeValue"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_StateAttributeValue RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE STSim_StateAttributeValue(StateAttributeValueID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, StateClassID INTEGER, StateAttributeTypeID  INTEGER, AgeMin INTEGER, AgeMax INTEGER, Value DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO STSim_StateAttributeValue(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, StateClassID, StateAttributeTypeID, AgeMin, AgeMax, Value) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, StateClassID, StateAttributeTypeID, AgeMin, AgeMax, Value FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("STSim_TransitionAttributeValue"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_TransitionAttributeValue RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE STSim_TransitionAttributeValue(TransitionAttributeValueID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, TransitionGroupID INTEGER, StateClassID INTEGER, TransitionAttributeTypeID  INTEGER, AgeMin INTEGER, AgeMax INTEGER, Value DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO STSim_TransitionAttributeValue(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, StateClassID, TransitionAttributeTypeID, AgeMin, AgeMax, Value) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, StateClassID, TransitionAttributeTypeID, AgeMin, AgeMax, Value FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("STSim_TransitionAttributeTarget"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_TransitionAttributeTarget RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE STSim_TransitionAttributeTarget(TransitionAttributeTargetID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, TransitionGroupID INTEGER, TransitionAttributeTypeID INTEGER, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE, DistributionFrequencyID INTEGER)");
                store.ExecuteNonQuery("INSERT INTO STSim_TransitionAttributeTarget(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionAttributeTypeID, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax, DistributionFrequencyID) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionAttributeTypeID, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax, DistributionFrequencyID FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("STSim_DistributionValue"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_DistributionValue RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE STSim_DistributionValue(DistributionValueID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, DistributionTypeID INTEGER, ExternalVariableTypeID INTEGER, ExternalVariableMin DOUBLE, ExternalVariableMax DOUBLE, Value DOUBLE, ValueDistributionTypeID INTEGER, ValueDistributionFrequency INTEGER, ValueDistributionSD DOUBLE, ValueDistributionMin DOUBLE, ValueDistributionMax DOUBLE, ValueDistributionRelativeFrequency DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO STSim_DistributionValue(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, DistributionTypeID, ExternalVariableTypeID, ExternalVariableMin, ExternalVariableMax, Value, ValueDistributionTypeID, ValueDistributionFrequency, ValueDistributionSD, ValueDistributionMin, ValueDistributionMax, ValueDistributionRelativeFrequency) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, DistributionTypeID, ExternalVariableTypeID, ExternalVariableMin, ExternalVariableMax, Value, ValueDistributionTypeID, ValueDistributionFrequency, ValueDistributionSD, ValueDistributionMin, ValueDistributionMax, ValueDistributionRelativeFrequency FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("STSim_TransitionDirectionMultiplier"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_TransitionDirectionMultiplier RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE STSim_TransitionDirectionMultiplier(TransitionDirectionMultiplierID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, TransitionGroupID INTEGER, CardinalDirection INTEGER, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE, DistributionFrequencyID INTEGER)");
                store.ExecuteNonQuery("INSERT INTO STSim_TransitionDirectionMultiplier(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, CardinalDirection, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax, DistributionFrequencyID) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, CardinalDirection, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax, DistributionFrequencyID FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("STSim_TransitionSlopeMultiplier"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_TransitionSlopeMultiplier RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE STSim_TransitionSlopeMultiplier(TransitionSlopeMultiplierID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, TransitionGroupID INTEGER, Slope INTEGER, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE, DistributionFrequencyID INTEGER)");
                store.ExecuteNonQuery("INSERT INTO STSim_TransitionSlopeMultiplier(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, Slope, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax, DistributionFrequencyID) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, Slope, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax, DistributionFrequencyID FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("STSim_TransitionAdjacencyMultiplier"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_TransitionAdjacencyMultiplier RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE STSim_TransitionAdjacencyMultiplier(TransitionAdjacencyMultiplierID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, TransitionGroupID INTEGER, AttributeValue DOUBLE, Amount DOUBLE, DistributionType INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE, DistributionFrequencyID INTEGER)");
                store.ExecuteNonQuery("INSERT INTO STSim_TransitionAdjacencyMultiplier(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, AttributeValue, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax, DistributionFrequencyID) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, AttributeValue, Amount, DistributionType, DistributionSD, DistributionMin, DistributionMax, DistributionFrequencyID FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("STSim_TransitionPathwayAutoCorrelation"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_TransitionPathwayAutoCorrelation RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE STSim_TransitionPathwayAutoCorrelation(TransitionPathwayAutoCorrelationID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, TransitionGroupID INTEGER, AutoCorrelationFactor DOUBLE, SpreadOnlyToLike INTEGER)");
                store.ExecuteNonQuery("INSERT INTO STSim_TransitionPathwayAutoCorrelation(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, AutoCorrelationFactor, SpreadOnlyToLike) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, AutoCorrelationFactor, SpreadOnlyToLike FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("STSim_OutputStratum"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_OutputStratum RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE STSim_OutputStratum(ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, Amount DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO STSim_OutputStratum(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, Amount FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("STSim_OutputStratumState"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_OutputStratumState RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE STSim_OutputStratumState(ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, StateClassID INTEGER, StateLabelXID INTEGER, StateLabelYID INTEGER, AgeMin INTEGER, AgeMax INTEGER, AgeClass INTEGER, Amount DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO STSim_OutputStratumState(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, StateClassID, StateLabelXID, StateLabelYID, AgeMin, AgeMax, AgeClass, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, StateClassID, StateLabelXID, StateLabelYID, AgeMin, AgeMax, AgeClass, Amount FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("STSim_OutputStratumTransition"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_OutputStratumTransition RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE STSim_OutputStratumTransition(ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, TransitionGroupID INTEGER, AgeMin INTEGER, AgeMax INTEGER, AgeClass INTEGER, Amount DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO STSim_OutputStratumTransition(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, AgeMin, AgeMax, AgeClass, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionGroupID, AgeMin, AgeMax, AgeClass, Amount FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("STSim_OutputStratumTransitionState"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_OutputStratumTransitionState RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE STSim_OutputStratumTransitionState(ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, TransitionTypeID INTEGER, StateClassID INTEGER, EndStateClassID INTEGER, Amount DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO STSim_OutputStratumTransitionState(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionTypeID, StateClassID, EndStateClassID, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionTypeID, StateClassID, EndStateClassID, Amount FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("STSim_OutputStateAttribute"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_OutputStateAttribute RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE STSim_OutputStateAttribute(ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, StateAttributeTypeID INTEGER, AgeMin INTEGER, AgeMax INTEGER, AgeClass INTEGER, Amount DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO STSim_OutputStateAttribute(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, StateAttributeTypeID, AgeMin, AgeMax, AgeClass, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, StateAttributeTypeID, AgeMin, AgeMax, AgeClass, Amount FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("STSim_OutputTransitionAttribute"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_OutputTransitionAttribute RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE STSim_OutputTransitionAttribute(ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, TransitionAttributeTypeID INTEGER, AgeMin INTEGER, AgeMax INTEGER, AgeClass INTEGER, Amount DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO STSim_OutputTransitionAttribute(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionAttributeTypeID, AgeMin, AgeMax, AgeClass, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TransitionAttributeTypeID, AgeMin, AgeMax, AgeClass, Amount FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }
        }

        /// <summary>
        /// SF0000063
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>Adds Omit fields for Secondary and Tertiary Strata to OutputOptions</remarks>
        private static void STSIM0000063(DataStore store)
        {
            if (store.TableExists("STSim_OutputOptions"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_OutputOptions RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE STSim_OutputOptions(OutputOptionsID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, SummaryOutputSC INTEGER, SummaryOutputSCTimesteps INTEGER, SummaryOutputSCZeroValues INTEGER, SummaryOutputTR INTEGER, SummaryOutputTRTimesteps INTEGER, SummaryOutputTRIntervalMean INTEGER, SummaryOutputTRSC INTEGER, SummaryOutputTRSCTimesteps INTEGER, SummaryOutputSA INTEGER, SummaryOutputSATimesteps INTEGER, SummaryOutputTA INTEGER, SummaryOutputTATimesteps INTEGER, SummaryOutputOmitSS INTEGER, SummaryOutputOmitTS INTEGER, RasterOutputSC INTEGER, RasterOutputSCTimesteps INTEGER, RasterOutputTR INTEGER, RasterOutputTRTimesteps INTEGER, RasterOutputAge INTEGER, RasterOutputAgeTimesteps INTEGER, RasterOutputTST INTEGER, RasterOutputTSTTimesteps INTEGER, RasterOutputST INTEGER, RasterOutputSTTimesteps INTEGER, RasterOutputSA INTEGER, RasterOutputSATimesteps INTEGER, RasterOutputTA INTEGER, RasterOutputTATimesteps INTEGER, RasterOutputAATP INTEGER, RasterOutputAATPTimesteps INTEGER)");
                store.ExecuteNonQuery("INSERT INTO STSim_OutputOptions(ScenarioID, SummaryOutputSC, SummaryOutputSCTimesteps, SummaryOutputSCZeroValues, SummaryOutputTR, SummaryOutputTRTimesteps, SummaryOutputTRIntervalMean, SummaryOutputTRSC, SummaryOutputTRSCTimesteps, SummaryOutputSA, SummaryOutputSATimesteps, SummaryOutputTA, SummaryOutputTATimesteps, RasterOutputSC, RasterOutputSCTimesteps, RasterOutputTR, RasterOutputTRTimesteps, RasterOutputAge, RasterOutputAgeTimesteps, RasterOutputTST, RasterOutputTSTTimesteps, RasterOutputST, RasterOutputSTTimesteps, RasterOutputSA, RasterOutputSATimesteps, RasterOutputTA, RasterOutputTATimesteps, RasterOutputAATP, RasterOutputAATPTimesteps) SELECT ScenarioID , SummaryOutputSC, SummaryOutputSCTimesteps, SummaryOutputSCZeroValues, SummaryOutputTR, SummaryOutputTRTimesteps, SummaryOutputTRIntervalMean, SummaryOutputTRSC, SummaryOutputTRSCTimesteps, SummaryOutputSA, SummaryOutputSATimesteps, SummaryOutputTA, SummaryOutputTATimesteps, RasterOutputSC, RasterOutputSCTimesteps, RasterOutputTR, RasterOutputTRTimesteps, RasterOutputAge, RasterOutputAgeTimesteps, RasterOutputTST, RasterOutputTSTTimesteps, RasterOutputST, RasterOutputSTTimesteps, RasterOutputSA, RasterOutputSATimesteps, RasterOutputTA, RasterOutputTATimesteps, RasterOutputAATP, RasterOutputAATPTimesteps FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }
        }

        /// <summary>
        /// SF0000064
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update changes the STSim_TransitionPathwayAutoCorrelation table as follows:
        /// 1. Renames the AutoCorrelationFactor and SpreadOnlyToLike fields
        /// 2. Chagne the AutoCorrelationFactor field to be an INTEGER (Boolean)
        /// </remarks>
        private static void STSIM0000064(DataStore store)
        {
            if (store.TableExists("STSim_TransitionPathwayAutoCorrelation"))
            {
                store.ExecuteNonQuery("UPDATE STSim_TransitionPathwayAutoCorrelation SET AutoCorrelationFactor = -1 WHERE AutoCorrelationFactor <> 0");
                store.ExecuteNonQuery("UPDATE STSim_TransitionPathwayAutoCorrelation SET SpreadOnlyToLike = 1 WHERE SpreadOnlyToLike <> 0");

                store.ExecuteNonQuery("ALTER TABLE STSim_TransitionPathwayAutoCorrelation RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE STSim_TransitionPathwayAutoCorrelation(TransitionPathwayAutoCorrelationID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, TransitionGroupID INTEGER, AutoCorrelation INTEGER, SpreadTo INTEGER)");
                store.ExecuteNonQuery("INSERT INTO STSim_TransitionPathwayAutoCorrelation(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TertiaryStratumID, TransitionGroupID, AutoCorrelation, SpreadTo) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TertiaryStratumID, TransitionGroupID, AutoCorrelationFactor, SpreadOnlyToLike FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }
        }

        /// <summary>
        /// STSIM0000065
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update restores indexes that were dropped as a result of a previous alteration to a table
        /// </remarks>
        private static void STSIM0000065(DataStore store)
        {
            UpdateProvider.CreateIndex(store, "STSim_InitialConditionsNonSpatialDistribution", new[] {"ScenarioID"});
            UpdateProvider.CreateIndex(store, "STSim_StateAttributeValue", new[] {"ScenarioID"});
            UpdateProvider.CreateIndex(store, "STSim_Transition", new[] {"ScenarioID"});
            UpdateProvider.CreateIndex(store, "STSim_TransitionAttributeTarget", new[] {"ScenarioID"});
            UpdateProvider.CreateIndex(store, "STSim_TransitionAttributeValue", new[] {"ScenarioID"});
            UpdateProvider.CreateIndex(store, "STSim_TransitionMultiplierValue", new[] {"ScenarioID"});
            UpdateProvider.CreateIndex(store, "STSim_TransitionTarget", new[] {"ScenarioID"});
            UpdateProvider.CreateIndex(store, "STSim_OutputStateAttribute", new[] {"ScenarioID", "Iteration", "Timestep", "StratumID", "SecondaryStratumID", "TertiaryStratumID", "StateAttributeTypeID", "AgeClass"});
            UpdateProvider.CreateIndex(store, "STSim_OutputStratum", new[] {"ScenarioID", "Iteration", "Timestep", "StratumID", "SecondaryStratumID", "TertiaryStratumID"});
            UpdateProvider.CreateIndex(store, "STSim_OutputStratumState", new[] {"ScenarioID", "Iteration", "Timestep", "StratumID", "SecondaryStratumID", "TertiaryStratumID", "StateClassID", "StateLabelXID", "StateLabelYID", "AgeClass"});
            UpdateProvider.CreateIndex(store, "STSim_OutputStratumTransition", new[] {"ScenarioID", "Iteration", "Timestep", "StratumID", "SecondaryStratumID", "TertiaryStratumID", "TransitionGroupID", "AgeClass"});
            UpdateProvider.CreateIndex(store, "STSim_OutputStratumTransitionState", new[] {"ScenarioID", "Iteration", "Timestep", "StratumID", "SecondaryStratumID", "TertiaryStratumID", "TransitionTypeID", "StateClassID", "EndStateClassID"});
            UpdateProvider.CreateIndex(store, "STSim_OutputTransitionAttribute", new[] {"ScenarioID", "Iteration", "Timestep", "StratumID", "SecondaryStratumID", "TertiaryStratumID", "TransitionAttributeTypeID", "AgeClass"});
        }

        /// <summary>
        /// STSIM0000066
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update restores indexes that were dropped as a result of a previous alteration to a table
        /// </remarks>
        private static void STSIM0000066(DataStore store)
        {
            UpdateProvider.CreateIndex(store, "STSim_DistributionValue", new[] {"ScenarioID"});
        }

        /// <summary>
        /// STSIM0000067
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update:     
        /// (1.) Creates a record in Transition Simulation Groups for each Transition Group in Transtion Types By Group
        ///      where IsPrimary is either yes, or null and the group is not auto-generated.
        /// (2.) Removes the IsPrimary field from STSim_TransitionTypeGroup     
        /// </remarks>
        private static void STSIM0000067(DataStore store)
        {
            //We need to create the Transition Simulation Group table first

            store.ExecuteNonQuery("CREATE TABLE STSim_TransitionSimulationGroup(TransitionSimulationGroupID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, TransitionGroupID INTEGER)");

            //1.  Above

            Dictionary<string, bool> AlreadyAdded = new Dictionary<string, bool>();
            DataTable TransitionTypeGroupTable = store.CreateDataTable("STSim_TransitionTypeGroup");

            foreach(DataRow dr in TransitionTypeGroupTable.Rows)
            {
                int ProjectID = Convert.ToInt32(dr["ProjectID"], CultureInfo.InvariantCulture);
                int TransitionGroupID = Convert.ToInt32(dr["TransitionGroupID"], CultureInfo.InvariantCulture);
                string k = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", ProjectID, TransitionGroupID);

                if (!AlreadyAdded.ContainsKey(k))
                {
                    object ObjIsPrimary = dr["IsPrimary"];
                    object ObjIsAuto = dr["IsAuto"];
                    bool IsPrimary = (ObjIsPrimary == DBNull.Value || Convert.ToInt32(ObjIsPrimary, CultureInfo.InvariantCulture) != 0);
                    bool IsAuto = (ObjIsAuto != DBNull.Value && Convert.ToInt32(ObjIsAuto, CultureInfo.InvariantCulture) != 0);

                    if (IsPrimary && !IsAuto)
                    {
                        int NextId = Library.GetNextSequenceId(store);

                        string query = string.Format(CultureInfo.InvariantCulture,
                            "INSERT INTO STSim_TransitionSimulationGroup(TransitionSimulationGroupID, ProjectID, TransitionGroupID) " +
                            "VALUES({0},{1},{2})", NextId, ProjectID, TransitionGroupID);

                        store.ExecuteNonQuery(query);
                        AlreadyAdded.Add(k, true);
                    }                    
                }        
            }

            //2.  Above

            store.ExecuteNonQuery("ALTER TABLE STSim_TransitionTypeGroup RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE STSim_TransitionTypeGroup(TransitionTypeGroupID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, TransitionTypeID INTEGER, TransitionGroupID INTEGER, IsAuto INTEGER)");
            store.ExecuteNonQuery("INSERT INTO STSim_TransitionTypeGroup(TransitionTypeGroupID, ProjectID, TransitionTypeID, TransitionGroupID, IsAuto) SELECT TransitionTypeGroupID, ProjectID, TransitionTypeID, TransitionGroupID, IsAuto FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
        }

        /// <summary>
        /// STSIM0000068
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This adds age output configuration columns to STSim_OutputOptions
        /// </remarks>
        private static void STSIM0000068(DataStore store)
        {
            string CreateQuery =
               "CREATE TABLE STSim_OutputOptions(" +
               "OutputOptionsID             INTEGER PRIMARY KEY AUTOINCREMENT," +
               "ScenarioID                  INTEGER," +
               "SummaryOutputSC             INTEGER," +
               "SummaryOutputSCTimesteps    INTEGER," +
               "SummaryOutputSCAges         INTEGER," +
               "SummaryOutputSCZeroValues   INTEGER," +
               "SummaryOutputTR             INTEGER," +
               "SummaryOutputTRTimesteps    INTEGER," +
               "SummaryOutputTRAges         INTEGER," +
               "SummaryOutputTRIntervalMean INTEGER," +
               "SummaryOutputTRSC           INTEGER," +
               "SummaryOutputTRSCTimesteps  INTEGER," +
               "SummaryOutputSA             INTEGER," +
               "SummaryOutputSATimesteps    INTEGER," +
               "SummaryOutputSAAges         INTEGER," +
               "SummaryOutputTA             INTEGER," +
               "SummaryOutputTATimesteps    INTEGER," +
               "SummaryOutputTAAges         INTEGER," +
               "SummaryOutputOmitSS         INTEGER," +
               "SummaryOutputOmitTS         INTEGER," +
               "RasterOutputSC              INTEGER," +
               "RasterOutputSCTimesteps     INTEGER," +
               "RasterOutputTR              INTEGER," +
               "RasterOutputTRTimesteps     INTEGER," +
               "RasterOutputAge             INTEGER," +
               "RasterOutputAgeTimesteps    INTEGER," +
               "RasterOutputTST             INTEGER," +
               "RasterOutputTSTTimesteps    INTEGER," +
               "RasterOutputST              INTEGER," +
               "RasterOutputSTTimesteps     INTEGER," +
               "RasterOutputSA              INTEGER," +
               "RasterOutputSATimesteps     INTEGER," +
               "RasterOutputTA              INTEGER," +
               "RasterOutputTATimesteps     INTEGER," +
               "RasterOutputAATP            INTEGER," +
               "RasterOutputAATPTimesteps   INTEGER)";

            store.ExecuteNonQuery("ALTER TABLE STSim_OutputOptions RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery(CreateQuery);

            store.ExecuteNonQuery("INSERT INTO STSim_OutputOptions(" +
               "ScenarioID," +
               "SummaryOutputSC," +
               "SummaryOutputSCTimesteps," +
               "SummaryOutputSCZeroValues," +
               "SummaryOutputTR," +
               "SummaryOutputTRTimesteps," +
               "SummaryOutputTRIntervalMean," +
               "SummaryOutputTRSC," +
               "SummaryOutputTRSCTimesteps ," +
               "SummaryOutputSA," +
               "SummaryOutputSATimesteps," +
               "SummaryOutputTA," +
               "SummaryOutputTATimesteps," +
               "SummaryOutputOmitSS," +
               "SummaryOutputOmitTS," +
               "RasterOutputSC," +
               "RasterOutputSCTimesteps," +
               "RasterOutputTR," +
               "RasterOutputTRTimesteps," +
               "RasterOutputAge," +
               "RasterOutputAgeTimesteps," +
               "RasterOutputTST," +
               "RasterOutputTSTTimesteps," +
               "RasterOutputST," +
               "RasterOutputSTTimesteps," +
               "RasterOutputSA," +
               "RasterOutputSATimesteps," +
               "RasterOutputTA," +
               "RasterOutputTATimesteps," +
               "RasterOutputAATP," +
               "RasterOutputAATPTimesteps) " +   
               "SELECT " + 
               "ScenarioID," +
               "SummaryOutputSC," +
               "SummaryOutputSCTimesteps," +
               "SummaryOutputSCZeroValues," +
               "SummaryOutputTR," +
               "SummaryOutputTRTimesteps," +
               "SummaryOutputTRIntervalMean," +
               "SummaryOutputTRSC," +
               "SummaryOutputTRSCTimesteps ," +
               "SummaryOutputSA," +
               "SummaryOutputSATimesteps," +
               "SummaryOutputTA," +
               "SummaryOutputTATimesteps," +
               "SummaryOutputOmitSS," +
               "SummaryOutputOmitTS," +
               "RasterOutputSC," +
               "RasterOutputSCTimesteps," +
               "RasterOutputTR," +
               "RasterOutputTRTimesteps," +
               "RasterOutputAge," +
               "RasterOutputAgeTimesteps," +
               "RasterOutputTST," +
               "RasterOutputTSTTimesteps," +
               "RasterOutputST," +
               "RasterOutputSTTimesteps," +
               "RasterOutputSA," +
               "RasterOutputSATimesteps," +
               "RasterOutputTA," +
               "RasterOutputTATimesteps," +
               "RasterOutputAATP," +
               "RasterOutputAATPTimesteps " +
               "FROM TEMP_TABLE");

            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
        }
    }
}
