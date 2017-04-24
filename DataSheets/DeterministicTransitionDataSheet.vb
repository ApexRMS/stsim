'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Imports SyncroSim.Core
Imports System.Reflection

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class DeterministicTransitionDataSheet
    Inherits DataSheet

    Protected Overrides Sub OnDataSheetChanged(e As DataSheetMonitorEventArgs)

        MyBase.OnDataSheetChanged(e)

        Dim Primary As String = Nothing
        Dim Secondary As String = Nothing

        GetStratumLabelTerminology(e.DataSheet, Primary, Secondary)

        Me.Columns(DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME).DisplayName = Primary
        Me.Columns(DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME).DisplayName = "To " & Primary

    End Sub

    Public Overrides Sub Validate(ByVal proposedValue As Object, columnName As String)

        MyBase.Validate(proposedValue, columnName)

        If (columnName = DATASHEET_DT_LOCATION_COLUMN_NAME) Then

            If (Not DTAnalyzer.IsValidLocation(proposedValue)) Then
                Throw New DataException(ERROR_INVALID_CELL_ADDRESS)
            End If

        End If

    End Sub

    Public Overrides Sub Validate(
        ByVal proposedRow As DataRow,
        ByVal transferMethod As DataTransferMethod)

        MyBase.Validate(proposedRow, transferMethod)

        Dim StratumIdSource As Nullable(Of Integer) = Nothing
        Dim StateClassIdSource As Integer = 0
        Dim StratumIdDest As Nullable(Of Integer) = Nothing
        Dim StateClassIdDest As Nullable(Of Integer) = Nothing

        DTAnalyzer.GetDTFieldValues(proposedRow, StratumIdSource, StateClassIdSource, StratumIdDest, StateClassIdDest)

        If (Not StateClassIdDest.HasValue) Then
            Return
        End If

        If (StateClassIdDest.Value = StateClassIdSource) Then

            If (Not StratumIdDest.HasValue) Then
                Return
            End If

            If (NullableUtilities.NullableIdsEqual(StratumIdSource, StratumIdDest)) Then
                Return
            End If

        End If

        Dim Analyzer As New DTAnalyzer(Me.GetData(), Me.Project)

        If (Not Analyzer.CanResolveStateClass(StratumIdSource, StratumIdDest, StateClassIdDest.Value)) Then
            Analyzer.ThrowDataException(StateClassIdDest.Value, True)
        End If

    End Sub

    ''' <summary>
    ''' Overrides Validate
    ''' </summary>
    ''' <param name="data"></param>
    ''' <param name="transferMethod"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' While the incoming state class Ids can themselves be valid, they must also be valid within the context of the union of the
    ''' incoming data and the existing data.  In other words, each destination state class must point to a source state class within
    ''' the same stratum.
    ''' </remarks>
    Public Overrides Sub Validate(
        ByVal proposedData As System.Data.DataTable,
        ByVal transferMethod As DataTransferMethod)

        MyBase.Validate(proposedData, transferMethod)

        Dim dtdata As DataTable = Me.GetData()
        Dim AnalyzerExisting As New DTAnalyzer(dtdata, Me.Project)
        Dim AnalyzerProposed As New DTAnalyzer(proposedData, Me.Project)

        For Each ProposedRow As DataRow In proposedData.Rows

            Dim StratumIdSource As Nullable(Of Integer) = Nothing
            Dim StateClassIdSource As Integer = 0
            Dim StratumIdDest As Nullable(Of Integer) = Nothing
            Dim StateClassIdDest As Nullable(Of Integer) = Nothing

            DTAnalyzer.GetDTFieldValues(ProposedRow, StratumIdSource, StateClassIdSource, StratumIdDest, StateClassIdDest)

            If (Not StateClassIdDest.HasValue) Then
                Return
            End If

            'If the state class is not part of the incoming data then we need to see if it is part of the existing data, and 
            'if it isn't then we can't continue.  Note that if the import option is 'Overwrite' then the state class
            'will not appear in the existing data!

            Dim ClassInClip As Boolean = AnalyzerProposed.CanResolveStateClass(StratumIdSource, StratumIdDest, StateClassIdDest.Value)
            Dim ClassInExisting As Boolean = AnalyzerExisting.CanResolveStateClass(StratumIdSource, StratumIdDest, StateClassIdDest.Value)
            Dim IsOverwrite As Boolean = (transferMethod = SyncroSim.Core.DataTransferMethod.Overwrite)

            If (Not ClassInClip) Then

                If (IsOverwrite Or (Not ClassInExisting)) Then
                    AnalyzerExisting.ThrowDataException(StateClassIdDest.Value, True)
                End If

            End If

            'Also validate the location

            If (Not DTAnalyzer.IsValidLocation(ProposedRow(DATASHEET_DT_LOCATION_COLUMN_NAME))) Then
                Throw New DataException(ERROR_INVALID_CELL_ADDRESS)
            End If

        Next

    End Sub

    ''' <summary>
    ''' Overrides OnAfterRowsDeleted
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' If deterministic transitions have been deleted then it's possible that some transitions no longer reference
    ''' valid state classes.  If it is a deterministic transition then we just need to fix up its destination state
    ''' class and stratum to be the same as the source state class and stratum.  If it's a probabilistic transition, 
    ''' however, we need to delete it.
    ''' <remarks></remarks>
    Protected Overrides Sub OnRowsDeleted(
        ByVal sender As Object,
        ByVal e As SyncroSim.Core.DataSheetRowEventArgs)

        Dim Analyzer As New DTAnalyzer(Me.GetData(), Me.Project)

        If (Me.ResolveDeterministicTransitions(Analyzer)) Then
            Me.Changes.Add(New ChangeRecord(Me, "DT OnRowsDeleted Modified DT Rows"))
        End If

        If (Me.ResolveProbabilisticTransitions(Analyzer)) Then
            Me.GetDataSheet(DATASHEET_PT_NAME).Changes.Add(New ChangeRecord(Me, "DT OnRowsDeleted Deleted PT Rows"))
        End If

        MyBase.OnRowsDeleted(sender, e)

