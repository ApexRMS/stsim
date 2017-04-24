'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Imports System.Collections.ObjectModel

''' <summary>
''' Collection of TransitionTypes keyed by TransitionTypeID
''' </summary>
Public Class TransitionTypeCollection
    Inherits KeyedCollection(Of Integer, TransitionType)

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks>
    ''' Prevent public constructor
    ''' </remarks>
    Friend Sub New()
        Return
    End Sub

    Protected Overrides Function GetKeyForItem(ByVal item As TransitionType) As Integer
        Return item.TransitionTypeId
    End Function

End Class
