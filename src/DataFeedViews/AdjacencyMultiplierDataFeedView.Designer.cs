// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal partial class AdjacencyMultiplierDataFeedView : SyncroSim.Core.Forms.DataFeedView
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
            this.PanelSettings = new System.Windows.Forms.Panel();
            this.PanelSettingsLabel = new System.Windows.Forms.Panel();
            this.LabelSettings = new System.Windows.Forms.Label();
            this.PanelMultipliers = new System.Windows.Forms.Panel();
            this.PanelMultiplersLabel = new System.Windows.Forms.Panel();
            this.LabelMultipliers = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerMain)).BeginInit();
            this.SplitContainerMain.Panel1.SuspendLayout();
            this.SplitContainerMain.Panel2.SuspendLayout();
            this.SplitContainerMain.SuspendLayout();
            this.PanelSettingsLabel.SuspendLayout();
            this.PanelMultiplersLabel.SuspendLayout();
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
            this.SplitContainerMain.Panel1.Controls.Add(this.PanelSettings);
            this.SplitContainerMain.Panel1.Controls.Add(this.PanelSettingsLabel);
            this.SplitContainerMain.Panel1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 20);
            // 
            // SplitContainerMain.Panel2
            // 
            this.SplitContainerMain.Panel2.BackColor = System.Drawing.Color.White;
            this.SplitContainerMain.Panel2.Controls.Add(this.PanelMultipliers);
            this.SplitContainerMain.Panel2.Controls.Add(this.PanelMultiplersLabel);
            this.SplitContainerMain.Panel2.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.SplitContainerMain.Size = new System.Drawing.Size(747, 557);
            this.SplitContainerMain.SplitterDistance = 278;
            this.SplitContainerMain.TabIndex = 10;
            // 
            // PanelSettings
            // 
            this.PanelSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelSettings.Location = new System.Drawing.Point(0, 30);
            this.PanelSettings.Name = "PanelSettings";
            this.PanelSettings.Size = new System.Drawing.Size(747, 228);
            this.PanelSettings.TabIndex = 1;
            // 
            // PanelSettingsLabel
            // 
            this.PanelSettingsLabel.BackColor = System.Drawing.Color.White;
            this.PanelSettingsLabel.Controls.Add(this.LabelSettings);
            this.PanelSettingsLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelSettingsLabel.Location = new System.Drawing.Point(0, 0);
            this.PanelSettingsLabel.Name = "PanelSettingsLabel";
            this.PanelSettingsLabel.Size = new System.Drawing.Size(747, 30);
            this.PanelSettingsLabel.TabIndex = 0;
            // 
            // LabelSettings
            // 
            this.LabelSettings.AutoSize = true;
            this.LabelSettings.Location = new System.Drawing.Point(-1, 8);
            this.LabelSettings.Name = "LabelSettings";
            this.LabelSettings.Size = new System.Drawing.Size(147, 13);
            this.LabelSettings.TabIndex = 0;
            this.LabelSettings.Text = "Transition adjacency settings:";
            // 
            // PanelMultipliers
            // 
            this.PanelMultipliers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMultipliers.Location = new System.Drawing.Point(0, 38);
            this.PanelMultipliers.Name = "PanelMultipliers";
            this.PanelMultipliers.Size = new System.Drawing.Size(747, 237);
            this.PanelMultipliers.TabIndex = 1;
            // 
            // PanelMultiplersLabel
            // 
            this.PanelMultiplersLabel.BackColor = System.Drawing.Color.White;
            this.PanelMultiplersLabel.Controls.Add(this.LabelMultipliers);
            this.PanelMultiplersLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelMultiplersLabel.Location = new System.Drawing.Point(0, 8);
            this.PanelMultiplersLabel.Name = "PanelMultiplersLabel";
            this.PanelMultiplersLabel.Size = new System.Drawing.Size(747, 30);
            this.PanelMultiplersLabel.TabIndex = 0;
            // 
            // LabelMultipliers
            // 
            this.LabelMultipliers.AutoSize = true;
            this.LabelMultipliers.Location = new System.Drawing.Point(-1, 8);
            this.LabelMultipliers.Name = "LabelMultipliers";
            this.LabelMultipliers.Size = new System.Drawing.Size(156, 13);
            this.LabelMultipliers.TabIndex = 0;
            this.LabelMultipliers.Text = "Transition adjacency multipliers:";
            // 
            // AdjacencyMultiplierDataFeedView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.SplitContainerMain);
            this.Name = "AdjacencyMultiplierDataFeedView";
            this.Size = new System.Drawing.Size(747, 557);
            this.SplitContainerMain.Panel1.ResumeLayout(false);
            this.SplitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerMain)).EndInit();
            this.SplitContainerMain.ResumeLayout(false);
            this.PanelSettingsLabel.ResumeLayout(false);
            this.PanelSettingsLabel.PerformLayout();
            this.PanelMultiplersLabel.ResumeLayout(false);
            this.PanelMultiplersLabel.PerformLayout();
            this.ResumeLayout(false);

        }
        internal System.Windows.Forms.SplitContainer SplitContainerMain;
        internal System.Windows.Forms.Panel PanelSettings;
        internal System.Windows.Forms.Panel PanelSettingsLabel;
        internal System.Windows.Forms.Label LabelSettings;
        internal System.Windows.Forms.Panel PanelMultipliers;
        internal System.Windows.Forms.Panel PanelMultiplersLabel;
        internal System.Windows.Forms.Label LabelMultipliers;
    }
}
