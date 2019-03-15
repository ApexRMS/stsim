// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.IO;
using System.Windows.Forms;
using SyncroSim.Core;
using SyncroSim.Core.Forms;
using SyncroSim.Common.Forms;

namespace SyncroSim.STSim
{
    internal partial class SpatialMultiplierDataFeedView
    {
        public SpatialMultiplierDataFeedView()
        {
            InitializeComponent();
        }

        private DataFeedView m_MultipliersView;
        private DataGridView m_MultipliersDataGrid;
        private delegate void DelegateNoArgs();
        private bool m_ColumnsInitialized;
        private bool m_IsEnabled = true;

        private const string BROWSE_BUTTON_TEXT = "...";
        private const int FILE_NAME_COLUMN_INDEX = 4;
        private const int BROWSE_COLUMN_INDEX = 5;

        protected override void InitializeView()
        {
            base.InitializeView();

            this.m_MultipliersView = (this.Session.CreateMultiRowDataFeedView(this.Scenario, this.ControllingScenario));
            this.m_MultipliersDataGrid = ((MultiRowDataFeedView)this.m_MultipliersView).GridControl;
            this.PanelMultipliersGrid.Controls.Add(this.m_MultipliersView);
        }

        public override void EnableView(bool enable)
        {
            if (this.PanelMultipliersGrid.Controls.Count > 0)
            {
                DataFeedView v = (DataFeedView)this.PanelMultipliersGrid.Controls[0];
                v.EnableView(enable);
            }

            this.m_IsEnabled = enable;
        }

        public override void LoadDataFeed(SyncroSim.Core.DataFeed dataFeed)
        {
            base.LoadDataFeed(dataFeed);
            this.m_MultipliersView.LoadDataFeed(dataFeed, Strings.DATASHEET_TRANSITION_SPATIAL_MULTIPLIER_NAME);

            if (!this.m_ColumnsInitialized)
            {
                //Add handlers
                this.m_MultipliersDataGrid.CellEnter += this.OnGridCellEnter;
                this.m_MultipliersDataGrid.CellMouseDown += this.OnGridCellMouseDown;
                this.m_MultipliersDataGrid.DataBindingComplete += this.OnGridBindingComplete;
                this.m_MultipliersDataGrid.KeyDown += this.OnGridKeyDown;

                //Configure columns
                this.m_MultipliersDataGrid.Columns[FILE_NAME_COLUMN_INDEX].DefaultCellStyle.BackColor = Constants.READONLY_COLUMN_COLOR;

                //Add the browse button column
                DataGridViewButtonColumn BrowseColumn = new DataGridViewButtonColumn();

                BrowseColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                BrowseColumn.Width = 40;
                BrowseColumn.MinimumWidth = 40;

                this.m_MultipliersDataGrid.Columns.Add(BrowseColumn);

                this.m_ColumnsInitialized = true;
            }
        }

        private void OnGridCellEnter(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == FILE_NAME_COLUMN_INDEX)
            {
                this.Session.MainForm.BeginInvoke(new DelegateNoArgs(this.OnNewCellEnterAsync), null);
            }
        }

        private void OnGridCellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == BROWSE_COLUMN_INDEX)
                {
                    this.GetMultiplierFile(e.RowIndex);
                }
            }
        }

        private void OnGridKeyDown(object sender, KeyEventArgs e)
        {
            if (this.m_MultipliersDataGrid.CurrentCell != null)
            {
                if (this.m_MultipliersDataGrid.CurrentCell.ColumnIndex == BROWSE_COLUMN_INDEX)
                {
                    if (e.KeyValue == (System.Int32)Keys.Enter)
                    {
                        this.GetMultiplierFile(this.m_MultipliersDataGrid.CurrentCell.RowIndex);
                        e.Handled = true;
                    }
                }
            }
        }

        private void OnGridBindingComplete(object sender, System.Windows.Forms.DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow dgr in this.m_MultipliersDataGrid.Rows)
            {
                dgr.Cells[BROWSE_COLUMN_INDEX].Value = BROWSE_BUTTON_TEXT;
            }
        }

        private void GetMultiplierFile(int rowIndex)
        {
            if (!this.m_IsEnabled)
            {
                return;
            }

            DataSheet ds = this.Scenario.GetDataSheet(Strings.DATASHEET_TRANSITION_SPATIAL_MULTIPLIER_NAME);
            string RasterFile = RasterUtilities.ChooseRasterFileName("Transition Spatial Mulitplier Raster File", this);

            if (RasterFile == null)
            {
                return;
            }

            using (HourGlass h = new HourGlass())
            {
                DataGridViewEditMode OldMode = this.m_MultipliersDataGrid.EditMode;

                this.m_MultipliersDataGrid.EditMode = DataGridViewEditMode.EditProgrammatically;
                this.m_MultipliersDataGrid.CurrentCell = this.m_MultipliersDataGrid.Rows[rowIndex].Cells[FILE_NAME_COLUMN_INDEX];
                this.m_MultipliersDataGrid.Rows[rowIndex].Cells[FILE_NAME_COLUMN_INDEX].Value = Path.GetFileName(RasterFile);
                this.m_MultipliersDataGrid.NotifyCurrentCellDirty(true);
                this.m_MultipliersDataGrid.BeginEdit(false);
                this.m_MultipliersDataGrid.EndEdit();
                this.m_MultipliersDataGrid.CurrentCell = this.m_MultipliersDataGrid.Rows[rowIndex].Cells[BROWSE_COLUMN_INDEX];

                ds.AddExternalInputFile(RasterFile);
                this.m_MultipliersDataGrid.EditMode = OldMode;
            }
        }

        private void OnNewCellEnterAsync()
        {
            int Row = this.m_MultipliersDataGrid.CurrentCell.RowIndex;
            int Col = this.m_MultipliersDataGrid.CurrentCell.ColumnIndex;

            if (Col == FILE_NAME_COLUMN_INDEX)
            {
                if (ModifierKeys == Keys.Shift)
                {
                    Col -= 1;

                    while (!(this.m_MultipliersDataGrid.Columns[Col].Visible))
                    {
                        Col -= 1;
                    }
                }
                else
                {
                    Col += 1;
                }

                this.m_MultipliersDataGrid.CurrentCell = this.m_MultipliersDataGrid.Rows[Row].Cells[Col];
            }
        }
    }
}
