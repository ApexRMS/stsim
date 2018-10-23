﻿// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using SyncroSim.Core.Forms;
using System.Windows.Forms;
using System.Reflection;
using System.Data;

namespace SyncroSim.STSim
{
    [ObfuscationAttribute(Exclude=true, ApplyToMembers=false)]
    internal partial class TransitionTypeGroupDataFeedView
    {
        public TransitionTypeGroupDataFeedView()
        {
            InitializeComponent();
        }

        private BaseDataGridView m_Grid;

        protected override void InitializeView()
        {
            base.InitializeView();

            DataFeedView v = this.Session.CreateMultiRowDataFeedView(this.Project);
            this.PanelMain.Controls.Add(v);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !this.IsDisposed)
            {
                if (this.m_Grid != null)
                {
                    this.m_Grid.CellBeginEdit -= OnGridCellBeginEdit;
                    this.m_Grid.CellEndEdit -= OnGridCellEndEdit;
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
            v.LoadDataFeed(dataFeed, Strings.DATASHEET_TRANSITION_TYPE_GROUP_NAME);

            this.m_Grid.CellBeginEdit += OnGridCellBeginEdit;
            this.m_Grid.CellEndEdit += OnGridCellEndEdit;
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
            if (e.ColumnIndex == this.m_Grid.Columns[Strings.DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME].Index)
            {
                DataGridViewRow dgv = this.m_Grid.Rows[e.RowIndex];
                DataSheet ds = this.Project.GetDataSheet(Strings.DATASHEET_TRANSITION_GROUP_NAME);
                string filter = "IsAuto IS NULL OR IsAuto=0";
                DataView dv = new DataView(ds.GetData(), filter, ds.DisplayMember, DataViewRowState.CurrentRows);
                DataGridViewComboBoxCell Cell = (DataGridViewComboBoxCell)dgv.Cells[Strings.DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME];

                Cell.DataSource = dv;
                Cell.ValueMember = ds.ValueMember;
                Cell.DisplayMember = ds.DisplayMember;
            }
        }

        private void OnGridCellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == this.m_Grid.Columns[Strings.DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME].Index)
            {
                DataGridViewRow dgv = this.m_Grid.Rows[e.RowIndex];
                DataGridViewComboBoxCell Cell = (DataGridViewComboBoxCell)dgv.Cells[Strings.DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME];
                DataGridViewComboBoxColumn Column = (DataGridViewComboBoxColumn)this.m_Grid.Columns[Strings.DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME];

                Cell.DataSource = Column.DataSource;
                Cell.ValueMember = Column.ValueMember;
                Cell.DisplayMember = Column.DisplayMember;
            }
        }
    }
}