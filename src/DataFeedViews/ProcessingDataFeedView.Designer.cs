// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal partial class ProcessingDataFeedView : SyncroSim.Core.Forms.DataFeedView
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
            this.CheckBoxSplitSecStrat = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            //
            //CheckBoxSplitSecStrat
            //
            this.CheckBoxSplitSecStrat.AutoSize = true;
            this.CheckBoxSplitSecStrat.Location = new System.Drawing.Point(13, 13);
            this.CheckBoxSplitSecStrat.Name = "CheckBoxSplitSecStrat";
            this.CheckBoxSplitSecStrat.Size = new System.Drawing.Size(255, 17);
            this.CheckBoxSplitSecStrat.TabIndex = 0;
            this.CheckBoxSplitSecStrat.Text = "Split jobs for non-spatial runs by secondary strata";
            this.CheckBoxSplitSecStrat.UseVisualStyleBackColor = true;
            //
            //ProcessingDataFeedView
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CheckBoxSplitSecStrat);
            this.Name = "ProcessingDataFeedView";
            this.Size = new System.Drawing.Size(344, 160);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        internal System.Windows.Forms.CheckBox CheckBoxSplitSecStrat;
    }
}
