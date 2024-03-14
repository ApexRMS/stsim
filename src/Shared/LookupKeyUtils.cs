// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Globalization;
using System.Collections.Generic;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    internal static class LookupKeyUtils
    {
        public static int GetOutputCollectionKey(int? value)
        {
            if (value.HasValue)
            {
                return value.Value;
            }
            else
            {
                return Constants.OUTPUT_COLLECTION_WILDCARD_KEY;
            }
        }

		public static Dictionary<int, bool> CreateRecordLookup(DataSheet ds, string colName)
		{
			Dictionary<int, bool> d = new Dictionary<int, bool>();
			DataTable dt = ds.GetData();

			foreach (DataRow dr in dt.Rows)
			{
				if (dr.RowState != DataRowState.Deleted)
				{
					d.Add(Convert.ToInt32(dr[colName], CultureInfo.InvariantCulture), true);
				}
			}

			return d;
		}
	}
}
