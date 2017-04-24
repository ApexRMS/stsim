'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
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
