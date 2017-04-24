'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports System.Globalization
Imports SyncroSim.Common.Forms

Partial Class TransitionDiagram

    Private Sub DrawDTLines(g As System.Drawing.Graphics)

        For Each line As ConnectorLine In Me.Lines

            If (TypeOf (line) Is DeterministicTransitionLine) Then

                If (line.IsVisible) Then
                    line.Render(g)
                End If

            End If

        Next

    End Sub

    Private Function GetIncomingDT(ByVal shape As StateClassShape) As DataRow()

        Dim Query As String = Nothing

        If (shape.StratumIdSource.HasValue) Then

            Query = String.Format(CultureInfo.InvariantCulture,
                "((StratumIDDest={0} AND StateClassIDDest={1}) OR (StratumIDDest IS NULL AND StratumIDSource={0} AND StateClassIDDest={1}))",
                 shape.StratumIdSource.Value, shape.StateClassIdSource)

        Else

            Query = String.Format(CultureInfo.InvariantCulture,
                "StratumIDSource IS NULL AND StateClassIDDest={0}",
                shape.StateClassIdSource)

        End If

        Return Me.m_DTDataSheet.GetData().Select(Query, Nothing)

    End Function

    Private Sub FillIncomingDT(ByVal shape As StateClassShape)

        Dim rows() As DataRow = Me.GetIncomingDT(shape)

        For Each dr As DataRow In rows
            shape.IncomingDT.Add(CreateDT(dr))
        Next

    End Sub

    Private Shared Function IsDTToSelf(ByVal dr As DataRow) As Boolean

        Dim StratumIdSource As Nullable(Of Integer) = Nothing
        Dim StateClassIdSource As Integer = 0
        Dim StratumIdDest As Nullable(Of Integer) = Nothing
        Dim StateClassIdDest As Nullable(Of Integer) = Nothing

        DTAnalyzer.GetDTFieldValues(dr, StratumIdSource, StateClassIdSource, StratumIdDest, StateClassIdDest)

        If (Not StateClassIdDest.HasValue) Then
            Return True
        End If

        If (StateClassIdSource = StateClassIdDest.Value) Then

            If (Not StratumIdDest.HasValue) Then
                Return True
            Else
                Return NullableUtilities.NullableIdsEqual(StratumIdSource, StratumIdDest)
            End If

        End If

        Return False

    End Function

    Private Sub CreateDTLines(ByVal analyzer As DTAnalyzer)

        For Each Shape As StateClassShape In Me.Shapes
            Me.CreateOutgoingDTLines(Shape, analyzer)
        Next

    End Sub

    Private Sub CreateDTOffStratumCues(ByVal analyzer As DTAnalyzer)

        For Each Shape As StateClassShape In Me.Shapes

            Me.CreateIncomingDTOffStratumCues(Shape)
            Me.CreateOutgoingDTOffStratumCues(Shape, analyzer)

        Next

    End Sub

    Private Sub CreateOutgoingDTLines(ByVal fromShape As StateClassShape, ByVal analyzer As DTAnalyzer)

        'If there is no destination state class then it is a transition-to-self

        If (Not fromShape.StateClassIdDest.HasValue) Then
            Return
        End If

        Dim ToShape As StateClassShape = Nothing
        Dim StratumIdActual As Nullable(Of Integer) = Nothing

        analyzer.ResolveStateClassStratum(
            fromShape.StratumIdSource, fromShape.StratumIdDest,
            fromShape.StateClassIdDest.Value, StratumIdActual)

        If (NullableUtilities.NullableIdsEqual(StratumIdActual, Me.m_StratumId)) Then

            'If the class was found in the current stratum then it will be in the
            'explicit lookups if the current stratum is explicit and the wildcard
            'lookups if it is not.

            If (Me.m_StratumId.HasValue) Then
                ToShape = Me.m_ExplicitClasses(fromShape.StateClassIdDest.Value)
            Else
                ToShape = Me.m_WildcardClasses(fromShape.StateClassIdDest.Value)
            End If

        Else

            'If the class was not found in the current stratum it will be in the
            'wild card lookups if its stratum is wild.

            If (Me.m_StratumId.HasValue And (Not StratumIdActual.HasValue)) Then
                ToShape = Me.m_WildcardClasses(fromShape.StateClassIdDest.Value)
            End If

        End If

        'If the class was not found or it is a transition-to-self then it is an 
        'off stratum transition.  Otherwise, add an outgoing line.

        If ((ToShape IsNot Nothing) And (ToShape IsNot fromShape)) Then

            Dim Line As New DeterministicTransitionLine(DETERMINISTIC_TRANSITION_LINE_COLOR)

            Me.FillLineSegments(fromShape, ToShape, Line, BoxArrowDiagramConnectorMode.Horizontal)
            Me.AddLine(Line)

            fromShape.OutgoingDTLines.Add(Line)
            ToShape.IncomingDTLines.Add(Line)

        End If

    End Sub

    Private Sub CreateIncomingDTOffStratumCues(ByVal shape As StateClassShape)

        For Each t As DeterministicTransition In shape.IncomingDT

            If (Not NullableUtilities.NullableIdsEqual(t.StratumIdSource, shape.StratumIdSource)) Then

                If (t.StratumIdSource.HasValue) Then

                    Dim l As DeterministicTransitionLine = CreateIncomingDTOffStratumCue(shape)
                    Me.AddLine(l)

                End If

            End If

        Next

    End Sub

    Private Sub CreateOutgoingDTOffStratumCues(ByVal shape As StateClassShape, ByVal analyzer As DTAnalyzer)

        'If there is no destination state class then it is a transition-to-self

        If (Not shape.StateClassIdDest.HasValue) Then
            Return
        End If

        Dim StratumIdActual As Nullable(Of Integer) = Nothing

        analyzer.ResolveStateClassStratum(
            shape.StratumIdSource, shape.StratumIdDest,
            shape.StateClassIdDest.Value, StratumIdActual)

        'If the class was found in the current stratum then it is not an off-stratum transition

        If (NullableUtilities.NullableIdsEqual(StratumIdActual, Me.m_StratumId)) Then
            Return
        End If

        'If the class was found in the wild card stratum then it is not an off-stratum transition

        If (Not StratumIdActual.HasValue) Then
            Return
        End If

        Me.AddLine(CreateOutgoingDTOffStratumCue(shape))

    End Sub

    Private Shared Function CreateOutgoingDTOffStratumCue(ByVal shape As StateClassShape) As DeterministicTransitionLine

        Dim X1 As Integer = shape.Bounds.X + shape.Bounds.Width
        Dim Y1 As Integer = shape.Bounds.Y + shape.Bounds.Height
        Dim X2 As Integer = X1 + TRANSITION_DIAGRAM_OFF_STRATUM_CUE_SIZE
        Dim Y2 As Integer = Y1 + TRANSITION_DIAGRAM_OFF_STRATUM_CUE_SIZE

        Dim Line As New DeterministicTransitionLine(DETERMINISTIC_TRANSITION_LINE_COLOR)

        Line.AddLineSegment(X1, Y1, X2, Y2)
        Line.AddArrowSegments(X2, Y2, BoxArrowDiagramArrowDirection.Southeast)

        Return Line

    End Function

    Private Shared Function CreateIncomingDTOffStratumCue(ByVal shape As StateClassShape) As DeterministicTransitionLine

        Dim X1 As Integer = shape.Bounds.X - TRANSITION_DIAGRAM_OFF_STRATUM_CUE_SIZE
        Dim Y1 As Integer = shape.Bounds.Y + shape.Bounds.Height + TRANSITION_DIAGRAM_OFF_STRATUM_CUE_SIZE
        Dim X2 As Integer = shape.Bounds.X
        Dim Y2 As Integer = shape.Bounds.Y + shape.Bounds.Height
        Dim Line As New DeterministicTransitionLine(DETERMINISTIC_TRANSITION_LINE_COLOR)

        Line.AddLineSegment(X1, Y1, X2, Y2)
        Line.AddArrowSegments(X2, Y2, BoxArrowDiagramArrowDirection.Northeast)

        Return Line

    End Function

    Private Function GetDTRows() As DataRow()

        Dim Query As String = Nothing

        If (Me.m_StratumId.HasValue) Then

            Query = String.Format(CultureInfo.InvariantCulture,
                "{0}={1} OR {2} IS NULL",
                DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME,
                Me.m_StratumId,
                DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME)

        Else

            Query = String.Format(CultureInfo.InvariantCulture,
                "{0} IS NULL",
                DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME)

        End If

        Dim SortOrder As String = String.Format(CultureInfo.InvariantCulture,
            "{0},{1}", DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME, DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME)

        Return Me.m_DTDataSheet.GetData.Select(Query, SortOrder)

    End Function

    Private Shared Function CreateDT(ByVal dr As DataRow) As DeterministicTransition

        Dim Iteration As Nullable(Of Integer) = Nothing
        Dim Timestep As Nullable(Of Integer) = Nothing
        Dim StratumIdSource As Nullable(Of Integer) = Nothing
        Dim StateClassIdSource As Integer = CInt(dr(DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME))
        Dim StratumIdDest As Nullable(Of Integer) = Nothing
        Dim StateClassIdDest As Nullable(Of Integer) = Nothing
        Dim AgeMinimum As Integer = 0
        Dim AgeMaximum As Integer = Integer.MaxValue

        If (dr(DATASHEET_ITERATION_COLUMN_NAME) IsNot DBNull.Value) Then
            Iteration = CType(dr(DATASHEET_ITERATION_COLUMN_NAME), Integer)
        End If

        If (dr(DATASHEET_TIMESTEP_COLUMN_NAME) IsNot DBNull.Value) Then
            Timestep = CType(dr(DATASHEET_TIMESTEP_COLUMN_NAME), Integer)
        End If

        If (dr(DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME) IsNot DBNull.Value) Then
            StratumIdSource = CInt(dr(DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME))
        End If

        If (dr(DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME) IsNot DBNull.Value) Then
            StratumIdDest = CInt(dr(DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME))
        End If

        If (dr(DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME) IsNot DBNull.Value) Then
            StateClassIdDest = CInt(dr(DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME))
        End If

        If (dr(DATASHEET_AGE_MIN_COLUMN_NAME) IsNot DBNull.Value) Then
            AgeMinimum = CInt(dr(DATASHEET_AGE_MIN_COLUMN_NAME))
        End If

        If (dr(DATASHEET_AGE_MAX_COLUMN_NAME) IsNot DBNull.Value) Then
            AgeMaximum = CInt(dr(DATASHEET_AGE_MAX_COLUMN_NAME))
        End If

        Dim dt As New DeterministicTransition(
            Iteration, Timestep,
            StratumIdSource, StateClassIdSource,
            StratumIdDest, StateClassIdDest,
            AgeMinimum, AgeMaximum)

        Return dt

    End Function

    Private Function CreateDTRecord(
        ByVal stateClassIdSource As Integer,
        ByVal stratumIdDestination As Nullable(Of Integer),
        ByVal stateClassIdDestination As Nullable(Of Integer),
        ByVal ageMinimum As Nullable(Of Integer),
        ByVal ageMaximum As Nullable(Of Integer),
        ByVal row As Integer,
        ByVal column As Integer) As DataRow

        Dim dr As DataRow = Me.m_DTDataSheet.GetData().NewRow

        dr(DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(Me.m_StratumId)
        dr(DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME) = stateClassIdSource
        dr(DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(stratumIdDestination)
        dr(DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(stateClassIdDestination)
        dr(DATASHEET_AGE_MIN_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(ageMinimum)
        dr(DATASHEET_AGE_MAX_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(ageMaximum)
        dr(DATASHEET_DT_LOCATION_COLUMN_NAME) = RowColToLocation(row, column)

        Me.m_DTDataSheet.GetData().Rows.Add(dr)
        Return dr

    End Function

End Class
