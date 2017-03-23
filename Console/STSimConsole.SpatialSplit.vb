'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports System.IO
Imports SyncroSim.Core
Imports System.Globalization

Partial Class STSimConsole

    Private Sub HandleSpatialSplitArgument()

        If (Me.Help) Then
            PrintSpatialSplitHelp()
        Else
            Me.SpatialSplit()
        End If

    End Sub

    Private Sub SpatialSplit()

        Dim ScenarioId As Integer = Me.GetDBIdArgument("sid")
        Dim OpenLibrary As Library = Me.OpenLibrary()
        Dim OpenScenario As Scenario = GetScenario(ScenarioId, OpenLibrary)
        Dim NumJobs As Nullable(Of Integer) = Me.ParseJobsArgument()
        Dim OutDir As String = Me.CreateOutputDirectory(OpenScenario)
        Dim ssids As List(Of Integer) = Me.GetSecondaryStratumIds(OpenScenario)

        Dim trx As STSimTransformer =
            CType(Me.Session.CreateTransformer("stsim:runtime",
                OpenLibrary, OpenScenario.Project, OpenScenario, OpenScenario), STSimTransformer)

        If (Not NumJobs.HasValue) Then
            NumJobs = Session.MaxParallelJobsDefault()
        End If

        trx.Configure()
        trx.JobFolderName = OutDir
        trx.SpatialSplit(NumJobs.Value, ssids)

        Me.PrintQuiet("Partial scenario ID is: {0}", trx.PartialScenarioId)

    End Sub

    Private Function CreateOutputDirectory(ByVal s As Scenario) As String

        Dim a As String = Me.GetArgument("out")

        If (String.IsNullOrEmpty(a)) Then

            Using store As DataStore = s.Library.CreateDataStore()
                a = s.Library.GetFolderName(LibraryFolderType.Temporary, s, False, store)
            End Using

            a = Path.Combine(a, "SSimJobs")

        Else
            a = Path.GetFullPath(a)
        End If

        If (Directory.Exists(a)) Then
            ExceptionUtils.ThrowArgumentException("The directory exists: {0}", a)
        End If

        Directory.CreateDirectory(a)
        Return a

    End Function

    Private Function ParseJobsArgument() As Nullable(Of Integer)

        Dim v As Integer = 0
        Dim a As String = Me.GetArgument("jobs")

        If (String.IsNullOrEmpty(a)) Then
            Return Nothing
        End If

        If (Me.GetArgument("child-process") = "True") Then
            ExceptionUtils.ThrowArgumentException("The --jobs argument cannot be used with the --child-process argument.")
        End If

        If (Not Integer.TryParse(a, NumberStyles.Any, CultureInfo.InvariantCulture, v)) Then
            ExceptionUtils.ThrowArgumentException("The format for the --jobs argument is not correct.")
        End If

        If (v <= 0) Then
            ExceptionUtils.ThrowArgumentException("The value for the --jobs argument must be greater than zero.")
        End If

        Return v

    End Function

    Private Function GetSecondaryStratumIds(ByVal s As Scenario) As List(Of Integer)

        Dim l As List(Of Integer) = Nothing
        Dim a As String = Me.GetArgument("ssids")

        If (String.IsNullOrEmpty(a)) Then
            l = GetAllSecondaryStratumIds(s)
        Else
            l = Me.GetExplicitSecondaryStratumIds(s)
        End If

        If (l.Count < 2) Then
            ExceptionUtils.ThrowArgumentException("Cannot split with fewer than 2 Secondary Strata.")
        End If

        Return l

    End Function

    Private Function GetExplicitSecondaryStratumIds(ByVal s As Scenario) As List(Of Integer)

        Dim l As New List(Of Integer)
        Dim ds As DataSheet = s.Project.GetDataSheet(DATASHEET_SECONDARY_STRATA_NAME)
        Dim dt As DataTable = ds.GetData()
        Dim ssids As IEnumerable(Of Integer) = Me.GetMultiDBIdArgument("ssids")

        For Each ssid As Integer In ssids

            Dim pkid As Integer = FindSecondaryStratumId(ssid, dt, ds.PrimaryKeyColumn.Name)

            If (Not l.Contains(pkid)) Then
                l.Add(pkid)
            End If

        Next

        Return l

    End Function

    Private Shared Function GetAllSecondaryStratumIds(ByVal s As Scenario) As List(Of Integer)

        Dim l As New List(Of Integer)
        Dim ds As DataSheet = s.Project.GetDataSheet(DATASHEET_SECONDARY_STRATA_NAME)
        Dim dt As DataTable = ds.GetData()

        For Each dr As DataRow In dt.Rows

            Dim pkid As Integer = CInt(dr(ds.PrimaryKeyColumn.Name))

            Debug.Assert(Not l.Contains(pkid))

            If (Not l.Contains(pkid)) Then
                l.Add(pkid)
            End If

        Next

        Return l

    End Function

    Private Shared Function FindSecondaryStratumId(
        ByVal ssid As Integer,
        ByVal data As DataTable,
        ByVal pkidColumnName As String) As Integer

        For Each dr As DataRow In data.Rows

            If (dr(DATASHEET_MAPID_COLUMN_NAME) IsNot DBNull.Value) Then

                Dim id As Integer = CInt(dr(DATASHEET_MAPID_COLUMN_NAME))

                If (id = ssid) Then
                    Return CInt(dr(pkidColumnName))
                End If

            End If

        Next

        ExceptionUtils.ThrowArgumentException(
            "A Secondary Stratum with the ID '{0}' was not found.", ssid)

        Return 0

    End Function

    Private Shared Sub PrintSpatialSplitHelp()

        System.Console.WriteLine("Splits an ST-Sim library spatially")
        System.Console.WriteLine("USAGE: --spatial-split [Arguments]")
        System.Console.WriteLine()
        System.Console.WriteLine("  --lib={name}     The library file name")
        System.Console.WriteLine("  --sid={id}       The scenario (or result scenario) ID.")
        System.Console.WriteLine("  --ssids={id}     The secondary stratum IDs for the split.  Optional.")
        System.Console.WriteLine("  --jobs={n}       The number of jobs to create.  Optional.")
        System.Console.WriteLine("  --out={name}     The name the output directory.  Optional.")
        System.Console.WriteLine()
        System.Console.WriteLine("Examples:")
        System.Console.WriteLine("  --spatial-split --lib=test.ssim --sid=123 --ssids=1,2,3")
        System.Console.WriteLine("  --spatial-split --lib=test.ssim --sid=123 --jobs=3 --ssids=1,2,3")
        System.Console.WriteLine("  --spatial-split --lib=test.ssim --sid=123 --out=c:\myfiles\split-123 --ssids=1,2,3")
        System.Console.WriteLine()
        System.Console.WriteLine("Notes:")
        System.Console.WriteLine("The library will be split by secondary stratum first and then by iteration.")

    End Sub

End Class
