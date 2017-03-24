'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports System.Collections.ObjectModel

''' <summary>
''' Transition Spread Distribution collection
''' </summary>
''' <remarks></remarks>
Friend Class TransitionSpreadDistributionCollection
    Inherits KeyedCollection(Of Integer, TransitionSpreadDistribution)

    Protected Overrides Function GetKeyForItem(item As TransitionSpreadDistribution) As Integer
        Return item.TransitionSpreadDistributionId
    End Function

End Class
