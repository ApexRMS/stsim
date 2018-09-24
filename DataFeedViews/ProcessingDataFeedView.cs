// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;
using System.Reflection;
using System.Globalization;

namespace SyncroSim.STSim
{
    [ObfuscationAttribute(Exclude=true, ApplyToMembers=false)]
    internal partial class ProcessingDataFeedView
    {
        public ProcessingDataFeedView()
        {
            InitializeComponent();
        }

        public override void LoadDataFeed(DataFeed dataFeed)
        {
            base.LoadDataFeed(dataFeed);

            this.SetCheckBoxBinding(this.CheckBoxSplitSecStrat, Strings.DATASHEET_PROCESSING_SPLIT_BY_SS_COLUMN_NAME);
            this.MonitorDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME, this.OnTerminologyChanged, true);
        }

        private void OnTerminologyChanged(DataSheetMonitorEventArgs e)
        {
            this.CheckBoxSplitSecStrat.Text = string.Format(CultureInfo.InvariantCulture, 
                "Split non-spatial runs by {0}", 
                Convert.ToString(
                    e.GetValue(Strings.DATASHEET_TERMINOLOGY_SECONDARY_STRATUM_LABEL_COLUMN_NAME), 
                    CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture));
        }
    }
}
