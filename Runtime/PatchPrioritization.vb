'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

''' <summary>
''' Patch Prioritization
''' </summary>
''' <remarks></remarks>
Friend Class PatchPrioritization

    Private m_PatchPrioritizationId As Integer
    Private m_PatchPrioritizationType As PatchPrioritizationType
    Private m_TransitionPatches As New List(Of TransitionPatch)

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="prioritizationId"></param>
    ''' <param name="prioritizationType"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal prioritizationId As Integer, ByVal prioritizationType As PatchPrioritizationType)

        Me.m_PatchPrioritizationId = prioritizationId
        Me.m_PatchPrioritizationType = prioritizationType

    End Sub

    ''' <summary>
    ''' Gets the Id for this patch prioritization
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property PatchPrioritizationId As Integer
        Get
            Return Me.m_PatchPrioritizationId
        End Get
    End Property

    ''' <summary>
    ''' Gets the prioritization type for this patch prioritization
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property PatchPrioritizationType As PatchPrioritizationType
        Get
            Return Me.m_PatchPrioritizationType
        End Get
    End Property

    ''' <summary>
    ''' Gets the transition patches collection
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property TransitionPatches As List(Of TransitionPatch)
        Get
            Return Me.m_TransitionPatches
        End Get
    End Property

End Class

