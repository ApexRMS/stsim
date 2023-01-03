// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    internal partial class OutputOptionsSpatialAverageDataFeedView
    {
        private const string DEFAULT_TIMESTEP_VALUE = "1";

        public OutputOptionsSpatialAverageDataFeedView()
        {
            InitializeComponent();
        }

        public override void LoadDataFeed(DataFeed dataFeed)
        {
            base.LoadDataFeed(dataFeed);

            this.SetCheckBoxBinding(this.CheckBoxAvgRasterSC, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_SC_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxAvgRasterSCTimesteps, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_SC_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxAvgRasterSCCumulative, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_SC_CUMULATIVE_COLUMN_NAME);

            this.SetCheckBoxBinding(this.CheckBoxAvgRasterAge, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_AGE_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxAvgRasterAgeTimesteps, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_AGE_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxAvgRasterAgeCumulative, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_AGE_CUMULATIVE_COLUMN_NAME);

            this.SetCheckBoxBinding(this.CheckBoxAvgRasterST, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_ST_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxAvgRasterSTTimesteps, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_ST_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxAvgRasterSTCumulative, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_ST_CUMULATIVE_COLUMN_NAME);

            this.SetCheckBoxBinding(this.CheckBoxAvgRasterTP, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_TP_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxAvgRasterTPTimesteps, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_TP_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxAvgRasterTPCumulative, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_TP_CUMULATIVE_COLUMN_NAME);

            this.SetCheckBoxBinding(this.CheckBoxAvgRasterTST, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_TST_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxAvgRasterTSTTimesteps, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_TST_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxAvgRasterTSTCumulative, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_TST_CUMULATIVE_COLUMN_NAME);

            this.SetCheckBoxBinding(this.CheckBoxAvgRasterSA, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_SA_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxAvgRasterSATimesteps, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_SA_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxAvgRasterSACumulative, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_SA_CUMULATIVE_COLUMN_NAME);

            this.SetCheckBoxBinding(this.CheckBoxAvgRasterTA, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_TA_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxAvgRasterTATimesteps, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_TA_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxAvgRasterTACumulative, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_TA_CUMULATIVE_COLUMN_NAME);

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

            this.LabelAvgRasterSTTimesteps.Text = NewTimestepsText;
            this.LabelAvgRasterSCTimesteps.Text = NewTimestepsText;
            this.LabelAvgRasterAgeTimesteps.Text = NewTimestepsText;
            this.LabelAvgRasterTPTimesteps.Text = NewTimestepsText;
            this.LabelAvgRasterTSTTimesteps.Text = NewTimestepsText;
            this.LabelAvgRasterSATimesteps.Text = NewTimestepsText;
            this.LabelAvgRasterTATimesteps.Text = NewTimestepsText;
        }

        private void EnableControls()
        {
            //Text Boxes
            this.TextBoxAvgRasterSCTimesteps.Enabled = this.CheckBoxAvgRasterSC.Checked;
            this.TextBoxAvgRasterAgeTimesteps.Enabled = this.CheckBoxAvgRasterAge.Checked;
            this.TextBoxAvgRasterSTTimesteps.Enabled = this.CheckBoxAvgRasterST.Checked;
            this.TextBoxAvgRasterTPTimesteps.Enabled = this.CheckBoxAvgRasterTP.Checked;
            this.TextBoxAvgRasterTSTTimesteps.Enabled = this.CheckBoxAvgRasterTST.Checked;
            this.TextBoxAvgRasterSATimesteps.Enabled = this.CheckBoxAvgRasterSA.Checked;
            this.TextBoxAvgRasterTATimesteps.Enabled = this.CheckBoxAvgRasterTA.Checked;

            //Timesteps labels
            this.LabelAvgRasterSCTimesteps.Enabled = this.CheckBoxAvgRasterSC.Checked;
            this.LabelAvgRasterAgeTimesteps.Enabled = this.CheckBoxAvgRasterAge.Checked;
            this.LabelAvgRasterSTTimesteps.Enabled = this.CheckBoxAvgRasterST.Checked;
            this.LabelAvgRasterTPTimesteps.Enabled = this.CheckBoxAvgRasterTP.Checked;
            this.LabelAvgRasterTSTTimesteps.Enabled = this.CheckBoxAvgRasterTST.Checked;
            this.LabelAvgRasterSATimesteps.Enabled = this.CheckBoxAvgRasterSA.Checked;
            this.LabelAvgRasterTATimesteps.Enabled = this.CheckBoxAvgRasterTA.Checked;

            //Secondary Checkboxes
            this.CheckBoxAvgRasterSCCumulative.Enabled = this.CheckBoxAvgRasterSC.Checked;
            this.CheckBoxAvgRasterAgeCumulative.Enabled = this.CheckBoxAvgRasterAge.Checked;
            this.CheckBoxAvgRasterSTCumulative.Enabled = this.CheckBoxAvgRasterST.Checked;
            this.CheckBoxAvgRasterTPCumulative.Enabled = this.CheckBoxAvgRasterTP.Checked;
            this.CheckBoxAvgRasterTSTCumulative.Enabled = this.CheckBoxAvgRasterTST.Checked;
            this.CheckBoxAvgRasterSACumulative.Enabled = this.CheckBoxAvgRasterSA.Checked;
            this.CheckBoxAvgRasterTACumulative.Enabled = this.CheckBoxAvgRasterTA.Checked;
        }

        protected override void OnBoundCheckBoxChanged(CheckBox checkBox, string columnName)
        {
            base.OnBoundCheckBoxChanged(checkBox, columnName);

            this.SetDefaultTimestepsIfCondition(checkBox, this.CheckBoxAvgRasterSC, this.TextBoxAvgRasterSCTimesteps);
            this.SetDefaultTimestepsIfCondition(checkBox, this.CheckBoxAvgRasterAge, this.TextBoxAvgRasterAgeTimesteps);
            this.SetDefaultTimestepsIfCondition(checkBox, this.CheckBoxAvgRasterST, this.TextBoxAvgRasterSTTimesteps);
            this.SetDefaultTimestepsIfCondition(checkBox, this.CheckBoxAvgRasterTP, this.TextBoxAvgRasterTPTimesteps);
            this.SetDefaultTimestepsIfCondition(checkBox, this.CheckBoxAvgRasterTST, this.TextBoxAvgRasterTSTTimesteps);
            this.SetDefaultTimestepsIfCondition(checkBox, this.CheckBoxAvgRasterSA, this.TextBoxAvgRasterSATimesteps);
            this.SetDefaultTimestepsIfCondition(checkBox, this.CheckBoxAvgRasterTA, this.TextBoxAvgRasterTATimesteps);

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
                    DataRow dr = this.DataFeed.GetDataSheet(Strings.DATASHEET_OO_SPATIAL_AVERAGE_NAME).GetDataRow();
                    dr[columnName] = Booleans.BoolToInt(true);
                    cbTarget.Checked = true;
                }
            }
        }
    }
}
