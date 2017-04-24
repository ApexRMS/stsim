'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports System.Collections.ObjectModel

''' <summary>
''' Collection of Transition Attribute Types
''' </summary>
Friend Class TransitionAttributeTypeCollection
    Inherits KeyedCollection(Of Integer, TransitionAttributeType)

    Protected Overrides Function GetKeyForItem(ByVal item As TransitionAttributeType) As Integer
        Return item.TransitionAttributeId
    End Function

End Class
