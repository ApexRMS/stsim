namespace SyncroSim.STSim
{
    internal partial class TransitionDiagramPasteSpecialForm : System.Windows.Forms.Form
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
            this.RadioButtonPasteAll = new System.Windows.Forms.RadioButton();
            this.RadioButtonPasteNone = new System.Windows.Forms.RadioButton();
            this.RadioButtonPasteBetween = new System.Windows.Forms.RadioButton();
            this.CheckboxPasteDeterministic = new System.Windows.Forms.CheckBox();
            this.CheckboxPasteProbabilistic = new System.Windows.Forms.CheckBox();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            //ButtonOK
            //
            this.ButtonOK.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.ButtonOK.Location = new System.Drawing.Point(39, 202);
            this.ButtonOK.Name = "ButtonOK";
            this.ButtonOK.Size = new System.Drawing.Size(75, 23);
            this.ButtonOK.TabIndex = 1;
            this.ButtonOK.Text = "OK";
            this.ButtonOK.UseVisualStyleBackColor = true;
            //
            //ButtonCancel
            //
            this.ButtonCancel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Location = new System.Drawing.Point(120, 202);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 2;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            //
            //RadioButtonPasteAll
            //
            this.RadioButtonPasteAll.AutoSize = true;
            this.RadioButtonPasteAll.Checked = true;
            this.RadioButtonPasteAll.Location = new System.Drawing.Point(26, 30);
            this.RadioButtonPasteAll.Name = "RadioButtonPasteAll";
            this.RadioButtonPasteAll.Size = new System.Drawing.Size(36, 17);
            this.RadioButtonPasteAll.TabIndex = 0;
            this.RadioButtonPasteAll.TabStop = true;
            this.RadioButtonPasteAll.Text = "All";
            this.RadioButtonPasteAll.UseVisualStyleBackColor = true;
            //
            //RadioButtonPasteNone
            //
            this.RadioButtonPasteNone.AutoSize = true;
            this.RadioButtonPasteNone.Location = new System.Drawing.Point(26, 76);
            this.RadioButtonPasteNone.Name = "RadioButtonPasteNone";
            this.RadioButtonPasteNone.Size = new System.Drawing.Size(51, 17);
            this.RadioButtonPasteNone.TabIndex = 2;
            this.RadioButtonPasteNone.Text = "None";
            this.RadioButtonPasteNone.UseVisualStyleBackColor = true;
            //
            //RadioButtonPasteBetween
            //
            this.RadioButtonPasteBetween.AutoSize = true;
            this.RadioButtonPasteBetween.Location = new System.Drawing.Point(26, 53);
            this.RadioButtonPasteBetween.Name = "RadioButtonPasteBetween";
            this.RadioButtonPasteBetween.Size = new System.Drawing.Size(148, 17);
            this.RadioButtonPasteBetween.TabIndex = 1;
            this.RadioButtonPasteBetween.Text = "Between selected classes";
            this.RadioButtonPasteBetween.UseVisualStyleBackColor = true;
            //
            //CheckboxPasteDeterministic
            //
            this.CheckboxPasteDeterministic.AutoSize = true;
            this.CheckboxPasteDeterministic.Checked = true;
            this.CheckboxPasteDeterministic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckboxPasteDeterministic.Location = new System.Drawing.Point(26, 114);
            this.CheckboxPasteDeterministic.Name = "CheckboxPasteDeterministic";
            this.CheckboxPasteDeterministic.Size = new System.Drawing.Size(136, 17);
            this.CheckboxPasteDeterministic.TabIndex = 3;
            this.CheckboxPasteDeterministic.Text = "Deterministic transitions";
            this.CheckboxPasteDeterministic.UseVisualStyleBackColor = true;
            //
            //CheckboxPasteProbabilistic
            //
            this.CheckboxPasteProbabilistic.AutoSize = true;
            this.CheckboxPasteProbabilistic.Checked = true;
            this.CheckboxPasteProbabilistic.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CheckboxPasteProbabilistic.Location = new System.Drawing.Point(26, 137);
            this.CheckboxPasteProbabilistic.Name = "CheckboxPasteProbabilistic";
            this.CheckboxPasteProbabilistic.Size = new System.Drawing.Size(132, 17);
            this.CheckboxPasteProbabilistic.TabIndex = 4;
            this.CheckboxPasteProbabilistic.Text = "Probabilistic transitions";
            this.CheckboxPasteProbabilistic.UseVisualStyleBackColor = true;
            //
            //GroupBox1
            //
            this.GroupBox1.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.GroupBox1.Controls.Add(this.RadioButtonPasteBetween);
            this.GroupBox1.Controls.Add(this.RadioButtonPasteAll);
            this.GroupBox1.Controls.Add(this.CheckboxPasteDeterministic);
            this.GroupBox1.Controls.Add(this.CheckboxPasteProbabilistic);
            this.GroupBox1.Controls.Add(this.RadioButtonPasteNone);
            this.GroupBox1.Location = new System.Drawing.Point(12, 12);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(219, 173);
            this.GroupBox1.TabIndex = 0;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Transitions Included";
            //
            //TransitionDiagramPasteSpecialForm
            //
            this.AcceptButton = this.ButtonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ButtonCancel;
            this.ClientSize = new System.Drawing.Size(243, 240);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TransitionDiagramPasteSpecialForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Paste Special";
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.ResumeLayout(false);

        	ButtonOK.Click += new System.EventHandler(ButtonOK_Click);
        	ButtonCancel.Click += new System.EventHandler(ButtonCancel_Click);
        	RadioButtonPasteNone.CheckedChanged += new System.EventHandler(PasteNoneRadioButton_CheckedChanged);
        	RadioButtonPasteBetween.CheckedChanged += new System.EventHandler(PasteBetweenRadioButton_CheckedChanged);
        	RadioButtonPasteAll.CheckedChanged += new System.EventHandler(PasteAllRadioButton_CheckedChanged);
        	CheckboxPasteDeterministic.CheckedChanged += new System.EventHandler(PasteDeterministicCheckbox_CheckedChanged);
        	CheckboxPasteProbabilistic.CheckedChanged += new System.EventHandler(PasteProbabilisticCheckbox_CheckedChanged);
        }
        internal System.Windows.Forms.Button ButtonOK;
        internal System.Windows.Forms.Button ButtonCancel;
        internal System.Windows.Forms.RadioButton RadioButtonPasteAll;
        internal System.Windows.Forms.RadioButton RadioButtonPasteBetween;
        internal System.Windows.Forms.RadioButton RadioButtonPasteNone;
        internal System.Windows.Forms.CheckBox CheckboxPasteDeterministic;
        internal System.Windows.Forms.CheckBox CheckboxPasteProbabilistic;
        internal System.Windows.Forms.GroupBox GroupBox1;
    }
}
