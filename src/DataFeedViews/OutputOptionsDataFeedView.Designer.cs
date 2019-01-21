// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

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
            this.GroupBoxSpatialOutput = new System.Windows.Forms.GroupBox();
            this.CheckBoxRasterAATP = new System.Windows.Forms.CheckBox();
            this.LabelRasterAATPTimesteps = new System.Windows.Forms.Label();
            this.TextBoxRasterAATPTimesteps = new System.Windows.Forms.TextBox();
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
            this.GroupBoxSummaryOutput = new System.Windows.Forms.GroupBox();
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
            this.GroupBoxSpatialOutput.SuspendLayout();
            this.GroupBoxSummaryOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBoxSpatialOutput
            // 
            this.GroupBoxSpatialOutput.Controls.Add(this.CheckBoxRasterAATP);
            this.GroupBoxSpatialOutput.Controls.Add(this.LabelRasterAATPTimesteps);
            this.GroupBoxSpatialOutput.Controls.Add(this.TextBoxRasterAATPTimesteps);
            this.GroupBoxSpatialOutput.Controls.Add(this.CheckBoxRasterTA);
            this.GroupBoxSpatialOutput.Controls.Add(this.LabelRasterTATimesteps);
            this.GroupBoxSpatialOutput.Controls.Add(this.TextBoxRasterTATimesteps);
            this.GroupBoxSpatialOutput.Controls.Add(this.CheckBoxRasterSA);
            this.GroupBoxSpatialOutput.Controls.Add(this.LabelRasterSATimesteps);
            this.GroupBoxSpatialOutput.Controls.Add(this.TextBoxRasterSATimesteps);
            this.GroupBoxSpatialOutput.Controls.Add(this.LabelRasterTSTTimesteps);
            this.GroupBoxSpatialOutput.Controls.Add(this.TextBoxRasterTSTTimesteps);
            this.GroupBoxSpatialOutput.Controls.Add(this.CheckBoxRasterTST);
            this.GroupBoxSpatialOutput.Controls.Add(this.TextBoxRasterAgeTimesteps);
            this.GroupBoxSpatialOutput.Controls.Add(this.CheckBoxRasterAge);
            this.GroupBoxSpatialOutput.Controls.Add(this.LabelRasterAgeTimesteps);
            this.GroupBoxSpatialOutput.Controls.Add(this.TextBoxRasterTRTimesteps);
            this.GroupBoxSpatialOutput.Controls.Add(this.CheckBoxRasterST);
            this.GroupBoxSpatialOutput.Controls.Add(this.LabelRasterSTTimesteps);
            this.GroupBoxSpatialOutput.Controls.Add(this.CheckBoxRasterSC);
            this.GroupBoxSpatialOutput.Controls.Add(this.CheckBoxRasterTR);
            this.GroupBoxSpatialOutput.Controls.Add(this.TextBoxRasterSCTimesteps);
            this.GroupBoxSpatialOutput.Controls.Add(this.TextBoxRasterSTTimesteps);
            this.GroupBoxSpatialOutput.Controls.Add(this.LabelRasterTRTimesteps);
            this.GroupBoxSpatialOutput.Controls.Add(this.LabelRasterSCTimesteps);
            this.GroupBoxSpatialOutput.Location = new System.Drawing.Point(3, 216);
            this.GroupBoxSpatialOutput.Name = "GroupBoxSpatialOutput";
            this.GroupBoxSpatialOutput.Size = new System.Drawing.Size(706, 214);
            this.GroupBoxSpatialOutput.TabIndex = 1;
            this.GroupBoxSpatialOutput.TabStop = false;
            this.GroupBoxSpatialOutput.Text = "Spatial output";
            // 
            // CheckBoxRasterAATP
            // 
            this.CheckBoxRasterAATP.AutoSize = true;
            this.CheckBoxRasterAATP.Location = new System.Drawing.Point(11, 184);
            this.CheckBoxRasterAATP.Name = "CheckBoxRasterAATP";
            this.CheckBoxRasterAATP.Size = new System.Drawing.Size(225, 17);
            this.CheckBoxRasterAATP.TabIndex = 21;
            this.CheckBoxRasterAATP.Text = "Average annual transition probability every";
            this.CheckBoxRasterAATP.UseVisualStyleBackColor = true;
            // 
            // LabelRasterAATPTimesteps
            // 
            this.LabelRasterAATPTimesteps.AutoSize = true;
            this.LabelRasterAATPTimesteps.Location = new System.Drawing.Point(312, 184);
            this.LabelRasterAATPTimesteps.Name = "LabelRasterAATPTimesteps";
            this.LabelRasterAATPTimesteps.Size = new System.Drawing.Size(51, 13);
            this.LabelRasterAATPTimesteps.TabIndex = 23;
            this.LabelRasterAATPTimesteps.Text = "timesteps";
            // 
            // TextBoxRasterAATPTimesteps
            // 
            this.TextBoxRasterAATPTimesteps.Location = new System.Drawing.Point(254, 181);
            this.TextBoxRasterAATPTimesteps.Name = "TextBoxRasterAATPTimesteps";
            this.TextBoxRasterAATPTimesteps.Size = new System.Drawing.Size(50, 20);
            this.TextBoxRasterAATPTimesteps.TabIndex = 22;
            this.TextBoxRasterAATPTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxRasterTA
            // 
            this.CheckBoxRasterTA.AutoSize = true;
            this.CheckBoxRasterTA.Location = new System.Drawing.Point(11, 161);
            this.CheckBoxRasterTA.Name = "CheckBoxRasterTA";
            this.CheckBoxRasterTA.Size = new System.Drawing.Size(147, 17);
            this.CheckBoxRasterTA.TabIndex = 18;
            this.CheckBoxRasterTA.Text = "Transition attributes every";
            this.CheckBoxRasterTA.UseVisualStyleBackColor = true;
            // 
            // LabelRasterTATimesteps
            // 
            this.LabelRasterTATimesteps.AutoSize = true;
            this.LabelRasterTATimesteps.Location = new System.Drawing.Point(312, 161);
            this.LabelRasterTATimesteps.Name = "LabelRasterTATimesteps";
            this.LabelRasterTATimesteps.Size = new System.Drawing.Size(51, 13);
            this.LabelRasterTATimesteps.TabIndex = 20;
            this.LabelRasterTATimesteps.Text = "timesteps";
            // 
            // TextBoxRasterTATimesteps
            // 
            this.TextBoxRasterTATimesteps.Location = new System.Drawing.Point(254, 158);
            this.TextBoxRasterTATimesteps.Name = "TextBoxRasterTATimesteps";
            this.TextBoxRasterTATimesteps.Size = new System.Drawing.Size(50, 20);
            this.TextBoxRasterTATimesteps.TabIndex = 19;
            this.TextBoxRasterTATimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxRasterSA
            // 
            this.CheckBoxRasterSA.AutoSize = true;
            this.CheckBoxRasterSA.Location = new System.Drawing.Point(11, 138);
            this.CheckBoxRasterSA.Name = "CheckBoxRasterSA";
            this.CheckBoxRasterSA.Size = new System.Drawing.Size(126, 17);
            this.CheckBoxRasterSA.TabIndex = 15;
            this.CheckBoxRasterSA.Text = "State attributes every";
            this.CheckBoxRasterSA.UseVisualStyleBackColor = true;
            // 
            // LabelRasterSATimesteps
            // 
            this.LabelRasterSATimesteps.AutoSize = true;
            this.LabelRasterSATimesteps.Location = new System.Drawing.Point(312, 138);
            this.LabelRasterSATimesteps.Name = "LabelRasterSATimesteps";
            this.LabelRasterSATimesteps.Size = new System.Drawing.Size(51, 13);
            this.LabelRasterSATimesteps.TabIndex = 17;
            this.LabelRasterSATimesteps.Text = "timesteps";
            // 
            // TextBoxRasterSATimesteps
            // 
            this.TextBoxRasterSATimesteps.Location = new System.Drawing.Point(254, 135);
            this.TextBoxRasterSATimesteps.Name = "TextBoxRasterSATimesteps";
            this.TextBoxRasterSATimesteps.Size = new System.Drawing.Size(50, 20);
            this.TextBoxRasterSATimesteps.TabIndex = 16;
            this.TextBoxRasterSATimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LabelRasterTSTTimesteps
            // 
            this.LabelRasterTSTTimesteps.AutoSize = true;
            this.LabelRasterTSTTimesteps.Location = new System.Drawing.Point(312, 92);
            this.LabelRasterTSTTimesteps.Name = "LabelRasterTSTTimesteps";
            this.LabelRasterTSTTimesteps.Size = new System.Drawing.Size(51, 13);
            this.LabelRasterTSTTimesteps.TabIndex = 11;
            this.LabelRasterTSTTimesteps.Text = "timesteps";
            // 
            // TextBoxRasterTSTTimesteps
            // 
            this.TextBoxRasterTSTTimesteps.Location = new System.Drawing.Point(254, 89);
            this.TextBoxRasterTSTTimesteps.Name = "TextBoxRasterTSTTimesteps";
            this.TextBoxRasterTSTTimesteps.Size = new System.Drawing.Size(50, 20);
            this.TextBoxRasterTSTTimesteps.TabIndex = 10;
            this.TextBoxRasterTSTTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxRasterTST
            // 
            this.CheckBoxRasterTST.AutoSize = true;
            this.CheckBoxRasterTST.Location = new System.Drawing.Point(11, 92);
            this.CheckBoxRasterTST.Name = "CheckBoxRasterTST";
            this.CheckBoxRasterTST.Size = new System.Drawing.Size(151, 17);
            this.CheckBoxRasterTST.TabIndex = 9;
            this.CheckBoxRasterTST.Text = "Time-since-transition every";
            this.CheckBoxRasterTST.UseVisualStyleBackColor = true;
            // 
            // TextBoxRasterAgeTimesteps
            // 
            this.TextBoxRasterAgeTimesteps.Location = new System.Drawing.Point(254, 66);
            this.TextBoxRasterAgeTimesteps.Name = "TextBoxRasterAgeTimesteps";
            this.TextBoxRasterAgeTimesteps.Size = new System.Drawing.Size(50, 20);
            this.TextBoxRasterAgeTimesteps.TabIndex = 7;
            this.TextBoxRasterAgeTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxRasterAge
            // 
            this.CheckBoxRasterAge.AutoSize = true;
            this.CheckBoxRasterAge.Location = new System.Drawing.Point(11, 69);
            this.CheckBoxRasterAge.Name = "CheckBoxRasterAge";
            this.CheckBoxRasterAge.Size = new System.Drawing.Size(79, 17);
            this.CheckBoxRasterAge.TabIndex = 6;
            this.CheckBoxRasterAge.Text = "Ages every";
            this.CheckBoxRasterAge.UseVisualStyleBackColor = true;
            // 
            // LabelRasterAgeTimesteps
            // 
            this.LabelRasterAgeTimesteps.AutoSize = true;
            this.LabelRasterAgeTimesteps.Location = new System.Drawing.Point(312, 69);
            this.LabelRasterAgeTimesteps.Name = "LabelRasterAgeTimesteps";
            this.LabelRasterAgeTimesteps.Size = new System.Drawing.Size(51, 13);
            this.LabelRasterAgeTimesteps.TabIndex = 8;
            this.LabelRasterAgeTimesteps.Text = "timesteps";
            // 
            // TextBoxRasterTRTimesteps
            // 
            this.TextBoxRasterTRTimesteps.Location = new System.Drawing.Point(254, 43);
            this.TextBoxRasterTRTimesteps.Name = "TextBoxRasterTRTimesteps";
            this.TextBoxRasterTRTimesteps.Size = new System.Drawing.Size(50, 20);
            this.TextBoxRasterTRTimesteps.TabIndex = 4;
            this.TextBoxRasterTRTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxRasterST
            // 
            this.CheckBoxRasterST.AutoSize = true;
            this.CheckBoxRasterST.Location = new System.Drawing.Point(11, 115);
            this.CheckBoxRasterST.Name = "CheckBoxRasterST";
            this.CheckBoxRasterST.Size = new System.Drawing.Size(83, 17);
            this.CheckBoxRasterST.TabIndex = 12;
            this.CheckBoxRasterST.Text = "Strata every";
            this.CheckBoxRasterST.UseVisualStyleBackColor = true;
            // 
            // LabelRasterSTTimesteps
            // 
            this.LabelRasterSTTimesteps.AutoSize = true;
            this.LabelRasterSTTimesteps.Location = new System.Drawing.Point(312, 115);
            this.LabelRasterSTTimesteps.Name = "LabelRasterSTTimesteps";
            this.LabelRasterSTTimesteps.Size = new System.Drawing.Size(51, 13);
            this.LabelRasterSTTimesteps.TabIndex = 14;
            this.LabelRasterSTTimesteps.Text = "timesteps";
            // 
            // CheckBoxRasterSC
            // 
            this.CheckBoxRasterSC.AutoSize = true;
            this.CheckBoxRasterSC.Location = new System.Drawing.Point(11, 23);
            this.CheckBoxRasterSC.Name = "CheckBoxRasterSC";
            this.CheckBoxRasterSC.Size = new System.Drawing.Size(118, 17);
            this.CheckBoxRasterSC.TabIndex = 0;
            this.CheckBoxRasterSC.Text = "State classes every";
            this.CheckBoxRasterSC.UseVisualStyleBackColor = true;
            // 
            // CheckBoxRasterTR
            // 
            this.CheckBoxRasterTR.AutoSize = true;
            this.CheckBoxRasterTR.Location = new System.Drawing.Point(11, 46);
            this.CheckBoxRasterTR.Name = "CheckBoxRasterTR";
            this.CheckBoxRasterTR.Size = new System.Drawing.Size(106, 17);
            this.CheckBoxRasterTR.TabIndex = 3;
            this.CheckBoxRasterTR.Text = "Transitions every";
            this.CheckBoxRasterTR.UseVisualStyleBackColor = true;
            // 
            // TextBoxRasterSCTimesteps
            // 
            this.TextBoxRasterSCTimesteps.Location = new System.Drawing.Point(254, 20);
            this.TextBoxRasterSCTimesteps.Name = "TextBoxRasterSCTimesteps";
            this.TextBoxRasterSCTimesteps.Size = new System.Drawing.Size(50, 20);
            this.TextBoxRasterSCTimesteps.TabIndex = 1;
            this.TextBoxRasterSCTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TextBoxRasterSTTimesteps
            // 
            this.TextBoxRasterSTTimesteps.Location = new System.Drawing.Point(254, 112);
            this.TextBoxRasterSTTimesteps.Name = "TextBoxRasterSTTimesteps";
            this.TextBoxRasterSTTimesteps.Size = new System.Drawing.Size(50, 20);
            this.TextBoxRasterSTTimesteps.TabIndex = 13;
            this.TextBoxRasterSTTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LabelRasterTRTimesteps
            // 
            this.LabelRasterTRTimesteps.AutoSize = true;
            this.LabelRasterTRTimesteps.Location = new System.Drawing.Point(312, 46);
            this.LabelRasterTRTimesteps.Name = "LabelRasterTRTimesteps";
            this.LabelRasterTRTimesteps.Size = new System.Drawing.Size(51, 13);
            this.LabelRasterTRTimesteps.TabIndex = 5;
            this.LabelRasterTRTimesteps.Text = "timesteps";
            // 
            // LabelRasterSCTimesteps
            // 
            this.LabelRasterSCTimesteps.AutoSize = true;
            this.LabelRasterSCTimesteps.Location = new System.Drawing.Point(312, 23);
            this.LabelRasterSCTimesteps.Name = "LabelRasterSCTimesteps";
            this.LabelRasterSCTimesteps.Size = new System.Drawing.Size(51, 13);
            this.LabelRasterSCTimesteps.TabIndex = 2;
            this.LabelRasterSCTimesteps.Text = "timesteps";
            // 
            // GroupBoxSummaryOutput
            // 
            this.GroupBoxSummaryOutput.Controls.Add(this.CheckBoxSummaryTAAges);
            this.GroupBoxSummaryOutput.Controls.Add(this.CheckBoxSummarySAAges);
            this.GroupBoxSummaryOutput.Controls.Add(this.CheckBoxSummaryTRAges);
            this.GroupBoxSummaryOutput.Controls.Add(this.CheckBoxSummarySCAges);
            this.GroupBoxSummaryOutput.Controls.Add(this.CheckBoxSummaryOmitTS);
            this.GroupBoxSummaryOutput.Controls.Add(this.CheckBoxSummaryOmitSS);
            this.GroupBoxSummaryOutput.Controls.Add(this.CheckBoxSummarySCZeroValues);
            this.GroupBoxSummaryOutput.Controls.Add(this.LabelSummaryTATimesteps);
            this.GroupBoxSummaryOutput.Controls.Add(this.TextBoxSummaryTATimesteps);
            this.GroupBoxSummaryOutput.Controls.Add(this.CheckBoxSummaryTA);
            this.GroupBoxSummaryOutput.Controls.Add(this.LabelSummarySATimesteps);
            this.GroupBoxSummaryOutput.Controls.Add(this.TextBoxSummarySATimesteps);
            this.GroupBoxSummaryOutput.Controls.Add(this.CheckBoxSummaryTRCalcIntervalMean);
            this.GroupBoxSummaryOutput.Controls.Add(this.CheckBoxSummarySA);
            this.GroupBoxSummaryOutput.Controls.Add(this.TextBoxSummarySCTimesteps);
            this.GroupBoxSummaryOutput.Controls.Add(this.LabelSummarySCTimesteps);
            this.GroupBoxSummaryOutput.Controls.Add(this.LabelSummaryTRSCTimesteps);
            this.GroupBoxSummaryOutput.Controls.Add(this.CheckBoxSummarySC);
            this.GroupBoxSummaryOutput.Controls.Add(this.CheckBoxSummaryTR);
            this.GroupBoxSummaryOutput.Controls.Add(this.TextBoxSummaryTRSCTimesteps);
            this.GroupBoxSummaryOutput.Controls.Add(this.LabelSummaryTRTimesteps);
            this.GroupBoxSummaryOutput.Controls.Add(this.CheckBoxSummaryTRSC);
            this.GroupBoxSummaryOutput.Controls.Add(this.TextBoxSummaryTRTimesteps);
            this.GroupBoxSummaryOutput.Location = new System.Drawing.Point(3, 15);
            this.GroupBoxSummaryOutput.Name = "GroupBoxSummaryOutput";
            this.GroupBoxSummaryOutput.Size = new System.Drawing.Size(706, 181);
            this.GroupBoxSummaryOutput.TabIndex = 0;
            this.GroupBoxSummaryOutput.TabStop = false;
            this.GroupBoxSummaryOutput.Text = "Tabular output";
            // 
            // CheckBoxSummaryTAAges
            // 
            this.CheckBoxSummaryTAAges.AutoSize = true;
            this.CheckBoxSummaryTAAges.Location = new System.Drawing.Point(393, 113);
            this.CheckBoxSummaryTAAges.Name = "CheckBoxSummaryTAAges";
            this.CheckBoxSummaryTAAges.Size = new System.Drawing.Size(87, 17);
            this.CheckBoxSummaryTAAges.TabIndex = 20;
            this.CheckBoxSummaryTAAges.Text = "Include ages";
            this.CheckBoxSummaryTAAges.UseVisualStyleBackColor = true;
            // 
            // CheckBoxSummarySAAges
            // 
            this.CheckBoxSummarySAAges.AutoSize = true;
            this.CheckBoxSummarySAAges.Location = new System.Drawing.Point(393, 91);
            this.CheckBoxSummarySAAges.Name = "CheckBoxSummarySAAges";
            this.CheckBoxSummarySAAges.Size = new System.Drawing.Size(87, 17);
            this.CheckBoxSummarySAAges.TabIndex = 16;
            this.CheckBoxSummarySAAges.Text = "Include ages";
            this.CheckBoxSummarySAAges.UseVisualStyleBackColor = true;
            // 
            // CheckBoxSummaryTRAges
            // 
            this.CheckBoxSummaryTRAges.AutoSize = true;
            this.CheckBoxSummaryTRAges.Location = new System.Drawing.Point(393, 44);
            this.CheckBoxSummaryTRAges.Name = "CheckBoxSummaryTRAges";
            this.CheckBoxSummaryTRAges.Size = new System.Drawing.Size(87, 17);
            this.CheckBoxSummaryTRAges.TabIndex = 8;
            this.CheckBoxSummaryTRAges.Text = "Include ages";
            this.CheckBoxSummaryTRAges.UseVisualStyleBackColor = true;
            // 
            // CheckBoxSummarySCAges
            // 
            this.CheckBoxSummarySCAges.AutoSize = true;
            this.CheckBoxSummarySCAges.Location = new System.Drawing.Point(393, 22);
            this.CheckBoxSummarySCAges.Name = "CheckBoxSummarySCAges";
            this.CheckBoxSummarySCAges.Size = new System.Drawing.Size(87, 17);
            this.CheckBoxSummarySCAges.TabIndex = 3;
            this.CheckBoxSummarySCAges.Text = "Include ages";
            this.CheckBoxSummarySCAges.UseVisualStyleBackColor = true;
            // 
            // CheckBoxSummaryOmitTS
            // 
            this.CheckBoxSummaryOmitTS.AutoSize = true;
            this.CheckBoxSummaryOmitTS.Location = new System.Drawing.Point(11, 154);
            this.CheckBoxSummaryOmitTS.Name = "CheckBoxSummaryOmitTS";
            this.CheckBoxSummaryOmitTS.Size = new System.Drawing.Size(110, 17);
            this.CheckBoxSummaryOmitTS.TabIndex = 22;
            this.CheckBoxSummaryOmitTS.Text = "Omit tertiary strata";
            this.CheckBoxSummaryOmitTS.UseVisualStyleBackColor = true;
            // 
            // CheckBoxSummaryOmitSS
            // 
            this.CheckBoxSummaryOmitSS.AutoSize = true;
            this.CheckBoxSummaryOmitSS.Location = new System.Drawing.Point(11, 132);
            this.CheckBoxSummaryOmitSS.Name = "CheckBoxSummaryOmitSS";
            this.CheckBoxSummaryOmitSS.Size = new System.Drawing.Size(128, 17);
            this.CheckBoxSummaryOmitSS.TabIndex = 21;
            this.CheckBoxSummaryOmitSS.Text = "Omit secondary strata";
            this.CheckBoxSummaryOmitSS.UseVisualStyleBackColor = true;
            // 
            // CheckBoxSummarySCZeroValues
            // 
            this.CheckBoxSummarySCZeroValues.AutoSize = true;
            this.CheckBoxSummarySCZeroValues.Location = new System.Drawing.Point(507, 22);
            this.CheckBoxSummarySCZeroValues.Name = "CheckBoxSummarySCZeroValues";
            this.CheckBoxSummarySCZeroValues.Size = new System.Drawing.Size(118, 17);
            this.CheckBoxSummarySCZeroValues.TabIndex = 4;
            this.CheckBoxSummarySCZeroValues.Text = "Include zero values";
            this.CheckBoxSummarySCZeroValues.UseVisualStyleBackColor = true;
            // 
            // LabelSummaryTATimesteps
            // 
            this.LabelSummaryTATimesteps.AutoSize = true;
            this.LabelSummaryTATimesteps.Location = new System.Drawing.Point(310, 114);
            this.LabelSummaryTATimesteps.Name = "LabelSummaryTATimesteps";
            this.LabelSummaryTATimesteps.Size = new System.Drawing.Size(51, 13);
            this.LabelSummaryTATimesteps.TabIndex = 19;
            this.LabelSummaryTATimesteps.Text = "timesteps";
            // 
            // TextBoxSummaryTATimesteps
            // 
            this.TextBoxSummaryTATimesteps.Location = new System.Drawing.Point(254, 111);
            this.TextBoxSummaryTATimesteps.Name = "TextBoxSummaryTATimesteps";
            this.TextBoxSummaryTATimesteps.Size = new System.Drawing.Size(50, 20);
            this.TextBoxSummaryTATimesteps.TabIndex = 18;
            this.TextBoxSummaryTATimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxSummaryTA
            // 
            this.CheckBoxSummaryTA.AutoSize = true;
            this.CheckBoxSummaryTA.Location = new System.Drawing.Point(11, 110);
            this.CheckBoxSummaryTA.Name = "CheckBoxSummaryTA";
            this.CheckBoxSummaryTA.Size = new System.Drawing.Size(147, 17);
            this.CheckBoxSummaryTA.TabIndex = 17;
            this.CheckBoxSummaryTA.Text = "Transition attributes every";
            this.CheckBoxSummaryTA.UseVisualStyleBackColor = true;
            // 
            // LabelSummarySATimesteps
            // 
            this.LabelSummarySATimesteps.AutoSize = true;
            this.LabelSummarySATimesteps.Location = new System.Drawing.Point(310, 91);
            this.LabelSummarySATimesteps.Name = "LabelSummarySATimesteps";
            this.LabelSummarySATimesteps.Size = new System.Drawing.Size(51, 13);
            this.LabelSummarySATimesteps.TabIndex = 15;
            this.LabelSummarySATimesteps.Text = "timesteps";
            // 
            // TextBoxSummarySATimesteps
            // 
            this.TextBoxSummarySATimesteps.Location = new System.Drawing.Point(254, 88);
            this.TextBoxSummarySATimesteps.Name = "TextBoxSummarySATimesteps";
            this.TextBoxSummarySATimesteps.Size = new System.Drawing.Size(50, 20);
            this.TextBoxSummarySATimesteps.TabIndex = 14;
            this.TextBoxSummarySATimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxSummaryTRCalcIntervalMean
            // 
            this.CheckBoxSummaryTRCalcIntervalMean.AutoSize = true;
            this.CheckBoxSummaryTRCalcIntervalMean.Location = new System.Drawing.Point(507, 44);
            this.CheckBoxSummaryTRCalcIntervalMean.Name = "CheckBoxSummaryTRCalcIntervalMean";
            this.CheckBoxSummaryTRCalcIntervalMean.Size = new System.Drawing.Size(184, 17);
            this.CheckBoxSummaryTRCalcIntervalMean.TabIndex = 9;
            this.CheckBoxSummaryTRCalcIntervalMean.Text = "Calculate as interval mean values";
            this.CheckBoxSummaryTRCalcIntervalMean.UseVisualStyleBackColor = true;
            // 
            // CheckBoxSummarySA
            // 
            this.CheckBoxSummarySA.AutoSize = true;
            this.CheckBoxSummarySA.Location = new System.Drawing.Point(11, 88);
            this.CheckBoxSummarySA.Name = "CheckBoxSummarySA";
            this.CheckBoxSummarySA.Size = new System.Drawing.Size(126, 17);
            this.CheckBoxSummarySA.TabIndex = 13;
            this.CheckBoxSummarySA.Text = "State attributes every";
            this.CheckBoxSummarySA.UseVisualStyleBackColor = true;
            // 
            // TextBoxSummarySCTimesteps
            // 
            this.TextBoxSummarySCTimesteps.Location = new System.Drawing.Point(254, 19);
            this.TextBoxSummarySCTimesteps.Name = "TextBoxSummarySCTimesteps";
            this.TextBoxSummarySCTimesteps.Size = new System.Drawing.Size(50, 20);
            this.TextBoxSummarySCTimesteps.TabIndex = 1;
            this.TextBoxSummarySCTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LabelSummarySCTimesteps
            // 
            this.LabelSummarySCTimesteps.AutoSize = true;
            this.LabelSummarySCTimesteps.Location = new System.Drawing.Point(310, 22);
            this.LabelSummarySCTimesteps.Name = "LabelSummarySCTimesteps";
            this.LabelSummarySCTimesteps.Size = new System.Drawing.Size(51, 13);
            this.LabelSummarySCTimesteps.TabIndex = 2;
            this.LabelSummarySCTimesteps.Text = "timesteps";
            // 
            // LabelSummaryTRSCTimesteps
            // 
            this.LabelSummaryTRSCTimesteps.AutoSize = true;
            this.LabelSummaryTRSCTimesteps.Location = new System.Drawing.Point(310, 68);
            this.LabelSummaryTRSCTimesteps.Name = "LabelSummaryTRSCTimesteps";
            this.LabelSummaryTRSCTimesteps.Size = new System.Drawing.Size(51, 13);
            this.LabelSummaryTRSCTimesteps.TabIndex = 12;
            this.LabelSummaryTRSCTimesteps.Text = "timesteps";
            // 
            // CheckBoxSummarySC
            // 
            this.CheckBoxSummarySC.AutoSize = true;
            this.CheckBoxSummarySC.Location = new System.Drawing.Point(11, 22);
            this.CheckBoxSummarySC.Name = "CheckBoxSummarySC";
            this.CheckBoxSummarySC.Size = new System.Drawing.Size(118, 17);
            this.CheckBoxSummarySC.TabIndex = 0;
            this.CheckBoxSummarySC.Text = "State classes every";
            this.CheckBoxSummarySC.UseVisualStyleBackColor = true;
            // 
            // CheckBoxSummaryTR
            // 
            this.CheckBoxSummaryTR.AutoSize = true;
            this.CheckBoxSummaryTR.Location = new System.Drawing.Point(11, 44);
            this.CheckBoxSummaryTR.Name = "CheckBoxSummaryTR";
            this.CheckBoxSummaryTR.Size = new System.Drawing.Size(106, 17);
            this.CheckBoxSummaryTR.TabIndex = 5;
            this.CheckBoxSummaryTR.Text = "Transitions every";
            this.CheckBoxSummaryTR.UseVisualStyleBackColor = true;
            // 
            // TextBoxSummaryTRSCTimesteps
            // 
            this.TextBoxSummaryTRSCTimesteps.Location = new System.Drawing.Point(254, 65);
            this.TextBoxSummaryTRSCTimesteps.Name = "TextBoxSummaryTRSCTimesteps";
            this.TextBoxSummaryTRSCTimesteps.Size = new System.Drawing.Size(50, 20);
            this.TextBoxSummaryTRSCTimesteps.TabIndex = 11;
            this.TextBoxSummaryTRSCTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LabelSummaryTRTimesteps
            // 
            this.LabelSummaryTRTimesteps.AutoSize = true;
            this.LabelSummaryTRTimesteps.Location = new System.Drawing.Point(310, 45);
            this.LabelSummaryTRTimesteps.Name = "LabelSummaryTRTimesteps";
            this.LabelSummaryTRTimesteps.Size = new System.Drawing.Size(51, 13);
            this.LabelSummaryTRTimesteps.TabIndex = 7;
            this.LabelSummaryTRTimesteps.Text = "timesteps";
            // 
            // CheckBoxSummaryTRSC
            // 
            this.CheckBoxSummaryTRSC.AutoSize = true;
            this.CheckBoxSummaryTRSC.Location = new System.Drawing.Point(11, 66);
            this.CheckBoxSummaryTRSC.Name = "CheckBoxSummaryTRSC";
            this.CheckBoxSummaryTRSC.Size = new System.Drawing.Size(173, 17);
            this.CheckBoxSummaryTRSC.TabIndex = 10;
            this.CheckBoxSummaryTRSC.Text = "Transitions by state class every";
            this.CheckBoxSummaryTRSC.UseVisualStyleBackColor = true;
            // 
            // TextBoxSummaryTRTimesteps
            // 
            this.TextBoxSummaryTRTimesteps.Location = new System.Drawing.Point(254, 42);
            this.TextBoxSummaryTRTimesteps.Name = "TextBoxSummaryTRTimesteps";
            this.TextBoxSummaryTRTimesteps.Size = new System.Drawing.Size(50, 20);
            this.TextBoxSummaryTRTimesteps.TabIndex = 6;
            this.TextBoxSummaryTRTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // OutputOptionsDataFeedView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GroupBoxSpatialOutput);
            this.Controls.Add(this.GroupBoxSummaryOutput);
            this.Name = "OutputOptionsDataFeedView";
            this.Size = new System.Drawing.Size(718, 440);
            this.GroupBoxSpatialOutput.ResumeLayout(false);
            this.GroupBoxSpatialOutput.PerformLayout();
            this.GroupBoxSummaryOutput.ResumeLayout(false);
            this.GroupBoxSummaryOutput.PerformLayout();
            this.ResumeLayout(false);

        }
        internal System.Windows.Forms.GroupBox GroupBoxSpatialOutput;
        internal System.Windows.Forms.CheckBox CheckBoxRasterAATP;
        internal System.Windows.Forms.Label LabelRasterAATPTimesteps;
        internal System.Windows.Forms.TextBox TextBoxRasterAATPTimesteps;
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
        internal System.Windows.Forms.GroupBox GroupBoxSummaryOutput;
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
    }
}
