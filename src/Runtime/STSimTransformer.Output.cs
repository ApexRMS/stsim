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
using SyncroSim.STSim.Shared;

namespace SyncroSim.STSim
{
    public partial class STSimTransformer
    {
        //Output Options
        private bool m_CreateSummaryStateClassOutput;
        private int m_SummaryStateClassOutputTimesteps;
        private bool m_SummaryStateClassOutputAges;
        private bool m_SummaryStateClassZeroValues;
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
        private int m_SummaryTransitionAttributeOutputTimesteps;
        private bool m_SummaryTransitionAttributeOutputAges;
        private bool m_SummaryOmitSecondaryStrata;
        private bool m_SummaryOmitTertiaryStrata;

        private bool m_CreateRasterStateClassOutput;
        private int m_RasterStateClassOutputTimesteps;
        private bool m_CreateRasterTransitionOutput;
        private int m_RasterTransitionOutputTimesteps;
        private bool m_CreateRasterAgeOutput;
        private int m_RasterAgeOutputTimesteps;
        private bool m_CreateRasterStratumOutput;
        private int m_RasterStratumOutputTimesteps;
        private bool m_CreateRasterTstOutput;
        private int m_RasterTstOutputTimesteps;
        private bool m_CreateRasterStateAttributeOutput;
        private int m_RasterStateAttributeOutputTimesteps;
        private bool m_CreateRasterTransitionAttributeOutput;
        private int m_RasterTransitionAttributeOutputTimesteps;
        private bool m_CreateRasterAATPOutput;
        private int m_RasterAATPTimesteps;
        private bool m_CreateRasterTransitionEventOutput;
        private int m_RasterTransitionEventTimesteps;

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
        /// Determines whether or not to do summary state class output for the specified timestep
        /// </summary>
        /// <param name="timestep">The timestep</param>
        /// <returns>
        /// True if the timestep is the first timestep, the last timestep, or the timestep is in the set specified by the user.  
        /// False will be returned if the user has not specified that this type of output should be generated or if the conditions for True are not met.
        /// </returns>
        /// <remarks></remarks>
        private bool IsSummaryStateClassTimestep(int timestep)
        {
            return this.IsOutputTimestep(timestep, this.m_SummaryStateClassOutputTimesteps, this.m_CreateSummaryStateClassOutput);
        }

        /// <summary>
        /// Determines whether or not to do summary transition output for the specified timestep
        /// </summary>
        /// <param name="timestep">The timestep</param>
        /// <returns>
        /// True if the timestep is the first timestep, the last timestep, or the timestep is in the set specified by the user.  
        /// False will be returned if the user has not specified that this type of output should be generated or if the conditions for True are not met.
        /// </returns>
        /// <remarks></remarks>
        private bool IsSummaryTransitionTimestep(int timestep)
        {
            return this.IsOutputTimestep(timestep, this.m_SummaryTransitionOutputTimesteps, this.m_CreateSummaryTransitionOutput);
        }

        /// <summary>
        /// Determines whether or not to do summary transition by state class output for the specified timestep
        /// </summary>
        /// <param name="timestep">The timestep</param>
        /// <returns>
        /// True if the timestep is the first timestep, the last timestep, or the timestep is in the set specified by the user.  
        /// False will be returned if the user has not specified that this type of output should be generated or if the conditions for True are not met.
        /// </returns>
        /// <remarks></remarks>
        private bool IsSummaryTransitionByStateClassTimestep(int timestep)
        {
            return this.IsOutputTimestep(timestep, this.m_SummaryTransitionByStateClassOutputTimesteps, this.m_CreateSummaryTransitionByStateClassOutput);
        }

        /// <summary>
        /// Determines whether or not to do summary state attribute output for the specified timestep
        /// </summary>
        /// <param name="timestep">The timestep</param>
        /// <returns>
        /// True if the timestep is the first timestep, the last timestep, or the timestep is in the set specified by the user.  
        /// False will be returned if the user has not specified that this type of output should be generated or if the conditions for True are not met.
        /// </returns>
        /// <remarks></remarks>
        private bool IsSummaryStateAttributeTimestep(int timestep)
        {
            return this.IsOutputTimestep(timestep, this.m_SummaryStateAttributeOutputTimesteps, this.m_CreateSummaryStateAttributeOutput);
        }

        /// <summary>
        /// Determines whether or not to do summary transition attribute output for the specified timestep
        /// </summary>
        /// <param name="timestep">The timestep</param>
        /// <returns>
        /// True if the timestep is the first timestep, the last timestep, or the timestep is in the set specified by the user.  
        /// False will be returned if the user has not specified that this type of output should be generated or if the conditions for True are not met.
        /// </returns>
        /// <remarks></remarks>
        private bool IsSummaryTransitionAttributeTimestep(int timestep)
        {
            return this.IsOutputTimestep(timestep, this.m_SummaryTransitionAttributeOutputTimesteps, this.m_CreateSummaryTransitionAttributeOutput);
        }

