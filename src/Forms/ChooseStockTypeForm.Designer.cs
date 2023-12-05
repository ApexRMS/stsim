namespace SyncroSim.STSim
{
	internal partial class ChooseStockTypeForm : System.Windows.Forms.Form
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
			this.ButtonOK = new System.Windows.Forms.Button();
			this.ButtonCancel = new System.Windows.Forms.Button();
			this.LabelStocks = new System.Windows.Forms.Label();
			this.ComboBoxStocks = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			//
			//ButtonOK
			//
			this.ButtonOK.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.ButtonOK.Location = new System.Drawing.Point(223, 60);
			this.ButtonOK.Name = "ButtonOK";
			this.ButtonOK.Size = new System.Drawing.Size(75, 23);
			this.ButtonOK.TabIndex = 2;
			this.ButtonOK.Text = "OK";
			this.ButtonOK.UseVisualStyleBackColor = true;
			//
			//ButtonCancel
			//
			this.ButtonCancel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.ButtonCancel.Location = new System.Drawing.Point(304, 60);
			this.ButtonCancel.Name = "ButtonCancel";
			this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
			this.ButtonCancel.TabIndex = 3;
			this.ButtonCancel.Text = "Cancel";
			this.ButtonCancel.UseVisualStyleBackColor = true;
			//
			//LabelStocks
			//
			this.LabelStocks.AutoSize = true;
			this.LabelStocks.Location = new System.Drawing.Point(10, 9);
			this.LabelStocks.Name = "LabelStocks";
			this.LabelStocks.Size = new System.Drawing.Size(115, 13);
			this.LabelStocks.TabIndex = 0;
			this.LabelStocks.Text = "Available stocks types:";
			//
			//ComboBoxStocks
			//
			this.ComboBoxStocks.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
			this.ComboBoxStocks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ComboBoxStocks.FormattingEnabled = true;
			this.ComboBoxStocks.Location = new System.Drawing.Point(13, 30);
			this.ComboBoxStocks.Name = "ComboBoxStocks";
			this.ComboBoxStocks.Size = new System.Drawing.Size(366, 21);
			this.ComboBoxStocks.TabIndex = 1;
			//
			//ChooseStockTypeForm
			//
			this.AcceptButton = this.ButtonOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.ButtonCancel;
			this.ClientSize = new System.Drawing.Size(391, 91);
			this.Controls.Add(this.ComboBoxStocks);
			this.Controls.Add(this.LabelStocks);
			this.Controls.Add(this.ButtonCancel);
			this.Controls.Add(this.ButtonOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ChooseStockTypeForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Add Stock";
			this.ResumeLayout(false);
			this.PerformLayout();

			ButtonOK.Click += new System.EventHandler(ButtonOK_Click);
			ButtonCancel.Click += new System.EventHandler(ButtonCancel_Click);
		}
		internal System.Windows.Forms.Button ButtonOK;
		internal System.Windows.Forms.Button ButtonCancel;
		internal System.Windows.Forms.Label LabelStocks;
		internal System.Windows.Forms.ComboBox ComboBoxStocks;
	}
}