<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InitialConditionsNonSpatialDataFeedView
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
        Me.CheckBoxCalcFromDist = New System.Windows.Forms.CheckBox()
        Me.TableLayoutPanelMain = New System.Windows.Forms.TableLayoutPanel()
        Me.TextBoxCellSize = New System.Windows.Forms.TextBox()
        Me.LabelCellSize = New System.Windows.Forms.Label()
        Me.LabelTotalAmount = New System.Windows.Forms.Label()
        Me.LabelNumCells = New System.Windows.Forms.Label()
        Me.TextBoxTotalAmount = New System.Windows.Forms.TextBox()
        Me.TextBoxNumCells = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ButtonClearAll = New System.Windows.Forms.Button()
        Me.PanelOptions = New System.Windows.Forms.Panel()
        Me.PanelDistribution = New System.Windows.Forms.Panel()
        Me.TableLayoutPanelMain.SuspendLayout()
        Me.PanelOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'CheckBoxCalcFromDist
        '
        Me.CheckBoxCalcFromDist.AutoSize = True
        Me.CheckBoxCalcFromDist.Location = New System.Drawing.Point(366, 42)
        Me.CheckBoxCalcFromDist.Name = "CheckBoxCalcFromDist"
        Me.CheckBoxCalcFromDist.Size = New System.Drawing.Size(146, 17)
        Me.CheckBoxCalcFromDist.TabIndex = 6
        Me.CheckBoxCalcFromDist.Text = "Calculate from distribution"
        Me.CheckBoxCalcFromDist.UseVisualStyleBackColor = True
        '
        'TableLayoutPanelMain
        '
        Me.TableLayoutPanelMain.ColumnCount = 2
        Me.TableLayoutPanelMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.62395!))
        Me.TableLayoutPanelMain.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.37605!))
        Me.TableLayoutPanelMain.Controls.Add(Me.TextBoxCellSize, 1, 2)
        Me.TableLayoutPanelMain.Controls.Add(Me.LabelCellSize, 0, 2)
        Me.TableLayoutPanelMain.Controls.Add(Me.LabelTotalAmount, 0, 0)
        Me.TableLayoutPanelMain.Controls.Add(Me.LabelNumCells, 0, 1)
        Me.TableLayoutPanelMain.Controls.Add(Me.TextBoxTotalAmount, 1, 0)
        Me.TableLayoutPanelMain.Controls.Add(Me.TextBoxNumCells, 1, 1)
        Me.TableLayoutPanelMain.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize
        Me.TableLayoutPanelMain.Location = New System.Drawing.Point(1, 12)
        Me.TableLayoutPanelMain.Name = "TableLayoutPanelMain"
        Me.TableLayoutPanelMain.RowCount = 3
        Me.TableLayoutPanelMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanelMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanelMain.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanelMain.Size = New System.Drawing.Size(359, 74)
        Me.TableLayoutPanelMain.TabIndex = 5
        '
        'TextBoxCellSize
        '
        Me.TextBoxCellSize.Enabled = False
        Me.TextBoxCellSize.Location = New System.Drawing.Point(234, 51)
        Me.TextBoxCellSize.Name = "TextBoxCellSize"
        Me.TextBoxCellSize.Size = New System.Drawing.Size(122, 20)
        Me.TextBoxCellSize.TabIndex = 5
        Me.TextBoxCellSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LabelCellSize
        '
        Me.LabelCellSize.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelCellSize.AutoSize = True
        Me.LabelCellSize.Location = New System.Drawing.Point(180, 53)
        Me.LabelCellSize.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        Me.LabelCellSize.Name = "LabelCellSize"
        Me.LabelCellSize.Size = New System.Drawing.Size(48, 13)
        Me.LabelCellSize.TabIndex = 4
        Me.LabelCellSize.Text = "Cell size:"
        Me.LabelCellSize.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LabelTotalAmount
        '
        Me.LabelTotalAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelTotalAmount.AutoSize = True
        Me.LabelTotalAmount.Location = New System.Drawing.Point(155, 5)
        Me.LabelTotalAmount.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        Me.LabelTotalAmount.Name = "LabelTotalAmount"
        Me.LabelTotalAmount.Size = New System.Drawing.Size(73, 13)
        Me.LabelTotalAmount.TabIndex = 0
        Me.LabelTotalAmount.Text = "Total Amount:"
        Me.LabelTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LabelNumCells
        '
        Me.LabelNumCells.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelNumCells.AutoSize = True
        Me.LabelNumCells.Location = New System.Drawing.Point(96, 29)
        Me.LabelNumCells.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        Me.LabelNumCells.Name = "LabelNumCells"
        Me.LabelNumCells.Size = New System.Drawing.Size(132, 13)
        Me.LabelNumCells.TabIndex = 2
        Me.LabelNumCells.Text = "Number of simulation cells:"
        Me.LabelNumCells.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextBoxTotalAmount
        '
        Me.TextBoxTotalAmount.Location = New System.Drawing.Point(234, 3)
        Me.TextBoxTotalAmount.Name = "TextBoxTotalAmount"
        Me.TextBoxTotalAmount.Size = New System.Drawing.Size(122, 20)
        Me.TextBoxTotalAmount.TabIndex = 1
        Me.TextBoxTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'TextBoxNumCells
        '
        Me.TextBoxNumCells.Location = New System.Drawing.Point(234, 27)
        Me.TextBoxNumCells.Name = "TextBoxNumCells"
        Me.TextBoxNumCells.Size = New System.Drawing.Size(122, 20)
        Me.TextBoxNumCells.TabIndex = 3
        Me.TextBoxNumCells.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(-1, 119)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Distribution:"
        '
        'ButtonClearAll
        '
        Me.ButtonClearAll.Location = New System.Drawing.Point(234, 92)
        Me.ButtonClearAll.Name = "ButtonClearAll"
        Me.ButtonClearAll.Size = New System.Drawing.Size(123, 23)
        Me.ButtonClearAll.TabIndex = 7
        Me.ButtonClearAll.Text = "Clear All"
        Me.ButtonClearAll.UseVisualStyleBackColor = True
        '
        'PanelOptions
        '
        Me.PanelOptions.Controls.Add(Me.TableLayoutPanelMain)
        Me.PanelOptions.Controls.Add(Me.ButtonClearAll)
        Me.PanelOptions.Controls.Add(Me.CheckBoxCalcFromDist)
        Me.PanelOptions.Controls.Add(Me.Label2)
        Me.PanelOptions.Dock = System.Windows.Forms.DockStyle.Top
        Me.PanelOptions.Location = New System.Drawing.Point(0, 0)
        Me.PanelOptions.Name = "PanelOptions"
        Me.PanelOptions.Size = New System.Drawing.Size(614, 142)
        Me.PanelOptions.TabIndex = 11
        '
        'PanelDistribution
        '
        Me.PanelDistribution.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelDistribution.Location = New System.Drawing.Point(0, 142)
        Me.PanelDistribution.Name = "PanelDistribution"
        Me.PanelDistribution.Size = New System.Drawing.Size(614, 230)
        Me.PanelDistribution.TabIndex = 12
        '
        'InitialConditionsNonSpatialDataFeedView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.PanelDistribution)
        Me.Controls.Add(Me.PanelOptions)
        Me.Name = "InitialConditionsNonSpatialDataFeedView"
        Me.Size = New System.Drawing.Size(614, 372)
        Me.TableLayoutPanelMain.ResumeLayout(False)
        Me.TableLayoutPanelMain.PerformLayout()
        Me.PanelOptions.ResumeLayout(False)
        Me.PanelOptions.PerformLayout()
        Me.ResumeLayout(False)

End Sub
    Friend WithEvents CheckBoxCalcFromDist As System.Windows.Forms.CheckBox
    Friend WithEvents TableLayoutPanelMain As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TextBoxCellSize As System.Windows.Forms.TextBox
    Friend WithEvents LabelCellSize As System.Windows.Forms.Label
    Friend WithEvents LabelTotalAmount As System.Windows.Forms.Label
    Friend WithEvents LabelNumCells As System.Windows.Forms.Label
    Friend WithEvents TextBoxTotalAmount As System.Windows.Forms.TextBox
    Friend WithEvents TextBoxNumCells As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ButtonClearAll As System.Windows.Forms.Button
    Friend WithEvents PanelOptions As System.Windows.Forms.Panel
    Friend WithEvents PanelDistribution As System.Windows.Forms.Panel

End Class
