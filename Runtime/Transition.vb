'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Public Class Transition

    Private m_Iteration As Nullable(Of Integer)
    Private m_Timestep As Nullable(Of Integer)
    Private m_StratumIdSource As Nullable(Of Integer)
    Private m_StateClassIdSource As Integer
    Private m_StratumIdDestination As Nullable(Of Integer)
    Private m_StateClassIdDestination As Nullable(Of Integer)
    Private m_TransitionTypeId As Integer
    Private m_Probability As Double
    Private m_Proportion As Double
    Private m_AgeMinimum As Integer
    Private m_AgeMaximum As Integer
    Private m_AgeRelative As Integer
    Private m_AgeReset As Boolean
    Private m_TstMinimum As Integer
    Private m_TstMaximum As Integer
    Private m_TstRelative As Integer

    'Internal use only
    Friend PropnWasNull As Boolean
    Friend AgeMinWasNull As Boolean
    Friend AgeMaxWasNull As Boolean
    Friend AgeRelativeWasNull As Boolean
    Friend AgeResetWasNull As Boolean
    Friend TstMinimumWasNull As Boolean
    Friend TstMaximumWasNull As Boolean
    Friend TstRelativeWasNull As Boolean

    Public Sub New(
        ByVal iteration As Nullable(Of Integer),
        ByVal timestep As Nullable(Of Integer),
        ByVal stratumIdSource As Nullable(Of Integer),
        ByVal stateClassIdSource As Integer,
        ByVal stratumIdDestination As Nullable(Of Integer),
        ByVal stateClassIdDestination As Nullable(Of Integer),
        ByVal transitionTypeId As Integer,
        ByVal probability As Double,
        ByVal proportion As Double,
        ByVal ageMinimum As Integer,
        ByVal ageMaximum As Integer,
        ByVal ageRelative As Integer,
        ByVal ageReset As Boolean,
        ByVal tstMinimum As Integer,
        ByVal tstMaximum As Integer,
        ByVal tstRelative As Integer)

        Me.m_Iteration = iteration
        Me.m_Timestep = timestep
        Me.m_StratumIdSource = stratumIdSource
        Me.m_StateClassIdSource = stateClassIdSource
        Me.m_StratumIdDestination = stratumIdDestination
        Me.m_StateClassIdDestination = stateClassIdDestination
        Me.m_TransitionTypeId = transitionTypeId
        Me.m_Probability = probability
        Me.m_Proportion = proportion
        Me.m_AgeMinimum = ageMinimum
        Me.m_AgeMaximum = ageMaximum
        Me.m_AgeRelative = ageRelative
        Me.m_AgeReset = ageReset
        Me.m_TstMinimum = tstMinimum
        Me.m_TstMaximum = tstMaximum
        Me.m_TstRelative = tstRelative

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

    Public Property TransitionTypeId As Integer
        Get
            Return Me.m_TransitionTypeId
        End Get
        Set(ByVal value As Integer)
            Me.m_TransitionTypeId = value
        End Set
    End Property

    Public Property Probability As Double
        Get
            Return Me.m_Probability
        End Get
        Set(ByVal value As Double)
            Me.m_Probability = value
        End Set
    End Property

    Public Property Proportion As Double
        Get
            Return Me.m_Proportion
        End Get
        Set(ByVal value As Double)
            Me.m_Proportion = value
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

    Public Property AgeRelative As Integer
        Get
            Return Me.m_AgeRelative
        End Get
        Set(ByVal value As Integer)
            Me.m_AgeRelative = value
        End Set
    End Property

    Public Property AgeReset As Boolean
        Get
            Return Me.m_AgeReset
        End Get
        Set(ByVal value As Boolean)
            Me.m_AgeReset = value
        End Set
    End Property

    Public Property TstMinimum As Integer
        Get
            Return Me.m_TstMinimum
        End Get
        Set(ByVal value As Integer)
            Me.m_TstMinimum = value
        End Set
    End Property

    Public Property TstMaximum As Integer
        Get
            Return Me.m_TstMaximum
        End Get
        Set(ByVal value As Integer)
            Me.m_TstMaximum = value
        End Set
    End Property

    Public Property TstRelative As Integer
        Get
            Return Me.m_TstRelative
        End Get
        Set(ByVal value As Integer)
            Me.m_TstRelative = value
        End Set
    End Property

End Class
