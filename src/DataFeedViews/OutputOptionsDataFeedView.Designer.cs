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
            this.GroupBoxSpatialOutput = new System.Windows.Forms.GroupBox();
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
            this.GroupBoxAvgSpatialOutput = new System.Windows.Forms.GroupBox();
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
            this.GroupBoxSpatialOutput.SuspendLayout();
            this.GroupBoxSummaryOutput.SuspendLayout();
            this.GroupBoxAvgSpatialOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBoxSpatialOutput
            // 
            this.GroupBoxSpatialOutput.Controls.Add(this.CheckBoxRasterTE);
            this.GroupBoxSpatialOutput.Controls.Add(this.LabelRasterTETimesteps);
            this.GroupBoxSpatialOutput.Controls.Add(this.TextBoxRasterTETimesteps);
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
            this.GroupBoxSpatialOutput.Location = new System.Drawing.Point(4, 236);
            this.GroupBoxSpatialOutput.Margin = new System.Windows.Forms.Padding(4);
            this.GroupBoxSpatialOutput.Name = "GroupBoxSpatialOutput";
            this.GroupBoxSpatialOutput.Padding = new System.Windows.Forms.Padding(4);
            this.GroupBoxSpatialOutput.Size = new System.Drawing.Size(849, 146);
            this.GroupBoxSpatialOutput.TabIndex = 1;
            this.GroupBoxSpatialOutput.TabStop = false;
            this.GroupBoxSpatialOutput.Text = "Spatial output";
            // 
            // CheckBoxRasterTE
            // 
            this.CheckBoxRasterTE.AutoSize = true;
            this.CheckBoxRasterTE.Location = new System.Drawing.Point(447, 112);
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
            this.LabelRasterTETimesteps.Location = new System.Drawing.Point(733, 115);
            this.LabelRasterTETimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelRasterTETimesteps.Name = "LabelRasterTETimesteps";
            this.LabelRasterTETimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelRasterTETimesteps.TabIndex = 26;
            this.LabelRasterTETimesteps.Text = "timesteps";
            // 
            // TextBoxRasterTETimesteps
            // 
            this.TextBoxRasterTETimesteps.Location = new System.Drawing.Point(656, 111);
            this.TextBoxRasterTETimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxRasterTETimesteps.Name = "TextBoxRasterTETimesteps";
            this.TextBoxRasterTETimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxRasterTETimesteps.TabIndex = 25;
            this.TextBoxRasterTETimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxRasterTA
            // 
            this.CheckBoxRasterTA.AutoSize = true;
            this.CheckBoxRasterTA.Location = new System.Drawing.Point(447, 83);
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
            this.LabelRasterTATimesteps.Location = new System.Drawing.Point(733, 86);
            this.LabelRasterTATimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelRasterTATimesteps.Name = "LabelRasterTATimesteps";
            this.LabelRasterTATimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelRasterTATimesteps.TabIndex = 20;
            this.LabelRasterTATimesteps.Text = "timesteps";
            // 
            // TextBoxRasterTATimesteps
            // 
            this.TextBoxRasterTATimesteps.Location = new System.Drawing.Point(656, 82);
            this.TextBoxRasterTATimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxRasterTATimesteps.Name = "TextBoxRasterTATimesteps";
            this.TextBoxRasterTATimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxRasterTATimesteps.TabIndex = 19;
            this.TextBoxRasterTATimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxRasterSA
            // 
            this.CheckBoxRasterSA.AutoSize = true;
            this.CheckBoxRasterSA.Location = new System.Drawing.Point(447, 55);
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
            this.LabelRasterSATimesteps.Location = new System.Drawing.Point(733, 58);
            this.LabelRasterSATimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelRasterSATimesteps.Name = "LabelRasterSATimesteps";
            this.LabelRasterSATimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelRasterSATimesteps.TabIndex = 17;
            this.LabelRasterSATimesteps.Text = "timesteps";
            // 
            // TextBoxRasterSATimesteps
            // 
            this.TextBoxRasterSATimesteps.Location = new System.Drawing.Point(656, 54);
            this.TextBoxRasterSATimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxRasterSATimesteps.Name = "TextBoxRasterSATimesteps";
            this.TextBoxRasterSATimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxRasterSATimesteps.TabIndex = 16;
            this.TextBoxRasterSATimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LabelRasterTSTTimesteps
            // 
            this.LabelRasterTSTTimesteps.AutoSize = true;
            this.LabelRasterTSTTimesteps.Location = new System.Drawing.Point(339, 113);
            this.LabelRasterTSTTimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelRasterTSTTimesteps.Name = "LabelRasterTSTTimesteps";
            this.LabelRasterTSTTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelRasterTSTTimesteps.TabIndex = 11;
            this.LabelRasterTSTTimesteps.Text = "timesteps";
            // 
            // TextBoxRasterTSTTimesteps
            // 
            this.TextBoxRasterTSTTimesteps.Location = new System.Drawing.Point(262, 110);
            this.TextBoxRasterTSTTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxRasterTSTTimesteps.Name = "TextBoxRasterTSTTimesteps";
            this.TextBoxRasterTSTTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxRasterTSTTimesteps.TabIndex = 10;
            this.TextBoxRasterTSTTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxRasterTST
            // 
            this.CheckBoxRasterTST.AutoSize = true;
            this.CheckBoxRasterTST.Location = new System.Drawing.Point(15, 113);
            this.CheckBoxRasterTST.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxRasterTST.Name = "CheckBoxRasterTST";
            this.CheckBoxRasterTST.Size = new System.Drawing.Size(201, 21);
            this.CheckBoxRasterTST.TabIndex = 9;
            this.CheckBoxRasterTST.Text = "Time-since-transition every";
            this.CheckBoxRasterTST.UseVisualStyleBackColor = true;
            // 
            // TextBoxRasterAgeTimesteps
            // 
            this.TextBoxRasterAgeTimesteps.Location = new System.Drawing.Point(262, 81);
            this.TextBoxRasterAgeTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxRasterAgeTimesteps.Name = "TextBoxRasterAgeTimesteps";
            this.TextBoxRasterAgeTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxRasterAgeTimesteps.TabIndex = 7;
            this.TextBoxRasterAgeTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxRasterAge
            // 
            this.CheckBoxRasterAge.AutoSize = true;
            this.CheckBoxRasterAge.Location = new System.Drawing.Point(15, 85);
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
            this.LabelRasterAgeTimesteps.Location = new System.Drawing.Point(339, 85);
            this.LabelRasterAgeTimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelRasterAgeTimesteps.Name = "LabelRasterAgeTimesteps";
            this.LabelRasterAgeTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelRasterAgeTimesteps.TabIndex = 8;
            this.LabelRasterAgeTimesteps.Text = "timesteps";
            // 
            // TextBoxRasterTRTimesteps
            // 
            this.TextBoxRasterTRTimesteps.Location = new System.Drawing.Point(262, 53);
            this.TextBoxRasterTRTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxRasterTRTimesteps.Name = "TextBoxRasterTRTimesteps";
            this.TextBoxRasterTRTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxRasterTRTimesteps.TabIndex = 4;
            this.TextBoxRasterTRTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxRasterST
            // 
            this.CheckBoxRasterST.AutoSize = true;
            this.CheckBoxRasterST.Location = new System.Drawing.Point(447, 27);
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
            this.LabelRasterSTTimesteps.Location = new System.Drawing.Point(733, 30);
            this.LabelRasterSTTimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelRasterSTTimesteps.Name = "LabelRasterSTTimesteps";
            this.LabelRasterSTTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelRasterSTTimesteps.TabIndex = 14;
            this.LabelRasterSTTimesteps.Text = "timesteps";
            // 
            // CheckBoxRasterSC
            // 
            this.CheckBoxRasterSC.AutoSize = true;
            this.CheckBoxRasterSC.Location = new System.Drawing.Point(15, 28);
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
            this.CheckBoxRasterTR.Location = new System.Drawing.Point(15, 57);
            this.CheckBoxRasterTR.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxRasterTR.Name = "CheckBoxRasterTR";
            this.CheckBoxRasterTR.Size = new System.Drawing.Size(139, 21);
            this.CheckBoxRasterTR.TabIndex = 3;
            this.CheckBoxRasterTR.Text = "Transitions every";
            this.CheckBoxRasterTR.UseVisualStyleBackColor = true;
            // 
            // TextBoxRasterSCTimesteps
            // 
            this.TextBoxRasterSCTimesteps.Location = new System.Drawing.Point(262, 25);
            this.TextBoxRasterSCTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxRasterSCTimesteps.Name = "TextBoxRasterSCTimesteps";
            this.TextBoxRasterSCTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxRasterSCTimesteps.TabIndex = 1;
            this.TextBoxRasterSCTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TextBoxRasterSTTimesteps
            // 
            this.TextBoxRasterSTTimesteps.Location = new System.Drawing.Point(656, 26);
            this.TextBoxRasterSTTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxRasterSTTimesteps.Name = "TextBoxRasterSTTimesteps";
            this.TextBoxRasterSTTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxRasterSTTimesteps.TabIndex = 13;
            this.TextBoxRasterSTTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LabelRasterTRTimesteps
            // 
            this.LabelRasterTRTimesteps.AutoSize = true;
            this.LabelRasterTRTimesteps.Location = new System.Drawing.Point(339, 57);
            this.LabelRasterTRTimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelRasterTRTimesteps.Name = "LabelRasterTRTimesteps";
            this.LabelRasterTRTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelRasterTRTimesteps.TabIndex = 5;
            this.LabelRasterTRTimesteps.Text = "timesteps";
            // 
            // LabelRasterSCTimesteps
            // 
            this.LabelRasterSCTimesteps.AutoSize = true;
            this.LabelRasterSCTimesteps.Location = new System.Drawing.Point(339, 28);
            this.LabelRasterSCTimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelRasterSCTimesteps.Name = "LabelRasterSCTimesteps";
            this.LabelRasterSCTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelRasterSCTimesteps.TabIndex = 2;
            this.LabelRasterSCTimesteps.Text = "timesteps";
            // 
            // CheckBoxAvgRasterTP
            // 
            this.CheckBoxAvgRasterTP.AutoSize = true;
            this.CheckBoxAvgRasterTP.Location = new System.Drawing.Point(15, 147);
            this.CheckBoxAvgRasterTP.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxAvgRasterTP.Name = "CheckBoxAvgRasterTP";
            this.CheckBoxAvgRasterTP.Size = new System.Drawing.Size(201, 21);
            this.CheckBoxAvgRasterTP.TabIndex = 17;
            this.CheckBoxAvgRasterTP.Text = "Transition probability every";
            this.CheckBoxAvgRasterTP.UseVisualStyleBackColor = true;
            // 
            // LabelAvgRasterTPTimesteps
            // 
            this.LabelAvgRasterTPTimesteps.AutoSize = true;
            this.LabelAvgRasterTPTimesteps.Location = new System.Drawing.Point(339, 147);
            this.LabelAvgRasterTPTimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelAvgRasterTPTimesteps.Name = "LabelAvgRasterTPTimesteps";
            this.LabelAvgRasterTPTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelAvgRasterTPTimesteps.TabIndex = 19;
            this.LabelAvgRasterTPTimesteps.Text = "timesteps";
            // 
            // TextBoxAvgRasterTPTimesteps
            // 
            this.TextBoxAvgRasterTPTimesteps.Location = new System.Drawing.Point(262, 144);
            this.TextBoxAvgRasterTPTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxAvgRasterTPTimesteps.Name = "TextBoxAvgRasterTPTimesteps";
            this.TextBoxAvgRasterTPTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxAvgRasterTPTimesteps.TabIndex = 18;
            this.TextBoxAvgRasterTPTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.GroupBoxSummaryOutput.Location = new System.Drawing.Point(4, 5);
            this.GroupBoxSummaryOutput.Margin = new System.Windows.Forms.Padding(4);
            this.GroupBoxSummaryOutput.Name = "GroupBoxSummaryOutput";
            this.GroupBoxSummaryOutput.Padding = new System.Windows.Forms.Padding(4);
            this.GroupBoxSummaryOutput.Size = new System.Drawing.Size(849, 223);
            this.GroupBoxSummaryOutput.TabIndex = 0;
            this.GroupBoxSummaryOutput.TabStop = false;
            this.GroupBoxSummaryOutput.Text = "Tabular output";
            // 
            // CheckBoxSummaryTAAges
            // 
            this.CheckBoxSummaryTAAges.AutoSize = true;
            this.CheckBoxSummaryTAAges.Location = new System.Drawing.Point(447, 139);
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
            this.CheckBoxSummarySAAges.Location = new System.Drawing.Point(447, 112);
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
            this.CheckBoxSummaryTRAges.Location = new System.Drawing.Point(447, 54);
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
            this.CheckBoxSummarySCAges.Location = new System.Drawing.Point(447, 27);
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
            this.CheckBoxSummaryOmitTS.Location = new System.Drawing.Point(15, 190);
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
            this.CheckBoxSummaryOmitSS.Location = new System.Drawing.Point(15, 162);
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
            this.CheckBoxSummarySCZeroValues.Location = new System.Drawing.Point(599, 27);
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
            this.LabelSummaryTATimesteps.Location = new System.Drawing.Point(336, 140);
            this.LabelSummaryTATimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelSummaryTATimesteps.Name = "LabelSummaryTATimesteps";
            this.LabelSummaryTATimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelSummaryTATimesteps.TabIndex = 19;
            this.LabelSummaryTATimesteps.Text = "timesteps";
            // 
            // TextBoxSummaryTATimesteps
            // 
            this.TextBoxSummaryTATimesteps.Location = new System.Drawing.Point(262, 137);
            this.TextBoxSummaryTATimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxSummaryTATimesteps.Name = "TextBoxSummaryTATimesteps";
            this.TextBoxSummaryTATimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxSummaryTATimesteps.TabIndex = 18;
            this.TextBoxSummaryTATimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxSummaryTA
            // 
            this.CheckBoxSummaryTA.AutoSize = true;
            this.CheckBoxSummaryTA.Location = new System.Drawing.Point(15, 135);
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
            this.LabelSummarySATimesteps.Location = new System.Drawing.Point(336, 112);
            this.LabelSummarySATimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelSummarySATimesteps.Name = "LabelSummarySATimesteps";
            this.LabelSummarySATimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelSummarySATimesteps.TabIndex = 15;
            this.LabelSummarySATimesteps.Text = "timesteps";
            // 
            // TextBoxSummarySATimesteps
            // 
            this.TextBoxSummarySATimesteps.Location = new System.Drawing.Point(262, 108);
            this.TextBoxSummarySATimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxSummarySATimesteps.Name = "TextBoxSummarySATimesteps";
            this.TextBoxSummarySATimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxSummarySATimesteps.TabIndex = 14;
            this.TextBoxSummarySATimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxSummaryTRCalcIntervalMean
            // 
            this.CheckBoxSummaryTRCalcIntervalMean.AutoSize = true;
            this.CheckBoxSummaryTRCalcIntervalMean.Location = new System.Drawing.Point(599, 54);
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
            this.CheckBoxSummarySA.Location = new System.Drawing.Point(15, 108);
            this.CheckBoxSummarySA.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxSummarySA.Name = "CheckBoxSummarySA";
            this.CheckBoxSummarySA.Size = new System.Drawing.Size(165, 21);
            this.CheckBoxSummarySA.TabIndex = 13;
            this.CheckBoxSummarySA.Text = "State attributes every";
            this.CheckBoxSummarySA.UseVisualStyleBackColor = true;
            // 
            // TextBoxSummarySCTimesteps
            // 
            this.TextBoxSummarySCTimesteps.Location = new System.Drawing.Point(262, 23);
            this.TextBoxSummarySCTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxSummarySCTimesteps.Name = "TextBoxSummarySCTimesteps";
            this.TextBoxSummarySCTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxSummarySCTimesteps.TabIndex = 1;
            this.TextBoxSummarySCTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LabelSummarySCTimesteps
            // 
            this.LabelSummarySCTimesteps.AutoSize = true;
            this.LabelSummarySCTimesteps.Location = new System.Drawing.Point(336, 27);
            this.LabelSummarySCTimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelSummarySCTimesteps.Name = "LabelSummarySCTimesteps";
            this.LabelSummarySCTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelSummarySCTimesteps.TabIndex = 2;
            this.LabelSummarySCTimesteps.Text = "timesteps";
            // 
            // LabelSummaryTRSCTimesteps
            // 
            this.LabelSummaryTRSCTimesteps.AutoSize = true;
            this.LabelSummaryTRSCTimesteps.Location = new System.Drawing.Point(336, 84);
            this.LabelSummaryTRSCTimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelSummaryTRSCTimesteps.Name = "LabelSummaryTRSCTimesteps";
            this.LabelSummaryTRSCTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelSummaryTRSCTimesteps.TabIndex = 12;
            this.LabelSummaryTRSCTimesteps.Text = "timesteps";
            // 
            // CheckBoxSummarySC
            // 
            this.CheckBoxSummarySC.AutoSize = true;
            this.CheckBoxSummarySC.Location = new System.Drawing.Point(15, 27);
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
            this.CheckBoxSummaryTR.Location = new System.Drawing.Point(15, 54);
            this.CheckBoxSummaryTR.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxSummaryTR.Name = "CheckBoxSummaryTR";
            this.CheckBoxSummaryTR.Size = new System.Drawing.Size(139, 21);
            this.CheckBoxSummaryTR.TabIndex = 5;
            this.CheckBoxSummaryTR.Text = "Transitions every";
            this.CheckBoxSummaryTR.UseVisualStyleBackColor = true;
            // 
            // TextBoxSummaryTRSCTimesteps
            // 
            this.TextBoxSummaryTRSCTimesteps.Location = new System.Drawing.Point(262, 80);
            this.TextBoxSummaryTRSCTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxSummaryTRSCTimesteps.Name = "TextBoxSummaryTRSCTimesteps";
            this.TextBoxSummaryTRSCTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxSummaryTRSCTimesteps.TabIndex = 11;
            this.TextBoxSummaryTRSCTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LabelSummaryTRTimesteps
            // 
            this.LabelSummaryTRTimesteps.AutoSize = true;
            this.LabelSummaryTRTimesteps.Location = new System.Drawing.Point(336, 55);
            this.LabelSummaryTRTimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelSummaryTRTimesteps.Name = "LabelSummaryTRTimesteps";
            this.LabelSummaryTRTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelSummaryTRTimesteps.TabIndex = 7;
            this.LabelSummaryTRTimesteps.Text = "timesteps";
            // 
            // CheckBoxSummaryTRSC
            // 
            this.CheckBoxSummaryTRSC.AutoSize = true;
            this.CheckBoxSummaryTRSC.Location = new System.Drawing.Point(15, 81);
            this.CheckBoxSummaryTRSC.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxSummaryTRSC.Name = "CheckBoxSummaryTRSC";
            this.CheckBoxSummaryTRSC.Size = new System.Drawing.Size(229, 21);
            this.CheckBoxSummaryTRSC.TabIndex = 10;
            this.CheckBoxSummaryTRSC.Text = "Transitions by state class every";
            this.CheckBoxSummaryTRSC.UseVisualStyleBackColor = true;
            // 
            // TextBoxSummaryTRTimesteps
            // 
            this.TextBoxSummaryTRTimesteps.Location = new System.Drawing.Point(262, 52);
            this.TextBoxSummaryTRTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxSummaryTRTimesteps.Name = "TextBoxSummaryTRTimesteps";
            this.TextBoxSummaryTRTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxSummaryTRTimesteps.TabIndex = 6;
            this.TextBoxSummaryTRTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // GroupBoxAvgSpatialOutput
            // 
            this.GroupBoxAvgSpatialOutput.Controls.Add(this.CheckBoxAvgRasterAgeAcrossTimesteps);
            this.GroupBoxAvgSpatialOutput.Controls.Add(this.CheckBoxAvgRasterAge);
            this.GroupBoxAvgSpatialOutput.Controls.Add(this.LabelAvgRasterAgeTimesteps);
            this.GroupBoxAvgSpatialOutput.Controls.Add(this.TextBoxAvgRasterAgeTimesteps);
            this.GroupBoxAvgSpatialOutput.Controls.Add(this.CheckBoxAvgRasterTPAcrossTimesteps);
            this.GroupBoxAvgSpatialOutput.Controls.Add(this.CheckBoxAvgRasterTAAcrossTimesteps);
            this.GroupBoxAvgSpatialOutput.Controls.Add(this.CheckBoxAvgRasterSAAcrossTimesteps);
            this.GroupBoxAvgSpatialOutput.Controls.Add(this.CheckBoxAvgRasterSCAcrossTimesteps);
            this.GroupBoxAvgSpatialOutput.Controls.Add(this.CheckBoxAvgRasterTP);
            this.GroupBoxAvgSpatialOutput.Controls.Add(this.TextBoxAvgRasterTPTimesteps);
            this.GroupBoxAvgSpatialOutput.Controls.Add(this.LabelAvgRasterTPTimesteps);
            this.GroupBoxAvgSpatialOutput.Controls.Add(this.CheckBoxAvgRasterTA);
            this.GroupBoxAvgSpatialOutput.Controls.Add(this.CheckBoxAvgRasterSC);
            this.GroupBoxAvgSpatialOutput.Controls.Add(this.LabelAvgRasterTATimesteps);
            this.GroupBoxAvgSpatialOutput.Controls.Add(this.LabelAvgRasterSCTimesteps);
            this.GroupBoxAvgSpatialOutput.Controls.Add(this.TextBoxAvgRasterTATimesteps);
            this.GroupBoxAvgSpatialOutput.Controls.Add(this.TextBoxAvgRasterSCTimesteps);
            this.GroupBoxAvgSpatialOutput.Controls.Add(this.CheckBoxAvgRasterSA);
            this.GroupBoxAvgSpatialOutput.Controls.Add(this.LabelAvgRasterSATimesteps);
            this.GroupBoxAvgSpatialOutput.Controls.Add(this.TextBoxAvgRasterSATimesteps);
            this.GroupBoxAvgSpatialOutput.Location = new System.Drawing.Point(4, 392);
            this.GroupBoxAvgSpatialOutput.Margin = new System.Windows.Forms.Padding(4);
            this.GroupBoxAvgSpatialOutput.Name = "GroupBoxAvgSpatialOutput";
            this.GroupBoxAvgSpatialOutput.Size = new System.Drawing.Size(849, 178);
            this.GroupBoxAvgSpatialOutput.TabIndex = 2;
            this.GroupBoxAvgSpatialOutput.TabStop = false;
            this.GroupBoxAvgSpatialOutput.Text = "Average spatial output";
            // 
            // CheckBoxAvgRasterAgeAcrossTimesteps
            // 
            this.CheckBoxAvgRasterAgeAcrossTimesteps.AutoSize = true;
            this.CheckBoxAvgRasterAgeAcrossTimesteps.Location = new System.Drawing.Point(447, 60);
            this.CheckBoxAvgRasterAgeAcrossTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxAvgRasterAgeAcrossTimesteps.Name = "CheckBoxAvgRasterAgeAcrossTimesteps";
            this.CheckBoxAvgRasterAgeAcrossTimesteps.Size = new System.Drawing.Size(268, 21);
            this.CheckBoxAvgRasterAgeAcrossTimesteps.TabIndex = 8;
            this.CheckBoxAvgRasterAgeAcrossTimesteps.Text = "Average across preceeding timesteps";
            this.CheckBoxAvgRasterAgeAcrossTimesteps.UseVisualStyleBackColor = true;
            // 
            // CheckBoxAvgRasterAge
            // 
            this.CheckBoxAvgRasterAge.AutoSize = true;
            this.CheckBoxAvgRasterAge.Location = new System.Drawing.Point(15, 60);
            this.CheckBoxAvgRasterAge.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxAvgRasterAge.Name = "CheckBoxAvgRasterAge";
            this.CheckBoxAvgRasterAge.Size = new System.Drawing.Size(101, 21);
            this.CheckBoxAvgRasterAge.TabIndex = 5;
            this.CheckBoxAvgRasterAge.Text = "Ages every";
            this.CheckBoxAvgRasterAge.UseVisualStyleBackColor = true;
            // 
            // LabelAvgRasterAgeTimesteps
            // 
            this.LabelAvgRasterAgeTimesteps.AutoSize = true;
            this.LabelAvgRasterAgeTimesteps.Location = new System.Drawing.Point(339, 60);
            this.LabelAvgRasterAgeTimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelAvgRasterAgeTimesteps.Name = "LabelAvgRasterAgeTimesteps";
            this.LabelAvgRasterAgeTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelAvgRasterAgeTimesteps.TabIndex = 7;
            this.LabelAvgRasterAgeTimesteps.Text = "timesteps";
            // 
            // TextBoxAvgRasterAgeTimesteps
            // 
            this.TextBoxAvgRasterAgeTimesteps.Location = new System.Drawing.Point(262, 57);
            this.TextBoxAvgRasterAgeTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxAvgRasterAgeTimesteps.Name = "TextBoxAvgRasterAgeTimesteps";
            this.TextBoxAvgRasterAgeTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxAvgRasterAgeTimesteps.TabIndex = 6;
            this.TextBoxAvgRasterAgeTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxAvgRasterTPAcrossTimesteps
            // 
            this.CheckBoxAvgRasterTPAcrossTimesteps.AutoSize = true;
            this.CheckBoxAvgRasterTPAcrossTimesteps.Location = new System.Drawing.Point(447, 147);
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
            this.CheckBoxAvgRasterTAAcrossTimesteps.Location = new System.Drawing.Point(447, 118);
            this.CheckBoxAvgRasterTAAcrossTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxAvgRasterTAAcrossTimesteps.Name = "CheckBoxAvgRasterTAAcrossTimesteps";
            this.CheckBoxAvgRasterTAAcrossTimesteps.Size = new System.Drawing.Size(268, 21);
            this.CheckBoxAvgRasterTAAcrossTimesteps.TabIndex = 16;
            this.CheckBoxAvgRasterTAAcrossTimesteps.Text = "Average across preceeding timesteps";
            this.CheckBoxAvgRasterTAAcrossTimesteps.UseVisualStyleBackColor = true;
            // 
            // CheckBoxAvgRasterSAAcrossTimesteps
            // 
            this.CheckBoxAvgRasterSAAcrossTimesteps.AutoSize = true;
            this.CheckBoxAvgRasterSAAcrossTimesteps.Location = new System.Drawing.Point(447, 89);
            this.CheckBoxAvgRasterSAAcrossTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxAvgRasterSAAcrossTimesteps.Name = "CheckBoxAvgRasterSAAcrossTimesteps";
            this.CheckBoxAvgRasterSAAcrossTimesteps.Size = new System.Drawing.Size(268, 21);
            this.CheckBoxAvgRasterSAAcrossTimesteps.TabIndex = 12;
            this.CheckBoxAvgRasterSAAcrossTimesteps.Text = "Average across preceeding timesteps";
            this.CheckBoxAvgRasterSAAcrossTimesteps.UseVisualStyleBackColor = true;
            // 
            // CheckBoxAvgRasterSCAcrossTimesteps
            // 
            this.CheckBoxAvgRasterSCAcrossTimesteps.AutoSize = true;
            this.CheckBoxAvgRasterSCAcrossTimesteps.Location = new System.Drawing.Point(447, 31);
            this.CheckBoxAvgRasterSCAcrossTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxAvgRasterSCAcrossTimesteps.Name = "CheckBoxAvgRasterSCAcrossTimesteps";
            this.CheckBoxAvgRasterSCAcrossTimesteps.Size = new System.Drawing.Size(268, 21);
            this.CheckBoxAvgRasterSCAcrossTimesteps.TabIndex = 4;
            this.CheckBoxAvgRasterSCAcrossTimesteps.Text = "Average across preceeding timesteps";
            this.CheckBoxAvgRasterSCAcrossTimesteps.UseVisualStyleBackColor = true;
            // 
            // CheckBoxAvgRasterTA
            // 
            this.CheckBoxAvgRasterTA.AutoSize = true;
            this.CheckBoxAvgRasterTA.Location = new System.Drawing.Point(15, 118);
            this.CheckBoxAvgRasterTA.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxAvgRasterTA.Name = "CheckBoxAvgRasterTA";
            this.CheckBoxAvgRasterTA.Size = new System.Drawing.Size(195, 21);
            this.CheckBoxAvgRasterTA.TabIndex = 13;
            this.CheckBoxAvgRasterTA.Text = "Transition attributes every";
            this.CheckBoxAvgRasterTA.UseVisualStyleBackColor = true;
            // 
            // CheckBoxAvgRasterSC
            // 
            this.CheckBoxAvgRasterSC.AutoSize = true;
            this.CheckBoxAvgRasterSC.Location = new System.Drawing.Point(15, 31);
            this.CheckBoxAvgRasterSC.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxAvgRasterSC.Name = "CheckBoxAvgRasterSC";
            this.CheckBoxAvgRasterSC.Size = new System.Drawing.Size(153, 21);
            this.CheckBoxAvgRasterSC.TabIndex = 1;
            this.CheckBoxAvgRasterSC.Text = "State classes every";
            this.CheckBoxAvgRasterSC.UseVisualStyleBackColor = true;
            // 
            // LabelAvgRasterTATimesteps
            // 
            this.LabelAvgRasterTATimesteps.AutoSize = true;
            this.LabelAvgRasterTATimesteps.Location = new System.Drawing.Point(339, 118);
            this.LabelAvgRasterTATimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelAvgRasterTATimesteps.Name = "LabelAvgRasterTATimesteps";
            this.LabelAvgRasterTATimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelAvgRasterTATimesteps.TabIndex = 15;
            this.LabelAvgRasterTATimesteps.Text = "timesteps";
            // 
            // LabelAvgRasterSCTimesteps
            // 
            this.LabelAvgRasterSCTimesteps.AutoSize = true;
            this.LabelAvgRasterSCTimesteps.Location = new System.Drawing.Point(339, 31);
            this.LabelAvgRasterSCTimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelAvgRasterSCTimesteps.Name = "LabelAvgRasterSCTimesteps";
            this.LabelAvgRasterSCTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelAvgRasterSCTimesteps.TabIndex = 3;
            this.LabelAvgRasterSCTimesteps.Text = "timesteps";
            // 
            // TextBoxAvgRasterTATimesteps
            // 
            this.TextBoxAvgRasterTATimesteps.Location = new System.Drawing.Point(262, 115);
            this.TextBoxAvgRasterTATimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxAvgRasterTATimesteps.Name = "TextBoxAvgRasterTATimesteps";
            this.TextBoxAvgRasterTATimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxAvgRasterTATimesteps.TabIndex = 14;
            this.TextBoxAvgRasterTATimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TextBoxAvgRasterSCTimesteps
            // 
            this.TextBoxAvgRasterSCTimesteps.Location = new System.Drawing.Point(262, 28);
            this.TextBoxAvgRasterSCTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxAvgRasterSCTimesteps.Name = "TextBoxAvgRasterSCTimesteps";
            this.TextBoxAvgRasterSCTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxAvgRasterSCTimesteps.TabIndex = 2;
            this.TextBoxAvgRasterSCTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxAvgRasterSA
            // 
            this.CheckBoxAvgRasterSA.AutoSize = true;
            this.CheckBoxAvgRasterSA.Location = new System.Drawing.Point(15, 89);
            this.CheckBoxAvgRasterSA.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxAvgRasterSA.Name = "CheckBoxAvgRasterSA";
            this.CheckBoxAvgRasterSA.Size = new System.Drawing.Size(165, 21);
            this.CheckBoxAvgRasterSA.TabIndex = 9;
            this.CheckBoxAvgRasterSA.Text = "State attributes every";
            this.CheckBoxAvgRasterSA.UseVisualStyleBackColor = true;
            // 
            // LabelAvgRasterSATimesteps
            // 
            this.LabelAvgRasterSATimesteps.AutoSize = true;
            this.LabelAvgRasterSATimesteps.Location = new System.Drawing.Point(339, 90);
            this.LabelAvgRasterSATimesteps.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LabelAvgRasterSATimesteps.Name = "LabelAvgRasterSATimesteps";
            this.LabelAvgRasterSATimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelAvgRasterSATimesteps.TabIndex = 11;
            this.LabelAvgRasterSATimesteps.Text = "timesteps";
            // 
            // TextBoxAvgRasterSATimesteps
            // 
            this.TextBoxAvgRasterSATimesteps.Location = new System.Drawing.Point(262, 86);
            this.TextBoxAvgRasterSATimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxAvgRasterSATimesteps.Name = "TextBoxAvgRasterSATimesteps";
            this.TextBoxAvgRasterSATimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxAvgRasterSATimesteps.TabIndex = 10;
            this.TextBoxAvgRasterSATimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // OutputOptionsDataFeedView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GroupBoxAvgSpatialOutput);
            this.Controls.Add(this.GroupBoxSpatialOutput);
            this.Controls.Add(this.GroupBoxSummaryOutput);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "OutputOptionsDataFeedView";
            this.Size = new System.Drawing.Size(860, 574);
            this.GroupBoxSpatialOutput.ResumeLayout(false);
            this.GroupBoxSpatialOutput.PerformLayout();
            this.GroupBoxSummaryOutput.ResumeLayout(false);
            this.GroupBoxSummaryOutput.PerformLayout();
            this.GroupBoxAvgSpatialOutput.ResumeLayout(false);
            this.GroupBoxAvgSpatialOutput.PerformLayout();
            this.ResumeLayout(false);

        }
        internal System.Windows.Forms.GroupBox GroupBoxSpatialOutput;
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
        internal System.Windows.Forms.CheckBox CheckBoxRasterTE;
        internal System.Windows.Forms.Label LabelRasterTETimesteps;
        internal System.Windows.Forms.TextBox TextBoxRasterTETimesteps;
        private System.Windows.Forms.GroupBox GroupBoxAvgSpatialOutput;
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
    }
}
