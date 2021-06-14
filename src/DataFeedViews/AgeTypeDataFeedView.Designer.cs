// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal partial class AgeTypeDataFeedView : SyncroSim.Core.Forms.DataFeedView
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
            this.TableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.Label1 = new System.Windows.Forms.Label();
            this.LabelTimesteps = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.TextBoxMaximum = new System.Windows.Forms.TextBox();
            this.TextBoxFrequency = new System.Windows.Forms.TextBox();
            this.TableLayoutPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // TableLayoutPanelMain
            // 
            this.TableLayoutPanelMain.ColumnCount = 3;
            this.TableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.22222F));
            this.TableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.66667F));
            this.TableLayoutPanelMain.Controls.Add(this.Label1, 0, 0);
            this.TableLayoutPanelMain.Controls.Add(this.LabelTimesteps, 2, 0);
            this.TableLayoutPanelMain.Controls.Add(this.Label2, 0, 1);
            this.TableLayoutPanelMain.Controls.Add(this.TextBoxMaximum, 1, 1);
            this.TableLayoutPanelMain.Controls.Add(this.TextBoxFrequency, 1, 0);
            this.TableLayoutPanelMain.Location = new System.Drawing.Point(4, 4);
            this.TableLayoutPanelMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TableLayoutPanelMain.Name = "TableLayoutPanelMain";
            this.TableLayoutPanelMain.RowCount = 2;
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TableLayoutPanelMain.Size = new System.Drawing.Size(600, 62);
            this.TableLayoutPanelMain.TabIndex = 6;
            // 
            // Label1
            // 
            this.Label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(81, 6);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(114, 17);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Age types every:";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LabelTimesteps
            // 
            this.LabelTimesteps.AutoSize = true;
            this.LabelTimesteps.Location = new System.Drawing.Point(312, 6);
            this.LabelTimesteps.Margin = new System.Windows.Forms.Padding(4, 6, 4, 0);
            this.LabelTimesteps.Name = "LabelTimesteps";
            this.LabelTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelTimesteps.TabIndex = 2;
            this.LabelTimesteps.Text = "timesteps";
            // 
            // Label2
            // 
            this.Label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(36, 37);
            this.Label2.Margin = new System.Windows.Forms.Padding(4, 6, 4, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(159, 17);
            this.Label2.TabIndex = 3;
            this.Label2.Text = "Maximum reporting age:";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TextBoxMaximum
            // 
            this.TextBoxMaximum.Location = new System.Drawing.Point(203, 35);
            this.TextBoxMaximum.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TextBoxMaximum.Name = "TextBoxMaximum";
            this.TextBoxMaximum.Size = new System.Drawing.Size(99, 22);
            this.TextBoxMaximum.TabIndex = 4;
            this.TextBoxMaximum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TextBoxFrequency
            // 
            this.TextBoxFrequency.Location = new System.Drawing.Point(203, 4);
            this.TextBoxFrequency.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TextBoxFrequency.Name = "TextBoxFrequency";
            this.TextBoxFrequency.Size = new System.Drawing.Size(99, 22);
            this.TextBoxFrequency.TabIndex = 1;
            this.TextBoxFrequency.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // AgeTypeDataFeedView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TableLayoutPanelMain);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "AgeTypeDataFeedView";
            this.Size = new System.Drawing.Size(608, 68);
            this.TableLayoutPanelMain.ResumeLayout(false);
            this.TableLayoutPanelMain.PerformLayout();
            this.ResumeLayout(false);

        }
        internal System.Windows.Forms.TableLayoutPanel TableLayoutPanelMain;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label LabelTimesteps;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox TextBoxMaximum;
        internal System.Windows.Forms.TextBox TextBoxFrequency;
    }
}
