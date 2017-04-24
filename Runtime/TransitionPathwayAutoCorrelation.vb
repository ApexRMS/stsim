'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Class TransitionPathwayAutoCorrelation

    Private m_Iteration As Nullable(Of Integer)
    Private m_Timestep As Nullable(Of Integer)
    Private m_StratumId As Nullable(Of Integer)
    Private m_SecondaryStratumId As Nullable(Of Integer)
    Private m_TransitionGroupId As Nullable(Of Integer)
    Private m_Factor As Double
    Private m_SpreadOnlyToLike As Boolean

    Public Sub New(
        ByVal iteration As Nullable(Of Integer),
        ByVal timestep As Nullable(Of Integer),
        ByVal stratumId As Nullable(Of Integer),
        ByVal secondaryStratumId As Nullable(Of Integer),
        ByVal transitionGroupId As Nullable(Of Integer),
        ByVal factor As Double,
        ByVal spreadOnlyToLike As Boolean)

        Me.m_Iteration = iteration
        Me.m_Timestep = timestep
        Me.m_StratumId = stratumId
        Me.m_SecondaryStratumId = secondaryStratumId
        Me.m_TransitionGroupId = transitionGroupId
        Me.m_Factor = factor
        Me.m_SpreadOnlyToLike = spreadOnlyToLike

    End Sub

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

    Public ReadOnly Property SecondaryStratumId As Nullable(Of Integer)
        Get
            Return Me.m_SecondaryStratumId
        End Get
    End Property

    Public ReadOnly Property TransitionGroupId As Nullable(Of Integer)
        Get
            Return Me.m_TransitionGroupId
        End Get
    End Property

    Public ReadOnly Property Factor As Double
        Get
            Return Me.m_Factor
        End Get
    End Property

    Public ReadOnly Property SpreadOnlyToLike As Boolean
        Get
            Return Me.m_SpreadOnlyToLike
        End Get
    End Property

End Class
