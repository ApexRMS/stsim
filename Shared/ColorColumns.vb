'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports System.Drawing
Imports System.Windows.Forms

Module ColorColumns

    Public Sub ColorPaintGridCell(
        ByVal gridView As DataGridView,
        ByVal e As System.Windows.Forms.DataGridViewCellPaintingEventArgs)

        Dim clr As Color = Color.White
        Dim cell As DataGridViewTextBoxCell = CType(gridView.Rows(e.RowIndex).Cells(e.ColumnIndex), DataGridViewTextBoxCell)

        If (cell.Value IsNot Nothing And cell.Value IsNot DBNull.Value) Then
            clr = ColorUtilities.ColorFromString(CStr(cell.Value))
        End If

        If (gridView.Rows(e.RowIndex).Selected) Then
            clr = gridView.DefaultCellStyle.SelectionBackColor
        End If

        Using b As New SolidBrush(clr)
            e.Graphics.FillRectangle(b, e.CellBounds)
        End Using

        Using p As New Pen(gridView.GridColor)

            e.Graphics.DrawLine(p,
                e.CellBounds.Left,
                e.CellBounds.Bottom - 1,
                e.CellBounds.Right - 1,
                e.CellBounds.Bottom - 1)

            e.Graphics.DrawLine(p,
                e.CellBounds.Right - 1,
                e.CellBounds.Top,
                e.CellBounds.Right - 1,
                e.CellBounds.Bottom)

            If (cell Is gridView.CurrentCell) Then

                Using p2 As New Pen(Color.Black)

                    p2.DashStyle = Drawing2D.DashStyle.Dot

                    e.Graphics.DrawLine(p2,
                        e.CellBounds.Left + 2,
                        e.CellBounds.Top + 2,
                        e.CellBounds.Right - 4,
                        e.CellBounds.Top + 2)

                    e.Graphics.DrawLine(p2,
                       e.CellBounds.Right - 4,
                       e.CellBounds.Top + 2,
                       e.CellBounds.Right - 4,
                       e.CellBounds.Bottom - 4)

                    e.Graphics.DrawLine(p2,
                       e.CellBounds.Right - 4,
                       e.CellBounds.Bottom - 4,
                       e.CellBounds.Left + 2,
                       e.CellBounds.Bottom - 4)

                    e.Graphics.DrawLine(p2,
                        e.CellBounds.Left + 2,
                        e.CellBounds.Bottom - 4,
                        e.CellBounds.Left + 2,
                        e.CellBounds.Top + 2)

                End Using

            End If

        End Using

        e.Handled = True

    End Sub

    Public Sub AssignGridViewColor(ByVal gridView As DataGridView, ByVal rowIndex As Integer, ByVal columnIndex As Integer)

        Dim clr As Color = Color.White
        Dim cell As DataGridViewTextBoxCell = CType(gridView.Rows(rowIndex).Cells(columnIndex), DataGridViewTextBoxCell)

        If (cell.Value IsNot Nothing And cell.Value IsNot DBNull.Value) Then
            clr = ColorUtilities.ColorFromString(CStr(cell.Value))
        End If

        Dim cd As New ColorDialog
        cd.Color = clr

        If (cd.ShowDialog() = DialogResult.OK) Then

            gridView.BeginEdit(False)
            cell.Value = ColorUtilities.StringFromColor(cd.Color)
            gridView.EndEdit()

            gridView.InvalidateCell(cell)

        End If

    End Sub

End Module
