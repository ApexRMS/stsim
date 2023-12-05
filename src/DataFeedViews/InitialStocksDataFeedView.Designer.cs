namespace SyncroSim.STSim
{
	internal partial class InitialStocksDataFeedView : SyncroSim.Core.Forms.DataFeedView
	{
		//UserControl overrides dispose to clean up the component list.
		[System.Diagnostics.DebuggerNonUserCode()]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && components != null)
				{
					components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components = null;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.SplitContainerMain = new System.Windows.Forms.SplitContainer();
            this.PanelNonSpatial = new System.Windows.Forms.Panel();
            this.PanelLabelTop = new System.Windows.Forms.Panel();
            this.LabelNonSpatial = new System.Windows.Forms.Label();
            this.PanelSpatial = new System.Windows.Forms.Panel();
            this.PanelLabelBottom = new System.Windows.Forms.Panel();
            this.LabelSpatial = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerMain)).BeginInit();
            this.SplitContainerMain.Panel1.SuspendLayout();
            this.SplitContainerMain.Panel2.SuspendLayout();
            this.SplitContainerMain.SuspendLayout();
            this.PanelLabelTop.SuspendLayout();
            this.PanelLabelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // SplitContainerMain
            // 
            this.SplitContainerMain.BackColor = System.Drawing.Color.Gainsboro;
            this.SplitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.SplitContainerMain.Name = "SplitContainerMain";
            this.SplitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SplitContainerMain.Panel1
            // 
            this.SplitContainerMain.Panel1.BackColor = System.Drawing.Color.White;
            this.SplitContainerMain.Panel1.Controls.Add(this.PanelNonSpatial);
            this.SplitContainerMain.Panel1.Controls.Add(this.PanelLabelTop);
            this.SplitContainerMain.Panel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
            // 
            // SplitContainerMain.Panel2
            // 
            this.SplitContainerMain.Panel2.BackColor = System.Drawing.Color.White;
            this.SplitContainerMain.Panel2.Controls.Add(this.PanelSpatial);
            this.SplitContainerMain.Panel2.Controls.Add(this.PanelLabelBottom);
            this.SplitContainerMain.Panel2.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.SplitContainerMain.Size = new System.Drawing.Size(604, 416);
            this.SplitContainerMain.SplitterDistance = 207;
            this.SplitContainerMain.TabIndex = 11;
            // 
            // PanelNonSpatial
            // 
            this.PanelNonSpatial.BackColor = System.Drawing.SystemColors.Control;
            this.PanelNonSpatial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelNonSpatial.Location = new System.Drawing.Point(0, 30);
            this.PanelNonSpatial.Name = "PanelNonSpatial";
            this.PanelNonSpatial.Size = new System.Drawing.Size(604, 157);
            this.PanelNonSpatial.TabIndex = 1;
            // 
            // PanelLabelTop
            // 
            this.PanelLabelTop.BackColor = System.Drawing.Color.White;
            this.PanelLabelTop.Controls.Add(this.LabelNonSpatial);
            this.PanelLabelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelLabelTop.Location = new System.Drawing.Point(0, 0);
            this.PanelLabelTop.Name = "PanelLabelTop";
            this.PanelLabelTop.Size = new System.Drawing.Size(604, 30);
            this.PanelLabelTop.TabIndex = 0;
            // 
            // LabelNonSpatial
            // 
            this.LabelNonSpatial.AutoSize = true;
            this.LabelNonSpatial.Location = new System.Drawing.Point(0, 8);
            this.LabelNonSpatial.Name = "LabelNonSpatial";
            this.LabelNonSpatial.Size = new System.Drawing.Size(128, 13);
            this.LabelNonSpatial.TabIndex = 0;
            this.LabelNonSpatial.Text = "Initial stocks - non-spatial:";
            // 
            // PanelSpatial
            // 
            this.PanelSpatial.BackColor = System.Drawing.SystemColors.Control;
            this.PanelSpatial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelSpatial.Location = new System.Drawing.Point(0, 38);
            this.PanelSpatial.Name = "PanelSpatial";
            this.PanelSpatial.Size = new System.Drawing.Size(604, 167);
            this.PanelSpatial.TabIndex = 1;
            // 
            // PanelLabelBottom
            // 
            this.PanelLabelBottom.BackColor = System.Drawing.Color.White;
            this.PanelLabelBottom.Controls.Add(this.LabelSpatial);
            this.PanelLabelBottom.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelLabelBottom.Location = new System.Drawing.Point(0, 8);
            this.PanelLabelBottom.Name = "PanelLabelBottom";
            this.PanelLabelBottom.Size = new System.Drawing.Size(604, 30);
            this.PanelLabelBottom.TabIndex = 0;
            // 
            // LabelSpatial
            // 
            this.LabelSpatial.AutoSize = true;
            this.LabelSpatial.Location = new System.Drawing.Point(0, 8);
            this.LabelSpatial.Name = "LabelSpatial";
            this.LabelSpatial.Size = new System.Drawing.Size(107, 13);
            this.LabelSpatial.TabIndex = 0;
            this.LabelSpatial.Text = "Initial stocks - spatial:";
            // 
            // InitialStocksDataFeedView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SplitContainerMain);
            this.Name = "InitialStocksDataFeedView";
            this.Size = new System.Drawing.Size(604, 416);
            this.SplitContainerMain.Panel1.ResumeLayout(false);
            this.SplitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerMain)).EndInit();
            this.SplitContainerMain.ResumeLayout(false);
            this.PanelLabelTop.ResumeLayout(false);
            this.PanelLabelTop.PerformLayout();
            this.PanelLabelBottom.ResumeLayout(false);
            this.PanelLabelBottom.PerformLayout();
            this.ResumeLayout(false);

		}
		internal System.Windows.Forms.SplitContainer SplitContainerMain;
		internal System.Windows.Forms.Panel PanelNonSpatial;
		internal System.Windows.Forms.Panel PanelLabelTop;
		internal System.Windows.Forms.Label LabelNonSpatial;
		internal System.Windows.Forms.Panel PanelSpatial;
		internal System.Windows.Forms.Panel PanelLabelBottom;
		internal System.Windows.Forms.Label LabelSpatial;
	}
}