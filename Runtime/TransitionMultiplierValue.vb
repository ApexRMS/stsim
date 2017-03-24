'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.StochasticTime

Friend Class TransitionMultiplierValue
    Inherits STSimDistributionBase

    Private m_TransitionGroupId As Integer
    Private m_StateClassId As Nullable(Of Integer)
    Private m_TransitionMultiplierTypeId As Nullable(Of Integer)

    Public Sub New(
        ByVal transitionGroupId As Integer,
        ByVal iteration As Nullable(Of Integer),
        ByVal timestep As Nullable(Of Integer),
        ByVal stratumId As Nullable(Of Integer),
        ByVal secondaryStratumId As Nullable(Of Integer),
        ByVal stateClassId As Nullable(Of Integer),
        ByVal transitionMultiplierTypeId As Nullable(Of Integer),
        ByVal multiplierValue As Nullable(Of Double),
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
            multiplierValue,
            distributionTypeId,
            distributionFrequency,
            distributionSD,
            distributionMin,
            distributionMax)

        Me.m_TransitionGroupId = transitionGroupId
        Me.m_StateClassId = stateClassId
        Me.m_TransitionMultiplierTypeId = transitionMultiplierTypeId

    End Sub

    Public ReadOnly Property TransitionGroupId As Integer
        Get
            Return Me.m_TransitionGroupId
        End Get
    End Property

    Public ReadOnly Property StateClassId As Nullable(Of Integer)
        Get
            Return Me.m_StateClassId
        End Get
    End Property

    Public ReadOnly Property TransitionMultiplierTypeId As Nullable(Of Integer)
        Get
            Return Me.m_TransitionMultiplierTypeId
        End Get
    End Property

    Public Overrides Function Clone() As STSimDistributionBase

        Return New TransitionMultiplierValue(
            Me.TransitionGroupId,
            Me.Iteration,
            Me.Timestep,
            Me.StratumId,
            Me.SecondaryStratumId,
            Me.StateClassId,
            Me.TransitionMultiplierTypeId,
            Me.DistributionValue,
            Me.DistributionTypeId,
            Me.DistributionFrequency,
            Me.DistributionSD,
            Me.DistributionMin,
            Me.DistributionMax)

    End Function

End Class

