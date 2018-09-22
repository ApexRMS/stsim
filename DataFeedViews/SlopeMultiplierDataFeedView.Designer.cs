namespace SyncroSim.STSim
{
    internal partial class SlopeMultiplierDataFeedView : SyncroSim.Core.Forms.DataFeedView
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
            this.ButtonClear = new System.Windows.Forms.Button();
            this.ButtonBrowse = new System.Windows.Forms.Button();
            this.TextBoxDEMFilename = new System.Windows.Forms.TextBox();
            this.LabelTMV = new System.Windows.Forms.Label();
            this.LabelDEM = new System.Windows.Forms.Label();
            this.PanelOptions = new System.Windows.Forms.Panel();
            this.PanelMultipliers = new System.Windows.Forms.Panel();
            this.PanelOptions.SuspendLayout();
            this.SuspendLayout();
            //
            //ButtonClear
            //
            this.ButtonClear.Location = new System.Drawing.Point(291, 27);
            this.ButtonClear.Name = "ButtonClear";
            this.ButtonClear.Size = new System.Drawing.Size(75, 23);
            this.ButtonClear.TabIndex = 3;
            this.ButtonClear.Text = "Clear";
            this.ButtonClear.UseVisualStyleBackColor = true;
            //
            //ButtonBrowse
            //
            this.ButtonBrowse.Location = new System.Drawing.Point(210, 27);
            this.ButtonBrowse.Name = "ButtonBrowse";
            this.ButtonBrowse.Size = new System.Drawing.Size(75, 23);
            this.ButtonBrowse.TabIndex = 2;
            this.ButtonBrowse.Text = "Browse...";
            this.ButtonBrowse.UseVisualStyleBackColor = true;
            //
            //TextBoxDEMFilename
            //
            this.TextBoxDEMFilename.Location = new System.Drawing.Point(3, 29);
            this.TextBoxDEMFilename.Name = "TextBoxDEMFilename";
            this.TextBoxDEMFilename.ReadOnly = true;
            this.TextBoxDEMFilename.Size = new System.Drawing.Size(200, 20);
            this.TextBoxDEMFilename.TabIndex = 1;
            //
            //LabelTMV
            //
            this.LabelTMV.AutoSize = true;
            this.LabelTMV.Location = new System.Drawing.Point(0, 69);
            this.LabelTMV.Name = "LabelTMV";
            this.LabelTMV.Size = new System.Drawing.Size(161, 13);
            this.LabelTMV.TabIndex = 4;
            this.LabelTMV.Text = "Transition slope multiplier values:";
            //
            //LabelDEM
            //
            this.LabelDEM.AutoSize = true;
            this.LabelDEM.Location = new System.Drawing.Point(1, 7);
            this.LabelDEM.Name = "LabelDEM";
            this.LabelDEM.Size = new System.Drawing.Size(161, 13);
            this.LabelDEM.TabIndex = 0;
            this.LabelDEM.Text = "Digital elevation model file name:";
            //
            //PanelOptions
            //
            this.PanelOptions.Controls.Add(this.LabelDEM);
            this.PanelOptions.Controls.Add(this.ButtonClear);
            this.PanelOptions.Controls.Add(this.LabelTMV);
            this.PanelOptions.Controls.Add(this.ButtonBrowse);
            this.PanelOptions.Controls.Add(this.TextBoxDEMFilename);
            this.PanelOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelOptions.Location = new System.Drawing.Point(0, 0);
            this.PanelOptions.Name = "PanelOptions";
            this.PanelOptions.Size = new System.Drawing.Size(557, 94);
            this.PanelOptions.TabIndex = 0;
            //
            //PanelMultipliers
            //
            this.PanelMultipliers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMultipliers.Location = new System.Drawing.Point(0, 94);
            this.PanelMultipliers.Name = "PanelMultipliers";
            this.PanelMultipliers.Size = new System.Drawing.Size(557, 235);
            this.PanelMultipliers.TabIndex = 1;
            //
            //SlopeMultiplierDataFeedView
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.PanelMultipliers);
            this.Controls.Add(this.PanelOptions);
            this.Name = "SlopeMultiplierDataFeedView";
            this.Size = new System.Drawing.Size(557, 329);
            this.PanelOptions.ResumeLayout(false);
            this.PanelOptions.PerformLayout();
            this.ResumeLayout(false);

        	ButtonBrowse.Click += new System.EventHandler(ButtonBrowse_Click);
        	ButtonClear.Click += new System.EventHandler(ButtonClear_Click);
        }
        internal System.Windows.Forms.Button ButtonClear;
        internal System.Windows.Forms.Button ButtonBrowse;
        internal System.Windows.Forms.TextBox TextBoxDEMFilename;
        internal System.Windows.Forms.Label LabelTMV;
        internal System.Windows.Forms.Label LabelDEM;
        internal System.Windows.Forms.Panel PanelOptions;
        internal System.Windows.Forms.Panel PanelMultipliers;
    }
}
