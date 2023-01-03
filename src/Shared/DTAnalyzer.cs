// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Globalization;
using System.Collections.Generic;
using SyncroSim.Core;
using System.Diagnostics;

namespace SyncroSim.STSim
{
    internal class DTAnalyzer
    {
        private DataTable m_DataSource;
        private Project m_Project;
        private Dictionary<string, DataRow> m_RowLookup = new Dictionary<string, DataRow>();
        private Dictionary<int, bool> m_StrataWithData = new Dictionary<int, bool>();

        public DTAnalyzer(DataTable dataSource, Project project)
        {
            this.m_DataSource = dataSource;
            this.m_Project = project;

            foreach (DataRow dr in this.m_DataSource.Rows)
            {
                if (dr.RowState == DataRowState.Deleted)
                {
                    continue;
                }

                int? StratumId = null;
                int StateClassId = Convert.ToInt32(dr[Strings.DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME], CultureInfo.InvariantCulture);

                if (dr[Strings.DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME] != DBNull.Value)
                {
                    StratumId = Convert.ToInt32(dr[Strings.DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME], CultureInfo.InvariantCulture);
                }

                int StratumKey = CreateStratumLookupKey(StratumId);
                string StateClassKey = CreateStateClassLookupKey(StratumId, StateClassId);

                this.m_RowLookup.Add(StateClassKey, dr);

                if (!this.m_StrataWithData.ContainsKey(StratumKey))
                {
                    this.m_StrataWithData.Add(StratumKey, true);
                }
            }
        }

        public static void GetDTFieldValues(DataRow dr, ref int? stratumIdSource, ref int stateClassIdSource, ref int? stratumIdDest, ref int? stateClassIdDest)
        {
            GetCoreFieldValues(dr, ref stratumIdSource, ref stateClassIdSource, ref stratumIdDest, ref stateClassIdDest, true);
        }

        public static void GetPTFieldValues(DataRow dr, ref int? stratumIdSource, ref int stateClassIdSource, ref int? stratumIdDest, ref int? stateClassIdDest)
        {
            GetCoreFieldValues(dr, ref stratumIdSource, ref stateClassIdSource, ref stratumIdDest, ref stateClassIdDest, false);
        }

        public bool CanResolveStateClass(int? stratumIdSource, int? stratumIdDest, int stateClassId)
        {
            var tempVar = new int?();
            return this.ResolveStateClassStratum(stratumIdSource, stratumIdDest, stateClassId, ref tempVar);
        }

        public bool ResolveStateClassStratum(int? stratumIdSource, int? stratumIdDest, int stateClassId, ref int? outStratumId)
        {
            DataRow dr = null;
            outStratumId = null;

            if (stratumIdSource.HasValue)
            {
                if (stratumIdDest.HasValue)
                {
                    dr = this.GetStateClassRow(stratumIdDest.Value, stateClassId);
                }
                else
                {
                    dr = this.GetStateClassRow(stratumIdSource.Value, stateClassId);
                }

                if (dr == null)
                {
                    dr = this.GetStateClassRow(null, stateClassId);
                }
            }
            else
            {
                if (stratumIdDest.HasValue)
                {
                    dr = this.GetStateClassRow(stratumIdDest.Value, stateClassId);
                }
                else
                {
                    dr = this.GetStateClassRow(null, stateClassId);
                }
            }

            if (dr == null)
            {
                return false;
            }

            if (dr[Strings.DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME] != DBNull.Value)
            {
                outStratumId = Convert.ToInt32(dr[Strings.DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME], CultureInfo.InvariantCulture);
            }

            return true;
        }

        public DataRow GetStateClassRow(int? stratumId, int stateClassId)
        {
            string k = CreateStateClassLookupKey(stratumId, stateClassId);

            if (this.m_RowLookup.ContainsKey(k))
            {
                return this.m_RowLookup[k];
            }
            else
            {
                return null;
            }
        }

        public bool StateClassExists(int? stratumId, int stateClassId)
        {
            return (this.GetStateClassRow(stratumId, stateClassId) != null);
        }

        public bool StratumHasData(int? stratumId)
        {
            int StratumKey = CreateStratumLookupKey(stratumId);
            return this.m_StrataWithData.ContainsKey(StratumKey);
        }

