namespace SyncroSim.STSim
{
	internal partial class FilterFlowTypesForm : System.Windows.Forms.Form
	{
		//Form overrides dispose to clean up the component list.
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
			this.CheckBoxPanelMain = new STSim.CheckBoxPanel();
			this.ButtonCancel = new System.Windows.Forms.Button();
			this.ButtonOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			//
			//CheckBoxPanelMain
			//
			this.CheckBoxPanelMain.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.CheckBoxPanelMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.CheckBoxPanelMain.IsReadOnly = false;
			this.CheckBoxPanelMain.Location = new System.Drawing.Point(12, 12);
			this.CheckBoxPanelMain.Name = "CheckBoxPanelMain";
			this.CheckBoxPanelMain.Size = new System.Drawing.Size(533, 330);
			this.CheckBoxPanelMain.TabIndex = 0;
			this.CheckBoxPanelMain.TitleBarText = "Item Names";
			//
			//ButtonCancel
			//
			this.ButtonCancel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.ButtonCancel.Location = new System.Drawing.Point(471, 348);
			this.ButtonCancel.Name = "ButtonCancel";
			this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
			this.ButtonCancel.TabIndex = 2;
			this.ButtonCancel.Text = "Cancel";
			this.ButtonCancel.UseVisualStyleBackColor = true;
			//
			//ButtonOK
			//
			this.ButtonOK.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.ButtonOK.Location = new System.Drawing.Point(390, 348);
			this.ButtonOK.Name = "ButtonOK";
			this.ButtonOK.Size = new System.Drawing.Size(75, 23);
			this.ButtonOK.TabIndex = 1;
			this.ButtonOK.Text = "OK";
			this.ButtonOK.UseVisualStyleBackColor = true;
			//
			//FilterFlowTypesForm
			//
			this.AcceptButton = this.ButtonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.ButtonCancel;
			this.ClientSize = new System.Drawing.Size(557, 379);
			this.Controls.Add(this.CheckBoxPanelMain);
			this.Controls.Add(this.ButtonCancel);
			this.Controls.Add(this.ButtonOK);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FilterFlowTypesForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Filter Flow Types";
			this.ResumeLayout(false);

			ButtonOK.Click += new System.EventHandler(ButtonOK_Click);
			ButtonCancel.Click += new System.EventHandler(ButtonCancel_Click);
		}
		internal STSim.CheckBoxPanel CheckBoxPanelMain;
		internal System.Windows.Forms.Button ButtonCancel;
		internal System.Windows.Forms.Button ButtonOK;
	}
}