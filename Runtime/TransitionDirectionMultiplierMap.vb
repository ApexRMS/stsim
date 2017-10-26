'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core
Imports SyncroSim.StochasticTime

Class TransitionDirectionMultiplierMap
    Inherits STSimMapBase5(Of TransitionDirectionMultiplier)

    Private m_DistributionProvider As STSimDistributionProvider

    Public Sub New(
        ByVal scenario As Scenario,
        ByVal multipliers As TransitionDirectionMultiplierCollection,
        ByVal distributionProvider As STSimDistributionProvider)

        MyBase.New(scenario)

        Me.m_DistributionProvider = distributionProvider

        For Each Item As TransitionDirectionMultiplier In multipliers
            Me.TryAddItem(Item)
        Next

    End Sub

    Public Function GetDirectionMultiplier(
        ByVal transitionGroupId As Integer,
        ByVal stratumId As Integer,
        ByVal secondaryStratumId As Nullable(Of Integer),
        ByVal tertiaryStratumId As Nullable(Of Integer),
        ByVal cardinalDirection As CardinalDirection,
        ByVal iteration As Integer,
        ByVal timestep As Integer) As Double

        Dim v As TransitionDirectionMultiplier = Me.GetItem(
            transitionGroupId,
            stratumId,
            secondaryStratumId,
            tertiaryStratumId,
            cardinalDirection,
            iteration,
            timestep)

        If (v Is Nothing) Then
            Return 1.0
        End If

        v.Sample(iteration, timestep, Me.m_DistributionProvider, DistributionFrequency.Always)
        Return v.CurrentValue.Value

    End Function

    Private Sub TryAddItem(ByVal item As TransitionDirectionMultiplier)

        Try

            Me.AddItem(
                item.TransitionGroupId,
                item.StratumId,
                item.SecondaryStratumId,
                item.TertiaryStratumId,
                item.CardinalDirection,
                item.Iteration,
                item.Timestep,
                item)

        Catch ex As STSimMapDuplicateItemException

            Dim template As String =
                "A duplicate transition direction multiplier was detected: More information:" & vbCrLf &
                "Transition Group={0}, {1}={2}, {3}={4}, {5}={6}, Iteration={7}, Timestep={8}, Cardinal Direction={9}." & vbCrLf &
                "NOTE: A user defined distribution can result in additional transition direction multipliers when the model is run."

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
                STSimMapBase.FormatValue(item.Timestep),
                GetCardinalDirection(item.CardinalDirection))

        End Try

    End Sub

    Private Shared Function GetCardinalDirection(ByVal value As CardinalDirection) As String

        If (value = CardinalDirection.N) Then
            Return "N"
        ElseIf (value = CardinalDirection.NE) Then
            Return "NE"
        ElseIf (value = CardinalDirection.E) Then
            Return "E"
        ElseIf (value = CardinalDirection.SE) Then
            Return "SE"
        ElseIf (value = CardinalDirection.S) Then
            Return "S"
        ElseIf (value = CardinalDirection.SW) Then
            Return "SW"
        ElseIf (value = CardinalDirection.W) Then
            Return "W"
        Else
            Debug.Assert(value = CardinalDirection.NW)
            Return "NW"
        End If

    End Function

End Class