        /// <summary>
        /// Determines whether or not to do Raster Age output for the specified timestep
        /// </summary>
        /// <param name="timestep">The timestep</param>
        /// <returns>
        /// True if the timestep is the first timestep, the last timestep, or the timestep is in the set specified by the user.  
        /// False will be returned if the user has not specified that this type of output should be generated or if the conditions for True are not met.
        /// </returns>
        /// <remarks></remarks>
        private bool IsRasterStateClassTimestep(int timestep)
        {
            return this.IsOutputTimestep(timestep, this.m_RasterStateClassOutputTimesteps, this.m_CreateRasterStateClassOutput);
        }

        /// <summary>
        /// Determines whether or not to do Raster Transition output for the specified timestep
        /// </summary>
        /// <param name="timestep">The timestep</param>
        /// <returns>
        /// True if the timestep is the first timestep, the last timestep, or the timestep is in the set specified by the user.  
        /// False will be returned if the user has not specified that this type of output should be generated or if the conditions for True are not met.
        /// </returns>
        /// <remarks></remarks>
        private bool IsRasterTransitionTimestep(int timestep)
        {
            return this.IsOutputTimestep(timestep, this.m_RasterTransitionOutputTimesteps, this.m_CreateRasterTransitionOutput);
        }

        /// <summary>
        /// Determines whether or not to do Raster Transition Event output for the specified timestep
        /// </summary>
        /// <param name="timestep">The timestep</param>
        /// <returns>
        /// True if the timestep is the first timestep, the last timestep, or the timestep is in the set specified by the user.  
        /// False will be returned if the user has not specified that this type of output should be generated or if the conditions for True are not met.
        /// </returns>
        /// <remarks></remarks>
        private bool IsRasterTransitionEventTimestep(int timestep)
        {
            return this.IsOutputTimestep(timestep, this.m_RasterTransitionEventTimesteps, this.m_CreateRasterTransitionEventOutput);
        }

        /// <summary>
        /// Determines whether or not to do Raster Age output for the specified timestep
        /// </summary>
        /// <param name="timestep">The timestep</param>
        /// <returns>
        /// True if the timestep is the first timestep, the last timestep, or the timestep is in the set specified by the user.  
        /// False will be returned if the user has not specified that this type of output should be generated or if the conditions for True are not met.
        /// </returns>
        /// <remarks></remarks>
        private bool IsRasterAgeTimestep(int timestep)
        {
            return this.IsOutputTimestep(timestep, this.m_RasterAgeOutputTimesteps, this.m_CreateRasterAgeOutput);
        }

        /// <summary>
        /// Determines whether or not to do Raster Tst output for the specified timestep
        /// </summary>
        /// <param name="timestep">The timestep</param>
        /// <returns>
        /// True if the timestep is the first timestep, the last timestep, or the timestep is in the set specified by the user.  
        /// False will be returned if the user has not specified that this type of output should be generated or if the conditions for True are not met.
        /// </returns>
        /// <remarks></remarks>
        private bool IsRasterTstTimestep(int timestep)
        {
            return this.IsOutputTimestep(timestep, this.m_RasterTstOutputTimesteps, this.m_CreateRasterTstOutput);
        }

        /// <summary>
        /// Determines whether or not to do Raster Age output for the specified timestep
        /// </summary>
        /// <param name="timestep">The timestep</param>
        /// <returns>
        /// True if the timestep is the first timestep, the last timestep, or the timestep is in the set specified by the user.  
        /// False will be returned if the user has not specified that this type of output should be generated or if the conditions for True are not met.
        /// </returns>
        /// <remarks></remarks>
        private bool IsRasterStratumTimestep(int timestep)
        {
            return this.IsOutputTimestep(timestep, this.m_RasterStratumOutputTimesteps, this.m_CreateRasterStratumOutput);
        }

        /// <summary>
        /// Determines whether or not to do raster state attribute output for the specified timestep
        /// </summary>
        /// <param name="timestep">The timestep</param>
        /// <returns>
        /// True if the timestep is the first timestep, the last timestep, or the timestep is in the set specified by the user.  
        /// False will be returned if the user has not specified that this type of output should be generated or if the conditions for True are not met.
        /// </returns>
        /// <remarks></remarks>
        private bool IsRasterStateAttributeTimestep(int timestep)
        {
            return this.IsOutputTimestep(timestep, this.m_RasterStateAttributeOutputTimesteps, this.m_CreateRasterStateAttributeOutput);
        }

        /// <summary>
        /// Determines whether or not to do  transition adjacency state attribute calculation for the specified timestep
        /// </summary>
        /// <param name="timestep">The timestep</param>
        /// <returns>
        /// True if the timestep is the first timestep, the last timestep, or the timestep is in the set specified by the user.  
        /// False will be returned if the user has not specified that this type of output should be generated or if the 
        /// conditions for True are not met.
        /// </returns>
        /// <remarks></remarks>
        private bool IsTransitionAdjacencyStateAttributeTimestep(int timestep, int transitionGroupId)
        {
            TransitionAdjacencySetting setting = this.m_TransitionAdjacencySettingMap.GetItem(transitionGroupId);

            if (setting == null)
            {
                return false;
            }

            return this.IsOutputTimestep(timestep, setting.UpdateFrequency, true);
        }

