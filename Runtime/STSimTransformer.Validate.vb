'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Partial Class STSimTransformer

#If DEBUG Then

    ''' <summary>
    ''' Validates the shufflable transition groups
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub VALIDATE_SHUFFLABLE_GROUPS()

        Dim d As New Dictionary(Of Integer, Boolean)

        For Each tg As TransitionGroup In Me.m_ShufflableTransitionGroups
            d.Add(tg.TransitionGroupId, True)
        Next

    End Sub

    ''' <summary>
    ''' Validates that the specified cell has been initialized correctly
    ''' </summary>
    ''' <param name="c"></param>
    ''' <remarks></remarks>
    Private Sub VALIDATE_INITIALIZED_CELL(
        ByVal c As Cell,
        ByVal iteration As Integer,
        ByVal timestep As Integer)

        'Make sure the ages are within the correct initial ranges
        Dim dt As DeterministicTransition = Me.GetDeterministicTransition(c, iteration, timestep)

        If (dt IsNot Nothing) Then

            Debug.Assert(c.Age >= dt.AgeMinimum)
            Debug.Assert(c.Age <= dt.AgeMaximum)

        End If

        Debug.Assert(c.Age >= 0)

    End Sub

    ''' <summary>
    ''' Validates that the specified cell has the correct transitions
    ''' </summary>
    ''' <param name="c"></param>
    ''' <remarks></remarks>
    Private Sub VALIDATE_CELL_TRANSITIONS(ByVal c As Cell, ByVal iteration As Integer, ByVal timestep As Integer)

        Dim trlist As TransitionCollection = Me.GetTransitionCollection(c, iteration, timestep)

        If (trlist Is Nothing) Then
            Debug.Assert(c.Transitions.Count = 0)
        Else

            Debug.Assert(trlist.Count > 0)
            Dim tcount As Integer = 0

            For Each tr As Transition In trlist

                If (c.Age < tr.AgeMinimum) Then
                    Continue For
                End If

                If (c.Age > tr.AgeMaximum) Then
                    Continue For
                End If

                If (Not Me.CompareTstValues(c, tr)) Then
                    Continue For
                End If

                Debug.Assert(c.Transitions.Contains(tr))
                tcount += 1

            Next

            Debug.Assert(tcount = c.Transitions.Count)

            For Each tr As Transition In c.Transitions
                Debug.Assert(trlist.Contains(tr))
            Next

        End If

    End Sub

#End If

End Class
