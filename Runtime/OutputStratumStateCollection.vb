'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.Common
Imports System.Collections.ObjectModel

''' <summary>
''' OutputStratumStateCollection
''' </summary>
''' <remarks></remarks>
Friend Class OutputStratumStateCollection
    Inherits KeyedCollection(Of SixIntegerLookupKey, OutputStratumState)

    Public Sub New()
        MyBase.New(New SixIntegerLookupKeyEqualityComparer)
    End Sub

    Protected Overrides Function GetKeyForItem(ByVal item As OutputStratumState) As SixIntegerLookupKey
        Return New SixIntegerLookupKey(item.StratumId, GetNullableKey(item.SecondaryStratumId), item.Iteration, item.Timestep, item.StateClassId, item.AgeKey)
    End Function

End Class

''' <summary>
''' OutputStratumStateCollectionZeroValues
''' </summary>
''' <remarks></remarks>
Friend Class OutputStratumStateCollectionZeroValues
    Inherits KeyedCollection(Of FiveIntegerLookupKey, OutputStratumState)

    Public Sub New()
        MyBase.New(New FiveIntegerLookupKeyEqualityComparer)
    End Sub

    Protected Overrides Function GetKeyForItem(ByVal item As OutputStratumState) As FiveIntegerLookupKey
        Return New FiveIntegerLookupKey(item.StratumId, GetNullableKey(item.SecondaryStratumId), item.Iteration, item.Timestep, item.StateClassId)
    End Function

End Class