        /// <summary>
        /// Determines whether or not to do raster transition attribute output for the specified timestep
        /// </summary>
        /// <param name="timestep">The timestep</param>
        /// <returns>
        /// True if the timestep is the first timestep, the last timestep, or the timestep is in the set specified by the user.  
        /// False will be returned if the user has not specified that this type of output should be generated or if the conditions for True are not met.
        /// </returns>
        /// <remarks></remarks>
        private bool IsRasterTransitionAttributeTimestep(int timestep)
        {
            return this.IsOutputTimestep(timestep, this.m_RasterTransitionAttributeOutputTimesteps, this.m_CreateRasterTransitionAttributeOutput);
        }

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

        internal static int GetEventIdKey(int? value)
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
        private void OnSummaryStateClassOutput(Cell simulationCell, int iteration, int timestep)
        {
            if (simulationCell.StratumId == 0 || simulationCell.StateClassId == 0)
            {
                return;
            }

            if ((this.IsSummaryStateClassTimestep(timestep)) || (this.m_StateAttributeTypeIdsNoAges.Count > 0 & this.IsSummaryStateAttributeTimestep(timestep)))
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
        /// Called to record transition class summary output for the specified simulation cell, iteration, and timestep
        /// </summary>
        /// <param name="simulationCell">The simulation cell</param>
        /// <param name="currentTransition">The current transition</param>
        /// <param name="iteration">The current iteration</param>
        /// <param name="timestep">The current timestep</param>
        /// <param name="eventId">The current event Id</param>
        /// <remarks>This function aggregates by stratum, iteration, timestep, and transition group.</remarks>
        private void OnSummaryTransitionOutput(Cell simulationCell, Transition currentTransition, int iteration, int timestep, Nullable<int> eventId)
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

        /// <summary>
        /// Called to record transition by state class summary output for the specified simulation cell, iteration, and timestep
        /// </summary>
        /// <param name="simulationCell">The simulation cell</param>
        /// <param name="currentTransition">The current transition</param>
        /// <param name="iteration">The current iteration</param>
        /// <param name="timestep">The current timestep</param>
        /// <remarks>This function aggregates by stratum, state class source, state class destination, and transition</remarks>
        private void OnSummaryTransitionByStateClassOutput(Cell simulationCell, Transition currentTransition, int iteration, int timestep)
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
        /// Called to record attribute summary output for the specified simulation cell, iteration, and timestep
        /// </summary>
        /// <param name="simulationCell">The simulation cell</param>
        /// <param name="iteration">The current iteration</param>
        /// <param name="timestep">The current timestep</param>
        /// <remarks>This function aggregates by stratum, iteration, timestep, and attribute type Id.</remarks>
        private void OnSummaryStateAttributeOutput(Cell simulationCell, int iteration, int timestep)
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

            if (!this.IsSummaryStateAttributeTimestep(timestep))
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
        /// Record transition type changes for the specified Transition Group.
        /// </summary>
        /// <param name="dictTransitionedPixels">A dictionary of arrays of Transition Types 
        /// which occured during the specified specified Interval / Timstep. Keyed by Transition Group Id.</param>
        /// <param name="iteration">The current iteration</param>
        /// <param name="timestep">The current timestep</param>
        /// <remarks></remarks>
        private void OnRasterTransitionOutput(int iteration, int timestep, Dictionary<int, int[]> dictTransitionedPixels)
        {
            if (!this.IsRasterTransitionTimestep(timestep) && !this.m_CreateRasterAATPOutput)
            {
                return;
            }

            //Loop thru the Transition Groups found in the dictionary
            foreach (int transitionGroupId in dictTransitionedPixels.Keys)
            {
                int[] transitionedPixels = dictTransitionedPixels[transitionGroupId];

                //Set up a raster as input to the Raster output function
                StochasticTimeRaster rastOP = this.m_InputRasters.CreateOutputRaster(RasterDataType.DTInteger);
                rastOP.SetIntValues( transitionedPixels);

                //Dont bother if there haven't been any transitions
                if ((transitionedPixels.Distinct().Count() > 1) && this.IsRasterTransitionTimestep(timestep))
                {
                    Spatial.WriteRasterData(
                        rastOP, 
                        this.ResultScenario.GetDataSheet(Constants.DATASHEET_OUTPUT_SPATIAL_TRANSITION), 
                        iteration, 
                        timestep, 
                        transitionGroupId, 
                        Constants.SPATIAL_MAP_TRANSITION_GROUP_VARIABLE_PREFIX, 
                        Constants.DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN);
                }
                
                //AATP Rasters
                if(this.m_CreateRasterAATPOutput)
                {
                    RecordAnnualAvgTransitionProbabilityOutput(
                    this.MaximumIteration - this.MinimumIteration + 1, 
                    timestep, 
                    transitionGroupId,
                    transitionedPixels);
                }
            }
        }

        /// <summary>
        /// Record transition type changes for the specified Transition Group for event data.
        /// </summary>
        /// <param name="dictTransitionedPixels">A dictionary of arrays of Transition Types 
        /// which occured during the specified specified Interval / Timstep. Keyed by Transition Group Id.</param>
        /// <param name="iteration">The current iteration</param>
        /// <param name="timestep">The current timestep</param>
        /// <remarks></remarks>
        private void OnRasterTransitionEventOutput(int iteration, int timestep, Dictionary<int, int[]> dictTransitionedPixels)
        {
            if (!this.IsRasterTransitionEventTimestep(timestep))
            {
                return;
            }

            //Loop thru the Transition Groups found in the dictionary
            foreach (int transitionGroupId in dictTransitionedPixels.Keys)
            {
                int[] transitionedPixels = dictTransitionedPixels[transitionGroupId];

                //Set up a raster as input to the Raster output function
                StochasticTimeRaster rastOP = this.m_InputRasters.CreateOutputRaster(RasterDataType.DTInteger);
                rastOP.SetIntValues(transitionedPixels);

                //Dont bother if there haven't been any transitions
                if (transitionedPixels.Distinct().Count() > 1)
                {
                    Spatial.WriteRasterData(
                        rastOP,
                        this.ResultScenario.GetDataSheet(Constants.DATASHEET_OUTPUT_SPATIAL_TRANSITION_EVENT),
                        iteration,
                        timestep,
                        transitionGroupId,
                        Constants.SPATIAL_MAP_TRANSITION_GROUP_EVENT_VARIABLE_PREFIX,
                        Constants.DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN);
                }               
            }
        }

        /// <summary>
        /// Record transition attribute changes for the specified Transition Group.
        /// </summary>
        /// <param name="RasterTransitionAttrValues"></param>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <remarks></remarks>
        private void OnRasterTransitionAttributeOutput(Dictionary<int, double[]> RasterTransitionAttrValues, int iteration, int timestep)
        {
            if (this.IsRasterTransitionAttributeTimestep(timestep))
            {
                foreach (int AttributeId in RasterTransitionAttrValues.Keys)
                {
                    //Set up a raster as input to the Raster output function
                    StochasticTimeRaster rastOP = this.m_InputRasters.CreateOutputRaster(RasterDataType.DTDouble);
                    rastOP.SetDoubleValues( RasterTransitionAttrValues[AttributeId]);

                    Spatial.WriteRasterData(
                        rastOP, 
                        this.ResultScenario.GetDataSheet(Constants.DATASHEET_OUTPUT_SPATIAL_TRANSITION_ATTRIBUTE), 
                        iteration, 
                        timestep, 
                        AttributeId,
                        Constants.SPATIAL_MAP_TRANSITION_ATTRIBUTE_VARIABLE_PREFIX, 
                        Constants.DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN);
                }
            }
        }

        /// <summary>
        /// Records summary transition output using the 'calculate as interval mean values' method
        /// </summary>
        /// <param name="simulationCell">The simulation cell</param>
        /// <param name="currentTransition">The current transition</param>
        /// <param name="iteration">The current iteration</param>
        /// <param name="timestep">The current timestep</param>
        /// <remarks>This function aggregates by stratum, iteration, timestep, and transition group.</remarks>
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

        /// <summary>
        /// Records summary transition output using the normal method
        /// </summary>
        /// <param name="simulationCell">The simulation cell</param>
        /// <param name="currentTransition">The current transition</param>
        /// <param name="iteration">The current iteration</param>
        /// <param name="timestep">The current timestep</param>
        /// <param name="eventId">The event Id</param>
        /// <remarks>This function aggregates by stratum, iteration, timestep, and transition group.</remarks>
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
                int EventIdKey = GetEventIdKey(eventId);

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
                        if (this.IsSpatial & this.IsRasterTransitionAttributeTimestep(timestep))
                        {
                            double[] arr = rasterTransitionAttrValues[AttributeTypeId];
                            if (arr[simulationCell.CellId] == Spatial.DefaultNoDataValue)
                            {
                                arr[simulationCell.CellId] = AttrValue.Value;
                            }
                            else
                            {
                                arr[simulationCell.CellId] += AttrValue.Value;
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
        /// Processes output stratum amounts
        /// </summary>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <remarks></remarks>
        private void ProcessOutputStratumAmounts(int iteration, int timestep)
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
                        object o = this.m_ProportionAccumulatorMap.GetValue(PrimaryStratum.StratumId, SecondaryStratumId, TertiaryStratumId);

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
        /// Processes Summary Stratum State results
        /// </summary>
        /// <param name="table"></param>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <remarks></remarks>
        private void ProcessSummaryStratumStateResults(DataTable table, int iteration, int timestep)
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
        /// Processes Summary Stratum Transition results
        /// </summary>
        /// <param name="table"></param>
        /// <remarks></remarks>
        private void ProcessSummaryStratumTransitionResults(int timestep, DataTable table)
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
        /// Processes Summary Stratum Transition State results
        /// </summary>
        /// <param name="table"></param>
        /// <remarks></remarks>
        private void ProcessSummaryStratumTransitionStateResults(DataTable table)
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
        /// Processes Summary State Attribute results
        /// </summary>
        /// <param name="table"></param>
        /// <remarks></remarks>
        private void ProcessSummaryStateAttributeResults(DataTable table)
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
        /// Processes Summary Transition Attribute results
        /// </summary>
        /// <param name="table"></param>
        /// <remarks></remarks>
        private void ProcessSummaryTransitionAttributeResults(DataTable table)
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
        /// Creates a dictionary of all secondary stratum ids in the current state class summary output
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
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

        /// <summary>
        /// Creates a dictionary of all tertiary stratum ids in the current state class summary output
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
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

        /// <summary>
        /// Process the Raster State Class output. Create a raster file as a snapshot of the current Cell state class values.
        /// </summary>
        /// <remarks></remarks>
        private void ProcessRasterStateClassOutput(int iteration, int timestep)
        {
            if (!this.IsSpatial)
            {
                Debug.Assert(!this.IsSpatial);
                return;
            }

            if (this.IsRasterStateClassTimestep(timestep))
            {
                StochasticTimeRaster rastOutput = this.m_InputRasters.CreateOutputRaster(RasterDataType.DTInteger);

                // Fetch the raster data from the Cells collection
                foreach (Cell c in this.Cells)
                {
                    rastOutput.SetIntValue(c.CellId, c.StateClassId);
                }

                // We need to remap the State Class values back to the original Raster values ( PK - > ID)
                DataSheet dsRemap = this.Project.GetDataSheet(Strings.DATASHEET_STATECLASS_NAME);

                //DEVNOTE: Tom - for now use default NoDataValue for remap. Ideally, we would bring the source files NoDataValue thru.
                rastOutput.SetIntValues( Spatial.RemapRasterCells(
                    rastOutput.GetIntValuesCopy(), 
                    dsRemap, 
                    Strings.DATASHEET_MAPID_COLUMN_NAME, 
                    false, 
                    Spatial.DefaultNoDataValue));

                Spatial.WriteRasterData(
                    rastOutput,
                    this.ResultScenario.GetDataSheet(Constants.DATASHEET_OUTPUT_SPATIAL_STATE_CLASS),
                    iteration,
                    timestep,
                    null,
                    Constants.SPATIAL_MAP_STATE_CLASS_VARIABLE_NAME,
                    Constants.DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN);
            }
        }

        /// <summary>
        /// Process the Raster Age output. Create a raster file as a snapshot of the current Cell Age values.
        /// </summary>
        /// <remarks></remarks>
        private void ProcessRasterAgeOutput(int iteration, int timestep)
        {
            if (!this.IsSpatial)
            {
                Debug.Assert(!this.IsSpatial);
                return;
            }

            if (this.IsRasterAgeTimestep(timestep))
            {
                StochasticTimeRaster rastOutput = this.m_InputRasters.CreateOutputRaster(RasterDataType.DTInteger);

                // Fetch the raster data from the Cells collection
                foreach (Cell c in this.Cells)
                {
                    rastOutput.SetIntValue(c.CellId, c.Age);
                }

                Spatial.WriteRasterData(
                    rastOutput, 
                    this.ResultScenario.GetDataSheet(Constants.DATASHEET_OUTPUT_SPATIAL_AGE), 
                    iteration, 
                    timestep,
                     null,                     
                    Constants.SPATIAL_MAP_AGE_VARIABLE_NAME, 
                    Constants.DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN);
            }
        }

        /// <summary>
        /// Process the Raster TST output. Create a raster file as a snapshot of the current Cell Age values.
        /// </summary>
        /// <remarks></remarks>
        private void ProcessRasterTSTOutput(int iteration, int timestep)
        {
            if (!this.IsSpatial)
            {
                Debug.Assert(!this.IsSpatial);
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
                                rastOutput.SetIntValue(cell.CellId, cell.TstValues[tg.TransitionGroupId].TstValue);
                            }
                        }
                    }

                    // If no values other than NODATAValue in rastOutput, then supress output for this timestep
                    if (rastOutput.GetNumberValidCells() > 0)
                    {
                        Spatial.WriteRasterData(
                            rastOutput,
                            this.ResultScenario.GetDataSheet(Constants.DATASHEET_OUTPUT_SPATIAL_TST),
                            iteration,
                            timestep,
                            tg.TransitionGroupId,
                            Constants.SPATIAL_MAP_TST_VARIABLE_NAME, 
                            Constants.DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN);
                    }
                }
            }
        }

