'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.Core
Imports System.Globalization

Class TransitionSizeDistributionDataSheet
    Inherits DataSheet

    Protected Overrides Sub OnDataSheetChanged(e As DataSheetMonitorEventArgs)

        MyBase.OnDataSheetChanged(e)

        Dim AmountLabel As String = Nothing
        Dim AmountUnits As TerminologyUnit = TerminologyUnit.None

        GetAmountLabelTerminology(e.DataSheet, AmountLabel, AmountUnits)

        Me.Columns(DATASHEET_TRANSITION_SIZE_DISTRIBUTION_MAXIMUM_AREA_COLUMN_NAME).DisplayName =
            (String.Format(CultureInfo.CurrentCulture, "Maximum {0} ({1})",
            AmountLabel, TerminologyUnitToString(AmountUnits)))

    End Sub

End Class
