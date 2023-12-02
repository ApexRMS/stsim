using System;

namespace SyncroSim.STSim
{
    internal partial class PasteStrataForm : System.Windows.Forms.Form
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
            System.Windows.Forms.DataGridViewCellStyle DataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle DataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle DataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle DataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle DataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.PanelGrid = new SyncroSim.Core.Forms.BasePanel();
            this.DataGridViewStrata = new SyncroSim.Core.Forms.BaseDataGridView();
            this.ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.ButtonOK = new System.Windows.Forms.Button();
            this.CheckBoxMergeDeps = new System.Windows.Forms.CheckBox();
            this.ButtonSelectAllStrata = new System.Windows.Forms.Button();
            this.PanelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.DataGridViewStrata).BeginInit();
            this.SuspendLayout();
            //
            //PanelGrid
            //
            this.PanelGrid.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right);
            this.PanelGrid.BorderColor = System.Drawing.Color.Gray;
            this.PanelGrid.Controls.Add(this.DataGridViewStrata);
            this.PanelGrid.Location = new System.Drawing.Point(11, 12);
            this.PanelGrid.Name = "PanelGrid";
            this.PanelGrid.Padding = new System.Windows.Forms.Padding(1);
            this.PanelGrid.Size = new System.Drawing.Size(584, 359);
            this.PanelGrid.TabIndex = 4;
            //
            //DataGridViewStrata
            //
            this.DataGridViewStrata.AllowUserToAddRows = false;
            this.DataGridViewStrata.AllowUserToDeleteRows = false;
            this.DataGridViewStrata.AllowUserToResizeRows = false;
            this.DataGridViewStrata.BorderStyle = System.Windows.Forms.BorderStyle.None;
            DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            DataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
            DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridViewStrata.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1;
            this.DataGridViewStrata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridViewStrata.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {this.ColumnName, this.ColumnDescription});
            DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            DataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
            DataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(232)), Convert.ToInt32(Convert.ToByte(232)), Convert.ToInt32(Convert.ToByte(232)));
            DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridViewStrata.DefaultCellStyle = DataGridViewCellStyle4;
            this.DataGridViewStrata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridViewStrata.IsReadOnly = false;
            this.DataGridViewStrata.Location = new System.Drawing.Point(1, 1);
            this.DataGridViewStrata.MultiSelect = false;
            this.DataGridViewStrata.Name = "DataGridViewStrata";
            this.DataGridViewStrata.RowHeadersVisible = false;
            DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Convert.ToByte(232)), Convert.ToInt32(Convert.ToByte(232)), Convert.ToInt32(Convert.ToByte(232)));
            this.DataGridViewStrata.RowsDefaultCellStyle = DataGridViewCellStyle5;
            this.DataGridViewStrata.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridViewStrata.Size = new System.Drawing.Size(582, 357);
            this.DataGridViewStrata.TabIndex = 0;
            //
            //ColumnName
            //
            this.ColumnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.ColumnName.DefaultCellStyle = DataGridViewCellStyle2;
            this.ColumnName.FillWeight = 50.0F;
            this.ColumnName.HeaderText = "Name";
            this.ColumnName.Name = "ColumnName";
            this.ColumnName.ReadOnly = true;
            //
            //ColumnDescription
            //
            this.ColumnDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.ColumnDescription.DefaultCellStyle = DataGridViewCellStyle3;
            this.ColumnDescription.FillWeight = 50.0F;
            this.ColumnDescription.HeaderText = "Description";
            this.ColumnDescription.Name = "ColumnDescription";
            this.ColumnDescription.ReadOnly = true;
            //
            //ButtonCancel
            //
            this.ButtonCancel.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Location = new System.Drawing.Point(520, 379);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 3;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            //
            //ButtonOK
            //
            this.ButtonOK.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
            this.ButtonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonOK.Location = new System.Drawing.Point(439, 379);
            this.ButtonOK.Name = "ButtonOK";
            this.ButtonOK.Size = new System.Drawing.Size(75, 23);
            this.ButtonOK.TabIndex = 2;
            this.ButtonOK.Text = "OK";
            this.ButtonOK.UseVisualStyleBackColor = true;
            //
            //CheckBoxMergeDeps
            //
            this.CheckBoxMergeDeps.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.CheckBoxMergeDeps.AutoSize = true;
            this.CheckBoxMergeDeps.Location = new System.Drawing.Point(95, 383);
            this.CheckBoxMergeDeps.Name = "CheckBoxMergeDeps";
            this.CheckBoxMergeDeps.Size = new System.Drawing.Size(128, 17);
            this.CheckBoxMergeDeps.TabIndex = 1;
            this.CheckBoxMergeDeps.Text = "Merge Dependencies";
            this.CheckBoxMergeDeps.UseVisualStyleBackColor = true;
            //
            //ButtonSelectAllStrata
            //
            this.ButtonSelectAllStrata.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
            this.ButtonSelectAllStrata.Location = new System.Drawing.Point(11, 379);
            this.ButtonSelectAllStrata.Name = "ButtonSelectAllStrata";
            this.ButtonSelectAllStrata.Size = new System.Drawing.Size(75, 23);
            this.ButtonSelectAllStrata.TabIndex = 5;
            this.ButtonSelectAllStrata.Text = "Select All";
            this.ButtonSelectAllStrata.UseVisualStyleBackColor = true;
            //
            //PasteStrataForm
            //
            this.AcceptButton = this.ButtonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6.0F, 13.0F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ButtonCancel;
            this.ClientSize = new System.Drawing.Size(607, 409);
            this.Controls.Add(this.ButtonSelectAllStrata);
            this.Controls.Add(this.CheckBoxMergeDeps);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonOK);
            this.Controls.Add(this.PanelGrid);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PasteStrataForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Paste Strata";
            this.PanelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.DataGridViewStrata).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        	ButtonSelectAllStrata.Click += new System.EventHandler(ButtonSelectAllStrata_Click);
        	ButtonOK.Click += new System.EventHandler(ButtonOK_Click);
        	base.Shown += new System.EventHandler(PasteStrataForm_Shown);
        	DataGridViewStrata.KeyDown += new System.Windows.Forms.KeyEventHandler(DataGridViewStrata_KeyDown);
        }
        internal SyncroSim.Core.Forms.BasePanel PanelGrid;
        internal SyncroSim.Core.Forms.BaseDataGridView DataGridViewStrata;
        internal System.Windows.Forms.DataGridViewTextBoxColumn ColumnName;
        internal System.Windows.Forms.DataGridViewTextBoxColumn ColumnDescription;
        internal System.Windows.Forms.Button ButtonCancel;
        internal System.Windows.Forms.Button ButtonOK;
        internal System.Windows.Forms.CheckBox CheckBoxMergeDeps;
        internal System.Windows.Forms.Button ButtonSelectAllStrata;
    }
}
