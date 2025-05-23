// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;
using System.Globalization;
using System.Reflection;

namespace SyncroSim.STSim
{
	[ObfuscationAttribute(Exclude=true, ApplyToMembers=false)]
	internal partial class StockFlowOutputOptionsDataFeedView
	{
		public StockFlowOutputOptionsDataFeedView()
		{
			InitializeComponent();
		}

		private const string DEFAULT_TIMESTEP_VALUE = "1";

		public override void LoadDataFeed(DataFeed dataFeed)
		{
			base.LoadDataFeed(dataFeed);

			this.SetCheckBoxBinding(this.CheckBoxSummaryST, Strings.DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_ST_COLUMN_NAME);
			this.SetTextBoxBinding(this.TextBoxSummarySTTimesteps, Strings.DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_ST_TIMESTEPS_COLUMN_NAME);
			this.SetCheckBoxBinding(this.CheckBoxSTOmitSS, Strings.DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_ST_OMIT_SS_COLUMN_NAME);
			this.SetCheckBoxBinding(this.CheckBoxSTOmitTS, Strings.DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_ST_OMIT_TS_COLUMN_NAME);
			this.SetCheckBoxBinding(this.CheckBoxSTOmitSC, Strings.DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_ST_OMIT_SC_COLUMN_NAME);

			this.SetCheckBoxBinding(this.CheckBoxSummaryFL, Strings.DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_FL_COLUMN_NAME);
			this.SetTextBoxBinding(this.TextBoxSummaryFLTimesteps, Strings.DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_FL_TIMESTEPS_COLUMN_NAME);
			this.SetCheckBoxBinding(this.CheckBoxFLOmitSS, Strings.DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_FL_OMIT_SS_COLUMN_NAME);
			this.SetCheckBoxBinding(this.CheckBoxFLOmitTS, Strings.DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_FL_OMIT_TS_COLUMN_NAME);
			this.SetCheckBoxBinding(this.CheckBoxFLOmitFromSC, Strings.DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_FL_OMIT_FROM_SC_COLUMN_NAME);
			this.SetCheckBoxBinding(this.CheckBoxFLOmitFromST, Strings.DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_FL_OMIT_FROM_ST_COLUMN_NAME);
			this.SetCheckBoxBinding(this.CheckBoxFLOmitTT, Strings.DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_FL_OMIT_TT_COLUMN_NAME);
			this.SetCheckBoxBinding(this.CheckBoxFLOmitToSC, Strings.DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_FL_OMIT_TO_SC_COLUMN_NAME);
			this.SetCheckBoxBinding(this.CheckBoxFLOmitToST, Strings.DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_FL_OMIT_TO_ST_COLUMN_NAME);

			this.SetCheckBoxBinding(this.CheckBoxSpatialST, Strings.DATASHEET_STOCKFLOW_OO_SPATIAL_OUTPUT_ST_COLUMN_NAME);
			this.SetTextBoxBinding(this.TextBoxSpatialSTTimesteps, Strings.DATASHEET_STOCKFLOW_OO_SPATIAL_OUTPUT_ST_TIMESTEPS_COLUMN_NAME);
			this.SetCheckBoxBinding(this.CheckBoxSpatialFL, Strings.DATASHEET_STOCKFLOW_OO_SPATIAL_OUTPUT_FL_COLUMN_NAME);
			this.SetTextBoxBinding(this.TextBoxSpatialFLTimesteps, Strings.DATASHEET_STOCKFLOW_OO_SPATIAL_OUTPUT_FL_TIMESTEPS_COLUMN_NAME);
			this.SetCheckBoxBinding(this.CheckBoxLateralFL, Strings.DATASHEET_STOCKFLOW_OO_LATERAL_OUTPUT_FL_COLUMN_NAME);
			this.SetTextBoxBinding(this.TextBoxLateralFLTimesteps, Strings.DATASHEET_STOCKFLOW_OO_LATERAL_OUTPUT_FL_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxAvgSpatialST, Strings.DATASHEET_STOCKFLOW_OO_AVG_SPATIAL_OUTPUT_ST_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxAvgSpatialSTTimesteps, Strings.DATASHEET_STOCKFLOW_OO_AVG_SPATIAL_OUTPUT_ST_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxAvgSpatialSTCumulative, Strings.DATASHEET_STOCKFLOW_OO_AVG_SPATIAL_OUTPUT_ST_ACROSS_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxAvgSpatialFL, Strings.DATASHEET_STOCKFLOW_OO_AVG_SPATIAL_OUTPUT_FL_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxAvgSpatialFLTimesteps, Strings.DATASHEET_STOCKFLOW_OO_AVG_SPATIAL_OUTPUT_FL_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxAvgSpatialFLCumulative, Strings.DATASHEET_STOCKFLOW_OO_AVG_SPATIAL_OUTPUT_FL_ACROSS_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxAvgSpatialLFL, Strings.DATASHEET_STOCKFLOW_OO_AVG_SPATIAL_OUTPUT_LFL_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxAvgSpatialLFLTimesteps, Strings.DATASHEET_STOCKFLOW_OO_AVG_SPATIAL_OUTPUT_LFL_TIMESTEPS_COLUMN_NAME);
            this.SetCheckBoxBinding(this.CheckBoxAvgSpatialLFLCumulative, Strings.DATASHEET_STOCKFLOW_OO_AVG_SPATIAL_OUTPUT_LFL_ACROSS_TIMESTEPS_COLUMN_NAME);

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
                e.GetValue("TimestepUnits", "timestep"), CultureInfo.InvariantCulture).
                ToLower(CultureInfo.InvariantCulture);

