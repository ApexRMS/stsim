'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.StochasticTime

Partial Class STSimTransformer

    ''' <summary>
    ''' Initializes the distribution provider
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeDistributionProvider()

        Debug.Assert(Me.m_DistributionProvider Is Nothing)
        Me.m_DistributionProvider = New STSimDistributionProvider(Me.ResultScenario, Me.m_RandomGenerator)

    End Sub

    ''' <summary>
    ''' Initializes the value for all collection items with distributions.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeDistributionValues()

        Me.m_DistributionProvider.InitializeExternalVariableValues()
        Me.m_DistributionProvider.STSimInitializeDistributionValues()

        Me.InitializeTransitionTargetDistributionValues()
        Me.InitializeTransitionAttributeTargetDistributionValues()
        Me.InitializeTransitionMultiplierDistributionValues()
        Me.InitializeTransitionDirectionMultiplierDistributionValues()
        Me.InitializeTransitionSlopeMultiplierDistributionValues()
        Me.InitializeTransitionAdjacencyMultiplierDistributionValues()

    End Sub

    Private Sub InitializeTransitionTargetDistributionValues()

        Try

            For Each t As TransitionTarget In Me.m_TransitionTargets
                t.Initialize(Me.MinimumIteration, Me.MinimumTimestep, Me.m_DistributionProvider)
            Next

        Catch ex As Exception
            Throw New ArgumentException("Transition Targets" & " -> " & ex.Message)
        End Try

    End Sub

    Private Sub InitializeTransitionAttributeTargetDistributionValues()

        Try

            For Each t As TransitionAttributeTarget In Me.m_TransitionAttributeTargets
                t.Initialize(Me.MinimumIteration, Me.MinimumTimestep, Me.m_DistributionProvider)
            Next

        Catch ex As Exception
            Throw New ArgumentException("Transition Attribute Targets" & " -> " & ex.Message)
        End Try

    End Sub

    Private Sub InitializeTransitionMultiplierDistributionValues()

        Try

            For Each t As TransitionMultiplierValue In Me.m_TransitionMultiplierValues
                t.Initialize(Me.MinimumIteration, Me.MinimumTimestep, Me.m_DistributionProvider)
            Next

        Catch ex As Exception
            Throw New ArgumentException("Transition Multiplier Values" & " -> " & ex.Message)
        End Try

    End Sub

    Private Sub InitializeTransitionDirectionMultiplierDistributionValues()

        Try

            For Each t As TransitionDirectionMultiplier In Me.m_TransitionDirectionMultipliers
                t.Initialize(Me.MinimumIteration, Me.MinimumTimestep, Me.m_DistributionProvider)
            Next

        Catch ex As Exception
            Throw New ArgumentException("Direction Multipliers" & " -> " & ex.Message)
        End Try

    End Sub

    Private Sub InitializeTransitionSlopeMultiplierDistributionValues()

        Try

            For Each t As TransitionSlopeMultiplier In Me.m_TransitionSlopeMultipliers
                t.Initialize(Me.MinimumIteration, Me.MinimumTimestep, Me.m_DistributionProvider)
            Next

        Catch ex As Exception
            Throw New ArgumentException("Transition Slope Multipliers" & " -> " & ex.Message)
        End Try

    End Sub

    Private Sub InitializeTransitionAdjacencyMultiplierDistributionValues()

        Try

            For Each t As TransitionAdjacencyMultiplier In Me.m_TransitionAdjacencyMultipliers
                t.Initialize(Me.MinimumIteration, Me.MinimumTimestep, Me.m_DistributionProvider)
            Next

        Catch ex As Exception
            Throw New ArgumentException("Transition Adjacency Multipliers" & " -> " & ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Resamples the external variable values
    ''' </summary>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <param name="frequency"></param>
    ''' <remarks></remarks>
    Private Sub ResampleExternalVariableValues(
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal frequency As DistributionFrequency)

        Try

            Me.m_DistributionProvider.SampleExternalVariableValues(iteration, timestep, frequency)

        Catch ex As Exception
            Throw New ArgumentException("External Variable Values" & " -> " & ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Resamples the distribution values
    ''' </summary>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <param name="frequency"></param>
    ''' <remarks></remarks>
    Private Sub ResampleDistributionValues(
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal frequency As DistributionFrequency)

        Try

            Me.m_DistributionProvider.SampleDistributionValues(iteration, timestep, frequency)

        Catch ex As Exception
            Throw New ArgumentException("Distribution Values" & " -> " & ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Resamples Transition Target values
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ResampleTransitionTargetValues(
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal frequency As DistributionFrequency)

        Try

            For Each t As TransitionTarget In Me.m_TransitionTargets
                t.Sample(iteration, timestep, Me.m_DistributionProvider, frequency)
            Next

        Catch ex As Exception
            Throw New ArgumentException("Transition Targets" & " -> " & ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Resamples Transition Attribute Target values
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ResampleTransitionAttributeTargetValues(
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal frequency As DistributionFrequency)

        Try

            For Each t As TransitionAttributeTarget In Me.m_TransitionAttributeTargets
                t.Sample(iteration, timestep, Me.m_DistributionProvider, frequency)
            Next

        Catch ex As Exception
            Throw New ArgumentException("Transition Attribute Targets" & " -> " & ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Resamples Transition Multiplier values
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ResampleTransitionMultiplierValues(
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal frequency As DistributionFrequency)

        Try

            For Each t As TransitionMultiplierValue In Me.m_TransitionMultiplierValues
                t.Sample(iteration, timestep, Me.m_DistributionProvider, frequency)
            Next

        Catch ex As Exception
            Throw New ArgumentException("Transition Mulitplier Values" & " -> " & ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Resamples Transition Direction Multiplier values
    ''' </summary>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <param name="frequency"></param>
    ''' <remarks></remarks>
    Private Sub ResampleTransitionDirectionMultiplierValues(
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal frequency As DistributionFrequency)

        Try

            For Each t As TransitionDirectionMultiplier In Me.m_TransitionDirectionMultipliers
                t.Sample(iteration, timestep, Me.m_DistributionProvider, frequency)
            Next

        Catch ex As Exception
            Throw New ArgumentException("Transition Direction Multipliers" & " -> " & ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Resamples Transition Slope Multiplier values
    ''' </summary>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <param name="frequency"></param>
    ''' <remarks></remarks>
    Private Sub ResampleTransitionSlopeMultiplierValues(
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal frequency As DistributionFrequency)

        Try

            For Each t As TransitionSlopeMultiplier In Me.m_TransitionSlopeMultipliers
                t.Sample(iteration, timestep, Me.m_DistributionProvider, frequency)
            Next

        Catch ex As Exception
            Throw New ArgumentException("Transition Slope Multipliers" & " -> " & ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Resamples Transition Adjacency Multiplier values
    ''' </summary>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <param name="frequency"></param>
    ''' <remarks></remarks>
    Private Sub ResampleTransitionAdjacencyMultiplierValues(
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal frequency As DistributionFrequency)

        Try

            For Each t As TransitionAdjacencyMultiplier In Me.m_TransitionAdjacencyMultipliers
                t.Sample(iteration, timestep, Me.m_DistributionProvider, frequency)
            Next

        Catch ex As Exception
            Throw New ArgumentException("Transition Adjacency Multipliers" & " -> " & ex.Message)
        End Try

    End Sub

End Class