#If DEBUG Then
        Me.VALIDATE_DETERMINISTIC_TRANSITIONS()
        Me.VALIDATE_PROBABILISTIC_TRANSITIONS()
#End If

    End Sub

    Protected Overrides Sub OnRowsModified(sender As Object, e As DataSheetRowEventArgs)

        MyBase.OnRowsModified(sender, e)

        Dim Analyzer As New DTAnalyzer(Me.GetData(), Me.Project)

        If (Me.ResolveDeterministicTransitions(Analyzer)) Then
            Me.Changes.Add(New ChangeRecord(Me, "DT OnRowsModified Modified DT Rows"))
        End If

        If (Me.ResolveProbabilisticTransitions(Analyzer)) Then
            Me.GetDataSheet(DATASHEET_PT_NAME).Changes.Add(New ChangeRecord(Me, "DT OnRowsModified Deleted PT Rows"))
        End If

#If DEBUG Then
        Me.VALIDATE_DETERMINISTIC_TRANSITIONS()
        Me.VALIDATE_PROBABILISTIC_TRANSITIONS()
#End If

    End Sub

    Private Function ResolveDeterministicTransitions(ByVal analyzer As DTAnalyzer) As Boolean

        Dim HasChanges As Boolean = False
        Dim dt As DataTable = Me.GetData()

        For Index As Integer = dt.Rows.Count - 1 To 0 Step -1

            Dim dr As DataRow = dt.Rows(Index)

            If (dr.RowState = DataRowState.Deleted) Then
                Continue For
            End If

            Dim StratumIdSource As Nullable(Of Integer) = Nothing
            Dim StateClassIdSource As Integer = 0
            Dim StratumIdDest As Nullable(Of Integer) = Nothing
            Dim StateClassIdDest As Nullable(Of Integer) = Nothing

            DTAnalyzer.GetDTFieldValues(dr, StratumIdSource, StateClassIdSource, StratumIdDest, StateClassIdDest)

            If (Not StateClassIdDest.HasValue) Then
                Continue For
            End If

            If (Not analyzer.CanResolveStateClass(StratumIdSource, StratumIdDest, StateClassIdDest.Value)) Then

                dr(DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME) = DBNull.Value
                dr(DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME) = DBNull.Value

                HasChanges = True

            End If

        Next

        Return HasChanges

    End Function

    Private Function ResolveProbabilisticTransitions(ByVal analyzer As DTAnalyzer) As Boolean

        Dim HasChanges As Boolean = False
        Dim dt As DataTable = Me.GetDataSheet(DATASHEET_PT_NAME).GetData()

        For Index As Integer = dt.Rows.Count - 1 To 0 Step -1

            Dim dr As DataRow = dt.Rows(Index)

            If (dr.RowState = DataRowState.Deleted) Then
                Continue For
            End If

            Dim StratumIdSource As Nullable(Of Integer) = Nothing
            Dim StateClassIdSource As Integer = 0
            Dim StratumIdDest As Nullable(Of Integer) = Nothing
            Dim StateClassIdDest As Nullable(Of Integer) = Nothing

            DTAnalyzer.GetDTFieldValues(dr, StratumIdSource, StateClassIdSource, StratumIdDest, StateClassIdDest)

            If (Not analyzer.StateClassExists(StratumIdSource, StateClassIdSource)) Then

                DataTableUtilities.DeleteTableRow(dt, dr)
                HasChanges = True

                Continue For

            End If

            If (Not StateClassIdDest.HasValue) Then
                Continue For
            End If

            If (Not analyzer.CanResolveStateClass(StratumIdSource, StratumIdDest, StateClassIdDest.Value)) Then

                DataTableUtilities.DeleteTableRow(dt, dr)
                HasChanges = True

            End If

        Next

        Return HasChanges

    End Function

