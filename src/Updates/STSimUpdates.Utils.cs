// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.IO;
using System.Data;
using System.Globalization;
using SyncroSim.Core;
using System.Diagnostics;

namespace SyncroSim.STSim
{
    internal partial class STSimUpdates
    {
        private static string GetDatasheetOutputFolder(DataStore store, int scenarioId, string datasheetName)
        {
            var baseFolder = GetCurrentOutputFolderBase(store);
            return Path.Combine(baseFolder, string.Format(CultureInfo.InvariantCulture, "Scenario-{0}", scenarioId), datasheetName);
        }

        private static string GetLegacyOutputFolder(DataStore store, int scenarioId)
        {
            var baseFolder = GetCurrentOutputFolderBase(store);
            return Path.Combine(baseFolder, string.Format(CultureInfo.InvariantCulture, "Scenario-{0}", scenarioId), "Spatial");
        }

        private static string GetCurrentOutputFolderBase(DataStore store)
        {
            const string FOLDER_NAME = "OutputFolderName";

            DataTable dt = null;

            if (store.TableExists("SSim_Files"))
            {
                dt = store.CreateDataTable("SSim_Files");
            }
            else if (store.TableExists("SSim_File"))
            {
                dt = store.CreateDataTable("SSim_File");
            }
            else if (store.TableExists("SSim_SysFolder"))
            {
                dt = store.CreateDataTable("SSim_SysFolder");
            }
            else
            {
                dt = store.CreateDataTable("core_SysFolder");
            }

            DataRow dr = null;

            if (dt.Rows.Count == 1)
            {
                dr = dt.Rows[0];
            }

            Debug.Assert(dt.Rows.Count == 1 || dt.Rows.Count == 0);

            string p = null;

            if ((dr != null) && (dr[FOLDER_NAME] != DBNull.Value))
            {
                p = Convert.ToString(dr[FOLDER_NAME], CultureInfo.InvariantCulture);
            }
            else
            {
                p = store.DataStoreConnection.ConnectionString + ".output";
            }

            return p;
        }

        private static string ExpandMapCriteriaFrom1x(string criteria)
        {
            Func<string, string> GetDataSheetName = (string c) =>
            {
                if (c == "tg")
                {
                    return "STSim_TransitionGroup";
                }
                else if (c == "tgap")
                {
                    return "STSim_TransitionGroup";
                }
                else if (c == "sa")
                {
                    return "STSim_StateAttributeType";
                }
                else if (c == "ta")
                {
                    return "STSim_TransitionAttributeType";
                }
                else if (c == "flo")
                {
                    return "SF_FlowType";
                }
                else if (c == "stk")
                {
                    return "SF_StockType";
                }
                else if (c == "stkg")
                {
                    return "SF_StockGroup";
                }
                else if (c == "flog")
                {
                    return "SF_FlowGroup";
                }
                else
                {
                    //This will eventually be discarded by the map criteria control
                    //but the setting will not be ported to 2.0

                    Debug.Assert(false);
                    return "Unknown";
                }
            };

            string[] parts = criteria.Split('-');
            Debug.Assert(parts.Length <= 2);

            if (parts.Length == 1)
            {
                return criteria;
            }
            else if (parts.Length == 2)
            {
                //tg-342-itemid-342-itemsrc-STSim_TransitionGroup
                var newcr = string.Format(CultureInfo.InvariantCulture, "{0}-itemid-{1}-itemsrc-{2}", criteria, parts[1], GetDataSheetName(parts[0]));
                return newcr;
            }
            else
            {
                Debug.Assert(false);
                return criteria;
            }
        }

        private static bool TableHasColumn(string tableName, string columnName, DataStore store)
        {
            if (!store.TableExists(tableName))
            {
                return false;
            }
            else
            {
                string query = string.Format(CultureInfo.InvariantCulture, "SELECT * FROM [{0}] LIMIT 1", tableName);
                DataTable dt = store.CreateDataTableFromQuery(query, "Table");
                return dt.Columns.Contains(columnName);
            }
        }
    }
}
