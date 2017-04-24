'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
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

