'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core
Imports System.Windows.Forms
Imports SyncroSim.Common.Forms

Partial Class TransitionDiagram

    Private Function GetPrioritizedShapeList() As List(Of StateClassShape)

        Dim l As New List(Of StateClassShape)
        Dim d As New Dictionary(Of Integer, Boolean)

        'The user can copy the same state class if it is in both the wild card stratum 
        'and in the current stratum.  In this case we only copy the one in the current stratum.

        For Each Shape As StateClassShape In Me.SelectedShapes

            If (Shape.StratumIdSource.HasValue) Then

                l.Add(Shape)
                d.Add(Shape.StateClassIdSource, True)

            End If

        Next

        For Each Shape As StateClassShape In Me.SelectedShapes

            If (Not d.ContainsKey(Shape.StateClassIdSource)) Then
                l.Add(Shape)
            End If

        Next

        Debug.Assert(l.Count > 0)
        Return l

    End Function

    Private Sub InternalCopyToClip()

        Dim dobj As New DataObject()
        Dim data As New TransitionDiagramClipData()

        Dim StratumSheet As DataSheet = Me.m_DataFeed.Project.GetDataSheet(DATASHEET_STRATA_NAME)
        Dim StateClassSheet As DataSheet = Me.m_DataFeed.Project.GetDataSheet(DATASHEET_STATECLASS_NAME)
        Dim TransitionTypeSheet As DataSheet = Me.m_DataFeed.Project.GetDataSheet(DATASHEET_TRANSITION_TYPE_NAME)
        Dim PrioritizedShapes As List(Of StateClassShape) = Me.GetPrioritizedShapeList()

        For Each Shape As StateClassShape In PrioritizedShapes

            Dim Entry As New TransitionDiagramClipDataEntry()

            If (Shape.StratumIdSource.HasValue) Then
                Entry.ShapeData.StratumSource = StratumSheet.ValidationTable.GetDisplayName(Shape.StratumIdSource.Value)
            End If

            Entry.ShapeData.StateClassSource = StateClassSheet.ValidationTable.GetDisplayName(Shape.StateClassIdSource)

            If (Shape.StratumIdDest.HasValue()) Then
                Entry.ShapeData.StratumDest = StratumSheet.ValidationTable.GetDisplayName(Shape.StratumIdDest.Value)
            End If

            If (Shape.StateClassIdDest.HasValue) Then
                Entry.ShapeData.StateClassDest = StateClassSheet.ValidationTable.GetDisplayName(Shape.StateClassIdDest.Value)
            End If

            Entry.ShapeData.AgeMin = Shape.AgeMinimum
            Entry.ShapeData.AgeMax = Shape.AgeMaximum
            Entry.Row = Shape.Row
            Entry.Column = Shape.Column
            Entry.Bounds = Shape.Bounds

            For Each t As DeterministicTransition In Shape.IncomingDT
                Entry.IncomingDT.Add(DTToClipFormat(t, StratumSheet, StateClassSheet))
            Next

            For Each t As Transition In Shape.IncomingPT
                Entry.IncomingPT.Add(PTToClipFormat(t, StratumSheet, StateClassSheet, TransitionTypeSheet))
            Next

            For Each t As Transition In Shape.OutgoingPT
                Entry.OutgoingPT.Add(PTToClipFormat(t, StratumSheet, StateClassSheet, TransitionTypeSheet))
            Next

            data.Entries.Add(Entry)

        Next

        dobj.SetData(CLIPBOARD_FORMAT_TRANSITION_DIAGRAM, data)
        Clipboard.SetDataObject(dobj)

    End Sub

    Private Sub InternalPasteSpecial(
        ByVal pasteAll As Boolean,
        ByVal pasteBetween As Boolean,
        ByVal pasteNone As Boolean,
        ByVal pasteDeterministic As Boolean,
        ByVal pasteProbabilistic As Boolean,
        ByVal isTargeted As Boolean)

        'Get the clipboard data and verify that all items can be pasted.  Note that
        'once it has been validated it will contain the correct Ids for those items.

        Dim cd As TransitionDiagramClipData =
            CType(Clipboard.GetData(CLIPBOARD_FORMAT_TRANSITION_DIAGRAM), TransitionDiagramClipData)

        Dim StratumSheet As DataSheet = Me.m_DataFeed.Project.GetDataSheet(DATASHEET_STRATA_NAME)
        Dim StateClassSheet As DataSheet = Me.m_DataFeed.Project.GetDataSheet(DATASHEET_STATECLASS_NAME)
        Dim TransitionTypeSheet As DataSheet = Me.m_DataFeed.Project.GetDataSheet(DATASHEET_TRANSITION_TYPE_NAME)

        If (Not ValidateClipData(cd, StratumSheet, StateClassSheet, TransitionTypeSheet)) Then
            Return
        End If

        'Make sure the user is Ok with overwriting any existing data

        If (Not Me.ConfirmPasteOverwrite(cd)) Then
            Return
        End If

        'Get the paste location deltas for the current paste shapes

        Dim dx As Integer = 0
        Dim dy As Integer = 0

        If (Not Me.GetPasteLocationDeltas(dx, dy, isTargeted)) Then

            If (isTargeted) Then
                FormsUtilities.ErrorMessageBox(ERROR_DIAGRAM_CANNOT_PASTE_SPECIFIC_LOCATION)
            Else
                FormsUtilities.ErrorMessageBox(ERROR_DIAGRAM_CANNOT_PASTE_ANY_LOCATION)
            End If

            Return

        End If

        'Paste all state classes and transitions

        Me.m_DTDataSheet.BeginAddRows()
        Me.m_PTDataSheet.BeginAddRows()

        Dim Analyzer As New DTAnalyzer(Me.m_DTDataSheet.GetData(), Me.m_DataFeed.Project)

        Me.InternalPasteStateClasses(
            cd, dx, dy, pasteNone, Analyzer)

        Me.PasteTransitions(
            cd, pasteAll, pasteBetween, pasteDeterministic, pasteProbabilistic, Analyzer)

        Me.m_DTDataSheet.EndAddRows()
        Me.m_PTDataSheet.EndAddRows()

        Me.DeselectAllShapes()

        For Each Entry As TransitionDiagramClipDataEntry In cd.Entries

            If (Me.m_ExplicitClasses.ContainsKey(Entry.ShapeData.StateClassIdSource)) Then
                Me.SelectShape(Me.m_ExplicitClasses(Entry.ShapeData.StateClassIdSource))
            ElseIf (Me.m_WildcardClasses.ContainsKey(Entry.ShapeData.StateClassIdSource)) Then
                Me.SelectShape(Me.m_WildcardClasses(Entry.ShapeData.StateClassIdSource))
            End If

        Next

        Me.Focus()

    End Sub

    Private Sub InternalPasteStateClasses(
        ByVal cd As TransitionDiagramClipData,
        ByVal dx As Integer,
        ByVal dy As Integer,
        ByVal pasteNone As Boolean,
        ByVal analyzer As DTAnalyzer)

        For Each Entry As TransitionDiagramClipDataEntry In cd.Entries

            Dim d As Dictionary(Of Integer, StateClassShape)

            If (Me.m_StratumId.HasValue) Then
                d = Me.m_ExplicitClasses
            Else
                d = Me.m_WildcardClasses
            End If

            'If the state class being pasted is already in the diagram then just 
            'update the ages.  Otherwise, create a new state class in this stratum.

            If (d.ContainsKey(Entry.ShapeData.StateClassIdSource)) Then
                Me.PasteStateClassesReplace(Entry, analyzer)
            Else
                Me.PasteStateClassesCreateNew(cd, Entry, dx, dy, pasteNone, analyzer)
            End If

        Next

    End Sub

    Private Sub PasteStateClassesReplace(
        ByVal entry As TransitionDiagramClipDataEntry,
        ByVal analyzer As DTAnalyzer)

        Dim dr As DataRow = analyzer.GetStateClassRow(Me.m_StratumId, entry.ShapeData.StateClassIdSource)

        If (entry.ShapeData.AgeMin.HasValue) Then
            dr(DATASHEET_AGE_MIN_COLUMN_NAME) = entry.ShapeData.AgeMin.Value
        Else
            dr(DATASHEET_AGE_MIN_COLUMN_NAME) = DBNull.Value
        End If

        If (entry.ShapeData.AgeMax.HasValue()) Then
            dr(DATASHEET_AGE_MAX_COLUMN_NAME) = entry.ShapeData.AgeMax.Value
        Else
            dr(DATASHEET_AGE_MAX_COLUMN_NAME) = DBNull.Value
        End If

    End Sub

    Private Sub PasteStateClassesCreateNew(
        ByVal cd As TransitionDiagramClipData,
        ByVal entry As TransitionDiagramClipDataEntry,
        ByVal dx As Integer,
        ByVal dy As Integer,
        ByVal pasteNone As Boolean,
        ByVal analyzer As DTAnalyzer)

        Dim TargetRow As Integer = entry.Row + dy
        Dim TargetColumn As Integer = entry.Column + dx

        'If 'PasteNone" or there is no destination state class then 
        'create a transition-to-self in this stratum.

        If (pasteNone Or (Not entry.ShapeData.StateClassIdDest.HasValue)) Then

            Me.CreateDTRecord(
                entry.ShapeData.StateClassIdSource, Nothing, Nothing,
                entry.ShapeData.AgeMin, entry.ShapeData.AgeMax,
                TargetRow, TargetColumn)

            Return

        End If

        'If the destination state class is in the clipboard then create a transition
        'to that state class in this stratum.

        If (ClipContainsStateClass(cd, entry.ShapeData.StateClassIdDest.Value)) Then

            Me.CreateDTRecord(
                 entry.ShapeData.StateClassIdSource, Nothing, entry.ShapeData.StateClassIdDest.Value,
                 entry.ShapeData.AgeMin, entry.ShapeData.AgeMax,
                 TargetRow, TargetColumn)

            Return

        End If

        'Resolve the destination stratum and create a transition to that stratum.  Note that the resolution
        'will fail if the destination state class no longer exists and is not in the wild card stratum.  In
        'this case, create a transition-to-self.

        Dim StratumIdActual As Nullable(Of Integer) = Nothing

        If (analyzer.ResolveStateClassStratum(
            entry.ShapeData.StratumIdSource, entry.ShapeData.StratumIdDest,
            entry.ShapeData.StateClassIdDest.Value, StratumIdActual)) Then

            Me.CreateDTRecord(
                 entry.ShapeData.StateClassIdSource, StratumIdActual, entry.ShapeData.StateClassIdDest.Value,
                 entry.ShapeData.AgeMin, entry.ShapeData.AgeMax,
                 TargetRow, TargetColumn)

        Else

            Me.CreateDTRecord(
                entry.ShapeData.StateClassIdSource, Nothing, Nothing,
                entry.ShapeData.AgeMin, entry.ShapeData.AgeMax,
                TargetRow, TargetColumn)

        End If

    End Sub

    Private Sub PasteTransitions(
        ByVal cd As TransitionDiagramClipData,
        ByVal pasteAll As Boolean,
        ByVal pasteBetween As Boolean,
        ByVal pasteDeterministic As Boolean,
        ByVal pasteProbabilistic As Boolean,
        ByVal analyzer As DTAnalyzer)

        If (pasteAll) Then

            If (pasteDeterministic) Then
                Me.PasteDTIncoming(cd, analyzer)
            End If

            If (pasteProbabilistic) Then
                Me.PastePT(cd, analyzer)
            End If

        ElseIf (pasteBetween) Then

            If (pasteProbabilistic) Then

                Dim AlreadyPasted As New List(Of ProbabilisticTransitionClipData)
                Me.PastePTBetween(cd, AlreadyPasted)

            End If

        End If

    End Sub

    Private Sub PasteDTIncoming(
        ByVal cd As TransitionDiagramClipData,
        ByVal analyzer As DTAnalyzer)

        For Each Entry As TransitionDiagramClipDataEntry In cd.Entries

            If (Not Me.m_ExplicitClasses.ContainsKey(Entry.ShapeData.StateClassIdSource)) Then
                Me.PasteDTIncoming(cd, Entry, analyzer)
            End If

        Next

    End Sub

    ''' <summary>
    ''' Pastes any incoming deterministic transitions
    ''' </summary>
    ''' <param name="cd"></param>
    ''' <param name="entry"></param>
    ''' <remarks>
    ''' Create all incoming deterministic transitions described in the specified clipboard entry as follows:
    ''' 
    ''' (1.) We are not going to look at any state classes that are in the clipboard since the transitions for these state 
    '''      classes were established when the state classes paste was performed.
    ''' 
    ''' (2.) If the transition is coming from a state class in the same diagram (but that state class is not found in the clipboard) then we
    '''      can create that transition.  But we can only do this if the source state class does not have a transition to another state class.
    ''' 
    ''' (3.) If it is a transition coming from an off-stratum state class, make sure the state class still exists in the source stratum.  And, 
    '''      as with a state class in this diagram, only do this if the source state class does not have a transition to another state class.
    ''' </remarks>
    Private Sub PasteDTIncoming(
        ByVal cd As TransitionDiagramClipData,
        ByVal entry As TransitionDiagramClipDataEntry,
        ByVal analyzer As DTAnalyzer)

        For Each t As DeterministicTransitionClipData In entry.IncomingDT

            If (Not ClipContainsStateClass(cd, t.StateClassIdSource)) Then

                If (Me.m_ExplicitClasses.ContainsKey(t.StateClassIdSource)) Then

                    Dim dr As DataRow = analyzer.GetStateClassRow(Me.m_StratumId, t.StateClassIdSource)

                    If (IsDTToSelf(dr)) Then
                        dr(DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME) = entry.ShapeData.StateClassIdSource
                    End If

                Else

                    Dim dr As DataRow = analyzer.GetStateClassRow(t.StratumIdSource, t.StateClassIdSource)

                    If (dr IsNot Nothing AndAlso IsDTToSelf(dr)) Then

                        dr(DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME) = entry.ShapeData.StateClassIdSource
                        dr(DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME) = Me.m_StratumId

                    End If

                End If

            End If

        Next

    End Sub

    Private Sub PastePT(
        ByVal cd As TransitionDiagramClipData,
        ByVal analyzer As DTAnalyzer)

        Dim AlreadyPasted As New List(Of ProbabilisticTransitionClipData)

        Me.PastePTBetween(cd, AlreadyPasted)
        Me.PastePTIncoming(cd, AlreadyPasted, analyzer)
        Me.PastePTOutgoing(cd, AlreadyPasted, analyzer)

    End Sub

    Private Sub PastePTBetween(
        ByVal cd As TransitionDiagramClipData,
        ByVal alreadyPasted As List(Of ProbabilisticTransitionClipData))

        For Each Entry As TransitionDiagramClipDataEntry In cd.Entries
            Me.PastePTOutgoingBetween(cd, Entry, alreadyPasted)
        Next

    End Sub

    Private Sub PastePTIncoming(
        ByVal cd As TransitionDiagramClipData,
        ByVal alreadyPasted As List(Of ProbabilisticTransitionClipData),
        ByVal analyzer As DTAnalyzer)

        For Each Entry As TransitionDiagramClipDataEntry In cd.Entries
            Me.PastePTIncoming(Entry, alreadyPasted, analyzer)
        Next

    End Sub

    Private Sub PastePTOutgoing(
        ByVal cd As TransitionDiagramClipData,
        ByVal alreadyPasted As List(Of ProbabilisticTransitionClipData),
        ByVal analyzer As DTAnalyzer)

        For Each Entry As TransitionDiagramClipDataEntry In cd.Entries
            Me.PastePTOutgoing(Entry, alreadyPasted, analyzer)
        Next

    End Sub

    Private Sub PastePTIncoming(
        ByVal entry As TransitionDiagramClipDataEntry,
        ByVal alreadyPasted As List(Of ProbabilisticTransitionClipData),
        ByVal analyzer As DTAnalyzer)

        For Each t As ProbabilisticTransitionClipData In entry.IncomingPT

            If (AlreadyPastedPT(t, alreadyPasted)) Then
                Continue For
            End If

            Dim StratumIdDest As Nullable(Of Integer) = Nothing

            If (t.StateClassIdDest.HasValue) Then

                analyzer.ResolveStateClassStratum(
                    t.StratumIdSource, t.StratumIdDest,
                    t.StateClassIdDest.Value, StratumIdDest)

            End If

            Me.CreatePTRecord(
                Me.m_StratumId, t.StateClassIdSource,
                StratumIdDest, entry.ShapeData.StateClassIdSource,
                t.TransitionTypeId, t.Probability, t.Proportion,
                t.AgeMin, t.AgeMax, t.AgeRelative, t.AgeReset,
                t.TstMin, t.TstMax, t.TstRelative)

            alreadyPasted.Add(t)

        Next

    End Sub

    Private Sub PastePTOutgoing(
        ByVal entry As TransitionDiagramClipDataEntry,
        ByVal alreadyPasted As List(Of ProbabilisticTransitionClipData),
        ByVal analyzer As DTAnalyzer)

        For Each t As ProbabilisticTransitionClipData In entry.OutgoingPT

            If (AlreadyPastedPT(t, alreadyPasted)) Then
                Continue For
            End If

            Dim StratumIdDest As Nullable(Of Integer) = Nothing

            If (t.StateClassIdDest.HasValue) Then

                analyzer.ResolveStateClassStratum(
                    t.StratumIdSource, t.StratumIdDest,
                    t.StateClassIdDest.Value, StratumIdDest)

            End If

            Me.CreatePTRecord(
                Me.m_StratumId, entry.ShapeData.StateClassIdSource,
                StratumIdDest, t.StateClassIdDest,
                t.TransitionTypeId, t.Probability, t.Proportion,
                t.AgeMin, t.AgeMax, t.AgeRelative, t.AgeReset,
                t.TstMin, t.TstMax, t.TstRelative)

            alreadyPasted.Add(t)

        Next

    End Sub

    Private Sub PastePTOutgoingBetween(
        ByVal cd As TransitionDiagramClipData,
        ByVal entry As TransitionDiagramClipDataEntry,
        ByVal alreadyPasted As List(Of ProbabilisticTransitionClipData))

        For Each t As ProbabilisticTransitionClipData In entry.OutgoingPT

            If (AlreadyPastedPT(t, alreadyPasted)) Then
                Continue For
            End If

            If (Not t.StateClassIdDest.HasValue) Then
                Continue For
            End If

            If (ClipContainsStateClass(cd, t.StateClassIdDest.Value)) Then

                Me.CreatePTRecord(
                    Me.m_StratumId, entry.ShapeData.StateClassIdSource,
                    Nothing, t.StateClassIdDest,
                    t.TransitionTypeId, t.Probability, t.Proportion,
                    t.AgeMin, t.AgeMax, t.AgeRelative, t.AgeReset,
                    t.TstMin, t.TstMax, t.TstRelative)

                alreadyPasted.Add(t)

            End If

        Next

    End Sub

    Private Shared Function ClipContainsStateClass(
        ByVal cd As TransitionDiagramClipData,
        ByVal stateClassId As Integer) As Boolean

        For Each Entry As TransitionDiagramClipDataEntry In cd.Entries

            If (Entry.ShapeData.StateClassIdSource = stateClassId) Then
                Return True
            End If

        Next

        Return False

    End Function

    Private Function ConfirmPasteOverwrite(ByVal cd As TransitionDiagramClipData) As Boolean

        Dim OverwriteRequired As Boolean = False

        For Each Entry As TransitionDiagramClipDataEntry In cd.Entries

            If (Me.m_ExplicitClasses.ContainsKey(Entry.ShapeData.StateClassIdSource)) Then

                OverwriteRequired = True
                Exit For

            ElseIf (Me.m_WildcardClasses.ContainsKey(Entry.ShapeData.StateClassIdSource)) Then

                If (Not Me.m_StratumId.HasValue) Then

                    OverwriteRequired = True
                    Exit For

                End If

            End If

        Next

        If (OverwriteRequired) Then

            If (FormsUtilities.ApplicationMessageBox(CONFIRM_DIAGRAM_PASTE_OVERWRITE,
                MessageBoxButtons.YesNo) <> DialogResult.Yes) Then

                Return False

            End If

        End If

        Return True

    End Function

    Private Shared Function PTClipObjectsEqual(
        ByVal t1 As ProbabilisticTransitionClipData,
        ByVal t2 As ProbabilisticTransitionClipData) As Boolean

        If (Not NullableUtilities.NullableIdsEqual(t1.StratumIdSource, t2.StratumIdSource)) Then
            Return False
        End If

        If (t1.StateClassIdSource <> t2.StateClassIdSource) Then
            Return False
        End If

        If (Not NullableUtilities.NullableIdsEqual(t1.StratumIdDest, t2.StratumIdDest)) Then
            Return False
        End If

        If (Not NullableUtilities.NullableIdsEqual(t1.StateClassIdDest, t2.StateClassIdDest)) Then
            Return False
        End If

        If (t1.TransitionTypeId <> t2.TransitionTypeId) Then
            Return False
        End If

        If (t1.Proportion <> t2.Proportion) Then
            Return False
        End If

        If (t1.Probability <> t2.Probability) Then
            Return False
        End If

        If (t1.AgeMin <> t2.AgeMin) Then
            Return False
        End If

        If (t1.AgeMax <> t2.AgeMax) Then
            Return False
        End If

        If (t1.AgeRelative <> t2.AgeRelative) Then
            Return False
        End If

        If (t1.AgeReset <> t2.AgeReset) Then
            Return False
        End If

        If (t1.TstMin <> t2.TstMin) Then
            Return False
        End If

        If (t1.TstMax <> t2.TstMax) Then
            Return False
        End If

        If (t1.TstRelative <> t2.TstRelative) Then
            Return False
        End If

        Return True

    End Function

    Private Shared Function AlreadyPastedPT(
        ByVal pt As ProbabilisticTransitionClipData,
        ByVal alreadyPasted As List(Of ProbabilisticTransitionClipData)) As Boolean

        For Each t As ProbabilisticTransitionClipData In alreadyPasted

            If (PTClipObjectsEqual(t, pt)) Then
                Return True
            End If

        Next

        Return False

    End Function

    ''' <summary>
    ''' Determines if the specified x and y offsets are for a valid paste location
    ''' </summary>
    ''' <param name="dx"></param>
    ''' <param name="dy"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsValidPasteLocation(ByVal dx As Integer, ByVal dy As Integer) As Boolean

        Dim cd As TransitionDiagramClipData =
            CType(Clipboard.GetData(CLIPBOARD_FORMAT_TRANSITION_DIAGRAM), TransitionDiagramClipData)

        For Each Entry As TransitionDiagramClipDataEntry In cd.Entries

            Dim TargetRow As Integer = Entry.Row + dy
            Dim TargetColumn As Integer = Entry.Column + dx

            If (TargetRow < 0 Or
                TargetColumn < 0 Or
                TargetRow >= TRANSITION_DIAGRAM_MAX_ROWS Or
                TargetColumn >= TRANSITION_DIAGRAM_MAX_COLUMNS) Then

                Return False

            End If

            Dim s As BoxDiagramShape = Me.GetShapeAt(TargetRow, TargetColumn)

            If (s IsNot Nothing) Then

                If (Not s.IsStatic) Then
                    Return False
                End If

            End If

        Next

        Return True

    End Function

    ''' <summary>
    ''' Gets the upper left clipboard entry
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetUpperLeftClipEntry() As TransitionDiagramClipDataEntry

        Dim MinRow As Integer = Integer.MaxValue
        Dim MinColumn As Integer = Integer.MaxValue
        Dim lst As New List(Of TransitionDiagramClipDataEntry)

        Dim cd As TransitionDiagramClipData =
            CType(Clipboard.GetData(CLIPBOARD_FORMAT_TRANSITION_DIAGRAM), TransitionDiagramClipData)

        For Each e As TransitionDiagramClipDataEntry In cd.Entries
            If (e.Row < MinRow) Then
                MinRow = e.Row
            End If
        Next

        For Each e As TransitionDiagramClipDataEntry In cd.Entries
            If (e.Row = MinRow) Then
                lst.Add(e)
            End If
        Next

        For Each e As TransitionDiagramClipDataEntry In lst
            If (e.Column < MinColumn) Then
                MinColumn = e.Column
            End If
        Next

        For Each e As TransitionDiagramClipDataEntry In lst
            If (e.Column = MinColumn) Then
                Return e
            End If
        Next

        Debug.Assert(False)
        Return Nothing

    End Function

    ''' <summary>
    ''' Gets the paste location delta values for the state classes on the clipboard
    ''' </summary>
    ''' <param name="dx"></param>
    ''' <param name="dy"></param>
    ''' <param name="targeted"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetPasteLocationDeltas(ByRef dx As Integer, ByRef dy As Integer, ByVal targeted As Boolean) As Boolean

        Dim UpperLeftEntry As TransitionDiagramClipDataEntry = GetUpperLeftClipEntry()

        'If the paste is targeted (using the context menu), then we only attempt to paste into the
        'cell that was current when the right mouse button was clicked.  But if it is a non-targeted
        'pasted then we need to search the diagram for a paste location.

        If (targeted) Then

            dx = Me.GetColumnDelta(Me.CurrentMousePoint.X, UpperLeftEntry.Bounds.X)
            dy = Me.GetRowDelta(Me.CurrentMousePoint.Y, UpperLeftEntry.Bounds.Y)

            Return (Me.IsValidPasteLocation(dx, dy))

        End If

        'First, try the original location

        dx = 0
        dy = 0

        If (Me.IsValidPasteLocation(dx, dy)) Then
            Return True
        End If

        'Search all rows in the current column

        dx = 0
        dy = 0

        While (dy < TRANSITION_DIAGRAM_MAX_ROWS)

            If (Me.IsValidPasteLocation(dx, dy)) Then
                Return True
            End If

            dy += 1

        End While

        'Search all columns and rows

        dx = Me.GetColumnDelta(0, UpperLeftEntry.Bounds.X)

        While (dx < TRANSITION_DIAGRAM_MAX_COLUMNS)

            dy = Me.GetRowDelta(0, UpperLeftEntry.Bounds.Y)

            While (dy < TRANSITION_DIAGRAM_MAX_ROWS)

                If (Me.IsValidPasteLocation(dx, dy)) Then
                    Return True
                End If

                dy += 1

            End While

            dx += 1

        End While

        'No location found anywhere!
        Return False

    End Function

    Private Shared Function DTToClipFormat(
        ByVal dt As DeterministicTransition,
        ByVal stratumSheet As DataSheet,
        ByVal stateClassSheet As DataSheet) As DeterministicTransitionClipData

        Debug.Assert(stratumSheet.Name = DATASHEET_STRATA_NAME)
        Debug.Assert(stateClassSheet.Name = DATASHEET_STATECLASS_NAME)

        Dim cd As New DeterministicTransitionClipData()

        If (dt.StratumIdSource.HasValue) Then
            cd.StratumSource = stratumSheet.ValidationTable.GetDisplayName(dt.StratumIdSource.Value)
        End If

        cd.StateClassSource = stateClassSheet.ValidationTable.GetDisplayName(dt.StateClassIdSource)

        If (dt.StratumIdDestination.HasValue) Then
            cd.StratumDest = stratumSheet.ValidationTable.GetDisplayName(dt.StratumIdDestination.Value)
        End If

        If (cd.StateClassIdDest.HasValue) Then
            cd.StateClassDest = stateClassSheet.ValidationTable.GetDisplayName(dt.StateClassIdDestination.Value)
        End If

        Return cd

    End Function

    Private Shared Function PTToClipFormat(
        ByVal pt As Transition,
        ByVal stratumSheet As DataSheet,
        ByVal stateClassSheet As DataSheet,
        ByVal transitionTypeSheet As DataSheet) As ProbabilisticTransitionClipData

        Debug.Assert(stratumSheet.Name = DATASHEET_STRATA_NAME)
        Debug.Assert(stateClassSheet.Name = DATASHEET_STATECLASS_NAME)
        Debug.Assert(transitionTypeSheet.Name = DATASHEET_TRANSITION_TYPE_NAME)

        Dim cd As New ProbabilisticTransitionClipData()

        If (pt.StratumIdSource.HasValue) Then
            cd.StratumSource = stratumSheet.ValidationTable.GetDisplayName(pt.StratumIdSource.Value)
        End If

        cd.StateClassSource = stateClassSheet.ValidationTable.GetDisplayName(pt.StateClassIdSource)

        If (pt.StratumIdDestination.HasValue) Then
            cd.StratumDest = stratumSheet.ValidationTable.GetDisplayName(pt.StratumIdDestination.Value)
        End If

        If (pt.StateClassIdDestination.HasValue) Then
            cd.StateClassDest = stateClassSheet.ValidationTable.GetDisplayName(pt.StateClassIdDestination.Value)
        End If

        cd.TransitionType = transitionTypeSheet.ValidationTable.GetDisplayName(pt.TransitionTypeId)
        cd.Probability = pt.Probability

        If (pt.PropnWasNull) Then
            cd.Proportion = Nothing
        Else
            cd.Proportion = pt.Proportion
        End If

        If (pt.AgeMinWasNull) Then
            cd.AgeMin = Nothing
        Else
            cd.AgeMin = pt.AgeMinimum
        End If

        If (pt.AgeMaxWasNull) Then
            cd.AgeMax = Nothing
        Else
            cd.AgeMax = pt.AgeMaximum
        End If

        If (pt.AgeRelativeWasNull) Then
            cd.AgeRelative = Nothing
        Else
            cd.AgeRelative = pt.AgeRelative
        End If

        If (pt.AgeResetWasNull) Then
            cd.AgeReset = Nothing
        Else
            cd.AgeReset = pt.AgeReset
        End If

        If (pt.TstMinimumWasNull) Then
            cd.TstMin = Nothing
        Else
            cd.TstMin = pt.TstMinimum
        End If

        If (pt.TstMaximumWasNull) Then
            cd.TstMax = Nothing
        Else
            cd.TstMax = pt.TstMaximum
        End If

        If (pt.TstRelativeWasNull) Then
            cd.TstRelative = Nothing
        Else
            cd.TstRelative = pt.TstRelative
        End If

        Return cd

    End Function

    Private Shared Function ValidateClipData(
        ByVal cd As TransitionDiagramClipData,
        ByVal stratumSheet As DataSheet,
        ByVal stateClassSheet As DataSheet,
        ByVal transitionTypeSheet As DataSheet) As Boolean

        Debug.Assert(stratumSheet.Name = DATASHEET_STRATA_NAME)
        Debug.Assert(stateClassSheet.Name = DATASHEET_STATECLASS_NAME)
        Debug.Assert(transitionTypeSheet.Name = DATASHEET_TRANSITION_TYPE_NAME)

        For Each entry As TransitionDiagramClipDataEntry In cd.Entries

            If (Not ValidateDTClipData(entry.ShapeData, stratumSheet, stateClassSheet)) Then
                Return False
            End If

            For Each t As DeterministicTransitionClipData In entry.IncomingDT

                If (Not ValidateDTClipData(t, stratumSheet, stateClassSheet)) Then
                    Return False
                End If

            Next

            For Each t As ProbabilisticTransitionClipData In entry.IncomingPT

                If (Not ValidatePTClipData(t, stratumSheet, stateClassSheet, transitionTypeSheet)) Then
                    Return False
                End If

            Next

            For Each t As ProbabilisticTransitionClipData In entry.OutgoingPT

                If (Not ValidatePTClipData(t, stratumSheet, stateClassSheet, transitionTypeSheet)) Then
                    Return False
                End If

            Next

        Next

        Return True

    End Function

    Private Shared Function ValidateDTClipData(
        ByVal dt As DeterministicTransitionClipData,
        ByVal stratumSheet As DataSheet,
        ByVal stateClassSheet As DataSheet) As Boolean

        Debug.Assert(stratumSheet.Name = DATASHEET_STRATA_NAME)
        Debug.Assert(stateClassSheet.Name = DATASHEET_STATECLASS_NAME)

        If (dt.StratumSource IsNot Nothing) Then

            If (Not stratumSheet.ValidationTable.ContainsValue(dt.StratumSource)) Then
                FormsUtilities.ErrorMessageBox("The stratum '{0}' does not exist in this project.", dt.StratumSource)
                Return False
            End If

        End If

        If (Not stateClassSheet.ValidationTable.ContainsValue(dt.StateClassSource)) Then
            FormsUtilities.ErrorMessageBox("The state class '{0}' does not exist in this project.", dt.StateClassSource)
            Return False
        End If

        If (dt.StratumDest IsNot Nothing) Then

            If (Not stratumSheet.ValidationTable.ContainsValue(dt.StratumDest)) Then
                FormsUtilities.ErrorMessageBox("The stratum '{0}' does not exist in this project.", dt.StratumDest)
                Return False
            End If

        End If

        If (dt.StateClassDest IsNot Nothing) Then

            If (Not stateClassSheet.ValidationTable.ContainsValue(dt.StateClassDest)) Then
                FormsUtilities.ErrorMessageBox("The state class '{0}' does not exist in this project.", dt.StateClassDest)
                Return False
            End If

        End If

        If (dt.StratumSource IsNot Nothing) Then
            dt.StratumIdSource = stratumSheet.ValidationTable.GetValue(dt.StratumSource)
        End If

        dt.StateClassIdSource = stateClassSheet.ValidationTable.GetValue(dt.StateClassSource)

        If (dt.StratumDest IsNot Nothing) Then
            dt.StratumIdDest = stratumSheet.ValidationTable.GetValue(dt.StratumDest)
        End If

        If (dt.StateClassDest IsNot Nothing) Then
            dt.StateClassIdDest = stateClassSheet.ValidationTable.GetValue(dt.StateClassDest)
        End If

