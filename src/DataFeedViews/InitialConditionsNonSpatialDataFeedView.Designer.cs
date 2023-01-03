// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal partial class InitialConditionsNonSpatialDataFeedView : SyncroSim.Core.Forms.DataFeedView
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
            this.CheckBoxCalcFromDist = new System.Windows.Forms.CheckBox();
            this.TableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.TextBoxCellSize = new System.Windows.Forms.TextBox();
            this.LabelCellSize = new System.Windows.Forms.Label();
            this.LabelTotalAmount = new System.Windows.Forms.Label();
            this.LabelNumCells = new System.Windows.Forms.Label();
            this.TextBoxTotalAmount = new System.Windows.Forms.TextBox();
            this.TextBoxNumCells = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.ButtonClearAll = new System.Windows.Forms.Button();
            this.PanelOptions = new System.Windows.Forms.Panel();
            this.PanelDistribution = new System.Windows.Forms.Panel();
            this.TableLayoutPanelMain.SuspendLayout();
            this.PanelOptions.SuspendLayout();
            this.SuspendLayout();
            //
            //CheckBoxCalcFromDist
            //
            this.CheckBoxCalcFromDist.AutoSize = true;
            this.CheckBoxCalcFromDist.Location = new System.Drawing.Point(366, 42);
            this.CheckBoxCalcFromDist.Name = "CheckBoxCalcFromDist";
            this.CheckBoxCalcFromDist.Size = new System.Drawing.Size(146, 17);
            this.CheckBoxCalcFromDist.TabIndex = 6;
            this.CheckBoxCalcFromDist.Text = "Calculate from distribution";
            this.CheckBoxCalcFromDist.UseVisualStyleBackColor = true;
            //
            //TableLayoutPanelMain
            //
            this.TableLayoutPanelMain.ColumnCount = 2;
            this.TableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.62395F));
            this.TableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.37605F));
            this.TableLayoutPanelMain.Controls.Add(this.TextBoxCellSize, 1, 2);
            this.TableLayoutPanelMain.Controls.Add(this.LabelCellSize, 0, 2);
            this.TableLayoutPanelMain.Controls.Add(this.LabelTotalAmount, 0, 0);
            this.TableLayoutPanelMain.Controls.Add(this.LabelNumCells, 0, 1);
            this.TableLayoutPanelMain.Controls.Add(this.TextBoxTotalAmount, 1, 0);
            this.TableLayoutPanelMain.Controls.Add(this.TextBoxNumCells, 1, 1);
            this.TableLayoutPanelMain.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.TableLayoutPanelMain.Location = new System.Drawing.Point(1, 12);
            this.TableLayoutPanelMain.Name = "TableLayoutPanelMain";
            this.TableLayoutPanelMain.RowCount = 3;
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0F));
            this.TableLayoutPanelMain.Size = new System.Drawing.Size(359, 74);
            this.TableLayoutPanelMain.TabIndex = 5;
            //
            //TextBoxCellSize
            //
            this.TextBoxCellSize.Enabled = false;
            this.TextBoxCellSize.Location = new System.Drawing.Point(234, 51);
            this.TextBoxCellSize.Name = "TextBoxCellSize";
            this.TextBoxCellSize.Size = new System.Drawing.Size(122, 20);
            this.TextBoxCellSize.TabIndex = 5;
            this.TextBoxCellSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            //LabelCellSize
            //
            this.LabelCellSize.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.LabelCellSize.AutoSize = true;
            this.LabelCellSize.Location = new System.Drawing.Point(180, 53);
            this.LabelCellSize.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.LabelCellSize.Name = "LabelCellSize";
            this.LabelCellSize.Size = new System.Drawing.Size(48, 13);
            this.LabelCellSize.TabIndex = 4;
            this.LabelCellSize.Text = "Cell size:";
            this.LabelCellSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            //LabelTotalAmount
            //
            this.LabelTotalAmount.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.LabelTotalAmount.AutoSize = true;
            this.LabelTotalAmount.Location = new System.Drawing.Point(155, 5);
            this.LabelTotalAmount.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.LabelTotalAmount.Name = "LabelTotalAmount";
            this.LabelTotalAmount.Size = new System.Drawing.Size(73, 13);
            this.LabelTotalAmount.TabIndex = 0;
            this.LabelTotalAmount.Text = "Total Amount:";
            this.LabelTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            //LabelNumCells
            //
            this.LabelNumCells.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.LabelNumCells.AutoSize = true;
            this.LabelNumCells.Location = new System.Drawing.Point(96, 29);
            this.LabelNumCells.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.LabelNumCells.Name = "LabelNumCells";
            this.LabelNumCells.Size = new System.Drawing.Size(132, 13);
            this.LabelNumCells.TabIndex = 2;
            this.LabelNumCells.Text = "Number of simulation cells:";
            this.LabelNumCells.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            //TextBoxTotalAmount
            //
            this.TextBoxTotalAmount.Location = new System.Drawing.Point(234, 3);
            this.TextBoxTotalAmount.Name = "TextBoxTotalAmount";
            this.TextBoxTotalAmount.Size = new System.Drawing.Size(122, 20);
            this.TextBoxTotalAmount.TabIndex = 1;
            this.TextBoxTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            //TextBoxNumCells
            //
            this.TextBoxNumCells.Location = new System.Drawing.Point(234, 27);
            this.TextBoxNumCells.Name = "TextBoxNumCells";
            this.TextBoxNumCells.Size = new System.Drawing.Size(122, 20);
            this.TextBoxNumCells.TabIndex = 3;
            this.TextBoxNumCells.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            //Label2
            //
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(-1, 119);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(62, 13);
            this.Label2.TabIndex = 8;
            this.Label2.Text = "Distribution:";
            //
            //ButtonClearAll
            //
            this.ButtonClearAll.Location = new System.Drawing.Point(234, 92);
            this.ButtonClearAll.Name = "ButtonClearAll";
            this.ButtonClearAll.Size = new System.Drawing.Size(123, 23);
            this.ButtonClearAll.TabIndex = 7;
            this.ButtonClearAll.Text = "Clear All";
            this.ButtonClearAll.UseVisualStyleBackColor = true;
            //
            //PanelOptions
            //
            this.PanelOptions.Controls.Add(this.TableLayoutPanelMain);
            this.PanelOptions.Controls.Add(this.ButtonClearAll);
            this.PanelOptions.Controls.Add(this.CheckBoxCalcFromDist);
            this.PanelOptions.Controls.Add(this.Label2);
            this.PanelOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelOptions.Location = new System.Drawing.Point(0, 0);
            this.PanelOptions.Name = "PanelOptions";
            this.PanelOptions.Size = new System.Drawing.Size(614, 142);
            this.PanelOptions.TabIndex = 11;
            //
            //PanelDistribution
            //
            this.PanelDistribution.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelDistribution.Location = new System.Drawing.Point(0, 142);
            this.PanelDistribution.Name = "PanelDistribution";
            this.PanelDistribution.Size = new System.Drawing.Size(614, 230);
            this.PanelDistribution.TabIndex = 12;
            //
            //InitialConditionsNonSpatialDataFeedView
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.PanelDistribution);
            this.Controls.Add(this.PanelOptions);
            this.Name = "InitialConditionsNonSpatialDataFeedView";
            this.Size = new System.Drawing.Size(614, 372);
            this.TableLayoutPanelMain.ResumeLayout(false);
            this.TableLayoutPanelMain.PerformLayout();
            this.PanelOptions.ResumeLayout(false);
            this.PanelOptions.PerformLayout();
            this.ResumeLayout(false);

            ButtonClearAll.Click += new System.EventHandler(ButtonClearAll_Click);
        }
        internal System.Windows.Forms.CheckBox CheckBoxCalcFromDist;
        internal System.Windows.Forms.TableLayoutPanel TableLayoutPanelMain;
        internal System.Windows.Forms.TextBox TextBoxCellSize;
        internal System.Windows.Forms.Label LabelCellSize;
        internal System.Windows.Forms.Label LabelTotalAmount;
        internal System.Windows.Forms.Label LabelNumCells;
        internal System.Windows.Forms.TextBox TextBoxTotalAmount;
        internal System.Windows.Forms.TextBox TextBoxNumCells;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Button ButtonClearAll;
        internal System.Windows.Forms.Panel PanelOptions;
        internal System.Windows.Forms.Panel PanelDistribution;
    }
}
