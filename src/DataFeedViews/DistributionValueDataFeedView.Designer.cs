// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal partial class DistributionValueDataFeedView : SyncroSim.Core.Forms.DataFeedView
    {
        //Required by the Windows Form Designer
        private System.ComponentModel.IContainer components = null;

        //NOTE: The following procedure is required by the Windows Form Designer
        //It can be modified using the Windows Form Designer.  
        //Do not modify it using the code editor.
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.PanelMain = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            //
            //PanelMain
            //
            this.PanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMain.Location = new System.Drawing.Point(0, 0);
            this.PanelMain.Name = "PanelMain";
            this.PanelMain.Size = new System.Drawing.Size(454, 298);
            this.PanelMain.TabIndex = 0;
            //
            //DistributionValueDataFeedView
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PanelMain);
            this.Name = "DistributionValueDataFeedView";
            this.Size = new System.Drawing.Size(454, 298);
            this.ResumeLayout(false);
        }
        internal System.Windows.Forms.Panel PanelMain;
    }
}
