'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Imports System.Collections.ObjectModel

''' <summary>
''' Collection of TimeSinceTransition values keyed by Transition Group ID
''' </summary>
Friend Class TstCollection
    Inherits KeyedCollection(Of Integer, Tst)

    Protected Overrides Function GetKeyForItem(ByVal item As Tst) As Integer
        Return item.TransitionGroupId
    End Function

End Class

