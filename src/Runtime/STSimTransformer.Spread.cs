// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Common;
using System.Diagnostics;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    public partial class STSimTransformer
    {
        /// <summary>
        /// Initializes the transition spread groups
        /// </summary>
        /// <remarks></remarks>
        private void InitializeTransitionSpreadGroups()
        {
#if DEBUG

            Debug.Assert(this.IsSpatial);
            Debug.Assert(this.m_TransitionSpreadGroups.Count == 0);

            foreach (TransitionGroup t in this.m_TransitionGroups)
            {
                Debug.Assert(t.TransitionSpreadCells.Count == 0);
            }

#endif

            //Get a unique list of transition groups from the spread distribution records
            Dictionary<int, TransitionSpreadDistribution> dict = new Dictionary<int, TransitionSpreadDistribution>();

            foreach (TransitionSpreadDistribution t in this.m_TransitionSpreadDistributions)
            {
                if (!dict.ContainsKey(t.TransitionGroupId))
                {
                    dict.Add(t.TransitionGroupId, t);
                }
            }

            foreach (TransitionSpreadDistribution t in dict.Values)
            {
                this.m_TransitionSpreadGroups.Add(this.m_TransitionGroups[t.TransitionGroupId]);
            }

            //Associate each spread distribution with its transition spread group
            foreach (TransitionSpreadDistribution t in this.m_TransitionSpreadDistributions)
            {
                TransitionGroup tg = this.m_TransitionGroups[t.TransitionGroupId];
                tg.TransitionSpreadDistributionMap.AddItem(t);
            }

            foreach (TransitionGroup tg in this.m_TransitionSpreadGroups)
            {
                tg.TransitionSpreadDistributionMap.Normalize();
            }
        }

        /// <summary>
        /// Applies the transition spread for the specified iteration and timestep
        /// </summary>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <remarks></remarks>
        private void ApplyTransitionSpread(
            int iteration, 
            int timestep, 
            Dictionary<int, double[]> rasterTransitionAttrValues, 
            Dictionary<int, int[]> dictTransitionedPixels)
        {
            Debug.Assert(this.IsSpatial);

            foreach (TransitionGroup SpreadGroup in this.m_TransitionSpreadGroups)
            {
                if (SpreadGroup.TransitionSpreadCells.Count > 0)
                {
                    CellCollection ContagiousCellCollection = new CellCollection();

                    foreach (Cell Cell in SpreadGroup.TransitionSpreadCells.Values)
                    {
                        ContagiousCellCollection.Add(Cell);
                    }

                    foreach (Cell ContagionCell in ContagiousCellCollection)
                    {
                        Action<Cell, Cell, TransitionGroup, int, int, bool, CardinalDirection> ApplyFuncCheckNull = 
                            (Cell c1, Cell c2, TransitionGroup tg, int i, int t, bool b, CardinalDirection d) =>
                        {
                            if (c2 != null)
                            {
                                this.ApplyTransitionSpread(c1, c2, tg, i, t, b, d, rasterTransitionAttrValues, dictTransitionedPixels);
                            }                            
                        };

                        ApplyFuncCheckNull(ContagionCell, this.GetCellNorth(ContagionCell), SpreadGroup, iteration, timestep, false, CardinalDirection.N);
                        ApplyFuncCheckNull(ContagionCell, this.GetCellNortheast(ContagionCell), SpreadGroup, iteration, timestep, true, CardinalDirection.NE);
                        ApplyFuncCheckNull(ContagionCell, this.GetCellEast(ContagionCell), SpreadGroup, iteration, timestep, false, CardinalDirection.E);
                        ApplyFuncCheckNull(ContagionCell, this.GetCellSoutheast(ContagionCell), SpreadGroup, iteration, timestep, true, CardinalDirection.SE);
                        ApplyFuncCheckNull(ContagionCell, this.GetCellSouth(ContagionCell), SpreadGroup, iteration, timestep, false, CardinalDirection.S);
                        ApplyFuncCheckNull(ContagionCell, this.GetCellSouthwest(ContagionCell), SpreadGroup, iteration, timestep, true, CardinalDirection.SW);
                        ApplyFuncCheckNull(ContagionCell, this.GetCellWest(ContagionCell), SpreadGroup, iteration, timestep, false, CardinalDirection.W);
                        ApplyFuncCheckNull(ContagionCell, this.GetCellNorthwest(ContagionCell), SpreadGroup, iteration, timestep, true, CardinalDirection.NW);
                    }
                }
            }
        }

        private void ApplyTransitionSpread(Cell contagionCell, Cell neighboringCell, TransitionGroup spreadGroup, int iteration, int timestep, bool isDiagonal, CardinalDirection direction, Dictionary<int, double[]> rasterTransitionAttrValues, Dictionary<int, int[]> dictTransitionedPixels)
        {
            Debug.Assert(this.IsSpatial);

            //Get the cell probability.  If it is less than or equal to zero we don't need to continue

            //DEVTODO: LEO, is this OK with the new Transition Prioritization Code?

            double CellProbability = this.SpatialCalculateCellProbability(neighboringCell, spreadGroup.TransitionGroupId, iteration, timestep);

            if (CellProbability <= 0.0)
            {
                return;
            }

            //Get the transition pathway.  If there isn't one we don't need to continue
            Transition tr = this.SelectTransitionPathway(neighboringCell, spreadGroup.TransitionGroupId, iteration, timestep);

            if (tr == null)
            {
                return;
            }

            //Prepare a TST value with a default of 1.  If we can find a TST group for the contagion cell's stratum and transition type, and 
            //the the contagion cells TST values contains that group, then use that TST value.

            int tstvalue = 1;

            TstTransitionGroup tstgroup = this.m_TstTransitionGroupMap.GetGroup(
                tr.TransitionTypeId, contagionCell.StratumId, contagionCell.SecondaryStratumId, contagionCell.TertiaryStratumId);

            if (tstgroup != null)
            {
                if (contagionCell.TstValues.Contains(tstgroup.TSTGroupId))
                {
                    int contagionTstValue = contagionCell.TstValues[tstgroup.TSTGroupId].TstValue;
                    if (contagionTstValue != int.MaxValue)
                    {
                        tstvalue = contagionTstValue;
                    }
                }
            }

            if (tstvalue > 0)
            {
                double MinThreshold = 0;
                double MaxThreshold = 0;
                double SpreadDistance = this.CalculateSpreadDistance(contagionCell, tstvalue, spreadGroup, iteration, timestep);
                double NeighborDistance = this.GetNeighborCellDistance(direction);
                double Slope = GetSlope(contagionCell, neighboringCell, NeighborDistance);

                SpreadDistance *= this.m_TransitionDirectionMultiplierMap.GetDirectionMultiplier(spreadGroup.TransitionGroupId, contagionCell.StratumId, contagionCell.SecondaryStratumId, contagionCell.TertiaryStratumId, direction, iteration, timestep);
                SpreadDistance *= this.m_TransitionSlopeMultiplierMap.GetSlopeMultiplier(spreadGroup.TransitionGroupId, contagionCell.StratumId, contagionCell.SecondaryStratumId, contagionCell.TertiaryStratumId, iteration, timestep, Slope);

                if (isDiagonal)
                {
                    MinThreshold = this.m_InputRasters.GetCellSizeDiagonalMeters() / 2;
                    MaxThreshold = this.m_InputRasters.GetCellSizeDiagonalMeters() * 1.5;
                }
                else
                {
                    MinThreshold = this.m_InputRasters.GetCellSizeMeters() / 2;
                    MaxThreshold = this.m_InputRasters.GetCellSizeMeters() * 1.5;
                }

                if (SpreadDistance >= MinThreshold && SpreadDistance <= MaxThreshold)
                {
                    this.RecordSummaryTransitionOutput(neighboringCell, tr, iteration, timestep, null);
                    this.RecordSummaryTransitionByStateClassOutput(neighboringCell, tr, iteration, timestep);

                    this.ChangeCellForProbabilisticTransition(neighboringCell, spreadGroup, tr, iteration, timestep, rasterTransitionAttrValues);
                    this.UpdateTransitionedPixels(neighboringCell, tr.TransitionTypeId, dictTransitionedPixels[spreadGroup.TransitionGroupId]);
                    this.FillProbabilisticTransitionsForCell(neighboringCell, iteration, timestep);
                }
                else if (SpreadDistance > MaxThreshold)
                {
                    int randdirection = this.m_RandomGenerator.GetNextInteger(0, 360);
                    Cell DistantCell = GetCellByDistanceAndDirection(contagionCell, randdirection, SpreadDistance);

                    if (DistantCell != null)
                    {
                        Transition DistantTransition = this.SelectTransitionPathway(DistantCell, spreadGroup.TransitionGroupId, iteration, timestep);

                        if (DistantTransition != null)
                        {
                            this.RecordSummaryTransitionOutput(DistantCell, DistantTransition, iteration, timestep, null);
                            this.RecordSummaryTransitionByStateClassOutput(DistantCell, DistantTransition, iteration, timestep);

                            this.ChangeCellForProbabilisticTransition(DistantCell, spreadGroup, DistantTransition, iteration, timestep, rasterTransitionAttrValues);
                            this.UpdateTransitionedPixels(DistantCell, DistantTransition.TransitionTypeId, dictTransitionedPixels[spreadGroup.TransitionGroupId]);
                            this.FillProbabilisticTransitionsForCell(DistantCell, iteration, timestep);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Calculates the spread distance for the specified criteria
        /// </summary>
        /// <param name="contagionCell"></param>
        /// <param name="tstValue"></param>
        /// <param name="spreadGroup"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private double CalculateSpreadDistance(Cell contagionCell, int tstValue, TransitionGroup spreadGroup, int iteration, int timestep)
        {
            Debug.Assert(tstValue > 0);
            Debug.Assert(this.IsSpatial);

            double SpreadDistance = 0.0;

            for (int tstval = 1; tstval <= tstValue; tstval++)
            {
                double rand = this.m_RandomGenerator.GetNextDouble();
                double PreviousCumulativeProportion = 0.0;
                double CumulativeProportion = 0.0;

                List<TransitionSpreadDistribution> lst = spreadGroup.TransitionSpreadDistributionMap.GetDistributionList(contagionCell.StratumId, contagionCell.StateClassId, iteration, timestep);

                if (lst == null)
                {
                    continue;
                }

                foreach (TransitionSpreadDistribution tsd in lst)
                {
                    PreviousCumulativeProportion = CumulativeProportion;
                    CumulativeProportion += tsd.Proportion;

                    if (CumulativeProportion > rand)
                    {
                        double diff1 = (rand - PreviousCumulativeProportion);

                        if (diff1 == 0.0)
                        {
                            SpreadDistance += tsd.MinimumDistance;
                        }
                        else
                        {
                            double diff2 = (CumulativeProportion - PreviousCumulativeProportion);
                            Debug.Assert(diff2 >= diff1);

                            if (diff1 == diff2)
                            {
                                SpreadDistance += tsd.MaximumDistance;
                            }
                            else
                            {
                                double diff3 = (tsd.MaximumDistance - tsd.MinimumDistance);
                                SpreadDistance += (tsd.MinimumDistance + ((diff1 / diff2) * diff3));
                            }
                        }

                        break;
                    }
                }
            }

            return SpreadDistance;
        }

        /// <summary>
        /// Updates the transition spread group membership for the specified cell
        /// </summary>
        /// <param name="cell"></param>
        /// <remarks></remarks>
        private void UpdateTransitionsSpreadGroupMembership(Cell cell, int iteration, int timestep)
        {
            Debug.Assert(this.IsSpatial);

            foreach (TransitionGroup t in this.m_TransitionSpreadGroups)
            {
                if (t.TransitionSpreadDistributionMap.GetDistributionList(cell.StratumId, cell.StateClassId, iteration, timestep) != null)
                {
                    if (!t.TransitionSpreadCells.ContainsKey(cell.CellId))
                    {
                        t.TransitionSpreadCells.Add(cell.CellId, cell);
                    }
                }
                else
                {
                    if (t.TransitionSpreadCells.ContainsKey(cell.CellId))
                    {
                        t.TransitionSpreadCells.Remove(cell.CellId);
                    }
                }
            }
        }

        /// <summary>
        /// Resets the transition spread group cells
        /// </summary>
        /// <remarks></remarks>
        private void ResetTransitionSpreadGroupCells()
        {
            ShuffleUtilities.ShuffleList(this.m_TransitionSpreadGroups, this.m_RandomGenerator.Random);

            foreach (TransitionGroup t in this.m_TransitionSpreadGroups)
            {
                t.TransitionSpreadCells.Clear();
            }

            foreach (Cell c in this.m_Cells)
            {
                foreach (TransitionGroup t in this.m_TransitionSpreadGroups)
                {
                    if (t.TransitionSpreadDistributionMap.HasDistributionRecords(c.StratumId, c.StateClassId))
                    {
                        t.TransitionSpreadCells.Add(c.CellId, c);
                    }
                }
            }
        }
    }
}
