'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports System.Text
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Globalization
Imports SyncroSim.Core
Imports SyncroSim.Core.Forms
Imports System.Reflection

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class StateClassQuickView

    Private m_StratumId As Nullable(Of Integer)
    Private m_StratumDataSheet As DataSheet
    Private m_StateClasses As List(Of Integer)
    Private m_DataFeed As DataFeed
    Private m_DTDataSheet As DataSheet
    Private m_DTView As MultiRowDataFeedView
    Private m_DTGrid As BaseDataGridView
    Private m_DTIterationVisible As Boolean
    Private m_DTTimestepVisible As Boolean
    Private m_DTStratumVisible As Boolean
    Private m_DTToStratumVisible As Boolean
    Private m_DTToClassVisible As Boolean
    Private m_DTAgeMinVisible As Boolean
    Private m_DTAgeMaxVisible As Boolean
    Private m_PTView As MultiRowDataFeedView
    Private m_PTGrid As BaseDataGridView
    Private m_PTIterationVisible As Boolean
    Private m_PTTimestepVisible As Boolean
    Private m_PTStratumVisible As Boolean
    Private m_PTToStratumVisible As Boolean
    Private m_PTToClassVisible As Boolean
    Private m_PTProportionVisible As Boolean
    Private m_PTAgeMinVisible As Boolean
    Private m_PTAgeMaxVisible As Boolean
    Private m_PTAgeRelativeVisible As Boolean
    Private m_PTAgeResetVisible As Boolean
    Private m_PTTstMinVisible As Boolean
    Private m_PTTstMaxVisible As Boolean
    Private m_PTTstRelativeVisible As Boolean
    Private m_ShowTransitionsFrom As Boolean = True
    Private m_ShowTransitionsTo As Boolean
    Private m_AddingDefaultValues As Boolean
    Private Delegate Sub DelegateNoArgs()
    Private m_Tag As String

    Public Sub LoadStateClasses(
        ByVal stratumId As Nullable(Of Integer),
        ByVal stateClasses As List(Of Integer),
        ByVal dataFeed As DataFeed,
        ByVal tag As String)

        Me.m_StratumId = stratumId
        Me.m_StratumDataSheet = Me.Project.GetDataSheet(DATASHEET_STRATA_NAME)
        Me.m_StateClasses = stateClasses
        Me.m_DataFeed = dataFeed
        Me.m_Tag = tag

        Dim sess As WinFormSession = CType(Me.Project.Library.Session, WinFormSession)

        Me.m_DTView = CType(sess.CreateMultiRowDataFeedView(dataFeed.Scenario, dataFeed.Scenario), MultiRowDataFeedView)
        Me.m_DTView.LoadDataFeed(Me.m_DataFeed, DATASHEET_DT_NAME)
        Me.m_DTDataSheet = Me.m_DataFeed.GetDataSheet(DATASHEET_DT_NAME)
        Me.m_DTGrid = Me.m_DTView.GridControl()
        Me.PanelDeterministic.Controls.Add(Me.m_DTView)

        Me.m_PTView = CType(sess.CreateMultiRowDataFeedView(dataFeed.Scenario, dataFeed.Scenario), MultiRowDataFeedView)
        Me.m_PTView.LoadDataFeed(Me.m_DataFeed, DATASHEET_PT_NAME)
        Me.m_PTGrid = Me.m_PTView.GridControl()
        Me.PanelProbabilistic.Controls.Add(Me.m_PTView)

        Me.m_DTGrid.RowHeadersVisible = False
        Me.m_DTGrid.PaintGridBorders = False
        Me.m_DTView.ManageOptionalColumns = False

        Me.m_PTGrid.PaintGridBorders = False
        Me.m_PTView.ManageOptionalColumns = False

        Me.FilterDeterministicTransitions()
        Me.FilterProbabilisticTransitions()

        AddHandler Me.m_DTGrid.CellValueChanged, AddressOf OnDeterministicCellValueChanged
        AddHandler Me.m_DTGrid.CellBeginEdit, AddressOf OnDeterministicCellBeginEdit
        AddHandler Me.m_DTGrid.CellEndEdit, AddressOf OnDeterministicCellEndEdit
        AddHandler Me.m_PTGrid.CellValueChanged, AddressOf OnProbabilisticCellValueChanged
        AddHandler Me.m_PTGrid.CellBeginEdit, AddressOf OnProbabilisticCellBeginEdit
        AddHandler Me.m_PTGrid.CellEndEdit, AddressOf OnProbabilisticCellEndEdit
        AddHandler Me.m_PTGrid.DefaultValuesNeeded, AddressOf OnProbabilisticOnDefaultValuesNeeded
        AddHandler Me.m_PTGrid.PressingCmdKey, AddressOf OnBeforePTGridCmdKey
        AddHandler Me.m_StratumDataSheet.RowsDeleted, AddressOf OnStratumDeleted

        Me.ConfigureContextMenus()
        Me.InitializeColumnVisiblity()
        Me.UpdateDTColumnVisibility()
        Me.UpdatePTColumnVisibility()
        Me.ConfigureColumnsReadOnly()

        'Mysteriously, if we set AllowUserToAddRows in this function it crashes and I'm not sure why.  However, 
        'The problem goes away if we set it asynchronously so that's what we are doing here.

        Me.Session.MainForm.BeginInvoke(New DelegateNoArgs(AddressOf GridConfigAsyncTarget), Nothing)

    End Sub

    ''' <summary>
    ''' Async Target for grid configuration
    ''' </summary>
    ''' <param name="arg"></param>
    ''' <remarks></remarks>
    Private Sub GridConfigAsyncTarget()
        Me.m_DTGrid.AllowUserToAddRows = False
    End Sub

    ''' <summary>
    ''' Overrides Dispose
    ''' </summary>
    ''' <param name="disposing"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)

        If (disposing And Not Me.IsDisposed) Then

            RemoveHandler Me.m_DTGrid.CellValueChanged, AddressOf OnDeterministicCellValueChanged
            RemoveHandler Me.m_DTGrid.CellBeginEdit, AddressOf OnDeterministicCellBeginEdit
            RemoveHandler Me.m_DTGrid.CellEndEdit, AddressOf OnDeterministicCellEndEdit
            RemoveHandler Me.m_PTGrid.CellValueChanged, AddressOf OnProbabilisticCellValueChanged
            RemoveHandler Me.m_PTGrid.CellBeginEdit, AddressOf OnProbabilisticCellBeginEdit
            RemoveHandler Me.m_PTGrid.CellEndEdit, AddressOf OnProbabilisticCellEndEdit
            RemoveHandler Me.m_PTGrid.DefaultValuesNeeded, AddressOf OnProbabilisticOnDefaultValuesNeeded
            RemoveHandler Me.m_PTGrid.PressingCmdKey, AddressOf OnBeforePTGridCmdKey
            RemoveHandler Me.m_StratumDataSheet.RowsDeleted, AddressOf OnStratumDeleted

            If components IsNot Nothing Then
                components.Dispose()
            End If

        End If

        MyBase.Dispose(disposing)

    End Sub

    ''' <summary>
    ''' OnStratumDeleted
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' If the stratum data feed is deleting our stratum then we need to close.  Note that follwing the call
    ''' to CloseHostedView, this controls containing form will be gone...
    ''' </remarks>
    Private Sub OnStratumDeleted(sender As Object, e As SyncroSim.Core.DataSheetRowEventArgs)

        Dim found As Boolean = False

        For Each dr As DataRow In Me.m_StratumDataSheet.ValidationTable.DataSource.Rows

            If (dr.RowState = DataRowState.Deleted) Then
                Continue For
            End If

            If (CInt(dr(Me.m_StratumDataSheet.ValueMember)) = Me.m_StratumId) Then

                found = True
                Exit For

            End If

        Next

        If (Not found) Then

            Dim sess As WinFormSession = CType(Me.Project.Session, WinFormSession)
            sess.Application.CloseView(Me.m_Tag)

            Return

        End If

    End Sub

    ''' <summary>
    ''' Configures the grids' context menus
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConfigureContextMenus()

        'Terminology
        Dim psl As String = Nothing
        Dim ssl As String = Nothing
        Dim dsterm As DataSheet = Me.m_DataFeed.Project.GetDataSheet(DATASHEET_TERMINOLOGY_NAME)

        GetStratumLabelTerminology(dsterm, psl, ssl)

        'Deterministic
        Me.m_DTGrid.ContextMenuStrip = Me.ContextMenuStripDeterministic
        Me.MenuItemTransitionsToDeterministic.Checked = Me.m_ShowTransitionsTo
        Me.MenuItemTransitionsFromDeterministic.Checked = Me.m_ShowTransitionsFrom
        Me.MenuItemStratumDeterministic.Text = psl
        Me.MenuItemToStratumDeterministic.Text = "To " & psl

        'Probabilistic
        For i As Integer = Me.m_PTView.Commands.Count - 1 To 0 Step -1

            Dim c As Command = Me.m_PTView.Commands(i)

            If (c.Name = "ssim_delete_all" Or
                c.Name = "ssim_import" Or
                c.Name = "ssim_export" Or
                c.Name = "ssim_export_all") Then

                Me.m_PTView.Commands.RemoveAt(i)

            End If

        Next

        Me.m_PTView.Commands.Add(New Command("Transitions To", AddressOf OnExecuteProbabilisticTranstionsToCommand, AddressOf OnUpdateProbabilisticTranstionsToCommand))
        Me.m_PTView.Commands.Add(New Command("Transitions From", AddressOf OnExecuteProbabilisticTranstionsFromCommand, AddressOf OnUpdateProbabilisticTranstionsFromCommand))
        Me.m_PTView.Commands.Add(Command.CreateSeparatorCommand())
        Me.m_PTView.Commands.Add(New Command("Iteration", AddressOf OnExecuteProbabilisticIterationCommand, AddressOf OnUpdateProbabilisticIterationCommand))
        Me.m_PTView.Commands.Add(New Command("Timestep", AddressOf OnExecuteProbabilisticTimestepCommand, AddressOf OnUpdateProbabilisticTimestepCommand))
        Me.m_PTView.Commands.Add(New Command(psl, AddressOf OnExecuteProbabilisticStratumCommand, AddressOf OnUpdateProbabilisticStratumCommand))
        Me.m_PTView.Commands.Add(New Command("To " & psl, AddressOf OnExecuteProbabilisticToStratumCommand, AddressOf OnUpdateProbabilisticToStratumCommand))
        Me.m_PTView.Commands.Add(New Command("To Class", AddressOf OnExecuteProbabilisticToClassCommand, AddressOf OnUpdateProbabilisticToClassCommand))
        Me.m_PTView.Commands.Add(New Command("Proportion", AddressOf OnExecuteProbabilisticProportionCommand, AddressOf OnUpdateProbabilisticProportionCommand))
        Me.m_PTView.Commands.Add(New Command("Age Min", AddressOf OnExecuteProbabilisticAgeMinCommand, AddressOf OnUpdateProbabilisticAgeMinCommand))
        Me.m_PTView.Commands.Add(New Command("Age Max", AddressOf OnExecuteProbabilisticAgeMaxCommand, AddressOf OnUpdateProbabilisticAgeMaxCommand))
        Me.m_PTView.Commands.Add(New Command("Age Shift", AddressOf OnExecuteProbabilisticAgeRelativeCommand, AddressOf OnUpdateProbabilisticAgeRelativeCommand))
        Me.m_PTView.Commands.Add(New Command("Age Reset", AddressOf OnExecuteProbabilisticAgeResetCommand, AddressOf OnUpdateProbabilisticAgeResetCommand))
        Me.m_PTView.Commands.Add(New Command("TST Min", AddressOf OnExecuteProbabilisticTstMinCommand, AddressOf OnUpdateProbabilisticTstMinCommand))
        Me.m_PTView.Commands.Add(New Command("TST Max", AddressOf OnExecuteProbabilisticTstMaxCommand, AddressOf OnUpdateProbabilisticTstMaxCommand))
        Me.m_PTView.Commands.Add(New Command("TST Shift", AddressOf OnExecuteProbabilisticTstRelativeCommand, AddressOf OnUpdateProbabilisticTstRelativeCommand))

        Me.m_PTView.RefreshContextMenuStrip()

        'Remove Optional menu items
        For i As Integer = Me.m_PTGrid.ContextMenuStrip.Items.Count - 1 To 0 Step -1

            Dim item As ToolStripItem = Me.m_PTGrid.ContextMenuStrip.Items(i)

            If (item.Name = "ssim_optional_column_separator" Or
                item.Name = "ssim_optional_column_item") Then

                Me.m_PTGrid.ContextMenuStrip.Items.RemoveAt(i)

            End If

        Next

    End Sub

    ''' <summary>
    ''' Creates the filter string for the grids
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateGridFilterString() As String

        Dim SelectedClassesFilter As String = CreateIntegerFilterSpec(Me.m_StateClasses)

        Dim FromFormatString As String = Nothing
        Dim ToFormatString As String = Nothing

        If (Me.m_StratumId.HasValue) Then

            FromFormatString = "StratumIDSource={0} AND StateClassIDSource IN ({1})"
            ToFormatString = "(StratumIDDest={0} AND StateClassIDDest IN ({1})) OR (StratumIDSource={0} AND StratumIDDest IS NULL AND StateClassIDDest IN ({1}))"

        Else

            FromFormatString = "StratumIDSource IS NULL AND StateClassIDSource IN ({1})"
            ToFormatString = "(StratumIDSource IS NULL AND StateClassIDDest IN ({1}))"

        End If

        If (Me.m_ShowTransitionsFrom) Then
            Return String.Format(CultureInfo.InvariantCulture, FromFormatString, Me.m_StratumId, SelectedClassesFilter)
        Else
            Debug.Assert(Me.m_ShowTransitionsTo)
            Return String.Format(CultureInfo.InvariantCulture, ToFormatString, Me.m_StratumId, SelectedClassesFilter)
        End If

    End Function

    ''' <summary>
    ''' Filters the deterministic transitions
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FilterDeterministicTransitions()

        Dim filter As String = Me.CreateGridFilterString()
        CType(Me.m_DTGrid.DataSource, BindingSource).Filter = filter

    End Sub

    ''' <summary>
    ''' Filters the probabilistic transitions
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FilterProbabilisticTransitions()

        Dim filter As String = Me.CreateGridFilterString()
        CType(Me.m_PTGrid.DataSource, BindingSource).Filter = filter

    End Sub

    ''' <summary>
    ''' Handles the CellValueChanged event from the deterministic transitions data grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OnDeterministicCellValueChanged(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs)

        If (e.ColumnIndex = Me.m_DTGrid.Columns(DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME).Index) Then

            Me.SetNewDestinationStateClassCellValue(
                DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME,
                DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME,
                DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME,
                DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME,
                Me.m_DTGrid,
                e.RowIndex)

        End If

    End Sub

    ''' <summary>
    ''' Handles the CellBeginEdit event for the deterministic transitions grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OnDeterministicCellBeginEdit(ByVal sender As Object, ByVal e As DataGridViewCellCancelEventArgs)

        If (e.ColumnIndex = Me.m_DTGrid.Columns(DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME).Index) Then

            Me.FilterStateClassCombo(
                DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME,
                DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME,
                DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME,
                Me.m_DTGrid,
                e.RowIndex,
                False)

        End If

    End Sub

    ''' <summary>
    ''' Handles the CellEndEdit event for the deterministic transitions grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OnDeterministicCellEndEdit(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs)

        If (e.ColumnIndex = Me.m_DTGrid.Columns(DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME).Index) Then
            ResetComboBoxRowFilter(e.RowIndex, DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME, Me.m_DTGrid)
        ElseIf (e.ColumnIndex = Me.m_DTGrid.Columns(DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME).Index) Then
            ResetComboBoxRowFilter(e.RowIndex, DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME, Me.m_DTGrid)
        End If

    End Sub

    ''' <summary>
    ''' Handles the CellValueChanged event from the probabilistic transitions grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OnProbabilisticCellValueChanged(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs)

        If (Me.m_AddingDefaultValues) Then
            Return
        End If

        If (e.ColumnIndex = Me.m_PTGrid.Columns(DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME).Index) Then

            Me.SetNewDestinationStateClassCellValue(
                DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME,
                DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME,
                DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME,
                DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME,
                Me.m_PTGrid,
                e.RowIndex)

        End If

    End Sub

    ''' <summary>
    ''' Handles the CellBeginEdit for the probabilistic transitions grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OnProbabilisticCellBeginEdit(ByVal sender As Object, ByVal e As DataGridViewCellCancelEventArgs)

        If (e.ColumnIndex = Me.m_PTGrid.Columns(DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME).Index) Then

            Me.FilterStateClassCombo(
                DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME,
                DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME,
                DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME,
                Me.m_PTGrid,
                e.RowIndex,
                Me.m_ShowTransitionsFrom)

        ElseIf (e.ColumnIndex = Me.m_PTGrid.Columns(DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME).Index) Then

            Me.FilterStateClassCombo(
                DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME,
                DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME,
                DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME,
                Me.m_PTGrid,
                e.RowIndex,
                False)

        End If

    End Sub

    ''' <summary>
    ''' Handles the CellEndEdit event for the probabilistic transitions grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OnProbabilisticCellEndEdit(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs)

        If (e.ColumnIndex = Me.m_PTGrid.Columns(DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME).Index) Then
            ResetComboBoxRowFilter(e.RowIndex, DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME, Me.m_PTGrid)
        ElseIf (e.ColumnIndex = Me.m_PTGrid.Columns(DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME).Index) Then
            ResetComboBoxRowFilter(e.RowIndex, DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME, Me.m_PTGrid)
        ElseIf (e.ColumnIndex = Me.m_PTGrid.Columns(DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME).Index) Then
            ResetComboBoxRowFilter(e.RowIndex, DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME, Me.m_PTGrid)
        ElseIf (e.ColumnIndex = Me.m_PTGrid.Columns(DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME).Index) Then
            ResetComboBoxRowFilter(e.RowIndex, DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME, Me.m_PTGrid)
        End If

    End Sub

    ''' <summary>
    ''' Handles the default values changed event for the probabilistic transitions grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OnProbabilisticOnDefaultValuesNeeded(ByVal sender As Object, ByVal e As DataGridViewRowEventArgs)

        Dim row As DataGridViewRow = e.Row

        Me.m_AddingDefaultValues = True

        row.Cells(DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME).Value = DataTableUtilities.GetNullableDatabaseValue(Me.m_StratumId)
        row.Cells(DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME).Value = Me.m_StateClasses(0)
        row.Cells(DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME).Value = Me.m_StateClasses(0)

        Me.m_AddingDefaultValues = False

    End Sub

    ''' <summary>
    ''' Handler for base grid custom OnBeforeCmdKey event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' When the user clicks on the 'new row' they get a bunch of default values which we supply
    ''' in response to the DefaultValuesNeeded event.  This works well unless:
    ''' 
    ''' (a.) there is only one row
    ''' (b.) that row is the 'new row'
    ''' (c.) the user hits the ESC key.  
    ''' 
    ''' Doing this will clear the default values, but the 'new' row will still be the current row and
    ''' the DefaultValuesNeeded event will not be raised again.  This crashes eventually because we expect 
    ''' those default values to be there. 
    ''' 
    ''' This seems like a bug in the DataGridView to me - why does hitting ESC not completely cancel the new row
    ''' editing session instead of just removing the defaults?
    ''' 
    ''' To make the grid think that it is starting the editing session over, we detect this condition and set 
    ''' the focus to the parent control.  This makes things work and the user will probably not notice that 
    ''' the current cell gets reset...
    ''' </remarks>
    Private Sub OnBeforePTGridCmdKey(ByVal sender As Object, ByVal e As BaseGridCmdKeyInfoEventArgs)

        If (Me.m_PTGrid.CurrentRow Is Nothing) Then
            Return
        End If

        If (e.KeyData = Keys.Escape) Then

            If (Me.m_PTGrid.CurrentRow.IsNewRow) Then

                If (Me.m_PTGrid.Rows.Count = 1) Then
                    Me.PanelProbabilistic.Focus()
                End If

            End If

        End If

    End Sub

    ''' <summary>
    ''' Resets the row filter for the combo box cell at the specified row and column
    ''' </summary>
    ''' <param name="rowIndex"></param>
    ''' <param name="columnName"></param>
    ''' <remarks></remarks>
    Private Shared Sub ResetComboBoxRowFilter(
        ByVal rowIndex As Integer,
        ByVal columnName As String,
        ByVal grid As DataGridView)

        Dim dgv As DataGridViewRow = grid.Rows(rowIndex)
        Dim DestStratumCell As DataGridViewComboBoxCell = CType(dgv.Cells(columnName), DataGridViewComboBoxCell)
        Dim DestStratumColumn As DataGridViewComboBoxColumn = CType(grid.Columns(columnName), DataGridViewComboBoxColumn)

        DestStratumCell.DataSource = DestStratumColumn.DataSource
        DestStratumCell.ValueMember = DestStratumColumn.ValueMember
        DestStratumCell.DisplayMember = DestStratumColumn.DisplayMember

    End Sub

    ''' <summary>
    ''' Gets the combo box cell for the specified column
    ''' </summary>
    ''' <param name="columnName"></param>
    ''' <param name="grid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetComboCell(
        ByVal columnName As String,
        ByVal grid As DataGridView,
        ByVal rowIndex As Integer) As DataGridViewComboBoxCell

        Dim dgv As DataGridViewRow = grid.Rows(rowIndex)
        Return CType(dgv.Cells(columnName), DataGridViewComboBoxCell)

    End Function

    ''' <summary>
    ''' Gets the correct stratum Id from the specified set of source and destination columns
    ''' </summary>
    ''' <param name="sourceStratumColumnName"></param>
    ''' <param name="destStratumColumnName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDestStratumIdFromGridValues(
        ByVal sourceStratumColumnName As String,
        ByVal destStratumColumnName As String,
        ByVal grid As DataGridView,
        ByVal rowIndex As Integer) As Nullable(Of Integer)

        Dim dgv As DataGridViewRow = grid.Rows(rowIndex)
        Dim SourceStratumCell As DataGridViewComboBoxCell = CType(dgv.Cells(sourceStratumColumnName), DataGridViewComboBoxCell)
        Dim DestStratumCell As DataGridViewComboBoxCell = CType(dgv.Cells(destStratumColumnName), DataGridViewComboBoxCell)

        Debug.Assert(DestStratumCell.Value IsNot Nothing)
        Debug.Assert(SourceStratumCell.Value IsNot Nothing)

        If (DestStratumCell.Value IsNot DBNull.Value) Then
            Return CInt(DestStratumCell.Value)
        End If

        If (SourceStratumCell.Value IsNot DBNull.Value) Then

            Debug.Assert(Me.m_StratumId.HasValue)
            Debug.Assert(CInt(SourceStratumCell.Value) = Me.m_StratumId.Value)

            Return Me.m_StratumId

        Else

            Debug.Assert(Not Me.m_StratumId.HasValue)
            Return Nothing

        End If

    End Function

    ''' <summary>
    ''' Creates a comma separated filter specification for the specified list of integers
    ''' </summary>
    ''' <param name="stateClassIDList"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function CreateIntegerFilterSpec(ByVal stateClassIDList As List(Of Integer)) As String

        Dim sb As New StringBuilder()

        For Each i As Integer In stateClassIDList
            sb.Append(i.ToString(CultureInfo.InvariantCulture))
            sb.Append(",")
        Next

        Return sb.ToString.TrimEnd(CChar(","))

    End Function

    ''' <summary>
    ''' Creates a filter for all state classes in the specified stratum
    ''' </summary>
    ''' <param name="sourceStratumId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateStateClassIdSourceFilter(ByVal sourceStratumId As Nullable(Of Integer)) As String

        Dim lst As New List(Of Integer)
        Dim query As String = Nothing

        If (sourceStratumId.HasValue) Then

            query = String.Format(CultureInfo.InvariantCulture,
                "StratumIDSource={0} OR (StratumIDSource IS NULL)", sourceStratumId.Value)

        Else
            query = "StratumIDSource IS NULL"
        End If

        Dim dt As DataTable = Me.m_DTDataSheet.GetData()
        Dim rows() As DataRow = dt.Select(query, Nothing, DataViewRowState.CurrentRows)

        For Each dr As DataRow In rows

            Dim id As Integer = CInt(dr("StateClassIDSource"))

            If (Not lst.Contains(id)) Then
                lst.Add(id)
            End If

        Next

        If (lst.Count = 0) Then
            Return "StateClassID=-1"
        Else
            Dim filter As String = CreateIntegerFilterSpec(lst)
            Return String.Format(CultureInfo.InvariantCulture, "StateClassID IN ({0})", filter)
        End If

    End Function

    ''' <summary>
    ''' Determines whether the specified view contains the specified state class Id
    ''' </summary>
    ''' <param name="dv"></param>
    ''' <param name="stateClassId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function ViewContainsStateClass(ByVal dv As DataView, ByVal stateClassId As Integer) As Boolean

        For Each drv As DataRowView In dv

            If (CInt(drv.Row("StateClassID")) = stateClassId) Then
                Return True
            End If

        Next

        Return False

    End Function

    ''' <summary>
    ''' Creates a state class dataview filtered by the specified stratum Id
    ''' </summary>
    ''' <param name="stratumID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateNewFilteredStateClassView(ByVal stratumId As Nullable(Of Integer)) As DataView

        Dim filter As String = Me.CreateStateClassIdSourceFilter(stratumId)
        Dim ds As DataSheet = Me.m_DataFeed.Project.GetDataSheet(DATASHEET_STATECLASS_NAME)

        Return New DataView(ds.GetData(), filter, ds.DisplayMember, DataViewRowState.CurrentRows)

    End Function

    ''' <summary>
    ''' Sets a new destination state class value
    ''' </summary>
    ''' <param name="sourceStratumColumnName"></param>
    ''' <param name="sourceStateClassColumnName"></param>
    ''' <param name="destStratumColumnName"></param>
    ''' <param name="destStateClassColumnName"></param>
    ''' <param name="grid"></param>
    ''' <param name="rowIndex"></param>
    ''' <remarks></remarks>
    Private Sub SetNewDestinationStateClassCellValue(
        ByVal sourceStratumColumnName As String,
        ByVal sourceStateClassColumnName As String,
        ByVal destStratumColumnName As String,
        ByVal destStateClassColumnName As String,
        ByVal grid As DataGridView,
        ByVal rowIndex As Integer)

        Dim SourceStateClassCell As DataGridViewComboBoxCell = GetComboCell(sourceStateClassColumnName, grid, rowIndex)
        Dim DestStateClassCell As DataGridViewComboBoxCell = GetComboCell(destStateClassColumnName, grid, rowIndex)
        Dim DestStratumId As Nullable(Of Integer) = Me.GetDestStratumIdFromGridValues(sourceStratumColumnName, destStratumColumnName, grid, rowIndex)
        Dim DestStateClassId As Nullable(Of Integer) = Nothing
        Dim CurrentDestStateClassId As Integer = CInt(SourceStateClassCell.Value)
        Dim dv As DataView = Me.CreateNewFilteredStateClassView(DestStratumId)

        If (DestStateClassCell.Value Is DBNull.Value) Then
            DestStateClassId = CInt(SourceStateClassCell.Value)
        Else
            DestStateClassId = CInt(DestStateClassCell.Value)
        End If

        Debug.Assert(CurrentDestStateClassId > 0)

        'If the new stratum has the current destination state class Id then we can use
        'that one.  Otherwise use the first one found.

        If (ViewContainsStateClass(dv, CurrentDestStateClassId)) Then
            DestStateClassCell.Value = CurrentDestStateClassId
        Else

            If (dv.Count > 0) Then

                Dim drvzero As DataRowView = dv(0)
                DestStateClassCell.Value = CInt(drvzero.Row("StateClassID"))

            End If

        End If

    End Sub

    ''' <summary>
    ''' Filters the state class combo for the specified column
    ''' </summary>
    ''' <param name="sourceStratumColumnName"></param>
    ''' <param name="destStratumColumnName"></param>
    ''' <param name="stateClassColumnName"></param>
    ''' <param name="grid"></param>
    ''' <param name="rowIndex"></param>
    ''' <param name="selectedClassesOnly"></param>
    ''' <remarks>
    ''' When the destination state class editing control is shown we want to filter the state classes for the destination stratum
    ''' </remarks>
    Private Sub FilterStateClassCombo(
        ByVal sourceStratumColumnName As String,
        ByVal destStratumColumnName As String,
        ByVal stateClassColumnName As String,
        ByVal grid As DataGridView,
        ByVal rowIndex As Integer,
        ByVal selectedClassesOnly As Boolean)

        Dim ds As DataSheet = Me.m_DataFeed.Project.GetDataSheet(DATASHEET_STATECLASS_NAME)
        Dim StateClassComboCell As DataGridViewComboBoxCell = GetComboCell(stateClassColumnName, grid, rowIndex)
        Dim TargetStratumId As Nullable(Of Integer) = Me.GetDestStratumIdFromGridValues(sourceStratumColumnName, destStratumColumnName, grid, rowIndex)
        Dim filter As String

        If (selectedClassesOnly) Then
            filter = String.Format(CultureInfo.InvariantCulture, "StateClassID IN ({0})", CreateIntegerFilterSpec(Me.m_StateClasses))
        Else
            filter = Me.CreateStateClassIdSourceFilter(TargetStratumId)
        End If

        Dim dv As New DataView(ds.GetData(), filter, ds.DisplayMember, DataViewRowState.CurrentRows)

        StateClassComboCell.DataSource = dv
        StateClassComboCell.ValueMember = "StateClassID"
        StateClassComboCell.DisplayMember = "Name"

    End Sub

    ''' <summary>
    ''' Determines whether the specified column contains any data
    ''' </summary>
    ''' <param name="columnName"></param>
    ''' <param name="grid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function ColumnContainsData(ByVal columnName As String, ByVal grid As DataGridView) As Boolean

        For Each dgr As DataGridViewRow In grid.Rows

            Dim v As Object = dgr.Cells(columnName).Value

            If (v IsNot DBNull.Value And v IsNot Nothing) Then
                Return True
            End If

        Next

        Return False

    End Function

    ''' <summary>
    ''' Initializes the visibility flags for each column
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeColumnVisiblity()

        'Deterministic
        Me.m_DTIterationVisible = ColumnContainsData(DATASHEET_ITERATION_COLUMN_NAME, Me.m_DTGrid)
        Me.m_DTTimestepVisible = ColumnContainsData(DATASHEET_TIMESTEP_COLUMN_NAME, Me.m_DTGrid)
        Me.m_DTStratumVisible = False
        Me.m_DTToStratumVisible = ColumnContainsData(DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME, Me.m_DTGrid)
        Me.m_DTToClassVisible = ColumnContainsData(DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME, Me.m_DTGrid)
        Me.m_DTAgeMinVisible = ColumnContainsData(DATASHEET_AGE_MIN_COLUMN_NAME, Me.m_DTGrid)
        Me.m_DTAgeMaxVisible = ColumnContainsData(DATASHEET_AGE_MAX_COLUMN_NAME, Me.m_DTGrid)

        'Probabilistic
        Me.m_PTIterationVisible = ColumnContainsData(DATASHEET_ITERATION_COLUMN_NAME, Me.m_PTGrid)
        Me.m_PTTimestepVisible = ColumnContainsData(DATASHEET_TIMESTEP_COLUMN_NAME, Me.m_PTGrid)
        Me.m_PTStratumVisible = False
        Me.m_PTToStratumVisible = ColumnContainsData(DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME, Me.m_PTGrid)
        Me.m_PTToClassVisible = ColumnContainsData(DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME, Me.m_PTGrid)
        Me.m_PTAgeMinVisible = ColumnContainsData(DATASHEET_AGE_MIN_COLUMN_NAME, Me.m_PTGrid)
        Me.m_PTAgeMaxVisible = ColumnContainsData(DATASHEET_AGE_MAX_COLUMN_NAME, Me.m_PTGrid)
        Me.m_PTAgeRelativeVisible = ColumnContainsData(DATASHEET_PT_AGE_RELATIVE_COLUMN_NAME, Me.m_PTGrid)
        Me.m_PTAgeResetVisible = ColumnContainsData(DATASHEET_PT_AGE_RESET_COLUMN_NAME, Me.m_PTGrid)
        Me.m_PTProportionVisible = ColumnContainsData(DATASHEET_PT_PROPORTION_COLUMN_NAME, Me.m_PTGrid)
        Me.m_PTTstMinVisible = ColumnContainsData(DATASHEET_PT_TST_MIN_COLUMN_NAME, Me.m_PTGrid)
        Me.m_PTTstMaxVisible = ColumnContainsData(DATASHEET_PT_TST_MAX_COLUMN_NAME, Me.m_PTGrid)
        Me.m_PTTstRelativeVisible = ColumnContainsData(DATASHEET_PT_TST_RELATIVE_COLUMN_NAME, Me.m_PTGrid)

    End Sub

    ''' <summary>
    ''' Updates the visibility of the columns in the deterministic transitions grid
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateDTColumnVisibility()

        If (Me.m_DTGrid.CurrentCell IsNot Nothing) Then

            Dim ci As Integer = Me.m_DTGrid.CurrentCell.ColumnIndex
            Dim ri As Integer = Me.m_DTGrid.CurrentCell.RowIndex
            Dim cn As String = Me.m_DTGrid.Columns(ci).Name
            Dim dgr As DataGridViewRow = Me.m_DTGrid.Rows(ri)

            If (cn = DATASHEET_ITERATION_COLUMN_NAME And Not Me.m_DTIterationVisible) Then
                Me.m_DTGrid.CurrentCell = dgr.Cells(DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME)
            ElseIf (cn = DATASHEET_TIMESTEP_COLUMN_NAME And Not Me.m_DTTimestepVisible) Then
                Me.m_DTGrid.CurrentCell = dgr.Cells(DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME)
            ElseIf (cn = DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME And Not Me.m_DTStratumVisible) Then
                Me.m_DTGrid.CurrentCell = dgr.Cells(DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME)
            ElseIf (cn = DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME And Not Me.m_DTToStratumVisible) Then
                Me.m_DTGrid.CurrentCell = dgr.Cells(DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME)
            ElseIf (cn = DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME And Not Me.m_DTToClassVisible) Then
                Me.m_DTGrid.CurrentCell = dgr.Cells(DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME)
            ElseIf (cn = DATASHEET_AGE_MIN_COLUMN_NAME And Not Me.m_DTAgeMinVisible) Then
                Me.m_DTGrid.CurrentCell = dgr.Cells(DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME)
            ElseIf (cn = DATASHEET_AGE_MAX_COLUMN_NAME And Not Me.m_DTAgeMaxVisible) Then
                Me.m_DTGrid.CurrentCell = dgr.Cells(DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME)
            End If

        End If

        Me.m_DTGrid.Columns(DATASHEET_ITERATION_COLUMN_NAME).Visible = Me.m_DTIterationVisible
        Me.m_DTGrid.Columns(DATASHEET_TIMESTEP_COLUMN_NAME).Visible = Me.m_DTTimestepVisible
        Me.m_DTGrid.Columns(DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME).Visible = Me.m_DTStratumVisible
        Me.m_DTGrid.Columns(DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME).Visible = Me.m_DTToStratumVisible
        Me.m_DTGrid.Columns(DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME).Visible = Me.m_DTToClassVisible
        Me.m_DTGrid.Columns(DATASHEET_AGE_MIN_COLUMN_NAME).Visible = Me.m_DTAgeMinVisible
        Me.m_DTGrid.Columns(DATASHEET_AGE_MAX_COLUMN_NAME).Visible = Me.m_DTAgeMaxVisible

        Me.MenuItemIterationDeterministic.Checked = Me.m_DTIterationVisible
        Me.MenuItemTimestepDeterministic.Checked = Me.m_DTTimestepVisible
        Me.MenuItemStratumDeterministic.Checked = Me.m_DTStratumVisible
        Me.MenuItemToStratumDeterministic.Checked = Me.m_DTToStratumVisible
        Me.MenuItemToClassDetreministic.Checked = Me.m_DTToClassVisible
        Me.MenuItemAgeMinDeterministic.Checked = Me.m_DTAgeMinVisible
        Me.MenuItemAgeMaxDeterministic.Checked = Me.m_DTAgeMaxVisible

    End Sub

    ''' <summary>
    ''' Updates the visibility of the columns in the probabilistic transitions grid
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdatePTColumnVisibility()

        If (Me.m_PTGrid.CurrentCell IsNot Nothing) Then

            Dim ci As Integer = Me.m_PTGrid.CurrentCell.ColumnIndex
            Dim ri As Integer = Me.m_PTGrid.CurrentCell.RowIndex
            Dim cn As String = Me.m_PTGrid.Columns(ci).Name
            Dim dgr As DataGridViewRow = Me.m_PTGrid.Rows(ri)

            If (cn = DATASHEET_ITERATION_COLUMN_NAME And Not Me.m_PTIterationVisible) Then
                Me.m_PTGrid.CurrentCell = dgr.Cells(DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME)
            ElseIf (cn = DATASHEET_TIMESTEP_COLUMN_NAME And Not Me.m_PTTimestepVisible) Then
                Me.m_PTGrid.CurrentCell = dgr.Cells(DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME)
            ElseIf (cn = DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME And Not Me.m_PTStratumVisible) Then
                Me.m_PTGrid.CurrentCell = dgr.Cells(DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME)
            ElseIf (cn = DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME And Not Me.m_PTToStratumVisible) Then
                Me.m_PTGrid.CurrentCell = dgr.Cells(DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME)
            ElseIf (cn = DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME And Not Me.m_PTToClassVisible) Then
                Me.m_PTGrid.CurrentCell = dgr.Cells(DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME)
            ElseIf (cn = DATASHEET_PT_PROPORTION_COLUMN_NAME And Not Me.m_PTProportionVisible) Then
                Me.m_PTGrid.CurrentCell = dgr.Cells(DATASHEET_PT_PROBABILITY_COLUMN_NAME)
            ElseIf (cn = DATASHEET_AGE_MIN_COLUMN_NAME And Not Me.m_PTAgeMinVisible) Then
                Me.m_PTGrid.CurrentCell = dgr.Cells(DATASHEET_PT_PROBABILITY_COLUMN_NAME)
            ElseIf (cn = DATASHEET_AGE_MAX_COLUMN_NAME And Not Me.m_PTAgeMaxVisible) Then
                Me.m_PTGrid.CurrentCell = dgr.Cells(DATASHEET_PT_PROBABILITY_COLUMN_NAME)
            ElseIf (cn = DATASHEET_PT_AGE_RELATIVE_COLUMN_NAME And Not Me.m_PTAgeRelativeVisible) Then
                Me.m_PTGrid.CurrentCell = dgr.Cells(DATASHEET_PT_PROBABILITY_COLUMN_NAME)
            ElseIf (cn = DATASHEET_PT_AGE_RESET_COLUMN_NAME And Not Me.m_PTAgeResetVisible) Then
                Me.m_PTGrid.CurrentCell = dgr.Cells(DATASHEET_PT_PROBABILITY_COLUMN_NAME)
            ElseIf (cn = DATASHEET_PT_TST_MIN_COLUMN_NAME And Not Me.m_PTTstMinVisible) Then
                Me.m_PTGrid.CurrentCell = dgr.Cells(DATASHEET_PT_PROBABILITY_COLUMN_NAME)
            ElseIf (cn = DATASHEET_PT_TST_MAX_COLUMN_NAME And Not Me.m_PTTstMaxVisible) Then
                Me.m_PTGrid.CurrentCell = dgr.Cells(DATASHEET_PT_PROBABILITY_COLUMN_NAME)
            ElseIf (cn = DATASHEET_PT_TST_RELATIVE_COLUMN_NAME And Not Me.m_PTTstRelativeVisible) Then
                Me.m_PTGrid.CurrentCell = dgr.Cells(DATASHEET_PT_PROBABILITY_COLUMN_NAME)
            End If

        End If

        Me.m_PTGrid.Columns(DATASHEET_ITERATION_COLUMN_NAME).Visible = Me.m_PTIterationVisible
        Me.m_PTGrid.Columns(DATASHEET_TIMESTEP_COLUMN_NAME).Visible = Me.m_PTTimestepVisible
        Me.m_PTGrid.Columns(DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME).Visible = Me.m_PTStratumVisible
        Me.m_PTGrid.Columns(DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME).Visible = Me.m_PTToStratumVisible
        Me.m_PTGrid.Columns(DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME).Visible = Me.m_PTToClassVisible
        Me.m_PTGrid.Columns(DATASHEET_PT_PROPORTION_COLUMN_NAME).Visible = Me.m_PTProportionVisible
        Me.m_PTGrid.Columns(DATASHEET_AGE_MIN_COLUMN_NAME).Visible = Me.m_PTAgeMinVisible
        Me.m_PTGrid.Columns(DATASHEET_AGE_MAX_COLUMN_NAME).Visible = Me.m_PTAgeMaxVisible
        Me.m_PTGrid.Columns(DATASHEET_PT_AGE_RELATIVE_COLUMN_NAME).Visible = Me.m_PTAgeRelativeVisible
        Me.m_PTGrid.Columns(DATASHEET_PT_AGE_RESET_COLUMN_NAME).Visible = Me.m_PTAgeResetVisible
        Me.m_PTGrid.Columns(DATASHEET_PT_TST_MIN_COLUMN_NAME).Visible = Me.m_PTTstMinVisible
        Me.m_PTGrid.Columns(DATASHEET_PT_TST_MAX_COLUMN_NAME).Visible = Me.m_PTTstMaxVisible
        Me.m_PTGrid.Columns(DATASHEET_PT_TST_RELATIVE_COLUMN_NAME).Visible = Me.m_PTTstRelativeVisible

    End Sub

    ''' <summary>
    ''' Sets a column to read-only mode
    ''' </summary>
    ''' <param name="columnName"></param>
    ''' <param name="grid"></param>
    ''' <remarks></remarks>
    Private Shared Sub SetColumnReadOnly(ByVal columnName As String, ByVal grid As DataGridView)

        Dim col As DataGridViewColumn = grid.Columns(columnName)
        col.DefaultCellStyle.BackColor = Color.FromArgb(232, 232, 232)
        col.ReadOnly = True

    End Sub

    ''' <summary>
    ''' Configures the read-only properties of the columns depending on the chosen filter
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConfigureColumnsReadOnly()

        Debug.Assert(Not (Me.m_ShowTransitionsTo And Me.m_ShowTransitionsFrom))

        For Each c As DataGridViewColumn In Me.m_DTGrid.Columns
            c.DefaultCellStyle.BackColor = Color.White
            c.ReadOnly = False
        Next

        For Each c As DataGridViewColumn In Me.m_PTGrid.Columns
            c.DefaultCellStyle.BackColor = Color.White
            c.ReadOnly = False
        Next

        SetColumnReadOnly(DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME, Me.m_DTGrid)
        SetColumnReadOnly(DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME, Me.m_DTGrid)
        SetColumnReadOnly(DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME, Me.m_PTGrid)

        If (Me.m_ShowTransitionsTo) Then

            SetColumnReadOnly(DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME, Me.m_DTGrid)
            SetColumnReadOnly(DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME, Me.m_DTGrid)
            SetColumnReadOnly(DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME, Me.m_PTGrid)
            SetColumnReadOnly(DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME, Me.m_PTGrid)
            SetColumnReadOnly(DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME, Me.m_PTGrid)

        End If

    End Sub

    ''' <summary>
    ''' Toggles the visibility of the Deterministic iteration column
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ToggleDeterministicIterationVisible()

        Me.m_DTIterationVisible = (Not Me.m_DTIterationVisible)
        Me.UpdateDTColumnVisibility()

    End Sub

    ''' <summary>
    ''' Toggles the visibility of the Deterministic timestep column
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ToggleDeterministicTimestepVisible()

        Me.m_DTTimestepVisible = (Not Me.m_DTTimestepVisible)
        Me.UpdateDTColumnVisibility()

    End Sub

    ''' <summary>
    ''' Toggles the visibility of the Deterministic Stratum column
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ToggleDeterministicStratumVisible()

        Me.m_DTStratumVisible = (Not Me.m_DTStratumVisible)
        Me.UpdateDTColumnVisibility()

    End Sub

    ''' <summary>
    ''' Toggles the visibility of the Deterministic To Stratum column
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ToggleDeterministicToStratumVisible()

        Me.m_DTToStratumVisible = (Not Me.m_DTToStratumVisible)
        Me.UpdateDTColumnVisibility()

    End Sub

    ''' <summary>
    ''' Toggles the visibility of the Deterministic To Class column
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ToggleDeterministicToClassVisible()

        Me.m_DTToClassVisible = (Not Me.m_DTToClassVisible)
        Me.UpdateDTColumnVisibility()

    End Sub

    ''' <summary>
    ''' Toggles the visibilty of the Deterministic age min column
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ToggleDeterministicAgeMinVisible()

        Me.m_DTAgeMinVisible = (Not Me.m_DTAgeMinVisible)
        Me.UpdateDTColumnVisibility()

    End Sub

    ''' <summary>
    ''' Toggles the visibilty of the Deterministic age max column
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ToggleDeterministicAgeMaxVisible()

        Me.m_DTAgeMaxVisible = (Not Me.m_DTAgeMaxVisible)
        Me.UpdateDTColumnVisibility()

    End Sub

    ''' <summary>
    ''' Toggles the visibility of the Probabilistic iteration column
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ToggleProbabilisticIterationVisible()

        Me.m_PTIterationVisible = (Not Me.m_PTIterationVisible)
        Me.UpdatePTColumnVisibility()

    End Sub

    ''' <summary>
    ''' Toggles the visibility of the Probabilistic timestep column
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ToggleProbabilisticTimestepVisible()

        Me.m_PTTimestepVisible = (Not Me.m_PTTimestepVisible)
        Me.UpdatePTColumnVisibility()

    End Sub

    ''' <summary>
    ''' Toggles the visibility of the Probabilistic Stratum column
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ToggleProbabilisticStratumVisible()

        Me.m_PTStratumVisible = (Not Me.m_PTStratumVisible)
        Me.UpdatePTColumnVisibility()

    End Sub

    ''' <summary>
    ''' Toggles the visibility of the Probabilistic To Stratum column
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ToggleProbabilisticToStratumVisible()

        Me.m_PTToStratumVisible = (Not Me.m_PTToStratumVisible)
        Me.UpdatePTColumnVisibility()

    End Sub

    ''' <summary>
    ''' Toggles the visibility of the Probabilistic To Class column
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ToggleProbabilisticToClassVisible()

        Me.m_PTToClassVisible = (Not Me.m_PTToClassVisible)
        Me.UpdatePTColumnVisibility()

    End Sub

    ''' <summary>
    ''' Toggles the visibilty of the Probabilistic age min column
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ToggleProbabilisticAgeMinVisible()

        Me.m_PTAgeMinVisible = (Not Me.m_PTAgeMinVisible)
        Me.UpdatePTColumnVisibility()

    End Sub

    ''' <summary>
    ''' Toggles the visibilty of the Probabilistic age max column
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ToggleProbabilisticAgeMaxVisible()

        Me.m_PTAgeMaxVisible = (Not Me.m_PTAgeMaxVisible)
        Me.UpdatePTColumnVisibility()

    End Sub

    ''' <summary>
    ''' Toggles the visibilty of the Probabilistic age relative (shift) column
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ToggleProbabilisticAgeRelativeVisible()

        Me.m_PTAgeRelativeVisible = (Not Me.m_PTAgeRelativeVisible)
        Me.UpdatePTColumnVisibility()

    End Sub

    ''' <summary>
    ''' Toggles the visibilty of the Probabilistic age reset column
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ToggleProbabilisticAgeResetVisible()

        Me.m_PTAgeResetVisible = (Not Me.m_PTAgeResetVisible)
        Me.UpdatePTColumnVisibility()

    End Sub

    ''' <summary>
    ''' Shows transitions To
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ShowTransitionsTo()

        Me.m_ShowTransitionsFrom = False
        Me.m_ShowTransitionsTo = True

        Me.FilterDeterministicTransitions()
        Me.FilterProbabilisticTransitions()

        Me.MenuItemTransitionsToDeterministic.Checked = Me.m_ShowTransitionsTo
        Me.MenuItemTransitionsFromDeterministic.Checked = Me.m_ShowTransitionsFrom

        Me.ConfigureColumnsReadOnly()

    End Sub

    ''' <summary>
    ''' Shows transitions from
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ShowTransitionsFrom()

        Me.m_ShowTransitionsFrom = True
        Me.m_ShowTransitionsTo = False

        Me.FilterDeterministicTransitions()
        Me.FilterProbabilisticTransitions()

        Me.MenuItemTransitionsToDeterministic.Checked = Me.m_ShowTransitionsTo
        Me.MenuItemTransitionsFromDeterministic.Checked = Me.m_ShowTransitionsFrom

        Me.ConfigureColumnsReadOnly()

    End Sub

    ''' <summary>
    ''' Executes the Probabilistic Transtions To command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnExecuteProbabilisticTranstionsToCommand(ByVal cmd As Command)

        If (Not Me.Validate()) Then
            Return
        End If

        Me.ShowTransitionsTo()

    End Sub

    ''' <summary>
    ''' Updates the Probabilistic Transtions To command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnUpdateProbabilisticTranstionsToCommand(ByVal cmd As Command)

        cmd.IsEnabled = True
        cmd.IsChecked = Me.m_ShowTransitionsTo

    End Sub

    ''' <summary>
    ''' Executes the Probabilistic Transtions From command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnExecuteProbabilisticTranstionsFromCommand(ByVal cmd As Command)

        If (Not Me.Validate()) Then
            Return
        End If

        Me.ShowTransitionsFrom()

    End Sub

    ''' <summary>
    ''' Updates the Probabilistic Transtions From command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnUpdateProbabilisticTranstionsFromCommand(ByVal cmd As Command)

        cmd.IsEnabled = True
        cmd.IsChecked = Me.m_ShowTransitionsFrom

    End Sub

    ''' <summary>
    ''' Executes the Probabilistic Transtions Iteration command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnExecuteProbabilisticIterationCommand(ByVal cmd As Command)

        If (Not Me.Validate()) Then
            Return
        End If

        Me.ToggleProbabilisticIterationVisible()

    End Sub

    ''' <summary>
    ''' Updates the Probabilistic Transtions Iteration command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnUpdateProbabilisticIterationCommand(ByVal cmd As Command)

        cmd.IsEnabled = True
        cmd.IsChecked = Me.m_PTIterationVisible

    End Sub

    ''' <summary>
    ''' Executes the Probabilistic Transtions Timestep command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnExecuteProbabilisticTimestepCommand(ByVal cmd As Command)

        If (Not Me.Validate()) Then
            Return
        End If

        Me.ToggleProbabilisticTimestepVisible()

    End Sub

    ''' <summary>
    ''' Updates the Probabilistic Transtions Timestep command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnUpdateProbabilisticTimestepCommand(ByVal cmd As Command)

        cmd.IsEnabled = True
        cmd.IsChecked = Me.m_PTTimestepVisible

    End Sub

    ''' <summary>
    ''' Executes the Probabilistic Transtions Stratum command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnExecuteProbabilisticStratumCommand(ByVal cmd As Command)

        If (Not Me.Validate()) Then
            Return
        End If

        Me.ToggleProbabilisticStratumVisible()

    End Sub

    ''' <summary>
    ''' Updates the Probabilistic Transtions Stratum command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnUpdateProbabilisticStratumCommand(ByVal cmd As Command)

        cmd.IsEnabled = True
        cmd.IsChecked = Me.m_PTStratumVisible

    End Sub

    ''' <summary>
    ''' Executes the Probabilistic Transtions To Stratum command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnExecuteProbabilisticToStratumCommand(ByVal cmd As Command)

        If (Not Me.Validate()) Then
            Return
        End If

        Me.ToggleProbabilisticToStratumVisible()

    End Sub

    ''' <summary>
    ''' Updates the Probabilistic Transtions To Stratum command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnUpdateProbabilisticToStratumCommand(ByVal cmd As Command)

        cmd.IsEnabled = True
        cmd.IsChecked = Me.m_PTToStratumVisible

    End Sub

    ''' <summary>
    ''' Executes the Probabilistic Transtions To Class command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnExecuteProbabilisticToClassCommand(ByVal cmd As Command)

        If (Not Me.Validate()) Then
            Return
        End If

        Me.ToggleProbabilisticToClassVisible()

    End Sub

    ''' <summary>
    ''' Updates the Probabilistic Transtions To Class command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnUpdateProbabilisticToClassCommand(ByVal cmd As Command)

        cmd.IsEnabled = True
        cmd.IsChecked = Me.m_PTToClassVisible

    End Sub

    ''' <summary>
    ''' Executes the Probabilistic Transtions Proportion command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnExecuteProbabilisticProportionCommand(ByVal cmd As Command)

        If (Not Me.Validate()) Then
            Return
        End If

        Me.m_PTProportionVisible = (Not Me.m_PTProportionVisible)
        Me.UpdatePTColumnVisibility()

    End Sub

    ''' <summary>
    ''' Updates the Probabilistic Transtions Proportion command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnUpdateProbabilisticProportionCommand(ByVal cmd As Command)

        cmd.IsEnabled = True
        cmd.IsChecked = Me.m_PTProportionVisible

    End Sub

    ''' <summary>
    ''' Executes the Probabilistic Transtions Age Min command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnExecuteProbabilisticAgeMinCommand(ByVal cmd As Command)

        If (Not Me.Validate()) Then
            Return
        End If

        Me.ToggleProbabilisticAgeMinVisible()

    End Sub

    ''' <summary>
    ''' Updates the Probabilistic Transtions Age Min command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnUpdateProbabilisticAgeMinCommand(ByVal cmd As Command)

        cmd.IsEnabled = True
        cmd.IsChecked = Me.m_PTAgeMinVisible

    End Sub

    ''' <summary>
    ''' Executes the Probabilistic Transtions Age Max command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnExecuteProbabilisticAgeMaxCommand(ByVal cmd As Command)

        If (Not Me.Validate()) Then
            Return
        End If

        Me.ToggleProbabilisticAgeMaxVisible()

    End Sub

    ''' <summary>
    ''' Updates the Probabilistic Transtions Age Max command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnUpdateProbabilisticAgeMaxCommand(ByVal cmd As Command)

        cmd.IsEnabled = True
        cmd.IsChecked = Me.m_PTAgeMaxVisible

    End Sub
    ''' <summary>
    ''' Executes the Probabilistic Transtions Age Relative command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnExecuteProbabilisticAgeRelativeCommand(ByVal cmd As Command)

        If (Not Me.Validate()) Then
            Return
        End If

        Me.ToggleProbabilisticAgeRelativeVisible()

    End Sub

    ''' <summary>
    ''' Updates the Probabilistic Transtions Age Relative command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnUpdateProbabilisticAgeRelativeCommand(ByVal cmd As Command)

        cmd.IsEnabled = True
        cmd.IsChecked = Me.m_PTAgeRelativeVisible

    End Sub
    ''' <summary>
    ''' Executes the Probabilistic Transtions Age Reset command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnExecuteProbabilisticAgeResetCommand(ByVal cmd As Command)

        If (Not Me.Validate()) Then
            Return
        End If

        Me.ToggleProbabilisticAgeResetVisible()

    End Sub

    ''' <summary>
    ''' Updates the Probabilistic Transtions Age Reset command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnUpdateProbabilisticAgeResetCommand(ByVal cmd As Command)

        cmd.IsEnabled = True
        cmd.IsChecked = Me.m_PTAgeResetVisible

    End Sub


    ''' <summary>
    ''' Executes the Probabilistic Transtions Tst Min command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnExecuteProbabilisticTstMinCommand(ByVal cmd As Command)

        If (Not Me.Validate()) Then
            Return
        End If

        Me.m_PTTstMinVisible = (Not Me.m_PTTstMinVisible)
        Me.UpdatePTColumnVisibility()

    End Sub

    ''' <summary>
    ''' Updates the Probabilistic Transtions Tst Min command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnUpdateProbabilisticTstMinCommand(ByVal cmd As Command)

        cmd.IsEnabled = True
        cmd.IsChecked = Me.m_PTTstMinVisible

    End Sub
    ''' <summary>
    ''' Executes the Probabilistic Transtions Tst Max command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnExecuteProbabilisticTstMaxCommand(ByVal cmd As Command)

        If (Not Me.Validate()) Then
            Return
        End If

        Me.m_PTTstMaxVisible = (Not Me.m_PTTstMaxVisible)
        Me.UpdatePTColumnVisibility()

    End Sub

    ''' <summary>
    ''' Updates the Probabilistic Transtions Tst Max command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnUpdateProbabilisticTstMaxCommand(ByVal cmd As Command)

        cmd.IsEnabled = True
        cmd.IsChecked = Me.m_PTTstMaxVisible

    End Sub

    ''' <summary>
    ''' Executes the Probabilistic Transtions Tst Relative command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnExecuteProbabilisticTstRelativeCommand(ByVal cmd As Command)

        If (Not Me.Validate()) Then
            Return
        End If

        Me.m_PTTstRelativeVisible = (Not Me.m_PTTstRelativeVisible)
        Me.UpdatePTColumnVisibility()

    End Sub

    ''' <summary>
    ''' Updates the Probabilistic Transtions Tst Relative command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnUpdateProbabilisticTstRelativeCommand(ByVal cmd As Command)

        cmd.IsEnabled = True
        cmd.IsChecked = Me.m_PTTstRelativeVisible

    End Sub

    ''' <summary>
    ''' Handles the Transitions To context menu item Clicked event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MenuItemTransitionsToDeterministic_Click(sender As System.Object, e As System.EventArgs) Handles MenuItemTransitionsToDeterministic.Click

        If (Me.Validate()) Then
            Me.ShowTransitionsTo()
        End If

    End Sub

    ''' <summary>
    ''' Handles the Transitions From context menu item Clicked event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MenuItemTransitionsFromDeterministic_Click(sender As System.Object, e As System.EventArgs) Handles MenuItemTransitionsFromDeterministic.Click

        If (Me.Validate()) Then
            Me.ShowTransitionsFrom()
        End If

    End Sub

    ''' <summary>
    ''' Handles the Iteration context menu item Clicked event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MenuItemIterationDeterministic_Click(sender As Object, e As EventArgs) Handles MenuItemIterationDeterministic.Click
        Me.ToggleDeterministicIterationVisible()
    End Sub

    ''' <summary>
    ''' Handles the Timestep context menu item Clicked event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MenuItemTimestepDeterministic_Click(sender As Object, e As EventArgs) Handles MenuItemTimestepDeterministic.Click
        Me.ToggleDeterministicTimestepVisible()
    End Sub

    ''' <summary>
    '''
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MenuItemStratumDeterministic_Click(sender As System.Object, e As System.EventArgs) Handles MenuItemStratumDeterministic.Click
        Me.ToggleDeterministicStratumVisible()
    End Sub

    ''' <summary>
    ''' Handles the To Stratum context menu item Clicked event 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MenuItemToStratumDeterministic_Click(sender As Object, e As EventArgs) Handles MenuItemToStratumDeterministic.Click
        Me.ToggleDeterministicToStratumVisible()
    End Sub

    ''' <summary>
    ''' Handles the To Class context menu item Clicked event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MenuItemToClassDetreministic_Click(sender As Object, e As EventArgs) Handles MenuItemToClassDetreministic.Click
        Me.ToggleDeterministicToClassVisible()
    End Sub

    ''' <summary>
    ''' Handles the Ages context menu item Clicked event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MenuItemAgeMinDeterministic_Click(sender As System.Object, e As System.EventArgs) Handles MenuItemAgeMinDeterministic.Click
        Me.ToggleDeterministicAgeMinVisible()
    End Sub

    ''' <summary>
    ''' Handles the Ages context menu item Clicked event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MenuItemAgeMaxDeterministic_Click(sender As System.Object, e As System.EventArgs) Handles MenuItemAgeMaxDeterministic.Click
        Me.ToggleDeterministicAgeMaxVisible()
    End Sub

End Class
