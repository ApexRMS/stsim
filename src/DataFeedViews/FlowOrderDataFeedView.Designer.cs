namespace SyncroSim.STSim
{
	internal partial class FlowOrderDataFeedView : SyncroSim.Core.Forms.DataFeedView
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
			this.LabelFlowOrder = new System.Windows.Forms.Label();
			this.CheckBoxApplyBeforeTransitions = new System.Windows.Forms.CheckBox();
			this.CheckBoxApplyEquallyRankedFlowsSimultaneously = new System.Windows.Forms.CheckBox();
			this.PanelOptions = new System.Windows.Forms.Panel();
			this.PanelFlowOrder = new System.Windows.Forms.Panel();
			this.PanelOptions.SuspendLayout();
			this.SuspendLayout();
			//
			//LabelFlowOrder
			//
			this.LabelFlowOrder.AutoSize = true;
			this.LabelFlowOrder.Location = new System.Drawing.Point(0, 57);
			this.LabelFlowOrder.Name = "LabelFlowOrder";
			this.LabelFlowOrder.Size = new System.Drawing.Size(61, 13);
			this.LabelFlowOrder.TabIndex = 2;
			this.LabelFlowOrder.Text = "Flow Order:";
			//
			//CheckBoxApplyBeforeTransitions
			//
			this.CheckBoxApplyBeforeTransitions.AutoSize = true;
			this.CheckBoxApplyBeforeTransitions.Location = new System.Drawing.Point(2, 6);
			this.CheckBoxApplyBeforeTransitions.Name = "CheckBoxApplyBeforeTransitions";
			this.CheckBoxApplyBeforeTransitions.Size = new System.Drawing.Size(135, 17);
			this.CheckBoxApplyBeforeTransitions.TabIndex = 0;
			this.CheckBoxApplyBeforeTransitions.Text = "Apply before transitions";
			this.CheckBoxApplyBeforeTransitions.UseVisualStyleBackColor = true;
			//
			//CheckBoxApplyEquallyRankedFlowsSimultaneously
			//
			this.CheckBoxApplyEquallyRankedFlowsSimultaneously.AutoSize = true;
			this.CheckBoxApplyEquallyRankedFlowsSimultaneously.Location = new System.Drawing.Point(2, 27);
			this.CheckBoxApplyEquallyRankedFlowsSimultaneously.Name = "CheckBoxApplyEquallyRankedFlowsSimultaneously";
			this.CheckBoxApplyEquallyRankedFlowsSimultaneously.Size = new System.Drawing.Size(222, 17);
			this.CheckBoxApplyEquallyRankedFlowsSimultaneously.TabIndex = 1;
			this.CheckBoxApplyEquallyRankedFlowsSimultaneously.Text = "Apply equally ranked flows simultaneously";
			this.CheckBoxApplyEquallyRankedFlowsSimultaneously.UseVisualStyleBackColor = true;
			//
			//PanelOptions
			//
			this.PanelOptions.Controls.Add(this.CheckBoxApplyBeforeTransitions);
			this.PanelOptions.Controls.Add(this.CheckBoxApplyEquallyRankedFlowsSimultaneously);
			this.PanelOptions.Controls.Add(this.LabelFlowOrder);
			this.PanelOptions.Dock = System.Windows.Forms.DockStyle.Top;
			this.PanelOptions.Location = new System.Drawing.Point(0, 0);
			this.PanelOptions.Name = "PanelOptions";
			this.PanelOptions.Size = new System.Drawing.Size(569, 77);
			this.PanelOptions.TabIndex = 12;
			//
			//PanelFlowOrder
			//
			this.PanelFlowOrder.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelFlowOrder.Location = new System.Drawing.Point(0, 77);
			this.PanelFlowOrder.Name = "PanelFlowOrder";
			this.PanelFlowOrder.Size = new System.Drawing.Size(569, 247);
			this.PanelFlowOrder.TabIndex = 0;
			//
			//FlowOrderDataFeedView
			//
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.PanelFlowOrder);
			this.Controls.Add(this.PanelOptions);
			this.Name = "FlowOrderDataFeedView";
			this.Size = new System.Drawing.Size(569, 324);
			this.PanelOptions.ResumeLayout(false);
			this.PanelOptions.PerformLayout();
			this.ResumeLayout(false);

		}
		internal System.Windows.Forms.Label LabelFlowOrder;
		internal System.Windows.Forms.CheckBox CheckBoxApplyBeforeTransitions;
		internal System.Windows.Forms.CheckBox CheckBoxApplyEquallyRankedFlowsSimultaneously;
		internal System.Windows.Forms.Panel PanelOptions;
		internal System.Windows.Forms.Panel PanelFlowOrder;
	}
}