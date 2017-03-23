'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
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
