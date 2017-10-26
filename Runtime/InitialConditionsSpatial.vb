'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Friend Class InitialConditionsSpatial

    Private m_Iteration As Nullable(Of Integer)
    Private m_PrimaryStratumFileName As String
    Private m_SecondaryStratumFileName As String
    Private m_TertiaryStratumFileName As String
    Private m_StateClassFileName As String
    Private m_AgeFileName As String

    Public Sub New(
        ByVal iteration As Nullable(Of Integer),
        ByVal primaryStratumName As String,
        ByVal secondaryStratumFileName As String,
        ByVal tertiaryStratumFileName As String,
        ByVal stateClassFileName As String,
        ByVal ageFileName As String)

        Me.m_Iteration = iteration
        Me.m_PrimaryStratumFileName = primaryStratumName
        Me.m_SecondaryStratumFileName = secondaryStratumFileName
        Me.m_TertiaryStratumFileName = tertiaryStratumFileName
        Me.m_StateClassFileName = stateClassFileName
        Me.m_AgeFileName = ageFileName

    End Sub

    Public ReadOnly Property Iteration As Integer?
        Get
            Return Me.m_Iteration
        End Get
    End Property

    Public ReadOnly Property PrimaryStratumFileName As String
        Get
            Return Me.m_PrimaryStratumFileName
        End Get
    End Property

    Public ReadOnly Property SecondaryStratumFileName As String
        Get
            Return Me.m_SecondaryStratumFileName
        End Get
    End Property

    Public ReadOnly Property TertiaryStratumFileName As String
        Get
            Return Me.m_TertiaryStratumFileName
        End Get
    End Property

    Public ReadOnly Property StateClassFileName As String
        Get
            Return Me.m_StateClassFileName
        End Get
    End Property

    Public ReadOnly Property AgeFileName As String
        Get
            Return Me.m_AgeFileName
        End Get
    End Property
End Class
