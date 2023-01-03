// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal partial class SpatialMultiplierDataFeedView : SyncroSim.Core.Forms.DataFeedView
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
            this.PanelMultipliersGrid = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            //
            //PanelMultipliersGrid
            //
            this.PanelMultipliersGrid.BackColor = System.Drawing.Color.White;
            this.PanelMultipliersGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMultipliersGrid.Location = new System.Drawing.Point(0, 0);
            this.PanelMultipliersGrid.Name = "PanelMultipliersGrid";
            this.PanelMultipliersGrid.Size = new System.Drawing.Size(432, 241);
            this.PanelMultipliersGrid.TabIndex = 1;
            //
            //SpatialMultiplierDataFeedView
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PanelMultipliersGrid);
            this.Name = "SpatialMultiplierDataFeedView";
            this.Size = new System.Drawing.Size(432, 241);
            this.ResumeLayout(false);
        }
        internal System.Windows.Forms.Panel PanelMultipliersGrid;
    }
}
