'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Public Class STSimEventArgs
    Inherits EventArgs

    Private m_Iteration As Integer
    Private m_Timestep As Integer

    Friend Sub New(ByVal iteration As Integer, ByVal timestep As Integer)

        Me.m_Iteration = iteration
        Me.m_Timestep = timestep

    End Sub

    Public ReadOnly Property Iteration As Integer
        Get
            Return Me.m_Iteration
        End Get
    End Property

    Public ReadOnly Property Timestep As Integer
        Get
            Return Me.m_Timestep
        End Get
    End Property

End Class

Public Class CellEventArgs
    Inherits STSimEventArgs

    Private m_SimulationCell As Cell

    Friend Sub New(
        ByVal simulationCell As Cell,
        ByVal iteration As Integer,
        ByVal timestep As Integer)

        MyBase.New(iteration, timestep)
        Me.m_SimulationCell = simulationCell

    End Sub

    Public ReadOnly Property SimulationCell As Cell
        Get
            Return Me.m_SimulationCell
        End Get
    End Property

End Class

Public Class CellChangeEventArgs
    Inherits CellEventArgs

    Private m_DeterministicPathway As DeterministicTransition
    Private m_ProbabilisticPathway As Transition

    Friend Sub New(
        ByVal simulationCell As Cell,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal deterministicPathway As DeterministicTransition,
        ByVal probabilisticPathway As Transition)

        MyBase.New(simulationCell, iteration, timestep)

        Me.m_DeterministicPathway = deterministicPathway
        Me.m_ProbabilisticPathway = probabilisticPathway

    End Sub

    Public ReadOnly Property DeterministicPathway As DeterministicTransition
        Get
            Return Me.m_DeterministicPathway
        End Get
    End Property

    Public ReadOnly Property ProbabilisticPathway As Transition
        Get
            Return Me.m_ProbabilisticPathway
        End Get
    End Property

End Class

Public Class SpatialTransitionEventArgs
    Inherits STSimEventArgs

    Friend Sub New(ByVal iteration As Integer, ByVal timestep As Integer)
        MyBase.New(iteration, timestep)
    End Sub

End Class

Public Class MultiplierEventArgs
    Inherits CellEventArgs

    Private m_TransitionGroupId As Integer
    Private m_Multiplier As Double = 1.0

    Friend Sub New(
        ByVal simulationCell As Cell,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal transitionGroupId As Integer)

        MyBase.New(simulationCell, iteration, timestep)
        Me.m_TransitionGroupId = transitionGroupId

    End Sub

    Public ReadOnly Property TransitionGroupId As Integer
        Get
            Return Me.m_TransitionGroupId
        End Get
    End Property

    Public ReadOnly Property Multiplier As Double
        Get
            Return Me.m_Multiplier
        End Get
    End Property

    Public Sub ApplyMultiplier(ByVal value As Double)
        Me.m_Multiplier *= value
    End Sub

End Class
