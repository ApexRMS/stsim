'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

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

