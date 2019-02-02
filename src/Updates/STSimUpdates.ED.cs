// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;

namespace SyncroSim.STSim
{
    internal partial class STSimUpdates
    {
        /// <summary>
        /// UpdateEcologicalDepartureTables_SSIM_V_1
        /// </summary>
        /// <remarks>
        /// Updates the Ecological Departure tables for SyncroSim version 1.  We're doing this here since
        /// this Add-On module might not be installed yet and we don't want to perform an upgrade the minute
        /// the user adds it.  From this point on, the normal Module update mechanism will do the upgrades.
        /// </remarks>
        private static void UpdateEcologicalDepartureTables_SSIM_V_1(DataStore store)
        {
            if (store.TableExists("ED_Version"))
            {
                int EDVersion = GetVersionTableValue(store, "ED_Version");

                if (EDVersion < 1)
                {
                    ED0000001(store);
                }

                if (EDVersion < 2)
                {
                    ED0000002(store);
                }
            }
            else
            {
                ED0000001(store);
                ED0000002(store);
            }

            if (store.TableExists("ED_Options"))
            {
                store.ExecuteNonQuery("ALTER TABLE ED_Options RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE ED_Options(OptionsID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Timesteps INTEGER, TransitionAttributeTypeID INTEGER)");
                store.ExecuteNonQuery("INSERT INTO ED_Options(ScenarioID, Timesteps, TransitionAttributeTypeID) SELECT ScenarioID, Timesteps, TransitionAttribute FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }

            if (store.TableExists("ED_ReferenceCondition"))
            {
                store.ExecuteNonQuery("ALTER TABLE ED_ReferenceCondition RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE ED_ReferenceCondition(ReferenceConditionID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, StratumID INTEGER, StateClassID INTEGER, RelativeAmount INTEGER, Undesirability DOUBLE, Threshold DOUBLE)");
                store.ExecuteNonQuery("INSERT INTO ED_ReferenceCondition(ScenarioID, StratumID, StateClassID, RelativeAmount, Undesirability, Threshold) SELECT ScenarioID, StratumID, StateClassID, RelativeAmount, Undesirability, Threshold FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }
        }

        private static void ED0000001(DataStore store)
        {
            if (store.TableExists("ED_ReferenceCondition"))
            {
                store.ExecuteNonQuery("ALTER TABLE ED_ReferenceCondition ADD COLUMN Undesirability DOUBLE");
                store.ExecuteNonQuery("ALTER TABLE ED_ReferenceCondition ADD COLUMN Threshold DOUBLE");
            }
        }

        private static void ED0000002(DataStore store)
        {
            if (store.TableExists("ED_Output"))
            {
                store.ExecuteNonQuery("DELETE FROM ED_Output WHERE Departure IS NULL");
            }
        }
    }
}
