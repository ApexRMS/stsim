'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

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
