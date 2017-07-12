'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports System.IO
Imports System.Globalization
Imports SyncroSim.Core
Imports SyncroSim.StochasticTime

Partial Class STSimTransformer

    ''' <summary>
    ''' Overrides merge so we can process AATP files
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub Merge()

        'Merges the external Spatial TGAP files
        ProcessAverageTransitionProbabilityFiles()

        'Do the normal merge
        MyBase.Merge()

        'Merges the datasheet records for Spatial TGAP files
        ProcessAverageTransitionProbabilityDatasheet()

    End Sub

    ''' <summary>
    ''' Processes the Average Transition Probability Files. These raster files are a special case because there only one file per Transition Type per run, so when mulitprocessing
    ''' we need to arithmetically combine these files together.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ProcessAverageTransitionProbabilityFiles()

        Dim config As ParallelJobConfig = LoadConfigurationFile()

        ' Find the number of iterations per job
        Dim dictJobIterations As Dictionary(Of Integer, Integer) = CreateJobIterationsDictionary(config)

        ' Create Same Names Files Dictionary for tgap only for current strata
        Dim dictFilenames As Dictionary(Of String, List(Of String)) = CreateSameNameTgapFilesDictionary(config)

        If (dictFilenames.Count = 0) Then
            Return
        End If

        ' Calculate the total number of iterations across all jobs. DEVNOTE: Do it here instead of below based on files beaause we wont always
        ' have a file if not transitions.
        Dim ttlIterations As Integer = 0
        For Each jobId In dictJobIterations.Keys
            Dim numIterations As Integer = dictJobIterations(jobId)
            ttlIterations += numIterations
        Next

        For Each k As String In dictFilenames.Keys

            Dim m As New TgapMerge()
            For Each f As String In dictFilenames(k)

                Dim jobId As Integer = GetJobIdFromFolder(f)
                Dim numIterations As Integer = dictJobIterations(jobId)

                If jobId <> 0 Or numIterations > 0 Then

                    m.Merge(f, numIterations)

                    ' Delete the file after we've merged it.
                    File.SetAttributes(f, FileAttributes.Normal)
                    File.Delete(f)

                Else
                    Debug.Assert(False, "Either the Job ID Or Number of iterations are invalid")
                End If

            Next

            ' Divide the merged raster by the total number of iterations
            m.Multiply(1 / ttlIterations)

            ' Save the final merged tgap raster, giving it the same name/path as the 1st file in the dictionary for this Strata
            Dim newFilename As String = dictFilenames(k).Item(0)
            m.Save(newFilename, StochasticTime.RasterCompression.GetGeoTiffCompressionType(Me.Library))


        Next

    End Sub

    ''' <summary>
    ''' Create a dictionary of Job Iterations 
    ''' </summary>
    ''' <param name="config"></param>
    ''' <returns>A dictionary of number of iterations, keyed by job ID</returns>
    ''' <remarks></remarks>
    Private Shared Function CreateJobIterationsDictionary(ByVal config As ParallelJobConfig) As Dictionary(Of Integer, Integer)

        Dim dict As New Dictionary(Of Integer, Integer)

        For Each j As ParallelJob In config.Jobs

            Using store As DataStore = Session.CreateDataStore(New DataStoreConnection(SQLITE_DATASTORE_NAME, j.Library))

                Dim MergeScenarioId As Integer = ParallelTransformer.GetMergeScenarioId(store)
                Dim OutputFolderName As String = GetExternalFileOutputFolder(j.Library, MergeScenarioId, False)

                If (Not Directory.Exists(OutputFolderName)) Then
                    Continue For
                End If

                Dim numIterations As Integer = CInt(store.ExecuteScalar(
                        "SELECT maximumIteration - minimumIteration + 1 FROM STSim_RunControl where scenarioId=" & MergeScenarioId))

                Debug.Assert(j.JobId > 0)

                If (Not dict.ContainsKey(j.JobId)) Then
                    dict.Add(j.JobId, numIterations)
                Else
                    Debug.Assert(False, "Job #'s should be unique when parsed from folder names")
                End If

            End Using

        Next

        Return dict

    End Function

    ''' <summary>
    ''' Creates a dictionary of lists of same file names for TGAP ( Annual Avg Transition Probability) 
    ''' </summary>
    ''' <param name="config"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Each file of the same name needs to be arithmetically merged in the event of a iteration split.  
    ''' This function returns a dictionary of 'same name' files that need to be merged.
    ''' </remarks>
    Private Shared Function CreateSameNameTgapFilesDictionary(ByVal config As ParallelJobConfig) As Dictionary(Of String, List(Of String))

        Dim dict As New Dictionary(Of String, List(Of String))

        For Each j As ParallelJob In config.Jobs

            Using store As DataStore = Session.CreateDataStore(New DataStoreConnection(SQLITE_DATASTORE_NAME, j.Library))

                Dim MergeScenarioId As Integer = ParallelTransformer.GetMergeScenarioId(store)
                Dim OutputFolderName As String = GetAATPSpatialOutputFolder(j.Library, MergeScenarioId)

                If (Not Directory.Exists(OutputFolderName)) Then
                    Continue For
                End If

                For Each f As String In Directory.GetFiles(OutputFolderName, "tgap_*.tif")

                    Dim key As String = Path.GetFileName(f).ToLower(CultureInfo.InvariantCulture)

                    If (Not dict.ContainsKey(key)) Then
                        dict.Add(key, New List(Of String))
                    End If

                    Debug.Assert(Not dict(key).Contains(f))
                    dict(key).Add(f)

                Next

            End Using

        Next

        Return dict

    End Function

    ''' <summary>
    ''' Gets the Average Annuanl Transition Probability Spatial output folder for the specified file and scenario Id
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <param name="scenarioId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetAATPSpatialOutputFolder(
        ByVal fileName As String,
        ByVal scenarioId As Integer) As String

        Return Path.Combine(GetExternalFileInputOutputFolderName(fileName, scenarioId, ".output", False), DATASHEET_OUTPUT_SPATIAL_AVERAGE_TRANSITION_PROBABILITY)

    End Function

    ''' <summary>
    ''' Processes the Average Transition Probability datasheet records. This datasheet is a special case because there is only one file per Transition Type per run, so when mulitprocessing
    ''' we remove the duplicate records created per job.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ProcessAverageTransitionProbabilityDatasheet()


        Using store As DataStore = Me.Library.CreateDataStore()

            Dim query As String = String.Format(
                CultureInfo.InvariantCulture,
                "SELECT *  FROM STSim_OutputSpatialAverageTransitionProbability WHERE ScenarioId={0}", Me.ResultScenario.Id)

            Dim dt As DataTable = store.CreateDataTableFromQuery(query, "Merge")

            Dim ttlCnt As Integer = dt.Rows.Count

            query = String.Format(
                CultureInfo.InvariantCulture,
                "SELECT scenarioId,iteration,timestep, filename, band,transitionGroupId FROM STSim_OutputSpatialAverageTransitionProbability WHERE ScenarioId={0} group by iteration, timestep,band,transitionGroupId", Me.ResultScenario.Id)

            dt = store.CreateDataTableFromQuery(query, "Merge")
            If (dt.Rows.Count < ttlCnt) Then
                ' We've go dupes do lets blow away the old records and create new single copies
                query = String.Format(CultureInfo.InvariantCulture, "delete from STSim_OutputSpatialAverageTransitionProbability where ScenarioId={0}", Me.ResultScenario.Id)
                store.ExecuteNonQuery(query)

                For Each row As DataRow In dt.Rows
                    Dim band = IIf(IsDBNull(row(4)), "null", row(4))
                    query = String.Format(CultureInfo.InvariantCulture, "insert into STSim_OutputSpatialAverageTransitionProbability (ScenarioId,iteration,timestep,filename,band,transitionGroupId) values ({0},{1},{2},'{3}',{4},{5})", row(0), row(1), row(2), row(3), band, row(5))
                    store.ExecuteNonQuery(query)
                Next
            End If


        End Using


    End Sub


End Class
