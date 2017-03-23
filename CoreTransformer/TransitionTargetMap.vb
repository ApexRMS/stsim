'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.Core

Class TransitionTargetMap
    Inherits STSimMapBase3(Of TransitionTarget)

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
        ByVal iteration As Integer,
        ByVal timestep As Integer) As TransitionTarget

        Return MyBase.GetItem(
            transitionGroupId,
            stratumId,
            secondaryStratumId,
            iteration,
            timestep)

    End Function

    Private Sub TryAddItem(ByVal item As TransitionTarget)

        Try

            Me.AddItem(
                item.TransitionGroupId,
                item.StratumId,
                item.SecondaryStratumId,
                item.Iteration,
                item.Timestep,
                item)

        Catch ex As STSimMapDuplicateItemException

            Dim template As String =
                "A duplicate transition target was detected: More information:" & vbCrLf &
                "Transition Group={0}, {1}={2}, {3}={4}, Iteration={5}, Timestep={6}." & vbCrLf &
                "NOTE: A user defined distribution can result in additional transition targets when the model is run."

            ExceptionUtils.ThrowArgumentException(
                template,
                Me.GetTransitionGroupName(item.TransitionGroupId),
                Me.PrimaryStratumLabel,
                Me.GetStratumName(item.StratumId),
                Me.SecondaryStratumLabel,
                Me.GetSecondaryStratumName(item.SecondaryStratumId),
                STSimMapBase.FormatValue(item.Iteration),
                STSimMapBase.FormatValue(item.Timestep))

        End Try

        Debug.Assert(Me.HasItems())

    End Sub

End Class
