// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Linq;
using SyncroSim.Core;
using SyncroSim.Core.Forms;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    internal partial class STSimConsole
    {
        private void HandleCreateReportArgument()
        {
            if (this.Help)
            {
                PrintCreateReportHelp();
            }
            else
            {
                this.CreateReport();
            }
        }

        private void CreateReport()
        {
            string n = this.GetReportName();
            string f = this.GetOutputFileName();
            Library l = this.OpenLibrary();
            IEnumerable<int> sids = this.GetMultiDatabaseIdArguments("sids");
            Project p = this.ConfigureReportActiveProject(sids, l);

            ValidateReportScenarios(sids, l);

            using (DataStore store = l.CreateDataStore())
            {
                if (n == Strings.STATECLASS_SUMMARY_REPORT_NAME)
                {
                    StateClassSummaryReport t = (StateClassSummaryReport)this.Session.CreateTransformer(
                        "stsim:state-class-summary-report", p.Library, p, null);

                    t.InternalExport(f, ExportType.CSVFile, false);
                }
                else if (n == Strings.TRANSITION_SUMMARY_REPORT_NAME)
                {
                    TransitionSummaryReport t = (TransitionSummaryReport)this.Session.CreateTransformer(
                        "stsim:transition-summary-report", p.Library, p, null);

                    t.InternalExport(f, ExportType.CSVFile, false);
                }
                else if (n == Strings.TRANSITION_STATECLASS_SUMMARY_REPORT_NAME)
                {
                    TransitionStateSummaryReport t = (TransitionStateSummaryReport)this.Session.CreateTransformer(
                        "stsim:transition-state-summary-report", p.Library, p, null);

                    t.InternalExport(f, ExportType.CSVFile, false);
                }
                else if (n == Strings.STATE_ATTRIBUTE_REPORT_NAME)
                {
                    StateAttributeReport t = (StateAttributeReport)this.Session.CreateTransformer(
                        "stsim:state-attribute-report", p.Library, p, null);

                    t.InternalExport(f, ExportType.CSVFile, false);
                }
                else if (n == Strings.TRANSITION_ATTRIBUTE_REPORT_NAME)
                {
                    TransitionAttributeReport t = (TransitionAttributeReport)this.Session.CreateTransformer(
                        "stsim:transition-attribute-report", p.Library, p, null);

                    t.InternalExport(f, ExportType.CSVFile, false);
                }
            }
        }

        private string GetReportName()
        {
            string n = this.GetRequiredArgument("name");

            if (n != Strings.STATECLASS_SUMMARY_REPORT_NAME && 
                n != Strings.TRANSITION_SUMMARY_REPORT_NAME && 
                n != Strings.TRANSITION_STATECLASS_SUMMARY_REPORT_NAME && 
                n != Strings.STATE_ATTRIBUTE_REPORT_NAME && 
                n != Strings.TRANSITION_ATTRIBUTE_REPORT_NAME)
            {
                ExceptionUtils.ThrowArgumentException("The report name is not valid.");
            }

            return n;
        }

        private static void ValidateReportScenarios(IEnumerable<int> sids, Library l)
        {
            Dictionary<int, bool> pids = new Dictionary<int, bool>();

            foreach (int id in sids)
            {
                if (!l.Scenarios.Contains(id))
                {
                    ExceptionUtils.ThrowArgumentException("The scenario does not exist: {0}", id);
                }

                Scenario s = l.Scenarios[id];

                if (!s.IsResult)
                {
                    ExceptionUtils.ThrowArgumentException("The scenario is not a result scenario: {0}", id);
                }

                if (!pids.ContainsKey(s.Project.Id))
                {
                    pids.Add(s.Project.Id, true);
                }

                if (pids.Count > 1)
                {
                    ExceptionUtils.ThrowArgumentException("The scenarios must belong to the same project: {0}", id);
                }
            }
        }

        private Project ConfigureReportActiveProject(IEnumerable<int> sids, Library l)
        {
            Project p = l.Scenarios[sids.First()].Project;
            Session.SetActiveProject(p);

            foreach (int id in sids)
            {
                Scenario s = l.Scenarios[id];
                s.IsActive = true;

                p.Results.Add(s);
            }

            return p;
        }

        private static void PrintCreateReportHelp()
        {
            System.Console.WriteLine("Creates an ST-Sim report");
            System.Console.WriteLine("USAGE: --create-report [Arguments]");
            System.Console.WriteLine();
            System.Console.WriteLine("  --lib={name}     The library file name");
            System.Console.WriteLine("  --sids={ids}     The scenario IDs separated by commas.  [Multiple IDs must be enclosed in quotes]");
            System.Console.WriteLine("  --name={name}    The name of the report to create");
            System.Console.WriteLine("  --file={name}    The file name for the report");
            System.Console.WriteLine();
            System.Console.WriteLine("Examples:");
            System.Console.WriteLine("  --create-report --lib=test.ssim --sids=\"1,2,3\" --name=stateclass-summary --file=sc.csv");
            System.Console.WriteLine("  --create-report --lib=\"my lib.ssim\" --sids=1 --name=stateclass-summary --file=\"my data.csv\"");
        }
    }
}
