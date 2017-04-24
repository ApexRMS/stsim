'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Imports System.Drawing
Imports System.Windows.Forms
Imports System.Globalization
Imports SyncroSim.Core
Imports SyncroSim.Common.Forms

Class TransitionDiagram
    Inherits BoxArrowDiagram

    Private m_StratumId As Nullable(Of Integer) = Nothing
    Private m_DataFeed As DataFeed
    Private m_SLXDataSheet As DataSheet
    Private m_SLYDataSheet As DataSheet
    Private m_SCDataSheet As DataSheet
    Private m_DTDataSheet As DataSheet
    Private m_PTDataSheet As DataSheet
    Private m_TTGDataSheet As DataSheet
    Private m_ExplicitClasses As New Dictionary(Of Integer, StateClassShape)
    Private m_WildcardClasses As New Dictionary(Of Integer, StateClassShape)
    Private m_IsFilterApplied As Boolean
    Private m_SelectionStatic As Boolean = True
    Private m_SelectedStateClasses As New Dictionary(Of String, StateClassShape)

    Public Sub New(ByVal stratumId As Nullable(Of Integer), ByVal dataFeed As DataFeed)
        Me.InternalConstruct(stratumId, dataFeed)
    End Sub

    Public ReadOnly Property StratumId As Nullable(Of Integer)
        Get
            Return Me.m_StratumId
        End Get
    End Property

    Public Sub RefreshDiagram()
        Me.InternalRefresh()
    End Sub

    Public Sub AddStateClass()
        Me.InternalAddStateClass()
    End Sub

    Public Sub EditStateClass()
        Me.InternalEditStateClass()
    End Sub

    Public Function CanOpenStateClasses() As Boolean

        If (Me.m_StratumId.HasValue) Then
            Return (Me.SelectedShapes.Count > 0 And Not Me.IsReadOnly And Not Me.m_SelectionStatic)
        Else
            Return (Me.SelectedShapes.Count > 0 And Not Me.IsReadOnly)
        End If

    End Function

    Public Function CanCutStateClasses() As Boolean
        Return (Me.SelectedShapes.Count > 0 And Not Me.IsReadOnly And Not Me.m_SelectionStatic)
    End Function

    Public Sub CutStateClasses()
        Me.InternalCutStateClasses()
    End Sub

    Public Function CanCopyStateClasses() As Boolean
        Return (Me.SelectedShapes.Count > 0)
    End Function

    Public Sub CopyStateClasses()
        Me.InternalCopyToClip()
    End Sub

    Public Function CanPasteStateClasses() As Boolean
        Return Me.InternalCanPasteStateClasses()
    End Function

    Public Sub PasteStateClasses(ByVal targeted As Boolean)
        Me.InternalPasteSpecial(True, False, False, True, True, targeted)
    End Sub

    Public Sub PasteStateClassesSpecial(
        ByVal pasteTransitionsAll As Boolean,
        ByVal pasteTransitionsBetween As Boolean,
        ByVal pasteTransitionsNone As Boolean,
        ByVal pasteTransitionsDeterministic As Boolean,
        ByVal pasteTransitionsProbabilistic As Boolean,
        ByVal targeted As Boolean)

        Me.InternalPasteSpecial(
            pasteTransitionsAll,
            pasteTransitionsBetween,
            pasteTransitionsNone,
            pasteTransitionsDeterministic,
            pasteTransitionsProbabilistic,
            targeted)

    End Sub

    Public Sub DeleteStateClasses()
        Me.InternalDeleteStateClasses()
    End Sub

    Public Function CanSelectAllStateClasses() As Boolean
        Return (Me.Shapes.Count > 0)
    End Function

    Public Sub SelectAllStateClasses()
        Me.SelectAllShapes()
    End Sub

    Public Sub SelectStateClass(ByVal stateClassId As Integer)
        Me.InternalSelectStateClass(stateClassId)
    End Sub

    Public Sub FilterTransitions(ByVal criteria As TransitionFilterCriteria)
        Me.InternalFilterTransitions(criteria)
    End Sub

    Public Sub ApplyReadonlySettings()
        Me.InternalConfigureReadOnly()
    End Sub

    Private Sub InternalConstruct(
       ByVal stratumId As Nullable(Of Integer),
       ByVal dataFeed As DataFeed)

        Me.m_StratumId = stratumId
        Me.m_DataFeed = dataFeed

        Me.Rows = TRANSITION_DIAGRAM_MAX_ROWS
        Me.Columns = TRANSITION_DIAGRAM_MAX_COLUMNS
        Me.CellPadding = TRANSITION_DIAGRAM_SHAPE_PADDING
        Me.BoxSize = TRANSITION_DIAGRAM_SHAPE_SIZE
        Me.LanesBetweenShapes = TRANSITION_DIAGRAM_LANES_BETWEEN_SHAPES

        Me.m_SLXDataSheet = dataFeed.Project.GetDataSheet(DATASHEET_STATE_LABEL_X_NAME)
        Me.m_SLYDataSheet = dataFeed.Project.GetDataSheet(DATASHEET_STATE_LABEL_Y_NAME)
        Me.m_SCDataSheet = dataFeed.Project.GetDataSheet(DATASHEET_STATECLASS_NAME)
        Me.m_DTDataSheet = dataFeed.Scenario.GetDataSheet(DATASHEET_DT_NAME)
        Me.m_PTDataSheet = dataFeed.Scenario.GetDataSheet(DATASHEET_PT_NAME)
        Me.m_TTGDataSheet = dataFeed.Project.GetDataSheet(DATASHEET_TRANSITION_TYPE_GROUP_NAME)

        Me.AutoScroll = False

    End Sub

    Private Sub SaveSelection()

        Me.m_SelectedStateClasses.Clear()

        For Each Shape As StateClassShape In Me.Shapes

            If (Shape.IsSelected) Then

                Dim k As String = DTAnalyzer.CreateStateClassLookupKey(Shape.StratumIdSource, Shape.StateClassIdSource)
                Me.m_SelectedStateClasses.Add(k, Shape)

            End If

        Next

    End Sub

    Private Sub RestoreSelection()

        For Each Shape As StateClassShape In Me.Shapes

            Dim k As String = DTAnalyzer.CreateStateClassLookupKey(Shape.StratumIdSource, Shape.StateClassIdSource)

            If (Me.m_SelectedStateClasses.ContainsKey(k)) Then
                Shape.IsSelected = True
            Else
                Shape.IsSelected = False
            End If

        Next

    End Sub

    Private Sub InternalCutStateClasses()

        Me.CopyStateClasses()
        Me.DeleteStateClasses()

    End Sub

    Private Sub InternalDeleteStateClasses()

        Me.m_DTDataSheet.BeginDeleteRows()
        Me.m_PTDataSheet.BeginDeleteRows()

        Dim Analyzer As New DTAnalyzer(Me.m_DTDataSheet.GetData(), Me.m_DataFeed.Project)

        For Each Shape As StateClassShape In Me.SelectedShapes

            If (Not Shape.IsStatic) Then

                Dim row As DataRow = Analyzer.GetStateClassRow(Shape.StratumIdSource, Shape.StateClassIdSource)

                If (row.RowState = DataRowState.Added) Then
                    Me.m_DTDataSheet.GetData().Rows.Remove(row)
                Else
                    row.Delete()
                End If

            End If

        Next

        Me.m_DTDataSheet.EndDeleteRows()
        Me.m_PTDataSheet.EndDeleteRows()

    End Sub

    Private Sub InternalSelectStateClass(ByVal stateClassId As Integer)

        If (Me.m_ExplicitClasses.ContainsKey(stateClassId)) Then

            Dim Shape As StateClassShape = Me.m_ExplicitClasses(stateClassId)
            Me.SelectShape(Shape)

        End If

    End Sub

    Private Function InternalCanPasteStateClasses() As Boolean

        If (Me.IsReadOnly) Then
            Return False
        Else
            Dim dobj As DataObject = CType(Clipboard.GetDataObject(), DataObject)
            Return dobj.GetDataPresent(CLIPBOARD_FORMAT_TRANSITION_DIAGRAM)
        End If

    End Function

    Private Sub InternalRefresh()

        Me.SaveSelection()
        Me.InternalRefreshStateClasses()
        Me.InternalRefreshLookups()
        Me.InternalRefreshTransitionLines()
        Me.ApplyReadonlySettings()
        Me.RestoreSelection()

        Me.Invalidate()

    End Sub

    Private Sub InternalAddStateClass()

        Dim dlg As New ChooseStateClassForm()

        If (Not dlg.Initialize(Me, Me.m_DataFeed, False)) Then
            Return
        End If

        If (dlg.ShowDialog(Me) <> DialogResult.OK) Then
            Return
        End If

        Dim Location As String = Me.GetNextStateClassLocation()

        If (Location Is Nothing) Then
            FormsUtilities.ErrorMessageBox(ERROR_DIAGRAM_NO_MORE_LOCATIONS)
            Return
        End If

        Dim StateClassId As Integer = Me.GetStateClassId(dlg.ChosenStateLabelX.Value, dlg.ChosenStateLabelY.Value)

        If (StateClassId = -1) Then
            StateClassId = Me.InternalCreateNewStateClass(dlg.ChosenStateLabelX, dlg.ChosenStateLabelY)
        End If

        Me.m_DTDataSheet.BeginAddRows()
        Dim dr As DataRow = Me.m_DTDataSheet.GetData().NewRow()

        dr(DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(Me.m_StratumId)
        dr(DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME) = StateClassId
        dr(DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME) = DBNull.Value
        dr(DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME) = DBNull.Value
        dr(DATASHEET_AGE_MIN_COLUMN_NAME) = DBNull.Value
        dr(DATASHEET_AGE_MAX_COLUMN_NAME) = DBNull.Value
        dr(DATASHEET_DT_LOCATION_COLUMN_NAME) = Location

        Me.m_DTDataSheet.GetData().Rows.Add(dr)
        Me.m_DTDataSheet.EndAddRows()

        Me.RefreshDiagram()
        Me.SelectStateClass(StateClassId)

    End Sub

    Private Sub InternalEditStateClass()

        Debug.Assert(Me.SelectedShapes.Count = 1)

        Dim dlg As New ChooseStateClassForm()
        dlg.Initialize(Me, Me.m_DataFeed, True)

        If (dlg.ShowDialog(Me) <> DialogResult.OK) Then
            Return
        End If

        Dim EditShape As StateClassShape = CType(Me.SelectedShapes.First, StateClassShape)
        Dim OldStateClassId As Integer = Me.GetStateClassId(EditShape.StateLabelXId, EditShape.StateLabelYId)
        Dim NewStateClassId As Integer = Me.GetStateClassId(dlg.ChosenStateLabelX.Value, dlg.ChosenStateLabelY.Value)

        If (OldStateClassId = NewStateClassId) Then
            Return
        End If

        If (NewStateClassId = -1) Then
            NewStateClassId = Me.InternalCreateNewStateClass(dlg.ChosenStateLabelX, dlg.ChosenStateLabelY)
        End If

        Me.m_DTDataSheet.BeginModifyRows()
        Me.m_PTDataSheet.BeginModifyRows()

        InternalChangeStateClassId(
            Me.m_DTDataSheet,
            DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME,
            DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME,
            DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME,
            DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME,
            OldStateClassId, NewStateClassId, Me.m_StratumId)

        InternalChangeStateClassId(
            Me.m_PTDataSheet,
            DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME,
            DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME,
            DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME,
            DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME,
            OldStateClassId, NewStateClassId, Me.m_StratumId)

        Me.m_DTDataSheet.EndModifyRows()
        Me.m_PTDataSheet.EndModifyRows()

        Me.RefreshDiagram()
        Me.SelectStateClass(NewStateClassId)

    End Sub

    Private Sub InternalFilterTransitions(ByVal criteria As TransitionFilterCriteria)

        Dim dtflt As Boolean = (Not criteria.IncludeDeterministic)
        Dim ptflt As Boolean = (Not criteria.IncludeProbabilistic)
        Dim tgflt As Boolean = IsTransitionGroupFilterApplied(criteria)

        Me.m_IsFilterApplied = (dtflt Or ptflt Or tgflt)

        For Each Line As TransitionDiagramLine In Me.Lines

            If (TypeOf (Line) Is DeterministicTransitionLine) Then
                Line.IsVisible = criteria.IncludeDeterministic
            Else

                Line.IsVisible = criteria.IncludeProbabilistic

                'If no filtering has been done then make the line visible.  But if any filters have been
                'applied then only make the line visible if one of its transition groups is not filtered out.

                If (criteria.IncludeProbabilistic And tgflt) Then

                    Dim ptline As ProbabilisticTransitionLine = CType(Line, ProbabilisticTransitionLine)

                    If (ptline.TransitionGroups.Count > 0) Then

                        ptline.IsVisible = False

                        For Each tg As Integer In ptline.TransitionGroups

                            If (criteria.TransitionGroups(tg)) Then

                                ptline.IsVisible = True
                                Exit For

                            End If

                        Next

                    End If

                End If

            End If

        Next

        Me.Invalidate()

    End Sub

    Private Sub InternalConfigureReadOnly()

        Dim ShapeTextColor As Color
        Dim ShapeBorderColor As Color

        If (Me.IsReadOnly) Then

            ShapeTextColor = TRANSITION_DIAGRAM_READONLY_TEXT_COLOR
            ShapeBorderColor = TRANSITION_DIAGRAM_SHAPE_READONLY_BORDER_COLOR

        Else

            ShapeTextColor = TRANSITION_DIAGRAM_TEXT_COLOR
            ShapeBorderColor = TRANSITION_DIAGRAM_SHAPE_BORDER_COLOR

        End If

        For Each Shape As StateClassShape In Me.Shapes

            Dim IsWild As Boolean = (Me.m_StratumId.HasValue And Not Shape.StratumIdSource.HasValue)

            If (IsWild) Then

                Shape.TitleTextColor = TRANSITION_DIAGRAM_READONLY_TEXT_COLOR
                Shape.BorderColor = TRANSITION_DIAGRAM_SHAPE_READONLY_BORDER_COLOR

            Else

                Shape.TitleTextColor = ShapeTextColor
                Shape.BorderColor = ShapeBorderColor

            End If

            For Each item As DiagramShapeItem In Shape.DiagramShapeItems

                If (IsWild) Then
                    item.ForegroundColor = TRANSITION_DIAGRAM_READONLY_TEXT_COLOR
                Else
                    item.ForegroundColor = ShapeTextColor
                End If

            Next

        Next

        Me.Invalidate()

    End Sub

    Private Sub InternalResetTransitionLines()

        For Each line As TransitionDiagramLine In Me.Lines

            line.IsSelected = False

            If (TypeOf (line) Is DeterministicTransitionLine) Then
                line.LineColor = DETERMINISTIC_TRANSITION_LINE_COLOR
            ElseIf (TypeOf (line) Is ProbabilisticTransitionLine) Then
                line.LineColor = PROBABILISTIC_TRANSITION_LINE_COLOR
            End If

        Next

    End Sub

    Private Sub InternalRefreshLookups()

        Me.m_ExplicitClasses.Clear()
        Me.m_WildcardClasses.Clear()

        For Each Shape As StateClassShape In Me.Shapes

            If (Shape.StratumIdSource.HasValue()) Then
                Me.m_ExplicitClasses.Add(Shape.StateClassIdSource, Shape)
            Else
                Me.m_WildcardClasses.Add(Shape.StateClassIdSource, Shape)
            End If

        Next

        Debug.Assert((Me.m_ExplicitClasses.Count + Me.m_WildcardClasses.Count) = Me.Shapes.Count)

    End Sub

    Private Sub InternalRefreshStateClasses()

        Me.RemoveAllShapes()

        Dim lst As List(Of StateClassShape) = Me.InternalGetStateClassShapes()

        For Each Shape As StateClassShape In lst

            Dim rc As Rectangle = Me.GetShapeRectangleFromRowCol(Shape.Row, Shape.Column)

            Shape.SetDimensions(rc.Width, rc.Height)
            Shape.SetLocation(rc.X, rc.Y)
            Shape.CreateConnectorPoints()

            Me.FillIncomingDT(Shape)
            Me.FillIncomingPT(Shape)
            Me.FillOutgoingPT(Shape)

            If (Shape.IsStatic) Then
                Me.AddShape(Shape, DiagramZOrder.Last)
            Else
                Me.AddShape(Shape, DiagramZOrder.First)
            End If

        Next

        Dim d As New Dictionary(Of Point, StateClassShape)

        For Each Shape As StateClassShape In Me.Shapes

            If (Shape.IsStatic) Then

                Dim p As New Point(Shape.Row, Shape.Column)
                d.Add(p, Shape)

            End If

        Next

        For Each Shape As StateClassShape In Me.Shapes

            If (Not Shape.IsStatic) Then

                Dim p As New Point(Shape.Row, Shape.Column)

                If (d.ContainsKey(p)) Then
                    Shape.SharesLocation = True
                End If

            End If

        Next

    End Sub

    Private Function InternalGetStateClassShapes() As List(Of StateClassShape)

        Dim lst As New List(Of StateClassShape)
        Dim rows() As DataRow = Me.GetDTRows()

        For Each dr As DataRow In rows

            Dim ShapeRow As Integer = -1
            Dim ShapeColumn As Integer = -1
            Dim Location As String = CStr(dr(DATASHEET_DT_LOCATION_COLUMN_NAME))

            LocationToRowCol(Location, ShapeRow, ShapeColumn)

            Dim StratumIdSource As Nullable(Of Integer) = Nothing
            Dim StateClassIdSource As Integer = CInt(dr(DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME))
            Dim StratumIdDestination As Nullable(Of Integer) = Nothing
            Dim StateClassIdDestination As Nullable(Of Integer) = Nothing
            Dim MinimumAge As Nullable(Of Integer) = Nothing
            Dim MaximumAge As Nullable(Of Integer) = Nothing

            Dim StateLabelXId As Integer = CInt(DataTableUtilities.GetTableValue(Me.m_SCDataSheet.GetData, Me.m_SCDataSheet.ValueMember, StateClassIdSource, DATASHEET_STATECLASS_STATE_LABEL_X_ID_COLUMN_NAME))
            Dim StateLabelXName As String = CStr(DataTableUtilities.GetTableValue(Me.m_SLXDataSheet.GetData, Me.m_SLXDataSheet.ValueMember, StateLabelXId, DATASHEET_NAME_COLUMN_NAME))
            Dim StateLabelYId As Integer = CInt(DataTableUtilities.GetTableValue(Me.m_SCDataSheet.GetData, Me.m_SCDataSheet.ValueMember, StateClassIdSource, DATASHEET_STATECLASS_STATE_LABEL_Y_ID_COLUMN_NAME))
            Dim StateLabelYName As String = CStr(DataTableUtilities.GetTableValue(Me.m_SLYDataSheet.GetData, Me.m_SLYDataSheet.ValueMember, StateLabelYId, DATASHEET_NAME_COLUMN_NAME))

            If (dr(DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME) IsNot DBNull.Value) Then
                StratumIdSource = CInt(dr(DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME))
            End If

            If (dr(DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME) IsNot DBNull.Value) Then
                StratumIdDestination = CInt(dr(DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME))
            End If

            If (dr(DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME) IsNot DBNull.Value) Then
                StateClassIdDestination = CInt(dr(DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME))
            End If

            If (dr(DATASHEET_AGE_MIN_COLUMN_NAME) IsNot DBNull.Value) Then
                MinimumAge = CInt(dr(DATASHEET_AGE_MIN_COLUMN_NAME))
            End If

            If (dr(DATASHEET_AGE_MAX_COLUMN_NAME) IsNot DBNull.Value) Then
                MaximumAge = CInt(dr(DATASHEET_AGE_MAX_COLUMN_NAME))
            End If

            Dim slxdesc As String = StateLabelXName
            Dim slydesc As String = StateLabelYName

            Dim o1 As Object = DataTableUtilities.GetTableValue(Me.m_SLXDataSheet.GetData,
                Me.m_SLXDataSheet.ValueMember, StateLabelXId, DATASHEET_DESCRIPTION_COLUMN_NAME)

            Dim o2 As Object = DataTableUtilities.GetTableValue(Me.m_SLYDataSheet.GetData,
                Me.m_SLYDataSheet.ValueMember, StateLabelYId, DATASHEET_DESCRIPTION_COLUMN_NAME)

            If (o1 IsNot DBNull.Value) Then
                slxdesc = CStr(o1)
            End If

            If (o2 IsNot DBNull.Value) Then
                slydesc = CStr(o2)
            End If

            Dim Shape As New StateClassShape(
                Me.m_DataFeed.Project,
                StratumIdSource,
                StateClassIdSource,
                StratumIdDestination,
                StateClassIdDestination,
                MinimumAge,
                MaximumAge,
                StateLabelXId,
                StateLabelXName,
                slxdesc,
                StateLabelYId,
                slydesc)

            Shape.Row = ShapeRow
            Shape.Column = ShapeColumn

            Dim SlyItem As New DiagramShapeItem(StateLabelYName)
            SlyItem.Alignment = DiagramAlignment.Center
            Shape.AddDiagramItem(SlyItem)

            Dim MinAge As Integer = 0

            If (MinimumAge.HasValue) Then
                MinAge = MinimumAge.Value
            End If

            Dim AgeText As String

            If (MaximumAge.HasValue) Then
                AgeText = String.Format(CultureInfo.InvariantCulture, "{0} - {1}", MinAge, MaximumAge.Value)
            Else
                AgeText = String.Format(CultureInfo.InvariantCulture, "{0}+", MinAge)
            End If

            Dim item As New DiagramShapeItem(AgeText)

            item.Alignment = DiagramAlignment.Center
            Shape.AddDiagramItem(item)

            Dim IsWild As Boolean = (Me.m_StratumId.HasValue And Not Shape.StratumIdSource.HasValue)

            If (IsWild) Then

                Shape.IsStatic = True
                Shape.TitleTextColor = TRANSITION_DIAGRAM_READONLY_TEXT_COLOR
                Shape.BorderColor = TRANSITION_DIAGRAM_SHAPE_READONLY_BORDER_COLOR

                For Each i As DiagramShapeItem In Shape.DiagramShapeItems
                    i.ForegroundColor = TRANSITION_DIAGRAM_READONLY_TEXT_COLOR
                Next

            End If

            lst.Add(Shape)

        Next

        Return lst

    End Function

    Private Sub InternalRefreshTransitionLines()

        Me.ClearLines()

        If (Me.Shapes.Count = 0) Then
            Return
        End If

        For Each Shape As BoxDiagramShape In Me.Shapes
            Shape.ResetConnectorPoints()
        Next

        Dim Analyzer As New DTAnalyzer(Me.m_DTDataSheet.GetData(), Me.m_DataFeed.Project)

        Me.CreateDTLines(Analyzer)
        Me.CreateDTOffStratumCues(Analyzer)

        Me.CreatePTLines(Analyzer)
        Me.CreatePTOffStratumCues(Analyzer)

    End Sub

    Private Function InternalCreateNewStateClass(
        ByVal slxitem As BaseValueDisplayListItem,
        ByVal slyitem As BaseValueDisplayListItem) As Integer

        Dim StateClassId As Integer = Me.m_SCDataSheet.GetNextRowIdentity()

        Me.m_SCDataSheet.BeginAddRows()
        Dim NewRow As DataRow = Me.m_SCDataSheet.GetData().NewRow

        NewRow(Me.m_SCDataSheet.ValueMember) = StateClassId
        NewRow(Me.m_SCDataSheet.DisplayMember) = slxitem.Display & ":" & slyitem.Display
        NewRow(DATASHEET_STATECLASS_STATE_LABEL_X_ID_COLUMN_NAME) = slxitem.Value
        NewRow(DATASHEET_STATECLASS_STATE_LABEL_Y_ID_COLUMN_NAME) = slyitem.Value

        Me.m_SCDataSheet.GetData().Rows.Add(NewRow)
        Me.m_SCDataSheet.EndAddRows()

        Return StateClassId

    End Function

    Private Shared Sub InternalChangeStateClassId(
        ByVal dataSheet As DataSheet,
        ByVal fromStratumColName As String,
        ByVal fromStateClassColName As String,
        ByVal toStratumColName As String,
        ByVal toStateClassColname As String,
        ByVal oldStateClassId As Integer,
        ByVal newStateClassId As Integer,
        ByVal currentStratumId As Nullable(Of Integer))

        For Each dr As DataRow In dataSheet.GetData.Rows

            If (dr.RowState = DataRowState.Deleted) Then
                Continue For
            End If

            Dim StratumIdSource As Nullable(Of Integer) = Nothing
            Dim StateClassIdSource As Integer = CInt(dr(fromStateClassColName))
            Dim StratumIdDest As Nullable(Of Integer) = Nothing
            Dim StateClassIdDest As Nullable(Of Integer) = Nothing

            If (dr(fromStratumColName) IsNot DBNull.Value) Then
                StratumIdSource = CInt(dr(fromStratumColName))
            End If

            If (dr(toStratumColName) IsNot DBNull.Value) Then
                StratumIdDest = CInt(dr(toStratumColName))
            End If

            If (dr(toStateClassColname) IsNot DBNull.Value) Then
                StateClassIdDest = CInt(dr(toStateClassColname))
            End If

            'If the FROM state class is the old ID then change it, but ONLY
            'if the FROM stratum is the current stratum.

            If (StateClassIdSource = oldStateClassId) Then

                If (NullableUtilities.NullableIdsEqual(StratumIdSource, currentStratumId)) Then
                    dr(fromStateClassColName) = newStateClassId
                End If

            End If

            'If the TO state class is the old ID then change it, but only
            'if the TO stratum is the current stratum.

            If (Not StateClassIdDest.HasValue) Then
                Continue For
            End If

            If (StateClassIdDest.Value = oldStateClassId) Then

                Dim update As Boolean = False

                If (currentStratumId.HasValue) Then

                    If (StratumIdDest.HasValue) Then
                        update = (StratumIdDest.Value = currentStratumId.Value)
                    Else
                        update = NullableUtilities.NullableIdsEqual(StratumIdSource, currentStratumId.Value)
                    End If

                Else
                    update = (Not StratumIdDest.HasValue)
                End If

                If (update) Then
                    dr(toStateClassColname) = newStateClassId
                End If

            End If

        Next

    End Sub

End Class
