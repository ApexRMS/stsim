'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'**********************************************************************************

Imports System.Drawing
Imports System.Windows.Forms
Imports SyncroSim.Common.Forms

Partial Class TransitionDiagram

    Protected Overrides Sub OnDropSelectedShapes(e As DiagramDragEventArgs)

        MyBase.OnDropSelectedShapes(e)

        Dim Analyzer As New DTAnalyzer(Me.m_DTDataSheet.GetData, Me.m_DataFeed.Project)

        Me.SaveSelection()
        Me.RecordNewStateClassLocations(Analyzer)
        Me.InternalRefreshLookups()
        Me.InternalRefreshTransitionLines()

        'DEVTODO: It would be better not to do this, but there is no obvious way to tell
        'if the user is about to drop the shapes they are dragging.  Is there?

        Me.m_DTDataSheet.BeginModifyRows()
        Me.m_DTDataSheet.EndModifyRows()

        Me.RestoreSelection()
        Me.Focus()

    End Sub

    Protected Overrides Sub OnPaint(e As System.Windows.Forms.PaintEventArgs)

        MyBase.OnPaint(e)

        If (Me.m_IsFilterApplied) Then
            e.Graphics.DrawImage(My.Resources.Filter16x16, New Point(4, 4))
        End If

    End Sub

    Protected Overrides Sub DrawLines(g As System.Drawing.Graphics)

        Me.DrawPTLines(g, False)
        Me.DrawPTLines(g, True)
        Me.DrawDTLines(g)

    End Sub

    Protected Overrides Sub OnShapeSelectionChanged()

        MyBase.OnShapeSelectionChanged()

        Me.InternalResetTransitionLines()
        Me.m_SelectionStatic = True

        For Each Shape As StateClassShape In Me.SelectedShapes

            If (Not Shape.IsStatic) Then
                Me.m_SelectionStatic = False
            End If

            If (ModifierKeys = Keys.Shift) Then

                For Each l As DeterministicTransitionLine In Shape.IncomingDTLines
                    l.IsSelected = True
                    l.LineColor = TRANSITION_SELECTED_LINE_COLOR
                Next

                For Each l As ProbabilisticTransitionLine In Shape.IncomingPTLines
                    l.IsSelected = True
                    l.LineColor = TRANSITION_SELECTED_LINE_COLOR
                Next

            Else

                For Each l As DeterministicTransitionLine In Shape.OutgoingDTLines
                    l.IsSelected = True
                    l.LineColor = TRANSITION_SELECTED_LINE_COLOR
                Next

                For Each l As ProbabilisticTransitionLine In Shape.OutgoingPTLines
                    l.IsSelected = True
                    l.LineColor = TRANSITION_SELECTED_LINE_COLOR
                Next

            End If

        Next

        Me.Invalidate()

    End Sub

End Class
