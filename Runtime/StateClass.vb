'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

''' <summary>
''' State Class
''' </summary>
''' <remarks></remarks>
Class StateClass

    Private m_Id As Integer
    Private m_StateLabelXID As Integer
    Private m_StateLabelYID As Integer

    Public Sub New(
        ByVal id As Integer,
        ByVal stateLabelXID As Integer,
        ByVal stateLabelYID As Integer)

        Me.m_Id = id

        Me.m_StateLabelXID = stateLabelXID
        Me.m_StateLabelYID = stateLabelYID

    End Sub

    Public ReadOnly Property Id As Integer
        Get
            Return Me.m_Id
        End Get
    End Property

    Public ReadOnly Property StateLabelXID As Integer
        Get
            Return Me.m_StateLabelXID
        End Get
    End Property

    Public ReadOnly Property StateLabelYID As Integer
        Get
            Return Me.m_StateLabelYID
        End Get
    End Property

End Class
