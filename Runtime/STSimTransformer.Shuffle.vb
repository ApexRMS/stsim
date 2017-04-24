'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Common

Partial Class STSimTransformer

    ''' <summary>
    ''' Shuffles the order of the specified stratum's cells
    ''' </summary>
    ''' <param name="stratum"></param>
    ''' <remarks></remarks>
    Private Sub ShuffleStratumCells(ByVal stratum As Stratum)

        If (stratum.Cells.Count = 0) Then
            Return
        End If

        Dim lst As New List(Of Cell)

        For Each c As Cell In stratum.Cells.Values
            lst.Add(c)
        Next

        ShuffleUtilities.ShuffleList(lst, Me.m_RandomGenerator.Random())

        stratum.Cells.Clear()

        For Each c As Cell In lst
            stratum.Cells.Add(c.CellId, c)
        Next

    End Sub

    ''' <summary>
    ''' Reorders the list of shufflable transition groups
    ''' </summary>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <param name="groups"></param>
    ''' <remarks></remarks>
    Private Sub ReorderShufflableTransitionGroups(ByVal iteration As Integer, ByVal timestep As Integer)

        Dim orders As TransitionOrderCollection = Me.m_TransitionOrderMap.GetTransitionOrders(iteration, timestep)

        If (orders Is Nothing) Then
            ShuffleUtilities.ShuffleList(Me.m_ShufflableTransitionGroups, Me.m_RandomGenerator.Random())
        Else
            Me.ReorderShufflableTransitionGroups(orders)
        End If

#If DEBUG Then
        Me.VALIDATE_SHUFFLABLE_GROUPS()
#End If

    End Sub

    ''' <summary>
    ''' Reorders the list of shufflable transition groups
    ''' </summary>
    ''' <param name="orders"></param>
    ''' <remarks></remarks>
    Private Sub ReorderShufflableTransitionGroups(ByVal orders As TransitionOrderCollection)

        'If there are less than two transition groups there is no reason to continue

        If (Me.m_ShufflableTransitionGroups.Count <= 1) Then
            Return
        End If

        'Reset all transition group order values

        For Each tg As TransitionGroup In Me.m_ShufflableTransitionGroups
            tg.Order = DEFAULT_TRANSITION_ORDER
        Next

        'Apply the new ordering from the order collection

        Debug.Assert(Me.m_TransitionGroups.Count = Me.m_ShufflableTransitionGroups.Count)

        For Each order As TransitionOrder In orders

            If (Me.m_TransitionGroups.Contains(order.TransitionGroupId)) Then

                Debug.Assert(Me.m_ShufflableTransitionGroups.Contains(Me.m_TransitionGroups(order.TransitionGroupId)))
                Me.m_TransitionGroups(order.TransitionGroupId).Order = order.Order

            End If

        Next

        'Sort by the transition groups by the order value

        Me.m_ShufflableTransitionGroups.Sort(
            Function(t1 As TransitionGroup, t2 As TransitionGroup) As Integer
                Return (t1.Order.CompareTo(t2.Order))
            End Function)

        'Find the number of times each order appears.  If it appears more than
        'once then shuffle the subset of transtion groups with this order.

        Dim OrderCounts As New Dictionary(Of Double, Integer)

        For Each o As TransitionOrder In orders

            If (Not OrderCounts.ContainsKey(o.Order)) Then
                OrderCounts.Add(o.Order, 1)
            Else
                OrderCounts(o.Order) += 1
            End If

        Next

        'If any order appears more than once then it is a subset
        'that we need to shuffle.  Note that there may be a subset
        'for the default order.

        For Each d As Double In OrderCounts.Keys

            If (OrderCounts(d) > 1) Then

                ShuffleUtilities.ShuffleSubList(
                    Me.m_ShufflableTransitionGroups,
                    Me.GetMinOrderIndex(d),
                    Me.GetMaxOrderIndex(d),
                    Me.m_RandomGenerator.Random())

            End If

        Next

        If (Me.DefaultOrderHasSubset()) Then

            ShuffleUtilities.ShuffleSubList(
                Me.m_ShufflableTransitionGroups,
                Me.GetMinOrderIndex(DEFAULT_TRANSITION_ORDER),
                Me.GetMaxOrderIndex(DEFAULT_TRANSITION_ORDER),
                Me.m_RandomGenerator.Random())

        End If

    End Sub

    Private Function DefaultOrderHasSubset() As Boolean

        Dim c As Integer = 0

        For Each tg As TransitionGroup In Me.m_ShufflableTransitionGroups

            If (tg.Order = DEFAULT_TRANSITION_ORDER) Then

                c += 1

                If (c = 2) Then
                    Return True
                End If

            End If

        Next

        Return False

    End Function

    Private Function GetMinOrderIndex(ByVal order As Double) As Integer

        For Index As Integer = 0 To Me.m_ShufflableTransitionGroups.Count - 1

            Dim tg As TransitionGroup = Me.m_ShufflableTransitionGroups(Index)

            If (tg.Order = order) Then
                Return Index
            End If

        Next

        Throw New InvalidOperationException("Cannot find minimum transition order!")
        Return -1

    End Function

    Private Function GetMaxOrderIndex(ByVal order As Double) As Integer

        For Index As Integer = Me.m_ShufflableTransitionGroups.Count - 1 To 0 Step -1

            Dim tg As TransitionGroup = Me.m_ShufflableTransitionGroups(Index)

            If (tg.Order = order) Then
                Return Index
            End If

        Next

        Throw New InvalidOperationException("Cannot find maximum transition order!")
        Return -1

    End Function

End Class
