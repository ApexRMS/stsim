'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

''' <summary>
''' Transition patch
''' </summary>
''' <remarks></remarks>
Class TransitionPatch

    Private m_Size As Double
    Private m_SeedCell As Cell
    Private m_EdgeCells As New Dictionary(Of Integer, Cell)()
    Private m_AllCells As New Dictionary(Of Integer, Cell)()

    Public Sub New(ByVal seedCell As Cell)
        Me.m_SeedCell = seedCell
    End Sub

    Public Property Size As Double
        Get
            Return Me.m_Size
        End Get
        Set(value As Double)
            Me.m_Size = value
        End Set
    End Property

    Public ReadOnly Property SeedCell As Cell
        Get
            Return Me.m_SeedCell
        End Get
    End Property

    Public ReadOnly Property EdgeCells As Dictionary(Of Integer, Cell)
        Get
            Return Me.m_EdgeCells
        End Get
    End Property

    Public ReadOnly Property AllCells As Dictionary(Of Integer, Cell)
        Get
            Return Me.m_AllCells
        End Get
    End Property

End Class
