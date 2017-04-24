'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

''' <summary>
''' IntervalMeanTimestepMap
''' </summary>
''' <remarks></remarks>
Class IntervalMeanTimestepMap

    Private m_Map As New Dictionary(Of Integer, Integer)
    Private m_MinimumTimestep As Integer
    Private m_MaximumTimestep As Integer
    Private m_TimestepZero As Integer
    Private m_Frequency As Integer

    Public Sub New(
        ByVal minimumTimestep As Integer,
        ByVal maximumTimestep As Integer,
        ByVal timestepZero As Integer,
        ByVal frequency As Integer)

        Me.m_MinimumTimestep = minimumTimestep
        Me.m_MaximumTimestep = maximumTimestep
        Me.m_TimestepZero = timestepZero
        Me.m_Frequency = frequency

        Me.FillMap()

    End Sub

    Public Function GetValue(ByVal value As Integer) As Integer
        Return Me.m_Map(value)
    End Function

    Private Sub FillMap()

        'Handle special cases that are relatively common and/or very simple
        If (Me.m_MinimumTimestep = Me.m_MaximumTimestep) Then

            Me.m_Map.Add(Me.m_MinimumTimestep, Me.m_MaximumTimestep)

        ElseIf (Me.m_Frequency = 1) Then

            For CurrentTimestep As Integer = Me.m_MinimumTimestep To Me.m_MaximumTimestep
                Me.m_Map.Add(CurrentTimestep, CurrentTimestep)
            Next

        Else

            Dim CurrentTimestep As Integer = Me.m_MinimumTimestep

            While (CurrentTimestep <= Me.m_MaximumTimestep)

                Dim AggregatorTimestep As Integer = Me.GetNextAggregatorTimestep(CurrentTimestep)
                Debug.Assert(AggregatorTimestep <= Me.m_MaximumTimestep)

                While (CurrentTimestep <= AggregatorTimestep)

                    Me.m_Map.Add(CurrentTimestep, AggregatorTimestep)
                    CurrentTimestep += 1

                End While

            End While

        End If

#If DEBUG Then

        Debug.Assert(Me.m_Map.Count = (Me.m_MaximumTimestep - Me.m_MinimumTimestep + 1))

        For t As Integer = Me.m_MinimumTimestep To Me.m_MaximumTimestep
            Debug.Assert(Me.m_Map.ContainsKey(t))
        Next

#End If

    End Sub

    ''' <summary>
    ''' Gets the next 'Aggregator' timestep
    ''' </summary>
    ''' <param name="currentAggregatorTimestep"></param>
    ''' <returns></returns>
    ''' <remarks>This function exists to support the 'calculate as interval mean values' feature for summary transition output.</remarks>
    Private Function GetNextAggregatorTimestep(ByVal currentAggregatorTimestep As Integer) As Integer

        Dim NextAggregatorTimestep As Integer = currentAggregatorTimestep

        While (NextAggregatorTimestep < Me.m_MaximumTimestep)

            If (((NextAggregatorTimestep - Me.m_TimestepZero) Mod Me.m_Frequency) = 0) Then
                Return NextAggregatorTimestep
            End If

            NextAggregatorTimestep += 1

        End While

        Debug.Assert(NextAggregatorTimestep = Me.m_MaximumTimestep)
        Return NextAggregatorTimestep

    End Function

End Class
