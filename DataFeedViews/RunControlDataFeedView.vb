'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Imports SyncroSim.Core
Imports System.Globalization
Imports System.Reflection

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class RunControlDataFeedView

    Public Overrides Sub LoadDataFeed(dataFeed As DataFeed)

        MyBase.LoadDataFeed(dataFeed)

        Me.SetTextBoxBinding(Me.TextBoxStartTimestep, "MinimumTimestep")
        Me.SetTextBoxBinding(Me.TextBoxEndTimestep, "MaximumTimestep")
        Me.SetTextBoxBinding(Me.TextBoxTotalIterations, "MaximumIteration")
        Me.SetCheckBoxBinding(Me.CheckBoxIsSpatial, "IsSpatial")

        Me.MonitorDataSheet(
          DATASHEET_TERMINOLOGY_NAME,
          AddressOf Me.OnTerminologyChanged,
          True)

    End Sub

    Private Sub OnTerminologyChanged(ByVal e As DataSheetMonitorEventArgs)

        Dim t As String = CStr(e.GetValue(
            "TimestepUnits",
            "Timestep")).ToLower(CultureInfo.CurrentCulture)

        Me.LabelStartTimestep.Text = String.Format(CultureInfo.CurrentCulture, "Start {0}:", t)
        Me.LabelEndTimestep.Text = String.Format(CultureInfo.CurrentCulture, "End {0}:", t)

    End Sub

    Private Sub ButtonClearAll_Click(sender As Object, e As EventArgs) Handles ButtonClearAll.Click

        Me.ResetBoundControls()
        Me.ClearDataSheets()

    End Sub

End Class
