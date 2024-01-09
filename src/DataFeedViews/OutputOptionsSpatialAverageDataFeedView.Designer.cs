// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal partial class OutputOptionsSpatialAverageDataFeedView : SyncroSim.Core.Forms.DataFeedView
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
            this.CheckBoxAvgRasterTP = new System.Windows.Forms.CheckBox();
            this.LabelAvgRasterTPTimesteps = new System.Windows.Forms.Label();
            this.TextBoxAvgRasterTPTimesteps = new System.Windows.Forms.TextBox();
            this.CheckBoxAvgRasterSTCumulative = new System.Windows.Forms.CheckBox();
            this.CheckBoxAvgRasterST = new System.Windows.Forms.CheckBox();
            this.LabelAvgRasterSTTimesteps = new System.Windows.Forms.Label();
            this.TextBoxAvgRasterSTTimesteps = new System.Windows.Forms.TextBox();
            this.CheckBoxAvgRasterAgeCumulative = new System.Windows.Forms.CheckBox();
            this.CheckBoxAvgRasterAge = new System.Windows.Forms.CheckBox();
            this.LabelAvgRasterAgeTimesteps = new System.Windows.Forms.Label();
            this.TextBoxAvgRasterAgeTimesteps = new System.Windows.Forms.TextBox();
            this.CheckBoxAvgRasterTPCumulative = new System.Windows.Forms.CheckBox();
            this.CheckBoxAvgRasterTACumulative = new System.Windows.Forms.CheckBox();
            this.CheckBoxAvgRasterSACumulative = new System.Windows.Forms.CheckBox();
            this.CheckBoxAvgRasterSCCumulative = new System.Windows.Forms.CheckBox();
            this.CheckBoxAvgRasterTA = new System.Windows.Forms.CheckBox();
            this.CheckBoxAvgRasterSC = new System.Windows.Forms.CheckBox();
            this.LabelAvgRasterTATimesteps = new System.Windows.Forms.Label();
            this.LabelAvgRasterSCTimesteps = new System.Windows.Forms.Label();
            this.TextBoxAvgRasterTATimesteps = new System.Windows.Forms.TextBox();
            this.TextBoxAvgRasterSCTimesteps = new System.Windows.Forms.TextBox();
            this.CheckBoxAvgRasterSA = new System.Windows.Forms.CheckBox();
            this.LabelAvgRasterSATimesteps = new System.Windows.Forms.Label();
            this.TextBoxAvgRasterSATimesteps = new System.Windows.Forms.TextBox();
            this.PanelMain = new System.Windows.Forms.Panel();
            this.TableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.CheckBoxAvgRasterTST = new System.Windows.Forms.CheckBox();
            this.TextBoxAvgRasterTSTTimesteps = new System.Windows.Forms.TextBox();
            this.LabelAvgRasterTSTTimesteps = new System.Windows.Forms.Label();
            this.CheckBoxAvgRasterTSTCumulative = new System.Windows.Forms.CheckBox();
            this.PanelMain.SuspendLayout();
            this.TableLayoutPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // CheckBoxAvgRasterTP
            // 
            this.CheckBoxAvgRasterTP.AutoSize = true;
            this.CheckBoxAvgRasterTP.Location = new System.Drawing.Point(4, 94);
            this.CheckBoxAvgRasterTP.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxAvgRasterTP.Name = "CheckBoxAvgRasterTP";
            this.CheckBoxAvgRasterTP.Size = new System.Drawing.Size(201, 21);
            this.CheckBoxAvgRasterTP.TabIndex = 12;
            this.CheckBoxAvgRasterTP.Text = "Transition probability every";
            this.CheckBoxAvgRasterTP.UseVisualStyleBackColor = true;
            // 
            // LabelAvgRasterTPTimesteps
            // 
            this.LabelAvgRasterTPTimesteps.AutoSize = true;
            this.LabelAvgRasterTPTimesteps.Location = new System.Drawing.Point(334, 96);
            this.LabelAvgRasterTPTimesteps.Margin = new System.Windows.Forms.Padding(4, 6, 4, 0);
            this.LabelAvgRasterTPTimesteps.Name = "LabelAvgRasterTPTimesteps";
            this.LabelAvgRasterTPTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelAvgRasterTPTimesteps.TabIndex = 14;
            this.LabelAvgRasterTPTimesteps.Text = "timesteps";
            // 
            // TextBoxAvgRasterTPTimesteps
            // 
            this.TextBoxAvgRasterTPTimesteps.Location = new System.Drawing.Point(247, 94);
            this.TextBoxAvgRasterTPTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxAvgRasterTPTimesteps.Name = "TextBoxAvgRasterTPTimesteps";
            this.TextBoxAvgRasterTPTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxAvgRasterTPTimesteps.TabIndex = 13;
            this.TextBoxAvgRasterTPTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxAvgRasterSTCumulative
            // 
            this.CheckBoxAvgRasterSTCumulative.AutoSize = true;
            this.CheckBoxAvgRasterSTCumulative.Location = new System.Drawing.Point(461, 64);
            this.CheckBoxAvgRasterSTCumulative.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxAvgRasterSTCumulative.Name = "CheckBoxAvgRasterSTCumulative";
            this.CheckBoxAvgRasterSTCumulative.Size = new System.Drawing.Size(99, 21);
            this.CheckBoxAvgRasterSTCumulative.TabIndex = 11;
            this.CheckBoxAvgRasterSTCumulative.Text = "Cumulative";
            this.CheckBoxAvgRasterSTCumulative.UseVisualStyleBackColor = true;
            // 
            // CheckBoxAvgRasterST
            // 
            this.CheckBoxAvgRasterST.AutoSize = true;
            this.CheckBoxAvgRasterST.Location = new System.Drawing.Point(4, 64);
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
            this.LabelAvgRasterSTTimesteps.Location = new System.Drawing.Point(334, 66);
            this.LabelAvgRasterSTTimesteps.Margin = new System.Windows.Forms.Padding(4, 6, 4, 0);
            this.LabelAvgRasterSTTimesteps.Name = "LabelAvgRasterSTTimesteps";
            this.LabelAvgRasterSTTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelAvgRasterSTTimesteps.TabIndex = 10;
            this.LabelAvgRasterSTTimesteps.Text = "timesteps";
            // 
            // TextBoxAvgRasterSTTimesteps
            // 
            this.TextBoxAvgRasterSTTimesteps.Location = new System.Drawing.Point(247, 64);
            this.TextBoxAvgRasterSTTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxAvgRasterSTTimesteps.Name = "TextBoxAvgRasterSTTimesteps";
            this.TextBoxAvgRasterSTTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxAvgRasterSTTimesteps.TabIndex = 9;
            this.TextBoxAvgRasterSTTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxAvgRasterAgeCumulative
            // 
            this.CheckBoxAvgRasterAgeCumulative.AutoSize = true;
            this.CheckBoxAvgRasterAgeCumulative.Location = new System.Drawing.Point(461, 34);
            this.CheckBoxAvgRasterAgeCumulative.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxAvgRasterAgeCumulative.Name = "CheckBoxAvgRasterAgeCumulative";
            this.CheckBoxAvgRasterAgeCumulative.Size = new System.Drawing.Size(99, 21);
            this.CheckBoxAvgRasterAgeCumulative.TabIndex = 7;
            this.CheckBoxAvgRasterAgeCumulative.Text = "Cumulative";
            this.CheckBoxAvgRasterAgeCumulative.UseVisualStyleBackColor = true;
            // 
            // CheckBoxAvgRasterAge
            // 
            this.CheckBoxAvgRasterAge.AutoSize = true;
            this.CheckBoxAvgRasterAge.Location = new System.Drawing.Point(4, 34);
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
            this.LabelAvgRasterAgeTimesteps.Location = new System.Drawing.Point(334, 36);
            this.LabelAvgRasterAgeTimesteps.Margin = new System.Windows.Forms.Padding(4, 6, 4, 0);
            this.LabelAvgRasterAgeTimesteps.Name = "LabelAvgRasterAgeTimesteps";
            this.LabelAvgRasterAgeTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelAvgRasterAgeTimesteps.TabIndex = 6;
            this.LabelAvgRasterAgeTimesteps.Text = "timesteps";
            // 
            // TextBoxAvgRasterAgeTimesteps
            // 
            this.TextBoxAvgRasterAgeTimesteps.Location = new System.Drawing.Point(247, 34);
            this.TextBoxAvgRasterAgeTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxAvgRasterAgeTimesteps.Name = "TextBoxAvgRasterAgeTimesteps";
            this.TextBoxAvgRasterAgeTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxAvgRasterAgeTimesteps.TabIndex = 5;
            this.TextBoxAvgRasterAgeTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxAvgRasterTPCumulative
            // 
            this.CheckBoxAvgRasterTPCumulative.AutoSize = true;
            this.CheckBoxAvgRasterTPCumulative.Location = new System.Drawing.Point(461, 94);
            this.CheckBoxAvgRasterTPCumulative.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxAvgRasterTPCumulative.Name = "CheckBoxAvgRasterTPCumulative";
            this.CheckBoxAvgRasterTPCumulative.Size = new System.Drawing.Size(99, 21);
            this.CheckBoxAvgRasterTPCumulative.TabIndex = 15;
            this.CheckBoxAvgRasterTPCumulative.Text = "Cumulative";
            this.CheckBoxAvgRasterTPCumulative.UseVisualStyleBackColor = true;
            // 
            // CheckBoxAvgRasterTACumulative
            // 
            this.CheckBoxAvgRasterTACumulative.AutoSize = true;
            this.CheckBoxAvgRasterTACumulative.Location = new System.Drawing.Point(461, 184);
            this.CheckBoxAvgRasterTACumulative.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxAvgRasterTACumulative.Name = "CheckBoxAvgRasterTACumulative";
            this.CheckBoxAvgRasterTACumulative.Size = new System.Drawing.Size(99, 21);
            this.CheckBoxAvgRasterTACumulative.TabIndex = 27;
            this.CheckBoxAvgRasterTACumulative.Text = "Cumulative";
            this.CheckBoxAvgRasterTACumulative.UseVisualStyleBackColor = true;
            // 
            // CheckBoxAvgRasterSACumulative
            // 
            this.CheckBoxAvgRasterSACumulative.AutoSize = true;
            this.CheckBoxAvgRasterSACumulative.Location = new System.Drawing.Point(461, 154);
            this.CheckBoxAvgRasterSACumulative.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxAvgRasterSACumulative.Name = "CheckBoxAvgRasterSACumulative";
            this.CheckBoxAvgRasterSACumulative.Size = new System.Drawing.Size(99, 21);
            this.CheckBoxAvgRasterSACumulative.TabIndex = 23;
            this.CheckBoxAvgRasterSACumulative.Text = "Cumulative";
            this.CheckBoxAvgRasterSACumulative.UseVisualStyleBackColor = true;
            // 
            // CheckBoxAvgRasterSCCumulative
            // 
            this.CheckBoxAvgRasterSCCumulative.AutoSize = true;
            this.CheckBoxAvgRasterSCCumulative.Location = new System.Drawing.Point(461, 4);
            this.CheckBoxAvgRasterSCCumulative.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxAvgRasterSCCumulative.Name = "CheckBoxAvgRasterSCCumulative";
            this.CheckBoxAvgRasterSCCumulative.Size = new System.Drawing.Size(99, 21);
            this.CheckBoxAvgRasterSCCumulative.TabIndex = 3;
            this.CheckBoxAvgRasterSCCumulative.Text = "Cumulative";
            this.CheckBoxAvgRasterSCCumulative.UseVisualStyleBackColor = true;
            // 
            // CheckBoxAvgRasterTA
            // 
            this.CheckBoxAvgRasterTA.AutoSize = true;
            this.CheckBoxAvgRasterTA.Location = new System.Drawing.Point(4, 184);
            this.CheckBoxAvgRasterTA.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxAvgRasterTA.Name = "CheckBoxAvgRasterTA";
            this.CheckBoxAvgRasterTA.Size = new System.Drawing.Size(195, 21);
            this.CheckBoxAvgRasterTA.TabIndex = 24;
            this.CheckBoxAvgRasterTA.Text = "Transition attributes every";
            this.CheckBoxAvgRasterTA.UseVisualStyleBackColor = true;
            // 
            // CheckBoxAvgRasterSC
            // 
            this.CheckBoxAvgRasterSC.AutoSize = true;
            this.CheckBoxAvgRasterSC.Location = new System.Drawing.Point(4, 4);
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
            this.LabelAvgRasterTATimesteps.Location = new System.Drawing.Point(334, 186);
            this.LabelAvgRasterTATimesteps.Margin = new System.Windows.Forms.Padding(4, 6, 4, 0);
            this.LabelAvgRasterTATimesteps.Name = "LabelAvgRasterTATimesteps";
            this.LabelAvgRasterTATimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelAvgRasterTATimesteps.TabIndex = 26;
            this.LabelAvgRasterTATimesteps.Text = "timesteps";
            // 
            // LabelAvgRasterSCTimesteps
            // 
            this.LabelAvgRasterSCTimesteps.AutoSize = true;
            this.LabelAvgRasterSCTimesteps.Location = new System.Drawing.Point(334, 6);
            this.LabelAvgRasterSCTimesteps.Margin = new System.Windows.Forms.Padding(4, 6, 4, 0);
            this.LabelAvgRasterSCTimesteps.Name = "LabelAvgRasterSCTimesteps";
            this.LabelAvgRasterSCTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelAvgRasterSCTimesteps.TabIndex = 2;
            this.LabelAvgRasterSCTimesteps.Text = "timesteps";
            // 
            // TextBoxAvgRasterTATimesteps
            // 
            this.TextBoxAvgRasterTATimesteps.Location = new System.Drawing.Point(247, 184);
            this.TextBoxAvgRasterTATimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxAvgRasterTATimesteps.Name = "TextBoxAvgRasterTATimesteps";
            this.TextBoxAvgRasterTATimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxAvgRasterTATimesteps.TabIndex = 25;
            this.TextBoxAvgRasterTATimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TextBoxAvgRasterSCTimesteps
            // 
            this.TextBoxAvgRasterSCTimesteps.Location = new System.Drawing.Point(247, 4);
            this.TextBoxAvgRasterSCTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxAvgRasterSCTimesteps.Name = "TextBoxAvgRasterSCTimesteps";
            this.TextBoxAvgRasterSCTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxAvgRasterSCTimesteps.TabIndex = 1;
            this.TextBoxAvgRasterSCTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxAvgRasterSA
            // 
            this.CheckBoxAvgRasterSA.AutoSize = true;
            this.CheckBoxAvgRasterSA.Location = new System.Drawing.Point(4, 154);
            this.CheckBoxAvgRasterSA.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxAvgRasterSA.Name = "CheckBoxAvgRasterSA";
            this.CheckBoxAvgRasterSA.Size = new System.Drawing.Size(165, 21);
            this.CheckBoxAvgRasterSA.TabIndex = 20;
            this.CheckBoxAvgRasterSA.Text = "State attributes every";
            this.CheckBoxAvgRasterSA.UseVisualStyleBackColor = true;
            // 
            // LabelAvgRasterSATimesteps
            // 
            this.LabelAvgRasterSATimesteps.AutoSize = true;
            this.LabelAvgRasterSATimesteps.Location = new System.Drawing.Point(334, 156);
            this.LabelAvgRasterSATimesteps.Margin = new System.Windows.Forms.Padding(4, 6, 4, 0);
            this.LabelAvgRasterSATimesteps.Name = "LabelAvgRasterSATimesteps";
            this.LabelAvgRasterSATimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelAvgRasterSATimesteps.TabIndex = 22;
            this.LabelAvgRasterSATimesteps.Text = "timesteps";
            // 
            // TextBoxAvgRasterSATimesteps
            // 
            this.TextBoxAvgRasterSATimesteps.Location = new System.Drawing.Point(247, 154);
            this.TextBoxAvgRasterSATimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxAvgRasterSATimesteps.Name = "TextBoxAvgRasterSATimesteps";
            this.TextBoxAvgRasterSATimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxAvgRasterSATimesteps.TabIndex = 21;
            this.TextBoxAvgRasterSATimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.TableLayoutPanelMain.ColumnCount = 4;
            this.TableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 243F));
            this.TableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            this.TableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 127F));
            this.TableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 1329F));
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxAvgRasterSC, 0, 0);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxAvgRasterAge, 0, 1);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxAvgRasterST, 0, 2);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxAvgRasterSTCumulative, 3, 2);
            this.TableLayoutPanelMain.Controls.Add(this.TextBoxAvgRasterSTTimesteps, 1, 2);
            this.TableLayoutPanelMain.Controls.Add(this.TextBoxAvgRasterAgeTimesteps, 1, 1);
            this.TableLayoutPanelMain.Controls.Add(this.LabelAvgRasterSTTimesteps, 2, 2);
            this.TableLayoutPanelMain.Controls.Add(this.TextBoxAvgRasterSCTimesteps, 1, 0);
            this.TableLayoutPanelMain.Controls.Add(this.LabelAvgRasterAgeTimesteps, 2, 1);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxAvgRasterTPCumulative, 3, 3);
            this.TableLayoutPanelMain.Controls.Add(this.TextBoxAvgRasterTPTimesteps, 1, 3);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxAvgRasterAgeCumulative, 3, 1);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxAvgRasterSCCumulative, 3, 0);
            this.TableLayoutPanelMain.Controls.Add(this.LabelAvgRasterSCTimesteps, 2, 0);
            this.TableLayoutPanelMain.Controls.Add(this.LabelAvgRasterTPTimesteps, 2, 3);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxAvgRasterTA, 0, 6);
            this.TableLayoutPanelMain.Controls.Add(this.TextBoxAvgRasterTATimesteps, 1, 6);
            this.TableLayoutPanelMain.Controls.Add(this.LabelAvgRasterTATimesteps, 2, 6);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxAvgRasterTACumulative, 3, 6);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxAvgRasterSA, 0, 5);
            this.TableLayoutPanelMain.Controls.Add(this.TextBoxAvgRasterSATimesteps, 1, 5);
            this.TableLayoutPanelMain.Controls.Add(this.LabelAvgRasterSATimesteps, 2, 5);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxAvgRasterSACumulative, 3, 5);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxAvgRasterTP, 0, 3);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxAvgRasterTST, 0, 4);
            this.TableLayoutPanelMain.Controls.Add(this.TextBoxAvgRasterTSTTimesteps, 1, 4);
            this.TableLayoutPanelMain.Controls.Add(this.LabelAvgRasterTSTTimesteps, 2, 4);
            this.TableLayoutPanelMain.Controls.Add(this.CheckBoxAvgRasterTSTCumulative, 3, 4);
            this.TableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.TableLayoutPanelMain.Location = new System.Drawing.Point(20, 20);
            this.TableLayoutPanelMain.Name = "TableLayoutPanelMain";
            this.TableLayoutPanelMain.RowCount = 7;
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.TableLayoutPanelMain.Size = new System.Drawing.Size(1786, 214);
            this.TableLayoutPanelMain.TabIndex = 24;
            // 
            // CheckBoxAvgRasterTST
            // 
            this.CheckBoxAvgRasterTST.AutoSize = true;
            this.CheckBoxAvgRasterTST.Location = new System.Drawing.Point(4, 124);
            this.CheckBoxAvgRasterTST.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxAvgRasterTST.Name = "CheckBoxAvgRasterTST";
            this.CheckBoxAvgRasterTST.Size = new System.Drawing.Size(201, 21);
            this.CheckBoxAvgRasterTST.TabIndex = 16;
            this.CheckBoxAvgRasterTST.Text = "Time-since-transition every";
            this.CheckBoxAvgRasterTST.UseVisualStyleBackColor = true;
            // 
            // TextBoxAvgRasterTSTTimesteps
            // 
            this.TextBoxAvgRasterTSTTimesteps.Location = new System.Drawing.Point(247, 124);
            this.TextBoxAvgRasterTSTTimesteps.Margin = new System.Windows.Forms.Padding(4);
            this.TextBoxAvgRasterTSTTimesteps.Name = "TextBoxAvgRasterTSTTimesteps";
            this.TextBoxAvgRasterTSTTimesteps.Size = new System.Drawing.Size(65, 22);
            this.TextBoxAvgRasterTSTTimesteps.TabIndex = 17;
            this.TextBoxAvgRasterTSTTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // LabelAvgRasterTSTTimesteps
            // 
            this.LabelAvgRasterTSTTimesteps.AutoSize = true;
            this.LabelAvgRasterTSTTimesteps.Location = new System.Drawing.Point(334, 126);
            this.LabelAvgRasterTSTTimesteps.Margin = new System.Windows.Forms.Padding(4, 6, 4, 0);
            this.LabelAvgRasterTSTTimesteps.Name = "LabelAvgRasterTSTTimesteps";
            this.LabelAvgRasterTSTTimesteps.Size = new System.Drawing.Size(68, 17);
            this.LabelAvgRasterTSTTimesteps.TabIndex = 18;
            this.LabelAvgRasterTSTTimesteps.Text = "timesteps";
            // 
            // CheckBoxAvgRasterTSTCumulative
            // 
            this.CheckBoxAvgRasterTSTCumulative.AutoSize = true;
            this.CheckBoxAvgRasterTSTCumulative.Location = new System.Drawing.Point(461, 124);
            this.CheckBoxAvgRasterTSTCumulative.Margin = new System.Windows.Forms.Padding(4);
            this.CheckBoxAvgRasterTSTCumulative.Name = "CheckBoxAvgRasterTSTCumulative";
            this.CheckBoxAvgRasterTSTCumulative.Size = new System.Drawing.Size(99, 21);
            this.CheckBoxAvgRasterTSTCumulative.TabIndex = 19;
            this.CheckBoxAvgRasterTSTCumulative.Text = "Cumulative";
            this.CheckBoxAvgRasterTSTCumulative.UseVisualStyleBackColor = true;
            // 
            // OutputOptionsSpatialAverageDataFeedView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PanelMain);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "OutputOptionsSpatialAverageDataFeedView";
            this.Size = new System.Drawing.Size(1826, 903);
            this.PanelMain.ResumeLayout(false);
            this.TableLayoutPanelMain.ResumeLayout(false);
            this.TableLayoutPanelMain.PerformLayout();
            this.ResumeLayout(false);

        }
        internal System.Windows.Forms.CheckBox CheckBoxAvgRasterTP;
        internal System.Windows.Forms.Label LabelAvgRasterTPTimesteps;
        internal System.Windows.Forms.TextBox TextBoxAvgRasterTPTimesteps;
        internal System.Windows.Forms.CheckBox CheckBoxAvgRasterTA;
        internal System.Windows.Forms.CheckBox CheckBoxAvgRasterSC;
        internal System.Windows.Forms.Label LabelAvgRasterTATimesteps;
        internal System.Windows.Forms.Label LabelAvgRasterSCTimesteps;
        internal System.Windows.Forms.TextBox TextBoxAvgRasterTATimesteps;
        internal System.Windows.Forms.TextBox TextBoxAvgRasterSCTimesteps;
        internal System.Windows.Forms.CheckBox CheckBoxAvgRasterSA;
        internal System.Windows.Forms.Label LabelAvgRasterSATimesteps;
        internal System.Windows.Forms.TextBox TextBoxAvgRasterSATimesteps;
        internal System.Windows.Forms.CheckBox CheckBoxAvgRasterSCCumulative;
        internal System.Windows.Forms.CheckBox CheckBoxAvgRasterTPCumulative;
        internal System.Windows.Forms.CheckBox CheckBoxAvgRasterTACumulative;
        internal System.Windows.Forms.CheckBox CheckBoxAvgRasterSACumulative;
        internal System.Windows.Forms.CheckBox CheckBoxAvgRasterAgeCumulative;
        internal System.Windows.Forms.CheckBox CheckBoxAvgRasterAge;
        internal System.Windows.Forms.Label LabelAvgRasterAgeTimesteps;
        internal System.Windows.Forms.TextBox TextBoxAvgRasterAgeTimesteps;
        internal System.Windows.Forms.CheckBox CheckBoxAvgRasterSTCumulative;
        internal System.Windows.Forms.CheckBox CheckBoxAvgRasterST;
        internal System.Windows.Forms.Label LabelAvgRasterSTTimesteps;
        internal System.Windows.Forms.TextBox TextBoxAvgRasterSTTimesteps;
        private System.Windows.Forms.Panel PanelMain;
        private System.Windows.Forms.TableLayoutPanel TableLayoutPanelMain;
        internal System.Windows.Forms.CheckBox CheckBoxAvgRasterTST;
        internal System.Windows.Forms.TextBox TextBoxAvgRasterTSTTimesteps;
        internal System.Windows.Forms.Label LabelAvgRasterTSTTimesteps;
        internal System.Windows.Forms.CheckBox CheckBoxAvgRasterTSTCumulative;
    }
}
