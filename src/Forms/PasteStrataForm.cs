// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Windows.Forms;
using System.Globalization;
using System.Collections.Generic;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    internal partial class PasteStrataForm
    {
        public PasteStrataForm()
        {
            InitializeComponent();
        }

        private Dictionary<string, bool> m_SelectedStrata;

        public Dictionary<string, bool> SelectedStrata
        {
            get
            {
                return this.m_SelectedStrata;
            }
        }

        public bool MergeDependencies
        {
            get
            {
                return this.CheckBoxMergeDeps.Checked;
            }
        }

        public void Initialize(Project project, bool enableMergeDeps)
        {
            this.DataGridViewStrata.BackgroundColor = System.Drawing.Color.White;
            this.DataGridViewStrata.PaintGridBorders = false;
            this.DataGridViewStrata.MultiSelect = true;
            this.DataGridViewStrata.StandardTab = true;
            this.PanelGrid.ShowBorder = true;
            this.CheckBoxMergeDeps.Enabled = enableMergeDeps;

            DataSheet ds = project.GetDataSheet(Strings.DATASHEET_STRATA_NAME);
            DataView dv = new DataView(ds.GetData(), null, ds.DisplayMember, DataViewRowState.CurrentRows);
            bool AtLeastOneDesc = false;

            foreach (DataRowView v in dv)
            {
                string n = Convert.ToString(v[ds.DisplayMember], CultureInfo.InvariantCulture);
                string d = DataTableUtilities.GetDataStr(v[Strings.DATASHEET_DESCRIPTION_COLUMN_NAME]);

                if (!string.IsNullOrEmpty(d))
                {
                    AtLeastOneDesc = true;
                }

                this.DataGridViewStrata.Rows.Add(n, d);
            }

            this.ButtonOK.Enabled = (this.DataGridViewStrata.Rows.Count > 0);
            this.DataGridViewStrata.Enabled = (this.DataGridViewStrata.Rows.Count > 0);

            if (!AtLeastOneDesc)
            {
                this.ColumnDescription.Visible = false;
            }
        }

        private void SelectStratumAndExit()
        {
            this.m_SelectedStrata = new Dictionary<string, bool>();

            foreach (DataGridViewRow dgr in this.DataGridViewStrata.SelectedRows)
            {
                SelectedStrata.Add(Convert.ToString(dgr.Cells[ColumnName.Name].Value, CultureInfo.InvariantCulture), true);
            }

            this.DialogResult = DialogResult.OK;
        }

        private void SelectAllStrataAndFocusGrid()
        {
            this.DataGridViewStrata.SelectAll();
            this.ActiveControl = this.DataGridViewStrata;
        }

        private void ButtonSelectAllStrata_Click(object sender, System.EventArgs e)
        {
            this.SelectAllStrataAndFocusGrid();
        }

        private void ButtonOK_Click(object sender, System.EventArgs e)
        {
            this.SelectStratumAndExit();
        }

        private void PasteStrataForm_Shown(object sender, System.EventArgs e)
        {
            this.SelectAllStrataAndFocusGrid();
        }

        private void DataGridViewStrata_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                this.SelectStratumAndExit();
                e.Handled = true;
            }
        }
    }
}
