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
Class TransitionAttributeReport
    Inherits ExportTransformer

    Protected Overrides Sub Export(location As String, exportType As ExportType)
        Me.InternalExport(location, exportType, True)
    End Sub

    Friend Sub InternalExport(location As String, exportType As ExportType, showMessage As Boolean)

        Dim columns As ExportColumnCollection = Me.CreateColumnCollection()

        If (exportType = ExportType.ExcelFile) Then
            Me.ExcelExport(location, columns, Me.CreateReportQuery(False), "Transition Based Attributes")
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

        Dim PrimaryStratumLabel As String = Nothing
        Dim SecondaryStratumLabel As String = Nothing
        Dim dsterm As DataSheet = Me.Project.GetDataSheet(DATASHEET_TERMINOLOGY_NAME)
        Dim TimestepLabel As String = GetTimestepUnits(Me.Project)
        GetStratumLabelTerminology(dsterm, PrimaryStratumLabel, SecondaryStratumLabel)

        c.Add(New ExportColumn("ScenarioID", "Scenario ID"))
        c.Add(New ExportColumn("ScenarioName", "Scenario"))
        c.Add(New ExportColumn("Iteration", "Iteration"))
        c.Add(New ExportColumn("Timestep", TimestepLabel))
        c.Add(New ExportColumn("Stratum", PrimaryStratumLabel))
        c.Add(New ExportColumn("SecondaryStratum", SecondaryStratumLabel))
        c.Add(New ExportColumn("AttributeType", "Attribute"))
        c.Add(New ExportColumn("AgeMin", "Age Min"))
        c.Add(New ExportColumn("AgeMax", "Age Max"))
        c.Add(New ExportColumn("Amount", "Total Value"))

        c("Amount").DecimalPlaces = 2
        c("Amount").Alignment = ColumnAlignment.Right

        Return c

    End Function

    Private Function CreateReportQuery(ByVal isCSV As Boolean) As String

        Dim ScenFilter As String = Me.CreateActiveResultScenarioFilter()

        If (isCSV) Then

            Return String.Format(CultureInfo.InvariantCulture,
                "SELECT " &
                "STSim_OutputTransitionAttribute.ScenarioID, " &
                "STSim_OutputTransitionAttribute.Iteration,  " &
                "STSim_OutputTransitionAttribute.Timestep,  " &
                "STSim_Stratum.Name AS Stratum,  " &
                "STSim_SecondaryStratum.Name AS SecondaryStratum,  " &
                "STSim_TransitionAttributeType.Name as AttributeType, " &
                "STSim_OutputTransitionAttribute.AgeMin, " &
                "STSim_OutputTransitionAttribute.AgeMax, " &
                "STSim_OutputTransitionAttribute.Amount " &
                "FROM STSim_OutputTransitionAttribute " &
                "INNER JOIN STSim_Stratum ON STSim_Stratum.StratumID = STSim_OutputTransitionAttribute.StratumID " &
                "LEFT JOIN STSim_SecondaryStratum ON STSim_SecondaryStratum.SecondaryStratumID = STSim_OutputTransitionAttribute.SecondaryStratumID " &
                "INNER JOIN STSim_TransitionAttributeType ON STSim_TransitionAttributeType.TransitionAttributeTypeID = STSim_OutputTransitionAttribute.TransitionAttributeTypeID " &
                "WHERE STSim_OutputTransitionAttribute.ScenarioID IN ({0})  " &
                "ORDER BY " &
                "STSim_OutputTransitionAttribute.ScenarioID, " &
                "STSim_OutputTransitionAttribute.Iteration, " &
                "STSim_OutputTransitionAttribute.Timestep, " &
                "STSim_Stratum.Name, " &
                "STSim_SecondaryStratum.Name, " &
                "STSim_TransitionAttributeType.Name, " &
                "AgeMin, " &
                "AgeMax",
                ScenFilter)

        Else

            Return String.Format(CultureInfo.InvariantCulture,
                "SELECT " &
                "STSim_OutputTransitionAttribute.ScenarioID, " &
                "SSim_Scenario.Name AS ScenarioName,  " &
                "STSim_OutputTransitionAttribute.Iteration,  " &
                "STSim_OutputTransitionAttribute.Timestep,  " &
                "STSim_Stratum.Name AS Stratum,  " &
                "STSim_SecondaryStratum.Name AS SecondaryStratum,  " &
                "STSim_TransitionAttributeType.Name as AttributeType, " &
                "STSim_OutputTransitionAttribute.AgeMin, " &
                "STSim_OutputTransitionAttribute.AgeMax, " &
                "STSim_OutputTransitionAttribute.Amount " &
                "FROM STSim_OutputTransitionAttribute " &
                "INNER JOIN SSim_Scenario ON SSim_Scenario.ScenarioID = STSim_OutputTransitionAttribute.ScenarioID " &
                "INNER JOIN STSim_Stratum ON STSim_Stratum.StratumID = STSim_OutputTransitionAttribute.StratumID " &
                "LEFT JOIN STSim_SecondaryStratum ON STSim_SecondaryStratum.SecondaryStratumID = STSim_OutputTransitionAttribute.SecondaryStratumID " &
                "INNER JOIN STSim_TransitionAttributeType ON STSim_TransitionAttributeType.TransitionAttributeTypeID = STSim_OutputTransitionAttribute.TransitionAttributeTypeID " &
                "WHERE STSim_OutputTransitionAttribute.ScenarioID IN ({0})  " &
                "ORDER BY " &
                "STSim_OutputTransitionAttribute.ScenarioID, " &
                "SSim_Scenario.Name, " &
                "STSim_OutputTransitionAttribute.Iteration, " &
                "STSim_OutputTransitionAttribute.Timestep, " &
                "STSim_Stratum.Name, " &
                "STSim_SecondaryStratum.Name, " &
                "STSim_TransitionAttributeType.Name, " &
                "AgeMin, " &
                "AgeMax",
                ScenFilter)

        End If

    End Function

End Class
