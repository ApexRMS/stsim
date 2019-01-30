// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.IO;
using System.Data;
using SyncroSim.Core;
using System.Globalization;
using System.Collections.Generic;
using System.Diagnostics;

namespace SyncroSim.STSim
{
    public partial class STSimTransformer
    {
        /// <summary>
        /// Overrides merge so we can process AATP files
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
                //Merges the external Spatial TGAP files
                ProcessAverageTransitionProbabilityFiles();

                //Do the normal merge
                base.Merge();

                //Merges the datasheet records for Spatial TGAP files
                ProcessAverageTransitionProbabilityDatasheet();
            }
        }

        /// <summary>
        /// Processes the Average Transition Probability Files. These raster files are a special case because there only one 
        /// file per Transition Type per run, so when mulitprocessing we need to arithmetically combine these files together.
        /// </summary>
        /// <remarks></remarks>
        private void ProcessAverageTransitionProbabilityFiles()
        {
            ParallelJobConfig config = LoadConfigurationFile();

            // Find the number of iterations per job
            Dictionary<int, int> dictJobIterations = CreateJobIterationsDictionary(config);

            // Create Same Names Files Dictionary for tgap only for current strata
            Dictionary<string, List<string>> dictFilenames = CreateSameNameTgapFilesDictionary(config);

            if (dictFilenames.Count == 0)
            {
                return;
            }

            // Calculate the total number of iterations across all jobs. DEVNOTE: Do it here instead of below based on files beaause we wont always
            // have a file if not transitions.
            int ttlIterations = 0;
            foreach (var jobId in dictJobIterations.Keys)
            {
                int numIterations = dictJobIterations[jobId];
                ttlIterations += numIterations;
            }

            foreach (string k in dictFilenames.Keys)
            {
                TgapMerge m = new TgapMerge();
                foreach (string f in dictFilenames[k])
                {
                    int jobId = GetJobIdFromFolder(f);
                    int numIterations = dictJobIterations[jobId];

                    if (jobId != 0 || numIterations > 0)
                    {
                        m.Merge(f, numIterations);

                        // Delete the file after we've merged it.
                        File.SetAttributes(f, FileAttributes.Normal);
                        File.Delete(f);
                    }
                    else
                    {
                        Debug.Assert(false, "Either the Job ID Or Number of iterations are invalid");
                    }
                }

                // Divide the merged raster by the total number of iterations
                m.Multiply(1 / (double)ttlIterations);

                // Save the final merged tgap raster, giving it the same name/path as the 1st file in the dictionary for this Strata
                string newFilename = dictFilenames[k][0];
                m.Save(newFilename, StochasticTime.Spatial.GetGeoTiffCompressionType(this.Library));
            }
        }

        /// <summary>
        /// Create a dictionary of Job Iterations 
        /// </summary>
        /// <param name="config"></param>
        /// <returns>A dictionary of number of iterations, keyed by job ID</returns>
        /// <remarks></remarks>
        private static Dictionary<int, int> CreateJobIterationsDictionary(ParallelJobConfig config)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();

            foreach (ParallelJob j in config.Jobs)
            {
                using (DataStore store = Session.CreateDataStore(new DataStoreConnection(Strings.SQLITE_DATASTORE_NAME, j.Library)))
                {
                    int MergeScenarioId = ParallelTransformer.GetMergeScenarioId(store);
                    string OutputFolderName = GetJobOutputScenarioFolderName(j.Library, MergeScenarioId, false);

                    if (!Directory.Exists(OutputFolderName))
                    {
                        continue;
                    }

                    int numIterations = Convert.ToInt32(store.ExecuteScalar(
                        "SELECT maximumIteration - minimumIteration + 1 FROM STSim_RunControl where scenarioId=" + MergeScenarioId), 
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

        /// <summary>
        /// Creates a dictionary of lists of same file names for TGAP ( Annual Avg Transition Probability) 
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        /// <remarks>
        /// Each file of the same name needs to be arithmetically merged in the event of a iteration split.  
        /// This function returns a dictionary of 'same name' files that need to be merged.
        /// </remarks>
        private static Dictionary<string, List<string>> CreateSameNameTgapFilesDictionary(ParallelJobConfig config)
        {
            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();

            foreach (ParallelJob j in config.Jobs)
            {
                using (DataStore store = Session.CreateDataStore(new DataStoreConnection(Strings.SQLITE_DATASTORE_NAME, j.Library)))
                {
                    int MergeScenarioId = ParallelTransformer.GetMergeScenarioId(store);
                    string OutputFolderName = GetAATPSpatialOutputFolder(j.Library, MergeScenarioId);

                    if (!Directory.Exists(OutputFolderName))
                    {
                        continue;
                    }

                    foreach (string f in Directory.GetFiles(OutputFolderName, "tgap_*.tif"))
                    {
                        string key = Path.GetFileName(f).ToLower(CultureInfo.InvariantCulture);

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

        /// <summary>
        /// Gets the Average Annuanl Transition Probability Spatial output folder for the specified file and scenario Id
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="scenarioId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private static string GetAATPSpatialOutputFolder(string fileName, int scenarioId)
        {
            return Path.Combine(
                GetJobOutputScenarioFolderName(fileName, scenarioId, false), 
                Constants.DATASHEET_OUTPUT_SPATIAL_AVERAGE_TRANSITION_PROBABILITY);
        }

        /// <summary>
        /// Processes the Average Transition Probability datasheet records. This datasheet is a special case because there is only one file per Transition Type per run, so when mulitprocessing
        /// we remove the duplicate records created per job.
        /// </summary>
        /// <remarks></remarks>
        private void ProcessAverageTransitionProbabilityDatasheet()
        {
            using (DataStore store = this.Library.CreateDataStore())
            {
                string query = string.Format(CultureInfo.InvariantCulture, 
                    "SELECT *  FROM STSim_OutputSpatialAverageTransitionProbability WHERE ScenarioId={0}", this.ResultScenario.Id);

                DataTable dt = store.CreateDataTableFromQuery(query, "Merge");

                int ttlCnt = dt.Rows.Count;

                query = string.Format(CultureInfo.InvariantCulture, 
                    "SELECT scenarioId,iteration,timestep, filename, band,transitionGroupId FROM STSim_OutputSpatialAverageTransitionProbability WHERE ScenarioId={0} group by iteration, timestep,band,transitionGroupId", 
                    this.ResultScenario.Id);

                dt = store.CreateDataTableFromQuery(query, "Merge");
                if (dt.Rows.Count < ttlCnt)
                {
                    // We've go dupes do lets blow away the old records and create new single copies
                    query = string.Format(CultureInfo.InvariantCulture, 
                        "delete from STSim_OutputSpatialAverageTransitionProbability where ScenarioId={0}", this.ResultScenario.Id);
                    store.ExecuteNonQuery(query);

                    foreach (DataRow row in dt.Rows)
                    {
                        var band = Convert.IsDBNull(row[4]) ? "null" : row[4];

                        query = string.Format(CultureInfo.InvariantCulture, 
                            "insert into STSim_OutputSpatialAverageTransitionProbability (ScenarioId,iteration,timestep,filename,band,transitionGroupId) values ({0},{1},{2},'{3}',{4},{5})",
                            row[0], row[1], row[2], row[3], band, row[5]);

                        store.ExecuteNonQuery(query);
                    }
                }
            }
        }
    }
}
