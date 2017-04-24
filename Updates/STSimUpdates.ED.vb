'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core

Partial Class STSimUpdates

    ''' <summary>
    ''' UpdateEcologicalDepartureTables_SSIM_V_1
    ''' </summary>
    ''' <remarks>
    ''' Updates the Ecological Departure tables for SyncroSim version 1.  We're doing this here since
    ''' this Add-On module might not be installed yet and we don't want to perform an upgrade the minute
    ''' the user adds it.  From this point on, the normal Module update mechanism will do the upgrades.
    ''' </remarks>
    Private Shared Sub UpdateEcologicalDepartureTables_SSIM_V_1(ByVal store As DataStore)

        If (store.TableExists("ED_Version")) Then

            Dim EDVersion As Integer = GetVersionTableValue(store, "ED_Version")

            If (EDVersion < 1) Then
                ED0000001(store)
            End If

            If (EDVersion < 2) Then
                ED0000002(store)
            End If

        Else

            ED0000001(store)
            ED0000002(store)

        End If

        If (store.TableExists("ED_Options")) Then

            store.ExecuteNonQuery("ALTER TABLE ED_Options RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE ED_Options(OptionsID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, Timesteps INTEGER, TransitionAttributeTypeID INTEGER)")
            store.ExecuteNonQuery("INSERT INTO ED_Options(ScenarioID, Timesteps, TransitionAttributeTypeID) SELECT ScenarioID, Timesteps, TransitionAttribute FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

        If (store.TableExists("ED_ReferenceCondition")) Then

            store.ExecuteNonQuery("ALTER TABLE ED_ReferenceCondition RENAME TO TEMP_TABLE")
            store.ExecuteNonQuery("CREATE TABLE ED_ReferenceCondition(ReferenceConditionID INTEGER PRIMARY KEY AUTOINCREMENT, ScenarioID INTEGER, StratumID INTEGER, StateClassID INTEGER, RelativeAmount INTEGER, Undesirability DOUBLE, Threshold DOUBLE)")
            store.ExecuteNonQuery("INSERT INTO ED_ReferenceCondition(ScenarioID, StratumID, StateClassID, RelativeAmount, Undesirability, Threshold) SELECT ScenarioID, StratumID, StateClassID, RelativeAmount, Undesirability, Threshold FROM TEMP_TABLE")
            store.ExecuteNonQuery("DROP TABLE TEMP_TABLE")

        End If

    End Sub

    Private Shared Sub ED0000001(ByVal store As DataStore)

        If (store.TableExists("ED_ReferenceCondition")) Then

            store.ExecuteNonQuery("ALTER TABLE ED_ReferenceCondition ADD COLUMN Undesirability DOUBLE")
            store.ExecuteNonQuery("ALTER TABLE ED_ReferenceCondition ADD COLUMN Threshold DOUBLE")

        End If

    End Sub

    Private Shared Sub ED0000002(ByVal store As DataStore)

        If (store.TableExists("ED_Output")) Then
            store.ExecuteNonQuery("DELETE FROM ED_Output WHERE Departure IS NULL")
        End If

    End Sub

End Class
