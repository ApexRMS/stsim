<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FilterTransitionsForm
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
        Me.components = New System.ComponentModel.Container()
        Me.ButtonOK = New System.Windows.Forms.Button()
        Me.ButtonCancel = New System.Windows.Forms.Button()
        Me.TransitionGroupsContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MenuItemCheckSelected = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItemUncheckSelected = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.MenuItemCheckAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuItemUncheckAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.CheckboxDeterministicTransitions = New System.Windows.Forms.CheckBox()
        Me.CheckboxProbabilisticTransitions = New System.Windows.Forms.CheckBox()
        Me.CheckBoxPanelMain = New SyncroSim.Common.Forms.CheckBoxPanel()
        Me.TransitionGroupsContextMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'ButtonOK
        '
        Me.ButtonOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonOK.Location = New System.Drawing.Point(403, 420)
        Me.ButtonOK.Name = "ButtonOK"
        Me.ButtonOK.Size = New System.Drawing.Size(75, 23)
        Me.ButtonOK.TabIndex = 3
        Me.ButtonOK.Text = "OK"
        Me.ButtonOK.UseVisualStyleBackColor = True
        '
        'ButtonCancel
        '
        Me.ButtonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.ButtonCancel.Location = New System.Drawing.Point(484, 420)
        Me.ButtonCancel.Name = "ButtonCancel"
        Me.ButtonCancel.Size = New System.Drawing.Size(75, 23)
        Me.ButtonCancel.TabIndex = 4
        Me.ButtonCancel.Text = "Cancel"
        Me.ButtonCancel.UseVisualStyleBackColor = True
        '
        'TransitionGroupsContextMenu
        '
        Me.TransitionGroupsContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuItemCheckSelected, Me.MenuItemUncheckSelected, Me.ToolStripSeparator1, Me.MenuItemCheckAll, Me.MenuItemUncheckAll})
        Me.TransitionGroupsContextMenu.Name = "TransitionGroupsContextMenu"
        Me.TransitionGroupsContextMenu.Size = New System.Drawing.Size(168, 98)
        '
        'MenuItemCheckSelected
        '
        Me.MenuItemCheckSelected.Name = "MenuItemCheckSelected"
        Me.MenuItemCheckSelected.Size = New System.Drawing.Size(167, 22)
        Me.MenuItemCheckSelected.Text = "Check Selected"
        '
        'MenuItemUncheckSelected
        '
        Me.MenuItemUncheckSelected.Name = "MenuItemUncheckSelected"
        Me.MenuItemUncheckSelected.Size = New System.Drawing.Size(167, 22)
        Me.MenuItemUncheckSelected.Text = "Uncheck Selected"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(164, 6)
        '
        'MenuItemCheckAll
        '
        Me.MenuItemCheckAll.Name = "MenuItemCheckAll"
        Me.MenuItemCheckAll.Size = New System.Drawing.Size(167, 22)
        Me.MenuItemCheckAll.Text = "Check All"
        '
        'MenuItemUncheckAll
        '
        Me.MenuItemUncheckAll.Name = "MenuItemUncheckAll"
        Me.MenuItemUncheckAll.Size = New System.Drawing.Size(167, 22)
        Me.MenuItemUncheckAll.Text = "Uncheck All"
        '
        'CheckboxDeterministicTransitions
        '
        Me.CheckboxDeterministicTransitions.AutoSize = True
        Me.CheckboxDeterministicTransitions.Location = New System.Drawing.Point(10, 13)
        Me.CheckboxDeterministicTransitions.Name = "CheckboxDeterministicTransitions"
        Me.CheckboxDeterministicTransitions.Size = New System.Drawing.Size(136, 17)
        Me.CheckboxDeterministicTransitions.TabIndex = 0
        Me.CheckboxDeterministicTransitions.Text = "Deterministic transitions"
        Me.CheckboxDeterministicTransitions.UseVisualStyleBackColor = True
        '
        'CheckboxProbabilisticTransitions
        '
        Me.CheckboxProbabilisticTransitions.AutoSize = True
        Me.CheckboxProbabilisticTransitions.Location = New System.Drawing.Point(10, 42)
        Me.CheckboxProbabilisticTransitions.Name = "CheckboxProbabilisticTransitions"
        Me.CheckboxProbabilisticTransitions.Size = New System.Drawing.Size(132, 17)
        Me.CheckboxProbabilisticTransitions.TabIndex = 1
        Me.CheckboxProbabilisticTransitions.Text = "Probabilistic transitions"
        Me.CheckboxProbabilisticTransitions.UseVisualStyleBackColor = True
        '
        'CheckBoxPanelMain
        '
        Me.CheckBoxPanelMain.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBoxPanelMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CheckBoxPanelMain.IsReadOnly = False
        Me.CheckBoxPanelMain.Location = New System.Drawing.Point(10, 71)
        Me.CheckBoxPanelMain.Name = "CheckBoxPanelMain"
        Me.CheckBoxPanelMain.Size = New System.Drawing.Size(548, 343)
        Me.CheckBoxPanelMain.TabIndex = 2
        Me.CheckBoxPanelMain.TitleBarText = "Item Names"
        '
        'FilterTransitionsForm
        '
        Me.AcceptButton = Me.ButtonOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.ButtonCancel
        Me.ClientSize = New System.Drawing.Size(568, 448)
        Me.Controls.Add(Me.CheckBoxPanelMain)
        Me.Controls.Add(Me.ButtonCancel)
        Me.Controls.Add(Me.ButtonOK)
        Me.Controls.Add(Me.CheckboxDeterministicTransitions)
        Me.Controls.Add(Me.CheckboxProbabilisticTransitions)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(430, 345)
        Me.Name = "FilterTransitionsForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Filter Transitions"
        Me.TransitionGroupsContextMenu.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ButtonOK As System.Windows.Forms.Button
    Friend WithEvents ButtonCancel As System.Windows.Forms.Button
    Friend WithEvents TransitionGroupsContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents MenuItemCheckSelected As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuItemUncheckSelected As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MenuItemCheckAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuItemUncheckAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CheckboxDeterministicTransitions As System.Windows.Forms.CheckBox
    Friend WithEvents CheckboxProbabilisticTransitions As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxPanelMain As SyncroSim.Common.Forms.CheckBoxPanel
End Class