#If DEBUG Then

    Private Sub VALIDATE_DETERMINISTIC_TRANSITIONS()

        Dim dtdata As DataTable = Me.GetData()
        Dim Analyzer As New DTAnalyzer(dtdata, Me.Project)

        For Each dr As DataRow In dtdata.Rows

            If (dr.RowState = DataRowState.Deleted) Then
                Continue For
            End If

            Dim StratumIdSource As Nullable(Of Integer) = Nothing
            Dim StateClassIdSource As Integer = 0
            Dim StratumIdDest As Nullable(Of Integer) = Nothing
            Dim StateClassIdDest As Nullable(Of Integer) = Nothing

            DTAnalyzer.GetDTFieldValues(dr, StratumIdSource, StateClassIdSource, StratumIdDest, StateClassIdDest)

            If (StateClassIdDest.HasValue) Then
                Debug.Assert(Analyzer.CanResolveStateClass(StratumIdSource, StratumIdDest, StateClassIdDest.Value))
            End If

        Next

    End Sub

    Private Sub VALIDATE_PROBABILISTIC_TRANSITIONS()

        Dim Analyzer As New DTAnalyzer(Me.GetDataSheet(DATASHEET_DT_NAME).GetData(), Me.Project)
        Dim ptdata As DataTable = Me.GetDataSheet(DATASHEET_PT_NAME).GetData()

        For Each dr As DataRow In ptdata.Rows

            If (dr.RowState = DataRowState.Deleted) Then
                Continue For
            End If

            Dim StratumIdSource As Nullable(Of Integer) = Nothing
            Dim StateClassIdSource As Integer = 0
            Dim StratumIdDest As Nullable(Of Integer) = Nothing
            Dim StateClassIdDest As Nullable(Of Integer) = Nothing

            DTAnalyzer.GetPTFieldValues(dr, StratumIdSource, StateClassIdSource, StratumIdDest, StateClassIdDest)

            Debug.Assert(Analyzer.CanResolveStateClass(StratumIdSource, StratumIdSource, StateClassIdSource))

            If (StateClassIdDest.HasValue) Then
                Debug.Assert(Analyzer.CanResolveStateClass(StratumIdSource, StratumIdDest, StateClassIdDest.Value))
            End If

        Next

    End Sub

#End If

End Class
