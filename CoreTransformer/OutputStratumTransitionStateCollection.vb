'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.Common
Imports System.Collections.ObjectModel

Friend Class OutputStratumTransitionStateCollection
    Inherits KeyedCollection(Of SevenIntegerLookupKey, OutputStratumTransitionState)

    Public Sub New()
        MyBase.New(New SevenIntegerLookupKeyEqualityComparer)
    End Sub

    Protected Overrides Function GetKeyForItem(ByVal item As OutputStratumTransitionState) As SevenIntegerLookupKey
        Return New SevenIntegerLookupKey(item.StratumId, GetNullableKey(item.SecondaryStratumId), item.Iteration, item.Timestep, item.TransitionTypeId, item.StateClassId, item.EndStateClassId)
    End Function

End Class
