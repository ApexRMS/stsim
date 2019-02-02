// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace SyncroSim.STSim
{
    internal static class ColorColumns
    {
        public static void ColorPaintGridCell(DataGridView gridView, System.Windows.Forms.DataGridViewCellPaintingEventArgs e)
        {
            Color clr = Color.White;
            DataGridViewTextBoxCell cell = (DataGridViewTextBoxCell)(gridView.Rows[e.RowIndex].Cells[e.ColumnIndex]);

            if (cell.Value != null && cell.Value != DBNull.Value)
            {
                clr = ColorUtilities.ColorFromString(Convert.ToString(cell.Value, CultureInfo.InvariantCulture));
            }

            if (gridView.Rows[e.RowIndex].Selected)
            {
                clr = gridView.DefaultCellStyle.SelectionBackColor;
            }

            using (SolidBrush b = new SolidBrush(clr))
            {
                e.Graphics.FillRectangle(b, e.CellBounds);
            }

            using (Pen p = new Pen(gridView.GridColor))
            {
                e.Graphics.DrawLine(p, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                e.Graphics.DrawLine(p, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);

                if (cell == gridView.CurrentCell)
                {
                    using (Pen p2 = new Pen(Color.Black))
                    {
                        p2.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                        e.Graphics.DrawLine(p2, e.CellBounds.Left + 2, e.CellBounds.Top + 2, e.CellBounds.Right - 4, e.CellBounds.Top + 2);
                        e.Graphics.DrawLine(p2, e.CellBounds.Right - 4, e.CellBounds.Top + 2, e.CellBounds.Right - 4, e.CellBounds.Bottom - 4);
                        e.Graphics.DrawLine(p2, e.CellBounds.Right - 4, e.CellBounds.Bottom - 4, e.CellBounds.Left + 2, e.CellBounds.Bottom - 4);
                        e.Graphics.DrawLine(p2, e.CellBounds.Left + 2, e.CellBounds.Bottom - 4, e.CellBounds.Left + 2, e.CellBounds.Top + 2);
                    }
                }
            }

            e.Handled = true;
        }

        public static void AssignGridViewColor(DataGridView gridView, int rowIndex, int columnIndex)
        {
            Color clr = Color.White;
            DataGridViewTextBoxCell cell = (DataGridViewTextBoxCell)(gridView.Rows[rowIndex].Cells[columnIndex]);

            if (cell.Value != null && cell.Value != DBNull.Value)
            {
                clr = ColorUtilities.ColorFromString(Convert.ToString(cell.Value, CultureInfo.InvariantCulture));
            }

            ColorDialog cd = new ColorDialog();
            cd.Color = clr;

            if (cd.ShowDialog(gridView) == DialogResult.OK)
            {
                gridView.BeginEdit(false);
                cell.Value = ColorUtilities.StringFromColor(cd.Color);
                gridView.EndEdit();

                gridView.InvalidateCell(cell);
            }
        }
    }
}
