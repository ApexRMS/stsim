'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core
Imports System.Globalization
Imports System.Reflection

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
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
