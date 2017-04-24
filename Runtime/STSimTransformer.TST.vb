'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Partial Class STSimTransformer

    ''' <summary>
    ''' Initializes the specified cell's Tst values
    ''' </summary>
    ''' <param name="simulationCell"></param>
    ''' <remarks></remarks>
    Private Sub InitializeCellTstValues(ByVal simulationCell As Cell, ByVal iteration As Integer)

        If (simulationCell.TstValues.Count > 0) Then

            Debug.Assert(Me.m_TstTransitionGroupMap.HasItems)

            'If there is a randomize value for this cell's stratum, then use that value to initialize
            'every Tst in the TstValues list.  If there is no value for this cell's stratum then set the
            'initial value to zero.

            For Each tg As TransitionGroup In Me.TransitionGroups

                If simulationCell.TstValues.Contains(tg.TransitionGroupId) Then

                    Dim TstRand As TstRandomize = Me.m_TstRandomizeMap.GetTstRandomize(
                        tg.TransitionGroupId,
                        simulationCell.StratumId,
                        simulationCell.SecondaryStratumId,
                        simulationCell.StateClassId,
                        iteration)

                    Dim TstMaxRandValue As Integer = 0
                    Dim TstMinRandValue As Integer = 0

                    If (TstRand IsNot Nothing) Then

                        TstMinRandValue = TstRand.MinInitialTst
                        TstMaxRandValue = TstRand.MaxInitialTst

                    End If

                    If (TstMaxRandValue = Integer.MaxValue) Then
                        TstMaxRandValue = Integer.MaxValue - 1
                    End If

                    Dim r As Integer = Me.m_RandomGenerator.GetNextInteger(TstMinRandValue, TstMaxRandValue + 1)
                    Dim cellTst As Tst = simulationCell.TstValues(tg.TransitionGroupId)

                    cellTst.TstValue = r

                End If

            Next

        End If

    End Sub

    ''' <summary>
    ''' Compares the simulation cell's Tst value to the transitions Tst min and max
    ''' </summary>
    ''' <param name="simulationCell">The simulation cell</param>
    ''' <param name="tr">The transition</param>
    ''' <returns>TRUE if the cell's Tst is in range and FALSE if not.</returns>
    ''' <remarks></remarks>
    Private Function CompareTstValues(ByVal simulationCell As Cell, ByVal tr As Transition) As Boolean

        'If the transition's Transition Type doesn't have an associated Transition Group in 
        'Time-Since-Transition groups then return True.

        Dim tstgroup As TstTransitionGroup = Me.m_TstTransitionGroupMap.GetGroup(
            tr.TransitionTypeId,
            simulationCell.StratumId,
            simulationCell.SecondaryStratumId)

        If (tstgroup Is Nothing) Then
            Return True
        End If

        'Get the matching Tst for the simulation cell
        Dim cellTst As Tst = simulationCell.TstValues(tstgroup.GroupId)

        'If the cell Tst value is within the Transition's TstMin and TstMax range then return TRUE
        If (cellTst.TstValue >= tr.TstMinimum And cellTst.TstValue <= tr.TstMaximum) Then
            Return True
        End If

        Return False

    End Function

    ''' <summary>
    ''' Changes a simulation cell's Tst value for a probabilistic transition
    ''' </summary>
    ''' <param name="simulationCell">The simulation cell to change</param>
    ''' <param name="tr">The probabilistic transition</param>
    ''' <remarks></remarks>
    Private Sub ChangeCellTstForProbabilisticTransition(ByVal simulationCell As Cell, ByVal tr As Transition)

        If (simulationCell.TstValues.Count = 0) Then

            Debug.Assert(Not Me.m_TstTransitionGroupMap.HasItems)
            Return

        End If

        Dim tt As TransitionType = Me.m_TransitionTypes(tr.TransitionTypeId)

        For Each tg As TransitionGroup In tt.TransitionGroups

            If (simulationCell.TstValues.Contains(tg.TransitionGroupId)) Then

                Dim celltst As Tst = simulationCell.TstValues(tg.TransitionGroupId)
                celltst.TstValue += tr.TstRelative

                If (celltst.TstValue < 0) Then
                    celltst.TstValue = 0
                End If

            End If

        Next

    End Sub

End Class
