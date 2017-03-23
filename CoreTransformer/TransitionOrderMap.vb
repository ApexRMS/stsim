'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.Common

Class TransitionOrderMap

    Private m_HasItems As Boolean
    Private m_Map As New SortedKeyMap2(Of TransitionOrderCollection)(SearchMode.ExactPrev)

    Public Sub New(ByVal orders As TransitionOrderCollection)

        For Each t As TransitionOrder In orders
            Me.AddTransitionOrder(t)
        Next

    End Sub

    Private Sub AddTransitionOrder(ByVal order As TransitionOrder)

        Dim l As TransitionOrderCollection = Me.m_Map.GetItemExact(order.Iteration, order.Timestep)

        If (l Is Nothing) Then

            l = New TransitionOrderCollection()
            Me.m_Map.AddItem(order.Iteration, order.Timestep, l)

        End If

        l.Add(order)

        Me.m_HasItems = True

    End Sub

    Public Function GetTransitionOrders(ByVal iteration As Integer, ByVal timestep As Integer) As TransitionOrderCollection

        If (Not Me.m_HasItems) Then
            Return Nothing
        End If

        Dim l As TransitionOrderCollection = Me.m_Map.GetItem(iteration, timestep)

        If (l Is Nothing) Then
            Return Nothing
        End If

        Debug.Assert(l.Count > 0)
        Return l

    End Function

End Class
