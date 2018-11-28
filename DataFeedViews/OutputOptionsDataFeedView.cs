// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.Globalization;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    internal partial class OutputOptionsDataFeedView
    {
        public OutputOptionsDataFeedView()
        {
            InitializeComponent();
        }

        private const string DEFAULT_TIMESTEP_VALUE = "1";

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
            this.SetCheckBoxBinding(this.CheckBoxRasterAATP, Strings.DATASHEET_OO_RASTER_OUTPUT_AATP_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxRasterAATPTimesteps, Strings.DATASHEET_OO_RASTER_OUTPUT_AATP_TIMESTEPS_COLUMN_NAME);

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
            string t = Convert.ToString(
                e.GetValue("TimestepUnits", "Timestep"), 
                CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture);

            this.LabelSummarySCTimesteps.Text = t;
            this.LabelSummaryTRTimesteps.Text = t;
            this.LabelSummaryTRSCTimesteps.Text = t;
            this.LabelSummarySATimesteps.Text = t;
            this.LabelSummaryTATimesteps.Text = t;
            this.LabelRasterSCTimesteps.Text = t;
            this.LabelRasterTRTimesteps.Text = t;
            this.LabelRasterAgeTimesteps.Text = t;
            this.LabelRasterTSTTimesteps.Text = t;
            this.LabelRasterSTTimesteps.Text = t;
            this.LabelRasterSATimesteps.Text = t;
            this.LabelRasterTATimesteps.Text = t;
            this.LabelRasterAATPTimesteps.Text = t;
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
            this.TextBoxRasterAATPTimesteps.Enabled = this.CheckBoxRasterAATP.Checked;

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
            this.LabelRasterAATPTimesteps.Enabled = this.CheckBoxRasterAATP.Checked;

            //Checkboxes
            this.CheckBoxSummarySCZeroValues.Enabled = this.CheckBoxSummarySC.Checked;
            this.CheckBoxSummarySCAges.Enabled = this.CheckBoxSummarySC.Checked;
            this.CheckBoxSummaryTRCalcIntervalMean.Enabled = this.CheckBoxSummaryTR.Checked;
            this.CheckBoxSummaryTRAges.Enabled = this.CheckBoxSummaryTR.Checked;
            this.CheckBoxSummarySAAges.Enabled = this.CheckBoxSummarySA.Checked;
            this.CheckBoxSummaryTAAges.Enabled = this.CheckBoxSummaryTA.Checked;
        }

        protected override void OnBoundCheckBoxChanged(System.Windows.Forms.CheckBox checkBox, string columnName)
        {
            base.OnBoundCheckBoxChanged(checkBox, columnName);

            if (checkBox == this.CheckBoxSummarySC && this.CheckBoxSummarySC.Checked & string.IsNullOrEmpty(this.TextBoxSummarySCTimesteps.Text))
            {
                this.SetTextBoxData(this.TextBoxSummarySCTimesteps, DEFAULT_TIMESTEP_VALUE);
            }
            else if (checkBox == this.CheckBoxSummaryTR && this.CheckBoxSummaryTR.Checked & string.IsNullOrEmpty(this.TextBoxSummaryTRTimesteps.Text))
            {
                this.SetTextBoxData(this.TextBoxSummaryTRTimesteps, DEFAULT_TIMESTEP_VALUE);
            }
            else if (checkBox == this.CheckBoxSummaryTRSC && this.CheckBoxSummaryTRSC.Checked & string.IsNullOrEmpty(this.TextBoxSummaryTRSCTimesteps.Text))
            {
                this.SetTextBoxData(this.TextBoxSummaryTRSCTimesteps, DEFAULT_TIMESTEP_VALUE);
            }
            else if (checkBox == this.CheckBoxSummarySA && this.CheckBoxSummarySA.Checked & string.IsNullOrEmpty(this.TextBoxSummarySATimesteps.Text))
            {
                this.SetTextBoxData(this.TextBoxSummarySATimesteps, DEFAULT_TIMESTEP_VALUE);
            }
            else if (checkBox == this.CheckBoxSummaryTA && this.CheckBoxSummaryTA.Checked & string.IsNullOrEmpty(this.TextBoxSummaryTATimesteps.Text))
            {
                this.SetTextBoxData(this.TextBoxSummaryTATimesteps, DEFAULT_TIMESTEP_VALUE);
            }
            else if (checkBox == this.CheckBoxRasterSC && this.CheckBoxRasterSC.Checked & string.IsNullOrEmpty(this.TextBoxRasterSCTimesteps.Text))
            {
                this.SetTextBoxData(this.TextBoxRasterSCTimesteps, DEFAULT_TIMESTEP_VALUE);
            }
            else if (checkBox == this.CheckBoxRasterTR && this.CheckBoxRasterTR.Checked & string.IsNullOrEmpty(this.TextBoxRasterTRTimesteps.Text))
            {
                this.SetTextBoxData(this.TextBoxRasterTRTimesteps, DEFAULT_TIMESTEP_VALUE);
            }
            else if (checkBox == this.CheckBoxRasterAge && this.CheckBoxRasterAge.Checked & string.IsNullOrEmpty(this.TextBoxRasterAgeTimesteps.Text))
            {
                this.SetTextBoxData(this.TextBoxRasterAgeTimesteps, DEFAULT_TIMESTEP_VALUE);
            }
            else if (checkBox == this.CheckBoxRasterTST && this.CheckBoxRasterTST.Checked & string.IsNullOrEmpty(this.TextBoxRasterTSTTimesteps.Text))
            {
                this.SetTextBoxData(this.TextBoxRasterTSTTimesteps, DEFAULT_TIMESTEP_VALUE);
            }
            else if (checkBox == this.CheckBoxRasterST && this.CheckBoxRasterST.Checked & string.IsNullOrEmpty(this.TextBoxRasterSTTimesteps.Text))
            {
                this.SetTextBoxData(this.TextBoxRasterSTTimesteps, DEFAULT_TIMESTEP_VALUE);
            }
            else if (checkBox == this.CheckBoxRasterSA && this.CheckBoxRasterSA.Checked & string.IsNullOrEmpty(this.TextBoxRasterSATimesteps.Text))
            {
                this.SetTextBoxData(this.TextBoxRasterSATimesteps, DEFAULT_TIMESTEP_VALUE);
            }
            else if (checkBox == this.CheckBoxRasterTA && this.CheckBoxRasterTA.Checked & string.IsNullOrEmpty(this.TextBoxRasterTATimesteps.Text))
            {
                this.SetTextBoxData(this.TextBoxRasterTATimesteps, DEFAULT_TIMESTEP_VALUE);
            }
            else if (checkBox == this.CheckBoxRasterAATP && this.CheckBoxRasterAATP.Checked & string.IsNullOrEmpty(this.TextBoxRasterAATPTimesteps.Text))
            {
                this.SetTextBoxData(this.TextBoxRasterAATPTimesteps, DEFAULT_TIMESTEP_VALUE);
            }

            this.EnableControls();
        }
    }
}
