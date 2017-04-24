'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Imports SyncroSim.StochasticTime
Imports System.ComponentModel

Public MustInherit Class STSimDistributionBase

    Private m_Iteration As Nullable(Of Integer)
    Private m_Timestep As Nullable(Of Integer)
    Private m_StratumId As Nullable(Of Integer)
    Private m_SecondaryStratumId As Nullable(Of Integer)
    Private m_DistributionValue As Nullable(Of Double)
    Private m_DistributionTypeId As Nullable(Of Integer)
    Private m_DistributionFrequency As DistributionFrequency = DistributionFrequency.Always
    Private m_DistributionSD As Nullable(Of Double)
    Private m_DistributionMin As Nullable(Of Double)
    Private m_DistributionMax As Nullable(Of Double)
    Private m_CurrentValue As Nullable(Of Double)

    Protected Sub New(
        ByVal iteration As Nullable(Of Integer),
        ByVal timestep As Nullable(Of Integer),
        ByVal stratumId As Nullable(Of Integer),
        ByVal secondaryStratumId As Nullable(Of Integer),
        ByVal distributionValue As Nullable(Of Double),
        ByVal distributionTypeId As Nullable(Of Integer),
        ByVal distributionFrequency As Nullable(Of DistributionFrequency),
        ByVal distributionSD As Nullable(Of Double),
        ByVal distributionMin As Nullable(Of Double),
        ByVal distributionMax As Nullable(Of Double))

        Me.m_Iteration = iteration
        Me.m_Timestep = timestep
        Me.m_StratumId = stratumId
        Me.m_SecondaryStratumId = secondaryStratumId
        Me.m_DistributionValue = distributionValue
        Me.m_DistributionTypeId = distributionTypeId
        Me.m_DistributionSD = distributionSD
        Me.m_DistributionMin = distributionMin
        Me.m_DistributionMax = distributionMax

        If (distributionFrequency.HasValue) Then
            Me.m_DistributionFrequency = distributionFrequency.Value
        End If

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

    Public Property StratumId As Nullable(Of Integer)
        Get
            Return Me.m_StratumId
        End Get
        Set(value As Nullable(Of Integer))
            Me.m_StratumId = value
        End Set
    End Property

    Public Property SecondaryStratumId As Nullable(Of Integer)
        Get
            Return Me.m_SecondaryStratumId
        End Get
        Set(value As Nullable(Of Integer))
            Me.m_SecondaryStratumId = value
        End Set
    End Property

    Public ReadOnly Property DistributionValue As Nullable(Of Double)
        Get
            Return Me.m_DistributionValue
        End Get
    End Property

    Public ReadOnly Property DistributionTypeId As Nullable(Of Integer)
        Get
            Return Me.m_DistributionTypeId
        End Get
    End Property

    Public ReadOnly Property DistributionFrequency As DistributionFrequency
        Get
            Return Me.m_DistributionFrequency
        End Get
    End Property

    Public ReadOnly Property DistributionSD As Nullable(Of Double)
        Get
            Return Me.m_DistributionSD
        End Get
    End Property

    Public ReadOnly Property DistributionMin As Nullable(Of Double)
        Get
            Return Me.m_DistributionMin
        End Get
    End Property

    Public ReadOnly Property DistributionMax As Nullable(Of Double)
        Get
            Return Me.m_DistributionMax
        End Get
    End Property

    <Browsable(False)>
    Public ReadOnly Property CurrentValue As Nullable(Of Double)
        Get
            Debug.Assert(Me.m_CurrentValue.HasValue)
            Return Me.m_CurrentValue
        End Get
    End Property

    Public Sub Initialize(
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal provider As STSimDistributionProvider)

        Me.InternalInitialize(iteration, timestep, provider)

    End Sub

    Public Function Sample(
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal provider As STSimDistributionProvider,
        ByVal frequency As DistributionFrequency) As Double

        Return Me.InternalSample(iteration, timestep, provider, frequency)

    End Function

    Public MustOverride Function Clone() As STSimDistributionBase

    Private Sub InternalInitialize(
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal provider As STSimDistributionProvider)

        If (Me.m_DistributionTypeId.HasValue) Then

            Dim IterationToUse As Integer = iteration
            Dim TimestepToUse As Integer = timestep

            If (Me.m_Iteration.HasValue) Then
                IterationToUse = Me.m_Iteration.Value
            End If

            If (Me.m_Timestep.HasValue) Then
                TimestepToUse = Me.m_Timestep.Value
            End If

            Me.m_CurrentValue = provider.STSimSample(
                Me.m_DistributionTypeId.Value,
                Me.m_DistributionValue,
                Me.m_DistributionSD,
                Me.m_DistributionMin,
                Me.m_DistributionMax,
                IterationToUse,
                TimestepToUse,
                Me.m_StratumId,
                Me.m_SecondaryStratumId)

        Else

            Debug.Assert(Me.m_DistributionValue.HasValue)
            Me.m_CurrentValue = Me.m_DistributionValue.Value

        End If

        Debug.Assert(Me.m_CurrentValue.HasValue)

    End Sub

    Private Function InternalSample(
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal provider As STSimDistributionProvider,
        ByVal frequency As DistributionFrequency) As Double

        If (Me.m_DistributionTypeId.HasValue) Then

            If (Me.m_DistributionFrequency = frequency Or
                Me.m_DistributionFrequency = StochasticTime.DistributionFrequency.Always) Then

                Me.m_CurrentValue = provider.STSimSample(
                    Me.m_DistributionTypeId.Value,
                    Me.m_DistributionValue,
                    Me.m_DistributionSD,
                    Me.m_DistributionMin,
                    Me.m_DistributionMax,
                    iteration,
                    timestep,
                    Me.m_StratumId,
                    Me.m_SecondaryStratumId)

            End If

        End If

        Debug.Assert(Me.m_CurrentValue.HasValue)
        Return Me.m_CurrentValue.Value

    End Function

End Class
