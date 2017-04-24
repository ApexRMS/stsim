'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Friend Class TstTransitionGroup

    Private m_GroupId As Integer

    Public Sub New(ByVal groupId As Integer)
        Me.m_GroupId = groupId
    End Sub

    Public ReadOnly Property GroupId As Integer
        Get
            Return Me.m_GroupId
        End Get
    End Property

End Class

