// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;

namespace SyncroSim.STSim
{
    internal partial class STSimUpdates
    {
        /// <summary>
        /// UpdateDynamicMultiplierTables_SSIM_V_1
        /// </summary>
        /// <remarks>
        /// Updates the Dynamic Multiplier tables for SyncroSim version 1.  We're doing this here since
        /// this Add-On module might not be installed yet and we don't want to perform an upgrade the minute
        /// the user adds it.  From this point on, the normal Module update mechanism will do the upgrades.
        /// </remarks>
        private static void UpdateDynamicMultiplierTables_SSIM_V_1(DataStore store)
        {
            if (store.TableExists("DM_DHSMultipliers"))
            {
                store.ExecuteNonQuery("ALTER TABLE DM_DHSMultipliers RENAME TO TEMP_TABLE");
                store.ExecuteNonQuery("CREATE TABLE DM_DHSMultipliers(DHSMultipliersID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Enabled INTEGER, Frequency INTEGER, StateAttributeTypeID INTEGER, Script TEXT)");
                store.ExecuteNonQuery("INSERT INTO DM_DHSMultipliers(ScenarioID, Enabled, Frequency, StateAttributeTypeID, Script) SELECT ScenarioID, Enabled, Frequency, StateAttributeType, Script FROM TEMP_TABLE");
                store.ExecuteNonQuery("DROP TABLE TEMP_TABLE");
            }
        }
    }
}
