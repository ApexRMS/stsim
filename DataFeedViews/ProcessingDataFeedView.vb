'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.Core
Imports System.Globalization
Imports System.Reflection

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class ProcessingDataFeedView

    Public Overrides Sub LoadDataFeed(dataFeed As DataFeed)

        MyBase.LoadDataFeed(dataFeed)
        Me.SetCheckBoxBinding(Me.CheckBoxSplitSecStrat, DATASHEET_PROCESSING_SPLIT_BY_SS_COLUMN_NAME)

        Me.MonitorDataSheet(
          DATASHEET_TERMINOLOGY_NAME,
          AddressOf Me.OnTerminologyChanged,
          True)

    End Sub

    Private Sub OnTerminologyChanged(ByVal e As DataSheetMonitorEventArgs)

        Me.CheckBoxSplitSecStrat.Text = String.Format(
            CultureInfo.CurrentCulture,
            "Split non-spatial runs by {0}",
            CStr(e.GetValue(DATASHEET_TERMINOLOGY_SECONDARY_STRATUM_LABEL_COLUMN_NAME)).ToLower(CultureInfo.CurrentCulture))

    End Sub

End Class
