'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.Common
Imports System.Collections.ObjectModel

Friend Class OutputTransitionAttributeCollection
    Inherits KeyedCollection(Of SixIntegerLookupKey, OutputTransitionAttribute)

    Public Sub New()
        MyBase.New(New SixIntegerLookupKeyEqualityComparer)
    End Sub

    Protected Overrides Function GetKeyForItem(ByVal item As OutputTransitionAttribute) As SixIntegerLookupKey
        Return New SixIntegerLookupKey(item.StratumId, GetNullableKey(item.SecondaryStratumId), item.Iteration, item.Timestep, item.TransitionAttributeTypeId, item.AgeKey)
    End Function

End Class
