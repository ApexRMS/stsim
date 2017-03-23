<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PasteStrataForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.PanelGrid = New SyncroSim.Core.Forms.BasePanel()
        Me.DataGridViewStrata = New SyncroSim.Core.Forms.BaseDataGridView()
        Me.ColumnName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColumnDescription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.CheckBoxMergeDeps = New System.Windows.Forms.CheckBox()
        Me.ButtonSelectAllStrata = New System.Windows.Forms.Button()
        Me.PanelGrid.SuspendLayout()
        CType(Me.DataGridViewStrata, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelGrid
        '
        Me.PanelGrid.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelGrid.BorderColor = System.Drawing.Color.Gray
        Me.PanelGrid.Controls.Add(Me.DataGridViewStrata)
        Me.PanelGrid.Location = New System.Drawing.Point(11, 12)
        Me.PanelGrid.Name = "PanelGrid"
        Me.PanelGrid.Padding = New System.Windows.Forms.Padding(1)
        Me.PanelGrid.ShowBorder = True
        Me.PanelGrid.Size = New System.Drawing.Size(584, 359)
        Me.PanelGrid.TabIndex = 4
        '
        'DataGridViewStrata
        '
        Me.DataGridViewStrata.AllowUserToAddRows = False
        Me.DataGridViewStrata.AllowUserToDeleteRows = False
        Me.DataGridViewStrata.AllowUserToResizeRows = False
        Me.DataGridViewStrata.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewStrata.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridViewStrata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewStrata.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColumnName, Me.ColumnDescription})
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewStrata.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridViewStrata.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridViewStrata.EnableMouseClicks = True
        Me.DataGridViewStrata.IsReadOnly = False
        Me.DataGridViewStrata.Location = New System.Drawing.Point(1, 1)
        Me.DataGridViewStrata.MultiSelect = False
        Me.DataGridViewStrata.Name = "DataGridViewStrata"
        Me.DataGridViewStrata.PaintGridBorders = True
        Me.DataGridViewStrata.PaintSelectionRectangle = True
        Me.DataGridViewStrata.RowHeadersVisible = False
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.DataGridViewStrata.RowsDefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridViewStrata.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridViewStrata.Size = New System.Drawing.Size(582, 357)
        Me.DataGridViewStrata.TabIndex = 0
        '
        'ColumnName
        '
        Me.ColumnName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black
        Me.ColumnName.DefaultCellStyle = DataGridViewCellStyle2
        Me.ColumnName.FillWeight = 50.0!
        Me.ColumnName.HeaderText = "Name"
        Me.ColumnName.Name = "ColumnName"
        Me.ColumnName.ReadOnly = True
        '
        'ColumnDescription
        '
        Me.ColumnDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black
        Me.ColumnDescription.DefaultCellStyle = DataGridViewCellStyle3
        Me.ColumnDescription.FillWeight = 50.0!
        Me.ColumnDescription.HeaderText = "Description"
        Me.ColumnDescription.Name = "ColumnDescription"
        Me.ColumnDescription.ReadOnly = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonCancel.Location = New System.Drawing.Point(520, 379)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 23)
        Me.ButtonCancel.TabIndex = 3
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonOK.Location = New System.Drawing.Point(439, 379)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(75, 23)
        Me.ButtonOK.TabIndex = 2
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'CheckBoxMergeDeps
        '
        Me.CheckBoxMergeDeps.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxMergeDeps.AutoSize = True
        Me.CheckBoxMergeDeps.Location = New System.Drawing.Point(95, 383)
        Me.CheckBoxMergeDeps.Name = "CheckBoxMergeDeps"
        Me.CheckBoxMergeDeps.Size = New System.Drawing.Size(128, 17)
        Me.CheckBoxMergeDeps.TabIndex = 1
        Me.CheckBoxMergeDeps.Text = "Merge Dependencies"
        Me.CheckBoxMergeDeps.UseVisualStyleBackColor = True
        '
        'ButtonSelectAllStrata
        '
        Me.ButtonSelectAllStrata.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ButtonSelectAllStrata.Location = New System.Drawing.Point(11, 379)
        Me.ButtonSelectAllStrata.Name = "ButtonSelectAllStrata"
        Me.ButtonSelectAllStrata.Size = New System.Drawing.Size(75, 23)
        Me.ButtonSelectAllStrata.TabIndex = 5
        Me.ButtonSelectAllStrata.Text = "Select All"
        Me.ButtonSelectAllStrata.UseVisualStyleBackColor = True
        '
        'PasteStrataForm
        '
        Me.AcceptButton = Me.ButtonOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ButtonCancel
        Me.ClientSize = New System.Drawing.Size(607, 409)
        Me.Controls.Add(Me.ButtonSelectAllStrata)
        Me.Controls.Add(Me.CheckBoxMergeDeps)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOK)
        Me.Controls.Add(Me.PanelGrid)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "PasteStrataForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Paste Strata"
        Me.PanelGrid.ResumeLayout(False)
        CType(Me.DataGridViewStrata, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PanelGrid As SyncroSim.Core.Forms.BasePanel
    Friend WithEvents DataGridViewStrata As SyncroSim.Core.Forms.BaseDataGridView
    Friend WithEvents ColumnName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColumnDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOK As System.Windows.Forms.Button
    Friend WithEvents CheckBoxMergeDeps As System.Windows.Forms.CheckBox
    Friend WithEvents ButtonSelectAllStrata As System.Windows.Forms.Button
End Class




