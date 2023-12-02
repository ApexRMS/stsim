using System;

namespace SyncroSim.STSim
{
    public partial class CheckBoxPanel : System.Windows.Forms.UserControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GridPanel = new SyncroSim.Core.Forms.BasePanel();
            this.CheckAllCheckbox = new System.Windows.Forms.CheckBox();
            this.ItemsDataGrid = new SyncroSim.Core.Forms.BaseDataGridView();
            this.IsSelectedColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ItemIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GridPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ItemsDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // GridPanel
            // 
            this.GridPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(197)))), ((int)(((byte)(216)))));
            this.GridPanel.Controls.Add(this.CheckAllCheckbox);
            this.GridPanel.Controls.Add(this.ItemsDataGrid);
            this.GridPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridPanel.Location = new System.Drawing.Point(0, 0);
            this.GridPanel.Margin = new System.Windows.Forms.Padding(4);
            this.GridPanel.Name = "GridPanel";
            this.GridPanel.Padding = new System.Windows.Forms.Padding(1);
            this.GridPanel.Size = new System.Drawing.Size(701, 440);
            this.GridPanel.TabIndex = 9;
            // 
            // CheckAllCheckbox
            // 
            this.CheckAllCheckbox.AutoSize = true;
            this.CheckAllCheckbox.Location = new System.Drawing.Point(12, 10);
            this.CheckAllCheckbox.Margin = new System.Windows.Forms.Padding(4);
            this.CheckAllCheckbox.Name = "CheckAllCheckbox";
            this.CheckAllCheckbox.Size = new System.Drawing.Size(18, 17);
            this.CheckAllCheckbox.TabIndex = 0;
            this.CheckAllCheckbox.UseVisualStyleBackColor = true;
            // 
            // ItemsDataGrid
            // 
            this.ItemsDataGrid.AllowUserToAddRows = false;
            this.ItemsDataGrid.AllowUserToDeleteRows = false;
            this.ItemsDataGrid.AllowUserToResizeColumns = false;
            this.ItemsDataGrid.AllowUserToResizeRows = false;
            this.ItemsDataGrid.ApplyFocusColors = true;
            this.ItemsDataGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ItemsDataGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ItemsDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ItemsDataGrid.ColumnHeadersHeight = 29;
            this.ItemsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ItemsDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IsSelectedColumn,
            this.ItemIdColumn,
            this.ItemNameColumn});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ItemsDataGrid.DefaultCellStyle = dataGridViewCellStyle5;
            this.ItemsDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ItemsDataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.ItemsDataGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.ItemsDataGrid.IsReadOnly = false;
            this.ItemsDataGrid.Location = new System.Drawing.Point(1, 1);
            this.ItemsDataGrid.Margin = new System.Windows.Forms.Padding(4);
            this.ItemsDataGrid.MultiSelect = false;
            this.ItemsDataGrid.Name = "ItemsDataGrid";
            this.ItemsDataGrid.RowHeadersVisible = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            this.ItemsDataGrid.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.ItemsDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ItemsDataGrid.Size = new System.Drawing.Size(699, 438);
            this.ItemsDataGrid.TabIndex = 7;
            // 
            // IsSelectedColumn
            // 
            this.IsSelectedColumn.DataPropertyName = "IsSelected";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.NullValue = false;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.IsSelectedColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.IsSelectedColumn.HeaderText = "";
            this.IsSelectedColumn.MinimumWidth = 30;
            this.IsSelectedColumn.Name = "IsSelectedColumn";
            this.IsSelectedColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.IsSelectedColumn.Width = 30;
            // 
            // ItemIdColumn
            // 
            this.ItemIdColumn.DataPropertyName = "ItemID";
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.ItemIdColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.ItemIdColumn.HeaderText = "Item ID";
            this.ItemIdColumn.Name = "ItemIdColumn";
            this.ItemIdColumn.Visible = false;
            // 
            // ItemNameColumn
            // 
            this.ItemNameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ItemNameColumn.DataPropertyName = "ItemName";
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.ItemNameColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.ItemNameColumn.HeaderText = "Item Name";
            this.ItemNameColumn.Name = "ItemNameColumn";
            this.ItemNameColumn.ReadOnly = true;
            // 
            // CheckBoxPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GridPanel);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CheckBoxPanel";
            this.Size = new System.Drawing.Size(701, 440);
            this.GridPanel.ResumeLayout(false);
            this.GridPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ItemsDataGrid)).EndInit();
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.CheckBox CheckAllCheckbox;
        private SyncroSim.Core.Forms.BasePanel GridPanel;
        private SyncroSim.Core.Forms.BaseDataGridView ItemsDataGrid;
        internal System.Windows.Forms.DataGridViewCheckBoxColumn IsSelectedColumn;
        internal System.Windows.Forms.DataGridViewTextBoxColumn ItemIdColumn;
        internal System.Windows.Forms.DataGridViewTextBoxColumn ItemNameColumn;
    }
}

