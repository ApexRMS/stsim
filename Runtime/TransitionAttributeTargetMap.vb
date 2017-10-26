'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core

Class TransitionAttributeTargetMap
    Inherits STSimMapBase4(Of TransitionAttributeTarget)

    Public Sub New(
        ByVal scenario As Scenario,
        ByVal collection As TransitionAttributeTargetCollection)

        MyBase.New(scenario)

        For Each Item As TransitionAttributeTarget In collection
            Me.TryAddItem(Item)
        Next

    End Sub

    Public Function GetAttributeTarget(
        ByVal transitionAttributeTypeId As Integer,
        ByVal stratumId As Integer,
        ByVal secondaryStratumId As Nullable(Of Integer),
        ByVal tertiaryStratumId As Nullable(Of Integer),
        ByVal iteration As Integer,
        ByVal timestep As Integer) As TransitionAttributeTarget

        Return MyBase.GetItem(
            transitionAttributeTypeId,
            stratumId,
            secondaryStratumId,
            tertiaryStratumId,
            iteration,
            timestep)

    End Function

    Private Sub TryAddItem(ByVal item As TransitionAttributeTarget)

        Try

            MyBase.AddItem(
                item.TransitionAttributeTypeId,
                item.StratumId,
                item.SecondaryStratumId,
                item.TertiaryStratumId,
                item.Iteration,
                item.Timestep,
                item)

        Catch ex As STSimMapDuplicateItemException

            Dim template As String =
                "A duplicate transition attribute target was detected: More information:" & vbCrLf &
                "Transition Attribute Type={0}, {1}={2}, {3}={4}, {5}={6}, Iteration={7}, Timestep={8}." & vbCrLf &
                "NOTE: A user defined distribution can result in additional transition attribute targets when the model is run."

            ExceptionUtils.ThrowArgumentException(
                template,
                Me.GetTransitionAttributeTypeName(item.TransitionAttributeTypeId),
                Me.PrimaryStratumLabel,
                Me.GetStratumName(item.StratumId),
                Me.SecondaryStratumLabel,
                Me.GetSecondaryStratumName(item.SecondaryStratumId),
                Me.TertiaryStratumLabel,
                Me.GetTertiaryStratumName(item.TertiaryStratumId),
                STSimMapBase.FormatValue(item.Iteration),
                STSimMapBase.FormatValue(item.Timestep))

        End Try

    End Sub

End Class