'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Imports System.Reflection
Imports System.Windows.Forms
Imports SyncroSim.Core.Forms

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class AgeGroupDataFeedView

    Private m_Grid As DataGridView
    Private m_View As MultiRowDataFeedView

    Protected Overrides Sub InitializeView()

        MyBase.InitializeView()

        Me.m_View = Me.Session.CreateMultiRowDataFeedView(Me.Project)
        Me.m_Grid = Me.m_View.GridControl()

        Me.Controls.Add(Me.m_View)

        AddHandler Me.m_Grid.CellDoubleClick, AddressOf Me.OnGridCellDoubleClick
        AddHandler Me.m_Grid.CellPainting, AddressOf Me.OnGridCellPainting
        AddHandler Me.m_Grid.KeyDown, AddressOf Me.OnGridKeyDown

    End Sub

    Protected Overrides Sub Dispose(disposing As Boolean)

        If (disposing And Not Me.IsDisposed) Then

            RemoveHandler Me.m_Grid.CellDoubleClick, AddressOf Me.OnGridCellDoubleClick
            RemoveHandler Me.m_Grid.CellPainting, AddressOf Me.OnGridCellPainting
            RemoveHandler Me.m_Grid.KeyDown, AddressOf Me.OnGridKeyDown

            If components IsNot Nothing Then
                components.Dispose()
            End If

        End If

        MyBase.Dispose(disposing)

    End Sub

    Public Overrides Sub LoadDataFeed(dataFeed As SyncroSim.Core.DataFeed)

        MyBase.LoadDataFeed(dataFeed)
        Me.m_View.LoadDataFeed(dataFeed)

    End Sub

    Private Sub OnGridCellDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs)

        If (e.RowIndex < 0 Or e.ColumnIndex < 0) Then
            Return
        End If

        If (Me.m_Grid.Columns(e.ColumnIndex).Name = DATASHEET_COLOR_COLUMN_NAME) Then
            AssignGridViewColor(Me.m_Grid, e.RowIndex, e.ColumnIndex)
        End If

    End Sub

    Private Sub OnGridCellPainting(sender As System.Object, e As System.Windows.Forms.DataGridViewCellPaintingEventArgs)

        If (e.RowIndex < 0 Or e.ColumnIndex < 0) Then
            Return
        End If

        If (Me.m_Grid.Columns(e.ColumnIndex).Name = DATASHEET_COLOR_COLUMN_NAME) Then
            ColorPaintGridCell(Me.m_Grid, e)
        End If

    End Sub

    Private Sub OnGridKeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)

        If (Me.m_Grid.Columns(Me.m_Grid.CurrentCell.ColumnIndex).Name = DATASHEET_COLOR_COLUMN_NAME) Then

            If (e.KeyCode = Keys.Delete) Then

                Me.m_Grid.BeginEdit(False)
                Me.m_Grid.CurrentCell.Value = Nothing
                Me.m_Grid.EndEdit()

                e.Handled = True

            ElseIf (e.KeyCode = Keys.Enter) Then

                AssignGridViewColor(Me.m_Grid, Me.m_Grid.CurrentCell.RowIndex, Me.m_Grid.CurrentCell.ColumnIndex)
                e.Handled = True

            End If

        End If

    End Sub

End Class
