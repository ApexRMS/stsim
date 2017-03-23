'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.Core
Imports System.Globalization
Imports SyncroSim.Core.Forms

Class TransitionStateSummaryReport
    Inherits ExportTransformer

    Protected Overrides Sub Export(location As String, exportType As ExportType)
        Me.InternalExport(location, exportType, True)
    End Sub

    Friend Sub InternalExport(location As String, exportType As ExportType, showMessage As Boolean)

        Dim AmountLabel As String = Nothing
        Dim TermUnit As TerminologyUnit
        Dim dsterm As DataSheet = Me.Project.GetDataSheet(DATASHEET_TERMINOLOGY_NAME)
        Dim columns As ExportColumnCollection = Me.CreateColumnCollection()

        GetAmountLabelTerminology(dsterm, AmountLabel, TermUnit)

        Dim WorksheetName As String = String.Format(CultureInfo.InvariantCulture, "{0} by Transition and State", AmountLabel)

        If (exportType = ExportType.ExcelFile) Then
            Me.ExcelExport(location, columns, Me.CreateReportQuery(False), WorksheetName)
        Else

            columns.Remove("ScenarioName")
            Me.CSVExport(location, columns, Me.CreateReportQuery(True))

            If (showMessage) Then
                FormsUtilities.InformationMessageBox("Data saved to '{0}'.", location)
            End If

        End If

    End Sub

    Private Function CreateColumnCollection() As ExportColumnCollection

        Dim c As New ExportColumnCollection()

        Dim AmountLabel As String = Nothing
        Dim UnitsLabel As String = Nothing
        Dim TermUnit As TerminologyUnit
        Dim PrimaryStratumLabel As String = Nothing
        Dim SecondaryStratumLabel As String = Nothing
        Dim dsterm As DataSheet = Me.Project.GetDataSheet(DATASHEET_TERMINOLOGY_NAME)
        Dim TimestepLabel As String = GetTimestepUnits(Me.Project)

        GetAmountLabelTerminology(dsterm, AmountLabel, TermUnit)
        GetStratumLabelTerminology(dsterm, PrimaryStratumLabel, SecondaryStratumLabel)
        UnitsLabel = TerminologyUnitToString(TermUnit)

        Dim AmountTitle As String = String.Format(CultureInfo.InvariantCulture, "{0} ({1})", AmountLabel, UnitsLabel)

        c.Add(New ExportColumn("ScenarioID", "Scenario ID"))
        c.Add(New ExportColumn("ScenarioName", "Scenario"))
        c.Add(New ExportColumn("Iteration", "Iteration"))
        c.Add(New ExportColumn("Timestep", TimestepLabel))
        c.Add(New ExportColumn("Stratum", PrimaryStratumLabel))
        c.Add(New ExportColumn("SecondaryStratum", SecondaryStratumLabel))
        c.Add(New ExportColumn("TransitionType", "Transition Type"))
        c.Add(New ExportColumn("StateClass", "State Class"))
        c.Add(New ExportColumn("EndStateClass", "End State Class"))
        c.Add(New ExportColumn("Amount", AmountTitle))

        c("Amount").DecimalPlaces = 2
        c("Amount").Alignment = ColumnAlignment.Right

        Return c

    End Function

    Private Function CreateReportQuery(ByVal isCSV As Boolean) As String

        Dim ScenFilter As String = Me.CreateActiveResultScenarioFilter()

        If (isCSV) Then

            Return String.Format(CultureInfo.InvariantCulture,
                "SELECT " &
                "STSim_OutputStratumTransitionState.ScenarioID, " &
                "STSim_OutputStratumTransitionState.Iteration,  " &
                "STSim_OutputStratumTransitionState.Timestep,  " &
                "ST1.Name AS Stratum,  " &
                "ST2.Name AS SecondaryStratum,  " &
                "STSim_TransitionType.Name as TransitionType, " &
                "SC1.Name AS StateClass, " &
                "SC2.Name AS EndStateClass, " &
                "STSim_OutputStratumTransitionState.Amount " &
                "FROM STSim_OutputStratumTransitionState " &
                "INNER JOIN STSim_Stratum AS ST1 ON ST1.StratumID = STSim_OutputStratumTransitionState.StratumID " &
                "LEFT JOIN STSim_SecondaryStratum AS ST2 ON ST2.SecondaryStratumID = STSim_OutputStratumTransitionState.SecondaryStratumID " &
                "INNER JOIN STSim_StateClass as SC1 ON SC1.StateClassID = STSim_OutputStratumTransitionState.StateClassID " &
                "INNER JOIN STSim_StateClass as SC2 ON SC2.StateClassID = STSim_OutputStratumTransitionState.EndStateClassID " &
                "INNER JOIN STSim_TransitionType ON STSim_TransitionType.TransitionTypeID = STSim_OutputStratumTransitionState.TransitionTypeID " &
                "WHERE STSim_OutputStratumTransitionState.ScenarioID IN ({0})  " &
                "ORDER BY " &
                "STSim_OutputStratumTransitionState.ScenarioID, " &
                "STSim_OutputStratumTransitionState.Iteration, " &
                "STSim_OutputStratumTransitionState.Timestep, " &
                "ST1.Name, " &
                "ST2.Name, " &
                "SC1.Name, " &
                "SC2.Name, " &
                "STSim_TransitionType.Name",
                ScenFilter)

        Else

            Return String.Format(CultureInfo.InvariantCulture,
                "SELECT " &
                "STSim_OutputStratumTransitionState.ScenarioID, " &
                "SSim_Scenario.Name AS ScenarioName,  " &
                "STSim_OutputStratumTransitionState.Iteration,  " &
                "STSim_OutputStratumTransitionState.Timestep,  " &
                "ST1.Name AS Stratum,  " &
                "ST2.Name AS SecondaryStratum,  " &
                "STSim_TransitionType.Name as TransitionType, " &
                "SC1.Name AS StateClass, " &
                "SC2.Name AS EndStateClass, " &
                "STSim_OutputStratumTransitionState.Amount " &
                "FROM STSim_OutputStratumTransitionState " &
                "INNER JOIN SSim_Scenario ON SSim_Scenario.ScenarioID = STSim_OutputStratumTransitionState.ScenarioID " &
                "INNER JOIN STSim_Stratum AS ST1 ON ST1.StratumID = STSim_OutputStratumTransitionState.StratumID " &
                "LEFT JOIN STSim_SecondaryStratum AS ST2 ON ST2.SecondaryStratumID = STSim_OutputStratumTransitionState.SecondaryStratumID " &
                "INNER JOIN STSim_StateClass as SC1 ON SC1.StateClassID = STSim_OutputStratumTransitionState.StateClassID " &
                "INNER JOIN STSim_StateClass as SC2 ON SC2.StateClassID = STSim_OutputStratumTransitionState.EndStateClassID " &
                "INNER JOIN STSim_TransitionType ON STSim_TransitionType.TransitionTypeID = STSim_OutputStratumTransitionState.TransitionTypeID " &
                "WHERE STSim_OutputStratumTransitionState.ScenarioID IN ({0})  " &
                "ORDER BY " &
                "STSim_OutputStratumTransitionState.ScenarioID, " &
                "SSim_Scenario.Name, " &
                "STSim_OutputStratumTransitionState.Iteration, " &
                "STSim_OutputStratumTransitionState.Timestep, " &
                "ST1.Name, " &
                "ST2.Name, " &
                "SC1.Name, " &
                "SC2.Name, " &
                "STSim_TransitionType.Name",
                ScenFilter)

        End If

    End Function

End Class
