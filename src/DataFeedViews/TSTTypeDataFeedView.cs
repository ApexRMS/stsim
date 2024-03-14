// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Globalization;
using System.Windows.Forms;
using SyncroSim.Core.Forms;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    internal partial class TSTTypeDataFeedView : DataFeedView
    {
        public TSTTypeDataFeedView()
        {
            InitializeComponent();
        }

        public override void LoadDataFeed(Core.DataFeed dataFeed)
        {
            base.LoadDataFeed(dataFeed);

            this.SetTextBoxBinding(this.TextBoxFrequency, Strings.DATASHEET_TST_TYPE_FREQUENCY_COLUMN_NAME);
            this.SetTextBoxBinding(this.TextBoxMaximum, Strings.DATASHEET_TST_TYPE_MAXIMUM_COLUMN_NAME);

            this.RefreshBoundControls();
            this.AddStandardCommands();

            this.MonitorDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME, this.OnTerminologyChanged, true);
        }

        protected override bool OnBoundTextBoxValidating(TextBox textBox, string columnName, string proposedValue)
        {
            if (!base.OnBoundTextBoxValidating(textBox, columnName, proposedValue))
            {
                return false;
            }

            if (!ProjectUtilities.ProjectHasResults(this.Project))
            {
                return true;
            }

            if (!ChartingUtilities.HasTSTClassUpdateTag(this.Project))
            {
                if (MessageBox.Show(
                    MessageStrings.PROMPT_TST_TYPE_CHANGE,
                    "TST Type", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    return false;
                }
            }

            return true;
        }

        private void OnTerminologyChanged(DataSheetMonitorEventArgs e)
        {
            string NewTimestepsText = Convert.ToString(
                e.GetValue("TimestepUnits", "Timestep"),
                CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture);

            this.LabelTimesteps.Text = NewTimestepsText;
        }
    }
}
