'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

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
