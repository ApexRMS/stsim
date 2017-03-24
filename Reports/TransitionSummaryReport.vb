'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.Core
Imports System.Globalization
Imports SyncroSim.Core.Forms
Imports System.Reflection

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class TransitionSummaryReport
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

        If (exportType = ExportType.ExcelFile) Then

            Dim WorksheetName As String = String.Format(CultureInfo.InvariantCulture, "{0} by Transition Group", AmountLabel)
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
        c.Add(New ExportColumn("TransitionGroup", "Transition Group"))
        c.Add(New ExportColumn("AgeMin", "Age Min"))
        c.Add(New ExportColumn("AgeMax", "Age Max"))
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
                "STSim_OutputStratumTransition.ScenarioID, " &
                "STSim_OutputStratumTransition.Iteration,  " &
                "STSim_OutputStratumTransition.Timestep,  " &
                "STSim_Stratum.Name AS Stratum,  " &
                "STSim_SecondaryStratum.Name AS SecondaryStratum,  " &
                "STSim_TransitionGroup.Name as TransitionGroup, " &
                "STSim_OutputStratumTransition.AgeMin, " &
                "STSim_OutputStratumTransition.AgeMax, " &
                "STSim_OutputStratumTransition.Amount " &
                "FROM STSim_OutputStratumTransition " &
                "INNER JOIN STSim_Stratum ON STSim_Stratum.StratumID = STSim_OutputStratumTransition.StratumID " &
                "LEFT JOIN STSim_SecondaryStratum ON STSim_SecondaryStratum.SecondaryStratumID = STSim_OutputStratumTransition.SecondaryStratumID " &
                "INNER JOIN STSim_TransitionGroup ON STSim_TransitionGroup.TransitionGroupID = STSim_OutputStratumTransition.TransitionGroupID " &
                "WHERE STSim_OutputStratumTransition.ScenarioID IN ({0})  " &
                "ORDER BY " &
                "STSim_OutputStratumTransition.ScenarioID, " &
                "STSim_OutputStratumTransition.Iteration, " &
                "STSim_OutputStratumTransition.Timestep, " &
                "STSim_Stratum.Name, " &
                "STSim_SecondaryStratum.Name, " &
                "STSim_TransitionGroup.Name, " &
                "AgeMin, " &
                "AgeMax",
                ScenFilter)

        Else

            Return String.Format(CultureInfo.InvariantCulture,
                "SELECT " &
                "STSim_OutputStratumTransition.ScenarioID, " &
                "SSim_Scenario.Name AS ScenarioName,  " &
                "STSim_OutputStratumTransition.Iteration,  " &
                "STSim_OutputStratumTransition.Timestep,  " &
                "STSim_Stratum.Name AS Stratum,  " &
                "STSim_SecondaryStratum.Name AS SecondaryStratum,  " &
                "STSim_TransitionGroup.Name as TransitionGroup, " &
                "STSim_OutputStratumTransition.AgeMin, " &
                "STSim_OutputStratumTransition.AgeMax, " &
                "STSim_OutputStratumTransition.Amount " &
                "FROM STSim_OutputStratumTransition " &
                "INNER JOIN SSim_Scenario ON SSim_Scenario.ScenarioID = STSim_OutputStratumTransition.ScenarioID " &
                "INNER JOIN STSim_Stratum ON STSim_Stratum.StratumID = STSim_OutputStratumTransition.StratumID " &
                "LEFT JOIN STSim_SecondaryStratum ON STSim_SecondaryStratum.SecondaryStratumID = STSim_OutputStratumTransition.SecondaryStratumID " &
                "INNER JOIN STSim_TransitionGroup ON STSim_TransitionGroup.TransitionGroupID = STSim_OutputStratumTransition.TransitionGroupID " &
                "WHERE STSim_OutputStratumTransition.ScenarioID IN ({0})  " &
                "ORDER BY " &
                "STSim_OutputStratumTransition.ScenarioID, " &
                "SSim_Scenario.Name, " &
                "STSim_OutputStratumTransition.Iteration, " &
                "STSim_OutputStratumTransition.Timestep, " &
                "STSim_Stratum.Name, " &
                "STSim_SecondaryStratum.Name, " &
                "STSim_TransitionGroup.Name, " &
                "AgeMin, " &
                "AgeMax",
                ScenFilter)

        End If

    End Function

End Class
