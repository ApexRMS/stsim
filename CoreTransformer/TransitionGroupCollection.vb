'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports System.Collections.ObjectModel

''' <summary>
''' Collection of TransitionGroups keyed by TransitionGroupID
''' </summary>
Public Class TransitionGroupCollection
    Inherits KeyedCollection(Of Integer, TransitionGroup)

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks>
    ''' Prevent public constructor
    ''' </remarks>
    Friend Sub New()
        Return
    End Sub

    Protected Overrides Function GetKeyForItem(ByVal item As TransitionGroup) As Integer
        Return item.TransitionGroupId
    End Function

End Class

