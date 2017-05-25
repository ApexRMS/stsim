'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core
Imports System.Reflection

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class AttributeGroupDataSheet
    Inherits DataSheet

    Public Overrides Sub DeleteRows(rows As IEnumerable(Of DataRow))

        Dim dssa As DataSheet = Me.Project.GetDataSheet(DATASHEET_STATE_ATTRIBUTE_TYPE_NAME)
        Dim dsta As DataSheet = Me.Project.GetDataSheet(DATASHEET_TRANSITION_ATTRIBUTE_TYPE_NAME)

        dssa.BeginModifyRows()
        dsta.BeginModifyRows()

        For Each ParentRow As DataRow In rows

            Dim GroupId As Integer = CInt(ParentRow(Me.PrimaryKeyColumn.Name))

            FixupChildReferences(dssa, GroupId)
            FixupChildReferences(dsta, GroupId)

        Next

        dssa.EndModifyRows()
        dsta.EndModifyRows()

    End Sub

    Private Shared Sub FixupChildReferences(ByVal dataSheet As DataSheet, ByVal groupId As Integer)

        Dim dt As DataTable = dataSheet.GetData()

        For Each dr As DataRow In dt.Rows()

            If (dr.RowState = DataRowState.Deleted) Then
                Continue For
            End If

            If (dr(DATASHEET_ATTRIBUTE_GROUP_ID_COLUMN_NAME) Is DBNull.Value) Then
                Continue For
            End If

            Dim id As Integer = CInt(dr(DATASHEET_ATTRIBUTE_GROUP_ID_COLUMN_NAME))

            If (id = groupId) Then
                dr(DATASHEET_ATTRIBUTE_GROUP_ID_COLUMN_NAME) = DBNull.Value
            End If

        Next

    End Sub

End Class
