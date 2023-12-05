// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Reflection;
using SyncroSim.Core;
using SyncroSim.Core.Forms;

namespace SyncroSim.STSim
{
	[ObfuscationAttribute(Exclude=true, ApplyToMembers=false)]
	internal partial class FlowOrderDataFeedView
	{
		public FlowOrderDataFeedView()
		{
			InitializeComponent();
		}
		protected override void InitializeView()
		{
			base.InitializeView();

			DataFeedView v = this.Session.CreateMultiRowDataFeedView(this.Scenario, this.ControllingScenario);
			this.PanelFlowOrder.Controls.Add(v);
		}

		public override void LoadDataFeed(DataFeed dataFeed)
		{
			base.LoadDataFeed(dataFeed);

			this.SetCheckBoxBinding(this.CheckBoxApplyBeforeTransitions, Strings.DATASHEET_FLOW_ORDER_OPTIONS, Strings.DATASHEET_FLOW_ORDER_OPTIONS_ABT_COLUMN_NAME);
			this.SetCheckBoxBinding(this.CheckBoxApplyEquallyRankedFlowsSimultaneously, Strings.DATASHEET_FLOW_ORDER_OPTIONS, Strings.DATASHEET_FLOW_ORDER_OPTIONS_AERS_COLUMN_NAME);

			DataFeedView v = (DataFeedView)this.PanelFlowOrder.Controls[0];
			v.LoadDataFeed(dataFeed, Strings.DATASHEET_FLOW_ORDER);
		}

		public override void EnableView(bool enable)
		{
			if (this.PanelFlowOrder.Controls.Count > 0)
			{
				DataFeedView v = (DataFeedView)this.PanelFlowOrder.Controls[0];
				v.EnableView(enable);
			}

			this.LabelFlowOrder.Enabled = enable;
			this.CheckBoxApplyBeforeTransitions.Enabled = enable;
			this.CheckBoxApplyEquallyRankedFlowsSimultaneously.Enabled = enable;
		}
	}
}