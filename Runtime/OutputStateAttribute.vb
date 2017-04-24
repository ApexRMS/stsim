'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

''' <summary>
''' Output State Attribute class
''' </summary>
''' <remarks></remarks>
Friend Class OutputStateAttribute

    Private m_StratumId As Integer
    Private m_SecondaryStratumId As Nullable(Of Integer)
    Private m_Iteration As Integer
    Private m_Timestep As Integer
    Private m_StateAttributeTypeId As Integer
    Private m_AgeMin As Nullable(Of Integer)
    Private m_AgeMax As Nullable(Of Integer)
    Private m_AgeKey As Integer
    Private m_Amount As Double

    Public Sub New(
        ByVal stratumId As Integer,
        ByVal secondaryStratumId As Nullable(Of Integer),
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal stateAttributeTypeId As Integer,
        ByVal ageMin As Nullable(Of Integer),
        ByVal ageMax As Nullable(Of Integer),
        ByVal ageKey As Integer,
        ByVal amount As Double)

        Me.m_StratumId = stratumId
        Me.m_SecondaryStratumId = secondaryStratumId
        Me.m_Iteration = iteration
        Me.m_Timestep = timestep
        Me.m_StateAttributeTypeId = stateAttributeTypeId
        Me.m_AgeMin = ageMin
        Me.m_AgeMax = ageMax
        Me.m_AgeKey = ageKey
        Me.m_Amount = amount

    End Sub

    ''' <summary>
    ''' Gets the Stratum Id
    ''' </summary>
    Public ReadOnly Property StratumId As Integer
        Get
            Return Me.m_StratumId
        End Get
    End Property

    ''' <summary>
    ''' Gets the secondary stratum Id
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property SecondaryStratumId As Nullable(Of Integer)
        Get
            Return Me.m_SecondaryStratumId
        End Get
    End Property

    ''' <summary>
    ''' Gets the iteration
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Iteration As Integer
        Get
            Return Me.m_Iteration
        End Get
    End Property

    ''' <summary>
    ''' Gets the timestep
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Timestep As Integer
        Get
            Return Me.m_Timestep
        End Get
    End Property

    ''' <summary>
    ''' Gets the State Attribute Type Id 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property StateAttributeTypeId As Integer
        Get
            Return Me.m_StateAttributeTypeId
        End Get
    End Property

    ''' <summary>
    ''' Gets the minimum age
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property AgeMin As Nullable(Of Integer)
        Get
            Return Me.m_AgeMin
        End Get
    End Property

    ''' <summary>
    ''' Gets the maximum age
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property AgeMax As Nullable(Of Integer)
        Get
            Return Me.m_AgeMax
        End Get
    End Property

    ''' <summary>
    ''' Gets the age key
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property AgeKey As Integer
        Get
            Return Me.m_AgeKey
        End Get
    End Property

    ''' <summary>
    ''' The output amount
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Amount As Double
        Get
            Return Me.m_Amount
        End Get
        Set(ByVal value As Double)
            Me.m_Amount = value
        End Set
    End Property

End Class
