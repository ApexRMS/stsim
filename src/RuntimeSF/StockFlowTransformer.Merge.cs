// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.IO;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    partial class StockFlowTransformer
    {
        private void ProcessAveragedStockGroupOutputFiles()
        {
            this.ProcessAveragedOutputFiles(
                Strings.DATASHEET_OUTPUT_AVG_SPATIAL_STOCK_GROUP,
                Strings.SPATIAL_MAP_AVG_STOCK_GROUP_VARIABLE_PREFIX + "*.tif");
        }

        private void ProcessAveragedFlowGroupOutputFiles()
        {
            this.ProcessAveragedOutputFiles(
                Strings.DATASHEET_OUTPUT_AVG_SPATIAL_FLOW_GROUP,
                Strings.SPATIAL_MAP_AVG_FLOW_GROUP_VARIABLE_PREFIX + "*.tif");
        }

        private void ProcessAveragedLateralFlowGroupOutputFiles()
        {
            this.ProcessAveragedOutputFiles(
                Strings.DATASHEET_OUTPUT_AVG_SPATIAL_LATERAL_FLOW_GROUP,
                Strings.SPATIAL_MAP_AVG_LATERAL_FLOW_GROUP_VARIABLE_PREFIX + "*.tif");
        }

        private void ProcessAveragedStockGroupDatasheet()
        {
            this.ProcessAveragedValueDatasheet(
                Strings.DATASHEET_OUTPUT_AVG_SPATIAL_STOCK_GROUP,
                Strings.STOCK_GROUP_ID_COLUMN_NAME);
        }

        private void ProcessAveragedFlowGroupDatasheet()
        {
            this.ProcessAveragedValueDatasheet(
                Strings.DATASHEET_OUTPUT_AVG_SPATIAL_FLOW_GROUP,
                Strings.FLOW_GROUP_ID_COLUMN_NAME);
        }

        private void ProcessAveragedLateralFlowGroupDatasheet()
        {
            this.ProcessAveragedValueDatasheet(
                Strings.DATASHEET_OUTPUT_AVG_SPATIAL_LATERAL_FLOW_GROUP,
                Strings.FLOW_GROUP_ID_COLUMN_NAME);
        }

        private void ProcessAveragedOutputFiles(string datasheetName, string fileSpec)
        {
            ParallelJobConfig config = this.m_STSimTransformer.STSimLoadConfigurationFile();
            Dictionary<int, int> dictJobIterations = CreateJobIterationsDictionary(config);
            Dictionary<string, List<string>> dictFilenames = CreateSameNameFilesDictionary(config, datasheetName, fileSpec);

            if (dictFilenames.Count == 0)
            {
                return;
            }

            int ttlIterations = 0;
            foreach (var jobId in dictJobIterations.Keys)
            {
                int numIterations = dictJobIterations[jobId];
                ttlIterations += numIterations;
            }

            foreach (string k in dictFilenames.Keys)
            {
                RasterMerger m = new RasterMerger();
                foreach (string f in dictFilenames[k])
                {
                    int jobId = this.m_STSimTransformer.STSimGetJobIdFromFolder(f);
                    int numIterations = dictJobIterations[jobId];

                    if (jobId != 0 || numIterations > 0)
                    {
                        m.Merge(f, numIterations);

                        File.SetAttributes(f, FileAttributes.Normal);
                        File.Delete(f);
                    }
                    else
                    {
                        Debug.Assert(false, "Either the Job ID Or Number of iterations are invalid");
                    }
                }

                m.Multiply(1 / (double)ttlIterations);

                string newFilename = dictFilenames[k][0];
                m.Save(newFilename, Spatial.GetGeoTiffCompressionType(this.Library));
            }
        }

        private Dictionary<int, int> CreateJobIterationsDictionary(ParallelJobConfig config)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();

            foreach (ParallelJob j in config.Jobs)
            {
                using (DataStore store = Session.CreateDataStore(new DataStoreConnection(Strings.SQLITE_DATASTORE_NAME, j.Library)))
                {
                    int MergeScenarioId = this.m_STSimTransformer.STSimGetMergeScenarioId(store);
                    string OutputFolderName = this.m_STSimTransformer.STSimGetJobOutputScenarioFolderName(j.Library, MergeScenarioId, false);

                    if (!Directory.Exists(OutputFolderName))
                    {
                        continue;
                    }

                    int numIterations = Convert.ToInt32(store.ExecuteScalar(
                        "SELECT maximumIteration - minimumIteration + 1 FROM stsim_RunControl where scenarioId=" + MergeScenarioId),
                        CultureInfo.InvariantCulture);

                    Debug.Assert(j.JobId > 0);

                    if (!dict.ContainsKey(j.JobId))
                    {
                        dict.Add(j.JobId, numIterations);
                    }
                    else
                    {
                        Debug.Assert(false, "Job #'s should be unique when parsed from folder names");
                    }
                }
            }

            return dict;
        }

        private Dictionary<string, List<string>> CreateSameNameFilesDictionary(
            ParallelJobConfig config, 
            string datasheetName, 
            string fileSpec)
        {
            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();

            foreach (ParallelJob j in config.Jobs)
            {
                using (DataStore store = Session.CreateDataStore(new DataStoreConnection(Strings.SQLITE_DATASTORE_NAME, j.Library)))
                {
                    int MergeScenarioId = this.m_STSimTransformer.STSimGetMergeScenarioId(store);
                    string OutputFolderName = this.GetOutputFolderName(j.Library, MergeScenarioId, datasheetName);

                    if (!Directory.Exists(OutputFolderName))
                    {
                        continue;
                    }

                    foreach (string f in Directory.GetFiles(OutputFolderName, fileSpec))
                    {
                        string key = Path.GetFileName(f).ToUpperInvariant();

                        if (!dict.ContainsKey(key))
                        {
                            dict.Add(key, new List<string>());
                        }

                        Debug.Assert(!(dict[key].Contains(f)));
                        dict[key].Add(f);
                    }
                }
            }

            return dict;
        }

        private void ProcessAveragedValueDatasheet(string datasheetName, string filterColumnName)
        {
            using (DataStore store = this.Library.CreateDataStore())
            {
                string query = string.Format(CultureInfo.InvariantCulture,
                    "SELECT * FROM {0} WHERE ScenarioId={1}", 
                    datasheetName,
                    this.ResultScenario.Id);

                DataTable dt = store.CreateDataTableFromQuery(query, "Merge");

                int ttlCnt = dt.Rows.Count;

                query = string.Format(CultureInfo.InvariantCulture,
                    "SELECT scenarioId,iteration,timestep,filename,band,{0} FROM {1} WHERE ScenarioId={2} group by iteration,timestep,band,{3}",
                    filterColumnName,
                    datasheetName,
                    this.ResultScenario.Id,
                    filterColumnName);

                dt = store.CreateDataTableFromQuery(query, "Merge");
                if (dt.Rows.Count < ttlCnt)
                {
                    // We've go dupes do lets blow away the old records and create new single copies

                    query = string.Format(CultureInfo.InvariantCulture,
                        "delete from {0} where ScenarioId={1}", 
                        datasheetName,
                        this.ResultScenario.Id);

                    store.ExecuteNonQuery(query);

                    foreach (DataRow row in dt.Rows)
                    {
                        var band = Convert.IsDBNull(row[4]) ? "null" : row[4];

                        query = string.Format(CultureInfo.InvariantCulture,
                            "insert into {0} (ScenarioId,iteration,timestep,filename,band,{1}) values ({2},{3},{4},'{5}',{6},{7})",
                            datasheetName,
                            filterColumnName,
                            row[0], row[1], row[2], row[3], band, row[5]);

                        store.ExecuteNonQuery(query);
                    }
                }
            }
        }

        private string GetOutputFolderName(string fileName, int scenarioId, string datasheetName)
        {
            return Path.Combine(
                this.m_STSimTransformer.STSimGetJobOutputScenarioFolderName(fileName, scenarioId, false),
                datasheetName);
        }
    }
}
