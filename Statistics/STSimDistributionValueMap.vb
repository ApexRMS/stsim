'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.Common
Imports SyncroSim.StochasticTime

Class STSimDistributionValueMap

    Private m_Map As New MultiLevelKeyMap3(Of SortedKeyMap2(Of DistributionValueCollection))

    Public Sub AddValue(ByVal value As STSimDistributionValue)

        Dim m As SortedKeyMap2(Of DistributionValueCollection) =
            Me.m_Map.GetItemExact(value.StratumId, value.SecondaryStratumId, value.DistributionTypeId)

        If (m Is Nothing) Then

            m = New SortedKeyMap2(Of DistributionValueCollection)(SearchMode.ExactPrevNext)
            Me.m_Map.AddItem(value.StratumId, value.SecondaryStratumId, value.DistributionTypeId, m)

        End If

        Dim c As DistributionValueCollection = m.GetItemExact(value.Iteration, value.Timestep)

        If (c Is Nothing) Then

            c = New DistributionValueCollection()
            m.AddItem(value.Iteration, value.Timestep, c)

        End If

        c.Add(value)

    End Sub

    Public Function GetValues(
        ByVal distributionTypeId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal stratumId As Nullable(Of Integer),
        ByVal secondaryStratumId As Nullable(Of Integer)) As DistributionValueCollection

        Dim m As SortedKeyMap2(Of DistributionValueCollection) =
            Me.m_Map.GetItem(stratumId, secondaryStratumId, distributionTypeId)

        If (m Is Nothing) Then
            Return Nothing
        End If

        Dim c As DistributionValueCollection = m.GetItem(iteration, timestep)

        If (c Is Nothing) Then
            Return Nothing
        End If

        Return c

    End Function

End Class

