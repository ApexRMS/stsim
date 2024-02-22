// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.Collections.Generic;
using SyncroSim.Core;
using SyncroSim.Core.Forms;
using SyncroSim.Apex.Forms;
using System.Reflection;

namespace SyncroSim.STSim
{
	[ObfuscationAttribute(Exclude=true, ApplyToMembers=false)]
	internal partial class FlowPathwayDataFeedView
	{
		public FlowPathwayDataFeedView()
		{
			InitializeComponent();
		}

		private bool m_IsEnabled;
		private readonly ToolTip m_TooltipZoomOut = new ToolTip();
		private readonly ToolTip m_TooltipZoomIn = new ToolTip();
		private readonly FlowPathwayTabStripItem m_DiagramTab = new FlowPathwayTabStripItem("Diagram");
		private readonly FlowPathwayTabStripItem m_StockTab = new FlowPathwayTabStripItem("Stocks");
		private readonly FlowPathwayTabStripItem m_FlowTab = new FlowPathwayTabStripItem("Flows");
		private FlowPathwayDiagramFilterCriteria m_FilterCriteria = new FlowPathwayDiagramFilterCriteria();

		protected override void InitializeView()
		{
			base.InitializeView();

			this.InitializeToolTips();
			this.InitializeCommands();

			this.TabStripMain.Items.Add(this.m_DiagramTab);
			this.TabStripMain.Items.Add(this.m_StockTab);
			this.TabStripMain.Items.Add(this.m_FlowTab);

			this.SplitContainerTabStrip.SplitterWidth = 8;
			this.SplitContainerTabStrip.SplitterDistance = 300;

			SplitContainerTabStrip.Paint += new System.Windows.Forms.PaintEventHandler(OnPaintSplitContainer);
			TabStripMain.SelectedItemChanging += OnSelectedTabItemChanging;
			TabStripMain.SelectedItemChanged += OnSelectedTabItemChanged;
			ScrollBarVertical.Scroll += new System.Windows.Forms.ScrollEventHandler(OnVerticalScroll);
			ScrollBarHorizontal.Scroll += new System.Windows.Forms.ScrollEventHandler(OnHorizontalScroll);
			ButtonZoomIn.Click += new System.EventHandler(ZoomIn);
			ButtonZoomOut.Click += new System.EventHandler(ZoomOut);

			this.Padding = new Padding(0, 0, 0, 1);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && !this.IsDisposed)
			{
				this.m_TooltipZoomOut.Dispose();
				this.m_TooltipZoomIn.Dispose();

				FlowPathwayDiagram d = this.GetFlowDiagram();

				if (d != null)
				{
					d.ZoomChanged -= OnDiagramZoomChanged;
					d.MouseDoubleClick -= OnDiagramMouseDoubleClick;
				}

				if (components != null)
				{
					components.Dispose();
				}

				this.m_DiagramTab.Dispose();
				this.m_StockTab.Dispose();
				this.m_FlowTab.Dispose();
			}

			base.Dispose(disposing);
		}

		public override void LoadDataFeed(DataFeed dataFeed)
		{
			base.LoadDataFeed(dataFeed);

			this.TabStripMain.SelectItem(0);
			this.SyncronizeFilterCriteria();
		}

		public override void EnableView(bool enable)
		{
			this.m_IsEnabled = enable;
			FlowPathwayDiagram d = (FlowPathwayDiagram)this.m_DiagramTab.Control;

			if (d != null)
			{
				d.IsReadOnly = (!this.m_IsEnabled);
				d.ConfigureShapeReadOnlySetting();
			}
		}

		private FlowPathwayDiagram GetFlowDiagram()
		{
			return (FlowPathwayDiagram)this.m_DiagramTab.Control;
		}

