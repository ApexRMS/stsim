// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal partial class StateClassQuickView : SyncroSim.Core.Forms.DataFeedView
    {
        //Required by the Windows Form Designer
        private System.ComponentModel.IContainer components = null;

        //NOTE: The following procedure is required by the Windows Form Designer
        //It can be modified using the Windows Form Designer.  
        //Do not modify it using the code editor.
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.LabelDeterministic = new System.Windows.Forms.Label();
            this.SplitContainerMain = new System.Windows.Forms.SplitContainer();
            this.PanelDeterministic = new System.Windows.Forms.Panel();
            this.LabelProbabilistic = new System.Windows.Forms.Label();
            this.PanelProbabilistic = new System.Windows.Forms.Panel();
            this.ContextMenuStripDeterministic = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemTransitionsToDeterministic = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemTransitionsFromDeterministic = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemIterationDeterministic = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemTimestepDeterministic = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemStratumDeterministic = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemToStratumDeterministic = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemToClassDetreministic = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemAgeMinDeterministic = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemAgeMaxDeterministic = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerMain)).BeginInit();
            this.SplitContainerMain.Panel1.SuspendLayout();
            this.SplitContainerMain.Panel2.SuspendLayout();
            this.SplitContainerMain.SuspendLayout();
            this.ContextMenuStripDeterministic.SuspendLayout();
            this.SuspendLayout();
            // 
            // LabelDeterministic
            // 
            this.LabelDeterministic.AutoSize = true;
            this.LabelDeterministic.Location = new System.Drawing.Point(7, 8);
            this.LabelDeterministic.Name = "LabelDeterministic";
            this.LabelDeterministic.Size = new System.Drawing.Size(37, 13);
            this.LabelDeterministic.TabIndex = 0;
            this.LabelDeterministic.Text = "States";
            // 
            // SplitContainerMain
            // 
            this.SplitContainerMain.BackColor = System.Drawing.SystemColors.Control;
            this.SplitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainerMain.Location = new System.Drawing.Point(0, 0);
            this.SplitContainerMain.Name = "SplitContainerMain";
            this.SplitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SplitContainerMain.Panel1
            // 
            this.SplitContainerMain.Panel1.BackColor = System.Drawing.Color.White;
            this.SplitContainerMain.Panel1.Controls.Add(this.LabelDeterministic);
            this.SplitContainerMain.Panel1.Controls.Add(this.PanelDeterministic);
            // 
            // SplitContainerMain.Panel2
            // 
            this.SplitContainerMain.Panel2.BackColor = System.Drawing.Color.White;
            this.SplitContainerMain.Panel2.Controls.Add(this.LabelProbabilistic);
            this.SplitContainerMain.Panel2.Controls.Add(this.PanelProbabilistic);
            this.SplitContainerMain.Size = new System.Drawing.Size(796, 388);
            this.SplitContainerMain.SplitterDistance = 150;
            this.SplitContainerMain.TabIndex = 4;
            // 
            // PanelDeterministic
            // 
            this.PanelDeterministic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelDeterministic.BackColor = System.Drawing.SystemColors.Control;
            this.PanelDeterministic.Location = new System.Drawing.Point(8, 30);
            this.PanelDeterministic.Name = "PanelDeterministic";
            this.PanelDeterministic.Size = new System.Drawing.Size(780, 112);
            this.PanelDeterministic.TabIndex = 3;
            // 
            // LabelProbabilistic
            // 
            this.LabelProbabilistic.AutoSize = true;
            this.LabelProbabilistic.Location = new System.Drawing.Point(7, 8);
            this.LabelProbabilistic.Name = "LabelProbabilistic";
            this.LabelProbabilistic.Size = new System.Drawing.Size(117, 13);
            this.LabelProbabilistic.TabIndex = 0;
            this.LabelProbabilistic.Text = "Probabilistic Transitions";
            // 
            // PanelProbabilistic
            // 
            this.PanelProbabilistic.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelProbabilistic.BackColor = System.Drawing.SystemColors.Control;
            this.PanelProbabilistic.Location = new System.Drawing.Point(8, 29);
            this.PanelProbabilistic.Name = "PanelProbabilistic";
            this.PanelProbabilistic.Size = new System.Drawing.Size(780, 197);
            this.PanelProbabilistic.TabIndex = 6;
            // 
            // ContextMenuStripDeterministic
            // 
            this.ContextMenuStripDeterministic.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemTransitionsToDeterministic,
            this.MenuItemTransitionsFromDeterministic,
            this.ToolStripSeparator1,
            this.MenuItemIterationDeterministic,
            this.MenuItemTimestepDeterministic,
            this.MenuItemStratumDeterministic,
            this.MenuItemToStratumDeterministic,
            this.MenuItemToClassDetreministic,
            this.MenuItemAgeMinDeterministic,
            this.MenuItemAgeMaxDeterministic});
            this.ContextMenuStripDeterministic.Name = "ContextMenuStripDeterministic";
            this.ContextMenuStripDeterministic.Size = new System.Drawing.Size(162, 208);
            // 
            // MenuItemTransitionsToDeterministic
            // 
            this.MenuItemTransitionsToDeterministic.Name = "MenuItemTransitionsToDeterministic";
            this.MenuItemTransitionsToDeterministic.Size = new System.Drawing.Size(161, 22);
            this.MenuItemTransitionsToDeterministic.Text = "Transitions To";
            this.MenuItemTransitionsToDeterministic.Click += new System.EventHandler(this.MenuItemTransitionsToDeterministic_Click);
            // 
            // MenuItemTransitionsFromDeterministic
            // 
            this.MenuItemTransitionsFromDeterministic.Name = "MenuItemTransitionsFromDeterministic";
            this.MenuItemTransitionsFromDeterministic.Size = new System.Drawing.Size(161, 22);
            this.MenuItemTransitionsFromDeterministic.Text = "Transitions From";
            this.MenuItemTransitionsFromDeterministic.Click += new System.EventHandler(this.MenuItemTransitionsFromDeterministic_Click);
            // 
            // ToolStripSeparator1
            // 
            this.ToolStripSeparator1.Name = "ToolStripSeparator1";
            this.ToolStripSeparator1.Size = new System.Drawing.Size(158, 6);
            // 
            // MenuItemIterationDeterministic
            // 
            this.MenuItemIterationDeterministic.Name = "MenuItemIterationDeterministic";
            this.MenuItemIterationDeterministic.Size = new System.Drawing.Size(161, 22);
            this.MenuItemIterationDeterministic.Text = "Iteration";
            this.MenuItemIterationDeterministic.Click += new System.EventHandler(this.MenuItemIterationDeterministic_Click);
            // 
            // MenuItemTimestepDeterministic
            // 
            this.MenuItemTimestepDeterministic.Name = "MenuItemTimestepDeterministic";
            this.MenuItemTimestepDeterministic.Size = new System.Drawing.Size(161, 22);
            this.MenuItemTimestepDeterministic.Text = "Timestep";
            this.MenuItemTimestepDeterministic.Click += new System.EventHandler(this.MenuItemTimestepDeterministic_Click);
            // 
            // MenuItemStratumDeterministic
            // 
            this.MenuItemStratumDeterministic.Name = "MenuItemStratumDeterministic";
            this.MenuItemStratumDeterministic.Size = new System.Drawing.Size(161, 22);
            this.MenuItemStratumDeterministic.Text = "Stratum";
            this.MenuItemStratumDeterministic.Click += new System.EventHandler(this.MenuItemStratumDeterministic_Click);
            // 
            // MenuItemToStratumDeterministic
            // 
            this.MenuItemToStratumDeterministic.Name = "MenuItemToStratumDeterministic";
            this.MenuItemToStratumDeterministic.Size = new System.Drawing.Size(161, 22);
            this.MenuItemToStratumDeterministic.Text = "To Stratum";
            this.MenuItemToStratumDeterministic.Click += new System.EventHandler(this.MenuItemToStratumDeterministic_Click);
            // 
            // MenuItemToClassDetreministic
            // 
            this.MenuItemToClassDetreministic.Name = "MenuItemToClassDetreministic";
            this.MenuItemToClassDetreministic.Size = new System.Drawing.Size(161, 22);
            this.MenuItemToClassDetreministic.Text = "To Class";
            this.MenuItemToClassDetreministic.Click += new System.EventHandler(this.MenuItemToClassDetreministic_Click);
            // 
            // MenuItemAgeMinDeterministic
            // 
            this.MenuItemAgeMinDeterministic.Name = "MenuItemAgeMinDeterministic";
            this.MenuItemAgeMinDeterministic.Size = new System.Drawing.Size(161, 22);
            this.MenuItemAgeMinDeterministic.Text = "Age Min";
            this.MenuItemAgeMinDeterministic.Click += new System.EventHandler(this.MenuItemAgeMinDeterministic_Click);
            // 
            // MenuItemAgeMaxDeterministic
            // 
            this.MenuItemAgeMaxDeterministic.Name = "MenuItemAgeMaxDeterministic";
            this.MenuItemAgeMaxDeterministic.Size = new System.Drawing.Size(161, 22);
            this.MenuItemAgeMaxDeterministic.Text = "Age Max";
            this.MenuItemAgeMaxDeterministic.Click += new System.EventHandler(this.MenuItemAgeMaxDeterministic_Click);
            // 
            // StateClassQuickView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SplitContainerMain);
            this.Name = "StateClassQuickView";
            this.Size = new System.Drawing.Size(796, 388);
            this.SplitContainerMain.Panel1.ResumeLayout(false);
            this.SplitContainerMain.Panel1.PerformLayout();
            this.SplitContainerMain.Panel2.ResumeLayout(false);
            this.SplitContainerMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainerMain)).EndInit();
            this.SplitContainerMain.ResumeLayout(false);
            this.ContextMenuStripDeterministic.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        internal System.Windows.Forms.Label LabelDeterministic;
        internal System.Windows.Forms.SplitContainer SplitContainerMain;
        internal System.Windows.Forms.Panel PanelDeterministic;
        internal System.Windows.Forms.Label LabelProbabilistic;
        internal System.Windows.Forms.Panel PanelProbabilistic;
        internal System.Windows.Forms.ContextMenuStrip ContextMenuStripDeterministic;
        internal System.Windows.Forms.ToolStripMenuItem MenuItemTransitionsToDeterministic;
        internal System.Windows.Forms.ToolStripMenuItem MenuItemTransitionsFromDeterministic;
        internal System.Windows.Forms.ToolStripSeparator ToolStripSeparator1;
        internal System.Windows.Forms.ToolStripMenuItem MenuItemStratumDeterministic;
        internal System.Windows.Forms.ToolStripMenuItem MenuItemAgeMinDeterministic;
        internal System.Windows.Forms.ToolStripMenuItem MenuItemAgeMaxDeterministic;
        internal System.Windows.Forms.ToolStripMenuItem MenuItemIterationDeterministic;
        internal System.Windows.Forms.ToolStripMenuItem MenuItemTimestepDeterministic;
        internal System.Windows.Forms.ToolStripMenuItem MenuItemToStratumDeterministic;
        internal System.Windows.Forms.ToolStripMenuItem MenuItemToClassDetreministic;
    }
}
