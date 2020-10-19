// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

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
            this.TextBoxCellAreaCalc = new System.Windows.Forms.TextBox();
            this.LabelCalcTtlAmount = new System.Windows.Forms.Label();
            this.LabelCalcCellArea = new System.Windows.Forms.Label();
            this.TextBoxTotalArea = new System.Windows.Forms.TextBox();
            this.CheckBoxCellSizeOverride = new System.Windows.Forms.CheckBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.TextBoxNumCells = new System.Windows.Forms.TextBox();
            this.LabelRasterFiles = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.TextBoxNumRows = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.TextBoxNumColumns = new System.Windows.Forms.TextBox();
            this.LabelRasterCellArea = new System.Windows.Forms.Label();
            this.TextBoxCellArea = new System.Windows.Forms.TextBox();
            this.LabelAttributes = new System.Windows.Forms.Label();
            this.LableCalculated = new System.Windows.Forms.Label();
            this.PanelBottom = new System.Windows.Forms.Panel();
            this.PanelBottomContent = new System.Windows.Forms.Panel();
            this.TableCalculated = new System.Windows.Forms.TableLayoutPanel();
            this.TableAttributes = new System.Windows.Forms.TableLayoutPanel();
            this.PanelTop = new System.Windows.Forms.Panel();
            this.PanelTopContent = new System.Windows.Forms.Panel();
            this.PanelBannerTop = new System.Windows.Forms.Panel();
            this.PanelBottom.SuspendLayout();
            this.PanelBottomContent.SuspendLayout();
            this.TableCalculated.SuspendLayout();
            this.TableAttributes.SuspendLayout();
            this.PanelTop.SuspendLayout();
            this.PanelBannerTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextBoxCellAreaCalc
            // 
            this.TextBoxCellAreaCalc.BackColor = System.Drawing.Color.White;
            this.TextBoxCellAreaCalc.Location = new System.Drawing.Point(227, 5);
            this.TextBoxCellAreaCalc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 4);
            this.TextBoxCellAreaCalc.Name = "TextBoxCellAreaCalc";
            this.TextBoxCellAreaCalc.ReadOnly = true;
            this.TextBoxCellAreaCalc.Size = new System.Drawing.Size(136, 22);
            this.TextBoxCellAreaCalc.TabIndex = 0;
            this.TextBoxCellAreaCalc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TextBoxCellAreaCalc.TextChanged += new System.EventHandler(this.TextBoxCellAreaCalc_TextChanged);
            this.TextBoxCellAreaCalc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxCellAreaCalc_KeyPress);
            this.TextBoxCellAreaCalc.Validated += new System.EventHandler(this.TextBoxCellAreaCalc_Validated);
            // 
            // LabelCalcTtlAmount
            // 
            this.LabelCalcTtlAmount.AutoSize = true;
            this.LabelCalcTtlAmount.Dock = System.Windows.Forms.DockStyle.Left;
            this.LabelCalcTtlAmount.Location = new System.Drawing.Point(4, 72);
            this.LabelCalcTtlAmount.Margin = new System.Windows.Forms.Padding(4, 10, 4, 0);
            this.LabelCalcTtlAmount.Name = "LabelCalcTtlAmount";
            this.LabelCalcTtlAmount.Size = new System.Drawing.Size(95, 23);
            this.LabelCalcTtlAmount.TabIndex = 5;
            this.LabelCalcTtlAmount.Text = "Total amount:";
            this.LabelCalcTtlAmount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // LabelCalcCellArea
            // 
            this.LabelCalcCellArea.AutoSize = true;
            this.LabelCalcCellArea.Dock = System.Windows.Forms.DockStyle.Left;
            this.LabelCalcCellArea.Location = new System.Drawing.Point(4, 10);
            this.LabelCalcCellArea.Margin = new System.Windows.Forms.Padding(4, 10, 4, 0);
            this.LabelCalcCellArea.Name = "LabelCalcCellArea";
            this.LabelCalcCellArea.Size = new System.Drawing.Size(64, 21);
            this.LabelCalcCellArea.TabIndex = 0;
            this.LabelCalcCellArea.Text = "Cell size:";
            this.LabelCalcCellArea.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TextBoxTotalArea
            // 
            this.TextBoxTotalArea.BackColor = System.Drawing.Color.White;
            this.TextBoxTotalArea.Location = new System.Drawing.Point(227, 67);
            this.TextBoxTotalArea.Margin = new System.Windows.Forms.Padding(4, 5, 4, 4);
            this.TextBoxTotalArea.Name = "TextBoxTotalArea";
            this.TextBoxTotalArea.ReadOnly = true;
            this.TextBoxTotalArea.Size = new System.Drawing.Size(136, 22);
            this.TextBoxTotalArea.TabIndex = 3;
            this.TextBoxTotalArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxCellSizeOverride
            // 
            this.CheckBoxCellSizeOverride.AutoSize = true;
            this.CheckBoxCellSizeOverride.Location = new System.Drawing.Point(379, 10);
            this.CheckBoxCellSizeOverride.Margin = new System.Windows.Forms.Padding(3, 10, 3, 2);
            this.CheckBoxCellSizeOverride.Name = "CheckBoxCellSizeOverride";
            this.CheckBoxCellSizeOverride.Size = new System.Drawing.Size(85, 19);
            this.CheckBoxCellSizeOverride.TabIndex = 1;
            this.CheckBoxCellSizeOverride.Text = "Override";
            this.CheckBoxCellSizeOverride.UseVisualStyleBackColor = true;
            this.CheckBoxCellSizeOverride.CheckedChanged += new System.EventHandler(this.CheckBoxCellSizeOverride_CheckedChanged);
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label7.Location = new System.Drawing.Point(4, 41);
            this.Label7.Margin = new System.Windows.Forms.Padding(4, 10, 4, 0);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(110, 21);
            this.Label7.TabIndex = 3;
            this.Label7.Text = "Number of cells:";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TextBoxNumCells
            // 
            this.TextBoxNumCells.BackColor = System.Drawing.Color.White;
            this.TextBoxNumCells.Location = new System.Drawing.Point(227, 36);
            this.TextBoxNumCells.Margin = new System.Windows.Forms.Padding(4, 5, 4, 4);
            this.TextBoxNumCells.Name = "TextBoxNumCells";
            this.TextBoxNumCells.ReadOnly = true;
            this.TextBoxNumCells.Size = new System.Drawing.Size(136, 22);
            this.TextBoxNumCells.TabIndex = 2;
            this.TextBoxNumCells.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LabelRasterFiles
            // 
            this.LabelRasterFiles.AutoSize = true;
            this.LabelRasterFiles.Location = new System.Drawing.Point(12, 11);
            this.LabelRasterFiles.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelRasterFiles.Name = "LabelRasterFiles";
            this.LabelRasterFiles.Size = new System.Drawing.Size(83, 17);
            this.LabelRasterFiles.TabIndex = 0;
            this.LabelRasterFiles.Text = "Raster files:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label2.Location = new System.Drawing.Point(4, 10);
            this.Label2.Margin = new System.Windows.Forms.Padding(4, 10, 4, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(111, 21);
            this.Label2.TabIndex = 0;
            this.Label2.Text = "Number of rows:";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TextBoxNumRows
            // 
            this.TextBoxNumRows.BackColor = System.Drawing.Color.White;
            this.TextBoxNumRows.Location = new System.Drawing.Point(210, 5);
            this.TextBoxNumRows.Margin = new System.Windows.Forms.Padding(4, 5, 4, 4);
            this.TextBoxNumRows.Name = "TextBoxNumRows";
            this.TextBoxNumRows.ReadOnly = true;
            this.TextBoxNumRows.Size = new System.Drawing.Size(136, 22);
            this.TextBoxNumRows.TabIndex = 0;
            this.TextBoxNumRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.Label1.Location = new System.Drawing.Point(4, 41);
            this.Label1.Margin = new System.Windows.Forms.Padding(4, 10, 4, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(134, 21);
            this.Label1.TabIndex = 2;
            this.Label1.Text = "Number of columns:";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TextBoxNumColumns
            // 
            this.TextBoxNumColumns.BackColor = System.Drawing.Color.White;
            this.TextBoxNumColumns.Location = new System.Drawing.Point(210, 36);
            this.TextBoxNumColumns.Margin = new System.Windows.Forms.Padding(4, 5, 4, 4);
            this.TextBoxNumColumns.Name = "TextBoxNumColumns";
            this.TextBoxNumColumns.ReadOnly = true;
            this.TextBoxNumColumns.Size = new System.Drawing.Size(136, 22);
            this.TextBoxNumColumns.TabIndex = 1;
            this.TextBoxNumColumns.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LabelRasterCellArea
            // 
            this.LabelRasterCellArea.AutoSize = true;
            this.LabelRasterCellArea.Dock = System.Windows.Forms.DockStyle.Left;
            this.LabelRasterCellArea.Location = new System.Drawing.Point(4, 72);
            this.LabelRasterCellArea.Margin = new System.Windows.Forms.Padding(4, 10, 4, 0);
            this.LabelRasterCellArea.Name = "LabelRasterCellArea";
            this.LabelRasterCellArea.Size = new System.Drawing.Size(64, 23);
            this.LabelRasterCellArea.TabIndex = 4;
            this.LabelRasterCellArea.Text = "Cell size:";
            this.LabelRasterCellArea.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TextBoxCellArea
            // 
            this.TextBoxCellArea.BackColor = System.Drawing.Color.White;
            this.TextBoxCellArea.Location = new System.Drawing.Point(210, 67);
            this.TextBoxCellArea.Margin = new System.Windows.Forms.Padding(4, 5, 4, 4);
            this.TextBoxCellArea.Name = "TextBoxCellArea";
            this.TextBoxCellArea.ReadOnly = true;
            this.TextBoxCellArea.Size = new System.Drawing.Size(136, 22);
            this.TextBoxCellArea.TabIndex = 2;
            this.TextBoxCellArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LabelAttributes
            // 
            this.LabelAttributes.AutoSize = true;
            this.LabelAttributes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelAttributes.Location = new System.Drawing.Point(17, 23);
            this.LabelAttributes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelAttributes.Name = "LabelAttributes";
            this.LabelAttributes.Size = new System.Drawing.Size(162, 17);
            this.LabelAttributes.TabIndex = 0;
            this.LabelAttributes.Text = "Raster file attributes:";
            // 
            // LableCalculated
            // 
            this.LableCalculated.AutoSize = true;
            this.LableCalculated.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LableCalculated.Location = new System.Drawing.Point(442, 23);
            this.LableCalculated.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LableCalculated.Name = "LableCalculated";
            this.LableCalculated.Size = new System.Drawing.Size(141, 17);
            this.LableCalculated.TabIndex = 2;
            this.LableCalculated.Text = "Calculated values:";
            // 
            // PanelBottom
            // 
            this.PanelBottom.Controls.Add(this.PanelBottomContent);
            this.PanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelBottom.Location = new System.Drawing.Point(0, 415);
            this.PanelBottom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PanelBottom.Name = "PanelBottom";
            this.PanelBottom.Size = new System.Drawing.Size(984, 194);
            this.PanelBottom.TabIndex = 0;
            // 
            // PanelBottomContent
            // 
            this.PanelBottomContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelBottomContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelBottomContent.Controls.Add(this.TableCalculated);
            this.PanelBottomContent.Controls.Add(this.LableCalculated);
            this.PanelBottomContent.Controls.Add(this.TableAttributes);
            this.PanelBottomContent.Controls.Add(this.LabelAttributes);
            this.PanelBottomContent.Location = new System.Drawing.Point(13, 6);
            this.PanelBottomContent.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PanelBottomContent.Name = "PanelBottomContent";
            this.PanelBottomContent.Size = new System.Drawing.Size(953, 184);
            this.PanelBottomContent.TabIndex = 0;
            // 
            // TableCalculated
            // 
            this.TableCalculated.ColumnCount = 3;
            this.TableCalculated.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.30851F));
            this.TableCalculated.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.69149F));
            this.TableCalculated.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.TableCalculated.Controls.Add(this.CheckBoxCellSizeOverride, 2, 0);
            this.TableCalculated.Controls.Add(this.TextBoxTotalArea, 1, 2);
            this.TableCalculated.Controls.Add(this.TextBoxCellAreaCalc, 1, 0);
            this.TableCalculated.Controls.Add(this.LabelCalcCellArea, 0, 0);
            this.TableCalculated.Controls.Add(this.TextBoxNumCells, 1, 1);
            this.TableCalculated.Controls.Add(this.LabelCalcTtlAmount, 0, 2);
            this.TableCalculated.Controls.Add(this.Label7, 0, 1);
            this.TableCalculated.Location = new System.Drawing.Point(440, 62);
            this.TableCalculated.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TableCalculated.Name = "TableCalculated";
            this.TableCalculated.RowCount = 3;
            this.TableCalculated.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TableCalculated.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TableCalculated.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TableCalculated.Size = new System.Drawing.Size(497, 95);
            this.TableCalculated.TabIndex = 3;
            // 
            // TableAttributes
            // 
            this.TableAttributes.ColumnCount = 2;
            this.TableAttributes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.60318F));
            this.TableAttributes.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.39682F));
            this.TableAttributes.Controls.Add(this.Label2, 0, 0);
            this.TableAttributes.Controls.Add(this.Label1, 0, 1);
            this.TableAttributes.Controls.Add(this.LabelRasterCellArea, 0, 2);
            this.TableAttributes.Controls.Add(this.TextBoxNumRows, 1, 0);
            this.TableAttributes.Controls.Add(this.TextBoxNumColumns, 1, 1);
            this.TableAttributes.Controls.Add(this.TextBoxCellArea, 1, 2);
            this.TableAttributes.Location = new System.Drawing.Point(16, 62);
            this.TableAttributes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.TableAttributes.Name = "TableAttributes";
            this.TableAttributes.RowCount = 3;
            this.TableAttributes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TableAttributes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TableAttributes.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TableAttributes.Size = new System.Drawing.Size(416, 95);
            this.TableAttributes.TabIndex = 1;
            // 
            // PanelTop
            // 
            this.PanelTop.Controls.Add(this.PanelTopContent);
            this.PanelTop.Controls.Add(this.PanelBannerTop);
            this.PanelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelTop.Location = new System.Drawing.Point(0, 0);
            this.PanelTop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PanelTop.Name = "PanelTop";
            this.PanelTop.Size = new System.Drawing.Size(984, 415);
            this.PanelTop.TabIndex = 0;
            // 
            // PanelTopContent
            // 
            this.PanelTopContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelTopContent.Location = new System.Drawing.Point(13, 37);
            this.PanelTopContent.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PanelTopContent.Name = "PanelTopContent";
            this.PanelTopContent.Size = new System.Drawing.Size(953, 370);
            this.PanelTopContent.TabIndex = 0;
            // 
            // PanelBannerTop
            // 
            this.PanelBannerTop.Controls.Add(this.LabelRasterFiles);
            this.PanelBannerTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelBannerTop.Location = new System.Drawing.Point(0, 0);
            this.PanelBannerTop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.PanelBannerTop.Name = "PanelBannerTop";
            this.PanelBannerTop.Size = new System.Drawing.Size(984, 37);
            this.PanelBannerTop.TabIndex = 0;
            // 
            // InitialConditionsSpatialDataFeedView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.PanelTop);
            this.Controls.Add(this.PanelBottom);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "InitialConditionsSpatialDataFeedView";
            this.Size = new System.Drawing.Size(984, 609);
            this.PanelBottom.ResumeLayout(false);
            this.PanelBottomContent.ResumeLayout(false);
            this.PanelBottomContent.PerformLayout();
            this.TableCalculated.ResumeLayout(false);
            this.TableCalculated.PerformLayout();
            this.TableAttributes.ResumeLayout(false);
            this.TableAttributes.PerformLayout();
            this.PanelTop.ResumeLayout(false);
            this.PanelBannerTop.ResumeLayout(false);
            this.PanelBannerTop.PerformLayout();
            this.ResumeLayout(false);

        }
        internal System.Windows.Forms.TextBox TextBoxCellAreaCalc;
        internal System.Windows.Forms.Label LabelCalcTtlAmount;
        internal System.Windows.Forms.Label LabelCalcCellArea;
        internal System.Windows.Forms.TextBox TextBoxTotalArea;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.TextBox TextBoxNumCells;
        internal System.Windows.Forms.CheckBox CheckBoxCellSizeOverride;
        internal System.Windows.Forms.Label LabelRasterFiles;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox TextBoxNumRows;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox TextBoxNumColumns;
        internal System.Windows.Forms.Label LabelRasterCellArea;
        internal System.Windows.Forms.TextBox TextBoxCellArea;
        private System.Windows.Forms.Label LabelAttributes;
        private System.Windows.Forms.Label LableCalculated;
        private System.Windows.Forms.Panel PanelBottom;
        private System.Windows.Forms.Panel PanelTop;
        internal System.Windows.Forms.Panel PanelTopContent;
        private System.Windows.Forms.Panel PanelBannerTop;
        private System.Windows.Forms.TableLayoutPanel TableCalculated;
        private System.Windows.Forms.TableLayoutPanel TableAttributes;
        private System.Windows.Forms.Panel PanelBottomContent;
    }
}
