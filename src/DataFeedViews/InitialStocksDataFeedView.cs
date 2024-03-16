// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.IO;
using System.Reflection;
using System.Windows.Forms;
using SyncroSim.Core;
using SyncroSim.Core.Forms;
using SyncroSim.Apex.Forms;

namespace SyncroSim.STSim
{
	[ObfuscationAttribute(Exclude=true, ApplyToMembers=false)]
	internal partial class InitialStocksDataFeedView
	{
		private DataFeedView m_RasterFilesView;

		public InitialStocksDataFeedView()
		{
			InitializeComponent();
		}

		protected override void InitializeView()
		{
			base.InitializeView();

			DataFeedView v1 = this.Session.CreateMultiRowDataFeedView(this.Scenario, this.ControllingScenario);
			this.PanelNonSpatial.Controls.Add(v1);

			this.m_RasterFilesView = this.Session.CreateMultiRowDataFeedView(this.Scenario, this.ControllingScenario);
			this.PanelSpatial.Controls.Add(this.m_RasterFilesView);

			this.ConfigureContextMenu();
		}

		public override void LoadDataFeed(Core.DataFeed dataFeed)
		{
			base.LoadDataFeed(dataFeed);

			DataFeedView v1 = (DataFeedView)this.PanelNonSpatial.Controls[0];
			v1.LoadDataFeed(dataFeed, Strings.DATASHEET_INITIAL_STOCK_NON_SPATIAL);

			DataFeedView v2 = (DataFeedView)this.PanelSpatial.Controls[0];
			v2.LoadDataFeed(dataFeed, Strings.DATASHEET_INITIAL_STOCK_SPATIAL);
		}

		/// <summary>
		/// Overrides EnableView
		/// </summary>
		/// <param name="enable"></param>
		/// <remarks>
		/// We override this so that we can manually enable the nested data feed view.  If we don't do this
		/// then the user will not be abled to interact with it at all if it is disabled and this is not really
		/// what we want here.  Also, we want to have control over the enabled state of the buttons.
		/// </remarks>
		public override void EnableView(bool enable)
		{
			if (this.PanelNonSpatial.Controls.Count > 0)
			{
				DataFeedView v = (DataFeedView)this.PanelNonSpatial.Controls[0];
				v.EnableView(enable);
			}

			if (this.PanelSpatial.Controls.Count > 0)
			{
				DataFeedView v = (DataFeedView)this.PanelSpatial.Controls[0];
				v.EnableView(enable);
			}

			this.LabelNonSpatial.Enabled = enable;
			this.LabelSpatial.Enabled = enable;
		}

		/// <summary>
		/// Configures the context menu for this view
		/// </summary>
		/// <remarks>We only want a subset of the default commands, so remove the others</remarks>
		private void ConfigureContextMenu()
		{
			for (int i = this.m_RasterFilesView.Commands.Count - 1; i >= 0; i--)
			{
				Command c = this.m_RasterFilesView.Commands[i];

				if (c.Name != "ssim_delete" && c.Name != "ssim_delete_all" && c.Name != "ssim_import" && c.Name != "ssim_export_all")
				{
					if (!c.IsSeparator)
					{
						this.m_RasterFilesView.Commands.RemoveAt(i);
					}
				}

				if (c.Name == "ssim_export_all")
				{
					c.DisplayName = "Export...";
				}
			}

			this.m_RasterFilesView.RefreshContextMenuStrip();
		}
	}
}