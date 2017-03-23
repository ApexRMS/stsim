'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports System.Collections.ObjectModel

''' <summary>
''' The stratum collection class
''' </summary>
''' <remarks></remarks>
Friend Class StratumCollection
    Inherits KeyedCollection(Of Integer, Stratum)

    Protected Overrides Function GetKeyForItem(ByVal item As Stratum) As Integer
        Return item.StratumId
    End Function

End Class

