<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InitialConditionsSpatialDataFeedView
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
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel()
        Me.TextBoxCellAreaCalc = New System.Windows.Forms.TextBox()
        Me.LabelCalcTtlAmount = New System.Windows.Forms.Label()
        Me.LabelCalcCellArea = New System.Windows.Forms.Label()
        Me.TextBoxTotalArea = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxNumCells = New System.Windows.Forms.TextBox()
        Me.CheckBoxCellSizeOverride = New System.Windows.Forms.CheckBox()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxNumRows = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxNumColumns = New System.Windows.Forms.TextBox()
        Me.LabelRasterCellArea = New System.Windows.Forms.Label()
        Me.TextBoxCellArea = New System.Windows.Forms.TextBox()
        Me.PanelInitialConditionSpatialFiles = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel2.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel2.ColumnCount = 3
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel2.Controls.Add(Me.TextBoxCellAreaCalc, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.LabelCalcTtlAmount, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.LabelCalcCellArea, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TextBoxTotalArea, 1, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.Label7, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.TextBoxNumCells, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.CheckBoxCellSizeOverride, 2, 0)
        Me.TableLayoutPanel2.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 446)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 3
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(662, 85)
        Me.TableLayoutPanel2.TabIndex = 8
        '
        'TextBoxCellAreaCalc
        '
        Me.TextBoxCellAreaCalc.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxCellAreaCalc.Location = New System.Drawing.Point(203, 3)
        Me.TextBoxCellAreaCalc.Name = "TextBoxCellAreaCalc"
        Me.TextBoxCellAreaCalc.ReadOnly = True
        Me.TextBoxCellAreaCalc.Size = New System.Drawing.Size(194, 20)
        Me.TextBoxCellAreaCalc.TabIndex = 1
        Me.TextBoxCellAreaCalc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LabelCalcTtlAmount
        '
        Me.LabelCalcTtlAmount.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelCalcTtlAmount.AutoSize = True
        Me.LabelCalcTtlAmount.Location = New System.Drawing.Point(125, 61)
        Me.LabelCalcTtlAmount.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        Me.LabelCalcTtlAmount.Name = "LabelCalcTtlAmount"
        Me.LabelCalcTtlAmount.Size = New System.Drawing.Size(72, 13)
        Me.LabelCalcTtlAmount.TabIndex = 5
        Me.LabelCalcTtlAmount.Text = "Total amount:"
        Me.LabelCalcTtlAmount.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LabelCalcCellArea
        '
        Me.LabelCalcCellArea.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelCalcCellArea.AutoSize = True
        Me.LabelCalcCellArea.Location = New System.Drawing.Point(149, 5)
        Me.LabelCalcCellArea.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        Me.LabelCalcCellArea.Name = "LabelCalcCellArea"
        Me.LabelCalcCellArea.Size = New System.Drawing.Size(48, 13)
        Me.LabelCalcCellArea.TabIndex = 0
        Me.LabelCalcCellArea.Text = "Cell size:"
        Me.LabelCalcCellArea.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TextBoxTotalArea
        '
        Me.TextBoxTotalArea.Location = New System.Drawing.Point(203, 59)
        Me.TextBoxTotalArea.Name = "TextBoxTotalArea"
        Me.TextBoxTotalArea.ReadOnly = True
        Me.TextBoxTotalArea.Size = New System.Drawing.Size(194, 20)
        Me.TextBoxTotalArea.TabIndex = 6
        Me.TextBoxTotalArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(114, 33)
        Me.Label7.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(83, 13)
        Me.Label7.TabIndex = 3
        Me.Label7.Text = "Number of cells:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TextBoxNumCells
        '
        Me.TextBoxNumCells.Location = New System.Drawing.Point(203, 31)
        Me.TextBoxNumCells.Name = "TextBoxNumCells"
        Me.TextBoxNumCells.ReadOnly = True
        Me.TextBoxNumCells.Size = New System.Drawing.Size(194, 20)
        Me.TextBoxNumCells.TabIndex = 4
        Me.TextBoxNumCells.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'CheckBoxCellSizeOverride
        '
        Me.CheckBoxCellSizeOverride.AutoSize = True
        Me.CheckBoxCellSizeOverride.Location = New System.Drawing.Point(402, 2)
        Me.CheckBoxCellSizeOverride.Margin = New System.Windows.Forms.Padding(2)
        Me.CheckBoxCellSizeOverride.Name = "CheckBoxCellSizeOverride"
        Me.CheckBoxCellSizeOverride.Size = New System.Drawing.Size(66, 17)
        Me.CheckBoxCellSizeOverride.TabIndex = 2
        Me.CheckBoxCellSizeOverride.Text = "Override"
        Me.CheckBoxCellSizeOverride.UseVisualStyleBackColor = True
        '
        'Panel9
        '
        Me.Panel9.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel9.BackColor = System.Drawing.Color.LightBlue
        Me.Panel9.Location = New System.Drawing.Point(3, 436)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(662, 2)
        Me.Panel9.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(0, 417)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(94, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Calculated values:"
        '
        'Panel7
        '
        Me.Panel7.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel7.BackColor = System.Drawing.Color.LightBlue
        Me.Panel7.Location = New System.Drawing.Point(3, 313)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(662, 2)
        Me.Panel7.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(0, 294)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(103, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Raster file attributes:"
        '
        'Panel8
        '
        Me.Panel8.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel8.BackColor = System.Drawing.Color.LightBlue
        Me.Panel8.Location = New System.Drawing.Point(3, 26)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(660, 2)
        Me.Panel8.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(0, 8)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(62, 13)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Raster files:"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.BackColor = System.Drawing.Color.White
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBoxNumRows, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBoxNumColumns, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LabelRasterCellArea, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.TextBoxCellArea, 1, 2)
        Me.TableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(2, 323)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(663, 85)
        Me.TableLayoutPanel1.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(113, 5)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Number of rows:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TextBoxNumRows
        '
        Me.TextBoxNumRows.Location = New System.Drawing.Point(203, 3)
        Me.TextBoxNumRows.Name = "TextBoxNumRows"
        Me.TextBoxNumRows.ReadOnly = True
        Me.TextBoxNumRows.Size = New System.Drawing.Size(195, 20)
        Me.TextBoxNumRows.TabIndex = 1
        Me.TextBoxNumRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(96, 33)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(101, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Number of columns:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TextBoxNumColumns
        '
        Me.TextBoxNumColumns.Location = New System.Drawing.Point(203, 31)
        Me.TextBoxNumColumns.Name = "TextBoxNumColumns"
        Me.TextBoxNumColumns.ReadOnly = True
        Me.TextBoxNumColumns.Size = New System.Drawing.Size(195, 20)
        Me.TextBoxNumColumns.TabIndex = 3
        Me.TextBoxNumColumns.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LabelRasterCellArea
        '
        Me.LabelRasterCellArea.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LabelRasterCellArea.AutoSize = True
        Me.LabelRasterCellArea.Location = New System.Drawing.Point(149, 61)
        Me.LabelRasterCellArea.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        Me.LabelRasterCellArea.Name = "LabelRasterCellArea"
        Me.LabelRasterCellArea.Size = New System.Drawing.Size(48, 13)
        Me.LabelRasterCellArea.TabIndex = 4
        Me.LabelRasterCellArea.Text = "Cell size:"
        Me.LabelRasterCellArea.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TextBoxCellArea
        '
        Me.TextBoxCellArea.Location = New System.Drawing.Point(203, 59)
        Me.TextBoxCellArea.Name = "TextBoxCellArea"
        Me.TextBoxCellArea.ReadOnly = True
        Me.TextBoxCellArea.Size = New System.Drawing.Size(195, 20)
        Me.TextBoxCellArea.TabIndex = 5
        Me.TextBoxCellArea.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'PanelInitialConditionSpatialFiles
        '
        Me.PanelInitialConditionSpatialFiles.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PanelInitialConditionSpatialFiles.Location = New System.Drawing.Point(3, 40)
        Me.PanelInitialConditionSpatialFiles.Name = "PanelInitialConditionSpatialFiles"
        Me.PanelInitialConditionSpatialFiles.Size = New System.Drawing.Size(662, 243)
        Me.PanelInitialConditionSpatialFiles.TabIndex = 2
        '
        'InitialConditionsSpatialDataFeedView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.PanelInitialConditionSpatialFiles)
        Me.Controls.Add(Me.TableLayoutPanel2)
        Me.Controls.Add(Me.Panel9)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "InitialConditionsSpatialDataFeedView"
        Me.Size = New System.Drawing.Size(668, 534)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TextBoxCellAreaCalc As System.Windows.Forms.TextBox
    Friend WithEvents LabelCalcTtlAmount As System.Windows.Forms.Label
    Friend WithEvents LabelCalcCellArea As System.Windows.Forms.Label
    Friend WithEvents TextBoxTotalArea As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNumCells As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxCellSizeOverride As System.Windows.Forms.CheckBox
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNumRows As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBoxNumColumns As System.Windows.Forms.TextBox
    Friend WithEvents LabelRasterCellArea As System.Windows.Forms.Label
    Friend WithEvents TextBoxCellArea As System.Windows.Forms.TextBox
    Friend WithEvents PanelInitialConditionSpatialFiles As System.Windows.Forms.Panel

End Class
