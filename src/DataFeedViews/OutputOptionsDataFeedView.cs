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
        }

        private void EnableControls()
        {
            //Text Boxes
            this.TextBoxSummarySCTimesteps.Enabled = this.CheckBoxSummarySC.Checked;
            this.TextBoxSummaryTRTimesteps.Enabled = this.CheckBoxSummaryTR.Checked;
            this.TextBoxSummaryTRSCTimesteps.Enabled = this.CheckBoxSummaryTRSC.Checked;
            this.TextBoxSummarySATimesteps.Enabled = this.CheckBoxSummarySA.Checked;
            this.TextBoxSummaryTATimesteps.Enabled = this.CheckBoxSummaryTA.Checked;

            //Timesteps labels
            this.LabelSummarySCTimesteps.Enabled = this.CheckBoxSummarySC.Checked;
            this.LabelSummaryTRTimesteps.Enabled = this.CheckBoxSummaryTR.Checked;
            this.LabelSummaryTRSCTimesteps.Enabled = this.CheckBoxSummaryTRSC.Checked;
            this.LabelSummarySATimesteps.Enabled = this.CheckBoxSummarySA.Checked;
            this.LabelSummaryTATimesteps.Enabled = this.CheckBoxSummaryTA.Checked;

            //Secondary Checkboxes
            this.CheckBoxSummarySCZeroValues.Enabled = this.CheckBoxSummarySC.Checked;
            this.CheckBoxSummarySCAges.Enabled = this.CheckBoxSummarySC.Checked;
            this.CheckBoxSummaryTRCalcIntervalMean.Enabled = this.CheckBoxSummaryTR.Checked;
            this.CheckBoxSummaryTRAges.Enabled = this.CheckBoxSummaryTR.Checked;
            this.CheckBoxSummarySAAges.Enabled = this.CheckBoxSummarySA.Checked;
            this.CheckBoxSummaryTAAges.Enabled = this.CheckBoxSummaryTA.Checked;
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
                    DataRow dr = this.DataFeed.GetDataSheet(Strings.DATASHEET_OO_TABULAR_NAME).GetDataRow();
                    dr[columnName] = Booleans.BoolToInt(true);
                    cbTarget.Checked = true;
                }
            }
        }
    }
}
