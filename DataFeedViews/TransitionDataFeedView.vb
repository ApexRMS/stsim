'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports System.Text
Imports System.Drawing
Imports System.Reflection
Imports System.Windows.Forms
Imports System.Globalization
Imports SyncroSim.Core
Imports SyncroSim.Core.Forms
Imports SyncroSim.Common.Forms

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class TransitionDataFeedView

    Private m_TooltipFirst As New ToolTip
    Private m_TooltipPrev As New ToolTip
    Private m_TooltipNext As New ToolTip
    Private m_TooltipSelect As New ToolTip
    Private m_TooltipLast As New ToolTip
    Private m_TooltipZoomOut As New ToolTip
    Private m_TooltipZoomIn As New ToolTip
    Private m_FilterCriteria As New TransitionFilterCriteria
    Private m_Monitor As DataSheetMonitor
    Private m_ShowTooltips As Boolean = True
    Private m_CurrentZoom As Single = 1.0
    Private m_IsLoading As Boolean
    Private m_IsEnabled As Boolean
    Private m_ShowGrid As Boolean

    ''' <summary>
    ''' Overrides InitializeView
    ''' </summary>
    ''' <remarks>We add a pixel of padding to make our custom border visible</remarks>
    Protected Overrides Sub InitializeView()

        MyBase.InitializeView()

        Me.InitializeToolTips()
        Me.InitializeCommands()

        Me.Padding = New Padding(1)

        Me.m_Monitor = New DataSheetMonitor(
            Me.Project, DATASHEET_TERMINOLOGY_NAME, AddressOf Me.OnTerminologyChanged)

        Me.m_Monitor.Invoke()

    End Sub

    ''' <summary>
    ''' Overrides Dispose
    ''' </summary>
    ''' <param name="disposing"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)

        If (disposing And Not Me.IsDisposed) Then

            Me.DisposeToolTips()
            Me.RemoveDiagramHandlers()
            Me.DisposeTabStripItems()
            Me.m_Monitor.Dispose()

            If components IsNot Nothing Then
                components.Dispose()
            End If

        End If

        MyBase.Dispose(disposing)

    End Sub

    ''' <summary>
    ''' Overrides LoadDataFeed
    ''' </summary>
    ''' <param name="dataFeed"></param>
    ''' <remarks>We completely refresh the entire control when the data feed changes</remarks>
    Public Overrides Sub LoadDataFeed(ByVal dataFeed As DataFeed)

        MyBase.LoadDataFeed(dataFeed)

        Me.m_IsLoading = True

        Me.SynchronizeFilterCriteria()
        Me.RefreshTabStripControls()

        Me.m_IsLoading = False

    End Sub

    ''' <summary>
    ''' Overrides EnableView
    ''' </summary>
    ''' <param name="enable"></param>
    ''' <remarks>We need to forward this to all diagrams and views since they are custom</remarks>
    Public Overrides Sub EnableView(enable As Boolean)

        Me.m_IsEnabled = enable

        For Each Item As TransitionDiagramTabStripItem In Me.TabStripMain.Items

            If (Item.Control Is Nothing) Then
                Continue For
            End If

            If (TypeOf (Item) Is StratumTabStripItem) Then

                Dim d As TransitionDiagram = CType(Item.Control, TransitionDiagram)
                d.IsReadOnly = (Not Me.m_IsEnabled)

                d.ApplyReadonlySettings()

            Else

                Dim v As DataFeedView = CType(Item.Control, DataFeedView)
                v.EnableView(Me.m_IsEnabled)

            End If

        Next

    End Sub

    ''' <summary>
    ''' Overrides OnPaint
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks>We override this function so we can have a light gray border...</remarks>
    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)

        MyBase.OnPaint(e)

        Dim p As Pen = Pens.Silver

        e.Graphics.DrawLine(p, 0, 0, Me.Bounds.Width - 1, 0)
        e.Graphics.DrawLine(p, Me.Bounds.Width - 1, 0, Me.Bounds.Width - 1, Me.Bounds.Height - 1)
        e.Graphics.DrawLine(p, Me.Bounds.Width - 1, Me.Bounds.Height - 1, 0, Me.Bounds.Height - 1)
        e.Graphics.DrawLine(p, 0, Me.Bounds.Height - 1, 0, 0)

    End Sub

    ''' <summary>
    ''' Called when rows have been added
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub OnRowsAdded(sender As Object, e As DataSheetRowEventArgs)

        MyBase.OnRowsAdded(sender, e)
        Me.HandleExternalRecordEvent(sender, e)

    End Sub

    ''' <summary>
    ''' Called when the rows have been deleted
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub OnRowsDeleted(sender As Object, e As DataSheetRowEventArgs)

        MyBase.OnRowsDeleted(sender, e)
        Me.HandleExternalRecordEvent(sender, e)

    End Sub

    ''' <summary>
    ''' Called when the rows have been modified
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub OnRowsModified(sender As Object, e As DataSheetRowEventArgs)

        MyBase.OnRowsModified(sender, e)
        Me.HandleExternalRecordEvent(sender, e)

    End Sub

    ''' <summary>
    ''' Overrides OnResize
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub OnResize(ByVal e As System.EventArgs)

        MyBase.OnResize(e)
        Me.ResetScrollbars()

    End Sub

    ''' <summary>
    ''' A callback for when the terminology changes
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OnTerminologyChanged(ByVal e As DataSheetMonitorEventArgs)
        Me.RefreshSelectStratumTooltip()
    End Sub

    ''' <summary>
    ''' Refreshes the Select Stratum tooltip with the correct terminology
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RefreshSelectStratumTooltip()

        Me.m_TooltipSelect.Dispose()
        Me.m_TooltipSelect = New ToolTip()

        Dim primary As String = Nothing
        Dim secondary As String = Nothing
        Dim ds As DataSheet = Me.Project.GetDataSheet(DATASHEET_TERMINOLOGY_NAME)

        GetStratumLabelTerminology(ds, primary, secondary)
        Me.m_TooltipSelect.SetToolTip(Me.ButtonSelectStratum, "Select " & primary)

    End Sub

    ''' <summary>
    ''' Disposes all tab strip items
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DisposeTabStripItems()

        For Each item As TabStripItem In Me.TabStripMain.Items
            item.Dispose()
        Next

    End Sub

    ''' <summary>
    ''' Initializes the tooltips for the command bar buttons
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeToolTips()

        Me.m_TooltipFirst.SetToolTip(Me.ButtonFirst, "First Item")
        Me.m_TooltipPrev.SetToolTip(Me.ButtonPrevious, "Previous Item")
        Me.m_TooltipNext.SetToolTip(Me.ButtonNext, "Next Item")
        Me.m_TooltipLast.SetToolTip(Me.ButtonLast, "Last Item")
        Me.m_TooltipZoomOut.SetToolTip(Me.ButtonZoomOut, "Zoom Out")
        Me.m_TooltipZoomIn.SetToolTip(Me.ButtonZoomIn, "Zoom In")

        Me.RefreshSelectStratumTooltip()

    End Sub

    ''' <summary>
    ''' Disposes the command bar tooltips
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DisposeToolTips()

        Me.m_TooltipFirst.Dispose()
        Me.m_TooltipPrev.Dispose()
        Me.m_TooltipSelect.Dispose()
        Me.m_TooltipNext.Dispose()
        Me.m_TooltipLast.Dispose()
        Me.m_TooltipZoomOut.Dispose()
        Me.m_TooltipZoomIn.Dispose()

    End Sub

    ''' <summary>
    ''' Initializes the command collection
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeCommands()

        Dim CmdOpen As New Command("stsim_open_state_classes", COMMAND_STRING_OPEN, Nothing, AddressOf OnExecuteOpenCommand, AddressOf OnUpdateOpenCommand)
        CmdOpen.IsBold = True
        Me.Commands.Add(CmdOpen)

        Me.Commands.Add(Command.CreateSeparatorCommand())
        Me.Commands.Add(New Command("ssim_cut", COMMAND_STRING_CUT, My.Resources.Cut16x16, AddressOf OnExecuteCutCommand, AddressOf OnUpdateCutCommand))
        Me.Commands.Add(New Command("ssim_copy", COMMAND_STRING_COPY, My.Resources.Copy16x16, AddressOf OnExecuteCopyCommand, AddressOf OnUpdateCopyCommand))
        Me.Commands.Add(New Command("ssim_paste", COMMAND_STRING_PASTE, My.Resources.Paste16x16, AddressOf OnExecutePasteCommand, AddressOf OnUpdatePasteCommand))
        Me.Commands.Add(New Command("stsim_paste_state_classes_special", COMMAND_STRING_PASTE_SPECIAL, AddressOf OnExecutePasteSpecialCommand, AddressOf OnUpdatePasteSpecialCommand))
        Me.Commands.Add(Command.CreateSeparatorCommand())
        Me.Commands.Add(New Command("ssim_delete", COMMAND_STRING_DELETE, My.Resources.Delete16x16, AddressOf OnExecuteDeleteCommand, AddressOf OnUpdateDeleteCommand))
        Me.Commands.Add(Command.CreateSeparatorCommand())
        Me.Commands.Add(New Command("ssim_select_all", COMMAND_STRING_SELECT_ALL, AddressOf OnExecuteSelectAllCommand, AddressOf OnUpdateSelectAllCommand))
        Me.Commands.Add(Command.CreateSeparatorCommand())
        Me.Commands.Add(New Command("stsim_show_grid", "Show Grid", AddressOf OnExecuteShowGridCommand, AddressOf OnUpdateShowGridCommand))
        Me.Commands.Add(New Command("stsim_show_tooltips", "Show Tooltips", AddressOf OnExecuteShowTooltipsCommand, AddressOf OnUpdateShowTooltipsCommand))
        Me.Commands.Add(Command.CreateSeparatorCommand())
        Me.Commands.Add(New Command("stsim_add_state_class", "Add State Class...", AddressOf OnExecuteAddStateClassCommand, AddressOf OnUpdateAddStateClassCommand))
        Me.Commands.Add(Command.CreateSeparatorCommand())
        Me.Commands.Add(New Command("stsim_edit_state_class", "Edit State Class...", AddressOf OnExecuteEditStateClassCommand, AddressOf OnUpdateEditStateClassCommand))
        Me.Commands.Add(Command.CreateSeparatorCommand())
        Me.Commands.Add(New Command("stsim_filter_transitions", "Filter Transitions...", My.Resources.Filter16x16, AddressOf OnExecuteFilterTransitionsCommand, AddressOf OnUpdateFilterTransitionsCommand))
        Me.Commands.Add(Command.CreateSeparatorCommand())
        Me.Commands.Add(New Command("stsim_select_stratum", "Select Stratum...", My.Resources.Search16x16, AddressOf OnExecuteSelectStratumCommand, AddressOf OnUpdateSelectStratumCommand))

    End Sub

    ''' <summary>
    ''' Removes any existing diagram handlers
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RemoveDiagramHandlers()

        For Each Item As TransitionDiagramTabStripItem In Me.TabStripMain.Items

            If (TypeOf (Item) Is StratumTabStripItem) Then

                If (Item.Control IsNot Nothing) Then

                    Dim d As BoxArrowDiagram = CType(Item.Control, BoxArrowDiagram)

                    RemoveHandler d.ZoomChanged, AddressOf OnDiagramZoomChanged
                    RemoveHandler d.MouseDoubleClick, AddressOf OnDiagramMouseDoubleClick

                End If

            End If

        Next

    End Sub

    ''' <summary>
    ''' Synchronizes the transition group criteria
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SynchronizeFilterCriteria()

        Dim cr As New TransitionFilterCriteria()
        Dim ds As DataSheet = Me.Project.GetDataSheet(DATASHEET_TRANSITION_GROUP_NAME)

        cr.IncludeDeterministic = Me.m_FilterCriteria.IncludeDeterministic
        cr.IncludeProbabilistic = Me.m_FilterCriteria.IncludeProbabilistic

        For Each dr As DataRow In ds.GetData().Rows

            If (dr.RowState <> DataRowState.Deleted) Then
                cr.TransitionGroups.Add(CInt(dr(ds.ValueMember)), True)
            End If

        Next

        For Each tg As Integer In Me.m_FilterCriteria.TransitionGroups.Keys

            If (cr.TransitionGroups.ContainsKey(tg)) Then
                cr.TransitionGroups(tg) = Me.m_FilterCriteria.TransitionGroups(tg)
            End If

        Next

        Me.m_FilterCriteria = cr

    End Sub

    ''' <summary>
    ''' Handles an external record event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub HandleExternalRecordEvent(sender As Object, e As DataSheetRowEventArgs)

        If (IsDataSheetEvent(sender, DATASHEET_STRATA_NAME)) Then

            Me.SynchronizeFilterCriteria()
            Me.RefreshTabStripControls()

        ElseIf (
            IsDataSheetEvent(sender, DATASHEET_TRANSITION_GROUP_NAME) Or
            IsDataSheetEvent(sender, DATASHEET_TRANSITION_TYPE_NAME) Or
            IsDataSheetEvent(sender, DATASHEET_TRANSITION_TYPE_GROUP_NAME)) Then

            Me.SynchronizeFilterCriteria()
            Me.RefreshTransitionDiagrams()

        ElseIf (
            IsDataSheetEvent(sender, DATASHEET_STATECLASS_NAME) Or
            IsDataSheetEvent(sender, DATASHEET_STATE_LABEL_X_NAME) Or
            IsDataSheetEvent(sender, DATASHEET_STATE_LABEL_Y_NAME)) Then

            Me.RefreshTransitionDiagrams()

        ElseIf (
            IsDataSheetEvent(sender, DATASHEET_DT_NAME) Or
            IsDataSheetEvent(sender, DATASHEET_PT_NAME)) Then

            Me.RefreshTransitionDiagrams()

        End If

    End Sub

    ''' <summary>
    ''' Determines if the specfied sender is a datasheet with the specified data feed name
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="dataFeedName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function IsDataSheetEvent(sender As Object, ByVal dataFeedName As String) As Boolean

        If (TypeOf (sender) Is DataSheet) Then

            If (CType(sender, DataSheet).Name = dataFeedName) Then
                Return True
            End If

        End If

        Return False

    End Function

    ''' <summary>
    ''' Gets the current diagram
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCurrentDiagram() As TransitionDiagram

        Dim Item As StratumTabStripItem = CType(Me.TabStripMain.SelectedItem, StratumTabStripItem)
        Return CType(Item.Control, TransitionDiagram)

    End Function

    ''' <summary>
    ''' Determines if the current tab strip item is a diagram item
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CurrentItemIsDiagramItem() As Boolean

        If (Me.TabStripMain.SelectedItem IsNot Nothing) Then

            If (TypeOf (Me.TabStripMain.SelectedItem) Is StratumTabStripItem) Then
                Return True
            End If

        End If

        Return False

    End Function

    ''' <summary>
    ''' Determines if it is possible to open state classes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CanOpenStateClasses() As Boolean

        If (Me.CurrentItemIsDiagramItem()) Then

            Dim d As TransitionDiagram = Me.GetCurrentDiagram()

            If (d.CanOpenStateClasses()) Then
                Return True
            End If

        End If

        Return False

    End Function

    ''' <summary>
    ''' Determines if it is possible to delete state classes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CanDeleteStateClasses() As Boolean

        If (Me.CurrentItemIsDiagramItem()) Then

            Dim d As TransitionDiagram = Me.GetCurrentDiagram()

            If (d.CanCutStateClasses()) Then
                Return True
            End If

        End If

        Return False

    End Function

    ''' <summary>
    ''' Determines if it is possible to paste state classes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CanPasteStateClasses() As Boolean

        If (Me.CurrentItemIsDiagramItem()) Then

            Dim d As TransitionDiagram = Me.GetCurrentDiagram()

            If (d.CanPasteStateClasses()) Then
                Return True
            End If

        End If

        Return False

    End Function

    ''' <summary>
    ''' Executes the Open command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnExecuteOpenCommand(ByVal cmd As Command)
        Me.OpenSelectedStateClasses()
    End Sub

    ''' <summary>
    ''' Updates the Open command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnUpdateOpenCommand(ByVal cmd As Command)
        cmd.IsEnabled = Me.CanOpenStateClasses()
    End Sub

    ''' <summary>
    ''' Executes the Cut command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnExecuteCutCommand(ByVal cmd As Command)

        Using h As New HourGlass
            Me.GetCurrentDiagram().CutStateClasses()
        End Using

    End Sub

    ''' <summary>
    ''' Updates the Cut command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnUpdateCutCommand(ByVal cmd As Command)
        cmd.IsEnabled = Me.CanDeleteStateClasses()
    End Sub

    ''' <summary>
    ''' Executes the Copy command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnExecuteCopyCommand(ByVal cmd As Command)

        Using h As New HourGlass
            Me.GetCurrentDiagram().CopyStateClasses()
        End Using

    End Sub

    ''' <summary>
    ''' Updates the Copy command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnUpdateCopyCommand(ByVal cmd As Command)

        If (Me.CurrentItemIsDiagramItem()) Then

            Dim d As TransitionDiagram = Me.GetCurrentDiagram()

            If (d.CanCopyStateClasses()) Then
                cmd.IsEnabled = True
                Return
            End If

        End If

        cmd.IsEnabled = False

    End Sub

    ''' <summary>
    ''' Executes the Paste command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnExecutePasteCommand(ByVal cmd As Command)

        Using h As New HourGlass
            Me.GetCurrentDiagram().PasteStateClasses(Not cmd.IsRouted())
        End Using

    End Sub

    ''' <summary>
    ''' Updates the Paste command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnUpdatePasteCommand(ByVal cmd As Command)
        cmd.IsEnabled = Me.CanPasteStateClasses()
    End Sub

    ''' <summary>
    ''' Executes the Paste Special command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnExecutePasteSpecialCommand(ByVal cmd As Command)

        Dim dlg As New TransitionDiagramPasteSpecialForm()

        If (dlg.ShowDialog() <> DialogResult.OK) Then
            Return
        End If

        Using h As New HourGlass

            Me.GetCurrentDiagram().PasteStateClassesSpecial(
                dlg.PasteTransitionsAll,
                dlg.PasteTransitionsBetween,
                dlg.PasteTransitionsNone,
                dlg.PasteTransitionsDeterministic,
                dlg.PasteTransitionsProbabilistic,
                cmd.IsRouted())

        End Using

    End Sub

    ''' <summary>
    ''' Updates the Paste Special command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnUpdatePasteSpecialCommand(ByVal cmd As Command)
        cmd.IsEnabled = Me.CanPasteStateClasses()
    End Sub

    ''' <summary>
    ''' Executes the Delete command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnExecuteDeleteCommand(ByVal cmd As Command)

        If (FormsUtilities.ApplicationMessageBox(
            CONFIRM_DIAGRAM_DELETE,
            MessageBoxButtons.YesNo) <> DialogResult.Yes) Then

            Return

        End If

        Using h As New HourGlass
            Me.GetCurrentDiagram().DeleteStateClasses()
        End Using

    End Sub

    ''' <summary>
    ''' Updates the Delete command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnUpdateDeleteCommand(ByVal cmd As Command)
        cmd.IsEnabled = Me.CanDeleteStateClasses()
    End Sub

    ''' <summary>
    ''' Executes the Select All command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnExecuteSelectAllCommand(ByVal cmd As Command)
        Me.GetCurrentDiagram().SelectAllStateClasses()
    End Sub

    ''' <summary>
    ''' Updates the Select All command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnUpdateSelectAllCommand(ByVal cmd As Command)

        If (Me.CurrentItemIsDiagramItem()) Then

            Dim d As TransitionDiagram = Me.GetCurrentDiagram()

            If (d.CanSelectAllStateClasses()) Then
                cmd.IsEnabled = True
                Return
            End If

        End If

        cmd.IsEnabled = False

    End Sub

    ''' <summary>
    ''' Executes the Show Grid command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnExecuteShowGridCommand(ByVal cmd As Command)

        Me.m_ShowGrid = Not Me.m_ShowGrid

        For Each Item As TransitionDiagramTabStripItem In Me.TabStripMain.Items

            If (TypeOf (Item) Is StratumTabStripItem And Item.Control IsNot Nothing) Then
                CType(Item.Control, TransitionDiagram).ShowGrid = Me.m_ShowGrid
            End If

        Next

    End Sub

    ''' <summary>
    ''' Updates the Show Grid command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnUpdateShowGridCommand(ByVal cmd As Command)

        cmd.IsEnabled = Me.CurrentItemIsDiagramItem()
        cmd.IsChecked = (Me.m_ShowGrid)

    End Sub

    ''' <summary>
    ''' Executes the Show Tooltips command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnExecuteShowTooltipsCommand(ByVal cmd As Command)

        Me.m_ShowTooltips = Not Me.m_ShowTooltips

        For Each Item As TransitionDiagramTabStripItem In Me.TabStripMain.Items

            If (TypeOf (Item) Is StratumTabStripItem And Item.Control IsNot Nothing) Then
                CType(Item.Control, TransitionDiagram).ShowToolTips = Me.m_ShowTooltips
            End If

        Next

    End Sub

    ''' <summary>
    ''' Updates the Show Tooltips command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnUpdateShowTooltipsCommand(ByVal cmd As Command)

        cmd.IsEnabled = Me.CurrentItemIsDiagramItem()
        cmd.IsChecked = (cmd.IsEnabled AndAlso Me.GetCurrentDiagram().ShowToolTips())

    End Sub

    ''' <summary>
    ''' Executes the Add State Class command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnExecuteAddStateClassCommand(ByVal cmd As Command)
        Me.GetCurrentDiagram().AddStateClass()
    End Sub

    ''' <summary>
    ''' Updates the Add State Class command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnUpdateAddStateClassCommand(ByVal cmd As Command)

        If (Me.CurrentItemIsDiagramItem()) Then

            Dim d As TransitionDiagram = Me.GetCurrentDiagram()

            If (Not d.IsReadOnly()) Then

                cmd.IsEnabled = True
                Return

            End If

        End If

        cmd.IsEnabled = False

    End Sub

    ''' <summary>
    ''' Executes the Edit State Class command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnExecuteEditStateClassCommand(ByVal cmd As Command)
        Me.GetCurrentDiagram().EditStateClass()
    End Sub

    ''' <summary>
    ''' Updates the Edit State Class command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnUpdateEditStateClassCommand(ByVal cmd As Command)

        If (Me.CurrentItemIsDiagramItem()) Then

            Dim d As TransitionDiagram = Me.GetCurrentDiagram()

            If (d.IsReadOnly()) Then
                cmd.IsEnabled = False
            ElseIf (d.SelectedShapes.Count <> 1) Then
                cmd.IsEnabled = False
            Else
                cmd.IsEnabled = True
            End If

        Else
            cmd.IsEnabled = False
        End If

    End Sub

    ''' <summary>
    ''' Executes the Filter Transitions command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnExecuteFilterTransitionsCommand(ByVal cmd As Command)

        Dim dlg As New FilterTransitionsForm()
        Dim ds As DataSheet = Me.Project.GetDataSheet(DATASHEET_TRANSITION_GROUP_NAME)

        dlg.CheckBoxPanelMain.Initialize()
        dlg.CheckBoxPanelMain.BeginAddItems()

        For Each dr As DataRow In ds.GetData().Rows

            If (dr.RowState <> DataRowState.Deleted) Then

                Dim Id As Integer = CInt(dr(ds.ValueMember))
                Dim Name As String = CStr(dr(ds.DisplayMember))
                Dim IsSelected As Boolean = Me.m_FilterCriteria.TransitionGroups(Id)

                dlg.CheckBoxPanelMain.AddItem(IsSelected, Id, Name)

            End If

        Next

        dlg.CheckBoxPanelMain.EndAddItems()
        dlg.CheckBoxPanelMain.TitleBarText = "Transition Groups"
        dlg.CheckboxDeterministicTransitions.Checked = Me.m_FilterCriteria.IncludeDeterministic
        dlg.CheckboxProbabilisticTransitions.Checked = Me.m_FilterCriteria.IncludeProbabilistic
        dlg.CheckBoxPanelMain.IsReadOnly = (Not Me.m_FilterCriteria.IncludeProbabilistic)

        If (dlg.ShowDialog(Me) <> DialogResult.OK) Then
            Return
        End If

        Me.m_FilterCriteria.IncludeDeterministic = dlg.CheckboxDeterministicTransitions.Checked
        Me.m_FilterCriteria.IncludeProbabilistic = dlg.CheckboxProbabilisticTransitions.Checked

        For Each dr As DataRow In dlg.CheckBoxPanelMain.DataSource.Rows
            Me.m_FilterCriteria.TransitionGroups(CInt(dr("ItemID"))) = CBool(dr("IsSelected"))
        Next

        For Each Item As TransitionDiagramTabStripItem In Me.TabStripMain.Items

            If (TypeOf (Item) Is StratumTabStripItem AndAlso Item.Control IsNot Nothing) Then

                Dim i As StratumTabStripItem = CType(Item, StratumTabStripItem)
                Dim d As TransitionDiagram = CType(i.Control, TransitionDiagram)

                d.RefreshDiagram()
                d.FilterTransitions(Me.m_FilterCriteria)

            End If

        Next

    End Sub

    ''' <summary>
    ''' Updates the Filter Transitions command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnUpdateFilterTransitionsCommand(ByVal cmd As Command)

        If (Me.CurrentItemIsDiagramItem()) Then

            If (Me.GetCurrentDiagram().Shapes.Count > 0) Then
                cmd.IsEnabled = True
                Return
            End If

        End If

        cmd.IsEnabled = False

    End Sub

    ''' <summary>
    ''' Updates the Select Stratum command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnUpdateSelectStratumCommand(ByVal cmd As Command)

        If (Me.TabStripMain.Items.Count = 0) Then
            cmd.IsEnabled = False
        End If

        cmd.IsEnabled = True

    End Sub

    ''' <summary>
    ''' Executes the Select Stratum command
    ''' </summary>
    ''' <param name="cmd"></param>
    ''' <remarks></remarks>
    Private Sub OnExecuteSelectStratumCommand(ByVal cmd As Command)
        Me.SelectStratum()
    End Sub

    ''' <summary>
    ''' Allows the user to select a specific stratum
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectStratum()

        Dim frm As New SelectStratumForm
        frm.Initialize(Me.Project, Me.TabStripMain.SelectedItem.Text)

        If (frm.ShowDialog(Me) = DialogResult.OK) Then

            For Each item As TabStripItem In Me.TabStripMain.Items

                If (item.Text = frm.SelectedStratum) Then

                    Me.TabStripMain.SelectItem(item)
                    Exit For

                End If

            Next

        End If

        frm.Dispose()

    End Sub

    ''' <summary>
    ''' Zooms the current diagram in
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ZoomIn(sender As System.Object, e As System.EventArgs) Handles ButtonZoomIn.Click
        Me.GetCurrentDiagram.ZoomIn()
    End Sub

    ''' <summary>
    ''' Zoooms the current diagram out
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ZoomOut(sender As System.Object, e As System.EventArgs) Handles ButtonZoomOut.Click
        Me.GetCurrentDiagram.ZoomOut()
    End Sub

    ''' <summary>
    ''' Handles the First button Clicked event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButtonFirst_Click(sender As System.Object, e As System.EventArgs) Handles ButtonFirst.Click
        Me.TabStripMain.SelectFirstItem()
    End Sub

    ''' <summary>
    ''' Handles the Previous button Clicked event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButtonPrevious_Click(sender As System.Object, e As System.EventArgs) Handles ButtonPrevious.Click
        Me.TabStripMain.SelectPreviousItem()
    End Sub

    ''' <summary>
    ''' Handles the Next button Clicked event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButtonNext_Click(sender As System.Object, e As System.EventArgs) Handles ButtonNext.Click
        Me.TabStripMain.SelectNextItem()
    End Sub

    ''' <summary>
    ''' Handles the Last button Clicked event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButtonLast_Click(sender As System.Object, e As System.EventArgs) Handles ButtonLast.Click
        Me.TabStripMain.SelectLastItem()
    End Sub

    ''' <summary>
    ''' Handles the Search button Clicked event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButtonSelectStratum_Click(sender As System.Object, e As System.EventArgs) Handles ButtonSelectStratum.Click
        Me.SelectStratum()
    End Sub

    ''' <summary>
    ''' Handles the selected tab item changing event
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OnSelectedTabItemChanging(sender As System.Object, ByVal e As SelectedTabStripItemChangingEventArgs) Handles TabStripMain.SelectedItemChanging

        'The first time we get this event there will be nothing to validate
        If (Me.TabStripMain.SelectedItem Is Nothing Or (Me.PanelControlHost.Controls.Count = 0)) Then
            Return
        End If

        Dim item As TransitionDiagramTabStripItem = CType(Me.TabStripMain.SelectedItem, TransitionDiagramTabStripItem)

        If (Not TypeOf (item) Is StratumTabStripItem) Then

            Dim v As SyncroSimView = CType(Me.PanelControlHost.Controls(0), SyncroSimView)

            If (Not v.Validate()) Then
                e.Cancel = True
            End If

        End If

    End Sub

    ''' <summary>
    ''' Handles the selected tab item changed event
    ''' </summary>
    ''' <remarks>If we are loading the cursor is already the hourglass and we don't want to change that here...</remarks>
    Private Sub OnSelectedTabItemChanged(sender As System.Object, e As SelectedTabStripItemChangedEventArgs) Handles TabStripMain.SelectedItemChanged

        If (Not Me.m_IsLoading) Then

            Using h As New HourGlass
                Me.OnSelectedTabItemChanged()
            End Using

        Else
            Me.OnSelectedTabItemChanged()
        End If

    End Sub

    ''' <summary>
    ''' Handles the selected tab item changed event
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OnSelectedTabItemChanged()

        Dim item As TransitionDiagramTabStripItem = CType(Me.TabStripMain.SelectedItem, TransitionDiagramTabStripItem)

        If (TypeOf (item) Is StratumTabStripItem) Then
            Me.ActivateStratumTabStripItem(CType(item, StratumTabStripItem))
        Else
            Me.ActivateTransitionsTabStripItem(item)
        End If

        Me.SetCurrentControl(item.Control)

    End Sub

    ''' <summary>
    ''' Paints the split container
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks>We custom paint the splitter rectangle to make it easier to see</remarks>
    Private Sub OnPaintSplitContainer(sender As System.Object, e As System.Windows.Forms.PaintEventArgs) Handles SplitContainerTabStrip.Paint

        Dim rc As New Rectangle(
            Me.SplitContainerTabStrip.SplitterRectangle.Left,
            Me.SplitContainerTabStrip.SplitterRectangle.Top,
            Me.SplitContainerTabStrip.SplitterRectangle.Width - 1,
            Me.SplitContainerTabStrip.SplitterRectangle.Height - 1)

        Dim brs As New System.Drawing.Drawing2D.LinearGradientBrush(rc, Color.SteelBlue, Color.White, Drawing2D.LinearGradientMode.Horizontal)

        e.Graphics.FillRectangle(brs, rc)
        e.Graphics.DrawRectangle(Pens.SteelBlue, rc)
        rc.Inflate(-1, -1)
        e.Graphics.DrawRectangle(Pens.White, rc)

        brs.Dispose()

    End Sub

    ''' <summary>
    ''' Handles the scroll event for the vertical scroll bar
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OnVerticalScroll(sender As System.Object, e As System.Windows.Forms.ScrollEventArgs) Handles ScrollBarVertical.Scroll

        If (e.NewValue = e.OldValue) Then
            Return
        End If

        Dim item As TransitionDiagramTabStripItem = CType(Me.TabStripMain.SelectedItem(), TransitionDiagramTabStripItem)

        If (TypeOf (item) Is StratumTabStripItem) Then

            Dim d As Diagram = CType(item.Control, Diagram)

            d.VerticalScrollValue = e.NewValue
            d.Invalidate()

        End If

    End Sub

    ''' <summary>
    ''' Handles the scroll event for the horizontal scroll bar
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OnHorizontalScroll(sender As System.Object, e As System.Windows.Forms.ScrollEventArgs) Handles ScrollBarHorizontal.Scroll

        If (e.NewValue = e.OldValue) Then
            Return
        End If

        Dim item As TransitionDiagramTabStripItem = CType(Me.TabStripMain.SelectedItem(), TransitionDiagramTabStripItem)

        If (TypeOf (item) Is StratumTabStripItem) Then

            Dim d As Diagram = CType(item.Control, Diagram)

            d.HorizontalScrollValue = e.NewValue
            d.Invalidate()

        End If

    End Sub

    ''' <summary>
    ''' Handles the MouseDoubleClick event 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OnDiagramMouseDoubleClick(ByVal sender As Object, ByVal e As MouseEventArgs)

        Dim item As TransitionDiagramTabStripItem = CType(Me.TabStripMain.SelectedItem(), TransitionDiagramTabStripItem)

        If (TypeOf (item) Is StratumTabStripItem) Then

            Dim d As TransitionDiagram = CType(item.Control, TransitionDiagram)

            If (d.CanOpenStateClasses()) Then
                Me.OpenSelectedStateClasses()
            End If

        End If

    End Sub

    ''' <summary>
    ''' Changes the diagram zoom for the specified diagram
    ''' </summary>
    ''' <param name="diagram"></param>
    ''' <param name="zoom"></param>
    ''' <remarks></remarks>
    Private Sub ChangeDiagramZoom(ByVal diagram As BoxArrowDiagram, zoom As Single)

        If (zoom = diagram.MinimumZoom Or
            zoom = diagram.MaximumZoom) Then

            Return

        End If

        Dim hnormal As Single = diagram.HorizontalScrollValue / Me.m_CurrentZoom
        Dim vnormal As Single = diagram.VerticalScrollValue / Me.m_CurrentZoom

        diagram.HorizontalScrollValue = CInt(hnormal * zoom)
        diagram.VerticalScrollValue = CInt(vnormal * zoom)

    End Sub

    ''' <summary>
    ''' Changes the zoom for the diagram contained in the specified tab strip item
    ''' </summary>
    ''' <param name="item"></param>
    ''' <param name="zoom"></param>
    ''' <remarks></remarks>
    Private Sub ChangeDiagramZoom(ByVal item As StratumTabStripItem, ByVal zoom As Single)

        If (item.Control Is Nothing) Then
            Return
        End If

        Dim i As StratumTabStripItem = CType(item, StratumTabStripItem)
        Dim d As BoxArrowDiagram = CType(i.Control, BoxArrowDiagram)

        Me.ChangeDiagramZoom(d, zoom)

    End Sub

    ''' <summary>
    ''' Handles the zoom changed event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' We need to handle this event here so we can synchronize zoom states across all diagrams
    ''' </remarks>
    Private Sub OnDiagramZoomChanged(ByVal sender As Object, ByVal e As EventArgs)

        Dim CurDiag As BoxArrowDiagram = Me.GetCurrentDiagram()

        For Each Item As TabStripItem In Me.TabStripMain.Items

            If (TypeOf (Item) Is StratumTabStripItem) Then
                ChangeDiagramZoom(CType(Item, StratumTabStripItem), CurDiag.Zoom)
            End If

        Next

        Me.m_CurrentZoom = CurDiag.Zoom
        Me.ResetScrollbars()

    End Sub

    ''' <summary>
    ''' Gets a tab strip item with the specified name
    ''' </summary>
    ''' <param name="itemName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetItemByName(ByVal itemName As String) As TabStripItem

        For Each item As TabStripItem In Me.TabStripMain.Items

            If (item.Text = itemName) Then
                Return item
            End If

        Next

        Return Nothing

    End Function

    ''' <summary>
    ''' Gets the tab for the first stratum with data
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetFirstStratumTabWithData() As TabStripItem

        Dim Analyzer As New DTAnalyzer(Me.Scenario.GetDataSheet(DATASHEET_DT_NAME).GetData(), Me.Project)

        For Each item In Me.TabStripMain.Items

            If (TypeOf (item) Is StratumTabStripItem) Then

                If (Analyzer.StratumHasData(CType(item, StratumTabStripItem).StratumId)) Then
                    Return item
                End If

            End If

        Next

        Return Nothing

    End Function

    ''' <summary>
    ''' Refreshes the tab strip controls
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RefreshTabStripControls()

        Dim NewSelectedItem As TabStripItem = Nothing
        Dim OldSelectedItem As TabStripItem = Me.TabStripMain.SelectedItem

        Me.RefreshAllTabItems()

        If (OldSelectedItem IsNot Nothing) Then
            NewSelectedItem = Me.GetItemByName(OldSelectedItem.Text)
        End If

        If (NewSelectedItem Is Nothing) Then
            NewSelectedItem = Me.GetFirstStratumTabWithData()
        End If

        If (NewSelectedItem Is Nothing) Then

            If (Me.TabStripMain.Items.Count > 0) Then
                NewSelectedItem = Me.TabStripMain.Items(0)
            End If

        End If

        If (NewSelectedItem IsNot Nothing) Then
            Me.TabStripMain.SelectItem(NewSelectedItem)
        End If

    End Sub

    ''' <summary>
    ''' Set the current control into the content panel
    ''' </summary>
    ''' <param name="c"></param>
    ''' <remarks></remarks>
    Private Sub SetCurrentControl(ByVal c As Control)

        Me.PanelControlHost.Controls.Clear()
        Me.PanelControlHost.Controls.Add(c)

        c.Dock = DockStyle.Fill
        c.Parent = Me.PanelControlHost

    End Sub

    ''' <summary>
    ''' Loads the transition diagram tab items
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RefreshAllTabItems()

        Me.DisposeTabStripItems()
        Me.TabStripMain.Items.Clear()

        Dim ds As DataSheet = Me.Project.GetDataSheet(DATASHEET_STRATA_NAME)
        Dim dv As New DataView(ds.GetData(), Nothing, ds.DisplayMember, DataViewRowState.CurrentRows)

        Me.TabStripMain.BeginAddItems()

        Me.TabStripMain.Items.Add(New StratumTabStripItem(DIAGRAM_ALL_STRATA_DISPLAY_NAME, Nothing))

        For Each v As DataRowView In dv

            Dim dr As DataRow = v.Row

            Me.TabStripMain.Items.Add(New StratumTabStripItem(
                CStr(dr(ds.DisplayMember)),
                CInt(dr(ds.ValueMember))))

        Next

        Me.TabStripMain.Items.Add(New DeterministicTransitionsTabStripItem())
        Me.TabStripMain.Items.Add(New ProbabilisticTransitionsTabStripItem())

        Me.TabStripMain.EndAddItems()

    End Sub

    ''' <summary>
    ''' Refreshes all transition diagrams
    ''' </summary>
    ''' <remarks>
    ''' Not all diagrams are actually refreshed.  All of them except for the selected diagram (if there is one) are only
    ''' queued for a refresh which will happen if the user clicks that diagram's tab.  This is done because loading a diagram
    ''' can be slow and there is no need to load one until the user wants to see it.
    ''' </remarks>
    Private Sub RefreshTransitionDiagrams()

        For Each item As TransitionDiagramTabStripItem In Me.TabStripMain.Items

            If (TypeOf (item) Is StratumTabStripItem) Then
                item.RefreshRequired = True
            End If

        Next

        If (TypeOf (Me.TabStripMain.SelectedItem) Is StratumTabStripItem) Then
            Me.OnSelectedTabItemChanged(Me, Nothing)
        End If

    End Sub

    ''' <summary>
    ''' Activates the specified stratum tab strip item
    ''' </summary>
    ''' <param name="item"></param>
    ''' <remarks></remarks>
    Private Sub ActivateStratumTabStripItem(ByVal item As StratumTabStripItem)

        If (item.RefreshRequired()) Then

            Dim d As TransitionDiagram = Nothing

            If (item.Control Is Nothing) Then

                d = New TransitionDiagram(item.StratumId, Me.DataFeed)

                AddHandler d.ZoomChanged, AddressOf OnDiagramZoomChanged
                AddHandler d.MouseDoubleClick, AddressOf OnDiagramMouseDoubleClick

                item.Control = d

            Else
                d = CType(item.Control, TransitionDiagram)
            End If

            d.IsReadOnly = (Not Me.m_IsEnabled)
            d.ShowGrid = Me.m_ShowGrid
            d.ShowToolTips = Me.m_ShowTooltips

            d.RefreshDiagram()
            d.FilterTransitions(Me.m_FilterCriteria)
            ChangeDiagramZoom(d, Me.m_CurrentZoom)

            item.RefreshRequired = False

        End If

        Me.ButtonZoomIn.Enabled = True
        Me.ButtonZoomOut.Enabled = True
        Me.ScrollBarVertical.Visible = True
        Me.ScrollBarHorizontal.Enabled = True
        Me.ScrollBarVertical.Value = CType(item.Control, TransitionDiagram).VerticalScrollValue
        Me.ScrollBarHorizontal.Value = CType(item.Control, TransitionDiagram).HorizontalScrollValue
        Me.PanelControlHost.Width = Me.PanelBottomControls.Width - Me.ScrollBarVertical.Width - 2

        Me.ResetScrollbars()

    End Sub

    ''' <summary>
    ''' Activates the specified transitions tab strip item
    ''' </summary>
    ''' <param name="item"></param>
    ''' <remarks></remarks>
    Private Sub ActivateTransitionsTabStripItem(ByVal item As TransitionDiagramTabStripItem)

        If (item.RefreshRequired()) Then

            Dim v As MultiRowDataFeedView

            If (item.Control Is Nothing) Then

                v = Me.Session.CreateMultiRowDataFeedView(Me.DataFeed.Scenario, Me.ControllingScenario)

                v.ShowBorder = False

                If (TypeOf (item) Is DeterministicTransitionsTabStripItem) Then
                    v.LoadDataFeed(Me.DataFeed, DATASHEET_DT_NAME)
                Else
                    v.LoadDataFeed(Me.DataFeed, DATASHEET_PT_NAME)
                End If

                item.Control = v

            Else
                v = CType(item.Control, MultiRowDataFeedView)
            End If

            v.EnableView(Me.m_IsEnabled)
            item.RefreshRequired = False

        End If

        Me.ButtonZoomIn.Enabled = False
        Me.ButtonZoomOut.Enabled = False
        Me.PanelControlHost.Width = Me.PanelBottomControls.Width
        Me.ScrollBarVertical.Visible = False
        Me.ScrollBarHorizontal.Enabled = False

    End Sub

    ''' <summary>
    ''' Resets the scroll bars for the current tab strip item
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ResetScrollbars()

        If (Me.TabStripMain.SelectedItem Is Nothing) Then
            Return
        End If

        Dim Item As TransitionDiagramTabStripItem = CType(Me.TabStripMain.SelectedItem, TransitionDiagramTabStripItem)

        If (Item.Control Is Nothing) Then
            Return
        End If

        If (Not TypeOf (Item) Is StratumTabStripItem) Then
            Return
        End If

        Dim diag As BoxArrowDiagram = CType(Item.Control, BoxArrowDiagram)

        Me.ResetHorizontalScrollbar(diag)
        Me.ResetVerticalScrollbar(diag)

    End Sub

    ''' <summary>
    ''' Resets the horizontal scroll bar
    ''' </summary>
    ''' <param name="diag"></param>
    ''' <remarks></remarks>
    Private Sub ResetHorizontalScrollbar(ByVal diag As BoxArrowDiagram)

        Dim zoom As Single = diag.Zoom
        Dim diagwid As Single = diag.WorkspaceRectangle.Width * zoom
        Dim clientwid As Single = diag.ClientSize.Width
        Dim Extra As Integer = CInt(30 * zoom)

        If (clientwid >= diagwid) Then

            Me.ScrollBarHorizontal.Enabled = False
            Me.ScrollBarHorizontal.Value = 0
            diag.HorizontalScrollValue = 0

        Else

            Me.ScrollBarHorizontal.Enabled = True

            Me.ScrollBarHorizontal.Minimum = 0
            Me.ScrollBarHorizontal.Maximum = CInt(diagwid - clientwid + Extra)
            Me.ScrollBarHorizontal.SmallChange = CInt(diag.CellSize * zoom)
            Me.ScrollBarHorizontal.LargeChange = CInt(diag.CellSize * zoom)

            If (Me.ScrollBarHorizontal.SmallChange = 0) Then
                Me.ScrollBarHorizontal.SmallChange = 1
            End If

            If (Me.ScrollBarHorizontal.LargeChange = 0) Then
                Me.ScrollBarHorizontal.LargeChange = 1
            End If

            Me.ScrollBarHorizontal.Maximum += CInt(Me.ScrollBarHorizontal.LargeChange)

            If (diag.HorizontalScrollValue <= ScrollBarHorizontal.Maximum) Then
                diag.HorizontalScrollValue = diag.HorizontalScrollValue
            End If

        End If

    End Sub

    ''' <summary>
    ''' Resets the vertical scroll bar
    ''' </summary>
    ''' <param name="diag"></param>
    ''' <remarks></remarks>
    Private Sub ResetVerticalScrollbar(ByVal diag As BoxArrowDiagram)

        Dim zoom As Single = diag.Zoom
        Dim diaghgt As Single = diag.WorkspaceRectangle.Height * zoom
        Dim clienthgt As Single = diag.ClientSize.Height
        Dim Extra As Integer = CInt(30 * zoom)

        If (clienthgt >= diaghgt) Then

            Me.ScrollBarVertical.Enabled = False
            Me.ScrollBarVertical.Value = 0
            diag.VerticalScrollValue = 0

        Else

            Me.ScrollBarVertical.Enabled = True

            Me.ScrollBarVertical.Minimum = 0
            Me.ScrollBarVertical.Maximum = CInt(diaghgt - clienthgt + Extra)
            Me.ScrollBarVertical.SmallChange = CInt(diag.CellSize * zoom)
            Me.ScrollBarVertical.LargeChange = CInt(diag.CellSize * zoom)

            If (Me.ScrollBarVertical.SmallChange = 0) Then
                Me.ScrollBarVertical.SmallChange = 1
            End If

            If (Me.ScrollBarVertical.LargeChange = 0) Then
                Me.ScrollBarVertical.LargeChange = 1
            End If

            Me.ScrollBarVertical.Maximum += CInt(Me.ScrollBarVertical.LargeChange)

            If (diag.VerticalScrollValue <= ScrollBarVertical.Maximum) Then
                diag.VerticalScrollValue = diag.VerticalScrollValue
            End If

        End If

    End Sub

    ''' <summary>
    ''' Creates a quick view title for the current set of selected state clases
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateQuickViewTitle() As String

        Dim sb As New StringBuilder()
        Dim ds As DataSheet = Me.DataFeed.Project.GetDataSheet(DATASHEET_STATECLASS_NAME)
        Dim d As TransitionDiagram = Me.GetCurrentDiagram()

        sb.AppendFormat(CultureInfo.CurrentCulture, "{0} - ", Me.DataFeed.Scenario.DisplayName)

        For Each s As StateClassShape In d.SelectedShapes

            sb.AppendFormat(CultureInfo.CurrentCulture, "{0},",
                CStr(DataTableUtilities.GetTableValue(ds.GetData(), ds.ValueMember, s.StateClassIdSource, ds.DisplayMember)))

        Next

        Return sb.ToString().Trim(CChar(","))

    End Function

    ''' <summary>
    ''' Creates a quick view tag for the current set of selected state clases
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Note that shape selection order will affect the tag so we need to sort the Ids before using them.</remarks>
    Private Function CreateQuickViewTag() As String

        Dim sb As New StringBuilder()
        Dim dsst As DataSheet = Me.DataFeed.Project.GetDataSheet(DATASHEET_STRATA_NAME)
        Dim dssc As DataSheet = Me.DataFeed.Project.GetDataSheet(DATASHEET_STATECLASS_NAME)
        Dim d As TransitionDiagram = Me.GetCurrentDiagram()

        If (d.StratumId.HasValue) Then

            sb.AppendFormat(CultureInfo.CurrentCulture, "{0}-{1}:", d.StratumId.Value,
                CStr(DataTableUtilities.GetTableValue(dsst.GetData(), dsst.ValueMember, d.StratumId.Value, dsst.DisplayMember)))

        Else
            sb.Append("NULL-NULL")
        End If

        Dim lst As New List(Of Integer)

        For Each s As StateClassShape In d.SelectedShapes
            lst.Add(s.StateClassIdSource)
        Next

        lst.Sort()

        For Each i As Integer In lst

            For Each s As StateClassShape In d.SelectedShapes

                If (s.StateClassIdSource = i) Then

                    Dim DisplayValue As String = CStr(DataTableUtilities.GetTableValue(
                        dssc.GetData(), dssc.ValueMember, s.StateClassIdSource, dssc.DisplayMember))

                    sb.AppendFormat(CultureInfo.CurrentCulture, "{0}-{1}:", s.StateClassIdSource, DisplayValue)

                End If

            Next

        Next

        Dim StateClasses As String = sb.ToString().Trim(CChar(":"))
        Return String.Format(CultureInfo.InvariantCulture, "{0}:{1}", Me.Project.Library.Connection.ConnectionString, StateClasses)

    End Function

    ''' <summary>
    ''' Opens the selected state classes
    ''' </summary>
    ''' <remarks>
    ''' DEVTODO: The tag we create below will not work if two libraries have the same state class and 
    ''' stratum Ids.  However, the worst that will happen is that the wrong quick view will be activated...
    ''' </remarks>
    Private Sub OpenSelectedStateClasses()

        Dim lst As New List(Of Integer)
        Dim d As TransitionDiagram = Me.GetCurrentDiagram()
        Dim title As String = Me.CreateQuickViewTitle()
        Dim tag As String = Me.CreateQuickViewTag()

        If (Me.Session.Application.GetView(tag) IsNot Nothing) Then
            Me.Session.Application.ActivateView(tag)
        Else

            Using h As New HourGlass

                For Each s As StateClassShape In d.SelectedShapes
                    lst.Add(s.StateClassIdSource)
                Next

                Dim v As StateClassQuickView = CType(Me.Session.CreateDataFeedView(
                    GetType(StateClassQuickView), Me.Library, Me.Project, Me.Scenario, Nothing), StateClassQuickView)

                v.LoadStateClasses(d.StratumId, lst, Me.DataFeed, tag)
                Me.Session.Application.HostView(v, title, tag)

            End Using

        End If

    End Sub

End Class
