'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.Common
Imports System.Collections.ObjectModel

Friend Class OutputStratumTransitionCollection
    Inherits KeyedCollection(Of SixIntegerLookupKey, OutputStratumTransition)

    Public Sub New()
        MyBase.New(New SixIntegerLookupKeyEqualityComparer)
    End Sub

    Protected Overrides Function GetKeyForItem(ByVal item As OutputStratumTransition) As SixIntegerLookupKey
        Return New SixIntegerLookupKey(item.StratumId, GetNullableKey(item.SecondaryStratumId), item.Iteration, item.Timestep, item.TransitionGroupId, item.AgeKey)
    End Function

End Class

