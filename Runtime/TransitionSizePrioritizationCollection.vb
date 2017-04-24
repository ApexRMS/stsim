'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports System.Collections.ObjectModel

Friend Class TransitionSizePrioritizationCollection
    Inherits KeyedCollection(Of Integer, TransitionSizePrioritization)

    Protected Overrides Function GetKeyForItem(item As TransitionSizePrioritization) As Integer
        Return item.TransitionSizePrioritizationId
    End Function

End Class
