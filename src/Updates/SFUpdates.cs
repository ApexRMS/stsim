// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Reflection;
using System.Diagnostics;
using System.Globalization;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    [ObfuscationAttribute(Exclude = true, ApplyToMembers = false)]
    internal class SFUpdates : DotNetUpdateProvider
    {
        public SFUpdates()
        {
            this.LegacyPackageName = "stsimsf";
        }

        protected override void OnAfterUpdate(DataStore store)
        {
            base.OnAfterUpdate(store);

#if DEBUG
            //Verify that all expected indexes exist after the update because it is easy to forget to recreate them after 
            //adding a column to an existing table (which requires the table to be recreated if you want to preserve column order.)

            ASSERT_INDEX_EXISTS(store, "stsimsf_FlowPathway");
            ASSERT_INDEX_EXISTS(store, "stsimsf_OutputFlow");
            ASSERT_INDEX_EXISTS(store, "stsimsf_OutputStock");
            ASSERT_INDEX_EXISTS(store, "stsimsf_StockTypeGroupMembership");
            ASSERT_INDEX_EXISTS(store, "stsimsf_FlowTypeGroupMembership");
            ASSERT_INDEX_EXISTS(store, "stsimsf_StockTransitionMultiplier");
            ASSERT_INDEX_EXISTS(store, "stsimsf_FlowMultiplier");
#endif
        }

        [UpdateAttribute(0.101, "This update adds support lateral flows")]
        public static void Update_0_101(DataStore store)
        {
            if (store.TableExists("SF_FlowPathway"))
            {
                store.ExecuteNonQuery("ALTER TABLE SF_FlowPathway RENAME TO TEMP_TABLE");

                store.ExecuteNonQuery("CREATE TABLE SF_FlowPathway( " +
                    "FlowPathwayID                INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "ScenarioID                   INTEGER, " +
                    "Iteration                    INTEGER, " +
                    "Timestep                     INTEGER, " +
                    "FromStratumID                INTEGER, " +
                    "FromSecondaryStratumID       INTEGER, " +
                    "FromTertiaryStratumID        INTEGER, " +
                    "FromStateClassID             INTEGER, " +
                    "FromAgeMin                   INTEGER, " +
                    "FromStockTypeID              INTEGER, " +
                    "ToStratumID                  INTEGER, " +
                    "ToStateClassID               INTEGER, " +
                    "ToAgeMin                     INTEGER, " +
                    "ToStockTypeID                INTEGER, " +
                    "TransitionGroupID            INTEGER, " +
                    "StateAttributeTypeID         INTEGER, " +
                    "FlowTypeID                   INTEGER, " +
                    "Multiplier                   DOUBLE, " +
                    "TransferToStratumID          INTEGER, " +
                    "TransferToSecondaryStratumID INTEGER, " +
                    "TransferToTertiaryStratumID  INTEGER, " +
                    "TransferToStateClassID       INTEGER, " +
                    "TransferToAgeMin             INTEGER)"
                );

                store.ExecuteNonQuery("INSERT INTO SF_FlowPathway( " +
                    "ScenarioID, " +
                    "Iteration, " +
                    "Timestep,  " +
                    "FromStratumID, " +
                    "FromStateClassID,  " +
                    "FromAgeMin, " +
                    "FromStockTypeID,  " +
                    "ToStratumID, " +
                    "ToStateClassID,  " +
                    "ToAgeMin, " +
                    "ToStockTypeID,  " +
                    "TransitionGroupID,  " +
                    "StateAttributeTypeID,  " +
                    "FlowTypeID,  " +
                    "Multiplier) " +
                    "SELECT " +
                    "ScenarioID,  " +
                    "Iteration, " +
                    "Timestep,  " +
                    "FromStratumID, " +
                    "FromStateClassID,  " +
                    "FromAgeMin, " +
                    "FromStockTypeID,  " +
                    "ToStratumID, " +
                    "ToStateClassID,  " +
                    "ToAgeMin, " +
                    "ToStockTypeID,  " +
                    "TransitionGroupID,  " +
                    "StateAttributeTypeID,  " +
                    "FlowTypeID, " +
                    "Multiplier " +
                    "FROM TEMP_TABLE");

                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

                store.ExecuteNonQuery("DROP INDEX IF EXISTS SF_FlowPathway_Index");
                store.ExecuteNonQuery("CREATE INDEX SF_FlowPathway_Index ON SF_FlowPathway(ScenarioID)");
            }

            if (store.TableExists("SF_OutputFlow"))
            {
                store.ExecuteNonQuery("ALTER TABLE SF_OutputFlow ADD COLUMN EndStratumID INTEGER");
                store.ExecuteNonQuery("ALTER TABLE SF_OutputFlow ADD COLUMN EndSecondaryStratumID INTEGER");
                store.ExecuteNonQuery("ALTER TABLE SF_OutputFlow ADD COLUMN EndTertiaryStratumID INTEGER");
                store.ExecuteNonQuery("ALTER TABLE SF_OutputFlow ADD COLUMN EndStateClassID INTEGER");
                store.ExecuteNonQuery("ALTER TABLE SF_OutputFlow ADD COLUMN EndMinAge INTEGER");

                string[] cols = new string[] {
                    "ScenarioID",
                    "Iteration",
                    "Timestep",
                    "FromStratumID",
                    "FromSecondaryStratumID",
                    "FromTertiaryStratumID",
                    "FromStateClassID",
                    "FromStockTypeID",
                    "TransitionTypeID",
                    "ToStratumID",
                    "ToStateClassID",
                    "ToStockTypeID",
                    "FlowGroupID",
                    "EndStratumID",
                    "EndSecondaryStratumID",
                    "EndTertiaryStratumID",
                    "EndStateClassID",
                    "EndMinAge"
                };

                CreateIndex(store, "SF_OutputFlow", cols);
            }

            if (store.TableExists("SF_OutputOptions"))
            {
                store.ExecuteNonQuery("ALTER TABLE SF_OutputOptions ADD COLUMN LateralOutputFL INTEGER");
                store.ExecuteNonQuery("ALTER TABLE SF_OutputOptions ADD COLUMN LateralOutputFLTimesteps INTEGER");
            }
        }

        [UpdateAttribute(0.102, "This update rebuilds indexes and updates chart criteria")]
        public static void Update_0_102(DataStore store)
        {
            RenameTablesWithPrefix(store, "SF_", "stsimsf_");

            store.ExecuteNonQuery("DROP INDEX IF EXISTS SF_FlowPathway_Index");
            store.ExecuteNonQuery("DROP INDEX IF EXISTS SF_FlowMultiplier_Index");
            store.ExecuteNonQuery("DROP INDEX IF EXISTS SF_StockTransitionMultiplier_Index");
            store.ExecuteNonQuery("DROP INDEX IF EXISTS SF_StockTypeGroupMembership_Index");
            store.ExecuteNonQuery("DROP INDEX IF EXISTS SF_FlowTypeGroupMembership_Index");
            store.ExecuteNonQuery("DROP INDEX IF EXISTS SF_OutputFlow_Index");
            store.ExecuteNonQuery("DROP INDEX IF EXISTS SF_OutputStock_Index");

            CreateIndex(store, "stsimsf_FlowPathway", new[] { "ScenarioID" });
            CreateIndex(store, "stsimsf_FlowMultiplier", new[] { "ScenarioID" });
            CreateIndex(store, "stsimsf_StockTransitionMultiplier", new[] { "ScenarioID" });
            CreateIndex(store, "stsimsf_StockTypeGroupMembership", new[] { "StockTypeID", "StockGroupID" });
            CreateIndex(store, "stsimsf_FlowTypeGroupMembership", new[] { "FlowTypeID", "FlowGroupID" });
            CreateIndex(store, "stsimsf_OutputFlow", new[] { "ScenarioID", "Iteration", "Timestep", "FromStratumID", "FromSecondaryStratumID", "FromTertiaryStratumID", "FromStateClassID", "FromStockTypeID", "TransitionTypeID", "ToStratumID", "ToStateClassID", "ToStockTypeID", "FlowGroupID", "EndStratumID", "EndSecondaryStratumID", "EndTertiaryStratumID", "EndStateClassID", "EndMinAge" });
            CreateIndex(store, "stsimsf_OutputStock", new[] { "ScenarioID", "Iteration", "Timestep", "StratumID", "SecondaryStratumID", "TertiaryStratumID", "StateClassID", "StockGroupID" });

            if (store.TableExists("core_Chart"))
            {
                store.ExecuteNonQuery("UPDATE core_Chart SET Criteria = REPLACE(Criteria, 'SF_', 'STSIMSF_')");
            }

            if (store.TableExists("core_Plot"))
            {
                store.ExecuteNonQuery("UPDATE core_Plot SET Criteria = REPLACE(Criteria, 'SF_', 'STSIMSF_')");
                store.ExecuteNonQuery("UPDATE core_Plot SET Criteria = REPLACE(Criteria, 'STKG', 'STSIMSF_STKG')");
                store.ExecuteNonQuery("UPDATE core_Plot SET Criteria = REPLACE(Criteria, 'FLOG', 'STSIMSF_FLOG')");
                store.ExecuteNonQuery("UPDATE core_Plot SET Criteria = REPLACE(Criteria, 'LFLOG', 'STSIMSF_LFLOG')");
            }
        }

        [UpdateAttribute(0.103, "This update adds AgeMin and AgeMax fields to the Flow Multipliers table")]
        public static void Update_0_103(DataStore store)
        {
            if (store.TableExists("stsimsf_FlowMultiplier"))
            {
                store.ExecuteNonQuery("ALTER TABLE stsimsf_FlowMultiplier RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE stsimsf_FlowMultiplier(FlowMultiplierID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, TertiaryStratumID INTEGER, StateClassID INTEGER, AgeMin INTEGER, AgeMax INTEGER, FlowGroupID INTEGER, FlowMultiplierTypeID INTEGER, Value DOUBLE, DistributionType INTEGER, DistributionFrequencyID INTEGER, DistributionSD DOUBLE, DistributionMin DOUBLE, DistributionMax DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO stsimsf_FlowMultiplier(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TertiaryStratumID, StateClassID, FlowGroupID, FlowMultiplierTypeID, Value, DistributionType, DistributionFrequencyID, DistributionSD, DistributionMin, DistributionMax) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, TertiaryStratumID, StateClassID, FlowGroupID, FlowMultiplierTypeID, Value, DistributionType, DistributionFrequencyID, DistributionSD, DistributionMin, DistributionMax FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

                CreateIndex(store, "stsimsf_FlowMultiplier", new[] { "ScenarioID" });
            }
        }

        [UpdateAttribute(0.104, "This update will apply namespace prefixes to chart and map criteria")]
        public static void Update_0_104(DataStore store)
        {
            if (store.TableExists("core_Chart"))
            {
                store.ExecuteNonQuery("UPDATE core_Chart SET Criteria = REPLACE(Criteria, 'STOCKGROUPS', 'STSIMSF_STOCKVARIABLESGROUP')");
                store.ExecuteNonQuery("UPDATE core_Chart SET Criteria = REPLACE(Criteria, 'FLOWGROUPS', 'STSIMSF_FLOWVARIABLESGROUP')");
            }

            RenameChartVariable(store, "stockgroupdensity", "stsimsf_StockGroupDensityVariable");
            RenameChartVariable(store, "stockgroup", "stsimsf_StockGroupVariable");
            RenameChartVariable(store, "flowgroupdensity", "stsimsf_FlowGroupDensityVariable");
            RenameChartVariable(store, "flowgroup", "stsimsf_FlowGroupVariable");

            RenamePlotVariable(store, "stkg", "stsimsf_stkg");
            RenamePlotVariable(store, "flog", "stsimsf_flog");
            RenamePlotVariable(store, "lflog", "stsimsf_lflog");
        }

        [UpdateAttribute(0.105, "This update adds spatial averaging to the output options.")]
        public static void Update_0_105(DataStore store)
        {
            if (store.TableExists("stsimsf_OutputOptions"))
            {
                store.ExecuteNonQuery("ALTER TABLE stsimsf_OutputOptions ADD COLUMN AvgSpatialOutputST INTEGER");
                store.ExecuteNonQuery("ALTER TABLE stsimsf_OutputOptions ADD COLUMN AvgSpatialOutputSTTimesteps INTEGER");
                store.ExecuteNonQuery("ALTER TABLE stsimsf_OutputOptions ADD COLUMN AvgSpatialOutputSTAcrossTimesteps INTEGER");
                store.ExecuteNonQuery("ALTER TABLE stsimsf_OutputOptions ADD COLUMN AvgSpatialOutputFL INTEGER");
                store.ExecuteNonQuery("ALTER TABLE stsimsf_OutputOptions ADD COLUMN AvgSpatialOutputFLTimesteps INTEGER");
                store.ExecuteNonQuery("ALTER TABLE stsimsf_OutputOptions ADD COLUMN AvgSpatialOutputFLAcrossTimesteps INTEGER");
                store.ExecuteNonQuery("ALTER TABLE stsimsf_OutputOptions ADD COLUMN AvgSpatialOutputLFL INTEGER");
                store.ExecuteNonQuery("ALTER TABLE stsimsf_OutputOptions ADD COLUMN AvgSpatialOutputLFLTimesteps INTEGER");
                store.ExecuteNonQuery("ALTER TABLE stsimsf_OutputOptions ADD COLUMN AvgSpatialOutputLFLAcrossTimesteps INTEGER");
            }
        }

        [UpdateAttribute(0.106, "This update adds a TargetType field to the Flow Pathways table")]
        public static void Update_0_106(DataStore store)
        {
            if (store.TableExists("stsimsf_FlowPathway"))
            {
                store.ExecuteNonQuery("ALTER TABLE stsimsf_FlowPathway RENAME TO TEMP_TABLE");

                store.ExecuteNonQuery("CREATE TABLE stsimsf_FlowPathway( " +
                    "FlowPathwayID                INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "ScenarioID                   INTEGER, " +
                    "Iteration                    INTEGER, " +
                    "Timestep                     INTEGER, " +
                    "FromStratumID                INTEGER, " +
                    "FromSecondaryStratumID       INTEGER, " +
                    "FromTertiaryStratumID        INTEGER, " +
                    "FromStateClassID             INTEGER, " +
                    "FromAgeMin                   INTEGER, " +
                    "FromStockTypeID              INTEGER, " +
                    "ToStratumID                  INTEGER, " +
                    "ToStateClassID               INTEGER, " +
                    "ToAgeMin                     INTEGER, " +
                    "ToStockTypeID                INTEGER, " +
                    "TransitionGroupID            INTEGER, " +
                    "StateAttributeTypeID         INTEGER, " +
                    "FlowTypeID                   INTEGER, " +
                    "TargetType                   INTEGER, " +
                    "Multiplier                   DOUBLE, " +
                    "TransferToStratumID          INTEGER, " +
                    "TransferToSecondaryStratumID INTEGER, " +
                    "TransferToTertiaryStratumID  INTEGER, " +
                    "TransferToStateClassID       INTEGER, " +
                    "TransferToAgeMin             INTEGER)"
                );

                store.ExecuteNonQuery("INSERT INTO stsimsf_FlowPathway( " +
                    "ScenarioID, " +
                    "Iteration, " +
                    "Timestep, " +
                    "FromStratumID, " +
                    "FromSecondaryStratumID, " +
                    "FromTertiaryStratumID, " +
                    "FromStateClassID, " +
                    "FromAgeMin, " +
                    "FromStockTypeID, " +
                    "ToStratumID, " +
                    "ToStateClassID, " +
                    "ToAgeMin, " +
                    "ToStockTypeID, " +
                    "TransitionGroupID, " +
                    "StateAttributeTypeID, " +
                    "FlowTypeID, " +
                    "Multiplier, " +
                    "TransferToStratumID, " +
                    "TransferToSecondaryStratumID, " +
                    "TransferToTertiaryStratumID, " +
                    "TransferToStateClassID, " +
                    "TransferToAgeMin) " +
                    "SELECT " +
                    "ScenarioID, " +
                    "Iteration, " +
                    "Timestep, " +
                    "FromStratumID, " +
                    "FromSecondaryStratumID, " +
                    "FromTertiaryStratumID, " +
                    "FromStateClassID, " +
                    "FromAgeMin, " +
                    "FromStockTypeID, " +
                    "ToStratumID, " +
                    "ToStateClassID, " +
                    "ToAgeMin, " +
                    "ToStockTypeID, " +
                    "TransitionGroupID, " +
                    "StateAttributeTypeID, " +
                    "FlowTypeID, " +
                    "Multiplier, " +
                    "TransferToStratumID, " +
                    "TransferToSecondaryStratumID, " +
                    "TransferToTertiaryStratumID, " +
                    "TransferToStateClassID, " +
                    "TransferToAgeMin " +
                    "FROM TEMP_TABLE");

                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
                CreateIndex(store, "stsimsf_FlowPathway", new[] { "ScenarioID" });
            }
        }

        [UpdateAttribute(0.107, "This update replaces the group name with the variable name in the chart criteria")]
        public static void Update_0_107(DataStore store)
        {
            RemoveChartGroupCriteria(store, new[] 
            {
                "stsimsf_StockVariablesGroup|stsimsf_StockGroupVariable",
                "stsimsf_StockVariablesGroup|stsimsf_StockGroupDensityVariable",
                "stsimsf_FlowVariablesGroup|stsimsf_FlowGroupVariable",
                "stsimsf_FlowVariablesGroup|stsimsf_FlowGroupDensityVariable"
            });
        }

        [UpdateAttribute(0.108, "This update changes the chart variable names to be more concise")]
        public static void Update_0_108(DataStore store)
        {
            RenameChartVariable(store, "stsimsf_StockGroupVariable", "stsimsf_StockGroup");
            RenameChartVariable(store, "stsimsf_StockGroupDensityVariable", "stsimsf_StockGroupDensity");
            RenameChartVariable(store, "stsimsf_FlowGroupVariable", "stsimsf_FlowGroup");
            RenameChartVariable(store, "stsimsf_FlowGroupDensityVariable", "stsimsf_FlowGroupDensity");
        }

        [UpdateAttribute(4.0, "This update adjusts the system tables for v2 to v3")]
        public static void Update_4_000(DataStore store)
        {
            DropTable(store, "stsimsf_Terminology");

            RenameTable(store, "stsimsf_OutputOptions", "stsim_OutputOptionsStockFlow");
            RenameTablesWithPrefix(store, "stsimsf_", "stsim_");

            RenameChartVariable(store, "stsimsf_StockGroup", "stsim_StockGroup");
            RenameChartVariable(store, "stsimsf_StockGroupDensity", "stsim_StockGroupDensity");
            RenameChartVariable(store, "stsimsf_FlowGroup", "stsim_FlowGroup");
            RenameChartVariable(store, "stsimsf_FlowGroupDensity", "stsim_FlowGroupDensity");

            RenamePlotVariable(store, "stsimsf_stkg", "stsim_stkg");
            RenamePlotVariable(store, "stsimsf_flog", "stsim_flog");
            RenamePlotVariable(store, "stsimsf_lflog", "stsim_lflog");
            RenamePlotVariable(store, "stsimsf_avgstkg", "stsim_avgstkg");
            RenamePlotVariable(store, "stsimsf_avgflog", "stsim_avgflog");
            RenamePlotVariable(store, "stsimsf_avglflog", "stsim_avglflog");

            store.ExecuteNonQuery("UPDATE core_Chart SET Criteria = REPLACE(Criteria, 'STSIMSF_', 'STSIM_')");
            store.ExecuteNonQuery("UPDATE core_Chart SET CriteriaXVariable = REPLACE(CriteriaXVariable, 'STSIMSF_', 'STSIM_')");
            store.ExecuteNonQuery("UPDATE core_Plot SET Criteria = REPLACE(Criteria, 'STSIMSF_', 'STSIM_')");
        }

#if DEBUG
        public static void ASSERT_INDEX_EXISTS(DataStore store, string tableName)
        {
            if (store.TableExists(tableName))
            {
                string IndexName = tableName + "_Index";
                string Query = string.Format(CultureInfo.InvariantCulture, "SELECT COUNT(name) FROM sqlite_master WHERE type = 'index' AND name = '{0}'", IndexName);
                Debug.Assert((long)store.ExecuteScalar(Query) == 1);
            }
        }
#endif
    }
}