			this.LabelSummarySTTimesteps.Text = t;
			this.LabelSummaryFLTimesteps.Text = t;
			this.LabelSpatialSTTimesteps.Text = t;
			this.LabelSpatialFLTimesteps.Text = t;
            this.LabelLateralFLTimesteps.Text = t;
            this.LabelAvgSpatialSTTimesteps.Text = t;
            this.LabelAvgSpatialFLTimesteps.Text = t;
            this.LabelAvgSpatialLFLTimesteps.Text = t;
        }

		protected override void OnBoundCheckBoxChanged(System.Windows.Forms.CheckBox checkBox, string columnName)
		{
			base.OnBoundCheckBoxChanged(checkBox, columnName);

			if (checkBox == this.CheckBoxSummaryST && this.CheckBoxSummaryST.Checked & string.IsNullOrEmpty(this.TextBoxSummarySTTimesteps.Text))
			{
				this.SetTextBoxData(this.TextBoxSummarySTTimesteps, DEFAULT_TIMESTEP_VALUE);
			}
			else if (checkBox == this.CheckBoxSummaryFL && this.CheckBoxSummaryFL.Checked & string.IsNullOrEmpty(this.TextBoxSummaryFLTimesteps.Text))
			{
				this.SetTextBoxData(this.TextBoxSummaryFLTimesteps, DEFAULT_TIMESTEP_VALUE);
			}
			else if (checkBox == this.CheckBoxSpatialST && this.CheckBoxSpatialST.Checked & string.IsNullOrEmpty(this.TextBoxSpatialSTTimesteps.Text))
			{
				this.SetTextBoxData(this.TextBoxSpatialSTTimesteps, DEFAULT_TIMESTEP_VALUE);
			}
			else if (checkBox == this.CheckBoxSpatialFL && this.CheckBoxSpatialFL.Checked & string.IsNullOrEmpty(this.TextBoxSpatialFLTimesteps.Text))
			{
				this.SetTextBoxData(this.TextBoxSpatialFLTimesteps, DEFAULT_TIMESTEP_VALUE);
			}
			else if (checkBox == this.CheckBoxLateralFL && this.CheckBoxLateralFL.Checked & string.IsNullOrEmpty(this.TextBoxLateralFLTimesteps.Text))
			{
				this.SetTextBoxData(this.TextBoxLateralFLTimesteps, DEFAULT_TIMESTEP_VALUE);
			}
            else if (checkBox == this.CheckBoxAvgSpatialST && this.CheckBoxAvgSpatialST.Checked & string.IsNullOrEmpty(this.TextBoxAvgSpatialSTTimesteps.Text))
            {
                this.SetTextBoxData(this.TextBoxAvgSpatialSTTimesteps, DEFAULT_TIMESTEP_VALUE);
            }
            else if (checkBox == this.CheckBoxAvgSpatialFL && this.CheckBoxAvgSpatialFL.Checked & string.IsNullOrEmpty(this.TextBoxAvgSpatialFLTimesteps.Text))
            {
                this.SetTextBoxData(this.TextBoxAvgSpatialFLTimesteps, DEFAULT_TIMESTEP_VALUE);
            }
            else if (checkBox == this.CheckBoxAvgSpatialLFL && this.CheckBoxAvgSpatialLFL.Checked & string.IsNullOrEmpty(this.TextBoxAvgSpatialLFLTimesteps.Text))
            {
                this.SetTextBoxData(this.TextBoxAvgSpatialLFLTimesteps, DEFAULT_TIMESTEP_VALUE);
            }

            this.EnableControls();
		}

