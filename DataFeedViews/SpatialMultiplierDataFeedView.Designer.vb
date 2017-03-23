<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SpatialMultiplierDataFeedView
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
        Me.PanelMultipliersGrid = New System.Windows.Forms.Panel()
        Me.SuspendLayout()
        '
        'PanelMultipliersGrid
        '
        Me.PanelMultipliersGrid.BackColor = System.Drawing.Color.White
        Me.PanelMultipliersGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelMultipliersGrid.Location = New System.Drawing.Point(0, 0)
        Me.PanelMultipliersGrid.Name = "PanelMultipliersGrid"
        Me.PanelMultipliersGrid.Size = New System.Drawing.Size(432, 241)
        Me.PanelMultipliersGrid.TabIndex = 1
        '
        'SpatialMultiplierDataFeedView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.PanelMultipliersGrid)
        Me.Name = "SpatialMultiplierDataFeedView"
        Me.Size = New System.Drawing.Size(432, 241)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelMultipliersGrid As System.Windows.Forms.Panel

End Class
