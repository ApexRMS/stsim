'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

''' <summary>
''' Transition Adjacency Setting
''' </summary>
''' <remarks></remarks>
Class TransitionAdjacencySetting

    Private m_TransitionGroupId As Integer
    Private m_StateAttributeTypeId As Integer
    Private m_NeighborhoodRadius As Double
    Private m_UpdateFrequency As Integer

    Public Sub New(
        ByVal transitionGroupId As Integer,
        ByVal stateAttributeTypeId As Integer,
        ByVal neighborhoodRadius As Double,
        ByVal updateFrequency As Integer)

        Me.m_TransitionGroupId = transitionGroupId
        Me.m_StateAttributeTypeId = stateAttributeTypeId
        Me.m_NeighborhoodRadius = neighborhoodRadius
        Me.m_UpdateFrequency = updateFrequency

    End Sub

    Public ReadOnly Property TransitionGroupId As Integer
        Get
            Return Me.m_TransitionGroupId
        End Get
    End Property

    Public ReadOnly Property StateAttributeTypeId As Integer
        Get
            Return Me.m_StateAttributeTypeId
        End Get
    End Property

    Public ReadOnly Property NeighborhoodRadius As Double
        Get
            Return Me.m_NeighborhoodRadius
        End Get
    End Property

    Public ReadOnly Property UpdateFrequency As Integer
        Get
            Return Me.m_UpdateFrequency
        End Get
    End Property

End Class
