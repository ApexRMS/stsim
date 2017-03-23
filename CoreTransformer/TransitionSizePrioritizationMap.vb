'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.Core

Class TransitionSizePrioritizationMap
    Inherits STSimMapBase2(Of TransitionSizePrioritization)

    Public Sub New(
        ByVal scenario As Scenario,
        ByVal collection As TransitionSizePrioritizationCollection)

        MyBase.New(scenario)

        For Each Item As TransitionSizePrioritization In collection
            Me.TryAddItem(Item)
        Next

    End Sub

    Public Function GetSizePrioritization(
        ByVal transitionGroupId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal stratumId As Integer) As TransitionSizePrioritization

        Return MyBase.GetItem(
            transitionGroupId,
            stratumId,
            iteration,
            timestep)

    End Function

    Private Sub TryAddItem(ByVal item As TransitionSizePrioritization)

        Try

            Me.AddItem(
                item.TransitionGroupId,
                item.StratumId,
                item.Iteration,
                item.Timestep,
                item)

        Catch ex As STSimMapDuplicateItemException

            Dim template As String =
                "A duplicate transition size prioritization was detected: More information:" & vbCrLf &
                "Transition Group={0}, {1}={2}, Iteration={3}, Timestep={4}"

            ExceptionUtils.ThrowArgumentException(
                template,
                Me.GetTransitionGroupName(item.TransitionGroupId),
                Me.PrimaryStratumLabel,
                Me.GetStratumName(item.StratumId),
                STSimMapBase.FormatValue(item.Iteration),
                STSimMapBase.FormatValue(item.Timestep))

        End Try

    End Sub

End Class
