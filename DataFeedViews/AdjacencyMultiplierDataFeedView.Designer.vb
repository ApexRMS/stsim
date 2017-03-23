<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AdjacencyMultiplierDataFeedView
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
        Me.SplitContainerMain = New System.Windows.Forms.SplitContainer()
        Me.PanelSettings = New System.Windows.Forms.Panel()
        Me.PanelSettingsLabel = New System.Windows.Forms.Panel()
        Me.LabelSettings = New System.Windows.Forms.Label()
        Me.PanelMultipliers = New System.Windows.Forms.Panel()
        Me.PanelMultiplersLabel = New System.Windows.Forms.Panel()
        Me.LabelMultipliers = New System.Windows.Forms.Label()
        CType(Me.SplitContainerMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerMain.Panel1.SuspendLayout()
        Me.SplitContainerMain.Panel2.SuspendLayout()
        Me.SplitContainerMain.SuspendLayout()
        Me.PanelSettingsLabel.SuspendLayout()
        Me.PanelMultiplersLabel.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainerMain
        '
        Me.SplitContainerMain.BackColor = System.Drawing.Color.Gainsboro
        Me.SplitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerMain.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerMain.Name = "SplitContainerMain"
        Me.SplitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerMain.Panel1
        '
        Me.SplitContainerMain.Panel1.BackColor = System.Drawing.Color.White
        Me.SplitContainerMain.Panel1.Controls.Add(Me.PanelSettings)
        Me.SplitContainerMain.Panel1.Controls.Add(Me.PanelSettingsLabel)
        Me.SplitContainerMain.Panel1.Padding = New System.Windows.Forms.Padding(0, 0, 0, 4)
        '
        'SplitContainerMain.Panel2
        '
        Me.SplitContainerMain.Panel2.Controls.Add(Me.PanelMultipliers)
        Me.SplitContainerMain.Panel2.Controls.Add(Me.PanelMultiplersLabel)
        Me.SplitContainerMain.Size = New System.Drawing.Size(747, 557)
        Me.SplitContainerMain.SplitterDistance = 278
        Me.SplitContainerMain.TabIndex = 10
        '
        'PanelSettings
        '
        Me.PanelSettings.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelSettings.Location = New System.Drawing.Point(0, 30)
        Me.PanelSettings.Name = "PanelSettings"
        Me.PanelSettings.Size = New System.Drawing.Size(747, 244)
        Me.PanelSettings.TabIndex = 1
        '
        'PanelSettingsLabel
        '
        Me.PanelSettingsLabel.BackColor = System.Drawing.Color.White
        Me.PanelSettingsLabel.Controls.Add(Me.LabelSettings)
        Me.PanelSettingsLabel.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelSettingsLabel.Location = New System.Drawing.Point(0, 0)
        Me.PanelSettingsLabel.Name = "PanelSettingsLabel"
        Me.PanelSettingsLabel.Size = New System.Drawing.Size(747, 30)
        Me.PanelSettingsLabel.TabIndex = 0
        '
        'LabelSettings
        '
        Me.LabelSettings.AutoSize = True
        Me.LabelSettings.Location = New System.Drawing.Point(-1, 8)
        Me.LabelSettings.Name = "LabelSettings"
        Me.LabelSettings.Size = New System.Drawing.Size(147, 13)
        Me.LabelSettings.TabIndex = 0
        Me.LabelSettings.Text = "Transition adjacency settings:"
        '
        'PanelMultipliers
        '
        Me.PanelMultipliers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelMultipliers.Location = New System.Drawing.Point(0, 30)
        Me.PanelMultipliers.Name = "PanelMultipliers"
        Me.PanelMultipliers.Size = New System.Drawing.Size(747, 245)
        Me.PanelMultipliers.TabIndex = 1
        '
        'PanelMultiplersLabel
        '
        Me.PanelMultiplersLabel.BackColor = System.Drawing.Color.White
        Me.PanelMultiplersLabel.Controls.Add(Me.LabelMultipliers)
        Me.PanelMultiplersLabel.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelMultiplersLabel.Location = New System.Drawing.Point(0, 0)
        Me.PanelMultiplersLabel.Name = "PanelMultiplersLabel"
        Me.PanelMultiplersLabel.Size = New System.Drawing.Size(747, 30)
        Me.PanelMultiplersLabel.TabIndex = 0
        '
        'LabelMultipliers
        '
        Me.LabelMultipliers.AutoSize = True
        Me.LabelMultipliers.Location = New System.Drawing.Point(-1, 8)
        Me.LabelMultipliers.Name = "LabelMultipliers"
        Me.LabelMultipliers.Size = New System.Drawing.Size(156, 13)
        Me.LabelMultipliers.TabIndex = 0
        Me.LabelMultipliers.Text = "Transition adjacency multipliers:"
        '
        'AdjacencyMultiplierDataFeedView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.SplitContainerMain)
        Me.Name = "AdjacencyMultiplierDataFeedView"
        Me.Size = New System.Drawing.Size(747, 557)
        Me.SplitContainerMain.Panel1.ResumeLayout(False)
        Me.SplitContainerMain.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerMain.ResumeLayout(False)
        Me.PanelSettingsLabel.ResumeLayout(False)
        Me.PanelSettingsLabel.PerformLayout()
        Me.PanelMultiplersLabel.ResumeLayout(False)
        Me.PanelMultiplersLabel.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainerMain As System.Windows.Forms.SplitContainer
    Friend WithEvents PanelSettings As System.Windows.Forms.Panel
    Friend WithEvents PanelSettingsLabel As System.Windows.Forms.Panel
    Friend WithEvents LabelSettings As System.Windows.Forms.Label
    Friend WithEvents PanelMultipliers As System.Windows.Forms.Panel
    Friend WithEvents PanelMultiplersLabel As System.Windows.Forms.Panel
    Friend WithEvents LabelMultipliers As System.Windows.Forms.Label

End Class
