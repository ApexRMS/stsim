// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.IO;
using System.Linq;
using System.Data;
using System.Text;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    internal static class ChartingUtilities
    {
        public static DataTable CreateProportionChartData(
            Scenario scenario, 
            ChartDescriptor descriptor, 
            string tableName, 
            DataStore store)
        {
            Dictionary<string, double> dict = CreateAmountDictionary(scenario, descriptor, store);

            if (dict.Count == 0)
            {
                return null;
            }

            string tag = GetChartCacheTag(descriptor);
            string query = CreateRawDataQuery(scenario, descriptor, tableName);
            DataTable dt = ChartCache.GetCachedData(scenario, query, tag);

            if (dt == null)
            {
                dt = store.CreateDataTableFromQuery(query, "RawData");
                ChartCache.SetCachedData(scenario, query, dt, tag);
            }

            foreach (DataRow dr in dt.Rows)
            {
                if (dr[Strings.DATASHEET_SUMOFAMOUNT_COLUMN_NAME] != DBNull.Value)
                {
                    int it = Convert.ToInt32(dr[Strings.DATASHEET_ITERATION_COLUMN_NAME], CultureInfo.InvariantCulture);
                    int ts = Convert.ToInt32(dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME], CultureInfo.InvariantCulture);

                    string k = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", it, ts);

                    dr[Strings.DATASHEET_SUMOFAMOUNT_COLUMN_NAME] = 
                        Convert.ToDouble(dr[Strings.DATASHEET_SUMOFAMOUNT_COLUMN_NAME], 
                        CultureInfo.InvariantCulture) / dict[k];
                }
            }

            return dt;
        }

        public static DataTable CreateRawAttributeChartData(
            Scenario scenario, 
            ChartDescriptor descriptor, 
            string tableName, 
            string attributeTypeColumnName, 
            int attributeTypeId, 
            bool isDensity, 
            DataStore store)
        {
            string tag = GetChartCacheTag(descriptor);
            string query = CreateRawAttributeDataQuery(scenario, descriptor, tableName, attributeTypeColumnName, attributeTypeId);
            DataTable dt = ChartCache.GetCachedData(scenario, query, tag);

            if (dt == null)
            {
                dt = store.CreateDataTableFromQuery(query, "RawData");
                ChartCache.SetCachedData(scenario, query, dt, tag);
            }
                          
            if (isDensity)
            {
                Dictionary<string, double> dict = CreateAmountDictionary(scenario, descriptor, store);

                if (dict.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr[Strings.DATASHEET_SUMOFAMOUNT_COLUMN_NAME] != DBNull.Value)
                        {
                            int it = Convert.ToInt32(dr[Strings.DATASHEET_ITERATION_COLUMN_NAME], CultureInfo.InvariantCulture);
                            int ts = Convert.ToInt32(dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME], CultureInfo.InvariantCulture);

                            string k = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", it, ts);

                            dr[Strings.DATASHEET_SUMOFAMOUNT_COLUMN_NAME] = 
                                Convert.ToDouble(dr[Strings.DATASHEET_SUMOFAMOUNT_COLUMN_NAME], 
                                CultureInfo.InvariantCulture) / dict[k];
                        }
                    }
                }
            }

            return dt;
        }

        public static DataTable CreateRawExternalVariableData(
            Scenario scenario,
            ChartDescriptor descriptor,
            DataStore store)
        {
            string tag = GetChartCacheTag(descriptor);
            string query = CreateRawExternalVariableDataQuery(scenario, descriptor);
            DataTable dt = ChartCache.GetCachedData(scenario, query, tag);

            if (dt == null)
            {
                dt = store.CreateDataTableFromQuery(query, "RawData");
                ChartCache.SetCachedData(scenario, query, dt, tag);
            }

            return dt;
        }

        public static DataTable CreateRawStockFlowChartData(
            DataSheet dataSheet,
            ChartDescriptor descriptor,
            DataStore store,
            string variableName)
        {
            Debug.Assert(
                variableName == Strings.STOCK_GROUP_VAR_NAME ||
                variableName == Strings.STOCK_GROUP_DENSITY_VAR_NAME ||
                variableName == Strings.FLOW_GROUP_VAR_NAME ||
                variableName == Strings.FLOW_GROUP_DENSITY_VAR_NAME);

            string query = CreateRawStockFlowChartDataQueryForGroup(dataSheet, descriptor, variableName);
            DataTable dt = ChartCache.GetCachedData(dataSheet.Scenario, query, null);

            if (dt == null)
            {
                dt = store.CreateDataTableFromQuery(query, "RawData");
                ChartCache.SetCachedData(dataSheet.Scenario, query, dt, null);
            }

            if (variableName.EndsWith("Density", StringComparison.Ordinal))
            {
                Dictionary<string, double> dict = CreateStockFlowAmountDictionary(dataSheet.Scenario, descriptor, variableName, store);

                if (dict.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        int it = Convert.ToInt32(dr[Strings.DATASHEET_ITERATION_COLUMN_NAME], CultureInfo.InvariantCulture);
                        int ts = Convert.ToInt32(dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME], CultureInfo.InvariantCulture);

                        string k = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", it, ts);

                        dr[Strings.DATASHEET_SUMOFAMOUNT_COLUMN_NAME] =
                            Convert.ToDouble(dr[Strings.DATASHEET_SUMOFAMOUNT_COLUMN_NAME],
                            CultureInfo.InvariantCulture) / dict[k];
                    }
                }
            }

            return dt;
        }

        private static string CreateRawStockFlowChartDataQueryForGroup(
            DataSheet dataSheet,
            ChartDescriptor descriptor,
            string variableName)
        {
            Debug.Assert(dataSheet.Scenario.Id > 0);

            Debug.Assert(
                variableName == Strings.STOCK_GROUP_VAR_NAME ||
                variableName == Strings.STOCK_GROUP_DENSITY_VAR_NAME ||
                variableName == Strings.FLOW_GROUP_VAR_NAME ||
                variableName == Strings.FLOW_GROUP_DENSITY_VAR_NAME);

            string ScenarioClause = string.Format(CultureInfo.InvariantCulture,
                "([{0}]={1})",
                Strings.DATASHEET_SCENARIOID_COLUMN_NAME, dataSheet.Scenario.Id);

            string SumStatement = string.Format(CultureInfo.InvariantCulture,
                "SUM([{0}]) AS {1}",
                descriptor.ColumnName, Strings.DATASHEET_SUMOFAMOUNT_COLUMN_NAME);

            string WhereClause = ScenarioClause;

            if (!string.IsNullOrEmpty(descriptor.DisaggregateFilter))
            {
                WhereClause = string.Format(CultureInfo.InvariantCulture,
                    "{0} AND ({1})",
                    WhereClause, descriptor.DisaggregateFilter);
            }

            if (!string.IsNullOrEmpty(descriptor.IncludeDataFilter))
            {
                WhereClause = string.Format(CultureInfo.InvariantCulture,
                    "{0} AND ({1})",
                    WhereClause, descriptor.IncludeDataFilter);
            }

            if (!string.IsNullOrEmpty(descriptor.SubsetFilter))
            {
                WhereClause = string.Format(CultureInfo.InvariantCulture,
                    "{0} AND ({1})",
                    WhereClause, descriptor.SubsetFilter);
            }

            string query = string.Format(CultureInfo.InvariantCulture,
                "SELECT {0},{1},{2} FROM {3} WHERE {4} GROUP BY [{5}],[{6}]",
                Strings.DATASHEET_ITERATION_COLUMN_NAME,
                Strings.DATASHEET_TIMESTEP_COLUMN_NAME,
                SumStatement,
                descriptor.DatasheetName,
                WhereClause,
                Strings.DATASHEET_ITERATION_COLUMN_NAME,
                Strings.DATASHEET_TIMESTEP_COLUMN_NAME);

            return query;
        }

        public static Dictionary<string, double> CreateStockFlowAmountDictionary(
            Scenario scenario,
            ChartDescriptor descriptor,
            string variableName,
            DataStore store)
        {
            Dictionary<string, double> dict = new Dictionary<string, double>();
            string query = CreateStockFlowAmountQuery(scenario, descriptor, variableName);
            DataTable dt = ChartCache.GetCachedData(scenario, query, null);

            if (dt == null)
            {
                dt = store.CreateDataTableFromQuery(query, "AmountData");
                ChartCache.SetCachedData(scenario, query, dt, null);
            }

            foreach (DataRow dr in dt.Rows)
            {
                int it = Convert.ToInt32(dr[Strings.DATASHEET_ITERATION_COLUMN_NAME], CultureInfo.InvariantCulture);
                int ts = Convert.ToInt32(dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME], CultureInfo.InvariantCulture);
                string k = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", it, ts);

                dict.Add(k, Convert.ToDouble
                    (dr[Strings.DATASHEET_SUMOFAMOUNT_COLUMN_NAME],
                    CultureInfo.InvariantCulture));
            }

            return dict;
        }

        private static string CreateStockFlowAmountQuery(Scenario scenario, ChartDescriptor descriptor, string variableName)
        {
            Debug.Assert(variableName.EndsWith("Density", StringComparison.Ordinal));

            string ScenarioClause = string.Format(CultureInfo.InvariantCulture,
                "([{0}]={1})",
                Strings.DATASHEET_SCENARIOID_COLUMN_NAME, scenario.Id);

            string WhereClause = ScenarioClause;
            string Disagg = RemoveUnwantedStockFlowColumnReferences(descriptor.DisaggregateFilter, variableName);
            string IncData = RemoveUnwantedStockFlowColumnReferences(descriptor.IncludeDataFilter, variableName);

            if (!string.IsNullOrEmpty(Disagg))
            {
                WhereClause = string.Format(CultureInfo.InvariantCulture, "{0} AND ({1})", WhereClause, Disagg);
            }

            if (!string.IsNullOrEmpty(IncData))
            {
                WhereClause = string.Format(CultureInfo.InvariantCulture, "{0} AND ({1})", WhereClause, IncData);
            }

            if (!string.IsNullOrEmpty(descriptor.SubsetFilter))
            {
                WhereClause = string.Format(CultureInfo.InvariantCulture, "{0} AND ({1})", WhereClause, descriptor.SubsetFilter);
            }

            string query = string.Format(CultureInfo.InvariantCulture,
                "SELECT Iteration, Timestep, SUM(Amount) AS SumOfAmount FROM stsim_OutputStratum WHERE ({0}) GROUP BY Iteration, Timestep",
                WhereClause);

            return query;
        }

        private static string RemoveUnwantedStockFlowColumnReferences(string filter, string variableName)
        {
            if (filter == null)
            {
                return null;
            }

            string[] AndSplit = filter.Split(new[] { " AND " }, StringSplitOptions.None);
            StringBuilder sb = new StringBuilder();

            if (variableName.StartsWith("stsim_Flow", StringComparison.Ordinal))
            {
                foreach (string s in AndSplit)
                {
                    string sCopy = s;

                    if (sCopy.Contains("FromStratumId"))
                    {
                        sCopy = sCopy.Replace("FromStratumId", "StratumId");
                        sb.AppendFormat(CultureInfo.InvariantCulture, "{0} AND ", sCopy);
                    }
                    else if (sCopy.Contains("FromSecondaryStratumId"))
                    {
                        sCopy = sCopy.Replace("FromSecondaryStratumId", "SecondaryStratumId");
                        sb.AppendFormat(CultureInfo.InvariantCulture, "{0} AND ", sCopy);
                    }
                    else if (sCopy.Contains("FromTertiaryStratumId"))
                    {
                        sCopy = sCopy.Replace("FromTertiaryStratumId", "TertiaryStratumId");
                        sb.AppendFormat(CultureInfo.InvariantCulture, "{0} AND ", sCopy);
                    }
                }
            }
            else
            {
                foreach (string s in AndSplit)
                {
                    if (s.Contains(Strings.DATASHEET_STRATUM_ID_COLUMN_NAME) ||
                        s.Contains(Strings.DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME) ||
                        s.Contains(Strings.DATASHEET_TERTIARY_STRATUM_ID_COLUMN_NAME))
                    {
                        sb.AppendFormat(CultureInfo.InvariantCulture, "{0} AND ", s);
                    }
                }
            }

            string final = sb.ToString();

            if (final.Count() > 0)
            {
                Debug.Assert(final.Count() >= 5);
                return final.Substring(0, final.Length - 5);
            }
            else
            {
                return null;
            }
        }

        private static Dictionary<string, double> CreateAmountDictionary(
            Scenario scenario, 
            ChartDescriptor descriptor, 
            DataStore store)
        {
            string tag = GetChartCacheTag(descriptor);
            Dictionary<string, double> dict = new Dictionary<string, double>();
            string query = CreateAmountQuery(scenario, descriptor);
            DataTable dt = ChartCache.GetCachedData(scenario, query, tag);

            if (dt == null)
            {
                dt = store.CreateDataTableFromQuery(query, "AmountData");
                ChartCache.SetCachedData(scenario, query, dt, tag);
            }

            foreach (DataRow dr in dt.Rows)
            {
                if (dr[Strings.DATASHEET_SUMOFAMOUNT_COLUMN_NAME] != DBNull.Value)
                {
                    int it = Convert.ToInt32(dr[Strings.DATASHEET_ITERATION_COLUMN_NAME], CultureInfo.InvariantCulture);
                    int ts = Convert.ToInt32(dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME], CultureInfo.InvariantCulture);

                    string k = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", it, ts);
                    dict.Add(k, Convert.ToDouble(dr[Strings.DATASHEET_SUMOFAMOUNT_COLUMN_NAME], CultureInfo.InvariantCulture));
                }
            }

            return dict;
        }

        private static string CreateAmountQuery(Scenario scenario, ChartDescriptor descriptor)
        {
            string ScenarioClause = string.Format(CultureInfo.InvariantCulture, 
                "([{0}]={1})", 
                Strings.DATASHEET_SCENARIOID_COLUMN_NAME, scenario.Id);

            string WhereClause = ScenarioClause;
            string Disagg = RemoveUnwantedColumnReferences(descriptor.DisaggregateFilter);
            string IncData = RemoveUnwantedColumnReferences(descriptor.IncludeDataFilter);

            if (!string.IsNullOrEmpty(Disagg))
            {
                WhereClause = string.Format(CultureInfo.InvariantCulture, 
                    "{0} And ({1})", 
                    WhereClause, Disagg);
            }

            if (!string.IsNullOrEmpty(IncData))
            {
                WhereClause = string.Format(CultureInfo.InvariantCulture, 
                    "{0} And ({1})", 
                    WhereClause, IncData);
            }

            if (!string.IsNullOrEmpty(descriptor.SubsetFilter))
            {
                WhereClause = string.Format(CultureInfo.InvariantCulture,
                    "{0} AND ({1})",
                    WhereClause, descriptor.SubsetFilter);
            }

            string query = string.Format(CultureInfo.InvariantCulture,
                "SELECT Iteration, Timestep, SUM(Amount) AS SumOfAmount FROM stsim_OutputStratum WHERE ({0}) GROUP BY Iteration, Timestep", 
                WhereClause);

            return query;
        }

        private static string CreateRawDataQuery(Scenario scenario, ChartDescriptor descriptor, string tableName)
        {
            string ScenarioClause = string.Format(CultureInfo.InvariantCulture, 
                "([{0}]={1})",
                Strings.DATASHEET_SCENARIOID_COLUMN_NAME, scenario.Id);

            string WhereClause = ScenarioClause;

            if (!string.IsNullOrEmpty(descriptor.DisaggregateFilter))
            {
                WhereClause = string.Format(CultureInfo.InvariantCulture, 
                    "{0} AND ({1})", 
                    WhereClause, descriptor.DisaggregateFilter);
            }

            if (!string.IsNullOrEmpty(descriptor.IncludeDataFilter))
            {
                WhereClause = string.Format(CultureInfo.InvariantCulture, 
                    "{0} AND ({1})", 
                    WhereClause, descriptor.IncludeDataFilter);
            }

            if (!string.IsNullOrEmpty(descriptor.SubsetFilter))
            {
                WhereClause = string.Format(CultureInfo.InvariantCulture,
                    "{0} AND ({1})",
                    WhereClause, descriptor.SubsetFilter);
            }

            string query = string.Format(CultureInfo.InvariantCulture, 
                "SELECT Iteration, Timestep, SUM(Amount) AS SumOfAmount FROM {0} WHERE ({1}) GROUP BY Iteration, Timestep", 
                tableName, WhereClause);

            return query;
        }

        private static string CreateRawAttributeDataQuery(
            Scenario scenario, 
            ChartDescriptor descriptor, 
            string tableName, 
            string attributeTypeColumnName, 
            int attributeTypeId)
        {
            string ScenarioClause = string.Format(CultureInfo.InvariantCulture,
                "([{0}]={1})", 
                Strings.DATASHEET_SCENARIOID_COLUMN_NAME, scenario.Id);

            string WhereClause = string.Format(CultureInfo.InvariantCulture, 
                "{0} AND ([{1}]={2})", 
                ScenarioClause, attributeTypeColumnName, attributeTypeId);

            if (!string.IsNullOrEmpty(descriptor.DisaggregateFilter))
            {
                WhereClause = string.Format(CultureInfo.InvariantCulture, 
                    "{0} AND ({1})", 
                    WhereClause, descriptor.DisaggregateFilter);
            }

            if (!string.IsNullOrEmpty(descriptor.IncludeDataFilter))
            {
                WhereClause = string.Format(CultureInfo.InvariantCulture, 
                    "{0} AND ({1})",
                    WhereClause, descriptor.IncludeDataFilter);
            }

            if (!string.IsNullOrEmpty(descriptor.SubsetFilter))
            {
                WhereClause = string.Format(CultureInfo.InvariantCulture,
                    "{0} AND ({1})",
                    WhereClause, descriptor.SubsetFilter);
            }

            string query = string.Format(CultureInfo.InvariantCulture, 
                "SELECT Iteration, Timestep, SUM(Amount) AS SumOfAmount FROM {0} WHERE ({1}) GROUP BY Iteration, Timestep", 
                tableName, WhereClause);

            return query;
        }

        private static string CreateRawExternalVariableDataQuery(Scenario scenario, ChartDescriptor descriptor)
        {
            string[] s = descriptor.VariableName.Split('-');
            Debug.Assert(s.Count() == 2 && s[0] == "stsim_ExternalVariable");
            int ExtVarTypeId = int.Parse(s[1], CultureInfo.InvariantCulture);

            string ScenarioClause = string.Format(CultureInfo.InvariantCulture,
                "([{0}]={1})",
                Strings.DATASHEET_SCENARIOID_COLUMN_NAME, scenario.Id);

            string WhereClause = string.Format(CultureInfo.InvariantCulture,
                "{0} AND ([{1}]={2})",
                ScenarioClause, Strings.OUTPUT_EXTERNAL_VARIABLE_VALUE_TYPE_ID_COLUMN_NAME, ExtVarTypeId);

            if (!string.IsNullOrEmpty(descriptor.SubsetFilter))
            {
                WhereClause = string.Format(CultureInfo.InvariantCulture,
                    "{0} AND ({1})",
                    WhereClause, descriptor.SubsetFilter);
            }

            string query = string.Format(CultureInfo.InvariantCulture,
                "SELECT Iteration, Timestep, Value AS SumOfAmount FROM {0} WHERE ({1}) ORDER BY Iteration, Timestep",
                Strings.OUTPUT_EXTERNAL_VARIABLE_VALUE_DATASHEET_NAME, WhereClause);

            return query;
        }

        private static string RemoveUnwantedColumnReferences(string filter)
        {
            if (filter == null)
            {
                return null;
            }

            string[] AndSplit = filter.Split(new[] {" AND "}, StringSplitOptions.None);
            StringBuilder sb = new StringBuilder();

            foreach (string s in AndSplit)
            {
                if (s.Contains(Strings.DATASHEET_STRATUM_ID_COLUMN_NAME) || 
                    s.Contains(Strings.DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME) ||
                    s.Contains(Strings.DATASHEET_TERTIARY_STRATUM_ID_COLUMN_NAME))
                {
                    sb.AppendFormat(CultureInfo.InvariantCulture, "{0} AND ", s);
                }
            }

            string final = sb.ToString();

            if (final.Count() > 0)
            {
                Debug.Assert(final.Count() >= 5);
                return final.Substring(0, final.Length - 5);
            }
            else
            {
                return null;
            }
        }

        private static bool OutputDataExists(
            DataStore store,
            string datasheetName,
            string columnName,
            Scenario scenario)
        {
            string query = string.Format(CultureInfo.InvariantCulture,
                "SELECT COUNT({0}) FROM {1} WHERE ScenarioId = {2}",
                columnName, datasheetName, scenario.Id);

            if (Convert.ToInt32(store.ExecuteScalar(query), CultureInfo.InvariantCulture) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static string GetChartCacheTag(ChartDescriptor descriptor)
        {
            if (DescriptorHasAgeReference(descriptor))
            {
                Debug.Assert(!DescriptorHasTSTReference(descriptor));
                return Constants.AGE_QUERY_CACHE_TAG;
            }
            else if (DescriptorHasTSTReference(descriptor))
            {
                Debug.Assert(!DescriptorHasAgeReference(descriptor));
                return Constants.TST_QUERY_CACHE_TAG;
            }
            else
            {
                return null;
            }
        }

        // =======================================================================================
        // Descriptor references
        // =======================================================================================

        private static bool DescriptorHasReference(ChartDescriptor descriptor, string columnName)
        {
            if (descriptor.IncludeDataFilter != null &&
                descriptor.IncludeDataFilter.Contains(columnName))
            {
                return true;
            }

            if (descriptor.DisaggregateFilter != null &&
                descriptor.DisaggregateFilter.Contains(columnName))
            {
                return true;
            }

            return false;
        }

        public static bool DescriptorHasAgeReference(ChartDescriptor descriptor)
        {
            return DescriptorHasReference(descriptor, Strings.DATASHEET_AGE_CLASS_COLUMN_NAME);
        }

        public static bool DescriptorHasTSTReference(ChartDescriptor descriptor)
        {
            return DescriptorHasReference(descriptor, Strings.DATASHEET_TST_CLASS_COLUMN_NAME);
        }

        // =======================================================================================
        // Cache entries
        // =======================================================================================

        private static void DeleteCacheEntriesWithTag(Scenario scenario, string tag)
        {
            string CacheFolder = ChartCache.GetCacheFolderName(scenario);

            foreach (string f in Directory.GetFiles(CacheFolder))
            {
                if (f.EndsWith(tag, StringComparison.Ordinal))
                {
                    File.Delete(f);
                }
            }
        }

        private static void DeleteAgeRelatedCacheEntries(Scenario scenario)
        {
            DeleteCacheEntriesWithTag(scenario, Constants.AGE_QUERY_CACHE_TAG);
        }

        private static void DeleteTSTRelatedCacheEntries(Scenario scenario)
        {
            DeleteCacheEntriesWithTag(scenario, Constants.TST_QUERY_CACHE_TAG);
        }

        // =======================================================================================
        // Class update tag
        // =======================================================================================

        private static void SetClassUpdateTag(Project project, string tag)
        {
            if (!project.Tags.Contains(tag))
            {
                project.Tags.Add(new Tag(tag, null));
            }
        }

        private static void ClearClassUpdateTag(Project project, string tag)
        {
            if (project.Tags.Contains(tag))
            {
                project.Tags.Remove(tag);
            }
        }

        public static void SetAgeClassUpdateTag(Project project)
        {
            SetClassUpdateTag(project, Constants.AGECLASS_UPDATE_REQUIRED_TAG);
        }

        public static void SetTSTClassUpdateTag(Project project)
        {
            SetClassUpdateTag(project, Constants.TSTCLASS_UPDATE_REQUIRED_TAG);
        }

        public static void ClearAgeClassUpdateTag(Project project)
        {
            ClearClassUpdateTag(project, Constants.AGECLASS_UPDATE_REQUIRED_TAG);
        }

        public static void ClearTSTClassUpdateTag(Project project)
        {
            ClearClassUpdateTag(project, Constants.TSTCLASS_UPDATE_REQUIRED_TAG);
        }

        public static bool HasAgeClassUpdateTag(Project project)
        {
            return project.Tags.Contains(Constants.AGECLASS_UPDATE_REQUIRED_TAG);
        }

        public static bool HasTSTClassUpdateTag(Project project)
        {
            return project.Tags.Contains(Constants.TSTCLASS_UPDATE_REQUIRED_TAG);
        }

        // =======================================================================================
        // Classes match data
        //
        // Mismatched age/tst bins can occur due to a difference in the current age/tst definitions
        // and the age/tst data that was generated when the scenario was run.
        // =======================================================================================

        private static bool ClassesMatchData(
            DataStore store, 
            DataSheet dataSheet, 
            string minColName, 
            string classColName)
        {
            //If any Age/tst Class values are NULL then the ?Min and ?Max did not fall into a Class bin.
            //This could happen if the data contains values such as 10 - 19, but the frequency is 13.  In this
            //case the bins would be 0-12, 13-25, etc., and 10-19 does not go into any of these bins.

            string query = string.Format(CultureInfo.InvariantCulture,
                "SELECT COUNT({0}) FROM {1} WHERE ({2} IS NULL) AND ScenarioId = {3}",
                minColName, dataSheet.Name, classColName, dataSheet.Scenario.Id);

            if (Convert.ToInt32(store.ExecuteScalar(query), CultureInfo.InvariantCulture) != 0)
            {
                return false;
            }

            return true;
        }

        private static bool AgeClassesMatchData(DataStore store, DataSheet dataSheet)
        {
            return ClassesMatchData(
                store, 
                dataSheet, 
                Strings.DATASHEET_AGE_MIN_COLUMN_NAME, 
                Strings.DATASHEET_AGE_CLASS_COLUMN_NAME);
        }

        private static bool TSTClassesMatchData(DataStore store, DataSheet dataSheet)
        {
            return ClassesMatchData(
                store, 
                dataSheet, 
                Strings.DATASHEET_TST_MIN_COLUMN_NAME, 
                Strings.DATASHEET_TST_CLASS_COLUMN_NAME);
        }

        // =======================================================================================
        // Class Bin Descriptors
        // =======================================================================================

        private static List<ClassBinDescriptor> GetClassBinDescriptors(
            Project project,
            string classTypeDatasheetName,
            string classTypeFrequencyColumnName,
            string classTypeMaximumColumnName, 
            string classGroupDatasheetName,
            string classGroupMaximumColumnName, 
            DataStore store)
        {
            List<ClassBinDescriptor> e = GetClassBinGroupDescriptors(
                project, 
                classGroupDatasheetName, 
                classGroupMaximumColumnName, 
                store);

            if (e == null)
            {
                e = GetClassBinTypeDescriptors(
                    project, 
                    classTypeDatasheetName, 
                    classTypeFrequencyColumnName, 
                    classTypeMaximumColumnName, 
                    store);
            }

#if DEBUG
            if (e != null)
            {
                Debug.Assert(e.Count() > 0);
            }
#endif

            if (e == null)
            {
                return null;
            }

            if (e.Count() == 0)
            {
                return null;
            }

            return e;
        }

        public static List<ClassBinDescriptor> GetClassBinGroupDescriptors(
            Project project, 
            string datasheetName, 
            string maximumColumnName, 
            DataStore store)
        {
            DataTable dt = project.GetDataSheet(datasheetName).GetData(store);
            DataView dv = new DataView(dt, null, null, DataViewRowState.CurrentRows);

            if (dv.Count == 0)
            {
                return null;
            }

            List<ClassBinDescriptor> lst = new List<ClassBinDescriptor>();
            Dictionary<int, bool> dict = new Dictionary<int, bool>();

            foreach (DataRowView drv in dv)
            {
                int value = Convert.ToInt32(drv[
                    maximumColumnName],
                    CultureInfo.InvariantCulture);

                if (!dict.ContainsKey(value))
                {
                    lst.Add(new ClassBinDescriptor(value, value));
                    dict.Add(value, true);
                }
            }

            lst.Sort((ClassBinDescriptor d1, ClassBinDescriptor d2) =>
            {
                return d1.Minimum.CompareTo(d2.Minimum);
            });

            int Prev = 0;

            for  (int i = 0; i < lst.Count; i++)
            {
                ClassBinDescriptor d = lst[i];
                int t = d.Minimum;

                d.Minimum = Prev;
                Prev = t + 1;
            }

            ClassBinDescriptor last = lst[lst.Count - 1];
            lst.Add(new ClassBinDescriptor(last.Maximum.Value + 1, null));

#if DEBUG
            Debug.Assert(lst.Count > 0);

            foreach (ClassBinDescriptor ad in lst)
            {
                if (ad.Maximum.HasValue)
                {
                    Debug.Assert(ad.Minimum <= ad.Maximum.Value);
                }
            }

            Debug.Assert(lst[lst.Count - 1].Maximum == null);
#endif

            return lst;
        }

        public static List<ClassBinDescriptor> GetClassBinTypeDescriptors(
            Project project, 
            string datasheetName, 
            string frequencyColumnName, 
            string maximumColumnName, 
            DataStore store)
        {
            DataRow dr = project.GetDataSheet(datasheetName).GetDataRow(store);

            if (dr != null)
            {
                if (dr[frequencyColumnName] != DBNull.Value &&
                    dr[maximumColumnName] != DBNull.Value)
                {
                    int f = Convert.ToInt32(dr[frequencyColumnName], CultureInfo.InvariantCulture);
                    int m = Convert.ToInt32(dr[maximumColumnName], CultureInfo.InvariantCulture);

                    if (f <= m)
                    {
                        ClassBinHelper h = new ClassBinHelper(true, f, m);
                        return h.GetDescriptors();
                    }
                }
            }

            return null;
        }

        // =======================================================================================
        // Table update
        // =======================================================================================

        public static void UpdateAgeClassIfRequired(DataStore store, Project project)
        {
            if (project.Tags.Contains(Constants.AGECLASS_UPDATE_REQUIRED_TAG))
            {
                foreach (Scenario s in project.Library.Scenarios)
                {
                    if (!s.IsDeleted && s.IsResult && s.Project == project)
                    {
                        UpdateAgeClassWork(store, project, s);
                        DeleteAgeRelatedCacheEntries(s);
                    }
                }
            }
        }

        public static void UpdateTSTClassIfRequired(DataStore store, Project project)
        {
            if (project.Tags.Contains(Constants.TSTCLASS_UPDATE_REQUIRED_TAG))
            {
                foreach (Scenario s in project.Library.Scenarios)
                {
                    if (!s.IsDeleted && s.IsResult && s.Project == project)
                    {
                        UpdateTSTClassWork(store, project, s);
                        DeleteTSTRelatedCacheEntries(s);
                    }
                }
            }
        }

        public static void UpdateAgeClassWork(DataStore store, Project project, Scenario scenario)
        {
            UpdateClassBinColumn(
                store, project, scenario,
                Strings.DATASHEET_AGE_TYPE_NAME,
                Strings.DATASHEET_AGE_TYPE_FREQUENCY_COLUMN_NAME,
                Strings.DATASHEET_AGE_TYPE_MAXIMUM_COLUMN_NAME,
                Strings.DATASHEET_AGE_GROUP_NAME,
                Strings.DATASHEET_AGE_GROUP_MAXIMUM_COLUMN_NAME,
                Strings.DATASHEET_OUTPUT_STRATUM_STATE_NAME,
                Strings.DATASHEET_AGE_MIN_COLUMN_NAME,
                Strings.DATASHEET_AGE_MAX_COLUMN_NAME,
                Strings.DATASHEET_AGE_CLASS_COLUMN_NAME);

            UpdateClassBinColumn(
                store, project, scenario,
                Strings.DATASHEET_AGE_TYPE_NAME,
                Strings.DATASHEET_AGE_TYPE_FREQUENCY_COLUMN_NAME,
                Strings.DATASHEET_AGE_TYPE_MAXIMUM_COLUMN_NAME,
                Strings.DATASHEET_AGE_GROUP_NAME,
                Strings.DATASHEET_AGE_GROUP_MAXIMUM_COLUMN_NAME,
                Strings.DATASHEET_OUTPUT_STRATUM_TRANSITION_NAME,
                Strings.DATASHEET_AGE_MIN_COLUMN_NAME,
                Strings.DATASHEET_AGE_MAX_COLUMN_NAME,
                Strings.DATASHEET_AGE_CLASS_COLUMN_NAME);

            UpdateClassBinColumn(
                store, project, scenario,
                Strings.DATASHEET_AGE_TYPE_NAME,
                Strings.DATASHEET_AGE_TYPE_FREQUENCY_COLUMN_NAME,
                Strings.DATASHEET_AGE_TYPE_MAXIMUM_COLUMN_NAME,
                Strings.DATASHEET_AGE_GROUP_NAME,
                Strings.DATASHEET_AGE_GROUP_MAXIMUM_COLUMN_NAME,
                Strings.DATASHEET_OUTPUT_STATE_ATTRIBUTE_NAME,
                Strings.DATASHEET_AGE_MIN_COLUMN_NAME,
                Strings.DATASHEET_AGE_MAX_COLUMN_NAME,
                Strings.DATASHEET_AGE_CLASS_COLUMN_NAME);

            UpdateClassBinColumn(
                store, project, scenario,
                Strings.DATASHEET_AGE_TYPE_NAME,
                Strings.DATASHEET_AGE_TYPE_FREQUENCY_COLUMN_NAME,
                Strings.DATASHEET_AGE_TYPE_MAXIMUM_COLUMN_NAME,
                Strings.DATASHEET_AGE_GROUP_NAME,
                Strings.DATASHEET_AGE_GROUP_MAXIMUM_COLUMN_NAME,
                Strings.DATASHEET_OUTPUT_TRANSITION_ATTRIBUTE_NAME,
                Strings.DATASHEET_AGE_MIN_COLUMN_NAME,
                Strings.DATASHEET_AGE_MAX_COLUMN_NAME,
                Strings.DATASHEET_AGE_CLASS_COLUMN_NAME);
        }

        public static void UpdateTSTClassWork(DataStore store, Project project, Scenario scenario)
        {
            UpdateClassBinColumn(
                store, project, scenario,
                Strings.DATASHEET_TST_TYPE_NAME,
                Strings.DATASHEET_TST_TYPE_FREQUENCY_COLUMN_NAME,
                Strings.DATASHEET_TST_TYPE_MAXIMUM_COLUMN_NAME,
                Strings.DATASHEET_TST_GROUP_NAME,
                Strings.DATASHEET_TST_GROUP_MAXIMUM_COLUMN_NAME,
                Strings.DATASHEET_OUTPUT_TST_NAME,
                Strings.DATASHEET_TST_MIN_COLUMN_NAME,
                Strings.DATASHEET_TST_MAX_COLUMN_NAME,
                Strings.DATASHEET_TST_CLASS_COLUMN_NAME);
        }

        private static void UpdateClassBinColumn(
            DataStore store, 
            Project project,
            Scenario scenario,
            string classTypeDatasheetName,
            string classTypeFrequencyColumnName,
            string classTypeMaximumColumnName,
            string classGroupDatasheetName,
            string classGroupMaximumColumnName, 
            string outputDatasheetName, 
            string outputDatasheetMinimumColumnName, 
            string outputDatasheetMaximumColumnName,
            string outputDatasheetClassColumnName)
        {
            List<ClassBinDescriptor> e = GetClassBinDescriptors(
                project,
                classTypeDatasheetName, 
                classTypeFrequencyColumnName, 
                classTypeMaximumColumnName,
                classGroupDatasheetName, 
                classGroupMaximumColumnName, 
                store);

            if (e == null)
            {
                return;
            }

            if (!OutputDataExists(
                store, 
                outputDatasheetName, 
                Strings.DATASHEET_SCENARIOID_COLUMN_NAME, 
                scenario))
            {
                return;
            }

            if (!OutputDataExists(
                store,
                outputDatasheetName,
                outputDatasheetMinimumColumnName,
                scenario))
            {
                return;
            }

            StringBuilder sb = new StringBuilder();
            Debug.Assert(!(e.ElementAtOrDefault(e.Count() - 1).Maximum.HasValue));

            if (e.Count() > 1)
            {
                sb.AppendFormat(CultureInfo.InvariantCulture,
                    "UPDATE [{0}] SET {1} = CASE",
                    outputDatasheetName, 
                    outputDatasheetClassColumnName);

                for (int i = 0; i < e.Count(); i++)
                {
                    ClassBinDescriptor d = e.ElementAtOrDefault(i);

                    if (d.Maximum.HasValue)
                    {
                        Debug.Assert(i < e.Count() - 1);

                        sb.AppendFormat(CultureInfo.InvariantCulture,
                            " WHEN {0} >= {1} And {2} <= {3} THEN {4}", 
                            outputDatasheetMinimumColumnName, 
                            d.Minimum, 
                            outputDatasheetMaximumColumnName,
                            d.Maximum.Value, 
                            d.Minimum);
                    }
                    else
                    {
                        Debug.Assert(i == e.Count() - 1);

                        sb.AppendFormat(CultureInfo.InvariantCulture,
                            " WHEN {0} >= {1} THEN {2}",
                            outputDatasheetMinimumColumnName,
                            d.Minimum,
                            d.Minimum);
                    }
                }

                sb.AppendFormat(" WHEN {0} IS NULL THEN {1}", 
                    outputDatasheetMaximumColumnName,
                    e.Last().Minimum);

                sb.Append(" END");
            }
            else
            {
                sb.AppendFormat(CultureInfo.InvariantCulture,
                    "UPDATE [{0}] SET {1} = {2}",
                    outputDatasheetName, 
                    outputDatasheetClassColumnName,
                    e.ElementAtOrDefault(0).Minimum);
            }

            sb.AppendFormat(CultureInfo.InvariantCulture, " WHERE ScenarioId = {0}", scenario.Id);
            store.ExecuteNonQuery(sb.ToString());
        }
    }
}
