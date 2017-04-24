'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports System.Collections.ObjectModel

''' <summary>
''' The stratum collection class
''' </summary>
''' <remarks></remarks>
Friend Class StratumCollection
    Inherits KeyedCollection(Of Integer, Stratum)

    Protected Overrides Function GetKeyForItem(ByVal item As Stratum) As Integer
        Return item.StratumId
    End Function

End Class

