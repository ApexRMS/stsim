'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.StochasticTime

Friend Class TransitionAttributeTarget
    Inherits STSimDistributionBase

    Private m_TransitionAttributeTargetId As Integer
    Private m_TransitionAttributeTypeId As Integer
    Private m_TargetRemaining As Double
    Private m_ExpectedAmount As Double
    Private m_Multiplier As Double = 1.0

    Public Sub New(
        ByVal transitionAttributeTargetId As Integer,
        ByVal iteration As Nullable(Of Integer),
        ByVal timestep As Nullable(Of Integer),
        ByVal stratumId As Nullable(Of Integer),
        ByVal secondaryStratumId As Nullable(Of Integer),
        ByVal tertiaryStratumId As Nullable(Of Integer),
        ByVal transitionAttributeTypeId As Integer,
        ByVal targetAmount As Nullable(Of Double),
        ByVal distributionTypeId As Nullable(Of Integer),
        ByVal distributionFrequency As Nullable(Of DistributionFrequency),
        ByVal distributionSD As Nullable(Of Double),
        ByVal distributionMin As Nullable(Of Double),
        ByVal distributionMax As Nullable(Of Double))

        MyBase.New(
            iteration,
            timestep,
            stratumId,
            secondaryStratumId,
            tertiaryStratumId,
            targetAmount,
            distributionTypeId,
            distributionFrequency,
            distributionSD,
            distributionMin,
            distributionMax)

        Me.m_TransitionAttributeTargetId = transitionAttributeTargetId
        Me.m_TransitionAttributeTypeId = transitionAttributeTypeId

    End Sub

    Public ReadOnly Property TransitionAttributeTargetId As Integer
        Get
            Return Me.m_TransitionAttributeTargetId
        End Get
    End Property

    Public ReadOnly Property TransitionAttributeTypeId As Integer
        Get
            Return Me.m_TransitionAttributeTypeId
        End Get
    End Property

    Public Property TargetRemaining As Double
        Get
            Return Me.m_TargetRemaining
        End Get
        Set(value As Double)
            Me.m_TargetRemaining = value
        End Set
    End Property

    Public Property Multiplier As Double
        Get
            Return Me.m_Multiplier
        End Get
        Set(ByVal value As Double)
            Me.m_Multiplier = value
        End Set
    End Property

    Public Property ExpectedAmount As Double
        Get
            Return Me.m_ExpectedAmount
        End Get
        Set(ByVal value As Double)
            Me.m_ExpectedAmount = value
        End Set
    End Property

    Public Overrides Function Clone() As STSimDistributionBase

        Dim t As New TransitionAttributeTarget(
            Me.TransitionAttributeTargetId,
            Me.Iteration,
            Me.Timestep,
            Me.StratumId,
            Me.SecondaryStratumId,
            Me.TertiaryStratumId,
            Me.TransitionAttributeTypeId,
            Me.DistributionValue,
            Me.DistributionTypeId,
            Me.DistributionFrequency,
            Me.DistributionSD,
            Me.DistributionMin,
            Me.DistributionMax)

        t.TargetRemaining = Me.TargetRemaining
        t.Multiplier = Me.Multiplier
        t.ExpectedAmount = Me.ExpectedAmount

        Return t

    End Function

End Class


