// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Linq;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using SyncroSim.Core;
using SyncroSim.Common;
using SyncroSim.StochasticTime;

namespace SyncroSim.STSim
{
    public partial class STSimTransformer
    {
        //Tabular Output Options
        private bool m_CreateSummaryStateClassOutput;
        private int m_SummaryStateClassOutputTimesteps;
        private bool m_SummaryStateClassOutputAges;
        private bool m_SummaryStateClassZeroValues;

        private int m_SummaryTransitionAttributeOutputTimesteps;
        private bool m_SummaryTransitionAttributeOutputAges;

        private bool m_CreateSummaryTransitionOutput;
        private int m_SummaryTransitionOutputTimesteps;
        private bool m_SummaryTransitionOutputAges;
        private bool m_SummaryTransitionOutputAsIntervalMean;

        private bool m_CreateSummaryTransitionByStateClassOutput;
        private int m_SummaryTransitionByStateClassOutputTimesteps;

        private bool m_CreateSummaryStateAttributeOutput;
        private int m_SummaryStateAttributeOutputTimesteps;

        private bool m_SummaryStateAttributeOutputAges;
        private bool m_CreateSummaryTransitionAttributeOutput;

        private bool m_SummaryOmitSecondaryStrata;
        private bool m_SummaryOmitTertiaryStrata;

        //Spatial Output Options
        private bool m_CreateRasterStateClassOutput;
        private int m_RasterStateClassOutputTimesteps;

        private bool m_CreateRasterAgeOutput;
        private int m_RasterAgeOutputTimesteps;

        private bool m_CreateRasterStratumOutput;
        private int m_RasterStratumOutputTimesteps;

        private bool m_CreateRasterTransitionOutput;
        private int m_RasterTransitionOutputTimesteps;

        private bool m_CreateRasterTransitionEventOutput;
        private int m_RasterTransitionEventOutputTimesteps;

        private bool m_CreateRasterTstOutput;
        private int m_RasterTstOutputTimesteps;

        private bool m_CreateRasterStateAttributeOutput;
        private int m_RasterStateAttributeOutputTimesteps;

        private bool m_CreateRasterTransitionAttributeOutput;
        private int m_RasterTransitionAttributeOutputTimesteps;

        //Spatial Average Output Options
        private bool m_CreateAvgRasterStateClassOutput;
        private int m_AvgRasterStateClassOutputTimesteps;
        private bool m_AvgRasterStateClassCumulative;

        private bool m_CreateAvgRasterAgeOutput;
        private int m_AvgRasterAgeOutputTimesteps;
        private bool m_AvgRasterAgeCumulative;

        private bool m_CreateAvgRasterStratumOutput;
        private int m_AvgRasterStratumOutputTimesteps;
        private bool m_AvgRasterStratumCumulative;

        private bool m_CreateAvgRasterTransitionProbOutput;
        private int m_AvgRasterTransitionProbOutputTimesteps;
        private bool m_AvgRasterTransitionProbCumulative;

        private bool m_CreateAvgRasterTSTOutput;
        private int m_AvgRasterTSTOutputTimesteps;
        private bool m_AvgRasterTSTCumulative;

        private bool m_CreateAvgRasterStateAttributeOutput;
        private int m_AvgRasterStateAttributeOutputTimesteps;
        private bool m_AvgRasterStateAttributeCumulative;

        private bool m_CreateAvgRasterTransitionAttributeOutput;
        private int m_AvgRasterTransitionAttributeOutputTimesteps;
        private bool m_AvgRasterTransitionAttributeCumulative;

        //Output Collections
        private OutputStratumStateCollection m_SummaryStratumStateResults = new OutputStratumStateCollection();
        private OutputStratumStateCollectionZeroValues m_SummaryStratumStateResultsZeroValues = new OutputStratumStateCollectionZeroValues();
        private OutputStratumTransitionCollection m_SummaryStratumTransitionResults = new OutputStratumTransitionCollection();
        private OutputStratumTransitionStateCollection m_SummaryStratumTransitionStateResults = new OutputStratumTransitionStateCollection();
        private OutputStateAttributeCollection m_SummaryStateAttributeResults = new OutputStateAttributeCollection();
        private OutputTransitionAttributeCollection m_SummaryTransitionAttributeResults = new OutputTransitionAttributeCollection();

        //Output data tables
        private DataTable m_OutputStratumAmountTable;
        private DataTable m_OutputStratumStateTable;
        private DataTable m_OutputStratumTransitionTable;
        private DataTable m_OutputStratumTransitionStateTable;
        private DataTable m_OutputStateAttributeTable;
        private DataTable m_OutputTransitionAttributeTable;