#If DEBUG Then
        If (dt.StratumIdSource.HasValue) Then
            Debug.Assert(dt.StratumIdSource.Value > 0)
        End If

        Debug.Assert(dt.StateClassIdSource > 0)

        If (dt.StratumDest IsNot Nothing) Then
            Debug.Assert(dt.StratumIdDest.Value > 0)
        End If

        If (dt.StateClassDest IsNot Nothing) Then
            Debug.Assert(dt.StateClassIdDest.Value > 0)
        End If
#End If

        Return True

    End Function

    Private Shared Function ValidatePTClipData(
        ByVal pt As ProbabilisticTransitionClipData,
        ByVal stratumSheet As DataSheet,
        ByVal stateClassSheet As DataSheet,
        ByVal transitionTypeSheet As DataSheet) As Boolean

        Debug.Assert(stratumSheet.Name = DATASHEET_STRATA_NAME)
        Debug.Assert(stateClassSheet.Name = DATASHEET_STATECLASS_NAME)
        Debug.Assert(transitionTypeSheet.Name = DATASHEET_TRANSITION_TYPE_NAME)

        If (pt.StratumSource IsNot Nothing) Then

            If (Not stratumSheet.ValidationTable.ContainsValue(pt.StratumSource)) Then
                FormsUtilities.ErrorMessageBox("The stratum '{0}' does not exist in this project.", pt.StratumSource)
                Return False
            End If

        End If

        If (Not stateClassSheet.ValidationTable.ContainsValue(pt.StateClassSource)) Then
            FormsUtilities.ErrorMessageBox("The state class '{0}' does not exist in this project.", pt.StateClassSource)
            Return False
        End If

        If (pt.StratumDest IsNot Nothing) Then

            If (Not stratumSheet.ValidationTable.ContainsValue(pt.StratumDest)) Then
                FormsUtilities.ErrorMessageBox("The stratum '{0}' does not exist in this project.", pt.StratumDest)
                Return False
            End If

        End If

        If (pt.StateClassDest IsNot Nothing) Then

            If (Not stateClassSheet.ValidationTable.ContainsValue(pt.StateClassDest)) Then
                FormsUtilities.ErrorMessageBox("The state class '{0}' does not exist in this project.", pt.StateClassDest)
                Return False
            End If

        End If

        If (Not transitionTypeSheet.ValidationTable.ContainsValue(pt.TransitionType)) Then
            FormsUtilities.ErrorMessageBox("The transition type '{0}' does not exist in this project.", pt.TransitionType)
            Return False
        End If

        If (pt.StratumSource IsNot Nothing) Then
            pt.StratumIdSource = stratumSheet.ValidationTable.GetValue(pt.StratumSource)
        End If

        pt.StateClassIdSource = stateClassSheet.ValidationTable.GetValue(pt.StateClassSource)

        If (pt.StratumDest IsNot Nothing) Then
            pt.StratumIdDest = stratumSheet.ValidationTable.GetValue(pt.StratumDest)
        End If

        If (pt.StateClassDest IsNot Nothing) Then
            pt.StateClassIdDest = stateClassSheet.ValidationTable.GetValue(pt.StateClassDest)
        End If

        pt.TransitionTypeId = transitionTypeSheet.ValidationTable.GetValue(pt.TransitionType)

#If DEBUG Then
        If (pt.StratumIdSource.HasValue) Then
            Debug.Assert(pt.StratumIdSource.Value > 0)
        End If

        Debug.Assert(pt.StateClassIdSource > 0)

        If (pt.StratumDest IsNot Nothing) Then
            Debug.Assert(pt.StratumIdDest.Value > 0)
        End If

        If (pt.StateClassDest IsNot Nothing) Then
            Debug.Assert(pt.StateClassIdDest.Value > 0)
        End If

        Debug.Assert(pt.TransitionTypeId > 0)
#End If

        Return True

    End Function

End Class
