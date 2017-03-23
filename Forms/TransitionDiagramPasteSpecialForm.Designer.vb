<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TransitionDiagramPasteSpecialForm
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
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.RadioButtonPasteAll = New System.Windows.Forms.RadioButton()
        Me.RadioButtonPasteNone = New System.Windows.Forms.RadioButton()
        Me.RadioButtonPasteBetween = New System.Windows.Forms.RadioButton()
        Me.CheckboxPasteDeterministic = New System.Windows.Forms.CheckBox()
        Me.CheckboxPasteProbabilistic = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOK.Location = New System.Drawing.Point(39, 202)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(75, 23)
        Me.ButtonOK.TabIndex = 1
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonCancel.Location = New System.Drawing.Point(120, 202)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 23)
        Me.ButtonCancel.TabIndex = 2
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'RadioButtonPasteAll
        '
        Me.RadioButtonPasteAll.AutoSize = True
        Me.RadioButtonPasteAll.Checked = True
        Me.RadioButtonPasteAll.Location = New System.Drawing.Point(26, 30)
        Me.RadioButtonPasteAll.Name = "RadioButtonPasteAll"
        Me.RadioButtonPasteAll.Size = New System.Drawing.Size(36, 17)
        Me.RadioButtonPasteAll.TabIndex = 0
        Me.RadioButtonPasteAll.TabStop = True
        Me.RadioButtonPasteAll.Text = "All"
        Me.RadioButtonPasteAll.UseVisualStyleBackColor = True
        '
        'RadioButtonPasteNone
        '
        Me.RadioButtonPasteNone.AutoSize = True
        Me.RadioButtonPasteNone.Location = New System.Drawing.Point(26, 76)
        Me.RadioButtonPasteNone.Name = "RadioButtonPasteNone"
        Me.RadioButtonPasteNone.Size = New System.Drawing.Size(51, 17)
        Me.RadioButtonPasteNone.TabIndex = 2
        Me.RadioButtonPasteNone.Text = "None"
        Me.RadioButtonPasteNone.UseVisualStyleBackColor = True
        '
        'RadioButtonPasteBetween
        '
        Me.RadioButtonPasteBetween.AutoSize = True
        Me.RadioButtonPasteBetween.Location = New System.Drawing.Point(26, 53)
        Me.RadioButtonPasteBetween.Name = "RadioButtonPasteBetween"
        Me.RadioButtonPasteBetween.Size = New System.Drawing.Size(148, 17)
        Me.RadioButtonPasteBetween.TabIndex = 1
        Me.RadioButtonPasteBetween.Text = "Between selected classes"
        Me.RadioButtonPasteBetween.UseVisualStyleBackColor = True
        '
        'CheckboxPasteDeterministic
        '
        Me.CheckboxPasteDeterministic.AutoSize = True
        Me.CheckboxPasteDeterministic.Checked = True
        Me.CheckboxPasteDeterministic.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckboxPasteDeterministic.Location = New System.Drawing.Point(26, 114)
        Me.CheckboxPasteDeterministic.Name = "CheckboxPasteDeterministic"
        Me.CheckboxPasteDeterministic.Size = New System.Drawing.Size(136, 17)
        Me.CheckboxPasteDeterministic.TabIndex = 3
        Me.CheckboxPasteDeterministic.Text = "Deterministic transitions"
        Me.CheckboxPasteDeterministic.UseVisualStyleBackColor = True
        '
        'CheckboxPasteProbabilistic
        '
        Me.CheckboxPasteProbabilistic.AutoSize = True
        Me.CheckboxPasteProbabilistic.Checked = True
        Me.CheckboxPasteProbabilistic.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckboxPasteProbabilistic.Location = New System.Drawing.Point(26, 137)
        Me.CheckboxPasteProbabilistic.Name = "CheckboxPasteProbabilistic"
        Me.CheckboxPasteProbabilistic.Size = New System.Drawing.Size(132, 17)
        Me.CheckboxPasteProbabilistic.TabIndex = 4
        Me.CheckboxPasteProbabilistic.Text = "Probabilistic transitions"
        Me.CheckboxPasteProbabilistic.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.RadioButtonPasteBetween)
        Me.GroupBox1.Controls.Add(Me.RadioButtonPasteAll)
        Me.GroupBox1.Controls.Add(Me.CheckboxPasteDeterministic)
        Me.GroupBox1.Controls.Add(Me.CheckboxPasteProbabilistic)
        Me.GroupBox1.Controls.Add(Me.RadioButtonPasteNone)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(219, 173)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Transitions Included"
        '
        'TransitionDiagramPasteSpecialForm
        '
        Me.AcceptButton = Me.ButtonOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ButtonCancel
        Me.ClientSize = New System.Drawing.Size(243, 240)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TransitionDiagramPasteSpecialForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Paste Special"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ButtonOK As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents RadioButtonPasteAll As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonPasteBetween As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButtonPasteNone As System.Windows.Forms.RadioButton
    Friend WithEvents CheckboxPasteDeterministic As System.Windows.Forms.CheckBox
    Friend WithEvents CheckboxPasteProbabilistic As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
End Class


