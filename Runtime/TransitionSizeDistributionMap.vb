'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.Core

Class TransitionSizeDistributionMap
    Inherits STSimMapBase2(Of List(Of TransitionSizeDistribution))

    Private m_Lists As New List(Of List(Of TransitionSizeDistribution))

    Public Sub New(
        ByVal scenario As Scenario,
        ByVal collection As TransitionSizeDistributionCollection)

        MyBase.New(scenario)

        For Each Item As TransitionSizeDistribution In collection
            Me.AddSizeDistribution(Item)
        Next

    End Sub

    Public Sub Normalize()

        For Each l As List(Of TransitionSizeDistribution) In Me.m_Lists
            NormalizeList(l)
        Next

    End Sub

    Public Function GetSizeDistributions(
        ByVal transitionGroupId As Integer,
        ByVal stratumId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer) As List(Of TransitionSizeDistribution)

        Return Me.GetItem(
            transitionGroupId,
            stratumId,
            iteration,
            timestep)

    End Function

    Private Sub AddSizeDistribution(ByVal item As TransitionSizeDistribution)

        Dim l As List(Of TransitionSizeDistribution) =
            Me.GetItemExact(
                item.TransitionGroupId,
                item.StratumId,
                item.Iteration,
                item.Timestep)

        If (l Is Nothing) Then

            l = New List(Of TransitionSizeDistribution)

            Me.AddItem(
                item.TransitionGroupId,
                item.StratumId,
                item.Iteration,
                item.Timestep,
                l)

        End If

        l.Add(item)

        If (Not Me.m_Lists.Contains(l)) Then
            Me.m_Lists.Add(l)
        End If

        Debug.Assert(Me.HasItems())

    End Sub

    Private Shared Sub NormalizeList(ByVal tsdList As List(Of TransitionSizeDistribution))

        tsdList.Sort(Function(tsd1 As TransitionSizeDistribution, tsd2 As TransitionSizeDistribution) As Integer
                         Return (tsd1.MaximumSize.CompareTo(tsd2.MaximumSize))
                     End Function)

        Dim TotalRelativeAmount As Double = 0.0

        For Index As Integer = 0 To tsdList.Count - 1

            Dim tsd As TransitionSizeDistribution = tsdList(Index)

            If (Index > 0) Then
                tsd.MinimumSize = tsdList(Index - 1).MaximumSize
            End If

            TotalRelativeAmount += tsd.RelativeAmount

        Next

        For Each tsd As TransitionSizeDistribution In tsdList
            tsd.Proportion = tsd.RelativeAmount / TotalRelativeAmount
        Next

    End Sub

End Class

