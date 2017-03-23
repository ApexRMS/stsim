<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SlopeMultiplierDataFeedView
    Inherits SyncroSim.Core.Forms.DataFeedView

    'UserControl overrides dispose to clean up the component list.
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
        Me.ButtonClear = New System.Windows.Forms.Button()
        Me.ButtonBrowse = New System.Windows.Forms.Button()
        Me.TextBoxDEMFilename = New System.Windows.Forms.TextBox()
        Me.LabelTMV = New System.Windows.Forms.Label()
        Me.LabelDEM = New System.Windows.Forms.Label()
        Me.PanelOptions = New System.Windows.Forms.Panel()
        Me.PanelMultipliers = New System.Windows.Forms.Panel()
        Me.PanelOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonClear
        '
        Me.ButtonClear.Location = New System.Drawing.Point(291, 27)
        Me.ButtonClear.Name = "ButtonClear"
        Me.ButtonClear.Size = New System.Drawing.Size(75, 23)
        Me.ButtonClear.TabIndex = 3
        Me.ButtonClear.Text = "Clear"
        Me.ButtonClear.UseVisualStyleBackColor = True
        '
        'ButtonBrowse
        '
        Me.ButtonBrowse.Location = New System.Drawing.Point(210, 27)
        Me.ButtonBrowse.Name = "ButtonBrowse"
        Me.ButtonBrowse.Size = New System.Drawing.Size(75, 23)
        Me.ButtonBrowse.TabIndex = 2
        Me.ButtonBrowse.Text = "Browse..."
        Me.ButtonBrowse.UseVisualStyleBackColor = True
        '
        'TextBoxDEMFilename
        '
        Me.TextBoxDEMFilename.Location = New System.Drawing.Point(3, 29)
        Me.TextBoxDEMFilename.Name = "TextBoxDEMFilename"
        Me.TextBoxDEMFilename.ReadOnly = True
        Me.TextBoxDEMFilename.Size = New System.Drawing.Size(200, 20)
        Me.TextBoxDEMFilename.TabIndex = 1
        '
        'LabelTMV
        '
        Me.LabelTMV.AutoSize = True
        Me.LabelTMV.Location = New System.Drawing.Point(0, 69)
        Me.LabelTMV.Name = "LabelTMV"
        Me.LabelTMV.Size = New System.Drawing.Size(161, 13)
        Me.LabelTMV.TabIndex = 4
        Me.LabelTMV.Text = "Transition slope multiplier values:"
        '
        'LabelDEM
        '
        Me.LabelDEM.AutoSize = True
        Me.LabelDEM.Location = New System.Drawing.Point(1, 7)
        Me.LabelDEM.Name = "LabelDEM"
        Me.LabelDEM.Size = New System.Drawing.Size(161, 13)
        Me.LabelDEM.TabIndex = 0
        Me.LabelDEM.Text = "Digital elevation model file name:"
        '
        'PanelOptions
        '
        Me.PanelOptions.Controls.Add(Me.LabelDEM)
        Me.PanelOptions.Controls.Add(Me.ButtonClear)
        Me.PanelOptions.Controls.Add(Me.LabelTMV)
        Me.PanelOptions.Controls.Add(Me.ButtonBrowse)
        Me.PanelOptions.Controls.Add(Me.TextBoxDEMFilename)
        Me.PanelOptions.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelOptions.Location = New System.Drawing.Point(0, 0)
        Me.PanelOptions.Name = "PanelOptions"
        Me.PanelOptions.Size = New System.Drawing.Size(557, 94)
        Me.PanelOptions.TabIndex = 0
        '
        'PanelMultipliers
        '
        Me.PanelMultipliers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelMultipliers.Location = New System.Drawing.Point(0, 94)
        Me.PanelMultipliers.Name = "PanelMultipliers"
        Me.PanelMultipliers.Size = New System.Drawing.Size(557, 235)
        Me.PanelMultipliers.TabIndex = 1
        '
        'SlopeMultiplierDataFeedView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.PanelMultipliers)
        Me.Controls.Add(Me.PanelOptions)
        Me.Name = "SlopeMultiplierDataFeedView"
        Me.Size = New System.Drawing.Size(557, 329)
        Me.PanelOptions.ResumeLayout(False)
        Me.PanelOptions.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ButtonClear As System.Windows.Forms.Button
    Friend WithEvents ButtonBrowse As System.Windows.Forms.Button
    Friend WithEvents TextBoxDEMFilename As System.Windows.Forms.TextBox
    Friend WithEvents LabelTMV As System.Windows.Forms.Label
    Friend WithEvents LabelDEM As System.Windows.Forms.Label
    Friend WithEvents PanelOptions As System.Windows.Forms.Panel
    Friend WithEvents PanelMultipliers As System.Windows.Forms.Panel

End Class
