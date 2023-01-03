// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal partial class RunControlDataFeedView : SyncroSim.Core.Forms.DataFeedView
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
            this.CheckBoxIsSpatial = new System.Windows.Forms.CheckBox();
            this.TextBoxEndTimestep = new System.Windows.Forms.TextBox();
            this.LabelStartTimestep = new System.Windows.Forms.Label();
            this.TextBoxStartTimestep = new System.Windows.Forms.TextBox();
            this.LabelEndTimestep = new System.Windows.Forms.Label();
            this.LabelTotalIterations = new System.Windows.Forms.Label();
            this.TextBoxTotalIterations = new System.Windows.Forms.TextBox();
            this.TableLayoutPanelMain.SuspendLayout();
            this.SuspendLayout();
            //
            //TableLayoutPanelMain
            //
            this.TableLayoutPanelMain.ColumnCount = 2;
            this.TableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0F));
            this.TableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0F));
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxIsSpatial, 1, 3);
            this.TableLayoutPanelMain.Controls.Add(this.TextBoxEndTimestep, 1, 1);
            this.TableLayoutPanelMain.Controls.Add(this.LabelStartTimestep, 0, 0);
            this.TableLayoutPanelMain.Controls.Add(this.TextBoxStartTimestep, 1, 0);
            this.TableLayoutPanelMain.Controls.Add(this.LabelEndTimestep, 0, 1);
            this.TableLayoutPanelMain.Controls.Add(this.LabelTotalIterations, 0, 2);
            this.TableLayoutPanelMain.Controls.Add(this.TextBoxTotalIterations, 1, 2);
            this.TableLayoutPanelMain.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.TableLayoutPanelMain.Location = new System.Drawing.Point(3, 3);
            this.TableLayoutPanelMain.Name = "TableLayoutPanelMain";
            this.TableLayoutPanelMain.RowCount = 4;
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0F));
            this.TableLayoutPanelMain.Size = new System.Drawing.Size(386, 111);
            this.TableLayoutPanelMain.TabIndex = 1;
            //
            //CheckBoxIsSpatial
            //
            this.CheckBoxIsSpatial.AutoSize = true;
            this.CheckBoxIsSpatial.Location = new System.Drawing.Point(196, 84);
            this.CheckBoxIsSpatial.Name = "CheckBoxIsSpatial";
            this.CheckBoxIsSpatial.Size = new System.Drawing.Size(117, 17);
            this.CheckBoxIsSpatial.TabIndex = 6;
            this.CheckBoxIsSpatial.Text = "Run model spatially";
            this.CheckBoxIsSpatial.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CheckBoxIsSpatial.UseVisualStyleBackColor = true;
            //
            //TextBoxEndTimestep
            //
            this.TextBoxEndTimestep.Location = new System.Drawing.Point(196, 30);
            this.TextBoxEndTimestep.Name = "TextBoxEndTimestep";
            this.TextBoxEndTimestep.Size = new System.Drawing.Size(117, 20);
            this.TextBoxEndTimestep.TabIndex = 3;
            this.TextBoxEndTimestep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            //LabelStartTimestep
            //
            this.LabelStartTimestep.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.LabelStartTimestep.AutoSize = true;
            this.LabelStartTimestep.Location = new System.Drawing.Point(116, 5);
            this.LabelStartTimestep.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.LabelStartTimestep.Name = "LabelStartTimestep";
            this.LabelStartTimestep.Size = new System.Drawing.Size(74, 13);
            this.LabelStartTimestep.TabIndex = 0;
            this.LabelStartTimestep.Text = "Start timestep:";
            this.LabelStartTimestep.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            //TextBoxStartTimestep
            //
            this.TextBoxStartTimestep.Location = new System.Drawing.Point(196, 3);
            this.TextBoxStartTimestep.Name = "TextBoxStartTimestep";
            this.TextBoxStartTimestep.Size = new System.Drawing.Size(117, 20);
            this.TextBoxStartTimestep.TabIndex = 1;
            this.TextBoxStartTimestep.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            //LabelEndTimestep
            //
            this.LabelEndTimestep.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.LabelEndTimestep.AutoSize = true;
            this.LabelEndTimestep.Location = new System.Drawing.Point(119, 32);
            this.LabelEndTimestep.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.LabelEndTimestep.Name = "LabelEndTimestep";
            this.LabelEndTimestep.Size = new System.Drawing.Size(71, 13);
            this.LabelEndTimestep.TabIndex = 2;
            this.LabelEndTimestep.Text = "End timestep:";
            this.LabelEndTimestep.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            //LabelTotalIterations
            //
            this.LabelTotalIterations.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.LabelTotalIterations.AutoSize = true;
            this.LabelTotalIterations.Location = new System.Drawing.Point(111, 59);
            this.LabelTotalIterations.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.LabelTotalIterations.Name = "LabelTotalIterations";
            this.LabelTotalIterations.Size = new System.Drawing.Size(79, 13);
            this.LabelTotalIterations.TabIndex = 4;
            this.LabelTotalIterations.Text = "Total iterations:";
            this.LabelTotalIterations.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            //TextBoxTotalIterations
            //
            this.TextBoxTotalIterations.Location = new System.Drawing.Point(196, 57);
            this.TextBoxTotalIterations.Name = "TextBoxTotalIterations";
            this.TextBoxTotalIterations.Size = new System.Drawing.Size(117, 20);
            this.TextBoxTotalIterations.TabIndex = 5;
            this.TextBoxTotalIterations.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            //RunControlDataFeedView
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TableLayoutPanelMain);
            this.Name = "RunControlDataFeedView";
            this.Size = new System.Drawing.Size(393, 118);
            this.TableLayoutPanelMain.ResumeLayout(false);
            this.TableLayoutPanelMain.PerformLayout();
            this.ResumeLayout(false);
        }
        internal System.Windows.Forms.TableLayoutPanel TableLayoutPanelMain;
        internal System.Windows.Forms.CheckBox CheckBoxIsSpatial;
        internal System.Windows.Forms.TextBox TextBoxEndTimestep;
        internal System.Windows.Forms.Label LabelStartTimestep;
        internal System.Windows.Forms.TextBox TextBoxStartTimestep;
        internal System.Windows.Forms.Label LabelEndTimestep;
        internal System.Windows.Forms.Label LabelTotalIterations;
        internal System.Windows.Forms.TextBox TextBoxTotalIterations;
    }
}
