// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.IO;
using System.Data;
using SyncroSim.Core;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    public partial class STSimTransformer
    {
        /// <summary>
        /// Overrides merge so we can process averaged spatial data
        /// </summary>
        /// <remarks></remarks>
        public override void Merge()
        {
            if (this.IsSpatialMultiprocessing())
            {
                base.Merge();
            }
            else
            {
                BeginNormalSpatialMerge?.Invoke(this, new EventArgs());

                //Merge spatial averaging rasters

                ProcessAverageStateClassRasters();
                ProcessAverageAgeRasters();
                ProcessAverageStratumRasters();
                ProcessAverageTransitionProbabilityRasters();
                ProcessAverageTSTRasters();
                ProcessAverageStateAttributeRasters();
                ProcessAverageTransitionAttributeRasters();

                //Do the normal merge
                base.Merge();

                //Merge spatial averaging datasheets
                ProcessAverageStateClassDatasheet(); 
                ProcessAverageAgeDatasheet(); 
                ProcessAverageStratumDatasheet();
                ProcessAverageTransitionProbabilityDatasheet();
                ProcessAverageTSTDatasheet();
                ProcessAverageStateAttributeDatasheet();       
                ProcessAverageTransitionAttributeDatasheet();

                NormalSpatialMergeComplete?.Invoke(this, new EventArgs());
            }
        }

        //Rasters

        private void ProcessAverageStateClassRasters()
        {
            this.ProcessAveragedOutputFiles(
                Constants.DATASHEET_OUTPUT_AVG_SPATIAL_STATE_CLASS,
                Constants.SPATIAL_MAP_AVG_STATE_CLASS_FILEPREFIX + "*.tif");
        }

        private void ProcessAverageAgeRasters()
        {
            this.ProcessAveragedOutputFiles(
                Constants.DATASHEET_OUTPUT_AVG_SPATIAL_AGE,
                Constants.SPATIAL_MAP_AVG_AGE_FILEPREFIX + "*.tif");
        }

        private void ProcessAverageStratumRasters()
        {
            this.ProcessAveragedOutputFiles(
                Constants.DATASHEET_OUTPUT_AVG_SPATIAL_STRATUM,
                Constants.SPATIAL_MAP_AVG_STRATUM_FILEPREFIX + "*.tif");
        }

        private void ProcessAverageTransitionProbabilityRasters()
        {
            this.ProcessAveragedOutputFiles(
                Constants.DATASHEET_OUTPUT_AVG_SPATIAL_TRANSITION_PROBABILITY,
                Constants.SPATIAL_MAP_AVG_TRANSITION_PROBABILITY_FILEPREFIX + "*.tif");
        }

        private void ProcessAverageTSTRasters()
        {
            this.ProcessAveragedOutputFiles(
                Constants.DATASHEET_OUTPUT_AVG_SPATIAL_TST,
                Constants.SPATIAL_MAP_AVG_TST_FILEPREFIX + "*.tif");
        }

        private void ProcessAverageStateAttributeRasters()
        {
            this.ProcessAveragedOutputFiles(
                Constants.DATASHEET_OUTPUT_AVG_SPATIAL_STATE_ATTRIBUTE,
                Constants.SPATIAL_MAP_AVG_STATE_ATTRIBUTE_FILEPREFIX + "*.tif");
        }

        private void ProcessAverageTransitionAttributeRasters()
        {
            this.ProcessAveragedOutputFiles(
                Constants.DATASHEET_OUTPUT_AVG_SPATIAL_TRANSITION_ATTRIBUTE,
                Constants.SPATIAL_MAP_AVG_TRANSITION_ATTRIBUTE_FILEPREFIX + "*.tif");
        }

        //Datasheets

        private void ProcessAverageStateClassDatasheet()
        {
            this.ProcessAveragedValueDatasheet(
                Constants.DATASHEET_OUTPUT_AVG_SPATIAL_STATE_CLASS, 
                Strings.DATASHEET_STATECLASS_ID_COLUMN_NAME);
        }

        private void ProcessAverageAgeDatasheet()
        {
            this.ProcessAveragedValueDatasheet(
                Constants.DATASHEET_OUTPUT_AVG_SPATIAL_AGE);
        }

        private void ProcessAverageStratumDatasheet()
        {
            this.ProcessAveragedValueDatasheet(
                Constants.DATASHEET_OUTPUT_AVG_SPATIAL_STRATUM, 
                Strings.DATASHEET_STRATUM_ID_COLUMN_NAME);
        }

        private void ProcessAverageTransitionProbabilityDatasheet()
        {
            this.ProcessAveragedValueDatasheet(
                Constants.DATASHEET_OUTPUT_AVG_SPATIAL_TRANSITION_PROBABILITY,
                Strings.DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME);
        }

        private void ProcessAverageTSTDatasheet()
        {
            this.ProcessAveragedValueDatasheet(
                Constants.DATASHEET_OUTPUT_AVG_SPATIAL_TST,
                Strings.DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME);
        }

        private void ProcessAverageStateAttributeDatasheet()
        {
            this.ProcessAveragedValueDatasheet(
                Constants.DATASHEET_OUTPUT_AVG_SPATIAL_STATE_ATTRIBUTE,
                Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_ID_COLUMN_NAME);
        }

        private void ProcessAverageTransitionAttributeDatasheet()
        {
            this.ProcessAveragedValueDatasheet(
                Constants.DATASHEET_OUTPUT_AVG_SPATIAL_TRANSITION_ATTRIBUTE,
                Strings.DATASHEET_TRANSITION_ATTRIBUTE_TYPE_ID_COLUMN_NAME);
        }

        private void ProcessAveragedOutputFiles(string datasheetName, string fileSpec)
        {
            ParallelJobConfig config = this.STSimLoadConfigurationFile();
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
                    int jobId = this.STSimGetJobIdFromFolder(f);
                    int numIterations = dictJobIterations[jobId];

                    if (jobId != 0 || numIterations > 0)
                    {
                        m.Merge(f, numIterations);

                        File.SetAttributes(f, FileAttributes.Normal);
                        File.Delete(f);
                    }
                    else
                    {
                        Debug.Assert(false, "Either the Job Id Or Number of iterations are invalid");
                    }
                }

                m.Multiply(1 / (double)ttlIterations);

                string newFilename = dictFilenames[k][0];
                m.Save(newFilename, Core.Spatial.GetGeoTiffCompressionType(this.Library));
            }
        }

        private Dictionary<int, int> CreateJobIterationsDictionary(ParallelJobConfig config)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();

            foreach (ParallelJob j in config.Jobs)
            {
                using (DataStore store = Session.CreateDataStore(new DataStoreConnection(Strings.SQLITE_DATASTORE_NAME, j.Library)))
                {
                    int MergeScenarioId = this.STSimGetMergeScenarioId(store);
                    string OutputFolderName = this.STSimGetJobOutputScenarioFolderName(j.Library, MergeScenarioId, false);

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
                    int MergeScenarioId = this.STSimGetMergeScenarioId(store);
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

        private void ProcessAveragedValueDatasheet(string datasheetName)
        {
            using (DataStore store = this.Library.CreateDataStore())
            {
                string query = string.Format(CultureInfo.InvariantCulture,
                    "SELECT * FROM {0} WHERE ScenarioId={1}",
                    datasheetName,
                    this.ResultScenario.Id);

                DataTable dt = store.CreateDataTableFromQuery(query, "Merge");
                int OriginalCount = dt.Rows.Count;

                query = string.Format(CultureInfo.InvariantCulture,
                    "SELECT scenarioId,iteration,timestep,filename,band FROM {0} WHERE ScenarioId={1} group by iteration,timestep,band",
                    datasheetName,
                    this.ResultScenario.Id);

                dt = store.CreateDataTableFromQuery(query, "Merge");

                if (dt.Rows.Count < OriginalCount)
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
                            "insert into {0} (ScenarioId,iteration,timestep,filename,band) values ({1},{2},{3},'{4}',{5})",
                            datasheetName,
                            row[0], row[1], row[2], row[3], band);

                        store.ExecuteNonQuery(query);
                    }
                }
            }
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
                int OriginalCount = dt.Rows.Count;

                query = string.Format(CultureInfo.InvariantCulture,
                    "SELECT scenarioId,iteration,timestep,filename,band,{0} FROM {1} WHERE ScenarioId={2} group by iteration,timestep,band,{3}",
                    filterColumnName,
                    datasheetName,
                    this.ResultScenario.Id,
                    filterColumnName);

                dt = store.CreateDataTableFromQuery(query, "Merge");

                if (dt.Rows.Count < OriginalCount)
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
                this.STSimGetJobOutputScenarioFolderName(fileName, scenarioId, false),
                datasheetName);
        }
    }
}