        /// <summary>
        /// Determines whether or not the specified timestep is an Output timestep
        /// </summary>
        /// <param name="timestep">The timestep to test</param>
        /// <param name="frequency">The frequency for timestep output</param>
        /// <param name="shouldCreateOutput">Whether or not to create output</param>
        /// <returns>
        /// True if the timestep is the first timestep, the last timestep, or the timestep is within the frequency specified by the user.  
        /// False will be returned if the user has not specified that this type of output should be generated or if the conditions for True are not met.
        /// </returns>
        /// <remarks>
        /// The frequency of the timestep corresponds to the values that the user has specified for timestep output.  For example, someone
        /// might specifiy that they only want data every 5 timesteps.  In this case, the frequency will be 5.
        /// </remarks>
        public bool IsOutputTimestep(int timestep, int frequency, bool shouldCreateOutput)
        {
            if (shouldCreateOutput)
            {
                if (timestep == this.MinimumTimestep || timestep == this.MaximumTimestep)
                {
                    return true;
                }

                if (((timestep - this.m_TimestepZero) % frequency) == 0)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsOutputTimestepSkipMinimum(int timestep, int frequency, bool shouldCreateOutput)
        {
            if (shouldCreateOutput)
            {
                if (timestep == this.MaximumTimestep)
                {
                    return true;
                }

                if (((timestep - this.m_TimestepZero) % frequency) == 0)
                {
                    return true;
                }
            }

            return false;
        }

        //Summary output

        private bool IsSummaryStateClassTimestep(int timestep)
        {
            return this.IsOutputTimestep(timestep, this.m_SummaryStateClassOutputTimesteps, this.m_CreateSummaryStateClassOutput);
        }

        private bool IsSummaryTransitionTimestep(int timestep)
        {
            return this.IsOutputTimestep(timestep, this.m_SummaryTransitionOutputTimesteps, this.m_CreateSummaryTransitionOutput);
        }

        private bool IsSummaryTransitionByStateClassTimestep(int timestep)
        {
            return this.IsOutputTimestep(timestep, this.m_SummaryTransitionByStateClassOutputTimesteps, this.m_CreateSummaryTransitionByStateClassOutput);
        }

        private bool IsSummaryStateAttributeTimestep(int timestep)
        {
            return this.IsOutputTimestep(timestep, this.m_SummaryStateAttributeOutputTimesteps, this.m_CreateSummaryStateAttributeOutput);
        }

        private bool IsSummaryTransitionAttributeTimestep(int timestep)
        {
            return this.IsOutputTimestep(timestep, this.m_SummaryTransitionAttributeOutputTimesteps, this.m_CreateSummaryTransitionAttributeOutput);
        }

        //Spatial output

        private bool IsRasterStateClassTimestep(int timestep)
        {
            return this.IsOutputTimestep(timestep, this.m_RasterStateClassOutputTimesteps, this.m_CreateRasterStateClassOutput);
        }

        private bool IsRasterAgeTimestep(int timestep)
        {
            return this.IsOutputTimestep(timestep, this.m_RasterAgeOutputTimesteps, this.m_CreateRasterAgeOutput);
        }

        private bool IsRasterStratumTimestep(int timestep)
        {
            return this.IsOutputTimestep(timestep, this.m_RasterStratumOutputTimesteps, this.m_CreateRasterStratumOutput);
        }

        private bool IsRasterTransitionTimestep(int timestep)
        {
            return this.IsOutputTimestep(timestep, this.m_RasterTransitionOutputTimesteps, this.m_CreateRasterTransitionOutput);
        }

        private bool IsRasterTransitionEventTimestep(int timestep)
        {
            return this.IsOutputTimestep(timestep, this.m_RasterTransitionEventOutputTimesteps, this.m_CreateRasterTransitionEventOutput);
        }

        private bool IsRasterTstTimestep(int timestep)
        {
            return this.IsOutputTimestep(timestep, this.m_RasterTstOutputTimesteps, this.m_CreateRasterTstOutput);
        }

        private bool IsRasterStateAttributeTimestep(int timestep)
        {
            return this.IsOutputTimestep(timestep, this.m_RasterStateAttributeOutputTimesteps, this.m_CreateRasterStateAttributeOutput);
        }

        private bool IsRasterTransitionAttributeTimestep(int timestep)
        {
            return this.IsOutputTimestep(timestep, this.m_RasterTransitionAttributeOutputTimesteps, this.m_CreateRasterTransitionAttributeOutput);
        }

        private bool IsTransitionAdjacencyStateAttributeTimestep(int timestep, int transitionGroupId)
        {
            TransitionAdjacencySetting setting = this.m_TransitionAdjacencySettingMap.GetItem(transitionGroupId);

            if (setting == null)
            {
                return false;
            }

            return this.IsOutputTimestep(timestep, setting.UpdateFrequency, true);
        }

        //Average spatial output

        private bool IsAvgRasterStateClassTimestep(int timestep)
        {
            return this.IsOutputTimestepSkipMinimum(timestep, this.m_AvgRasterStateClassOutputTimesteps, this.m_CreateAvgRasterStateClassOutput);
        }

        private bool IsAvgRasterAgeTimestep(int timestep)
        {
            return this.IsOutputTimestepSkipMinimum(timestep, this.m_AvgRasterAgeOutputTimesteps, this.m_CreateAvgRasterAgeOutput);
        }

        private bool IsAvgRasterStratumTimestep(int timestep)
        {
            return this.IsOutputTimestepSkipMinimum(timestep, this.m_AvgRasterStratumOutputTimesteps, this.m_CreateAvgRasterStratumOutput);
        }

        private bool IsAvgRasterTransitionProbTimestep(int timestep)
        {
            return this.IsOutputTimestepSkipMinimum(timestep, this.m_AvgRasterTransitionProbOutputTimesteps, this.m_CreateAvgRasterTransitionProbOutput);
        }

        private bool IsAvgRasterTSTTimestep(int timestep)
        {
            return this.IsOutputTimestepSkipMinimum(timestep, this.m_AvgRasterTSTOutputTimesteps, this.m_CreateAvgRasterTSTOutput);
        }

        private bool IsAvgRasterStateAttributeTimestep(int timestep)
        {
            return this.IsOutputTimestepSkipMinimum(timestep, this.m_AvgRasterStateAttributeOutputTimesteps, this.m_CreateAvgRasterStateAttributeOutput);
        }

        private bool IsAvgRasterTransitionAttributeTimestep(int timestep)
        {
            return this.IsOutputTimestepSkipMinimum(timestep, this.m_AvgRasterTransitionAttributeOutputTimesteps, this.m_CreateAvgRasterTransitionAttributeOutput);
        }

        //Summary collection keys

        internal static int GetKeyOrWildcardKey(int? value)
        {
            if (!value.HasValue)
            {
                return Constants.OUTPUT_COLLECTION_WILDCARD_KEY;
            }
            else
            {
                return value.Value;
            }          
        }

        internal int GetSecondaryStratumIdKey(int? value)
        {
            if (this.m_SummaryOmitSecondaryStrata)
            {
                return Constants.OUTPUT_COLLECTION_WILDCARD_KEY;
            }
            else
            {
                return LookupKeyUtils.GetOutputCollectionKey(value);
            }
        }

        private int? GetSecondaryStratumIdValue(int? value)
        {
            if (this.m_SummaryOmitSecondaryStrata)
            {
                return null;
            }
            else
            {
                return value;
            }
        }

        internal int GetTertiaryStratumIdKey(int? value)
        {
            if (this.m_SummaryOmitTertiaryStrata)
            {
                return Constants.OUTPUT_COLLECTION_WILDCARD_KEY;
            }
            else
            {
                return LookupKeyUtils.GetOutputCollectionKey(value);
            }
        }

        private int? GetTertiaryStratumIdValue(int? value)
        {
            if (this.m_SummaryOmitTertiaryStrata)
            {
                return null;
            }
            else
            {
                return value;
            }
        }

        private int GetSecondaryStratumIdKey(Cell simulationCell)
        {
            return GetSecondaryStratumIdKey(simulationCell.SecondaryStratumId);
        }

        private int? GetSecondaryStratumIdValue(Cell simulationCell)
        {
            return GetSecondaryStratumIdValue(simulationCell.SecondaryStratumId);
        }

        private int GetTertiaryStratumIdKey(Cell simulationCell)
        {
            return GetTertiaryStratumIdKey(simulationCell.TertiaryStratumId);
        }

        private int? GetTertiaryStratumIdValue(Cell simulationCell)
        {
            return GetTertiaryStratumIdValue(simulationCell.TertiaryStratumId);
        }

        /// <summary>
        /// Called to record state class summary output for the specified simulation cell, iteration, and timestep
        /// </summary>
        /// <param name="simulationCell">The simulation cell</param>
        /// <param name="iteration">The current iteration</param>
        /// <param name="timestep">The current timestep</param>
        /// <remarks>This function aggregates by stratum, iteration, timestep, and state class.</remarks>
        private void RecordSummaryStateClassOutput(Cell simulationCell, int iteration, int timestep)
        {
            if (simulationCell.StratumId == 0 || simulationCell.StateClassId == 0)
            {
                return;
            }

            if ((this.IsSummaryStateClassTimestep(timestep)) || 
                (this.m_StateAttributeTypeIdsNoAges.Count > 0 & this.IsSummaryStateAttributeTimestep(timestep)))
            {
                int AgeKey = this.m_AgeReportingHelperSC.GetKey(simulationCell.Age);

                SevenIntegerLookupKey key = new SevenIntegerLookupKey(
                    simulationCell.StratumId, 
                    GetSecondaryStratumIdKey(simulationCell), 
                    GetTertiaryStratumIdKey(simulationCell), 
                    iteration, 
                    timestep, 
                    simulationCell.StateClassId, 
                    AgeKey);

                if (this.m_SummaryStratumStateResults.Contains(key))
                {
                    OutputStratumState oss = this.m_SummaryStratumStateResults[key];
                    oss.Amount += this.m_AmountPerCell;
                }
                else
                {
                    OutputStratumState oss = new OutputStratumState(
                        simulationCell.StratumId, 
                        GetSecondaryStratumIdValue(simulationCell), 
                        GetTertiaryStratumIdValue(simulationCell), 
                        iteration, 
                        timestep, 
                        simulationCell.StateClassId, 
                        this.m_AgeReportingHelperSC.GetAgeMinimum(simulationCell.Age), 
                        this.m_AgeReportingHelperSC.GetAgeMaximum(simulationCell.Age), 
                        AgeKey, 
                        this.m_AmountPerCell);

                    this.m_SummaryStratumStateResults.Add(oss);
                }
            }
        }

        /// <summary>
        /// Called to record transition class summary output for the specified simulation cell, 
        /// iteration, and timestep
        /// </summary>
        /// <param name="simulationCell">The simulation cell</param>
        /// <param name="currentTransition">The current transition</param>
        /// <param name="iteration">The current iteration</param>
        /// <param name="timestep">The current timestep</param>
        /// <param name="eventId">The current event Id</param>
        /// <remarks>This function aggregates by stratum, iteration, timestep, and transition group.</remarks>
        private void RecordSummaryTransitionOutput(
            Cell simulationCell, 
            Transition currentTransition, 
            int iteration, 
            int timestep, 
            Nullable<int> eventId)
        {
            if (simulationCell.StratumId == 0 || simulationCell.StateClassId == 0)
            {
                return;
            }

            if (this.m_SummaryTransitionOutputAsIntervalMean)
            {
                this.RecordTransitionOutputIntervalMeanMethod(simulationCell, currentTransition, iteration, timestep);
            }
            else
            {
                this.RecordTransitionOutputNormalMethod(simulationCell, currentTransition, iteration, timestep, eventId);
            }
        }

        private void RecordTransitionOutputIntervalMeanMethod(
            Cell simulationCell,
            Transition currentTransition,
            int iteration,
            int timestep)
        {
            //Look up the output record using the aggregator timestep instead of the actual timestep.
            int AggregatorTimestep = this.m_IntervalMeanTimestepMap.GetValue(timestep);
            TransitionType tt = this.m_TransitionTypes[currentTransition.TransitionTypeId];
            int EventIdKey = 0;

            foreach (TransitionGroup tg in tt.TransitionGroups)
            {
                int AgeKey = this.m_AgeReportingHelperTR.GetKey(simulationCell.Age);

                EightIntegerLookupKey key = new EightIntegerLookupKey(
                    simulationCell.StratumId,
                    GetSecondaryStratumIdKey(simulationCell),
                    GetTertiaryStratumIdKey(simulationCell),
                    iteration,
                    AggregatorTimestep,
                    tg.TransitionGroupId,
                    AgeKey,
                    EventIdKey);

                if (this.m_SummaryStratumTransitionResults.Contains(key))
                {
                    OutputStratumTransition ost = this.m_SummaryStratumTransitionResults[key];
                    ost.Amount += this.m_AmountPerCell;
                }
                else
                {
                    OutputStratumTransition ost = new OutputStratumTransition(
                        simulationCell.StratumId,
                        GetSecondaryStratumIdValue(simulationCell),
                        GetTertiaryStratumIdValue(simulationCell),
                        iteration,
                        AggregatorTimestep,
                        tg.TransitionGroupId,
                        this.m_AgeReportingHelperTR.GetAgeMinimum(simulationCell.Age),
                        this.m_AgeReportingHelperTR.GetAgeMaximum(simulationCell.Age),
                        AgeKey,
                        null,
                        EventIdKey,
                        this.m_AmountPerCell);

                    this.m_SummaryStratumTransitionResults.Add(ost);
                }
            }
        }

        private void RecordTransitionOutputNormalMethod(
            Cell simulationCell,
            Transition currentTransition,
            int iteration,
            int timestep,
            Nullable<int> eventId)
        {
            if (!this.IsSummaryTransitionTimestep(timestep))
            {
                return;
            }

            TransitionType tt = this.m_TransitionTypes[currentTransition.TransitionTypeId];

            foreach (TransitionGroup tg in tt.TransitionGroups)
            {
                int AgeKey = this.m_AgeReportingHelperTR.GetKey(simulationCell.Age);
                int EventIdKey = GetKeyOrWildcardKey(eventId);

                EightIntegerLookupKey key = new EightIntegerLookupKey(
                    simulationCell.StratumId,
                    GetSecondaryStratumIdKey(simulationCell),
                    GetTertiaryStratumIdKey(simulationCell),
                    iteration,
                    timestep,
                    tg.TransitionGroupId,
                    AgeKey,
                    EventIdKey);

                if (this.m_SummaryStratumTransitionResults.Contains(key))
                {
                    OutputStratumTransition ost = this.m_SummaryStratumTransitionResults[key];
                    ost.Amount += this.m_AmountPerCell;
                }
                else
                {
                    OutputStratumTransition ost = new OutputStratumTransition(
                        simulationCell.StratumId,
                        GetSecondaryStratumIdValue(simulationCell),
                        GetTertiaryStratumIdValue(simulationCell),
                        iteration,
                        timestep,
                        tg.TransitionGroupId,
                        this.m_AgeReportingHelperTR.GetAgeMinimum(simulationCell.Age),
                        this.m_AgeReportingHelperTR.GetAgeMaximum(simulationCell.Age),
                        AgeKey,
                        eventId,
                        EventIdKey,
                        this.m_AmountPerCell);

                    this.m_SummaryStratumTransitionResults.Add(ost);
                }
            }
        }

        /// <summary>
        /// Called to record transition by state class summary output for the specified 
        /// simulation cell, iteration, and timestep
        /// </summary>
        /// <param name="simulationCell">The simulation cell</param>
        /// <param name="currentTransition">The current transition</param>
        /// <param name="iteration">The current iteration</param>
        /// <param name="timestep">The current timestep</param>
        /// <remarks>This function aggregates by stratum, state class source, 
        /// state class destination, and transition</remarks>
        private void RecordSummaryTransitionByStateClassOutput(
            Cell simulationCell, 
            Transition currentTransition, 
            int iteration, 
            int timestep)
        {
            if (simulationCell.StratumId == 0 || simulationCell.StateClassId == 0)
            {
                return;
            }

            if (this.IsSummaryTransitionByStateClassTimestep(timestep))
            {
                int DestStateClass = simulationCell.StateClassId;

                if (currentTransition.StateClassIdDestination.HasValue)
                {
                    DestStateClass = currentTransition.StateClassIdDestination.Value;
                }

                EightIntegerLookupKey key = new EightIntegerLookupKey(
                    simulationCell.StratumId, 
                    GetSecondaryStratumIdKey(simulationCell), 
                    GetTertiaryStratumIdKey(simulationCell), 
                    iteration, 
                    timestep, 
                    currentTransition.TransitionTypeId, 
                    currentTransition.StateClassIdSource, 
                    DestStateClass);

                if (this.m_SummaryStratumTransitionStateResults.Contains(key))
                {
                    OutputStratumTransitionState osts = this.m_SummaryStratumTransitionStateResults[key];
                    osts.Amount += this.m_AmountPerCell;
                }
                else
                {
                    OutputStratumTransitionState osts = new OutputStratumTransitionState(
                        simulationCell.StratumId, 
                        GetSecondaryStratumIdValue(simulationCell), 
                        GetTertiaryStratumIdValue(simulationCell), 
                        iteration, 
                        timestep, 
                        currentTransition.TransitionTypeId, 
                        currentTransition.StateClassIdSource, 
                        DestStateClass, 
                        this.m_AmountPerCell);

                    this.m_SummaryStratumTransitionStateResults.Add(osts);
                }
            }
        }

        /// <summary>
        /// Called to record attribute summary output for the specified 
        /// simulation cell, iteration, and timestep
        /// </summary>
        /// <param name="simulationCell">The simulation cell</param>
        /// <param name="iteration">The current iteration</param>
        /// <param name="timestep">The current timestep</param>
        /// <remarks>This function aggregates by stratum, iteration, timestep, and attribute type Id.</remarks>
        private void RecordSummaryStateAttributeOutput(Cell simulationCell, int iteration, int timestep)
        {
            if (simulationCell.StratumId == 0 || simulationCell.StateClassId == 0)
            {
                return;
            }

            if (this.m_StateAttributeTypeIdsAges.Count == 0)
            {
                Debug.Assert(!this.m_StateAttributeValueMapAges.HasItems);
                return;
            }

            if (!this.IsSummaryStateAttributeTimestep(timestep) && 
                !this.IsAvgRasterStateAttributeTimestep(timestep))
            {
                return;
            }

            foreach (int AttributeTypeId in this.m_StateAttributeTypeIdsAges.Keys)
            {
                double? AttrValue = this.m_StateAttributeValueMapAges.GetAttributeValueByAge(
                    AttributeTypeId, 
                    simulationCell.StratumId, 
                    GetSecondaryStratumIdValue(simulationCell), 
                    GetTertiaryStratumIdValue(simulationCell), 
                    simulationCell.StateClassId, 
                    iteration, 
                    timestep, 
                    simulationCell.Age);

                if (AttrValue.HasValue)
                {
                    int AgeKey = this.m_AgeReportingHelperSA.GetKey(simulationCell.Age);

                    SevenIntegerLookupKey key = new SevenIntegerLookupKey(
                        simulationCell.StratumId, 
                        GetSecondaryStratumIdKey(simulationCell), 
                        GetTertiaryStratumIdKey(simulationCell), 
                        iteration, 
                        timestep, 
                        AttributeTypeId, 
                        AgeKey);

                    if (this.m_SummaryStateAttributeResults.Contains(key))
                    {
                        OutputStateAttribute ossa = this.m_SummaryStateAttributeResults[key];
                        ossa.Amount += (this.m_AmountPerCell * AttrValue.Value);
                    }
                    else
                    {
                        OutputStateAttribute ossa = new OutputStateAttribute(
                            simulationCell.StratumId, 
                            GetSecondaryStratumIdValue(simulationCell), 
                            GetTertiaryStratumIdValue(simulationCell), 
                            iteration, 
                            timestep, 
                            AttributeTypeId, 
                            this.m_AgeReportingHelperSA.GetAgeMinimum(simulationCell.Age), 
                            this.m_AgeReportingHelperSA.GetAgeMaximum(simulationCell.Age), 
                            AgeKey, 
                            (this.m_AmountPerCell * AttrValue.Value));

                        this.m_SummaryStateAttributeResults.Add(ossa);
                    }
                }
            }
        }

        /// <summary>
        /// Records average raster state class data for the specified timestep
        /// </summary>
        /// <param name="timestep"></param>
        private void RecordAvgRasterStateClassData(int timestep)
        {
            if (!this.m_IsSpatial)
            {
                return;
            }

            if (!this.m_CreateAvgRasterStateClassOutput)
            {
                return;
            }

            if (this.m_AvgRasterStateClassCumulative)
            {
                this.RecordAvgRasterStateClassOutputCumulative(timestep);
            }
            else
            {
                if (this.IsAvgRasterStateClassTimestep(timestep))
                {
                    this.RecordAvgRasterStateClassOutputNormalMethod(timestep);
                }
            }
        }

        private void RecordAvgRasterStateClassOutputNormalMethod(int timestep)
        {
            Debug.Assert(this.IsSpatial);
            Debug.Assert(this.m_CreateAvgRasterStateClassOutput);
            Debug.Assert(!this.m_AvgRasterStateClassCumulative);

            foreach (StateClass sc in this.m_StateClasses)
            {
                Dictionary<int, double[]> dict = this.m_AvgStateClassMap[sc.Id];
                double[] Values = dict[timestep];

                foreach (Cell c in this.Cells)
                {
                    if (c.StateClassId == sc.Id)
                    {
                        Debug.Assert(Values[c.CollectionIndex] >= 0.0);
                        Values[c.CollectionIndex] += 1 / (double)this.m_TotalIterations;
                    }
                }
            }
        }

        private void RecordAvgRasterStateClassOutputCumulative(int timestep)
        {
            Debug.Assert(this.IsSpatial);
            Debug.Assert(this.m_CreateAvgRasterStateClassOutput);
            Debug.Assert(this.m_AvgRasterStateClassCumulative);

            int timestepKey = this.GetTimestepKeyForAcrossTimestepAverage(timestep, this.m_AvgRasterStateClassOutputTimesteps);

            foreach (StateClass sc in this.m_StateClasses)
            {
                Dictionary<int, double[]> dict = this.m_AvgStateClassMap[sc.Id];
                double[] Values = dict[timestepKey];

                foreach (Cell c in this.Cells)
                {
                    if (c.StateClassId == sc.Id)
                    {
                        int i = c.CollectionIndex;
                        Debug.Assert(Values[i] >= 0.0);

                        //Now lets do the probability calculation.  The value to increment by is 1/(tsf*N) 
                        //where tsf is the timestep frequency N is the number of iterations.
                        //Accomodate last bin, where not multiple of frequency. For instance MaxTS of 8, 
                        //and freq of 5, would give bins 1-5, and 6-8.

                        if ((timestepKey == this.MaximumTimestep) && (((timestepKey - this.TimestepZero) % this.m_AvgRasterStateClassOutputTimesteps) != 0))
                        {
                            Values[i] += 1 / (double)((timestepKey - this.TimestepZero) % this.m_AvgRasterStateClassOutputTimesteps * this.m_TotalIterations);
                        }
                        else
                        {
                            Values[i] += 1 / (double)(this.m_AvgRasterStateClassOutputTimesteps * this.m_TotalIterations);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Records average raster age data for the specified timestep
        /// </summary>
        /// <param name="timestep"></param>
        private void RecordAvgRasterAgeData(int timestep)
        {
            if (!this.m_IsSpatial)
            {
                return;
            }

            if (!this.m_CreateAvgRasterAgeOutput)
            {
                return;
            }

            if (this.m_AvgRasterAgeCumulative)
            {
                this.RecordAvgRasterAgeOutputCumulative(timestep);
            }
            else
            {
                if (this.IsAvgRasterAgeTimestep(timestep))
                {
                    this.RecordAvgRasterAgeOutputNormalMethod(timestep);
                }
            }
        }

        private void RecordAvgRasterAgeOutputNormalMethod(int timestep)
        {
            Debug.Assert(this.IsSpatial);
            Debug.Assert(this.m_CreateAvgRasterAgeOutput);
            Debug.Assert(!this.m_AvgRasterAgeCumulative);

            double[] Values = this.m_AvgAgeMap[timestep];

            foreach (Cell cell in this.Cells)
            {
                Values[cell.CollectionIndex] += cell.Age / (double)this.m_TotalIterations; 
            }
        }

        private void RecordAvgRasterAgeOutputCumulative(int timestep)
        {
            Debug.Assert(this.IsSpatial);
            Debug.Assert(this.m_CreateAvgRasterAgeOutput);
            Debug.Assert(this.m_AvgRasterAgeCumulative);

            int timestepKey = this.GetTimestepKeyForAcrossTimestepAverage(timestep, this.m_AvgRasterAgeOutputTimesteps);
            double[] Values = this.m_AvgAgeMap[timestepKey];

            foreach (Cell cell in this.Cells)
            {
                int i = cell.CollectionIndex;

                //Accomodate last bin, where not multiple of frequency. For instance MaxTS of 8, 
                //and freq of 5, would give bins 1-5, and 6-8.

                if ((timestepKey == this.MaximumTimestep) && (((timestepKey - this.TimestepZero) % this.m_AvgRasterAgeOutputTimesteps) != 0))
                {
                    Values[i] += cell.Age / (double)((timestepKey - this.TimestepZero) % this.m_AvgRasterAgeOutputTimesteps * this.m_TotalIterations);
                }
                else
                {
                    Values[i] += cell.Age / (double)(this.m_AvgRasterAgeOutputTimesteps * this.m_TotalIterations);
                }
            }
        }

        /// <summary>
        /// Records average raster stratum data for the specified timestep
        /// </summary>
        /// <param name="timestep"></param>
        private void RecordAvgRasterStratumData(int timestep)
        {
            if (!this.m_IsSpatial)
            {
                return;
            }

            if (!this.m_CreateAvgRasterStratumOutput)
            {
                return;
            }

            if (this.m_AvgRasterStratumCumulative)
            {
                this.RecordAvgRasterStratumOutputCumulative(timestep);
            }
            else
            {
                if (this.IsAvgRasterStratumTimestep(timestep))
                {
                    this.RecordAvgRasterStratumOutputNormalMethod(timestep);
                }
            }
        }

        private void RecordAvgRasterStratumOutputNormalMethod(int timestep)
        {
            Debug.Assert(this.IsSpatial);
            Debug.Assert(this.m_CreateAvgRasterStratumOutput);
            Debug.Assert(!this.m_AvgRasterStratumCumulative);

            foreach (Stratum st in this.m_Strata)
            {
                Dictionary<int, double[]> dict = this.m_AvgStratumMap[st.StratumId];
                double[] Values = dict[timestep];

                foreach (Cell c in this.Cells)
                {
                    if (c.StratumId == st.StratumId)
                    {
                        Debug.Assert(Values[c.CollectionIndex] >= 0.0);
                        Values[c.CollectionIndex] += 1 / (double)this.m_TotalIterations;
                    }
                }
            }
        }

        private void RecordAvgRasterStratumOutputCumulative(int timestep)
        {
            Debug.Assert(this.IsSpatial);
            Debug.Assert(this.m_CreateAvgRasterStratumOutput);
            Debug.Assert(this.m_AvgRasterStratumCumulative);

            int timestepKey = this.GetTimestepKeyForAcrossTimestepAverage(timestep, this.m_AvgRasterStratumOutputTimesteps);

            foreach (Stratum st in this.m_Strata)
            {
                Dictionary<int, double[]> dict = this.m_AvgStratumMap[st.StratumId];
                double[] Values = dict[timestepKey];

                foreach (Cell c in this.Cells)
                {
                    if (c.StratumId == st.StratumId)
                    {
                        int i = c.CollectionIndex;
                        Debug.Assert(Values[i] >= 0.0);

                        //Now lets do the probability calculation.  The value to increment by is 1/(tsf*N) 
                        //where tsf is the timestep frequency N is the number of iterations.
                        //Accomodate last bin, where not multiple of frequency. For instance MaxTS of 8, 
                        //and freq of 5, would give bins 1-5, and 6-8.

                        if ((timestepKey == this.MaximumTimestep) && (((timestepKey - this.TimestepZero) % this.m_AvgRasterStratumOutputTimesteps) != 0))
                        {
                            Values[i] += 1 / (double)((timestepKey - this.TimestepZero) % this.m_AvgRasterStratumOutputTimesteps * this.m_TotalIterations);
                        }
                        else
                        {
                            Values[i] += 1 / (double)(this.m_AvgRasterStratumOutputTimesteps * this.m_TotalIterations);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Record average transition probability data for the specified timestep.
        /// </summary>
        /// <param name="timestep">The current timestep</param>
        /// <param name="dictTransitionedPixels">A dictionary of arrays of Transition Types</param>
        /// <remarks></remarks>
        private void RecordAvgRasterTransitionProbabilityData(
            int timestep,
            Dictionary<int, int[]> dictTransitionedPixels)
        {
            if (!this.m_CreateAvgRasterTransitionProbOutput)
            {
                return;
            }

            foreach (int transitionGroupId in dictTransitionedPixels.Keys)
            {
                int[] transitionedPixels = dictTransitionedPixels[transitionGroupId];
                var distArray = transitionedPixels.Distinct();

                if (distArray.Count() == 1)
                {
                    var el0 = distArray.ElementAt(0);

                    if (el0.Equals(0.0) || el0.Equals(Spatial.DefaultNoDataValue))
                    {
                        continue;
                    }
                }

                if (this.m_AvgRasterTransitionProbCumulative)
                {
                    this.RecordAvgTransitionProbabilityOutputCumulative(
                        timestep, transitionGroupId, transitionedPixels);
                }
                else
                {
                    if (this.IsAvgRasterTransitionProbTimestep(timestep))
                    {
                        this.RecordAvgTransitionProbabilityOutputNormalMethod(
                            timestep, transitionGroupId, transitionedPixels);
                    }
                }
            }
        }

        private void RecordAvgTransitionProbabilityOutputNormalMethod(
            int timestep,
            int transitionGroupId,
            int[] transitionedPixels)
        {
            Debug.Assert(this.IsSpatial);
            Debug.Assert(this.m_CreateAvgRasterTransitionProbOutput);
            Debug.Assert(!this.m_AvgRasterTransitionProbCumulative);

            Dictionary<int, double[]> dict = this.m_AvgTransitionProbMap[transitionGroupId];
            double[] Values = dict[timestep];

            foreach (Cell cell in this.Cells)
            {
                int i = cell.CollectionIndex;

                if (transitionedPixels[i] > 0)
                {
                    Debug.Assert(Values[i] >= 0.0);
                    Values[i] += 1 / (double)this.m_TotalIterations;
                }
            }
        }

        private void RecordAvgTransitionProbabilityOutputCumulative(
            int timestep,
            int transitionGroupId,
            int[] transitionedPixels)
        {
            Debug.Assert(this.IsSpatial);
            Debug.Assert(this.m_CreateAvgRasterTransitionProbOutput);
            Debug.Assert(this.m_AvgRasterTransitionProbCumulative);

            Dictionary<int, double[]> dict = this.m_AvgTransitionProbMap[transitionGroupId];
            int timestepKey = this.GetTimestepKeyForAcrossTimestepAverage(timestep, this.m_AvgRasterTransitionProbOutputTimesteps);
            double[] Values = dict[timestepKey];

            foreach (Cell cell in this.Cells)
            {
                int i = cell.CollectionIndex;

                if (transitionedPixels[i] > 0)
                {
                    Debug.Assert(Values[i] >= 0.0);

                    //Now lets do the probability calculation.  The value to increment by is 1/(tsf*N) 
                    //where tsf is the timestep frequency N is the number of iterations.
                    //Accomodate last bin, where not multiple of frequency. For instance MaxTS of 8, 
                    //and freq of 5, would give bins 1-5, and 6-8.

                    if ((timestepKey == this.MaximumTimestep) && (((timestepKey - this.TimestepZero) % this.m_AvgRasterTransitionProbOutputTimesteps) != 0))
                    {
                        Values[i] += 1 / (double)((timestepKey - this.TimestepZero) % this.m_AvgRasterTransitionProbOutputTimesteps * this.m_TotalIterations);
                    }
                    else
                    {
                        Values[i] += 1 / (double)(this.m_AvgRasterTransitionProbOutputTimesteps * this.m_TotalIterations);
                    }
                }
            }
        }

        /// <summary>
        /// Record average TST data for the specified timestep.
        /// </summary>
        /// <param name="timestep">The current timestep</param>
        /// <remarks></remarks>
        private void RecordAvgRasterTSTData(int timestep)
        {
            if (!this.m_CreateAvgRasterTSTOutput)
            {
                return;
            }

            List<int> TSTGroupIds = this.GetTSTTransitionGroupIds();

            if (TSTGroupIds.Count == 0)
            {
                return;
            }

            if (this.m_AvgRasterTSTCumulative)
            {
                this.RecordAvgTSTOutputCumulative(timestep, TSTGroupIds);
            }
            else
            {
                if (this.IsAvgRasterTSTTimestep(timestep))
                {
                    this.RecordAvgTSTOutputNormalMethod(timestep, TSTGroupIds);
                }
            }
        }

        private void RecordAvgTSTOutputNormalMethod(int timestep, List<int> tstGroupIds)
        {
            Debug.Assert(this.IsSpatial);
            Debug.Assert(this.m_CreateAvgRasterTSTOutput);
            Debug.Assert(!this.m_AvgRasterTSTCumulative);
            Debug.Assert(tstGroupIds.Count > 0);

            foreach (int tgid in tstGroupIds)
            {
                Dictionary<int, double[]> dict = this.m_AvgTSTMap[tgid];
                double[] Values = dict[timestep];

                foreach (Cell c in this.Cells)
                {
                    int i = c.CollectionIndex;
                    Tst tst = c.TstValues[tgid];

                    Values[i] += tst.TstValue / (double)this.m_TotalIterations;
                }
            }
        }

        private void RecordAvgTSTOutputCumulative(int timestep, List<int> tstGroupIds)
        {
            Debug.Assert(this.IsSpatial);
            Debug.Assert(this.m_CreateAvgRasterTSTOutput);
            Debug.Assert(this.m_AvgRasterTSTCumulative);
            Debug.Assert(tstGroupIds.Count > 0);

            int timestepKey = this.GetTimestepKeyForAcrossTimestepAverage(
                timestep, this.m_AvgRasterTSTOutputTimesteps);

            foreach (int tgid in tstGroupIds)
            {
                Dictionary<int, double[]> dict = this.m_AvgTSTMap[tgid];
                double[] Values = dict[timestepKey];

                foreach (Cell c in this.Cells)
                {
                    int i = c.CollectionIndex;
                    Tst tst = c.TstValues[tgid];

                    if ((timestepKey == this.MaximumTimestep) && (((timestepKey - this.TimestepZero) % this.m_AvgRasterTSTOutputTimesteps) != 0))
                    {
                        Values[i] += tst.TstValue / (double)((timestepKey - this.TimestepZero) % this.m_AvgRasterTSTOutputTimesteps * this.m_TotalIterations);
                    }
                    else
                    {
                        Values[i] += tst.TstValue / (double)(this.m_AvgRasterTSTOutputTimesteps * this.m_TotalIterations);
                    }
                }
            }
        }

        /// <summary>
        /// Record average state attribute data for the specified iteration and timestep.
        /// </summary>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        private void RecordAvgRasterStateAttributeData(int iteration, int timestep)
        {
            if (!this.m_IsSpatial)
            {
                return;
            }

            if (!this.m_CreateAvgRasterStateAttributeOutput)
            {
                return;
            }

            if (this.m_AvgRasterAgeCumulative)
            {
                this.RecordAvgRasterStateAttributeOutputCumulative(iteration, timestep);
            }
            else
            {
                if (this.IsAvgRasterStateAttributeTimestep(timestep))
                {
                    this.RecordAvgRasterStateAttributeOutputNormalMethod(iteration, timestep);
                }
            }
        }

        private void RecordAvgRasterStateAttributeOutputNormalMethod(int iteration, int timestep)
        {
            Debug.Assert(this.IsSpatial);
            Debug.Assert(this.m_CreateAvgRasterStateAttributeOutput);
            Debug.Assert(!this.m_AvgRasterStateAttributeCumulative);

            foreach (int AttributeTypeId in this.m_StateAttributeTypeIdsNoAges.Keys)
            {
                Dictionary<int, double[]> dict = this.m_AvgStateAttrMap[AttributeTypeId];
                double[] Values = dict[timestep];

                foreach (Cell c in this.Cells)
                {
                    double? AttrValue = this.m_StateAttributeValueMapNoAges.GetAttributeValueNoAge(
                        AttributeTypeId, 
                        c.StratumId, c.SecondaryStratumId, c.TertiaryStratumId, 
                        c.StateClassId, iteration, timestep);

                    if (AttrValue != null)
                    {
                        int i = c.CollectionIndex;

                        if (Values[i] == Spatial.DefaultNoDataValue)
                        {
                            Values[i] = 0.0;
                        }

                        double v = Convert.ToDouble(AttrValue, CultureInfo.InvariantCulture);
                        Values[i] += v / this.m_TotalIterations;
                    }
                }
            }

            foreach (int AttributeTypeId in this.m_StateAttributeTypeIdsAges.Keys)
            {
                Dictionary<int, double[]> dict = this.m_AvgStateAttrMap[AttributeTypeId];
                double[] Values = dict[timestep];

                foreach (Cell c in this.Cells)
                {
                    double? AttrValue = this.m_StateAttributeValueMapAges.GetAttributeValueByAge(
                        AttributeTypeId, 
                        c.StratumId, c.SecondaryStratumId, c.TertiaryStratumId, 
                        c.StateClassId, iteration, timestep, c.Age);

                    if (AttrValue != null)
                    {
                        int i = c.CollectionIndex;

                        if (Values[i] == Spatial.DefaultNoDataValue)
                        {
                            Values[i] = 0.0;
                        }

                        double v = Convert.ToDouble(AttrValue, CultureInfo.InvariantCulture);
                        Values[i] += v / this.m_TotalIterations;
                    }
                }
            }
        }

        private void RecordAvgRasterStateAttributeOutputCumulative(int iteration, int timestep)
        {
            Debug.Assert(this.IsSpatial);
            Debug.Assert(this.m_CreateAvgRasterAgeOutput);
            Debug.Assert(this.m_AvgRasterAgeCumulative);

            int timestepKey = this.GetTimestepKeyForAcrossTimestepAverage(
                timestep, this.m_AvgRasterStateAttributeOutputTimesteps);

            foreach (int AttributeTypeId in this.m_StateAttributeTypeIdsNoAges.Keys)
            {
                Dictionary<int, double[]> dict = this.m_AvgStateAttrMap[AttributeTypeId];
                double[] Values = dict[timestepKey];

                foreach (Cell c in this.Cells)
                {
                    double? AttrValue = this.m_StateAttributeValueMapNoAges.GetAttributeValueNoAge(
                        AttributeTypeId,
                        c.StratumId, c.SecondaryStratumId, c.TertiaryStratumId,
                        c.StateClassId, iteration, timestep);

                    if (AttrValue != null)
                    {
                        int i = c.CollectionIndex;

                        if (Values[i] == Spatial.DefaultNoDataValue)
                        {
                            Values[i] = 0.0;
                        }

                        double v = Convert.ToDouble(AttrValue, CultureInfo.InvariantCulture);

                        //Accomodate last bin, where not multiple of frequency. For instance MaxTS of 8, 
                        //and freq of 5, would give bins 1-5, and 6-8.

                        if ((timestepKey == this.MaximumTimestep) && (((timestepKey - this.TimestepZero) % this.m_AvgRasterStateAttributeOutputTimesteps) != 0))
                        {
                            Values[i] += v / (double)((timestepKey - this.TimestepZero) % this.m_AvgRasterStateAttributeOutputTimesteps * this.m_TotalIterations);
                        }
                        else
                        {
                            Values[i] += v / (double)(this.m_AvgRasterStateAttributeOutputTimesteps * this.m_TotalIterations);
                        }
                    }
                }
            }

            foreach (int AttributeTypeId in this.m_StateAttributeTypeIdsAges.Keys)
            {
                Dictionary<int, double[]> dict = this.m_AvgStateAttrMap[AttributeTypeId];
                double[] Values = dict[timestepKey];

                foreach (Cell c in this.Cells)
                {
                    double? AttrValue = this.m_StateAttributeValueMapAges.GetAttributeValueByAge(
                        AttributeTypeId,
                        c.StratumId, c.SecondaryStratumId, c.TertiaryStratumId,
                        c.StateClassId, iteration, timestep, c.Age);

                    if (AttrValue != null)
                    {
                        int i = c.CollectionIndex;

                        if (Values[i] == Spatial.DefaultNoDataValue)
                        {
                            Values[i] = 0.0;
                        }

                        double v = Convert.ToDouble(AttrValue, CultureInfo.InvariantCulture);

                        //Accomodate last bin, where not multiple of frequency. For instance MaxTS of 8, 
                        //and freq of 5, would give bins 1-5, and 6-8.

                        if ((timestepKey == this.MaximumTimestep) && (((timestepKey - this.TimestepZero) % this.m_AvgRasterStateAttributeOutputTimesteps) != 0))
                        {
                            Values[i] += v / (double)((timestepKey - this.TimestepZero) % this.m_AvgRasterStateAttributeOutputTimesteps * this.m_TotalIterations);
                        }
                        else
                        {
                            Values[i] += v / (double)(this.m_AvgRasterStateAttributeOutputTimesteps * this.m_TotalIterations);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Record average transition attribute data for the specified timestep.
        /// </summary>
        /// <param name="timestep">The current timestep</param>
        /// <param name="rasterTransitionAttrValues">The transitioned attribute values</param>
        /// <remarks></remarks>
        private void RecordAvgRasterTransitionAttributeData(
            int timestep,
            Dictionary<int, double[]> rasterTransitionAttrValues)
        {
            if (!this.m_CreateAvgRasterTransitionAttributeOutput)
            {
                return;
            }

            foreach (int AttributeId in rasterTransitionAttrValues.Keys)
            {
                double[] AttrValues = rasterTransitionAttrValues[AttributeId];
                var distArray = AttrValues.Distinct();

                if (distArray.Count() == 1)
                {
                    var el0 = distArray.ElementAt(0);

                    if (el0.Equals(Spatial.DefaultNoDataValue))
                    {
                        continue;
                    }
                }

                if (this.m_AvgRasterTransitionAttributeCumulative)
                {
                    this.RecordAvgTransitionAttributeOutputCumulative(
                        timestep, AttributeId, AttrValues);
                }
                else
                {
                    if (this.IsAvgRasterTransitionAttributeTimestep(timestep))
                    {
                        this.RecordAvgTransitionAttributeOutputNormalMethod(
                            timestep, AttributeId, AttrValues);
                    }
                }
            }
        }

        private void RecordAvgTransitionAttributeOutputNormalMethod(
            int timestep,
            int transitionAttributeTypeId,
            double[] rasterTransitionAttrValues)
        {
            Debug.Assert(this.IsSpatial);
            Debug.Assert(this.m_CreateAvgRasterTransitionAttributeOutput);
            Debug.Assert(!this.m_AvgRasterTransitionAttributeCumulative);

            Dictionary<int, double[]> dict = this.m_AvgTransitionAttrMap[transitionAttributeTypeId];
            double[] Values = dict[timestep];

            foreach (Cell cell in this.Cells)
            {
                int i = cell.CollectionIndex;
                double v = rasterTransitionAttrValues[i];

                if (!v.Equals(Spatial.DefaultNoDataValue))
                {
                    if (Values[i] == Spatial.DefaultNoDataValue)
                    {
                        Values[i] = 0.0;
                    }

                    Values[i] += v / this.m_TotalIterations;
                }
            }
        }

        private void RecordAvgTransitionAttributeOutputCumulative(
            int timestep,
            int transitionAttributeTypeId,
            double[] rasterTransitionAttrValues)
        {
            Debug.Assert(this.IsSpatial);
            Debug.Assert(this.m_CreateAvgRasterTransitionAttributeOutput);
            Debug.Assert(this.m_AvgRasterTransitionAttributeCumulative);

            Dictionary<int, double[]> dict = this.m_AvgTransitionAttrMap[transitionAttributeTypeId];
            int timestepKey = this.GetTimestepKeyForAcrossTimestepAverage(timestep, this.m_AvgRasterTransitionAttributeOutputTimesteps);
            double[] Values = dict[timestepKey];

            foreach (Cell cell in this.Cells)
            {
                int i = cell.CollectionIndex;
                double v = rasterTransitionAttrValues[i];

                if (!v.Equals(Spatial.DefaultNoDataValue))
                {
                    //Accomodate last bin, where not multiple of frequency. For instance MaxTS of 8, 
                    //and freq of 5, would give bins 1-5, and 6-8.

                    if (Values[i] == Spatial.DefaultNoDataValue)
                    {
                        Values[i] = 0.0;
                    }

                    if ((timestepKey == this.MaximumTimestep) && (((timestepKey - this.TimestepZero) % this.m_AvgRasterTransitionAttributeOutputTimesteps) != 0))
                    {
                        Values[i] += v / (double)((timestepKey - this.TimestepZero) % this.m_AvgRasterTransitionAttributeOutputTimesteps * this.m_TotalIterations);
                    }
                    else
                    {
                        Values[i] += v / (double)(this.m_AvgRasterTransitionAttributeOutputTimesteps * this.m_TotalIterations);
                    }
                }
            }
        }

        /// <summary>
        /// Writes the stratum amount tabular data
        /// </summary>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <remarks></remarks>
        private void WriteStratumAmountTabularData(int iteration, int timestep)
        {
            if (!this.IsSummaryStateClassTimestep(timestep) && !this.IsSummaryTransitionTimestep(timestep))
            {
                return;
            }

            List<int?> SecondaryStratumIds = new List<int?>();
            List<int?> TertiaryStratumIds = new List<int?>();

            int? ssnull = null;
            int? tsnull = null;

            SecondaryStratumIds.Add(ssnull);
            TertiaryStratumIds.Add(tsnull);

            foreach (Stratum s in this.m_SecondaryStrata)
            {
                SecondaryStratumIds.Add(s.StratumId);
            }

            foreach (Stratum s in this.m_TertiaryStrata)
            {
                TertiaryStratumIds.Add(s.StratumId);
            }

            foreach (Stratum PrimaryStratum in this.m_Strata)
            {
                foreach (int? SecondaryStratumId in SecondaryStratumIds)
                {
                    foreach (int? TertiaryStratumId in TertiaryStratumIds)
                    {
                        object o = this.m_ProportionAccumulatorMap.GetValue(
                            PrimaryStratum.StratumId, SecondaryStratumId, TertiaryStratumId);

                        if (o != null)
                        {
                            DataRow dr = this.m_OutputStratumAmountTable.NewRow();

                            dr[Strings.DATASHEET_ITERATION_COLUMN_NAME] = iteration;
                            dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME] = timestep;
                            dr[Strings.DATASHEET_STRATUM_ID_COLUMN_NAME] = PrimaryStratum.StratumId;
                            dr[Strings.DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(SecondaryStratumId);
                            dr[Strings.DATASHEET_TERTIARY_STRATUM_ID_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(TertiaryStratumId);
                            dr[Strings.DATASHEET_AMOUNT_COLUMN_NAME] = Convert.ToDouble(o, CultureInfo.InvariantCulture);

                            this.m_OutputStratumAmountTable.Rows.Add(dr);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Writes the summary state class tabular data
        /// </summary>
        /// <param name="table"></param>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <remarks></remarks>
        private void WriteSummaryStateClassTabularData(DataTable table, int iteration, int timestep)
        {
            if (this.m_CreateSummaryStateClassOutput)
            {
                if (this.m_SummaryStateClassZeroValues)
                {
                    Dictionary<int, bool> SSKeys = this.CreateSecondaryStratumDictionary();
                    Dictionary<int, bool> TSKeys = this.CreateTertiaryStratumDictionary();

                    Debug.Assert(!(SSKeys.Count == 0) && this.m_SummaryStratumStateResults.Count > 0);
                    Debug.Assert(!(TSKeys.Count == 0) && this.m_SummaryStratumStateResults.Count > 0);

                    foreach (int ss in SSKeys.Keys)
                    {
                        foreach (int ts in TSKeys.Keys)
                        {
                            foreach (DeterministicTransition dt in this.m_DeterministicTransitions)
                            {
                                SixIntegerLookupKey key = new SixIntegerLookupKey(
                                    LookupKeyUtils.GetOutputCollectionKey(dt.StratumIdSource),
                                    ss, ts, iteration, timestep, dt.StateClassIdSource);

                                if (!this.m_SummaryStratumStateResultsZeroValues.Contains(key))
                                {
                                    OutputStratumState oss = new OutputStratumState(
                                        LookupKeyUtils.GetOutputCollectionKey(dt.StratumIdSource),
                                        ss, ts, iteration, timestep, dt.StateClassIdSource, dt.AgeMinimum, dt.AgeMaximum, 0, 0.0);

                                    SevenIntegerLookupKey k2 = new SevenIntegerLookupKey(
                                        LookupKeyUtils.GetOutputCollectionKey(dt.StratumIdSource),
                                        ss, ts, iteration, timestep, dt.StateClassIdSource, 0);

                                    if (!this.m_SummaryStratumStateResults.Contains(k2))
                                    {
                                        this.m_SummaryStratumStateResults.Add(oss);
                                    }

                                    this.m_SummaryStratumStateResultsZeroValues.Add(oss);
                                }
                            }
                        }
                    }
                }

                foreach (OutputStratumState r in this.m_SummaryStratumStateResults)
                {
                    if (this.IsSummaryStateClassTimestep(r.Timestep))
                    {
                        int slxid = this.m_StateClasses[r.StateClassId].StateLabelXID;
                        int slyid = this.m_StateClasses[r.StateClassId].StateLabelYID;

                        DataRow dr = table.NewRow();

                        dr[Strings.DATASHEET_ITERATION_COLUMN_NAME] = r.Iteration;
                        dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME] = r.Timestep;
                        dr[Strings.DATASHEET_STRATUM_ID_COLUMN_NAME] = r.StratumId;
                        dr[Strings.DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(r.SecondaryStratumId);
                        dr[Strings.DATASHEET_TERTIARY_STRATUM_ID_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(r.TertiaryStratumId);
                        dr[Strings.DATASHEET_STATECLASS_ID_COLUMN_NAME] = r.StateClassId;
                        dr[Strings.DATASHEET_STATECLASS_STATE_LABEL_X_ID_COLUMN_NAME] = slxid;
                        dr[Strings.DATASHEET_STATECLASS_STATE_LABEL_Y_ID_COLUMN_NAME] = slyid;
                        dr[Strings.DATASHEET_AGE_MIN_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(r.AgeMin);
                        dr[Strings.DATASHEET_AGE_MAX_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(r.AgeMax);
                        dr[Strings.DATASHEET_AGE_CLASS_COLUMN_NAME] = DBNull.Value;
                        dr[Strings.DATASHEET_AMOUNT_COLUMN_NAME] = r.Amount;

                        table.Rows.Add(dr);
                    }
                }
            }

            this.m_SummaryStratumStateResults.Clear();
            this.m_SummaryStratumStateResultsZeroValues.Clear();
        }

        /// <summary>
        /// Writes the summary transition tabular data
        /// </summary>
        /// <param name="table"></param>
        /// <remarks></remarks>
        private void WriteSummaryTransitionTabularData(int timestep, DataTable table)
        {
            if (!this.IsSummaryTransitionTimestep(timestep))
            {
                return;
            }

            foreach (OutputStratumTransition r in this.m_SummaryStratumTransitionResults)
            {
                double AmountToReport = 0;

                if (this.m_SummaryTransitionOutputAsIntervalMean)
                {
                    if (timestep == (this.m_TimestepZero + 1))
                    {
                        return;
                    }

                    double divisor = this.m_SummaryTransitionOutputTimesteps;

                    if (r.Timestep == this.MaximumTimestep)
                    {
                        if ((r.Timestep % this.m_SummaryTransitionOutputTimesteps) != 0)
                        {
                            divisor = ((r.Timestep - this.m_TimestepZero) % this.m_SummaryTransitionOutputTimesteps);
                        }
                    }

                    AmountToReport = r.Amount / divisor;
                }
                else
                {
                    AmountToReport = r.Amount;
                }

                DataRow dr = table.NewRow();

                dr[Strings.DATASHEET_ITERATION_COLUMN_NAME] = r.Iteration;
                dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME] = r.Timestep;
                dr[Strings.DATASHEET_STRATUM_ID_COLUMN_NAME] = r.StratumId;
                dr[Strings.DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(r.SecondaryStratumId);
                dr[Strings.DATASHEET_TERTIARY_STRATUM_ID_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(r.TertiaryStratumId);
                dr[Strings.DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME] = r.TransitionGroupId;
                dr[Strings.DATASHEET_AGE_MIN_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(r.AgeMin);
                dr[Strings.DATASHEET_AGE_MAX_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(r.AgeMax);
                dr[Strings.DATASHEET_AGE_CLASS_COLUMN_NAME] = DBNull.Value;
                dr[Strings.DATASHEET_SIZE_CLASS_ID_COLUMN_NAME] = DBNull.Value;
                dr[Strings.DATASHEET_EVENT_ID_COLUMN_NAME] = DBNull.Value;
                dr[Strings.DATASHEET_AMOUNT_COLUMN_NAME] = AmountToReport;

                if (r.EventId.HasValue)
                {
                    object SCValue = this.m_SizeClassHelper.GetSizeClassDatabaseValue(AmountToReport);
                    dr[Strings.DATASHEET_SIZE_CLASS_ID_COLUMN_NAME] = SCValue;

                    if (SCValue != DBNull.Value)
                    {
                        Debug.Assert(r.EventId.HasValue);
                        dr[Strings.DATASHEET_EVENT_ID_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(r.EventId);
                    }
                }

                table.Rows.Add(dr);
            }

            this.m_SummaryStratumTransitionResults.Clear();
        }

        /// <summary>
        /// Writes the summary transition state class tabular data
        /// </summary>
        /// <param name="table"></param>
        /// <remarks></remarks>
        private void WriteSummaryTransitionStateTabularData(DataTable table)
        {
            if (this.m_CreateSummaryTransitionByStateClassOutput)
            {
                foreach (OutputStratumTransitionState r in this.m_SummaryStratumTransitionStateResults)
                {
                    DataRow dr = table.NewRow();

                    dr[Strings.DATASHEET_ITERATION_COLUMN_NAME] = r.Iteration;
                    dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME] = r.Timestep;
                    dr[Strings.DATASHEET_STRATUM_ID_COLUMN_NAME] = r.StratumId;
                    dr[Strings.DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(r.SecondaryStratumId);
                    dr[Strings.DATASHEET_TERTIARY_STRATUM_ID_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(r.TertiaryStratumId);
                    dr[Strings.DATASHEET_TRANSITION_TYPE_ID_COLUMN_NAME] = r.TransitionTypeId;
                    dr[Strings.DATASHEET_STATECLASS_ID_COLUMN_NAME] = r.StateClassId;
                    dr[Strings.DATASHEET_END_STATECLASS_ID_COLUMN_NAME] = r.EndStateClassId;
                    dr[Strings.DATASHEET_AMOUNT_COLUMN_NAME] = r.Amount;

                    table.Rows.Add(dr);
                }
            }

            this.m_SummaryStratumTransitionStateResults.Clear();
        }

        /// <summary>
        /// Writes the summary state attribute tabular data
        /// </summary>
        /// <param name="table"></param>
        /// <remarks></remarks>
        private void WriteSummaryStateAttributeTabularData(DataTable table)
        {
            if (this.m_CreateSummaryStateAttributeOutput)
            {
                foreach (OutputStateAttribute r in this.m_SummaryStateAttributeResults)
                {
                    DataRow dr = table.NewRow();

                    dr[Strings.DATASHEET_ITERATION_COLUMN_NAME] = r.Iteration;
                    dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME] = r.Timestep;
                    dr[Strings.DATASHEET_STRATUM_ID_COLUMN_NAME] = r.StratumId;
                    dr[Strings.DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(r.SecondaryStratumId);
                    dr[Strings.DATASHEET_TERTIARY_STRATUM_ID_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(r.TertiaryStratumId);
                    dr[Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_ID_COLUMN_NAME] = r.StateAttributeTypeId;
                    dr[Strings.DATASHEET_AGE_MIN_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(r.AgeMin);
                    dr[Strings.DATASHEET_AGE_MAX_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(r.AgeMax);
                    dr[Strings.DATASHEET_AGE_CLASS_COLUMN_NAME] = DBNull.Value;
                    dr[Strings.DATASHEET_AMOUNT_COLUMN_NAME] = r.Amount;

                    table.Rows.Add(dr);
                }
            }

            this.m_SummaryStateAttributeResults.Clear();
        }

        /// <summary>
        /// Writes the summary transition attribute tabular data
        /// </summary>
        /// <param name="table"></param>
        /// <remarks></remarks>
        private void WriteSummaryTransitionAttributeTabularData(DataTable table)
        {
            if (this.m_CreateSummaryTransitionAttributeOutput)
            {
                foreach (OutputTransitionAttribute r in this.m_SummaryTransitionAttributeResults)
                {
                    DataRow dr = table.NewRow();

                    dr[Strings.DATASHEET_ITERATION_COLUMN_NAME] = r.Iteration;
                    dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME] = r.Timestep;
                    dr[Strings.DATASHEET_STRATUM_ID_COLUMN_NAME] = r.StratumId;
                    dr[Strings.DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(r.SecondaryStratumId);
                    dr[Strings.DATASHEET_TERTIARY_STRATUM_ID_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(r.TertiaryStratumId);
                    dr[Strings.DATASHEET_TRANSITION_ATTRIBUTE_TYPE_ID_COLUMN_NAME] = r.TransitionAttributeTypeId;
                    dr[Strings.DATASHEET_AGE_MIN_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(r.AgeMin);
                    dr[Strings.DATASHEET_AGE_MAX_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(r.AgeMax);
                    dr[Strings.DATASHEET_AGE_CLASS_COLUMN_NAME] = DBNull.Value;
                    dr[Strings.DATASHEET_AMOUNT_COLUMN_NAME] = r.Amount;

                    table.Rows.Add(dr);
                }
            }

            this.m_SummaryTransitionAttributeResults.Clear();
        }

        /// <summary>
        /// Writes a state class raster for the specified iteration and timestep
        /// </summary>
        /// <remarks></remarks>
        private void WriteStateClassRaster(int iteration, int timestep)
        {
            if (!this.IsSpatial)
            {
                return;
            }

            if (this.IsRasterStateClassTimestep(timestep))
            {
                StochasticTimeRaster rastOutput = this.m_InputRasters.CreateOutputRaster(RasterDataType.DTInteger);

                foreach (Cell c in this.Cells)
                {
                    rastOutput.IntCells[c.CellId] = c.StateClassId;
                }

                //We need to remap the State Class values back to the original Raster values (PK -> ID)
                DataSheet dsRemap = this.Project.GetDataSheet(Strings.DATASHEET_STATECLASS_NAME);

                rastOutput.IntCells = Spatial.RemapRasterCells(
                    rastOutput.IntCells,
                    dsRemap,
                    Strings.DATASHEET_MAPID_COLUMN_NAME,
                    false,
                    Spatial.DefaultNoDataValue);

                Spatial.WriteRasterData(
                    rastOutput,
                    this.ResultScenario.GetDataSheet(Constants.DATASHEET_OUTPUT_SPATIAL_STATE_CLASS),
                    iteration,
                    timestep,
                    null,
                    Constants.SPATIAL_MAP_STATE_CLASS_FILEPREFIX,
                    Constants.DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN);
            }
        }

        /// <summary>
        /// Writes transition group rasters for the specified iteration and timestep
        /// </summary>
        /// <param name="dictTransitionedPixels">A dictionary of arrays of Transition Types 
        /// which occured during the specified specified Interval / Timstep. Keyed by Transition Group Id.</param>
        /// <param name="iteration">The current iteration</param>
        /// <param name="timestep">The current timestep</param>
        /// <remarks></remarks>
        private void WriteTransitionGroupRasters(
            int iteration, 
            int timestep, 
            Dictionary<int, int[]> dictTransitionedPixels)
        {
            if (!this.IsRasterTransitionTimestep(timestep))
            {
                return;
            }

            //Loop thru the Transition Groups found in the dictionary
            foreach (int transitionGroupId in dictTransitionedPixels.Keys)
            {
                int[] transitionedPixels = dictTransitionedPixels[transitionGroupId];
                var distArray = transitionedPixels.Distinct();

                if (distArray.Count() == 1)
                {
                    var el0 = distArray.ElementAt(0);

                    if (el0.Equals(Spatial.DefaultNoDataValue))
                    {
                        continue;
                    }
                }

                StochasticTimeRaster rastOP = this.m_InputRasters.CreateOutputRaster(RasterDataType.DTInteger);
                int[] arr = rastOP.IntCells;

                foreach (Cell c in this.Cells)
                {
                    arr[c.CellId] = transitionedPixels[c.CollectionIndex];
                }

                Spatial.WriteRasterData(
                    rastOP, 
                    this.ResultScenario.GetDataSheet(Constants.DATASHEET_OUTPUT_SPATIAL_TRANSITION), 
                    iteration, 
                    timestep, 
                    transitionGroupId, 
                    Constants.SPATIAL_MAP_TRANSITION_GROUP_FILEPREFIX, 
                    Constants.DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN);            
            }
        }

        /// <summary>
        /// Creates a raster file as a snapshot of the current Cell Age values.
        /// </summary>
        /// <remarks></remarks>
        private void WriteAgeRaster(int iteration, int timestep)
        {
            if (!this.IsSpatial)
            {
                return;
            }

            if (this.IsRasterAgeTimestep(timestep))
            {
                StochasticTimeRaster rastOutput = this.m_InputRasters.CreateOutputRaster(RasterDataType.DTInteger);

                // Fetch the raster data from the Cells collection
                foreach (Cell c in this.Cells)
                {
                    rastOutput.IntCells[c.CellId] = c.Age;
                }

                Spatial.WriteRasterData(
                    rastOutput,
                    this.ResultScenario.GetDataSheet(Constants.DATASHEET_OUTPUT_SPATIAL_AGE),
                    iteration,
                    timestep,
                     null,
                    Constants.SPATIAL_MAP_AGE_FILEPREFIX,
                    Constants.DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN);
            }
        }

        /// <summary>
        /// Writes a raster for the TST of each transition group
        /// </summary>
        /// <remarks></remarks>
        private void WriteTSTRasters(int iteration, int timestep)
        {
            if (!this.IsSpatial)
            {
                return;
            }

            if (this.IsRasterTstTimestep(timestep))
            {
                foreach (TransitionGroup tg in this.m_TransitionGroups)
                {
                    StochasticTimeRaster rastOutput = this.m_InputRasters.CreateOutputRaster(RasterDataType.DTInteger);

                    foreach (Cell cell in this.Cells)
                    {
                        if (cell.TstValues.Count != 0)
                        {
                            // Make sure the TstValues contains our TransitionGroupId
                            if (cell.TstValues.Contains(tg.TransitionGroupId))
                            {
                                rastOutput.IntCells[cell.CellId] = cell.TstValues[tg.TransitionGroupId].TstValue;
                            }
                        }
                    }

                    // If no values other than NODATAValue in rastOutput, then supress output for this timestep
                    var distArray = rastOutput.IntCells.Distinct();

                    if (distArray.Count() == 1)
                    {
                        var el0 = distArray.ElementAt(0);

                        if (el0.Equals(Spatial.DefaultNoDataValue))
                        {
                            continue;
                        }
                    }

                    Spatial.WriteRasterData(
                        rastOutput,
                        this.ResultScenario.GetDataSheet(Constants.DATASHEET_OUTPUT_SPATIAL_TST),
                        iteration,
                        timestep,
                        tg.TransitionGroupId,
                        Constants.SPATIAL_MAP_TST_FILEPREFIX,
                        Constants.DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN);
                }
            }
        }

        /// <summary>
        /// Creates a raster file as a snapshot of the current Cell stratum values.
        /// </summary>
        /// <remarks></remarks>
        private void WriteStratumRaster(int iteration, int timestep)
        {
            if (!this.IsSpatial)
            {
                return;
            }

            if (this.IsRasterStratumTimestep(timestep))
            {
                StochasticTimeRaster rastOutput = this.m_InputRasters.CreateOutputRaster(RasterDataType.DTInteger);

                foreach (Cell c in this.Cells)
                {
                    // Fetch the raster data from the Cells collection
                    rastOutput.IntCells[c.CellId] = c.StratumId;
                }

                // We need to remap the Stratum values back to the original Raster values ( PK - > ID)
                DataSheet dsRemap = this.Project.GetDataSheet(Strings.DATASHEET_STRATA_NAME);

                //DEVNOTE: Tom - for now use default NoDataValue during remap. Ideally, we would bring the source files NoDataValue thru.

                rastOutput.IntCells = Spatial.RemapRasterCells(
                    rastOutput.IntCells,
                    dsRemap,
                    Strings.DATASHEET_MAPID_COLUMN_NAME,
                    false,
                    Spatial.DefaultNoDataValue);

                Spatial.WriteRasterData(
                    rastOutput,
                    this.ResultScenario.GetDataSheet(Constants.DATASHEET_OUTPUT_SPATIAL_STRATUM),
                    iteration,
                    timestep,
                    null,
                    Constants.SPATIAL_MAP_STRATUM_FILEPREFIX,
                    Constants.DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN);
            }
        }

        /// <summary>
        /// Writes state attribute rasters
        /// </summary>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <remarks></remarks>
        private void WriteStateAttributeRasters(int iteration, int timestep)
        {
            if (!this.IsSpatial)
            {
                return;
            }

            if (this.IsRasterStateAttributeTimestep(timestep))
            {
                StochasticTimeRaster rastOutput = this.m_InputRasters.CreateOutputRaster(RasterDataType.DTDouble);

                foreach (int AttributeTypeId in this.m_StateAttributeTypeIdsNoAges.Keys)
                {
                    rastOutput.InitDblCells();

                    foreach (Cell c in this.Cells)
                    {
                        double? AttrValue = this.m_StateAttributeValueMapNoAges.GetAttributeValueNoAge(
                            AttributeTypeId, c.StratumId, c.SecondaryStratumId, c.TertiaryStratumId, c.StateClassId, iteration, timestep);

                        //If no value, then use NO_DATA (initialized above), otherwise AttrValue

                        if (AttrValue != null)
                        {
                            rastOutput.DblCells[c.CellId] = Convert.ToDouble(AttrValue, CultureInfo.InvariantCulture);
                        }
                    }

                    Spatial.WriteRasterData(
                        rastOutput,
                        this.ResultScenario.GetDataSheet(Constants.DATASHEET_OUTPUT_SPATIAL_STATE_ATTRIBUTE),
                        iteration,
                        timestep,
                        AttributeTypeId,
                        Constants.SPATIAL_MAP_STATE_ATTRIBUTE_FILEPREFIX,
                        Constants.DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN);
                }

                foreach (int AttributeTypeId in this.m_StateAttributeTypeIdsAges.Keys)
                {
                    rastOutput.InitDblCells();

                    foreach (Cell c in this.Cells)
                    {
                        double? AttrValue = this.m_StateAttributeValueMapAges.GetAttributeValueByAge(
                            AttributeTypeId, c.StratumId, c.SecondaryStratumId, c.TertiaryStratumId, c.StateClassId, iteration, timestep, c.Age);

                        //If no value, then use NO_DATA, otherwise AttrValue

                        if (AttrValue != null)
                        {
                            rastOutput.DblCells[c.CellId] = Convert.ToDouble(AttrValue, CultureInfo.InvariantCulture);
                        }
                    }

                    Spatial.WriteRasterData(
                        rastOutput,
                        this.ResultScenario.GetDataSheet(Constants.DATASHEET_OUTPUT_SPATIAL_STATE_ATTRIBUTE),
                        iteration,
                        timestep,
                        AttributeTypeId,
                        Constants.SPATIAL_MAP_STATE_ATTRIBUTE_FILEPREFIX,
                        Constants.DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN);
                }
            }
        }

        /// <summary>
        /// Writes transition attribute changes for the specified iteration and timestep.
        /// </summary>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <param name="rasterTransitionAttrValues"></param>
        private void WriteTransitionAttributeRasters(
            int iteration,
            int timestep,
            Dictionary<int, double[]> rasterTransitionAttrValues)
        {
            if (this.IsRasterTransitionAttributeTimestep(timestep))
            {
                foreach (int AttributeId in rasterTransitionAttrValues.Keys)
                {
                    StochasticTimeRaster rastOP = this.m_InputRasters.CreateOutputRaster(RasterDataType.DTDouble);
                    double[] NewValues = rasterTransitionAttrValues[AttributeId];
                    double[] arr = rastOP.DblCells;

                    foreach (Cell c in this.Cells)
                    {
                        arr[c.CellId] = NewValues[c.CollectionIndex];
                    }

                    Spatial.WriteRasterData(
                        rastOP,
                        this.ResultScenario.GetDataSheet(Constants.DATASHEET_OUTPUT_SPATIAL_TRANSITION_ATTRIBUTE),
                        iteration,
                        timestep,
                        AttributeId,
                        Constants.SPATIAL_MAP_TRANSITION_ATTRIBUTE_FILEPREFIX,
                        Constants.DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN);
                }
            }
        }

        /// <summary>
        /// Writes transition event rasters for the specified iteration and timestep
        /// </summary>
        /// <param name="dictTransitionedPixels">A dictionary of arrays of Transition Types 
        /// which occured during the specified specified Interval / Timstep. Keyed by Transition Group Id.</param>
        /// <param name="iteration">The current iteration</param>
        /// <param name="timestep">The current timestep</param>
        /// <remarks></remarks>
        private void WriteTransitionEventRasters(
            int iteration,
            int timestep,
            Dictionary<int, int[]> dictTransitionedPixels)
        {
            if (!this.IsRasterTransitionEventTimestep(timestep))
            {
                return;
            }

            //Loop thru the Transition Groups found in the dictionary
            foreach (int transitionGroupId in dictTransitionedPixels.Keys)
            {
                int[] transitionedPixels = dictTransitionedPixels[transitionGroupId];
                var distArray = transitionedPixels.Distinct();

                if (distArray.Count() == 1)
                {
                    var el0 = distArray.ElementAt(0);

                    if (el0.Equals(Spatial.DefaultNoDataValue))
                    {
                        continue;
                    }
                }

                StochasticTimeRaster rastOP = this.m_InputRasters.CreateOutputRaster(RasterDataType.DTInteger);
                int[] arr = rastOP.IntCells;

                foreach (Cell c in this.Cells)
                {
                    arr[c.CellId] = transitionedPixels[c.CollectionIndex];
                }

                Spatial.WriteRasterData(
                    rastOP,
                    this.ResultScenario.GetDataSheet(Constants.DATASHEET_OUTPUT_SPATIAL_TRANSITION_EVENT),
                    iteration,
                    timestep,
                    transitionGroupId,
                    Constants.SPATIAL_MAP_TRANSITION_EVENT_FILEPREFIX,
                    Constants.DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN);
            }
        }

        /// <summary>
        /// Writes the average state class rasters
        /// </summary>
        private void WriteAvgStateClassRasters()
        {
            if (!this.IsSpatial)
            {
                return;
            }

            if (!this.m_CreateAvgRasterStateClassOutput)
            {
                return;
            }

            foreach (int StateClassId in this.m_AvgStateClassMap.Keys)
            {
                Dictionary<int, double[]> dict = this.m_AvgStateClassMap[StateClassId];

                foreach (int timestep in dict.Keys)
                {
                    double[] Values = dict[timestep];
                    var DistVals = Values.Distinct();

                    if (DistVals.Count() == 1)
                    {
                        var el0 = DistVals.ElementAt(0);

                        if (el0.Equals(0.0))
                        {
                            continue;
                        }
                    }

                    StochasticTimeRaster RastOutput = this.m_InputRasters.CreateOutputRaster(RasterDataType.DTDouble);
                    double[] arr = RastOutput.DblCells;

                    foreach (Cell c in this.Cells)
                    {
                        arr[c.CellId] = Values[c.CollectionIndex];
                    }

                    Spatial.WriteRasterData(
                        RastOutput,
                        this.ResultScenario.GetDataSheet(Constants.DATASHEET_OUTPUT_AVG_SPATIAL_STATE_CLASS),
                        0,
                        timestep,
                        StateClassId,
                        Constants.SPATIAL_MAP_AVG_STATE_CLASS_FILEPREFIX,
                        Constants.DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN);
                }
            }
        }

        /// <summary>
        /// Writes the average age rasters
        /// </summary>
        private void WriteAvgAgeRasters()
        {
            foreach (int timestep in this.m_AvgAgeMap.Keys)
            {
                double[] Values = this.m_AvgAgeMap[timestep];
                StochasticTimeRaster RastOutput = this.m_InputRasters.CreateOutputRaster(RasterDataType.DTDouble);
                double[] arr = RastOutput.DblCells;

                foreach (Cell c in this.Cells)
                {
                    arr[c.CellId] = Values[c.CollectionIndex];
                    Debug.Assert(arr[c.CellId] != Spatial.DefaultNoDataValue);
                }

                Spatial.WriteRasterData(
                    RastOutput,
                    this.ResultScenario.GetDataSheet(Constants.DATASHEET_OUTPUT_AVG_SPATIAL_AGE),
                    0,
                    timestep,
                    null,
                    Constants.SPATIAL_MAP_AVG_AGE_FILEPREFIX,
                    Constants.DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN);
            }
        }

        /// <summary>
        /// Writes the average stratum rasters
        /// </summary>
        private void WriteAvgStratumRasters()
        {
            if (!this.IsSpatial)
            {
                return;
            }

            if (!this.m_CreateAvgRasterStratumOutput)
            {
                return;
            }

            foreach (int StratumId in this.m_AvgStratumMap.Keys)
            {
                Dictionary<int, double[]> dict = this.m_AvgStratumMap[StratumId];

                foreach (int timestep in dict.Keys)
                {
                    double[] Values = dict[timestep];
                    var DistVals = Values.Distinct();

                    if (DistVals.Count() == 1)
                    {
                        var el0 = DistVals.ElementAt(0);

                        if (el0.Equals(0.0))
                        {
                            continue;
                        }
                    }

                    StochasticTimeRaster RastOutput = this.m_InputRasters.CreateOutputRaster(RasterDataType.DTDouble);
                    double[] arr = RastOutput.DblCells;

                    foreach (Cell c in this.Cells)
                    {
                        arr[c.CellId] = Values[c.CollectionIndex];
                    }

                    Spatial.WriteRasterData(
                        RastOutput,
                        this.ResultScenario.GetDataSheet(Constants.DATASHEET_OUTPUT_AVG_SPATIAL_STRATUM),
                        0,
                        timestep,
                        StratumId,
                        Constants.SPATIAL_MAP_AVG_STRATUM_FILEPREFIX,
                        Constants.DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN);
                }
            }
        }

        /// <summary>
        /// Writes the Average Transition Probability rasters.
        /// </summary>
        /// <remarks></remarks>
        private void WriteAvgTransitionProbabiltyRasters()
        {
            if (!this.IsSpatial)
            {
                return;
            }

            if (!this.m_CreateAvgRasterTransitionProbOutput)
            {
                return;
            }

            foreach (int tgId in this.m_AvgTransitionProbMap.Keys)
            {
                Dictionary<int, double[]> dict = this.m_AvgTransitionProbMap[tgId];

                // Now lets loop thru the timestep arrays in the dict
                foreach (int timestep in dict.Keys)
                {
                    double[] Values = dict[timestep];

                    //Dont bother writing out any array thats all DEFAULT_NO_DATA_VALUEs or 0's
                    var DistVals = Values.Distinct();

                    if (DistVals.Count() == 1)
                    {
                        Debug.Print("Skipping Average Transition Probabilities output for TG {0} / Timestep {1} as no non-DEFAULT_NO_DATA_VALUE values found.", tgId, timestep);
                        continue;
                    }
                    else if (DistVals.Count() == 2)
                    {
                        if (DistVals.ElementAt(0) <= 0 && DistVals.ElementAt(1) <= 0)
                        {
                            Debug.Print("Skipping Average Transition Probabilities output for TG {0} / Timestep {1} as no non-DEFAULT_NO_DATA_VALUE values found.", tgId, timestep);
                            continue;
                        }
                    }

                    StochasticTimeRaster RastOutput = this.m_InputRasters.CreateOutputRaster(RasterDataType.DTDouble);
                    double[] arr = RastOutput.DblCells;

                    foreach (Cell c in this.Cells)
                    {
                        arr[c.CellId] = Values[c.CollectionIndex];
                    }

                    Spatial.WriteRasterData(
                        RastOutput,
                        this.ResultScenario.GetDataSheet(Constants.DATASHEET_OUTPUT_AVG_SPATIAL_TRANSITION_PROBABILITY),
                        0,
                        timestep,
                        tgId,
                        Constants.SPATIAL_MAP_AVG_TRANSITION_PROBABILITY_FILEPREFIX,
                        Constants.DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN);
                }
            }
        }

        /// <summary>
        /// Writes the Average TST rasters.
        /// </summary>
        private void WriteAvgTSTRasters()
        {
            if (!this.IsSpatial)
            {
                return;
            }

            if (!this.m_CreateAvgRasterTSTOutput)
            {
                return;
            }

            foreach (int TransitionGroupId in this.m_AvgTSTMap.Keys)
            {
                Dictionary<int, double[]> dict = this.m_AvgTSTMap[TransitionGroupId];

                foreach (int timestep in dict.Keys)
                {
                    double[] Values = dict[timestep];
                    StochasticTimeRaster RastOutput = this.m_InputRasters.CreateOutputRaster(RasterDataType.DTDouble);
                    double[] arr = RastOutput.DblCells;

                    foreach (Cell c in this.Cells)
                    {
                        arr[c.CellId] = Values[c.CollectionIndex];
                    }

                    Spatial.WriteRasterData(
                        RastOutput,
                        this.ResultScenario.GetDataSheet(Constants.DATASHEET_OUTPUT_AVG_SPATIAL_TST),
                        0,
                        timestep,
                        TransitionGroupId,
                        Constants.SPATIAL_MAP_AVG_TST_FILEPREFIX,
                        Constants.DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN);
                }
            }
        }

        /// <summary>
        /// Writes the Average State Attribute rasters.
        /// </summary>
        private void WriteAvgStateAttributeRasters()
        {
            if (!this.IsSpatial)
            {
                return;
            }

            if (!this.m_CreateAvgRasterStateAttributeOutput)
            {
                return;
            }

            foreach (int AttrId in this.m_AvgStateAttrMap.Keys)
            {
                Dictionary<int, double[]> dict = this.m_AvgStateAttrMap[AttrId];

                foreach (int timestep in dict.Keys)
                {
                    double[] Values = dict[timestep];
                    var DistVals = Values.Distinct();

                    if (DistVals.Count() == 1)
                    {
                        var el0 = DistVals.ElementAt(0);

                        if (el0.Equals(Spatial.DefaultNoDataValue))
                        {
                            continue;
                        }
                    }

                    StochasticTimeRaster RastOutput = this.m_InputRasters.CreateOutputRaster(RasterDataType.DTDouble);
                    double[] arr = RastOutput.DblCells;

                    foreach (Cell c in this.Cells)
                    {
                        arr[c.CellId] = Values[c.CollectionIndex];
                    }

                    Spatial.WriteRasterData(
                        RastOutput,
                        this.ResultScenario.GetDataSheet(Constants.DATASHEET_OUTPUT_AVG_SPATIAL_STATE_ATTRIBUTE),
                        0,
                        timestep,
                        AttrId,
                        Constants.SPATIAL_MAP_AVG_STATE_ATTRIBUTE_FILEPREFIX,
                        Constants.DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN);
                }
            }
        }

        /// <summary>
        /// Writes the Average Transition Attribute rasters.
        /// </summary>
        private void WriteAvgTransitionAttributeRasters()
        {
            if (!this.IsSpatial)
            {
                return;
            }

            if (!this.m_CreateAvgRasterTransitionAttributeOutput)
            {
                return;
            }

            foreach (int AttrId in this.m_AvgTransitionAttrMap.Keys)
            {
                Dictionary<int, double[]> dict = this.m_AvgTransitionAttrMap[AttrId];

                foreach (int timestep in dict.Keys)
                {
                    double[] Values = dict[timestep];
                    var DistVals = Values.Distinct();

                    if (DistVals.Count() == 1)
                    {
                        var el0 = DistVals.ElementAt(0);

                        if (el0.Equals(Spatial.DefaultNoDataValue))
                        {
                            continue;
                        }
                    }

                    StochasticTimeRaster RastOutput = this.m_InputRasters.CreateOutputRaster(RasterDataType.DTDouble);
                    double[] arr = RastOutput.DblCells;

                    foreach (Cell c in this.Cells)
                    {
                        arr[c.CellId] = Values[c.CollectionIndex];
                    }

                    Spatial.WriteRasterData(
                        RastOutput,
                        this.ResultScenario.GetDataSheet(Constants.DATASHEET_OUTPUT_AVG_SPATIAL_TRANSITION_ATTRIBUTE),
                        0,
                        timestep,
                        AttrId,
                        Constants.SPATIAL_MAP_AVG_TRANSITION_ATTRIBUTE_FILEPREFIX,
                        Constants.DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN);
                }
            }
        }

        /// <summary>
        /// Generates state class attributes for the current summary state class records
        /// </summary>
        /// <remarks></remarks>
        private void GenerateStateClassAttributes()
        {
            if (this.m_StateAttributeTypeIdsNoAges.Count == 0)
            {
                Debug.Assert(!this.m_StateAttributeValueMapNoAges.HasItems);
                return;
            }

            foreach (OutputStratumState oss in this.m_SummaryStratumStateResults)
            {
                int AgeKey = Constants.AGE_KEY_NO_AGES;

                if (oss.AgeMin.HasValue)
                {
                    AgeKey = this.m_AgeReportingHelperSC.GetKey(oss.AgeMin.Value);
                }

                foreach (int AttributeTypeId in this.m_StateAttributeTypeIdsNoAges.Keys)
                {
                    double? AttrValue = this.m_StateAttributeValueMapNoAges.GetAttributeValueNoAge(
                        AttributeTypeId, 
                        oss.StratumId, 
                        GetSecondaryStratumIdValue(oss.SecondaryStratumId), 
                        GetTertiaryStratumIdValue(oss.TertiaryStratumId), 
                        oss.StateClassId, 
                        oss.Iteration, 
                        oss.Timestep);

                    if (AttrValue.HasValue)
                    {
                        SevenIntegerLookupKey key = new SevenIntegerLookupKey(
                            oss.StratumId, 
                            GetSecondaryStratumIdKey(oss.SecondaryStratumId), 
                            GetTertiaryStratumIdKey(oss.TertiaryStratumId), 
                            oss.Iteration, 
                            oss.Timestep, 
                            AttributeTypeId, 
                            AgeKey);

                        if (this.m_SummaryStateAttributeResults.Contains(key))
                        {
                            OutputStateAttribute osa = this.m_SummaryStateAttributeResults[key];
                            osa.Amount += (oss.Amount * AttrValue.Value);
                        }
                        else
                        {
                            OutputStateAttribute osa = new OutputStateAttribute(
                                oss.StratumId, 
                                GetSecondaryStratumIdValue(oss.SecondaryStratumId),
                                GetTertiaryStratumIdValue(oss.TertiaryStratumId), 
                                oss.Iteration, 
                                oss.Timestep, 
                                AttributeTypeId, 
                                oss.AgeMin, 
                                oss.AgeMax, 
                                AgeKey, 
                                (oss.Amount * AttrValue.Value));

                            this.m_SummaryStateAttributeResults.Add(osa);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Generates state class attributes for the current summary transition records
        /// </summary>
        /// <param name="simulationCell"></param>
        /// <param name="tr"></param>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <remarks></remarks>
        private void GenerateTransitionAttributes(
            Cell simulationCell, 
            Transition tr, 
            int iteration, 
            int timestep, 
            Dictionary<int, double[]> rasterTransitionAttrValues)
        {
            if (simulationCell.StratumId == 0 || simulationCell.StateClassId == 0)
            {
                return;
            }

            if (!this.m_TransitionAttributeValueMap.HasItems)
            {
                return;
            }

            TransitionType tt = this.m_TransitionTypes[tr.TransitionTypeId];

            foreach (int AttributeTypeId in this.m_TransitionAttributeTypeIds.Keys)
            {
                foreach (TransitionGroup tg in tt.TransitionGroups)
                {
                    double? AttrValue = this.m_TransitionAttributeValueMap.GetAttributeValue(
                        AttributeTypeId,
                        tg.TransitionGroupId, 
                        simulationCell.StratumId, 
                        GetSecondaryStratumIdValue(simulationCell), 
                        GetTertiaryStratumIdValue(simulationCell), 
                        simulationCell.StateClassId, 
                        iteration, 
                        timestep, 
                        simulationCell.Age);

                    if (AttrValue.HasValue)
                    {
                        bool IsAttrTimestep =
                            this.IsRasterTransitionAttributeTimestep(timestep) ||
                            this.IsAvgRasterTransitionAttributeTimestep(timestep);

                        if (this.IsSpatial && IsAttrTimestep)
                        {
                            double[] arr = rasterTransitionAttrValues[AttributeTypeId];
                            if (arr[simulationCell.CollectionIndex] == Spatial.DefaultNoDataValue)
                            {
                                arr[simulationCell.CollectionIndex] = AttrValue.Value;
                            }
                            else
                            {
                                arr[simulationCell.CollectionIndex] += AttrValue.Value;
                            }
                        }

                        TransitionAttributeTarget Target = this.m_TransitionAttributeTargetMap.GetAttributeTarget(
                            AttributeTypeId, 
                            simulationCell.StratumId, 
                            simulationCell.SecondaryStratumId,
                            simulationCell.TertiaryStratumId, 
                            iteration, 
                            timestep);

                        if (Target != null && !Target.IsDisabled)
                        {
                            Target.TargetRemaining -= AttrValue.Value * m_AmountPerCell;

                            if (Target.TargetRemaining < 0.0)
                            {
                                Target.TargetRemaining = 0.0;
                            }
                        }

                        if (this.IsSummaryTransitionAttributeTimestep(timestep))
                        {
                            int AgeKey = this.m_AgeReportingHelperTA.GetKey(simulationCell.Age);

                            SevenIntegerLookupKey key = new SevenIntegerLookupKey(
                                simulationCell.StratumId, 
                                GetSecondaryStratumIdKey(simulationCell), 
                                GetTertiaryStratumIdKey(simulationCell), 
                                iteration, 
                                timestep, 
                                AttributeTypeId, 
                                AgeKey);

                            if (this.m_SummaryTransitionAttributeResults.Contains(key))
                            {
                                OutputTransitionAttribute ota = this.m_SummaryTransitionAttributeResults[key];
                                ota.Amount += (this.m_AmountPerCell * AttrValue.Value);
                            }
                            else
                            {
                                OutputTransitionAttribute ota = new OutputTransitionAttribute(
                                    simulationCell.StratumId, 
                                    GetSecondaryStratumIdValue(simulationCell.SecondaryStratumId), 
                                    GetTertiaryStratumIdValue(simulationCell.TertiaryStratumId), 
                                    iteration,
                                    timestep,
                                    AttributeTypeId, 
                                    this.m_AgeReportingHelperTA.GetAgeMinimum(simulationCell.Age), 
                                    this.m_AgeReportingHelperTA.GetAgeMaximum(simulationCell.Age), 
                                    AgeKey, 
                                    (this.m_AmountPerCell * AttrValue.Value));

                                this.m_SummaryTransitionAttributeResults.Add(ota);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Process Transition Adjacency State Attribute Output
        /// </summary>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <remarks>
        /// At the Landscape Update Frequency specified above generate a raster of the state attribute in question 
        /// and then do a moving window analysis of the raster such that for each cell the average value of the state 
        /// attribute within it’s neighborhood radius is calculated. Create an in memory raster array of the moving 
        /// window analysis results. Hold on to this raster in memory (as a single dimensional array) which can be 
        /// accessed when needed.
        /// </remarks>
        private void ProcessTransitionAdjacencyStateAttributeOutput(int iteration, int timestep)
        {
            if (!this.IsSpatial)
            {
                return;
            }

            // Loop thru all the Transition Adjacency Settings records. Each can have update frequencies.

            int cellRow = 0;
            int cellColumn = 0;

            foreach (TransitionAdjacencySetting setting in this.m_TransitionAdjacencySettings)
            {
                if (this.IsTransitionAdjacencyStateAttributeTimestep(timestep, setting.TransitionGroupId))
                {
                    // Determine the relative neighbors of interest for the specified radius
                    IEnumerable<CellOffset> listNeighbors = InputRasters.GetCellNeighborOffsetsForRadius(setting.NeighborhoodRadius);

                    // Create one cell array per Transition Adjacency Settings record, indexes by TGId
                    double[] CellValues = null;
                    double[] CellAverageValues = null;

                    StateAttributeValueMap stateAttributeValueMap = null;
                    var IsNoAges = false;

                    if (!setting.StateClassId.HasValue)
                    {   
                        // Extract State Attribute values from StateAttributeValueMaps ( just do it once, to enhance performance)
                        // check whether StateAttributeTypeId is in m_StateAttributeTypeIdsNoAges or m_StateAttributeTypeIdsAges. 

                        if (this.m_StateAttributeTypeIdsNoAges.Keys.Contains(setting.StateAttributeTypeId.Value))
                        {
                            stateAttributeValueMap = this.m_StateAttributeValueMapNoAges;
                            IsNoAges = true;
                        }

                        if (this.m_StateAttributeTypeIdsAges.Keys.Contains(setting.StateAttributeTypeId.Value))
                        {
                            stateAttributeValueMap = this.m_StateAttributeValueMapAges;
                        }
                    }

                    if (stateAttributeValueMap != null || setting.StateClassId.HasValue)
                    {
                        CellValues = new double[this.m_InputRasters.NumberCells];

                        for (var i = 0; i < this.m_InputRasters.NumberCells; i++)
                        {
                            CellValues[i] = Spatial.DefaultNoDataValue;
                        }

                        // Loop thru raster and pull out the State Attribute Value for each cell
                        foreach (Cell cell in this.Cells)
                        {
                            // Pull out the values 1st, before doing the neighbor averaging, to get our repeated cost if GetValue.
                            double? CellValue = null;

                            if (!setting.StateClassId.HasValue)
                            {
                                if (IsNoAges)
                                {
                                    CellValue = stateAttributeValueMap.GetAttributeValueNoAge(
                                        setting.StateAttributeTypeId.Value,
                                        cell.StratumId, cell.SecondaryStratumId, cell.TertiaryStratumId, 
                                        cell.StateClassId, iteration, timestep);
                                }
                                else
                                {
                                    CellValue = stateAttributeValueMap.GetAttributeValueByAge(
                                        setting.StateAttributeTypeId.Value,
                                        cell.StratumId, cell.SecondaryStratumId, cell.TertiaryStratumId,
                                        cell.StateClassId, iteration, timestep, cell.Age);
                                }
                            }
                            else
                            {
                                if (cell.StateClassId == setting.StateClassId.Value)
                                {
                                    CellValue = 1.0;
                                }
                                else
                                {
                                    CellValue = 0.0;
                                }
                            }

                            if (CellValue != null)
                            {
                                CellValues[cell.CellId] = Convert.ToDouble(CellValue, CultureInfo.InvariantCulture);
                            }
                        }

                        // Now lets loop thru State Attribute array and generate the neighbor average for each cell
                        double attrValueTotal = 0;
                        int attrValueCnt = 0;

                        CellAverageValues = new double[this.m_InputRasters.NumberCells];

                        for (int i = 0; i < this.m_InputRasters.NumberCells; i++)
                        {
                            attrValueTotal = 0;
                            attrValueCnt = 0;

                            // Calculate row/column once, to eek performance ( small improvement) 
                            this.m_InputRasters.GetRowColForId(i, ref cellRow, ref cellColumn);

                            foreach (CellOffset offset in listNeighbors)
                            {
                                // Convert the relative neighbor into absolute neighbor.

                                int neighborCellId = this.m_InputRasters.GetCellIdByOffset(
                                    cellRow, cellColumn, offset.Row, offset.Column);

                                if (neighborCellId != -1)
                                {
                                    double attrValue = CellValues[neighborCellId];

                                    // If NO_DATA, don't include in the averaging
                                    if (!attrValue.Equals(Spatial.DefaultNoDataValue))
                                    {
                                        attrValueTotal += Convert.ToDouble(attrValue);
                                        attrValueCnt += 1;
                                    }
                                }
                            }

                            if (attrValueCnt > 0)
                            {
                                //Use count of possible neighbors, not actual neghbors (attrValueCnt).
                                CellAverageValues[i] = attrValueTotal / listNeighbors.Count();
                            }
                            else
                            {
                                CellAverageValues[i] = Spatial.DefaultNoDataValue;
                            }
                        }

                        // Remove the old value array from the map, to be replaced with new array
                        this.m_TransitionAdjacencyStateAttributeValueMap.Remove(setting.TransitionGroupId);
                        this.m_TransitionAdjacencyStateAttributeValueMap.Add(setting.TransitionGroupId, CellAverageValues);
                    }
                }
            }
        }

        private int GetTimestepKeyForAcrossTimestepAverage(int currentTimestep, int everyNthTimestep)
        {
            int timestepKey = 0;

            if (currentTimestep == this.MaximumTimestep)
            {
                timestepKey = this.MaximumTimestep;
            }
            else
            {
                //We're looking for the the timestep which is the first one that is >= to the current timestep

                timestepKey = Convert.ToInt32(Math.Ceiling(
                    Convert.ToDouble(currentTimestep - this.TimestepZero) / everyNthTimestep) * everyNthTimestep) +
                        this.TimestepZero;

                if (timestepKey > this.MaximumTimestep)
                {
                    timestepKey = this.MaximumTimestep;
                }
            }

            return timestepKey;
        }

        private Dictionary<int, bool> CreateSecondaryStratumDictionary()
        {
            Dictionary<int, bool> d = new Dictionary<int, bool>();

            foreach (OutputStratumState r in this.m_SummaryStratumStateResults)
            {
                int k = LookupKeyUtils.GetOutputCollectionKey(r.SecondaryStratumId);

                if (!d.ContainsKey(k))
                {
                    d.Add(k, true);
                }
            }

            return d;
        }

        private Dictionary<int, bool> CreateTertiaryStratumDictionary()
        {
            Dictionary<int, bool> d = new Dictionary<int, bool>();

            foreach (OutputStratumState r in this.m_SummaryStratumStateResults)
            {
                int k = LookupKeyUtils.GetOutputCollectionKey(r.TertiaryStratumId);

                if (!d.ContainsKey(k))
                {
                    d.Add(k, true);
                }
            }

            return d;
        }
    }
}
