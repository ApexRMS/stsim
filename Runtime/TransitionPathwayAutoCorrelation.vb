'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Enum AutoCorrelationSpread
    ToAnyCell = 0
    ToSamePathway = 1
    ToSamePrimaryStratum = 2
    ToSameSecondaryStratum = 3
    ToSameTertiaryStratum = 4
End Enum

Class TransitionPathwayAutoCorrelation

    Private m_Iteration As Nullable(Of Integer)
    Private m_Timestep As Nullable(Of Integer)
    Private m_StratumId As Nullable(Of Integer)
    Private m_SecondaryStratumId As Nullable(Of Integer)
    Private m_TertiaryStratumId As Nullable(Of Integer)
    Private m_TransitionGroupId As Nullable(Of Integer)
    Private m_AutoCorrelation As Boolean
    Private m_SpreadTo As AutoCorrelationSpread = AutoCorrelationSpread.ToAnyCell

    Public Sub New(
        ByVal iteration As Nullable(Of Integer),
        ByVal timestep As Nullable(Of Integer),
        ByVal stratumId As Nullable(Of Integer),
        ByVal secondaryStratumId As Nullable(Of Integer),
        ByVal tertiaryStratumId As Nullable(Of Integer),
        ByVal transitionGroupId As Nullable(Of Integer),
        ByVal autoCorrelation As Boolean,
        ByVal spreadTo As AutoCorrelationSpread)

        Me.m_Iteration = iteration
        Me.m_Timestep = timestep
        Me.m_StratumId = stratumId
        Me.m_SecondaryStratumId = secondaryStratumId
        Me.m_TertiaryStratumId = tertiaryStratumId
        Me.m_TransitionGroupId = transitionGroupId
        Me.m_AutoCorrelation = autoCorrelation
        Me.m_SpreadTo = spreadTo

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

    Public ReadOnly Property TertiaryStratumId As Nullable(Of Integer)
        Get
            Return Me.m_TertiaryStratumId
        End Get
    End Property

    Public ReadOnly Property TransitionGroupId As Nullable(Of Integer)
        Get
            Return Me.m_TransitionGroupId
        End Get
    End Property

    Public ReadOnly Property AutoCorrelation As Boolean
        Get
            Return Me.m_AutoCorrelation
        End Get
    End Property

    Public ReadOnly Property SpreadTo As AutoCorrelationSpread
        Get
            Return Me.m_SpreadTo
        End Get
    End Property

End Class
