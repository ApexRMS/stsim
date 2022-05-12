// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.IO;
using System.Linq;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using SyncroSim.Core;
using SyncroSim.Common;
using SyncroSim.StochasticTime;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    public partial class STSimTransformer
    {
        /// <summary>Get the Cell Neighbor at the compass direction North relative to the specified Cell</summary>
        /// <param name="initiationCell">The cell we're looking for the neighbor of</param>
        /// <returns>The neighboring Cell at the North compass direction</returns>
        private Cell GetCellNorth(Cell initiationCell)
        {
            return GetCellByOffset(initiationCell.CellId, -1, 0);
        }

        /// <summary>Get the Cell Neighbor at the compass direction North East relative to the specified Cell</summary>
        /// <param name="initiationCell">The cell we're looking for the neighbor of</param>
        /// <returns>The neighboring Cell at the North East compass direction</returns>
        private Cell GetCellNortheast(Cell initiationCell)
        {
            return GetCellByOffset(initiationCell.CellId, -1, 1);
        }

        /// <summary>Get the Cell Neighbor at the compass direction East relative to the specified Cell</summary>
        /// <param name="initiationCell">The cell we're looking for the neighbor of</param>
        /// <returns>The neighboring Cell at the East compass direction</returns>
        private Cell GetCellEast(Cell initiationCell)
        {
            return GetCellByOffset(initiationCell.CellId, 0, 1);
        }

        /// <summary>Get the Cell Neighbor at the compass direction South East relative to the specified Cell</summary>
        /// <param name="initiationCell">The cell we're looking for the neighbor of</param>
        /// <returns>The neighboring Cell at the South East compass direction</returns>
        private Cell GetCellSoutheast(Cell initiationCell)
        {
            return GetCellByOffset(initiationCell.CellId, 1, 1);
        }

        /// <summary>Get the Cell Neighbor at the compass direction South relative to the specified Cell</summary>
        /// <param name="initiationCell">The cell we're looking for the neighbor of</param>
        /// <returns>The neighboring Cell at the South compass direction</returns>
        private Cell GetCellSouth(Cell initiationCell)
        {
            return GetCellByOffset(initiationCell.CellId, 1, 0);
        }

        /// <summary>Get the Cell Neighbor at the compass direction South West relative to the specified Cell</summary>
        /// <param name="initiationCell">The cell we're looking for the neighbor of</param>
        /// <returns>The neighboring Cell at the South West compass direction</returns>
        private Cell GetCellSouthwest(Cell initiationCell)
        {
            return GetCellByOffset(initiationCell.CellId, 1, -1);
        }

        /// <summary>Get the Cell Neighbor at the compass direction West relative to the specified Cell</summary>
        /// <param name="initiationCell">The cell we're looking for the neighbor of</param>
        /// <returns>The neighboring Cell at the West compass direction</returns>
        private Cell GetCellWest(Cell initiationCell)
        {
            return GetCellByOffset(initiationCell.CellId, 0, -1);
        }

        /// <summary>Get the Cell Neighbor at the compass direction North West relative to the specified Cell</summary>
        /// <param name="initiationCell">The cell we're looking for the neighbor of</param>
        /// <returns>The neighboring Cell at the North West compass direction</returns>
        private Cell GetCellNorthwest(Cell initiationCell)
        {
            return GetCellByOffset(initiationCell.CellId, -1, -1);
        }

        /// <summary>
        /// Gets a cell for the specified initiation cell Id and row and column offsets
        /// </summary>
        /// <param name="initiationCellId"></param>
        /// <param name="rowOffset"></param>
        /// <param name="columnOffset"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private Cell GetCellByOffset(int initiationCellId, int rowOffset, int columnOffset)
        {
            int id = this.m_InputRasters.GetCellIdByOffset(initiationCellId, rowOffset, columnOffset);

            if (id == -1)
            {
                return null;
            }
            else
            {
                if (this.Cells.Contains(id))
                {
                    return this.Cells[id];
                }
                else
                {
                    return null;
                }
            }
        }

        private List<Cell> GetNeighboringCells(Cell c)
        {
            List<Cell> neighbors = new List<Cell>();

            Action<Cell> addNeighbor = (Cell c1) =>
            {
                if (c1 != null)
                {
                    neighbors.Add(c1);
                }
            };

            addNeighbor(this.GetCellNorth(c));
            addNeighbor(this.GetCellNortheast(c));
            addNeighbor(this.GetCellEast(c));
            addNeighbor(this.GetCellSoutheast(c));
            addNeighbor(this.GetCellSouth(c));
            addNeighbor(this.GetCellSouthwest(c));
            addNeighbor(this.GetCellWest(c));
            addNeighbor(this.GetCellNorthwest(c));

            return neighbors;
        }

        /// <summary>Get the Cell Neighbor at the direction and distance relative to the specified Cell</summary>
        /// <param name="initiationCell">The cell we're looking for the neighbor of</param>
        /// <returns>The neighboring Cell at the specified direction and distance </returns>
        private Cell GetCellByDistanceAndDirection(Cell initiationCell, int directionDegrees, double distanceM)
        {
            int id = this.m_InputRasters.GetCellIdByDistanceAndDirection(initiationCell.CellId, directionDegrees, distanceM);

            if (id == -1)
            {
                return null;
            }
            else
            {
                if (this.Cells.Contains(id))
                {
                    return this.Cells[id];
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets the distance between two neighboring cells
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private double GetNeighborCellDistance(CardinalDirection direction)
        {
            double dist = this.m_InputRasters.GetCellSizeMeters();

            if (direction == CardinalDirection.NE || 
                direction == CardinalDirection.SE || 
                direction == CardinalDirection.SW || 
                direction == CardinalDirection.NW)
            {
                dist = this.m_InputRasters.GetCellSizeDiagonalMeters();
            }

            return dist;
        }

        /// <summary>
        /// Gets the slope for the specified cells
        /// </summary>
        /// <param name="sourceCell"></param>
        /// <param name="destinationCell"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private double GetSlope(Cell sourceCell, Cell destinationCell, double distance)
        {
            double rise = this.GetCellElevation(destinationCell) - this.GetCellElevation(sourceCell);
            double radians = Math.Atan(rise / distance);
            double degrees = radians * (180 / Math.PI);

            return degrees;
        }

        /// <summary>
        /// Gets the elevation for the specified cell
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private double GetCellElevation(Cell cell)
        {
            if (this.m_InputRasters.DEMRaster == null || (this.m_InputRasters.DemCells.Count() == 0))
            {
                return 1.0;
            }
            else
            {
                return this.m_InputRasters.DemCells[cell.CellId];
            }
        }

        /// <summary>
        /// Gets the average attribute value for the specified cell's neighborhood and attribute type
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="transitionGroupId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private double? GetNeighborhoodAttributeValue(Cell cell, int transitionGroupId)
        {
            if (this.m_TransitionAdjacencyStateAttributeValueMap.ContainsKey(transitionGroupId))
            {
                double[] attrVals = this.m_TransitionAdjacencyStateAttributeValueMap[transitionGroupId];

                if (attrVals[cell.CellId] == Spatial.DefaultNoDataValue)
                {
                    return null;
                }
                else
                {
                    return attrVals[cell.CellId];
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Updates the transitioned pixels array for the specified timestep
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="transitionTypeId"></param>
        /// <param name="transitionedPixels"></param>
        /// <remarks></remarks>
        private void UpdateTransitionedPixels(Cell cell, int transitionTypeId, int[] transitionedPixels)
        {
            Debug.Assert(this.IsSpatial);

            if (!(this.m_CreateRasterTransitionOutput || this.m_CreateAvgRasterTransitionProbOutput))
            {
                return;
            }

            if (transitionedPixels == null)
            {
                return;
            }

            //Dereference to find TT "ID". If blank, dont bother to record transition.
            int? TransTypeMapId = this.m_TransitionTypes[transitionTypeId].MapId;

            if (TransTypeMapId.HasValue)
            {
                transitionedPixels[cell.CollectionIndex] = TransTypeMapId.Value;
            }
        }

        /// <summary>
        /// Updates the transitioned pixel events array for the specified timestep
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="transitionTypeId"></param>
        /// <param name="transitionedPixels"></param>
        /// <remarks></remarks>
        private void UpdateTransitionedPixelEvents(Cell cell, int eventId, int[] transitionedPixels)
        {
            Debug.Assert(this.IsSpatial);

            if (!this.m_CreateRasterTransitionEventOutput)
            {
                return;
            }

            if (transitionedPixels == null)
            {
                return;
            }

            transitionedPixels[cell.CollectionIndex] = eventId;
        }

        /// <summary>
        /// Creates a dictionary of transitioned pixel arrays, with Transition Group Id as the dictionary key
        /// </summary>
        /// <returns>Dictionary(Of Integer, Integer())</returns>
        /// <remarks></remarks>
        private Dictionary<int, int[]> CreateTransitionGroupTransitionedPixels(OutputFilterFlagTransitionGroup flags)
        {
            Debug.Assert(this.IsSpatial);

            Dictionary<int, int[]> dictTransitionPixels = new Dictionary<int, int[]>();

            // Loop thru transition groups. 
            foreach (TransitionGroup tg in this.m_TransitionGroups)
            {
                //Make sure Primary
                if (tg.PrimaryTransitionTypes.Count == 0)
                {
                    continue;
                }

                // Create a transitionPixel array object. If no Transition Output actually configured, economize on memory by not
                // dimensioning the array

                int[] transitionPixel = null;

                if ((tg.OutputFilter & flags) != 0)
                {
                    if (this.m_CreateRasterTransitionOutput || 
                        this.m_CreateAvgRasterTransitionProbOutput || 
                        this.m_CreateRasterTransitionEventOutput)
                        {
                            transitionPixel = new int[this.Cells.Count];

                            for (var i = 0; i < this.Cells.Count; i++)
                            {
                                transitionPixel[i] = 0;
                            }
                        }
                }

                dictTransitionPixels.Add(tg.TransitionGroupId, transitionPixel);
            }

            return dictTransitionPixels;
        }

        /// <summary>
        /// Creates a dictionary of transition attribute value arrays
        /// </summary>
        /// <param name="timestep"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private Dictionary<int, double[]> CreateRasterTransitionAttributeArrays(int timestep)
        {
            Debug.Assert(this.IsSpatial);
            Dictionary<int, double[]> dict = new Dictionary<int, double[]>();

            if (this.IsRasterTransitionAttributeTimestep(timestep) || 
                this.IsAvgRasterTransitionAttributeTimestep(timestep))
            {
                foreach (int id in this.m_TransitionAttributeTypeIds.Keys)
                {
                    TransitionAttributeType tat = this.m_TransitionAttributeTypes[id];

                    if (tat.OutputFilter.HasFlag(OutputFilterFlagAttribute.Spatial) ||
                        tat.OutputFilter.HasFlag(OutputFilterFlagAttribute.AvgSpatial))
                    {
                        double[] arr = new double[this.Cells.Count];

                        for (int i = 0; i < this.Cells.Count; i++)
                        {
                            arr[i] = 0.0;
                        }

                        dict.Add(id, arr);
                    }
                }
            }

            return dict;
        }

        protected virtual void OnSpatialTransitionGroup(object sender, SpatialTransitionGroupEventArgs e)
        {
            this.ApplySpatialTransitionGroup?.Invoke(this, e);
        }

        /// <summary>
        /// Applies probabilistic transitions in raster mode
        /// </summary>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <param name="rasterTransitionAttrValues"></param>
        /// <param name="dictTransitionedPixels"></param>
        /// <param name="dictTransitionedEventPixels"></param>
        private void ApplyProbabilisticTransitionsRaster(
            int iteration, 
            int timestep, 
            Dictionary<int, double[]> rasterTransitionAttrValues, 
            Dictionary<int, int[]> dictTransitionedPixels, 
            Dictionary<int, int[]> dictTransitionedEventPixels)
        {
            Debug.Assert(this.IsSpatial);

            foreach (Stratum Stratum in this.m_Strata)
            {
                this.ShuffleStratumCells(Stratum);
            }

            Dictionary<int, TransitionGroup> RemainingTransitionGroups = new Dictionary<int, TransitionGroup>();

            foreach (TransitionGroup tg in this.m_ShufflableTransitionGroups)
            {
                RemainingTransitionGroups.Add(tg.TransitionGroupId, tg);
            }

            foreach (TransitionGroup TransitionGroup in this.m_ShufflableTransitionGroups)
            {
                if (TransitionGroup.PrimaryTransitionTypes.Count == 0)
                {
                    continue;
                }

                SpatialTransitionGroupEventArgs args = new SpatialTransitionGroupEventArgs(iteration, timestep, TransitionGroup, dictTransitionedPixels[TransitionGroup.TransitionGroupId],
                            rasterTransitionAttrValues);
                this.OnSpatialTransitionGroup(this, args);

                if (args.Cancel)
                {
                    continue;
                }

                int TransitionEventId = Constants.STARTING_TRANSITION_EVENT_ID;
                Debug.Assert(TransitionEventId == 0);

                MultiLevelKeyMap1<Dictionary<int, TransitionAttributeTarget>> tatMap = new MultiLevelKeyMap1<Dictionary<int, TransitionAttributeTarget>>();

                this.ResetTransitionTargetMultipliers(iteration, timestep, TransitionGroup);
                this.ResetTransitionAttributeTargetMultipliers(iteration, timestep, RemainingTransitionGroups, tatMap, TransitionGroup);

                RemainingTransitionGroups.Remove(TransitionGroup.TransitionGroupId);

                //If the transition group has no size distribution or transition patches then call the non-spatial algorithm for this group.

                if ((!TransitionGroup.HasSizeDistribution) && (TransitionGroup.PatchPrioritization == null))
                {
                    foreach (Cell simulationCell in this.m_Cells)
                    {
                        ApplyProbabilisticTransitionsByCell(
                            simulationCell, iteration, timestep, TransitionGroup, 
                            dictTransitionedPixels[TransitionGroup.TransitionGroupId], 
                            rasterTransitionAttrValues);
                    }
                }
                else
                {
                    Dictionary<int, Cell> TransitionedCells = new Dictionary<int, Cell>();

                    foreach (Stratum Stratum in this.m_Strata)
                    {
                        double ExpectedArea = 0.0;
                        double MaxCellProbability = 0.0;

                        this.FillTransitionPatches(TransitionedCells, Stratum, TransitionGroup, iteration, timestep);

                        Dictionary<int, Cell> InitiationCells = this.CreateInitiationCellCollection(
                            TransitionedCells, Stratum.StratumId, TransitionGroup.TransitionGroupId, iteration, timestep, 
                            ref ExpectedArea, ref MaxCellProbability);

                        if (ExpectedArea > 0.0 && MaxCellProbability > 0.0)
                        {
                            bool GroupHasTarget = TransitionGroupHasTarget(TransitionGroup.TransitionGroupId, Stratum.StratumId, iteration, timestep);
                            bool MaximizeFidelityToTotalArea = this.MaximizeFidelityToTotalArea(TransitionGroup.TransitionGroupId, Stratum.StratumId, iteration, timestep);
                            double rand = this.m_RandomGenerator.GetNextDouble();

                            while ((MathUtils.CompareDoublesGT(ExpectedArea / this.m_AmountPerCell, rand, 0.000001)) && (InitiationCells.Count > 0))
                            {
                                List<TransitionEvent> TransitionEventList = this.CreateTransitionEventList(Stratum.StratumId, TransitionGroup.TransitionGroupId, iteration, timestep, ExpectedArea);

                                this.GenerateTransitionEvents(
                                    TransitionEventList, 
                                    TransitionedCells, 
                                    InitiationCells, 
                                    Stratum.StratumId, 
                                    TransitionGroup.TransitionGroupId, 
                                    iteration, 
                                    timestep, 
                                    MaxCellProbability, 
                                    dictTransitionedPixels[TransitionGroup.TransitionGroupId], 
                                    dictTransitionedEventPixels[TransitionGroup.TransitionGroupId], 
                                    ref ExpectedArea, 
                                    ref TransitionEventId, 
                                    rasterTransitionAttrValues);

                                if (!GroupHasTarget)
                                {
                                    if (!MaximizeFidelityToTotalArea)
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    Dictionary<int, TransitionAttributeTarget> d = tatMap.GetItem(Stratum.StratumId);

                                    if (d != null)
                                    {
                                        bool TargetsMet = true;

                                        foreach (TransitionAttributeTarget tat in d.Values)
                                        {
                                            if (!tat.IsDisabled && tat.TargetRemaining > 0.0)
                                            {
                                                TargetsMet = false;
                                                break;
                                            }

                                            if (TargetsMet)
                                            {
                                                goto ExitWhile;
                                            }
                                        }
                                    }
                                }
                            }

                            ExitWhile: ;
                        }

                        this.ClearTransitionPatches(TransitionGroup);
                    }
                }
            }
        }

        private List<TransitionEvent> CreateTransitionEventList(
            int stratumId, 
            int transitionGroupId,
            int iteration, 
            int timestep, 
            double expectedArea)
        {
            Debug.Assert(this.IsSpatial);
            Debug.Assert(expectedArea > 0.0);

            double AccumulatedArea = 0.0;
            List<TransitionEvent> TransitionEventList = new List<TransitionEvent>();

            while (MathUtils.CompareDoublesGT(expectedArea, AccumulatedArea, 0.000001))
            {
                double diff = expectedArea - AccumulatedArea;

                if (this.m_AmountPerCell > diff)
                {
                    double rand = this.m_RandomGenerator.GetNextDouble();
                    double prob = diff / this.m_AmountPerCell;

                    if (rand > prob)
                    {
                        break;
                    }
                }

                double MinimumSize = this.m_AmountPerCell;
                double MaximumSize = (expectedArea - AccumulatedArea);
                double TargetSize = this.m_AmountPerCell;
                double AreaDifference = (expectedArea - AccumulatedArea);

                this.GetTargetSizeClass(stratumId, transitionGroupId, iteration, timestep, AreaDifference, ref MinimumSize, ref MaximumSize, ref TargetSize);

                TransitionEventList.Add(new TransitionEvent(TargetSize));

                AccumulatedArea = AccumulatedArea + TargetSize;

                Debug.Assert(MinimumSize >= 0.0);
                Debug.Assert(MaximumSize >= 0.0);
                Debug.Assert(TargetSize >= 0.0);
                Debug.Assert(MinimumSize <= MaximumSize);
                Debug.Assert(TargetSize >= MinimumSize && TargetSize <= MaximumSize);
                Debug.Assert(TransitionEventList.Count < 100000);
            }

            this.SortTransitionEventList(stratumId, transitionGroupId, iteration, timestep, TransitionEventList);
            return TransitionEventList;
        }

        private void GetTargetSizeClass(
            int stratumId, 
            int transitionGroupId, 
            int iteration, 
            int timestep, 
            double areaDifference, 
            ref double minimumSizeOut, 
            ref double maximumSizeOut, 
            ref double targetSizeOut)
        {
            Debug.Assert(this.IsSpatial);

            double CumulativeProportion = 0.0;
            double Rand1 = this.m_RandomGenerator.GetNextDouble();

            List<TransitionSizeDistribution> tsdlist = this.m_TransitionSizeDistributionMap.GetSizeDistributions(transitionGroupId, stratumId, iteration, timestep);

            if (tsdlist == null)
            {
                minimumSizeOut = this.m_AmountPerCell;
                maximumSizeOut = this.m_AmountPerCell;
                targetSizeOut = this.m_AmountPerCell;

                return;
            }

            foreach (TransitionSizeDistribution tsd in tsdlist)
            {
                CumulativeProportion += tsd.Proportion;

                if (CumulativeProportion >= Rand1)
                {
                    minimumSizeOut = tsd.MinimumSize;
                    maximumSizeOut = tsd.MaximumSize;

                    break;
                }
            }

            Debug.Assert(minimumSizeOut <= maximumSizeOut);

            if (maximumSizeOut > areaDifference)
            {
                maximumSizeOut = areaDifference;
                minimumSizeOut = areaDifference;
            }

            double Rand2 = this.m_RandomGenerator.GetNextDouble();
            double Rand3 = (maximumSizeOut - minimumSizeOut) * Rand2;

            targetSizeOut = Rand3 + minimumSizeOut;
        }

        /// <summary>
        /// Determines whether to maximize fidelity to total area
        /// </summary>
        /// <param name="transitionGroupId"></param>
        /// <param name="stratumId"></param>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool MaximizeFidelityToTotalArea(int transitionGroupId, int stratumId, int iteration, int timestep)
        {
            TransitionSizePrioritization tsp = this.m_TransitionSizePrioritizationMap.GetSizePrioritization(
                transitionGroupId, stratumId, iteration, timestep);

            if (tsp == null)
            {
                return false;
            }
            else
            {
                return tsp.MaximizeFidelityToTotalArea;
            }
        }

        /// <summary>
        /// Determines whether there are transition targets or transition attribute targets associated with this 
        /// transition group, stratum, iteration and timestep
        /// </summary>
        /// <param name="transitionGroupId"></param>
        /// <param name="stratumId"></param>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool TransitionGroupHasTarget(int transitionGroupId, int stratumId, int iteration, int timestep)
        {
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

            foreach (int? ss in SecondaryStratumIds)
            {
                foreach (int? ts in TertiaryStratumIds)
                {
                    TransitionTarget tt = this.m_TransitionTargetMap.GetTransitionTarget(
                        transitionGroupId, stratumId, ss, ts, iteration, timestep);

                    if (tt != null)
                    {
                        return true;
                    }
                }
            }

            if (this.m_TransitionAttributeValueMap.TypeGroupMap.ContainsKey(transitionGroupId))
            {
                Dictionary<int, bool> d = this.m_TransitionAttributeValueMap.TypeGroupMap[transitionGroupId];

                foreach (TransitionAttributeType ta in this.m_TransitionAttributeTypes)
                {
                    if (d.ContainsKey(ta.TransitionAttributeId))
                    {
                        foreach (int? ss in SecondaryStratumIds)
                        {
                            foreach (int? ts in TertiaryStratumIds)
                            {
                                TransitionAttributeTarget tat = this.m_TransitionAttributeTargetMap.GetAttributeTarget(
                                    ta.TransitionAttributeId, stratumId, ss, ts, iteration, timestep);

                                if (tat != null)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        private void SortTransitionEventList(
            int stratumId, 
            int transitionGroupId, 
            int iteration, 
            int timestep, 
            List<TransitionEvent> transitionEventList)
        {
            TransitionSizePrioritization tsp = this.m_TransitionSizePrioritizationMap.GetSizePrioritization(
                transitionGroupId, stratumId, iteration, timestep);

            if ((tsp == null) || (tsp.SizePrioritization == SizePrioritization.None))
            {
                ShuffleUtilities.ShuffleList(transitionEventList, this.m_RandomGenerator.Random);
            }
            else
            {
                if (tsp.SizePrioritization == SizePrioritization.Smallest)
                {
                    transitionEventList.Sort((TransitionEvent e1, TransitionEvent e2) =>
                    {
                        return e1.TargetAmount.CompareTo(e2.TargetAmount);
                    });
                }
                else
                {
                    transitionEventList.Sort((TransitionEvent e1, TransitionEvent e2) =>
                    {
                        return (-(e1.TargetAmount.CompareTo(e2.TargetAmount)));
                    });
                }
            }
        }

        private Dictionary<int, Cell> CreateInitiationCellCollection(
            Dictionary<int, Cell> transitionedCells, 
            int stratumId, 
            int transitionGroupId, 
            int iteration, 
            int timestep, 
            ref double expectedAreaOut, 
            ref double maxCellProbabilityOut)
        {
            Debug.Assert(this.IsSpatial);

            double ExpectedArea = 0.0;
            double MaxCellProbability = 0.0;
            Stratum Stratum = this.m_Strata[stratumId];
            Dictionary<int, Cell> InitiationCells = new Dictionary<int, Cell>();

            foreach (Cell SimulationCell in Stratum.Cells.Values)
            {
                Debug.Assert(SimulationCell.StratumId != 0);
                Debug.Assert(SimulationCell.StateClassId != 0);

                if (!transitionedCells.ContainsKey(SimulationCell.CellId))
                {
                    double CellProbability = this.SpatialCalculateCellProbabilityNonTruncated(SimulationCell, transitionGroupId, iteration, timestep);

                    ExpectedArea += (CellProbability * this.m_AmountPerCell);

                    //Include Initiation Multiplier in the calculation of cell probability once expected area has been calculated

                    CellProbability *= this.GetTransitionSpatialInitiationMultiplier(SimulationCell, transitionGroupId, iteration, timestep);

                    if (CellProbability > MaxCellProbability)
                    {
                        MaxCellProbability = CellProbability;

                        if (MaxCellProbability > 1.0)
                        {
                            MaxCellProbability = 1.0;
                        }
                    }

                    if (CellProbability > 0.0)
                    {
                        InitiationCells.Add(SimulationCell.CellId, SimulationCell);
                    }
                }
            }

            expectedAreaOut = ExpectedArea;
            maxCellProbabilityOut = MaxCellProbability;

            return InitiationCells;
        }

        private Cell SelectInitiationCell(
            Dictionary<int, Cell> initiationCells, int transitionGroupId, 
            int iteration, int timestep, double maxCellProbability)
        {
            Debug.Assert(this.IsSpatial);

            Cell SimulationCell = null;
            double CellProbability = 0.0;
            double Rand1 = this.m_RandomGenerator.GetNextDouble();
            int NumCellsChecked = 0;
            bool KeepLooping = true;

            do
            {
                NumCellsChecked += 1;
                int Rand2 = this.m_RandomGenerator.GetNextInteger(0, (initiationCells.Count - 1));
                SimulationCell = initiationCells.Values.ElementAt(Rand2);

                CellProbability = this.SpatialCalculateCellProbability(SimulationCell, transitionGroupId, iteration, timestep);
                CellProbability *= this.GetTransitionSpatialInitiationMultiplier(SimulationCell, transitionGroupId, iteration, timestep);
                CellProbability = CellProbability / maxCellProbability;

                //Increase probability of selection as the number of cells checked increases

                if (CellProbability < (NumCellsChecked / (double)initiationCells.Count))
                {
                    CellProbability = NumCellsChecked / (double)initiationCells.Count;
                }

                Rand1 = this.m_RandomGenerator.GetNextDouble();

                if (!MathUtils.CompareDoublesGT(Rand1, CellProbability, 0.000001))
                {
                    KeepLooping = false;
                }

                if (CellProbability == 0.0)
                {
                    KeepLooping = true;
                }

                if (initiationCells.Count == 0)
                {
                    KeepLooping = false;
                }
            } while (KeepLooping);

            initiationCells.Remove(SimulationCell.CellId);

            return SimulationCell;
        }

        private void GenerateTransitionEvents(
            List<TransitionEvent> transitionEventList, 
            Dictionary<int, Cell> transitionedCells, 
            Dictionary<int, Cell> initiationCells, 
            int stratumId, 
            int transitionGroupId, 
            int iteration, 
            int timestep, 
            double maxCellProbability, 
            int[] transitionedPixels, 
            int[] transitionedEventPixels,
            ref double expectedArea, 
            ref int transitionEventId,
            Dictionary<int, double[]> rasterTransitionAttrValues)
        {
#if DEBUG

            Debug.Assert(this.IsSpatial);
            Debug.Assert(maxCellProbability > 0.0);
            Debug.Assert(transitionEventId >= 0);

            foreach (Cell c in initiationCells.Values)
            {
                Debug.Assert(c.StratumId == stratumId);
            }

#endif

            TransitionGroup TransitionGroup = this.m_TransitionGroups[transitionGroupId];

            while ((transitionEventList.Count > 0) && (initiationCells.Count > 0) && (expectedArea > 0))
            {
                Cell InitiationCell = null;

                if (TransitionGroup.PatchPrioritization != null)
                {
                    InitiationCell = this.SelectPatchInitiationCell(TransitionGroup);

                    if (InitiationCell == null)
                    {
                        Debug.Assert(TransitionGroup.PatchPrioritization.TransitionPatches.Count == 0);
                        initiationCells.Clear(); //No Patches left. Clear Initiation Cells.

                        break;
                    }
                }
                else
                {
                    InitiationCell = this.SelectInitiationCell(initiationCells, transitionGroupId, iteration, timestep, maxCellProbability);
                }

                if (InitiationCell != null)
                {
                    double CellProbability = this.SpatialCalculateCellProbability(InitiationCell, transitionGroupId, iteration, timestep);

                    if (CellProbability > 0.0)
                    {
                        TransitionEvent TransitionEvent = transitionEventList[0];
                        TransitionSizePrioritization tsp = this.m_TransitionSizePrioritizationMap.GetSizePrioritization(
                            transitionGroupId, stratumId, iteration, timestep);

                        transitionEventId += 1;

                        this.GrowTransitionEvent(
                            transitionEventList, 
                            TransitionEvent, 
                            transitionedCells, 
                            initiationCells, 
                            InitiationCell, 
                            TransitionGroup, 
                            iteration, timestep, 
                            transitionedPixels, 
                            transitionedEventPixels, 
                            ref expectedArea, 
                            transitionEventId, 
                            rasterTransitionAttrValues, 
                            tsp);
                    }
                }
            }
        }

        private void GrowTransitionEvent(
            List<TransitionEvent> transitionEventList, 
            TransitionEvent transitionEvent, 
            Dictionary<int, Cell> transitionedCells, 
            Dictionary<int, Cell> initiationCells, 
            Cell initiationCell, 
            TransitionGroup tg, 
            int iteration, 
            int timestep, 
            int[] transitionedPixels, 
            int[] transitionedEventPixels,
            ref double expectedArea, 
            int transitionEventId,
            Dictionary<int, double[]> rasterTransitionAttrValues, 
            TransitionSizePrioritization tsp)
        {
            Debug.Assert(this.IsSpatial);

            double TotalEventAmount = 0.0;
            GrowEventRecordCollection EventCandidates = new GrowEventRecordCollection(this.m_RandomGenerator);
            Dictionary<int, Cell> SeenBefore = new Dictionary<int, Cell>();

            EventCandidates.AddRecord(new GrowEventRecord(initiationCell, 0.0, 1.0));
            SeenBefore.Add(initiationCell.CellId, initiationCell);

            Dictionary<int, Transition> transitionDictionary = new Dictionary<int, Transition>();

            while ((EventCandidates.Count > 0) && (TotalEventAmount <= expectedArea))
            {
                Transition Transition = null;
                GrowEventRecord CurrentRecord = EventCandidates.RemoveRecord();
                List<Cell> neighbors = this.GetNeighboringCells(CurrentRecord.Cell);

                TransitionPathwayAutoCorrelation AutoCorrelation = 
                    this.m_TransitionPathwayAutoCorrelationMap.GetCorrelation(
                        tg.TransitionGroupId, 
                        CurrentRecord.Cell.StratumId, 
                        CurrentRecord.Cell.SecondaryStratumId, 
                        CurrentRecord.Cell.TertiaryStratumId, 
                        iteration, timestep);

                if (AutoCorrelation != null)
                {
                    if (AutoCorrelation.SpreadTo == AutoCorrelationSpread.ToSamePrimaryStratum && 
                        CurrentRecord.Cell.StratumId != initiationCell.StratumId)
                    {
                        continue;
                    }
                    else if (AutoCorrelation.SpreadTo == AutoCorrelationSpread.ToSameSecondaryStratum && 
                        CurrentRecord.Cell.SecondaryStratumId != initiationCell.SecondaryStratumId)
                    {
                        continue;
                    }
                    else if (AutoCorrelation.SpreadTo == AutoCorrelationSpread.ToSameTertiaryStratum && 
                        CurrentRecord.Cell.TertiaryStratumId != initiationCell.TertiaryStratumId)
                    {
                        continue;
                    }

                    foreach (Cell c in neighbors)
                    {
                        if (transitionDictionary.ContainsKey(c.CellId))
                        {
                            Transition neighborTransition = transitionDictionary[c.CellId];
                            if (CurrentRecord.Cell.Transitions.Contains(neighborTransition))
                            {
                                Transition = neighborTransition;
                                break;
                            }
                        }
                    }
                }

                if (Transition == null)
                {
                    if (AutoCorrelation != null)
                    {
                        if ((AutoCorrelation.SpreadTo == AutoCorrelationSpread.ToSamePathway) && (transitionDictionary.Count > 0))
                        {
                            continue;
                        }
                    }

                    Transition = this.SelectTransitionPathway(CurrentRecord.Cell, tg.TransitionGroupId, iteration, timestep);
                }
                else
                {
                    if (AutoCorrelation == null || (!AutoCorrelation.AutoCorrelation))
                    {
                        Transition = this.SelectTransitionPathway(CurrentRecord.Cell, tg.TransitionGroupId, iteration, timestep);
                    }
                }

                if (Transition == null)
                {
                    continue;
                }

                if (this.IsTransitionAttributeTargetExceded(CurrentRecord.Cell, Transition, iteration, timestep))
                {
                    initiationCells.Remove(CurrentRecord.Cell.CellId);
                    continue;
                }

                this.RecordSummaryTransitionOutput(CurrentRecord.Cell, Transition, iteration, timestep, transitionEventId);
                this.RecordSummaryTransitionByStateClassOutput(CurrentRecord.Cell, Transition, iteration, timestep);

                this.ChangeCellForProbabilisticTransition(CurrentRecord.Cell, tg, Transition, iteration, timestep, rasterTransitionAttrValues);

                if (!transitionDictionary.ContainsKey(CurrentRecord.Cell.CellId))
                {
                    transitionDictionary.Add(CurrentRecord.Cell.CellId, Transition);
                }

                this.FillProbabilisticTransitionsForCell(CurrentRecord.Cell, iteration, timestep);

                this.UpdateCellPatchMembership(tg.TransitionGroupId, CurrentRecord.Cell);
                this.UpdateTransitionedPixels(CurrentRecord.Cell, Transition.TransitionTypeId, transitionedPixels);
                this.UpdateTransitionedPixelEvents(CurrentRecord.Cell, transitionEventId, transitionedEventPixels);

                Debug.Assert(!transitionedCells.ContainsKey(CurrentRecord.Cell.CellId));

                transitionedCells.Add(CurrentRecord.Cell.CellId, CurrentRecord.Cell);
                initiationCells.Remove(CurrentRecord.Cell.CellId);

                TotalEventAmount += this.m_AmountPerCell;

                if ((TotalEventAmount >= (transitionEvent.TargetAmount - (0.5 * this.m_AmountPerCell))) || (TotalEventAmount >= expectedArea))
                {
                    break;
                }

                double tempVar = CurrentRecord.TravelTime;

                this.AddGrowEventRecords(
                    EventCandidates, transitionedCells, SeenBefore, CurrentRecord.Cell,
                    tg.TransitionGroupId, iteration, timestep, ref tempVar);
            }

            expectedArea -= TotalEventAmount;

            if (expectedArea < 0.0)
            {
                expectedArea = 0.0;
            }

            bool MaximizeFidelityToDistribution = true;

            if (tsp != null)
            {
                MaximizeFidelityToDistribution = tsp.MaximizeFidelityToDistribution;
            }

            if ((!MaximizeFidelityToDistribution) || (TotalEventAmount >= transitionEvent.TargetAmount))
            {
                transitionEventList.Remove(transitionEvent);
            }
            else
            {
                RemoveNearestSizedEvent(transitionEventList, TotalEventAmount);
            }
        }

        private void AddGrowEventRecords(GrowEventRecordCollection eventCandidates, Dictionary<int, Cell> transitionedCells, Dictionary<int, Cell> seenBefore, Cell initiationCell, int transitionGroupId, int iteration, int timestep, ref double travelTime)
        {
            Debug.Assert(this.IsSpatial);

            this.AddGrowEventRecord(eventCandidates, transitionedCells, seenBefore, initiationCell, this.GetCellNorth(initiationCell), transitionGroupId, iteration, timestep, travelTime, CardinalDirection.N);
            this.AddGrowEventRecord(eventCandidates, transitionedCells, seenBefore, initiationCell, this.GetCellEast(initiationCell), transitionGroupId, iteration, timestep, travelTime, CardinalDirection.E);
            this.AddGrowEventRecord(eventCandidates, transitionedCells, seenBefore, initiationCell, this.GetCellSouth(initiationCell), transitionGroupId, iteration, timestep, travelTime, CardinalDirection.S);
            this.AddGrowEventRecord(eventCandidates, transitionedCells, seenBefore, initiationCell, this.GetCellWest(initiationCell), transitionGroupId, iteration, timestep, travelTime, CardinalDirection.W);
            this.AddGrowEventRecord(eventCandidates, transitionedCells, seenBefore, initiationCell, this.GetCellNortheast(initiationCell), transitionGroupId, iteration, timestep, travelTime, CardinalDirection.NE);
            this.AddGrowEventRecord(eventCandidates, transitionedCells, seenBefore, initiationCell, this.GetCellSoutheast(initiationCell), transitionGroupId, iteration, timestep, travelTime, CardinalDirection.SE);
            this.AddGrowEventRecord(eventCandidates, transitionedCells, seenBefore, initiationCell, this.GetCellSouthwest(initiationCell), transitionGroupId, iteration, timestep, travelTime, CardinalDirection.SW);
            this.AddGrowEventRecord(eventCandidates, transitionedCells, seenBefore, initiationCell, this.GetCellNorthwest(initiationCell), transitionGroupId, iteration, timestep, travelTime, CardinalDirection.NW);
        }

        private void AddGrowEventRecord(GrowEventRecordCollection eventCandidates, Dictionary<int, Cell> transitionedCells, Dictionary<int, Cell> seenBefore, Cell initiationCell, Cell simulationCell, int transitionGroupId, int iteration, int timestep, double travelTime, CardinalDirection direction)
        {
            Debug.Assert(this.IsSpatial);

            if (simulationCell != null)
            {
                if ((!transitionedCells.ContainsKey(simulationCell.CellId)) & (!seenBefore.ContainsKey(simulationCell.CellId)))
                {
                    TransitionGroup tg = this.m_TransitionGroups[transitionGroupId];

                    if (tg.PatchPrioritization != null)
                    {
                        if (tg.PatchPrioritization.PatchPrioritizationType == PatchPrioritizationType.LargestEdgesOnly || tg.PatchPrioritization.PatchPrioritizationType == PatchPrioritizationType.SmallestEdgesOnly)
                        {
                            if (tg.PatchPrioritization.TransitionPatches.Count() == 0)
                            {
                                return;
                            }

                            TransitionPatch patch = tg.PatchPrioritization.TransitionPatches.First();

                            if (tg.PatchPrioritization.PatchPrioritizationType == PatchPrioritizationType.LargestEdgesOnly)
                            {
                                patch = tg.PatchPrioritization.TransitionPatches.Last();
                            }

                            if (!patch.EdgeCells.ContainsKey(simulationCell.CellId))
                            {
                                return;
                            }
                        }
                    }

                    double Probability = this.SpatialCalculateCellProbability(simulationCell, transitionGroupId, iteration, timestep);

                    if (Probability > 0.0)
                    {
                        double dist = this.GetNeighborCellDistance(direction);
                        double slope = GetSlope(initiationCell, simulationCell, dist);

                        double dirmult = this.m_TransitionDirectionMultiplierMap.GetDirectionMultiplier(
                            transitionGroupId, simulationCell.StratumId, simulationCell.SecondaryStratumId, simulationCell.TertiaryStratumId, 
                            direction, iteration, timestep);

                        double slopemult = this.m_TransitionSlopeMultiplierMap.GetSlopeMultiplier(
                            transitionGroupId, simulationCell.StratumId, simulationCell.SecondaryStratumId, simulationCell.TertiaryStratumId, 
                            iteration, timestep, slope);

                        double rate = slopemult * dirmult;

                        Debug.Assert(rate >= 0.0);

                        if (rate > 0.0)
                        {
                            double tt = travelTime + (dist / rate);
                            //DevToDo - Change variable name li to something more understandable.
                            double li = Probability / tt;

                            GrowEventRecord Record = new GrowEventRecord(simulationCell, tt, li);

                            eventCandidates.AddRecord(Record);
                            seenBefore.Add(simulationCell.CellId, simulationCell);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Removes the event that is nearest in size to the specified total event amount
        /// </summary>
        /// <param name="transitionEvents"></param>
        /// <param name="totalEventAmount"></param>
        /// <remarks>
        /// This function expects the transition events to be sorted in descending order by target amount.
        /// </remarks>
        private static void RemoveNearestSizedEvent(List<TransitionEvent> transitionEvents, double totalEventAmount)
        {
            if (transitionEvents.Count > 0)
            {
                TransitionEvent RemoveEvent = null;
                double CurrentDifference = double.MaxValue;

                foreach (TransitionEvent TransitionEvent in transitionEvents)
                {
                    double ThisDifference = Math.Abs(totalEventAmount - TransitionEvent.TargetAmount);

                    if (ThisDifference <= CurrentDifference)
                    {
                        RemoveEvent = TransitionEvent;
                        CurrentDifference = ThisDifference;
                    }
                    else
                    {
                        break;
                    }
                }

                Debug.Assert(RemoveEvent != null);
                transitionEvents.Remove(RemoveEvent);
            }
        }

        /// <summary>
        /// Create Spatial Initial Condition files and appropriate config based on Non-spatial Initial Condition configuration
        /// </summary>
        /// <remarks></remarks>
        private void CreateSpatialICFromNonSpatialIC()
        {
            DataRow drta = this.ResultScenario.GetDataSheet(Strings.DATASHEET_NSIC_NAME).GetDataRow();
            var CalcNumCellsFromDist = DataTableUtilities.GetDataBool(drta, Strings.DATASHEET_NSIC_CALC_FROM_DIST_COLUMN_NAME);

            if (CalcNumCellsFromDist)
            {
                this.CreateRastersFromNonRasterICCalcFromDist();
            }
            else
            {
                this.CreateRastersFromNonRasterICNoCalcFromDist();
            }
        }

        /// <summary>
        /// Create Spatial Initial Condition files and appropriate config based on Non-spatial
        /// Initial Condition configuration. Calculate Cell Area based on Distributtion
        /// </summary>
        /// <remarks></remarks>
        private void CreateRastersFromNonRasterICCalcFromDist()
        {
            // Fetch the number of cells from the NS IC setting
            DataRow drrc = this.ResultScenario.GetDataSheet(Strings.DATASHEET_NSIC_NAME).GetDataRow();
            int numCells = Convert.ToInt32(drrc[Strings.DATASHEET_NSIC_NUM_CELLS_COLUMN_NAME], CultureInfo.InvariantCulture);
            double ttlArea = Convert.ToDouble(drrc[Strings.DATASHEET_NSIC_TOTAL_AMOUNT_COLUMN_NAME], CultureInfo.InvariantCulture);

            CreateICSpatialProperties(numCells, ttlArea);

            // Get a list of the Iterations that are defined in the  InitialConditionsDistribution
            var lstIterations = this.m_InitialConditionsDistributions.GetSortedIterationList();

            CellCollection cells = new CellCollection();

            for (int CellId = 0; CellId < numCells; CellId++)
            {
                cells.Add(new Cell(CellId, CellId));
            }

            foreach (var iteration in lstIterations)
            {
                int cellIndex = 0;

                InitialConditionsDistributionCollection icds = this.m_InitialConditionsDistributions.GetForIteration(iteration);
                double sumOfRelativeAmountForIteration = CalcSumOfRelativeAmount(iteration);

                foreach (InitialConditionsDistribution icd in icds)
                {
                    // DEVNOTE:To support multiple iterations, use relativeAmount / sum For Iteration as scale of total number of cells. Number of cells determined by 1st iteration specified. 
                    // Otherwise, there's too much likelyhood that Number of cells will vary per iteration, which we cant/wont support.

                    int NumCellsICD = Convert.ToInt32(Math.Round(icd.RelativeAmount / sumOfRelativeAmountForIteration * numCells));
                    for (int i = 0; i < NumCellsICD; i++)
                    {
                        Cell c = cells[cellIndex];

                        int sisagemin = Math.Min(icd.AgeMin, icd.AgeMax);
                        int sisagemax = Math.Max(icd.AgeMin, icd.AgeMax);

                        int Iter = this.MinimumIteration;

                        if (iteration.HasValue)
                        {
                            Iter = iteration.Value;
                        }

                        this.InitializeCellAge(c, icd.StratumId, icd.StateClassId, sisagemin, sisagemax, Iter, this.m_TimestepZero);

                        c.StratumId = icd.StratumId;
                        c.StateClassId = icd.StateClassId;
                        c.SecondaryStratumId = icd.SecondaryStratumId;
                        c.TertiaryStratumId = icd.TertiaryStratumId;

                        cellIndex += 1;
                    }
                }

                // Randomize the cell distriubtion so we dont get blocks of same  ICD pixels.
                List<Cell> lst = new List<Cell>();
                foreach (Cell c in cells)
                {
                    lst.Add(c);
                }

                ShuffleUtilities.ShuffleList(lst, this.m_RandomGenerator.Random);
                SaveCellsToUndefinedICRasters(lst, iteration);
            }
        }

        /// <summary>
        /// Create Spatial Initial Condition files and appropriate config based on Non-spatial 
        /// Initial Condition configuration. Use entered Cell area (don't Calculate Cell Area based on Distributtion)
        /// </summary>
        /// <remarks></remarks>
        private void CreateRastersFromNonRasterICNoCalcFromDist()
        {
            // Fetch the number of cells from the NS IC setting
            DataRow drrc = this.ResultScenario.GetDataSheet(Strings.DATASHEET_NSIC_NAME).GetDataRow();
            int numCells = Convert.ToInt32(drrc[Strings.DATASHEET_NSIC_NUM_CELLS_COLUMN_NAME], CultureInfo.InvariantCulture);
            double ttlArea = Convert.ToDouble(drrc[Strings.DATASHEET_NSIC_TOTAL_AMOUNT_COLUMN_NAME], CultureInfo.InvariantCulture);

            CreateICSpatialProperties(numCells, ttlArea);

            // Get a list of the Iterations that are defined in the  InitialConditionsDistribution
            var lstIterations = this.m_InitialConditionsDistributions.GetSortedIterationList();

            foreach (var iteration in lstIterations)
            {
                var sumOfRelativeAmount = CalcSumOfRelativeAmount(iteration);

                InitialConditionsDistributionCollection icds = this.m_InitialConditionsDistributions.GetForIteration(iteration);

                CellCollection cells = new CellCollection();

                for (int CellId = 0; CellId < numCells; CellId++)
                {
                    cells.Add(new Cell(CellId, CellId));
                }

                foreach (Cell c in cells)
                {
                    double Rand = this.m_RandomGenerator.GetNextDouble();
                    double CumulativeProportion = 0.0;

                    foreach (InitialConditionsDistribution icd in icds)
                    {
                        CumulativeProportion += (icd.RelativeAmount / sumOfRelativeAmount);

                        if (Rand < CumulativeProportion)
                        {
                            int sisagemin = Math.Min(icd.AgeMin, icd.AgeMax);
                            int sisagemax = Math.Max(icd.AgeMin, icd.AgeMax);

                            int Iter = this.MinimumIteration;

                            if (iteration.HasValue)
                            {
                                Iter = iteration.Value;
                            }

                            this.InitializeCellAge(c, icd.StratumId, icd.StateClassId, sisagemin, sisagemax, Iter, this.m_TimestepZero);

                            c.StratumId = icd.StratumId;
                            c.StateClassId = icd.StateClassId;
                            c.SecondaryStratumId = icd.SecondaryStratumId;
                            c.TertiaryStratumId = icd.TertiaryStratumId;

                            break;
                        }
                    }
                }

                List<Cell> lst = new List<Cell>();
                foreach (Cell c in cells)
                {
                    lst.Add(c);
                }

                SaveCellsToUndefinedICRasters(lst, iteration);
            }
        }

        private bool CanAutoGenerateSSRaster(InitialConditionsSpatial ics, int? iteration)
        {
            Debug.Assert(string.IsNullOrEmpty(ics.SecondaryStratumFileName));

            if (this.Project.GetDataSheet(Strings.DATASHEET_SECONDARY_STRATA_NAME).GetData().DefaultView.Count == 0)
            {
                return false;
            }

            if (this.m_ICDistributionsAutoGenerated)
            {
                return false;
            }

            InitialConditionsDistributionCollection icds = this.m_InitialConditionsDistributionMap.GetICDs(iteration);

            if (icds == null)
            {
                return false;
            }

            foreach (InitialConditionsDistribution icd in icds)
            {
                if (icd.SecondaryStratumId.HasValue)
                {
                    return true;
                }
            }

            return false;
        }

        private bool CanAutoGenerateTSRaster(InitialConditionsSpatial ics, int? iteration)
        {
            Debug.Assert(string.IsNullOrEmpty(ics.TertiaryStratumFileName));

            if (this.Project.GetDataSheet(Strings.DATASHEET_TERTIARY_STRATA_NAME).GetData().DefaultView.Count == 0)
            {
                return false;
            }

            if (this.m_ICDistributionsAutoGenerated)
            {
                return false;
            }

            InitialConditionsDistributionCollection icds = this.m_InitialConditionsDistributionMap.GetICDs(iteration);

            if (icds == null)
            {
                return false;
            }

            foreach (InitialConditionsDistribution icd in icds)
            {
                if (icd.TertiaryStratumId.HasValue)
                {
                    return true;
                }
            }

            return false;
        }

        private bool CanAutoGenerateAgeRaster(InitialConditionsSpatial ics, int? iteration)
        {
            Debug.Assert(string.IsNullOrEmpty(ics.AgeFileName));

            if (this.m_ICDistributionsAutoGenerated)
            {
                return false;
            }

            return (this.m_InitialConditionsDistributionMap.GetICDs(iteration) != null);
        }

        /// <summary>
        /// Create Spatial Initial Condition files and appropriate config based on a combination of Spatial 
        /// and Non-spatial Initial Condition configuration. 
        /// </summary>
        /// <remarks></remarks>
        private void CreateSpatialICFromCombinedIC()
        {
            DataSheet dsIC = this.ResultScenario.GetDataSheet(Strings.DATASHEET_SPIC_NAME);

            // Get a list of the Iterations that are defined in the InitialConditionsSpatials
            var lstIterations = this.m_InitialConditionsSpatialValues.GetSortedIterationList();
            bool StateClassDefined = false;
            bool SecondaryStratumDefined = false;
            bool TertiaryStratumDefined = false;
            bool AgeDefined = false;
            string sMsg = null;
            bool ICFilesCreated = false;

            foreach (var iteration in lstIterations)
            {
                InitialConditionsSpatial ics = this.m_InitialConditionsSpatialMap.GetICS(iteration);
                int[] primary_stratum_cells = null;
                string ssName = ics.SecondaryStratumFileName;
                string tsName = ics.TertiaryStratumFileName;
                string scName = ics.StateClassFileName;
                string ageName = ics.AgeFileName;
                int[] stateclass_cells = new int[1];
                int[] age_cells = new int[1];
                int[] secondary_stratum_cells = new int[1];
                int[] tertiary_stratum_cells = new int[1];
                DataSheet dsRemap = null;

                if (ics.PrimaryStratumFileName.Length == 0)
                {
                    throw new ArgumentException(MessageStrings.ERROR_SPATIAL_PRIMARY_STRATUM_FILE_NOT_DEFINED);
                }

                StateClassDefined = (!string.IsNullOrEmpty(scName));
                SecondaryStratumDefined = (!string.IsNullOrEmpty(ssName));
                TertiaryStratumDefined = (!string.IsNullOrEmpty(tsName));
                AgeDefined = (!string.IsNullOrEmpty(ageName));

                //If all the Spatial files are already defined then there is nothing to 
                //do for this iteration.

                if (StateClassDefined && SecondaryStratumDefined && TertiaryStratumDefined && AgeDefined)
                {
                    continue;
                }

                //If the secondary/tertiary/age rasters are not defined and they can't be auto-generated 
                //then there is nothing to do for this iteration.

                bool AutoGenerateRasters = false;

                if (!SecondaryStratumDefined && this.CanAutoGenerateSSRaster(ics, iteration))
                {
                    AutoGenerateRasters = true;
                }

                if (!TertiaryStratumDefined && this.CanAutoGenerateTSRaster(ics, iteration))
                {
                    AutoGenerateRasters = true;
                }

                if (!AgeDefined && this.CanAutoGenerateAgeRaster(ics, iteration))
                {
                    AutoGenerateRasters = true;
                }

                if (!AutoGenerateRasters)
                {
                    continue;
                }

                // So we've got a PS file defined, so lets load it up
                // Load the Primary Stratum Raster
                string rasterFileName = ics.PrimaryStratumFileName;
                string fullFileName = Spatial.GetSpatialInputFileName(dsIC, rasterFileName, false);
                StochasticTimeRaster PrimaryStratumRaster = new StochasticTimeRaster(fullFileName, RasterDataType.DTInteger);

                // Now lets remap the ID's in the raster to the Stratum PK values
                dsRemap = this.Project.GetDataSheet(Strings.DATASHEET_STRATA_NAME);
                primary_stratum_cells = Spatial.RemapRasterCells(PrimaryStratumRaster.IntCells, dsRemap, Strings.DATASHEET_MAPID_COLUMN_NAME);

                // Load the State Class Raster, if defined
                if (StateClassDefined)
                {
                    fullFileName = Spatial.GetSpatialInputFileName(dsIC, ics.StateClassFileName, false);
                    StochasticTimeRaster StateClassRaster = new StochasticTimeRaster(fullFileName, PrimaryStratumRaster);
                    StateClassRaster.LoadData();

                    // Now lets remap the ID's in the raster to the State Class PK values
                    dsRemap = this.Project.GetDataSheet(Strings.DATASHEET_STATECLASS_NAME);
                    stateclass_cells = Spatial.RemapRasterCells(StateClassRaster.IntCells, dsRemap, Strings.DATASHEET_MAPID_COLUMN_NAME);

                    if (stateclass_cells.Count() != primary_stratum_cells.Count())
                    {
                        throw new DataException(string.Format(CultureInfo.InvariantCulture, 
                            MessageStrings.ERROR_SPATIAL_FILE_MISMATCHED_METADATA, fullFileName, "Different Cell Count"));
                    }
                }

                // Load the Age Raster, if defined
                if (AgeDefined)
                {
                    fullFileName = Spatial.GetSpatialInputFileName(dsIC, ics.AgeFileName, false);
                    StochasticTimeRaster AgeRaster = new StochasticTimeRaster(fullFileName, PrimaryStratumRaster);
                    AgeRaster.LoadData();

                    age_cells = AgeRaster.IntCells;

                    if (age_cells.Count() != primary_stratum_cells.Count())
                    {
                        throw new DataException(string.Format(CultureInfo.InvariantCulture, 
                            MessageStrings.ERROR_SPATIAL_FILE_MISMATCHED_METADATA, fullFileName, "Different Cell Count"));
                    }
                }

                // Load the Secondary Stratum Raster, if defined
                if (SecondaryStratumDefined)
                {
                    fullFileName = Spatial.GetSpatialInputFileName(dsIC, ics.SecondaryStratumFileName, false);
                    StochasticTimeRaster SecondaryStratumRaster = new StochasticTimeRaster(fullFileName, PrimaryStratumRaster);
                    SecondaryStratumRaster.LoadData();

                    // Now lets remap the ID's in the raster to the Secondary Stratum PK values
                    dsRemap = this.Project.GetDataSheet(Strings.DATASHEET_SECONDARY_STRATA_NAME);
                    secondary_stratum_cells = Spatial.RemapRasterCells(SecondaryStratumRaster.IntCells, dsRemap, Strings.DATASHEET_MAPID_COLUMN_NAME);

                    if (secondary_stratum_cells.Count() != primary_stratum_cells.Count())
                    {
                        throw new DataException(string.Format(CultureInfo.InvariantCulture, 
                            MessageStrings.ERROR_SPATIAL_FILE_MISMATCHED_METADATA, fullFileName, "Different Cell Count"));
                    }
                }

                // Load the Tertiary Stratum Raster, if defined
                if (TertiaryStratumDefined)
                {
                    fullFileName = Spatial.GetSpatialInputFileName(dsIC, ics.TertiaryStratumFileName, false);
                    StochasticTimeRaster TertiaryStratumRaster = new StochasticTimeRaster(fullFileName, PrimaryStratumRaster);
                    TertiaryStratumRaster.LoadData();

                    // Now lets remap the ID's in the raster to the Tertiary Stratum PK values
                    dsRemap = this.Project.GetDataSheet(Strings.DATASHEET_TERTIARY_STRATA_NAME);
                    tertiary_stratum_cells = Spatial.RemapRasterCells(TertiaryStratumRaster.IntCells, dsRemap, Strings.DATASHEET_MAPID_COLUMN_NAME);

                    if (tertiary_stratum_cells.Count() != primary_stratum_cells.Count())
                    {
                        throw new DataException(string.Format(CultureInfo.InvariantCulture, 
                            MessageStrings.ERROR_SPATIAL_FILE_MISMATCHED_METADATA, fullFileName, "Different Cell Count"));
                    }
                }

                // Initalize a Cells collection
                CellCollection cells = new CellCollection();
                for (int CellId = 0; CellId < primary_stratum_cells.Count(); CellId++)
                {
                    Cell c = new Cell(CellId, CellId);
                    c.StratumId = primary_stratum_cells[CellId];

                    if (StateClassDefined)
                    {
                        c.StateClassId = stateclass_cells[CellId];
                    }
                    else
                    {
                        c.StateClassId = Spatial.DefaultNoDataValue;
                    }

                    if (AgeDefined)
                    {
                        c.Age = age_cells[CellId];
                    }
                    else
                    {
                        c.Age = Spatial.DefaultNoDataValue;
                    }

                    if (SecondaryStratumDefined)
                    {
                        c.SecondaryStratumId = secondary_stratum_cells[CellId];
                    }
                    else
                    {
                        c.SecondaryStratumId = Spatial.DefaultNoDataValue;
                    }

                    if (TertiaryStratumDefined)
                    {
                        c.TertiaryStratumId = tertiary_stratum_cells[CellId];
                    }
                    else
                    {
                        c.TertiaryStratumId = Spatial.DefaultNoDataValue;
                    }

                    cells.Add(c);
                }

                InitialConditionsDistributionCollection icds = this.m_InitialConditionsDistributionMap.GetICDs(iteration);

                if (icds == null)
                {
                    sMsg = string.Format(CultureInfo.InvariantCulture, 
                        MessageStrings.STATUS_SPATIAL_RUN_USING_COMBINED_IC_MISSING_ICD, iteration.GetValueOrDefault());

                    this.RecordStatus(StatusType.Warning, sMsg);
                }
                else
                {
                    foreach (Cell c in cells)
                    {
                        if (c.StratumId != 0)
                        {
                            // Now lets filter the ICDs by Primary Stratum, and optionally Age, StateClass, and Secondary Stratum 
                            InitialConditionsDistributionCollection filteredICDs = icds.GetFiltered(c);

                            var sumOfRelativeAmount = filteredICDs.CalcSumOfRelativeAmount();

                            double Rand = this.m_RandomGenerator.GetNextDouble();
                            double CumulativeProportion = 0.0;

                            foreach (InitialConditionsDistribution icd in filteredICDs)
                            {
                                CumulativeProportion += (icd.RelativeAmount / sumOfRelativeAmount);

                                if (Rand < CumulativeProportion)
                                {
                                    if (!AgeDefined)
                                    {
                                        int sisagemin = Math.Min(icd.AgeMin, icd.AgeMax);
                                        int sisagemax = Math.Max(icd.AgeMin, icd.AgeMax);

                                        int Iter = this.MinimumIteration;

                                        if (iteration.HasValue)
                                        {
                                            Iter = iteration.Value;
                                        }

                                        this.InitializeCellAge(
                                            c, icd.StratumId, icd.StateClassId, 
                                            sisagemin, sisagemax, 
                                            Iter, this.m_TimestepZero);
                                    }

                                    c.StratumId = icd.StratumId;
                                    c.StateClassId = icd.StateClassId;
                                    c.SecondaryStratumId = icd.SecondaryStratumId;
                                    c.TertiaryStratumId = icd.TertiaryStratumId;

                                    break;
                                }
                            }
                        }
                    }
                }

                List<Cell> lst = new List<Cell>();
                foreach (Cell c in cells)
                {
                    lst.Add(c);
                }

                if (SaveCellsToUndefinedICRasters(lst, ics.Iteration))
                {
                    ICFilesCreated = true;
                }
            }

            if (ICFilesCreated)
            {
                this.RecordStatus(StatusType.Information, MessageStrings.STATUS_SPATIAL_RUN_USING_COMBINED_IC);
            }
        }

        /// <summary>
        /// Create a Initial Condition Spatial Properties record for the current Results Scenario, based on values dervied from Non-Spatial Initial condition settings
        /// </summary>
        /// <param name="numberOfCells">The number of cells </param>
        /// <param name="ttlArea">The total area</param>
        /// <remarks></remarks>
        private void CreateICSpatialProperties(int numberOfCells, double ttlArea)
        {
            // We want a square raster thats just big enough to accomodate the number of cells specified by user
            int numRasterCells = Convert.ToInt32(System.Math.Pow(Math.Ceiling(Math.Sqrt(numberOfCells)), 2));

            DataSheet dsSpicProp = this.ResultScenario.GetDataSheet(Strings.DATASHEET_SPPIC_NAME);
            DataRow drSpIcProp = dsSpicProp.GetDataRow();

            if (drSpIcProp == null)
            {
                drSpIcProp = dsSpicProp.GetData().NewRow();
                dsSpicProp.GetData().Rows.Add(drSpIcProp);
            }
            else
            {
                Debug.Assert(false, "We should not be here if there's already a IC Spatial Properties record defined");
            }

            // We need convert from Terminalogy Units to M2 for Raster.
            double cellSizeTermUnits = ttlArea / numberOfCells;

            string amountlabel = null;
            TerminologyUnit units = 0;

            TerminologyUtilities.GetAmountLabelTerminology(
                this.Project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME), ref amountlabel, ref units);

            string cellSizeUnits = RasterCellSizeUnit.Meter.ToString();
            double convFactor = InitialConditionsSpatialDataSheet.CalcCellArea(1.0, cellSizeUnits, units);
            double cellArea = cellSizeTermUnits / convFactor;

            drSpIcProp[Strings.DATASHEET_SPPIC_NUM_ROWS_COLUMN_NAME] = Convert.ToInt32(Math.Sqrt(numRasterCells), CultureInfo.InvariantCulture);
            drSpIcProp[Strings.DATASHEET_SPPIC_NUM_COLUMNS_COLUMN_NAME] = Convert.ToInt32(Math.Sqrt(numRasterCells), CultureInfo.InvariantCulture);
            drSpIcProp[Strings.DATASHEET_SPPIC_NUM_CELLS_COLUMN_NAME] = numberOfCells;
            drSpIcProp[Strings.DATASHEET_SPPIC_CELL_AREA_COLUMN_NAME] = cellSizeTermUnits;
            drSpIcProp[Strings.DATASHEET_SPPIC_CELL_SIZE_COLUMN_NAME] = Convert.ToDecimal(Math.Sqrt(cellArea), CultureInfo.InvariantCulture);

            // Arbitrary values
            drSpIcProp[Strings.DATASHEET_SPPIC_CELL_SIZE_UNITS_COLUMN_NAME] = cellSizeUnits;
            drSpIcProp[Strings.DATASHEET_SPPIC_YLLCORNER_COLUMN_NAME] = 0;
            drSpIcProp[Strings.DATASHEET_SPPIC_XLLCORNER_COLUMN_NAME] = 0;

            // DEVNOTE: Set Projection  - Corresponds to NAD83 / UTM zone 12N EPSG:26912. Totally arbitrary, but need something to support units of Meters.
            // Use gdalsrsinfo -o WKT_SIMPLE "EPSG:26912" to get the following

            string default_proj_wkt = @"PROJCS[""NAD83 / UTM zone 12N"",
            GEOGCS[""NAD83"",
            DATUM[""North_American_Datum_1983"",
            SPHEROID[""GRS 1980"", 6378137, 298.257222101],
            TOWGS84[0, 0, 0, 0, 0, 0, 0]],
            PRIMEM[""Greenwich"", 0],
            UNIT[""degree"", 0.0174532925199433]],
            PROJECTION[""Transverse_Mercator""],
            PARAMETER[""latitude_of_origin"", 0],
            PARAMETER[""central_meridian"", -111],
            PARAMETER[""scale_factor"", 0.9996],
            PARAMETER[""false_easting"", 500000],
            PARAMETER[""false_northing"", 0],
            UNIT[""metre"", 1]]";

            drSpIcProp[Strings.DATASHEET_SPPIC_SRS_COLUMN_NAME] = default_proj_wkt;

        }

        /// <summary>
        /// Save the values found in the List of Cells to Initial Conditions Spatial Input Raster files, using default naming templates. Note that the file 
        /// will only be saved in currently unspecified in the Initial Conditions Spatial datasheet. Also, update the appropriate
        /// file names in the  Initial Conditions Spatial datasheet.  
        /// </summary>
        /// <param name="cells">A List of Cell objects</param>
        /// <param name="iteration">The iteration that we are creating the raster file(s) for.</param>
        /// <returns>True is a raster file was saved</returns>
        /// <remarks>Raster files will only be created if not already defined in the Initial Conditions Spatial datasheet.</remarks>
        private bool SaveCellsToUndefinedICRasters(List<Cell> cells, int? iteration)
        {
            bool rasterSaved = false;
            int iterVal = 0;

            if (iteration == null)
            {
                iterVal = 0;
            }
            else
            {
                iterVal = iteration.Value;
            }
         
            DataSheet dsSpIcProp = this.ResultScenario.GetDataSheet(Strings.DATASHEET_SPPIC_NAME);
            DataRow drProp = dsSpIcProp.GetDataRow();

            int Width = Convert.ToInt32(drProp[Strings.DATASHEET_SPPIC_NUM_COLUMNS_COLUMN_NAME], CultureInfo.InvariantCulture);
            int Height = Convert.ToInt32(drProp[Strings.DATASHEET_SPPIC_NUM_ROWS_COLUMN_NAME], CultureInfo.InvariantCulture);
            double xll = Convert.ToDouble(drProp[Strings.DATASHEET_SPPIC_XLLCORNER_COLUMN_NAME], CultureInfo.InvariantCulture);
            double yll = Convert.ToDouble(drProp[Strings.DATASHEET_SPPIC_YLLCORNER_COLUMN_NAME], CultureInfo.InvariantCulture);
            double cellsize = Convert.ToDouble(drProp[Strings.DATASHEET_SPPIC_CELL_SIZE_COLUMN_NAME], CultureInfo.InvariantCulture);
            string cellsizeunits = drProp[Strings.DATASHEET_SPPIC_CELL_SIZE_UNITS_COLUMN_NAME].ToString();
            string projection = drProp[Strings.DATASHEET_SPPIC_SRS_COLUMN_NAME].ToString();
            double[] geo = Spatial.CreateGeoTransform((double)xll, (double)yll, Height, (double)cellsize);             
            int numValidCells = cells.Count;

            StochasticTimeRaster TemplateRaster = new StochasticTimeRaster(
                "template",
                RasterDataType.DTInteger, 
                1, 
                Width,
                Height,
                xll, 
                yll, 
                cellsize, 
                cellsizeunits, 
                projection,
                geo,
                Spatial.DefaultNoDataValue,
                false, 
                Spatial.UndefinedRasterBand);

            // We also need to get the datarow for this InitialConditionSpatial
            string filter = null;
            DataSheet dsSpatialIC = this.ResultScenario.GetDataSheet(Strings.DATASHEET_SPIC_NAME);
            DataRow drICS = null;

            if ((iteration == null))
            {
                filter = string.Format(CultureInfo.InvariantCulture, "iteration is null");
            }
            else
            {
                filter = string.Format(CultureInfo.InvariantCulture, "iteration={0}", iteration.Value);
            }

            DataRow[] drICSpatials = dsSpatialIC.GetData().Select(filter);

            if (drICSpatials.Count() == 0)
            {
                drICS = dsSpatialIC.GetData().NewRow();
                if (iteration.HasValue)
                {
                    drICS[Strings.DATASHEET_ITERATION_COLUMN_NAME] = iteration;
                }
                dsSpatialIC.GetData().Rows.Add(drICS);
            }
            else
            {
                drICS = drICSpatials[0];
            }

            // Create Primary Stratum file, if not already defined
            if (string.IsNullOrEmpty(drICS[Strings.DATASHEET_SPIC_STRATUM_FILE_COLUMN_NAME].ToString()))
            {
                string FileName = CreatePrimaryStratumInputRasterFileName(this.ResultScenario, iterVal, 0);
                StochasticTimeRaster rst = new StochasticTimeRaster(FileName, TemplateRaster);
                GeoTiffCompressionType comptype = Spatial.GetGeoTiffCompressionType(this.Library);

                rst.InitIntCells();

                for (var i = 0; i < numValidCells; i++)
                {
                    rst.IntCells[i] = cells[i].StratumId;
                }

                // We need to remap the Primary Stratum PK to the Raster values ( PK - > ID)
                DataSheet dsRemap = this.Project.GetDataSheet(Strings.DATASHEET_STRATA_NAME);
                rst.IntCells = Spatial.RemapRasterCells(rst.IntCells, dsRemap, Strings.DATASHEET_MAPID_COLUMN_NAME, false, Spatial.DefaultNoDataValue);

                rst.Save(comptype);
                drICS[Strings.DATASHEET_SPIC_STRATUM_FILE_COLUMN_NAME] = Path.GetFileName(FileName);
                dsSpatialIC.AddExternalInputFile(FileName);
                rasterSaved = true;
            }
            else
            {
                //If the primary stratum raster is defined then we are going to use that for the template
                //because the current version of the GDAL API returns a slightly different projection string
                //than the one that might be in the spatial initial conditions properties.

                string BaseFileName = (string)drICS[Strings.DATASHEET_SPIC_STRATUM_FILE_COLUMN_NAME];
                string FullFileName = Spatial.GetSpatialInputFileName(dsSpatialIC, BaseFileName, false);

                TemplateRaster = new StochasticTimeRaster(FullFileName, RasterDataType.DTInteger, false, Spatial.DefaultNoDataValue);
            }

            // Create State Class IC raster, if not already defined
            if (string.IsNullOrEmpty(drICS[Strings.DATASHEET_SPIC_STATE_CLASS_FILE_COLUMN_NAME].ToString()))
            {
                string FileName = CreateStateClassInputRasterFileName(this.ResultScenario, iterVal, 0);
                StochasticTimeRaster rst = new StochasticTimeRaster(FileName, TemplateRaster);
                GeoTiffCompressionType comptype = Spatial.GetGeoTiffCompressionType(this.Library);

                rst.InitIntCells();

                for (var i = 0; i < numValidCells; i++)
                {
                    rst.IntCells[i] = cells[i].StateClassId;
                }

                // We need to remap the State Class PK to the Raster values ( PK - > ID)
                DataSheet dsRemap = this.Project.GetDataSheet(Strings.DATASHEET_STATECLASS_NAME);
                rst.IntCells = Spatial.RemapRasterCells(rst.IntCells, dsRemap, Strings.DATASHEET_MAPID_COLUMN_NAME, false, Spatial.DefaultNoDataValue);

                rst.Save(comptype);
                drICS[Strings.DATASHEET_SPIC_STATE_CLASS_FILE_COLUMN_NAME] = Path.GetFileName(FileName);
                dsSpatialIC.AddExternalInputFile(FileName);
                rasterSaved = true;
            }

            // Create Secondary Stratum IC raster , if appropriate and/or not already defined
            if (string.IsNullOrEmpty(drICS[Strings.DATASHEET_SPIC_SECONDARY_STRATUM_FILE_COLUMN_NAME].ToString()))
            {
                string FileName = CreateSecondaryStratumInputRasterFileName(this.ResultScenario, iterVal, 0);
                StochasticTimeRaster rst = new StochasticTimeRaster(FileName, TemplateRaster);
                GeoTiffCompressionType comptype = Spatial.GetGeoTiffCompressionType(this.Library);

                rst.InitIntCells();

                for (var i = 0; i < numValidCells; i++)
                {
                    if (cells[i].SecondaryStratumId.HasValue)
                    {
                        rst.IntCells[i] = cells[i].SecondaryStratumId.Value;
                    }
                }

                // Test the 2nd stratum has values worth exporting
                if (rst.IntCells.Distinct().Count() > 1 || rst.IntCells[0] != Spatial.DefaultNoDataValue)
                {
                    // We need to remap the Stratum PK to the Raster values ( PK - > ID)
                    DataSheet dsRemap = this.Project.GetDataSheet(Strings.DATASHEET_SECONDARY_STRATA_NAME);
                    rst.IntCells = Spatial.RemapRasterCells(rst.IntCells, dsRemap, Strings.DATASHEET_MAPID_COLUMN_NAME, false, Spatial.DefaultNoDataValue);

                    rst.Save(comptype);
                    drICS[Strings.DATASHEET_SPIC_SECONDARY_STRATUM_FILE_COLUMN_NAME] = Path.GetFileName(FileName);
                    dsSpatialIC.AddExternalInputFile(FileName);
                    rasterSaved = true;
                }
            }

            // Create Tertiary Stratum IC raster, if appropriate and/or not already defined
            if (string.IsNullOrEmpty(drICS[Strings.DATASHEET_SPIC_TERTIARY_STRATUM_FILE_COLUMN_NAME].ToString()))
            {
                string FileName = CreateTertiaryStratumInputRasterFileName(this.ResultScenario, iterVal, 0);
                StochasticTimeRaster rst = new StochasticTimeRaster(FileName, TemplateRaster);
                GeoTiffCompressionType comptype = Spatial.GetGeoTiffCompressionType(this.Library);

                rst.InitIntCells();

                for (var i = 0; i < numValidCells; i++)
                {
                    if (cells[i].TertiaryStratumId.HasValue)
                    {
                        rst.IntCells[i] = cells[i].TertiaryStratumId.Value;
                    }
                }

                // Test the Tertiary stratum has values worth exporting
                if (rst.IntCells.Distinct().Count() > 1 || rst.IntCells[0] != Spatial.DefaultNoDataValue)
                {
                    // We need to remap the Tertiary Stratum PK to the Raster values ( PK - > ID)
                    DataSheet dsRemap = this.Project.GetDataSheet(Strings.DATASHEET_TERTIARY_STRATA_NAME);
                    rst.IntCells = Spatial.RemapRasterCells(rst.IntCells, dsRemap, Strings.DATASHEET_MAPID_COLUMN_NAME, false, Spatial.DefaultNoDataValue);

                    rst.Save(comptype);
                    drICS[Strings.DATASHEET_SPIC_TERTIARY_STRATUM_FILE_COLUMN_NAME] = Path.GetFileName(FileName);
                    dsSpatialIC.AddExternalInputFile(FileName);
                    rasterSaved = true;
                }
            }

            // Create Age IC raster, if not already defined
            if (string.IsNullOrEmpty(drICS[Strings.DATASHEET_SPIC_AGE_FILE_COLUMN_NAME].ToString()))
            {
                string FileName = CreateAgeInputRasterFileName(this.ResultScenario, iterVal, 0);
                StochasticTimeRaster rst = new StochasticTimeRaster(FileName, TemplateRaster);
                GeoTiffCompressionType comptype = Spatial.GetGeoTiffCompressionType(this.Library);

                rst.InitIntCells();

                for (var i = 0; i < numValidCells; i++)
                {
                    rst.IntCells[i] = cells[i].Age;
                }

                rst.Save(comptype);
                drICS[Strings.DATASHEET_SPIC_AGE_FILE_COLUMN_NAME] = Path.GetFileName(FileName);
                dsSpatialIC.AddExternalInputFile(FileName);
                rasterSaved = true;
            }

            return rasterSaved;
        }

        /// <summary>
        /// Calculates the cell probability
        /// </summary>
        /// <param name="simulationCell"></param>
        /// <param name="transitionGroupId"></param>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <returns>If the probability excedes 1.0 then it returns 1.0</returns>
        /// <remarks></remarks>
        private double SpatialCalculateCellProbability(Cell simulationCell, int transitionGroupId, int iteration, int timestep)
        {
            Debug.Assert(this.IsSpatial);
            double CellProbability = this.SpatialCalculateCellProbabilityNonTruncated(simulationCell, transitionGroupId, iteration, timestep);

            if (CellProbability > 1.0)
            {
                CellProbability = 1.0;
            }

            return CellProbability;
        }

        /// <summary>
        /// Calculates the cell probability
        /// </summary>
        /// <param name="simulationCell"></param>
        /// <param name="transitionGroupId"></param>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <returns></returns>
        /// <remarks>
        /// If the probability excedes 1 it will not be adjusted in any way.
        /// </remarks>
        private double SpatialCalculateCellProbabilityNonTruncated(Cell simulationCell, int transitionGroupId, int iteration, int timestep)
        {
            Debug.Assert(this.IsSpatial);
            double CellProbability = 0.0;
            TransitionGroup TransitionGroup = this.m_TransitionGroups[transitionGroupId];

            foreach (Transition tr in simulationCell.Transitions)
            {
                if (TransitionGroup.PrimaryTransitionTypes.Contains(tr.TransitionTypeId))
                {
                    double multiplier = GetTransitionMultiplier(tr.TransitionTypeId, iteration, timestep, simulationCell);
                    multiplier *= this.GetExternalTransitionMultipliers(tr.TransitionTypeId, iteration, timestep, simulationCell);

                    TransitionTarget target = this.m_TransitionTargetMap.GetTransitionTarget(
                        TransitionGroup.TransitionGroupId, simulationCell.StratumId, simulationCell.SecondaryStratumId,
                        simulationCell.TertiaryStratumId, iteration, timestep);

                    bool TargetPrioritizationMultiplierApplied = false;

                    if (target != null && !target.IsDisabled && target.HasPrioritizations)
                    {                       
                        TransitionTargetPrioritization pri = target.GetPrioritization(
                            simulationCell.StratumId, simulationCell.SecondaryStratumId,
                            simulationCell.TertiaryStratumId, simulationCell.StateClassId, 
                            iteration, timestep);
                          
                        if (pri != null && pri.ProbabilityOverride.HasValue)
                        {
                            Debug.Assert(pri.ProbabilityOverride.Value == 1.0 || pri.ProbabilityOverride.Value == 0.0);

                            if (pri.ProbabilityOverride.Value == 1.0)
                            {
                                return 1.0;
                            }
                            else if (pri.ProbabilityOverride.Value == 0.0)
                            {
                                return 0.0;
                            }
                        }                           
                        else
                        {
                            multiplier *= pri.ProbabilityMultiplier;
                            TargetPrioritizationMultiplierApplied = true;
                        }                      
                    }

                    if (!TargetPrioritizationMultiplierApplied)
                    {
                        multiplier *= this.GetTransitionTargetMultiplier(
                            TransitionGroup.TransitionGroupId, simulationCell.StratumId, simulationCell.SecondaryStratumId,
                            simulationCell.TertiaryStratumId, iteration, timestep);
                    }
                   
                    if (this.IsSpatial)
                    {
                        multiplier *= this.GetTransitionSpatialMultiplier(simulationCell, tr.TransitionTypeId, iteration, timestep);

                        TransitionType tt = this.m_TransitionTypes[tr.TransitionTypeId];

                        foreach (TransitionGroup tg in tt.TransitionGroups)
                        {
                            multiplier *= this.GetTransitionAdjacencyMultiplier(tg.TransitionGroupId, iteration, timestep, simulationCell);
                            multiplier *= this.GetExternalSpatialMultipliers(simulationCell, iteration, timestep, tg.TransitionGroupId);
                        }
                    }

                    if (this.m_TransitionAttributeTargets.Count > 0)
                    {
                        TransitionType tt = this.TransitionTypes[tr.TransitionTypeId];
                        double? ProbOverride = this.GetAttributeTargetProbabilityOverride(tt, simulationCell, iteration, timestep);

                        if (ProbOverride.HasValue)
                        {
                            if (ProbOverride.Value == 1.0)
                            {
                                return 1.0;
                            }
                            else if (ProbOverride.Value == 0.0)
                            {
                                return 0.0;
                            }
                        }

                        multiplier = this.ModifyMultiplierForTransitionAttributeTarget(multiplier, tt, simulationCell, iteration, timestep);
                    }

                    CellProbability += tr.Probability * tr.Proportion * multiplier;
                }
            }

            return CellProbability;
        }

        /// <summary>
        /// Initializes all simulations cells in Raster mode
        /// </summary>
        /// <remarks></remarks>
        private void InitializeCellsRaster(int iteration)
        {
            Debug.Assert(this.IsSpatial);
            Debug.Assert(this.m_Cells.Count > 0);

            //Loop thru cells and set stratum(s),state class, and age.
            //Note that some cells in the raster don't have a valid state class or stratum.
            //We need to ignore these cells in this routine.

            for (int i = 0; i < this.m_InputRasters.NumberCells; i++)
            {
                // Skip a cell that wasnt initially created because of StateClass or Stratum = 0
                if (!this.m_Cells.Contains(i))
                {
                    continue;
                }

                Cell c = this.m_Cells[i];

                c.StateClassId = this.m_InputRasters.SClassCells[i];
                c.StratumId = this.m_InputRasters.PrimaryStratumCells[i];

                Debug.Assert(!(c.StateClassId == 0 || c.StratumId == 0), "The Cell object should never have been created with StateClass or Stratum = 0");

                if (this.m_InputRasters.SecondaryStratumRaster != null)
                {
                    c.SecondaryStratumId = this.m_InputRasters.SecondaryStratumCells[i];
                }

                if (this.m_InputRasters.TertiaryStratumRaster != null)
                {
                    c.TertiaryStratumId = this.m_InputRasters.TertiaryStratumCells[i];
                }

                if (this.m_InputRasters.AgeRaster == null)
                {
                    this.InitializeCellAge(c, c.StratumId, c.StateClassId, 0, int.MaxValue, iteration, this.m_TimestepZero);
                }
                else
                {
                    c.Age = this.m_InputRasters.AgeCells[i];
                    int ndv = Spatial.DefaultNoDataValue;

                    if (c.Age == ndv && ndv != 0)
                    {
                        this.InitializeCellAge(c, c.StratumId, c.StateClassId, 0, int.MaxValue, iteration, this.m_TimestepZero);
                    }
                    else
                    {
                        DeterministicTransition dt = this.GetDeterministicTransition(c, iteration, this.m_TimestepZero);

                        if (dt != null)
                        {
                            if (c.Age < dt.AgeMinimum || c.Age > dt.AgeMaximum)
                            {
                                if (dt.AgeMaximum == int.MaxValue)
                                {
                                    c.Age = dt.AgeMinimum;
                                }
                                else
                                {
                                    c.Age = this.m_RandomGenerator.GetNextInteger(dt.AgeMinimum, dt.AgeMaximum);
                                }
                            }
                        }
                    }
                }

                this.InitializeCellTstValues(c, iteration, null, true);

#if DEBUG
                this.VALIDATE_INITIALIZED_CELL(c, iteration, this.m_TimestepZero);
#endif

                this.m_Strata[c.StratumId].Cells.Add(c.CellId, c);
                this.m_ProportionAccumulatorMap.AddOrIncrement(c.StratumId, c.SecondaryStratumId, c.TertiaryStratumId);

                this.RecordSummaryStateClassOutput(c, iteration, this.m_TimestepZero);
                this.RecordSummaryTSTOutput(c, iteration, this.m_TimestepZero);
                this.RecordSummaryStateAttributeOutput(c, iteration, this.m_TimestepZero);

                CellInitialized?.Invoke(this, new CellEventArgs(c, iteration, this.m_TimestepZero));
            }

            CellsInitialized?.Invoke(this, new CellEventArgs(null, iteration, this.m_TimestepZero));
        }

        protected virtual string GetStateClassRaster(int iteration, DataSheet dsIC, string stateClassFileName)
        {
            return Spatial.GetSpatialInputFileName(dsIC, stateClassFileName, false);
        }

        protected virtual string GetPrimaryStratumRaster(int iteration, DataSheet dsIC, string primaryStratumFileName)
        {
            return Spatial.GetSpatialInputFileName(dsIC, primaryStratumFileName, false);
        }

        protected virtual string GetSecondaryStratumRaster(int iteration, DataSheet dsIC, string secondaryStratumFileName)
        {
            return Spatial.GetSpatialInputFileName(dsIC, secondaryStratumFileName, false);
        }

        protected virtual string GetTertiaryStratumRaster(int iteration, DataSheet dsIC, string tertiaryStratumFileName)
        {
            return Spatial.GetSpatialInputFileName(dsIC, tertiaryStratumFileName, false);
        }

        protected virtual string GetAgeRaster(int iteration, DataSheet dsIC, string ageFileName)
        {
            return Spatial.GetSpatialInputFileName(dsIC, ageFileName, false);
        }

        protected virtual string GetDemRaster(DataSheet dsIC, string demFileName)
        {
            return Spatial.GetSpatialInputFileName(dsIC, demFileName, false);
        }

        /// <summary>
        /// Fills the raster data if this is a raster model run
        /// </summary>
        /// <remarks></remarks>
        private void InitializeRasterData(int iteration)
        {
            Debug.Assert(this.IsSpatial);
            string Message = null;

            // Now import the rasters, if they are configured in the RasterInitialCondition 
            DataSheet dsIC = this.ResultScenario.GetDataSheet(Strings.DATASHEET_SPIC_NAME);
            InitialConditionsSpatial ics = this.m_InitialConditionsSpatialMap.GetICS(iteration);

            if (ics == null)
            {
                throw new ArgumentException(MessageStrings.ERROR_NO_APPLICABLE_INITIAL_CONDITIONS_SPATIAL_RECORDS);
            }

            if (ics.StateClassFileName == null || ics.PrimaryStratumFileName == null)
            {
                throw new ArgumentException(MessageStrings.ERROR_SPATIAL_FILE_NOT_DEFINED);
            }

            //State Class Raster
            if (this.m_InputRasters.StateClassRaster == null || ics.StateClassFileName != Path.GetFileName(this.m_InputRasters.StateClassName))
            {
                string fullFileName = this.GetStateClassRaster(iteration, dsIC, ics.StateClassFileName);
                this.m_InputRasters.StateClassRaster = this.LoadMaskedInputRaster(fullFileName, RasterDataType.DTInteger);
                DataSheet dsRemap = this.Project.GetDataSheet(Strings.DATASHEET_STATECLASS_NAME);
                this.m_InputRasters.SClassCells = Spatial.RemapRasterCells(this.m_InputRasters.StateClassRaster.IntCells, dsRemap, Strings.DATASHEET_MAPID_COLUMN_NAME);
            }

            //Primary Stratum Raster
            if (this.m_InputRasters.PrimaryStratumRaster == null || ics.PrimaryStratumFileName != Path.GetFileName(this.m_InputRasters.PrimaryStratumName))
            {
                string fullFileName = this.GetPrimaryStratumRaster(iteration, dsIC, ics.PrimaryStratumFileName);
                this.m_InputRasters.PrimaryStratumRaster = this.LoadMaskedInputRaster(fullFileName, RasterDataType.DTInteger);

                DataSheet dsRemap = this.Project.GetDataSheet(Strings.DATASHEET_STRATA_NAME);
                this.m_InputRasters.PrimaryStratumCells = Spatial.RemapRasterCells(this.m_InputRasters.PrimaryStratumRaster.IntCells, dsRemap, Strings.DATASHEET_MAPID_COLUMN_NAME);

                if (string.IsNullOrWhiteSpace(this.m_InputRasters.PrimaryStratumRaster.Projection))
                {
                    Message = string.Format(CultureInfo.InvariantCulture, MessageStrings.STATUS_SPATIAL_FILE_MISSING_PROJECTION_WARNING, fullFileName);
                    this.RecordStatus(StatusType.Information, Message);
                }

                if (iteration == this.MinimumIteration)
                {
                    this.m_InputRasters.SetMetadata(this.m_InputRasters.PrimaryStratumRaster);
                }
            }

            //Secondary Stratum Raster
            if (!string.IsNullOrEmpty(ics.SecondaryStratumFileName))
            {
                if (this.m_InputRasters.SecondaryStratumRaster == null || ics.SecondaryStratumFileName != Path.GetFileName(this.m_InputRasters.SecondaryStratumName))
                {
                    string fullFileName = this.GetSecondaryStratumRaster(iteration, dsIC, ics.SecondaryStratumFileName);
                    this.m_InputRasters.SecondaryStratumRaster = new StochasticTimeRaster(fullFileName, RasterDataType.DTInteger);
                    DataSheet dsRemap = this.Project.GetDataSheet(Strings.DATASHEET_SECONDARY_STRATA_NAME);
                    this.m_InputRasters.SecondaryStratumCells = Spatial.RemapRasterCells(this.m_InputRasters.SecondaryStratumRaster.IntCells, dsRemap, Strings.DATASHEET_MAPID_COLUMN_NAME);
                }
            }
            else
            {
                this.m_InputRasters.SecondaryStratumRaster = null;
            }

            //Tertiary Stratum Raster
            if (!string.IsNullOrEmpty(ics.TertiaryStratumFileName))
            {
                if (this.m_InputRasters.TertiaryStratumRaster == null || ics.TertiaryStratumFileName != Path.GetFileName(this.m_InputRasters.TertiaryStratumName))
                {
                    string fullFileName = this.GetTertiaryStratumRaster(iteration, dsIC, ics.TertiaryStratumFileName);
                    this.m_InputRasters.TertiaryStratumRaster = new StochasticTimeRaster(fullFileName, RasterDataType.DTInteger);
                    DataSheet dsRemap = this.Project.GetDataSheet(Strings.DATASHEET_TERTIARY_STRATA_NAME);
                    this.m_InputRasters.TertiaryStratumCells = Spatial.RemapRasterCells(this.m_InputRasters.TertiaryStratumRaster.IntCells, dsRemap, Strings.DATASHEET_MAPID_COLUMN_NAME);
                }
            }
            else
            {
                this.m_InputRasters.TertiaryStratumRaster = null;
            }

            //Age Raster
            if (!string.IsNullOrEmpty(ics.AgeFileName))
            {
                if (this.m_InputRasters.AgeRaster == null || ics.AgeFileName != Path.GetFileName(this.m_InputRasters.AgeName))
                {
                    string fullFileName = this.GetAgeRaster(iteration, dsIC, ics.AgeFileName);
                    this.m_InputRasters.AgeRaster = new StochasticTimeRaster(fullFileName, RasterDataType.DTInteger);
                }
            }
            else
            {
                this.m_InputRasters.AgeRaster = null;
            }

            //Digital Elevation Model (DEM) Raster
            dsIC = this.ResultScenario.GetDataSheet(Strings.DATASHEET_DIGITAL_ELEVATION_MODEL_NAME);
            DataRow drRIS = dsIC.GetDataRow();

            if (drRIS != null && this.ResultScenario.DisplayName != Constants.STSIMRESOLUTION_SCENARIO_NAME)
            {
                string rasterFileName = drRIS[Strings.DATASHEET_DIGITAL_ELEVATION_MODEL_FILE_NAME_COLUMN_NAME].ToString();

                if (!string.IsNullOrEmpty(rasterFileName))
                {
                    if (this.m_InputRasters.DEMRaster == null || rasterFileName != Path.GetFileName(this.m_InputRasters.DemName))
                    {
                        string fullFileName = this.GetDemRaster(dsIC, rasterFileName);
                        this.m_InputRasters.DEMRaster = new StochasticTimeRaster(fullFileName, RasterDataType.DTDouble);
                    }
                }
                else
                {
                    this.m_InputRasters.DEMRaster = null;
                }
            }
            else
            {
                this.m_InputRasters.DEMRaster = null;
            }

            // Compare the rasters to make sure meta data matches. Note that we might not have loaded a raster 
            // because one of the same name already loaded for a previous iteration.

            CompareMetadataResult cmpResult = 0;
            string cmpMsg = "";

            // State Class
            if (this.m_InputRasters.StateClassRaster != null && this.m_InputRasters.StateClassRaster.TotalCells > 0)
            {
                cmpResult = this.m_InputRasters.CompareMetadata(this.m_InputRasters.StateClassRaster, ref cmpMsg);
                if (cmpResult == CompareMetadataResult.RowColumnMismatch)
                {
                    Message = string.Format(CultureInfo.InvariantCulture, MessageStrings.ERROR_SPATIAL_FILE_MISMATCHED_METADATA, this.m_InputRasters.StateClassName, cmpMsg);
                    throw new STSimException(Message);
                }
                else if (cmpResult == CompareMetadataResult.UnimportantDifferences)
                {
                    Message = string.Format(CultureInfo.InvariantCulture, MessageStrings.STATUS_SPATIAL_FILE_MISMATCHED_METADATA_INFO, this.m_InputRasters.StateClassName, cmpMsg);
                    this.RecordStatus(StatusType.Information, Message);
                }
            }

            // Primary Stratum
            if (this.m_InputRasters.PrimaryStratumRaster != null && this.m_InputRasters.PrimaryStratumRaster.TotalCells > 0)
            {
                cmpResult = this.m_InputRasters.CompareMetadata(this.m_InputRasters.PrimaryStratumRaster, ref cmpMsg);
                if (cmpResult == CompareMetadataResult.RowColumnMismatch)
                {
                    Message = string.Format(CultureInfo.InvariantCulture, MessageStrings.ERROR_SPATIAL_FILE_MISMATCHED_METADATA, this.m_InputRasters.PrimaryStratumName, cmpMsg);
                    throw new STSimException(Message);
                }
                else if (cmpResult == CompareMetadataResult.UnimportantDifferences)
                {
                    Message = string.Format(CultureInfo.InvariantCulture, MessageStrings.STATUS_SPATIAL_FILE_MISMATCHED_METADATA_INFO, this.m_InputRasters.PrimaryStratumName, cmpMsg);
                    this.RecordStatus(StatusType.Information, Message);
                }
            }

            //Secondary Stratum
            if (this.m_InputRasters.SecondaryStratumRaster != null && this.m_InputRasters.SecondaryStratumRaster.TotalCells > 0)
            {
                cmpResult = this.m_InputRasters.CompareMetadata(this.m_InputRasters.SecondaryStratumRaster, ref cmpMsg);
                if (cmpResult == CompareMetadataResult.RowColumnMismatch)
                {
                    Message = string.Format(CultureInfo.InvariantCulture, MessageStrings.ERROR_SPATIAL_FILE_MISMATCHED_METADATA, this.m_InputRasters.SecondaryStratumName, cmpMsg);
                    throw new STSimException(Message);
                }
                else if (cmpResult == CompareMetadataResult.UnimportantDifferences)
                {
                    Message = string.Format(CultureInfo.InvariantCulture, MessageStrings.STATUS_SPATIAL_FILE_MISMATCHED_METADATA_INFO, this.m_InputRasters.SecondaryStratumName, cmpMsg);
                    this.RecordStatus(StatusType.Information, Message);
                }
            }

            //Tertiary Stratum
            if (this.m_InputRasters.TertiaryStratumRaster != null && this.m_InputRasters.TertiaryStratumRaster.TotalCells > 0)
            {
                cmpResult = this.m_InputRasters.CompareMetadata(this.m_InputRasters.TertiaryStratumRaster, ref cmpMsg);
                if (cmpResult == CompareMetadataResult.RowColumnMismatch)
                {
                    Message = string.Format(CultureInfo.InvariantCulture, MessageStrings.ERROR_SPATIAL_FILE_MISMATCHED_METADATA, this.m_InputRasters.TertiaryStratumName, cmpMsg);
                    throw new STSimException(Message);
                }
                else if (cmpResult == CompareMetadataResult.UnimportantDifferences)
                {
                    Message = string.Format(CultureInfo.InvariantCulture, MessageStrings.STATUS_SPATIAL_FILE_MISMATCHED_METADATA_INFO, this.m_InputRasters.TertiaryStratumName, cmpMsg);
                    this.RecordStatus(StatusType.Information, Message);
                }
            }

            // Age
            if (this.m_InputRasters.AgeRaster != null && this.m_InputRasters.AgeRaster.TotalCells > 0)
            {
                cmpResult = this.m_InputRasters.CompareMetadata(this.m_InputRasters.AgeRaster, ref cmpMsg);
                if (cmpResult == CompareMetadataResult.RowColumnMismatch)
                {
                    Message = string.Format(CultureInfo.InvariantCulture, MessageStrings.ERROR_SPATIAL_FILE_MISMATCHED_METADATA, this.m_InputRasters.AgeName, cmpMsg);
                    throw new STSimException(Message);
                }
                else if (cmpResult == CompareMetadataResult.UnimportantDifferences)
                {
                    Message = string.Format(CultureInfo.InvariantCulture, MessageStrings.STATUS_SPATIAL_FILE_MISMATCHED_METADATA_INFO, this.m_InputRasters.AgeName, cmpMsg);
                    this.RecordStatus(StatusType.Information, Message);
                }
            }

            //DEM 
            if (this.m_InputRasters.DEMRaster != null && this.m_InputRasters.DEMRaster.TotalCells > 0)
            {
                cmpResult = this.m_InputRasters.CompareMetadata(this.m_InputRasters.DEMRaster, ref cmpMsg);
                if (cmpResult == CompareMetadataResult.RowColumnMismatch)
                {
                    Message = string.Format(CultureInfo.InvariantCulture, MessageStrings.ERROR_SPATIAL_FILE_MISMATCHED_METADATA, this.m_InputRasters.DemName, cmpMsg);
                    throw new STSimException(Message);
                }
                else if (cmpResult == CompareMetadataResult.UnimportantDifferences)
                {
                    Message = string.Format(CultureInfo.InvariantCulture, MessageStrings.STATUS_SPATIAL_FILE_MISMATCHED_METADATA_INFO, this.m_InputRasters.DemName, cmpMsg);
                    this.RecordStatus(StatusType.Information, Message);
                }
            }       
        }

        /// <summary>
        /// Initializes the average raster state class map
        /// </summary>
        private void InitializeAvgRasterStateClassMap()
        {
            Debug.Assert(this.IsSpatial);
            Debug.Assert(this.MinimumTimestep > 0);

            if (!this.m_CreateAvgRasterStateClassOutput)
            {
                return;
            }

            foreach (StateClass sc in this.m_StateClasses)
            {
                Dictionary<int, double[]> dict = new Dictionary<int, double[]>();

                for (var timestep = this.MinimumTimestep; timestep <= this.MaximumTimestep; timestep++)
                {
                    if (this.IsAvgRasterStateClassTimestep(timestep))
                    {
                        double[] Values = new double[this.Cells.Count];

                        for (var i = 0; i < this.Cells.Count; i++)
                        {
                            Values[i] = 0.0;
                        }

                        dict.Add(timestep, Values);
                    }
                }

                if (!dict.ContainsKey(this.TimestepZero))
                {
                    double[] Values = new double[this.Cells.Count];

                    for (var i = 0; i < this.Cells.Count; i++)
                    {
                        Values[i] = 0.0;
                    }

                    dict.Add(this.TimestepZero, Values);
                }

                this.m_AvgStateClassMap.Add(sc.Id, dict);
            }
        }

        /// <summary>
        /// Initializes the average raster age map
        /// </summary>
        private void InitializeAvgRasterAgeMap()
        {
            Debug.Assert(this.IsSpatial);
            Debug.Assert(this.MinimumTimestep > 0);

            if (!this.m_CreateAvgRasterAgeOutput)
            {
                return;
            }

            for (var timestep = this.MinimumTimestep; timestep <= this.MaximumTimestep; timestep++)
            {
                if (this.IsAvgRasterAgeTimestep(timestep))
                {
                    double[] Values = new double[this.Cells.Count];

                    for (var i = 0; i < this.Cells.Count; i++)
                    {
                        Values[i] = 0.0;
                    }

                    this.m_AvgAgeMap.Add(timestep, Values);
                }
            }

            if (!this.m_AvgAgeMap.ContainsKey(this.TimestepZero))
            {
                double[] Values = new double[this.Cells.Count];

                for (var i = 0; i < this.Cells.Count; i++)
                {
                    Values[i] = 0.0;
                }

                this.m_AvgAgeMap.Add(this.TimestepZero, Values);
            }
        }

        /// <summary>
        /// Initializes the average raster stratum map
        /// </summary>
        private void InitializeAvgRasterStratumMap()
        {
            Debug.Assert(this.IsSpatial);
            Debug.Assert(this.MinimumTimestep > 0);

            if (!this.m_CreateAvgRasterStratumOutput)
            {
                return;
            }

            foreach (Stratum st in this.m_Strata)
            {
                Dictionary<int, double[]> dict = new Dictionary<int, double[]>();

                for (var timestep = this.MinimumTimestep; timestep <= this.MaximumTimestep; timestep++)
                {
                    if (this.IsAvgRasterStratumTimestep(timestep))
                    {
                        double[] Values = new double[this.Cells.Count];

                        for (var i = 0; i < this.Cells.Count; i++)
                        {
                            Values[i] = 0.0;
                        }

                        dict.Add(timestep, Values);
                    }
                }

                if (!dict.ContainsKey(this.TimestepZero))
                {
                    double[] Values = new double[this.Cells.Count];

                    for (var i = 0; i < this.Cells.Count; i++)
                    {
                        Values[i] = 0.0;
                    }

                    dict.Add(this.TimestepZero, Values);
                }

                this.m_AvgStratumMap.Add(st.StratumId, dict);
            }
        }

        /// <summary>
        /// Initializes the Average Raster Transition Probability Maps
        /// </summary>
        /// <remarks></remarks>
        private void InitializeAvgRasterTransitionProbMaps()
        {
            Debug.Assert(this.IsSpatial);
            Debug.Assert(this.MinimumTimestep > 0);

            if (!this.m_CreateAvgRasterTransitionProbOutput)
            {
                return;
            }

            foreach (TransitionGroup tg in this.m_TransitionGroups)
            {
                if (tg.PrimaryTransitionTypes.Count == 0)
                {
                    continue;
                }

                if (!tg.OutputFilter.HasFlag(OutputFilterFlagTransitionGroup.SpatialProbability))
                {
                    continue;
                }

                Dictionary<int, double[]> dict = new Dictionary<int, double[]>();

                for (var timestep = this.MinimumTimestep; timestep <= this.MaximumTimestep; timestep++)
                {
                    if (this.IsAvgRasterTransitionProbTimestep(timestep))
                    {
                        double[] Values = new double[this.Cells.Count];

                        for (var i = 0; i < this.Cells.Count; i++)
                        {
                            Values[i] = 0;
                        }

                        dict.Add(timestep, Values);
                    }
                }

                this.m_AvgTransitionProbMap.Add(tg.TransitionGroupId, dict);
            }
        }

        /// <summary>
        /// Initializes the Average TST Maps
        /// </summary>
        /// <remarks></remarks>
        private void InitializeAvgRasterTSTMaps()
        {
            Debug.Assert(this.IsSpatial);
            Debug.Assert(this.MinimumTimestep > 0);

            if (!this.m_CreateAvgRasterTSTOutput)
            {
                return;
            }

            List<int> TSTGroupIds = this.GetTSTTransitionGroupIds();

            if (TSTGroupIds.Count == 0)
            {
                return;
            }

            foreach (int tgid in TSTGroupIds)
            {
                TransitionGroup tg = this.m_TransitionGroups[tgid];

                if (!tg.OutputFilter.HasFlag(OutputFilterFlagTransitionGroup.AvgSpatialTST))
                {
                    continue;
                }

                Dictionary<int, double[]> dict = new Dictionary<int, double[]>();

                for (var timestep = this.MinimumTimestep; timestep <= this.MaximumTimestep; timestep++)
                {
                    if (this.IsAvgRasterTSTTimestep(timestep))
                    {
                        double[] Values = new double[this.Cells.Count];

                        for (var i = 0; i < this.Cells.Count; i++)
                        {
                            Values[i] = 0.0;
                        }

                        dict.Add(timestep, Values);
                    }
                }

                if (!dict.ContainsKey(this.TimestepZero))
                {
                    double[] Values = new double[this.Cells.Count];

                    for (var i = 0; i < this.Cells.Count; i++)
                    {
                        Values[i] = 0.0;
                    }

                    dict.Add(this.TimestepZero, Values);
                }

                this.m_AvgTSTMap.Add(tgid, dict);
            }
        }

        /// <summary>
        /// Initializes the Average Raster State Attribute Map
        /// </summary>
        /// <remarks></remarks>
        private void InitializeAvgRasterStateAttributeMaps()
        {
            Debug.Assert(this.IsSpatial);
            Debug.Assert(this.MinimumTimestep > 0);

            if (!this.m_CreateAvgRasterStateAttributeOutput)
            {
                return;
            }

            foreach (int AttributeTypeId in this.m_StateAttributeTypeIds.Keys)
            {
                StateAttributeType sat = this.m_StateAttributeTypes[AttributeTypeId];

                if (!sat.OutputFilter.HasFlag(OutputFilterFlagAttribute.AvgSpatial))
                {
                    continue;
                }

                Dictionary<int, double[]> dict = new Dictionary<int, double[]>();

                for (var timestep = this.MinimumTimestep; timestep <= this.MaximumTimestep; timestep++)
                {
                    if (this.IsAvgRasterStateAttributeTimestep(timestep))
                    {
                        double[] Values = new double[this.Cells.Count];

                        for (var i = 0; i < this.Cells.Count; i++)
                        {
                            Values[i] = 0.0;
                        }

                        dict.Add(timestep, Values);
                    }
                }

                if (!dict.ContainsKey(this.TimestepZero))
                {
                    double[] Values = new double[this.Cells.Count];

                    for (var i = 0; i < this.Cells.Count; i++)
                    {
                        Values[i] = 0.0;
                    }

                    dict.Add(this.TimestepZero, Values);
                }

                this.m_AvgStateAttrMap.Add(sat.Id, dict);
            }
        }

        /// <summary>
        /// Initializes the Average Raster Transition Attribute Map
        /// </summary>
        /// <remarks></remarks>
        private void InitializeAvgRasterTransitionAttributeMaps()
        {
            Debug.Assert(this.IsSpatial);
            Debug.Assert(this.MinimumTimestep > 0);

            if (!this.m_CreateAvgRasterTransitionAttributeOutput)
            {
                return;
            }

            foreach (TransitionAttributeType tat in this.m_TransitionAttributeTypes)
            {
                if (!tat.OutputFilter.HasFlag(OutputFilterFlagAttribute.AvgSpatial))
                {
                    continue;
                }

                Dictionary<int, double[]> dict = new Dictionary<int, double[]>();

                for (var timestep = this.MinimumTimestep; timestep <= this.MaximumTimestep; timestep++)
                {
                    if (this.IsAvgRasterTransitionAttributeTimestep(timestep))
                    {
                        double[] Values = new double[this.Cells.Count];

                        for (var i = 0; i < this.Cells.Count; i++)
                        {
                            Values[i] = 0.0;
                        }

                        dict.Add(timestep, Values);
                    }
                }

                this.m_AvgTransitionAttrMap.Add(tat.TransitionAttributeId, dict);
            }
        }

        private bool IsTransitionAttributeTargetExceded(
            Cell simulationCell,
            Transition tr,
            int iteration,
            int timestep)
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
                        AttributeTypeId, tg.TransitionGroupId, simulationCell.StratumId, simulationCell.SecondaryStratumId, simulationCell.TertiaryStratumId,
                        simulationCell.StateClassId, iteration, timestep, simulationCell.Age, simulationCell.TstValues);

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

        private static string CreatePrimaryStratumInputRasterFileName(Scenario scenario, int iteration, int timestep)
        {
            //Name template = Itx-Tsy-Stratum.tif
            string f = string.Format(
                CultureInfo.InvariantCulture,
                "It{0}-Ts{1}-{2}",
                iteration.ToString("0000", CultureInfo.InvariantCulture),
                timestep.ToString("0000", CultureInfo.InvariantCulture),
                Constants.SPATIAL_MAP_STRATUM_FILEPREFIX);

            DataSheet ds = scenario.GetDataSheet(Strings.DATASHEET_SPIC_NAME);
            return Spatial.GetSpatialInputFileNameUnique(ds, f, true);
        }

        private static string CreateSecondaryStratumInputRasterFileName(Scenario scenario, int iteration, int timestep)
        {
            //Name template = Itx-Tsy-secstr.tif
            string f = string.Format(
                CultureInfo.InvariantCulture, "It{0}-Ts{1}-{2}",
                iteration.ToString("0000", CultureInfo.InvariantCulture),
                timestep.ToString("0000", CultureInfo.InvariantCulture),
                Constants.SPATIAL_MAP_SECONDARY_STRATUM_FILEPREFIX);

            DataSheet ds = scenario.GetDataSheet(Strings.DATASHEET_SPIC_NAME);
            return Spatial.GetSpatialInputFileNameUnique(ds, f, true);
        }

        private static string CreateTertiaryStratumInputRasterFileName(Scenario scenario, int iteration, int timestep)
        {
            //Name template = Itx-Tsy-terstr.tif
            string f = string.Format(
                CultureInfo.InvariantCulture,
                "It{0}-Ts{1}-{2}", iteration.ToString("0000", CultureInfo.InvariantCulture),
                timestep.ToString("0000", CultureInfo.InvariantCulture),
                Constants.SPATIAL_MAP_TERTIARY_STRATUM_FILEPREFIX);

            DataSheet ds = scenario.GetDataSheet(Strings.DATASHEET_SPIC_NAME);
            return Spatial.GetSpatialInputFileNameUnique(ds, f, true);
        }

        private static string CreateStateClassInputRasterFileName(Scenario scenario, int iteration, int timestep)
        {
            //Name template = Itx-Tsy-sc.tif
            string f = string.Format(
                CultureInfo.InvariantCulture,
                "It{0}-Ts{1}-{2}", iteration.ToString("0000", CultureInfo.InvariantCulture),
                timestep.ToString("0000", CultureInfo.InvariantCulture),
                Constants.SPATIAL_MAP_STATE_CLASS_FILEPREFIX);

            DataSheet ds = scenario.GetDataSheet(Strings.DATASHEET_SPIC_NAME);
            return Spatial.GetSpatialInputFileNameUnique(ds, f, true);
        }

        private static string CreateAgeInputRasterFileName(Scenario scenario, int iteration, int timestep)
        {
            //Name template = Itx-Tsy-Age.tif
            string f = string.Format(
                CultureInfo.InvariantCulture,
                "It{0}-Ts{1}-{2}.tif",
                iteration.ToString("0000", CultureInfo.InvariantCulture),
                timestep.ToString("0000", CultureInfo.InvariantCulture),
                Constants.SPATIAL_MAP_AGE_FILEPREFIX);

            DataSheet ds = scenario.GetDataSheet(Strings.DATASHEET_SPIC_NAME);
            return Spatial.GetSpatialInputFileNameUnique(ds, f, true);
        }
    }
}
