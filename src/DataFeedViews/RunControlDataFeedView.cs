// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;
using System.Globalization;

namespace SyncroSim.STSim
{
    internal partial class RunControlDataFeedView
    {
        public RunControlDataFeedView()
        {
            InitializeComponent();
        }

        public override void LoadDataFeed(DataFeed dataFeed)
        {
            base.LoadDataFeed(dataFeed);

            this.SetTextBoxBinding(this.TextBoxStartTimestep, "MinimumTimestep");
            this.SetTextBoxBinding(this.TextBoxEndTimestep, "MaximumTimestep");
            this.SetTextBoxBinding(this.TextBoxTotalIterations, "MaximumIteration");
            this.SetCheckBoxBinding(this.CheckBoxIsSpatial, "IsSpatial");

            this.MonitorDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME, this.OnTerminologyChanged, true);
            this.AddStandardCommands();
        }

        private void OnTerminologyChanged(DataSheetMonitorEventArgs e)
        {
            string t = Convert.ToString(
                e.GetValue("TimestepUnits", "Timestep"), CultureInfo.InvariantCulture).
                ToLower(CultureInfo.InvariantCulture);

            this.LabelStartTimestep.Text = string.Format(CultureInfo.InvariantCulture, "Start {0}:", t);
            this.LabelEndTimestep.Text = string.Format(CultureInfo.InvariantCulture, "End {0}:", t);
        }
    }
}
