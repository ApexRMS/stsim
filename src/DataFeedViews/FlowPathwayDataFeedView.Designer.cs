namespace SyncroSim.STSim
{
	internal partial class FlowPathwayDataFeedView : SyncroSim.Core.Forms.DataFeedView
	{
		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components = null;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.PanelBottomControls = new System.Windows.Forms.Panel();
			this.PanelZoomControls = new System.Windows.Forms.Panel();
			this.ButtonZoomIn = new System.Windows.Forms.Button();
			this.ButtonZoomOut = new System.Windows.Forms.Button();
			this.SplitContainerTabStrip = new System.Windows.Forms.SplitContainer();
			this.TabStripMain = new SyncroSim.Apex.Forms.TabStrip();
			this.ScrollBarHorizontal = new System.Windows.Forms.HScrollBar();
			this.ScrollBarVertical = new System.Windows.Forms.VScrollBar();
			this.PanelControlHost = new System.Windows.Forms.Panel();
			this.PanelBottomControls.SuspendLayout();
			this.PanelZoomControls.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)this.SplitContainerTabStrip).BeginInit();
			this.SplitContainerTabStrip.Panel1.SuspendLayout();
			this.SplitContainerTabStrip.Panel2.SuspendLayout();
			this.SplitContainerTabStrip.SuspendLayout();
			this.SuspendLayout();
			//
			//PanelBottomControls
			//
			this.PanelBottomControls.BackColor = System.Drawing.SystemColors.Control;
			this.PanelBottomControls.Controls.Add(this.PanelZoomControls);
			this.PanelBottomControls.Controls.Add(this.SplitContainerTabStrip);
			this.PanelBottomControls.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.PanelBottomControls.Location = new System.Drawing.Point(0, 420);
			this.PanelBottomControls.Name = "PanelBottomControls";
			this.PanelBottomControls.Size = new System.Drawing.Size(883, 20);
			this.PanelBottomControls.TabIndex = 1;
			//
			//PanelZoomControls
			//
			this.PanelZoomControls.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.PanelZoomControls.Controls.Add(this.ButtonZoomIn);
			this.PanelZoomControls.Controls.Add(this.ButtonZoomOut);
			this.PanelZoomControls.Location = new System.Drawing.Point(823, 0);
			this.PanelZoomControls.Name = "PanelZoomControls";
			this.PanelZoomControls.Size = new System.Drawing.Size(43, 20);
			this.PanelZoomControls.TabIndex = 14;
			//
			//ButtonZoomIn
			//
			this.ButtonZoomIn.BackColor = System.Drawing.SystemColors.Control;
			this.ButtonZoomIn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.ButtonZoomIn.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
			this.ButtonZoomIn.FlatAppearance.BorderSize = 0;
			this.ButtonZoomIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ButtonZoomIn.Image = SyncroSim.STSim.Properties.Resources.Plus16x16;
			this.ButtonZoomIn.Location = new System.Drawing.Point(20, 0);
			this.ButtonZoomIn.Name = "ButtonZoomIn";
			this.ButtonZoomIn.Size = new System.Drawing.Size(20, 20);
			this.ButtonZoomIn.TabIndex = 1;
			this.ButtonZoomIn.UseVisualStyleBackColor = false;
			//
			//ButtonZoomOut
			//
			this.ButtonZoomOut.BackColor = System.Drawing.SystemColors.Control;
			this.ButtonZoomOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.ButtonZoomOut.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
			this.ButtonZoomOut.FlatAppearance.BorderSize = 0;
			this.ButtonZoomOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.ButtonZoomOut.Image = SyncroSim.STSim.Properties.Resources.Minus16x16;
			this.ButtonZoomOut.Location = new System.Drawing.Point(0, 0);
			this.ButtonZoomOut.Name = "ButtonZoomOut";
			this.ButtonZoomOut.Size = new System.Drawing.Size(20, 20);
			this.ButtonZoomOut.TabIndex = 0;
			this.ButtonZoomOut.UseVisualStyleBackColor = false;
			//
			//SplitContainerTabStrip
			//
			this.SplitContainerTabStrip.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.SplitContainerTabStrip.BackColor = System.Drawing.Color.LightSteelBlue;
			this.SplitContainerTabStrip.Location = new System.Drawing.Point(0, 0);
			this.SplitContainerTabStrip.Name = "SplitContainerTabStrip";
			//
			//SplitContainerTabStrip.Panel1
			//
			this.SplitContainerTabStrip.Panel1.Controls.Add(this.TabStripMain);
			//
			//SplitContainerTabStrip.Panel2
			//
			this.SplitContainerTabStrip.Panel2.Controls.Add(this.ScrollBarHorizontal);
			this.SplitContainerTabStrip.Size = new System.Drawing.Size(822, 20);
			this.SplitContainerTabStrip.SplitterDistance = 612;
			this.SplitContainerTabStrip.SplitterWidth = 8;
			this.SplitContainerTabStrip.TabIndex = 5;
			//
			//TabStripMain
			//
			this.TabStripMain.BackColor = System.Drawing.Color.White;
			this.TabStripMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TabStripMain.Location = new System.Drawing.Point(0, 0);
			this.TabStripMain.Name = "TabStripMain";
			this.TabStripMain.Size = new System.Drawing.Size(612, 20);
			this.TabStripMain.TabIndex = 0;
			this.TabStripMain.TabStop = false;
			this.TabStripMain.Text = "TabStripMain";
			//
			//ScrollBarHorizontal
			//
			this.ScrollBarHorizontal.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ScrollBarHorizontal.Location = new System.Drawing.Point(0, 0);
			this.ScrollBarHorizontal.Name = "ScrollBarHorizontal";
			this.ScrollBarHorizontal.Size = new System.Drawing.Size(202, 20);
			this.ScrollBarHorizontal.TabIndex = 0;
			this.ScrollBarHorizontal.TabStop = true;
			//
			//ScrollBarVertical
			//
			this.ScrollBarVertical.Dock = System.Windows.Forms.DockStyle.Right;
			this.ScrollBarVertical.Location = new System.Drawing.Point(863, 0);
			this.ScrollBarVertical.Name = "ScrollBarVertical";
			this.ScrollBarVertical.Size = new System.Drawing.Size(20, 420);
			this.ScrollBarVertical.TabIndex = 1;
			this.ScrollBarVertical.TabStop = true;
			//
			//PanelControlHost
			//
			this.PanelControlHost.BackColor = System.Drawing.Color.White;
			this.PanelControlHost.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelControlHost.Location = new System.Drawing.Point(0, 0);
			this.PanelControlHost.Name = "PanelControlHost";
			this.PanelControlHost.Size = new System.Drawing.Size(863, 420);
			this.PanelControlHost.TabIndex = 0;
			this.PanelControlHost.TabStop = true;
			//
			//FlowPathwayDataFeedView
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.PanelControlHost);
			this.Controls.Add(this.ScrollBarVertical);
			this.Controls.Add(this.PanelBottomControls);
			this.Name = "FlowPathwayDataFeedView";
			this.Size = new System.Drawing.Size(883, 440);
			this.PanelBottomControls.ResumeLayout(false);
			this.PanelZoomControls.ResumeLayout(false);
			this.SplitContainerTabStrip.Panel1.ResumeLayout(false);
			this.SplitContainerTabStrip.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)this.SplitContainerTabStrip).EndInit();
			this.SplitContainerTabStrip.ResumeLayout(false);
			this.ResumeLayout(false);

			SplitContainerTabStrip.Paint += new System.Windows.Forms.PaintEventHandler(OnPaintSplitContainer);
			TabStripMain.SelectedItemChanging += OnSelectedTabItemChanging;
			TabStripMain.SelectedItemChanged += OnSelectedTabItemChanged;
			ScrollBarVertical.Scroll += new System.Windows.Forms.ScrollEventHandler(OnVerticalScroll);
			ScrollBarHorizontal.Scroll += new System.Windows.Forms.ScrollEventHandler(OnHorizontalScroll);
			ButtonZoomIn.Click += new System.EventHandler(ZoomIn);
			ButtonZoomOut.Click += new System.EventHandler(ZoomOut);
		}
		internal System.Windows.Forms.Panel PanelBottomControls;
		internal System.Windows.Forms.Panel PanelZoomControls;
		internal System.Windows.Forms.Button ButtonZoomIn;
		internal System.Windows.Forms.Button ButtonZoomOut;
		internal System.Windows.Forms.SplitContainer SplitContainerTabStrip;
		internal SyncroSim.Apex.Forms.TabStrip TabStripMain;
		internal System.Windows.Forms.HScrollBar ScrollBarHorizontal;
		internal System.Windows.Forms.VScrollBar ScrollBarVertical;
		internal System.Windows.Forms.Panel PanelControlHost;
	}
}