'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Imports SyncroSim.Core
Imports System.Globalization
Imports SyncroSim.Core.Forms
Imports System.Reflection

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class StateAttributeReport
    Inherits ExportTransformer

    Protected Overrides Sub Export(location As String, exportType As ExportType)
        Me.InternalExport(location, exportType, True)
    End Sub

    Friend Sub InternalExport(location As String, exportType As ExportType, showMessage As Boolean)

        Dim columns As ExportColumnCollection = Me.CreateColumnCollection()

        If (exportType = ExportType.ExcelFile) Then
            Me.ExcelExport(location, columns, Me.CreateReportQuery(False), "State Based Attributes")
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
                "STSim_OutputStateAttribute.ScenarioID, " &
                "STSim_OutputStateAttribute.Iteration,  " &
                "STSim_OutputStateAttribute.Timestep,  " &
                "STSim_Stratum.Name AS Stratum,  " &
                "STSim_SecondaryStratum.Name AS SecondaryStratum,  " &
                "STSim_StateAttributeType.Name as AttributeType, " &
                "STSim_OutputStateAttribute.AgeMin, " &
                "STSim_OutputStateAttribute.AgeMax, " &
                "STSim_OutputStateAttribute.Amount " &
                "FROM STSim_OutputStateAttribute " &
                "INNER JOIN STSim_Stratum ON STSim_Stratum.StratumID = STSim_OutputStateAttribute.StratumID " &
                "LEFT JOIN STSim_SecondaryStratum ON STSim_SecondaryStratum.SecondaryStratumID = STSim_OutputStateAttribute.SecondaryStratumID " &
                "INNER JOIN STSim_StateAttributeType ON STSim_StateAttributeType.StateAttributeTypeID = STSim_OutputStateAttribute.StateAttributeTypeID " &
                "WHERE STSim_OutputStateAttribute.ScenarioID IN ({0})  " &
                "ORDER BY " &
                "STSim_OutputStateAttribute.ScenarioID, " &
                "STSim_OutputStateAttribute.Iteration, " &
                "STSim_OutputStateAttribute.Timestep, " &
                "STSim_Stratum.Name, " &
                "STSim_SecondaryStratum.Name, " &
                "STSim_StateAttributeType.Name, " &
                "AgeMin, " &
                "AgeMax",
                ScenFilter)

        Else

            Return String.Format(CultureInfo.InvariantCulture,
                "SELECT " &
                "STSim_OutputStateAttribute.ScenarioID, " &
                "SSim_Scenario.Name AS ScenarioName,  " &
                "STSim_OutputStateAttribute.Iteration,  " &
                "STSim_OutputStateAttribute.Timestep,  " &
                "STSim_Stratum.Name AS Stratum,  " &
                "STSim_SecondaryStratum.Name AS SecondaryStratum,  " &
                "STSim_StateAttributeType.Name as AttributeType, " &
                "STSim_OutputStateAttribute.AgeMin, " &
                "STSim_OutputStateAttribute.AgeMax, " &
                "STSim_OutputStateAttribute.Amount " &
                "FROM STSim_OutputStateAttribute " &
                "INNER JOIN SSim_Scenario ON SSim_Scenario.ScenarioID = STSim_OutputStateAttribute.ScenarioID " &
                "INNER JOIN STSim_Stratum ON STSim_Stratum.StratumID = STSim_OutputStateAttribute.StratumID " &
                "LEFT JOIN STSim_SecondaryStratum ON STSim_SecondaryStratum.SecondaryStratumID = STSim_OutputStateAttribute.SecondaryStratumID " &
                "INNER JOIN STSim_StateAttributeType ON STSim_StateAttributeType.StateAttributeTypeID = STSim_OutputStateAttribute.StateAttributeTypeID " &
                "WHERE STSim_OutputStateAttribute.ScenarioID IN ({0})  " &
                "ORDER BY " &
                "STSim_OutputStateAttribute.ScenarioID, " &
                "SSim_Scenario.Name, " &
                "STSim_OutputStateAttribute.Iteration, " &
                "STSim_OutputStateAttribute.Timestep, " &
                "STSim_Stratum.Name, " &
                "STSim_SecondaryStratum.Name, " &
                "STSim_StateAttributeType.Name, " &
                "AgeMin, " &
                "AgeMax",
                ScenFilter)

        End If

    End Function

End Class
