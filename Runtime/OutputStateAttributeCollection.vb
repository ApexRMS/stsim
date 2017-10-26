'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Common
Imports System.Collections.ObjectModel

Friend Class OutputStateAttributeCollection
    Inherits KeyedCollection(Of SevenIntegerLookupKey, OutputStateAttribute)

    Public Sub New()
        MyBase.New(New SevenIntegerLookupKeyEqualityComparer)
    End Sub

    Protected Overrides Function GetKeyForItem(ByVal item As OutputStateAttribute) As SevenIntegerLookupKey
        Return New SevenIntegerLookupKey(item.StratumId, GetNullableKey(item.SecondaryStratumId), GetNullableKey(item.TertiaryStratumId), item.Iteration, item.Timestep, item.StateAttributeTypeId, item.AgeKey)
    End Function

End Class
