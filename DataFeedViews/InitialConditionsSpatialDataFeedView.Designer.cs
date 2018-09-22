// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal partial class InitialConditionsSpatialDataFeedView : SyncroSim.Core.Forms.DataFeedView
    {
        //Required by the Windows Form Designer
        private System.ComponentModel.IContainer components = null;

        //NOTE: The following procedure is required by the Windows Form Designer
        //It can be modified using the Windows Form Designer.  
        //Do not modify it using the code editor.
        [System.Diagnostics.DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.TableLayoutPanelCalculated = new System.Windows.Forms.TableLayoutPanel();
            this.TextBoxCellAreaCalc = new System.Windows.Forms.TextBox();
            this.LabelCalcTtlAmount = new System.Windows.Forms.Label();
            this.LabelCalcCellArea = new System.Windows.Forms.Label();
            this.TextBoxTotalArea = new System.Windows.Forms.TextBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.TextBoxNumCells = new System.Windows.Forms.TextBox();
            this.CheckBoxCellSizeOverride = new System.Windows.Forms.CheckBox();
            this.LabelFiles = new System.Windows.Forms.Label();
            this.TableLayoutPanelAttributes = new System.Windows.Forms.TableLayoutPanel();
            this.Label2 = new System.Windows.Forms.Label();
            this.TextBoxNumRows = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.TextBoxNumColumns = new System.Windows.Forms.TextBox();
            this.LabelRasterCellArea = new System.Windows.Forms.Label();
            this.TextBoxCellArea = new System.Windows.Forms.TextBox();
            this.PanelInitialConditionSpatialFiles = new System.Windows.Forms.Panel();
            this.LabelValidate = new System.Windows.Forms.Label();
            this.GroupBoxAttributes = new System.Windows.Forms.GroupBox();
            this.GroupBoxCalculated = new System.Windows.Forms.GroupBox();
            this.TableLayoutPanelCalculated.SuspendLayout();
            this.TableLayoutPanelAttributes.SuspendLayout();
            this.GroupBoxAttributes.SuspendLayout();
            this.GroupBoxCalculated.SuspendLayout();
            this.SuspendLayout();
            //
            //TableLayoutPanelCalculated
            //
            this.TableLayoutPanelCalculated.BackColor = System.Drawing.Color.White;
            this.TableLayoutPanelCalculated.ColumnCount = 2;
            this.TableLayoutPanelCalculated.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117.0F));
            this.TableLayoutPanelCalculated.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 283.0F));
            this.TableLayoutPanelCalculated.Controls.Add(this.TextBoxCellAreaCalc, 1, 0);
            this.TableLayoutPanelCalculated.Controls.Add(this.LabelCalcTtlAmount, 0, 2);
            this.TableLayoutPanelCalculated.Controls.Add(this.LabelCalcCellArea, 0, 0);
            this.TableLayoutPanelCalculated.Controls.Add(this.TextBoxTotalArea, 1, 2);
            this.TableLayoutPanelCalculated.Controls.Add(this.Label7, 0, 1);
            this.TableLayoutPanelCalculated.Controls.Add(this.TextBoxNumCells, 1, 1);
            this.TableLayoutPanelCalculated.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.TableLayoutPanelCalculated.Location = new System.Drawing.Point(24, 33);
            this.TableLayoutPanelCalculated.Name = "TableLayoutPanelCalculated";
            this.TableLayoutPanelCalculated.RowCount = 3;
            this.TableLayoutPanelCalculated.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TableLayoutPanelCalculated.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TableLayoutPanelCalculated.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TableLayoutPanelCalculated.Size = new System.Drawing.Size(230, 85);
            this.TableLayoutPanelCalculated.TabIndex = 0;
            //
            //TextBoxCellAreaCalc
            //
            this.TextBoxCellAreaCalc.Location = new System.Drawing.Point(120, 3);
            this.TextBoxCellAreaCalc.Name = "TextBoxCellAreaCalc";
            this.TextBoxCellAreaCalc.ReadOnly = true;
            this.TextBoxCellAreaCalc.Size = new System.Drawing.Size(103, 20);
            this.TextBoxCellAreaCalc.TabIndex = 1;
            this.TextBoxCellAreaCalc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            //LabelCalcTtlAmount
            //
            this.LabelCalcTtlAmount.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.LabelCalcTtlAmount.AutoSize = true;
            this.LabelCalcTtlAmount.Location = new System.Drawing.Point(42, 61);
            this.LabelCalcTtlAmount.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.LabelCalcTtlAmount.Name = "LabelCalcTtlAmount";
            this.LabelCalcTtlAmount.Size = new System.Drawing.Size(72, 13);
            this.LabelCalcTtlAmount.TabIndex = 4;
            this.LabelCalcTtlAmount.Text = "Total amount:";
            this.LabelCalcTtlAmount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            //
            //LabelCalcCellArea
            //
            this.LabelCalcCellArea.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.LabelCalcCellArea.AutoSize = true;
            this.LabelCalcCellArea.Location = new System.Drawing.Point(66, 5);
            this.LabelCalcCellArea.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.LabelCalcCellArea.Name = "LabelCalcCellArea";
            this.LabelCalcCellArea.Size = new System.Drawing.Size(48, 13);
            this.LabelCalcCellArea.TabIndex = 0;
            this.LabelCalcCellArea.Text = "Cell size:";
            this.LabelCalcCellArea.TextAlign = System.Drawing.ContentAlignment.TopRight;
            //
            //TextBoxTotalArea
            //
            this.TextBoxTotalArea.Location = new System.Drawing.Point(120, 59);
            this.TextBoxTotalArea.Name = "TextBoxTotalArea";
            this.TextBoxTotalArea.ReadOnly = true;
            this.TextBoxTotalArea.Size = new System.Drawing.Size(103, 20);
            this.TextBoxTotalArea.TabIndex = 5;
            this.TextBoxTotalArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            //Label7
            //
            this.Label7.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(31, 33);
            this.Label7.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(83, 13);
            this.Label7.TabIndex = 2;
            this.Label7.Text = "Number of cells:";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            //
            //TextBoxNumCells
            //
            this.TextBoxNumCells.Location = new System.Drawing.Point(120, 31);
            this.TextBoxNumCells.Name = "TextBoxNumCells";
            this.TextBoxNumCells.ReadOnly = true;
            this.TextBoxNumCells.Size = new System.Drawing.Size(103, 20);
            this.TextBoxNumCells.TabIndex = 3;
            this.TextBoxNumCells.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            //CheckBoxCellSizeOverride
            //
            this.CheckBoxCellSizeOverride.AutoSize = true;
            this.CheckBoxCellSizeOverride.Location = new System.Drawing.Point(263, 44);
            this.CheckBoxCellSizeOverride.Margin = new System.Windows.Forms.Padding(2);
            this.CheckBoxCellSizeOverride.Name = "CheckBoxCellSizeOverride";
            this.CheckBoxCellSizeOverride.Size = new System.Drawing.Size(66, 17);
            this.CheckBoxCellSizeOverride.TabIndex = 1;
            this.CheckBoxCellSizeOverride.Text = "Override";
            this.CheckBoxCellSizeOverride.UseVisualStyleBackColor = true;
            //
            //LabelFiles
            //
            this.LabelFiles.AutoSize = true;
            this.LabelFiles.Location = new System.Drawing.Point(0, 8);
            this.LabelFiles.Name = "LabelFiles";
            this.LabelFiles.Size = new System.Drawing.Size(62, 13);
            this.LabelFiles.TabIndex = 0;
            this.LabelFiles.Text = "Raster files:";
            //
            //TableLayoutPanelAttributes
            //
            this.TableLayoutPanelAttributes.BackColor = System.Drawing.Color.White;
            this.TableLayoutPanelAttributes.ColumnCount = 2;
            this.TableLayoutPanelAttributes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 119.0F));
            this.TableLayoutPanelAttributes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0F));
            this.TableLayoutPanelAttributes.Controls.Add(this.Label2, 0, 0);
            this.TableLayoutPanelAttributes.Controls.Add(this.TextBoxNumRows, 1, 0);
            this.TableLayoutPanelAttributes.Controls.Add(this.Label1, 0, 1);
            this.TableLayoutPanelAttributes.Controls.Add(this.TextBoxNumColumns, 1, 1);
            this.TableLayoutPanelAttributes.Controls.Add(this.LabelRasterCellArea, 0, 2);
            this.TableLayoutPanelAttributes.Controls.Add(this.TextBoxCellArea, 1, 2);
            this.TableLayoutPanelAttributes.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.TableLayoutPanelAttributes.Location = new System.Drawing.Point(22, 33);
            this.TableLayoutPanelAttributes.Name = "TableLayoutPanelAttributes";
            this.TableLayoutPanelAttributes.RowCount = 3;
            this.TableLayoutPanelAttributes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TableLayoutPanelAttributes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TableLayoutPanelAttributes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TableLayoutPanelAttributes.Size = new System.Drawing.Size(230, 85);
            this.TableLayoutPanelAttributes.TabIndex = 0;
            //
            //Label2
            //
            this.Label2.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(32, 5);
            this.Label2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(84, 13);
            this.Label2.TabIndex = 0;
            this.Label2.Text = "Number of rows:";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            //
            //TextBoxNumRows
            //
            this.TextBoxNumRows.Location = new System.Drawing.Point(122, 3);
            this.TextBoxNumRows.Name = "TextBoxNumRows";
            this.TextBoxNumRows.ReadOnly = true;
            this.TextBoxNumRows.Size = new System.Drawing.Size(103, 20);
            this.TextBoxNumRows.TabIndex = 1;
            this.TextBoxNumRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            //Label1
            //
            this.Label1.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(15, 33);
            this.Label1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(101, 13);
            this.Label1.TabIndex = 2;
            this.Label1.Text = "Number of columns:";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            //
            //TextBoxNumColumns
            //
            this.TextBoxNumColumns.Location = new System.Drawing.Point(122, 31);
            this.TextBoxNumColumns.Name = "TextBoxNumColumns";
            this.TextBoxNumColumns.ReadOnly = true;
            this.TextBoxNumColumns.Size = new System.Drawing.Size(103, 20);
            this.TextBoxNumColumns.TabIndex = 3;
            this.TextBoxNumColumns.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            //LabelRasterCellArea
            //
            this.LabelRasterCellArea.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
            this.LabelRasterCellArea.AutoSize = true;
            this.LabelRasterCellArea.Location = new System.Drawing.Point(68, 61);
            this.LabelRasterCellArea.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.LabelRasterCellArea.Name = "LabelRasterCellArea";
            this.LabelRasterCellArea.Size = new System.Drawing.Size(48, 13);
            this.LabelRasterCellArea.TabIndex = 4;
            this.LabelRasterCellArea.Text = "Cell size:";
            this.LabelRasterCellArea.TextAlign = System.Drawing.ContentAlignment.TopRight;
            //
            //TextBoxCellArea
            //
            this.TextBoxCellArea.Location = new System.Drawing.Point(122, 59);
            this.TextBoxCellArea.Name = "TextBoxCellArea";
            this.TextBoxCellArea.ReadOnly = true;
            this.TextBoxCellArea.Size = new System.Drawing.Size(103, 20);
            this.TextBoxCellArea.TabIndex = 5;
            this.TextBoxCellArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            //PanelInitialConditionSpatialFiles
            //
            this.PanelInitialConditionSpatialFiles.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.PanelInitialConditionSpatialFiles.Location = new System.Drawing.Point(3, 31);
            this.PanelInitialConditionSpatialFiles.Name = "PanelInitialConditionSpatialFiles";
            this.PanelInitialConditionSpatialFiles.Size = new System.Drawing.Size(693, 190);
            this.PanelInitialConditionSpatialFiles.TabIndex = 2;
            //
            //LabelValidate
            //
            this.LabelValidate.AutoSize = true;
            this.LabelValidate.Location = new System.Drawing.Point(0, 388);
            this.LabelValidate.Name = "LabelValidate";
            this.LabelValidate.Size = new System.Drawing.Size(159, 13);
            this.LabelValidate.TabIndex = 1;
            this.LabelValidate.Text = "Validating rasters.  Please wait...";
            //
            //GroupBoxAttributes
            //
            this.GroupBoxAttributes.Controls.Add(this.TableLayoutPanelAttributes);
            this.GroupBoxAttributes.Location = new System.Drawing.Point(3, 237);
            this.GroupBoxAttributes.Name = "GroupBoxAttributes";
            this.GroupBoxAttributes.Size = new System.Drawing.Size(279, 141);
            this.GroupBoxAttributes.TabIndex = 3;
            this.GroupBoxAttributes.TabStop = false;
            this.GroupBoxAttributes.Text = "Raster file attributes";
            //
            //GroupBoxCalculated
            //
            this.GroupBoxCalculated.Controls.Add(this.TableLayoutPanelCalculated);
            this.GroupBoxCalculated.Controls.Add(this.CheckBoxCellSizeOverride);
            this.GroupBoxCalculated.Location = new System.Drawing.Point(309, 237);
            this.GroupBoxCalculated.Name = "GroupBoxCalculated";
            this.GroupBoxCalculated.Size = new System.Drawing.Size(347, 141);
            this.GroupBoxCalculated.TabIndex = 4;
            this.GroupBoxCalculated.TabStop = false;
            this.GroupBoxCalculated.Text = "Calculated values";
            //
            //InitialConditionsSpatialDataFeedView
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GroupBoxCalculated);
            this.Controls.Add(this.GroupBoxAttributes);
            this.Controls.Add(this.LabelValidate);
            this.Controls.Add(this.PanelInitialConditionSpatialFiles);
            this.Controls.Add(this.LabelFiles);
            this.Name = "InitialConditionsSpatialDataFeedView";
            this.Size = new System.Drawing.Size(699, 412);
            this.TableLayoutPanelCalculated.ResumeLayout(false);
            this.TableLayoutPanelCalculated.PerformLayout();
            this.TableLayoutPanelAttributes.ResumeLayout(false);
            this.TableLayoutPanelAttributes.PerformLayout();
            this.GroupBoxAttributes.ResumeLayout(false);
            this.GroupBoxCalculated.ResumeLayout(false);
            this.GroupBoxCalculated.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        	CheckBoxCellSizeOverride.CheckedChanged += new System.EventHandler(CheckBoxCellSizeOverride_CheckedChanged);
        	TextBoxCellAreaCalc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(TextBoxCellAreaCalc_KeyPress);
        	TextBoxCellAreaCalc.TextChanged += new System.EventHandler(TextBoxCellAreaCalc_TextChanged);
        	TextBoxCellAreaCalc.Validated += new System.EventHandler(TextBoxCellAreaCalc_Validated);
        }
        internal System.Windows.Forms.TableLayoutPanel TableLayoutPanelCalculated;
        internal System.Windows.Forms.TextBox TextBoxCellAreaCalc;
        internal System.Windows.Forms.Label LabelCalcTtlAmount;
        internal System.Windows.Forms.Label LabelCalcCellArea;
        internal System.Windows.Forms.TextBox TextBoxTotalArea;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.TextBox TextBoxNumCells;
        internal System.Windows.Forms.CheckBox CheckBoxCellSizeOverride;
        internal System.Windows.Forms.Label LabelFiles;
        internal System.Windows.Forms.TableLayoutPanel TableLayoutPanelAttributes;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox TextBoxNumRows;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox TextBoxNumColumns;
        internal System.Windows.Forms.Label LabelRasterCellArea;
        internal System.Windows.Forms.TextBox TextBoxCellArea;
        internal System.Windows.Forms.Panel PanelInitialConditionSpatialFiles;
        internal System.Windows.Forms.Label LabelValidate;
        internal System.Windows.Forms.GroupBox GroupBoxAttributes;
        internal System.Windows.Forms.GroupBox GroupBoxCalculated;
    }
}
