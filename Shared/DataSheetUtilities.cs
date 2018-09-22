// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;

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

            return Convert.ToBoolean(dr[Strings.DATASHEET_TRANSITION_TYPE_GROUP_PRIMARY_COLUMN_NAME]);
        }
    }
}
