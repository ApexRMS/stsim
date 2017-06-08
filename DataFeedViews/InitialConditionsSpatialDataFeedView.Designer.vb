<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class InitialConditionsSpatialDataFeedView
    Inherits SyncroSim.Core.Forms.DataFeedView

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TableLayoutPanelCalculated = New System.Windows.Forms.TableLayoutPanel()
        Me.TextBoxCellAreaCalc = New System.Windows.Forms.TextBox()
        Me.LabelCalcTtlAmount = New System.Windows.Forms.Label()
        Me.LabelCalcCellArea = New System.Windows.Forms.Label()
        Me.TextBoxTotalArea = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxNumCells = New System.Windows.Forms.TextBox()
        Me.CheckBoxCellSizeOverride = New System.Windows.Forms.CheckBox()
        Me.LabelFiles = New System.Windows.Forms.Label()
        Me.TableLayoutPanelAttributes = New System.Windows.Forms.TableLayoutPanel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxNumRows = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxNumColumns = New System.Windows.Forms.TextBox()
        Me.LabelRasterCellArea = New System.Windows.Forms.Label()
        Me.TextBoxCellArea = New System.Windows.Forms.TextBox()
        Me.PanelInitialConditionSpatialFiles = New System.Windows.Forms.Panel()
        Me.LabelValidate = New System.Windows.Forms.Label()
        Me.GroupBoxAttributes = New System.Windows.Forms.GroupBox()
        Me.GroupBoxCalculated = New System.Windows.Forms.GroupBox()
        Me.TableLayoutPanelCalculated.SuspendLayout()
        Me.TableLayoutPanelAttributes.SuspendLayout()
        Me.GroupBoxAttributes.SuspendLayout()
        Me.GroupBoxCalculated.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanelCalculated
        '
        Me.TableLayoutPanelCalculated.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanelCalculated.ColumnCount = 2
        Me.TableLayoutPanelCalculated.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117.0!))
        Me.TableLayoutPanelCalculated.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 283.0!))
        Me.TableLayoutPanelCalculated.Controls.Add(Me.TextBoxCellAreaCalc, 1, 0)
        Me.TableLayoutPanelCalculated.Controls.Add(Me.LabelCalcTtlAmount, 0, 2)
        Me.TableLayoutPanelCalculated.Controls.Add(Me.LabelCalcCellArea, 0, 0)
        Me.TableLayoutPanelCalculated.Controls.Add(Me.TextBoxTotalArea, 1, 2)
        Me.TableLayoutPanelCalculated.Controls.Add(Me.Label7, 0, 1)
        Me.TableLayoutPanelCalculated.Controls.Add(Me.TextBoxNumCells, 1, 1)
        Me.TableLayoutPanelCalculated.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize
        Me.TableLayoutPanelCalculated.Location = New System.Drawing.Point(24, 33)
        Me.TableLayoutPanelCalculated.Name = "TableLayoutPanelCalculated"
        Me.TableLayoutPanelCalculated.RowCount = 3
        Me.TableLayoutPanelCalculated.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanelCalculated.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanelCalculated.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanelCalculated.Size = New System.Drawing.Size(230, 85)
        Me.TableLayoutPanelCalculated.TabIndex = 0
        '
        'TextBoxCellAreaCalc
        '
        Me.TextBoxCellAreaCalc.Location = New System.Drawing.Point(120, 3)
        Me.TextBoxCellAreaCalc.Name = "TextBoxCellAreaCalc"
        Me.TextBoxCellAreaCalc.ReadOnly = True
        Me.TextBoxCellAreaCalc.Size = New System.Drawing.Size(103, 20)
        Me.TextBoxCellAreaCalc.TabIndex = 1
        Me.TextBoxCellAreaCalc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LabelCalcTtlAmount
        '
        Me.LabelCalcTtlAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelCalcTtlAmount.AutoSize = True
        Me.LabelCalcTtlAmount.Location = New System.Drawing.Point(42, 61)
        Me.LabelCalcTtlAmount.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        Me.LabelCalcTtlAmount.Name = "LabelCalcTtlAmount"
        Me.LabelCalcTtlAmount.Size = New System.Drawing.Size(72, 13)
        Me.LabelCalcTtlAmount.TabIndex = 4
        Me.LabelCalcTtlAmount.Text = "Total amount:"
        Me.LabelCalcTtlAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LabelCalcCellArea
        '
        Me.LabelCalcCellArea.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelCalcCellArea.AutoSize = True
        Me.LabelCalcCellArea.Location = New System.Drawing.Point(66, 5)
        Me.LabelCalcCellArea.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        Me.LabelCalcCellArea.Name = "LabelCalcCellArea"
        Me.LabelCalcCellArea.Size = New System.Drawing.Size(48, 13)
        Me.LabelCalcCellArea.TabIndex = 0
        Me.LabelCalcCellArea.Text = "Cell size:"
        Me.LabelCalcCellArea.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TextBoxTotalArea
        '
        Me.TextBoxTotalArea.Location = New System.Drawing.Point(120, 59)
        Me.TextBoxTotalArea.Name = "TextBoxTotalArea"
        Me.TextBoxTotalArea.ReadOnly = True
        Me.TextBoxTotalArea.Size = New System.Drawing.Size(103, 20)
        Me.TextBoxTotalArea.TabIndex = 5
        Me.TextBoxTotalArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(31, 33)
        Me.Label7.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(83, 13)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "Number of cells:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TextBoxNumCells
        '
        Me.TextBoxNumCells.Location = New System.Drawing.Point(120, 31)
        Me.TextBoxNumCells.Name = "TextBoxNumCells"
        Me.TextBoxNumCells.ReadOnly = True
        Me.TextBoxNumCells.Size = New System.Drawing.Size(103, 20)
        Me.TextBoxNumCells.TabIndex = 3
        Me.TextBoxNumCells.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CheckBoxCellSizeOverride
        '
        Me.CheckBoxCellSizeOverride.AutoSize = True
        Me.CheckBoxCellSizeOverride.Location = New System.Drawing.Point(263, 44)
        Me.CheckBoxCellSizeOverride.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxCellSizeOverride.Name = "CheckBoxCellSizeOverride"
        Me.CheckBoxCellSizeOverride.Size = New System.Drawing.Size(66, 17)
        Me.CheckBoxCellSizeOverride.TabIndex = 1
        Me.CheckBoxCellSizeOverride.Text = "Override"
        Me.CheckBoxCellSizeOverride.UseVisualStyleBackColor = True
        '
        'LabelFiles
        '
        Me.LabelFiles.AutoSize = True
        Me.LabelFiles.Location = New System.Drawing.Point(0, 8)
        Me.LabelFiles.Name = "LabelFiles"
        Me.LabelFiles.Size = New System.Drawing.Size(62, 13)
        Me.LabelFiles.TabIndex = 0
        Me.LabelFiles.Text = "Raster files:"
        '
        'TableLayoutPanelAttributes
        '
        Me.TableLayoutPanelAttributes.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanelAttributes.ColumnCount = 2
        Me.TableLayoutPanelAttributes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 119.0!))
        Me.TableLayoutPanelAttributes.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanelAttributes.Controls.Add(Me.Label2, 0, 0)
        Me.TableLayoutPanelAttributes.Controls.Add(Me.TextBoxNumRows, 1, 0)
        Me.TableLayoutPanelAttributes.Controls.Add(Me.Label1, 0, 1)
        Me.TableLayoutPanelAttributes.Controls.Add(Me.TextBoxNumColumns, 1, 1)
        Me.TableLayoutPanelAttributes.Controls.Add(Me.LabelRasterCellArea, 0, 2)
        Me.TableLayoutPanelAttributes.Controls.Add(Me.TextBoxCellArea, 1, 2)
        Me.TableLayoutPanelAttributes.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize
        Me.TableLayoutPanelAttributes.Location = New System.Drawing.Point(22, 33)
        Me.TableLayoutPanelAttributes.Name = "TableLayoutPanelAttributes"
        Me.TableLayoutPanelAttributes.RowCount = 3
        Me.TableLayoutPanelAttributes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanelAttributes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanelAttributes.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanelAttributes.Size = New System.Drawing.Size(230, 85)
        Me.TableLayoutPanelAttributes.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(32, 5)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Number of rows:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TextBoxNumRows
        '
        Me.TextBoxNumRows.Location = New System.Drawing.Point(122, 3)
        Me.TextBoxNumRows.Name = "TextBoxNumRows"
        Me.TextBoxNumRows.ReadOnly = True
        Me.TextBoxNumRows.Size = New System.Drawing.Size(103, 20)
        Me.TextBoxNumRows.TabIndex = 1
        Me.TextBoxNumRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 33)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(101, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Number of columns:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TextBoxNumColumns
        '
        Me.TextBoxNumColumns.Location = New System.Drawing.Point(122, 31)
        Me.TextBoxNumColumns.Name = "TextBoxNumColumns"
        Me.TextBoxNumColumns.ReadOnly = True
        Me.TextBoxNumColumns.Size = New System.Drawing.Size(103, 20)
        Me.TextBoxNumColumns.TabIndex = 3
        Me.TextBoxNumColumns.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LabelRasterCellArea
        '
        Me.LabelRasterCellArea.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelRasterCellArea.AutoSize = True
        Me.LabelRasterCellArea.Location = New System.Drawing.Point(68, 61)
        Me.LabelRasterCellArea.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        Me.LabelRasterCellArea.Name = "LabelRasterCellArea"
        Me.LabelRasterCellArea.Size = New System.Drawing.Size(48, 13)
        Me.LabelRasterCellArea.TabIndex = 4
        Me.LabelRasterCellArea.Text = "Cell size:"
        Me.LabelRasterCellArea.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TextBoxCellArea
        '
        Me.TextBoxCellArea.Location = New System.Drawing.Point(122, 59)
        Me.TextBoxCellArea.Name = "TextBoxCellArea"
        Me.TextBoxCellArea.ReadOnly = True
        Me.TextBoxCellArea.Size = New System.Drawing.Size(103, 20)
        Me.TextBoxCellArea.TabIndex = 5
        Me.TextBoxCellArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'PanelInitialConditionSpatialFiles
        '
        Me.PanelInitialConditionSpatialFiles.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelInitialConditionSpatialFiles.Location = New System.Drawing.Point(3, 31)
        Me.PanelInitialConditionSpatialFiles.Name = "PanelInitialConditionSpatialFiles"
        Me.PanelInitialConditionSpatialFiles.Size = New System.Drawing.Size(693, 190)
        Me.PanelInitialConditionSpatialFiles.TabIndex = 2
        '
        'LabelValidate
        '
        Me.LabelValidate.AutoSize = True
        Me.LabelValidate.Location = New System.Drawing.Point(0, 388)
        Me.LabelValidate.Name = "LabelValidate"
        Me.LabelValidate.Size = New System.Drawing.Size(159, 13)
        Me.LabelValidate.TabIndex = 1
        Me.LabelValidate.Text = "Validating rasters.  Please wait..."
        '
        'GroupBoxAttributes
        '
        Me.GroupBoxAttributes.Controls.Add(Me.TableLayoutPanelAttributes)
        Me.GroupBoxAttributes.Location = New System.Drawing.Point(3, 237)
        Me.GroupBoxAttributes.Name = "GroupBoxAttributes"
        Me.GroupBoxAttributes.Size = New System.Drawing.Size(279, 141)
        Me.GroupBoxAttributes.TabIndex = 3
        Me.GroupBoxAttributes.TabStop = False
        Me.GroupBoxAttributes.Text = "Raster file attributes"
        '
        'GroupBoxCalculated
        '
        Me.GroupBoxCalculated.Controls.Add(Me.TableLayoutPanelCalculated)
        Me.GroupBoxCalculated.Controls.Add(Me.CheckBoxCellSizeOverride)
        Me.GroupBoxCalculated.Location = New System.Drawing.Point(309, 237)
        Me.GroupBoxCalculated.Name = "GroupBoxCalculated"
        Me.GroupBoxCalculated.Size = New System.Drawing.Size(347, 141)
        Me.GroupBoxCalculated.TabIndex = 4
        Me.GroupBoxCalculated.TabStop = False
        Me.GroupBoxCalculated.Text = "Calculated values"
        '
        'InitialConditionsSpatialDataFeedView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.GroupBoxCalculated)
        Me.Controls.Add(Me.GroupBoxAttributes)
        Me.Controls.Add(Me.LabelValidate)
        Me.Controls.Add(Me.PanelInitialConditionSpatialFiles)
        Me.Controls.Add(Me.LabelFiles)
        Me.Name = "InitialConditionsSpatialDataFeedView"
        Me.Size = New System.Drawing.Size(699, 412)
        Me.TableLayoutPanelCalculated.ResumeLayout(False)
        Me.TableLayoutPanelCalculated.PerformLayout()
        Me.TableLayoutPanelAttributes.ResumeLayout(False)
        Me.TableLayoutPanelAttributes.PerformLayout()
        Me.GroupBoxAttributes.ResumeLayout(False)
        Me.GroupBoxCalculated.ResumeLayout(False)
        Me.GroupBoxCalculated.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanelCalculated As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TextBoxCellAreaCalc As System.Windows.Forms.TextBox
    Friend WithEvents LabelCalcTtlAmount As System.Windows.Forms.Label
    Friend WithEvents LabelCalcCellArea As System.Windows.Forms.Label
    Friend WithEvents TextBoxTotalArea As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNumCells As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxCellSizeOverride As System.Windows.Forms.CheckBox
    Friend WithEvents LabelFiles As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanelAttributes As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNumRows As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNumColumns As System.Windows.Forms.TextBox
    Friend WithEvents LabelRasterCellArea As System.Windows.Forms.Label
    Friend WithEvents TextBoxCellArea As System.Windows.Forms.TextBox
    Friend WithEvents PanelInitialConditionSpatialFiles As System.Windows.Forms.Panel
    Friend WithEvents LabelValidate As System.Windows.Forms.Label
    Friend WithEvents GroupBoxAttributes As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBoxCalculated As System.Windows.Forms.GroupBox
End Class
