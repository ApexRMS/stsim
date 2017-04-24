'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core

Class TransitionPatchPrioritizationMap
    Inherits STSimMapBase1(Of TransitionPatchPrioritization)

    Public Sub New(
        ByVal scenario As Scenario,
        ByVal items As TransitionPatchPrioritizationCollection)

        MyBase.New(scenario)

        For Each Item As TransitionPatchPrioritization In items
            Me.TryAddItem(Item)
        Next

    End Sub

    Public Function GetPatchPrioritization(
        ByVal transitionGroupId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer) As TransitionPatchPrioritization

        Return MyBase.GetItem(
            transitionGroupId,
            iteration,
            timestep)

    End Function

    Private Sub TryAddItem(ByVal item As TransitionPatchPrioritization)

        Try

            Me.AddItem(
                item.TransitionGroupId,
                item.Iteration,
                item.Timestep,
                item)

        Catch ex As STSimMapDuplicateItemException

            Dim template As String =
                "A duplicate transition patch prioritization was detected: More information:" & vbCrLf &
                "Transition Group={0}, Iteration={1}, Timestep={2}"

            ExceptionUtils.ThrowArgumentException(
                template,
                Me.GetTransitionGroupName(item.TransitionGroupId),
                STSimMapBase.FormatValue(item.Iteration),
                STSimMapBase.FormatValue(item.Timestep))

        End Try

    End Sub

End Class
