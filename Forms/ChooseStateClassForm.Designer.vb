<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ChooseStateClassForm
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
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.LabelStateLabelX = New System.Windows.Forms.Label()
        Me.LabelStateLabelY = New System.Windows.Forms.Label()
        Me.ComboBoxStateLabelX = New System.Windows.Forms.ComboBox()
        Me.ComboBoxStateLabelY = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonCancel.Location = New System.Drawing.Point(314, 120)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 23)
        Me.ButtonCancel.TabIndex = 5
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOK.Location = New System.Drawing.Point(233, 120)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(75, 23)
        Me.ButtonOK.TabIndex = 4
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'LabelStateLabelX
        '
        Me.LabelStateLabelX.AutoSize = True
        Me.LabelStateLabelX.Location = New System.Drawing.Point(10, 9)
        Me.LabelStateLabelX.Name = "LabelStateLabelX"
        Me.LabelStateLabelX.Size = New System.Drawing.Size(74, 13)
        Me.LabelStateLabelX.TabIndex = 0
        Me.LabelStateLabelX.Text = "State Label X:"
        '
        'LabelStateLabelY
        '
        Me.LabelStateLabelY.AutoSize = True
        Me.LabelStateLabelY.Location = New System.Drawing.Point(10, 56)
        Me.LabelStateLabelY.Name = "LabelStateLabelY"
        Me.LabelStateLabelY.Size = New System.Drawing.Size(74, 13)
        Me.LabelStateLabelY.TabIndex = 2
        Me.LabelStateLabelY.Text = "State Label Y:"
        '
        'ComboBoxStateLabelX
        '
        Me.ComboBoxStateLabelX.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxStateLabelX.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxStateLabelX.FormattingEnabled = True
        Me.ComboBoxStateLabelX.Location = New System.Drawing.Point(10, 28)
        Me.ComboBoxStateLabelX.Name = "ComboBoxStateLabelX"
        Me.ComboBoxStateLabelX.Size = New System.Drawing.Size(381, 21)
        Me.ComboBoxStateLabelX.TabIndex = 1
        '
        'ComboBoxStateLabelY
        '
        Me.ComboBoxStateLabelY.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBoxStateLabelY.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxStateLabelY.FormattingEnabled = True
        Me.ComboBoxStateLabelY.Location = New System.Drawing.Point(10, 75)
        Me.ComboBoxStateLabelY.Name = "ComboBoxStateLabelY"
        Me.ComboBoxStateLabelY.Size = New System.Drawing.Size(381, 21)
        Me.ComboBoxStateLabelY.TabIndex = 3
        '
        'ChooseStateClassForm
        '
        Me.AcceptButton = Me.ButtonOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ButtonCancel
        Me.ClientSize = New System.Drawing.Size(401, 149)
        Me.Controls.Add(Me.ComboBoxStateLabelY)
        Me.Controls.Add(Me.ComboBoxStateLabelX)
        Me.Controls.Add(Me.LabelStateLabelY)
        Me.Controls.Add(Me.LabelStateLabelX)
        Me.Controls.Add(Me.ButtonOK)
        Me.Controls.Add(Me.ButtonCancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ChooseStateClassForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Title"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents ButtonOK As System.Windows.Forms.Button
    Friend WithEvents LabelStateLabelX As System.Windows.Forms.Label
    Friend WithEvents LabelStateLabelY As System.Windows.Forms.Label
    Friend WithEvents ComboBoxStateLabelX As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBoxStateLabelY As System.Windows.Forms.ComboBox
End Class
