'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports System.Reflection
Imports System.Globalization
Imports SyncroSim.Core

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class OutputOptionsDataFeedView

    Const DEFAULT_TIMESTEP_VALUE As String = "1"

    Public Overrides Sub LoadDataFeed(dataFeed As DataFeed)

        MyBase.LoadDataFeed(dataFeed)

        Me.SetCheckBoxBinding(Me.CheckBoxSummarySC, DATASHEET_OO_SUMMARY_OUTPUT_SC_COLUMN_NAME)
        Me.SetTextBoxBinding(Me.TextBoxSummarySCTimesteps, DATASHEET_OO_SUMMARY_OUTPUT_SC_TIMESTEPS_COLUMN_NAME)
        Me.SetCheckBoxBinding(Me.CheckBoxSummarySCZeroValues, DATASHEET_OO_SUMMARY_OUTPUT_SC_ZERO_VALUES_COLUMN_NAME)
        Me.SetCheckBoxBinding(Me.CheckBoxSummaryTR, DATASHEET_OO_SUMMARY_OUTPUT_TR_COLUMN_NAME)
        Me.SetTextBoxBinding(Me.TextBoxSummaryTRTimesteps, DATASHEET_OO_SUMMARY_OUTPUT_TR_TIMESTEPS_COLUMN_NAME)
        Me.SetCheckBoxBinding(Me.CheckBoxSummaryTRCalcIntervalMean, DATASHEET_OO_SUMMARY_OUTPUT_TR_INTERVAL_MEAN_COLUMN_NAME)
        Me.SetCheckBoxBinding(Me.CheckBoxSummaryTRSC, DATASHEET_OO_SUMMARY_OUTPUT_TRSC_COLUMN_NAME)
        Me.SetTextBoxBinding(Me.TextBoxSummaryTRSCTimesteps, DATASHEET_OO_SUMMARY_OUTPUT_TRSC_TIMESTEPS_COLUMN_NAME)
        Me.SetCheckBoxBinding(Me.CheckBoxSummarySA, DATASHEET_OO_SUMMARY_OUTPUT_SA_COLUMN_NAME)
        Me.SetTextBoxBinding(Me.TextBoxSummarySATimesteps, DATASHEET_OO_SUMMARY_OUTPUT_SA_TIMESTEPS_COLUMN_NAME)
        Me.SetCheckBoxBinding(Me.CheckBoxSummaryTA, DATASHEET_OO_SUMMARY_OUTPUT_TA_COLUMN_NAME)
        Me.SetTextBoxBinding(Me.TextBoxSummaryTATimesteps, DATASHEET_OO_SUMMARY_OUTPUT_TA_TIMESTEPS_COLUMN_NAME)
        Me.SetCheckBoxBinding(Me.CheckBoxRasterSC, DATASHEET_OO_RASTER_OUTPUT_SC_COLUMN_NAME)
        Me.SetTextBoxBinding(Me.TextBoxRasterSCTimesteps, DATASHEET_OO_RASTER_OUTPUT_SC_TIMESTEPS_COLUMN_NAME)
        Me.SetCheckBoxBinding(Me.CheckBoxRasterTR, DATASHEET_OO_RASTER_OUTPUT_TR_COLUMN_NAME)
        Me.SetTextBoxBinding(Me.TextBoxRasterTRTimesteps, DATASHEET_OO_RASTER_OUTPUT_TR_TIMESTEPS_COLUMN_NAME)
        Me.SetCheckBoxBinding(Me.CheckBoxRasterAge, DATASHEET_OO_RASTER_OUTPUT_AGE_COLUMN_NAME)
        Me.SetTextBoxBinding(Me.TextBoxRasterAgeTimesteps, DATASHEET_OO_RASTER_OUTPUT_AGE_TIMESTEPS_COLUMN_NAME)
        Me.SetCheckBoxBinding(Me.CheckBoxRasterTST, DATASHEET_OO_RASTER_OUTPUT_TST_COLUMN_NAME)
        Me.SetTextBoxBinding(Me.TextBoxRasterTSTTimesteps, DATASHEET_OO_RASTER_OUTPUT_TST_TIMESTEPS_COLUMN_NAME)
        Me.SetCheckBoxBinding(Me.CheckBoxRasterST, DATASHEET_OO_RASTER_OUTPUT_ST_COLUMN_NAME)
        Me.SetTextBoxBinding(Me.TextBoxRasterSTTimesteps, DATASHEET_OO_RASTER_OUTPUT_ST_TIMESTEPS_COLUMN_NAME)
        Me.SetCheckBoxBinding(Me.CheckBoxRasterSA, DATASHEET_OO_RASTER_OUTPUT_SA_COLUMN_NAME)
        Me.SetTextBoxBinding(Me.TextBoxRasterSATimesteps, DATASHEET_OO_RASTER_OUTPUT_SA_TIMESTEPS_COLUMN_NAME)
        Me.SetCheckBoxBinding(Me.CheckBoxRasterTA, DATASHEET_OO_RASTER_OUTPUT_TA_COLUMN_NAME)
        Me.SetTextBoxBinding(Me.TextBoxRasterTATimesteps, DATASHEET_OO_RASTER_OUTPUT_TA_TIMESTEPS_COLUMN_NAME)
        Me.SetCheckBoxBinding(Me.CheckBoxRasterAATP, DATASHEET_OO_RASTER_OUTPUT_AATP_COLUMN_NAME)
        Me.SetTextBoxBinding(Me.TextBoxRasterAATPTimesteps, DATASHEET_OO_RASTER_OUTPUT_AATP_TIMESTEPS_COLUMN_NAME)

        Me.MonitorDataSheet(DATASHEET_TERMINOLOGY_NAME, AddressOf Me.OnTerminologyChanged, True)
        Me.AddStandardCommands()

    End Sub

    Public Overrides Sub EnableView(enable As Boolean)

        MyBase.EnableView(enable)

        If (enable) Then
            Me.EnableControls()
        End If

    End Sub

    Protected Overrides Sub OnRowsAdded(sender As Object, e As DataSheetRowEventArgs)

        MyBase.OnRowsAdded(sender, e)

        If (Me.ShouldEnableView()) Then
            Me.EnableControls()
        End If

    End Sub

    Protected Overrides Sub OnRowsDeleted(sender As Object, e As DataSheetRowEventArgs)

        MyBase.OnRowsDeleted(sender, e)

        If (Me.ShouldEnableView()) Then
            Me.EnableControls()
        End If

    End Sub

    Private Sub OnTerminologyChanged(ByVal e As DataSheetMonitorEventArgs)

        Dim t As String = CStr(e.GetValue("TimestepUnits", "Timestep")).ToLower(CultureInfo.InvariantCulture)

        Me.LabelSummarySCTimesteps.Text = t
        Me.LabelSummaryTRTimesteps.Text = t
        Me.LabelSummaryTRSCTimesteps.Text = t
        Me.LabelSummarySATimesteps.Text = t
        Me.LabelSummaryTATimesteps.Text = t
        Me.LabelRasterSCTimesteps.Text = t
        Me.LabelRasterTRTimesteps.Text = t
        Me.LabelRasterAgeTimesteps.Text = t
        Me.LabelRasterTSTTimesteps.Text = t
        Me.LabelRasterSTTimesteps.Text = t
        Me.LabelRasterSATimesteps.Text = t
        Me.LabelRasterTATimesteps.Text = t
        Me.LabelRasterAATPTimesteps.Text = t

    End Sub

    Private Sub EnableControls()

        'Text Boxes
        Me.TextBoxSummarySCTimesteps.Enabled = Me.CheckBoxSummarySC.Checked
        Me.TextBoxSummaryTRTimesteps.Enabled = Me.CheckBoxSummaryTR.Checked
        Me.TextBoxSummaryTRSCTimesteps.Enabled = Me.CheckBoxSummaryTRSC.Checked
        Me.TextBoxSummarySATimesteps.Enabled = Me.CheckBoxSummarySA.Checked
        Me.TextBoxSummaryTATimesteps.Enabled = Me.CheckBoxSummaryTA.Checked
        Me.TextBoxRasterSCTimesteps.Enabled = Me.CheckBoxRasterSC.Checked
        Me.TextBoxRasterTRTimesteps.Enabled = Me.CheckBoxRasterTR.Checked
        Me.TextBoxRasterAgeTimesteps.Enabled = Me.CheckBoxRasterAge.Checked
        Me.TextBoxRasterTSTTimesteps.Enabled = Me.CheckBoxRasterTST.Checked
        Me.TextBoxRasterSTTimesteps.Enabled = Me.CheckBoxRasterST.Checked
        Me.TextBoxRasterSATimesteps.Enabled = Me.CheckBoxRasterSA.Checked
        Me.TextBoxRasterTATimesteps.Enabled = Me.CheckBoxRasterTA.Checked
        Me.TextBoxRasterAATPTimesteps.Enabled = Me.CheckBoxRasterAATP.Checked

        'Timesteps labels
        Me.LabelSummarySCTimesteps.Enabled = Me.CheckBoxSummarySC.Checked
        Me.LabelSummaryTRTimesteps.Enabled = Me.CheckBoxSummaryTR.Checked
        Me.LabelSummaryTRSCTimesteps.Enabled = Me.CheckBoxSummaryTRSC.Checked
        Me.LabelSummarySATimesteps.Enabled = Me.CheckBoxSummarySA.Checked
        Me.LabelSummaryTATimesteps.Enabled = Me.CheckBoxSummaryTA.Checked
        Me.LabelRasterSCTimesteps.Enabled = Me.CheckBoxRasterSC.Checked
        Me.LabelRasterTRTimesteps.Enabled = Me.CheckBoxRasterTR.Checked
        Me.LabelRasterAgeTimesteps.Enabled = Me.CheckBoxRasterAge.Checked
        Me.LabelRasterTSTTimesteps.Enabled = Me.CheckBoxRasterTST.Checked
        Me.LabelRasterSTTimesteps.Enabled = Me.CheckBoxRasterST.Checked
        Me.LabelRasterSATimesteps.Enabled = Me.CheckBoxRasterSA.Checked
        Me.LabelRasterTATimesteps.Enabled = Me.CheckBoxRasterTA.Checked
        Me.LabelRasterAATPTimesteps.Enabled = Me.CheckBoxRasterAATP.Checked

        'Checkboxes
        Me.CheckBoxSummarySCZeroValues.Enabled = Me.CheckBoxSummarySC.Checked
        Me.CheckBoxSummaryTRCalcIntervalMean.Enabled = Me.CheckBoxSummaryTR.Checked

    End Sub

    Protected Overrides Sub OnBoundCheckBoxChanged(checkBox As System.Windows.Forms.CheckBox, columnName As String)

        MyBase.OnBoundCheckBoxChanged(checkBox, columnName)

        If (checkBox Is Me.CheckBoxSummarySC And Me.CheckBoxSummarySC.Checked And String.IsNullOrEmpty(Me.TextBoxSummarySCTimesteps.Text)) Then
            Me.SetTextBoxData(Me.TextBoxSummarySCTimesteps, DEFAULT_TIMESTEP_VALUE)
        ElseIf (checkBox Is Me.CheckBoxSummaryTR And Me.CheckBoxSummaryTR.Checked And String.IsNullOrEmpty(Me.TextBoxSummaryTRTimesteps.Text)) Then
            Me.SetTextBoxData(Me.TextBoxSummaryTRTimesteps, DEFAULT_TIMESTEP_VALUE)
        ElseIf (checkBox Is Me.CheckBoxSummaryTRSC And Me.CheckBoxSummaryTRSC.Checked And String.IsNullOrEmpty(Me.TextBoxSummaryTRSCTimesteps.Text)) Then
            Me.SetTextBoxData(Me.TextBoxSummaryTRSCTimesteps, DEFAULT_TIMESTEP_VALUE)
        ElseIf (checkBox Is Me.CheckBoxSummarySA And Me.CheckBoxSummarySA.Checked And String.IsNullOrEmpty(Me.TextBoxSummarySATimesteps.Text)) Then
            Me.SetTextBoxData(Me.TextBoxSummarySATimesteps, DEFAULT_TIMESTEP_VALUE)
        ElseIf (checkBox Is Me.CheckBoxSummaryTA And Me.CheckBoxSummaryTA.Checked And String.IsNullOrEmpty(Me.TextBoxSummaryTATimesteps.Text)) Then
            Me.SetTextBoxData(Me.TextBoxSummaryTATimesteps, DEFAULT_TIMESTEP_VALUE)
        ElseIf (checkBox Is Me.CheckBoxRasterSC And Me.CheckBoxRasterSC.Checked And String.IsNullOrEmpty(Me.TextBoxRasterSCTimesteps.Text)) Then
            Me.SetTextBoxData(Me.TextBoxRasterSCTimesteps, DEFAULT_TIMESTEP_VALUE)
        ElseIf (checkBox Is Me.CheckBoxRasterTR And Me.CheckBoxRasterTR.Checked And String.IsNullOrEmpty(Me.TextBoxRasterTRTimesteps.Text)) Then
            Me.SetTextBoxData(Me.TextBoxRasterTRTimesteps, DEFAULT_TIMESTEP_VALUE)
        ElseIf (checkBox Is Me.CheckBoxRasterAge And Me.CheckBoxRasterAge.Checked And String.IsNullOrEmpty(Me.TextBoxRasterAgeTimesteps.Text)) Then
            Me.SetTextBoxData(Me.TextBoxRasterAgeTimesteps, DEFAULT_TIMESTEP_VALUE)
        ElseIf (checkBox Is Me.CheckBoxRasterTST And Me.CheckBoxRasterTST.Checked And String.IsNullOrEmpty(Me.TextBoxRasterTSTTimesteps.Text)) Then
            Me.SetTextBoxData(Me.TextBoxRasterTSTTimesteps, DEFAULT_TIMESTEP_VALUE)
        ElseIf (checkBox Is Me.CheckBoxRasterST And Me.CheckBoxRasterST.Checked And String.IsNullOrEmpty(Me.TextBoxRasterSTTimesteps.Text)) Then
            Me.SetTextBoxData(Me.TextBoxRasterSTTimesteps, DEFAULT_TIMESTEP_VALUE)
        ElseIf (checkBox Is Me.CheckBoxRasterSA And Me.CheckBoxRasterSA.Checked And String.IsNullOrEmpty(Me.TextBoxRasterSATimesteps.Text)) Then
            Me.SetTextBoxData(Me.TextBoxRasterSATimesteps, DEFAULT_TIMESTEP_VALUE)
        ElseIf (checkBox Is Me.CheckBoxRasterTA And Me.CheckBoxRasterTA.Checked And String.IsNullOrEmpty(Me.TextBoxRasterTATimesteps.Text)) Then
            Me.SetTextBoxData(Me.TextBoxRasterTATimesteps, DEFAULT_TIMESTEP_VALUE)
        ElseIf (checkBox Is Me.CheckBoxRasterAATP And Me.CheckBoxRasterAATP.Checked And String.IsNullOrEmpty(Me.TextBoxRasterAATPTimesteps.Text)) Then
            Me.SetTextBoxData(Me.TextBoxRasterAATPTimesteps, DEFAULT_TIMESTEP_VALUE)
        End If

        Me.EnableControls()

    End Sub

End Class
