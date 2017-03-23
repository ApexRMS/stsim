'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports System.Collections.ObjectModel

Friend Class TransitionSizePrioritizationCollection
    Inherits KeyedCollection(Of Integer, TransitionSizePrioritization)

    Protected Overrides Function GetKeyForItem(item As TransitionSizePrioritization) As Integer
        Return item.TransitionSizePrioritizationId
    End Function

End Class
