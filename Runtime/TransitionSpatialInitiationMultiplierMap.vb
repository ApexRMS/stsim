'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.Common

''' <summary>
''' Transition spatial Initiation multiplier map
''' </summary>
''' <remarks></remarks>
Class TransitionSpatialInitiationMultiplierMap

    Private m_Map As New MultiLevelKeyMap1(Of SortedKeyMap2(Of TransitionSpatialInitiationMultiplier))

    ''' <summary>
    ''' Consturctor
    ''' </summary>
    ''' <param name="transitionSpatialInitiationMultipliers">The Initiation multiplier collection</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal transitionSpatialInitiationMultipliers As TransitionSpatialInitiationMultiplierCollection)

        For Each m As TransitionSpatialInitiationMultiplier In transitionSpatialInitiationMultipliers
            Me.AddMultiplierRaster(m)
        Next

    End Sub

    ''' <summary>
    ''' Gets a multipler raster for the specified transition group, iteration, and timestep
    ''' </summary>
    ''' <param name="transitionGroupId"></param>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMultiplierRaster(
        ByVal transitionGroupId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer) As TransitionSpatialInitiationMultiplier

        Dim m As SortedKeyMap2(Of TransitionSpatialInitiationMultiplier) = Me.m_Map.GetItem(transitionGroupId)

        If (m Is Nothing) Then
            Return Nothing
        End If

        Return m.GetItem(iteration, timestep)

    End Function

    ''' <summary>
    ''' Adds a multiplier to the map
    ''' </summary>
    ''' <param name="multiplier"></param>
    ''' <remarks></remarks>
    Private Sub AddMultiplierRaster(ByVal multiplier As TransitionSpatialInitiationMultiplier)

        Dim m As SortedKeyMap2(Of TransitionSpatialInitiationMultiplier) = Me.m_Map.GetItemExact(multiplier.TransitionGroupId)

        If (m Is Nothing) Then

            m = New SortedKeyMap2(Of TransitionSpatialInitiationMultiplier)(SearchMode.ExactPrev)
            Me.m_Map.AddItem(multiplier.TransitionGroupId, m)

        End If

        m.AddItem(multiplier.Iteration, multiplier.Timestep, multiplier)

    End Sub

End Class

