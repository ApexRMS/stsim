'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Public Class TransitionGroup

    Private m_TransitionGroupId As Integer
    Private m_DisplayName As String
    Private m_TransitionTypes As New TransitionTypeCollection
    Private m_PrimaryTransitionTypes As New TransitionTypeCollection
    Private m_TransitionSpreadCells As New Dictionary(Of Integer, Cell)
    Private m_TransitionSpreadDistributionMap As New TransitionSpreadDistributionMap()
    Private m_HasSizeDistribution As Boolean
    Private m_PatchPrioritization As PatchPrioritization
    Private m_Order As Double = DEFAULT_TRANSITION_ORDER
    Private m_IsAuto As Boolean

    Public Sub New(
        ByVal transitionGroupId As Integer,
        ByVal transitionGroupName As String,
        ByVal isAuto As Boolean)

        Me.m_TransitionGroupId = transitionGroupId
        Me.m_DisplayName = transitionGroupName
        Me.m_IsAuto = isAuto

    End Sub

    Public ReadOnly Property TransitionGroupId As Integer
        Get
            Return Me.m_TransitionGroupId
        End Get
    End Property

    Public ReadOnly Property DisplayName() As String
        Get
            Return m_DisplayName
        End Get
    End Property

    Friend ReadOnly Property TransitionTypes As TransitionTypeCollection
        Get
            Return Me.m_TransitionTypes
        End Get
    End Property

    Friend ReadOnly Property PrimaryTransitionTypes As TransitionTypeCollection
        Get
            Return Me.m_PrimaryTransitionTypes
        End Get
    End Property

    Friend ReadOnly Property TransitionSpreadCells As Dictionary(Of Integer, Cell)
        Get
            Return Me.m_TransitionSpreadCells
        End Get
    End Property

    Friend ReadOnly Property TransitionSpreadDistributionMap As TransitionSpreadDistributionMap
        Get
            Return Me.m_TransitionSpreadDistributionMap
        End Get
    End Property

    Friend Property PatchPrioritization As PatchPrioritization
        Get
            Return Me.m_PatchPrioritization
        End Get
        Set(value As PatchPrioritization)
            Me.m_PatchPrioritization = value
        End Set
    End Property

    Public Property HasSizeDistribution As Boolean
        Get
            Return Me.m_HasSizeDistribution
        End Get
        Set(value As Boolean)
            Me.m_HasSizeDistribution = value
        End Set
    End Property

    Public Property Order As Double
        Get
            Return Me.m_Order
        End Get
        Set(value As Double)
            Me.m_Order = value
        End Set
    End Property

    Public ReadOnly Property IsAuto As Boolean
        Get
            Return Me.m_IsAuto
        End Get
    End Property

End Class

