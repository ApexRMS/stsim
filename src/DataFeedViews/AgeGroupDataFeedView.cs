// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using SyncroSim.Core.Forms;
using System.Windows.Forms;

namespace SyncroSim.STSim
{
    internal partial class AgeGroupDataFeedView
    {
        private BaseDataGridView m_Grid;
        private MultiRowDataFeedView m_View;

        public AgeGroupDataFeedView()
        {
            InitializeComponent();
        }

        protected override void InitializeView()
        {
            base.InitializeView();

            this.m_View = this.Session.CreateMultiRowDataFeedView(this.Project);
            this.m_Grid = this.m_View.GridControl;

            this.Controls.Add(this.m_View);

            this.m_Grid.CellValidating += this.OnCellValidating;
            this.m_Grid.CellDoubleClick += this.OnGridCellDoubleClick;
            this.m_Grid.CellPainting += this.OnGridCellPainting;
            this.m_Grid.KeyDown += this.OnGridKeyDown;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.IsDisposed)
            {
                this.m_Grid.CellValidating -= this.OnCellValidating;
                this.m_Grid.CellDoubleClick -= this.OnGridCellDoubleClick;
                this.m_Grid.CellPainting -= this.OnGridCellPainting;
                this.m_Grid.KeyDown -= this.OnGridKeyDown;

                if (components != null)
                {
                    components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        public override void LoadDataFeed(DataFeed dataFeed)
        {
            base.LoadDataFeed(dataFeed);
            this.m_View.LoadDataFeed(dataFeed);
        }

        private void OnCellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.Cancel == true)
            {
                return;
            }

            if (!this.m_Grid.IsCurrentCellDirty)
            {
                return;
            }

            DataGridViewColumn c = this.m_Grid.Columns[e.ColumnIndex];

            if (c.Name != Strings.DATASHEET_AGE_GROUP_MAXIMUM_COLUMN_NAME)
            {
                return;
            }

            if (!AgeUtilities.HasAgeClassUpdateTag(this.Project))
            {
                if (MessageBox.Show(MessageStrings.PROMPT_AGE_GROUP_CHANGE, "Age Group", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    this.m_Grid.CancelEdit();
                    this.ActiveControl = this.m_Grid;

                    e.Cancel = true;
                }
            }
        }

        private void OnGridCellDoubleClick(object sender, DataGridViewCellEventArgs e)
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

        private void OnGridCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
                    ColorColumns.AssignGridViewColor(
                        this.m_Grid, 
                        this.m_Grid.CurrentCell.RowIndex, 
                        this.m_Grid.CurrentCell.ColumnIndex);

                    e.Handled = true;
                }
            }
        }
    }
}
