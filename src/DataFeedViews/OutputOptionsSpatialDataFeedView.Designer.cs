// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal partial class OutputOptionsSpatialDataFeedView : SyncroSim.Core.Forms.DataFeedView
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
            this.CheckBoxRasterTE = new System.Windows.Forms.CheckBox();
            this.LabelRasterTETimesteps = new System.Windows.Forms.Label();
            this.TextBoxRasterTETimesteps = new System.Windows.Forms.TextBox();
            this.CheckBoxRasterTA = new System.Windows.Forms.CheckBox();
            this.LabelRasterTATimesteps = new System.Windows.Forms.Label();
            this.TextBoxRasterTATimesteps = new System.Windows.Forms.TextBox();
            this.CheckBoxRasterSA = new System.Windows.Forms.CheckBox();
            this.LabelRasterSATimesteps = new System.Windows.Forms.Label();
            this.TextBoxRasterSATimesteps = new System.Windows.Forms.TextBox();
            this.LabelRasterTSTTimesteps = new System.Windows.Forms.Label();
            this.TextBoxRasterTSTTimesteps = new System.Windows.Forms.TextBox();
            this.CheckBoxRasterTST = new System.Windows.Forms.CheckBox();
            this.TextBoxRasterAgeTimesteps = new System.Windows.Forms.TextBox();
            this.CheckBoxRasterAge = new System.Windows.Forms.CheckBox();
            this.LabelRasterAgeTimesteps = new System.Windows.Forms.Label();
            this.TextBoxRasterTRTimesteps = new System.Windows.Forms.TextBox();
            this.CheckBoxRasterST = new System.Windows.Forms.CheckBox();
            this.LabelRasterSTTimesteps = new System.Windows.Forms.Label();
            this.CheckBoxRasterSC = new System.Windows.Forms.CheckBox();
            this.CheckBoxRasterTR = new System.Windows.Forms.CheckBox();
            this.TextBoxRasterSCTimesteps = new System.Windows.Forms.TextBox();
            this.TextBoxRasterSTTimesteps = new System.Windows.Forms.TextBox();
            this.LabelRasterTRTimesteps = new System.Windows.Forms.Label();
            this.LabelRasterSCTimesteps = new System.Windows.Forms.Label();
            this.PanelMain = new System.Windows.Forms.Panel();
            this.TableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.PanelMain.SuspendLayout();
            this.TableLayoutPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // CheckBoxRasterTE
            // 
            this.CheckBoxRasterTE.AutoSize = true;
            this.CheckBoxRasterTE.Location = new System.Drawing.Point(4, 124);
            this.CheckBoxRasterTE.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxRasterTE.Name = "CheckBoxRasterTE";
            this.CheckBoxRasterTE.Size = new System.Drawing.Size(178, 21);
            this.CheckBoxRasterTE.TabIndex = 12;
            this.CheckBoxRasterTE.Text = "Transition events every";
            this.CheckBoxRasterTE.UseVisualStyleBackColor = true;
            // 
            // LabelRasterTETimesteps
            // 
            this.LabelRasterTETimesteps.AutoSize = true;
            this.LabelRasterTETimesteps.Location = new System.Drawing.Point(330, 126);
            this.LabelRasterTETimesteps.Margin = new System.Windows.Forms.Padding(4, 6, 4, 0);
            this.LabelRasterTETimesteps.Name = "LabelRasterTETimesteps";
            this.LabelRasterTETimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelRasterTETimesteps.TabIndex = 14;
            this.LabelRasterTETimesteps.Text = "timesteps";
            // 
            // TextBoxRasterTETimesteps
            // 
            this.TextBoxRasterTETimesteps.Location = new System.Drawing.Point(225, 124);
            this.TextBoxRasterTETimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxRasterTETimesteps.Name = "TextBoxRasterTETimesteps";
            this.TextBoxRasterTETimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxRasterTETimesteps.TabIndex = 13;
            this.TextBoxRasterTETimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxRasterTA
            // 
            this.CheckBoxRasterTA.AutoSize = true;
            this.CheckBoxRasterTA.Location = new System.Drawing.Point(4, 214);
            this.CheckBoxRasterTA.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxRasterTA.Name = "CheckBoxRasterTA";
            this.CheckBoxRasterTA.Size = new System.Drawing.Size(195, 21);
            this.CheckBoxRasterTA.TabIndex = 21;
            this.CheckBoxRasterTA.Text = "Transition attributes every";
            this.CheckBoxRasterTA.UseVisualStyleBackColor = true;
            // 
            // LabelRasterTATimesteps
            // 
            this.LabelRasterTATimesteps.AutoSize = true;
            this.LabelRasterTATimesteps.Location = new System.Drawing.Point(330, 216);
            this.LabelRasterTATimesteps.Margin = new System.Windows.Forms.Padding(4, 6, 4, 0);
            this.LabelRasterTATimesteps.Name = "LabelRasterTATimesteps";
            this.LabelRasterTATimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelRasterTATimesteps.TabIndex = 23;
            this.LabelRasterTATimesteps.Text = "timesteps";
            // 
            // TextBoxRasterTATimesteps
            // 
            this.TextBoxRasterTATimesteps.Location = new System.Drawing.Point(225, 214);
            this.TextBoxRasterTATimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxRasterTATimesteps.Name = "TextBoxRasterTATimesteps";
            this.TextBoxRasterTATimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxRasterTATimesteps.TabIndex = 22;
            this.TextBoxRasterTATimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxRasterSA
            // 
            this.CheckBoxRasterSA.AutoSize = true;
            this.CheckBoxRasterSA.Location = new System.Drawing.Point(4, 184);
            this.CheckBoxRasterSA.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxRasterSA.Name = "CheckBoxRasterSA";
            this.CheckBoxRasterSA.Size = new System.Drawing.Size(165, 21);
            this.CheckBoxRasterSA.TabIndex = 18;
            this.CheckBoxRasterSA.Text = "State attributes every";
            this.CheckBoxRasterSA.UseVisualStyleBackColor = true;
            // 
            // LabelRasterSATimesteps
            // 
            this.LabelRasterSATimesteps.AutoSize = true;
            this.LabelRasterSATimesteps.Location = new System.Drawing.Point(330, 186);
            this.LabelRasterSATimesteps.Margin = new System.Windows.Forms.Padding(4, 6, 4, 0);
            this.LabelRasterSATimesteps.Name = "LabelRasterSATimesteps";
            this.LabelRasterSATimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelRasterSATimesteps.TabIndex = 20;
            this.LabelRasterSATimesteps.Text = "timesteps";
            // 
            // TextBoxRasterSATimesteps
            // 
            this.TextBoxRasterSATimesteps.Location = new System.Drawing.Point(225, 184);
            this.TextBoxRasterSATimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxRasterSATimesteps.Name = "TextBoxRasterSATimesteps";
            this.TextBoxRasterSATimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxRasterSATimesteps.TabIndex = 19;
            this.TextBoxRasterSATimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LabelRasterTSTTimesteps
            // 
            this.LabelRasterTSTTimesteps.AutoSize = true;
            this.LabelRasterTSTTimesteps.Location = new System.Drawing.Point(330, 156);
            this.LabelRasterTSTTimesteps.Margin = new System.Windows.Forms.Padding(4, 6, 4, 0);
            this.LabelRasterTSTTimesteps.Name = "LabelRasterTSTTimesteps";
            this.LabelRasterTSTTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelRasterTSTTimesteps.TabIndex = 17;
            this.LabelRasterTSTTimesteps.Text = "timesteps";
            // 
            // TextBoxRasterTSTTimesteps
            // 
            this.TextBoxRasterTSTTimesteps.Location = new System.Drawing.Point(225, 154);
            this.TextBoxRasterTSTTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxRasterTSTTimesteps.Name = "TextBoxRasterTSTTimesteps";
            this.TextBoxRasterTSTTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxRasterTSTTimesteps.TabIndex = 16;
            this.TextBoxRasterTSTTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxRasterTST
            // 
            this.CheckBoxRasterTST.AutoSize = true;
            this.CheckBoxRasterTST.Location = new System.Drawing.Point(4, 154);
            this.CheckBoxRasterTST.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxRasterTST.Name = "CheckBoxRasterTST";
            this.CheckBoxRasterTST.Size = new System.Drawing.Size(201, 21);
            this.CheckBoxRasterTST.TabIndex = 15;
            this.CheckBoxRasterTST.Text = "Time-since-transition every";
            this.CheckBoxRasterTST.UseVisualStyleBackColor = true;
            // 
            // TextBoxRasterAgeTimesteps
            // 
            this.TextBoxRasterAgeTimesteps.Location = new System.Drawing.Point(225, 34);
            this.TextBoxRasterAgeTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxRasterAgeTimesteps.Name = "TextBoxRasterAgeTimesteps";
            this.TextBoxRasterAgeTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxRasterAgeTimesteps.TabIndex = 4;
            this.TextBoxRasterAgeTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxRasterAge
            // 
            this.CheckBoxRasterAge.AutoSize = true;
            this.CheckBoxRasterAge.Location = new System.Drawing.Point(4, 34);
            this.CheckBoxRasterAge.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxRasterAge.Name = "CheckBoxRasterAge";
            this.CheckBoxRasterAge.Size = new System.Drawing.Size(101, 21);
            this.CheckBoxRasterAge.TabIndex = 3;
            this.CheckBoxRasterAge.Text = "Ages every";
            this.CheckBoxRasterAge.UseVisualStyleBackColor = true;
            // 
            // LabelRasterAgeTimesteps
            // 
            this.LabelRasterAgeTimesteps.AutoSize = true;
            this.LabelRasterAgeTimesteps.Location = new System.Drawing.Point(330, 36);
            this.LabelRasterAgeTimesteps.Margin = new System.Windows.Forms.Padding(4, 6, 4, 0);
            this.LabelRasterAgeTimesteps.Name = "LabelRasterAgeTimesteps";
            this.LabelRasterAgeTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelRasterAgeTimesteps.TabIndex = 5;
            this.LabelRasterAgeTimesteps.Text = "timesteps";
            // 
            // TextBoxRasterTRTimesteps
            // 
            this.TextBoxRasterTRTimesteps.Location = new System.Drawing.Point(225, 94);
            this.TextBoxRasterTRTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxRasterTRTimesteps.Name = "TextBoxRasterTRTimesteps";
            this.TextBoxRasterTRTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxRasterTRTimesteps.TabIndex = 10;
            this.TextBoxRasterTRTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxRasterST
            // 
            this.CheckBoxRasterST.AutoSize = true;
            this.CheckBoxRasterST.Location = new System.Drawing.Point(4, 64);
            this.CheckBoxRasterST.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxRasterST.Name = "CheckBoxRasterST";
            this.CheckBoxRasterST.Size = new System.Drawing.Size(107, 21);
            this.CheckBoxRasterST.TabIndex = 6;
            this.CheckBoxRasterST.Text = "Strata every";
            this.CheckBoxRasterST.UseVisualStyleBackColor = true;
            // 
            // LabelRasterSTTimesteps
            // 
            this.LabelRasterSTTimesteps.AutoSize = true;
            this.LabelRasterSTTimesteps.Location = new System.Drawing.Point(330, 66);
            this.LabelRasterSTTimesteps.Margin = new System.Windows.Forms.Padding(4, 6, 4, 0);
            this.LabelRasterSTTimesteps.Name = "LabelRasterSTTimesteps";
            this.LabelRasterSTTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelRasterSTTimesteps.TabIndex = 8;
            this.LabelRasterSTTimesteps.Text = "timesteps";
            // 
            // CheckBoxRasterSC
            // 
            this.CheckBoxRasterSC.AutoSize = true;
            this.CheckBoxRasterSC.Location = new System.Drawing.Point(4, 4);
            this.CheckBoxRasterSC.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxRasterSC.Name = "CheckBoxRasterSC";
            this.CheckBoxRasterSC.Size = new System.Drawing.Size(153, 21);
            this.CheckBoxRasterSC.TabIndex = 0;
            this.CheckBoxRasterSC.Text = "State classes every";
            this.CheckBoxRasterSC.UseVisualStyleBackColor = true;
            // 
            // CheckBoxRasterTR
            // 
            this.CheckBoxRasterTR.AutoSize = true;
            this.CheckBoxRasterTR.Location = new System.Drawing.Point(4, 94);
            this.CheckBoxRasterTR.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxRasterTR.Name = "CheckBoxRasterTR";
            this.CheckBoxRasterTR.Size = new System.Drawing.Size(139, 21);
            this.CheckBoxRasterTR.TabIndex = 9;
            this.CheckBoxRasterTR.Text = "Transitions every";
            this.CheckBoxRasterTR.UseVisualStyleBackColor = true;
            // 
            // TextBoxRasterSCTimesteps
            // 
            this.TextBoxRasterSCTimesteps.Location = new System.Drawing.Point(225, 4);
            this.TextBoxRasterSCTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxRasterSCTimesteps.Name = "TextBoxRasterSCTimesteps";
            this.TextBoxRasterSCTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxRasterSCTimesteps.TabIndex = 1;
            this.TextBoxRasterSCTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TextBoxRasterSTTimesteps
            // 
            this.TextBoxRasterSTTimesteps.Location = new System.Drawing.Point(225, 64);
            this.TextBoxRasterSTTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxRasterSTTimesteps.Name = "TextBoxRasterSTTimesteps";
            this.TextBoxRasterSTTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxRasterSTTimesteps.TabIndex = 7;
            this.TextBoxRasterSTTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LabelRasterTRTimesteps
            // 
            this.LabelRasterTRTimesteps.AutoSize = true;
            this.LabelRasterTRTimesteps.Location = new System.Drawing.Point(330, 96);
            this.LabelRasterTRTimesteps.Margin = new System.Windows.Forms.Padding(4, 6, 4, 0);
            this.LabelRasterTRTimesteps.Name = "LabelRasterTRTimesteps";
            this.LabelRasterTRTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelRasterTRTimesteps.TabIndex = 11;
            this.LabelRasterTRTimesteps.Text = "timesteps";
            // 
            // LabelRasterSCTimesteps
            // 
            this.LabelRasterSCTimesteps.AutoSize = true;
            this.LabelRasterSCTimesteps.Location = new System.Drawing.Point(330, 6);
            this.LabelRasterSCTimesteps.Margin = new System.Windows.Forms.Padding(4, 6, 4, 0);
            this.LabelRasterSCTimesteps.Name = "LabelRasterSCTimesteps";
            this.LabelRasterSCTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelRasterSCTimesteps.TabIndex = 2;
            this.LabelRasterSCTimesteps.Text = "timesteps";
            // 
            // PanelMain
            // 
            this.PanelMain.Controls.Add(this.TableLayoutPanelMain);
            this.PanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMain.Location = new System.Drawing.Point(0, 0);
            this.PanelMain.Name = "PanelMain";
            this.PanelMain.Padding = new System.Windows.Forms.Padding(20);
            this.PanelMain.Size = new System.Drawing.Size(1826, 903);
            this.PanelMain.TabIndex = 0;
            // 
            // TableLayoutPanelMain
            // 
            this.TableLayoutPanelMain.ColumnCount = 3;
            this.TableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 221F));
            this.TableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.TableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1500F));
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxRasterTE, 0, 4);
            this.TableLayoutPanelMain.Controls.Add(this.LabelRasterTETimesteps, 2, 4);
            this.TableLayoutPanelMain.Controls.Add(this.TextBoxRasterTETimesteps, 1, 4);
            this.TableLayoutPanelMain.Controls.Add(this.LabelRasterSCTimesteps, 2, 0);
            this.TableLayoutPanelMain.Controls.Add(this.TextBoxRasterSCTimesteps, 1, 0);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxRasterSC, 0, 0);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxRasterTA, 0, 7);
            this.TableLayoutPanelMain.Controls.Add(this.TextBoxRasterTATimesteps, 1, 7);
            this.TableLayoutPanelMain.Controls.Add(this.LabelRasterTATimesteps, 2, 7);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxRasterSA, 0, 6);
            this.TableLayoutPanelMain.Controls.Add(this.TextBoxRasterSATimesteps, 1, 6);
            this.TableLayoutPanelMain.Controls.Add(this.LabelRasterSATimesteps, 2, 6);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxRasterTST, 0, 5);
            this.TableLayoutPanelMain.Controls.Add(this.TextBoxRasterTSTTimesteps, 1, 5);
            this.TableLayoutPanelMain.Controls.Add(this.LabelRasterTSTTimesteps, 2, 5);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxRasterAge, 0, 1);
            this.TableLayoutPanelMain.Controls.Add(this.TextBoxRasterAgeTimesteps, 1, 1);
            this.TableLayoutPanelMain.Controls.Add(this.LabelRasterAgeTimesteps, 2, 1);
            this.TableLayoutPanelMain.Controls.Add(this.LabelRasterSTTimesteps, 2, 2);
            this.TableLayoutPanelMain.Controls.Add(this.TextBoxRasterSTTimesteps, 1, 2);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxRasterST, 0, 2);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxRasterTR, 0, 3);
            this.TableLayoutPanelMain.Controls.Add(this.TextBoxRasterTRTimesteps, 1, 3);
            this.TableLayoutPanelMain.Controls.Add(this.LabelRasterTRTimesteps, 2, 3);
            this.TableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.TableLayoutPanelMain.Location = new System.Drawing.Point(20, 20);
            this.TableLayoutPanelMain.Name = "TableLayoutPanelMain";
            this.TableLayoutPanelMain.RowCount = 8;
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayoutPanelMain.Size = new System.Drawing.Size(1786, 245);
            this.TableLayoutPanelMain.TabIndex = 24;
            // 
            // OutputOptionsSpatialDataFeedView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PanelMain);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "OutputOptionsSpatialDataFeedView";
            this.Size = new System.Drawing.Size(1826, 903);
            this.PanelMain.ResumeLayout(false);
            this.TableLayoutPanelMain.ResumeLayout(false);
            this.TableLayoutPanelMain.PerformLayout();
            this.ResumeLayout(false);

        }
        internal System.Windows.Forms.CheckBox CheckBoxRasterTA;
        internal System.Windows.Forms.Label LabelRasterTATimesteps;
        internal System.Windows.Forms.TextBox TextBoxRasterTATimesteps;
        internal System.Windows.Forms.CheckBox CheckBoxRasterSA;
        internal System.Windows.Forms.Label LabelRasterSATimesteps;
        internal System.Windows.Forms.TextBox TextBoxRasterSATimesteps;
        internal System.Windows.Forms.Label LabelRasterTSTTimesteps;
        internal System.Windows.Forms.TextBox TextBoxRasterTSTTimesteps;
        internal System.Windows.Forms.CheckBox CheckBoxRasterTST;
        internal System.Windows.Forms.TextBox TextBoxRasterAgeTimesteps;
        internal System.Windows.Forms.CheckBox CheckBoxRasterAge;
        internal System.Windows.Forms.Label LabelRasterAgeTimesteps;
        internal System.Windows.Forms.TextBox TextBoxRasterTRTimesteps;
        internal System.Windows.Forms.CheckBox CheckBoxRasterST;
        internal System.Windows.Forms.Label LabelRasterSTTimesteps;
        internal System.Windows.Forms.CheckBox CheckBoxRasterSC;
        internal System.Windows.Forms.CheckBox CheckBoxRasterTR;
        internal System.Windows.Forms.TextBox TextBoxRasterSCTimesteps;
        internal System.Windows.Forms.TextBox TextBoxRasterSTTimesteps;
        internal System.Windows.Forms.Label LabelRasterTRTimesteps;
        internal System.Windows.Forms.Label LabelRasterSCTimesteps;
        internal System.Windows.Forms.CheckBox CheckBoxRasterTE;
        internal System.Windows.Forms.Label LabelRasterTETimesteps;
        internal System.Windows.Forms.TextBox TextBoxRasterTETimesteps;
        private System.Windows.Forms.Panel PanelMain;
        private System.Windows.Forms.TableLayoutPanel TableLayoutPanelMain;
    }
}
