'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Imports System.Text
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Globalization
Imports SyncroSim.Core
Imports SyncroSim.Core.Forms

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class DistributionValueDataFeedView

    Private m_Grid As BaseDataGridView
    Private m_TypeId As Nullable(Of Integer)

    Protected Overrides Sub InitializeView()

        MyBase.InitializeView()

        Dim v As DataFeedView = Me.Session.CreateMultiRowDataFeedView(Me.Scenario, Me.ControllingScenario)
        Me.PanelMain.Controls.Add(v)

        Dim ds As DataSheet = Me.Project.GetDataSheet(DISTRIBUTION_TYPE_DATASHEET_NAME)

        Try
            Me.m_TypeId = ds.ValidationTable.GetValue(DISTRIBUTION_TYPE_NAME_UNIFORM_INTEGER)
        Catch ex As Exception
            Debug.Assert(False)
        End Try

    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)

        If (disposing And Not Me.IsDisposed) Then

            If (Me.m_Grid IsNot Nothing) Then

                RemoveHandler Me.m_Grid.CellBeginEdit, AddressOf OnGridCellBeginEdit
                RemoveHandler Me.m_Grid.CellEndEdit, AddressOf OnGridCellEndEdit
                RemoveHandler Me.m_Grid.CellFormatting, AddressOf OnGridCellFormatting
                RemoveHandler Me.m_Grid.CellValueChanged, AddressOf OnGridCellValueChanged

            End If

            If components IsNot Nothing Then
                components.Dispose()
            End If

        End If

        MyBase.Dispose(disposing)

    End Sub

    Public Overrides Sub LoadDataFeed(dataFeed As Core.DataFeed)

        MyBase.LoadDataFeed(dataFeed)

        Dim v As MultiRowDataFeedView = CType(Me.PanelMain.Controls(0), MultiRowDataFeedView)
        Me.m_Grid = v.GridControl()
        v.LoadDataFeed(dataFeed, DISTRIBUTION_VALUE_DATASHEET_NAME)

        AddHandler Me.m_Grid.CellBeginEdit, AddressOf OnGridCellBeginEdit
        AddHandler Me.m_Grid.CellEndEdit, AddressOf OnGridCellEndEdit
        AddHandler Me.m_Grid.CellFormatting, AddressOf OnGridCellFormatting
        AddHandler Me.m_Grid.CellValueChanged, AddressOf OnGridCellValueChanged

    End Sub

    Public Overrides Sub EnableView(enable As Boolean)

        If (Me.PanelMain.Controls.Count > 0) Then

            Dim v As DataFeedView = CType(Me.PanelMain.Controls(0), DataFeedView)
            v.EnableView(enable)

        End If

    End Sub

    Private Sub OnGridCellBeginEdit(ByVal sender As Object, ByVal e As DataGridViewCellCancelEventArgs)

        If (e.ColumnIndex = Me.m_Grid.Columns(DISTRIBUTION_VALUE_DIST_TYPE_ID_COLUMN_NAME).Index) Then

            Dim dgv As DataGridViewRow = Me.m_Grid.Rows(e.RowIndex)
            Dim ds As DataSheet = Me.Project.GetDataSheet(DISTRIBUTION_TYPE_DATASHEET_NAME)
            Dim filter As String = CreateUserDistributionTypeFilter(Me.Project)
            Dim dv As New DataView(ds.GetData(), filter, ds.DisplayMember, DataViewRowState.CurrentRows)
            Dim Cell As DataGridViewComboBoxCell = CType(dgv.Cells(DISTRIBUTION_VALUE_DIST_TYPE_ID_COLUMN_NAME), DataGridViewComboBoxCell)

            Cell.DataSource = dv
            Cell.ValueMember = ds.ValueMember
            Cell.DisplayMember = ds.DisplayMember

        ElseIf (e.ColumnIndex = Me.m_Grid.Columns(DISTRIBUTION_VALUE_VALUE_DIST_TYPE_ID_COLUMN_NAME).Index) Then

            Dim dgv As DataGridViewRow = Me.m_Grid.Rows(e.RowIndex)
            Dim ds As DataSheet = Me.Project.GetDataSheet(DISTRIBUTION_TYPE_DATASHEET_NAME)
            Dim filter As String = CreateKnownDistributionTypeFilter(Me.Project)
            Dim dv As New DataView(ds.GetData(), filter, ds.DisplayMember, DataViewRowState.CurrentRows)
            Dim Cell As DataGridViewComboBoxCell = CType(dgv.Cells(DISTRIBUTION_VALUE_VALUE_DIST_TYPE_ID_COLUMN_NAME), DataGridViewComboBoxCell)

            Cell.DataSource = dv
            Cell.ValueMember = ds.ValueMember
            Cell.DisplayMember = ds.DisplayMember

        End If

    End Sub

    Private Sub OnGridCellEndEdit(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs)

        If (e.ColumnIndex = Me.m_Grid.Columns(DISTRIBUTION_VALUE_DIST_TYPE_ID_COLUMN_NAME).Index) Then

            Dim dgv As DataGridViewRow = Me.m_Grid.Rows(e.RowIndex)
            Dim Cell As DataGridViewComboBoxCell = CType(dgv.Cells(DISTRIBUTION_VALUE_DIST_TYPE_ID_COLUMN_NAME), DataGridViewComboBoxCell)
            Dim Column As DataGridViewComboBoxColumn = CType(Me.m_Grid.Columns(DISTRIBUTION_VALUE_DIST_TYPE_ID_COLUMN_NAME), DataGridViewComboBoxColumn)

            Cell.DataSource = Column.DataSource
            Cell.ValueMember = Column.ValueMember
            Cell.DisplayMember = Column.DisplayMember

        ElseIf (e.ColumnIndex = Me.m_Grid.Columns(DISTRIBUTION_VALUE_VALUE_DIST_TYPE_ID_COLUMN_NAME).Index) Then

            Dim dgv As DataGridViewRow = Me.m_Grid.Rows(e.RowIndex)
            Dim Cell As DataGridViewComboBoxCell = CType(dgv.Cells(DISTRIBUTION_VALUE_DIST_TYPE_ID_COLUMN_NAME), DataGridViewComboBoxCell)
            Dim Column As DataGridViewComboBoxColumn = CType(Me.m_Grid.Columns(DISTRIBUTION_VALUE_VALUE_DIST_TYPE_ID_COLUMN_NAME), DataGridViewComboBoxColumn)

            Cell.DataSource = Column.DataSource
            Cell.ValueMember = Column.ValueMember
            Cell.DisplayMember = Column.DisplayMember

        End If

    End Sub

    Private Sub OnGridCellFormatting(ByVal sender As Object, ByVal e As DataGridViewCellFormattingEventArgs)

        If (e.RowIndex < 0) Then
            Return
        End If

        'Is it an index we want?

        If (e.ColumnIndex <> Me.m_Grid.Columns(DISTRIBUTION_VALUE_VALUE_DIST_MIN_COLUMN_NAME).Index And
            e.ColumnIndex <> Me.m_Grid.Columns(DISTRIBUTION_VALUE_VALUE_DIST_MAX_COLUMN_NAME).Index) Then

            Return

        End If

        'Set the default
        e.CellStyle.Format = "0.0000"

        'Does the cell have a value?

        Dim TargetCell As DataGridViewCell = Me.m_Grid.Rows(e.RowIndex).Cells(e.ColumnIndex)

        If (TargetCell.Value Is Nothing Or TargetCell.Value Is DBNull.Value) Then
            Return
        End If

        'Is the distribution type 'Uniform Integer'?

        Dim TypeCell As DataGridViewCell = Me.m_Grid.Rows(e.RowIndex).Cells(DISTRIBUTION_VALUE_VALUE_DIST_TYPE_ID_COLUMN_NAME)

        If (TypeCell.Value Is Nothing Or TypeCell.Value Is DBNull.Value Or (Not Me.m_TypeId.HasValue)) Then
            Return
        End If

        Dim ValTypeId As Integer = CInt(TypeCell.Value)

        If (ValTypeId <> Me.m_TypeId.Value) Then
            Return
        End If

        e.CellStyle.Format = "0"

    End Sub

    Private Sub OnGridCellValueChanged(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs)

        If (e.ColumnIndex = Me.m_Grid.Columns(DISTRIBUTION_VALUE_VALUE_DIST_TYPE_ID_COLUMN_NAME).Index) Then
            Me.m_Grid.InvalidateRow(e.RowIndex)
        End If

    End Sub


    Private Shared Function CreateKnownDistributionTypeFilter(ByVal project As Project) As String

        Dim ids As List(Of Integer) = GetInternalDistributionTypeIds(project)
        Debug.Assert(ids.Count > 0)

        If (ids.Count = 0) Then
            Return Nothing
        Else

            Return String.Format(CultureInfo.InvariantCulture,
                "DistributionTypeID IN ({0})", CreateIntegerFilterSpec(ids))

        End If

    End Function

    Private Shared Function CreateUserDistributionTypeFilter(ByVal project As Project) As String

        Dim ids As List(Of Integer) = GetInternalDistributionTypeIds(project)

        If (ids.Count = 0) Then
            Return Nothing
        Else

            Return String.Format(CultureInfo.InvariantCulture,
                "DistributionTypeID NOT IN ({0})", CreateIntegerFilterSpec(ids))

        End If

    End Function

    Private Shared Function GetInternalDistributionTypeIds(ByVal project As Project) As List(Of Integer)

        Dim ids As New List(Of Integer)
        Dim ds As DataSheet = project.GetDataSheet(DISTRIBUTION_TYPE_DATASHEET_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            If (dr.RowState <> DataRowState.Deleted) Then

                If (dr(DISTRIBUTION_TYPE_IS_INTERNAL_COLUMN_NAME) IsNot DBNull.Value) Then

                    If (CInt(dr(DISTRIBUTION_TYPE_IS_INTERNAL_COLUMN_NAME)) = CInt(True)) Then
                        ids.Add(CInt(dr(ds.PrimaryKeyColumn.Name)))
                    End If

                End If

            End If

        Next

        Return ids

    End Function

    Private Shared Function CreateIntegerFilterSpec(ByVal ids As List(Of Integer)) As String

        Dim sb As New StringBuilder()

        For Each i As Integer In ids

            sb.Append(i.ToString(CultureInfo.InvariantCulture))
            sb.Append(",")

        Next

        Return sb.ToString.TrimEnd(CChar(","))

    End Function

End Class
