'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.Core
Imports SyncroSim.StochasticTime

Partial Class STSimTransformer

    Private Sub ChangeCellAgeForProbabilisticTransition(
        ByVal simulationCell As Cell,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal transition As Transition)

        simulationCell.Age = Me.DetermineTargetAgeProbabilistic(
            simulationCell.Age,
            simulationCell.StratumId,
            simulationCell.StateClassId,
            iteration,
            timestep,
            transition)

    End Sub

    Public Function DetermineTargetAgeProbabilistic(
        ByVal currentCellAge As Integer,
        ByVal destinationStratumId As Nullable(Of Integer),
        ByVal destinationStateClassId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal transition As Transition) As Integer

        Return Me.InternalDetermineTargetAgeProbabilistic(
            currentCellAge,
            destinationStratumId,
            destinationStateClassId,
            iteration,
            timestep,
            transition)

    End Function

    Private Function InternalDetermineTargetAgeProbabilistic(
        ByVal currentCellAge As Integer,
        ByVal destinationStratumId As Nullable(Of Integer),
        ByVal destinationStateClassId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal transition As Transition) As Integer

        Dim dt As DeterministicTransition = Nothing

        Me.GetDeterministicTransition(destinationStratumId, destinationStateClassId, iteration, timestep)

        Dim AgeMin As Integer
        Dim AgeMax As Integer

        If (dt Is Nothing) Then
            AgeMin = 0
            AgeMax = Integer.MaxValue
        Else
            AgeMin = dt.AgeMinimum
            AgeMax = dt.AgeMaximum
        End If

        Dim NewAge As Integer

        If Not (transition.AgeReset) Then

            Dim TargetSimCellAge As Integer = (currentCellAge + transition.AgeRelative)

            If (TargetSimCellAge < AgeMin) Then
                NewAge = AgeMin
            ElseIf (TargetSimCellAge > AgeMax) Then
                NewAge = AgeMax
            Else
                NewAge = TargetSimCellAge
            End If

        Else

            Dim TargetSimCellAge As Integer = Math.Max(AgeMin, AgeMin + transition.AgeRelative)

            If (TargetSimCellAge > AgeMax) Then
                TargetSimCellAge = AgeMax
            End If

            NewAge = TargetSimCellAge

        End If

        If (NewAge < 0) Then
            NewAge = 0
        End If

        Return NewAge

    End Function

    Private Sub InitializeCellAge(
        ByVal simulationCell As Cell,
        ByVal stratumId As Integer,
        ByVal stateClassId As Integer,
        ByVal minimumAge As Integer,
        ByVal maximumAge As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer)

        Dim dt As DeterministicTransition = Nothing

        If (Me.m_DeterministicTransitionMap IsNot Nothing) Then
            dt = Me.GetDeterministicTransition(stratumId, stateClassId, iteration, timestep)
        End If

        Debug.Assert(minimumAge <> Integer.MaxValue)

        If (dt Is Nothing) Then

            If maximumAge = Integer.MaxValue Then
                simulationCell.Age = minimumAge
            Else
                simulationCell.Age = Me.m_RandomGenerator.GetNextInteger(minimumAge, maximumAge + 1)
            End If

        Else

            Dim AgeMinOut As Integer = 0
            Dim AgeMaxOut As Integer = 0

            GetAgeMinMax(dt, minimumAge, maximumAge, AgeMinOut, AgeMaxOut)

            If AgeMaxOut = Integer.MaxValue Then
                simulationCell.Age = AgeMinOut
            Else
                simulationCell.Age = Me.m_RandomGenerator.GetNextInteger(AgeMinOut, AgeMaxOut + 1)
            End If

        End If

    End Sub

    ''' <summary>
    ''' Initializes the age reporting helper
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeAgeReportingHelper()

        Debug.Assert(Me.m_AgeReportingHelper Is Nothing)

        Me.m_AgeReportingHelper = New AgeHelper(False, 0, 0)
        Dim dr As DataRow = Me.Project.GetDataSheet(DATASHEET_AGE_TYPE_NAME).GetDataRow()

        If (dr Is Nothing) Then
            Return
        End If

        If (dr(DATASHEET_AGE_TYPE_FREQUENCY_COLUMN_NAME) IsNot DBNull.Value) Then

            If (dr(DATASHEET_AGE_TYPE_MAXIMUM_COLUMN_NAME) Is DBNull.Value) Then

                Me.AddStatusRecord(StatusRecordType.Warning,
                    "Age reporting freqency set without age reporting maximum.  Not reporting ages.")

                Return

            End If

        End If

        If (dr(DATASHEET_AGE_TYPE_MAXIMUM_COLUMN_NAME) IsNot DBNull.Value) Then

            If (dr(DATASHEET_AGE_TYPE_FREQUENCY_COLUMN_NAME) Is DBNull.Value) Then

                Me.AddStatusRecord(StatusRecordType.Warning,
                    "Age reporting maximum set without age reporting frequency.  Not reporting ages.")

                Return

            End If

        End If

        Dim f As Integer = CInt(dr(DATASHEET_AGE_TYPE_FREQUENCY_COLUMN_NAME))
        Dim m As Integer = CInt(dr(DATASHEET_AGE_TYPE_MAXIMUM_COLUMN_NAME))

        If (m < f) Then

            Me.AddStatusRecord(StatusRecordType.Warning,
                "Age reporting maximum is less than age reporting frequency.  Not reporting ages.")

            Return

        End If

        Me.m_AgeReportingHelper = New AgeHelper(True, f, m)

    End Sub

    ''' <summary>
    ''' Gets the age min and age max for the specified deterministic transition and age values
    ''' </summary>
    ''' <param name="dt">The deterministic transition</param>
    ''' <param name="ageMinIn">The minimum age IN value</param>
    ''' <param name="ageMaxIn">The maximum age IN value</param>
    ''' <param name="ageMinOut">The minimum age OUT value</param>
    ''' <param name="ageMaxOut">The maximum age OUT value</param>
    ''' <remarks></remarks>
    Friend Shared Sub GetAgeMinMax(
        ByVal dt As DeterministicTransition,
        ByVal ageMinIn As Integer,
        ByVal ageMaxIn As Integer,
        ByRef ageMinOut As Integer,
        ByRef ageMaxOut As Integer)

        'Normalize
        Dim dtagemin As Integer = Math.Min(dt.AgeMinimum, dt.AgeMaximum)
        Dim dtagemax As Integer = Math.Max(dt.AgeMinimum, dt.AgeMaximum)

        'This should already be normalized
        Debug.Assert(ageMinIn <= ageMaxIn)

        'If any age value is outside of the dt value, set the age value equal to the nearest dt value.
        'For example, if ageMinIn > dt.agemax the set ageMinIn = dt.agemax.

        If (ageMinIn < dtagemin) Then
            ageMinIn = dtagemin
        End If

        If (ageMaxIn < dtagemin) Then
            ageMaxIn = dtagemin
        End If

        If (ageMinIn > dtagemax) Then
            ageMinIn = dtagemax
        End If

        If (ageMaxIn > dtagemax) Then
            ageMaxIn = dtagemax
        End If

        ageMinOut = ageMinIn
        ageMaxOut = ageMaxIn

        Debug.Assert(ageMinOut <= ageMaxOut)

    End Sub

End Class
