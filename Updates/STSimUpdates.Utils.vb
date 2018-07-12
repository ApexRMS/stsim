'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports System.IO
Imports System.Globalization
Imports SyncroSim.Core

Partial Class STSimUpdates

    Private Shared Function GetDatasheetOutputFolder(store As DataStore, scenarioId As Integer, datasheetName As String) As String

        Dim baseFolder = GetCurrentOutputFolderBase(store)
        Return Path.Combine(baseFolder, String.Format(CultureInfo.InvariantCulture, "Scenario-{0}", scenarioId), datasheetName)

    End Function

    Private Shared Function GetLegacyOutputFolder(store As DataStore, scenarioId As Integer) As String

        Dim baseFolder = GetCurrentOutputFolderBase(store)
        Return Path.Combine(baseFolder, String.Format(CultureInfo.InvariantCulture, "Scenario-{0}", scenarioId), "Spatial")

    End Function

    Private Shared Function GetCurrentOutputFolderBase(ByVal store As DataStore) As String

        Const FOLDER_NAME As String = "OutputFolderName"

        Dim dt As DataTable = Nothing

        If (store.TableExists("SSim_Files")) Then
            dt = store.CreateDataTable("SSim_Files")
        ElseIf (store.TableExists("SSim_File")) Then
            dt = store.CreateDataTable("SSim_File")
        Else
            dt = store.CreateDataTable("SSim_SysFolder")
        End If

        Dim dr As DataRow = Nothing

        If dt.Rows.Count = 1 Then
            dr = dt.Rows(0)
        End If

        Debug.Assert(dt.Rows.Count = 1 Or dt.Rows.Count = 0)

        Dim p As String = Nothing

        If (dr IsNot Nothing) AndAlso (dr(FOLDER_NAME) IsNot DBNull.Value) Then
            p = CStr(dr(FOLDER_NAME))
        Else
            p = store.DataStoreConnection.ConnectionString & ".output"
        End If

        Return p

    End Function

    Private Shared Function ExpandMapCriteriaFrom1x(ByVal criteria As String) As String

        Dim GetDataSheetName As Func(Of String, String) =
            Function(c As String) As String

                If (c = "tg") Then
                    Return "STSim_TransitionGroup"
                ElseIf (c = "tgap") Then
                    Return "STSim_TransitionGroup"
                ElseIf (c = "sa") Then
                    Return "STSim_StateAttributeType"
                ElseIf (c = "ta") Then
                    Return "STSim_TransitionAttributeType"
                ElseIf (c = "flo") Then
                    Return "SF_FlowType"
                ElseIf (c = "stk") Then
                    Return "SF_StockType"
                ElseIf (c = "stkg") Then
                    Return "SF_StockGroup"
                ElseIf (c = "flog") Then
                    Return "SF_FlowGroup"
                Else

                    'This will eventually be discarded by the map criteria control
                    'but the setting will not be ported to 2.0

                    Debug.Assert(False)
                    Return "Unknown"

                End If

            End Function

        Dim parts() As String = criteria.Split(CChar("-"))
        Debug.Assert(parts.Length <= 2)

        If (parts.Length = 1) Then
            Return criteria
        ElseIf (parts.Length = 2) Then

            'tg-342-itemid-342-itemsrc-STSim_TransitionGroup
            Dim newcr = String.Format(CultureInfo.InvariantCulture, "{0}-itemid-{1}-itemsrc-{2}", criteria, parts(1), GetDataSheetName(parts(0)))
            Return newcr

        Else
            Debug.Assert(False)
            Return criteria
        End If

    End Function

End Class
