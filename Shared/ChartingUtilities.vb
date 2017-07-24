'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports System.Text
Imports System.Globalization
Imports SyncroSim.Core
Imports SyncroSim.StochasticTime.Forms

Module ChartingUtilities

    ''' <summary>
    ''' Determinies if a descriptor has an age reference
    ''' </summary>
    ''' <param name="descriptors"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function HasAgeReference(ByVal descriptors As ChartDescriptorCollection) As Boolean

        For Each d As ChartDescriptor In descriptors

            If (d.IncludeDataFilter IsNot Nothing AndAlso d.IncludeDataFilter.Contains("AgeClass")) Then
                Return True
            End If

            If (d.DisaggregateFilter IsNot Nothing AndAlso d.DisaggregateFilter.Contains("AgeClass")) Then
                Return True
            End If

        Next

        Return False

    End Function

    ''' <summary>
    ''' Gets stochastic time chart status entries for the specified data sheet
    ''' </summary>
    ''' <param name="store"></param>
    ''' <param name="dataSheet"></param>
    ''' <param name="chartDescriptors"></param>
    ''' <param name="statusEntries"></param>
    ''' <remarks></remarks>
    Public Sub GetAgeRelatedStatusEntries(
        ByVal store As DataStore,
        ByVal dataSheet As DataSheet,
        ByVal statusEntries As StochasticTimeStatusCollection)

        Dim e As IEnumerable(Of AgeDescriptor) = GetAgeDescriptors(dataSheet.Project)

        If (e Is Nothing) Then
            Return
        End If

        If (Not AnyDataExists(store, dataSheet)) Then
            Return
        End If

        If (Not AgeDataExists(store, dataSheet)) Then

            Dim entry As String = String.Format(CultureInfo.InvariantCulture,
                ERROR_AGE_DATA_MISSING, dataSheet.Scenario.DisplayName)

            statusEntries.Add(New StochasticTimeStatus(entry))
            Return

        End If

        If (Not AgeClassesMatchData(store, dataSheet)) Then

            Dim query As String = String.Format(CultureInfo.InvariantCulture,
                "SELECT AgeMin, AgeMax FROM {0} WHERE (AgeClass IS NULL) AND ScenarioID = {1} GROUP BY AgeMin, AgeMax ORDER BY AgeMin",
                dataSheet.Name, dataSheet.Scenario.Id)

            Dim dt As DataTable = store.CreateDataTableFromQuery(query, "ageclassdata")

            Dim sb1 As New StringBuilder()
            Dim sb2 As New StringBuilder()

            sb1.AppendLine("***")
            sb1.AppendLine("Inconsistent Age Types and Age Groups detected for scenario:")
            sb1.AppendLine()
            sb1.AppendLine(dataSheet.Scenario.DisplayName)
            sb1.AppendLine()
            sb1.AppendLine("The Age Types for this scenario include the following ranges:")
            sb1.AppendLine()

            Dim c As Integer = 0
            Const MAX_AGE_ROWS As Integer = 5

            sb1.AppendFormat(CultureInfo.InvariantCulture, "{0,-15}{1,-15}", "Minimum Age", "Maximum Age")
            sb1.AppendLine()

            Dim AgeTypeMaxDefault As String = GetAgeTypeMaxValueDefault(dataSheet.Project)

            For Each dr As DataRow In dt.Rows

                Dim AgeMaxValue As String = AgeTypeMaxDefault

                If (dr("AgeMax") IsNot DBNull.Value) Then
                    AgeMaxValue = CStr(dr("AgeMax"))
                End If

                sb1.AppendFormat(CultureInfo.InvariantCulture, "{0,-15}{1,-15}", CInt(dr("AgeMin")), AgeMaxValue)
                sb1.AppendLine()

                sb2.AppendFormat(CultureInfo.InvariantCulture, "{0}, ", AgeMaxValue)

                c += 1

                If (c = MAX_AGE_ROWS) Then
                    Exit For
                End If

            Next

            If (dt.Rows.Count > MAX_AGE_ROWS) Then
                sb1.AppendLine("etc...")
                sb2.Append("etc...")
            End If

            Dim FinalSB2 As String = sb2.ToString().TrimEnd()
            FinalSB2 = FinalSB2.TrimEnd(CChar(","))

            sb1.AppendLine()
            sb1.AppendLine("To correct this problem you can do one of the following:")
            sb1.AppendLine()
            sb1.AppendLine("(1.) Modify the Age Types and rerun your model.")
            sb1.AppendFormat(CultureInfo.InvariantCulture, "(2.) Ensure that the Maximum Age for each Age Group is a subset of the upper bounds for the Age Type ranges shown above (i.e. {0})", FinalSB2)
            sb1.AppendLine()
            sb1.AppendLine("***")

            statusEntries.Add(New StochasticTimeStatus(sb1.ToString()))

        End If

    End Sub

    ''' <summary>
    ''' Updates the age class column for the specified table
    ''' </summary>
    ''' <param name="store"></param>
    ''' <param name="dataSheet"></param>
    ''' <remarks></remarks>
    Public Sub UpdateAgeClassColumn(ByVal store As DataStore, ByVal dataSheet As DataSheet)

        Dim e As IEnumerable(Of AgeDescriptor) = GetAgeDescriptors(dataSheet.Project)

        If (e Is Nothing) Then
            Return
        End If

        If (Not AnyDataExists(store, dataSheet)) Then
            Return
        End If

        If (Not AgeDataExists(store, dataSheet)) Then
            Return
        End If

        Dim sb As New StringBuilder()
        Debug.Assert(Not e(e.Count - 1).MaximumAge.HasValue)

        If (e.Count > 1) Then

            sb.AppendFormat(CultureInfo.InvariantCulture,
                "UPDATE [{0}] SET AgeClass = CASE", dataSheet.Name)

            For i As Integer = 0 To e.Count - 1

                Dim d As AgeDescriptor = e(i)

                If (d.MaximumAge.HasValue) Then

                    Debug.Assert(i < e.Count - 1)

                    sb.AppendFormat(CultureInfo.InvariantCulture,
                        " WHEN AgeMin >= {0} And AgeMax <= {1} THEN {2}",
                        d.MinimumAge, d.MaximumAge.Value, d.MinimumAge)

                Else

                    Debug.Assert(i = e.Count - 1)

                    sb.AppendFormat(CultureInfo.InvariantCulture,
                        " WHEN AgeMin >= {0} THEN {1}",
                        d.MinimumAge, d.MinimumAge)

                End If

            Next

            sb.Append(" END")

        Else

            sb.AppendFormat(CultureInfo.InvariantCulture,
                "UPDATE [{0}] SET AgeClass = {1}", dataSheet.Name, e(0).MinimumAge)

        End If

        sb.AppendFormat(CultureInfo.InvariantCulture, " WHERE ScenarioID = {0}", dataSheet.Scenario.Id)
        store.ExecuteNonQuery(sb.ToString())

    End Sub

    Public Function CreateProportionChartData(
        ByVal scenario As Scenario,
        ByVal descriptor As ChartDescriptor,
        ByVal tableName As String,
        ByVal store As DataStore) As DataTable

        Dim dict As Dictionary(Of String, Double) = CreateAmountDictionary(scenario, descriptor, store)

        If (dict.Count = 0) Then
            Return Nothing
        End If

        Dim query As String = CreateRawDataQuery(scenario, descriptor, tableName)
        Dim dt As DataTable = store.CreateDataTableFromQuery(query, "RawData")

        For Each dr As DataRow In dt.Rows

            If (dr(DATASHEET_SUMOFAMOUNT_COLUMN_NAME) IsNot DBNull.Value) Then

                Dim it As Integer = CInt(dr(DATASHEET_ITERATION_COLUMN_NAME))
                Dim ts As Integer = CInt(dr(DATASHEET_TIMESTEP_COLUMN_NAME))

                Dim k As String = String.Format(CultureInfo.InvariantCulture, "{0}-{1}", it, ts)
                dr(DATASHEET_SUMOFAMOUNT_COLUMN_NAME) = CDbl(dr(DATASHEET_SUMOFAMOUNT_COLUMN_NAME)) / dict(k)

            End If

        Next

        Return dt

    End Function

    Public Function CreateRawAttributeChartData(
        ByVal scenario As Scenario,
        ByVal descriptor As ChartDescriptor,
        ByVal tableName As String,
        ByVal attributeTypeColumnName As String,
        ByVal attributeTypeId As Integer,
        ByVal isDensity As Boolean,
        ByVal store As DataStore) As DataTable

        Dim query As String = CreateRawAttributeDataQuery(scenario, descriptor, tableName, attributeTypeColumnName, attributeTypeId)
        Dim dt As DataTable = store.CreateDataTableFromQuery(query, "RawData")

        If (isDensity) Then

            Dim dict As Dictionary(Of String, Double) = CreateAmountDictionary(scenario, descriptor, store)

            If (dict.Count > 0) Then

                For Each dr As DataRow In dt.Rows

                    If (dr(DATASHEET_SUMOFAMOUNT_COLUMN_NAME) IsNot DBNull.Value) Then

                        Dim it As Integer = CInt(dr(DATASHEET_ITERATION_COLUMN_NAME))
                        Dim ts As Integer = CInt(dr(DATASHEET_TIMESTEP_COLUMN_NAME))

                        Dim k As String = String.Format(CultureInfo.InvariantCulture, "{0}-{1}", it, ts)
                        dr(DATASHEET_SUMOFAMOUNT_COLUMN_NAME) = CDbl(dr(DATASHEET_SUMOFAMOUNT_COLUMN_NAME)) / dict(k)

                    End If

                Next

            End If

        End If

        Return dt

    End Function

    Public Function CreateAmountDictionary(
        ByVal scenario As Scenario,
        ByVal descriptor As ChartDescriptor,
        ByVal store As DataStore) As Dictionary(Of String, Double)

        Dim dict As New Dictionary(Of String, Double)
        Dim query As String = CreateAmountQuery(scenario, descriptor)
        Dim dt As DataTable = store.CreateDataTableFromQuery(query, "AmountData")

        For Each dr As DataRow In dt.Rows

            If (dr(DATASHEET_SUMOFAMOUNT_COLUMN_NAME) IsNot DBNull.Value) Then

                Dim it As Integer = CInt(dr(DATASHEET_ITERATION_COLUMN_NAME))
                Dim ts As Integer = CInt(dr(DATASHEET_TIMESTEP_COLUMN_NAME))

                Dim k As String = String.Format(CultureInfo.InvariantCulture, "{0}-{1}", it, ts)
                dict.Add(k, CDbl(dr(DATASHEET_SUMOFAMOUNT_COLUMN_NAME)))

            End If

        Next

        Return dict

    End Function

    Private Function CreateAmountQuery(
        ByVal scenario As Scenario,
        ByVal descriptor As ChartDescriptor) As String

        Dim ScenarioClause As String = String.Format(CultureInfo.InvariantCulture,
            "([{0}]={1})", DATASHEET_SCENARIOID_COLUMN_NAME, scenario.Id)

        Dim WhereClause As String = ScenarioClause

        Dim Disagg As String = RemoveUnwantedColumnReferences(descriptor.DisaggregateFilter)
        Dim IncData As String = RemoveUnwantedColumnReferences(descriptor.IncludeDataFilter)

        If (Not String.IsNullOrEmpty(Disagg)) Then
            WhereClause = String.Format(CultureInfo.InvariantCulture, "{0} And ({1})", WhereClause, Disagg)
        End If

        If (Not String.IsNullOrEmpty(IncData)) Then
            WhereClause = String.Format(CultureInfo.InvariantCulture, "{0} And ({1})", WhereClause, IncData)
        End If

        Dim query As String = String.Format(CultureInfo.InvariantCulture,
            "SELECT Iteration, Timestep, SUM(Amount) AS SumOfAmount FROM STSim_OutputStratum WHERE ({0}) GROUP BY Iteration, Timestep",
            WhereClause)

        Return query

    End Function

    Private Function CreateRawDataQuery(
        ByVal scenario As Scenario,
        ByVal descriptor As ChartDescriptor,
        ByVal tableName As String) As String

        Dim ScenarioClause As String = String.Format(CultureInfo.InvariantCulture,
            "([{0}]={1})", DATASHEET_SCENARIOID_COLUMN_NAME, scenario.Id)

        Dim WhereClause As String = ScenarioClause

        If (Not String.IsNullOrEmpty(descriptor.DisaggregateFilter)) Then
            WhereClause = String.Format(CultureInfo.InvariantCulture, "{0} AND ({1})", WhereClause, descriptor.DisaggregateFilter)
        End If

        If (Not String.IsNullOrEmpty(descriptor.IncludeDataFilter)) Then
            WhereClause = String.Format(CultureInfo.InvariantCulture, "{0} AND ({1})", WhereClause, descriptor.IncludeDataFilter)
        End If

        Dim query As String = String.Format(CultureInfo.InvariantCulture,
            "SELECT Iteration, Timestep, SUM(Amount) AS SumOfAmount FROM {0} WHERE ({1}) GROUP BY Iteration, Timestep",
            tableName, WhereClause)

        Return query

    End Function

    Private Function CreateRawAttributeDataQuery(
        ByVal scenario As Core.Scenario,
        ByVal descriptor As ChartDescriptor,
        ByVal tableName As String,
        ByVal attributeTypeColumnName As String,
        ByVal attributeTypeId As Integer) As String

        Dim ScenarioClause As String = String.Format(CultureInfo.InvariantCulture,
            "([{0}]={1})", DATASHEET_SCENARIOID_COLUMN_NAME, scenario.Id)

        Dim WhereClause As String = String.Format(CultureInfo.InvariantCulture,
            "{0} AND ([{1}]={2})", ScenarioClause, attributeTypeColumnName, attributeTypeId)

        If (Not String.IsNullOrEmpty(descriptor.DisaggregateFilter)) Then
            WhereClause = String.Format(CultureInfo.InvariantCulture, "{0} AND ({1})", WhereClause, descriptor.DisaggregateFilter)
        End If

        If (Not String.IsNullOrEmpty(descriptor.IncludeDataFilter)) Then
            WhereClause = String.Format(CultureInfo.InvariantCulture, "{0} AND ({1})", WhereClause, descriptor.IncludeDataFilter)
        End If

        Dim query As String = String.Format(CultureInfo.InvariantCulture,
            "SELECT Iteration, Timestep, SUM(Amount) AS SumOfAmount FROM {0} WHERE ({1}) GROUP BY Iteration, Timestep",
            tableName, WhereClause)

        Return query

    End Function

    Private Function RemoveUnwantedColumnReferences(ByVal filter As String) As String

        If (filter Is Nothing) Then
            Return Nothing
        End If

        Dim AndSplit() As String = filter.Split({" AND "}, StringSplitOptions.None)
        Dim sb As New StringBuilder

        For Each s As String In AndSplit

            If (s.Contains("StratumID") Or
                s.Contains("SecondaryStratumID")) Then

                sb.AppendFormat(CultureInfo.InvariantCulture, "{0} AND ", s)

            End If

        Next

        Dim final As String = sb.ToString

        If (final.Count > 0) Then

            Debug.Assert(final.Count >= 5)
            Return Mid(final, 1, final.Length - 5)

        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Determines if any data exists for the specified data sheet
    ''' </summary>
    ''' <param name="store"></param>
    ''' <param name="dataSheet"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AnyDataExists(ByVal store As DataStore, ByVal dataSheet As DataSheet) As Boolean

        Dim query As String = String.Format(CultureInfo.InvariantCulture,
            "SELECT COUNT(ScenarioID) FROM {0} WHERE ScenarioID = {1}", dataSheet.Name, dataSheet.Scenario.Id)

        If (CInt(store.ExecuteScalar(query)) = 0) Then
            Return False
        Else
            Return True
        End If

    End Function

    ''' <summary>
    ''' Determines if any age data exists for the specified scenario
    ''' </summary>
    ''' <param name="store"></param>
    ''' <param name="dataSheet"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AgeDataExists(ByVal store As DataStore, ByVal dataSheet As DataSheet) As Boolean

        Dim query As String = String.Format(CultureInfo.InvariantCulture,
            "SELECT COUNT(AgeMin) FROM {0} WHERE ScenarioID = {1}", dataSheet.Name, dataSheet.Scenario.Id)

        If (CInt(store.ExecuteScalar(query)) = 0) Then
            Return False
        Else
            Return True
        End If

    End Function

    ''' <summary>
    ''' Determines if the current age classes match the data
    ''' </summary>
    ''' <param name="store"></param>
    ''' <param name="dataSheet"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Mismatched age bins which can occur due to a difference in the current age configuration
    ''' and the age data that was generated when the scenario was run.
    ''' </remarks>
    Private Function AgeClassesMatchData(ByVal store As DataStore, ByVal dataSheet As DataSheet) As Boolean

        'If any AgeClass values are NULL then the AgeMin and AgeMax did not fall into an AgeClass bin.
        'This could happen if the data contains values such as 10 - 19, but the frequency is 13.  In this
        'case the bins would be 0-12, 13-25, etc., and 10-19 does not go into any of these bins.

        Dim query As String = String.Format(CultureInfo.InvariantCulture,
            "SELECT COUNT(AgeMin) FROM {0} WHERE (AgeClass IS NULL) AND ScenarioID = {1}",
            dataSheet.Name, dataSheet.Scenario.Id)

        If (CInt(store.ExecuteScalar(query)) <> 0) Then
            Return False
        End If

        Return True

    End Function

End Module
