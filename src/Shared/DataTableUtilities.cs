// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Globalization;

namespace SyncroSim.STSim
{
    internal static class DataTableUtilities
    {
        /// <summary>
        /// Sets the specified row value
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        public static void SetRowValue(DataRow dr, string columnName, object value)
        {
            if (object.ReferenceEquals(value, DBNull.Value) || object.ReferenceEquals(value, null))
            {
                dr[columnName] = DBNull.Value;
            }
            else
            {
                dr[columnName] = value;
            }
        }

        /// <summary>
        /// Gets a bool for the specified database object
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool GetDataBool(object value)
        {
            if (object.ReferenceEquals(value, DBNull.Value))
            {
                return false;
            }
            else
            {
                return Convert.ToBoolean(value, CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Gets a boolean from the specified data row and column name
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool GetDataBool(DataRow dr, string columnName)
        {
            return GetDataBool(dr[columnName]);
        }

        /// <summary>
        /// Gets a int for the specified database object
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static int GetDataInt(object value)
        {
            if (object.ReferenceEquals(value, DBNull.Value))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(value, CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Gets a Single for the specified database object
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static float GetDataSingle(object value)
        {
            if (object.ReferenceEquals(value, DBNull.Value))
            {
                return 0F;
            }
            else
            {
                return Convert.ToSingle(value, CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Gets a Double for the specified database object
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static double GetDataDbl(object value)
        {
            if (object.ReferenceEquals(value, DBNull.Value))
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(value, CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Gets a string from the specified database object
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetDataStr(object value)
        {
            if (object.ReferenceEquals(value, DBNull.Value))
            {
                return null;
            }
            else
            {
                return Convert.ToString(value, CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Gets a string from the specified data row and column name
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetDataStr(DataRow dr, string columnName)
        {
            return GetDataStr(dr[columnName]);
        }

        /// <summary>
        /// Gets a database value for the specified nullable boolean
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object GetNullableDatabaseValue(bool? value)
        {
            if (value.HasValue)
            {
                if (value.Value)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return DBNull.Value;
            }
        }

        /// <summary>
        /// Gets a database value for the specified nullable integer
        /// </summary>
        /// <param name="value"></param>
        /// <returns>If the integer has a value, that value is returned.  Otherwise, DBNull.Value is returned.</returns>
        public static object GetNullableDatabaseValue(int? value)
        {
            if (value.HasValue)
            {
                return value.Value;
            }
            else
            {
                return DBNull.Value;
            }
        }

        /// <summary>
        /// Gets a database value for the specified nullable double
        /// </summary>
        /// <param name="value"></param>
        /// <returns>If the double has a value, that value is returned.  Otherwise, DBNull.Value is returned.</returns>
        public static object GetNullableDatabaseValue(double? value)
        {
            if (value.HasValue)
            {
                return value.Value;
            }
            else
            {
                return DBNull.Value;
            }
        }

        /// <summary>
        /// Gets a nullable integer for the specified data row and colum name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static int? GetNullableInt(DataRow dr, string columnName)
        {
            object value = dr[columnName];

            if (object.ReferenceEquals(value, DBNull.Value) || object.ReferenceEquals(value, null))
            {
                return null;
            }
            else
            {
                return Convert.ToInt32(value, CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Gets a nullable double for the specified data row and colum name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static double? GetNullableDouble(DataRow dr, string columnName)
        {
            object value = dr[columnName];

            if (object.ReferenceEquals(value, DBNull.Value) || object.ReferenceEquals(value, null))
            {
                return null;
            }
            else
            {
                return Convert.ToDouble(value, CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Deletes the specified table row
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dr"></param>
        /// <remarks></remarks>

        public static void DeleteTableRow(DataTable dt, DataRow dr)
        {
            if (dr.RowState == DataRowState.Added)
            {
                dt.Rows.Remove(dr);
            }
            else
            {
                dr.Delete();
            }
        }

        /// <summary>
        /// Gets the table value for the specified id column and value column
        /// </summary>
        /// <param name="table"></param>
        /// <param name="idColumnName"></param>
        /// <param name="idColumnValue"></param>
        /// <param name="valueColumnName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static object GetTableValue(DataTable table, string idColumnName, int idColumnValue, string valueColumnName)
        {
            foreach (DataRow dr in table.Rows)
            {
                if (dr.RowState == DataRowState.Deleted)
                {
                    continue;
                }

                if (Convert.ToInt32(dr[idColumnName], CultureInfo.InvariantCulture) == idColumnValue)
                {
                    return (dr[valueColumnName]);
                }
            }

            return DBNull.Value;
        }

        public static bool TableHasData(DataTable table, string columnName)
        {

            foreach (DataRow dr in table.Rows)
            {
                if (dr.RowState == DataRowState.Deleted)
                {
                    continue;
                }

                object value = dr[columnName];

                if (!object.ReferenceEquals(value, DBNull.Value))
                {
                    return true;
                }
            }

            return false;
        }

        public static FlowPathway CreateFlowPathway(DataRow dr)
        {
            int? Iteration = null;
            int? Timestep = null;
            int? FromStratumId = null;
            int? FromSecondaryStratumId = null;
            int? FromTertiaryStratumId = null;
            int? FromStateClassId = null;
            int? FromMinimumAge = null;
            int? FromStockTypeId = null;
            int? ToStratumId = null;
            int? ToStateClassId = null;
            int? ToMinimumAge = null;
            int? ToStockTypeId = null;
            int? StateAttributeTypeId = null;
            int? TargetType = null;
            int? TransferToStratumId = null;
            int? TransferToSecondaryStratumId = null;
            int? TransferToTertiaryStratumId = null;
            int? TransferToStateClassId = null;
            int? TransferToMinimumAge = null;

            if (dr[Strings.DATASHEET_ITERATION_COLUMN_NAME] != DBNull.Value)
            {
                Iteration = Convert.ToInt32(dr[Strings.DATASHEET_ITERATION_COLUMN_NAME], CultureInfo.InvariantCulture);
            }

            if (dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME] != DBNull.Value)
            {
                Timestep = Convert.ToInt32(dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME], CultureInfo.InvariantCulture);
            }

            if (dr[Strings.FROM_STRATUM_ID_COLUMN_NAME] != DBNull.Value)
            {
                FromStratumId = Convert.ToInt32(dr[Strings.FROM_STRATUM_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
            }

            if (dr[Strings.FROM_SECONDARY_STRATUM_ID_COLUMN_NAME] != DBNull.Value)
            {
                FromSecondaryStratumId = Convert.ToInt32(dr[Strings.FROM_SECONDARY_STRATUM_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
            }

            if (dr[Strings.FROM_TERTIARY_STRATUM_ID_COLUMN_NAME] != DBNull.Value)
            {
                FromTertiaryStratumId = Convert.ToInt32(dr[Strings.FROM_TERTIARY_STRATUM_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
            }

            if (dr[Strings.FROM_STATECLASS_ID_COLUMN_NAME] != DBNull.Value)
            {
                FromStateClassId = Convert.ToInt32(dr[Strings.FROM_STATECLASS_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
            }

            if (dr[Strings.FROM_MIN_AGE_COLUMN_NAME] != DBNull.Value)
            {
                FromMinimumAge = Convert.ToInt32(dr[Strings.FROM_MIN_AGE_COLUMN_NAME], CultureInfo.InvariantCulture);
            }

            if (dr[Strings.FROM_STOCK_TYPE_ID_COLUMN_NAME] != DBNull.Value)
            {
                FromStockTypeId = Convert.ToInt32(dr[Strings.FROM_STOCK_TYPE_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
            }

            if (dr[Strings.TO_STRATUM_ID_COLUMN_NAME] != DBNull.Value)
            {
                ToStratumId = Convert.ToInt32(dr[Strings.TO_STRATUM_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
            }

            if (dr[Strings.TO_STATECLASS_ID_COLUMN_NAME] != DBNull.Value)
            {
                ToStateClassId = Convert.ToInt32(dr[Strings.TO_STATECLASS_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
            }

            if (dr[Strings.TO_MIN_AGE_COLUMN_NAME] != DBNull.Value)
            {
                ToMinimumAge = Convert.ToInt32(dr[Strings.TO_MIN_AGE_COLUMN_NAME], CultureInfo.InvariantCulture);
            }

            if (dr[Strings.TO_STOCK_TYPE_ID_COLUMN_NAME] != DBNull.Value)
            {
                ToStockTypeId = Convert.ToInt32(dr[Strings.TO_STOCK_TYPE_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
            }

            int TransitionGroupId;
            if (dr[Strings.DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME] != DBNull.Value)
            {
                TransitionGroupId = Convert.ToInt32(dr[Strings.DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
            }
            else
            {
                TransitionGroupId = 0;
            }

            if (dr[Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_ID_COLUMN_NAME] != DBNull.Value)
            {
                StateAttributeTypeId = Convert.ToInt32(dr[Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
            }

            int FlowTypeId = Convert.ToInt32(dr[Strings.FLOW_TYPE_ID_COLUMN_NAME], CultureInfo.InvariantCulture);

            if (dr[Strings.TARGET_TYPE] != DBNull.Value)
            {
                TargetType = Convert.ToInt32(dr[Strings.TARGET_TYPE], CultureInfo.InvariantCulture);
            }

            double Multiplier = Convert.ToDouble(dr[Strings.DATASHEET_MULTIPLIER_COLUMN_NAME], CultureInfo.InvariantCulture);

            if (dr[Strings.TRANSFER_TO_STRATUM_ID_COLUMN_NAME] != DBNull.Value)
            {
                TransferToStratumId = Convert.ToInt32(dr[Strings.TRANSFER_TO_STRATUM_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
            }

            if (dr[Strings.TRANSFER_TO_SECONDARY_STRATUM_ID_COLUMN_NAME] != DBNull.Value)
            {
                TransferToSecondaryStratumId = Convert.ToInt32(dr[Strings.TRANSFER_TO_SECONDARY_STRATUM_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
            }

            if (dr[Strings.TRANSFER_TO_TERTIARY_STRATUM_ID_COLUMN_NAME] != DBNull.Value)
            {
                TransferToTertiaryStratumId = Convert.ToInt32(dr[Strings.TRANSFER_TO_TERTIARY_STRATUM_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
            }

            if (dr[Strings.TRANSFER_TO_STATECLASS_ID_COLUMN_NAME] != DBNull.Value)
            {
                TransferToStateClassId = Convert.ToInt32(dr[Strings.TRANSFER_TO_STATECLASS_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
            }

            if (dr[Strings.TRANSFER_TO_MIN_AGE_COLUMN_NAME] != DBNull.Value)
            {
                TransferToMinimumAge = Convert.ToInt32(dr[Strings.TRANSFER_TO_MIN_AGE_COLUMN_NAME], CultureInfo.InvariantCulture);
            }

            FlowPathway p = new FlowPathway(
                Iteration, Timestep,
                FromStratumId, FromSecondaryStratumId, FromTertiaryStratumId, FromStateClassId, FromMinimumAge, FromStockTypeId,
                ToStratumId, ToStateClassId, ToMinimumAge, ToStockTypeId,
                TransitionGroupId, StateAttributeTypeId, FlowTypeId, TargetType, Multiplier,
                TransferToStratumId, TransferToSecondaryStratumId, TransferToTertiaryStratumId, TransferToStateClassId, TransferToMinimumAge);

            return p;
        }

        public static string GetAutoGeneratedGroupName(string typeName)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0} {1}", typeName, Strings.AUTO_COLUMN_SUFFIX);
        }
    }
}
