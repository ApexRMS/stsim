'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports System.Collections.ObjectModel

Friend Class TransitionSpatialMultiplierCollection
    Inherits KeyedCollection(Of Integer, TransitionSpatialMultiplier)

    Protected Overrides Function GetKeyForItem(ByVal item As TransitionSpatialMultiplier) As Integer
        Return item.TransitionSpatialMultiplierId
    End Function

End Class

