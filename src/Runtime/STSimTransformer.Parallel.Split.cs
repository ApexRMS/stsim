// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.IO;
using System.Linq;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    public partial class STSimTransformer
    {
        public override void Split(int maximumJobs)
        {
            if (this.IsSplitByStrata())
            {
                this.NormalizeRunControl();
                this.ConfigureIsSpatialRunFlag();
                this.ConfigureTimestepsAndIterations();
                this.ValidateNormalSplit();

                DataTable SplitData = CreateSplitTableSchema();
                List<int> SecondaryStratumIds = this.GetApplicableSecondaryStrata();

                AddSecondaryStratumRows(SplitData, maximumJobs, SecondaryStratumIds);

                this.AddIterationRows(SplitData, maximumJobs);
                this.CalculateNewInitialConditions(SplitData);
                this.CreatePartialLibraries(SplitData, false);
            }
            else
            {
                if (this.IsSpatialMultiprocessing())
                {
                    this.WarnAboutTargets();
                }

                base.Split(maximumJobs);
            }
        }

        private void WarnAboutTargets()
        {
            bool HasTransitionTargets = this.ResultScenario.GetDataSheet(Strings.DATASHEET_TRANSITION_TARGET_NAME).HasData();
            bool HasTransitionAttributeTargets = this.ResultScenario.GetDataSheet(Strings.DATASHEET_TRANSITION_ATTRIBUTE_TARGET_NAME).HasData();

            if (HasTransitionTargets || HasTransitionAttributeTargets)
            {
                this.RecordStatus(StatusType.Information,
                    "Targets for transitions or transition attributes are being used in conjunction with spatial multiprocessing." + 
                    Environment.NewLine + 
                    "If the mask boundaries do not match the boundaries for the targets (i.e., strata boundaries) the results will not reflect the input targets accurately.");

                this.Library.Save();
            }
        }

        internal void SpatialSplit(int maximumJobs, List<int> secondaryStratumIds)
        {
            Debug.Assert(secondaryStratumIds.Count >= 2);

            this.NormalizeRunControl();
            this.ConfigureTimestepsAndIterations();
            this.ValidateSpatialSplit();

            DataTable SplitData = CreateSplitTableSchema();

            AddSecondaryStratumRows(SplitData, maximumJobs, secondaryStratumIds);

            this.AddIterationRows(SplitData, maximumJobs);
            this.CreatePartialLibraries(SplitData, true);
        }

        private static DataTable CreateSplitTableSchema()
        {
            DataTable dt = new DataTable("Split Configuration");
            dt.Locale = CultureInfo.InvariantCulture;

            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("FileName", typeof(string));
            dt.Columns.Add("SecondaryStratumIds", typeof(string));
            dt.Columns.Add("NumIterations", typeof(int));
            dt.Columns.Add("MinIteration", typeof(int));
            dt.Columns.Add("MaxIteration", typeof(int));
            dt.Columns.Add("TotalAmount", typeof(double));
            dt.Columns.Add("NumCells", typeof(int));
            dt.Columns.Add("RelativeAmountTotal", typeof(double));
            dt.Columns.Add("RATIO", typeof(double));
            dt.Columns.Add("JobTotalAmount", typeof(double));
            dt.Columns.Add("JobNumCells", typeof(int));
            dt.Columns.Add("JobRelativeAmountTotal", typeof(double));

            return dt;
        }

        public override int GetMaximumJobs()
        {
            int MaxJobs = base.GetMaximumJobs();

            if (this.IsSplitByStrata())
            {
                return (MaxJobs * this.GetApplicableSecondaryStrata().Count);
            }
            else
            {
                return MaxJobs;
            }
        }

        private static void AddSecondaryStratumRows(DataTable splitData, int maximumJobs, List<int> secondaryStratumIds)
        {
            Debug.Assert(splitData.Rows.Count == 0);

            int TotalRows = Math.Min(maximumJobs, secondaryStratumIds.Count);

            for (int i = 0; i < TotalRows; i++)
            {
                splitData.Rows.Add(new[] {i});
            }

            int RowIndex = 0;

            for (int i = 0; i < secondaryStratumIds.Count; i++)
            {
                DataRow dr = splitData.Rows[RowIndex];
                int ssid = secondaryStratumIds[i];
                string NewIds = null;

                if (dr["SecondaryStratumIds"] == DBNull.Value)
                {
                    NewIds = ssid.ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    NewIds = string.Format(CultureInfo.InvariantCulture, 
                        "{0},{1}", 
                        Convert.ToInt32(dr["SecondaryStratumIds"], CultureInfo.InvariantCulture), 
                        ssid);
                }

                dr["SecondaryStratumIds"] = NewIds;
                RowIndex += 1;

                if (RowIndex > splitData.Rows.Count - 1)
                {
                    RowIndex = 0;
                }
            }

            Debug.Assert(TotalRows <= maximumJobs);
            Debug.Assert(splitData.Rows.Count == TotalRows);
        }

        private void AddIterationRows(DataTable splitData, int maximumJobs)
        {
            Debug.Assert(splitData.Rows.Count <= maximumJobs);

            DataTable dt = splitData.Clone();
            int TotalIterations = (this.MaximumIteration - this.MinimumIteration + 1);
            int NewMaximumJobs = Math.Min(maximumJobs, (splitData.Rows.Count * TotalIterations));

            foreach (DataRow dr in splitData.Rows)
            {
                CloneRow(dr, dt);
            }

            int RowIndex = 0;

            while (dt.Rows.Count < NewMaximumJobs)
            {
                DataRow dr = splitData.Rows[RowIndex];

                CloneRow(dr, dt);
                RowIndex += 1;

                if (RowIndex > splitData.Rows.Count - 1)
                {
                    RowIndex = 0;
                }
            }

            splitData.Rows.Clear();

            foreach (DataRow dr in dt.Rows)
            {
                CloneRow(dr, splitData);
            }

            AssignIterationCounts(splitData, TotalIterations);
            this.CreateIterationRanges(splitData);

            Debug.Assert(splitData.Rows.Count == NewMaximumJobs);
        }

        private static void AssignIterationCounts(DataTable splitData, int totalIterations)
        {
            List<int> RowIds = CreateUniqueIdList(splitData, "ID");

            foreach (int id in RowIds)
            {
                string q = string.Format(CultureInfo.InvariantCulture, "ID={0}", id);
                DataRow[] rows = splitData.Select(q);

                Debug.Assert(rows.Count() > 0);

                if (rows.Count() == 1)
                {
                    rows[0]["NumIterations"] = totalIterations;
                }
                else
                {
                    int RowIndex = 0;
                    int IterationsRemaining = totalIterations;

                    foreach (DataRow dr in rows)
                    {
                        dr["NumIterations"] = 0;
                    }

                    while (IterationsRemaining > 0)
                    {
                        DataRow dr = rows[RowIndex];
                        dr["NumIterations"] = Convert.ToInt32(dr["NumIterations"], CultureInfo.InvariantCulture) + 1;

                        RowIndex += 1;

                        if (RowIndex > rows.Count() - 1)
                        {
                            RowIndex = 0;
                        }

                        IterationsRemaining -= 1;
                    }
                }
            }
        }

        private void CreateIterationRanges(DataTable splitData)
        {
            List<int> RowIds = CreateUniqueIdList(splitData, "ID");

            foreach (int id in RowIds)
            {
                string q = string.Format(CultureInfo.InvariantCulture, "ID={0}", id);
                DataRow[] rows = splitData.Select(q);

                Debug.Assert(rows.Count() > 0);

                int MinIter = this.MinimumIteration;

                for (int RowIndex = 0; RowIndex < rows.Count(); RowIndex++)
                {
                    DataRow dr = rows[RowIndex];
                    int NumIters = Convert.ToInt32(dr["NumIterations"], CultureInfo.InvariantCulture);
                    int MaxIter = MinIter + NumIters - 1;

                    dr["MinIteration"] = MinIter;
                    dr["MaxIteration"] = MaxIter;

                    MinIter = MaxIter + 1;
                }
            }
        }

        private void CalculateNewInitialConditions(DataTable splitData)
        {
            DataRow icdata = this.ResultScenario.GetDataSheet(Strings.DATASHEET_NSIC_NAME).GetDataRow();
            DataTable distdata = this.ResultScenario.GetDataSheet(Strings.DATASHEET_NSIC_DISTRIBUTION_NAME).GetData();
            double TotalAmount = Convert.ToDouble(icdata[Strings.DATASHEET_NSIC_TOTAL_AMOUNT_COLUMN_NAME], CultureInfo.InvariantCulture);
            int NumCells = Convert.ToInt32(icdata[Strings.DATASHEET_NSIC_NUM_CELLS_COLUMN_NAME], CultureInfo.InvariantCulture);
            var RelativeAmountTotal = Convert.ToDouble(distdata.Compute("SUM(RelativeAmount)", null), CultureInfo.InvariantCulture);

            foreach (DataRow dr in splitData.Rows)
            {
                string ids = Convert.ToString(dr["SecondaryStratumIds"], CultureInfo.InvariantCulture);
                string q = string.Format(CultureInfo.InvariantCulture, "SecondaryStratumId IN({0})", ids);
                double JobRelativeAmountTotal = Convert.ToDouble(distdata.Compute("SUM(RelativeAmount)", q), CultureInfo.InvariantCulture);
                double ratio = JobRelativeAmountTotal / RelativeAmountTotal;

                dr["TotalAmount"] = TotalAmount;
                dr["NumCells"] = NumCells;
                dr["RelativeAmountTotal"] = RelativeAmountTotal;

                dr["RATIO"] = ratio;

                dr["JobTotalAmount"] = TotalAmount * ratio;
                dr["JobNumCells"] = Convert.ToInt32(NumCells * ratio, CultureInfo.InvariantCulture);
                dr["JobRelativeAmountTotal"] = JobRelativeAmountTotal;
            }
        }

        private void CreatePartialLibraries(DataTable splitData, bool isSpatialSplit)
        {
            string psl = null;
            string ssl = null;
            string tsl = null;

            TerminologyUtilities.GetStratumLabelTerminology(
                this.Project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME), ref psl, ref ssl, ref tsl);

            //Save the library because we may have added status data to the scenario

            if (this.Library.HasChanges())
            {
                this.Library.Save();
            }

            this.BeginProgress(splitData.Rows.Count + 1);

            this.SetStatusMessage(string.Format(CultureInfo.InvariantCulture, 
                "Preparing Data For Parallel Processing (Split by '{0}')", 
                ssl));

            //We have numJobs + 1 because we want the progress indicator to include
            //the time it takes to create the partial library.

            this.CreatePartialLibrary();
            this.StepProgress();

            //Then make a copy of the partial library for each job.  Note, however, that for the final
            //job we move the partial library instead of copying it.

            List<string> Files = new List<string>();
            DataView dv = new DataView(splitData, null, "ID", DataViewRowState.CurrentRows);
            int JobId = 1;

            foreach (DataRowView drv in dv)
            {
                string JobFileName = string.Format(CultureInfo.InvariantCulture, "Job-{0}.ssim", JobId);
                string FullFileName = Path.Combine(this.JobFolderName, JobFileName);

                drv.Row["FileName"] = FullFileName;

                if (JobId == splitData.Rows.Count)
                {
                    File.Move(this.PartialLibraryName, FullFileName);
                }
                else
                {
                    File.Copy(this.PartialLibraryName, FullFileName);
                }

                this.ConfigureDatabase(drv.Row, isSpatialSplit);

                Debug.Assert(!Files.Contains(FullFileName));
                Files.Add(FullFileName);

                this.StepProgress();

                JobId += 1;
            }

            this.CompleteProgress();

            //If it is a spatial split, configure the external files

            if (isSpatialSplit)
            {
                this.ConfigureExternalFiles(splitData);
            }

            //Create the configuration file

            this.CreateSplitConfigurationFile(Files, splitData.Rows.Count);

            //The partial file and input folder should now be gone

            Debug.Assert(!File.Exists(this.PartialLibraryName));
            Debug.Assert(!Directory.Exists(Path.Combine(this.JobFolderName, "Partial.ssim.input")));

            this.SetStatusMessage(null);
        }

        private void ConfigureExternalFiles(DataTable splitData)
        {
            string PartialInputFolderName = Path.Combine(this.JobFolderName, "Partial.ssim.input");

            if (!Directory.Exists(PartialInputFolderName))
            {
                return;
            }

            for (int i = 0; i < splitData.Rows.Count; i++)
            {
                DataRow dr = splitData.Rows[i];
                string f = Convert.ToString(dr["FileName"], CultureInfo.InvariantCulture);
                string BaseFolderName = Path.GetFileName(f) + ".input";
                string JobInputFolderName = Path.Combine(this.JobFolderName, BaseFolderName);

                if (i == splitData.Rows.Count - 1)
                {
                    Directory.Move(PartialInputFolderName, JobInputFolderName);
                }
                else
                {
                    Shared.FileSystemUtilities.CopyDirectory(PartialInputFolderName, JobInputFolderName);
                }
            }
        }

        private void ConfigureDatabase(DataRow splitDataRow, bool isSpatailSplit)
        {
            using (SyncroSimTransactionScope scope = Session.CreateTransactionScope())
            {
                string FileName = Convert.ToString(splitDataRow["FileName"], CultureInfo.InvariantCulture);

                using (DataStore store = Session.CreateDataStore(new DataStoreConnection(Strings.SQLITE_DATASTORE_NAME, FileName)))
                {
                    UpdateRunControl(splitDataRow, store);
                    RemoveSecondaryStrata(splitDataRow, store, this.ResultScenario);

                    if (!isSpatailSplit)
                    {
                        UpdateInitialConditions(splitDataRow, store);
                        UpdateTransitionTargets(splitDataRow, store);
                        UpdateTransitionAttributeTargets(splitDataRow, store);
                    }
                }

                scope.Complete();
            }
        }

        private static void UpdateRunControl(DataRow splitDataRow, DataStore store)
        {
            int MinIter = Convert.ToInt32(splitDataRow["MinIteration"], CultureInfo.InvariantCulture);
            int MaxIter = Convert.ToInt32(splitDataRow["MaxIteration"], CultureInfo.InvariantCulture);

            Debug.Assert(MinIter <= MaxIter);

            string q = string.Format(CultureInfo.InvariantCulture, 
                "UPDATE {0} SET {1}={2}, {3}={4}",
                Strings.DATASHEET_RUN_CONTROL_NAME, 
                Strings.RUN_CONTROL_MIN_ITERATION_COLUMN_NAME, MinIter, 
                Strings.RUN_CONTROL_MAX_ITERATION_COLUMN_NAME, MaxIter);

            store.ExecuteNonQuery(q);
        }

        private static void RemoveSecondaryStrata(DataRow splitDataRow, DataStore store, Scenario scenario)
        {
            Debug.Assert(splitDataRow["SecondaryStratumIds"] != DBNull.Value);
            string ids = Convert.ToString(splitDataRow["SecondaryStratumIds"], CultureInfo.InvariantCulture);

            store.ExecuteNonQuery(string.Format(CultureInfo.InvariantCulture,
                "DELETE FROM stsim_SecondaryStratum WHERE SecondaryStratumId NOT IN({0})", ids));

            foreach (DataFeed df in scenario.DataFeeds)
            {
                foreach (DataSheet ds in df.DataSheets)
                {
                    foreach (DataSheetColumn dc in ds.Columns)
                    {
                        if (dc.ValidationTable != null && 
                            dc.ValidationType == ColumnValidationType.IsDataSheet && 
                            dc.Formula1 == "stsim_SecondaryStratum")
                        {
                            store.ExecuteNonQuery(string.Format(CultureInfo.InvariantCulture, 
                                "DELETE FROM [{0}] WHERE {1} NOT IN ({2})", ds.Name, dc.Name, ids));
                        }
                    }
                }
            }
        }

        private static void UpdateInitialConditions(DataRow splitDataRow, DataStore store)
        {
            Debug.Assert(splitDataRow["SecondaryStratumIds"] != DBNull.Value);
            store.ExecuteNonQuery(string.Format(CultureInfo.InvariantCulture, "UPDATE stsim_InitialConditionsNonSpatial SET TotalAmount={0}", Convert.ToDouble(splitDataRow["JobTotalAmount"], CultureInfo.InvariantCulture)));
            store.ExecuteNonQuery(string.Format(CultureInfo.InvariantCulture, "UPDATE stsim_InitialConditionsNonSpatial SET NumCells={0}", Convert.ToDouble(splitDataRow["JobNumCells"], CultureInfo.InvariantCulture)));       
            store.ExecuteNonQuery(string.Format(CultureInfo.InvariantCulture, "UPDATE stsim_InitialConditionsNonSpatialDistribution SET RelativeAmount=(RelativeAmount * {0})", Convert.ToDouble(splitDataRow["RATIO"], CultureInfo.InvariantCulture)));
        }

        private static void UpdateTransitionTargets(DataRow splitDataRow, DataStore store)
        {
            Debug.Assert(splitDataRow["SecondaryStratumIds"] != DBNull.Value);
            double ratio = Convert.ToDouble(splitDataRow["RATIO"], CultureInfo.InvariantCulture);

            store.ExecuteNonQuery(string.Format(CultureInfo.InvariantCulture, "UPDATE stsim_TransitionTarget SET Amount=(Amount * {0}) WHERE SecondaryStratumId IS NULL", ratio));        
            store.ExecuteNonQuery(string.Format(CultureInfo.InvariantCulture, "UPDATE stsim_TransitionTarget SET DistributionSD=(DistributionSD * {0}) WHERE SecondaryStratumId IS NULL", ratio));
            store.ExecuteNonQuery(string.Format(CultureInfo.InvariantCulture, "UPDATE stsim_TransitionTarget SET DistributionMin=(DistributionMin * {0}) WHERE SecondaryStratumId IS NULL", ratio));
            store.ExecuteNonQuery(string.Format(CultureInfo.InvariantCulture, "UPDATE stsim_TransitionTarget SET DistributionMax=(DistributionMax * {0}) WHERE SecondaryStratumId IS NULL", ratio));
        }

        private static void UpdateTransitionAttributeTargets(DataRow splitDataRow, DataStore store)
        {
            Debug.Assert(splitDataRow["SecondaryStratumIds"] != DBNull.Value);
            double ratio = Convert.ToDouble(splitDataRow["RATIO"], CultureInfo.InvariantCulture);

            store.ExecuteNonQuery(string.Format(CultureInfo.InvariantCulture, "UPDATE stsim_TransitionAttributeTarget SET Amount=(Amount * {0}) WHERE SecondaryStratumId IS NULL", ratio));
            store.ExecuteNonQuery(string.Format(CultureInfo.InvariantCulture, "UPDATE stsim_TransitionAttributeTarget SET DistributionSD=(DistributionSD * {0}) WHERE SecondaryStratumId IS NULL", ratio));
            store.ExecuteNonQuery(string.Format(CultureInfo.InvariantCulture, "UPDATE stsim_TransitionAttributeTarget SET DistributionMin=(DistributionMin * {0}) WHERE SecondaryStratumId IS NULL", ratio));
            store.ExecuteNonQuery(string.Format(CultureInfo.InvariantCulture, "UPDATE stsim_TransitionAttributeTarget SET DistributionMax=(DistributionMax * {0}) WHERE SecondaryStratumId IS NULL", ratio));
        }

        private List<int> GetApplicableSecondaryStrata()
        {
            List<int> l = new List<int>();
            string psl = null;
            string ssl = null;
            string tsl = null;

            TerminologyUtilities.GetStratumLabelTerminology(this.Project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME), ref psl, ref ssl, ref tsl);
            DataSheet ds = this.ResultScenario.GetDataSheet(Strings.DATASHEET_NSIC_DISTRIBUTION_NAME);

            foreach (DataRow dr in ds.GetData().Rows)
            {
                if (dr[Strings.DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME] == DBNull.Value)
                {
                    ExceptionUtils.ThrowInvalidOperationException(
                        "Cannot split by '{0}' because '{1}' is not specified for all records in Initial Conditions Distribution.", ssl, ssl);
                }

                int id = Convert.ToInt32(dr[Strings.DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME], CultureInfo.InvariantCulture);

                if (!l.Contains(id))
                {
                    l.Add(id);
                }
            }

            Debug.Assert(l.Count > 0);
            return l;
        }

        private static void CloneRow(DataRow sourceRow, DataTable targetTable)
        {
            DataRow r = targetTable.NewRow();

            foreach (DataColumn dc in targetTable.Columns)
            {
                r[dc.ColumnName] = sourceRow[dc.ColumnName];
            }

            targetTable.Rows.Add(r);
        }

        private static List<int> CreateUniqueIdList(DataTable dt, string columnName)
        {
            List<int> Ids = new List<int>();

            foreach (DataRow dr in dt.Rows)
            {
                int id = Convert.ToInt32(dr[columnName], CultureInfo.InvariantCulture);

                if (!Ids.Contains(id))
                {
                    Ids.Add(id);
                }
            }

            Ids.Sort();
            return Ids;
        }

        private static bool NullValueExists(DataTable dt, string columnName)
        {
            foreach (DataRow dr in dt.Rows)
            {
                if (dr[columnName] == DBNull.Value)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsSplitByStrata()
        {
            DataRow dr = this.ResultScenario.GetDataSheet(Strings.DATASHEET_MULTI_PROCESSING_NAME).GetDataRow();

            if (dr == null)
            {
                return false;
            }

            return DataTableUtilities.GetDataBool(dr[Strings.DATASHEET_MULTI_PROCESSING_SPLIT_BY_SS_COLUMN_NAME]);
        }

        private void ValidateNormalSplit()
        {
            string psl = null;
            string ssl = null;
            string tsl = null;
            string aml = null;
            TerminologyUnit amu = 0;
            DataSheet tds = this.Project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME);
            TerminologyUtilities.GetStratumLabelTerminology(tds, ref psl, ref ssl, ref tsl);
            TerminologyUtilities.GetAmountLabelTerminology(tds, ref aml, ref amu);

            //We don't support splits by secondary strata for spatial runs as this time

            if (this.IsSpatial)
            {
                ExceptionUtils.ThrowInvalidOperationException("Cannot split by '{0}' for a spatial model run.", ssl);
            }

            //If there are less than 2 secondary strata records referenced by 
            //Initial Conditions Distribution we cannot do a split by secondary strata

            List<int> l = this.GetApplicableSecondaryStrata();

            if (l.Count < 2)
            {
                ExceptionUtils.ThrowInvalidOperationException("Cannot split by '{0}' because there are fewer than two references to '{1}' in Initial Conditions Distribution.", ssl, ssl);
            }

            //If there are Transition Targets with NULL secondary stata then add a warning

            if (NullValueExists(this.ResultScenario.GetDataSheet(Strings.DATASHEET_TRANSITION_TARGET_NAME).GetData(), Strings.DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME))
            {
                this.RecordStatus(StatusType.Warning, string.Format(CultureInfo.InvariantCulture, "Run is splitting by '{0}' but Transition Targets are not specified by '{1}'.  Allocating targets in proportion to '{2}'.", ssl, ssl, aml));
            }

            //If there are Transition Attribute Targets with NULL secondary stata then add a warning

            if (NullValueExists(this.ResultScenario.GetDataSheet(Strings.DATASHEET_TRANSITION_ATTRIBUTE_TARGET_NAME).GetData(), Strings.DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME))
            {
                this.RecordStatus(StatusType.Warning, string.Format(CultureInfo.InvariantCulture, "Run is splitting by '{0}' but Transition Attribute Targets are not specified by '{1}'.  Allocating targets in proportion to '{2}'.", ssl, ssl, aml));
            }
        }

        private void ValidateSpatialSplit()
        {
            //If there are Transition Targets with NULL secondary stata then log a warning

            if (NullValueExists(this.ResultScenario.GetDataSheet(Strings.DATASHEET_TRANSITION_TARGET_NAME).GetData(), Strings.DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME))
            {
                this.RecordStatus(StatusType.Warning, "Run is splitting by Secondary Stratum but Transition Targets are not specified by Secondary Stratum.");
            }

            //If there are Transition Attribute Targets with NULL secondary stata then log a warning

            if (NullValueExists(this.ResultScenario.GetDataSheet(Strings.DATASHEET_TRANSITION_ATTRIBUTE_TARGET_NAME).GetData(), Strings.DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME))
            {
                this.RecordStatus(StatusType.Warning, "Run is splitting by Secondary Stratum but Transition Attribute Targets are not specified by Secondary Stratum.");
            }
        }
    }
}
