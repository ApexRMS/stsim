'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Imports SyncroSim.Common

Class InitialConditionsDistributionMap

    Private m_HasItems As Boolean
    Private m_Map As New SortedKeyMap1(Of InitialConditionsDistributionCollection)(SearchMode.ExactPrev)

    Public Sub New(ByVal icd As InitialConditionsDistributionCollection)

        For Each t As InitialConditionsDistribution In icd
            Me.AddICD(t)
        Next

    End Sub

    Private Sub AddICD(ByVal order As InitialConditionsDistribution)

        Dim l As InitialConditionsDistributionCollection = Me.m_Map.GetItemExact(order.Iteration)

        If (l Is Nothing) Then

            l = New InitialConditionsDistributionCollection()
            Me.m_Map.AddItem(order.Iteration, l)

        End If

        l.Add(order)

        Me.m_HasItems = True

    End Sub

    Public Function GetICDs(ByVal iteration As Nullable(Of Integer)) As InitialConditionsDistributionCollection

        If (Not Me.m_HasItems) Then
            Return Nothing
        End If

        Dim l As InitialConditionsDistributionCollection = Me.m_Map.GetItem(iteration)

        If (l Is Nothing) Then
            Return Nothing
        End If

        Debug.Assert(l.Count > 0)
        Return l

    End Function

End Class