		private void InitializeCommands()
		{
			Command CmdOpen = new Command("stock_flow_open_stock_types", "Open", null, OnExecuteOpenCommand, OnUpdateOpenCommand);
			CmdOpen.IsBold = true;
			this.Commands.Add(CmdOpen);

			this.Commands.Add(Command.CreateSeparatorCommand());
			this.Commands.Add(new Command("ssim_delete", "Delete", Properties.Resources.Delete16x16, OnExecuteDeleteCommand, OnUpdateDeleteCommand));
			this.Commands.Add(Command.CreateSeparatorCommand());
			this.Commands.Add(new Command("ssim_select_all", "Select All", OnExecuteSelectAllCommand, OnUpdateSelectAllCommand));
			this.Commands.Add(Command.CreateSeparatorCommand());
			this.Commands.Add(new Command("stock_flow_show_grid", "Show Grid", OnExecuteShowGridCommand, OnUpdateShowGridCommand));
			this.Commands.Add(new Command("stock_flow_show_tooltips", "Show Tooltips", OnExecuteShowTooltipsCommand, OnUpdateShowTooltipsCommand));
			this.Commands.Add(Command.CreateSeparatorCommand());
			this.Commands.Add(new Command("stock_flow_add_stock", "Add Stock...", OnExecuteAddStockCommand, OnUpdateAddStockCommand));
			this.Commands.Add(Command.CreateSeparatorCommand());
			this.Commands.Add(new Command("stock_flow_filter_flow_types", "Filter Flow Types...", Properties.Resources.Filter16x16, OnExecuteFilterFlowsCommand, OnUpdateFilterFlowsCommand));
		}

		protected override void OnRowsAdded(object sender, DataSheetRowEventArgs e)
		{
			base.OnRowsAdded(sender, e);

			this.SyncronizeFilterCriteria(sender);
			this.GetFlowDiagram().RefreshDiagram();
		}

		protected override void OnRowsDeleted(object sender, DataSheetRowEventArgs e)
		{
			base.OnRowsDeleted(sender, e);

			this.SyncronizeFilterCriteria(sender);
			this.GetFlowDiagram().RefreshDiagram();
		}

		protected override void OnRowsModified(object sender, DataSheetRowEventArgs e)
		{
			base.OnRowsModified(sender, e);
			this.GetFlowDiagram().RefreshDiagram();
		}

		protected override void OnResize(System.EventArgs e)
		{
			base.OnResize(e);
			this.ResetScrollbars();
		}

		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint(e);

			Pen p = Pens.Silver;

