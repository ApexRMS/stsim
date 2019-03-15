// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;

namespace SyncroSim.STSim
{
    internal partial class STSimUpdates
    {
        /// <summary>
        /// UpdateTPETables_SSIM_V_1
        /// </summary>
        /// <remarks>
        /// Updates the Transition Probability Estimator tables for SyncroSim version 1.  We're doing this here since
        /// this Add-On module might not be installed yet and we don't want to perform an upgrade the minute
        /// the user adds it.  From this point on, the normal Module update mechanism will do the upgrades.
        /// </remarks>
        private static void UpdateTransitionProbabilityEstimatorTables_SSIM_V_1(DataStore store)
        {
            if (store.TableExists("TPE_AnalysisUnit"))
            {
                store.ExecuteNonQuery("ALTER TABLE TPE_AnalysisUnit RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE TPE_AnalysisUnit(AnalysisUnitID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT, Description TEXT)");
                store.ExecuteNonQuery("INSERT INTO TPE_AnalysisUnit(AnalysisUnitID, ProjectID, Name, Description) SELECT TPE_AnalysisUnitID, ProjectID, Name, Description FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("TPE_Indicator"))
            {
                store.ExecuteNonQuery("ALTER TABLE TPE_Indicator RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE TPE_Indicator(IndicatorID INTEGER PRIMARY KEY, ProjectID INTEGER, Name TEXT, Description TEXT, Units TEXT)");
                store.ExecuteNonQuery("INSERT INTO TPE_Indicator(IndicatorID, ProjectID, Name, Description, Units) SELECT TPE_IndicatorID, ProjectID, Name, Description, Units FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("TPE_IndicatorTimeSeries"))
            {
                store.ExecuteNonQuery("ALTER TABLE TPE_IndicatorTimeSeries RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE TPE_IndicatorTimeSeries(IndicatorTimeSeriesID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, AnalysisUnitID INTEGER, IndicatorID INTEGER, Replicate INTEGER, Timestep INTEGER, Value DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO TPE_IndicatorTimeSeries(ScenarioID, AnalysisUnitID, IndicatorID, Replicate, Timestep, Value) SELECT ScenarioID, AnalysisUnitID, IndicatorID, Replicate, Timestep, Value FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("TPE_TransitionThreshold"))
            {
                store.ExecuteNonQuery("ALTER TABLE TPE_TransitionThreshold RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE TPE_TransitionThreshold(TransitionThresholdID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, TransitionGroupID INTEGER, StratumID INTEGER, SecondaryStratumID INTEGER, StateClassID INTEGER, Timestep INTEGER,  AnalysisUnitID INTEGER, FromIndicatorID INTEGER, FromMinThresholdValue DOUBLE, FromMaxThresholdValue DOUBLE, FromTimestep INTEGER, ToIndicatorID INTEGER, ToMinThresholdValue DOUBLE, ToMaxThresholdValue DOUBLE, ToTimestep INTEGER)");
                store.ExecuteNonQuery("INSERT INTO TPE_TransitionThreshold(ScenarioID, TransitionGroupID, StratumID, SecondaryStratumID, StateClassID, Timestep, AnalysisUnitID, FromIndicatorID, FromMinThresholdValue, FromMaxThresholdValue, FromTimestep, ToIndicatorID, ToMinThresholdValue, ToMaxThresholdValue, ToTimestep) SELECT ScenarioID, TransitionGroupID, StratumID, SecondaryStratumID, StateClassID, Timestep, AnalysisUnitID, FromIndicatorID, FromMinThresholdValue, FromMaxThresholdValue, FromTimestep, ToIndicatorID, ToMinThresholdValue, ToMaxThresholdValue, ToTimestep FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }
        }
    }
}