        /// <summary>
        /// Process the Raster Stratum output. Create a raster file as a snapshot of the current Cell stratum values.
        /// </summary>
        /// <remarks></remarks>
        private void ProcessRasterStratumOutput(int iteration, int timestep)
        {
            if (!this.IsSpatial)
            {
                Debug.Assert(!this.IsSpatial);
                return;
            }

            if (this.IsRasterStratumTimestep(timestep))
            {
                StochasticTimeRaster rastOutput = this.m_InputRasters.CreateOutputRaster(RasterDataType.DTInteger);

                foreach (Cell c in this.Cells)
                {
                    // Fetch the raster data from the Cells collection
                    rastOutput.SetIntValue(c.CellId, c.StratumId);
                }

                // We need to remap the Stratum values back to the original Raster values ( PK - > ID)
                DataSheet dsRemap = this.Project.GetDataSheet(Strings.DATASHEET_STRATA_NAME);

                //DEVNOTE: Tom - for now use default NoDataValue during remap. Ideally, we would bring the source files NoDataValue thru.

                rastOutput.SetIntValues(Spatial.RemapRasterCells(
                    rastOutput.GetIntValuesCopy(), 
                    dsRemap, 
                    Strings.DATASHEET_MAPID_COLUMN_NAME, 
                    false, 
                    Spatial.DefaultNoDataValue));

                Spatial.WriteRasterData(
                    rastOutput,
                    this.ResultScenario.GetDataSheet(Constants.DATASHEET_OUTPUT_SPATIAL_STRATUM),
                    iteration,
                    timestep,
                    null,
                    Constants.SPATIAL_MAP_STRATUM_VARIABLE_NAME,
                    Constants.DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN);
            }
        }

