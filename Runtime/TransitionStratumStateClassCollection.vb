'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

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
