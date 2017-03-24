'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports System.Collections.ObjectModel

''' <summary>
''' Patch Prioritization Collection
''' </summary>
''' <remarks></remarks>
Friend Class PatchPrioritizationCollection
    Inherits KeyedCollection(Of Integer, PatchPrioritization)

    Protected Overrides Function GetKeyForItem(item As PatchPrioritization) As Integer
        Return item.PatchPrioritizationId
    End Function

End Class
