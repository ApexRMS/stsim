// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using SyncroSim.Core;
using SyncroSim.Core.Forms;
using System.Diagnostics;
using System.Data;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    internal partial class DistributionValueDataFeedView
    {
        public DistributionValueDataFeedView()
        {
            InitializeComponent();
        }

        private BaseDataGridView m_Grid;
        private int? m_TypeId;

        protected override void InitializeView()
        {
            base.InitializeView();

            DataFeedView v = this.Session.CreateMultiRowDataFeedView(this.Scenario, this.ControllingScenario);
            this.PanelMain.Controls.Add(v);

            DataSheet ds = this.Project.GetDataSheet(Strings.DISTRIBUTION_TYPE_DATASHEET_NAME);

            try
            {
                this.m_TypeId = ds.ValidationTable.GetValue(Strings.DISTRIBUTION_TYPE_NAME_UNIFORM_INTEGER);
            }
            catch (Exception)
            {
                Debug.Assert(false);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.IsDisposed)
            {
                if (this.m_Grid != null)
                {
                    this.m_Grid.CellBeginEdit -= OnGridCellBeginEdit;
                    this.m_Grid.CellEndEdit -= OnGridCellEndEdit;
                    this.m_Grid.CellFormatting -= OnGridCellFormatting;
                    this.m_Grid.CellValueChanged -= OnGridCellValueChanged;
                }

                if (components != null)
                {
                    components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        public override void LoadDataFeed(Core.DataFeed dataFeed)
        {
            base.LoadDataFeed(dataFeed);

            MultiRowDataFeedView v = (MultiRowDataFeedView)this.PanelMain.Controls[0];
            this.m_Grid = v.GridControl;
            v.LoadDataFeed(dataFeed, Strings.DISTRIBUTION_VALUE_DATASHEET_NAME);

            this.m_Grid.CellBeginEdit += OnGridCellBeginEdit;
            this.m_Grid.CellEndEdit += OnGridCellEndEdit;
            this.m_Grid.CellFormatting += OnGridCellFormatting;
            this.m_Grid.CellValueChanged += OnGridCellValueChanged;
        }

        public override void EnableView(bool enable)
        {
            if (this.PanelMain.Controls.Count > 0)
            {
                DataFeedView v = (DataFeedView)this.PanelMain.Controls[0];
                v.EnableView(enable);
            }
        }

        private void OnGridCellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == this.m_Grid.Columns[Strings.DISTRIBUTION_VALUE_DIST_TYPE_ID_COLUMN_NAME].Index)
            {
                DataGridViewRow dgv = this.m_Grid.Rows[e.RowIndex];
                DataSheet ds = this.Project.GetDataSheet(Strings.DISTRIBUTION_TYPE_DATASHEET_NAME);
                string filter = CreateUserDistributionTypeFilter(this.Project);
                DataView dv = new DataView(ds.GetData(), filter, ds.DisplayMember, DataViewRowState.CurrentRows);
                DataGridViewComboBoxCell Cell = (DataGridViewComboBoxCell)dgv.Cells[Strings.DISTRIBUTION_VALUE_DIST_TYPE_ID_COLUMN_NAME];

                Cell.DataSource = dv;
                Cell.ValueMember = ds.ValueMember;
                Cell.DisplayMember = ds.DisplayMember;
            }
            else if (e.ColumnIndex == this.m_Grid.Columns[Strings.DISTRIBUTION_VALUE_VALUE_DIST_TYPE_ID_COLUMN_NAME].Index)
            {
                DataGridViewRow dgv = this.m_Grid.Rows[e.RowIndex];
                DataSheet ds = this.Project.GetDataSheet(Strings.DISTRIBUTION_TYPE_DATASHEET_NAME);
                string filter = CreateKnownDistributionTypeFilter(this.Project);
                DataView dv = new DataView(ds.GetData(), filter, ds.DisplayMember, DataViewRowState.CurrentRows);
                DataGridViewComboBoxCell Cell = (DataGridViewComboBoxCell)dgv.Cells[Strings.DISTRIBUTION_VALUE_VALUE_DIST_TYPE_ID_COLUMN_NAME];

                Cell.DataSource = dv;
                Cell.ValueMember = ds.ValueMember;
                Cell.DisplayMember = ds.DisplayMember;
            }
        }

        private void OnGridCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.m_Grid.Columns[Strings.DISTRIBUTION_VALUE_DIST_TYPE_ID_COLUMN_NAME].Index)
            {
                DataGridViewRow dgv = this.m_Grid.Rows[e.RowIndex];
                DataGridViewComboBoxCell Cell = (DataGridViewComboBoxCell)dgv.Cells[Strings.DISTRIBUTION_VALUE_DIST_TYPE_ID_COLUMN_NAME];
                DataGridViewComboBoxColumn Column = (DataGridViewComboBoxColumn)this.m_Grid.Columns[Strings.DISTRIBUTION_VALUE_DIST_TYPE_ID_COLUMN_NAME];

                Cell.DataSource = Column.DataSource;
                Cell.ValueMember = Column.ValueMember;
                Cell.DisplayMember = Column.DisplayMember;
            }
            else if (e.ColumnIndex == this.m_Grid.Columns[Strings.DISTRIBUTION_VALUE_VALUE_DIST_TYPE_ID_COLUMN_NAME].Index)
            {
                DataGridViewRow dgv = this.m_Grid.Rows[e.RowIndex];
                DataGridViewComboBoxCell Cell = (DataGridViewComboBoxCell)dgv.Cells[Strings.DISTRIBUTION_VALUE_DIST_TYPE_ID_COLUMN_NAME];
                DataGridViewComboBoxColumn Column = (DataGridViewComboBoxColumn)this.m_Grid.Columns[Strings.DISTRIBUTION_VALUE_VALUE_DIST_TYPE_ID_COLUMN_NAME];

                Cell.DataSource = Column.DataSource;
                Cell.ValueMember = Column.ValueMember;
                Cell.DisplayMember = Column.DisplayMember;
            }
        }

        private void OnGridCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            //Is it an index we want?

            if (e.ColumnIndex != this.m_Grid.Columns[Strings.DISTRIBUTION_VALUE_VALUE_DIST_MIN_COLUMN_NAME].Index && 
                e.ColumnIndex != this.m_Grid.Columns[Strings.DISTRIBUTION_VALUE_VALUE_DIST_MAX_COLUMN_NAME].Index)
            {
                return;
            }

            //Set the default
            e.CellStyle.Format = "0.0000";

            //Does the cell have a value?

            DataGridViewCell TargetCell = this.m_Grid.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (TargetCell.Value == null || TargetCell.Value == DBNull.Value)
            {
                return;
            }

            //Is the distribution type 'Uniform Integer'?

            DataGridViewCell TypeCell = this.m_Grid.Rows[e.RowIndex].Cells[Strings.DISTRIBUTION_VALUE_VALUE_DIST_TYPE_ID_COLUMN_NAME];

            if (TypeCell.Value == null || TypeCell.Value == DBNull.Value || (!this.m_TypeId.HasValue))
            {
                return;
            }

            int ValTypeId = Convert.ToInt32(TypeCell.Value, CultureInfo.InvariantCulture);

            if (ValTypeId != this.m_TypeId.Value)
            {
                return;
            }

            e.CellStyle.Format = "0";
        }

        private void OnGridCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.m_Grid.Columns[Strings.DISTRIBUTION_VALUE_VALUE_DIST_TYPE_ID_COLUMN_NAME].Index)
            {
                this.m_Grid.InvalidateRow(e.RowIndex);
            }
        }


        private static string CreateKnownDistributionTypeFilter(Project project)
        {
            List<int> ids = GetInternalDistributionTypeIds(project);
            Debug.Assert(ids.Count > 0);

            if (ids.Count == 0)
            {
                return null;
            }
            else
            {
                return string.Format(CultureInfo.InvariantCulture, "DistributionTypeID IN ({0})", CreateIntegerFilterSpec(ids));
            }
        }

        private static string CreateUserDistributionTypeFilter(Project project)
        {
            List<int> ids = GetInternalDistributionTypeIds(project);

            if (ids.Count == 0)
            {
                return null;
            }
            else
            {
                return string.Format(CultureInfo.InvariantCulture, 
                    "DistributionTypeID NOT IN ({0})", 
                    CreateIntegerFilterSpec(ids));
            }
        }

        private static List<int> GetInternalDistributionTypeIds(Project project)
        {
            List<int> ids = new List<int>();
            DataSheet ds = project.GetDataSheet(Strings.DISTRIBUTION_TYPE_DATASHEET_NAME);

            foreach (DataRow dr in ds.GetData().Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    if (Booleans.BoolFromValue(dr[Strings.DISTRIBUTION_TYPE_IS_INTERNAL_COLUMN_NAME]))
                    {
                        ids.Add(Convert.ToInt32(dr[ds.PrimaryKeyColumn.Name], CultureInfo.InvariantCulture));
                    }
                }
            }

            return ids;
        }

        private static string CreateIntegerFilterSpec(List<int> ids)
        {
            StringBuilder sb = new StringBuilder();

            foreach (int i in ids)
            {
                sb.Append(i.ToString(CultureInfo.InvariantCulture));
                sb.Append(",");
            }

            return sb.ToString().TrimEnd(',');
        }
    }
}
