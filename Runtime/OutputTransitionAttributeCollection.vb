'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Common
Imports System.Collections.ObjectModel

Friend Class OutputTransitionAttributeCollection
    Inherits KeyedCollection(Of SevenIntegerLookupKey, OutputTransitionAttribute)

    Public Sub New()
        MyBase.New(New SevenIntegerLookupKeyEqualityComparer)
    End Sub

    Protected Overrides Function GetKeyForItem(ByVal item As OutputTransitionAttribute) As SevenIntegerLookupKey

        Return New SevenIntegerLookupKey(
            item.StratumId,
            LookupKeyUtils.GetOutputCollectionKey(item.SecondaryStratumId),
            LookupKeyUtils.GetOutputCollectionKey(item.TertiaryStratumId),
            item.Iteration,
            item.Timestep,
            item.TransitionAttributeTypeId,
            item.AgeKey)

    End Function

End Class
