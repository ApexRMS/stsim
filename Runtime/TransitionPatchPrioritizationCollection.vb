'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports System.Collections.ObjectModel

''' <summary>
''' Transition Patch Prioritization Collection
''' </summary>
''' <remarks></remarks>
Friend Class TransitionPatchPrioritizationCollection
    Inherits KeyedCollection(Of Integer, TransitionPatchPrioritization)

    Protected Overrides Function GetKeyForItem(item As TransitionPatchPrioritization) As Integer
        Return item.TransitionPatchPrioritizationId
    End Function

End Class