        /// <summary>
        /// Process Raster State Attribute Output
        /// </summary>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <remarks></remarks>
        private void ProcessRasterStateAttributeOutput(int iteration, int timestep)
        {
            if (!this.IsSpatial)
            {
                Debug.Assert(!this.IsSpatial);
                return;
            }

            if (this.IsRasterStateAttributeTimestep(timestep))
            {
                StochasticTimeRaster rastOutput = this.m_InputRasters.CreateOutputRaster(RasterDataType.DTDouble);

                foreach (int AttributeTypeId in this.m_StateAttributeTypeIdsNoAges.Keys)
                {

                    foreach (Cell c in this.Cells)
                    {
                        double? AttrValue = this.m_StateAttributeValueMapNoAges.GetAttributeValueNoAge(
                            AttributeTypeId, c.StratumId, c.SecondaryStratumId, c.TertiaryStratumId, c.StateClassId, iteration, timestep);

                        if (AttrValue != null)
                        {
                            rastOutput.SetDoubleValue(c.CellId, Convert.ToDouble(AttrValue, CultureInfo.InvariantCulture));
                        }
                    }

                    Spatial.WriteRasterData(
                        rastOutput,
                        this.ResultScenario.GetDataSheet(Constants.DATASHEET_OUTPUT_SPATIAL_STATE_ATTRIBUTE),
                        iteration,
                        timestep,
                        AttributeTypeId,
                        Constants.SPATIAL_MAP_STATE_ATTRIBUTE_VARIABLE_PREFIX,
                        Constants.DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN);
                }

                foreach (int AttributeTypeId in this.m_StateAttributeTypeIdsAges.Keys)
                {

                    foreach (Cell c in this.Cells)
                    {
                        double? AttrValue = this.m_StateAttributeValueMapAges.GetAttributeValueByAge(
                            AttributeTypeId, c.StratumId, c.SecondaryStratumId, c.TertiaryStratumId, c.StateClassId, iteration, timestep, c.Age);

                        if (AttrValue != null)
                        {
                            rastOutput.SetDoubleValue(c.CellId, Convert.ToDouble(AttrValue, CultureInfo.InvariantCulture));
                        }
                    }

                    Spatial.WriteRasterData(
                        rastOutput, 
                        this.ResultScenario.GetDataSheet(Constants.DATASHEET_OUTPUT_SPATIAL_STATE_ATTRIBUTE), 
                        iteration, 
                        timestep, 
                        AttributeTypeId, 
                        Constants.SPATIAL_MAP_STATE_ATTRIBUTE_VARIABLE_PREFIX, 
                        Constants.DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN);
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
        /// window analysis results. Hold on to this raster in memory (as a single dimensional array) which can be accessed when needed.
        /// </remarks>
        private void ProcessTransitionAdjacencyStateAttribute(int iteration, int timestep)
        {
            if (!this.IsSpatial)
            {
                Debug.Assert(!this.IsSpatial);
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
                    double[] stateAttrVals = null;
                    double[] stateAttrAvgs = null;

                    int stateAttributeTypeId = setting.StateAttributeTypeId;
                    StateAttributeValueMap stateAttributeValueMap = null;
                    var IsNoAges = false;

                    // Extract State Attribute values from StateAttributeValueMaps ( just do it once, to enhance performance)
                    // check whether StateAttributeTypeId is in m_StateAttributeTypeIdsNoAges or m_StateAttributeTypeIdsAges. 

                    if (this.m_StateAttributeTypeIdsNoAges.Keys.Contains(stateAttributeTypeId))
                    {
                        stateAttributeValueMap = this.m_StateAttributeValueMapNoAges;
                        IsNoAges = true;
                    }

                    if (this.m_StateAttributeTypeIdsAges.Keys.Contains(stateAttributeTypeId))
                    {
                        stateAttributeValueMap = this.m_StateAttributeValueMapAges;
                    }

                    if (stateAttributeValueMap != null)
                    {
                        //TODO: TKR This might benefit from memory compression save as other rasters
                        stateAttrVals = new double[this.m_InputRasters.NumberCells];

                        for (var i = 0; i < this.m_InputRasters.NumberCells; i++)
                        {
                            stateAttrVals[i] = Spatial.DefaultNoDataValue;
                        }

                        // Loop thru raster and pull out the State Attribute Value for each cell
                        foreach (Cell cell in this.Cells)
                        {
                            // Pull out the values 1st, before doing the neighbor averaging, to get our repeated cost if GetValue.
                            double? attrValue = null;

                            if (IsNoAges)
                            {
                                attrValue = stateAttributeValueMap.GetAttributeValueNoAge(
                                    stateAttributeTypeId, 
                                    cell.StratumId, 
                                    cell.SecondaryStratumId, 
                                    cell.TertiaryStratumId, 
                                    cell.StateClassId, 
                                    iteration, timestep);
                            }
                            else
                            {
                                attrValue = stateAttributeValueMap.GetAttributeValueByAge(
                                    stateAttributeTypeId, 
                                    cell.StratumId, 
                                    cell.SecondaryStratumId, 
                                    cell.TertiaryStratumId, 
                                    cell.StateClassId, 
                                    iteration, timestep, cell.Age);
                            }

                            if (attrValue != null)
                            {
                                stateAttrVals[cell.CellId] = Convert.ToDouble(attrValue, CultureInfo.InvariantCulture);
                            }
                        }

                        // Now lets loop thru State Attribute array and generate the neighbor average for each cell
                        double attrValueTotal = 0;
                        int attrValueCnt = 0;

                        stateAttrAvgs = new double[this.m_InputRasters.NumberCells];

                        for (int i = 0; i < this.m_InputRasters.NumberCells; i++)
                        {
                            attrValueTotal = 0;
                            attrValueCnt = 0;

                            // Calculate row/column once, to eek performance ( small improvement) 
                            this.m_InputRasters.GetRowColForId(i, ref cellRow, ref cellColumn);

                            foreach (CellOffset offset in listNeighbors)
                            {
                                // Convert the relative neighbor into absolute neighbor.
                                int neighborCellId = this.m_InputRasters.GetCellIdByOffset(cellRow, cellColumn, offset.Row, offset.Column);
                                if (neighborCellId != -1)
                                {
                                    double attrValue = stateAttrVals[neighborCellId];

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
                                stateAttrAvgs[i] = attrValueTotal / listNeighbors.Count();
                            }
                            else
                            {
                                stateAttrAvgs[i] = Spatial.DefaultNoDataValue;
                            }
                        }

                        // Remove the old value array from the map, to be replaced with new array
                        this.m_TransitionAdjacencyStateAttributeValueMap.Remove(setting.TransitionGroupId);

                        //DEVNOTE: We've switched from double[] to RasterDouble to benefit from memory compression
                        RasterDoubles vals = new RasterDoubles(this.m_InputRasters.NumberCells);
                        vals.SetValues(stateAttrAvgs);
                        this.m_TransitionAdjacencyStateAttributeValueMap.Add(setting.TransitionGroupId, vals);
                    }
                }
            }
        }

        /// <summary>
        /// Process the Annual Average Transition Probabilities to raster file output.
        /// </summary>
        /// <remarks></remarks>
        private void ProcessRasterAATPOutput()
        {
            if (!this.IsSpatial)
            {
                Debug.Assert(!this.IsSpatial);
                return;
            }

            if (!this.m_CreateRasterAATPOutput)
            {
                return;
            }

            foreach (int tgId in this.m_AnnualAvgTransitionProbMap.Keys)
            {
                Dictionary<int, RasterDoubles> dictAatp = this.m_AnnualAvgTransitionProbMap[tgId];

                // Now lets loop thru the timestep arrays in the dictAatp
                foreach (int timestep in dictAatp.Keys)
                {
                    StochasticTimeRaster rastAatp = this.m_InputRasters.CreateOutputRaster(RasterDataType.DTDouble);
                    rastAatp.SetDoubleValues(dictAatp[timestep].GetValuesCopy());

                    //Don't bother writing out any array thats all DEFAULT_NO_DATA_VALUEs or 0's
                    if (rastAatp.GetNumberValidCells() == 0)
                    {
                        Debug.Print(
                            "Skipping Annual Average Transition Probabilities output for TG {0} / Timestep {1} as no non-DEFAULT_NO_DATA_VALUE values found.",
                            tgId, timestep);
                        continue;

                    }

                    Spatial.WriteRasterData(
                        rastAatp, 
                        this.ResultScenario.GetDataSheet(Constants.DATASHEET_OUTPUT_SPATIAL_AVERAGE_TRANSITION_PROBABILITY), 
                        0, 
                        timestep, 
                        tgId, 
                        Constants.SPATIAL_MAP_AVG_ANNUAL_TRANSITION_PROBABILITY_VARIABLE_PREFIX, 
                        Constants.DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN);
                }
            }
        }

        /// <summary>
        /// Record the Annual Average Transition Probability output.
        /// </summary>
        /// <param name="numIterations">The number of interactions the model will run</param>
        /// <param name="timestep">The current timestep</param>
        /// <param name="transitionGroupId">The Transition Group Id</param>
        /// <param name="cellArray">A cell array containing transition pixels for the specified Transition Group</param>
        /// <remarks></remarks>
        private void RecordAnnualAvgTransitionProbabilityOutput(int numIterations, int timestep, int transitionGroupId, int[] cellArray)
        {
            if (!this.IsSpatial)
            {
                Debug.Assert(!this.IsSpatial);
                return;
            }

            if (!this.m_CreateRasterAATPOutput)
            {
                return;
            }

            //Dont bother if there haven't been any transitions this timestep
            var distArray = cellArray.Distinct();
            if (distArray.Count() == 1)
            {
                var el0 = distArray.ElementAt(0);
                if (el0.Equals(0.0) || el0.Equals(Spatial.DefaultNoDataValue))
                {
                    //  Debug.Print("Found all 0's or NO_DATA_VALUES")
                    return;
                }
            }

            // See if the Dictionary for this Transition Group exists. If not, init routine screwed up.
            if (!this.m_AnnualAvgTransitionProbMap.ContainsKey(transitionGroupId))
            {
                Debug.Assert(false, "Where the heck is the Transition Group in the m_AnnualAvgTransitionProbMap member ?");
            }

            Dictionary<int, RasterDoubles> dictTgAATP = this.m_AnnualAvgTransitionProbMap[transitionGroupId];

            // We should now have a Dictionary of timestep-keyed RasterDouble objects
            // See if the specified timestep is a multiple of user timestep specified, or last timestep
            int timestepKey = 0;

            if (timestep == this.MaximumTimestep)
            {
                timestepKey = this.MaximumTimestep;
            }
            else
            {
                //We're looking for the the raster whose timestep key is the first one that is >= to the current timestep
                timestepKey = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(timestep - this.TimestepZero) / this.m_RasterAATPTimesteps) * this.m_RasterAATPTimesteps) + this.TimestepZero;

                if (timestepKey > this.MaximumTimestep)
                {
                    timestepKey = this.MaximumTimestep;
                }
            }

            // We should be able to find a dictionary
            RasterDoubles aatp = null;

            if (dictTgAATP.ContainsKey(timestepKey))
            {
                aatp = dictTgAATP[timestepKey];
            }
            else
            {
                Debug.Assert(false, "Where the heck is the Timestep keyed array in the m_AnnualAvgTransitionProbMap member.");
            }

            foreach (SyncroSim.STSim.Cell cell in this.Cells)
            {
                int i = cell.CellId;

                //Test for > 0 ( and not equal to DEFAULT_NO_DATA_VALUE either )
                if (cellArray[i] > 0)
                {

                    // Now lets do the probability calculation
                    //The value to increment by is 1/(tsf*N) 
                    //where tsf is the timestep frequency 
                    //N is the number of iterations.
                    // Accomodate last bin, where not multiple of frequency. For instance MaxTS of 8, and freq of 5, would give bins 1-5, and 6-8.

                    if ((timestepKey == this.MaximumTimestep) && (((timestepKey - this.TimestepZero) % this.m_RasterAATPTimesteps) != 0))
                    {
                        Double val = aatp.GetValue(i);
                        aatp.SetValue(i, val += 1 / (double)((timestepKey - this.TimestepZero) % this.m_RasterAATPTimesteps * numIterations));
                    }
                    else
                    {
                        Double val = aatp.GetValue(i);
                        aatp.SetValue(i, val += 1 / (double)(this.m_RasterAATPTimesteps * numIterations));
                    }
                }
            }
        }

        private bool IsTransitionAttributeTargetExceded(Cell simulationCell, Transition tr, int iteration, int timestep)
        {
            if (!this.m_TransitionAttributeValueMap.HasItems)
            {
                return false;
            }

            TransitionType tt = this.m_TransitionTypes[tr.TransitionTypeId];

            foreach (int AttributeTypeId in this.m_TransitionAttributeTypeIds.Keys)
            {
                foreach (TransitionGroup tg in tt.TransitionGroups)
                {
                    double? AttrValue = this.m_TransitionAttributeValueMap.GetAttributeValue(
                        AttributeTypeId, tg.TransitionGroupId, simulationCell.StratumId, simulationCell.SecondaryStratumId, 
                        simulationCell.TertiaryStratumId, simulationCell.StateClassId, iteration, timestep, simulationCell.Age);

                    if (AttrValue.HasValue)
                    {
                        TransitionAttributeTarget Target = this.m_TransitionAttributeTargetMap.GetAttributeTarget(
                            AttributeTypeId, simulationCell.StratumId, simulationCell.SecondaryStratumId, 
                            simulationCell.TertiaryStratumId, iteration, timestep);

                        if (Target != null && !Target.IsDisabled)
                        {
                            if (Target.TargetRemaining <= 0.0)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}
