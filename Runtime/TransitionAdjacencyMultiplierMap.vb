'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Imports SyncroSim.Core
Imports SyncroSim.StochasticTime

Class TransitionAdjacencyMultiplierMapEntry
    Public Map As New Dictionary(Of Double, TransitionAdjacencyMultiplier)
    Public Items As New SortedList(Of Double, TransitionAdjacencyMultiplier)
End Class

Class TransitionAdjacencyMultiplierMap
    Inherits STSimMapBase3(Of TransitionAdjacencyMultiplierMapEntry)

    Private m_DistributionProvider As STSimDistributionProvider

    Public Sub New(
        ByVal scenario As Scenario,
        ByVal multipliers As TransitionAdjacencyMultiplierCollection,
        ByVal distributionProvider As STSimDistributionProvider)

        MyBase.New(scenario)

        Me.m_DistributionProvider = DistributionProvider

        For Each Item As TransitionAdjacencyMultiplier In multipliers
            Me.AddMultiplier(Item)
        Next

    End Sub

    Public Function GetAdjacencyMultiplier(
        ByVal transitionGroupId As Integer,
        ByVal stratumId As Integer,
        ByVal secondaryStratumId As Nullable(Of Integer),
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal attributeValue As Double) As Double

        Dim e As TransitionAdjacencyMultiplierMapEntry =
            Me.GetItem(
                transitionGroupId,
                stratumId,
                secondaryStratumId,
                iteration,
                timestep)

        If (e Is Nothing) Then
            Return 1.0
        End If

        Debug.Assert(e.Items.Count = e.Map.Count)

        If (e.Map.Count = 1) Then

            Dim tam As TransitionAdjacencyMultiplier = e.Items.First.Value
            tam.Sample(iteration, timestep, Me.m_DistributionProvider, DistributionFrequency.Always)

            Return tam.CurrentValue.Value

        End If

        If (e.Map.ContainsKey(attributeValue)) Then

            Dim tam As TransitionAdjacencyMultiplier = e.Map(attributeValue)
            tam.Sample(iteration, timestep, Me.m_DistributionProvider, DistributionFrequency.Always)

            Return tam.CurrentValue.Value

        End If

        Dim PrevVal As Double = -1.0
        Dim NextVal As Double = -1.0

        Debug.Assert(attributeValue >= 0.0)

        For Each k As Double In e.Items.Keys

            Debug.Assert(k <> attributeValue)

            If (k > attributeValue) Then
                NextVal = k
                Exit For
            End If

            PrevVal = k

        Next

        If (NextVal = -1.0) Then
            NextVal = PrevVal
        End If

        Dim PrevMult As TransitionAdjacencyMultiplier = e.Map(PrevVal)
        Dim NextMult As TransitionAdjacencyMultiplier = e.Map(NextVal)

        PrevMult.Sample(iteration, timestep, Me.m_DistributionProvider, DistributionFrequency.Always)
        NextMult.Sample(iteration, timestep, Me.m_DistributionProvider, DistributionFrequency.Always)

        If (PrevVal <> -1.0) Then

            If NextVal <> -1.0 Then

                If (PrevVal = NextVal) Then
                    Return PrevMult.CurrentValue.Value
                Else

                    Return Interpolate(
                        PrevVal,
                        PrevMult.CurrentValue.Value,
                        NextVal,
                        NextMult.CurrentValue.Value,
                        attributeValue)

                End If

            Else
                Return PrevMult.CurrentValue.Value
            End If

        Else
            Return 1.0
        End If

    End Function

    Private Sub AddMultiplier(ByVal item As TransitionAdjacencyMultiplier)

        Dim e As TransitionAdjacencyMultiplierMapEntry =
            Me.GetItemExact(
                item.TransitionGroupId,
                item.StratumId,
                item.SecondaryStratumId,
                item.Iteration,
                item.Timestep)

        If (e Is Nothing) Then

            e = New TransitionAdjacencyMultiplierMapEntry()

            Me.AddItem(
                item.TransitionGroupId,
                item.StratumId,
                item.SecondaryStratumId,
                item.Iteration,
                item.Timestep,
                e)

        End If

        If (Not e.Map.ContainsKey(item.AttributeValue)) Then

            Debug.Assert(Not e.Items.ContainsKey(item.AttributeValue))

            e.Map.Add(item.AttributeValue, item)
            e.Items.Add(item.AttributeValue, item)

        End If

        Debug.Assert(Me.HasItems())

    End Sub

End Class
