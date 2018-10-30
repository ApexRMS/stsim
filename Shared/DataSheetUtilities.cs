// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Globalization;

namespace SyncroSim.STSim
{
    internal static class DataSheetUtilities
    {
        public static bool IsPrimaryTypeByGroup(DataRow dr)
        {
            if (dr[Strings.DATASHEET_TRANSITION_TYPE_GROUP_PRIMARY_COLUMN_NAME] == DBNull.Value)
            {
                return true;
            }

            return Convert.ToBoolean(
                dr[Strings.DATASHEET_TRANSITION_TYPE_GROUP_PRIMARY_COLUMN_NAME], 
                CultureInfo.InvariantCulture);
        }
    }
}
