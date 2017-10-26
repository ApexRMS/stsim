'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

''' <summary>
''' The Output Stratum Transition State class
''' </summary>
''' <remarks></remarks>
Friend Class OutputStratumTransitionState

    Private m_StratumId As Integer
    Private m_SecondaryStratumId As Nullable(Of Integer)
    Private m_TertiaryStratumId As Nullable(Of Integer)
    Private m_Iteration As Integer
    Private m_Timestep As Integer
    Private m_TransitionTypeId As Integer
    Private m_StateClassId As Integer
    Private m_EndStateClassId As Integer
    Private m_Amount As Double

    Public Sub New(
        ByVal stratumId As Integer,
        ByVal secondaryStratumId As Nullable(Of Integer),
        ByVal tertiaryStratumId As Nullable(Of Integer),
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal transitionTypeId As Integer,
        ByVal stateClassId As Integer,
        ByVal endStateClassId As Integer,
        ByVal amount As Double)

        Me.m_StratumId = stratumId
        Me.m_SecondaryStratumId = secondaryStratumId
        Me.m_TertiaryStratumId = tertiaryStratumId
        Me.m_Iteration = iteration
        Me.m_Timestep = timestep
        Me.m_TransitionTypeId = transitionTypeId
        Me.m_StateClassId = stateClassId
        Me.m_EndStateClassId = endStateClassId
        Me.m_Amount = amount

    End Sub

    ''' <summary>
    ''' The output Stratum ID
    ''' </summary>
    Public ReadOnly Property StratumId As Integer
        Get
            Return Me.m_StratumId
        End Get
    End Property

    ''' <summary>
    ''' Gets the secondary stratum Id
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property SecondaryStratumId As Nullable(Of Integer)
        Get
            Return Me.m_SecondaryStratumId
        End Get
    End Property

    ''' <summary>
    ''' Gets the tertiary stratum Id
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TertiaryStratumId As Nullable(Of Integer)
        Get
            Return Me.m_TertiaryStratumId
        End Get
    End Property

    ''' <summary>
    ''' The output iteration
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Iteration As Integer
        Get
            Return Me.m_Iteration
        End Get
    End Property

    ''' <summary>
    ''' The output timestep
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Timestep As Integer
        Get
            Return Me.m_Timestep
        End Get
    End Property

    ''' <summary>
    ''' Gets the output transition type
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TransitionTypeId As Integer
        Get
            Return Me.m_TransitionTypeId
        End Get
    End Property

    ''' <summary>
    ''' The output State Class Id
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property StateClassId As Integer
        Get
            Return Me.m_StateClassId
        End Get
    End Property

    ''' <summary>
    ''' The output End State Class Id
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property EndStateClassId As Integer
        Get
            Return Me.m_EndStateClassId
        End Get
    End Property

    ''' <summary>
    ''' The output amount
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Amount As Double
        Get
            Return Me.m_Amount
        End Get
        Set(ByVal value As Double)
            Me.m_Amount = value
        End Set
    End Property

End Class

