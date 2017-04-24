'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

''' <summary>
''' Cell event arguments
''' </summary>
''' <remarks></remarks>
Public Class CellEventArgs
    Inherits EventArgs

    Private m_Cell As Cell
    Private m_Iteration As Integer
    Private m_Timestep As Integer
    Private m_AmountPerCell As Double

    Friend Sub New(
        ByVal cell As Cell,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal amountPerCell As Double)

        Me.m_Cell = cell
        Me.m_Iteration = iteration
        Me.m_Timestep = timestep
        Me.m_AmountPerCell = amountPerCell

    End Sub

    Public ReadOnly Property Cell As Cell
        Get
            Return Me.m_Cell
        End Get
    End Property

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

    Public ReadOnly Property AmountPerCell As Double
        Get
            Return Me.m_AmountPerCell
        End Get
    End Property

End Class

''' <summary>
''' Cell change probabilistic event arguments
''' </summary>
''' <remarks></remarks>
Public Class CellChangeEventArgs
    Inherits CellEventArgs

    Private m_DeterministicPathway As DeterministicTransition
    Private m_ProbabilisticPathway As Transition

    Friend Sub New(
        ByVal cell As Cell,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal deterministicPathway As DeterministicTransition,
        ByVal probabilisticPathway As Transition,
        ByVal amountPerCell As Double)

        MyBase.New(cell, iteration, timestep, amountPerCell)

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

''' <summary>
''' Apply probabilistic transitions raster event arguments
''' </summary>
''' <remarks></remarks>
Public Class ApplyProbabilisticTransitionsRasterEventArgs
    Inherits EventArgs

    Private m_Timestep As Integer
    Private m_Iteration As Integer

    Friend Sub New(ByVal iteration As Integer, ByVal timestep As Integer)
        Me.m_Iteration = iteration
        Me.m_Timestep = timestep
    End Sub

    Public ReadOnly Property Iteration() As Integer
        Get
            Return m_Iteration
        End Get
    End Property

    Public ReadOnly Property Timestep As Integer
        Get
            Return Me.m_Timestep
        End Get
    End Property

End Class

''' <summary>
''' Get external multipliers event arguments
''' </summary>
''' <remarks></remarks>
Public Class ExternalMultipliersEventArgs
    Inherits EventArgs

    Private m_CellId As Integer
    Private m_Timestep As Integer
    Private m_TransitionGroupId As Integer
    Private m_Multiplier As Double = 1.0

    Friend Sub New(
        ByVal cellId As Integer,
        ByVal timestep As Integer,
        ByVal transitionGroupId As Integer)

        Me.m_CellId = cellId
        Me.m_Timestep = timestep
        Me.m_TransitionGroupId = transitionGroupId

    End Sub

    Public ReadOnly Property CellId As Integer
        Get
            Return Me.m_CellId
        End Get
    End Property

    Public ReadOnly Property Timestep As Integer
        Get
            Return Me.m_Timestep
        End Get
    End Property

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




