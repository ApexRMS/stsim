// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using SyncroSim.Core;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;

namespace SyncroSim.STSim
{
    internal partial class SelectStratumForm
    {
        public SelectStratumForm()
        {
            InitializeComponent();
        }

        private string m_SelectedStratum;

        public string SelectedStratum
        {
            get
            {
                return this.m_SelectedStratum;
            }
        }

        public void Initialize(Project project, string selectedStratum)
        {
            this.DataGridViewStrata.BackgroundColor = System.Drawing.Color.White;
            this.DataGridViewStrata.PaintSelectionRectangle = false;
            this.DataGridViewStrata.PaintGridBorders = false;
            this.PanelGrid.ShowBorder = true;

            DataSheet ds = project.GetDataSheet(Strings.DATASHEET_STRATA_NAME);
            DataView dv = new DataView(ds.GetData(), null, ds.DisplayMember, DataViewRowState.CurrentRows);
            bool AtLeastOneDesc = false;

            this.DataGridViewStrata.Rows.Add(Strings.DIAGRAM_ALL_STRATA_DISPLAY_NAME, null);

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
            this.m_SelectedStratum = selectedStratum;

            if (!AtLeastOneDesc)
            {
                this.ColumnDescription.Visible = false;
            }

            this.RefreshTitleBar(project);
        }

        private void RefreshTitleBar(Project project)
        {
            string primary = null;
            string secondary = null;
            string tertiary = null;
            DataSheet ds = project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME);

            TerminologyUtilities.GetStratumLabelTerminology(ds, ref primary, ref secondary, ref tertiary);
            this.Text = "Select " + primary;
        }

        private void SelectStratumAndExit()
        {
            Debug.Assert(this.DataGridViewStrata.SelectedRows.Count == 1);

            this.DialogResult = DialogResult.OK;

            this.m_SelectedStratum = Convert.ToString(
                this.DataGridViewStrata.SelectedRows[0].Cells[ColumnName.Name].Value, 
                CultureInfo.InvariantCulture);
        }

        private void ButtonOK_Click(object sender, System.EventArgs e)
        {
            this.SelectStratumAndExit();
        }

        private void DataGridViewStrata_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                this.SelectStratumAndExit();
            }
        }

        private void DataGridViewStrata_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                this.SelectStratumAndExit();
                e.Handled = true;
            }
        }

        private void SelectStratumForm_Shown(object sender, System.EventArgs e)
        {
            this.ActiveControl = this.DataGridViewStrata;
            this.DataGridViewStrata.Focus();
            this.DataGridViewStrata.StandardTab = true;
            this.DataGridViewStrata.ClearSelection();

            foreach (DataGridViewRow dgr in this.DataGridViewStrata.Rows)
            {
                if (Convert.ToString(dgr.Cells[this.ColumnName.Name].Value, CultureInfo.InvariantCulture) == this.m_SelectedStratum)
                {
                    dgr.Selected = true;
                    this.DataGridViewStrata.CurrentCell = dgr.Cells[0];

                    break;
                }
            }
        }
    }
}
