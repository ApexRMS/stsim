'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.StochasticTime

Friend Class TransitionTarget
    Inherits STSimDistributionBase

    Private m_TransitionGroupId As Integer
    Private m_ExpectedAmount As Double
    Private m_Multiplier As Double = 1.0

    Public Sub New(
        ByVal iteration As Nullable(Of Integer),
        ByVal timestep As Nullable(Of Integer),
        ByVal stratumId As Nullable(Of Integer),
        ByVal secondaryStratumId As Nullable(Of Integer),
        ByVal transitionGroupId As Integer,
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
            targetAmount,
            distributionTypeId,
            distributionFrequency,
            distributionSD,
            distributionMin,
            distributionMax)

        Me.m_TransitionGroupId = transitionGroupId

    End Sub

    Public ReadOnly Property TransitionGroupId As Integer
        Get
            Return Me.m_TransitionGroupId
        End Get
    End Property

    Public Property ExpectedAmount As Double
        Get
            Return Me.m_ExpectedAmount
        End Get
        Set(ByVal value As Double)
            Me.m_ExpectedAmount = value
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

    Public Overrides Function Clone() As STSimDistributionBase

        Dim t As New TransitionTarget(
             Me.Iteration,
             Me.Timestep,
             Me.StratumId,
             Me.SecondaryStratumId,
             Me.TransitionGroupId,
             Me.DistributionValue,
             Me.DistributionTypeId,
             Me.DistributionFrequency,
             Me.DistributionSD,
             Me.DistributionMin,
             Me.DistributionMax)

        t.ExpectedAmount = Me.ExpectedAmount
        t.Multiplier = Me.Multiplier

        Return t

    End Function

End Class

