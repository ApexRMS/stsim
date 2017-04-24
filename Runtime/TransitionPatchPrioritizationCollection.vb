'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
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
