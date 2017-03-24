'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

''' <summary>
''' Transition Spread Distribution
''' </summary>
''' <remarks></remarks>
Friend Class TransitionSpreadDistribution

    Private m_TransitionSpreadDistributionId As Integer
    Private m_StratumId As Nullable(Of Integer)
    Private m_Iteration As Nullable(Of Integer)
    Private m_Timestep As Nullable(Of Integer)
    Private m_TransitionGroupId As Integer
    Private m_StateClassId As Integer
    Private m_MinimumDistance As Double
    Private m_MaximumDistance As Double
    Private m_RelativeAmount As Double
    Private m_Proportion As Double

    Public Sub New(
        ByVal transitionSpreadDistributionId As Integer,
        ByVal stratumId As Nullable(Of Integer),
        ByVal iteration As Nullable(Of Integer),
        ByVal timestep As Nullable(Of Integer),
        ByVal transitionGroupId As Integer,
        ByVal stateClassId As Integer,
        ByVal maximumDistance As Double,
        ByVal relativeAmount As Double)

        Me.m_TransitionSpreadDistributionId = transitionSpreadDistributionId
        Me.m_StratumId = stratumId
        Me.m_Iteration = iteration
        Me.m_Timestep = timestep
        Me.m_TransitionGroupId = transitionGroupId
        Me.m_StateClassId = stateClassId
        Me.m_MaximumDistance = maximumDistance
        Me.m_RelativeAmount = relativeAmount

    End Sub

    ''' <summary>
    ''' Gets the Transition Spread Distribution Id
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TransitionSpreadDistributionId As Integer
        Get
            Return Me.m_TransitionSpreadDistributionId
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
    ''' Gets the state class Id
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property StateClassId As Integer
        Get
            Return Me.m_StateClassId
        End Get
    End Property

    ''' <summary>
    ''' Gets or sets the minimum distance
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property MinimumDistance As Double
        Get
            Return Me.m_MinimumDistance
        End Get
        Set(value As Double)
            Me.m_MinimumDistance = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the maximum distance
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MaximumDistance As Double
        Get
            Return Me.m_MaximumDistance
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
