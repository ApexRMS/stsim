'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2016 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.Core

Class TransitionPathwayAutoCorrelationMap
    Inherits STSimMapBase3(Of TransitionPathwayAutoCorrelation)

    Public Sub New(
        ByVal scenario As Scenario,
        ByVal collection As TransitionPathwayAutoCorrelationCollection)

        MyBase.New(scenario)

        For Each Item As TransitionPathwayAutoCorrelation In collection
            Me.TryAddItem(Item)
        Next

    End Sub

    Public Function GetCorrelation(
        ByVal transitionGroupId As Integer,
        ByVal stratumId As Integer,
        ByVal secondaryStratumId As Nullable(Of Integer),
        ByVal iteration As Integer,
        ByVal timestep As Integer) As TransitionPathwayAutoCorrelation

        Return Me.GetItem(
            transitionGroupId,
            stratumId,
            secondaryStratumId,
            iteration,
            timestep)

    End Function

    Private Sub TryAddItem(ByVal item As TransitionPathwayAutoCorrelation)

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
                "A duplicate transition pathway auto-correlation was detected: More information:" & vbCrLf &
                "Transition Group={0}, {1}={2}, {3}={4}, Iteration={5}, Timestep={6}."

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

    End Sub

End Class