        public void ThrowDataException(int stateClassId, bool isDestination)
        {
            string psl = null;
            string ssl = null;
            string tsl = null;
            string StateClassName = "NULL";
            DataSheet StateClassDataSheet = this.m_Project.GetDataSheet(Strings.DATASHEET_STATECLASS_NAME);
            string Location = "From";

            if (isDestination)
            {
                Location = "To";
            }

            TerminologyUtilities.GetStratumLabelTerminology(this.m_Project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME), ref psl, ref ssl, ref tsl);
            StateClassName = Convert.ToString(DataTableUtilities.GetTableValue(StateClassDataSheet.GetData(), StateClassDataSheet.ValueMember, stateClassId, Strings.DATASHEET_NAME_COLUMN_NAME), CultureInfo.InvariantCulture);
            string msg = string.Format(CultureInfo.InvariantCulture, "The state class '{0}' could not be located in '{1} {2}'.", StateClassName, Location, psl);

            throw new DataException(msg);
        }

        private static int CreateStratumLookupKey(int? stratumId)
        {
            if (stratumId.HasValue)
            {
                return stratumId.Value;
            }
            else
            {
                return 0;
            }
        }

        public static string CreateStateClassLookupKey(int? stratumId, int stateClassId)
        {
            string k1 = "NULL";
            string k2 = stateClassId.ToString(CultureInfo.InvariantCulture);

            if (stratumId.HasValue)
            {
                Debug.Assert(Convert.ToInt32(stratumId.Value, CultureInfo.InvariantCulture) > 0);
                k1 = stratumId.Value.ToString(CultureInfo.InvariantCulture);
            }

            return string.Format(CultureInfo.InvariantCulture, "{0}-{1}", k1, k2);
        }

        private static void GetCoreFieldValues(
            DataRow dr, ref int? stratumIdSource, ref int stateClassIdSource, ref int? stratumIdDest, 
            ref int? stateClassIdDest, bool deterministic)
        {
            string stsrc = Strings.DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME;
            string scsrc = Strings.DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME;
            string stdst = Strings.DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME;
            string scdst = Strings.DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME;

            if (!deterministic)
            {
                stsrc = Strings.DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME;
                scsrc = Strings.DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME;
                stdst = Strings.DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME;
                scdst = Strings.DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME;
            }

            stratumIdSource = null;
            stateClassIdSource = Convert.ToInt32(dr[scsrc], CultureInfo.InvariantCulture);
            stratumIdDest = null;
            stateClassIdDest = null;

            Debug.Assert(stateClassIdSource > 0);

            if (dr[stsrc] != DBNull.Value)
            {
                stratumIdSource = Convert.ToInt32(dr[stsrc], CultureInfo.InvariantCulture);
                Debug.Assert(stratumIdSource.Value > 0);
            }

            if (dr[stdst] != DBNull.Value)
            {
                stratumIdDest = Convert.ToInt32(dr[stdst], CultureInfo.InvariantCulture);
                Debug.Assert(stratumIdDest.Value > 0);
            }

            if (dr[scdst] != DBNull.Value)
            {
                stateClassIdDest = Convert.ToInt32(dr[scdst], CultureInfo.InvariantCulture);
                Debug.Assert(stateClassIdDest.Value > 0);
            }
        }

        public static bool IsValidLocation(object proposedLocation)
        {
            if (proposedLocation == null)
            {
                return false;
            }

            string Location = Convert.ToString(proposedLocation, CultureInfo.InvariantCulture);

            if (string.IsNullOrEmpty(Location))
            {
                return false;
            }

            string LocUpper = Location.ToUpper(CultureInfo.InvariantCulture).Trim();

            if (string.IsNullOrEmpty(LocUpper))
            {
                return false;
            }

            if (LocUpper.Length < 2)
            {
                return false;
            }

            string CharPart = LocUpper.Substring(0, 1);
            string NumPart = LocUpper.Substring(1, LocUpper.Length - 1);

            if (string.IsNullOrEmpty(CharPart) | string.IsNullOrEmpty(NumPart))
            {
                return false;
            }

            if (CharPart[0] < 'A' || CharPart[0] > 'Z')
            {
                return false;
            }

            int n = 0;
            if (!int.TryParse(NumPart, out n))
            {
                return false;
            }

            if (n <= 0 || n > 256)
            {
                return false;
            }

            return true;
        }
    }
}
