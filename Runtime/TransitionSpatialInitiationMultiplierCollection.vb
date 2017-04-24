'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports System.Collections.ObjectModel

Friend Class TransitionSpatialInitiationMultiplierCollection
    Inherits KeyedCollection(Of Integer, TransitionSpatialInitiationMultiplier)


    Protected Overrides Function GetKeyForItem(ByVal item As TransitionSpatialInitiationMultiplier) As Integer
        Return item.TransitionSpatialInitiationMultiplierId
    End Function

End Class
