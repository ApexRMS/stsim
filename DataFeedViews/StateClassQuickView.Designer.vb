<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StateClassQuickView
    Inherits SyncroSim.Core.Forms.DataFeedView

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.LabelDeterministic = New System.Windows.Forms.Label()
        Me.SplitContainerMain = New System.Windows.Forms.SplitContainer()
        Me.PanelDeterministic = New System.Windows.Forms.Panel()
        Me.LabelProbabilistic = New System.Windows.Forms.Label()
        Me.PanelProbabilistic = New System.Windows.Forms.Panel()
        Me.ContextMenuStripDeterministic = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MenuItemTransitionsToDeterministic = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItemTransitionsFromDeterministic = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuItemIterationDeterministic = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItemTimestepDeterministic = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItemStratumDeterministic = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItemToStratumDeterministic = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItemAgeMinDeterministic = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItemAgeMaxDeterministic = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItemToClassDetreministic = New System.Windows.Forms.ToolStripMenuItem()
        CType(Me.SplitContainerMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerMain.Panel1.SuspendLayout()
        Me.SplitContainerMain.Panel2.SuspendLayout()
        Me.SplitContainerMain.SuspendLayout()
        Me.ContextMenuStripDeterministic.SuspendLayout()
        Me.SuspendLayout()
        '
        'LabelDeterministic
        '
        Me.LabelDeterministic.AutoSize = True
        Me.LabelDeterministic.Location = New System.Drawing.Point(14, 8)
        Me.LabelDeterministic.Name = "LabelDeterministic"
        Me.LabelDeterministic.Size = New System.Drawing.Size(37, 13)
        Me.LabelDeterministic.TabIndex = 0
        Me.LabelDeterministic.Text = "States"
        '
        'SplitContainerMain
        '
        Me.SplitContainerMain.BackColor = System.Drawing.Color.LightGray
        Me.SplitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerMain.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerMain.Name = "SplitContainerMain"
        Me.SplitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainerMain.Panel1
        '
        Me.SplitContainerMain.Panel1.BackColor = System.Drawing.Color.White
        Me.SplitContainerMain.Panel1.Controls.Add(Me.LabelDeterministic)
        Me.SplitContainerMain.Panel1.Controls.Add(Me.PanelDeterministic)
        '
        'SplitContainerMain.Panel2
        '
        Me.SplitContainerMain.Panel2.BackColor = System.Drawing.Color.White
        Me.SplitContainerMain.Panel2.Controls.Add(Me.LabelProbabilistic)
        Me.SplitContainerMain.Panel2.Controls.Add(Me.PanelProbabilistic)
        Me.SplitContainerMain.Size = New System.Drawing.Size(796, 388)
        Me.SplitContainerMain.SplitterDistance = 150
        Me.SplitContainerMain.TabIndex = 4
        '
        'PanelDeterministic
        '
        Me.PanelDeterministic.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelDeterministic.BackColor = System.Drawing.SystemColors.Control
        Me.PanelDeterministic.Location = New System.Drawing.Point(15, 30)
        Me.PanelDeterministic.Name = "PanelDeterministic"
        Me.PanelDeterministic.Size = New System.Drawing.Size(766, 108)
        Me.PanelDeterministic.TabIndex = 3
        '
        'LabelProbabilistic
        '
        Me.LabelProbabilistic.AutoSize = True
        Me.LabelProbabilistic.Location = New System.Drawing.Point(14, 8)
        Me.LabelProbabilistic.Name = "LabelProbabilistic"
        Me.LabelProbabilistic.Size = New System.Drawing.Size(117, 13)
        Me.LabelProbabilistic.TabIndex = 0
        Me.LabelProbabilistic.Text = "Probabilistic Transitions"
        '
        'PanelProbabilistic
        '
        Me.PanelProbabilistic.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelProbabilistic.BackColor = System.Drawing.SystemColors.Control
        Me.PanelProbabilistic.Location = New System.Drawing.Point(15, 29)
        Me.PanelProbabilistic.Name = "PanelProbabilistic"
        Me.PanelProbabilistic.Size = New System.Drawing.Size(766, 193)
        Me.PanelProbabilistic.TabIndex = 6
        '
        'ContextMenuStripDeterministic
        '
        Me.ContextMenuStripDeterministic.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuItemTransitionsToDeterministic, Me.MenuItemTransitionsFromDeterministic, Me.ToolStripSeparator1, Me.MenuItemIterationDeterministic, Me.MenuItemTimestepDeterministic, Me.MenuItemStratumDeterministic, Me.MenuItemToStratumDeterministic, Me.MenuItemToClassDetreministic, Me.MenuItemAgeMinDeterministic, Me.MenuItemAgeMaxDeterministic})
        Me.ContextMenuStripDeterministic.Name = "ContextMenuStripDeterministic"
        Me.ContextMenuStripDeterministic.Size = New System.Drawing.Size(164, 230)
        '
        'MenuItemTransitionsToDeterministic
        '
        Me.MenuItemTransitionsToDeterministic.Name = "MenuItemTransitionsToDeterministic"
        Me.MenuItemTransitionsToDeterministic.Size = New System.Drawing.Size(163, 22)
        Me.MenuItemTransitionsToDeterministic.Text = "Transitions To"
        '
        'MenuItemTransitionsFromDeterministic
        '
        Me.MenuItemTransitionsFromDeterministic.Name = "MenuItemTransitionsFromDeterministic"
        Me.MenuItemTransitionsFromDeterministic.Size = New System.Drawing.Size(163, 22)
        Me.MenuItemTransitionsFromDeterministic.Text = "Transitions From"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(160, 6)
        '
        'MenuItemIterationDeterministic
        '
        Me.MenuItemIterationDeterministic.Name = "MenuItemIterationDeterministic"
        Me.MenuItemIterationDeterministic.Size = New System.Drawing.Size(163, 22)
        Me.MenuItemIterationDeterministic.Text = "Iteration"
        '
        'MenuItemTimestepDeterministic
        '
        Me.MenuItemTimestepDeterministic.Name = "MenuItemTimestepDeterministic"
        Me.MenuItemTimestepDeterministic.Size = New System.Drawing.Size(163, 22)
        Me.MenuItemTimestepDeterministic.Text = "Timestep"
        '
        'MenuItemStratumDeterministic
        '
        Me.MenuItemStratumDeterministic.Name = "MenuItemStratumDeterministic"
        Me.MenuItemStratumDeterministic.Size = New System.Drawing.Size(163, 22)
        Me.MenuItemStratumDeterministic.Text = "Stratum"
        '
        'MenuItemToStratumDeterministic
        '
        Me.MenuItemToStratumDeterministic.Name = "MenuItemToStratumDeterministic"
        Me.MenuItemToStratumDeterministic.Size = New System.Drawing.Size(163, 22)
        Me.MenuItemToStratumDeterministic.Text = "To Stratum"
        '
        'MenuItemAgeMinDeterministic
        '
        Me.MenuItemAgeMinDeterministic.Name = "MenuItemAgeMinDeterministic"
        Me.MenuItemAgeMinDeterministic.Size = New System.Drawing.Size(163, 22)
        Me.MenuItemAgeMinDeterministic.Text = "Age Min"
        '
        'MenuItemAgeMaxDeterministic
        '
        Me.MenuItemAgeMaxDeterministic.Name = "MenuItemAgeMaxDeterministic"
        Me.MenuItemAgeMaxDeterministic.Size = New System.Drawing.Size(163, 22)
        Me.MenuItemAgeMaxDeterministic.Text = "Age Max"
        '
        'MenuItemToClassDetreministic
        '
        Me.MenuItemToClassDetreministic.Name = "MenuItemToClassDetreministic"
        Me.MenuItemToClassDetreministic.Size = New System.Drawing.Size(163, 22)
        Me.MenuItemToClassDetreministic.Text = "To Class"
        '
        'StateClassQuickView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.SplitContainerMain)
        Me.Name = "StateClassQuickView"
        Me.Size = New System.Drawing.Size(796, 388)
        Me.SplitContainerMain.Panel1.ResumeLayout(False)
        Me.SplitContainerMain.Panel1.PerformLayout()
        Me.SplitContainerMain.Panel2.ResumeLayout(False)
        Me.SplitContainerMain.Panel2.PerformLayout()
        CType(Me.SplitContainerMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerMain.ResumeLayout(False)
        Me.ContextMenuStripDeterministic.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents LabelDeterministic As System.Windows.Forms.Label
    Friend WithEvents SplitContainerMain As System.Windows.Forms.SplitContainer
    Friend WithEvents PanelDeterministic As System.Windows.Forms.Panel
    Friend WithEvents LabelProbabilistic As System.Windows.Forms.Label
    Friend WithEvents PanelProbabilistic As System.Windows.Forms.Panel
    Friend WithEvents ContextMenuStripDeterministic As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents MenuItemTransitionsToDeterministic As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuItemTransitionsFromDeterministic As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MenuItemStratumDeterministic As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuItemAgeMinDeterministic As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuItemAgeMaxDeterministic As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuItemIterationDeterministic As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuItemTimestepDeterministic As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuItemToStratumDeterministic As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuItemToClassDetreministic As System.Windows.Forms.ToolStripMenuItem

End Class
