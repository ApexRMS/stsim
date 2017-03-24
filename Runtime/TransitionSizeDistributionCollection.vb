'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports System.Collections.ObjectModel

''' <summary>
''' The transition size distribution collection class
''' </summary>
''' <remarks></remarks>
Friend Class TransitionSizeDistributionCollection
    Inherits KeyedCollection(Of Integer, TransitionSizeDistribution)

    ''' <summary>
    ''' Gets the key for the specified item
    ''' </summary>
    ''' <param name="item"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Overrides Function GetKeyForItem(ByVal item As TransitionSizeDistribution) As Integer
        Return item.TransitionSizeDistributionId
    End Function

End Class
