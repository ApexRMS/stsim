'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core

Partial Class STSimUpdates

    ''' <summary>
    ''' UpdateDynamicMultiplierTables_SSIM_V_1
    ''' </summary>
    ''' <remarks>
    ''' Updates the Dynamic Multiplier tables for SyncroSim version 1.  We're doing this here since
    ''' this Add-On module might not be installed yet and we don't want to perform an upgrade the minute
    ''' the user adds it.  From this point on, the normal Module update mechanism will do the upgrades.
    ''' </remarks>
    Private Shared Sub UpdateDynamicMultiplierTables_SSIM_V_1(ByVal store As DataStore)

        If (store.TableExists("DM_DHSMultipliers")) Then

            store.ExecuteNonQuery("ALTER TABLE DM_DHSMultipliers RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE DM_DHSMultipliers(DHSMultipliersID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Enabled INTEGER, Frequency INTEGER, StateAttributeTypeID INTEGER, Script TEXT)")
            store.ExecuteNonQuery("INSERT INTO DM_DHSMultipliers(ScenarioID, Enabled, Frequency, StateAttributeTypeID, Script) SELECT ScenarioID, Enabled, Frequency, StateAttributeType, Script FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

    End Sub

End Class
