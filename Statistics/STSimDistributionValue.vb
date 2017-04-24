'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Imports SyncroSim.StochasticTime

Public Class STSimDistributionValue
    Inherits DistributionValue

    Private m_StratumId As Nullable(Of Integer)
    Private m_SecondaryStratumId As Nullable(Of Integer)

    Public Sub New(
        ByVal iteration As Nullable(Of Integer),
        ByVal timestep As Nullable(Of Integer),
        ByVal stratumId As Nullable(Of Integer),
        ByVal secondaryStratumId As Nullable(Of Integer),
        ByVal distributionTypeId As Integer,
        ByVal externalVariableTypeId As Nullable(Of Integer),
        ByVal externalVariableMin As Nullable(Of Double),
        ByVal externalVariableMax As Nullable(Of Double),
        ByVal value As Nullable(Of Double),
        ByVal valueDistributionTypeId As Nullable(Of Integer),
        ByVal valueDistributionFrequency As Nullable(Of DistributionFrequency),
        ByVal valueDistributionSD As Nullable(Of Double),
        ByVal valueDistributionMin As Nullable(Of Double),
        ByVal valueDistributionMax As Nullable(Of Double),
        ByVal valueDistributionRelativeFrequency As Nullable(Of Double))

        MyBase.New(
              iteration,
              timestep,
              distributionTypeId,
              externalVariableTypeId,
              externalVariableMin,
              externalVariableMax,
              value,
              valueDistributionTypeId,
              valueDistributionFrequency,
              valueDistributionSD,
              valueDistributionMin,
              valueDistributionMax,
              valueDistributionRelativeFrequency)

        Me.m_StratumId = stratumId
        Me.m_SecondaryStratumId = secondaryStratumId

    End Sub

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

End Class