		private void EnableControls()
		{
			//Text Boxes
			this.TextBoxSummarySTTimesteps.Enabled = this.CheckBoxSummaryST.Checked;
			this.TextBoxSummaryFLTimesteps.Enabled = this.CheckBoxSummaryFL.Checked;
			this.TextBoxSpatialSTTimesteps.Enabled = this.CheckBoxSpatialST.Checked;
			this.TextBoxSpatialFLTimesteps.Enabled = this.CheckBoxSpatialFL.Checked;
			this.TextBoxLateralFLTimesteps.Enabled = this.CheckBoxLateralFL.Checked;
            this.TextBoxAvgSpatialSTTimesteps.Enabled = this.CheckBoxAvgSpatialST.Checked;
            this.TextBoxAvgSpatialFLTimesteps.Enabled = this.CheckBoxAvgSpatialFL.Checked;
            this.TextBoxAvgSpatialLFLTimesteps.Enabled = this.CheckBoxAvgSpatialLFL.Checked;

			//Timesteps labels
			this.LabelSummarySTTimesteps.Enabled = this.CheckBoxSummaryST.Checked;
			this.LabelSummaryFLTimesteps.Enabled = this.CheckBoxSummaryFL.Checked;
			this.LabelSpatialSTTimesteps.Enabled = this.CheckBoxSpatialST.Checked;
			this.LabelSpatialFLTimesteps.Enabled = this.CheckBoxSpatialFL.Checked;
			this.LabelLateralFLTimesteps.Enabled = this.CheckBoxLateralFL.Checked;
			this.LabelAvgSpatialSTTimesteps.Enabled = this.CheckBoxAvgSpatialST.Checked;
			this.LabelAvgSpatialFLTimesteps.Enabled = this.CheckBoxAvgSpatialFL.Checked;
			this.LabelAvgSpatialLFLTimesteps.Enabled = this.CheckBoxAvgSpatialLFL.Checked;

            //Secondary Check Boxes
            this.CheckBoxAvgSpatialSTCumulative.Enabled = this.CheckBoxAvgSpatialST.Checked;
            this.CheckBoxAvgSpatialFLCumulative.Enabled = this.CheckBoxAvgSpatialFL.Checked;
            this.CheckBoxAvgSpatialLFLCumulative.Enabled = this.CheckBoxAvgSpatialLFL.Checked;
			this.CheckBoxSTOmitSS.Enabled = this.CheckBoxSummaryST.Checked;
			this.CheckBoxSTOmitTS.Enabled = this.CheckBoxSummaryST.Checked;
			this.CheckBoxSTOmitSC.Enabled = this.CheckBoxSummaryST.Checked;
		}
	}
}