'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core
Imports SyncroSim.StochasticTime

Class TransitionSlopeMultiplierMap
    Inherits STSimMapBase3(Of SortedList(Of Integer, TransitionSlopeMultiplier))

    Private m_DistributionProvider As STSimDistributionProvider

    Public Sub New(
        ByVal scenario As Scenario,
        ByVal multipliers As TransitionSlopeMultiplierCollection,
        ByVal distributionProvider As STSimDistributionProvider)

        MyBase.New(scenario)

        Me.m_DistributionProvider = distributionProvider

        For Each Item As TransitionSlopeMultiplier In multipliers
            Me.AddSlopeMultiplier(Item)
        Next

    End Sub

    Public Function GetSlopeMultiplier(
        ByVal transitionGroupId As Integer,
        ByVal stratumId As Integer,
        ByVal secondaryStratumId As Nullable(Of Integer),
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal slope As Double) As Double

        Dim lst As SortedList(Of Integer, TransitionSlopeMultiplier) =
            Me.GetItem(
                transitionGroupId,
                stratumId,
                secondaryStratumId,
                iteration,
                timestep)

        If (lst Is Nothing) Then
            Return 1.0
        End If

        If (lst.Count = 1) Then

            Dim tsm As TransitionSlopeMultiplier = lst.First.Value
            tsm.Sample(iteration, timestep, Me.m_DistributionProvider, DistributionFrequency.Always)

            Return tsm.CurrentValue.Value

        End If

        Dim SlopeInt As Integer = CInt(slope)

        If (lst.ContainsKey(SlopeInt)) Then

            Dim tsm As TransitionSlopeMultiplier = lst(SlopeInt)
            tsm.Sample(iteration, timestep, Me.m_DistributionProvider, DistributionFrequency.Always)

            Return tsm.CurrentValue.Value

        End If

        Dim PrevKey As Integer = -91
        Dim ThisKey As Integer = -91

        For Each k As Integer In lst.Keys

            Debug.Assert(k <> SlopeInt)

            If (k > SlopeInt) Then

                ThisKey = k
                Exit For

            End If

            PrevKey = k

        Next

        If (PrevKey = -91) Then

            Dim tsm As TransitionSlopeMultiplier = lst.First.Value
            tsm.Sample(iteration, timestep, Me.m_DistributionProvider, DistributionFrequency.Always)

            Return tsm.CurrentValue.Value

        End If

        If (ThisKey = -91) Then

            Dim tsm As TransitionSlopeMultiplier = lst.Last.Value
            tsm.Sample(iteration, timestep, Me.m_DistributionProvider, DistributionFrequency.Always)

            Return tsm.CurrentValue.Value

        End If

        Dim PrevMult As TransitionSlopeMultiplier = lst(PrevKey)
        Dim ThisMult As TransitionSlopeMultiplier = lst(ThisKey)

        PrevMult.Sample(iteration, timestep, Me.m_DistributionProvider, DistributionFrequency.Always)
        ThisMult.Sample(iteration, timestep, Me.m_DistributionProvider, DistributionFrequency.Always)

        Return Interpolate(
            PrevKey,
            PrevMult.CurrentValue.Value,
            ThisKey,
            ThisMult.CurrentValue.Value,
            slope)

    End Function

    Private Sub AddSlopeMultiplier(ByVal item As TransitionSlopeMultiplier)

        Dim l As SortedList(Of Integer, TransitionSlopeMultiplier) =
            Me.GetItemExact(
                item.TransitionGroupId,
                item.StratumId,
                item.SecondaryStratumId,
                item.Iteration,
                item.Timestep)

        If (l Is Nothing) Then

            l = New SortedList(Of Integer, TransitionSlopeMultiplier)

            Me.AddItem(
                item.TransitionGroupId,
                item.StratumId,
                item.SecondaryStratumId,
                item.Iteration,
                item.Timestep,
                l)

        End If

        l.Add(CInt(item.Slope), item)
        Debug.Assert(Me.HasItems())

    End Sub

End Class
