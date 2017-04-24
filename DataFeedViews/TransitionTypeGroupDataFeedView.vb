'************************************************************************************
' StochasticTime: A .NET class library for developing stochastic time models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Imports SyncroSim.Core
Imports SyncroSim.Core.Forms
Imports System.Windows.Forms
Imports System.Reflection

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class TransitionTypeGroupDataFeedView

    Private m_Grid As BaseDataGridView

    Protected Overrides Sub InitializeView()

        MyBase.InitializeView()

        Dim v As DataFeedView = Me.Session.CreateMultiRowDataFeedView(Me.Project)
        Me.PanelMain.Controls.Add(v)

    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)

        If (disposing And Not Me.IsDisposed) Then

            If (Me.m_Grid IsNot Nothing) Then

                RemoveHandler Me.m_Grid.CellBeginEdit, AddressOf OnGridCellBeginEdit
                RemoveHandler Me.m_Grid.CellEndEdit, AddressOf OnGridCellEndEdit

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
        v.LoadDataFeed(dataFeed, DATASHEET_TRANSITION_TYPE_GROUP_NAME)

        AddHandler Me.m_Grid.CellBeginEdit, AddressOf OnGridCellBeginEdit
        AddHandler Me.m_Grid.CellEndEdit, AddressOf OnGridCellEndEdit

    End Sub

    Public Overrides Sub EnableView(enable As Boolean)

        If (Me.PanelMain.Controls.Count > 0) Then

            Dim v As DataFeedView = CType(Me.PanelMain.Controls(0), DataFeedView)
            v.EnableView(enable)

        End If

    End Sub

    Private Sub OnGridCellBeginEdit(ByVal sender As Object, ByVal e As DataGridViewCellCancelEventArgs)

        If (e.ColumnIndex = Me.m_Grid.Columns(DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME).Index) Then

            Dim dgv As DataGridViewRow = Me.m_Grid.Rows(e.RowIndex)
            Dim ds As DataSheet = Me.Project.GetDataSheet(DATASHEET_TRANSITION_GROUP_NAME)
            Dim filter As String = "IsAuto IS NULL OR IsAuto=0"
            Dim dv As New DataView(ds.GetData(), filter, ds.DisplayMember, DataViewRowState.CurrentRows)
            Dim Cell As DataGridViewComboBoxCell = CType(dgv.Cells(DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME), DataGridViewComboBoxCell)

            Cell.DataSource = dv
            Cell.ValueMember = ds.ValueMember
            Cell.DisplayMember = ds.DisplayMember

        End If

    End Sub

    Private Sub OnGridCellEndEdit(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs)

        If (e.ColumnIndex = Me.m_Grid.Columns(DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME).Index) Then

            Dim dgv As DataGridViewRow = Me.m_Grid.Rows(e.RowIndex)
            Dim Cell As DataGridViewComboBoxCell = CType(dgv.Cells(DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME), DataGridViewComboBoxCell)
            Dim Column As DataGridViewComboBoxColumn = CType(Me.m_Grid.Columns(DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME), DataGridViewComboBoxColumn)

            Cell.DataSource = Column.DataSource
            Cell.ValueMember = Column.ValueMember
            Cell.DisplayMember = Column.DisplayMember

        End If

    End Sub

End Class
