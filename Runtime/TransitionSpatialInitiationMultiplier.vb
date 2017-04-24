'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.StochasticTime

''' <summary>
''' Transition spatial multiplier
''' </summary>
''' <remarks></remarks>
Friend Class TransitionSpatialInitiationMultiplier

    Private m_TransitionSpatialInitiationMultiplierId As Integer
    Private m_TransitionGroupId As Integer
    Private m_TransitionMultiplierTypeId As Nullable(Of Integer)
    Private m_Iteration As Nullable(Of Integer)
    Private m_Timestep As Nullable(Of Integer)
    Private m_Filename As String

    Public Sub New(
        ByVal transitionSpatialInitiationMultiplierId As Integer,
        ByVal transitionGroupId As Integer,
        ByVal transitionMultiplierTypeId As Nullable(Of Integer),
        ByVal iteration As Nullable(Of Integer),
        ByVal timestep As Nullable(Of Integer),
        ByVal fileName As String)

        If (fileName Is Nothing) Then
            Throw New ArgumentException("The fileName parameter is not valid.")
        End If

        Me.m_TransitionSpatialInitiationMultiplierId = transitionSpatialInitiationMultiplierId
        Me.m_TransitionGroupId = transitionGroupId
        Me.m_TransitionMultiplierTypeId = transitionmultipliertypeid
        Me.m_Iteration = iteration
        Me.m_Timestep = timestep
        Me.m_Filename = fileName

    End Sub

    ''' <summary>
    ''' Gets the Transition Spatial Multiplier Id
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TransitionSpatialInitiationMultiplierId As Integer
        Get
            Return Me.m_TransitionSpatialInitiationMultiplierId
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
    ''' Gets the transition multiplier type Id
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TransitionMultiplierTypeId As Nullable(Of Integer)
        Get
            Return Me.m_TransitionMultiplierTypeId
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
    ''' Gets the file name
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property FileName As String
        Get
            Return Me.m_Filename
        End Get
    End Property

End Class

