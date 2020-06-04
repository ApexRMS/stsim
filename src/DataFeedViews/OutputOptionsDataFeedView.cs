// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    internal partial class OutputOptionsDataFeedView
    {
        private bool m_SettingCheckBox;
        private const string DEFAULT_TIMESTEP_VALUE = "1";

        public OutputOptionsDataFeedView()
        {
            InitializeComponent();
        }

        public override void LoadDataFeed(DataFeed dataFeed)
        {
            base.LoadDataFeed(dataFeed);

            this.SetCheckBoxBinding(this.CheckBoxSummarySC, Strings.DATASHEET_OO_SUMMARY_OUTPUT_SC_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxSummarySCTimesteps, Strings.DATASHEET_OO_SUMMARY_OUTPUT_SC_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxSummarySCAges, Strings.DATASHEET_OO_SUMMARY_OUTPUT_SC_AGES_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxSummarySCZeroValues, Strings.DATASHEET_OO_SUMMARY_OUTPUT_SC_ZERO_VALUES_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxSummaryTR, Strings.DATASHEET_OO_SUMMARY_OUTPUT_TR_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxSummaryTRTimesteps, Strings.DATASHEET_OO_SUMMARY_OUTPUT_TR_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxSummaryTRAges, Strings.DATASHEET_OO_SUMMARY_OUTPUT_TR_AGES_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxSummaryTRCalcIntervalMean, Strings.DATASHEET_OO_SUMMARY_OUTPUT_TR_INTERVAL_MEAN_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxSummaryTRSC, Strings.DATASHEET_OO_SUMMARY_OUTPUT_TRSC_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxSummaryTRSCTimesteps, Strings.DATASHEET_OO_SUMMARY_OUTPUT_TRSC_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxSummarySA, Strings.DATASHEET_OO_SUMMARY_OUTPUT_SA_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxSummarySATimesteps, Strings.DATASHEET_OO_SUMMARY_OUTPUT_SA_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxSummarySAAges, Strings.DATASHEET_OO_SUMMARY_OUTPUT_SA_AGES_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxSummaryTA, Strings.DATASHEET_OO_SUMMARY_OUTPUT_TA_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxSummaryTATimesteps, Strings.DATASHEET_OO_SUMMARY_OUTPUT_TA_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxSummaryTAAges, Strings.DATASHEET_OO_SUMMARY_OUTPUT_TA_AGES_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxSummaryOmitSS, Strings.DATASHEET_OO_SUMMARY_OUTPUT_OMIT_SS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxSummaryOmitTS, Strings.DATASHEET_OO_SUMMARY_OUTPUT_OMIT_TS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxRasterSC, Strings.DATASHEET_OO_RASTER_OUTPUT_SC_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxRasterSCTimesteps, Strings.DATASHEET_OO_RASTER_OUTPUT_SC_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxRasterTR, Strings.DATASHEET_OO_RASTER_OUTPUT_TR_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxRasterTRTimesteps, Strings.DATASHEET_OO_RASTER_OUTPUT_TR_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxRasterAge, Strings.DATASHEET_OO_RASTER_OUTPUT_AGE_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxRasterAgeTimesteps, Strings.DATASHEET_OO_RASTER_OUTPUT_AGE_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxRasterTST, Strings.DATASHEET_OO_RASTER_OUTPUT_TST_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxRasterTSTTimesteps, Strings.DATASHEET_OO_RASTER_OUTPUT_TST_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxRasterST, Strings.DATASHEET_OO_RASTER_OUTPUT_ST_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxRasterSTTimesteps, Strings.DATASHEET_OO_RASTER_OUTPUT_ST_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxRasterSA, Strings.DATASHEET_OO_RASTER_OUTPUT_SA_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxRasterSATimesteps, Strings.DATASHEET_OO_RASTER_OUTPUT_SA_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxRasterTA, Strings.DATASHEET_OO_RASTER_OUTPUT_TA_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxRasterTATimesteps, Strings.DATASHEET_OO_RASTER_OUTPUT_TA_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxRasterTE, Strings.DATASHEET_OO_RASTER_OUTPUT_TE_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxRasterTETimesteps, Strings.DATASHEET_OO_RASTER_OUTPUT_TE_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxAvgRasterSC, Strings.DATASHEET_OO_AVG_RASTER_OUTPUT_SC_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxAvgRasterSCTimesteps, Strings.DATASHEET_OO_AVG_RASTER_OUTPUT_SC_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxAvgRasterSCAcrossTimesteps, Strings.DATASHEET_OO_AVG_RASTER_OUTPUT_SC_ACROSS_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxAvgRasterSA, Strings.DATASHEET_OO_AVG_RASTER_OUTPUT_SA_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxAvgRasterSATimesteps, Strings.DATASHEET_OO_AVG_RASTER_OUTPUT_SA_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxAvgRasterSAAcrossTimesteps, Strings.DATASHEET_OO_AVG_RASTER_OUTPUT_SA_ACROSS_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxAvgRasterTA, Strings.DATASHEET_OO_AVG_RASTER_OUTPUT_TA_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxAvgRasterTATimesteps, Strings.DATASHEET_OO_AVG_RASTER_OUTPUT_TA_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxAvgRasterTAAcrossTimesteps, Strings.DATASHEET_OO_AVG_RASTER_OUTPUT_TA_ACROSS_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxAvgRasterTP, Strings.DATASHEET_OO_AVG_RASTER_OUTPUT_TP_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxAvgRasterTPTimesteps, Strings.DATASHEET_OO_AVG_RASTER_OUTPUT_TP_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxAvgRasterTPAcrossTimesteps, Strings.DATASHEET_OO_AVG_RASTER_OUTPUT_TP_ACROSS_TIMESTEPS_COLUMN_NAME);

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

            this.LabelSummarySCTimesteps.Text = NewTimestepsText;
            this.LabelSummaryTRTimesteps.Text = NewTimestepsText;
            this.LabelSummaryTRSCTimesteps.Text = NewTimestepsText;
            this.LabelSummarySATimesteps.Text = NewTimestepsText;
            this.LabelSummaryTATimesteps.Text = NewTimestepsText;
            this.LabelRasterSCTimesteps.Text = NewTimestepsText;
            this.LabelRasterTRTimesteps.Text = NewTimestepsText;
            this.LabelRasterAgeTimesteps.Text = NewTimestepsText;
            this.LabelRasterTSTTimesteps.Text = NewTimestepsText;
            this.LabelRasterSTTimesteps.Text = NewTimestepsText;
            this.LabelRasterSATimesteps.Text = NewTimestepsText;
            this.LabelRasterTATimesteps.Text = NewTimestepsText;
            this.LabelRasterTETimesteps.Text = NewTimestepsText;
            this.LabelAvgRasterSCTimesteps.Text = NewTimestepsText;
            this.LabelAvgRasterSATimesteps.Text = NewTimestepsText;
            this.LabelAvgRasterTATimesteps.Text = NewTimestepsText;
            this.LabelAvgRasterTPTimesteps.Text = NewTimestepsText;

            string NewAverageAcrossText = string.Format(CultureInfo.CurrentCulture,
                "Average across preceeding {0}s", NewTimestepsText);

            this.CheckBoxAvgRasterSCAcrossTimesteps.Text = NewAverageAcrossText;
            this.CheckBoxAvgRasterSAAcrossTimesteps.Text = NewAverageAcrossText;
            this.CheckBoxAvgRasterTAAcrossTimesteps.Text = NewAverageAcrossText;
            this.CheckBoxAvgRasterTPAcrossTimesteps.Text = NewAverageAcrossText;
        }

        private void EnableControls()
        {
            //Text Boxes
            this.TextBoxSummarySCTimesteps.Enabled = this.CheckBoxSummarySC.Checked;
            this.TextBoxSummaryTRTimesteps.Enabled = this.CheckBoxSummaryTR.Checked;
            this.TextBoxSummaryTRSCTimesteps.Enabled = this.CheckBoxSummaryTRSC.Checked;
            this.TextBoxSummarySATimesteps.Enabled = this.CheckBoxSummarySA.Checked;
            this.TextBoxSummaryTATimesteps.Enabled = this.CheckBoxSummaryTA.Checked;
            this.TextBoxRasterSCTimesteps.Enabled = this.CheckBoxRasterSC.Checked;
            this.TextBoxRasterTRTimesteps.Enabled = this.CheckBoxRasterTR.Checked;
            this.TextBoxRasterAgeTimesteps.Enabled = this.CheckBoxRasterAge.Checked;
            this.TextBoxRasterTSTTimesteps.Enabled = this.CheckBoxRasterTST.Checked;
            this.TextBoxRasterSTTimesteps.Enabled = this.CheckBoxRasterST.Checked;
            this.TextBoxRasterSATimesteps.Enabled = this.CheckBoxRasterSA.Checked;
            this.TextBoxRasterTATimesteps.Enabled = this.CheckBoxRasterTA.Checked;
            this.TextBoxAvgRasterTPTimesteps.Enabled = this.CheckBoxAvgRasterTP.Checked;
            this.TextBoxRasterTETimesteps.Enabled = this.CheckBoxRasterTE.Checked;

            //Timesteps labels
            this.LabelSummarySCTimesteps.Enabled = this.CheckBoxSummarySC.Checked;
            this.LabelSummaryTRTimesteps.Enabled = this.CheckBoxSummaryTR.Checked;
            this.LabelSummaryTRSCTimesteps.Enabled = this.CheckBoxSummaryTRSC.Checked;
            this.LabelSummarySATimesteps.Enabled = this.CheckBoxSummarySA.Checked;
            this.LabelSummaryTATimesteps.Enabled = this.CheckBoxSummaryTA.Checked;
            this.LabelRasterSCTimesteps.Enabled = this.CheckBoxRasterSC.Checked;
            this.LabelRasterTRTimesteps.Enabled = this.CheckBoxRasterTR.Checked;
            this.LabelRasterAgeTimesteps.Enabled = this.CheckBoxRasterAge.Checked;
            this.LabelRasterTSTTimesteps.Enabled = this.CheckBoxRasterTST.Checked;
            this.LabelRasterSTTimesteps.Enabled = this.CheckBoxRasterST.Checked;
            this.LabelRasterSATimesteps.Enabled = this.CheckBoxRasterSA.Checked;
            this.LabelRasterTATimesteps.Enabled = this.CheckBoxRasterTA.Checked;
            this.LabelRasterTETimesteps.Enabled = this.CheckBoxRasterTE.Checked;
            this.LabelAvgRasterSCTimesteps.Enabled = this.CheckBoxAvgRasterSC.Checked;
            this.LabelAvgRasterSATimesteps.Enabled = this.CheckBoxAvgRasterSA.Checked;
            this.LabelAvgRasterTATimesteps.Enabled = this.CheckBoxAvgRasterTA.Checked;
            this.LabelAvgRasterTPTimesteps.Enabled = this.CheckBoxAvgRasterTP.Checked;

            //Secondary Checkboxes
            this.CheckBoxSummarySCZeroValues.Enabled = this.CheckBoxSummarySC.Checked;
            this.CheckBoxSummarySCAges.Enabled = this.CheckBoxSummarySC.Checked;
            this.CheckBoxSummaryTRCalcIntervalMean.Enabled = this.CheckBoxSummaryTR.Checked;
            this.CheckBoxSummaryTRAges.Enabled = this.CheckBoxSummaryTR.Checked;
            this.CheckBoxSummarySAAges.Enabled = this.CheckBoxSummarySA.Checked;
            this.CheckBoxSummaryTAAges.Enabled = this.CheckBoxSummaryTA.Checked;
            this.CheckBoxAvgRasterSCAcrossTimesteps.Enabled = this.CheckBoxAvgRasterSC.Checked;
            this.CheckBoxAvgRasterSAAcrossTimesteps.Enabled = this.CheckBoxAvgRasterSA.Checked;
            this.CheckBoxAvgRasterTAAcrossTimesteps.Enabled = this.CheckBoxAvgRasterTA.Checked;
            this.CheckBoxAvgRasterTPAcrossTimesteps.Enabled = this.CheckBoxAvgRasterTP.Checked;
        }

        protected override void OnBoundCheckBoxChanged(CheckBox checkBox, string columnName)
        {
            if (this.m_SettingCheckBox)
            {
                return;
            }

            base.OnBoundCheckBoxChanged(checkBox, columnName);

            this.SetDefaultTimestepsIfCondition(checkBox, this.CheckBoxSummarySC, this.TextBoxSummarySCTimesteps);
            this.SetDefaultTimestepsIfCondition(checkBox, this.CheckBoxSummaryTR, this.TextBoxSummaryTRTimesteps);
            this.SetDefaultTimestepsIfCondition(checkBox, this.CheckBoxSummaryTRSC, this.TextBoxSummaryTRSCTimesteps);
            this.SetDefaultTimestepsIfCondition(checkBox, this.CheckBoxSummarySA, this.TextBoxSummarySATimesteps);
            this.SetDefaultTimestepsIfCondition(checkBox, this.CheckBoxSummaryTA, this.TextBoxSummaryTATimesteps);
            this.SetDefaultTimestepsIfCondition(checkBox, this.CheckBoxRasterSC, this.TextBoxRasterSCTimesteps);
            this.SetDefaultTimestepsIfCondition(checkBox, this.CheckBoxRasterTR, this.TextBoxRasterTRTimesteps);
            this.SetDefaultTimestepsIfCondition(checkBox, this.CheckBoxRasterAge, this.TextBoxRasterAgeTimesteps);
            this.SetDefaultTimestepsIfCondition(checkBox, this.CheckBoxRasterTST, this.TextBoxRasterTSTTimesteps);
            this.SetDefaultTimestepsIfCondition(checkBox, this.CheckBoxRasterST, this.TextBoxRasterSTTimesteps);
            this.SetDefaultTimestepsIfCondition(checkBox, this.CheckBoxRasterSA, this.TextBoxRasterSATimesteps);
            this.SetDefaultTimestepsIfCondition(checkBox, this.CheckBoxRasterTA, this.TextBoxRasterTATimesteps);
            this.SetDefaultTimestepsIfCondition(checkBox, this.CheckBoxRasterTE, this.TextBoxRasterTETimesteps);
            this.SetDefaultTimestepsIfCondition(checkBox, this.CheckBoxAvgRasterSC, this.TextBoxAvgRasterSCTimesteps);
            this.SetDefaultTimestepsIfCondition(checkBox, this.CheckBoxAvgRasterSA, this.TextBoxAvgRasterSATimesteps);
            this.SetDefaultTimestepsIfCondition(checkBox, this.CheckBoxAvgRasterTA, this.TextBoxAvgRasterTATimesteps);
            this.SetDefaultTimestepsIfCondition(checkBox, this.CheckBoxAvgRasterTP, this.TextBoxAvgRasterTPTimesteps);

            this.m_SettingCheckBox = true;
            this.SetCheckBoxValueIfCondition(checkBox, this.CheckBoxSummarySC, this.CheckBoxSummarySCAges, Strings.DATASHEET_OO_SUMMARY_OUTPUT_SC_AGES_COLUMN_NAME);
            this.SetCheckBoxValueIfCondition(checkBox, this.CheckBoxSummaryTR, this.CheckBoxSummaryTRAges, Strings.DATASHEET_OO_SUMMARY_OUTPUT_TR_AGES_COLUMN_NAME);
            this.SetCheckBoxValueIfCondition(checkBox, this.CheckBoxSummarySA, this.CheckBoxSummarySAAges, Strings.DATASHEET_OO_SUMMARY_OUTPUT_SA_AGES_COLUMN_NAME);
            this.SetCheckBoxValueIfCondition(checkBox, this.CheckBoxSummaryTA, this.CheckBoxSummaryTAAges, Strings.DATASHEET_OO_SUMMARY_OUTPUT_TA_AGES_COLUMN_NAME);
            this.m_SettingCheckBox = false;

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
                    DataRow dr = this.DataFeed.GetDataSheet(Strings.DATASHEET_OO_NAME).GetDataRow();
                    dr[columnName] = Booleans.BoolToInt(true);
                    cbTarget.Checked = true;
                }
            }
        }
    }
}
