'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Public Class DeterministicTransition

    Private m_Iteration As Nullable(Of Integer)
    Private m_Timestep As Nullable(Of Integer)
    Private m_StratumIdSource As Nullable(Of Integer)
    Private m_StateClassIdSource As Integer
    Private m_StratumIdDestination As Nullable(Of Integer)
    Private m_StateClassIdDestination As Nullable(Of Integer)
    Private m_AgeMinimum As Integer
    Private m_AgeMaximum As Integer

    Public Sub New(
        ByVal iteration As Nullable(Of Integer),
        ByVal timestep As Nullable(Of Integer),
        ByVal stratumIdSource As Nullable(Of Integer),
        ByVal stateClassIdSource As Integer,
        ByVal stratumIdDestination As Nullable(Of Integer),
        ByVal stateClassIdDestination As Nullable(Of Integer),
        ByVal ageMinimum As Integer,
        ByVal ageMaximum As Integer)

        Me.m_Iteration = iteration
        Me.m_Timestep = timestep
        Me.m_StratumIdSource = stratumIdSource
        Me.m_StateClassIdSource = stateClassIdSource
        Me.m_StratumIdDestination = stratumIdDestination
        Me.m_StateClassIdDestination = stateClassIdDestination
        Me.m_AgeMinimum = ageMinimum
        Me.m_AgeMaximum = ageMaximum

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

    Public Property StratumIdSource As Nullable(Of Integer)
        Get
            Return Me.m_StratumIdSource
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me.m_StratumIdSource = value
        End Set
    End Property

    Public Property StateClassIdSource As Integer
        Get
            Return Me.m_StateClassIdSource
        End Get
        Set(ByVal value As Integer)
            Me.m_StateClassIdSource = value
        End Set
    End Property

    Public Property StratumIdDestination As Nullable(Of Integer)
        Get
            Return Me.m_StratumIdDestination
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me.m_StratumIdDestination = value
        End Set
    End Property

    Public Property StateClassIdDestination As Nullable(Of Integer)
        Get
            Return Me.m_StateClassIdDestination
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me.m_StateClassIdDestination = value
        End Set
    End Property

    Public Property AgeMinimum As Integer
        Get
            Return Me.m_AgeMinimum
        End Get
        Set(ByVal value As Integer)
            Me.m_AgeMinimum = value
        End Set
    End Property

    Public Property AgeMaximum As Integer
        Get
            Return Me.m_AgeMaximum
        End Get
        Set(ByVal value As Integer)
            Me.m_AgeMaximum = value
        End Set
    End Property

End Class

