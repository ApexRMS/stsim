// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.IO;
using System.Data;
using System.Linq;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    /// <summary>
    /// Age Utilities
    /// </summary>
    /// <remarks></remarks>
    internal static class AgeUtilities
    {
        /// <summary>
        /// Gets a collection of current age descriptors
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static IEnumerable<AgeDescriptor> GetAgeDescriptors(Project project)
        {
            IEnumerable<AgeDescriptor> e = GetAgeGroupDescriptors(project);

            if (e == null)
            {
                e = GetAgeTypeDescriptors(project);
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

        /// <summary>
        /// Gets the AgeDescriptor in position [max-1]
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public static AgeDescriptor GetNextToLastAgeDescriptor(Project project)
        {
            IEnumerable<AgeDescriptor> e = GetAgeTypeDescriptors(project);

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

            if (e.Count() == 1)
            {
                return e.First();
            }
            else
            {
                List<AgeDescriptor> l = e.ToList();
                return l[e.Count() - 2];
            }
        }

        /// <summary>
        /// Gets an enumeration of age descriptors from the age group data sheet
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static IEnumerable<AgeDescriptor> GetAgeGroupDescriptors(Project project)
        {
            DataTable dt = project.GetDataSheet(Strings.DATASHEET_AGE_GROUP_NAME).GetData();
            DataView dv = new DataView(dt, null, null, DataViewRowState.CurrentRows);

            if (dv.Count == 0)
            {
                return null;
            }

            List<AgeDescriptor> lst = new List<AgeDescriptor>();
            Dictionary<int, bool> dict = new Dictionary<int, bool>();

            foreach (DataRowView drv in dv)
            {
                int value = Convert.ToInt32(drv[Strings.DATASHEET_AGE_GROUP_MAXIMUM_COLUMN_NAME], CultureInfo.InvariantCulture);

                if (!dict.ContainsKey(value))
                {
                    lst.Add(new AgeDescriptor(value, value));
                    dict.Add(value, true);
                }
            }

            lst.Sort((AgeDescriptor ad1, AgeDescriptor ad2) =>
            {
                     return ad1.MinimumAge.CompareTo(ad2.MinimumAge);
            });

            int Prev = 0;

            foreach (AgeDescriptor ad in lst)
            {
                int t = ad.MinimumAge;

                ad.MinimumAge = Prev;
                Prev = t + 1;
            }

            lst.Add(new AgeDescriptor(Prev, null));

#if DEBUG

            Debug.Assert(lst.Count > 0);

            foreach (AgeDescriptor ad in lst)
            {
                if (ad.MaximumAge.HasValue)
                {
                    Debug.Assert(ad.MinimumAge <= ad.MaximumAge.Value);
                }
            }

#endif

            lst[lst.Count - 1].MaximumAge = null;
            return lst;
        }

        /// <summary>
        /// Gets an enumeration of age descriptors from the age type data sheet
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static IEnumerable<AgeDescriptor> GetAgeTypeDescriptors(Project project)
        {
            DataRow dr = project.GetDataSheet(Strings.DATASHEET_AGE_TYPE_NAME).GetDataRow();

            if (dr != null)
            {
                if (dr[Strings.DATASHEET_AGE_TYPE_FREQUENCY_COLUMN_NAME] != DBNull.Value && 
                    dr[Strings.DATASHEET_AGE_TYPE_MAXIMUM_COLUMN_NAME] != DBNull.Value)
                {
                    int f = Convert.ToInt32(dr[Strings.DATASHEET_AGE_TYPE_FREQUENCY_COLUMN_NAME], CultureInfo.InvariantCulture);
                    int m = Convert.ToInt32(dr[Strings.DATASHEET_AGE_TYPE_MAXIMUM_COLUMN_NAME], CultureInfo.InvariantCulture);

                    if (f <= m)
                    {
                        AgeHelper h = new AgeHelper(true, f, m);
                        return h.GetAges();
                    }
                }
            }

            return null;
        }

        public static void UpdateAgeClassIfRequired(DataStore store, Project project)
        {
            if (project.Tags.Contains(Constants.AGECLASS_UPDATE_REQUIRED_TAG))
            {
                foreach (Scenario s in project.Library.Scenarios)
                {
                    if (!s.IsDeleted && s.IsResult && s.Project == project)
                    {
                        UpdateAgeClassWork(store, s);
                        DeleteAgeRelatedCacheEntries(s);
                    }
                }                
            }
        }

        public static void UpdateAgeClassWork(DataStore store, Scenario s)
        {
            ChartingUtilities.UpdateAgeClassColumn(store, s.GetDataSheet(Strings.DATASHEET_OUTPUT_STRATUM_TRANSITION_NAME));
            ChartingUtilities.UpdateAgeClassColumn(store, s.GetDataSheet(Strings.DATASHEET_OUTPUT_STRATUM_STATE_NAME));
            ChartingUtilities.UpdateAgeClassColumn(store, s.GetDataSheet(Strings.DATASHEET_OUTPUT_STATE_ATTRIBUTE_NAME));
            ChartingUtilities.UpdateAgeClassColumn(store, s.GetDataSheet(Strings.DATASHEET_OUTPUT_TRANSITION_ATTRIBUTE_NAME));
        }

        public static void DeleteAgeRelatedCacheEntries(Scenario scenario)
        {
            string CacheFolder = StochasticTime.ChartCache.GetCacheFolderName(scenario);

            foreach (string f in Directory.GetFiles(CacheFolder))
            {
                if (f.EndsWith(Constants.AGE_QUERY_CACHE_TAG, StringComparison.Ordinal))
                {
                    File.Delete(f);
                }
            }
        }

        public static void SetAgeClassUpdateTag(Project project)
        {
            if (!project.Tags.Contains(Constants.AGECLASS_UPDATE_REQUIRED_TAG))
            {
                project.Tags.Add(new Tag(Constants.AGECLASS_UPDATE_REQUIRED_TAG, null));
            }
        }

        public static void ClearAgeClassUpdateTag(Project project)
        {
            if (project.Tags.Contains(Constants.AGECLASS_UPDATE_REQUIRED_TAG))
            {
                project.Tags.Remove(Constants.AGECLASS_UPDATE_REQUIRED_TAG);
            }
        }

        public static bool HasAgeClassUpdateTag(Project project)
        {
            return project.Tags.Contains(Constants.AGECLASS_UPDATE_REQUIRED_TAG);
        }
    }
}
