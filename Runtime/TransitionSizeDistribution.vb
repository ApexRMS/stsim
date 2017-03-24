'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

''' <summary>
''' Transition Size Distribution class
''' </summary>
''' <remarks></remarks>
Friend Class TransitionSizeDistribution

    Private m_TransitionSizeDistributionId As Integer
    Private m_StratumId As Nullable(Of Integer)
    Private m_Iteration As Nullable(Of Integer)
    Private m_Timestep As Nullable(Of Integer)
    Private m_TransitionGroupId As Integer
    Private m_MinimumSize As Double
    Private m_MaximumSize As Double
    Private m_RelativeAmount As Double
    Private m_Proportion As Double

    Public Sub New(
        ByVal transitionSizeDistributionId As Integer,
        ByVal stratumId As Nullable(Of Integer),
        ByVal iteration As Nullable(Of Integer),
        ByVal timestep As Nullable(Of Integer),
        ByVal transitionGroupId As Integer,
        ByVal maximumSize As Double,
        ByVal relativeAmount As Double)

        Me.m_TransitionSizeDistributionId = transitionSizeDistributionId
        Me.m_StratumId = stratumId
        Me.m_Iteration = iteration
        Me.m_Timestep = timestep
        Me.m_TransitionGroupId = transitionGroupId
        Me.m_MaximumSize = maximumSize
        Me.m_RelativeAmount = relativeAmount

    End Sub

    ''' <summary>
    ''' Gets the transition size distribution Id
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TransitionSizeDistributionId As Integer
        Get
            Return Me.m_TransitionSizeDistributionId
        End Get
    End Property
    ''' <summary>
    ''' Gets the stratum Id
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property StratumId As Nullable(Of Integer)
        Get
            Return Me.m_StratumId
        End Get
    End Property

    ''' <summary>
    ''' Gets the iteration
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Iteration As Nullable(Of Integer)
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
    Public ReadOnly Property Timestep As Nullable(Of Integer)
        Get
            Return Me.m_Timestep
        End Get
    End Property

    ''' <summary>
    ''' Gets the transition group Id
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TransitionGroupId As Integer
        Get
            Return Me.m_TransitionGroupId
        End Get
    End Property

    ''' <summary>
    ''' Gets or sets the minimum size
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MinimumSize As Double
        Get
            Return Me.m_MinimumSize
        End Get
        Set(value As Double)
            Me.m_MinimumSize = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the maximum size
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MaximumSize As Double
        Get
            Return Me.m_MaximumSize
        End Get
    End Property

    ''' <summary>
    ''' Gets the relative amount
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property RelativeAmount As Double
        Get
            Return Me.m_RelativeAmount
        End Get
    End Property

    ''' <summary>
    ''' Gets or sets the proportion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Proportion As Double
        Get
            Return Me.m_Proportion
        End Get
        Set(value As Double)
            Me.m_Proportion = value
        End Set
    End Property

End Class
