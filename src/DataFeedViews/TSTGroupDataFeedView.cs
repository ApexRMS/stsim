// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using SyncroSim.Core.Forms;
using System.Windows.Forms;

namespace SyncroSim.STSim
{
    internal partial class TSTGroupDataFeedView : DataFeedView
    {
        private BaseDataGridView m_Grid;
        private MultiRowDataFeedView m_View;

        public TSTGroupDataFeedView()
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
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.IsDisposed)
            {
                this.m_Grid.CellValidating -= this.OnCellValidating;

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

            if (c.Name != Strings.DATASHEET_TST_GROUP_MAXIMUM_COLUMN_NAME)
            {
                return;
            }

            if (!ProjectUtilities.ProjectHasResults(this.Project))
            {
                return;
            }

            if (!ChartingUtilities.HasTSTClassUpdateTag(this.Project))
            {
                if (MessageBox.Show(
                    MessageStrings.PROMPT_TST_GROUP_CHANGE,
                    "TST Group", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    this.m_Grid.CancelEdit();
                    this.ActiveControl = this.m_Grid;

                    e.Cancel = true;
                }
            }
        }

    }
}
