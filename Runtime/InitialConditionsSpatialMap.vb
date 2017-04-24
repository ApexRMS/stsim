'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Imports SyncroSim.Common

Class InitialConditionsSpatialMap

    Private m_HasItems As Boolean
    Private m_Map As New SortedKeyMap1(Of InitialConditionsSpatial)(SearchMode.ExactPrev)

    Public Sub New(ByVal ics As InitialConditionsSpatialCollection)

        For Each t As InitialConditionsSpatial In ics
            Me.AddICS(t)
        Next

    End Sub

    Private Sub AddICS(ByVal ics As InitialConditionsSpatial)

        Me.m_Map.AddItem(ics.Iteration, ics)

        Me.m_HasItems = True

    End Sub

    Public Function GetICS(ByVal iteration As Nullable(Of Integer)) As InitialConditionsSpatial

        If (Not Me.m_HasItems) Then
            Return Nothing
        End If

        Dim l As InitialConditionsSpatial = Me.m_Map.GetItem(iteration)

        Return l

    End Function

End Class

