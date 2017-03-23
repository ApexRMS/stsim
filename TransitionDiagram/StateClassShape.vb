'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports System.Drawing
Imports SyncroSim.Core
Imports SyncroSim.Common.Forms

Class StateClassShape
    Inherits BoxDiagramShape

    Private m_Project As Project
    Private m_StratumIdSource As Nullable(Of Integer)
    Private m_StateClassIdSource As Integer
    Private m_StratumIdDest As Nullable(Of Integer)
    Private m_StateClassIdDest As Nullable(Of Integer)
    Private m_AgeMinimum As Nullable(Of Integer)
    Private m_AgeMaximum As Nullable(Of Integer)
    Private m_SLXId As Integer
    Private m_SLXDisplayName As String
    Private m_SLYId As Integer
    Private m_SLYDisplayName As String
    Private m_TooltipText As String
    Private m_IncomingDT As New List(Of DeterministicTransition)
    Private m_IncomingDTLines As New List(Of DeterministicTransitionLine)
    Private m_OutgoingDTLines As New List(Of DeterministicTransitionLine)
    Private m_IncomingPT As New List(Of Transition)
    Private m_IncomingPTLines As New List(Of ProbabilisticTransitionLine)
    Private m_OutgoingPT As New List(Of Transition)
    Private m_OutgoingPTLines As New List(Of ProbabilisticTransitionLine)
    Private m_StaticBorderPen As New Pen(TRANSITION_DIAGRAM_SHAPE_READONLY_BORDER_COLOR, ZOOM_SAFE_PEN_WIDTH)
    Private m_StaticBkBrush As New SolidBrush(TRANSITION_DIAGRAM_SHAPE_BACKGROUND_COLOR)
    Private m_SharesLocation As Boolean
    Private m_Monitor As DataSheetMonitor
    Private m_IsDisposed As Boolean

    Const ZOOM_SAFE_PEN_WIDTH As Integer = -1

    Public Sub New(
        ByVal project As Project,
        ByVal stratumIdSource As Nullable(Of Integer),
        ByVal stateClassIdSource As Integer,
        ByVal stratumIdDestination As Nullable(Of Integer),
        ByVal stateClassIdDestination As Nullable(Of Integer),
        ByVal ageMinimum As Nullable(Of Integer),
        ByVal ageMaximum As Nullable(Of Integer),
        ByVal stateLabelXId As Integer,
        ByVal stateLabelXName As String,
        ByVal stateLabelXDisplayName As String,
        ByVal stateLabelYId As Integer,
        ByVal stateLabelYDisplayName As String)

        MyBase.New(
            TRANSITION_DIAGRAM_NUM_VERTICAL_CONNECTORS,
            TRANSITION_DIAGRAM_NUM_HORIZONTAL_CONNECTORS)

        Me.m_Project = project
        Me.TitleBarText = stateLabelXName
        Me.m_StratumIdSource = stratumIdSource
        Me.m_StateClassIdSource = stateClassIdSource
        Me.m_StratumIdDest = stratumIdDestination
        Me.m_StateClassIdDest = stateClassIdDestination
        Me.m_AgeMinimum = ageMinimum
        Me.m_AgeMaximum = ageMaximum
        Me.m_SLXId = stateLabelXId
        Me.m_SLXDisplayName = stateLabelXDisplayName
        Me.m_SLYId = stateLabelYId
        Me.m_SLYDisplayName = stateLabelYDisplayName
        Me.TitleHeight = TRANSITION_DIAGRAM_TITLE_BAR_HEIGHT
        Me.ItemHeight = TRANSITION_DIAGRAM_ITEM_HEIGHT
        Me.BackgroundColor = TRANSITION_DIAGRAM_SHAPE_BACKGROUND_COLOR
        Me.SelectedBackgroundColor = Me.BackgroundColor
        Me.TitleBackgroundColor = Me.BackgroundColor
        Me.TitleSelectedBackgroundColor = Me.BackgroundColor
        Me.TitleTextColor = TRANSITION_DIAGRAM_TEXT_COLOR
        Me.TitleSelectedTextColor = TRANSITION_DIAGRAM_SELECTED_TEXT_COLOR
        Me.BorderColor = TRANSITION_DIAGRAM_SHAPE_BORDER_COLOR
        Me.DrawItemSeparators = False

        Me.m_Monitor = New DataSheetMonitor(
            Me.m_Project, DATASHEET_TERMINOLOGY_NAME, AddressOf Me.OnTerminologyChanged)

        Me.m_Monitor.Invoke()

    End Sub

    Protected Overrides Sub Dispose(disposing As Boolean)

        If (disposing And Not Me.m_IsDisposed) Then

            If (Me.m_Monitor IsNot Nothing) Then
                Me.m_Monitor.Dispose()
            End If

            If (Me.m_StaticBorderPen IsNot Nothing) Then
                Me.m_StaticBorderPen.Dispose()
            End If

            If (Me.m_StaticBkBrush IsNot Nothing) Then
                Me.m_StaticBkBrush.Dispose()
            End If

            Me.m_IsDisposed = True

        End If

        MyBase.Dispose(disposing)

    End Sub

    Public ReadOnly Property StratumIdSource() As Nullable(Of Integer)
        Get
            Return Me.m_StratumIdSource
        End Get
    End Property

    Public ReadOnly Property StateClassIdSource() As Integer
        Get
            Return Me.m_StateClassIdSource
        End Get
    End Property

    Public ReadOnly Property StratumIdDest As Nullable(Of Integer)
        Get
            Return Me.m_StratumIdDest
        End Get
    End Property

    Public ReadOnly Property StateClassIdDest() As Nullable(Of Integer)
        Get
            Return Me.m_StateClassIdDest
        End Get
    End Property

    Public ReadOnly Property AgeMinimum() As Nullable(Of Integer)
        Get
            Return Me.m_AgeMinimum
        End Get
    End Property

    Public ReadOnly Property AgeMaximum() As Nullable(Of Integer)
        Get
            Return Me.m_AgeMaximum
        End Get
    End Property

    Public ReadOnly Property StateLabelXId() As Integer
        Get
            Return Me.m_SLXId
        End Get
    End Property

    Public ReadOnly Property StateLabelYId() As Integer
        Get
            Return Me.m_SLYId
        End Get
    End Property

    Public ReadOnly Property OutgoingPT() As List(Of Transition)
        Get
            Return Me.m_OutgoingPT
        End Get
    End Property

    Public ReadOnly Property OutgoingPTLines As List(Of ProbabilisticTransitionLine)
        Get
            Return Me.m_OutgoingPTLines
        End Get
    End Property

    Public ReadOnly Property IncomingPT() As List(Of Transition)
        Get
            Return Me.m_IncomingPT
        End Get
    End Property

    Public ReadOnly Property IncomingPTLines As List(Of ProbabilisticTransitionLine)
        Get
            Return Me.m_IncomingPTLines
        End Get
    End Property

    Public ReadOnly Property IncomingDT() As List(Of DeterministicTransition)
        Get
            Return Me.m_IncomingDT
        End Get
    End Property

    Public ReadOnly Property IncomingDTLines As List(Of DeterministicTransitionLine)
        Get
            Return Me.m_IncomingDTLines
        End Get
    End Property

    Public ReadOnly Property OutgoingDTLines As List(Of DeterministicTransitionLine)
        Get
            Return Me.m_OutgoingDTLines
        End Get
    End Property

    Public WriteOnly Property SharesLocation As Boolean
        Set(value As Boolean)
            Me.m_SharesLocation = value
        End Set
    End Property

    Public Overrides Function GetToolTipText() As String
        Return Me.m_TooltipText
    End Function

    Public Overrides Sub Render(g As Drawing.Graphics)

        If (Me.m_SharesLocation And Not Me.IsSelected) Then

            Dim rc As New Rectangle(
                Me.Bounds.Left - 8,
                Me.Bounds.Top - 8,
                Me.Bounds.Width,
                Me.Bounds.Height)

            g.FillRectangle(Me.m_StaticBkBrush, rc)
            g.DrawRectangle(Me.m_StaticBorderPen, rc)

        End If

        MyBase.Render(g)

    End Sub

    Private Sub OnTerminologyChanged(ByVal e As DataSheetMonitorEventArgs)

        Dim slxlabel As String = Nothing
        Dim slylabel As String = Nothing

        Dim ds As DataSheet = Me.m_Project.GetDataSheet(DATASHEET_TERMINOLOGY_NAME)
        GetStateLabelTerminology(ds, slxlabel, slylabel)

        Me.m_TooltipText =
            slxlabel & ": " & Me.m_SLXDisplayName & vbCrLf &
            slylabel & ": " & Me.m_SLYDisplayName

    End Sub

End Class