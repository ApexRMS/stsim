'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.StochasticTime

Class TransitionAdjacencyMultiplier
    Inherits STSimDistributionBase

    Private m_TransitionGroupId As Integer
    Private m_AttributeValue As Double

    Public Sub New(
        ByVal transitionGroupId As Integer,
        ByVal iteration As Nullable(Of Integer),
        ByVal timestep As Nullable(Of Integer),
        ByVal stratumId As Nullable(Of Integer),
        ByVal secondaryStratumId As Nullable(Of Integer),
        ByVal tertiaryStratumId As Nullable(Of Integer),
        ByVal attributeValue As Double,
        ByVal multiplierAmount As Nullable(Of Double),
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
            multiplierAmount,
            distributionTypeId,
            distributionFrequency,
            distributionSD,
            distributionMin,
            distributionMax)

        Me.m_TransitionGroupId = transitionGroupId
        Me.m_AttributeValue = attributeValue

    End Sub

    Public ReadOnly Property TransitionGroupId As Integer
        Get
            Return Me.m_TransitionGroupId
        End Get
    End Property

    Public ReadOnly Property AttributeValue As Double
        Get
            Return Me.m_AttributeValue
        End Get
    End Property

    Public Overrides Function Clone() As STSimDistributionBase

        Return New TransitionAdjacencyMultiplier(
            Me.TransitionGroupId,
            Me.Iteration,
            Me.Timestep,
            Me.StratumId,
            Me.SecondaryStratumId,
            Me.TertiaryStratumId,
            Me.AttributeValue,
            Me.DistributionValue,
            Me.DistributionTypeId,
            Me.DistributionFrequency,
            Me.DistributionSD,
            Me.DistributionMin,
            Me.DistributionMax)

    End Function

End Class
