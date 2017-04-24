'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Imports System.Drawing
Imports System.Globalization
Imports SyncroSim.Common.Forms

Partial Class TransitionDiagram

    Private Sub DrawPTLines(g As System.Drawing.Graphics, ByVal selected As Boolean)

        For Each line As ConnectorLine In Me.Lines

            If (TypeOf (line) Is ProbabilisticTransitionLine) Then

                If (line.IsVisible) Then

                    If (line.IsSelected = selected) Then
                        line.Render(g)
                    End If

                End If

            End If

        Next

    End Sub

    Private Sub FillIncomingPT(ByVal shape As StateClassShape)

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

        Dim rows() As DataRow = Me.m_PTDataSheet.GetData().Select(Query, Nothing)

        For Each dr As DataRow In rows

            Debug.Assert(CInt(dr(DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME)) = shape.StateClassIdSource)

            Dim pt As Transition = CreatePT(dr)
            shape.IncomingPT.Add(pt)

        Next

    End Sub

    Private Sub FillOutgoingPT(ByVal shape As StateClassShape)

        Dim Query As String = Nothing

        If (shape.StratumIdSource.HasValue) Then

            Query = String.Format(CultureInfo.InvariantCulture,
                "StratumIDSource={0} AND StateClassIDSource={1}",
                shape.StratumIdSource.Value, shape.StateClassIdSource)

        Else

            Query = String.Format(CultureInfo.InvariantCulture,
                "StratumIDSource IS NULL AND StateClassIDSource={0}",
                shape.StateClassIdSource)

        End If

        Dim rows() As DataRow = Me.m_PTDataSheet.GetData().Select(Query, Nothing)

        For Each dr As DataRow In rows

            Debug.Assert(CInt(dr(DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME)) = shape.StateClassIdSource)

            Dim pt As Transition = CreatePT(dr)
            shape.OutgoingPT.Add(pt)

        Next

    End Sub

    Private Sub FillPTLineTransitionGroups(ByVal line As ProbabilisticTransitionLine)

        Debug.Assert(line.TransitionGroups.Count = 0)

        Dim filter As String = String.Format(CultureInfo.InvariantCulture, "TransitionTypeID={0}", line.TransitionTypeId)
        Dim trrows() As DataRow = Me.m_TTGDataSheet.GetData().Select(filter)

        For Each dr As DataRow In trrows

            Dim TransitionGroupId As Integer = CInt(dr("TransitionGroupID"))
            line.TransitionGroups.Add(TransitionGroupId)

        Next

    End Sub

    Private Sub CreatePTLines(ByVal analyzer As DTAnalyzer)

        For Each Shape As StateClassShape In Me.Shapes
            Me.CreateOutgoingPTLines(Shape, analyzer)
        Next

    End Sub

    Private Sub CreatePTOffStratumCues(ByVal analyzer As DTAnalyzer)

        For Each Shape As StateClassShape In Me.Shapes

            CreateIncomingPTOffStratumCues(Shape)
            Me.CreateOutgoingPTOffStratumCues(Shape, analyzer)

        Next

    End Sub

    Private Sub CreateOutgoingPTLines(ByVal fromShape As StateClassShape, ByVal analyzer As DTAnalyzer)

        For Each t As Transition In fromShape.OutgoingPT

            Dim ToShape As StateClassShape = Nothing

            'If there is no destination state class then it is a transition-to-self

            If (Not t.StateClassIdDestination.HasValue) Then
                ToShape = fromShape
            Else

                Dim StratumIdActual As Nullable(Of Integer) = Nothing

                analyzer.ResolveStateClassStratum(
                    t.StratumIdSource, t.StratumIdDestination,
                    t.StateClassIdDestination.Value, StratumIdActual)

                If (NullableUtilities.NullableIdsEqual(StratumIdActual, Me.m_StratumId)) Then

                    'If the class was found in the current stratum then it will be in the
                    'explicit lookups if the current stratum is explicit and the wildcard
                    'lookups if it is not.

                    If (Me.m_StratumId.HasValue) Then
                        ToShape = Me.m_ExplicitClasses(t.StateClassIdDestination.Value)
                    Else
                        ToShape = Me.m_WildcardClasses(t.StateClassIdDestination.Value)
                    End If

                Else

                    'If the class was not found in the current stratum it will be in the
                    'wild card lookups if its stratum is wild.

                    If (Me.m_StratumId.HasValue And (Not StratumIdActual.HasValue)) Then
                        ToShape = Me.m_WildcardClasses(t.StateClassIdDestination.Value)
                    End If

                End If

            End If

            'If a shape was not found then it is an off-stratum transition.  
            'Otherwise, create the approprate line and add it to the diagram.

            If (ToShape IsNot Nothing) Then

                Dim Line As ProbabilisticTransitionLine = Nothing

                If (fromShape Is ToShape) Then
                    Line = CreatePTLineToSelf(fromShape, t.TransitionTypeId)
                Else

                    Line = New ProbabilisticTransitionLine(t.TransitionTypeId, PROBABILISTIC_TRANSITION_LINE_COLOR)
                    Me.FillLineSegments(fromShape, ToShape, Line, BoxArrowDiagramConnectorMode.Vertical)

                End If

                Me.FillPTLineTransitionGroups(Line)
                Me.AddLine(Line)

                fromShape.OutgoingPTLines.Add(Line)
                ToShape.IncomingPTLines.Add(Line)

            End If

        Next

    End Sub

    Private Sub CreateIncomingPTOffStratumCues(ByVal shape As StateClassShape)

        For Each t As Transition In shape.IncomingPT

            If (Not NullableUtilities.NullableIdsEqual(t.StratumIdSource, shape.StratumIdSource)) Then
                Me.AddLine(Me.CreateIncomingPTOffStratumCue(shape, t))
            End If

        Next

    End Sub

    Private Sub CreateOutgoingPTOffStratumCues(ByVal shape As StateClassShape, ByVal analyzer As DTAnalyzer)

        For Each t As Transition In shape.OutgoingPT

            'If it is a transition-to-self then it is not an off-stratum transition

            If (Not t.StateClassIdDestination.HasValue) Then
                Continue For
            End If

            Dim StratumIdActual As Nullable(Of Integer) = Nothing

            analyzer.ResolveStateClassStratum(
                t.StratumIdSource, t.StratumIdDestination,
                t.StateClassIdDestination.Value, StratumIdActual)

            'If the class was found in the current stratum then it is not an off-stratum transition

            If (NullableUtilities.NullableIdsEqual(StratumIdActual, Me.m_StratumId)) Then
                Continue For
            End If

            'If the class was found in the wild card stratum then it is not an off-stratum transition

            If (Not StratumIdActual.HasValue) Then
                Continue For
            End If

            Me.AddLine(Me.CreateOutgoingPTOffStratumCue(shape, t))

        Next

    End Sub

    Private Function CreateOutgoingPTOffStratumCue(
        ByVal shape As StateClassShape,
        ByVal t As Transition) As ProbabilisticTransitionLine

        Dim X1 As Integer = shape.Bounds.X + shape.Bounds.Width
        Dim Y1 As Integer = shape.Bounds.Y
        Dim X2 As Integer = X1 + TRANSITION_DIAGRAM_OFF_STRATUM_CUE_SIZE
        Dim Y2 As Integer = shape.Bounds.Y - TRANSITION_DIAGRAM_OFF_STRATUM_CUE_SIZE
        Dim Line As New ProbabilisticTransitionLine(t.TransitionTypeId, PROBABILISTIC_TRANSITION_LINE_COLOR)

        Me.FillPTLineTransitionGroups(Line)

        Line.AddLineSegment(X1, Y1, X2, Y2)
        Line.AddArrowSegments(X2, Y2, BoxArrowDiagramArrowDirection.Northeast)

        Return Line

    End Function

    Private Function CreateIncomingPTOffStratumCue(
        ByVal shape As StateClassShape,
        ByVal t As Transition) As ProbabilisticTransitionLine

        Dim X1 As Integer = shape.Bounds.X - TRANSITION_DIAGRAM_OFF_STRATUM_CUE_SIZE
        Dim Y1 As Integer = shape.Bounds.Y - TRANSITION_DIAGRAM_OFF_STRATUM_CUE_SIZE
        Dim X2 As Integer = shape.Bounds.X
        Dim Y2 As Integer = shape.Bounds.Y
        Dim Line As New ProbabilisticTransitionLine(t.TransitionTypeId, PROBABILISTIC_TRANSITION_LINE_COLOR)

        Me.FillPTLineTransitionGroups(Line)

        Line.AddLineSegment(X1, Y1, X2, Y2)
        Line.AddArrowSegments(X2, Y2, BoxArrowDiagramArrowDirection.Southeast)

        Return Line

    End Function

    Private Shared Function CreatePTLineToSelf(
        ByVal shape As StateClassShape,
        ByVal transitionTypeId As Integer) As ProbabilisticTransitionLine

        Dim Line As New ProbabilisticTransitionLine(transitionTypeId, PROBABILISTIC_TRANSITION_LINE_COLOR)

        Const PT_CIRCLE_RADIUS As Integer = 10

        Dim lrx As Integer = shape.Bounds.X + shape.Bounds.Width - PT_CIRCLE_RADIUS
        Dim lry As Integer = shape.Bounds.Y + shape.Bounds.Height - PT_CIRCLE_RADIUS
        Dim rc As New Rectangle(lrx, lry, 2 * PT_CIRCLE_RADIUS, 2 * PT_CIRCLE_RADIUS)

        Line.AddEllipse(rc)

        Return Line

    End Function

    Private Shared Function CreatePT(ByVal dr As DataRow) As Transition

        Dim Iteration As Nullable(Of Integer) = Nothing
        Dim Timestep As Nullable(Of Integer) = Nothing
        Dim StratumIdSource As Nullable(Of Integer) = Nothing
        Dim StateClassIdSource As Integer = CInt(dr(DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME))
        Dim StratumIdDest As Nullable(Of Integer) = Nothing
        Dim StateClassIdDest As Nullable(Of Integer) = Nothing
        Dim Propn As Double = 1.0
        Dim AgeMin As Integer = 0
        Dim AgeMax As Integer = Integer.MaxValue
        Dim AgeRel As Integer = 0
        Dim AgeReset As Boolean = True
        Dim TstMin As Integer = 0
        Dim TstMax As Integer = Integer.MaxValue
        Dim TstRel As Integer = 0

        If (dr(DATASHEET_ITERATION_COLUMN_NAME) IsNot DBNull.Value) Then
            Iteration = CType(dr(DATASHEET_ITERATION_COLUMN_NAME), Integer)
        End If

        If (dr(DATASHEET_TIMESTEP_COLUMN_NAME) IsNot DBNull.Value) Then
            Timestep = CType(dr(DATASHEET_TIMESTEP_COLUMN_NAME), Integer)
        End If

        If (dr(DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME) IsNot DBNull.Value) Then
            StratumIdSource = CInt(dr(DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME))
        End If

        If (dr(DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME) IsNot DBNull.Value) Then
            StratumIdDest = CInt(dr(DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME))
        End If

        If (dr(DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME) IsNot DBNull.Value) Then
            StateClassIdDest = CInt(dr(DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME))
        End If

        Dim pt As New Transition(
            Iteration,
            Timestep,
            StratumIdSource,
            StateClassIdSource,
            StratumIdDest,
            StateClassIdDest,
            CInt(dr(DATASHEET_TRANSITION_TYPE_ID_COLUMN_NAME)),
            CDbl(dr(DATASHEET_PT_PROBABILITY_COLUMN_NAME)),
            Propn,
            AgeMin,
            AgeMax,
            AgeRel,
            AgeReset,
            TstMin,
            TstMax,
            TstRel)

        pt.PropnWasNull = True
        pt.AgeMinWasNull = True
        pt.AgeMaxWasNull = True
        pt.AgeRelativeWasNull = True
        pt.AgeResetWasNull = True
        pt.TstMinimumWasNull = True
        pt.TstMaximumWasNull = True
        pt.TstRelativeWasNull = True

        If (dr(DATASHEET_PT_PROPORTION_COLUMN_NAME) IsNot DBNull.Value) Then
            pt.Proportion = CDbl(dr(DATASHEET_PT_PROPORTION_COLUMN_NAME))
            pt.PropnWasNull = False
        End If

        If (dr(DATASHEET_AGE_MIN_COLUMN_NAME) IsNot DBNull.Value) Then
            pt.AgeMinimum = CInt(dr(DATASHEET_AGE_MIN_COLUMN_NAME))
            pt.AgeMinWasNull = False
        End If

        If (dr(DATASHEET_AGE_MAX_COLUMN_NAME) IsNot DBNull.Value) Then
            pt.AgeMaximum = CInt(dr(DATASHEET_AGE_MAX_COLUMN_NAME))
            pt.AgeMaxWasNull = False
        End If

        If (dr(DATASHEET_PT_AGE_RELATIVE_COLUMN_NAME) IsNot DBNull.Value) Then
            pt.AgeRelative = CInt(dr(DATASHEET_PT_AGE_RELATIVE_COLUMN_NAME))
            pt.AgeRelativeWasNull = False
        End If

        If (dr(DATASHEET_PT_AGE_RESET_COLUMN_NAME) IsNot DBNull.Value) Then
            pt.AgeReset = DataTableUtilities.GetDataBool(dr(DATASHEET_PT_AGE_RESET_COLUMN_NAME))
            pt.AgeResetWasNull = False
        End If

        If (dr(DATASHEET_PT_TST_MIN_COLUMN_NAME) IsNot DBNull.Value) Then
            pt.TstMinimum = CInt(dr(DATASHEET_PT_TST_MIN_COLUMN_NAME))
            pt.TstMinimumWasNull = False
        End If

        If (dr(DATASHEET_PT_TST_MAX_COLUMN_NAME) IsNot DBNull.Value) Then
            pt.TstMaximum = CInt(dr(DATASHEET_PT_TST_MAX_COLUMN_NAME))
            pt.TstMaximumWasNull = False
        End If

        If (dr(DATASHEET_PT_TST_RELATIVE_COLUMN_NAME) IsNot DBNull.Value) Then
            pt.TstRelative = CInt(dr(DATASHEET_PT_TST_RELATIVE_COLUMN_NAME))
            pt.TstRelativeWasNull = False
        End If

        Return pt

    End Function

    Private Function CreatePTRecord(
        ByVal stratumIdSource As Nullable(Of Integer),
        ByVal stateClassIdSource As Integer,
        ByVal stratumIdDest As Nullable(Of Integer),
        ByVal stateClassIdDest As Nullable(Of Integer),
        ByVal transitionTypeId As Integer,
        ByVal probability As Double,
        ByVal proportion As Nullable(Of Double),
        ByVal ageMin As Nullable(Of Integer),
        ByVal ageMax As Nullable(Of Integer),
        ByVal ageRelative As Nullable(Of Integer),
        ByVal ageReset As Nullable(Of Boolean),
        ByVal tstMin As Nullable(Of Integer),
        ByVal tstMax As Nullable(Of Integer),
        ByVal tstRelative As Nullable(Of Integer)) As DataRow

        Dim dr As DataRow = Me.m_PTDataSheet.GetData().NewRow

        dr(DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(stratumIdSource)
        dr(DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME) = stateClassIdSource
        dr(DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(stratumIdDest)
        dr(DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(stateClassIdDest)
        dr(DATASHEET_PT_TRANSITION_TYPE_COLUMN_NAME) = transitionTypeId
        dr(DATASHEET_PT_PROBABILITY_COLUMN_NAME) = probability
        dr(DATASHEET_PT_PROPORTION_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(proportion)
        dr(DATASHEET_AGE_MIN_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(ageMin)
        dr(DATASHEET_AGE_MAX_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(ageMax)
        dr(DATASHEET_PT_AGE_RELATIVE_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(ageRelative)
        dr(DATASHEET_PT_AGE_RESET_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(ageReset)
        dr(DATASHEET_PT_TST_MIN_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(tstMin)
        dr(DATASHEET_PT_TST_MAX_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(tstMax)
        dr(DATASHEET_PT_TST_RELATIVE_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(tstRelative)

#If DEBUG Then
        If (dr(DATASHEET_PT_AGE_RESET_COLUMN_NAME) IsNot DBNull.Value) Then
            Debug.Assert(CInt(dr(DATASHEET_PT_AGE_RESET_COLUMN_NAME)) = 0 Or CInt(dr(DATASHEET_PT_AGE_RESET_COLUMN_NAME)) = -1)
        End If
#End If

        Me.m_PTDataSheet.GetData().Rows.Add(dr)
        Return dr

    End Function

End Class
