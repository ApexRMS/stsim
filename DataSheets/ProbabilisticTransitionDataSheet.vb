'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports System.Reflection
Imports SyncroSim.Core

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class ProbabilisticTransitionDataSheet
    Inherits DataSheet

    Protected Overrides Sub OnDataSheetChanged(e As DataSheetMonitorEventArgs)

        MyBase.OnDataSheetChanged(e)

        Dim Primary As String = Nothing
        Dim Secondary As String = Nothing

        GetStratumLabelTerminology(e.DataSheet, Primary, Secondary)

        Me.Columns(DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME).DisplayName = Primary
        Me.Columns(DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME).DisplayName = "To " & Primary

    End Sub

    Public Overrides Sub Validate(
        ByVal proposedRow As DataRow,
        ByVal transferMethod As DataTransferMethod)

        MyBase.Validate(proposedRow, transferMethod)

        Dim dt As DataTable = Me.GetDataSheet(DATASHEET_DT_NAME).GetData()
        Dim Analyzer As New DTAnalyzer(dt, Me.Project)
        Dim StratumIdSource As Nullable(Of Integer) = Nothing
        Dim StateClassIdSource As Integer = 0
        Dim StratumIdDest As Nullable(Of Integer) = Nothing
        Dim StateClassIdDest As Nullable(Of Integer) = Nothing

        DTAnalyzer.GetPTFieldValues(proposedRow, StratumIdSource, StateClassIdSource, StratumIdDest, StateClassIdDest)

        If (Not Analyzer.CanResolveStateClass(StratumIdSource, StratumIdSource, StateClassIdSource)) Then
            Analyzer.ThrowDataException(StateClassIdSource, False)
        End If

        If (StateClassIdDest.HasValue) Then

            If (Not Analyzer.CanResolveStateClass(StratumIdSource, StratumIdDest, StateClassIdDest.Value)) Then
                Analyzer.ThrowDataException(StateClassIdDest.Value, True)
            End If

        End If

    End Sub

    Public Overrides Sub Validate(
        ByVal proposedData As System.Data.DataTable,
        ByVal transferMethod As DataTransferMethod)

        MyBase.Validate(proposedData, transferMethod)

        Dim DeterministicSheet As DataSheet = Me.GetDataSheet(DATASHEET_DT_NAME)
        Dim Analyzer As New DTAnalyzer(DeterministicSheet.GetData(), Me.Project)

        Const IMPORT_ERROR As String =
            "Error importing transitions." & vbCrLf & vbCrLf &
            "Note that each probabilistic transition's source and destination state class must exist in " &
            "this scenario's deterministic transition records.   More information:" & vbCrLf & vbCrLf

        Try

            For Each dr As DataRow In proposedData.Rows

                Dim StratumIdSource As Nullable(Of Integer) = Nothing
                Dim StateClassIdSource As Integer = 0
                Dim StratumIdDest As Nullable(Of Integer) = Nothing
                Dim StateClassIdDest As Nullable(Of Integer) = Nothing

                DTAnalyzer.GetPTFieldValues(dr, StratumIdSource, StateClassIdSource, StratumIdDest, StateClassIdDest)

                If (Not Analyzer.CanResolveStateClass(StratumIdSource, StratumIdSource, StateClassIdSource)) Then
                    Analyzer.ThrowDataException(StateClassIdSource, False)
                End If

                If (StateClassIdDest.HasValue) Then

                    If (Not Analyzer.CanResolveStateClass(StratumIdSource, StratumIdDest, StateClassIdDest.Value)) Then
                        Analyzer.ThrowDataException(StateClassIdDest.Value, True)
                    End If

                End If

            Next

        Catch ex As DataException
            Throw New DataException(IMPORT_ERROR & ex.Message)
        End Try

    End Sub

End Class
