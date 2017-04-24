'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

''' <summary>
''' Represents a single Transition Type
''' </summary>
Public Class TransitionType

    Private m_TransitionTypeId As Integer
    Private m_DisplayName As String
    Private m_MapId As Nullable(Of Integer)
    Private m_TransitionGroups As New TransitionGroupCollection
    Private m_PrimaryTransitionGroups As New TransitionGroupCollection

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="transitionTypeId">The Id for the transition type</param>
    ''' <param name="displayName">The name for the transition type</param>
    ''' <param name="mapId">The map Id for the transition type</param>
    ''' <remarks></remarks>
    Public Sub New(
        ByVal transitionTypeId As Integer,
        ByVal displayName As String,
        ByVal mapId As Nullable(Of Integer))

        Me.m_TransitionTypeId = transitionTypeId
        Me.m_DisplayName = displayName
        Me.m_MapId = mapId

    End Sub

    ''' <summary>
    ''' Transition Type Id
    ''' </summary>
    Public ReadOnly Property TransitionTypeId As Integer
        Get
            Return Me.m_TransitionTypeId
        End Get
    End Property

    ''' <summary>
    ''' Gets the display name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DisplayName As String
        Get
            Return Me.m_DisplayName
        End Get
    End Property

    ''' <summary>
    ''' Gets the map Id
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MapId As Nullable(Of Integer)
        Get
            Return Me.m_MapId
        End Get
    End Property

    ''' <summary>
    ''' Gets the transition group collection
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TransitionGroups As TransitionGroupCollection
        Get
            Return Me.m_TransitionGroups
        End Get
    End Property

    ''' <summary>
    ''' Gets the primary transition group collection
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property PrimaryTransitionGroups As TransitionGroupCollection
        Get
            Return Me.m_PrimaryTransitionGroups
        End Get
    End Property

End Class

