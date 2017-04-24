'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Common
Imports System.Collections.ObjectModel

''' <summary>
''' The stratum stateclass transition collection class
''' </summary>
''' <remarks></remarks>
Friend Class TransitionStratumStateClassCollection
    Inherits KeyedCollection(Of TwoIntegerLookupKey, TransitionStratumStateClass)

    Public Sub New()
        MyBase.New(New TwoIntegerLookupKeyEqualityComparer)
    End Sub

    Protected Overrides Function GetKeyForItem(ByVal item As TransitionStratumStateClass) As TwoIntegerLookupKey
        Return New TwoIntegerLookupKey(item.StratumId, item.StateClassId)
    End Function

End Class
