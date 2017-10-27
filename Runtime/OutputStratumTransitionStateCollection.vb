'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Common
Imports System.Collections.ObjectModel

Friend Class OutputStratumTransitionStateCollection
    Inherits KeyedCollection(Of EightIntegerLookupKey, OutputStratumTransitionState)

    Public Sub New()
        MyBase.New(New EightIntegerLookupKeyEqualityComparer)
    End Sub

    Protected Overrides Function GetKeyForItem(ByVal item As OutputStratumTransitionState) As EightIntegerLookupKey

        Return New EightIntegerLookupKey(
            item.StratumId,
            LookupKeyUtils.GetOutputCollectionKey(item.SecondaryStratumId),
            LookupKeyUtils.GetOutputCollectionKey(item.TertiaryStratumId),
            item.Iteration,
            item.Timestep,
            item.TransitionTypeId,
            item.StateClassId,
            item.EndStateClassId)

    End Function

End Class
