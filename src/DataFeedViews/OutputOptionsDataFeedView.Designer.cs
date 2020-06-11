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
            this.CheckBoxAvgRasterTP = new System.Windows.Forms.CheckBox();
            this.LabelAvgRasterTPTimesteps = new System.Windows.Forms.Label();
            this.TextBoxAvgRasterTPTimesteps = new System.Windows.Forms.TextBox();
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
            this.CheckBoxAvgRasterSTAcrossTimesteps = new System.Windows.Forms.CheckBox();
            this.CheckBoxAvgRasterST = new System.Windows.Forms.CheckBox();
            this.LabelAvgRasterSTTimesteps = new System.Windows.Forms.Label();
            this.TextBoxAvgRasterSTTimesteps = new System.Windows.Forms.TextBox();
            this.CheckBoxAvgRasterAgeAcrossTimesteps = new System.Windows.Forms.CheckBox();
            this.CheckBoxAvgRasterAge = new System.Windows.Forms.CheckBox();
            this.LabelAvgRasterAgeTimesteps = new System.Windows.Forms.Label();
            this.TextBoxAvgRasterAgeTimesteps = new System.Windows.Forms.TextBox();
            this.CheckBoxAvgRasterTPAcrossTimesteps = new System.Windows.Forms.CheckBox();
            this.CheckBoxAvgRasterTAAcrossTimesteps = new System.Windows.Forms.CheckBox();
            this.CheckBoxAvgRasterSAAcrossTimesteps = new System.Windows.Forms.CheckBox();
            this.CheckBoxAvgRasterSCAcrossTimesteps = new System.Windows.Forms.CheckBox();
            this.CheckBoxAvgRasterTA = new System.Windows.Forms.CheckBox();
            this.CheckBoxAvgRasterSC = new System.Windows.Forms.CheckBox();
            this.LabelAvgRasterTATimesteps = new System.Windows.Forms.Label();
            this.LabelAvgRasterSCTimesteps = new System.Windows.Forms.Label();
            this.TextBoxAvgRasterTATimesteps = new System.Windows.Forms.TextBox();
            this.TextBoxAvgRasterSCTimesteps = new System.Windows.Forms.TextBox();
            this.CheckBoxAvgRasterSA = new System.Windows.Forms.CheckBox();
            this.LabelAvgRasterSATimesteps = new System.Windows.Forms.Label();
            this.TextBoxAvgRasterSATimesteps = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.LabelAvgSpatialTitle = new System.Windows.Forms.Label();
            this.LabelSpatialTitle = new System.Windows.Forms.Label();
            this.LabelTabularTitle = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CheckBoxRasterTE
            // 
            this.CheckBoxRasterTE.AutoSize = true;
            this.CheckBoxRasterTE.Location = new System.Drawing.Point(451, 317);
            this.CheckBoxRasterTE.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxRasterTE.Name = "CheckBoxRasterTE";
            this.CheckBoxRasterTE.Size = new System.Drawing.Size(178, 21);
            this.CheckBoxRasterTE.TabIndex = 24;
            this.CheckBoxRasterTE.Text = "Transition events every";
            this.CheckBoxRasterTE.UseVisualStyleBackColor = true;
            // 
            // LabelRasterTETimesteps
            // 
            this.LabelRasterTETimesteps.AutoSize = true;
            this.LabelRasterTETimesteps.Location = new System.Drawing.Point(737, 319);
            this.LabelRasterTETimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelRasterTETimesteps.Name = "LabelRasterTETimesteps";
            this.LabelRasterTETimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelRasterTETimesteps.TabIndex = 26;
            this.LabelRasterTETimesteps.Text = "timesteps";
            // 
            // TextBoxRasterTETimesteps
            // 
            this.TextBoxRasterTETimesteps.Location = new System.Drawing.Point(660, 316);
            this.TextBoxRasterTETimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxRasterTETimesteps.Name = "TextBoxRasterTETimesteps";
            this.TextBoxRasterTETimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxRasterTETimesteps.TabIndex = 25;
            this.TextBoxRasterTETimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxRasterTA
            // 
            this.CheckBoxRasterTA.AutoSize = true;
            this.CheckBoxRasterTA.Location = new System.Drawing.Point(451, 291);
            this.CheckBoxRasterTA.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxRasterTA.Name = "CheckBoxRasterTA";
            this.CheckBoxRasterTA.Size = new System.Drawing.Size(195, 21);
            this.CheckBoxRasterTA.TabIndex = 18;
            this.CheckBoxRasterTA.Text = "Transition attributes every";
            this.CheckBoxRasterTA.UseVisualStyleBackColor = true;
            // 
            // LabelRasterTATimesteps
            // 
            this.LabelRasterTATimesteps.AutoSize = true;
            this.LabelRasterTATimesteps.Location = new System.Drawing.Point(737, 293);
            this.LabelRasterTATimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelRasterTATimesteps.Name = "LabelRasterTATimesteps";
            this.LabelRasterTATimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelRasterTATimesteps.TabIndex = 20;
            this.LabelRasterTATimesteps.Text = "timesteps";
            // 
            // TextBoxRasterTATimesteps
            // 
            this.TextBoxRasterTATimesteps.Location = new System.Drawing.Point(660, 289);
            this.TextBoxRasterTATimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxRasterTATimesteps.Name = "TextBoxRasterTATimesteps";
            this.TextBoxRasterTATimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxRasterTATimesteps.TabIndex = 19;
            this.TextBoxRasterTATimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxRasterSA
            // 
            this.CheckBoxRasterSA.AutoSize = true;
            this.CheckBoxRasterSA.Location = new System.Drawing.Point(451, 265);
            this.CheckBoxRasterSA.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxRasterSA.Name = "CheckBoxRasterSA";
            this.CheckBoxRasterSA.Size = new System.Drawing.Size(165, 21);
            this.CheckBoxRasterSA.TabIndex = 15;
            this.CheckBoxRasterSA.Text = "State attributes every";
            this.CheckBoxRasterSA.UseVisualStyleBackColor = true;
            // 
            // LabelRasterSATimesteps
            // 
            this.LabelRasterSATimesteps.AutoSize = true;
            this.LabelRasterSATimesteps.Location = new System.Drawing.Point(737, 267);
            this.LabelRasterSATimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelRasterSATimesteps.Name = "LabelRasterSATimesteps";
            this.LabelRasterSATimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelRasterSATimesteps.TabIndex = 17;
            this.LabelRasterSATimesteps.Text = "timesteps";
            // 
            // TextBoxRasterSATimesteps
            // 
            this.TextBoxRasterSATimesteps.Location = new System.Drawing.Point(660, 262);
            this.TextBoxRasterSATimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxRasterSATimesteps.Name = "TextBoxRasterSATimesteps";
            this.TextBoxRasterSATimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxRasterSATimesteps.TabIndex = 16;
            this.TextBoxRasterSATimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LabelRasterTSTTimesteps
            // 
            this.LabelRasterTSTTimesteps.AutoSize = true;
            this.LabelRasterTSTTimesteps.Location = new System.Drawing.Point(343, 319);
            this.LabelRasterTSTTimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelRasterTSTTimesteps.Name = "LabelRasterTSTTimesteps";
            this.LabelRasterTSTTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelRasterTSTTimesteps.TabIndex = 11;
            this.LabelRasterTSTTimesteps.Text = "timesteps";
            // 
            // TextBoxRasterTSTTimesteps
            // 
            this.TextBoxRasterTSTTimesteps.Location = new System.Drawing.Point(266, 316);
            this.TextBoxRasterTSTTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxRasterTSTTimesteps.Name = "TextBoxRasterTSTTimesteps";
            this.TextBoxRasterTSTTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxRasterTSTTimesteps.TabIndex = 10;
            this.TextBoxRasterTSTTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxRasterTST
            // 
            this.CheckBoxRasterTST.AutoSize = true;
            this.CheckBoxRasterTST.Location = new System.Drawing.Point(19, 317);
            this.CheckBoxRasterTST.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxRasterTST.Name = "CheckBoxRasterTST";
            this.CheckBoxRasterTST.Size = new System.Drawing.Size(201, 21);
            this.CheckBoxRasterTST.TabIndex = 9;
            this.CheckBoxRasterTST.Text = "Time-since-transition every";
            this.CheckBoxRasterTST.UseVisualStyleBackColor = true;
            // 
            // TextBoxRasterAgeTimesteps
            // 
            this.TextBoxRasterAgeTimesteps.Location = new System.Drawing.Point(266, 289);
            this.TextBoxRasterAgeTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxRasterAgeTimesteps.Name = "TextBoxRasterAgeTimesteps";
            this.TextBoxRasterAgeTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxRasterAgeTimesteps.TabIndex = 7;
            this.TextBoxRasterAgeTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxRasterAge
            // 
            this.CheckBoxRasterAge.AutoSize = true;
            this.CheckBoxRasterAge.Location = new System.Drawing.Point(19, 291);
            this.CheckBoxRasterAge.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxRasterAge.Name = "CheckBoxRasterAge";
            this.CheckBoxRasterAge.Size = new System.Drawing.Size(101, 21);
            this.CheckBoxRasterAge.TabIndex = 6;
            this.CheckBoxRasterAge.Text = "Ages every";
            this.CheckBoxRasterAge.UseVisualStyleBackColor = true;
            // 
            // LabelRasterAgeTimesteps
            // 
            this.LabelRasterAgeTimesteps.AutoSize = true;
            this.LabelRasterAgeTimesteps.Location = new System.Drawing.Point(343, 292);
            this.LabelRasterAgeTimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelRasterAgeTimesteps.Name = "LabelRasterAgeTimesteps";
            this.LabelRasterAgeTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelRasterAgeTimesteps.TabIndex = 8;
            this.LabelRasterAgeTimesteps.Text = "timesteps";
            // 
            // TextBoxRasterTRTimesteps
            // 
            this.TextBoxRasterTRTimesteps.Location = new System.Drawing.Point(266, 262);
            this.TextBoxRasterTRTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxRasterTRTimesteps.Name = "TextBoxRasterTRTimesteps";
            this.TextBoxRasterTRTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxRasterTRTimesteps.TabIndex = 4;
            this.TextBoxRasterTRTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxRasterST
            // 
            this.CheckBoxRasterST.AutoSize = true;
            this.CheckBoxRasterST.Location = new System.Drawing.Point(451, 239);
            this.CheckBoxRasterST.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxRasterST.Name = "CheckBoxRasterST";
            this.CheckBoxRasterST.Size = new System.Drawing.Size(107, 21);
            this.CheckBoxRasterST.TabIndex = 12;
            this.CheckBoxRasterST.Text = "Strata every";
            this.CheckBoxRasterST.UseVisualStyleBackColor = true;
            // 
            // LabelRasterSTTimesteps
            // 
            this.LabelRasterSTTimesteps.AutoSize = true;
            this.LabelRasterSTTimesteps.Location = new System.Drawing.Point(737, 241);
            this.LabelRasterSTTimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelRasterSTTimesteps.Name = "LabelRasterSTTimesteps";
            this.LabelRasterSTTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelRasterSTTimesteps.TabIndex = 14;
            this.LabelRasterSTTimesteps.Text = "timesteps";
            // 
            // CheckBoxRasterSC
            // 
            this.CheckBoxRasterSC.AutoSize = true;
            this.CheckBoxRasterSC.Location = new System.Drawing.Point(19, 239);
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
            this.CheckBoxRasterTR.Location = new System.Drawing.Point(19, 265);
            this.CheckBoxRasterTR.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxRasterTR.Name = "CheckBoxRasterTR";
            this.CheckBoxRasterTR.Size = new System.Drawing.Size(139, 21);
            this.CheckBoxRasterTR.TabIndex = 3;
            this.CheckBoxRasterTR.Text = "Transitions every";
            this.CheckBoxRasterTR.UseVisualStyleBackColor = true;
            // 
            // TextBoxRasterSCTimesteps
            // 
            this.TextBoxRasterSCTimesteps.Location = new System.Drawing.Point(266, 235);
            this.TextBoxRasterSCTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxRasterSCTimesteps.Name = "TextBoxRasterSCTimesteps";
            this.TextBoxRasterSCTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxRasterSCTimesteps.TabIndex = 1;
            this.TextBoxRasterSCTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TextBoxRasterSTTimesteps
            // 
            this.TextBoxRasterSTTimesteps.Location = new System.Drawing.Point(660, 235);
            this.TextBoxRasterSTTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxRasterSTTimesteps.Name = "TextBoxRasterSTTimesteps";
            this.TextBoxRasterSTTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxRasterSTTimesteps.TabIndex = 13;
            this.TextBoxRasterSTTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LabelRasterTRTimesteps
            // 
            this.LabelRasterTRTimesteps.AutoSize = true;
            this.LabelRasterTRTimesteps.Location = new System.Drawing.Point(343, 266);
            this.LabelRasterTRTimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelRasterTRTimesteps.Name = "LabelRasterTRTimesteps";
            this.LabelRasterTRTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelRasterTRTimesteps.TabIndex = 5;
            this.LabelRasterTRTimesteps.Text = "timesteps";
            // 
            // LabelRasterSCTimesteps
            // 
            this.LabelRasterSCTimesteps.AutoSize = true;
            this.LabelRasterSCTimesteps.Location = new System.Drawing.Point(343, 239);
            this.LabelRasterSCTimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelRasterSCTimesteps.Name = "LabelRasterSCTimesteps";
            this.LabelRasterSCTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelRasterSCTimesteps.TabIndex = 2;
            this.LabelRasterSCTimesteps.Text = "timesteps";
            // 
            // CheckBoxAvgRasterTP
            // 
            this.CheckBoxAvgRasterTP.AutoSize = true;
            this.CheckBoxAvgRasterTP.Location = new System.Drawing.Point(18, 510);
            this.CheckBoxAvgRasterTP.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxAvgRasterTP.Name = "CheckBoxAvgRasterTP";
            this.CheckBoxAvgRasterTP.Size = new System.Drawing.Size(201, 21);
            this.CheckBoxAvgRasterTP.TabIndex = 20;
            this.CheckBoxAvgRasterTP.Text = "Transition probability every";
            this.CheckBoxAvgRasterTP.UseVisualStyleBackColor = true;
            // 
            // LabelAvgRasterTPTimesteps
            // 
            this.LabelAvgRasterTPTimesteps.AutoSize = true;
            this.LabelAvgRasterTPTimesteps.Location = new System.Drawing.Point(342, 512);
            this.LabelAvgRasterTPTimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelAvgRasterTPTimesteps.Name = "LabelAvgRasterTPTimesteps";
            this.LabelAvgRasterTPTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelAvgRasterTPTimesteps.TabIndex = 22;
            this.LabelAvgRasterTPTimesteps.Text = "timesteps";
            // 
            // TextBoxAvgRasterTPTimesteps
            // 
            this.TextBoxAvgRasterTPTimesteps.Location = new System.Drawing.Point(265, 507);
            this.TextBoxAvgRasterTPTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxAvgRasterTPTimesteps.Name = "TextBoxAvgRasterTPTimesteps";
            this.TextBoxAvgRasterTPTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxAvgRasterTPTimesteps.TabIndex = 21;
            this.TextBoxAvgRasterTPTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxSummaryTAAges
            // 
            this.CheckBoxSummaryTAAges.AutoSize = true;
            this.CheckBoxSummaryTAAges.Location = new System.Drawing.Point(451, 135);
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
            this.CheckBoxSummarySAAges.Location = new System.Drawing.Point(451, 109);
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
            this.CheckBoxSummaryTRAges.Location = new System.Drawing.Point(451, 53);
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
            this.CheckBoxSummarySCAges.Location = new System.Drawing.Point(451, 27);
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
            this.CheckBoxSummaryOmitTS.Location = new System.Drawing.Point(19, 183);
            this.CheckBoxSummaryOmitTS.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxSummaryOmitTS.Name = "CheckBoxSummaryOmitTS";
            this.CheckBoxSummaryOmitTS.Size = new System.Drawing.Size(147, 21);
            this.CheckBoxSummaryOmitTS.TabIndex = 22;
            this.CheckBoxSummaryOmitTS.Text = "Omit tertiary strata";
            this.CheckBoxSummaryOmitTS.UseVisualStyleBackColor = true;
            // 
            // CheckBoxSummaryOmitSS
            // 
            this.CheckBoxSummaryOmitSS.AutoSize = true;
            this.CheckBoxSummaryOmitSS.Location = new System.Drawing.Point(19, 157);
            this.CheckBoxSummaryOmitSS.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxSummaryOmitSS.Name = "CheckBoxSummaryOmitSS";
            this.CheckBoxSummaryOmitSS.Size = new System.Drawing.Size(169, 21);
            this.CheckBoxSummaryOmitSS.TabIndex = 21;
            this.CheckBoxSummaryOmitSS.Text = "Omit secondary strata";
            this.CheckBoxSummaryOmitSS.UseVisualStyleBackColor = true;
            // 
            // CheckBoxSummarySCZeroValues
            // 
            this.CheckBoxSummarySCZeroValues.AutoSize = true;
            this.CheckBoxSummarySCZeroValues.Location = new System.Drawing.Point(603, 27);
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
            this.LabelSummaryTATimesteps.Location = new System.Drawing.Point(340, 135);
            this.LabelSummaryTATimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelSummaryTATimesteps.Name = "LabelSummaryTATimesteps";
            this.LabelSummaryTATimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelSummaryTATimesteps.TabIndex = 19;
            this.LabelSummaryTATimesteps.Text = "timesteps";
            // 
            // TextBoxSummaryTATimesteps
            // 
            this.TextBoxSummaryTATimesteps.Location = new System.Drawing.Point(266, 132);
            this.TextBoxSummaryTATimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxSummaryTATimesteps.Name = "TextBoxSummaryTATimesteps";
            this.TextBoxSummaryTATimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxSummaryTATimesteps.TabIndex = 18;
            this.TextBoxSummaryTATimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxSummaryTA
            // 
            this.CheckBoxSummaryTA.AutoSize = true;
            this.CheckBoxSummaryTA.Location = new System.Drawing.Point(19, 131);
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
            this.LabelSummarySATimesteps.Location = new System.Drawing.Point(340, 108);
            this.LabelSummarySATimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelSummarySATimesteps.Name = "LabelSummarySATimesteps";
            this.LabelSummarySATimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelSummarySATimesteps.TabIndex = 15;
            this.LabelSummarySATimesteps.Text = "timesteps";
            // 
            // TextBoxSummarySATimesteps
            // 
            this.TextBoxSummarySATimesteps.Location = new System.Drawing.Point(266, 104);
            this.TextBoxSummarySATimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxSummarySATimesteps.Name = "TextBoxSummarySATimesteps";
            this.TextBoxSummarySATimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxSummarySATimesteps.TabIndex = 14;
            this.TextBoxSummarySATimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxSummaryTRCalcIntervalMean
            // 
            this.CheckBoxSummaryTRCalcIntervalMean.AutoSize = true;
            this.CheckBoxSummaryTRCalcIntervalMean.Location = new System.Drawing.Point(603, 53);
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
            this.CheckBoxSummarySA.Location = new System.Drawing.Point(19, 105);
            this.CheckBoxSummarySA.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxSummarySA.Name = "CheckBoxSummarySA";
            this.CheckBoxSummarySA.Size = new System.Drawing.Size(165, 21);
            this.CheckBoxSummarySA.TabIndex = 13;
            this.CheckBoxSummarySA.Text = "State attributes every";
            this.CheckBoxSummarySA.UseVisualStyleBackColor = true;
            // 
            // TextBoxSummarySCTimesteps
            // 
            this.TextBoxSummarySCTimesteps.Location = new System.Drawing.Point(266, 23);
            this.TextBoxSummarySCTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxSummarySCTimesteps.Name = "TextBoxSummarySCTimesteps";
            this.TextBoxSummarySCTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxSummarySCTimesteps.TabIndex = 1;
            this.TextBoxSummarySCTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LabelSummarySCTimesteps
            // 
            this.LabelSummarySCTimesteps.AutoSize = true;
            this.LabelSummarySCTimesteps.Location = new System.Drawing.Point(340, 27);
            this.LabelSummarySCTimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelSummarySCTimesteps.Name = "LabelSummarySCTimesteps";
            this.LabelSummarySCTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelSummarySCTimesteps.TabIndex = 2;
            this.LabelSummarySCTimesteps.Text = "timesteps";
            // 
            // LabelSummaryTRSCTimesteps
            // 
            this.LabelSummaryTRSCTimesteps.AutoSize = true;
            this.LabelSummaryTRSCTimesteps.Location = new System.Drawing.Point(340, 81);
            this.LabelSummaryTRSCTimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelSummaryTRSCTimesteps.Name = "LabelSummaryTRSCTimesteps";
            this.LabelSummaryTRSCTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelSummaryTRSCTimesteps.TabIndex = 12;
            this.LabelSummaryTRSCTimesteps.Text = "timesteps";
            // 
            // CheckBoxSummarySC
            // 
            this.CheckBoxSummarySC.AutoSize = true;
            this.CheckBoxSummarySC.Location = new System.Drawing.Point(19, 27);
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
            this.CheckBoxSummaryTR.Location = new System.Drawing.Point(19, 53);
            this.CheckBoxSummaryTR.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxSummaryTR.Name = "CheckBoxSummaryTR";
            this.CheckBoxSummaryTR.Size = new System.Drawing.Size(139, 21);
            this.CheckBoxSummaryTR.TabIndex = 5;
            this.CheckBoxSummaryTR.Text = "Transitions every";
            this.CheckBoxSummaryTR.UseVisualStyleBackColor = true;
            // 
            // TextBoxSummaryTRSCTimesteps
            // 
            this.TextBoxSummaryTRSCTimesteps.Location = new System.Drawing.Point(266, 77);
            this.TextBoxSummaryTRSCTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxSummaryTRSCTimesteps.Name = "TextBoxSummaryTRSCTimesteps";
            this.TextBoxSummaryTRSCTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxSummaryTRSCTimesteps.TabIndex = 11;
            this.TextBoxSummaryTRSCTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LabelSummaryTRTimesteps
            // 
            this.LabelSummaryTRTimesteps.AutoSize = true;
            this.LabelSummaryTRTimesteps.Location = new System.Drawing.Point(340, 55);
            this.LabelSummaryTRTimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelSummaryTRTimesteps.Name = "LabelSummaryTRTimesteps";
            this.LabelSummaryTRTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelSummaryTRTimesteps.TabIndex = 7;
            this.LabelSummaryTRTimesteps.Text = "timesteps";
            // 
            // CheckBoxSummaryTRSC
            // 
            this.CheckBoxSummaryTRSC.AutoSize = true;
            this.CheckBoxSummaryTRSC.Location = new System.Drawing.Point(19, 79);
            this.CheckBoxSummaryTRSC.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxSummaryTRSC.Name = "CheckBoxSummaryTRSC";
            this.CheckBoxSummaryTRSC.Size = new System.Drawing.Size(229, 21);
            this.CheckBoxSummaryTRSC.TabIndex = 10;
            this.CheckBoxSummaryTRSC.Text = "Transitions by state class every";
            this.CheckBoxSummaryTRSC.UseVisualStyleBackColor = true;
            // 
            // TextBoxSummaryTRTimesteps
            // 
            this.TextBoxSummaryTRTimesteps.Location = new System.Drawing.Point(266, 50);
            this.TextBoxSummaryTRTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxSummaryTRTimesteps.Name = "TextBoxSummaryTRTimesteps";
            this.TextBoxSummaryTRTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxSummaryTRTimesteps.TabIndex = 6;
            this.TextBoxSummaryTRTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxAvgRasterSTAcrossTimesteps
            // 
            this.CheckBoxAvgRasterSTAcrossTimesteps.AutoSize = true;
            this.CheckBoxAvgRasterSTAcrossTimesteps.Location = new System.Drawing.Point(450, 429);
            this.CheckBoxAvgRasterSTAcrossTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxAvgRasterSTAcrossTimesteps.Name = "CheckBoxAvgRasterSTAcrossTimesteps";
            this.CheckBoxAvgRasterSTAcrossTimesteps.Size = new System.Drawing.Size(268, 21);
            this.CheckBoxAvgRasterSTAcrossTimesteps.TabIndex = 11;
            this.CheckBoxAvgRasterSTAcrossTimesteps.Text = "Average across preceeding timesteps";
            this.CheckBoxAvgRasterSTAcrossTimesteps.UseVisualStyleBackColor = true;
            // 
            // CheckBoxAvgRasterST
            // 
            this.CheckBoxAvgRasterST.AutoSize = true;
            this.CheckBoxAvgRasterST.Location = new System.Drawing.Point(18, 430);
            this.CheckBoxAvgRasterST.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxAvgRasterST.Name = "CheckBoxAvgRasterST";
            this.CheckBoxAvgRasterST.Size = new System.Drawing.Size(187, 21);
            this.CheckBoxAvgRasterST.TabIndex = 8;
            this.CheckBoxAvgRasterST.Text = "Stratum probability every";
            this.CheckBoxAvgRasterST.UseVisualStyleBackColor = true;
            // 
            // LabelAvgRasterSTTimesteps
            // 
            this.LabelAvgRasterSTTimesteps.AutoSize = true;
            this.LabelAvgRasterSTTimesteps.Location = new System.Drawing.Point(342, 429);
            this.LabelAvgRasterSTTimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelAvgRasterSTTimesteps.Name = "LabelAvgRasterSTTimesteps";
            this.LabelAvgRasterSTTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelAvgRasterSTTimesteps.TabIndex = 10;
            this.LabelAvgRasterSTTimesteps.Text = "timesteps";
            // 
            // TextBoxAvgRasterSTTimesteps
            // 
            this.TextBoxAvgRasterSTTimesteps.Location = new System.Drawing.Point(265, 426);
            this.TextBoxAvgRasterSTTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxAvgRasterSTTimesteps.Name = "TextBoxAvgRasterSTTimesteps";
            this.TextBoxAvgRasterSTTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxAvgRasterSTTimesteps.TabIndex = 9;
            this.TextBoxAvgRasterSTTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxAvgRasterAgeAcrossTimesteps
            // 
            this.CheckBoxAvgRasterAgeAcrossTimesteps.AutoSize = true;
            this.CheckBoxAvgRasterAgeAcrossTimesteps.Location = new System.Drawing.Point(450, 402);
            this.CheckBoxAvgRasterAgeAcrossTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxAvgRasterAgeAcrossTimesteps.Name = "CheckBoxAvgRasterAgeAcrossTimesteps";
            this.CheckBoxAvgRasterAgeAcrossTimesteps.Size = new System.Drawing.Size(268, 21);
            this.CheckBoxAvgRasterAgeAcrossTimesteps.TabIndex = 7;
            this.CheckBoxAvgRasterAgeAcrossTimesteps.Text = "Average across preceeding timesteps";
            this.CheckBoxAvgRasterAgeAcrossTimesteps.UseVisualStyleBackColor = true;
            // 
            // CheckBoxAvgRasterAge
            // 
            this.CheckBoxAvgRasterAge.AutoSize = true;
            this.CheckBoxAvgRasterAge.Location = new System.Drawing.Point(18, 403);
            this.CheckBoxAvgRasterAge.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxAvgRasterAge.Name = "CheckBoxAvgRasterAge";
            this.CheckBoxAvgRasterAge.Size = new System.Drawing.Size(101, 21);
            this.CheckBoxAvgRasterAge.TabIndex = 4;
            this.CheckBoxAvgRasterAge.Text = "Ages every";
            this.CheckBoxAvgRasterAge.UseVisualStyleBackColor = true;
            // 
            // LabelAvgRasterAgeTimesteps
            // 
            this.LabelAvgRasterAgeTimesteps.AutoSize = true;
            this.LabelAvgRasterAgeTimesteps.Location = new System.Drawing.Point(342, 402);
            this.LabelAvgRasterAgeTimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelAvgRasterAgeTimesteps.Name = "LabelAvgRasterAgeTimesteps";
            this.LabelAvgRasterAgeTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelAvgRasterAgeTimesteps.TabIndex = 6;
            this.LabelAvgRasterAgeTimesteps.Text = "timesteps";
            // 
            // TextBoxAvgRasterAgeTimesteps
            // 
            this.TextBoxAvgRasterAgeTimesteps.Location = new System.Drawing.Point(265, 399);
            this.TextBoxAvgRasterAgeTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxAvgRasterAgeTimesteps.Name = "TextBoxAvgRasterAgeTimesteps";
            this.TextBoxAvgRasterAgeTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxAvgRasterAgeTimesteps.TabIndex = 5;
            this.TextBoxAvgRasterAgeTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxAvgRasterTPAcrossTimesteps
            // 
            this.CheckBoxAvgRasterTPAcrossTimesteps.AutoSize = true;
            this.CheckBoxAvgRasterTPAcrossTimesteps.Location = new System.Drawing.Point(450, 510);
            this.CheckBoxAvgRasterTPAcrossTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxAvgRasterTPAcrossTimesteps.Name = "CheckBoxAvgRasterTPAcrossTimesteps";
            this.CheckBoxAvgRasterTPAcrossTimesteps.Size = new System.Drawing.Size(268, 21);
            this.CheckBoxAvgRasterTPAcrossTimesteps.TabIndex = 20;
            this.CheckBoxAvgRasterTPAcrossTimesteps.Text = "Average across preceeding timesteps";
            this.CheckBoxAvgRasterTPAcrossTimesteps.UseVisualStyleBackColor = true;
            // 
            // CheckBoxAvgRasterTAAcrossTimesteps
            // 
            this.CheckBoxAvgRasterTAAcrossTimesteps.AutoSize = true;
            this.CheckBoxAvgRasterTAAcrossTimesteps.Location = new System.Drawing.Point(450, 483);
            this.CheckBoxAvgRasterTAAcrossTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxAvgRasterTAAcrossTimesteps.Name = "CheckBoxAvgRasterTAAcrossTimesteps";
            this.CheckBoxAvgRasterTAAcrossTimesteps.Size = new System.Drawing.Size(268, 21);
            this.CheckBoxAvgRasterTAAcrossTimesteps.TabIndex = 19;
            this.CheckBoxAvgRasterTAAcrossTimesteps.Text = "Average across preceeding timesteps";
            this.CheckBoxAvgRasterTAAcrossTimesteps.UseVisualStyleBackColor = true;
            // 
            // CheckBoxAvgRasterSAAcrossTimesteps
            // 
            this.CheckBoxAvgRasterSAAcrossTimesteps.AutoSize = true;
            this.CheckBoxAvgRasterSAAcrossTimesteps.Location = new System.Drawing.Point(450, 456);
            this.CheckBoxAvgRasterSAAcrossTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxAvgRasterSAAcrossTimesteps.Name = "CheckBoxAvgRasterSAAcrossTimesteps";
            this.CheckBoxAvgRasterSAAcrossTimesteps.Size = new System.Drawing.Size(268, 21);
            this.CheckBoxAvgRasterSAAcrossTimesteps.TabIndex = 15;
            this.CheckBoxAvgRasterSAAcrossTimesteps.Text = "Average across preceeding timesteps";
            this.CheckBoxAvgRasterSAAcrossTimesteps.UseVisualStyleBackColor = true;
            // 
            // CheckBoxAvgRasterSCAcrossTimesteps
            // 
            this.CheckBoxAvgRasterSCAcrossTimesteps.AutoSize = true;
            this.CheckBoxAvgRasterSCAcrossTimesteps.Location = new System.Drawing.Point(450, 375);
            this.CheckBoxAvgRasterSCAcrossTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxAvgRasterSCAcrossTimesteps.Name = "CheckBoxAvgRasterSCAcrossTimesteps";
            this.CheckBoxAvgRasterSCAcrossTimesteps.Size = new System.Drawing.Size(268, 21);
            this.CheckBoxAvgRasterSCAcrossTimesteps.TabIndex = 3;
            this.CheckBoxAvgRasterSCAcrossTimesteps.Text = "Average across preceeding timesteps";
            this.CheckBoxAvgRasterSCAcrossTimesteps.UseVisualStyleBackColor = true;
            // 
            // CheckBoxAvgRasterTA
            // 
            this.CheckBoxAvgRasterTA.AutoSize = true;
            this.CheckBoxAvgRasterTA.Location = new System.Drawing.Point(18, 483);
            this.CheckBoxAvgRasterTA.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxAvgRasterTA.Name = "CheckBoxAvgRasterTA";
            this.CheckBoxAvgRasterTA.Size = new System.Drawing.Size(195, 21);
            this.CheckBoxAvgRasterTA.TabIndex = 16;
            this.CheckBoxAvgRasterTA.Text = "Transition attributes every";
            this.CheckBoxAvgRasterTA.UseVisualStyleBackColor = true;
            // 
            // CheckBoxAvgRasterSC
            // 
            this.CheckBoxAvgRasterSC.AutoSize = true;
            this.CheckBoxAvgRasterSC.Location = new System.Drawing.Point(18, 375);
            this.CheckBoxAvgRasterSC.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxAvgRasterSC.Name = "CheckBoxAvgRasterSC";
            this.CheckBoxAvgRasterSC.Size = new System.Drawing.Size(207, 21);
            this.CheckBoxAvgRasterSC.TabIndex = 0;
            this.CheckBoxAvgRasterSC.Text = "State class probability every";
            this.CheckBoxAvgRasterSC.UseVisualStyleBackColor = true;
            // 
            // LabelAvgRasterTATimesteps
            // 
            this.LabelAvgRasterTATimesteps.AutoSize = true;
            this.LabelAvgRasterTATimesteps.Location = new System.Drawing.Point(342, 484);
            this.LabelAvgRasterTATimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelAvgRasterTATimesteps.Name = "LabelAvgRasterTATimesteps";
            this.LabelAvgRasterTATimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelAvgRasterTATimesteps.TabIndex = 18;
            this.LabelAvgRasterTATimesteps.Text = "timesteps";
            // 
            // LabelAvgRasterSCTimesteps
            // 
            this.LabelAvgRasterSCTimesteps.AutoSize = true;
            this.LabelAvgRasterSCTimesteps.Location = new System.Drawing.Point(342, 375);
            this.LabelAvgRasterSCTimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelAvgRasterSCTimesteps.Name = "LabelAvgRasterSCTimesteps";
            this.LabelAvgRasterSCTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelAvgRasterSCTimesteps.TabIndex = 2;
            this.LabelAvgRasterSCTimesteps.Text = "timesteps";
            // 
            // TextBoxAvgRasterTATimesteps
            // 
            this.TextBoxAvgRasterTATimesteps.Location = new System.Drawing.Point(265, 480);
            this.TextBoxAvgRasterTATimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxAvgRasterTATimesteps.Name = "TextBoxAvgRasterTATimesteps";
            this.TextBoxAvgRasterTATimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxAvgRasterTATimesteps.TabIndex = 17;
            this.TextBoxAvgRasterTATimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TextBoxAvgRasterSCTimesteps
            // 
            this.TextBoxAvgRasterSCTimesteps.Location = new System.Drawing.Point(265, 371);
            this.TextBoxAvgRasterSCTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxAvgRasterSCTimesteps.Name = "TextBoxAvgRasterSCTimesteps";
            this.TextBoxAvgRasterSCTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxAvgRasterSCTimesteps.TabIndex = 1;
            this.TextBoxAvgRasterSCTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxAvgRasterSA
            // 
            this.CheckBoxAvgRasterSA.AutoSize = true;
            this.CheckBoxAvgRasterSA.Location = new System.Drawing.Point(18, 456);
            this.CheckBoxAvgRasterSA.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxAvgRasterSA.Name = "CheckBoxAvgRasterSA";
            this.CheckBoxAvgRasterSA.Size = new System.Drawing.Size(165, 21);
            this.CheckBoxAvgRasterSA.TabIndex = 12;
            this.CheckBoxAvgRasterSA.Text = "State attributes every";
            this.CheckBoxAvgRasterSA.UseVisualStyleBackColor = true;
            // 
            // LabelAvgRasterSATimesteps
            // 
            this.LabelAvgRasterSATimesteps.AutoSize = true;
            this.LabelAvgRasterSATimesteps.Location = new System.Drawing.Point(342, 457);
            this.LabelAvgRasterSATimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelAvgRasterSATimesteps.Name = "LabelAvgRasterSATimesteps";
            this.LabelAvgRasterSATimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelAvgRasterSATimesteps.TabIndex = 14;
            this.LabelAvgRasterSATimesteps.Text = "timesteps";
            // 
            // TextBoxAvgRasterSATimesteps
            // 
            this.TextBoxAvgRasterSATimesteps.Location = new System.Drawing.Point(265, 453);
            this.TextBoxAvgRasterSATimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxAvgRasterSATimesteps.Name = "TextBoxAvgRasterSATimesteps";
            this.TextBoxAvgRasterSATimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxAvgRasterSATimesteps.TabIndex = 13;
            this.TextBoxAvgRasterSATimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.LabelAvgSpatialTitle);
            this.panel1.Controls.Add(this.LabelSpatialTitle);
            this.panel1.Controls.Add(this.LabelTabularTitle);
            this.panel1.Controls.Add(this.CheckBoxAvgRasterSTAcrossTimesteps);
            this.panel1.Controls.Add(this.CheckBoxRasterTE);
            this.panel1.Controls.Add(this.CheckBoxAvgRasterST);
            this.panel1.Controls.Add(this.CheckBoxSummaryTAAges);
            this.panel1.Controls.Add(this.LabelAvgRasterSTTimesteps);
            this.panel1.Controls.Add(this.LabelRasterTETimesteps);
            this.panel1.Controls.Add(this.TextBoxAvgRasterSTTimesteps);
            this.panel1.Controls.Add(this.CheckBoxSummarySC);
            this.panel1.Controls.Add(this.CheckBoxAvgRasterAgeAcrossTimesteps);
            this.panel1.Controls.Add(this.TextBoxRasterTETimesteps);
            this.panel1.Controls.Add(this.CheckBoxAvgRasterAge);
            this.panel1.Controls.Add(this.CheckBoxSummarySAAges);
            this.panel1.Controls.Add(this.LabelAvgRasterAgeTimesteps);
            this.panel1.Controls.Add(this.CheckBoxRasterTA);
            this.panel1.Controls.Add(this.TextBoxAvgRasterAgeTimesteps);
            this.panel1.Controls.Add(this.TextBoxSummaryTRTimesteps);
            this.panel1.Controls.Add(this.CheckBoxAvgRasterTPAcrossTimesteps);
            this.panel1.Controls.Add(this.LabelRasterTATimesteps);
            this.panel1.Controls.Add(this.CheckBoxAvgRasterTAAcrossTimesteps);
            this.panel1.Controls.Add(this.CheckBoxSummaryTRAges);
            this.panel1.Controls.Add(this.CheckBoxAvgRasterSAAcrossTimesteps);
            this.panel1.Controls.Add(this.TextBoxRasterTATimesteps);
            this.panel1.Controls.Add(this.CheckBoxAvgRasterSCAcrossTimesteps);
            this.panel1.Controls.Add(this.CheckBoxSummaryTRSC);
            this.panel1.Controls.Add(this.CheckBoxAvgRasterTP);
            this.panel1.Controls.Add(this.CheckBoxRasterSA);
            this.panel1.Controls.Add(this.TextBoxAvgRasterTPTimesteps);
            this.panel1.Controls.Add(this.CheckBoxSummarySCAges);
            this.panel1.Controls.Add(this.LabelAvgRasterTPTimesteps);
            this.panel1.Controls.Add(this.LabelRasterSATimesteps);
            this.panel1.Controls.Add(this.CheckBoxAvgRasterTA);
            this.panel1.Controls.Add(this.LabelSummaryTRTimesteps);
            this.panel1.Controls.Add(this.CheckBoxAvgRasterSC);
            this.panel1.Controls.Add(this.LabelAvgRasterTATimesteps);
            this.panel1.Controls.Add(this.TextBoxRasterSATimesteps);
            this.panel1.Controls.Add(this.LabelAvgRasterSCTimesteps);
            this.panel1.Controls.Add(this.CheckBoxSummaryOmitTS);
            this.panel1.Controls.Add(this.TextBoxAvgRasterTATimesteps);
            this.panel1.Controls.Add(this.LabelRasterTSTTimesteps);
            this.panel1.Controls.Add(this.TextBoxAvgRasterSCTimesteps);
            this.panel1.Controls.Add(this.TextBoxSummaryTRSCTimesteps);
            this.panel1.Controls.Add(this.CheckBoxAvgRasterSA);
            this.panel1.Controls.Add(this.TextBoxRasterTSTTimesteps);
            this.panel1.Controls.Add(this.LabelAvgRasterSATimesteps);
            this.panel1.Controls.Add(this.CheckBoxSummaryOmitSS);
            this.panel1.Controls.Add(this.TextBoxAvgRasterSATimesteps);
            this.panel1.Controls.Add(this.CheckBoxRasterTST);
            this.panel1.Controls.Add(this.CheckBoxSummaryTR);
            this.panel1.Controls.Add(this.TextBoxRasterAgeTimesteps);
            this.panel1.Controls.Add(this.CheckBoxSummarySCZeroValues);
            this.panel1.Controls.Add(this.CheckBoxRasterAge);
            this.panel1.Controls.Add(this.LabelSummaryTRSCTimesteps);
            this.panel1.Controls.Add(this.LabelRasterAgeTimesteps);
            this.panel1.Controls.Add(this.LabelSummaryTATimesteps);
            this.panel1.Controls.Add(this.TextBoxRasterTRTimesteps);
            this.panel1.Controls.Add(this.LabelSummarySCTimesteps);
            this.panel1.Controls.Add(this.CheckBoxRasterST);
            this.panel1.Controls.Add(this.TextBoxSummaryTATimesteps);
            this.panel1.Controls.Add(this.LabelRasterSTTimesteps);
            this.panel1.Controls.Add(this.TextBoxSummarySCTimesteps);
            this.panel1.Controls.Add(this.CheckBoxRasterSC);
            this.panel1.Controls.Add(this.CheckBoxRasterTR);
            this.panel1.Controls.Add(this.CheckBoxSummaryTA);
            this.panel1.Controls.Add(this.TextBoxRasterSCTimesteps);
            this.panel1.Controls.Add(this.CheckBoxSummarySA);
            this.panel1.Controls.Add(this.TextBoxRasterSTTimesteps);
            this.panel1.Controls.Add(this.LabelSummarySATimesteps);
            this.panel1.Controls.Add(this.LabelRasterTRTimesteps);
            this.panel1.Controls.Add(this.CheckBoxSummaryTRCalcIntervalMean);
            this.panel1.Controls.Add(this.LabelRasterSCTimesteps);
            this.panel1.Controls.Add(this.TextBoxSummarySATimesteps);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1826, 903);
            this.panel1.TabIndex = 2;
            // 
            // LabelAvgSpatialTitle
            // 
            this.LabelAvgSpatialTitle.AutoSize = true;
            this.LabelAvgSpatialTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelAvgSpatialTitle.Location = new System.Drawing.Point(3, 350);
            this.LabelAvgSpatialTitle.Name = "LabelAvgSpatialTitle";
            this.LabelAvgSpatialTitle.Size = new System.Drawing.Size(172, 17);
            this.LabelAvgSpatialTitle.TabIndex = 29;
            this.LabelAvgSpatialTitle.Text = "Average spatial output";
            // 
            // LabelSpatialTitle
            // 
            this.LabelSpatialTitle.AutoSize = true;
            this.LabelSpatialTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelSpatialTitle.Location = new System.Drawing.Point(5, 215);
            this.LabelSpatialTitle.Name = "LabelSpatialTitle";
            this.LabelSpatialTitle.Size = new System.Drawing.Size(109, 17);
            this.LabelSpatialTitle.TabIndex = 28;
            this.LabelSpatialTitle.Text = "Spatial output";
            // 
            // LabelTabularTitle
            // 
            this.LabelTabularTitle.AutoSize = true;
            this.LabelTabularTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTabularTitle.Location = new System.Drawing.Point(4, 4);
            this.LabelTabularTitle.Name = "LabelTabularTitle";
            this.LabelTabularTitle.Size = new System.Drawing.Size(115, 17);
            this.LabelTabularTitle.TabIndex = 27;
            this.LabelTabularTitle.Text = "Tabular output";
            // 
            // OutputOptionsDataFeedView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "OutputOptionsDataFeedView";
            this.Size = new System.Drawing.Size(1826, 903);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }
        internal System.Windows.Forms.CheckBox CheckBoxAvgRasterTP;
        internal System.Windows.Forms.Label LabelAvgRasterTPTimesteps;
        internal System.Windows.Forms.TextBox TextBoxAvgRasterTPTimesteps;
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
        internal System.Windows.Forms.CheckBox CheckBoxRasterTE;
        internal System.Windows.Forms.Label LabelRasterTETimesteps;
        internal System.Windows.Forms.TextBox TextBoxRasterTETimesteps;
        internal System.Windows.Forms.CheckBox CheckBoxAvgRasterTA;
        internal System.Windows.Forms.CheckBox CheckBoxAvgRasterSC;
        internal System.Windows.Forms.Label LabelAvgRasterTATimesteps;
        internal System.Windows.Forms.Label LabelAvgRasterSCTimesteps;
        internal System.Windows.Forms.TextBox TextBoxAvgRasterTATimesteps;
        internal System.Windows.Forms.TextBox TextBoxAvgRasterSCTimesteps;
        internal System.Windows.Forms.CheckBox CheckBoxAvgRasterSA;
        internal System.Windows.Forms.Label LabelAvgRasterSATimesteps;
        internal System.Windows.Forms.TextBox TextBoxAvgRasterSATimesteps;
        internal System.Windows.Forms.CheckBox CheckBoxAvgRasterSCAcrossTimesteps;
        internal System.Windows.Forms.CheckBox CheckBoxAvgRasterTPAcrossTimesteps;
        internal System.Windows.Forms.CheckBox CheckBoxAvgRasterTAAcrossTimesteps;
        internal System.Windows.Forms.CheckBox CheckBoxAvgRasterSAAcrossTimesteps;
        internal System.Windows.Forms.CheckBox CheckBoxAvgRasterAgeAcrossTimesteps;
        internal System.Windows.Forms.CheckBox CheckBoxAvgRasterAge;
        internal System.Windows.Forms.Label LabelAvgRasterAgeTimesteps;
        internal System.Windows.Forms.TextBox TextBoxAvgRasterAgeTimesteps;
        internal System.Windows.Forms.CheckBox CheckBoxAvgRasterSTAcrossTimesteps;
        internal System.Windows.Forms.CheckBox CheckBoxAvgRasterST;
        internal System.Windows.Forms.Label LabelAvgRasterSTTimesteps;
        internal System.Windows.Forms.TextBox TextBoxAvgRasterSTTimesteps;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label LabelAvgSpatialTitle;
        private System.Windows.Forms.Label LabelSpatialTitle;
        private System.Windows.Forms.Label LabelTabularTitle;
    }
}
