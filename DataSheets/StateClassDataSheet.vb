'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core
Imports System.Reflection
Imports System.Globalization

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class StateClassDataSheet
    Inherits DataSheet

    Private m_SlxSheet As DataSheet
    Private m_SlySheet As DataSheet

    Protected Overrides Sub OnDataFeedsRefreshed()

        MyBase.OnDataFeedsRefreshed()

        Me.m_SlxSheet = Me.Project.GetDataSheet(DATASHEET_STATE_LABEL_X_NAME)
        Me.m_SlySheet = Me.Project.GetDataSheet(DATASHEET_STATE_LABEL_Y_NAME)

    End Sub

    Public Overrides Sub Validate(proposedRow As DataRow, transferMethod As DataTransferMethod)

        Dim ResetNull As Boolean = False

        'It's not possible to have NULL for a display member, but we want the user to be able to
        'have this name be auto-generated.  To avoid a validation failure because of a NULL here
        'we are going to fix up the Name with a default value until the validation is done and then
        'set it back to NULL.  The OnRowsAdded() override will auto-generate the name.  Note that
        'this should only be done for detached rows (i.e. rows that are being editing by a control
        'such as the DataGridView...)

        If (proposedRow(DATASHEET_NAME_COLUMN_NAME) Is DBNull.Value And
            proposedRow.RowState = DataRowState.Detached) Then

            proposedRow(DATASHEET_NAME_COLUMN_NAME) = "__SYNCROSIM_TEMP_VALUE__"
            ResetNull = True

        End If

        Try
            MyBase.Validate(proposedRow, transferMethod)
        Finally

            If (ResetNull) Then
                proposedRow(DATASHEET_NAME_COLUMN_NAME) = DBNull.Value
            End If

        End Try

    End Sub

    Protected Overrides Sub OnRowsAdded(sender As Object, e As DataSheetRowEventArgs)

        MyBase.OnRowsAdded(sender, e)

        Dim d As New Dictionary(Of String, Boolean)

        'Create a dictionary of the existing names

        For Each dr As DataRow In Me.GetData().Rows

            If (dr.RowState = DataRowState.Deleted) Then
                Continue For
            End If

            If (dr(DATASHEET_NAME_COLUMN_NAME) IsNot DBNull.Value) Then
                d.Add(CStr(dr(DATASHEET_NAME_COLUMN_NAME)), True)
            End If

        Next

        'If any name is NULL then add the name, checking for duplicates as we go

        For Each dr As DataRow In Me.GetData().Rows

            If (dr.RowState = DataRowState.Deleted) Then
                Continue For
            End If

            If (dr(DATASHEET_NAME_COLUMN_NAME) Is DBNull.Value) Then

                Dim slxdata As DataTable = Me.m_SlxSheet.GetData()
                Dim slydata As DataTable = Me.m_SlySheet.GetData()
                Dim slxid As Integer = CInt(dr(DATASHEET_STATECLASS_STATE_LABEL_X_ID_COLUMN_NAME))
                Dim slyid As Integer = CInt(dr(DATASHEET_STATECLASS_STATE_LABEL_Y_ID_COLUMN_NAME))
                Dim slxname As String = CStr(DataTableUtilities.GetTableValue(slxdata, Me.m_SlxSheet.ValueMember, slxid, DATASHEET_NAME_COLUMN_NAME))
                Dim slyname As String = CStr(DataTableUtilities.GetTableValue(slydata, Me.m_SlySheet.ValueMember, slyid, DATASHEET_NAME_COLUMN_NAME))

                Dim n As String = GetNextName(slxname & ":" & slyname, d)
                dr(DATASHEET_NAME_COLUMN_NAME) = n

                d.Add(n, True)

            End If

        Next

    End Sub

    Protected Overrides Sub BeforeImportData(ByVal proposedData As DataTable)

        MyBase.BeforeImportData(proposedData)

        'We require the State Label X and Y values but they are not validated 
        'until after the call to BeforeImportData() so validate them now.

        For Each dr As DataRow In proposedData.Rows

            If (dr(DATASHEET_STATECLASS_STATE_LABEL_X_ID_COLUMN_NAME) Is DBNull.Value) Then

                ExceptionUtils.ThrowArgumentException("The data contains a NULL for '{0}'.",
                    Me.Columns(DATASHEET_STATECLASS_STATE_LABEL_X_ID_COLUMN_NAME).DisplayName)

            End If

            If (dr(DATASHEET_STATECLASS_STATE_LABEL_Y_ID_COLUMN_NAME) Is DBNull.Value) Then

                ExceptionUtils.ThrowArgumentException("The data contains a NULL for '{0}'.",
                    Me.Columns(DATASHEET_STATECLASS_STATE_LABEL_Y_ID_COLUMN_NAME).DisplayName)

            End If

        Next

        'Create a dictionary of the existing names

        Dim d As New Dictionary(Of String, Boolean)

        For Each dr As DataRow In proposedData.Rows

            If (dr(DATASHEET_NAME_COLUMN_NAME) IsNot DBNull.Value) Then
                d.Add(CStr(dr(DATASHEET_NAME_COLUMN_NAME)), True)
            End If

        Next

        'If any name is NULL then add the name, checking for duplicates as we go

        For Each dr As DataRow In proposedData.Rows

            If (dr(DATASHEET_NAME_COLUMN_NAME) Is DBNull.Value) Then

                Dim InitialName As String =
                    CStr(dr(DATASHEET_STATECLASS_STATE_LABEL_X_ID_COLUMN_NAME)) &
                    ":" &
                    CStr(dr(DATASHEET_STATECLASS_STATE_LABEL_Y_ID_COLUMN_NAME))

                Dim FinalName As String = GetNextName(InitialName, d)
                dr(DATASHEET_NAME_COLUMN_NAME) = FinalName

                d.Add(FinalName, True)

            End If

        Next

    End Sub

    Private Shared Function GetNextName(
        ByVal proposedName As String,
        ByVal existingNames As Dictionary(Of String, Boolean)) As String

        Debug.Assert(Not String.IsNullOrEmpty(proposedName))
        Dim NewProposedName As String = proposedName

        Dim NextNum As Integer = 1
        Dim NextName As String = NewProposedName

        While (existingNames.ContainsKey(NextName))

            NextNum += 1
            NextName = String.Format(CultureInfo.InvariantCulture, "{0} ({1})", NewProposedName, NextNum)
            Debug.Assert(NextNum < 500)

        End While

        Return NextName

    End Function

End Class
