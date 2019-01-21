// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;

namespace SyncroSim.STSim
{
    internal partial class STSimUpdates
    {
        /// <summary>
        /// UpdateStockFlowTables_SSIM_V_1
        /// </summary>
        /// <remarks>
        /// Updates the Stock Flow tables for SyncroSim version 1.  We're doing this here since
        /// this Add-On module might not be installed yet and we don't want to perform an upgrade the minute
        /// the user adds it.  From this point on, the normal Module update mechanism will do the upgrades.
        /// </remarks>
        private static void UpdateStockFlowTables_SSIM_V_1(DataStore store)
        {
            if (store.TableExists("SF_Version"))
            {
                int SFVersion = GetVersionTableValue(store, "SF_Version");

                if (SFVersion < 1)
                {
                    SF0000001(store);
                }

                if (SFVersion < 2)
                {
                    SF0000002(store);
                }

                if (SFVersion < 3)
                {
                    SF0000003(store);
                }
            }
            else
            {
                SF0000001(store);
                SF0000002(store);
                SF0000003(store);
            }

            if (store.TableExists("SF_StockGroup"))
            {
                store.ExecuteNonQuery("ALTER TABLE SF_StockGroup RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE SF_StockGroup(StockGroupID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT, Description TEXT, Units TEXT)");
                store.ExecuteNonQuery("INSERT INTO SF_StockGroup(StockGroupID, ProjectID, Name, Description, Units) SELECT SF_StockGroupID, ProjectID, Name, Description, Units FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("SF_StockType"))
            {
                store.ExecuteNonQuery("ALTER TABLE SF_StockType RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE SF_StockType(StockTypeID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT, Description TEXT)");
                store.ExecuteNonQuery("INSERT INTO SF_StockType(StockTypeID, ProjectID, Name, Description) SELECT SF_StockTypeID, ProjectID, Name, Description FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("SF_FlowGroup"))
            {
                store.ExecuteNonQuery("ALTER TABLE SF_FlowGroup RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE SF_FlowGroup(FlowGroupID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT, Description TEXT, Units TEXT)");
                store.ExecuteNonQuery("INSERT INTO SF_FlowGroup(FlowGroupID, ProjectID, Name, Description, Units) SELECT SF_FlowGroupID, ProjectID, Name, Description, Units FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("SF_FlowType"))
            {
                store.ExecuteNonQuery("ALTER TABLE SF_FlowType RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE SF_FlowType(FlowTypeID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT, Description TEXT, TransitionGroupID INTEGER, StateAttributeTypeID INTEGER)");
                store.ExecuteNonQuery("INSERT INTO SF_FlowType(FlowTypeID, ProjectID, Name, Description, TransitionGroupID, StateAttributeTypeID) SELECT SF_FlowTypeID, ProjectID, Name, Description, TransitionGroupID, StateAttributeTypeID FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("SF_Terminology"))
            {
                store.ExecuteNonQuery("ALTER TABLE SF_Terminology RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE SF_Terminology(TerminologyID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, StockUnits TEXT)");
                store.ExecuteNonQuery("INSERT INTO SF_Terminology(TerminologyID, ProjectID, StockUnits) SELECT SF_TerminologyID, ProjectID, StockUnits FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("SF_FlowPathway"))
            {
                store.ExecuteNonQuery("ALTER TABLE SF_FlowPathway RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE SF_FlowPathway(FlowPathwayID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, FromStratumID INTEGER, FromSecondaryStratumID INTEGER, FromStateClassID INTEGER, FromAgeMin INTEGER, FromStockTypeID INTEGER, ToStratumID INTEGER, ToStateClassID INTEGER, ToAgeMin INTEGER, ToStockTypeID INTEGER, FlowTypeID INTEGER, Multiplier DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO SF_FlowPathway(ScenarioID, Iteration, Timestep, FromStratumID, FromStateClassID, FromAgeMin, FromStockTypeID, ToStratumID, ToStateClassID, ToAgeMin, ToStockTypeID, FlowTypeID, Multiplier) SELECT ScenarioID, Iteration, Timestep, StratumID, StateClassID, MinimumAge, StockTypeIDSource, StratumID, NULL, MinimumAge, StockTypeIDDest, FlowTypeID, Proportion FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("SF_InitialStockNonSpatial"))
            {
                store.ExecuteNonQuery("ALTER TABLE SF_InitialStockNonSpatial RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE SF_InitialStockNonSpatial(InitialStockNonSpatialID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, StockTypeID INTEGER, StateAttributeTypeID INTEGER)");
                store.ExecuteNonQuery("INSERT INTO SF_InitialStockNonSpatial(ScenarioID, StockTypeID, StateAttributeTypeID) SELECT ScenarioID, StockTypeID, StateAttributeTypeID FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("SF_InitialStockSpatial"))
            {
                store.ExecuteNonQuery("ALTER TABLE SF_InitialStockSpatial RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE SF_InitialStockSpatial(InitialStockSpatialID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, StockTypeID INTEGER, RasterFileName TEXT)");
                store.ExecuteNonQuery("INSERT INTO SF_InitialStockSpatial(ScenarioID, StockTypeID, RasterFileName) SELECT ScenarioID, StockTypeID, RasterFileName FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("SF_StockTypeGroupMembership"))
            {
                store.ExecuteNonQuery("ALTER TABLE SF_StockTypeGroupMembership RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE SF_StockTypeGroupMembership(StockTypeGroupMembershipID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, StockTypeID INTEGER, StockGroupID INTEGER, Value DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO SF_StockTypeGroupMembership(ScenarioID, StockTypeID, StockGroupID, Value) SELECT ScenarioID, StockTypeID, StockGroupID, Value FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("SF_StockGroupGroupMembership"))
            {
                store.ExecuteNonQuery("ALTER TABLE SF_StockGroupGroupMembership RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE SF_StockGroupGroupMembership(StockGroupGroupMembershipID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, StockGroupIDOne INTEGER, StockGroupIDTwo INTEGER, Value DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO SF_StockGroupGroupMembership(ScenarioID, StockGroupIDOne, StockGroupIDTwo, Value) SELECT ScenarioID, StockGroupID1, StockGroupID2, Value FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("SF_FlowTypeGroupMembership"))
            {
                store.ExecuteNonQuery("ALTER TABLE SF_FlowTypeGroupMembership RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE SF_FlowTypeGroupMembership(FlowTypeGroupMembershipID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, FlowTypeID INTEGER, FlowGroupID INTEGER, Value DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO SF_FlowTypeGroupMembership(ScenarioID, FlowTypeID, FlowGroupID, Value) SELECT ScenarioID, FlowTypeID, FlowGroupID, Value FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("SF_FlowGroupGroupMembership"))
            {
                store.ExecuteNonQuery("ALTER TABLE SF_FlowGroupGroupMembership RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE SF_FlowGroupGroupMembership(FlowGroupGroupMembershipID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, FlowGroupIDOne INTEGER, FlowGroupIDTwo INTEGER, Value DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO SF_FlowGroupGroupMembership(ScenarioID, FlowGroupIDOne, FlowGroupIDTwo, Value) SELECT ScenarioID, FlowGroupID1, FlowGroupID2, Value FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("SF_OutputOptions"))
            {
                store.ExecuteNonQuery("ALTER TABLE SF_OutputOptions RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE SF_OutputOptions(OutputOptionsID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, SummaryOutputST INTEGER, SummaryOutputSTTimesteps INTEGER, SummaryOutputFL INTEGER, SummaryOutputFLTimesteps INTEGER, SpatialOutputST INTEGER, SpatialOutputSTTimesteps INTEGER, SpatialOutputFL INTEGER, SpatialOutputFLTimesteps INTEGER)");
                store.ExecuteNonQuery("INSERT INTO SF_OutputOptions(ScenarioID, SummaryOutputST, SummaryOutputSTTimesteps, SummaryOutputFL, SummaryOutputFLTimesteps, SpatialOutputST, SpatialOutputSTTimesteps, SpatialOutputFL, SpatialOutputFLTimesteps) SELECT ScenarioID, SummaryOutputST, SummaryOutputSTTimesteps, SummaryOutputFL, SummaryOutputFLTimesteps, SpatialOutputST, SpatialOutputSTTimesteps, SpatialOutputFL, SpatialOutputFLTimesteps FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("SF_OutputStock"))
            {
                store.ExecuteNonQuery("DROP INDEX IF EXISTS SF_OutputStock_Index");
                store.ExecuteNonQuery("DROP INDEX IF EXISTS SF_OutputStock_Index_ST");

                store.ExecuteNonQuery("ALTER TABLE SF_OutputStock RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE SF_OutputStock(ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, StateClassID INTEGER, StockTypeID INTEGER, Amount DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO SF_OutputStock(ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, StockTypeID, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, StockTypeID, Amount FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

                store.ExecuteNonQuery("CREATE INDEX SF_OutputStock_Index ON SF_OutputStock(ScenarioID, Iteration, Timestep, StockTypeID)");
            }

            if (store.TableExists("SF_OutputStockGroupMultiplier"))
            {
                store.ExecuteNonQuery("DROP INDEX IF EXISTS SF_OutputStockGroupMultiplier_Index");
                store.ExecuteNonQuery("DROP INDEX IF EXISTS SF_OutputStockGroupMultiplier_Index_ST");

                store.ExecuteNonQuery("CREATE INDEX SF_OutputStockGroupMultiplier_Index ON SF_OutputStockGroupMultiplier(ScenarioID, StockTypeID, StockGroupID)");
            }

            if (store.TableExists("SF_OutputFlow"))
            {
                store.ExecuteNonQuery("DROP INDEX IF EXISTS SF_OutputFlow_Index");
                store.ExecuteNonQuery("DROP INDEX IF EXISTS SF_OutputFlow_Index_FT");

                store.ExecuteNonQuery("ALTER TABLE SF_OutputFlow RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE SF_OutputFlow(ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, FromStratumID INTEGER, FromSecondaryStratumID INTEGER, FromStateClassID INTEGER, FromStockTypeID INTEGER, TransitionTypeID INTEGER, ToStratumID INTEGER, ToSecondaryStratumID INTEGER, ToStateClassID INTEGER, ToStockTypeID INTEGER, FlowTypeID INTEGER, Amount DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO SF_OutputFlow(ScenarioID, Iteration, Timestep, FromStratumID, ToStratumID, FlowTypeID, Amount) SELECT ScenarioID, Iteration, Timestep, StratumID, StratumID, FlowTypeID, Amount FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

                store.ExecuteNonQuery("CREATE INDEX SF_OutputFlow_Index ON SF_OutputFlow(ScenarioID, Iteration, Timestep, FlowTypeID)");
            }

            if (store.TableExists("SF_OutputFlowGroupMultiplier"))
            {
                store.ExecuteNonQuery("DROP INDEX IF EXISTS SF_OutputFlowGroupMultiplier_Index");
                store.ExecuteNonQuery("DROP INDEX IF EXISTS SF_OutputFlowGroupMultiplier_Index_FT");

                store.ExecuteNonQuery("CREATE INDEX SF_OutputFlowGroupMultiplier_Index ON SF_OutputFlowGroupMultiplier(ScenarioID, FlowTypeID, FlowGroupID)");
            }

            //Remove Transition Group from FlowType and add it to Flow Pathway.  Note that we want to migrate the Transition Group values
            //from SF_FlowType to SF_FlowPathway - these are currently in a 1 to 1 relationship, and NULL is allowed.

            if (store.TableExists("SF_FlowPathway"))
            {
                store.ExecuteNonQuery("ALTER TABLE SF_FlowPathway RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE SF_FlowPathway(FlowPathwayID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, FromStratumID INTEGER, FromSecondaryStratumID INTEGER, FromStateClassID INTEGER, FromAgeMin INTEGER, FromStockTypeID INTEGER, ToStratumID INTEGER, ToStateClassID INTEGER, ToAgeMin INTEGER, ToStockTypeID INTEGER, TransitionGroupID INTEGER, StateAttributeTypeID INTEGER, FlowTypeID INTEGER, Multiplier DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO SF_FlowPathway(ScenarioID, Iteration, Timestep, FromStratumID, FromSecondaryStratumID, FromStateClassID, FromAgeMin, FromStockTypeID, ToStratumID, ToStateClassID, ToAgeMin, ToStockTypeID, FlowTypeID, Multiplier) SELECT ScenarioID, Iteration, Timestep, FromStratumID, FromSecondaryStratumID, FromStateClassID, FromAgeMin, FromStockTypeID, ToStratumID, ToStateClassID, ToAgeMin, ToStockTypeID, FlowTypeID, Multiplier FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");

                store.ExecuteNonQuery("UPDATE SF_FlowPathway SET TransitionGroupID=(SELECT TransitionGroupID FROM SF_FlowType WHERE SF_FlowType.FlowTypeID=SF_FlowPathway.FlowTypeID)");
                store.ExecuteNonQuery("UPDATE SF_FlowPathway SET StateAttributeTypeID=(SELECT StateAttributeTypeID FROM SF_FlowType WHERE SF_FlowType.FlowTypeID=SF_FlowPathway.FlowTypeID)");

                store.ExecuteNonQuery("ALTER TABLE SF_FlowType RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE SF_FlowType(FlowTypeID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT, Description TEXT, StateAttributeTypeID INTEGER)");
                store.ExecuteNonQuery("INSERT INTO SF_FlowType(FlowTypeID, ProjectID, Name, Description, StateAttributeTypeID) SELECT FlowTypeID, ProjectID, Name, Description, StateAttributeTypeID FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }
        }

        /// <summary>
        /// SF0000001
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// We are only changing the column order here.  Specifically, we want Iteration and Timestep to come
        /// before after ScenarioID and before StratumID.  We are also going to take the opportunity to rename 
        /// the table to SF_FlowPathway instead of SF_FlowPathways so as to be consistent with other table names.
        /// </remarks>
        private static void SF0000001(DataStore store)
        {
            if (!store.TableExists("SF_FlowPathways"))
            {
                return;
            }

            store.ExecuteNonQuery("ALTER TABLE SF_FlowPathways RENAME TO TEMP_TABLE");
            store.ExecuteNonQuery("CREATE TABLE SF_FlowPathway (SF_FlowPathwayID INTEGER PRIMARY KEY AUTOINCREMENT, ProjectID INTEGER, ScenarioID INTEGER, Iteration INTEGER, Timestep INTEGER, StratumID INTEGER, StateClassID INTEGER, MinimumAge INTEGER, MaximumAge INTEGER, FlowTypeID INTEGER, StockTypeIDSource INTEGER, StockTypeIDDest INTEGER, Proportion DOUBLE)");
            store.ExecuteNonQuery("INSERT INTO SF_FlowPathway(ProjectID, ScenarioID,  Iteration, Timestep, StratumID, StateClassID, MinimumAge, MaximumAge, FlowTypeID, StockTypeIDSource, StockTypeIDDest, Proportion) SELECT ProjectID, ScenarioID,  Iteration, Timestep, StratumID, StateClassID, MinimumAge, MaximumAge, FlowTypeID, StockTypeIDSource, StockTypeIDDest, Proportion FROM TEMP_TABLE");
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
        }

        /// <summary>
        /// STSIM0000002
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update will:
        /// 
        /// (1.) Remove the embedded ID value from STime_ChartVariable.VariableName.
        /// (2.) Rename STime_ChartVariable.DataSheetName to contain the correct data sheet name
        /// (3.) Prepend a "T-" to each variable name so it can be distinguished from groups
        /// </remarks>
        private static void SF0000002(DataStore store)
        {
            if (store.TableExists("STime_ChartVariable"))
            {
                store.ExecuteNonQuery("UPDATE STime_ChartVariable SET VariableName = rtrim(VariableName, '0123456789') WHERE DataSheetName = 'SF_OutputStockDataFeed'");
                store.ExecuteNonQuery("UPDATE STime_ChartVariable SET VariableName = rtrim(VariableName, '0123456789') WHERE DataSheetName = 'SF_OutputFlowDataFeed'");
                store.ExecuteNonQuery("UPDATE STime_ChartVariable SET VariableName = rtrim(VariableName, '-') WHERE DataSheetName = 'SF_OutputStockDataFeed'");
                store.ExecuteNonQuery("UPDATE STime_ChartVariable SET VariableName = rtrim(VariableName, '-') WHERE DataSheetName = 'SF_OutputFlowDataFeed'");

                store.ExecuteNonQuery("UPDATE STime_ChartVariable SET DataSheetName = 'SF_OutputStock' WHERE DataSheetName = 'SF_OutputStockDataFeed'");
                store.ExecuteNonQuery("UPDATE STime_ChartVariable SET DataSheetName = 'SF_OutputFlow' WHERE DataSheetName = 'SF_OutputFlowDataFeed'");

                store.ExecuteNonQuery("UPDATE STime_ChartVariable SET VariableName = ('T-' || VariableName) WHERE DataSheetName = 'SF_OutputStock'");
                store.ExecuteNonQuery("UPDATE STime_ChartVariable SET VariableName = ('T-' || VariableName) WHERE DataSheetName = 'SF_OutputFlow'");
            }
        }

        /// <summary>
        /// SF0000003
        /// </summary>
        /// <param name="store"></param>
        /// <remarks>
        /// This update will add an index on the stock/flow type ID column for:
        /// 
        /// SF_OutputFlow
        /// SF_OutputFlowGroupMultiplier
        /// SF_OutputStock
        /// SF_OutputStockGroupMultiplier
        /// 
        /// </remarks>
        private static void SF0000003(DataStore store)
        {
            if (store.TableExists("SF_OutputFlow"))
            {
                store.ExecuteNonQuery("CREATE INDEX SF_OutputFlow_Index_FT ON SF_OutputFlow(FlowTypeID)");
            }

            if (store.TableExists("SF_OutputFlowGroupMultiplier"))
            {
                store.ExecuteNonQuery("DELETE FROM SF_OutputFlowGroupMultiplier");
                store.ExecuteNonQuery("CREATE INDEX SF_OutputFlowGroupMultiplier_Index_FT ON SF_OutputFlowGroupMultiplier(FlowTypeID)");
            }

            if (store.TableExists("SF_OutputStock"))
            {
                store.ExecuteNonQuery("CREATE INDEX SF_OutputStock_Index_ST ON SF_OutputStock(StockTypeID)");
            }

            if (store.TableExists("SF_OutputStockGroupMultiplier"))
            {
                store.ExecuteNonQuery("DELETE FROM SF_OutputStockGroupMultiplier");
                store.ExecuteNonQuery("CREATE INDEX SF_OutputStockGroupMultiplier_Index_ST ON SF_OutputStockGroupMultiplier(StockTypeID)");
            }
        }
    }
}
