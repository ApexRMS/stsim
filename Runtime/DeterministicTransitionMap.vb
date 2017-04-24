'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core

Class DeterministicTransitionMap
    Inherits STSimMapBase2(Of DeterministicTransition)

    Public Sub New(
        ByVal scenario As Scenario,
        ByVal transitions As DeterministicTransitionCollection)

        MyBase.New(scenario)

        For Each t As DeterministicTransition In transitions
            AddTransition(t)
        Next

    End Sub

    Public Function GetDeterministicTransition(
        ByVal stratumId As Nullable(Of Integer),
        ByVal stateClassId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer) As DeterministicTransition

        Return Me.GetItem(stratumId, stateClassId, iteration, timestep)

    End Function

    Private Sub AddTransition(ByVal t As DeterministicTransition)

        Try

            Me.AddItem(
                t.StratumIdSource,
                t.StateClassIdSource,
                t.Iteration,
                t.Timestep,
                t)

        Catch ex As STSimMapDuplicateItemException

            Dim template As String =
                "A duplicate deterministic transition was detected: More information:" & vbCrLf &
                "Source {0}={1}, Source State Class={2}, Iteration={3}, Timestep={4}."

            ExceptionUtils.ThrowArgumentException(
                template,
                Me.PrimaryStratumLabel,
                Me.GetStratumName(t.StratumIdSource),
                Me.GetStateClassName(t.StateClassIdSource),
                STSimMapBase.FormatValue(t.Iteration),
                STSimMapBase.FormatValue(t.Timestep))

        End Try

        Debug.Assert(Me.HasItems())

    End Sub

End Class
