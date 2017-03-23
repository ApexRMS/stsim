<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProcessingDataFeedView
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
        Me.CheckBoxSplitSecStrat = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        '
        'CheckBoxSplitSecStrat
        '
        Me.CheckBoxSplitSecStrat.AutoSize = True
        Me.CheckBoxSplitSecStrat.Location = New System.Drawing.Point(13, 13)
        Me.CheckBoxSplitSecStrat.Name = "CheckBoxSplitSecStrat"
        Me.CheckBoxSplitSecStrat.Size = New System.Drawing.Size(255, 17)
        Me.CheckBoxSplitSecStrat.TabIndex = 0
        Me.CheckBoxSplitSecStrat.Text = "Split jobs for non-spatial runs by secondary strata"
        Me.CheckBoxSplitSecStrat.UseVisualStyleBackColor = True
        '
        'ProcessingDataFeedView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.CheckBoxSplitSecStrat)
        Me.Name = "ProcessingDataFeedView"
        Me.Size = New System.Drawing.Size(344, 160)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CheckBoxSplitSecStrat As System.Windows.Forms.CheckBox

End Class
