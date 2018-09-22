// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

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
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.TextBoxMaximum = new System.Windows.Forms.TextBox();
            this.TextBoxFrequency = new System.Windows.Forms.TextBox();
            this.TableLayoutPanelMain.SuspendLayout();
            this.SuspendLayout();
            //
            //TableLayoutPanelMain
            //
            this.TableLayoutPanelMain.ColumnCount = 3;
            this.TableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.22222F));
            this.TableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.66667F));
            this.TableLayoutPanelMain.Controls.Add(this.Label1, 0, 0);
            this.TableLayoutPanelMain.Controls.Add(this.Label3, 2, 0);
            this.TableLayoutPanelMain.Controls.Add(this.Label2, 0, 1);
            this.TableLayoutPanelMain.Controls.Add(this.TextBoxMaximum, 1, 1);
            this.TableLayoutPanelMain.Controls.Add(this.TextBoxFrequency, 1, 0);
            this.TableLayoutPanelMain.Location = new System.Drawing.Point(3, 3);
            this.TableLayoutPanelMain.Name = "TableLayoutPanelMain";
            this.TableLayoutPanelMain.RowCount = 2;
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0F));
            this.TableLayoutPanelMain.Size = new System.Drawing.Size(450, 50);
            this.TableLayoutPanelMain.TabIndex = 6;
            //
            //Label1
            //
            this.Label1.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(60, 5);
            this.Label1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(86, 13);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Age types every:";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            //Label3
            //
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(233, 5);
            this.Label3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(51, 13);
            this.Label3.TabIndex = 2;
            this.Label3.Text = "timesteps";
            //
            //Label2
            //
            this.Label2.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(27, 30);
            this.Label2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(119, 13);
            this.Label2.TabIndex = 3;
            this.Label2.Text = "Maximum reporting age:";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            //TextBoxMaximum
            //
            this.TextBoxMaximum.Location = new System.Drawing.Point(152, 28);
            this.TextBoxMaximum.Name = "TextBoxMaximum";
            this.TextBoxMaximum.Size = new System.Drawing.Size(75, 20);
            this.TextBoxMaximum.TabIndex = 4;
            this.TextBoxMaximum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            //TextBoxFrequency
            //
            this.TextBoxFrequency.Location = new System.Drawing.Point(152, 3);
            this.TextBoxFrequency.Name = "TextBoxFrequency";
            this.TextBoxFrequency.Size = new System.Drawing.Size(75, 20);
            this.TextBoxFrequency.TabIndex = 1;
            this.TextBoxFrequency.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            //AgeTypeDataFeedView
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TableLayoutPanelMain);
            this.Name = "AgeTypeDataFeedView";
            this.Size = new System.Drawing.Size(456, 55);
            this.TableLayoutPanelMain.ResumeLayout(false);
            this.TableLayoutPanelMain.PerformLayout();
            this.ResumeLayout(false);
        }
        internal System.Windows.Forms.TableLayoutPanel TableLayoutPanelMain;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox TextBoxMaximum;
        internal System.Windows.Forms.TextBox TextBoxFrequency;
    }
}
