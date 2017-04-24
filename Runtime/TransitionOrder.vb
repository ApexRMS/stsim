'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Friend Class TransitionOrder

    Private m_TransitionGroupId As Integer
    Private m_Iteration As Nullable(Of Integer)
    Private m_Timestep As Nullable(Of Integer)
    Private m_Order As Double = DEFAULT_TRANSITION_ORDER

    Public Sub New(
        ByVal transitionGroupId As Integer,
        ByVal iteration As Nullable(Of Integer),
        ByVal timestep As Nullable(Of Integer),
        ByVal order As Nullable(Of Double))

        Me.m_TransitionGroupId = transitionGroupId
        Me.m_Iteration = iteration
        Me.m_Timestep = timestep

        If (order.HasValue) Then
            Me.m_Order = order.Value
        End If

    End Sub

    Public ReadOnly Property TransitionGroupId As Integer
        Get
            Return Me.m_TransitionGroupId
        End Get
    End Property

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

    Public ReadOnly Property Order As Double
        Get
            Return Me.m_Order
        End Get
    End Property

End Class

