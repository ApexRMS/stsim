'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports System.IO
Imports System.Drawing
Imports System.Windows.Forms
Imports SyncroSim.Core
Imports SyncroSim.Core.Forms
Imports System.Reflection

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class SpatialMultiplierDataFeedView

    Private m_MultipliersView As DataFeedView
    Private m_MultipliersDataGrid As DataGridView
    Private Delegate Sub DelegateNoArgs()
    Private m_ColumnsInitialized As Boolean
    Private m_IsEnabled As Boolean = True

    Const BROWSE_BUTTON_TEXT As String = "..."
    Const FILE_NAME_COLUMN_INDEX As Integer = 4
    Const BROWSE_COLUMN_INDEX As Integer = 5

    Protected Overrides Sub InitializeView()

        MyBase.InitializeView()

        Me.m_MultipliersView = (Me.Session.CreateMultiRowDataFeedView(Me.Scenario, Me.ControllingScenario))
        Me.m_MultipliersDataGrid = CType(Me.m_MultipliersView, MultiRowDataFeedView).GridControl
        Me.PanelMultipliersGrid.Controls.Add(Me.m_MultipliersView)

    End Sub

    Public Overrides Sub EnableView(enable As Boolean)

        If (Me.PanelMultipliersGrid.Controls.Count > 0) Then

            Dim v As DataFeedView = CType(Me.PanelMultipliersGrid.Controls(0), DataFeedView)
            v.EnableView(enable)

        End If

        Me.m_IsEnabled = enable

    End Sub

    Public Overrides Sub LoadDataFeed(dataFeed As SyncroSim.Core.DataFeed)

        MyBase.LoadDataFeed(dataFeed)
        Me.m_MultipliersView.LoadDataFeed(dataFeed, DATASHEET_TRANSITION_SPATIAL_MULTIPLIER_NAME)

        If (Not Me.m_ColumnsInitialized) Then

            'Add handlers
            AddHandler Me.m_MultipliersDataGrid.CellEnter, AddressOf Me.OnGridCellEnter
            AddHandler Me.m_MultipliersDataGrid.CellMouseDown, AddressOf Me.OnGridCellMouseDown
            AddHandler Me.m_MultipliersDataGrid.DataBindingComplete, AddressOf Me.OnGridBindingComplete
            AddHandler Me.m_MultipliersDataGrid.KeyDown, AddressOf Me.OnGridKeyDown

            'Configure columns
            Me.m_MultipliersDataGrid.Columns(FILE_NAME_COLUMN_INDEX).DefaultCellStyle.BackColor = Color.LightGray

            'Add the browse button column
            Dim BrowseColumn As New DataGridViewButtonColumn()

            BrowseColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            BrowseColumn.Width = 40
            BrowseColumn.MinimumWidth = 40

            Me.m_MultipliersDataGrid.Columns.Add(BrowseColumn)

            Me.m_ColumnsInitialized = True

        End If

    End Sub

    Private Sub OnGridCellEnter(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs)

        If (e.ColumnIndex = FILE_NAME_COLUMN_INDEX) Then
            Me.Session.MainForm.BeginInvoke(New DelegateNoArgs(AddressOf Me.OnNewCellEnterAsync), Nothing)
        End If

    End Sub

    Private Sub OnGridCellMouseDown(ByVal sender As Object, ByVal e As DataGridViewCellMouseEventArgs)

        If (e.RowIndex >= 0) Then

            If (e.ColumnIndex = BROWSE_COLUMN_INDEX) Then
                Me.GetMultiplierFile(e.RowIndex)
            End If

        End If

    End Sub

    Private Sub OnGridKeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)

        If (Me.m_MultipliersDataGrid.CurrentCell IsNot Nothing) Then

            If (Me.m_MultipliersDataGrid.CurrentCell.ColumnIndex = BROWSE_COLUMN_INDEX) Then

                If (e.KeyValue = Keys.Enter) Then

                    Me.GetMultiplierFile(Me.m_MultipliersDataGrid.CurrentCell.RowIndex)
                    e.Handled = True

                End If

            End If

        End If

    End Sub

    Private Sub OnGridBindingComplete(sender As System.Object, e As System.Windows.Forms.DataGridViewBindingCompleteEventArgs)

        For Each dgr As DataGridViewRow In Me.m_MultipliersDataGrid.Rows
            dgr.Cells(BROWSE_COLUMN_INDEX).Value = BROWSE_BUTTON_TEXT
        Next

    End Sub

    Private Sub GetMultiplierFile(ByVal rowIndex As Integer)

        If (Not Me.m_IsEnabled) Then
            Return
        End If

        Dim ds As DataSheet = Me.Scenario.GetDataSheet(DATASHEET_TRANSITION_SPATIAL_MULTIPLIER_NAME)
        Dim RasterFile As String = ChooseRasterFileName("Transition Spatial Mulitplier Raster File", Me)

        If (RasterFile Is Nothing) Then
            Return
        End If

        Dim OldMode As DataGridViewEditMode = Me.m_MultipliersDataGrid.EditMode
        Me.m_MultipliersDataGrid.EditMode = DataGridViewEditMode.EditProgrammatically

        Me.m_MultipliersDataGrid.CurrentCell = Me.m_MultipliersDataGrid.Rows(rowIndex).Cells(FILE_NAME_COLUMN_INDEX)
        Me.m_MultipliersDataGrid.Rows(rowIndex).Cells(FILE_NAME_COLUMN_INDEX).Value = Path.GetFileName(RasterFile)
        Me.m_MultipliersDataGrid.NotifyCurrentCellDirty(True)

        Me.m_MultipliersDataGrid.BeginEdit(False)
        Me.m_MultipliersDataGrid.EndEdit()

        Me.m_MultipliersDataGrid.CurrentCell = Me.m_MultipliersDataGrid.Rows(rowIndex).Cells(BROWSE_COLUMN_INDEX)

        ds.AddExternalInputFile(RasterFile)

        Me.m_MultipliersDataGrid.EditMode = OldMode

    End Sub

    Private Sub OnNewCellEnterAsync()

        Dim Row As Integer = Me.m_MultipliersDataGrid.CurrentCell.RowIndex
        Dim Col As Integer = Me.m_MultipliersDataGrid.CurrentCell.ColumnIndex

        If (Col = FILE_NAME_COLUMN_INDEX) Then

            If (ModifierKeys = Keys.Shift) Then

                Col -= 1

                While (Not Me.m_MultipliersDataGrid.Columns(Col).Visible)
                    Col -= 1
                End While

            Else
                Col += 1
            End If

            Me.m_MultipliersDataGrid.CurrentCell = Me.m_MultipliersDataGrid.Rows(Row).Cells(Col)

        End If

    End Sub

End Class
