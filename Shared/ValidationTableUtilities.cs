// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

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

            IEnumerable<AgeDescriptor> e = AgeUtilities.GetAgeGroupDescriptors(project);

            if (e == null)
            {
                e = AgeUtilities.GetAgeTypeDescriptors(project);
            }

            if (e != null)
            {
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
