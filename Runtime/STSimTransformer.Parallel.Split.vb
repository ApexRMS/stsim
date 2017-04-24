'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports System.IO
Imports System.Globalization
Imports SyncroSim.Core

Partial Class STSimTransformer

    Public Overrides Sub Split(ByVal maximumJobs As Integer)

        If (Me.IsSplitByStrata()) Then

            Me.NormalizeRunControl()
            Me.ConfigureIsSpatialRunFlag()
            Me.ConfigureTimestepsAndIterations()
            Me.ValidateNormalSplit()

            Dim SplitData As DataTable = CreateSplitTableSchema()
            Dim SecondaryStratumIds As List(Of Integer) = Me.GetApplicableSecondaryStrata()

            AddSecondaryStratumRows(SplitData, maximumJobs, SecondaryStratumIds)

            Me.AddIterationRows(SplitData, maximumJobs)
            Me.CalculateNewInitialConditions(SplitData)
            Me.CreatePartialLibraries(SplitData, False)

        Else
            MyBase.Split(maximumJobs)
        End If

    End Sub

    Friend Sub SpatialSplit(
        ByVal maximumJobs As Integer,
        ByVal secondaryStratumIds As List(Of Integer))

        Debug.Assert(secondaryStratumIds.Count >= 2)

        Me.NormalizeRunControl()
        Me.ConfigureTimestepsAndIterations()
        Me.ValidateSpatialSplit()

        Dim SplitData As DataTable = CreateSplitTableSchema()

        AddSecondaryStratumRows(SplitData, maximumJobs, secondaryStratumIds)

        Me.AddIterationRows(SplitData, maximumJobs)
        Me.CreatePartialLibraries(SplitData, True)

    End Sub

    Private Shared Function CreateSplitTableSchema() As DataTable

        Dim dt As New DataTable("Split Configuration")
        dt.Locale = CultureInfo.InvariantCulture

        dt.Columns.Add("ID", GetType(Integer))
        dt.Columns.Add("FileName", GetType(String))
        dt.Columns.Add("SecondaryStratumIds", GetType(String))
        dt.Columns.Add("NumIterations", GetType(Integer))
        dt.Columns.Add("MinIteration", GetType(Integer))
        dt.Columns.Add("MaxIteration", GetType(Integer))
        dt.Columns.Add("TotalAmount", GetType(Double))
        dt.Columns.Add("NumCells", GetType(Integer))
        dt.Columns.Add("RelativeAmountTotal", GetType(Double))
        dt.Columns.Add("RATIO", GetType(Double))
        dt.Columns.Add("JobTotalAmount", GetType(Double))
        dt.Columns.Add("JobNumCells", GetType(Integer))
        dt.Columns.Add("JobRelativeAmountTotal", GetType(Double))

        Return dt

    End Function

    Public Overrides Function GetMaximumJobs() As Integer

        Dim MaxJobs As Integer = MyBase.GetMaximumJobs()

        If (Me.IsSplitByStrata()) Then
            Return (MaxJobs * Me.GetApplicableSecondaryStrata().Count)
        Else
            Return MaxJobs
        End If

    End Function

    Private Shared Sub AddSecondaryStratumRows(
        ByVal splitData As DataTable,
        ByVal maximumJobs As Integer,
        ByVal secondaryStratumIds As List(Of Integer))

        Debug.Assert(splitData.Rows.Count = 0)

        Dim TotalRows As Integer = Math.Min(maximumJobs, secondaryStratumIds.Count)

        For i As Integer = 0 To TotalRows - 1
            splitData.Rows.Add({i})
        Next

        Dim RowIndex As Integer = 0

        For i As Integer = 0 To secondaryStratumIds.Count - 1

            Dim dr As DataRow = splitData.Rows(RowIndex)
            Dim ssid As Integer = secondaryStratumIds(i)
            Dim NewIds As String = Nothing

            If (dr("SecondaryStratumIds") Is DBNull.Value) Then
                NewIds = CStr(ssid)
            Else

                NewIds = String.Format(CultureInfo.InvariantCulture,
                    "{0},{1}", CInt(dr("SecondaryStratumIds")), ssid)

            End If

            dr("SecondaryStratumIds") = NewIds
            RowIndex += 1

            If (RowIndex > splitData.Rows.Count - 1) Then
                RowIndex = 0
            End If

        Next

        Debug.Assert(TotalRows <= maximumJobs)
        Debug.Assert(splitData.Rows.Count = TotalRows)

    End Sub

    Private Sub AddIterationRows(
        ByVal splitData As DataTable,
        ByVal maximumJobs As Integer)

        Debug.Assert(splitData.Rows.Count <= maximumJobs)

        Dim dt As DataTable = splitData.Clone
        Dim TotalIterations As Integer = (Me.MaximumIteration - Me.MinimumIteration + 1)
        Dim NewMaximumJobs As Integer = Math.Min(maximumJobs, (splitData.Rows.Count * TotalIterations))

        For Each dr As DataRow In splitData.Rows
            CloneRow(dr, dt)
        Next

        Dim RowIndex As Integer = 0

        While (dt.Rows.Count < NewMaximumJobs)

            Dim dr As DataRow = splitData.Rows(RowIndex)

            CloneRow(dr, dt)
            RowIndex += 1

            If (RowIndex > splitData.Rows.Count - 1) Then
                RowIndex = 0
            End If

        End While

        splitData.Rows.Clear()

        For Each dr As DataRow In dt.Rows
            CloneRow(dr, splitData)
        Next

        AssignIterationCounts(splitData, TotalIterations)
        Me.CreateIterationRanges(splitData)

        Debug.Assert(splitData.Rows.Count = NewMaximumJobs)

    End Sub

    Private Shared Sub AssignIterationCounts(
        ByVal splitData As DataTable,
        ByVal totalIterations As Integer)

        Dim RowIds As List(Of Integer) = CreateUniqueIdList(splitData, "ID")

        For Each id As Integer In RowIds

            Dim q As String = String.Format(CultureInfo.InvariantCulture, "ID={0}", id)
            Dim rows() As DataRow = splitData.Select(q)

            Debug.Assert(rows.Count > 0)

            If (rows.Count = 1) Then
                rows(0)("NumIterations") = totalIterations
            Else

                Dim RowIndex As Integer = 0
                Dim IterationsRemaining As Integer = totalIterations

                For Each dr As DataRow In rows
                    dr("NumIterations") = 0
                Next

                While (IterationsRemaining > 0)

                    Dim dr As DataRow = rows(RowIndex)
                    dr("NumIterations") = CInt(dr("NumIterations")) + 1

                    RowIndex += 1

                    If (RowIndex > rows.Count - 1) Then
                        RowIndex = 0
                    End If

                    IterationsRemaining -= 1

                End While

            End If

        Next

    End Sub

    Private Sub CreateIterationRanges(ByVal splitData As DataTable)

        Dim RowIds As List(Of Integer) = CreateUniqueIdList(splitData, "ID")

        For Each id As Integer In RowIds

            Dim q As String = String.Format(CultureInfo.InvariantCulture, "ID={0}", id)
            Dim rows() As DataRow = splitData.Select(q)

            Debug.Assert(rows.Count > 0)

            Dim MinIter As Integer = Me.MinimumIteration

            For RowIndex As Integer = 0 To rows.Count - 1

                Dim dr As DataRow = rows(RowIndex)
                Dim NumIters As Integer = CInt(dr("NumIterations"))
                Dim MaxIter As Integer = MinIter + NumIters - 1

                dr("MinIteration") = MinIter
                dr("MaxIteration") = MaxIter

                MinIter = MaxIter + 1

            Next

        Next

    End Sub

    Private Sub CalculateNewInitialConditions(ByVal splitData As DataTable)

        Dim icdata As DataRow = Me.ResultScenario.GetDataSheet(DATASHEET_NSIC_NAME).GetDataRow()
        Dim distdata As DataTable = Me.ResultScenario.GetDataSheet(DATASHEET_NSIC_DISTRIBUTION_NAME).GetData()
        Dim TotalAmount As Double = CDbl(icdata(DATASHEET_NSIC_TOTAL_AMOUNT_COLUMN_NAME))
        Dim NumCells As Integer = CInt(icdata(DATASHEET_NSIC_NUM_CELLS_COLUMN_NAME))
        Dim RelativeAmountTotal = CDbl(distdata.Compute("SUM(RelativeAmount)", Nothing))

        For Each dr As DataRow In splitData.Rows

            Dim ids As String = CStr(dr("SecondaryStratumIds"))
            Dim q As String = String.Format(CultureInfo.InvariantCulture, "SecondaryStratumID IN({0})", ids)
            Dim JobRelativeAmountTotal As Double = CDbl(distdata.Compute("SUM(RelativeAmount)", q))
            Dim ratio As Double = JobRelativeAmountTotal / RelativeAmountTotal

            dr("TotalAmount") = TotalAmount
            dr("NumCells") = NumCells
            dr("RelativeAmountTotal") = RelativeAmountTotal

            dr("RATIO") = ratio

            dr("JobTotalAmount") = TotalAmount * ratio
            dr("JobNumCells") = CInt(NumCells * ratio)
            dr("JobRelativeAmountTotal") = JobRelativeAmountTotal

        Next

    End Sub

    Private Sub CreatePartialLibraries(ByVal splitData As DataTable, ByVal isSpatialSplit As Boolean)

        Dim psl As String = Nothing
        Dim ssl As String = Nothing

        GetStratumLabelTerminology(Me.Project.GetDataSheet(DATASHEET_TERMINOLOGY_NAME), psl, ssl)

        'Save the library because we may have added status data to the scenario

        If (Me.Library.HasChanges()) Then
            Me.Library.Save()
        End If

        Me.BeginProgress(splitData.Rows.Count + 1)

        Me.SetStatusMessage(String.Format(CultureInfo.CurrentCulture,
            "Preparing Data For Parallel Processing (Split by '{0}')", ssl))

        'We have numJobs + 1 because we want the progress indicator to include
        'the time it takes to create the partial library.

        Me.CreatePartialLibrary()
        Me.StepProgress()

        'Then make a copy of the partial library for each job.  Note, however, that for the final
        'job we move the partial library instead of copying it.

        Dim Files As New List(Of String)
        Dim dv As New DataView(splitData, Nothing, "ID", DataViewRowState.CurrentRows)
        Dim JobId As Integer = 1

        For Each drv As DataRowView In dv

            Dim JobFileName As String = String.Format(CultureInfo.InvariantCulture, "Job-{0}.ssim", JobId)
            Dim FullFileName As String = Path.Combine(Me.JobFolderName, JobFileName)

            drv.Row("FileName") = FullFileName

            If (JobId = splitData.Rows.Count) Then
                File.Move(Me.PartialLibraryName, FullFileName)
            Else
                File.Copy(Me.PartialLibraryName, FullFileName)
            End If

            Me.ConfigureDatabase(drv.Row, isSpatialSplit)

            Debug.Assert(Not Files.Contains(FullFileName))
            Files.Add(FullFileName)

            Me.StepProgress()

            JobId += 1

        Next

        Me.CompleteProgress()

        'If it is a spatial split, configure the external files

        If (isSpatialSplit) Then
            Me.ConfigureExternalFiles(splitData)
        End If

        'Create the configuration file

        Me.CreateSplitConfigurationFile(Files, splitData.Rows.Count)

        'The partial file and input folder should now be gone

        Debug.Assert(Not File.Exists(Me.PartialLibraryName))
        Debug.Assert(Not Directory.Exists(Path.Combine(Me.JobFolderName, "Partial.ssim.input")))

        Me.SetStatusMessage(Nothing)

    End Sub

    Private Sub ConfigureExternalFiles(ByVal splitData As DataTable)

        Dim PartialInputFolderName As String = Path.Combine(Me.JobFolderName, "Partial.ssim.input")

        If (Not Directory.Exists(PartialInputFolderName)) Then
            Return
        End If

        For i As Integer = 0 To splitData.Rows.Count - 1

            Dim dr As DataRow = splitData.Rows(i)
            Dim f As String = CStr(dr("FileName"))
            Dim BaseFolderName As String = Path.GetFileName(f) & ".input"
            Dim JobInputFolderName As String = Path.Combine(Me.JobFolderName, BaseFolderName)

            If (i = splitData.Rows.Count - 1) Then
                Directory.Move(PartialInputFolderName, JobInputFolderName)
            Else
                My.Computer.FileSystem.CopyDirectory(PartialInputFolderName, JobInputFolderName, True)
            End If

        Next

    End Sub

    Private Sub ConfigureDatabase(ByVal splitDataRow As DataRow, ByVal isSpatailSplit As Boolean)

        Using scope As SyncroSimTransactionScope = Session.CreateTransactionScope()

            Dim FileName As String = CStr(splitDataRow("FileName"))

            Using store As DataStore = Session.CreateDataStore(
                New DataStoreConnection(SQLITE_DATASTORE_NAME, FileName))

                UpdateRunControl(splitDataRow, store)
                RemoveSecondaryStrata(splitDataRow, store, Me.ResultScenario)

                If (Not isSpatailSplit) Then

                    UpdateInitialConditions(splitDataRow, store)
                    UpdateTransitionTargets(splitDataRow, store)
                    UpdateTransitionAttributeTargets(splitDataRow, store)

                End If

            End Using

            scope.Complete()

        End Using

    End Sub

    Private Shared Sub UpdateRunControl(ByVal splitDataRow As DataRow, ByVal store As DataStore)

        Dim MinIter As Integer = CInt(splitDataRow("MinIteration"))
        Dim MaxIter As Integer = CInt(splitDataRow("MaxIteration"))

        Debug.Assert(MinIter <= MaxIter)

        Dim q As String = String.Format(CultureInfo.InvariantCulture,
            "UPDATE {0} SET {1}={2}, {3}={4}",
            DATASHEET_RUN_CONTROL_NAME,
            RUN_CONTROL_MIN_ITERATION_COLUMN_NAME, MinIter,
            RUN_CONTROL_MAX_ITERATION_COLUMN_NAME, MaxIter)

        store.ExecuteNonQuery(q)

    End Sub

    Private Shared Sub RemoveSecondaryStrata(
        ByVal splitDataRow As DataRow,
        ByVal store As DataStore,
        ByVal scenario As Scenario)

        Debug.Assert(splitDataRow("SecondaryStratumIds") IsNot DBNull.Value)

        Dim ids As String = CStr(splitDataRow("SecondaryStratumIds"))

        store.ExecuteNonQuery(String.Format(CultureInfo.InvariantCulture,
            "DELETE FROM STSim_SecondaryStratum WHERE SecondaryStratumID NOT IN({0})", ids))

        For Each df As DataFeed In scenario.DataFeeds

            If (df.IsOutput) Then
                Continue For
            End If

            For Each ds As DataSheet In df.DataSheets

                If (Not ds.IsPersistable) Then
                    Continue For
                End If

                For Each dc As DataSheetColumn In ds.Columns

                    If (dc.ValidationTable IsNot Nothing AndAlso
                        dc.ValidationType = ColumnValidationType.IsDataSheet AndAlso
                        dc.Formula1 = "STSim_SecondaryStratum") Then

                        store.ExecuteNonQuery(String.Format(CultureInfo.InvariantCulture,
                            "DELETE FROM [{0}] WHERE {1} NOT IN ({2})", ds.Name, dc.Name, ids))

                    End If

                Next

            Next

        Next

    End Sub

    Private Shared Sub UpdateInitialConditions(ByVal splitDataRow As DataRow, ByVal store As DataStore)

        Debug.Assert(splitDataRow("SecondaryStratumIds") IsNot DBNull.Value)

        store.ExecuteNonQuery(String.Format(CultureInfo.InvariantCulture,
            "UPDATE STSim_InitialConditionsNonSpatial SET TotalAmount={0}",
            CDbl(splitDataRow("JobTotalAmount"))))

        store.ExecuteNonQuery(String.Format(CultureInfo.InvariantCulture,
            "UPDATE STSim_InitialConditionsNonSpatial SET NumCells={0}",
            CDbl(splitDataRow("JobNumCells"))))

        store.ExecuteNonQuery(String.Format(CultureInfo.InvariantCulture,
            "UPDATE STSim_InitialConditionsNonSpatialDistribution SET RelativeAmount=(RelativeAmount * {0})",
            CDbl(splitDataRow("RATIO"))))

    End Sub

    Private Shared Sub UpdateTransitionTargets(ByVal splitDataRow As DataRow, ByVal store As DataStore)

        Debug.Assert(splitDataRow("SecondaryStratumIds") IsNot DBNull.Value)

        Dim ratio As Double = CDbl(splitDataRow("RATIO"))

        store.ExecuteNonQuery(String.Format(CultureInfo.InvariantCulture,
            "UPDATE STSim_TransitionTarget SET Amount=(Amount * {0}) WHERE SecondaryStratumID IS NULL", ratio))

        store.ExecuteNonQuery(String.Format(CultureInfo.InvariantCulture,
            "UPDATE STSim_TransitionTarget SET DistributionSD=(DistributionSD * {0}) WHERE SecondaryStratumID IS NULL", ratio))

        store.ExecuteNonQuery(String.Format(CultureInfo.InvariantCulture,
            "UPDATE STSim_TransitionTarget SET DistributionMin=(DistributionMin * {0}) WHERE SecondaryStratumID IS NULL", ratio))

        store.ExecuteNonQuery(String.Format(CultureInfo.InvariantCulture,
            "UPDATE STSim_TransitionTarget SET DistributionMax=(DistributionMax * {0}) WHERE SecondaryStratumID IS NULL", ratio))

    End Sub

    Private Shared Sub UpdateTransitionAttributeTargets(ByVal splitDataRow As DataRow, ByVal store As DataStore)

        Debug.Assert(splitDataRow("SecondaryStratumIds") IsNot DBNull.Value)

        Dim ratio As Double = CDbl(splitDataRow("RATIO"))

        store.ExecuteNonQuery(String.Format(CultureInfo.InvariantCulture,
            "UPDATE STSim_TransitionAttributeTarget SET Amount=(Amount * {0}) WHERE SecondaryStratumID IS NULL", ratio))

        store.ExecuteNonQuery(String.Format(CultureInfo.InvariantCulture,
            "UPDATE STSim_TransitionAttributeTarget SET DistributionSD=(DistributionSD * {0}) WHERE SecondaryStratumID IS NULL", ratio))

        store.ExecuteNonQuery(String.Format(CultureInfo.InvariantCulture,
            "UPDATE STSim_TransitionAttributeTarget SET DistributionMin=(DistributionMin * {0}) WHERE SecondaryStratumID IS NULL", ratio))

        store.ExecuteNonQuery(String.Format(CultureInfo.InvariantCulture,
            "UPDATE STSim_TransitionAttributeTarget SET DistributionMax=(DistributionMax * {0}) WHERE SecondaryStratumID IS NULL", ratio))

    End Sub

    Private Function GetApplicableSecondaryStrata() As List(Of Integer)

        Dim l As New List(Of Integer)
        Dim psl As String = Nothing
        Dim ssl As String = Nothing

        GetStratumLabelTerminology(Me.Project.GetDataSheet(DATASHEET_TERMINOLOGY_NAME), psl, ssl)
        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_NSIC_DISTRIBUTION_NAME)

        For Each dr As DataRow In ds.GetData().Rows

            If (dr(DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME) Is DBNull.Value) Then

                ExceptionUtils.ThrowInvalidOperationException(
                    "Cannot split by '{0}' because '{1}' is not specified for all records in Initial Conditions Distribution.", ssl, ssl)

            End If

            Dim id As Integer = CInt(dr(DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME))

            If (Not l.Contains(id)) Then
                l.Add(id)
            End If

        Next

        Debug.Assert(l.Count > 0)
        Return l

    End Function

    Private Shared Sub CloneRow(ByVal sourceRow As DataRow, ByVal targetTable As DataTable)

        Dim r As DataRow = targetTable.NewRow

        For Each dc As DataColumn In targetTable.Columns
            r(dc.ColumnName) = sourceRow(dc.ColumnName)
        Next

        targetTable.Rows.Add(r)

    End Sub

    Private Shared Function CreateUniqueIdList(ByVal dt As DataTable, ByVal columnName As String) As List(Of Integer)

        Dim Ids As New List(Of Integer)

        For Each dr As DataRow In dt.Rows

            Dim id As Integer = CInt(dr(columnName))

            If (Not Ids.Contains(id)) Then
                Ids.Add(id)
            End If

        Next

        Ids.Sort()
        Return Ids

    End Function

    Private Shared Function NullValueExists(ByVal dt As DataTable, ByVal columnName As String) As Boolean

        For Each dr As DataRow In dt.Rows

            If (dr(columnName) Is DBNull.Value) Then
                Return True
            End If

        Next

        Return False

    End Function

    Private Function IsSplitByStrata() As Boolean

        Dim dr As DataRow = Me.ResultScenario.GetDataSheet(DATASHEET_PROCESSING_NAME).GetDataRow()

        If (dr Is Nothing) Then
            Return False
        End If

        Return DataTableUtilities.GetDataBool(dr(DATASHEET_PROCESSING_SPLIT_BY_SS_COLUMN_NAME))

    End Function

    Private Sub ValidateNormalSplit()

        Dim psl As String = Nothing
        Dim ssl As String = Nothing
        Dim aml As String = Nothing
        Dim amu As TerminologyUnit

        Dim tds As DataSheet = Me.Project.GetDataSheet(DATASHEET_TERMINOLOGY_NAME)

        GetStratumLabelTerminology(tds, psl, ssl)
        GetAmountLabelTerminology(tds, aml, amu)

        'We don't support splits by secondary strata for spatial runs as this time

        If (Me.IsSpatial()) Then

            ExceptionUtils.ThrowInvalidOperationException(
                "Cannot split by '{0}' for a spatial model run.", ssl)

        End If

        'If there are less than 2 secondary strata records referenced by 
        'Initial Conditions Distribution we cannot do a split by secondary strata

        Dim l As List(Of Integer) = Me.GetApplicableSecondaryStrata()

        If (l.Count < 2) Then

            ExceptionUtils.ThrowInvalidOperationException(
                "Cannot split by '{0}' because there are fewer than two references to '{1}' in Initial Conditions Distribution.",
                ssl, ssl)

        End If

        'If there are Transition Targets with NULL secondary stata then add a warning

        If (NullValueExists(Me.ResultScenario.GetDataSheet(DATASHEET_TRANSITION_TARGET_NAME).GetData, DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME)) Then

            Me.AddStatusRecord(StatusRecordType.Warning, String.Format(CultureInfo.CurrentCulture,
                "Run is splitting by '{0}' but Transition Targets are not specified by '{1}'.  Allocating targets in proportion to '{2}'.",
                ssl, ssl, aml))

        End If

        'If there are Transition Attribute Targets with NULL secondary stata then add a warning

        If (NullValueExists(Me.ResultScenario.GetDataSheet(DATASHEET_TRANSITION_ATTRIBUTE_TARGET_NAME).GetData, DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME)) Then

            Me.AddStatusRecord(StatusRecordType.Warning, String.Format(CultureInfo.CurrentCulture,
                "Run is splitting by '{0}' but Transition Attribute Targets are not specified by '{1}'.  Allocating targets in proportion to '{2}'.",
                ssl, ssl, aml))

        End If

    End Sub

    Private Sub ValidateSpatialSplit()

        'If there are Transition Targets with NULL secondary stata then log a warning

        If (NullValueExists(Me.ResultScenario.GetDataSheet(DATASHEET_TRANSITION_TARGET_NAME).GetData, DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME)) Then

            Me.AddStatusRecord(StatusRecordType.Warning,
                "Run is splitting by Secondary Stratum but Transition Targets are not specified by Secondary Stratum.")

        End If

        'If there are Transition Attribute Targets with NULL secondary stata then log a warning

        If (NullValueExists(Me.ResultScenario.GetDataSheet(DATASHEET_TRANSITION_ATTRIBUTE_TARGET_NAME).GetData, DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME)) Then

            Me.AddStatusRecord(StatusRecordType.Warning,
                "Run is splitting by Secondary Stratum but Transition Attribute Targets are not specified by Secondary Stratum.")

        End If

    End Sub

End Class
