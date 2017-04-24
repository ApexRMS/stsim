'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

''' <summary>
''' Transition Patch Prioritization
''' </summary>
''' <remarks></remarks>
Friend Class TransitionPatchPrioritization

    Private m_TransitionPatchPrioritizationId As Integer
    Private m_Iteration As Nullable(Of Integer)
    Private m_Timestep As Nullable(Of Integer)
    Private m_TransitionGroupId As Integer
    Private m_PatchPrioritizationId As Integer

    Public Sub New(
        ByVal transitionPatchPrioritizationId As Integer,
        ByVal iteration As Nullable(Of Integer),
        ByVal timestep As Nullable(Of Integer),
        ByVal transitionGroupId As Integer,
        ByVal patchPrioritizationId As Integer)

        Me.m_TransitionPatchPrioritizationId = transitionPatchPrioritizationId
        Me.m_Iteration = iteration
        Me.m_Timestep = timestep
        Me.m_TransitionGroupId = transitionGroupId
        Me.m_PatchPrioritizationId = patchPrioritizationId

    End Sub

    ''' <summary>
    ''' Gets the Transition Patch PrioritizationId
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TransitionPatchPrioritizationId As Integer
        Get
            Return Me.m_TransitionPatchPrioritizationId
        End Get
    End Property

    ''' <summary>
    ''' Gets the iteration
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Iteration As Nullable(Of Integer)
        Get
            Return Me.m_Iteration
        End Get
    End Property

    ''' <summary>
    ''' Gets the timestep
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Timestep As Nullable(Of Integer)
        Get
            Return Me.m_Timestep
        End Get
    End Property

    ''' <summary>
    ''' Gets the transition group Id
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TransitionGroupId As Integer
        Get
            Return Me.m_TransitionGroupId
        End Get
    End Property

    ''' <summary>
    ''' Gets the Patch Prioritization Id
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property PatchPrioritizationId As Integer
        Get
            Return Me.m_PatchPrioritizationId
        End Get
    End Property

End Class
