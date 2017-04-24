'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Imports System.IO
Imports System.Text
Imports System.Data.Common
Imports System.Globalization
Imports SyncroSim.Core
Imports SyncroSim.Core.Forms
Imports SyncroSim.Common
Imports System.Reflection

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class StateClassSummaryReport
    Inherits ExportTransformer

    Private m_SecondaryStrataExist As Boolean
    Private m_ScenarioData As Dictionary(Of Integer, ScenarioData)
    Private m_PrimaryStratumAmountMap As MultiLevelKeyMap4(Of StratumAmount)
    Private m_SecondaryStratumAmountMap As MultiLevelKeyMap5(Of StratumAmount)
    Private m_MultiplePrimaryStrataExist As Boolean

    Const CSV_INTEGER_FORMAT As String = "F0"
    Const CSV_DOUBLE_FORMAT As String = "F4"

    Protected Overrides Sub Export(location As String, exportType As ExportType)
        Me.InternalExport(location, exportType, True)
    End Sub

    Friend Sub InternalExport(location As String, exportType As ExportType, showMessage As Boolean)

        Using store As DataStore = Me.Library.CreateDataStore

            Me.FillScenarioData(store)

            If (Me.m_ScenarioData.Count = 0) Then
                FormsUtilities.ErrorMessageBox("There is no data for the specified scenarios.")
                Return
            End If

            Me.FillPrimaryStratumAmountMap(store)
            Me.FillSecondaryStratumAmountMap(store)

            Me.m_MultiplePrimaryStrataExist = Me.MultiplePrimaryStrataExist()
            Me.m_SecondaryStrataExist = Me.AnySeconaryStrataExist()

        End Using

        If (exportType = ExportType.ExcelFile) Then
            Me.CreateExcelReport(location)
        Else

            Me.CreateCSVReport(location)

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

        Dim AmountTitle As String = String.Format(CultureInfo.CurrentCulture, "{0} ({1})", AmountLabel, UnitsLabel)
        Dim Propn2Title As String = String.Format(CultureInfo.CurrentCulture, "Proportion of {0}", PrimaryStratumLabel)
        Dim Propn3Title As String = String.Format(CultureInfo.CurrentCulture, "Proportion of {0}/{1}", PrimaryStratumLabel, SecondaryStratumLabel)

        c.Add(New ExportColumn("ScenarioID", "Scenario ID"))
        c.Add(New ExportColumn("ScenarioName", "Scenario"))
        c.Add(New ExportColumn("Iteration", "Iteration"))
        c.Add(New ExportColumn("Timestep", TimestepLabel))
        c.Add(New ExportColumn("Stratum", PrimaryStratumLabel))
        c.Add(New ExportColumn("SecondaryStratum", SecondaryStratumLabel))
        c.Add(New ExportColumn("StateClass", "State Class"))
        c.Add(New ExportColumn("AgeMin", "Age Min"))
        c.Add(New ExportColumn("AgeMax", "Age Max"))
        c.Add(New ExportColumn("Amount", AmountTitle))
        c.Add(New ExportColumn("Proportion1", "Proportion of Landscape"))

        c("Amount").DecimalPlaces = 2
        c("Amount").Alignment = ColumnAlignment.Right

        c("Proportion1").Alignment = ColumnAlignment.Right
        c("Proportion1").DecimalPlaces = 4

        If (Me.m_MultiplePrimaryStrataExist) Then

            c.Add(New ExportColumn("Proportion2", Propn2Title))
            c("Proportion2").Alignment = ColumnAlignment.Right
            c("Proportion2").DecimalPlaces = 4

        End If

        If (Me.m_SecondaryStrataExist) Then

            c.Add(New ExportColumn("Proportion3", Propn3Title))
            c("Proportion3").Alignment = ColumnAlignment.Right
            c("Proportion3").DecimalPlaces = 4

        End If

        Return c

    End Function

    Private Sub CreateExcelReport(ByVal fileName As String)

        Dim AmountLabel As String = Nothing
        Dim AmountLabelUnits As TerminologyUnit
        Dim ReportQuery As String = CreateReportQuery(False)
        Dim ReportData As DataTable = Me.GetDataTableForReport(ReportQuery)
        Dim dsterm As DataSheet = Me.Project.GetDataSheet(DATASHEET_TERMINOLOGY_NAME)

        GetAmountLabelTerminology(dsterm, AmountLabel, AmountLabelUnits)
        Dim WorksheetName As String = String.Format(CultureInfo.CurrentCulture, "{0} by State Class", AmountLabel)

        ExportTransformer.ExcelExport(fileName, Me.CreateColumnCollection(), ReportData, WorksheetName)

    End Sub

    Private Sub CreateCSVReport(ByVal fileName As String)

        Using store As DataStore = Me.Library.CreateDataStore
            Me.CreateCSVReport(fileName, store)
        End Using

    End Sub

    Private Sub CreateCSVReport(ByVal fileName As String, ByVal store As DataStore)

        Dim Propn2Title As String = "ProportionOfStratumID"
        Dim Propn3Title As String = "ProportionOfStratumIDOverSecondaryStratumID"

        Using sw As New StreamWriter(fileName, False)

            sw.Write("ScenarioID,")
            sw.Write("Iteration,")
            sw.Write("Timestep,")
            sw.Write("StratumID,")
            sw.Write("SecondaryStratumID,")
            sw.Write("StateClassID,")
            sw.Write("AgeMin,")
            sw.Write("AgeMax,")
            sw.Write("Amount,")
            sw.Write("ProportionOfLandscape")

            If (Me.m_MultiplePrimaryStrataExist) Then
                sw.Write(",")
                sw.Write(CSVFormatString(Propn2Title))
            End If

            If (Me.m_SecondaryStrataExist) Then
                sw.Write(",")
                sw.Write(CSVFormatString(Propn3Title))
            End If

            Using cmd As DbCommand = store.CreateCommand()

                cmd.CommandText = CreateReportQuery(True)
                cmd.CommandType = CommandType.Text
                cmd.Connection = store.DatabaseConnection

                Using reader As DbDataReader = cmd.ExecuteReader()

                    While (reader.Read())

                        sw.Write(vbCrLf)

                        Dim ScenarioId As Integer = reader.GetInt32(0)
                        Dim Iteration As Integer = reader.GetInt32(1)
                        Dim Timestep As Integer = reader.GetInt32(2)
                        Dim StratumId As Integer = reader.GetInt32(3)
                        Dim SecondaryStratumId As Nullable(Of Integer) = Nothing

                        If (Not reader.IsDBNull(4)) Then
                            SecondaryStratumId = reader.GetInt32(4)
                        End If

                        Dim StratumName As String = reader.GetString(5)
                        Dim SecondaryStratumName As String = Nothing

                        If (Not reader.IsDBNull(6)) Then
                            SecondaryStratumName = reader.GetString(6)
                        End If

                        Dim StateClass As String = reader.GetString(7)
                        Dim AgeMin As Nullable(Of Integer) = Nothing

                        If (Not reader.IsDBNull(8)) Then
                            AgeMin = reader.GetInt32(8)
                        End If

                        Dim AgeMax As Nullable(Of Integer) = Nothing

                        If (Not reader.IsDBNull(9)) Then
                            AgeMax = reader.GetInt32(9)
                        End If

                        Dim Amount As Double = reader.GetDouble(10)

                        sw.Write(CSVFormatInteger(ScenarioId))
                        sw.Write(",")

                        sw.Write(CSVFormatInteger(Iteration))
                        sw.Write(",")

                        sw.Write(CSVFormatInteger(Timestep))
                        sw.Write(",")

                        sw.Write(CSVFormatString(StratumName))
                        sw.Write(",")

                        If (SecondaryStratumName IsNot Nothing) Then
                            sw.Write(CSVFormatString(SecondaryStratumName))
                        End If

                        sw.Write(",")

                        sw.Write(CSVFormatString(StateClass))
                        sw.Write(",")

                        If (AgeMin.HasValue) Then
                            sw.Write(CSVFormatInteger(AgeMin.Value))
                        End If

                        sw.Write(",")

                        If (AgeMax.HasValue) Then
                            sw.Write(CSVFormatInteger(AgeMax.Value))
                        End If

                        sw.Write(",")

                        sw.Write(CSVFormatDouble(Amount))
                        sw.Write(",")

                        'Proportion1
                        If (Me.m_ScenarioData(ScenarioId).TotalAmount = 0.0) Then
                            sw.Write("")
                        Else
                            sw.Write(CSVFormatDouble(Amount / Me.m_ScenarioData(ScenarioId).TotalAmount))
                        End If

                        'Proportion2
                        If (Me.m_MultiplePrimaryStrataExist) Then

                            sw.Write(",")

                            Dim sa As StratumAmount = Me.m_PrimaryStratumAmountMap.GetItemExact(
                                ScenarioId, StratumId, Iteration, Timestep)

                            If (sa Is Nothing OrElse sa.Amount = 0.0) Then
                                sw.Write("")
                            Else
                                sw.Write(CSVFormatDouble(Amount / sa.Amount))
                            End If

                        End If

                        'Proportion 3
                        If (Me.m_SecondaryStrataExist) Then

                            sw.Write(",")

                            Dim sa As StratumAmount = Me.m_SecondaryStratumAmountMap.GetItemExact(
                                ScenarioId, StratumId, SecondaryStratumId, Iteration, Timestep)

                            If (sa Is Nothing OrElse sa.Amount = 0.0) Then
                                sw.Write("")
                            Else
                                sw.Write(CSVFormatDouble(Amount / sa.Amount))
                            End If

                        End If

                    End While

                    reader.Close()

                End Using

            End Using

        End Using

    End Sub

    Private Function CreateReportQuery(ByVal isCSV As Boolean) As String

        Dim ScenFilter As String = CreateIntegerFilter(Me.m_ScenarioData.Keys)

        If (isCSV) Then

            Return String.Format(CultureInfo.InvariantCulture,
                "SELECT " &
                "STSim_OutputStratumState.ScenarioID, " &
                "STSim_OutputStratumState.Iteration,  " &
                "STSim_OutputStratumState.Timestep,  " &
                "STSim_OutputStratumState.StratumID, " &
                "STSim_OutputStratumState.SecondaryStratumID, " &
                "STSim_Stratum.Name AS Stratum,  " &
                "STSim_SecondaryStratum.Name AS SecondaryStratum,  " &
                "STSim_StateClass.Name as StateClass, " &
                "STSim_OutputStratumState.AgeMin, " &
                "STSim_OutputStratumState.AgeMax, " &
                "STSim_OutputStratumState.Amount " &
                "FROM STSim_OutputStratumState " &
                "INNER JOIN STSim_Stratum ON STSim_Stratum.StratumID = STSim_OutputStratumState.StratumID " &
                "LEFT JOIN STSim_SecondaryStratum ON STSim_SecondaryStratum.SecondaryStratumID = STSim_OutputStratumState.SecondaryStratumID " &
                "INNER JOIN STSim_StateClass ON STSim_StateClass.StateClassID = STSim_OutputStratumState.StateClassID " &
                "WHERE STSim_OutputStratumState.ScenarioID IN ({0})  " &
                "ORDER BY " &
                "STSim_OutputStratumState.ScenarioID, " &
                "STSim_OutputStratumState.Iteration, " &
                "STSim_OutputStratumState.Timestep, " &
                "STSim_OutputStratumState.StratumID, " &
                "STSim_OutputStratumState.SecondaryStratumID, " &
                "STSim_Stratum.Name, " &
                "STSim_SecondaryStratum.Name, " &
                "STSim_StateClass.Name, " &
                "AgeMin, " &
                "AgeMax",
                ScenFilter)

        Else

            Return String.Format(CultureInfo.InvariantCulture,
                "SELECT " &
                "STSim_OutputStratumState.ScenarioID, " &
                "SSim_Scenario.Name AS ScenarioName,  " &
                "STSim_OutputStratumState.Iteration,  " &
                "STSim_OutputStratumState.Timestep,  " &
                "STSim_OutputStratumState.StratumID, " &
                "STSim_OutputStratumState.SecondaryStratumID, " &
                "STSim_Stratum.Name AS Stratum,  " &
                "STSim_SecondaryStratum.Name AS SecondaryStratum,  " &
                "STSim_StateClass.Name as StateClass, " &
                "STSim_OutputStratumState.AgeMin, " &
                "STSim_OutputStratumState.AgeMax, " &
                "STSim_OutputStratumState.Amount " &
                "FROM STSim_OutputStratumState " &
                "INNER JOIN SSim_Scenario ON SSim_Scenario.ScenarioID = STSim_OutputStratumState.ScenarioID " &
                "INNER JOIN STSim_Stratum ON STSim_Stratum.StratumID = STSim_OutputStratumState.StratumID " &
                "LEFT JOIN STSim_SecondaryStratum ON STSim_SecondaryStratum.SecondaryStratumID = STSim_OutputStratumState.SecondaryStratumID " &
                "INNER JOIN STSim_StateClass ON STSim_StateClass.StateClassID = STSim_OutputStratumState.StateClassID " &
                "WHERE STSim_OutputStratumState.ScenarioID IN ({0})  " &
                "ORDER BY " &
                "STSim_OutputStratumState.ScenarioID, " &
                "SSim_Scenario.Name, " &
                "STSim_OutputStratumState.Iteration, " &
                "STSim_OutputStratumState.Timestep, " &
                "STSim_OutputStratumState.StratumID, " &
                "STSim_OutputStratumState.SecondaryStratumID, " &
                "STSim_Stratum.Name, " &
                "STSim_SecondaryStratum.Name, " &
                "STSim_StateClass.Name, " &
                "AgeMin, " &
                "AgeMax",
                ScenFilter)

        End If

    End Function

    Private Function GetDataTableForReport(ByVal reportQuery As String) As DataTable

        If (Me.m_ScenarioData.Count = 0) Then
            Return Nothing
        End If

        Using store As DataStore = Me.Library.CreateDataStore

            Dim dt As DataTable = store.CreateDataTableFromQuery(reportQuery, "State Class Summary")

            dt.Columns.Add(New DataColumn("Proportion1", GetType(Double)))
            dt.Columns.Add(New DataColumn("Proportion2", GetType(Double)))
            dt.Columns.Add(New DataColumn("Proportion3", GetType(Double)))

            For Each dr As DataRow In dt.Rows

                Dim sid As Integer = CInt(dr("ScenarioID"))

                dr("Proportion1") = DBNull.Value
                dr("Proportion2") = DBNull.Value
                dr("Proportion3") = DBNull.Value

                If (Me.m_ScenarioData(sid).TotalAmount = 0.0) Then
                    dr("Proportion1") = DBNull.Value
                Else
                    dr("Proportion1") = CDbl(dr("Amount")) / Me.m_ScenarioData(sid).TotalAmount
                End If

                Dim sa As StratumAmount = Me.m_PrimaryStratumAmountMap.GetItemExact(
                    CInt(dr("ScenarioID")),
                    CInt(dr("StratumID")),
                    CInt(dr("Iteration")),
                    CInt(dr("Timestep")))

                If (sa IsNot Nothing) Then

                    If (sa.Amount = 0.0) Then
                        Debug.Assert(False)
                        dr("Proportion2") = DBNull.Value
                    Else
                        dr("Proportion2") = CDbl(dr("Amount")) / sa.Amount
                    End If

                End If

                If (dr("SecondaryStratumID") IsNot DBNull.Value) Then

                    sa = Me.m_SecondaryStratumAmountMap.GetItemExact(
                        CInt(dr("ScenarioID")),
                        CInt(dr("StratumID")),
                        CInt(dr("SecondaryStratumID")),
                        CInt(dr("Iteration")),
                        CInt(dr("Timestep")))

                    If (sa IsNot Nothing) Then

                        If (sa.Amount = 0.0) Then
                            Debug.Assert(False)
                            dr("Proportion3") = DBNull.Value
                        Else
                            dr("Proportion3") = CDbl(dr("Amount")) / sa.Amount
                        End If

                    End If

                End If

            Next

            Return dt

        End Using

    End Function

    ''' <summary>
    ''' Determines if there is OutputStratumState data for the specified scenario Id
    ''' </summary>
    ''' <param name="store"></param>
    ''' <param name="scenarioId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function ScenarioHasData(ByVal store As DataStore, ByVal scenarioId As Integer) As Boolean

        Dim o1 As Object = store.ExecuteScalar(String.Format(CultureInfo.InvariantCulture,
            "SELECT MIN(Iteration) FROM STSim_OutputStratumState WHERE ScenarioID={0}", scenarioId))

        Dim o2 As Object = store.ExecuteScalar(String.Format(CultureInfo.InvariantCulture,
            "SELECT MIN(Timestep) FROM STSim_OutputStratumState WHERE ScenarioID={0}", scenarioId))

        Return (o1 IsNot DBNull.Value And o2 IsNot DBNull.Value)

    End Function

    ''' <summary>
    ''' Gets the total area for the specified scenario Id
    ''' </summary>
    ''' <param name="store"></param>
    ''' <param name="scenarioId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetTotalArea(ByVal store As DataStore, ByVal scenarioId As Integer) As Double

        Dim q1 As String = String.Format(CultureInfo.InvariantCulture,
            "SELECT MIN(Iteration) FROM STSim_OutputStratumState WHERE ScenarioID={0}",
            scenarioId)

        Dim q2 As String = String.Format(CultureInfo.InvariantCulture,
            "SELECT MIN(Timestep) FROM STSim_OutputStratumState WHERE ScenarioID={0}",
            scenarioId)

        Dim Iteration As Integer = CInt(store.ExecuteScalar(q1))
        Dim Timestep As Integer = CInt(store.ExecuteScalar(q2))

        Dim q3 As String = String.Format(CultureInfo.InvariantCulture,
            "SELECT SUM(Amount) FROM STSim_OutputStratumState WHERE ScenarioID={0} AND Iteration={1} AND Timestep={2}",
            scenarioId, Iteration, Timestep)

        Dim o As Object = store.ExecuteScalar(q3)

        If (o Is DBNull.Value) Then
            Debug.Assert(False)
            Return 0.0
        Else
            Return CDbl(o)
        End If

    End Function

    ''' <summary>
    ''' Fills the primary strata for the specified scenario Id
    ''' </summary>
    ''' <param name="store"></param>
    ''' <param name="scenarioId"></param>
    ''' <param name="sd"></param>
    ''' <remarks></remarks>
    Private Shared Sub FillPrimaryStrata(ByVal store As DataStore, ByVal scenarioId As Integer, ByVal sd As ScenarioData)

        Debug.Assert(sd.PrimaryStrata.Count = 0)

        Dim query As String = String.Format(CultureInfo.InvariantCulture,
            "SELECT DISTINCT StratumID FROM STSim_OutputStratumState WHERE ScenarioID={0}", scenarioId)

        Dim dt As DataTable = store.CreateDataTableFromQuery(query, "DistinctStrata")

        For Each dr As DataRow In dt.Rows

            Debug.Assert(Not sd.PrimaryStrata.Contains(CInt(dr("StratumID"))))
            sd.PrimaryStrata.Add(CInt(dr("StratumID")))

        Next

    End Sub

    ''' <summary>
    ''' Determines if there are any secondary strata for the specified scenario Id
    ''' </summary>
    ''' <param name="store"></param>
    ''' <param name="scenarioId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function HasSecondaryStrata(ByVal store As DataStore, ByVal scenarioId As Integer) As Boolean

        Dim query As String = String.Format(CultureInfo.InvariantCulture,
            "SELECT DISTINCT SecondaryStratumID FROM STSim_OutputStratumState WHERE ScenarioID={0} AND SecondaryStratumID IS NOT NULL", scenarioId)

        Dim dt As DataTable = store.CreateDataTableFromQuery(query, "DistinctSecondaryStrata")
        Return (dt.Rows.Count > 0)

    End Function

    ''' <summary>
    ''' Determines if multiple primary strata exist across all result scenarios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MultiplePrimaryStrataExist() As Boolean

        Dim dict As New Dictionary(Of Integer, Boolean)

        For Each sd As ScenarioData In Me.m_ScenarioData.Values

            If (sd.PrimaryStrata.Count > 1) Then
                Return True
            End If

            For Each id As Integer In sd.PrimaryStrata

                If (Not dict.ContainsKey(id)) Then
                    dict.Add(id, True)
                End If

            Next

        Next

        Return (dict.Count > 1)

    End Function

    ''' <summary>
    ''' Determines if any secondary strata exist
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AnySeconaryStrataExist() As Boolean

        For Each sd As ScenarioData In Me.m_ScenarioData.Values

            If (sd.HasSecondaryStrata) Then
                Return True
            End If

        Next

        Return False

    End Function

    ''' <summary>
    ''' Fills the scenario data
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks></remarks>
    Private Sub FillScenarioData(ByVal store As DataStore)

        Me.m_ScenarioData = New Dictionary(Of Integer, ScenarioData)

        For Each s As Scenario In Me.GetActiveResultScenarios()

            If (ScenarioHasData(store, s.Id)) Then

                Dim sd As New ScenarioData(GetTotalArea(store, s.Id))

                FillPrimaryStrata(store, s.Id, sd)
                sd.HasSecondaryStrata = HasSecondaryStrata(store, s.Id)

                Me.m_ScenarioData.Add(s.Id, sd)

            End If

        Next

    End Sub

    ''' <summary>
    ''' Fills the primary stratum amount map
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks></remarks>
    Private Sub FillPrimaryStratumAmountMap(ByVal store As DataStore)

        Me.m_PrimaryStratumAmountMap = New MultiLevelKeyMap4(Of StratumAmount)

        Dim query As String = String.Format(CultureInfo.InvariantCulture,
            "SELECT ScenarioID, Iteration, Timestep, StratumID, SUM(Amount) as SumOfAmount " &
            "FROM STSim_OutputStratum " &
            "WHERE ScenarioID IN ({0}) " &
            "GROUP BY ScenarioID, Iteration, Timestep, StratumID",
            CreateIntegerFilter(Me.m_ScenarioData.Keys))

        Dim dt As DataTable = store.CreateDataTableFromQuery(query, "PrimaryStratumAmounts")

        For Each dr As DataRow In dt.Rows

            Dim sa As New StratumAmount(CDbl(dr("SumOfAmount")))

            Me.m_PrimaryStratumAmountMap.AddItem(
                CInt(dr("ScenarioID")),
                CInt(dr("StratumID")),
                CInt(dr("Iteration")),
                CInt(dr("Timestep")), sa)

        Next

    End Sub

    ''' <summary>
    ''' Fills the secondary stratum amount map
    ''' </summary>
    ''' <param name="store"></param>
    ''' <remarks></remarks>
    Private Sub FillSecondaryStratumAmountMap(ByVal store As DataStore)

        Me.m_SecondaryStratumAmountMap = New MultiLevelKeyMap5(Of StratumAmount)

        Dim query As String = String.Format(CultureInfo.InvariantCulture,
            "SELECT ScenarioID, Iteration, Timestep, StratumID, SecondaryStratumID, Amount " &
            "FROM STSim_OutputStratum " &
            "WHERE ScenarioID IN ({0}) AND SecondaryStratumID IS NOT NULL",
            CreateIntegerFilter(Me.m_ScenarioData.Keys))

        Dim dt As DataTable = store.CreateDataTableFromQuery(query, "PrimaryStratumAmounts")

        For Each dr As DataRow In dt.Rows

            Dim sa As New StratumAmount(CDbl(dr("Amount")))

            Me.m_SecondaryStratumAmountMap.AddItem(
                CInt(dr("ScenarioID")),
                CInt(dr("StratumID")),
                CInt(dr("SecondaryStratumID")),
                CInt(dr("Iteration")),
                CInt(dr("Timestep")), sa)

        Next

    End Sub

    ''' <summary>
    ''' Creates a SQL filter from the specified integers
    ''' </summary>
    ''' <param name="values"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function CreateIntegerFilter(ByVal values As IEnumerable(Of Integer)) As String

        Dim sb As New StringBuilder()

        For Each id As Integer In values
            sb.AppendFormat(CultureInfo.InvariantCulture, "{0},", id)
        Next

        Debug.Assert(values.Count > 0)
        Return sb.ToString.TrimEnd(CChar(","))

    End Function

    Private Shared Function CSVFormatInteger(value As Int32) As String
        Return value.ToString(CSV_INTEGER_FORMAT, CultureInfo.InvariantCulture)
    End Function

    Private Shared Function CSVFormatInteger(value As Int64) As String
        Return value.ToString(CSV_INTEGER_FORMAT, CultureInfo.InvariantCulture)
    End Function

    Private Shared Function CSVFormatDouble(value As Double) As String
        Return value.ToString(CSV_DOUBLE_FORMAT, CultureInfo.InvariantCulture)
    End Function

    Private Shared Function CSVFormatString(value As String) As String
        Return InternalFormatStringCSV(value)
    End Function

    Private Shared Function InternalFormatStringCSV(value As String) As String

        Dim ContainsComma As Boolean = value.Contains(","c)
        Dim ContainsQuote As Boolean = value.Contains(""""c)

        If (Not ContainsComma And Not ContainsQuote) Then
            Return value
        End If

        If (ContainsQuote) Then

            Dim s As String = value.Replace("""", """""")
            Return String.Format(CultureInfo.CurrentCulture, """{0}""", s)

        Else
            Return String.Format(CultureInfo.CurrentCulture, """{0}""", value)
        End If

    End Function

End Class

''' <summary>
''' Stratum Amount class
''' </summary>
''' <remarks>So we can have NULL references in the multi-level maps</remarks>
Class StratumAmount

    Private m_Amount As Double

    Public Sub New(ByVal amount As Double)
        Me.m_Amount = amount
    End Sub

    Public ReadOnly Property Amount As Double
        Get
            Return Me.m_Amount
        End Get
    End Property

End Class

''' <summary>
''' Scenario Data class
''' </summary>
''' <remarks></remarks>
Class ScenarioData

    Public m_TotalAmount As Double
    Public m_PrimaryStrata As New List(Of Integer)
    Public m_HasSecondaryStrata As Boolean

    Public Sub New(ByVal totalAmount As Double)
        Me.m_TotalAmount = totalAmount
    End Sub

    Public ReadOnly Property TotalAmount As Double
        Get
            Return Me.m_TotalAmount
        End Get
    End Property

    Public ReadOnly Property PrimaryStrata As List(Of Integer)
        Get
            Return Me.m_PrimaryStrata
        End Get
    End Property

    Public Property HasSecondaryStrata As Boolean
        Get
            Return Me.m_HasSecondaryStrata
        End Get
        Set(value As Boolean)
            Me.m_HasSecondaryStrata = value
        End Set
    End Property

End Class
