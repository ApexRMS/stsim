'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

''' <summary>
''' The stratum stateclass type
''' </summary>
''' <remarks></remarks>
Friend Class TransitionStratumStateClass

    Private m_StratumId As Integer
    Private m_StateClassId As Integer
    Private m_Transitions As New TransitionCollection

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="stratumId">The stratum</param>
    ''' <param name="stateClassId">The state class</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal stratumId As Integer, ByVal stateClassId As Integer)

        Me.m_StratumId = stratumId
        Me.m_StateClassId = stateClassId

    End Sub

    ''' <summary>
    ''' Gets the Stratum Id
    ''' </summary>
    Public ReadOnly Property StratumId As Integer
        Get
            Return Me.m_StratumId
        End Get
    End Property

    ''' <summary>
    ''' Gets the StateClass Id
    ''' </summary>
    Public ReadOnly Property StateClassId As Integer
        Get
            Return Me.m_StateClassId
        End Get
    End Property

    ''' <summary>
    ''' Gets the transition collection
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Transitions As TransitionCollection
        Get
            Return Me.m_Transitions
        End Get
    End Property

End Class

