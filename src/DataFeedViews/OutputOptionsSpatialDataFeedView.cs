// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    internal partial class OutputOptionsSpatialDataFeedView
    {
        private const string DEFAULT_TIMESTEP_VALUE = "1";

        public OutputOptionsSpatialDataFeedView()
        {
            InitializeComponent();
        }

        public override void LoadDataFeed(DataFeed dataFeed)
        {
            base.LoadDataFeed(dataFeed);

            this.SetCheckBoxBinding(this.CheckBoxRasterSC, Strings.DATASHEET_OO_SPATIAL_RASTER_OUTPUT_SC_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxRasterSCTimesteps, Strings.DATASHEET_OO_SPATIAL_RASTER_OUTPUT_SC_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxRasterTR, Strings.DATASHEET_OO_SPATIAL_RASTER_OUTPUT_TR_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxRasterTRTimesteps, Strings.DATASHEET_OO_SPATIAL_RASTER_OUTPUT_TR_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxRasterAge, Strings.DATASHEET_OO_SPATIAL_RASTER_OUTPUT_AGE_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxRasterAgeTimesteps, Strings.DATASHEET_OO_SPATIAL_RASTER_OUTPUT_AGE_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxRasterTST, Strings.DATASHEET_OO_SPATIAL_RASTER_OUTPUT_TST_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxRasterTSTTimesteps, Strings.DATASHEET_OO_SPATIAL_RASTER_OUTPUT_TST_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxRasterST, Strings.DATASHEET_OO_SPATIAL_RASTER_OUTPUT_ST_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxRasterSTTimesteps, Strings.DATASHEET_OO_SPATIAL_RASTER_OUTPUT_ST_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxRasterSA, Strings.DATASHEET_OO_SPATIAL_RASTER_OUTPUT_SA_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxRasterSATimesteps, Strings.DATASHEET_OO_SPATIAL_RASTER_OUTPUT_SA_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxRasterTA, Strings.DATASHEET_OO_SPATIAL_RASTER_OUTPUT_TA_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxRasterTATimesteps, Strings.DATASHEET_OO_SPATIAL_RASTER_OUTPUT_TA_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxRasterTE, Strings.DATASHEET_OO_SPATIAL_RASTER_OUTPUT_TE_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxRasterTETimesteps, Strings.DATASHEET_OO_SPATIAL_RASTER_OUTPUT_TE_TIMESTEPS_COLUMN_NAME);

            this.MonitorDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME, this.OnTerminologyChanged, true);
            this.AddStandardCommands();
        }

        public override void EnableView(bool enable)
        {
            base.EnableView(enable);

            if (enable)
            {
                this.EnableControls();
            }
        }

        protected override void OnRowsAdded(object sender, DataSheetRowEventArgs e)
        {
            base.OnRowsAdded(sender, e);

            if (this.ShouldEnableView())
            {
                this.EnableControls();
            }
        }

        protected override void OnRowsDeleted(object sender, DataSheetRowEventArgs e)
        {
            base.OnRowsDeleted(sender, e);

            if (this.ShouldEnableView())
            {
                this.EnableControls();
            }
        }

        private void OnTerminologyChanged(DataSheetMonitorEventArgs e)
        {
            string NewTimestepsText = Convert.ToString(
                e.GetValue("TimestepUnits", "Timestep"), 
                CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture);

            this.LabelRasterSCTimesteps.Text = NewTimestepsText;
            this.LabelRasterTRTimesteps.Text = NewTimestepsText;
            this.LabelRasterAgeTimesteps.Text = NewTimestepsText;
            this.LabelRasterTSTTimesteps.Text = NewTimestepsText;
            this.LabelRasterSTTimesteps.Text = NewTimestepsText;
            this.LabelRasterSATimesteps.Text = NewTimestepsText;
            this.LabelRasterTATimesteps.Text = NewTimestepsText;
            this.LabelRasterTETimesteps.Text = NewTimestepsText;
        }

        private void EnableControls()
        {
            //Text Boxes
            this.TextBoxRasterSCTimesteps.Enabled = this.CheckBoxRasterSC.Checked;
            this.TextBoxRasterTRTimesteps.Enabled = this.CheckBoxRasterTR.Checked;
            this.TextBoxRasterAgeTimesteps.Enabled = this.CheckBoxRasterAge.Checked;
            this.TextBoxRasterTSTTimesteps.Enabled = this.CheckBoxRasterTST.Checked;
            this.TextBoxRasterSTTimesteps.Enabled = this.CheckBoxRasterST.Checked;
            this.TextBoxRasterSATimesteps.Enabled = this.CheckBoxRasterSA.Checked;
            this.TextBoxRasterTATimesteps.Enabled = this.CheckBoxRasterTA.Checked;
            this.TextBoxRasterTETimesteps.Enabled = this.CheckBoxRasterTE.Checked;

            //Timesteps labels
            this.LabelRasterSCTimesteps.Enabled = this.CheckBoxRasterSC.Checked;
            this.LabelRasterTRTimesteps.Enabled = this.CheckBoxRasterTR.Checked;
            this.LabelRasterAgeTimesteps.Enabled = this.CheckBoxRasterAge.Checked;
            this.LabelRasterTSTTimesteps.Enabled = this.CheckBoxRasterTST.Checked;
            this.LabelRasterSTTimesteps.Enabled = this.CheckBoxRasterST.Checked;
            this.LabelRasterSATimesteps.Enabled = this.CheckBoxRasterSA.Checked;
            this.LabelRasterTATimesteps.Enabled = this.CheckBoxRasterTA.Checked;
            this.LabelRasterTETimesteps.Enabled = this.CheckBoxRasterTE.Checked;
        }

        protected override void OnBoundCheckBoxChanged(CheckBox checkBox, string columnName)
        {
            base.OnBoundCheckBoxChanged(checkBox, columnName);

            this.SetDefaultTimestepsIfCondition(checkBox, this.CheckBoxRasterSC, this.TextBoxRasterSCTimesteps);
            this.SetDefaultTimestepsIfCondition(checkBox, this.CheckBoxRasterTR, this.TextBoxRasterTRTimesteps);
            this.SetDefaultTimestepsIfCondition(checkBox, this.CheckBoxRasterAge, this.TextBoxRasterAgeTimesteps);
            this.SetDefaultTimestepsIfCondition(checkBox, this.CheckBoxRasterTST, this.TextBoxRasterTSTTimesteps);
            this.SetDefaultTimestepsIfCondition(checkBox, this.CheckBoxRasterST, this.TextBoxRasterSTTimesteps);
            this.SetDefaultTimestepsIfCondition(checkBox, this.CheckBoxRasterSA, this.TextBoxRasterSATimesteps);
            this.SetDefaultTimestepsIfCondition(checkBox, this.CheckBoxRasterTA, this.TextBoxRasterTATimesteps);
            this.SetDefaultTimestepsIfCondition(checkBox, this.CheckBoxRasterTE, this.TextBoxRasterTETimesteps);

            this.EnableControls();
        }

        private void SetDefaultTimestepsIfCondition(CheckBox cbSender, CheckBox cbCompare, TextBox tb)
        {
            if (cbSender == cbCompare && cbCompare.Checked)
            {
                if (string.IsNullOrEmpty(tb.Text))
                {
                    this.SetTextBoxData(tb, DEFAULT_TIMESTEP_VALUE);
                }
            }
        }

        private void SetCheckBoxValueIfCondition(CheckBox cbSender, CheckBox cbCompare, CheckBox cbTarget, string columnName)
        {
            if (cbSender == cbCompare && cbCompare.Checked)
            {
                if (!cbTarget.Checked)
                {
                    DataRow dr = this.DataFeed.GetDataSheet(Strings.DATASHEET_OO_SPATIAL_NAME).GetDataRow();
                    dr[columnName] = Booleans.BoolToInt(true);
                    cbTarget.Checked = true;
                }
            }
        }
    }
}
