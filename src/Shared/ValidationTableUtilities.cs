// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using SyncroSim.Core;
using System.Globalization;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    internal static class ValidationTableUtilities
    {
        /// <summary>
        /// Creates an age validation table
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static ValidationTable CreateAgeValidationTable(Project project)
        {
            DataTable dt = new DataTable(Strings.AGE_VALIDATION_TABLE_NAME);
            dt.Locale = CultureInfo.InvariantCulture;

            dt.Columns.Add(new DataColumn(Strings.VALUE_MEMBER_COLUMN_NAME, typeof(long)));
            dt.Columns.Add(new DataColumn(Strings.DISPLAY_MEMBER_COLUMN_NAME, typeof(string)));

            IEnumerable<AgeDescriptor> e = ChartingUtilities.GetAgeGroupDescriptors(project);
            bool AddZeroRecord = false;

            if (e == null)
            {
                e = ChartingUtilities.GetAgeTypeDescriptors(project);
                AddZeroRecord = true;
            }

            if (e != null)
            {
                if (AddZeroRecord)
                {
                    dt.Rows.Add(new object[] { 0, "0" });
                }

                foreach (AgeDescriptor d in e)
                {
                    long Value = Convert.ToInt64(d.MinimumAge);
                    string Display = null;

                    if (d.MaximumAge.HasValue)
                    {
                        if (d.MaximumAge.Value == d.MinimumAge)
                        {
                            Display = string.Format(CultureInfo.InvariantCulture, "{0}", d.MinimumAge);
                        }
                        else
                        {
                            Display = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", d.MinimumAge, d.MaximumAge.Value);
                        }
                    }
                    else
                    {
                        Display = string.Format(CultureInfo.InvariantCulture, "{0}+", d.MinimumAge);
                    }

                    dt.Rows.Add(new object[] {Value, Display});
                }
            }

            return new ValidationTable(dt, Strings.VALUE_MEMBER_COLUMN_NAME, Strings.DISPLAY_MEMBER_COLUMN_NAME, SortOrder.None);
        }
    }
}
