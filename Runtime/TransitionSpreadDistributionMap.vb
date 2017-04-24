'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Common

''' <summary>
''' Transition Spread Distribution Map
''' </summary>
''' <remarks></remarks>
Class TransitionSpreadDistributionMap

    Private m_Map As New MultiLevelKeyMap2(Of SortedKeyMap2(Of List(Of TransitionSpreadDistribution)))
    Private m_Lists As New List(Of List(Of TransitionSpreadDistribution))

    ''' <summary>
    ''' Adds a transition spread distribution to the map
    ''' </summary>
    ''' <param name="tsd">The transition spread distribution to add</param>
    ''' <remarks></remarks>
    Public Sub AddItem(ByVal tsd As TransitionSpreadDistribution)

        Dim m As SortedKeyMap2(Of List(Of TransitionSpreadDistribution)) = Me.m_Map.GetItemExact(tsd.StratumId, tsd.StateClassId)

        If (m Is Nothing) Then

            m = New SortedKeyMap2(Of List(Of TransitionSpreadDistribution))(SearchMode.ExactPrev)
            Me.m_Map.AddItem(tsd.StratumId, tsd.StateClassId, m)

        End If

        Dim l As List(Of TransitionSpreadDistribution) = m.GetItemExact(tsd.Iteration, tsd.Timestep)

        If (l Is Nothing) Then

            l = New List(Of TransitionSpreadDistribution)
            m.AddItem(tsd.Iteration, tsd.Timestep, l)

        End If

        l.Add(tsd)

        If (Not Me.m_Lists.Contains(l)) Then
            Me.m_Lists.Add(l)
        End If

    End Sub

    ''' <summary>
    ''' Normallizes the specified list of transition spread distributions
    ''' </summary>
    ''' <param name="tsdList">The list to normalize</param>
    ''' <remarks></remarks>
    Private Shared Sub NormalizeList(ByVal tsdList As List(Of TransitionSpreadDistribution))

        tsdList.Sort(Function(tsd1 As TransitionSpreadDistribution, tsd2 As TransitionSpreadDistribution) As Integer
                         Return (tsd1.MaximumDistance.CompareTo(tsd2.MaximumDistance))
                     End Function)

        Dim TotalRelativeAmount As Double = 0.0

        For Index As Integer = 0 To tsdList.Count - 1

            Dim tsd As TransitionSpreadDistribution = tsdList(Index)

            If (Index > 0) Then
                tsd.MinimumDistance = tsdList(Index - 1).MaximumDistance
            End If

            TotalRelativeAmount += tsd.RelativeAmount

        Next

        For Each tsd As TransitionSpreadDistribution In tsdList
            tsd.Proportion = tsd.RelativeAmount / TotalRelativeAmount
        Next

    End Sub

    ''' <summary>
    ''' Normalizes the distribution lists after they have been added to the map
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Normalize()

        For Each l As List(Of TransitionSpreadDistribution) In Me.m_Lists
            NormalizeList(l)
        Next

    End Sub

    ''' <summary>
    ''' Determines if there are any distribution records for the specified stratum and state class
    ''' </summary>
    ''' <param name="stratumId"></param>
    ''' <param name="stateClassId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function HasDistributionRecords(ByVal stratumId As Integer, ByVal stateClassId As Integer) As Boolean

        Dim m As SortedKeyMap2(Of List(Of TransitionSpreadDistribution)) = Me.m_Map.GetItem(stratumId, stateClassId)

        If (m Is Nothing) Then
            Return False
        Else
            Return True
        End If

    End Function

    ''' <summary>
    ''' Gets the transition spread distribution list for the specified stratum and state class
    ''' </summary>
    ''' <param name="stratumId">The stratum for the list</param>
    ''' <param name="stateClassId">The state class for the list</param>
    ''' <param name="iteration">The current iteration</param>
    ''' <param name="timestep">The current timestep</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDistributionList(
        ByVal stratumId As Integer,
        ByVal stateClassId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer) As List(Of TransitionSpreadDistribution)

        Dim m As SortedKeyMap2(Of List(Of TransitionSpreadDistribution)) = Me.m_Map.GetItem(stratumId, stateClassId)

        If (m Is Nothing) Then
            Return Nothing
        End If

        Return m.GetItem(iteration, timestep)

    End Function

End Class






