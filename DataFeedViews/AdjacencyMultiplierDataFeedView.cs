// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System.Reflection;
using SyncroSim.Core.Forms;

namespace SyncroSim.STSim
{
    [ObfuscationAttribute(Exclude=true, ApplyToMembers=false)]
    internal partial class AdjacencyMultiplierDataFeedView
    {
        public AdjacencyMultiplierDataFeedView()
        {
            InitializeComponent();
        }

        protected override void InitializeView()
        {
            base.InitializeView();

            DataFeedView v1 = this.Session.CreateMultiRowDataFeedView(this.Scenario, this.ControllingScenario);
            this.PanelSettings.Controls.Add(v1);

            DataFeedView v2 = this.Session.CreateMultiRowDataFeedView(this.Scenario, this.ControllingScenario);
            this.PanelMultipliers.Controls.Add(v2);
        }

        public override void LoadDataFeed(Core.DataFeed dataFeed)
        {
            base.LoadDataFeed(dataFeed);

            DataFeedView v1 = (DataFeedView)this.PanelSettings.Controls[0];
            v1.LoadDataFeed(dataFeed, Strings.DATASHEET_TRANSITION_ADJACENCY_SETTING_NAME);

            DataFeedView v2 = (DataFeedView)this.PanelMultipliers.Controls[0];
            v2.LoadDataFeed(dataFeed, Strings.DATASHEET_TRANSITION_ADJACENCY_MULTIPLIER_NAME);
        }

        public override void EnableView(bool enable)
        {
            if (this.PanelSettings.Controls.Count > 0)
            {
                DataFeedView v = (DataFeedView)this.PanelSettings.Controls[0];
                v.EnableView(enable);
            }

            if (this.PanelMultipliers.Controls.Count > 0)
            {
                DataFeedView v = (DataFeedView)this.PanelMultipliers.Controls[0];
                v.EnableView(enable);
            }

            this.LabelSettings.Enabled = enable;
            this.LabelMultipliers.Enabled = enable;
        }
    }
}
