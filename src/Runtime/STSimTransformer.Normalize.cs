// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Globalization;
using SyncroSim.Core;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    public partial class STSimTransformer
    {
        /// <summary>
        /// Normalizes the run control data feeds
        /// </summary>
        /// <remarks></remarks>
        private void NormalizeRunControl()
        {
            DataSheet ds = this.ResultScenario.GetDataSheet(Strings.DATASHEET_RUN_CONTROL_NAME);
            DataRow dr = ds.GetDataRow();

            if (dr == null)
            {
                dr = ds.GetData().NewRow();
                ds.GetData().Rows.Add(dr);
            }

            if (dr[Strings.RUN_CONTROL_MIN_ITERATION_COLUMN_NAME] == DBNull.Value)
            {
                dr[Strings.RUN_CONTROL_MIN_ITERATION_COLUMN_NAME] = 1;
            }

            if (dr[Strings.RUN_CONTROL_MAX_ITERATION_COLUMN_NAME] == DBNull.Value)
            {
                dr[Strings.RUN_CONTROL_MAX_ITERATION_COLUMN_NAME] = 1;
                this.RecordStatus(StatusType.Warning, MessageStrings.STATUS_USING_DEFAULT_MAX_ITERATIONS_WARNING);
            }

            if (dr[Strings.RUN_CONTROL_MIN_TIMESTEP_COLUMN_NAME] == DBNull.Value)
            {
                dr[Strings.RUN_CONTROL_MIN_TIMESTEP_COLUMN_NAME] = 0;
            }

            if (dr[Strings.RUN_CONTROL_MAX_TIMESTEP_COLUMN_NAME] == DBNull.Value)
            {
                dr[Strings.RUN_CONTROL_MAX_TIMESTEP_COLUMN_NAME] = 10;

                string msg = string.Format(CultureInfo.InvariantCulture, 
                    MessageStrings.STATUS_USING_DEFAULT_MAX_TIMESTEP_WARNING, this.m_TimestepUnitsLower);

                this.RecordStatus(StatusType.Warning, msg);
            }
        }

        /// <summary>
        /// Normalizes the tabular output options data feed
        /// </summary>
        /// <remarks></remarks>
        private void NormalizeTabularOutputOptions()
        {
            DataSheet dsrc = this.ResultScenario.GetDataSheet(Strings.DATASHEET_RUN_CONTROL_NAME);
            DataRow drrc = dsrc.GetDataRow();
            int MaxTimestep = Convert.ToInt32(drrc[Strings.RUN_CONTROL_MAX_TIMESTEP_COLUMN_NAME], CultureInfo.InvariantCulture);
            DataSheet dsoo = this.ResultScenario.GetDataSheet(Strings.DATASHEET_OO_TABULAR_NAME);
            DataRow droo = dsoo.GetDataRow();

            if (droo == null)
            {
                droo = dsoo.GetData().NewRow();
                dsoo.GetData().Rows.Add(droo);

                DataTableUtilities.SetRowValue(droo, Strings.DATASHEET_OO_SUMMARY_OUTPUT_SC_COLUMN_NAME, Booleans.BoolToInt(true));
                DataTableUtilities.SetRowValue(droo, Strings.DATASHEET_OO_SUMMARY_OUTPUT_TR_COLUMN_NAME, Booleans.BoolToInt(true));
                DataTableUtilities.SetRowValue(droo, Strings.DATASHEET_OO_SUMMARY_OUTPUT_SC_AGES_COLUMN_NAME, Booleans.BoolToInt(true));
                DataTableUtilities.SetRowValue(droo, Strings.DATASHEET_OO_SUMMARY_OUTPUT_TR_AGES_COLUMN_NAME, Booleans.BoolToInt(true));
                DataTableUtilities.SetRowValue(droo, Strings.DATASHEET_OO_SUMMARY_OUTPUT_SC_TIMESTEPS_COLUMN_NAME, 1);
                DataTableUtilities.SetRowValue(droo, Strings.DATASHEET_OO_SUMMARY_OUTPUT_TR_TIMESTEPS_COLUMN_NAME, 1);

                this.RecordStatus(StatusType.Information, MessageStrings.STATUS_NO_OUTPUT_OPTIONS_WARNIING);
            }

            this.ValidateTimesteps(droo, Strings.DATASHEET_OO_SUMMARY_OUTPUT_SC_COLUMN_NAME, Strings.DATASHEET_OO_SUMMARY_OUTPUT_SC_TIMESTEPS_COLUMN_NAME, "Summary state classes", MaxTimestep);
            this.ValidateTimesteps(droo, Strings.DATASHEET_OO_SUMMARY_OUTPUT_TR_COLUMN_NAME, Strings.DATASHEET_OO_SUMMARY_OUTPUT_TR_TIMESTEPS_COLUMN_NAME, "Summary transitions", MaxTimestep);
            this.ValidateTimesteps(droo, Strings.DATASHEET_OO_SUMMARY_OUTPUT_TRSC_COLUMN_NAME, Strings.DATASHEET_OO_SUMMARY_OUTPUT_TRSC_TIMESTEPS_COLUMN_NAME, "Summary transitions by state class", MaxTimestep);
            this.ValidateTimesteps(droo, Strings.DATASHEET_OO_SUMMARY_OUTPUT_SA_COLUMN_NAME, Strings.DATASHEET_OO_SUMMARY_OUTPUT_SA_TIMESTEPS_COLUMN_NAME, "Summary state attributes", MaxTimestep);
            this.ValidateTimesteps(droo, Strings.DATASHEET_OO_SUMMARY_OUTPUT_TA_COLUMN_NAME, Strings.DATASHEET_OO_SUMMARY_OUTPUT_TA_TIMESTEPS_COLUMN_NAME, "Summary transition attributes", MaxTimestep);
            this.ValidateTimesteps(droo, Strings.DATASHEET_OO_SUMMARY_OUTPUT_EV_COLUMN_NAME, Strings.DATASHEET_OO_SUMMARY_OUTPUT_EV_TIMESTEPS_COLUMN_NAME, "Summary external variables", MaxTimestep);
            this.ValidateTimesteps(droo, Strings.DATASHEET_OO_SUMMARY_OUTPUT_TST_COLUMN_NAME, Strings.DATASHEET_OO_SUMMARY_OUTPUT_TST_TIMESTEPS_COLUMN_NAME, "Summary TST", MaxTimestep);
        }

        /// <summary>
        /// Normalizes the spatial output options data feed
        /// </summary>
        /// <remarks></remarks>
        private void NormalizeSpatialOutputOptions()
        {
            if (!this.m_IsSpatial)
            {
                return;
            }

            DataSheet dsrc = this.ResultScenario.GetDataSheet(Strings.DATASHEET_RUN_CONTROL_NAME);
            DataRow drrc = dsrc.GetDataRow();
            int MaxTimestep = Convert.ToInt32(drrc[Strings.RUN_CONTROL_MAX_TIMESTEP_COLUMN_NAME], CultureInfo.InvariantCulture);
            DataSheet dsoo = this.ResultScenario.GetDataSheet(Strings.DATASHEET_OO_SPATIAL_NAME);
            DataRow droo = dsoo.GetDataRow();

            if (droo != null)
            {
                this.ValidateTimesteps(droo, Strings.DATASHEET_OO_SPATIAL_RASTER_OUTPUT_SC_COLUMN_NAME, Strings.DATASHEET_OO_SPATIAL_RASTER_OUTPUT_SC_TIMESTEPS_COLUMN_NAME, "Raster state classes", MaxTimestep);
                this.ValidateTimesteps(droo, Strings.DATASHEET_OO_SPATIAL_RASTER_OUTPUT_AGE_COLUMN_NAME, Strings.DATASHEET_OO_SPATIAL_RASTER_OUTPUT_AGE_TIMESTEPS_COLUMN_NAME, "Raster ages", MaxTimestep);
                this.ValidateTimesteps(droo, Strings.DATASHEET_OO_SPATIAL_RASTER_OUTPUT_ST_COLUMN_NAME, Strings.DATASHEET_OO_SPATIAL_RASTER_OUTPUT_ST_TIMESTEPS_COLUMN_NAME, "Raster stratum", MaxTimestep);
                this.ValidateTimesteps(droo, Strings.DATASHEET_OO_SPATIAL_RASTER_OUTPUT_TR_COLUMN_NAME, Strings.DATASHEET_OO_SPATIAL_RASTER_OUTPUT_TR_TIMESTEPS_COLUMN_NAME, "Raster transitions", MaxTimestep);
                this.ValidateTimesteps(droo, Strings.DATASHEET_OO_SPATIAL_RASTER_OUTPUT_TE_COLUMN_NAME, Strings.DATASHEET_OO_SPATIAL_RASTER_OUTPUT_TE_TIMESTEPS_COLUMN_NAME, "Raster transition events", MaxTimestep);
                this.ValidateTimesteps(droo, Strings.DATASHEET_OO_SPATIAL_RASTER_OUTPUT_TST_COLUMN_NAME, Strings.DATASHEET_OO_SPATIAL_RASTER_OUTPUT_TST_TIMESTEPS_COLUMN_NAME, "Raster Time-since-transition", MaxTimestep);
                this.ValidateTimesteps(droo, Strings.DATASHEET_OO_SPATIAL_RASTER_OUTPUT_SA_COLUMN_NAME, Strings.DATASHEET_OO_SPATIAL_RASTER_OUTPUT_SA_TIMESTEPS_COLUMN_NAME, "Raster state attributes", MaxTimestep);
                this.ValidateTimesteps(droo, Strings.DATASHEET_OO_SPATIAL_RASTER_OUTPUT_TA_COLUMN_NAME, Strings.DATASHEET_OO_SPATIAL_RASTER_OUTPUT_TA_TIMESTEPS_COLUMN_NAME, "Raster transition attributes", MaxTimestep);
            }
        }

        /// <summary>
        /// Normalizes the spatial average output options data feed
        /// </summary>
        /// <remarks></remarks>
        private void NormalizeSpatialAverageOutputOptions()
        {
            if (!this.m_IsSpatial)
            {
                return;
            }

            DataSheet dsrc = this.ResultScenario.GetDataSheet(Strings.DATASHEET_RUN_CONTROL_NAME);
            DataRow drrc = dsrc.GetDataRow();
            int MaxTimestep = Convert.ToInt32(drrc[Strings.RUN_CONTROL_MAX_TIMESTEP_COLUMN_NAME], CultureInfo.InvariantCulture);
            DataSheet dsoo = this.ResultScenario.GetDataSheet(Strings.DATASHEET_OO_SPATIAL_AVERAGE_NAME);
            DataRow droo = dsoo.GetDataRow();

            if (droo != null)
            {
                this.ValidateTimesteps(droo, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_SC_COLUMN_NAME, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_SC_TIMESTEPS_COLUMN_NAME, "Average raster state classes", MaxTimestep);
                this.ValidateTimesteps(droo, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_AGE_COLUMN_NAME, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_AGE_TIMESTEPS_COLUMN_NAME, "Average raster ages", MaxTimestep);
                this.ValidateTimesteps(droo, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_ST_COLUMN_NAME, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_ST_TIMESTEPS_COLUMN_NAME, "Average raster strata", MaxTimestep);
                this.ValidateTimesteps(droo, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_TP_COLUMN_NAME, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_TP_TIMESTEPS_COLUMN_NAME, "Average Raster transition probability", MaxTimestep);
                this.ValidateTimesteps(droo, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_TST_COLUMN_NAME, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_TST_TIMESTEPS_COLUMN_NAME, "Average raster time-since-transition", MaxTimestep);
                this.ValidateTimesteps(droo, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_SA_COLUMN_NAME, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_SA_TIMESTEPS_COLUMN_NAME, "Average Raster state attributes", MaxTimestep);
                this.ValidateTimesteps(droo, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_TA_COLUMN_NAME, Strings.DATASHEET_OO_SPATIAL_AVG_RASTER_OUTPUT_TA_TIMESTEPS_COLUMN_NAME, "Average Raster transition attributes", MaxTimestep);
            }
        }

        /// <summary>
        /// Normalizes the Initial conditions
        /// </summary>
        /// <remarks></remarks>
        private void NormalizeInitialConditions()
        {
            if (this.IsSpatial)
            {
                // See if we're going to run Spatially based on ONLY on Non spatial initial conditions
                bool NoSpatialIC = !(this.ResultScenario.GetDataSheet(Strings.DATASHEET_SPIC_NAME).HasData());

                this.NormalizeNonSpatialInitialConditions(false);
                this.FillInitialConditionsDistributionCollectionAndMap();

                // See if we're going to run Spatially based on Non spatial initial conditions
                if (NoSpatialIC)
                {
                    // If no Spatial IC records for this Scenario, we're using Non spatial initial conditions.
                    this.RecordStatus(StatusType.Information, MessageStrings.STATUS_SPATIAL_RUN_USING_NONSPATIAL_IC);
                    this.CreateSpatialICFromNonSpatialIC();
                }
                else
                {
                    this.FillInitialConditionsSpatialCollectionAndMap();
                    this.CreateSpatialICFromCombinedIC();
                }
            }
            else
            {
                this.NormalizeNonSpatialInitialConditions(true);
            }
        }

        /// <summary>
        /// Normalizes the Non Spatial initial conditions data sheets
        /// </summary>
        /// <remarks></remarks>
        private void NormalizeNonSpatialInitialConditions(bool verboseStatus)
        {
            DataSheet ICNS_Sheet = this.ResultScenario.GetDataSheet(Strings.DATASHEET_NSIC_NAME);
            DataRow ICNS_Row = ICNS_Sheet.GetDataRow();
            DataTable ICNSDist_Table = this.ResultScenario.GetDataSheet(Strings.DATASHEET_NSIC_DISTRIBUTION_NAME).GetData();
            DataTable DeterministicTransitions_Table = this.ResultScenario.GetDataSheet(Strings.DATASHEET_DT_NAME).GetData();
            DataSheet Strata_Sheet = this.ResultScenario.Project.GetDataSheet(Strings.DATASHEET_STRATA_NAME);

            //Add a row if necessary 

            if (ICNS_Row == null)
            {
                ICNS_Row = ICNS_Sheet.GetData().NewRow();
                ICNS_Sheet.GetData().Rows.Add(ICNS_Row);
            }

            //Total Amount

            if (ICNS_Row[Strings.DATASHEET_NSIC_TOTAL_AMOUNT_COLUMN_NAME] == DBNull.Value)
            {
                ICNS_Row[Strings.DATASHEET_NSIC_TOTAL_AMOUNT_COLUMN_NAME] = Constants.DATASHEET_RUN_CONTROL_TOTAL_AMOUNT_DEFAULT;

                if (verboseStatus)
                {
                    this.RecordStatus(StatusType.Warning, MessageStrings.STATUS_USING_DEFAULT_TOTAL_AMOUNT_WARNING);
                }
            }

            //Num Cells

            if (ICNS_Row[Strings.DATASHEET_NSIC_NUM_CELLS_COLUMN_NAME] == DBNull.Value)
            {
                if (this.IsSpatial)
                {
                    ICNS_Row[Strings.DATASHEET_NSIC_NUM_CELLS_COLUMN_NAME] = Constants.DATASHEET_RUN_CONTROL_SPATIAL_NUM_CELLS_DEFAULT;
                }
                else
                {
                    ICNS_Row[Strings.DATASHEET_NSIC_NUM_CELLS_COLUMN_NAME] = Constants.DATASHEET_RUN_CONTROL_NON_SPATIAL_NUM_CELLS_DEFAULT;
                }

                if (verboseStatus)
                {
                    this.RecordStatus(StatusType.Warning, MessageStrings.STATUS_USING_DEFAULT_NUM_CELLS_WARNING);
                }
            }

            //Initial Conditions Non-Spatial Distribution.  If there are already rows in this table then
            //don't do anything, but if not then attempt to populate it using the data in deterministic transitions.

            if (ICNSDist_Table.Rows.Count > 0)
            {
                return;
            }

            if (verboseStatus)
            {
                this.RecordStatus(StatusType.Warning, MessageStrings.STATUS_NO_INITIAL_CONDITIONS_WARNING);
            }

            if (DeterministicTransitions_Table.Rows.Count == 0)
            {
                return;
            }

            //If a deterministic transition has an explicit stratum we want to use its data to create
            //a new record in the distribution.  And if a deterministic transition has a NULL stratum
            //then we want to a new record in the distribution for each stratum.  Note, however, that
            //we want to favor explicit strata over NULL strata.

            Dictionary<string, bool> ExplicitStrata = new Dictionary<string, bool>();

            foreach (DataRow dr in DeterministicTransitions_Table.Rows)
            {
                if (dr[Strings.DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME] != DBNull.Value)
                {
                    int StratumId = Convert.ToInt32(dr[Strings.DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME], CultureInfo.InvariantCulture);
                    int StateClassId = Convert.ToInt32(dr[Strings.DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME], CultureInfo.InvariantCulture);
                    string Key = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", StratumId, StateClassId);

                    ExplicitStrata.Add(Key, true);

                    DataRow NewRow = ICNSDist_Table.NewRow();

                    NewRow[Strings.DATASHEET_STRATUM_ID_COLUMN_NAME] = StratumId;
                    NewRow[Strings.DATASHEET_STATECLASS_ID_COLUMN_NAME] = StateClassId;
                    NewRow[Strings.DATASHEET_AGE_MIN_COLUMN_NAME] = dr[Strings.DATASHEET_AGE_MIN_COLUMN_NAME];
                    NewRow[Strings.DATASHEET_AGE_MAX_COLUMN_NAME] = dr[Strings.DATASHEET_AGE_MAX_COLUMN_NAME];
                    NewRow[Strings.DATASHEET_NSIC_DISTRIBUTION_RELATIVE_AMOUNT_COLUMN_NAME] = 1.0;

                    ICNSDist_Table.Rows.Add(NewRow);
                    this.m_ICDistributionsAutoGenerated = true;
                }
            }

            foreach (DataRow dr in DeterministicTransitions_Table.Rows)
            {
                if (dr[Strings.DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME] == DBNull.Value)
                {
                    foreach (DataRow drst in Strata_Sheet.GetData().Rows)
                    {
                        if (drst.RowState != DataRowState.Deleted)
                        {
                            int StratumId = Convert.ToInt32(drst[Strata_Sheet.PrimaryKeyColumn.Name], CultureInfo.InvariantCulture);
                            int StateClassId = Convert.ToInt32(dr[Strings.DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME], CultureInfo.InvariantCulture);
                            string Key = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", StratumId, StateClassId);

                            if (!ExplicitStrata.ContainsKey(Key))
                            {
                                DataRow NewRow = ICNSDist_Table.NewRow();

                                NewRow[Strings.DATASHEET_STRATUM_ID_COLUMN_NAME] = StratumId;
                                NewRow[Strings.DATASHEET_STATECLASS_ID_COLUMN_NAME] = StateClassId;
                                NewRow[Strings.DATASHEET_AGE_MIN_COLUMN_NAME] = dr[Strings.DATASHEET_AGE_MIN_COLUMN_NAME];
                                NewRow[Strings.DATASHEET_AGE_MAX_COLUMN_NAME] = dr[Strings.DATASHEET_AGE_MAX_COLUMN_NAME];
                                NewRow[Strings.DATASHEET_NSIC_DISTRIBUTION_RELATIVE_AMOUNT_COLUMN_NAME] = 1.0;

                                ICNSDist_Table.Rows.Add(NewRow);
                                this.m_ICDistributionsAutoGenerated = true;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Validates the timesteps for the specified column name and maximum timestep
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="optionColumnName"></param>
        /// <param name="timestepsColumnName"></param>
        /// <param name="timestepsColumnHeaderText"></param>
        /// <param name="maxTimestep"></param>
        /// <remarks></remarks>
        private void ValidateTimesteps(
            DataRow dr, 
            string optionColumnName, 
            string timestepsColumnName, 
            string timestepsColumnHeaderText, 
            int maxTimestep)
        {
            if (dr[optionColumnName] == DBNull.Value)
            {
                return;
            }

            if (!Convert.ToBoolean(dr[optionColumnName], CultureInfo.InvariantCulture))
            {
                return;
            }

            if (dr[timestepsColumnName] == DBNull.Value)
            {
                string message = string.Format(CultureInfo.InvariantCulture, 
                    "ST-Sim: The {0} value for '{1}' is invalid.  Using default.", 
                    this.m_TimestepUnitsLower, timestepsColumnHeaderText);

                this.RecordStatus(StatusType.Warning, message);
                dr[timestepsColumnName] = 5;

                return;
            }

            int val = Convert.ToInt32(dr[timestepsColumnName], CultureInfo.InvariantCulture);

            if (val > maxTimestep)
            {
                string message = string.Format(CultureInfo.InvariantCulture, 
                    "ST-Sim: The {0} value for '{1}' is out of range.  Using default.", 
                    this.m_TimestepUnitsLower, timestepsColumnHeaderText);

                this.RecordStatus(StatusType.Warning, message);
                dr[timestepsColumnName] = maxTimestep;

                return;
            }
        }

        /// <summary>
        /// Normalizes the color data for any color data feeds
        /// </summary>
        /// <remarks>
        /// If there are records but no colors at all then randomly add from a palette.  Note that if any
        /// records have color data then we don't change anything.
        /// </remarks>
        private void NormalizeColorData()
        {
            Debug.Assert(this.IsSpatial);

            NormalizeColorData(this.Project.GetDataSheet(Strings.DATASHEET_STATECLASS_NAME), Strings.DATASHEET_COLOR_COLUMN_NAME);
            NormalizeColorData(this.Project.GetDataSheet(Strings.DATASHEET_TRANSITION_TYPE_NAME), Strings.DATASHEET_COLOR_COLUMN_NAME);
            NormalizeColorData(this.Project.GetDataSheet(Strings.DATASHEET_AGE_GROUP_NAME), Strings.DATASHEET_COLOR_COLUMN_NAME);
        }

        private void NormalizeColorData(DataSheet ds, string colorColumnName)
        {
            if (this.Session.IsRunningOnMono)
            {
                return;
            }

            Random rnd = new Random();
            DataTable dt = ds.GetData();

            System.Drawing.Color[] colors = {
                Color.Red, Color.Green, Color.Blue, Color.Yellow, Color.Orange,
                Color.Teal, Color.Brown, Color.Purple, Color.Gray, Color.Cyan};

            if (dt.DefaultView.Count == 0)
            {
                return;
            }

            foreach (DataRow dr in dt.Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    if (dr[colorColumnName] != DBNull.Value)
                    {
                        return;
                    }
                }
            }

            var availableColors = colors.ToList();

            foreach (DataRow dr in dt.Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    int Index = rnd.Next(availableColors.Count);
                    dr[colorColumnName] = ColorUtilities.StringFromColor(availableColors[Index]);

                    //Remove used color from the list of availables. 
                    //If depleted, recharge with a fresh list

                    availableColors.RemoveAt(Index);

                    if (availableColors.Count == 0)
                    {
                        availableColors = colors.ToList();
                    }
                }
            }

            ds.Changes.Add(new ChangeRecord(this, "Normalized Color Data"));
            string msg = string.Format(CultureInfo.InvariantCulture, "Color values not specified for '{0}.'  Using defaults.", ds.DisplayName);
            this.RecordStatus(StatusType.Information, msg);
        }

        /// <summary>
        /// Normalizes the Id values for Stratums and State Class.
        /// </summary>
        /// <remarks>
        /// If there are records with no Id then add sequenctial Id values.  
        /// </remarks>
        private void NormalizeMapIds()
        {
            if (this.IsSpatial)
            {
                NormalizeMapIds(this.Project.GetDataSheet(Strings.DATASHEET_STRATA_NAME));
                NormalizeMapIds(this.Project.GetDataSheet(Strings.DATASHEET_SECONDARY_STRATA_NAME));
                NormalizeMapIds(this.Project.GetDataSheet(Strings.DATASHEET_TERTIARY_STRATA_NAME));
                NormalizeMapIds(this.Project.GetDataSheet(Strings.DATASHEET_STATECLASS_NAME));
                NormalizeMapIds(this.Project.GetDataSheet(Strings.DATASHEET_TRANSITION_TYPE_NAME));
            }
        }

        private void NormalizeMapIds(DataSheet ds)
        {
            DataTable dt = ds.GetData();

            if (dt.DefaultView.Count == 0)
            {
                return;
            }

            int index = 0;
            // Grab the max value already in use, if any exist
            foreach (DataRow dr in dt.Rows)
            {
                if (dr[Strings.DATASHEET_MAPID_COLUMN_NAME] != DBNull.Value)
                {
                    index = Math.Max(index, Convert.ToInt32(dr[Strings.DATASHEET_MAPID_COLUMN_NAME], CultureInfo.InvariantCulture));
                }
            }

            bool noIDFound = false;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr[Strings.DATASHEET_MAPID_COLUMN_NAME] == DBNull.Value)
                {
                    noIDFound = true;
                    index += 1;
                    dr[Strings.DATASHEET_MAPID_COLUMN_NAME] = index;
                }
            }

            if (noIDFound)
            {
                string msg = string.Format(CultureInfo.InvariantCulture, "Id values not specified for '{0}'. Using defaults.", ds.DisplayName);
                this.RecordStatus(StatusType.Information, msg);
            }
        }

        /// <summary>
        /// If a collection item (a.) references a user distribution value, and (b.) has a NULL
        /// for stratum and/or secondary stratum then we want to replace that item with an "expanded"
        /// set of items with explicit values based on the contents of the distribution value collection.
        ///      
        /// NOTE: The expanded set of records should contain only unique combinations of stratum and
        ///       secondary stratum, null-able values inclusive.
        /// </summary>
        /// <remarks></remarks>
        private void NormalizeForUserDistributions()
        {
            if (this.m_DistributionProvider.Values.Count > 0)
            {
                STSimDistributionBaseExpander Expander = new STSimDistributionBaseExpander(this.m_DistributionProvider);

                this.ExpandStateAttributeValues(Expander);
                this.ExpandTransitionAttributeValues(Expander);
                this.ExpandTransitionTargets(Expander);
                this.ExpandTransitionAttributeTargets(Expander);
                this.ExpandTransitionMultipliers(Expander);
                this.ExpandTransitionDirectionMultipliers(Expander);
                this.ExpandTransitionSlopeMultipliers(Expander);
                this.ExpandTransitionAdjacencyMultipliers(Expander);
            }
        }

        private void ExpandStateAttributeValues(STSimDistributionBaseExpander expander)
        {
            if (this.m_StateAttributeValues.Count > 0)
            {
                IEnumerable<STSimDistributionBase> NewItems = expander.Expand(this.m_StateAttributeValues);

                this.m_StateAttributeValues.Clear();

                foreach (StateAttributeValue t in NewItems)
                {
                    this.m_StateAttributeValues.Add(t);
                }
            }
        }

        private void ExpandTransitionAttributeValues(STSimDistributionBaseExpander expander)
        {
            if (this.m_TransitionAttributeValues.Count > 0)
            {
                IEnumerable<STSimDistributionBase> NewItems = expander.Expand(this.m_TransitionAttributeValues);

                this.m_TransitionAttributeValues.Clear();

                foreach (TransitionAttributeValue t in NewItems)
                {
                    this.m_TransitionAttributeValues.Add(t);
                }
            }
        }

        private void ExpandTransitionTargets(STSimDistributionBaseExpander expander)
        {
            if (this.m_TransitionTargets.Count > 0)
            {
                IEnumerable<STSimDistributionBase> NewItems = expander.Expand(this.m_TransitionTargets);

                this.m_TransitionTargets.Clear();

                foreach (TransitionTarget t in NewItems)
                {
                    this.m_TransitionTargets.Add(t);
                }
            }
        }

        private void ExpandTransitionAttributeTargets(STSimDistributionBaseExpander expander)
        {
            if (this.m_TransitionAttributeTargets.Count > 0)
            {
                IEnumerable<STSimDistributionBase> NewItems = expander.Expand(this.m_TransitionAttributeTargets);

                this.m_TransitionAttributeTargets.Clear();

                foreach (TransitionAttributeTarget t in NewItems)
                {
                    this.m_TransitionAttributeTargets.Add(t);
                }
            }
        }

        private void ExpandTransitionMultipliers(STSimDistributionBaseExpander expander)
        {
            if (this.m_TransitionMultiplierValues.Count > 0)
            {
                IEnumerable<STSimDistributionBase> NewItems = expander.Expand(this.m_TransitionMultiplierValues);

                this.m_TransitionMultiplierValues.Clear();

                foreach (TransitionMultiplierValue t in NewItems)
                {
                    this.m_TransitionMultiplierValues.Add(t);
                }
            }
        }

        private void ExpandTransitionDirectionMultipliers(STSimDistributionBaseExpander expander)
        {
            if (this.m_TransitionDirectionMultipliers.Count > 0)
            {
                IEnumerable<STSimDistributionBase> NewItems = expander.Expand(this.m_TransitionDirectionMultipliers);

                this.m_TransitionDirectionMultipliers.Clear();

                foreach (TransitionDirectionMultiplier t in NewItems)
                {
                    this.m_TransitionDirectionMultipliers.Add(t);
                }
            }
        }

        private void ExpandTransitionSlopeMultipliers(STSimDistributionBaseExpander expander)
        {
            if (this.m_TransitionSlopeMultipliers.Count > 0)
            {
                IEnumerable<STSimDistributionBase> NewItems = expander.Expand(this.m_TransitionSlopeMultipliers);

                this.m_TransitionSlopeMultipliers.Clear();

                foreach (TransitionSlopeMultiplier t in NewItems)
                {
                    this.m_TransitionSlopeMultipliers.Add(t);
                }
            }
        }

        private void ExpandTransitionAdjacencyMultipliers(STSimDistributionBaseExpander expander)
        {
            if (this.m_TransitionAdjacencyMultipliers.Count > 0)
            {
                IEnumerable<STSimDistributionBase> NewItems = expander.Expand(this.m_TransitionAdjacencyMultipliers);

                this.m_TransitionAdjacencyMultipliers.Clear();

                foreach (TransitionAdjacencyMultiplier t in NewItems)
                {
                    this.m_TransitionAdjacencyMultipliers.Add(t);
                }
            }
        }
    }
}
