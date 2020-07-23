// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal partial class OutputOptionsDataFeedView : SyncroSim.Core.Forms.DataFeedView
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
            this.CheckBoxSummaryTAAges = new System.Windows.Forms.CheckBox();
            this.CheckBoxSummarySAAges = new System.Windows.Forms.CheckBox();
            this.CheckBoxSummaryTRAges = new System.Windows.Forms.CheckBox();
            this.CheckBoxSummarySCAges = new System.Windows.Forms.CheckBox();
            this.CheckBoxSummaryOmitTS = new System.Windows.Forms.CheckBox();
            this.CheckBoxSummaryOmitSS = new System.Windows.Forms.CheckBox();
            this.CheckBoxSummarySCZeroValues = new System.Windows.Forms.CheckBox();
            this.LabelSummaryTATimesteps = new System.Windows.Forms.Label();
            this.TextBoxSummaryTATimesteps = new System.Windows.Forms.TextBox();
            this.CheckBoxSummaryTA = new System.Windows.Forms.CheckBox();
            this.LabelSummarySATimesteps = new System.Windows.Forms.Label();
            this.TextBoxSummarySATimesteps = new System.Windows.Forms.TextBox();
            this.CheckBoxSummaryTRCalcIntervalMean = new System.Windows.Forms.CheckBox();
            this.CheckBoxSummarySA = new System.Windows.Forms.CheckBox();
            this.TextBoxSummarySCTimesteps = new System.Windows.Forms.TextBox();
            this.LabelSummarySCTimesteps = new System.Windows.Forms.Label();
            this.LabelSummaryTRSCTimesteps = new System.Windows.Forms.Label();
            this.CheckBoxSummarySC = new System.Windows.Forms.CheckBox();
            this.CheckBoxSummaryTR = new System.Windows.Forms.CheckBox();
            this.TextBoxSummaryTRSCTimesteps = new System.Windows.Forms.TextBox();
            this.LabelSummaryTRTimesteps = new System.Windows.Forms.Label();
            this.CheckBoxSummaryTRSC = new System.Windows.Forms.CheckBox();
            this.TextBoxSummaryTRTimesteps = new System.Windows.Forms.TextBox();
            this.PanelMain = new System.Windows.Forms.Panel();
            this.TableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.CheckBoxSummaryEV = new System.Windows.Forms.CheckBox();
            this.LabelSummaryEVTimesteps = new System.Windows.Forms.Label();
            this.TextBoxSummaryEVTimesteps = new System.Windows.Forms.TextBox();
            this.PanelMain.SuspendLayout();
            this.TableLayoutPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // CheckBoxSummaryTAAges
            // 
            this.CheckBoxSummaryTAAges.AutoSize = true;
            this.CheckBoxSummaryTAAges.Location = new System.Drawing.Point(427, 124);
            this.CheckBoxSummaryTAAges.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxSummaryTAAges.Name = "CheckBoxSummaryTAAges";
            this.CheckBoxSummaryTAAges.Size = new System.Drawing.Size(110, 21);
            this.CheckBoxSummaryTAAges.TabIndex = 20;
            this.CheckBoxSummaryTAAges.Text = "Include ages";
            this.CheckBoxSummaryTAAges.UseVisualStyleBackColor = true;
            // 
            // CheckBoxSummarySAAges
            // 
            this.CheckBoxSummarySAAges.AutoSize = true;
            this.CheckBoxSummarySAAges.Location = new System.Drawing.Point(427, 94);
            this.CheckBoxSummarySAAges.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxSummarySAAges.Name = "CheckBoxSummarySAAges";
            this.CheckBoxSummarySAAges.Size = new System.Drawing.Size(110, 21);
            this.CheckBoxSummarySAAges.TabIndex = 16;
            this.CheckBoxSummarySAAges.Text = "Include ages";
            this.CheckBoxSummarySAAges.UseVisualStyleBackColor = true;
            // 
            // CheckBoxSummaryTRAges
            // 
            this.CheckBoxSummaryTRAges.AutoSize = true;
            this.CheckBoxSummaryTRAges.Location = new System.Drawing.Point(427, 34);
            this.CheckBoxSummaryTRAges.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxSummaryTRAges.Name = "CheckBoxSummaryTRAges";
            this.CheckBoxSummaryTRAges.Size = new System.Drawing.Size(110, 21);
            this.CheckBoxSummaryTRAges.TabIndex = 8;
            this.CheckBoxSummaryTRAges.Text = "Include ages";
            this.CheckBoxSummaryTRAges.UseVisualStyleBackColor = true;
            // 
            // CheckBoxSummarySCAges
            // 
            this.CheckBoxSummarySCAges.AutoSize = true;
            this.CheckBoxSummarySCAges.Location = new System.Drawing.Point(427, 4);
            this.CheckBoxSummarySCAges.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxSummarySCAges.Name = "CheckBoxSummarySCAges";
            this.CheckBoxSummarySCAges.Size = new System.Drawing.Size(110, 21);
            this.CheckBoxSummarySCAges.TabIndex = 3;
            this.CheckBoxSummarySCAges.Text = "Include ages";
            this.CheckBoxSummarySCAges.UseVisualStyleBackColor = true;
            // 
            // CheckBoxSummaryOmitTS
            // 
            this.CheckBoxSummaryOmitTS.AutoSize = true;
            this.CheckBoxSummaryOmitTS.Location = new System.Drawing.Point(4, 244);
            this.CheckBoxSummaryOmitTS.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxSummaryOmitTS.Name = "CheckBoxSummaryOmitTS";
            this.CheckBoxSummaryOmitTS.Size = new System.Drawing.Size(147, 21);
            this.CheckBoxSummaryOmitTS.TabIndex = 25;
            this.CheckBoxSummaryOmitTS.Text = "Omit tertiary strata";
            this.CheckBoxSummaryOmitTS.UseVisualStyleBackColor = true;
            // 
            // CheckBoxSummaryOmitSS
            // 
            this.CheckBoxSummaryOmitSS.AutoSize = true;
            this.CheckBoxSummaryOmitSS.Location = new System.Drawing.Point(4, 214);
            this.CheckBoxSummaryOmitSS.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxSummaryOmitSS.Name = "CheckBoxSummaryOmitSS";
            this.CheckBoxSummaryOmitSS.Size = new System.Drawing.Size(169, 21);
            this.CheckBoxSummaryOmitSS.TabIndex = 24;
            this.CheckBoxSummaryOmitSS.Text = "Omit secondary strata";
            this.CheckBoxSummaryOmitSS.UseVisualStyleBackColor = true;
            // 
            // CheckBoxSummarySCZeroValues
            // 
            this.CheckBoxSummarySCZeroValues.AutoSize = true;
            this.CheckBoxSummarySCZeroValues.Location = new System.Drawing.Point(556, 4);
            this.CheckBoxSummarySCZeroValues.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxSummarySCZeroValues.Name = "CheckBoxSummarySCZeroValues";
            this.CheckBoxSummarySCZeroValues.Size = new System.Drawing.Size(152, 21);
            this.CheckBoxSummarySCZeroValues.TabIndex = 4;
            this.CheckBoxSummarySCZeroValues.Text = "Include zero values";
            this.CheckBoxSummarySCZeroValues.UseVisualStyleBackColor = true;
            // 
            // LabelSummaryTATimesteps
            // 
            this.LabelSummaryTATimesteps.AutoSize = true;
            this.LabelSummaryTATimesteps.Location = new System.Drawing.Point(314, 126);
            this.LabelSummaryTATimesteps.Margin = new System.Windows.Forms.Padding(4, 6, 4, 0);
            this.LabelSummaryTATimesteps.Name = "LabelSummaryTATimesteps";
            this.LabelSummaryTATimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelSummaryTATimesteps.TabIndex = 19;
            this.LabelSummaryTATimesteps.Text = "timesteps";
            // 
            // TextBoxSummaryTATimesteps
            // 
            this.TextBoxSummaryTATimesteps.Location = new System.Drawing.Point(235, 124);
            this.TextBoxSummaryTATimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxSummaryTATimesteps.Name = "TextBoxSummaryTATimesteps";
            this.TextBoxSummaryTATimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxSummaryTATimesteps.TabIndex = 18;
            this.TextBoxSummaryTATimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxSummaryTA
            // 
            this.CheckBoxSummaryTA.AutoSize = true;
            this.CheckBoxSummaryTA.Location = new System.Drawing.Point(4, 124);
            this.CheckBoxSummaryTA.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxSummaryTA.Name = "CheckBoxSummaryTA";
            this.CheckBoxSummaryTA.Size = new System.Drawing.Size(195, 21);
            this.CheckBoxSummaryTA.TabIndex = 17;
            this.CheckBoxSummaryTA.Text = "Transition attributes every";
            this.CheckBoxSummaryTA.UseVisualStyleBackColor = true;
            // 
            // LabelSummarySATimesteps
            // 
            this.LabelSummarySATimesteps.AutoSize = true;
            this.LabelSummarySATimesteps.Location = new System.Drawing.Point(314, 96);
            this.LabelSummarySATimesteps.Margin = new System.Windows.Forms.Padding(4, 6, 4, 0);
            this.LabelSummarySATimesteps.Name = "LabelSummarySATimesteps";
            this.LabelSummarySATimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelSummarySATimesteps.TabIndex = 15;
            this.LabelSummarySATimesteps.Text = "timesteps";
            // 
            // TextBoxSummarySATimesteps
            // 
            this.TextBoxSummarySATimesteps.Location = new System.Drawing.Point(235, 94);
            this.TextBoxSummarySATimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxSummarySATimesteps.Name = "TextBoxSummarySATimesteps";
            this.TextBoxSummarySATimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxSummarySATimesteps.TabIndex = 14;
            this.TextBoxSummarySATimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxSummaryTRCalcIntervalMean
            // 
            this.CheckBoxSummaryTRCalcIntervalMean.AutoSize = true;
            this.CheckBoxSummaryTRCalcIntervalMean.Location = new System.Drawing.Point(556, 34);
            this.CheckBoxSummaryTRCalcIntervalMean.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxSummaryTRCalcIntervalMean.Name = "CheckBoxSummaryTRCalcIntervalMean";
            this.CheckBoxSummaryTRCalcIntervalMean.Size = new System.Drawing.Size(241, 21);
            this.CheckBoxSummaryTRCalcIntervalMean.TabIndex = 9;
            this.CheckBoxSummaryTRCalcIntervalMean.Text = "Calculate as interval mean values";
            this.CheckBoxSummaryTRCalcIntervalMean.UseVisualStyleBackColor = true;
            // 
            // CheckBoxSummarySA
            // 
            this.CheckBoxSummarySA.AutoSize = true;
            this.CheckBoxSummarySA.Location = new System.Drawing.Point(4, 94);
            this.CheckBoxSummarySA.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxSummarySA.Name = "CheckBoxSummarySA";
            this.CheckBoxSummarySA.Size = new System.Drawing.Size(165, 21);
            this.CheckBoxSummarySA.TabIndex = 13;
            this.CheckBoxSummarySA.Text = "State attributes every";
            this.CheckBoxSummarySA.UseVisualStyleBackColor = true;
            // 
            // TextBoxSummarySCTimesteps
            // 
            this.TextBoxSummarySCTimesteps.Location = new System.Drawing.Point(235, 4);
            this.TextBoxSummarySCTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxSummarySCTimesteps.Name = "TextBoxSummarySCTimesteps";
            this.TextBoxSummarySCTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxSummarySCTimesteps.TabIndex = 1;
            this.TextBoxSummarySCTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LabelSummarySCTimesteps
            // 
            this.LabelSummarySCTimesteps.AutoSize = true;
            this.LabelSummarySCTimesteps.Location = new System.Drawing.Point(314, 6);
            this.LabelSummarySCTimesteps.Margin = new System.Windows.Forms.Padding(4, 6, 4, 0);
            this.LabelSummarySCTimesteps.Name = "LabelSummarySCTimesteps";
            this.LabelSummarySCTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelSummarySCTimesteps.TabIndex = 2;
            this.LabelSummarySCTimesteps.Text = "timesteps";
            // 
            // LabelSummaryTRSCTimesteps
            // 
            this.LabelSummaryTRSCTimesteps.AutoSize = true;
            this.LabelSummaryTRSCTimesteps.Location = new System.Drawing.Point(314, 66);
            this.LabelSummaryTRSCTimesteps.Margin = new System.Windows.Forms.Padding(4, 6, 4, 0);
            this.LabelSummaryTRSCTimesteps.Name = "LabelSummaryTRSCTimesteps";
            this.LabelSummaryTRSCTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelSummaryTRSCTimesteps.TabIndex = 12;
            this.LabelSummaryTRSCTimesteps.Text = "timesteps";
            // 
            // CheckBoxSummarySC
            // 
            this.CheckBoxSummarySC.AutoSize = true;
            this.CheckBoxSummarySC.Location = new System.Drawing.Point(4, 4);
            this.CheckBoxSummarySC.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxSummarySC.Name = "CheckBoxSummarySC";
            this.CheckBoxSummarySC.Size = new System.Drawing.Size(153, 21);
            this.CheckBoxSummarySC.TabIndex = 0;
            this.CheckBoxSummarySC.Text = "State classes every";
            this.CheckBoxSummarySC.UseVisualStyleBackColor = true;
            // 
            // CheckBoxSummaryTR
            // 
            this.CheckBoxSummaryTR.AutoSize = true;
            this.CheckBoxSummaryTR.Location = new System.Drawing.Point(4, 34);
            this.CheckBoxSummaryTR.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxSummaryTR.Name = "CheckBoxSummaryTR";
            this.CheckBoxSummaryTR.Size = new System.Drawing.Size(139, 21);
            this.CheckBoxSummaryTR.TabIndex = 5;
            this.CheckBoxSummaryTR.Text = "Transitions every";
            this.CheckBoxSummaryTR.UseVisualStyleBackColor = true;
            // 
            // TextBoxSummaryTRSCTimesteps
            // 
            this.TextBoxSummaryTRSCTimesteps.Location = new System.Drawing.Point(235, 64);
            this.TextBoxSummaryTRSCTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxSummaryTRSCTimesteps.Name = "TextBoxSummaryTRSCTimesteps";
            this.TextBoxSummaryTRSCTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxSummaryTRSCTimesteps.TabIndex = 11;
            this.TextBoxSummaryTRSCTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LabelSummaryTRTimesteps
            // 
            this.LabelSummaryTRTimesteps.AutoSize = true;
            this.LabelSummaryTRTimesteps.Location = new System.Drawing.Point(314, 36);
            this.LabelSummaryTRTimesteps.Margin = new System.Windows.Forms.Padding(4, 6, 4, 0);
            this.LabelSummaryTRTimesteps.Name = "LabelSummaryTRTimesteps";
            this.LabelSummaryTRTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelSummaryTRTimesteps.TabIndex = 7;
            this.LabelSummaryTRTimesteps.Text = "timesteps";
            // 
            // CheckBoxSummaryTRSC
            // 
            this.CheckBoxSummaryTRSC.AutoSize = true;
            this.CheckBoxSummaryTRSC.Location = new System.Drawing.Point(4, 64);
            this.CheckBoxSummaryTRSC.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxSummaryTRSC.Name = "CheckBoxSummaryTRSC";
            this.CheckBoxSummaryTRSC.Size = new System.Drawing.Size(223, 21);
            this.CheckBoxSummaryTRSC.TabIndex = 10;
            this.CheckBoxSummaryTRSC.Text = "Transitions by state class every";
            this.CheckBoxSummaryTRSC.UseVisualStyleBackColor = true;
            // 
            // TextBoxSummaryTRTimesteps
            // 
            this.TextBoxSummaryTRTimesteps.Location = new System.Drawing.Point(235, 34);
            this.TextBoxSummaryTRTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxSummaryTRTimesteps.Name = "TextBoxSummaryTRTimesteps";
            this.TextBoxSummaryTRTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxSummaryTRTimesteps.TabIndex = 6;
            this.TextBoxSummaryTRTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.TableLayoutPanelMain.ColumnCount = 5;
            this.TableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 231F));
            this.TableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.TableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.TableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 129F));
            this.TableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1234F));
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxSummaryOmitTS, 0, 8);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxSummaryTRCalcIntervalMean, 4, 1);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxSummarySCZeroValues, 4, 0);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxSummarySCAges, 3, 0);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxSummaryTRAges, 3, 1);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxSummarySAAges, 3, 3);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxSummaryTAAges, 3, 4);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxSummaryOmitSS, 0, 7);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxSummarySC, 0, 0);
            this.TableLayoutPanelMain.Controls.Add(this.TextBoxSummaryTRTimesteps, 1, 1);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxSummaryTA, 0, 4);
            this.TableLayoutPanelMain.Controls.Add(this.LabelSummaryTRTimesteps, 2, 1);
            this.TableLayoutPanelMain.Controls.Add(this.LabelSummarySCTimesteps, 2, 0);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxSummarySA, 0, 3);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxSummaryTRSC, 0, 2);
            this.TableLayoutPanelMain.Controls.Add(this.LabelSummaryTRSCTimesteps, 2, 2);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxSummaryTR, 0, 1);
            this.TableLayoutPanelMain.Controls.Add(this.LabelSummaryTATimesteps, 2, 4);
            this.TableLayoutPanelMain.Controls.Add(this.LabelSummarySATimesteps, 2, 3);
            this.TableLayoutPanelMain.Controls.Add(this.TextBoxSummaryTRSCTimesteps, 1, 2);
            this.TableLayoutPanelMain.Controls.Add(this.TextBoxSummaryTATimesteps, 1, 4);
            this.TableLayoutPanelMain.Controls.Add(this.TextBoxSummarySCTimesteps, 1, 0);
            this.TableLayoutPanelMain.Controls.Add(this.TextBoxSummarySATimesteps, 1, 3);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxSummaryEV, 0, 5);
            this.TableLayoutPanelMain.Controls.Add(this.LabelSummaryEVTimesteps, 2, 5);
            this.TableLayoutPanelMain.Controls.Add(this.TextBoxSummaryEVTimesteps, 1, 5);
            this.TableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.TableLayoutPanelMain.Location = new System.Drawing.Point(20, 20);
            this.TableLayoutPanelMain.Name = "TableLayoutPanelMain";
            this.TableLayoutPanelMain.RowCount = 9;
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayoutPanelMain.Size = new System.Drawing.Size(1786, 281);
            this.TableLayoutPanelMain.TabIndex = 23;
            // 
            // CheckBoxSummaryEV
            // 
            this.CheckBoxSummaryEV.AutoSize = true;
            this.CheckBoxSummaryEV.Location = new System.Drawing.Point(4, 154);
            this.CheckBoxSummaryEV.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxSummaryEV.Name = "CheckBoxSummaryEV";
            this.CheckBoxSummaryEV.Size = new System.Drawing.Size(181, 21);
            this.CheckBoxSummaryEV.TabIndex = 21;
            this.CheckBoxSummaryEV.Text = "External variables every";
            this.CheckBoxSummaryEV.UseVisualStyleBackColor = true;
            // 
            // LabelSummaryEVTimesteps
            // 
            this.LabelSummaryEVTimesteps.AutoSize = true;
            this.LabelSummaryEVTimesteps.Location = new System.Drawing.Point(314, 156);
            this.LabelSummaryEVTimesteps.Margin = new System.Windows.Forms.Padding(4, 6, 4, 0);
            this.LabelSummaryEVTimesteps.Name = "LabelSummaryEVTimesteps";
            this.LabelSummaryEVTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelSummaryEVTimesteps.TabIndex = 23;
            this.LabelSummaryEVTimesteps.Text = "timesteps";
            // 
            // TextBoxSummaryEVTimesteps
            // 
            this.TextBoxSummaryEVTimesteps.Location = new System.Drawing.Point(235, 154);
            this.TextBoxSummaryEVTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxSummaryEVTimesteps.Name = "TextBoxSummaryEVTimesteps";
            this.TextBoxSummaryEVTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxSummaryEVTimesteps.TabIndex = 22;
            this.TextBoxSummaryEVTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // OutputOptionsDataFeedView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PanelMain);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "OutputOptionsDataFeedView";
            this.Size = new System.Drawing.Size(1826, 903);
            this.PanelMain.ResumeLayout(false);
            this.TableLayoutPanelMain.ResumeLayout(false);
            this.TableLayoutPanelMain.PerformLayout();
            this.ResumeLayout(false);

        }
        internal System.Windows.Forms.CheckBox CheckBoxSummarySCZeroValues;
        internal System.Windows.Forms.Label LabelSummaryTATimesteps;
        internal System.Windows.Forms.TextBox TextBoxSummaryTATimesteps;
        internal System.Windows.Forms.CheckBox CheckBoxSummaryTA;
        internal System.Windows.Forms.Label LabelSummarySATimesteps;
        internal System.Windows.Forms.TextBox TextBoxSummarySATimesteps;
        internal System.Windows.Forms.CheckBox CheckBoxSummaryTRCalcIntervalMean;
        internal System.Windows.Forms.CheckBox CheckBoxSummarySA;
        internal System.Windows.Forms.TextBox TextBoxSummarySCTimesteps;
        internal System.Windows.Forms.Label LabelSummarySCTimesteps;
        internal System.Windows.Forms.Label LabelSummaryTRSCTimesteps;
        internal System.Windows.Forms.CheckBox CheckBoxSummarySC;
        internal System.Windows.Forms.CheckBox CheckBoxSummaryTR;
        internal System.Windows.Forms.TextBox TextBoxSummaryTRSCTimesteps;
        internal System.Windows.Forms.Label LabelSummaryTRTimesteps;
        internal System.Windows.Forms.CheckBox CheckBoxSummaryTRSC;
        internal System.Windows.Forms.TextBox TextBoxSummaryTRTimesteps;
        internal System.Windows.Forms.CheckBox CheckBoxSummaryOmitTS;
        internal System.Windows.Forms.CheckBox CheckBoxSummaryOmitSS;
        internal System.Windows.Forms.CheckBox CheckBoxSummaryTAAges;
        internal System.Windows.Forms.CheckBox CheckBoxSummarySAAges;
        internal System.Windows.Forms.CheckBox CheckBoxSummaryTRAges;
        internal System.Windows.Forms.CheckBox CheckBoxSummarySCAges;
        private System.Windows.Forms.Panel PanelMain;
        private System.Windows.Forms.TableLayoutPanel TableLayoutPanelMain;
        internal System.Windows.Forms.CheckBox CheckBoxSummaryEV;
        internal System.Windows.Forms.Label LabelSummaryEVTimesteps;
        internal System.Windows.Forms.TextBox TextBoxSummaryEVTimesteps;
    }
}
