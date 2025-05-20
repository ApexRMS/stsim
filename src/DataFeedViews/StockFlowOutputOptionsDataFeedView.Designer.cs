namespace SyncroSim.STSim
{
	internal partial class StockFlowOutputOptionsDataFeedView : SyncroSim.Core.Forms.DataFeedView
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
            this.LabelSpatialFLTimesteps = new System.Windows.Forms.Label();
            this.LabelSpatialSTTimesteps = new System.Windows.Forms.Label();
            this.TextBoxSpatialFLTimesteps = new System.Windows.Forms.TextBox();
            this.TextBoxSpatialSTTimesteps = new System.Windows.Forms.TextBox();
            this.CheckBoxSpatialFL = new System.Windows.Forms.CheckBox();
            this.CheckBoxSpatialST = new System.Windows.Forms.CheckBox();
            this.GroupBoxSpatial = new System.Windows.Forms.GroupBox();
            this.LabelLateralFLTimesteps = new System.Windows.Forms.Label();
            this.TextBoxLateralFLTimesteps = new System.Windows.Forms.TextBox();
            this.CheckBoxLateralFL = new System.Windows.Forms.CheckBox();
            this.LabelSummaryFLTimesteps = new System.Windows.Forms.Label();
            this.LabelSummarySTTimesteps = new System.Windows.Forms.Label();
            this.TextBoxSummaryFLTimesteps = new System.Windows.Forms.TextBox();
            this.GroupBoxSummaryST = new System.Windows.Forms.GroupBox();
            this.CheckBoxSTOmitSC = new System.Windows.Forms.CheckBox();
            this.CheckBoxSTOmitTS = new System.Windows.Forms.CheckBox();
            this.CheckBoxSTOmitSS = new System.Windows.Forms.CheckBox();
            this.TextBoxSummarySTTimesteps = new System.Windows.Forms.TextBox();
            this.CheckBoxSummaryST = new System.Windows.Forms.CheckBox();
            this.CheckBoxSummaryFL = new System.Windows.Forms.CheckBox();
            this.GroupBoxAvgSpatial = new System.Windows.Forms.GroupBox();
            this.LabelAvgSpatialLFLTimesteps = new System.Windows.Forms.Label();
            this.TextBoxAvgSpatialLFLTimesteps = new System.Windows.Forms.TextBox();
            this.CheckBoxAvgSpatialLFLCumulative = new System.Windows.Forms.CheckBox();
            this.CheckBoxAvgSpatialLFL = new System.Windows.Forms.CheckBox();
            this.LabelAvgSpatialFLTimesteps = new System.Windows.Forms.Label();
            this.LabelAvgSpatialSTTimesteps = new System.Windows.Forms.Label();
            this.TextBoxAvgSpatialFLTimesteps = new System.Windows.Forms.TextBox();
            this.TextBoxAvgSpatialSTTimesteps = new System.Windows.Forms.TextBox();
            this.CheckBoxAvgSpatialFLCumulative = new System.Windows.Forms.CheckBox();
            this.CheckBoxAvgSpatialFL = new System.Windows.Forms.CheckBox();
            this.CheckBoxAvgSpatialSTCumulative = new System.Windows.Forms.CheckBox();
            this.CheckBoxAvgSpatialST = new System.Windows.Forms.CheckBox();
            this.GroupBoxSummaryFL = new System.Windows.Forms.GroupBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.GroupBoxSpatial.SuspendLayout();
            this.GroupBoxSummaryST.SuspendLayout();
            this.GroupBoxAvgSpatial.SuspendLayout();
            this.GroupBoxSummaryFL.SuspendLayout();
            this.SuspendLayout();
            // 
            // LabelSpatialFLTimesteps
            // 
            this.LabelSpatialFLTimesteps.AutoSize = true;
            this.LabelSpatialFLTimesteps.Location = new System.Drawing.Point(206, 46);
            this.LabelSpatialFLTimesteps.Name = "LabelSpatialFLTimesteps";
            this.LabelSpatialFLTimesteps.Size = new System.Drawing.Size(51, 13);
            this.LabelSpatialFLTimesteps.TabIndex = 5;
            this.LabelSpatialFLTimesteps.Text = "timesteps";
            // 
            // LabelSpatialSTTimesteps
            // 
            this.LabelSpatialSTTimesteps.AutoSize = true;
            this.LabelSpatialSTTimesteps.Location = new System.Drawing.Point(206, 25);
            this.LabelSpatialSTTimesteps.Name = "LabelSpatialSTTimesteps";
            this.LabelSpatialSTTimesteps.Size = new System.Drawing.Size(51, 13);
            this.LabelSpatialSTTimesteps.TabIndex = 2;
            this.LabelSpatialSTTimesteps.Text = "timesteps";
            // 
            // TextBoxSpatialFLTimesteps
            // 
            this.TextBoxSpatialFLTimesteps.Location = new System.Drawing.Point(144, 44);
            this.TextBoxSpatialFLTimesteps.Name = "TextBoxSpatialFLTimesteps";
            this.TextBoxSpatialFLTimesteps.Size = new System.Drawing.Size(56, 20);
            this.TextBoxSpatialFLTimesteps.TabIndex = 4;
            this.TextBoxSpatialFLTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TextBoxSpatialSTTimesteps
            // 
            this.TextBoxSpatialSTTimesteps.Location = new System.Drawing.Point(144, 21);
            this.TextBoxSpatialSTTimesteps.Name = "TextBoxSpatialSTTimesteps";
            this.TextBoxSpatialSTTimesteps.Size = new System.Drawing.Size(56, 20);
            this.TextBoxSpatialSTTimesteps.TabIndex = 1;
            this.TextBoxSpatialSTTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxSpatialFL
            // 
            this.CheckBoxSpatialFL.AutoSize = true;
            this.CheckBoxSpatialFL.Location = new System.Drawing.Point(15, 47);
            this.CheckBoxSpatialFL.Name = "CheckBoxSpatialFL";
            this.CheckBoxSpatialFL.Size = new System.Drawing.Size(82, 17);
            this.CheckBoxSpatialFL.TabIndex = 3;
            this.CheckBoxSpatialFL.Text = "Flows every";
            this.CheckBoxSpatialFL.UseVisualStyleBackColor = true;
            // 
            // CheckBoxSpatialST
            // 
            this.CheckBoxSpatialST.AutoSize = true;
            this.CheckBoxSpatialST.Location = new System.Drawing.Point(15, 24);
            this.CheckBoxSpatialST.Name = "CheckBoxSpatialST";
            this.CheckBoxSpatialST.Size = new System.Drawing.Size(88, 17);
            this.CheckBoxSpatialST.TabIndex = 0;
            this.CheckBoxSpatialST.Text = "Stocks every";
            this.CheckBoxSpatialST.UseVisualStyleBackColor = true;
            // 
            // GroupBoxSpatial
            // 
            this.GroupBoxSpatial.Controls.Add(this.LabelLateralFLTimesteps);
            this.GroupBoxSpatial.Controls.Add(this.LabelSpatialFLTimesteps);
            this.GroupBoxSpatial.Controls.Add(this.LabelSpatialSTTimesteps);
            this.GroupBoxSpatial.Controls.Add(this.TextBoxLateralFLTimesteps);
            this.GroupBoxSpatial.Controls.Add(this.TextBoxSpatialFLTimesteps);
            this.GroupBoxSpatial.Controls.Add(this.TextBoxSpatialSTTimesteps);
            this.GroupBoxSpatial.Controls.Add(this.CheckBoxLateralFL);
            this.GroupBoxSpatial.Controls.Add(this.CheckBoxSpatialFL);
            this.GroupBoxSpatial.Controls.Add(this.CheckBoxSpatialST);
            this.GroupBoxSpatial.Location = new System.Drawing.Point(3, 259);
            this.GroupBoxSpatial.Name = "GroupBoxSpatial";
            this.GroupBoxSpatial.Size = new System.Drawing.Size(577, 102);
            this.GroupBoxSpatial.TabIndex = 2;
            this.GroupBoxSpatial.TabStop = false;
            this.GroupBoxSpatial.Text = "Spatial output";
            // 
            // LabelLateralFLTimesteps
            // 
            this.LabelLateralFLTimesteps.AutoSize = true;
            this.LabelLateralFLTimesteps.Location = new System.Drawing.Point(206, 69);
            this.LabelLateralFLTimesteps.Name = "LabelLateralFLTimesteps";
            this.LabelLateralFLTimesteps.Size = new System.Drawing.Size(51, 13);
            this.LabelLateralFLTimesteps.TabIndex = 8;
            this.LabelLateralFLTimesteps.Text = "timesteps";
            // 
            // TextBoxLateralFLTimesteps
            // 
            this.TextBoxLateralFLTimesteps.Location = new System.Drawing.Point(144, 67);
            this.TextBoxLateralFLTimesteps.Name = "TextBoxLateralFLTimesteps";
            this.TextBoxLateralFLTimesteps.Size = new System.Drawing.Size(56, 20);
            this.TextBoxLateralFLTimesteps.TabIndex = 7;
            this.TextBoxLateralFLTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxLateralFL
            // 
            this.CheckBoxLateralFL.AutoSize = true;
            this.CheckBoxLateralFL.Location = new System.Drawing.Point(15, 70);
            this.CheckBoxLateralFL.Name = "CheckBoxLateralFL";
            this.CheckBoxLateralFL.Size = new System.Drawing.Size(114, 17);
            this.CheckBoxLateralFL.TabIndex = 6;
            this.CheckBoxLateralFL.Text = "Lateral flows every";
            this.CheckBoxLateralFL.UseVisualStyleBackColor = true;
            // 
            // LabelSummaryFLTimesteps
            // 
            this.LabelSummaryFLTimesteps.AutoSize = true;
            this.LabelSummaryFLTimesteps.Location = new System.Drawing.Point(206, 26);
            this.LabelSummaryFLTimesteps.Name = "LabelSummaryFLTimesteps";
            this.LabelSummaryFLTimesteps.Size = new System.Drawing.Size(51, 13);
            this.LabelSummaryFLTimesteps.TabIndex = 2;
            this.LabelSummaryFLTimesteps.Text = "timesteps";
            // 
            // LabelSummarySTTimesteps
            // 
            this.LabelSummarySTTimesteps.AutoSize = true;
            this.LabelSummarySTTimesteps.Location = new System.Drawing.Point(206, 26);
            this.LabelSummarySTTimesteps.Name = "LabelSummarySTTimesteps";
            this.LabelSummarySTTimesteps.Size = new System.Drawing.Size(51, 13);
            this.LabelSummarySTTimesteps.TabIndex = 2;
            this.LabelSummarySTTimesteps.Text = "timesteps";
            // 
            // TextBoxSummaryFLTimesteps
            // 
            this.TextBoxSummaryFLTimesteps.Location = new System.Drawing.Point(144, 24);
            this.TextBoxSummaryFLTimesteps.Name = "TextBoxSummaryFLTimesteps";
            this.TextBoxSummaryFLTimesteps.Size = new System.Drawing.Size(56, 20);
            this.TextBoxSummaryFLTimesteps.TabIndex = 1;
            this.TextBoxSummaryFLTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // GroupBoxSummaryST
            // 
            this.GroupBoxSummaryST.Controls.Add(this.CheckBoxSTOmitSC);
            this.GroupBoxSummaryST.Controls.Add(this.CheckBoxSTOmitTS);
            this.GroupBoxSummaryST.Controls.Add(this.CheckBoxSTOmitSS);
            this.GroupBoxSummaryST.Controls.Add(this.LabelSummarySTTimesteps);
            this.GroupBoxSummaryST.Controls.Add(this.TextBoxSummarySTTimesteps);
            this.GroupBoxSummaryST.Controls.Add(this.CheckBoxSummaryST);
            this.GroupBoxSummaryST.Location = new System.Drawing.Point(3, 11);
            this.GroupBoxSummaryST.Name = "GroupBoxSummaryST";
            this.GroupBoxSummaryST.Size = new System.Drawing.Size(574, 102);
            this.GroupBoxSummaryST.TabIndex = 0;
            this.GroupBoxSummaryST.TabStop = false;
            this.GroupBoxSummaryST.Text = "Tabular output for stocks";
            // 
            // CheckBoxSTOmitSC
            // 
            this.CheckBoxSTOmitSC.AutoSize = true;
            this.CheckBoxSTOmitSC.Location = new System.Drawing.Point(290, 72);
            this.CheckBoxSTOmitSC.Name = "CheckBoxSTOmitSC";
            this.CheckBoxSTOmitSC.Size = new System.Drawing.Size(100, 17);
            this.CheckBoxSTOmitSC.TabIndex = 5;
            this.CheckBoxSTOmitSC.Text = "Omit state class";
            this.CheckBoxSTOmitSC.UseVisualStyleBackColor = true;
            // 
            // CheckBoxSTOmitTS
            // 
            this.CheckBoxSTOmitTS.AutoSize = true;
            this.CheckBoxSTOmitTS.Location = new System.Drawing.Point(290, 49);
            this.CheckBoxSTOmitTS.Name = "CheckBoxSTOmitTS";
            this.CheckBoxSTOmitTS.Size = new System.Drawing.Size(110, 17);
            this.CheckBoxSTOmitTS.TabIndex = 4;
            this.CheckBoxSTOmitTS.Text = "Omit tertiary strata";
            this.CheckBoxSTOmitTS.UseVisualStyleBackColor = true;
            // 
            // CheckBoxSTOmitSS
            // 
            this.CheckBoxSTOmitSS.AutoSize = true;
            this.CheckBoxSTOmitSS.Location = new System.Drawing.Point(290, 26);
            this.CheckBoxSTOmitSS.Name = "CheckBoxSTOmitSS";
            this.CheckBoxSTOmitSS.Size = new System.Drawing.Size(128, 17);
            this.CheckBoxSTOmitSS.TabIndex = 3;
            this.CheckBoxSTOmitSS.Text = "Omit secondary strata";
            this.CheckBoxSTOmitSS.UseVisualStyleBackColor = true;
            // 
            // TextBoxSummarySTTimesteps
            // 
            this.TextBoxSummarySTTimesteps.Location = new System.Drawing.Point(144, 22);
            this.TextBoxSummarySTTimesteps.Name = "TextBoxSummarySTTimesteps";
            this.TextBoxSummarySTTimesteps.Size = new System.Drawing.Size(56, 20);
            this.TextBoxSummarySTTimesteps.TabIndex = 1;
            this.TextBoxSummarySTTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxSummaryST
            // 
            this.CheckBoxSummaryST.AutoSize = true;
            this.CheckBoxSummaryST.Location = new System.Drawing.Point(15, 24);
            this.CheckBoxSummaryST.Name = "CheckBoxSummaryST";
            this.CheckBoxSummaryST.Size = new System.Drawing.Size(88, 17);
            this.CheckBoxSummaryST.TabIndex = 0;
            this.CheckBoxSummaryST.Text = "Stocks every";
            this.CheckBoxSummaryST.UseVisualStyleBackColor = true;
            // 
            // CheckBoxSummaryFL
            // 
            this.CheckBoxSummaryFL.AutoSize = true;
            this.CheckBoxSummaryFL.Location = new System.Drawing.Point(15, 24);
            this.CheckBoxSummaryFL.Name = "CheckBoxSummaryFL";
            this.CheckBoxSummaryFL.Size = new System.Drawing.Size(82, 17);
            this.CheckBoxSummaryFL.TabIndex = 0;
            this.CheckBoxSummaryFL.Text = "Flows every";
            this.CheckBoxSummaryFL.UseVisualStyleBackColor = true;
            // 
            // GroupBoxAvgSpatial
            // 
            this.GroupBoxAvgSpatial.Controls.Add(this.LabelAvgSpatialLFLTimesteps);
            this.GroupBoxAvgSpatial.Controls.Add(this.TextBoxAvgSpatialLFLTimesteps);
            this.GroupBoxAvgSpatial.Controls.Add(this.CheckBoxAvgSpatialLFLCumulative);
            this.GroupBoxAvgSpatial.Controls.Add(this.CheckBoxAvgSpatialLFL);
            this.GroupBoxAvgSpatial.Controls.Add(this.LabelAvgSpatialFLTimesteps);
            this.GroupBoxAvgSpatial.Controls.Add(this.LabelAvgSpatialSTTimesteps);
            this.GroupBoxAvgSpatial.Controls.Add(this.TextBoxAvgSpatialFLTimesteps);
            this.GroupBoxAvgSpatial.Controls.Add(this.TextBoxAvgSpatialSTTimesteps);
            this.GroupBoxAvgSpatial.Controls.Add(this.CheckBoxAvgSpatialFLCumulative);
            this.GroupBoxAvgSpatial.Controls.Add(this.CheckBoxAvgSpatialFL);
            this.GroupBoxAvgSpatial.Controls.Add(this.CheckBoxAvgSpatialSTCumulative);
            this.GroupBoxAvgSpatial.Controls.Add(this.CheckBoxAvgSpatialST);
            this.GroupBoxAvgSpatial.Location = new System.Drawing.Point(3, 372);
            this.GroupBoxAvgSpatial.Margin = new System.Windows.Forms.Padding(2);
            this.GroupBoxAvgSpatial.Name = "GroupBoxAvgSpatial";
            this.GroupBoxAvgSpatial.Padding = new System.Windows.Forms.Padding(2);
            this.GroupBoxAvgSpatial.Size = new System.Drawing.Size(577, 102);
            this.GroupBoxAvgSpatial.TabIndex = 3;
            this.GroupBoxAvgSpatial.TabStop = false;
            this.GroupBoxAvgSpatial.Text = "Spatial average output";
            // 
            // LabelAvgSpatialLFLTimesteps
            // 
            this.LabelAvgSpatialLFLTimesteps.AutoSize = true;
            this.LabelAvgSpatialLFLTimesteps.Location = new System.Drawing.Point(209, 70);
            this.LabelAvgSpatialLFLTimesteps.Name = "LabelAvgSpatialLFLTimesteps";
            this.LabelAvgSpatialLFLTimesteps.Size = new System.Drawing.Size(51, 13);
            this.LabelAvgSpatialLFLTimesteps.TabIndex = 10;
            this.LabelAvgSpatialLFLTimesteps.Text = "timesteps";
            // 
            // TextBoxAvgSpatialLFLTimesteps
            // 
            this.TextBoxAvgSpatialLFLTimesteps.Location = new System.Drawing.Point(144, 67);
            this.TextBoxAvgSpatialLFLTimesteps.Name = "TextBoxAvgSpatialLFLTimesteps";
            this.TextBoxAvgSpatialLFLTimesteps.Size = new System.Drawing.Size(56, 20);
            this.TextBoxAvgSpatialLFLTimesteps.TabIndex = 9;
            this.TextBoxAvgSpatialLFLTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxAvgSpatialLFLCumulative
            // 
            this.CheckBoxAvgSpatialLFLCumulative.AutoSize = true;
            this.CheckBoxAvgSpatialLFLCumulative.Location = new System.Drawing.Point(290, 70);
            this.CheckBoxAvgSpatialLFLCumulative.Name = "CheckBoxAvgSpatialLFLCumulative";
            this.CheckBoxAvgSpatialLFLCumulative.Size = new System.Drawing.Size(78, 17);
            this.CheckBoxAvgSpatialLFLCumulative.TabIndex = 11;
            this.CheckBoxAvgSpatialLFLCumulative.Text = "Cumulative";
            this.CheckBoxAvgSpatialLFLCumulative.UseVisualStyleBackColor = true;
            // 
            // CheckBoxAvgSpatialLFL
            // 
            this.CheckBoxAvgSpatialLFL.AutoSize = true;
            this.CheckBoxAvgSpatialLFL.Location = new System.Drawing.Point(15, 70);
            this.CheckBoxAvgSpatialLFL.Name = "CheckBoxAvgSpatialLFL";
            this.CheckBoxAvgSpatialLFL.Size = new System.Drawing.Size(114, 17);
            this.CheckBoxAvgSpatialLFL.TabIndex = 8;
            this.CheckBoxAvgSpatialLFL.Text = "Lateral flows every";
            this.CheckBoxAvgSpatialLFL.UseVisualStyleBackColor = true;
            // 
            // LabelAvgSpatialFLTimesteps
            // 
            this.LabelAvgSpatialFLTimesteps.AutoSize = true;
            this.LabelAvgSpatialFLTimesteps.Location = new System.Drawing.Point(209, 47);
            this.LabelAvgSpatialFLTimesteps.Name = "LabelAvgSpatialFLTimesteps";
            this.LabelAvgSpatialFLTimesteps.Size = new System.Drawing.Size(51, 13);
            this.LabelAvgSpatialFLTimesteps.TabIndex = 6;
            this.LabelAvgSpatialFLTimesteps.Text = "timesteps";
            // 
            // LabelAvgSpatialSTTimesteps
            // 
            this.LabelAvgSpatialSTTimesteps.AutoSize = true;
            this.LabelAvgSpatialSTTimesteps.Location = new System.Drawing.Point(209, 24);
            this.LabelAvgSpatialSTTimesteps.Name = "LabelAvgSpatialSTTimesteps";
            this.LabelAvgSpatialSTTimesteps.Size = new System.Drawing.Size(51, 13);
            this.LabelAvgSpatialSTTimesteps.TabIndex = 2;
            this.LabelAvgSpatialSTTimesteps.Text = "timesteps";
            // 
            // TextBoxAvgSpatialFLTimesteps
            // 
            this.TextBoxAvgSpatialFLTimesteps.Location = new System.Drawing.Point(144, 45);
            this.TextBoxAvgSpatialFLTimesteps.Name = "TextBoxAvgSpatialFLTimesteps";
            this.TextBoxAvgSpatialFLTimesteps.Size = new System.Drawing.Size(56, 20);
            this.TextBoxAvgSpatialFLTimesteps.TabIndex = 5;
            this.TextBoxAvgSpatialFLTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TextBoxAvgSpatialSTTimesteps
            // 
            this.TextBoxAvgSpatialSTTimesteps.Location = new System.Drawing.Point(144, 22);
            this.TextBoxAvgSpatialSTTimesteps.Name = "TextBoxAvgSpatialSTTimesteps";
            this.TextBoxAvgSpatialSTTimesteps.Size = new System.Drawing.Size(56, 20);
            this.TextBoxAvgSpatialSTTimesteps.TabIndex = 1;
            this.TextBoxAvgSpatialSTTimesteps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // CheckBoxAvgSpatialFLCumulative
            // 
            this.CheckBoxAvgSpatialFLCumulative.AutoSize = true;
            this.CheckBoxAvgSpatialFLCumulative.Location = new System.Drawing.Point(290, 47);
            this.CheckBoxAvgSpatialFLCumulative.Name = "CheckBoxAvgSpatialFLCumulative";
            this.CheckBoxAvgSpatialFLCumulative.Size = new System.Drawing.Size(78, 17);
            this.CheckBoxAvgSpatialFLCumulative.TabIndex = 7;
            this.CheckBoxAvgSpatialFLCumulative.Text = "Cumulative";
            this.CheckBoxAvgSpatialFLCumulative.UseVisualStyleBackColor = true;
            // 
            // CheckBoxAvgSpatialFL
            // 
            this.CheckBoxAvgSpatialFL.AutoSize = true;
            this.CheckBoxAvgSpatialFL.Location = new System.Drawing.Point(15, 47);
            this.CheckBoxAvgSpatialFL.Name = "CheckBoxAvgSpatialFL";
            this.CheckBoxAvgSpatialFL.Size = new System.Drawing.Size(82, 17);
            this.CheckBoxAvgSpatialFL.TabIndex = 4;
            this.CheckBoxAvgSpatialFL.Text = "Flows every";
            this.CheckBoxAvgSpatialFL.UseVisualStyleBackColor = true;
            // 
            // CheckBoxAvgSpatialSTCumulative
            // 
            this.CheckBoxAvgSpatialSTCumulative.AutoSize = true;
            this.CheckBoxAvgSpatialSTCumulative.Location = new System.Drawing.Point(290, 24);
            this.CheckBoxAvgSpatialSTCumulative.Name = "CheckBoxAvgSpatialSTCumulative";
            this.CheckBoxAvgSpatialSTCumulative.Size = new System.Drawing.Size(78, 17);
            this.CheckBoxAvgSpatialSTCumulative.TabIndex = 3;
            this.CheckBoxAvgSpatialSTCumulative.Text = "Cumulative";
            this.CheckBoxAvgSpatialSTCumulative.UseVisualStyleBackColor = true;
            // 
            // CheckBoxAvgSpatialST
            // 
            this.CheckBoxAvgSpatialST.AutoSize = true;
            this.CheckBoxAvgSpatialST.Location = new System.Drawing.Point(15, 24);
            this.CheckBoxAvgSpatialST.Name = "CheckBoxAvgSpatialST";
            this.CheckBoxAvgSpatialST.Size = new System.Drawing.Size(88, 17);
            this.CheckBoxAvgSpatialST.TabIndex = 0;
            this.CheckBoxAvgSpatialST.Text = "Stocks every";
            this.CheckBoxAvgSpatialST.UseVisualStyleBackColor = true;
            // 
            // GroupBoxSummaryFL
            // 
            this.GroupBoxSummaryFL.Controls.Add(this.checkBox2);
            this.GroupBoxSummaryFL.Controls.Add(this.checkBox6);
            this.GroupBoxSummaryFL.Controls.Add(this.checkBox7);
            this.GroupBoxSummaryFL.Controls.Add(this.checkBox1);
            this.GroupBoxSummaryFL.Controls.Add(this.checkBox3);
            this.GroupBoxSummaryFL.Controls.Add(this.checkBox4);
            this.GroupBoxSummaryFL.Controls.Add(this.checkBox5);
            this.GroupBoxSummaryFL.Controls.Add(this.LabelSummaryFLTimesteps);
            this.GroupBoxSummaryFL.Controls.Add(this.CheckBoxSummaryFL);
            this.GroupBoxSummaryFL.Controls.Add(this.TextBoxSummaryFLTimesteps);
            this.GroupBoxSummaryFL.Location = new System.Drawing.Point(3, 124);
            this.GroupBoxSummaryFL.Name = "GroupBoxSummaryFL";
            this.GroupBoxSummaryFL.Size = new System.Drawing.Size(574, 124);
            this.GroupBoxSummaryFL.TabIndex = 1;
            this.GroupBoxSummaryFL.TabStop = false;
            this.GroupBoxSummaryFL.Text = "Tabular output for flows";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(435, 73);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(128, 17);
            this.checkBox2.TabIndex = 9;
            this.checkBox2.Text = "Omit secondary strata";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Location = new System.Drawing.Point(435, 50);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(128, 17);
            this.checkBox6.TabIndex = 8;
            this.checkBox6.Text = "Omit secondary strata";
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Location = new System.Drawing.Point(435, 27);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(128, 17);
            this.checkBox7.TabIndex = 7;
            this.checkBox7.Text = "Omit secondary strata";
            this.checkBox7.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(290, 95);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(128, 17);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "Omit secondary strata";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(290, 72);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(128, 17);
            this.checkBox3.TabIndex = 5;
            this.checkBox3.Text = "Omit secondary strata";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(290, 49);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(128, 17);
            this.checkBox4.TabIndex = 4;
            this.checkBox4.Text = "Omit secondary strata";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Location = new System.Drawing.Point(290, 26);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(128, 17);
            this.checkBox5.TabIndex = 3;
            this.checkBox5.Text = "Omit secondary strata";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // StockFlowOutputOptionsDataFeedView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GroupBoxSummaryFL);
            this.Controls.Add(this.GroupBoxAvgSpatial);
            this.Controls.Add(this.GroupBoxSpatial);
            this.Controls.Add(this.GroupBoxSummaryST);
            this.Name = "StockFlowOutputOptionsDataFeedView";
            this.Size = new System.Drawing.Size(580, 476);
            this.GroupBoxSpatial.ResumeLayout(false);
            this.GroupBoxSpatial.PerformLayout();
            this.GroupBoxSummaryST.ResumeLayout(false);
            this.GroupBoxSummaryST.PerformLayout();
            this.GroupBoxAvgSpatial.ResumeLayout(false);
            this.GroupBoxAvgSpatial.PerformLayout();
            this.GroupBoxSummaryFL.ResumeLayout(false);
            this.GroupBoxSummaryFL.PerformLayout();
            this.ResumeLayout(false);

		}
		internal System.Windows.Forms.Label LabelSpatialFLTimesteps;
		internal System.Windows.Forms.Label LabelSpatialSTTimesteps;
		internal System.Windows.Forms.TextBox TextBoxSpatialFLTimesteps;
		internal System.Windows.Forms.TextBox TextBoxSpatialSTTimesteps;
		internal System.Windows.Forms.CheckBox CheckBoxSpatialFL;
		internal System.Windows.Forms.CheckBox CheckBoxSpatialST;
		internal System.Windows.Forms.GroupBox GroupBoxSpatial;
		internal System.Windows.Forms.Label LabelSummaryFLTimesteps;
		internal System.Windows.Forms.Label LabelSummarySTTimesteps;
		internal System.Windows.Forms.TextBox TextBoxSummaryFLTimesteps;
		internal System.Windows.Forms.GroupBox GroupBoxSummaryST;
		internal System.Windows.Forms.TextBox TextBoxSummarySTTimesteps;
		internal System.Windows.Forms.CheckBox CheckBoxSummaryFL;
		internal System.Windows.Forms.CheckBox CheckBoxSummaryST;
        internal System.Windows.Forms.Label LabelLateralFLTimesteps;
        internal System.Windows.Forms.TextBox TextBoxLateralFLTimesteps;
        internal System.Windows.Forms.CheckBox CheckBoxLateralFL;
        private System.Windows.Forms.GroupBox GroupBoxAvgSpatial;
        internal System.Windows.Forms.Label LabelAvgSpatialFLTimesteps;
        internal System.Windows.Forms.Label LabelAvgSpatialSTTimesteps;
        internal System.Windows.Forms.TextBox TextBoxAvgSpatialFLTimesteps;
        internal System.Windows.Forms.TextBox TextBoxAvgSpatialSTTimesteps;
        internal System.Windows.Forms.CheckBox CheckBoxAvgSpatialFL;
        internal System.Windows.Forms.CheckBox CheckBoxAvgSpatialST;
        internal System.Windows.Forms.CheckBox CheckBoxAvgSpatialFLCumulative;
        internal System.Windows.Forms.CheckBox CheckBoxAvgSpatialSTCumulative;
        internal System.Windows.Forms.Label LabelAvgSpatialLFLTimesteps;
        internal System.Windows.Forms.TextBox TextBoxAvgSpatialLFLTimesteps;
        internal System.Windows.Forms.CheckBox CheckBoxAvgSpatialLFLCumulative;
        internal System.Windows.Forms.CheckBox CheckBoxAvgSpatialLFL;
        internal System.Windows.Forms.CheckBox CheckBoxSTOmitSC;
        internal System.Windows.Forms.CheckBox CheckBoxSTOmitTS;
        internal System.Windows.Forms.CheckBox CheckBoxSTOmitSS;
        internal System.Windows.Forms.GroupBox GroupBoxSummaryFL;
        internal System.Windows.Forms.CheckBox checkBox3;
        internal System.Windows.Forms.CheckBox checkBox4;
        internal System.Windows.Forms.CheckBox checkBox5;
        internal System.Windows.Forms.CheckBox checkBox2;
        internal System.Windows.Forms.CheckBox checkBox6;
        internal System.Windows.Forms.CheckBox checkBox7;
        internal System.Windows.Forms.CheckBox checkBox1;
    }
}