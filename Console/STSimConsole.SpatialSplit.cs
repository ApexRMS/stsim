// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.IO;
using System.Data;
using SyncroSim.Core;
using System.Globalization;
using System.Collections.Generic;
using System.Diagnostics;

namespace SyncroSim.STSim
{
    internal partial class STSimConsole
    {
        private void HandleSpatialSplitArgument()
        {
            if (this.Help)
            {
                PrintSpatialSplitHelp();
            }
            else
            {
                this.SpatialSplit();
            }
        }

        private void SpatialSplit()
        {
            int ScenarioId = this.GetDatabaseIdArgument("sid");
            Library OpenLibrary = this.OpenLibrary();
            Scenario OpenScenario = GetScenario(ScenarioId, OpenLibrary);
            int? NumJobs = this.ParseJobsArgument();
            string OutDir = this.CreateOutputDirectory(OpenScenario);
            List<int> ssids = this.GetSecondaryStratumIds(OpenScenario);

            STSimTransformer trx = (STSimTransformer)this.Session.CreateTransformer(
                "stsim:runtime", OpenLibrary, OpenScenario.Project, OpenScenario, OpenScenario);

            if (!NumJobs.HasValue)
            {
                NumJobs = MaxParallelJobsDefault();
            }

            trx.Configure();
            trx.JobFolderName = OutDir;
            trx.SpatialSplit(NumJobs.Value, ssids);

            this.PrintQuiet("Partial scenario ID is: {0}", trx.PartialScenarioId);
        }

        private static int MaxParallelJobsDefault()
        {
            if (Environment.ProcessorCount <= 2)
            {
                return 2;
            }
            else
            {
                return Environment.ProcessorCount - 1;
            }
        }

        private string CreateOutputDirectory(Scenario s)
        {
            string a = this.GetArgument("out");

            if (string.IsNullOrEmpty(a))
            {
                a = s.Library.GetFolderName(LibraryFolderType.Temporary, s, false);
                a = Path.Combine(a, "SSimJobs");
            }
            else
            {
                a = Path.GetFullPath(a);
            }

            if (Directory.Exists(a))
            {
                ExceptionUtils.ThrowArgumentException("The directory exists: {0}", a);
            }

            Directory.CreateDirectory(a);
            return a;
        }

        private int? ParseJobsArgument()
        {
            int v = 0;
            string a = this.GetArgument("jobs");

            if (string.IsNullOrEmpty(a))
            {
                return null;
            }

            if (this.GetArgument("child-process") == "True")
            {
                ExceptionUtils.ThrowArgumentException("The --jobs argument cannot be used with the --child-process argument.");
            }

            if (!int.TryParse(a, NumberStyles.Any, CultureInfo.InvariantCulture, out v))
            {
                ExceptionUtils.ThrowArgumentException("The format for the --jobs argument is not correct.");
            }

            if (v <= 0)
            {
                ExceptionUtils.ThrowArgumentException("The value for the --jobs argument must be greater than zero.");
            }

            return v;
        }

        private List<int> GetSecondaryStratumIds(Scenario s)
        {
            List<int> l = null;
            string a = this.GetArgument("ssids");

            if (string.IsNullOrEmpty(a))
            {
                l = GetAllSecondaryStratumIds(s);
            }
            else
            {
                l = this.GetExplicitSecondaryStratumIds(s);
            }

            if (l.Count < 2)
            {
                ExceptionUtils.ThrowArgumentException("Cannot split with fewer than 2 Secondary Strata.");
            }

            return l;
        }

        private List<int> GetExplicitSecondaryStratumIds(Scenario s)
        {
            List<int> l = new List<int>();
            DataSheet ds = s.Project.GetDataSheet(Strings.DATASHEET_SECONDARY_STRATA_NAME);
            DataTable dt = ds.GetData();
            IEnumerable<int> ssids = this.GetMultiDatabaseIdArguments("ssids");

            foreach (int ssid in ssids)
            {
                int pkid = FindSecondaryStratumId(ssid, dt, ds.PrimaryKeyColumn.Name);

                if (!l.Contains(pkid))
                {
                    l.Add(pkid);
                }
            }

            return l;
        }

        private static List<int> GetAllSecondaryStratumIds(Scenario s)
        {
            List<int> l = new List<int>();
            DataSheet ds = s.Project.GetDataSheet(Strings.DATASHEET_SECONDARY_STRATA_NAME);
            DataTable dt = ds.GetData();

            foreach (DataRow dr in dt.Rows)
            {
                int pkid = Convert.ToInt32(dr[ds.PrimaryKeyColumn.Name]);

                Debug.Assert(!l.Contains(pkid));

                if (!l.Contains(pkid))
                {
                    l.Add(pkid);
                }
            }

            return l;
        }

        private static int FindSecondaryStratumId(int ssid, DataTable data, string pkidColumnName)
        {
            foreach (DataRow dr in data.Rows)
            {
                if (dr[Strings.DATASHEET_MAPID_COLUMN_NAME] != DBNull.Value)
                {
                    int id = Convert.ToInt32(dr[Strings.DATASHEET_MAPID_COLUMN_NAME]);

                    if (id == ssid)
                    {
                        return Convert.ToInt32(dr[pkidColumnName]);
                    }
                }
            }

            ExceptionUtils.ThrowArgumentException("A Secondary Stratum with the ID '{0}' was not found.", ssid);
            return 0;
        }

        private static void PrintSpatialSplitHelp()
        {
            System.Console.WriteLine("Splits an ST-Sim library spatially");
            System.Console.WriteLine("USAGE: --spatial-split [Arguments]");
            System.Console.WriteLine();
            System.Console.WriteLine("  --lib={name}     The library file name");
            System.Console.WriteLine("  --sid={id}       The scenario (or result scenario) ID.");
            System.Console.WriteLine("  --ssids={id}     The secondary stratum IDs for the split.   [Optional.  Multiple IDs must be enclosed in quotes]");
            System.Console.WriteLine("  --jobs={n}       The number of jobs to create.              [Optional]");
            System.Console.WriteLine("  --out={name}     The name the output directory.             [Optional]");
            System.Console.WriteLine();
            System.Console.WriteLine("Examples:");
            System.Console.WriteLine("  --spatial-split --lib=test.ssim --sid=123 --ssids=1");
            System.Console.WriteLine("  --spatial-split --lib=test.ssim --sid=123 --jobs=3 --ssids=\"1,2,3\"");
            System.Console.WriteLine("  --spatial-split --lib=test.ssim --sid=123 --out=c:\\myfiles\\split-123 --ssids=\"1,2,3\"");
            System.Console.WriteLine();
            System.Console.WriteLine("Notes:");
            System.Console.WriteLine("The library will be split by secondary stratum first and then by iteration.");
        }
    }
}
