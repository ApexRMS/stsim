// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System.Windows.Forms;
using SyncroSim.Core.Forms;

namespace SyncroSim.STSim
{
    internal partial class StateClassDataFeedView
    {
        public StateClassDataFeedView()
        {
            InitializeComponent();
        }

        private DataGridView m_Grid;
        private MultiRowDataFeedView m_View;

        protected override void InitializeView()
        {
            base.InitializeView();

            this.m_View = this.Session.CreateMultiRowDataFeedView(this.Project);
            this.m_Grid = this.m_View.GridControl;

            this.Controls.Add(this.m_View);

            this.m_Grid.CellDoubleClick += this.OnGridCellDoubleClick;
            this.m_Grid.CellPainting += this.OnGridCellPainting;
            this.m_Grid.KeyDown += this.OnGridKeyDown;
        }

        public override void LoadDataFeed(SyncroSim.Core.DataFeed dataFeed)
        {
            base.LoadDataFeed(dataFeed);
            this.m_View.LoadDataFeed(dataFeed);
        }

        public override void EnableView(bool enable)
        {
            base.EnableView(enable);
            this.m_View.EnableView(enable);
        }

        private void OnGridCellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            if (this.m_Grid.Columns[e.ColumnIndex].Name == Strings.DATASHEET_COLOR_COLUMN_NAME)
            {
                ColorColumns.AssignGridViewColor(this.m_Grid, e.RowIndex, e.ColumnIndex);
            }
        }

        private void OnGridCellPainting(object sender, System.Windows.Forms.DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            if (this.m_Grid.Columns[e.ColumnIndex].Name == Strings.DATASHEET_COLOR_COLUMN_NAME)
            {
                ColorColumns.ColorPaintGridCell(this.m_Grid, e);
            }
        }

        private void OnGridKeyDown(object sender, KeyEventArgs e)
        {
            if (this.m_Grid.CurrentCell == null)
            {
                return;
            }

            if (this.m_Grid.Columns[this.m_Grid.CurrentCell.ColumnIndex].Name == Strings.DATASHEET_COLOR_COLUMN_NAME)
            {
                if (e.KeyCode == Keys.Delete)
                {
                    this.m_Grid.BeginEdit(false);
                    this.m_Grid.CurrentCell.Value = null;
                    this.m_Grid.EndEdit();

                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    ColorColumns.AssignGridViewColor(this.m_Grid, this.m_Grid.CurrentCell.RowIndex, this.m_Grid.CurrentCell.ColumnIndex);
                    e.Handled = true;
                }
            }
        }
    }
}
