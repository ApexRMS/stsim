namespace SyncroSim.STSim
{
	internal partial class StockTypeQuickView : SyncroSim.Core.Forms.DataFeedView
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
			this.PanelMain = new SyncroSim.Core.Forms.BasePanel();
			this.SuspendLayout();
			//
			//PanelMain
			//
			this.PanelMain.BorderColor = System.Drawing.Color.Gray;
			this.PanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelMain.Location = new System.Drawing.Point(4, 4);
			this.PanelMain.Name = "PanelMain";
			this.PanelMain.PaintBottomBorder = true;
			this.PanelMain.PaintLeftBorder = true;
			this.PanelMain.PaintRightBorder = true;
			this.PanelMain.PaintTopBorder = true;
			this.PanelMain.Size = new System.Drawing.Size(466, 203);
			this.PanelMain.TabIndex = 0;
			//
			//StockTypeQuickView
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.PanelMain);
			this.Name = "StockTypeQuickView";
			this.Padding = new System.Windows.Forms.Padding(4);
			this.Size = new System.Drawing.Size(474, 211);
			this.ResumeLayout(false);

		}
		internal SyncroSim.Core.Forms.BasePanel PanelMain;
	}
}