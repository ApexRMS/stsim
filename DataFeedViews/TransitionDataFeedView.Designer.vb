<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TransitionDataFeedView
    Inherits SyncroSim.Core.Forms.DataFeedView

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.PanelZoomControls = New System.Windows.Forms.Panel()
        Me.ButtonZoomIn = New System.Windows.Forms.Button()
        Me.ButtonZoomOut = New System.Windows.Forms.Button()
        Me.SplitContainerTabStrip = New System.Windows.Forms.SplitContainer()
        Me.TabStripMain = New SyncroSim.Common.Forms.TabStrip()
        Me.ScrollBarHorizontal = New System.Windows.Forms.HScrollBar()
        Me.PanelBottomControls = New System.Windows.Forms.Panel()
        Me.PanelTabNavigator = New System.Windows.Forms.Panel()
        Me.ButtonSelectStratum = New System.Windows.Forms.Button()
        Me.ButtonLast = New System.Windows.Forms.Button()
        Me.ButtonFirst = New System.Windows.Forms.Button()
        Me.ButtonNext = New System.Windows.Forms.Button()
        Me.ButtonPrevious = New System.Windows.Forms.Button()
        Me.ScrollBarVertical = New System.Windows.Forms.VScrollBar()
        Me.PanelControlHost = New System.Windows.Forms.Panel()
        Me.PanelZoomControls.SuspendLayout()
        CType(Me.SplitContainerTabStrip, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerTabStrip.Panel1.SuspendLayout()
        Me.SplitContainerTabStrip.Panel2.SuspendLayout()
        Me.SplitContainerTabStrip.SuspendLayout()
        Me.PanelBottomControls.SuspendLayout()
        Me.PanelTabNavigator.SuspendLayout()
        Me.SuspendLayout()
        '
        'PanelZoomControls
        '
        Me.PanelZoomControls.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelZoomControls.Controls.Add(Me.ButtonZoomIn)
        Me.PanelZoomControls.Controls.Add(Me.ButtonZoomOut)
        Me.PanelZoomControls.Location = New System.Drawing.Point(674, 0)
        Me.PanelZoomControls.Name = "PanelZoomControls"
        Me.PanelZoomControls.Size = New System.Drawing.Size(43, 20)
        Me.PanelZoomControls.TabIndex = 14
        '
        'ButtonZoomIn
        '
        Me.ButtonZoomIn.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonZoomIn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ButtonZoomIn.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.ButtonZoomIn.FlatAppearance.BorderSize = 0
        Me.ButtonZoomIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonZoomIn.Image = Global.SyncroSim.STSim.My.Resources.Resources.Plus16x16
        Me.ButtonZoomIn.Location = New System.Drawing.Point(20, 0)
        Me.ButtonZoomIn.Name = "ButtonZoomIn"
        Me.ButtonZoomIn.Size = New System.Drawing.Size(20, 20)
        Me.ButtonZoomIn.TabIndex = 1
        Me.ButtonZoomIn.UseVisualStyleBackColor = False
        '
        'ButtonZoomOut
        '
        Me.ButtonZoomOut.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonZoomOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ButtonZoomOut.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.ButtonZoomOut.FlatAppearance.BorderSize = 0
        Me.ButtonZoomOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonZoomOut.Image = Global.SyncroSim.STSim.My.Resources.Resources.Minus16x16
        Me.ButtonZoomOut.Location = New System.Drawing.Point(0, 0)
        Me.ButtonZoomOut.Name = "ButtonZoomOut"
        Me.ButtonZoomOut.Size = New System.Drawing.Size(20, 20)
        Me.ButtonZoomOut.TabIndex = 0
        Me.ButtonZoomOut.UseVisualStyleBackColor = False
        '
        'SplitContainerTabStrip
        '
        Me.SplitContainerTabStrip.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainerTabStrip.BackColor = System.Drawing.Color.LightSteelBlue
        Me.SplitContainerTabStrip.Location = New System.Drawing.Point(100, 0)
        Me.SplitContainerTabStrip.Name = "SplitContainerTabStrip"
        '
        'SplitContainerTabStrip.Panel1
        '
        Me.SplitContainerTabStrip.Panel1.Controls.Add(Me.TabStripMain)
        '
        'SplitContainerTabStrip.Panel2
        '
        Me.SplitContainerTabStrip.Panel2.Controls.Add(Me.ScrollBarHorizontal)
        Me.SplitContainerTabStrip.Size = New System.Drawing.Size(552, 20)
        Me.SplitContainerTabStrip.SplitterDistance = 413
        Me.SplitContainerTabStrip.SplitterWidth = 8
        Me.SplitContainerTabStrip.TabIndex = 5
        '
        'TabStripMain
        '
        Me.TabStripMain.BackColor = System.Drawing.Color.White
        Me.TabStripMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabStripMain.Location = New System.Drawing.Point(0, 0)
        Me.TabStripMain.Name = "TabStripMain"
        Me.TabStripMain.Size = New System.Drawing.Size(413, 20)
        Me.TabStripMain.TabIndex = 0
        Me.TabStripMain.TabStop = False
        Me.TabStripMain.Text = "TabStripMain"
        '
        'ScrollBarHorizontal
        '
        Me.ScrollBarHorizontal.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ScrollBarHorizontal.Location = New System.Drawing.Point(0, 0)
        Me.ScrollBarHorizontal.Name = "ScrollBarHorizontal"
        Me.ScrollBarHorizontal.Size = New System.Drawing.Size(131, 20)
        Me.ScrollBarHorizontal.TabIndex = 0
        Me.ScrollBarHorizontal.TabStop = True
        '
        'PanelBottomControls
        '
        Me.PanelBottomControls.BackColor = System.Drawing.SystemColors.Control
        Me.PanelBottomControls.Controls.Add(Me.PanelZoomControls)
        Me.PanelBottomControls.Controls.Add(Me.PanelTabNavigator)
        Me.PanelBottomControls.Controls.Add(Me.SplitContainerTabStrip)
        Me.PanelBottomControls.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelBottomControls.Location = New System.Drawing.Point(0, 333)
        Me.PanelBottomControls.Name = "PanelBottomControls"
        Me.PanelBottomControls.Size = New System.Drawing.Size(734, 20)
        Me.PanelBottomControls.TabIndex = 0
        '
        'PanelTabNavigator
        '
        Me.PanelTabNavigator.BackColor = System.Drawing.Color.Maroon
        Me.PanelTabNavigator.Controls.Add(Me.ButtonSelectStratum)
        Me.PanelTabNavigator.Controls.Add(Me.ButtonLast)
        Me.PanelTabNavigator.Controls.Add(Me.ButtonFirst)
        Me.PanelTabNavigator.Controls.Add(Me.ButtonNext)
        Me.PanelTabNavigator.Controls.Add(Me.ButtonPrevious)
        Me.PanelTabNavigator.Location = New System.Drawing.Point(0, 0)
        Me.PanelTabNavigator.Name = "PanelTabNavigator"
        Me.PanelTabNavigator.Size = New System.Drawing.Size(100, 20)
        Me.PanelTabNavigator.TabIndex = 2
        '
        'ButtonSelectStratum
        '
        Me.ButtonSelectStratum.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonSelectStratum.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ButtonSelectStratum.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.ButtonSelectStratum.FlatAppearance.BorderSize = 0
        Me.ButtonSelectStratum.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonSelectStratum.Image = Global.SyncroSim.STSim.My.Resources.Resources.Search16x16
        Me.ButtonSelectStratum.Location = New System.Drawing.Point(40, 0)
        Me.ButtonSelectStratum.Name = "ButtonSelectStratum"
        Me.ButtonSelectStratum.Size = New System.Drawing.Size(20, 20)
        Me.ButtonSelectStratum.TabIndex = 2
        Me.ButtonSelectStratum.UseVisualStyleBackColor = False
        '
        'ButtonLast
        '
        Me.ButtonLast.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonLast.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ButtonLast.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.ButtonLast.FlatAppearance.BorderSize = 0
        Me.ButtonLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonLast.Image = Global.SyncroSim.STSim.My.Resources.Resources.Last16x16
        Me.ButtonLast.Location = New System.Drawing.Point(80, 0)
        Me.ButtonLast.Name = "ButtonLast"
        Me.ButtonLast.Size = New System.Drawing.Size(20, 20)
        Me.ButtonLast.TabIndex = 4
        Me.ButtonLast.UseVisualStyleBackColor = False
        '
        'ButtonFirst
        '
        Me.ButtonFirst.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonFirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ButtonFirst.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.ButtonFirst.FlatAppearance.BorderSize = 0
        Me.ButtonFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonFirst.Image = Global.SyncroSim.STSim.My.Resources.Resources.First16x16
        Me.ButtonFirst.Location = New System.Drawing.Point(0, 0)
        Me.ButtonFirst.Name = "ButtonFirst"
        Me.ButtonFirst.Size = New System.Drawing.Size(20, 20)
        Me.ButtonFirst.TabIndex = 0
        Me.ButtonFirst.UseVisualStyleBackColor = False
        '
        'ButtonNext
        '
        Me.ButtonNext.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ButtonNext.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.ButtonNext.FlatAppearance.BorderSize = 0
        Me.ButtonNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonNext.Image = Global.SyncroSim.STSim.My.Resources.Resources.Next16x16
        Me.ButtonNext.Location = New System.Drawing.Point(60, 0)
        Me.ButtonNext.Name = "ButtonNext"
        Me.ButtonNext.Size = New System.Drawing.Size(20, 20)
        Me.ButtonNext.TabIndex = 3
        Me.ButtonNext.UseVisualStyleBackColor = False
        '
        'ButtonPrevious
        '
        Me.ButtonPrevious.BackColor = System.Drawing.SystemColors.Control
        Me.ButtonPrevious.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ButtonPrevious.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control
        Me.ButtonPrevious.FlatAppearance.BorderSize = 0
        Me.ButtonPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ButtonPrevious.Image = Global.SyncroSim.STSim.My.Resources.Resources.Previous16x16
        Me.ButtonPrevious.Location = New System.Drawing.Point(20, 0)
        Me.ButtonPrevious.Name = "ButtonPrevious"
        Me.ButtonPrevious.Size = New System.Drawing.Size(20, 20)
        Me.ButtonPrevious.TabIndex = 1
        Me.ButtonPrevious.UseVisualStyleBackColor = False
        '
        'ScrollBarVertical
        '
        Me.ScrollBarVertical.Dock = System.Windows.Forms.DockStyle.Right
        Me.ScrollBarVertical.Location = New System.Drawing.Point(714, 0)
        Me.ScrollBarVertical.Name = "ScrollBarVertical"
        Me.ScrollBarVertical.Size = New System.Drawing.Size(20, 333)
        Me.ScrollBarVertical.TabIndex = 1
        Me.ScrollBarVertical.TabStop = True
        '
        'PanelControlHost
        '
        Me.PanelControlHost.BackColor = System.Drawing.Color.White
        Me.PanelControlHost.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControlHost.Location = New System.Drawing.Point(0, 0)
        Me.PanelControlHost.Name = "PanelControlHost"
        Me.PanelControlHost.Size = New System.Drawing.Size(714, 333)
        Me.PanelControlHost.TabIndex = 0
        Me.PanelControlHost.TabStop = True
        '
        'TransitionDataFeedView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.PanelControlHost)
        Me.Controls.Add(Me.ScrollBarVertical)
        Me.Controls.Add(Me.PanelBottomControls)
        Me.Name = "TransitionDataFeedView"
        Me.Size = New System.Drawing.Size(734, 353)
        Me.PanelZoomControls.ResumeLayout(False)
        Me.SplitContainerTabStrip.Panel1.ResumeLayout(False)
        Me.SplitContainerTabStrip.Panel2.ResumeLayout(False)
        CType(Me.SplitContainerTabStrip, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerTabStrip.ResumeLayout(False)
        Me.PanelBottomControls.ResumeLayout(False)
        Me.PanelTabNavigator.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelBottomControls As System.Windows.Forms.Panel
    Friend WithEvents TabStripMain As SyncroSim.Common.Forms.TabStrip
    Friend WithEvents PanelTabNavigator As System.Windows.Forms.Panel
    Friend WithEvents SplitContainerTabStrip As System.Windows.Forms.SplitContainer
    Friend WithEvents ButtonLast As System.Windows.Forms.Button
    Friend WithEvents ButtonNext As System.Windows.Forms.Button
    Friend WithEvents ButtonPrevious As System.Windows.Forms.Button
    Friend WithEvents ButtonFirst As System.Windows.Forms.Button
    Friend WithEvents PanelZoomControls As System.Windows.Forms.Panel
    Friend WithEvents ButtonZoomIn As System.Windows.Forms.Button
    Friend WithEvents ButtonZoomOut As System.Windows.Forms.Button
    Friend WithEvents ScrollBarHorizontal As System.Windows.Forms.HScrollBar
    Friend WithEvents ScrollBarVertical As System.Windows.Forms.VScrollBar
    Friend WithEvents PanelControlHost As System.Windows.Forms.Panel
    Friend WithEvents ButtonSelectStratum As System.Windows.Forms.Button

End Class
