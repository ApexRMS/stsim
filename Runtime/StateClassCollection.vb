'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Imports System.Collections.ObjectModel

''' <summary>
''' The state class collection class
''' </summary>
''' <remarks></remarks>
Friend Class StateClassCollection
    Inherits KeyedCollection(Of Integer, StateClass)

    Protected Overrides Function GetKeyForItem(ByVal item As StateClass) As Integer
        Return item.Id
    End Function

End Class