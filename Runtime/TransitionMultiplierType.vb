'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.Core

Friend Class TransitionMultiplierType

    Private m_Scenario As Scenario
    Private m_Provider As STSimDistributionProvider
    Private m_TransitionMultiplierTypeId As Nullable(Of Integer)
    Private m_TransitionMultiplierValues As New TransitionMultiplierValueCollection
    Private m_TransitionMultiplierValueMap As TransitionMultiplierValueMap
    Private m_TransitionSpatialMultipliers As New TransitionSpatialMultiplierCollection
    Private m_TransitionSpatialMultiplierMap As TransitionSpatialMultiplierMap
    Private m_TransitionSpatialInitiationMultipliers As New TransitionSpatialInitiationMultiplierCollection
    Private m_TransitionSpatialInitiationMultiplierMap As TransitionSpatialInitiationMultiplierMap

    Public Sub New(
        ByVal transitionMultiplierTypeId As Nullable(Of Integer),
        ByVal scenario As Scenario,
        ByVal provider As STSimDistributionProvider)

        Me.m_TransitionMultiplierTypeId = transitionMultiplierTypeId
        Me.m_Scenario = scenario
        Me.m_Provider = provider

    End Sub

    Public ReadOnly Property TransitionMultiplierTypeId As Nullable(Of Integer)
        Get
            Return Me.m_TransitionMultiplierTypeId
        End Get
    End Property

    Friend ReadOnly Property TransitionMultiplierValueMap As TransitionMultiplierValueMap
        Get
            Return Me.m_TransitionMultiplierValueMap
        End Get
    End Property

    Friend ReadOnly Property TransitionSpatialMultiplierMap As TransitionSpatialMultiplierMap
        Get
            Return Me.m_TransitionSpatialMultiplierMap
        End Get
    End Property

    Friend ReadOnly Property TransitionSpatialInitiationMultiplierMap As TransitionSpatialInitiationMultiplierMap
        Get
            Return Me.m_TransitionSpatialInitiationMultiplierMap
        End Get
    End Property

    Friend Sub AddTransitionMultiplierValue(ByVal multiplier As TransitionMultiplierValue)

        If (multiplier.TransitionMultiplierTypeId <> Me.m_TransitionMultiplierTypeId) Then
            Throw New ArgumentException("The transition multiplier type is not correct.")
        End If

        Me.m_TransitionMultiplierValues.Add(multiplier)

    End Sub

    Friend Sub AddTransitionSpatialMultiplier(ByVal multiplier As TransitionSpatialMultiplier)

        If (multiplier.TransitionMultiplierTypeId <> Me.m_TransitionMultiplierTypeId) Then
            Throw New ArgumentException("The transition multiplier type is not correct.")
        End If

        Me.m_TransitionSpatialMultipliers.Add(multiplier)

    End Sub

    Friend Sub AddTransitionSpatialInitiationMultiplier(ByVal multiplier As TransitionSpatialInitiationMultiplier)

        If (multiplier.TransitionMultiplierTypeId <> Me.m_TransitionMultiplierTypeId) Then
            Throw New ArgumentException("The transition multiplier type is not correct.")
        End If

        Me.m_TransitionSpatialInitiationMultipliers.Add(multiplier)

    End Sub

    Friend Sub ClearSpatial()

        Me.m_TransitionSpatialMultipliers.Clear()
        Me.m_TransitionSpatialMultiplierMap = Nothing

        Me.m_TransitionSpatialInitiationMultipliers.Clear()
        Me.m_TransitionSpatialInitiationMultiplierMap = Nothing

    End Sub

    Friend Sub CreateTransitionMultiplierValueMap()

        If (Me.m_TransitionMultiplierValues.Count > 0) Then

            Debug.Assert(Me.m_TransitionMultiplierValueMap Is Nothing)

            Me.m_TransitionMultiplierValueMap = New TransitionMultiplierValueMap(
                Me.m_Scenario, Me.m_TransitionMultiplierValues, Me.m_Provider)

        End If

    End Sub

    Friend Sub CreateTransitionSpatialMultiplierMap()

        If (Me.m_TransitionSpatialMultipliers.Count > 0) Then

            Debug.Assert(Me.m_TransitionSpatialMultiplierMap Is Nothing)
            Me.m_TransitionSpatialMultiplierMap = New TransitionSpatialMultiplierMap(Me.m_TransitionSpatialMultipliers)

        End If

    End Sub

    Friend Sub CreateTransitionSpatialInitiationMultiplierMap()

        If (Me.m_TransitionSpatialInitiationMultipliers.Count > 0) Then

            Debug.Assert(Me.m_TransitionSpatialInitiationMultiplierMap Is Nothing)
            Me.m_TransitionSpatialInitiationMultiplierMap = New TransitionSpatialInitiationMultiplierMap(Me.m_TransitionSpatialInitiationMultipliers)

        End If

    End Sub

End Class
