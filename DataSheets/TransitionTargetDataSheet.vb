'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.Core
Imports System.Globalization

Class TransitionTargetDataSheet
    Inherits DataSheet

    Protected Overrides Sub OnDataSheetChanged(e As DataSheetMonitorEventArgs)

        MyBase.OnDataSheetChanged(e)

        Dim AmountLabel As String = Nothing
        Dim AmountUnits As TerminologyUnit = TerminologyUnit.None

        GetAmountLabelTerminology(e.DataSheet, AmountLabel, AmountUnits)

        Me.Columns(DATASHEET_AMOUNT_COLUMN_NAME).DisplayName =
            (String.Format(CultureInfo.CurrentCulture, "Target {0} ({1})",
            AmountLabel, TerminologyUnitToString(AmountUnits)))

        Me.Columns(DATASHEET_DISTRIBUTIONTYPE_COLUMN_NAME).DisplayName = String.Format(CultureInfo.CurrentCulture, "Target {0} Distribution", AmountLabel)
        Me.Columns(DATASHEET_DISTRIBUTION_FREQUENCY_COLUMN_NAME).DisplayName = String.Format(CultureInfo.CurrentCulture, "Target {0} Sampling Frequency", AmountLabel)
        Me.Columns(DATASHEET_DISTRIBUTIONSD_COLUMN_NAME).DisplayName = String.Format(CultureInfo.CurrentCulture, "Target {0} SD", AmountLabel)
        Me.Columns(DATASHEET_DISTRIBUTIONMIN_COLUMN_NAME).DisplayName = String.Format(CultureInfo.CurrentCulture, "Target {0} Min", AmountLabel)
        Me.Columns(DATASHEET_DISTRIBUTIONMAX_COLUMN_NAME).DisplayName = String.Format(CultureInfo.CurrentCulture, "Target {0} Max", AmountLabel)

    End Sub

End Class
