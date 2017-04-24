'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Imports System.Collections.ObjectModel

''' <summary>
''' Collection of simulation cells keyed by CellID
''' </summary>
Public Class CellCollection
    Inherits KeyedCollection(Of Integer, Cell)

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks>
    ''' Prevent public constructor
    ''' </remarks>
    Friend Sub New()
        Return
    End Sub

    ''' <summary>
    ''' Gets the key for the specified collection item
    ''' </summary>
    ''' <param name="item"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overrides Function GetKeyForItem(ByVal item As Cell) As Integer
        Return item.CellId
    End Function

End Class


