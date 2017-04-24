'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core
Imports System.Globalization
Imports System.Reflection

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class StateClassDataSheet
    Inherits DataSheet

    Private m_SlxSheet As DataSheet
    Private m_SlySheet As DataSheet
    Private m_IsDisposed As Boolean

    Protected Overrides Sub Dispose(disposing As Boolean)

        If (disposing And Not Me.m_IsDisposed) Then

            Me.RemoveHandlers()
            Me.m_IsDisposed = True

        End If

        MyBase.Dispose(disposing)

    End Sub

    Protected Overrides Sub OnDataFeedsRefreshed()

        MyBase.OnDataFeedsRefreshed()

        Me.RemoveHandlers()

        Me.m_SlxSheet = Me.Project.GetDataSheet(DATASHEET_STATE_LABEL_X_NAME)
        Me.m_SlySheet = Me.Project.GetDataSheet(DATASHEET_STATE_LABEL_Y_NAME)

        Me.AddHandlers()

    End Sub

    Private Sub AddHandlers()

        If (Me.m_SlxSheet IsNot Nothing) Then
            AddHandler Me.m_SlxSheet.RowsModified, AddressOf Me.OnSlxSlyRowsModified
        End If

        If (Me.m_SlySheet IsNot Nothing) Then
            AddHandler Me.m_SlySheet.RowsModified, AddressOf Me.OnSlxSlyRowsModified
        End If

    End Sub

    Private Sub RemoveHandlers()

        If (Me.m_SlxSheet IsNot Nothing) Then
            RemoveHandler Me.m_SlxSheet.RowsModified, AddressOf Me.OnSlxSlyRowsModified
        End If

        If (Me.m_SlySheet IsNot Nothing) Then
            RemoveHandler Me.m_SlySheet.RowsModified, AddressOf Me.OnSlxSlyRowsModified
        End If

    End Sub

    Private Sub OnSlxSlyRowsModified(ByVal sender As Object, ByVal e As DataSheetRowEventArgs)
        Me.UpdateNames()
    End Sub

    Protected Overrides Sub OnRowsAdded(sender As Object, e As DataSheetRowEventArgs)

        MyBase.OnRowsAdded(sender, e)
        Me.UpdateNames()

    End Sub

    Protected Overrides Sub OnRowsModified(sender As Object, e As DataSheetRowEventArgs)

        MyBase.OnRowsModified(sender, e)
        Me.UpdateNames()

    End Sub

    Protected Overrides Sub OnRowsDeleted(sender As Object, e As DataSheetRowEventArgs)

        MyBase.OnRowsDeleted(sender, e)
        Me.UpdateNames()

    End Sub

    Public Overrides Sub Validate(ByVal proposedRow As DataRow, transferMethod As DataTransferMethod)

        If (proposedRow(DATASHEET_STATECLASS_STATE_LABEL_X_ID_COLUMN_NAME) Is DBNull.Value Or
            proposedRow(DATASHEET_STATECLASS_STATE_LABEL_Y_ID_COLUMN_NAME) Is DBNull.Value) Then

            Dim msg As String = String.Format(CultureInfo.CurrentCulture, "You must choose both a '{0}' and a '{1}'.",
                Me.Columns(DATASHEET_STATECLASS_STATE_LABEL_X_ID_COLUMN_NAME).DisplayName,
                Me.Columns(DATASHEET_STATECLASS_STATE_LABEL_Y_ID_COLUMN_NAME).DisplayName)

            Throw New DataException(msg)

        End If

        Me.UpdateName(proposedRow)
        MyBase.Validate(proposedRow, transferMethod)

    End Sub

    Protected Overrides Sub BeforeImportData(ByVal proposedData As DataTable)

        MyBase.BeforeImportData(proposedData)

        For Each dr As DataRow In proposedData.Rows

            If (dr(DATASHEET_STATECLASS_STATE_LABEL_X_ID_COLUMN_NAME) Is DBNull.Value) Then

                ExceptionUtils.ThrowArgumentException("The data contains a NULL for '{0}'.",
                    Me.Columns(DATASHEET_STATECLASS_STATE_LABEL_X_ID_COLUMN_NAME).DisplayName)

            End If

            If (dr(DATASHEET_STATECLASS_STATE_LABEL_Y_ID_COLUMN_NAME) Is DBNull.Value) Then

                ExceptionUtils.ThrowArgumentException("The data contains a NULL for '{0}'.",
                    Me.Columns(DATASHEET_STATECLASS_STATE_LABEL_Y_ID_COLUMN_NAME).DisplayName)

            End If

            dr(DATASHEET_NAME_COLUMN_NAME) =
                CStr(dr(DATASHEET_STATECLASS_STATE_LABEL_X_ID_COLUMN_NAME)) &
                ":" &
                CStr(dr(DATASHEET_STATECLASS_STATE_LABEL_Y_ID_COLUMN_NAME))

        Next

    End Sub

    Private Sub UpdateNames()

        Dim dt As DataTable = Me.GetData()

        If (dt.Rows.Count > 0) Then

            For Each dr As DataRow In dt.Rows

                If (dr.RowState <> DataRowState.Deleted) Then
                    Me.UpdateName(dr)
                End If

            Next

            Me.Changes.Add(New ChangeRecord(Me, "Name Changed"))

        End If

    End Sub

    Private Sub UpdateName(ByVal dr As DataRow)

        Dim slxid As Integer = CInt(dr(DATASHEET_STATECLASS_STATE_LABEL_X_ID_COLUMN_NAME))
        Dim slyid As Integer = CInt(dr(DATASHEET_STATECLASS_STATE_LABEL_Y_ID_COLUMN_NAME))

        Dim slx As String = CStr(DataTableUtilities.GetTableValue(
                Me.m_SlxSheet.GetData(), Me.m_SlxSheet.ValueMember, slxid, DATASHEET_NAME_COLUMN_NAME))

        Dim sly As String = CStr(DataTableUtilities.GetTableValue(
                Me.m_SlySheet.GetData(), Me.m_SlySheet.ValueMember, slyid, DATASHEET_NAME_COLUMN_NAME))

        dr(DATASHEET_NAME_COLUMN_NAME) = slx & ":" & sly

    End Sub

End Class
