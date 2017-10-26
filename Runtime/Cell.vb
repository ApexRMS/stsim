'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

''' <summary>
''' The simulation cell class
''' </summary>
''' <remarks>
''' There can be a huge number of these so:
''' (1.) Don't add any data members to this class unless you absolutely need to
''' (2.) Don't make any allocations unless they are required for the model run
''' </remarks>
Public Class Cell

    Private m_CellId As Integer
    Private m_StratumId As Integer
    Private m_SecondaryStratumId As Nullable(Of Integer)
    Private m_TertiaryStratumId As Nullable(Of Integer)
    Private m_StateClassId As Integer
    Private m_Age As Integer
    Private m_TstValues As New TstCollection
    Private m_Transitions As New List(Of Transition)
    Private m_Keys As Dictionary(Of String, Object)

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="cellID">The ID for this cell</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal cellId As Integer)
        Me.m_CellId = cellId
    End Sub

    ''' <summary>
    ''' Unique integer Id for the cell
    ''' </summary>
    Public ReadOnly Property CellId As Integer
        Get
            Return Me.m_CellId
        End Get
    End Property

    ''' <summary>
    ''' Stratum Id for the cell
    ''' </summary>
    Public Property StratumId As Integer
        Get
            Return Me.m_StratumId
        End Get
        Set(ByVal value As Integer)
            Me.m_StratumId = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the secondary stratum Id for the cell
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SecondaryStratumId As Nullable(Of Integer)
        Get
            Return Me.m_SecondaryStratumId
        End Get
        Set(value As Nullable(Of Integer))
            Me.m_SecondaryStratumId = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the tertiary stratum Id for the cell
    ''' </summary>
    ''' <returns></returns>
    Public Property TertiaryStratumId As Nullable(Of Integer)
        Get
            Return Me.m_TertiaryStratumId
        End Get
        Set(ByVal value As Nullable(Of Integer))
            Me.m_TertiaryStratumId = value
        End Set
    End Property

    ''' <summary>
    ''' StateClass Id for the cell
    ''' </summary>
    Public Property StateClassId As Integer
        Get
            Return Me.m_StateClassId
        End Get
        Set(ByVal value As Integer)
            Me.m_StateClassId = value
        End Set
    End Property

    ''' <summary>
    ''' Age of the cell
    ''' </summary>
    ''' <remarks></remarks>
    Public Property Age As Integer
        Get
            Return Me.m_Age
        End Get
        Set(ByVal value As Integer)
            Me.m_Age = value
        End Set
    End Property

    ''' <summary>
    ''' Collection of TST values for the cell
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend ReadOnly Property TstValues As TstCollection
        Get
            Return Me.m_TstValues
        End Get
    End Property

    ''' <summary>
    ''' Collection of transitions for the cell
    ''' </summary>
    ''' <remarks></remarks>
    Friend ReadOnly Property Transitions As IList(Of Transition)
        Get
            Return Me.m_Transitions
        End Get
    End Property

    ''' <summary>
    ''' Associates an object with this cell
    ''' </summary>
    ''' <param name="key"></param>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Public Sub SetAssociatedObject(ByVal key As String, ByVal value As Object)

        If (Me.m_Keys Is Nothing) Then
            Me.m_Keys = New Dictionary(Of String, Object)
        End If

        Me.m_Keys.Add(key, value)

    End Sub

    ''' <summary>
    ''' Gets an associated object for this cell
    ''' </summary>
    ''' <param name="key"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAssociatedObject(ByVal key As String) As Object

        If (Me.m_Keys Is Nothing) Then
            Return Nothing
        End If

        Return Me.m_Keys(key)

    End Function

End Class
