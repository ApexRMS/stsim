// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using System.Collections.Specialized;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    public partial class STSimTransformer
    {
        private string m_ExtProcExeName;
        private string m_ExtProcScriptName;
        private string m_ExtProcArguments;
        private readonly Dictionary<int, bool> m_ExtProcBeforeIterations = new Dictionary<int, bool>();
        private readonly Dictionary<int, bool> m_ExtProcAfterIterations = new Dictionary<int, bool>();
        private readonly Dictionary<int, bool> m_ExtProcBeforeTimesteps = new Dictionary<int, bool>();
        private readonly Dictionary<int, bool> m_ExtProcAfterTimesteps = new Dictionary<int, bool>();

        private void ExtProcInitialize()
        {
            DataSheet ds = this.ResultScenario.GetDataSheet(Strings.EXTERNAL_DATASHEET_NAME);
            DataRow dr = ds.GetDataRow();

            if (dr == null)
            {
                return;
            }

            this.m_ExtProcExeName = ExtProcGetDataStr(dr, Strings.EXTERNAL_DATASHEET_EXE_COLUMN_NAME);
            this.m_ExtProcScriptName = ExtProcGetDataStr(dr, Strings.EXTERNAL_DATASHEET_SCRIPT_COLUMN_NAME);
            this.m_ExtProcArguments = ExtProcGetDataStr(dr, Strings.EXTERNAL_DATASHEET_ARGS_COLUMN_NAME);

            string bi = ExtProcGetDataStr(dr, Strings.EXTERNAL_DATASHEET_BI_COLUMN_NAME);
            string ai = ExtProcGetDataStr(dr, Strings.EXTERNAL_DATASHEET_AI_COLUMN_NAME);
            string bt = ExtProcGetDataStr(dr, Strings.EXTERNAL_DATASHEET_BT_COLUMN_NAME);
            string at = ExtProcGetDataStr(dr, Strings.EXTERNAL_DATASHEET_AT_COLUMN_NAME);

            ExtProcFillDictionary(this.m_ExtProcBeforeIterations, bi);
            ExtProcFillDictionary(this.m_ExtProcAfterIterations, ai);
            ExtProcFillDictionary(this.m_ExtProcBeforeTimesteps, bt);
            ExtProcFillDictionary(this.m_ExtProcAfterTimesteps, at);

            if (this.m_ExtProcBeforeIterations.Count == 0 &&
                this.m_ExtProcAfterIterations.Count == 0 &&
                this.m_ExtProcBeforeTimesteps.Count == 0 &&
                this.m_ExtProcAfterTimesteps.Count == 0)
            {
                throw new ArgumentException("Cannot run external program with no iteration or timesteps specified.");
            }
        }

        private void ExtProcCallBeforeIteration(int iteration)
        {
            if (this.m_ExtProcBeforeIterations.ContainsKey(iteration))
            {
                StringDictionary Environment = new StringDictionary
                {
                    { Constants.EXTPROC_ENVIRONMENT_BEFORE_ITERATION, iteration.ToString(CultureInfo.InvariantCulture) }
                };

                Debug.Assert(false); //Force Save !!!
                this.ExternalTransform(this.m_ExtProcExeName, this.m_ExtProcScriptName, this.m_ExtProcArguments, false, Environment);
            }
        }

        private void ExtProcCallAfterIteration(int iteration)
        {
            if (this.m_ExtProcAfterIterations.ContainsKey(iteration))
            {
                StringDictionary Environment = new StringDictionary
                {
                    { Constants.EXTPROC_ENVIRONMENT_AFTER_ITERATION, iteration.ToString(CultureInfo.InvariantCulture) }
                };

                Debug.Assert(false); //Force Save !!!
                this.ExternalTransform(this.m_ExtProcExeName, this.m_ExtProcScriptName, this.m_ExtProcArguments, false, Environment);
            }
        }

        private void ExtProcCallBeforeTimestep(int iteration, int timestep)
        {
            if (this.m_ExtProcBeforeTimesteps.ContainsKey(timestep))
            {
                StringDictionary Environment = new StringDictionary
                {
                    { Constants.EXTPROC_ENVIRONMENT_BEFORE_ITERATION, iteration.ToString(CultureInfo.InvariantCulture) },
                    { Constants.EXTPROC_ENVIRONMENT_BEFORE_TIMESTEP, timestep.ToString(CultureInfo.InvariantCulture) }
                };

                Debug.Assert(false); //Force Save !!!
                this.ExternalTransform(this.m_ExtProcExeName, this.m_ExtProcScriptName, this.m_ExtProcArguments, false, Environment);
            }
        }

        private void ExtProcCallAfterTimestep(int iteration, int timestep)
        {
            if (this.m_ExtProcAfterTimesteps.ContainsKey(timestep))
            {
                StringDictionary Environment = new StringDictionary
                {
                    { Constants.EXTPROC_ENVIRONMENT_AFTER_ITERATION, iteration.ToString(CultureInfo.InvariantCulture) },
                    { Constants.EXTPROC_ENVIRONMENT_AFTER_TIMESTEP, timestep.ToString(CultureInfo.InvariantCulture) }
                };

                Debug.Assert(false); //Force Save !!!
                this.ExternalTransform(this.m_ExtProcExeName, this.m_ExtProcScriptName, this.m_ExtProcArguments, false, Environment);
            }
        }

        private static void ExtProcFillDictionary(Dictionary<int, bool> d, string values)
        {
            Debug.Assert(d.Count == 0);

            if (values == null)
            {
                return;
            }

            string[] split = values.Split(',');

            foreach (string s in split)
            {
                if (s.Contains("-"))
                {
                    ExtProcTryAddRange(s, d);
                }
                else
                {
                    ExtProcTryAddSingle(s, d);
                }
            }
        }

        private static void ExtProcTryAddSingle(string s, Dictionary<int, bool> d)
        {
            string t = s.Trim();

            if (!int.TryParse(t, out int i))
            {
                ExtProcThrowParseException(t);
            }

            if (!d.ContainsKey(i))
            {
                d.Add(i, true);
            }
        }

        private static void ExtProcTryAddRange(string s, Dictionary<int, bool> d)
        {
            string t = s.Trim();
            string[] split = t.Split('-');

            if (split.Length != 2)
            {
                ExtProcThrowParseException(t);
            }


            if (!int.TryParse(split[0], out int t1))
            {
                ExtProcThrowParseException(t);
            }

            if (!int.TryParse(split[1], out int t2))
            {
                ExtProcThrowParseException(t);
            }

            if (t1 > t2)
            {
                int it = t1;

                t1 = t2;
                t2 = it;
            }

            for (int i = t1; i <= t2; i++)
            {
                if (!d.ContainsKey(i))
                {
                    d.Add(i, true);
                }
            }
        }

        private static string ExtProcGetDataStr(DataRow dr, string columnName)
        {
            return ExtProcGetDataStr(dr[columnName]);
        }

        private static string ExtProcGetDataStr(object value)
        {
            if ((object.ReferenceEquals(value, DBNull.Value)))
            {
                return null;
            }
            else
            {
                return Convert.ToString(value);
            }
        }

        private static void ExtProcThrowParseException(string value)
        {
            throw new ArgumentException("The iteration and/or timestep values are not valid: {0}", value);
        }

        private void ExtProcOnExternalDataReady(DataSheet dataSheet)
        {
            base.OnExternalDataReady(dataSheet);

            if (dataSheet.Name == Strings.DATASHEET_PT_NAME)
            {
                this.m_Transitions.Clear();
                this.FillProbabilisticTransitionsCollection();
                this.m_TransitionMap = new TransitionMap(this.ResultScenario, this.m_Transitions);
            }
            else if (dataSheet.Name == Strings.DATASHEET_TRANSITION_TARGET_NAME)
            {
                this.m_TransitionTargets.Clear();
                this.FillTransitionTargetCollection();
                this.InitializeTransitionTargetDistributionValues();
                this.InitializeTransitionTargetPrioritizations();
                this.m_TransitionTargetMap = new TransitionTargetMap(this.ResultScenario, this.m_TransitionTargets);
            }
            else if (dataSheet.Name == Strings.DATASHEET_TRANSITION_MULTIPLIER_VALUE_NAME)
            {
                this.m_TransitionMultiplierValues.Clear();
                this.FillTransitionMultiplierValueCollection();
                this.InitializeTransitionMultiplierDistributionValues();

                foreach (TransitionMultiplierType tmt in this.m_TransitionMultiplierTypes)
                {
                    tmt.ClearMultiplierValueMap();
                }

                foreach (TransitionMultiplierValue sm in this.m_TransitionMultiplierValues)
                {
                    TransitionMultiplierType mt = this.GetTransitionMultiplierType(sm.TransitionMultiplierTypeId);
                    mt.AddTransitionMultiplierValue(sm);
                }

                foreach (TransitionMultiplierType tmt in this.m_TransitionMultiplierTypes)
                {
                    tmt.CreateMultiplierValueMap();
                }
            }
            else if (dataSheet.Name == Strings.DATASHEET_TRANSITION_SPATIAL_MULTIPLIER_NAME)
            {
                if (this.m_IsSpatial)
                {
                    this.m_TransitionSpatialMultipliers.Clear();
                    this.m_TransitionSpatialMultiplierRasters.Clear();
                    this.FillTransitionSpatialMultiplierCollection();

                    foreach (TransitionMultiplierType tmt in this.m_TransitionMultiplierTypes)
                    {
                        tmt.ClearSpatialMultiplierMap();
                    }

                    foreach (TransitionSpatialMultiplier sm in this.m_TransitionSpatialMultipliers)
                    {
                        TransitionMultiplierType mt = this.GetTransitionMultiplierType(sm.TransitionMultiplierTypeId);
                        mt.AddTransitionSpatialMultiplier(sm);
                    }

                    foreach (TransitionMultiplierType tmt in this.m_TransitionMultiplierTypes)
                    {
                        tmt.CreateSpatialMultiplierMap();
                    }
                }
            }
            else if (dataSheet.Name == Strings.DATASHEET_TRANSITION_SPATIAL_INITIATION_MULTIPLIER_NAME)
            {
                if (this.m_IsSpatial)
                {
                    this.m_TransitionSpatialInitiationMultipliers.Clear();
                    this.m_TransitionSpatialInitiationMultiplierRasters.Clear();
                    this.FillTransitionSpatialInitiationMultiplierCollection();

                    foreach (TransitionMultiplierType tmt in this.m_TransitionMultiplierTypes)
                    {
                        tmt.ClearSpatialInitiationMultiplierMap();
                    }

                    foreach (TransitionSpatialInitiationMultiplier sm in this.m_TransitionSpatialInitiationMultipliers)
                    {
                        TransitionMultiplierType mt = this.GetTransitionMultiplierType(sm.TransitionMultiplierTypeId);
                        mt.AddTransitionSpatialInitiationMultiplier(sm);
                    }

                    foreach (TransitionMultiplierType tmt in this.m_TransitionMultiplierTypes)
                    {
                        tmt.CreateSpatialInitiationMultiplierMap();
                    }
                }
            }
            else if (dataSheet.Name == Strings.DATASHEET_TRANSITION_ORDER_NAME)
            {
                this.m_TransitionOrders.Clear();
                this.FillTransitionOrderCollection();
                this.m_TransitionOrderMap = new TransitionOrderMap(this.m_TransitionOrders);
            }
            else if (dataSheet.Name == Strings.DATASHEET_STATE_ATTRIBUTE_VALUE_NAME)
            {
                this.m_StateAttributeValues.Clear();
                this.FillStateAttributeValueCollection();
                this.m_StateAttributeTypeIds = null;
                this.m_StateAttributeValueMap = null;
                this.InitializeStateAttributes();
            }
            else if (dataSheet.Name == Strings.DATASHEET_TRANSITION_ATTRIBUTE_VALUE_NAME)
            {
                this.m_TransitionAttributeValues.Clear();
                this.FillTransitionAttributeValueCollection();
                this.m_TransitionAttributeValueMap = null;
                this.m_TransitionAttributeTypeIds = null;
                this.InitializeTransitionAttributes();
            }
            else if (dataSheet.Name == Strings.DATASHEET_TRANSITION_ATTRIBUTE_TARGET_NAME)
            {
                this.m_TransitionAttributeTargets.Clear();
                this.FillTransitionAttributeTargetCollection();
                this.InitializeTransitionAttributeTargetDistributionValues();
                this.InitializeTransitionAttributeTargetPrioritizations();
                this.m_TransitionAttributeTargetMap = new TransitionAttributeTargetMap(this.ResultScenario, this.m_TransitionAttributeTargets);
            }
            else
            {
                string msg = string.Format(CultureInfo.InvariantCulture, "External data is not supported for: {0}", dataSheet.Name);
                throw new TransformerFailedException(msg);
            }
        }
    }
}
