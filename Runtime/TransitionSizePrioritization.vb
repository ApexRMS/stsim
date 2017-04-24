'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Friend Class TransitionSizePrioritization

    Private m_TransitionSizePrioritizationId As Integer
    Private m_Iteration As Nullable(Of Integer)
    Private m_Timestep As Nullable(Of Integer)
    Private m_StratumId As Nullable(Of Integer)
    Private m_TransitionGroupId As Nullable(Of Integer)
    Private m_SizePrioritization As SizePrioritization
    Private m_MaximizeFidelityToDistribution As Boolean
    Private m_MaximizeFidelityToTotalArea As Boolean

    Public Sub New(
        ByVal transitionSizePrioritizationId As Integer,
        ByVal iteration As Nullable(Of Integer),
        ByVal timestep As Nullable(Of Integer),
        ByVal stratumId As Nullable(Of Integer),
        ByVal transitionGroupId As Nullable(Of Integer),
        ByVal sizePrioritization As SizePrioritization,
        ByVal maximizeFidelityToDistribution As Boolean,
        ByVal maximizeFidelityToTotalArea As Boolean)

        Me.m_TransitionSizePrioritizationId = transitionSizePrioritizationId
        Me.m_Iteration = iteration
        Me.m_Timestep = timestep
        Me.m_StratumId = stratumId
        Me.m_TransitionGroupId = transitionGroupId
        Me.m_SizePrioritization = sizePrioritization
        Me.m_MaximizeFidelityToDistribution = maximizeFidelityToDistribution
        Me.m_MaximizeFidelityToTotalArea = maximizeFidelityToTotalArea

    End Sub

    Public ReadOnly Property TransitionSizePrioritizationId As Integer
        Get
            Return Me.m_TransitionSizePrioritizationId
        End Get
    End Property

    Public ReadOnly Property Iteration As Nullable(Of Integer)
        Get
            Return Me.m_Iteration
        End Get
    End Property

    Public ReadOnly Property Timestep As Nullable(Of Integer)
        Get
            Return Me.m_Timestep
        End Get
    End Property

    Public ReadOnly Property StratumId As Nullable(Of Integer)
        Get
            Return Me.m_StratumId
        End Get
    End Property

    Public ReadOnly Property TransitionGroupId As Nullable(Of Integer)
        Get
            Return Me.m_TransitionGroupId
        End Get
    End Property

    Public ReadOnly Property SizePrioritization As SizePrioritization
        Get
            Return Me.m_SizePrioritization
        End Get
    End Property

    Public ReadOnly Property MaximizeFidelityToDistribution As Boolean
        Get
            Return Me.m_MaximizeFidelityToDistribution
        End Get
    End Property

    Public ReadOnly Property MaximizeFidelityToTotalArea As Boolean
        Get
            Return Me.m_MaximizeFidelityToTotalArea
        End Get
    End Property

End Class

