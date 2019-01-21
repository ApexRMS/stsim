namespace SyncroSim.STSim
{
    internal partial class ChooseStateClassForm : System.Windows.Forms.Form
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
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.ButtonOK = new System.Windows.Forms.Button();
            this.LabelStateLabelX = new System.Windows.Forms.Label();
            this.LabelStateLabelY = new System.Windows.Forms.Label();
            this.ComboBoxStateLabelX = new System.Windows.Forms.ComboBox();
            this.ComboBoxStateLabelY = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            //
            //ButtonCancel
            //
            this.ButtonCancel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Location = new System.Drawing.Point(314, 120);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 5;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            //
            //ButtonOK
            //
            this.ButtonOK.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.ButtonOK.Location = new System.Drawing.Point(233, 120);
            this.ButtonOK.Name = "ButtonOK";
            this.ButtonOK.Size = new System.Drawing.Size(75, 23);
            this.ButtonOK.TabIndex = 4;
            this.ButtonOK.Text = "OK";
            this.ButtonOK.UseVisualStyleBackColor = true;
            //
            //LabelStateLabelX
            //
            this.LabelStateLabelX.AutoSize = true;
            this.LabelStateLabelX.Location = new System.Drawing.Point(10, 9);
            this.LabelStateLabelX.Name = "LabelStateLabelX";
            this.LabelStateLabelX.Size = new System.Drawing.Size(74, 13);
            this.LabelStateLabelX.TabIndex = 0;
            this.LabelStateLabelX.Text = "State Label X:";
            //
            //LabelStateLabelY
            //
            this.LabelStateLabelY.AutoSize = true;
            this.LabelStateLabelY.Location = new System.Drawing.Point(10, 56);
            this.LabelStateLabelY.Name = "LabelStateLabelY";
            this.LabelStateLabelY.Size = new System.Drawing.Size(74, 13);
            this.LabelStateLabelY.TabIndex = 2;
            this.LabelStateLabelY.Text = "State Label Y:";
            //
            //ComboBoxStateLabelX
            //
            this.ComboBoxStateLabelX.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.ComboBoxStateLabelX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxStateLabelX.FormattingEnabled = true;
            this.ComboBoxStateLabelX.Location = new System.Drawing.Point(10, 28);
            this.ComboBoxStateLabelX.Name = "ComboBoxStateLabelX";
            this.ComboBoxStateLabelX.Size = new System.Drawing.Size(381, 21);
            this.ComboBoxStateLabelX.TabIndex = 1;
            //
            //ComboBoxStateLabelY
            //
            this.ComboBoxStateLabelY.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.ComboBoxStateLabelY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxStateLabelY.FormattingEnabled = true;
            this.ComboBoxStateLabelY.Location = new System.Drawing.Point(10, 75);
            this.ComboBoxStateLabelY.Name = "ComboBoxStateLabelY";
            this.ComboBoxStateLabelY.Size = new System.Drawing.Size(381, 21);
            this.ComboBoxStateLabelY.TabIndex = 3;
            //
            //ChooseStateClassForm
            //
            this.AcceptButton = this.ButtonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ButtonCancel;
            this.ClientSize = new System.Drawing.Size(401, 149);
            this.Controls.Add(this.ComboBoxStateLabelY);
            this.Controls.Add(this.ComboBoxStateLabelX);
            this.Controls.Add(this.LabelStateLabelY);
            this.Controls.Add(this.LabelStateLabelX);
            this.Controls.Add(this.ButtonOK);
            this.Controls.Add(this.ButtonCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChooseStateClassForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Title";
            this.ResumeLayout(false);
            this.PerformLayout();

        	ButtonOK.Click += new System.EventHandler(ButtonOK_Click);
        	ButtonCancel.Click += new System.EventHandler(ButtonCancel_Click);
        }
        internal System.Windows.Forms.Button ButtonCancel;
        internal System.Windows.Forms.Button ButtonOK;
        internal System.Windows.Forms.Label LabelStateLabelX;
        internal System.Windows.Forms.Label LabelStateLabelY;
        internal System.Windows.Forms.ComboBox ComboBoxStateLabelX;
        internal System.Windows.Forms.ComboBox ComboBoxStateLabelY;
    }
}
