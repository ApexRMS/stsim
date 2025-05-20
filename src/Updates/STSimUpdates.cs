// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Diagnostics;
using System.Collections.Generic;
using SyncroSim.Core;
using System.Globalization;
using static System.Windows.Forms.AxHost;

namespace SyncroSim.STSim
{
    internal partial class STSimUpdates : DotNetUpdateProvider
    {
        protected override IEnumerable<UpdateProvider> GetAdditional2xLegacyUpdateProviders(DataStore store)
        {
            //The stsim and stsimsf packges were combined for v3, but we need to keep their
            //updates separate so we return the stsimsf update provider here. Note, however,
            //that we don't want to try to do a legacy update if there is no schema table.

            List<UpdateProvider> l = new List<UpdateProvider>();

            if (store.TableExists("stsimsf_Schema"))
            {
                l.Add(new SFUpdates());
            }

            return l;
        }

        [UpdateAttribute(0.101, "This update adds support for transition events in various tables")]
        public static void Update_0_101(DataStore store)
        {
            if (store.TableExists("STSim_OutputOptions"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_OutputOptions ADD COLUMN RasterOutputTransitionEvents INTEGER");
                store.ExecuteNonQuery("ALTER TABLE STSim_OutputOptions ADD COLUMN RasterOutputTransitionEventTimesteps INTEGER");
            }

            if (store.TableExists("STSim_OutputStratumTransition"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_OutputStratumTransition ADD COLUMN SizeClassID INTEGER");
                store.ExecuteNonQuery("ALTER TABLE STSim_OutputStratumTransition ADD COLUMN EventID INTEGER");
            }
        }

        [UpdateAttribute(0.102, "This update adds an IsAutoName column to the STSim_StateClass table")]
        public static void Update_0_102(DataStore store)
        {
            if (store.TableExists("STSim_StateClass"))
            {
                store.ExecuteNonQuery("ALTER TABLE STSim_StateClass ADD COLUMN IsAutoName INTEGER");
            }
        }

        [UpdateAttribute(0.103, "This update will add an IsAutoName column to the STSim_StateClass table")]
        public static void Update_0_103(DataStore store)
        {
            RenameTable(store, "STSim_Processing", "stsim_Multiprocessing");
            RenameTablesWithPrefix(store, "STSim_", "stsim_");

            store.ExecuteNonQuery("DROP INDEX IF EXISTS STSim_InitialConditionsNonSpatialDistribution_Index");
            store.ExecuteNonQuery("DROP INDEX IF EXISTS STSim_StateAttributeValue_Index");
            store.ExecuteNonQuery("DROP INDEX IF EXISTS STSim_Transition_Index");
            store.ExecuteNonQuery("DROP INDEX IF EXISTS STSim_TransitionAttributeTarget_Index");
            store.ExecuteNonQuery("DROP INDEX IF EXISTS STSim_TransitionAttributeValue_Index");
            store.ExecuteNonQuery("DROP INDEX IF EXISTS STSim_TransitionMultiplierValue_Index");
            store.ExecuteNonQuery("DROP INDEX IF EXISTS STSim_TransitionTarget_Index");
            store.ExecuteNonQuery("DROP INDEX IF EXISTS STSim_DistributionValue_Index");
            store.ExecuteNonQuery("DROP INDEX IF EXISTS STSim_OutputStateAttribute_Index");
            store.ExecuteNonQuery("DROP INDEX IF EXISTS STSim_OutputStratum_Index");
            store.ExecuteNonQuery("DROP INDEX IF EXISTS STSim_OutputStratumState_Index");
            store.ExecuteNonQuery("DROP INDEX IF EXISTS STSim_OutputStratumTransition_Index");
            store.ExecuteNonQuery("DROP INDEX IF EXISTS STSim_OutputStratumTransitionState_Index");
            store.ExecuteNonQuery("DROP INDEX IF EXISTS STSim_OutputTransitionAttribute_Index");

            CreateIndex(store, "stsim_InitialConditionsNonSpatialDistribution", new[] { "ScenarioID" });
            CreateIndex(store, "stsim_StateAttributeValue", new[] { "ScenarioID" });
            CreateIndex(store, "stsim_Transition", new[] { "ScenarioID" });
            CreateIndex(store, "stsim_TransitionAttributeTarget", new[] { "ScenarioID" });
            CreateIndex(store, "stsim_TransitionAttributeValue", new[] { "ScenarioID" });
            CreateIndex(store, "stsim_TransitionMultiplierValue", new[] { "ScenarioID" });
            CreateIndex(store, "stsim_TransitionTarget", new[] { "ScenarioID" });
            CreateIndex(store, "stsim_DistributionValue", new[] { "ScenarioID" });
            CreateIndex(store, "stsim_OutputStateAttribute", new[] { "ScenarioID", "Iteration", "Timestep", "StratumID", "SecondaryStratumID", "TertiaryStratumID", "StateAttributeTypeID", "AgeClass" });
            CreateIndex(store, "stsim_OutputStratum", new[] { "ScenarioID", "Iteration", "Timestep", "StratumID", "SecondaryStratumID", "TertiaryStratumID" });
            CreateIndex(store, "stsim_OutputStratumState", new[] { "ScenarioID", "Iteration", "Timestep", "StratumID", "SecondaryStratumID", "TertiaryStratumID", "StateClassID", "StateLabelXID", "StateLabelYID", "AgeClass" });
            CreateIndex(store, "stsim_OutputStratumTransition", new[] { "ScenarioID", "Iteration", "Timestep", "StratumID", "SecondaryStratumID", "TertiaryStratumID", "TransitionGroupID", "AgeClass" });
            CreateIndex(store, "stsim_OutputStratumTransitionState", new[] { "ScenarioID", "Iteration", "Timestep", "StratumID", "SecondaryStratumID", "TertiaryStratumID", "TransitionTypeID", "StateClassID", "EndStateClassID" });
            CreateIndex(store, "stsim_OutputTransitionAttribute", new[] { "ScenarioID", "Iteration", "Timestep", "StratumID", "SecondaryStratumID", "TertiaryStratumID", "TransitionAttributeTypeID", "AgeClass" });

            if (store.TableExists("core_Chart"))
            {
                store.ExecuteNonQuery("UPDATE core_Chart SET Criteria = REPLACE(Criteria, 'STSim_', 'stsim_')");
            }

            if (store.TableExists("core_Map"))
            {
                store.ExecuteNonQuery("UPDATE core_Map SET Criteria = REPLACE(Criteria, 'STSim_', 'stsim_')");
            }
        }

        [UpdateAttribute(0.104, "The update removes AgeGroup.ID and adds support for distributions in various tables")]
        public static void Update_0_104(DataStore store)
        {
            if (store.TableExists("stsim_AgeGroup"))
            {
                //Note that AgeGroupID is AUTOINCREMENT because this table cannot be a validation source.

                store.ExecuteNonQuery("ALTER TABLE stsim_AgeGroup RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE stsim_AgeGroup(AgeGroupID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, MaximumAge INTEGER, Color TEXT)");
                store.ExecuteNonQuery("INSERT INTO stsim_AgeGroup(ProjectID, MaximumAge, Color) SELECT ProjectID, MaximumAge, Color FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("stsim_StateAttributeValue"))
            {
                store.ExecuteNonQuery("ALTER TABLE stsim_StateAttributeValue ADD COLUMN DistributionType INTEGER");
                store.ExecuteNonQuery("ALTER TABLE stsim_StateAttributeValue ADD COLUMN DistributionFrequencyID INTEGER");
                store.ExecuteNonQuery("ALTER TABLE stsim_StateAttributeValue ADD COLUMN DistributionSD DOUBLE");
                store.ExecuteNonQuery("ALTER TABLE stsim_StateAttributeValue ADD COLUMN DistributionMin DOUBLE");
                store.ExecuteNonQuery("ALTER TABLE stsim_StateAttributeValue ADD COLUMN DistributionMax DOUBLE");
            }

            if (store.TableExists("stsim_TransitionAttributeValue"))
            {
                store.ExecuteNonQuery("ALTER TABLE stsim_TransitionAttributeValue ADD COLUMN DistributionType INTEGER");
                store.ExecuteNonQuery("ALTER TABLE stsim_TransitionAttributeValue ADD COLUMN DistributionFrequencyID INTEGER");
                store.ExecuteNonQuery("ALTER TABLE stsim_TransitionAttributeValue ADD COLUMN DistributionSD DOUBLE");
                store.ExecuteNonQuery("ALTER TABLE stsim_TransitionAttributeValue ADD COLUMN DistributionMin DOUBLE");
                store.ExecuteNonQuery("ALTER TABLE stsim_TransitionAttributeValue ADD COLUMN DistributionMax DOUBLE");
            }
        }

        [UpdateAttribute(0.105, "This update will apply namespace prefixes to chart and map criteria")]
        public static void Update_0_105(DataStore store)
        {
            //Note: Criteria is all uppercase in v3 

            if (store.TableExists("core_Chart"))
            {
                store.ExecuteNonQuery("UPDATE core_Chart SET Criteria = REPLACE(Criteria, 'STATECLASSGROUP', 'STSIM_STATECLASSVARIABLEGROUP')");
                store.ExecuteNonQuery("UPDATE core_Chart SET Criteria = REPLACE(Criteria, 'TRANSITIONGROUP-DISAGG', 'STSIM_TRANSITIONVARIABLEGROUP-DISAGG')");
                store.ExecuteNonQuery("UPDATE core_Chart SET Criteria = REPLACE(Criteria, 'TRANSITIONGROUP-INCLUDE', 'STSIM_TRANSITIONVARIABLEGROUP-INCLUDE')");
                store.ExecuteNonQuery("UPDATE core_Chart SET Criteria = REPLACE(Criteria, 'STATEATTRIBUTEGROUP', 'STSIM_STATEATTRIBUTEVARIABLEGROUP')");
                store.ExecuteNonQuery("UPDATE core_Chart SET Criteria = REPLACE(Criteria, 'TRANSITIONATTRIBUTEGROUP', 'STSIM_TRANSITIONATTRIBUTEVARIABLEGROUP')");
            }

            RenameChartVariable(store, "STSimStateClassNormalVariable", "stsim_StateClassNormalVariable");
            RenameChartVariable(store, "STSimStateClassProportionVariable", "stsim_StateClassProportionVariable");
            RenameChartVariable(store, "STSimTransitionNormalVariable", "stsim_TransitionNormalVariable");
            RenameChartVariable(store, "STSimTransitionProportionVariable", "stsim_TransitionProportionVariable");
            RenameChartVariable(store, "attrnormal", "stsim_AttrNormal");
            RenameChartVariable(store, "attrdensity", "stsim_AttrDensity");

            RenameMapVariable(store, "str", "stsim_str");
            RenameMapVariable(store, "secstr", "stsim_secstr");
            RenameMapVariable(store, "terstr", "stsim_terstr");
            RenameMapVariable(store, "sc", "stsim_sc");
            RenameMapVariable(store, "tg", "stsim_tg");
            RenameMapVariable(store, "age", "stsim_age");
            RenameMapVariable(store, "tst", "stsim_tst");
            RenameMapVariable(store, "sa", "stsim_sa");
            RenameMapVariable(store, "ta", "stsim_ta");
            RenameMapVariable(store, "tge", "stsim_tge");
            RenameMapVariable(store, "tgap", "stsim_tgap");
        }

        [UpdateAttribute(0.106, "This update adds StateClassID and Neighborhood fields to the TransitionAdjacencySetting table")]
        public static void Update_0_106(DataStore store)
        {
            if (store.TableExists("stsim_TransitionAdjacencySetting"))
            {
                store.ExecuteNonQuery("ALTER TABLE stsim_TransitionAdjacencySetting RENAME TO TEMP_TABLE");

                store.ExecuteNonQuery(@"CREATE TABLE stsim_TransitionAdjacencySetting(
                    TransitionAdjacencySettingID INTEGER PRIMARY KEY AUTOINCREMENT,
                    ScenarioID                   INTEGER,
                    TransitionGroupID            INTEGER,
                    StateClassID                 INTEGER,
                    StateAttributeTypeID         INTEGER,
                    NeighborhoodRadius           DOUBLE,
                    UpdateFrequency              INTEGER
                )");

                store.ExecuteNonQuery(@"INSERT INTO 
                    stsim_TransitionAdjacencySetting(ScenarioID, TransitionGroupID, StateAttributeTypeID, NeighborhoodRadius, UpdateFrequency) 
                    SELECT ScenarioID, TransitionGroupID, StateAttributeTypeID, NeighborhoodRadius, UpdateFrequency FROM TEMP_TABLE");

                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }
        }

        [UpdateAttribute(0.107, "This update will separate the tabular, spatial, and spatial averaging options into their own tables")]
        public static void Update_0_107(DataStore store)
        {
            if (store.TableExists("stsim_OutputOptions"))
            {
                //Keep a copy of the original
                store.ExecuteNonQuery("ALTER TABLE stsim_OutputOptions RENAME TO TEMP_TABLE");

                //Migrate data
                MigrateTabularOutputOptions(store);
                MigrateSpatialOutputOptions(store);
                MigrateSpatialAveragingOutputOptions(store);

                //Drop old table
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }
        }

        [UpdateAttribute(0.108, "This update adds AgeMin, AgeMax, TSTGroup, TSTMin, and TSTMax columns to the TransitionMultiplierValue table")]
        public static void Update_0_108(DataStore store)
        {
            if (store.TableExists("stsim_TransitionMultiplierValue"))
            {
                store.ExecuteNonQuery("ALTER TABLE stsim_TransitionMultiplierValue RENAME TO TEMP_TABLE");

                store.ExecuteNonQuery(@"CREATE TABLE stsim_TransitionMultiplierValue(
                    TransitionMultiplierValueID INTEGER PRIMARY KEY AUTOINCREMENT,
                    ScenarioID                  INTEGER,
                    Iteration                   INTEGER,
                    Timestep                    INTEGER,
                    StratumID                   INTEGER,
                    SecondaryStratumID          INTEGER,
                    TertiaryStratumID           INTEGER,
                    StateClassID                INTEGER,
                    AgeMin                      INTEGER,
                    AgeMax                      INTEGER,
                    TSTGroupID                  INTEGER,
                    TSTMin                      INTEGER,
                    TSTMax                      INTEGER,
                    TransitionGroupID           INTEGER,
                    TransitionMultiplierTypeID  INTEGER,
                    Amount                      DOUBLE,
                    DistributionType            INTEGER,
                    DistributionFrequencyID     INTEGER,
                    DistributionSD              DOUBLE,
                    DistributionMin             DOUBLE,
                    DistributionMax             DOUBLE
                )");

                store.ExecuteNonQuery(@"INSERT INTO stsim_TransitionMultiplierValue(
                    ScenarioID                  ,
                    Iteration                   ,
                    Timestep                    ,
                    StratumID                   ,
                    SecondaryStratumID          ,
                    TertiaryStratumID           ,
                    StateClassID                ,
                    TransitionGroupID           ,
                    TransitionMultiplierTypeID  ,
                    Amount                      ,
                    DistributionType            ,
                    DistributionFrequencyID     ,
                    DistributionSD              ,
                    DistributionMin             ,
                    DistributionMax             )
                    SELECT
                    ScenarioID                  ,
                    Iteration                   ,
                    Timestep                    ,
                    StratumID                   ,
                    SecondaryStratumID          ,
                    TertiaryStratumID           ,
                    StateClassID                ,
                    TransitionGroupID           ,
                    TransitionMultiplierTypeID  ,
                    Amount                      ,
                    DistributionType            ,
                    DistributionFrequencyID     ,
                    DistributionSD              ,
                    DistributionMin             ,
                    DistributionMax             
                    FROM TEMP_TABLE");

                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

                store.ExecuteNonQuery("DROP INDEX IF EXISTS STSim_TransitionMultiplierValue_Index");
                CreateIndex(store, "stsim_TransitionMultiplierValue", new[] { "ScenarioID" });
            }
        }

        [UpdateAttribute(0.109, "This update adds External Variable columns to the OutputOptions table")]
        public static void Update_0_109(DataStore store)
        {
            if (store.TableExists("stsim_OutputOptions"))
            {
                store.ExecuteNonQuery("ALTER TABLE stsim_OutputOptions ADD COLUMN SummaryOutputEV INTEGER");
                store.ExecuteNonQuery("ALTER TABLE stsim_OutputOptions ADD COLUMN SummaryOutputEVTimesteps INTEGER");
            }
        }

        [UpdateAttribute(0.110, "This update adds TST support to the StateAttributeValue table")]
        public static void Update_0_110(DataStore store)
        {
            if (store.TableExists("stsim_StateAttributeValue"))
            {
                store.ExecuteNonQuery("ALTER TABLE stsim_StateAttributeValue RENAME TO TEMP_TABLE");

                store.ExecuteNonQuery(@"CREATE TABLE stsim_StateAttributeValue ( 
                    StateAttributeValueID   INTEGER PRIMARY KEY AUTOINCREMENT,
                    ScenarioID              INTEGER,
                    Iteration               INTEGER,
                    Timestep                INTEGER,
                    StratumID               INTEGER,
                    SecondaryStratumID      INTEGER,
                    TertiaryStratumID       INTEGER,
                    StateClassID            INTEGER,
                    StateAttributeTypeID    INTEGER,
                    AgeMin                  INTEGER,
                    AgeMax                  INTEGER,
                    TSTGroupID              INTEGER,
                    TSTMin                  INTEGER,
                    TSTMax                  INTEGER,
                    Value                   DOUBLE,
                    DistributionType        INTEGER,
                    DistributionFrequencyID INTEGER,
                    DistributionSD          DOUBLE,
                    DistributionMin         DOUBLE,
                    DistributionMax         DOUBLE 
                )");

                store.ExecuteNonQuery(@"INSERT INTO stsim_StateAttributeValue(
                        ScenarioID,
                        Iteration,
                        Timestep,
                        StratumID,
                        SecondaryStratumID,
                        TertiaryStratumID,
                        StateClassID,
                        StateAttributeTypeID,
                        AgeMin,
                        AgeMax,
                        Value,
                        DistributionType,
                        DistributionFrequencyID,
                        DistributionSD,
                        DistributionMin,
                        DistributionMax)  
                    SELECT  
                        ScenarioID,
                        Iteration,
                        Timestep,
                        StratumID,
                        SecondaryStratumID,
                        TertiaryStratumID,
                        StateClassID,
                        StateAttributeTypeID,
                        AgeMin,
                        AgeMax,
                        Value,
                        DistributionType,
                        DistributionFrequencyID,
                        DistributionSD,
                        DistributionMin,
                        DistributionMax         
                    FROM TEMP_TABLE");

                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
                CreateIndex(store, "stsim_StateAttributeValue", new[] { "ScenarioID" });
            }
        }

        [UpdateAttribute(0.111, "This update adds TST support to the TransitionAttributeValue table")]
        public static void Update_0_111(DataStore store)
        {
            if (store.TableExists("stsim_TransitionAttributeValue"))
            {
                store.ExecuteNonQuery("ALTER TABLE stsim_TransitionAttributeValue RENAME TO TEMP_TABLE");

                store.ExecuteNonQuery(@"CREATE TABLE stsim_TransitionAttributeValue ( 
                    TransitionAttributeValueID INTEGER PRIMARY KEY AUTOINCREMENT,
                    ScenarioID                 INTEGER,
                    Iteration                  INTEGER,
                    Timestep                   INTEGER,
                    StratumID                  INTEGER,
                    SecondaryStratumID         INTEGER,
                    TertiaryStratumID          INTEGER,
                    TransitionGroupID          INTEGER,
                    StateClassID               INTEGER,
                    TransitionAttributeTypeID  INTEGER,
                    AgeMin                     INTEGER,
                    AgeMax                     INTEGER,
                    TSTGroupID                 INTEGER,
                    TSTMin                     INTEGER,
                    TSTMax                     INTEGER,
                    Value                      DOUBLE,
                    DistributionType           INTEGER,
                    DistributionFrequencyID    INTEGER,
                    DistributionSD             DOUBLE,
                    DistributionMin            DOUBLE,
                    DistributionMax            DOUBLE 
                )");

                store.ExecuteNonQuery(@"INSERT INTO stsim_TransitionAttributeValue(
                        ScenarioID,                 
                        Iteration,                  
                        Timestep,                   
                        StratumID,                  
                        SecondaryStratumID,         
                        TertiaryStratumID,          
                        TransitionGroupID,          
                        StateClassID,               
                        TransitionAttributeTypeID,  
                        AgeMin,                     
                        AgeMax,                     
                        Value,                      
                        DistributionType,           
                        DistributionFrequencyID,    
                        DistributionSD,             
                        DistributionMin,            
                        DistributionMax)  
                    SELECT  
                        ScenarioID,                 
                        Iteration,                  
                        Timestep,                   
                        StratumID,                  
                        SecondaryStratumID,         
                        TertiaryStratumID,          
                        TransitionGroupID,          
                        StateClassID,               
                        TransitionAttributeTypeID,  
                        AgeMin,                     
                        AgeMax,                     
                        Value,                      
                        DistributionType,           
                        DistributionFrequencyID,    
                        DistributionSD,             
                        DistributionMin,            
                        DistributionMax 
                    FROM TEMP_TABLE");

                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
                CreateIndex(store, "stsim_TransitionAttributeValue", new[] { "ScenarioID" });
            }
        }

        [UpdateAttribute(0.112, "This update adds TST support to the InitialConditionsNonSpatialDistribution table")]
        public static void Update_0_112(DataStore store)
        {
            if (store.TableExists("stsim_InitialConditionsNonSpatialDistribution"))
            {
                store.ExecuteNonQuery("ALTER TABLE stsim_InitialConditionsNonSpatialDistribution RENAME TO TEMP_TABLE");

                store.ExecuteNonQuery(@"CREATE TABLE stsim_InitialConditionsNonSpatialDistribution ( 
                    InitialConditionsNonSpatialDistributionID INTEGER PRIMARY KEY AUTOINCREMENT,
                    ScenarioID                                INTEGER,
                    Iteration                                 INTEGER,
                    StratumID                                 INTEGER,
                    SecondaryStratumID                        INTEGER,
                    TertiaryStratumID                         INTEGER,
                    StateClassID                              INTEGER,
                    AgeMin                                    INTEGER,
                    AgeMax                                    INTEGER,
                    TSTGroupID                                INTEGER,
                    TSTMin                                    INTEGER,
                    TSTMax                                    INTEGER,
                    RelativeAmount                            DOUBLE 
                )");

                store.ExecuteNonQuery(@"INSERT INTO stsim_InitialConditionsNonSpatialDistribution(
                        ScenarioID                                ,
                        Iteration                                 ,
                        StratumID                                 ,
                        SecondaryStratumID                        ,
                        TertiaryStratumID                         ,
                        StateClassID                              ,
                        AgeMin                                    ,
                        AgeMax                                    ,
                        RelativeAmount)  
                    SELECT  
                        ScenarioID                                ,
                        Iteration                                 ,
                        StratumID                                 ,
                        SecondaryStratumID                        ,
                        TertiaryStratumID                         ,
                        StateClassID                              ,
                        AgeMin                                    ,
                        AgeMax                                    ,
                        RelativeAmount                            
                    FROM TEMP_TABLE");

                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
                CreateIndex(store, "stsim_InitialConditionsNonSpatialDistribution", new[] { "ScenarioID" });
            }
        }

        [UpdateAttribute(0.113, "This update adds TST columns to the stsim_OutputOptions table")]
        public static void Update_0_113(DataStore store)
        {
            if (store.TableExists("stsim_OutputOptions"))
            {
                store.ExecuteNonQuery("ALTER TABLE stsim_OutputOptions ADD COLUMN SummaryOutputTST INTEGER");
                store.ExecuteNonQuery("ALTER TABLE stsim_OutputOptions ADD COLUMN SummaryOutputTSTTimesteps INTEGER");
            }
        }

        [UpdateAttribute(0.114, "This update changes the transformer table for new transformer names")]
        public static void Update_0_114(DataStore _)
        {
            //This update is obsolete

            //UpdateTransformerTable(store, 
            //    "stsim_Primary", "ST-Sim", "stsim", "The ST-Sim state-and-transition simulation model");
        }

        [UpdateAttribute(0.115, "This update changes the state and transition attribute chart keys to be unique and more descriptive")]
        public static void Update_0_115(DataStore store)
        {
            //The state and transition variables currently use the same prefix:
            //
            //stsim_AttrNormal-N
            //stsim_AttrDensity-N
            //
            //This works because their primary keys are unique and they target different tables.
            //However, for clarity and to make it easier to remove group-based selections in the
            //criteria (see Update_0_116) we want to name the variables are follows:
            //
            //stsim_StateAttributeNormalVariable
            //stsim_StateAttributeDensityVariable
            //stsim_TransitionAttributeNormalVariable
            //stsim_TransitionAttributeDensityVariable

            //First, rename the state attribute variables

            if (!store.TableExists("stsim_StateAttributeType") || !store.TableExists("stsim_TransitionAttributeType"))
            {
                Debug.Assert(false);
                return;
            }

            DataTable dt = store.CreateDataTable("stsim_StateAttributeType");

            foreach (DataRow dr in dt.Rows)
            {
                int Id = Convert.ToInt32(dr["StateAttributeTypeID"]);
                string OldNormal = string.Format("stsim_AttrNormal-{0}", Id);
                string OldDensity = string.Format("stsim_AttrDensity-{0}", Id);
                string NewNormal = string.Format("stsim_StateAttributeNormalVariable-{0}", Id);
                string NewDensity = string.Format("stsim_StateAttributeDensityVariable-{0}", Id);

                RenameChartVariable(store, OldNormal, NewNormal);
                RenameChartVariable(store, OldDensity, NewDensity);
            }

            //Now rename the remaining stsim_AttrNormal and stsim_AttrDensity
            //variables to their new unique, descriptive names.

            RenameChartVariable(store, "stsim_AttrNormal", "stsim_TransitionAttributeNormalVariable");
            RenameChartVariable(store, "stsim_AttrDensity", "stsim_TransitionAttributeDensityVariable");
        }

        [UpdateAttribute(0.116, "This update changes chart keys to use the variable name instead of the group name")]
        public static void Update_0_116(DataStore store)
        {
            RemoveChartGroupCriteria(store, new[]
            {
                "stsim_StateClassVariableGroup|stsim_StateClassNormalVariable",
                "stsim_StateClassVariableGroup|stsim_StateClassProportionVariable",
                "stsim_TransitionVariableGroup|stsim_TransitionNormalVariable",
                "stsim_TransitionVariableGroup|stsim_TransitionProportionVariable",
                "stsim_TSTGroup|stsim_TSTVariable",
                "stsim_StateAttributeVariableGroup|stsim_StateAttributeNormalVariable",
                "stsim_StateAttributeVariableGroup|stsim_StateAttributeDensityVariable",
                "stsim_TransitionAttributeVariableGroup|stsim_TransitionAttributeNormalVariable",
                "stsim_TransitionAttributeVariableGroup|stsim_TransitionAttributeDensityVariable"
            });
        }

        [UpdateAttribute(0.117, "This update changes the chart variable names to be more concise")]
        public static void Update_0_117(DataStore store)
        {
            RenameChartVariable(store, "stsim_StateClassNormalVariable", "stsim_StateClass");
            RenameChartVariable(store, "stsim_StateClassProportionVariable", "stsim_StateClassProportion");
            RenameChartVariable(store, "stsim_TransitionNormalVariable", "stsim_Transition");
            RenameChartVariable(store, "stsim_TransitionProportionVariable", "stsim_TransitionProportion");
            RenameChartVariable(store, "stsim_TSTVariable", "stsim_TST");
            RenameChartVariable(store, "stsim_StateAttributeNormalVariable", "stsim_StateAttribute");
            RenameChartVariable(store, "stsim_StateAttributeDensityVariable", "stsim_StateAttributeDensity");
            RenameChartVariable(store, "stsim_TransitionAttributeNormalVariable", "stsim_TransitionAttribute");
            RenameChartVariable(store, "stsim_TransitionAttributeDensityVariable", "stsim_TransitionAttributeDensity");
        }

        [UpdateAttribute(0.118, "This update changes the map variable names to be more concise")]
        public static void Update_0_118(DataStore store)
        {
            RenameMapVariable(store, "stsim_sc", "stsim_StateClass");
            RenameMapVariable(store, "stsim_str", "stsim_Stratum");
            RenameMapVariable(store, "stsim_age", "stsim_Age");
            RenameMapVariable(store, "stsim_tge", "stsim_TransitionEvent");
            RenameMapVariable(store, "stsim_tg", "stsim_TransitionGroup");
            RenameMapVariable(store, "stsim_tst", "stsim_TST");
            RenameMapVariable(store, "stsim_sa", "stsim_StateAttribute");
            RenameMapVariable(store, "stsim_ta", "stsim_TransitionAttribute");
            RenameMapVariable(store, "avgsc", "stsim_StateClassProb");
            RenameMapVariable(store, "avgstr", "stsim_StratumProb");
            RenameMapVariable(store, "stsim_AgesAvgGroup", "stsim_AgeProb");
            RenameMapVariable(store, "stsim_avgtp", "stsim_TransitionProb");
            RenameMapVariable(store, "stsim_avgtst", "stsim_TSTProb");
            RenameMapVariable(store, "stsim_avgsa", "stsim_StateAttributeProb");
            RenameMapVariable(store, "stsim_avgta", "stsim_TransitionAttributeProb");

            RenameProjectFilesContainingVariableName(store, "stsim_sc", "stsim_StateClass");
            RenameProjectFilesContainingVariableName(store, "stsim_str", "stsim_Stratum");
            RenameProjectFilesContainingVariableName(store, "stsim_age", "stsim_Age");
            RenameProjectFilesContainingVariableName(store, "stsim_tge", "stsim_TransitionEvent");
            RenameProjectFilesContainingVariableName(store, "stsim_tg", "stsim_TransitionGroup");
            RenameProjectFilesContainingVariableName(store, "stsim_tst", "stsim_TST");
            RenameProjectFilesContainingVariableName(store, "stsim_sa", "stsim_StateAttribute");
            RenameProjectFilesContainingVariableName(store, "stsim_ta", "stsim_TransitionAttribute");
            RenameProjectFilesContainingVariableName(store, "avgsc", "stsim_StateClassProb");
            RenameProjectFilesContainingVariableName(store, "avgstr", "stsim_StratumProb");
            RenameProjectFilesContainingVariableName(store, "stsim_AgesAvgGroup", "stsim_AgeProb");
            RenameProjectFilesContainingVariableName(store, "stsim_avgtp", "stsim_TransitionProb");
            RenameProjectFilesContainingVariableName(store, "stsim_avgtst", "stsim_TSTProb");
            RenameProjectFilesContainingVariableName(store, "stsim_avgsa", "stsim_StateAttributeProb");
            RenameProjectFilesContainingVariableName(store, "stsim_avgta", "stsim_TransitionAttributeProb");
        }

        [UpdateAttribute(4.0, "This update adjusts the system tables for v2 to v3")]
        public static void Update_4_000(DataStore store)
        {
            store.ExecuteNonQuery("ALTER TABLE stsim_Terminology ADD COLUMN StockUnits TEXT");
            store.ExecuteNonQuery("UPDATE stsim_Terminology SET StockUnits='Tons'");

            RenameColumn(store, "stsim_OutputFilterStateAttributes", "OutputFilterStateAttributeID", "OutputFilterStateAttributesID");
            RenameColumn(store, "stsim_OutputFilterTransitionAttributes", "OutputFilterTransitionAttributeID", "OutputFilterTransitionAttributesID");
            RenameColumn(store, "stsim_Multiprocessing", "ProcessingID", "MultiprocessingID");

            store.ExecuteNonQuery("UPDATE core_Transformer SET Name='stsim_Main' WHERE Name='stsim_Primary'");
        }

        [UpdateAttribute(4.1, "This update adds the new ResolutionId column to all spatial output datasheets, and sets its value to 0 for all rows.")]
        public static void Update_4_100(DataStore store)
        {
            if (!store.TableExists("stsim_Resolution"))
            {
                store.ExecuteNonQuery("CREATE TABLE stsim_Resolution(ResolutionId INTEGER PRIMARY KEY, ProjectId INTEGER, Id INTEGER, Name TEXT)");

                DataTable dt = store.CreateDataTable("core_Project");

                foreach (DataRow dr in dt.Rows)
                {
                    int pid = Convert.ToInt32(dr["ProjectID"], CultureInfo.InvariantCulture);

                    store.ExecuteNonQuery(string.Format(CultureInfo.InvariantCulture,
                        "INSERT INTO stsim_Resolution(ResolutionId, ProjectId, Id, Name) VALUES({0}, {1}, {2}, 'Base')",
                        Library.GetNextSequenceId(store), pid, 0));
                    store.ExecuteNonQuery(string.Format(CultureInfo.InvariantCulture,
                        "INSERT INTO stsim_Resolution(ResolutionId, ProjectId, Id, Name) VALUES({0}, {1}, {2}, 'Fine')",
                        Library.GetNextSequenceId(store), pid, 1));
                }
            }

            store.ExecuteNonQuery("ALTER TABLE stsim_OutputSpatialState ADD COLUMN ResolutionId INTEGER");
            store.ExecuteNonQuery("UPDATE stsim_OutputSpatialState SET ResolutionId=0");

            store.ExecuteNonQuery("ALTER TABLE stsim_OutputSpatialAge ADD COLUMN ResolutionId INTEGER");
            store.ExecuteNonQuery("UPDATE stsim_OutputSpatialAge SET ResolutionId=0");

            store.ExecuteNonQuery("ALTER TABLE stsim_OutputSpatialStratum ADD COLUMN ResolutionId INTEGER");
            store.ExecuteNonQuery("UPDATE stsim_OutputSpatialStratum SET ResolutionId=0");

            store.ExecuteNonQuery("ALTER TABLE stsim_OutputSpatialTransition ADD COLUMN ResolutionId INTEGER");
            store.ExecuteNonQuery("UPDATE stsim_OutputSpatialTransition SET ResolutionId=0");

            store.ExecuteNonQuery("ALTER TABLE stsim_OutputSpatialTransitionEvent ADD COLUMN ResolutionId INTEGER");
            store.ExecuteNonQuery("UPDATE stsim_OutputSpatialTransitionEvent SET ResolutionId=0");

            store.ExecuteNonQuery("ALTER TABLE stsim_OutputSpatialTST ADD COLUMN ResolutionId INTEGER");
            store.ExecuteNonQuery("UPDATE stsim_OutputSpatialTST SET ResolutionId=0");

            store.ExecuteNonQuery("ALTER TABLE stsim_OutputSpatialStateAttribute ADD COLUMN ResolutionId INTEGER");
            store.ExecuteNonQuery("UPDATE stsim_OutputSpatialStateAttribute SET ResolutionId=0");

            store.ExecuteNonQuery("ALTER TABLE stsim_OutputSpatialTransitionAttribute ADD COLUMN ResolutionId INTEGER");
            store.ExecuteNonQuery("UPDATE stsim_OutputSpatialTransitionAttribute SET ResolutionId=0");

            store.ExecuteNonQuery("ALTER TABLE stsim_OutputSpatialAverageStateClass ADD COLUMN ResolutionId INTEGER");
            store.ExecuteNonQuery("UPDATE stsim_OutputSpatialAverageStateClass SET ResolutionId=0");

            store.ExecuteNonQuery("ALTER TABLE stsim_OutputSpatialAverageAge ADD COLUMN ResolutionId INTEGER");
            store.ExecuteNonQuery("UPDATE stsim_OutputSpatialAverageAge SET ResolutionId=0");

            store.ExecuteNonQuery("ALTER TABLE stsim_OutputSpatialAverageStratum ADD COLUMN ResolutionId INTEGER");
            store.ExecuteNonQuery("UPDATE stsim_OutputSpatialAverageStratum SET ResolutionId=0");

            store.ExecuteNonQuery("ALTER TABLE stsim_OutputSpatialAverageTransitionProbability ADD COLUMN ResolutionId INTEGER");
            store.ExecuteNonQuery("UPDATE stsim_OutputSpatialAverageTransitionProbability SET ResolutionId=0");

            store.ExecuteNonQuery("ALTER TABLE stsim_OutputSpatialAverageTST ADD COLUMN ResolutionId INTEGER");
            store.ExecuteNonQuery("UPDATE stsim_OutputSpatialAverageTST SET ResolutionId=0");

            store.ExecuteNonQuery("ALTER TABLE stsim_OutputSpatialAverageStateAttribute ADD COLUMN ResolutionId INTEGER");
            store.ExecuteNonQuery("UPDATE stsim_OutputSpatialAverageStateAttribute SET ResolutionId=0");

            store.ExecuteNonQuery("ALTER TABLE stsim_OutputSpatialAverageTransitionAttribute ADD COLUMN ResolutionId INTEGER");
            store.ExecuteNonQuery("UPDATE stsim_OutputSpatialAverageTransitionAttribute SET ResolutionId=0");

            if (store.TableExists("stsim_OutputSpatialStockGroup"))
            {
                store.ExecuteNonQuery("ALTER TABLE stsim_OutputSpatialStockGroup ADD COLUMN ResolutionId INTEGER");
                store.ExecuteNonQuery("UPDATE stsim_OutputSpatialStockGroup SET ResolutionId=0");
            }

            if (store.TableExists("stsim_OutputSpatialFlowGroup"))
            {
                store.ExecuteNonQuery("ALTER TABLE stsim_OutputSpatialFlowGroup ADD COLUMN ResolutionId INTEGER");
                store.ExecuteNonQuery("UPDATE stsim_OutputSpatialFlowGroup SET ResolutionId=0");
            }

            if (store.TableExists("stsim_OutputLateralFlowGroup"))
            {
                store.ExecuteNonQuery("ALTER TABLE stsim_OutputLateralFlowGroup ADD COLUMN ResolutionId INTEGER");
                store.ExecuteNonQuery("UPDATE stsim_OutputLateralFlowGroup SET ResolutionId=0");
            }

            if (store.TableExists("stsim_OutputAverageSpatialStockGroup"))
            {
                store.ExecuteNonQuery("ALTER TABLE stsim_OutputAverageSpatialStockGroup ADD COLUMN ResolutionId INTEGER");
                store.ExecuteNonQuery("UPDATE stsim_OutputAverageSpatialStockGroup SET ResolutionId=0");
            }

            if (store.TableExists("stsim_OutputAverageSpatialFlowGroup"))
            {
                store.ExecuteNonQuery("ALTER TABLE stsim_OutputAverageSpatialFlowGroup ADD COLUMN ResolutionId INTEGER");
                store.ExecuteNonQuery("UPDATE stsim_OutputAverageSpatialFlowGroup SET ResolutionId=0");
            }

            if (store.TableExists("stsim_OutputAverageLateralFlowGroup"))
            {
                store.ExecuteNonQuery("ALTER TABLE stsim_OutputAverageLateralFlowGroup ADD COLUMN ResolutionId INTEGER");
                store.ExecuteNonQuery("UPDATE stsim_OutputAverageLateralFlowGroup SET ResolutionId=0");
            }
        }

        [UpdateAttribute(4.2, "Placeholder for external program feature.")]
        public static void Update_4_200(DataStore store)
        {
        }

        [UpdateAttribute(4.3, "This update adds the new ResolutionId column to all tabular output datasheets, and sets its value to 0 for all rows.")]
        public static void Update_4_300(DataStore store)
        {
            store.ExecuteNonQuery("ALTER TABLE stsim_OutputStratum ADD COLUMN ResolutionId INTEGER");
            store.ExecuteNonQuery("UPDATE stsim_OutputStratum SET ResolutionId=0");

            store.ExecuteNonQuery("ALTER TABLE stsim_OutputStratumState ADD COLUMN ResolutionId INTEGER");
            store.ExecuteNonQuery("UPDATE stsim_OutputStratumState SET ResolutionId=0");

            store.ExecuteNonQuery("ALTER TABLE stsim_OutputStratumTransition ADD COLUMN ResolutionId INTEGER");
            store.ExecuteNonQuery("UPDATE stsim_OutputStratumTransition SET ResolutionId=0");

            store.ExecuteNonQuery("ALTER TABLE stsim_OutputStratumTransitionState ADD COLUMN ResolutionId INTEGER");
            store.ExecuteNonQuery("UPDATE stsim_OutputStratumTransitionState SET ResolutionId=0");

            store.ExecuteNonQuery("ALTER TABLE stsim_OutputTST ADD COLUMN ResolutionId INTEGER");
            store.ExecuteNonQuery("UPDATE stsim_OutputTST SET ResolutionId=0");

            store.ExecuteNonQuery("ALTER TABLE stsim_OutputStateAttribute ADD COLUMN ResolutionId INTEGER");
            store.ExecuteNonQuery("UPDATE stsim_OutputStateAttribute SET ResolutionId=0");

            store.ExecuteNonQuery("ALTER TABLE stsim_OutputTransitionAttribute ADD COLUMN ResolutionId INTEGER");
            store.ExecuteNonQuery("UPDATE stsim_OutputTransitionAttribute SET ResolutionId=0");

            DataTable dt = store.CreateDataTable("stsim_OutputExternalVariableValue");

            if (!dt.Columns.Contains("ExternalVariableTypeId"))
            {
                store.ExecuteNonQuery("ALTER TABLE stsim_OutputExternalVariableValue ADD COLUMN ExternalVariableTypeId INTEGER");
            }

            store.ExecuteNonQuery("ALTER TABLE stsim_OutputExternalVariableValue ADD COLUMN ResolutionId INTEGER");
            store.ExecuteNonQuery("UPDATE stsim_OutputExternalVariableValue SET ResolutionId=0");

            // Not sure if these conditionals are needed, but they were used in Update_4_100 for stocks and flows, so assuming they are also needed here
            if (store.TableExists("stsim_OutputStock"))
            {
                store.ExecuteNonQuery("ALTER TABLE stsim_OutputStock ADD COLUMN ResolutionId INTEGER");
                store.ExecuteNonQuery("UPDATE stsim_OutputStock SET ResolutionId=0");
            }

            if (store.TableExists("stsim_OutputFlow"))
            {
                store.ExecuteNonQuery("ALTER TABLE stsim_OutputFlow ADD COLUMN ResolutionId INTEGER");
                store.ExecuteNonQuery("UPDATE stsim_OutputFlow SET ResolutionId=0");
            }
        }

        [UpdateAttribute(4.4, "This update adds OMIT fields to the stock flow output options.")]
        public static void Update_4_400(DataStore store)
        {
            if (store.TableExists("stsim_OutputOptionsStockFlow"))
            {
                store.ExecuteNonQuery("ALTER TABLE stsim_OutputOptionsStockFlow ADD COLUMN SummaryOutputSTOmitSS INTEGER");
                store.ExecuteNonQuery("ALTER TABLE stsim_OutputOptionsStockFlow ADD COLUMN SummaryOutputSTOmitTS INTEGER");
                store.ExecuteNonQuery("ALTER TABLE stsim_OutputOptionsStockFlow ADD COLUMN SummaryOutputSTOmitSC INTEGER");
            }
        }
    }
}
