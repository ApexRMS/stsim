'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core

Class TransitionTargetMap
    Inherits STSimMapBase4(Of TransitionTarget)

    Public Sub New(
        ByVal scenario As Scenario,
        ByVal collection As TransitionTargetCollection)

        MyBase.New(scenario)

        For Each Item As TransitionTarget In collection
            Me.TryAddItem(Item)
        Next

    End Sub

    Public Function GetTransitionTarget(
        ByVal transitionGroupId As Integer,
        ByVal stratumId As Integer,
        ByVal secondaryStratumId As Nullable(Of Integer),
        ByVal tertiaryStratumId As Nullable(Of Integer),
        ByVal iteration As Integer,
        ByVal timestep As Integer) As TransitionTarget

        Return MyBase.GetItem(
            transitionGroupId,
            stratumId,
            secondaryStratumId,
            tertiaryStratumId,
            iteration,
            timestep)

    End Function

    Private Sub TryAddItem(ByVal item As TransitionTarget)

        Try

            Me.AddItem(
                item.TransitionGroupId,
                item.StratumId,
                item.SecondaryStratumId,
                item.TertiaryStratumId,
                item.Iteration,
                item.Timestep,
                item)

        Catch ex As STSimMapDuplicateItemException

            Dim template As String =
                "A duplicate transition target was detected: More information:" & vbCrLf &
                "Transition Group={0}, {1}={2}, {3}={4}, {5}={6}, Iteration={7}, Timestep={8}." & vbCrLf &
                "NOTE: A user defined distribution can result in additional transition targets when the model is run."

            ExceptionUtils.ThrowArgumentException(
                template,
                Me.GetTransitionGroupName(item.TransitionGroupId),
                Me.PrimaryStratumLabel,
                Me.GetStratumName(item.StratumId),
                Me.SecondaryStratumLabel,
                Me.GetSecondaryStratumName(item.SecondaryStratumId),
                Me.TertiaryStratumLabel,
                Me.GetTertiaryStratumName(item.TertiaryStratumId),
                STSimMapBase.FormatValue(item.Iteration),
                STSimMapBase.FormatValue(item.Timestep))

        End Try

        Debug.Assert(Me.HasItems())

    End Sub

End Class
