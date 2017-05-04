'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core
Imports SyncroSim.Core.Forms

Partial Class STSimConsole

    Private Sub HandleCreateReportArgument()

        If (Me.Help) Then
            PrintCreateReportHelp()
        Else
            Me.CreateReport()
        End If

    End Sub

    Private Sub CreateReport()

        Dim n As String = Me.GetReportName()
        Dim f As String = Me.GetOutputFileName()
        Dim l As Library = Me.OpenLibrary()
        Dim sids As IEnumerable(Of Integer) = Me.GetMultiDatabaseIdArguments("sids")
        Dim p As Project = Me.ConfigureReportActiveProject(sids, l)

        ValidateReportScenarios(sids, l)

        Using store As DataStore = l.CreateDataStore

            If (n = STATECLASS_SUMMARY_REPORT_NAME) Then

                Dim t As StateClassSummaryReport = CType(Me.Session.CreateTransformer(
                    "stsim:state-class-summary-report", p.Library, p, Nothing), StateClassSummaryReport)

                t.InternalExport(f, ExportType.CSVFile, False)

            ElseIf (n = TRANSITION_SUMMARY_REPORT_NAME) Then

                Dim t As TransitionSummaryReport = CType(Me.Session.CreateTransformer(
                    "stsim:transition-summary-report", p.Library, p, Nothing), TransitionSummaryReport)

                t.InternalExport(f, ExportType.CSVFile, False)

            ElseIf (n = TRANSITION_STATECLASS_SUMMARY_REPORT_NAME) Then

                Dim t As TransitionStateSummaryReport = CType(Me.Session.CreateTransformer(
                    "stsim:transition-state-summary-report", p.Library, p, Nothing), TransitionStateSummaryReport)

                t.InternalExport(f, ExportType.CSVFile, False)

            ElseIf (n = STATE_ATTRIBUTE_REPORT_NAME) Then

                Dim t As StateAttributeReport = CType(Me.Session.CreateTransformer(
                    "stsim:state-attribute-report", p.Library, p, Nothing), StateAttributeReport)

                t.InternalExport(f, ExportType.CSVFile, False)

            ElseIf (n = TRANSITION_ATTRIBUTE_REPORT_NAME) Then

                Dim t As TransitionAttributeReport = CType(Me.Session.CreateTransformer(
                    "stsim:transition-attribute-report", p.Library, p, Nothing), TransitionAttributeReport)

                t.InternalExport(f, ExportType.CSVFile, False)

            End If

        End Using

    End Sub

    Private Function GetReportName() As String

        Dim n As String = Me.GetRequiredArgument("name")

        If (n <> STATECLASS_SUMMARY_REPORT_NAME And
            n <> TRANSITION_SUMMARY_REPORT_NAME And
            n <> TRANSITION_STATECLASS_SUMMARY_REPORT_NAME And
            n <> STATE_ATTRIBUTE_REPORT_NAME And
            n <> TRANSITION_ATTRIBUTE_REPORT_NAME) Then

            ExceptionUtils.ThrowArgumentException("The report name is not valid.")

        End If

        Return n

    End Function

    Private Shared Sub ValidateReportScenarios(ByVal sids As IEnumerable(Of Integer), ByVal l As Library)

        Dim pids As New Dictionary(Of Integer, Boolean)

        For Each id As Integer In sids

            If (Not l.Scenarios.Contains(id)) Then
                ExceptionUtils.ThrowArgumentException("The scenario does not exist: {0}", id)
            End If

            Dim s As Scenario = l.Scenarios(id)

            If (Not s.IsResult) Then
                ExceptionUtils.ThrowArgumentException("The scenario is not a result scenario: {0}", id)
            End If

            If (Not pids.ContainsKey(s.Project.Id)) Then
                pids.Add(s.Project.Id, True)
            End If

            If (pids.Count > 1) Then
                ExceptionUtils.ThrowArgumentException("The scenarios must belong to the same project: {0}", id)
            End If

        Next

    End Sub

    Private Function ConfigureReportActiveProject(ByVal sids As IEnumerable(Of Integer), ByVal l As Library) As Project

        Dim p As Project = l.Scenarios(sids.First).Project
        Session.SetActiveProject(p)

        For Each id As Integer In sids

            Dim s As Scenario = l.Scenarios(id)
            s.IsActive = True

            p.Results.Add(s)

        Next

        Return p

    End Function

    Private Shared Sub PrintCreateReportHelp()

        System.Console.WriteLine("Creates an ST-Sim report")
        System.Console.WriteLine("USAGE: --create-report [Arguments]")
        System.Console.WriteLine()
        System.Console.WriteLine("  --lib={name}     The library file name")
        System.Console.WriteLine("  --sids={ids}     The scenario IDs separated by commas.  [Multiple IDs must be enclosed in quotes]")
        System.Console.WriteLine("  --name={name}    The name of the report to create")
        System.Console.WriteLine("  --file={name}    The file name for the report")
        System.Console.WriteLine()
        System.Console.WriteLine("Examples:")
        System.Console.WriteLine("  --create-report --lib=test.ssim --sids=""1,2,3"" --name=stateclass-summary --file=sc.csv")
        System.Console.WriteLine("  --create-report --lib=""my lib.ssim"" --sids=1 --name=stateclass-summary --file=""my data.csv""")

    End Sub

End Class
