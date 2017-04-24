'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

''' <summary>
''' Initial Conditions Spatial
''' </summary>
''' <remarks></remarks>
''' 
Friend Class InitialConditionsSpatial

    Private m_Iteration As Nullable(Of Integer) = Nothing
    Private m_PrimaryStratumFileName As String
    Private m_SecondaryStratumFileName As String
    Private m_StateClassFileName As String
    Private m_AgeFileName As String

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="iteration"></param>
    ''' <param name="primaryStratumName"></param>
    ''' <param name="secondaryStratumFileName"></param>
    ''' <param name="stateClassFileName"></param>
    ''' <param name="ageFileName"></param>
    ''' <remarks></remarks>
    Public Sub New(
        ByVal iteration As Nullable(Of Integer),
        ByVal primaryStratumName As String,
        ByVal secondaryStratumFileName As String,
        ByVal stateClassFileName As String,
        ByVal ageFileName As String
)
        Me.m_Iteration = iteration
        Me.m_PrimaryStratumFileName = primaryStratumName
        Me.m_SecondaryStratumFileName = secondaryStratumFileName
        Me.m_StateClassFileName = stateClassFileName
        Me.m_AgeFileName = ageFileName

    End Sub

    Public ReadOnly Property Iteration As Integer?
        Get
            Return m_Iteration
        End Get
    End Property

    Public ReadOnly Property PrimaryStratumFileName As String
        Get
            Return m_PrimaryStratumFileName
        End Get
    End Property

    Public ReadOnly Property SecondaryStratumFileName As String
        Get
            Return m_SecondaryStratumFileName
        End Get
    End Property

    Public ReadOnly Property StateClassFileName As String
        Get
            Return m_StateClassFileName
        End Get
    End Property

    Public ReadOnly Property AgeFileName As String
        Get
            Return m_AgeFileName
        End Get
    End Property
End Class