			e.Graphics.DrawLine(p, 0, 0, this.Bounds.Width - 1, 0);
			e.Graphics.DrawLine(p, this.Bounds.Width - 1, 0, this.Bounds.Width - 1, this.Bounds.Height - 1);
			e.Graphics.DrawLine(p, this.Bounds.Width - 1, this.Bounds.Height - 1, 0, this.Bounds.Height - 1);
			e.Graphics.DrawLine(p, 0, this.Bounds.Height - 1, 0, 0);
		}

		private void OnPaintSplitContainer(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Rectangle rc = new Rectangle(this.SplitContainerTabStrip.SplitterRectangle.Left, this.SplitContainerTabStrip.SplitterRectangle.Top, this.SplitContainerTabStrip.SplitterRectangle.Width - 1, this.SplitContainerTabStrip.SplitterRectangle.Height - 1);
			System.Drawing.Drawing2D.LinearGradientBrush brs = new System.Drawing.Drawing2D.LinearGradientBrush(rc, Color.SteelBlue, Color.White, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);

			e.Graphics.FillRectangle(brs, rc);
			e.Graphics.DrawRectangle(Pens.SteelBlue, rc);
			rc.Inflate(-1, -1);
			e.Graphics.DrawRectangle(Pens.White, rc);

			brs.Dispose();
		}

		private void OnSelectedTabItemChanging(object sender, SelectedTabStripItemChangingEventArgs e)
		{
			if ((this.TabStripMain.SelectedItem == null) || (this.PanelControlHost.Controls.Count == 0))
			{
				return;
			}

			if (this.TabStripMain.SelectedItem != this.m_DiagramTab)
			{
				SyncroSimView v = (SyncroSimView)this.PanelControlHost.Controls[0];

				if (!v.Validate())
				{
					e.Cancel = true;
				}
			}
		}

		private void OnSelectedTabItemChanged(object sender, SelectedTabStripItemChangedEventArgs e)
		{
			using (HourGlass h = new HourGlass())
			{            
				if (this.TabStripMain.SelectedItem == this.m_DiagramTab)
				{
					this.SwitchToDiagramView();
				}
				else
				{
					this.SwitchToGridView();
				}
			}
		}

		private void OnVerticalScroll(object sender, System.Windows.Forms.ScrollEventArgs e)
		{
			if (e.NewValue == e.OldValue)
			{
				return;
			}

			if (this.TabStripMain.SelectedItem == this.m_DiagramTab)
			{
				FlowPathwayDiagram d = this.GetFlowDiagram();

				d.VerticalScrollValue = e.NewValue;
				d.Invalidate();
			}
		}

		private void OnHorizontalScroll(object sender, System.Windows.Forms.ScrollEventArgs e)
		{
			if (e.NewValue == e.OldValue)
			{
				return;
			}

			if (this.TabStripMain.SelectedItem == this.m_DiagramTab)
			{
				FlowPathwayDiagram d = this.GetFlowDiagram();

				d.HorizontalScrollValue = e.NewValue;
				d.Invalidate();
			}
		}

		private void OnDiagramMouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (this.GetFlowDiagram().CanOpenStockTypes())
			{
				this.OpenSelectedStockTypes();
			}
		}

		private void OnDiagramZoomChanged(object sender, EventArgs e)
		{
			this.ResetScrollbars();
		}

		private void ZoomIn(object sender, System.EventArgs e)
		{
			this.GetFlowDiagram().ZoomIn();
		}

		private void ZoomOut(object sender, System.EventArgs e)
		{
			this.GetFlowDiagram().ZoomOut();
		}

		private void OnExecuteOpenCommand(Command cmd)
		{
			this.OpenSelectedStockTypes();
		}

		private void OnUpdateOpenCommand(Command cmd)
		{
			cmd.IsEnabled = this.GetFlowDiagram().CanOpenStockTypes();
		}

		private void OnExecuteDeleteCommand(Command cmd)
		{
			if (FormsUtilities.ApplicationMessageBox(
                "Are you sure you want to delete the selected stock types and associated flows?", 
                MessageBoxButtons.YesNo) != DialogResult.Yes)
			{
				return;
			}

			using (HourGlass h = new HourGlass())
			{
				this.GetFlowDiagram().DeleteSelectedStockTypeShapes();
			}
		}

		private void OnUpdateDeleteCommand(Command cmd)
		{
			cmd.IsEnabled = this.GetFlowDiagram().CanCutStockTypes();
		}

		private void OnExecuteSelectAllCommand(Command cmd)
		{
			this.GetFlowDiagram().SelectAllShapes();
		}

		private void OnUpdateSelectAllCommand(Command cmd)
		{
			cmd.IsEnabled = (this.m_IsEnabled && this.GetFlowDiagram().Shapes.Count() > 0);
		}

		private void OnExecuteShowGridCommand(Command cmd)
		{
			FlowPathwayDiagram d = this.GetFlowDiagram();
			d.ShowGrid = (!d.ShowGrid);
		}

		private void OnUpdateShowGridCommand(Command cmd)
		{
			cmd.IsEnabled = true;
			cmd.IsChecked = (this.GetFlowDiagram().ShowGrid);
		}

		private void OnExecuteShowTooltipsCommand(Command cmd)
		{
			FlowPathwayDiagram d = this.GetFlowDiagram();
			d.ShowToolTips = (!d.ShowToolTips);
		}

		private void OnUpdateShowTooltipsCommand(Command cmd)
		{
			cmd.IsEnabled = true;
			cmd.IsChecked = (this.GetFlowDiagram().ShowToolTips);
		}

		private void OnExecuteAddStockCommand(Command cmd)
		{
			DataSheet ds = this.Project.GetDataSheet(Strings.DATASHEET_STOCK_TYPE_NAME);
			DataView dv = new DataView(ds.GetData(), null, ds.DisplayMember, DataViewRowState.CurrentRows);

			if (dv.Count == 0)
			{
				FormsUtilities.InformationMessageBox("No stock types have been defined.");
				return;
			}

			FlowPathwayDiagram d = GetFlowDiagram();
			ChooseStockTypeForm f = new ChooseStockTypeForm();

			f.Initialize(d, this.Project);

			if (f.ShowDialog(this) == DialogResult.OK)
			{
				using (HourGlass h = new HourGlass())
				{
					d.AddStockTypeShape(f.StockTypeId);
				}
			}
		}

		private void OnUpdateAddStockCommand(Command cmd)
		{
			cmd.IsEnabled = this.m_IsEnabled;
		}

		private void OnExecuteFilterFlowsCommand(Command cmd)
		{
			FilterFlowTypesForm dlg = new FilterFlowTypesForm();
			DataSheet ds = this.Project.GetDataSheet(Strings.DATASHEET_FLOW_TYPE_NAME);
			DataTable dt = ds.GetData();

			dlg.CheckBoxPanelMain.Initialize();
			dlg.CheckBoxPanelMain.BeginAddItems();

			foreach (DataRow dr in dt.Rows)
			{
				if (dr.RowState != DataRowState.Deleted)
				{
					int Id = Convert.ToInt32(dr[ds.ValueMember], CultureInfo.InvariantCulture);
					string Name = Convert.ToString(dr[ds.DisplayMember], CultureInfo.InvariantCulture);
					bool IsSelected = this.m_FilterCriteria.FlowTypes[Id];

					dlg.CheckBoxPanelMain.AddItem(IsSelected, Id, Name);
				}
			}

			dlg.CheckBoxPanelMain.EndAddItems();
			dlg.CheckBoxPanelMain.TitleBarText = "Flow Types";

			if (dlg.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}

			foreach (DataRow dr in dlg.CheckBoxPanelMain.DataSource.Rows)
			{
				this.m_FilterCriteria.FlowTypes[
                    Convert.ToInt32(dr["ItemID"], CultureInfo.InvariantCulture)] = 
                    Convert.ToBoolean(dr["IsSelected"], CultureInfo.InvariantCulture);
			}

			this.GetFlowDiagram().FilterFlowPathways(this.m_FilterCriteria);
		}

		private void OnUpdateFilterFlowsCommand(Command cmd)
		{
			cmd.IsEnabled = (this.GetFlowDiagram().Shapes.Count() > 0);
		}

		private void InitializeToolTips()
		{
			this.m_TooltipZoomOut.SetToolTip(this.ButtonZoomOut, "Zoom Out");
			this.m_TooltipZoomIn.SetToolTip(this.ButtonZoomIn, "Zoom In");      
		}

		private void ResetScrollbars()
		{
			if (this.TabStripMain.SelectedItem == null)
			{
				return;
			}

			if (this.TabStripMain.SelectedItem != this.m_DiagramTab)
			{
				return;
			}

			if (this.m_DiagramTab.Control == null)
			{
				return;
			}

			this.ResetHorizontalScrollbar();
			this.ResetVerticalScrollbar();
		}

		private void ResetHorizontalScrollbar()
		{
			BoxArrowDiagram d = this.GetFlowDiagram();

			float zoom = d.Zoom;
			float diagwid = d.WorkspaceRectangle.Width * zoom;
			float clientwid = d.ClientSize.Width;
			int Extra = Convert.ToInt32(30 * zoom);

			if (clientwid >= diagwid)
			{
				this.ScrollBarHorizontal.Enabled = false;
				this.ScrollBarHorizontal.Value = 0;
				d.HorizontalScrollValue = 0;
			}
			else
			{
				this.ScrollBarHorizontal.Enabled = true;

				this.ScrollBarHorizontal.Minimum = 0;
				this.ScrollBarHorizontal.Maximum = Convert.ToInt32(diagwid - clientwid + Extra);
				this.ScrollBarHorizontal.SmallChange = Convert.ToInt32(d.CellSize * zoom);
				this.ScrollBarHorizontal.LargeChange = Convert.ToInt32(d.CellSize * zoom);

				if (this.ScrollBarHorizontal.SmallChange == 0)
				{
					this.ScrollBarHorizontal.SmallChange = 1;
				}

				if (this.ScrollBarHorizontal.LargeChange == 0)
				{
					this.ScrollBarHorizontal.LargeChange = 1;
				}

				this.ScrollBarHorizontal.Maximum += Convert.ToInt32(this.ScrollBarHorizontal.LargeChange);

				if (d.HorizontalScrollValue <= ScrollBarHorizontal.Maximum)
				{
					d.HorizontalScrollValue = d.HorizontalScrollValue;
				}
			}       
		}

		private void ResetVerticalScrollbar()
		{
			BoxArrowDiagram d = this.GetFlowDiagram();
			float zoom = d.Zoom;
			float diaghgt = d.WorkspaceRectangle.Height * zoom;
			float clienthgt = d.ClientSize.Height;
			int Extra = Convert.ToInt32(30 * zoom);

			if (clienthgt >= diaghgt)
			{
				this.ScrollBarVertical.Enabled = false;
				this.ScrollBarVertical.Value = 0;
				d.VerticalScrollValue = 0;
			}
			else
			{
				this.ScrollBarVertical.Enabled = true;
				this.ScrollBarVertical.Minimum = 0;
				this.ScrollBarVertical.Maximum = Convert.ToInt32(diaghgt - clienthgt + Extra);
				this.ScrollBarVertical.SmallChange = Convert.ToInt32(d.CellSize * zoom);
				this.ScrollBarVertical.LargeChange = Convert.ToInt32(d.CellSize * zoom);

				if (this.ScrollBarVertical.SmallChange == 0)
				{
					this.ScrollBarVertical.SmallChange = 1;
				}

				if (this.ScrollBarVertical.LargeChange == 0)
				{
					this.ScrollBarVertical.LargeChange = 1;
				}

				this.ScrollBarVertical.Maximum += Convert.ToInt32(this.ScrollBarVertical.LargeChange);

				if (d.VerticalScrollValue <= ScrollBarVertical.Maximum)
				{
					d.VerticalScrollValue = d.VerticalScrollValue;
				}           
			}
		}

		private void OpenSelectedStockTypes()
		{    
			List<int> lst = new List<int>();
			string title = this.CreateQuickViewTitle();
			string tag = this.CreateQuickViewTag();
			WinFormSession sess = (WinFormSession)this.Project.Library.Session;

			if (sess.Application.GetView(tag) != null)
			{
				sess.Application.ActivateView(tag);
			}
			else
			{
				using (HourGlass h = new HourGlass())
				{
					FlowPathwayDiagram d = this.GetFlowDiagram();

					foreach (StockTypeShape s in d.SelectedShapes)
					{
						lst.Add(s.StockTypeId);
					}

					StockTypeQuickView v = (StockTypeQuickView)this.Session.CreateDataFeedView(
                        typeof(StockTypeQuickView), this.Library, this.Project, this.Scenario, null);

					v.LoadStockTypes(this.DataFeed, lst);
					sess.Application.HostView(v, title, tag);
				}
			}      
		}

		private string CreateQuickViewTitle()
		{
			StringBuilder sb = new StringBuilder();
			FlowPathwayDiagram d = this.GetFlowDiagram();
			DataSheet ds = this.DataFeed.Project.GetDataSheet(Strings.DATASHEET_STOCK_TYPE_NAME);

			sb.AppendFormat(CultureInfo.InvariantCulture, "{0} - ", this.DataFeed.Scenario.DisplayName);

			foreach (StockTypeShape s in d.SelectedShapes)
			{
                object v = DataTableUtilities.GetTableValue(ds.GetData(), ds.ValueMember, s.StockTypeId, ds.DisplayMember);
				sb.AppendFormat(CultureInfo.InvariantCulture, "{0},", Convert.ToString(v, CultureInfo.InvariantCulture));
			}

			return sb.ToString().Trim(',');
		}

		private string CreateQuickViewTag()
		{
			StringBuilder sb = new StringBuilder();
			List<int> lst = new List<int>();
			FlowPathwayDiagram d = this.GetFlowDiagram();
			DataSheet ds = this.DataFeed.Project.GetDataSheet(Strings.DATASHEET_STOCK_TYPE_NAME);

			foreach (StockTypeShape s in d.SelectedShapes)
			{
				lst.Add(s.StockTypeId);
			}

			lst.Sort();

			foreach (int i in lst)
			{
				foreach (StockTypeShape s in d.SelectedShapes)
				{
					if (s.StockTypeId == i)
					{
                        object v = DataTableUtilities.GetTableValue(ds.GetData(), ds.ValueMember, s.StockTypeId, ds.DisplayMember);
						string DisplayValue = Convert.ToString(v, CultureInfo.InvariantCulture);
						sb.AppendFormat(CultureInfo.InvariantCulture, "{0}-{1}:", s.StockTypeId, DisplayValue);
					}
				}
			}

			string k1 = sb.ToString().Trim(':');
			return string.Format(CultureInfo.InvariantCulture, "{0}:{1}", this.Project.Library.Connection.ConnectionString, k1);
		}

		private void SyncronizeFilterCriteria(object sender)
		{
			DataSheet ds = (DataSheet)sender;

			if (ds.Name == Strings.DATASHEET_FLOW_TYPE_NAME || ds.Name == Strings.DATASHEET_FLOW_PATHWAY_NAME)
			{
				this.SyncronizeFilterCriteria();
			}
		}

		private void SyncronizeFilterCriteria()
		{
			FlowPathwayDiagramFilterCriteria cr = new FlowPathwayDiagramFilterCriteria();
			DataSheet ds = this.Project.GetDataSheet(Strings.DATASHEET_FLOW_TYPE_NAME);
			DataTable dt = ds.GetData();

			foreach (DataRow dr in dt.Rows)
			{
				if (dr.RowState != DataRowState.Deleted)
				{
					cr.FlowTypes.Add(Convert.ToInt32(dr[ds.ValueMember], CultureInfo.InvariantCulture), true);
				}
			}

			foreach (int tg in this.m_FilterCriteria.FlowTypes.Keys)
			{
				if (cr.FlowTypes.ContainsKey(tg))
				{
					cr.FlowTypes[tg] = this.m_FilterCriteria.FlowTypes[tg];
				}
			}

			this.m_FilterCriteria = cr;
		}

		private void SwitchToDiagramView()
		{
			FlowPathwayDiagram d = null;

			if (this.m_DiagramTab.Control == null)
			{
				d = new FlowPathwayDiagram();

				d.Load(this.DataFeed);
				d.IsReadOnly = (!this.m_IsEnabled);
				d.ConfigureShapeReadOnlySetting();

				d.ZoomChanged += this.OnDiagramZoomChanged;
				d.MouseDoubleClick += this.OnDiagramMouseDoubleClick;

				this.m_DiagramTab.Control = d;
			}
			else
			{
				d = this.GetFlowDiagram();
			}

			this.ButtonZoomIn.Enabled = true;
			this.ButtonZoomOut.Enabled = true;
			this.ScrollBarVertical.Visible = true;
			this.ScrollBarHorizontal.Enabled = true;
			this.ScrollBarVertical.Value = d.VerticalScrollValue;
			this.ScrollBarHorizontal.Value = d.HorizontalScrollValue;
			this.PanelControlHost.Width = this.PanelBottomControls.Width - this.ScrollBarVertical.Width - 2;

			this.ResetScrollbars();
			this.SetCurrentControl();
		}

		private void SwitchToGridView()
		{
			if (this.TabStripMain.SelectedItem == this.m_StockTab)
			{
				if (this.m_StockTab.Control == null)
				{
					MultiRowDataFeedView v = this.Session.CreateMultiRowDataFeedView(this.Scenario, this.ControllingScenario);

					v.LoadDataFeed(DataFeed, Strings.DATASHEET_FLOW_PATHWAY_DIAGRAM_NAME);
					v.EnableView(this.m_IsEnabled);

					this.m_StockTab.Control = v;
				}
			}
			else
			{
				if (this.m_FlowTab.Control == null)
				{
					MultiRowDataFeedView v = this.Session.CreateMultiRowDataFeedView(this.Scenario, this.ControllingScenario);

					v.LoadDataFeed(DataFeed, Strings.DATASHEET_FLOW_PATHWAY_NAME);
					v.EnableView(this.m_IsEnabled);

					this.m_FlowTab.Control = v;
				}
			}

			this.ButtonZoomIn.Enabled = false;
			this.ButtonZoomOut.Enabled = false;
			this.PanelControlHost.Width = this.PanelBottomControls.Width;
			this.ScrollBarVertical.Visible = false;
			this.ScrollBarHorizontal.Enabled = false;

			this.SetCurrentControl();
		}

		private void SetCurrentControl()
		{
			Control c = ((FlowPathwayTabStripItem)this.TabStripMain.SelectedItem).Control;

			this.PanelControlHost.Controls.Clear();
			this.PanelControlHost.Controls.Add(c);

			c.Dock = DockStyle.Fill;
			c.Parent = this.PanelControlHost;
		}  
	}
}