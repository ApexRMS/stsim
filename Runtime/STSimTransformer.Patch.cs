// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace SyncroSim.STSim
{
    public partial class STSimTransformer
    {
        /// <summary>
        /// Selects a patch initiation cell
        /// </summary>
        /// <param name="transitionGroup"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private Cell SelectPatchInitiationCell(TransitionGroup transitionGroup)
        {
            Debug.Assert(this.IsSpatial);

            PatchPrioritization pp = transitionGroup.PatchPrioritization;
            List<TransitionPatch> patches = pp.TransitionPatches;

            if (patches.Count == 0)
            {
                return null;
            }

            TransitionPatch Patch = patches[0];

            if (pp.PatchPrioritizationType == PatchPrioritizationType.Largest ||
                pp.PatchPrioritizationType == PatchPrioritizationType.LargestEdgesOnly)
            {
                Patch = patches[patches.Count - 1];
            }

            if (pp.PatchPrioritizationType == PatchPrioritizationType.SmallestEdgesOnly || 
                pp.PatchPrioritizationType == PatchPrioritizationType.LargestEdgesOnly)
            {
                if (Patch.EdgeCells.ContainsKey(Patch.SeedCell.CellId))
                {
                    return Patch.SeedCell;
                }
                else
                {
                    return Patch.EdgeCells.Values.ElementAt(0);
                }
            }
            else
            {
                if (Patch.AllCells.ContainsKey(Patch.SeedCell.CellId))
                {
                    return Patch.SeedCell;
                }
                else
                {
                    return Patch.AllCells.Values.ElementAt(0);
                }
            }
        }

        /// <summary>
        /// Assigns patch prioritizations based on the current iteration and timestep
        /// </summary>
        /// <remarks></remarks>
        private void AssignPatchPrioritizations(int iteration, int timestep)
        {
            if (!this.IsSpatial)
            {
                return;
            }

            foreach (TransitionGroup tg in this.m_TransitionGroups)
            {
                tg.PatchPrioritization = null;

                TransitionPatchPrioritization tpp = this.m_TransitionPatchPrioritizationMap.GetPatchPrioritization(
                    tg.TransitionGroupId, iteration, timestep);

                if (tpp != null)
                {
                    tg.PatchPrioritization = this.m_PatchPrioritizations[tpp.PatchPrioritizationId];
                }
            }
        }

        /// <summary>
        /// Determines if the specified cell is a edge cell in the specified patch
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="patch"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool IsPatchEdgeCell(Cell cell, TransitionPatch patch)
        {
            if (!CellInPatch(this.GetCellNorth(cell), patch))
            {
                return true;
            }

            if (!CellInPatch(this.GetCellNortheast(cell), patch))
            {
                return true;
            }

            if (!CellInPatch(this.GetCellEast(cell), patch))
            {
                return true;
            }

            if (!CellInPatch(this.GetCellSoutheast(cell), patch))
            {
                return true;
            }

            if (!CellInPatch(this.GetCellSouth(cell), patch))
            {
                return true;
            }

            if (!CellInPatch(this.GetCellSouthwest(cell), patch))
            {
                return true;
            }

            if (!CellInPatch(this.GetCellWest(cell), patch))
            {
                return true;
            }

            if (!CellInPatch(this.GetCellNorthwest(cell), patch))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines if the specified cell is part of the specified patch
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="patch"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private static bool CellInPatch(Cell cell, TransitionPatch patch)
        {
            if (cell == null)
            {
                return false;
            }

            if (patch.AllCells.ContainsKey(cell.CellId))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Updates the patch membership for the specified transition group and cell
        /// </summary>
        /// <param name="transitionGroupId"></param>
        /// <param name="cell"></param>
        /// <remarks>
        /// This function will also remove a patch if its cell collection becomes empty.
        /// </remarks>
        private void UpdateCellPatchMembership(int transitionGroupId, Cell cell)
        {
            TransitionGroup TransitionGroup = this.m_TransitionGroups[transitionGroupId];

            if (TransitionGroup.PatchPrioritization != null)
            {
                TransitionPatch Patch = null;

                if (
                    TransitionGroup.PatchPrioritization.PatchPrioritizationType == PatchPrioritizationType.Smallest || 
                    TransitionGroup.PatchPrioritization.PatchPrioritizationType == PatchPrioritizationType.SmallestEdgesOnly)
                {
                    Patch = TransitionGroup.PatchPrioritization.TransitionPatches.First();
                }
                else
                {
                    Patch = TransitionGroup.PatchPrioritization.TransitionPatches.Last();
                }

                if (Patch.AllCells.ContainsKey(cell.CellId))
                {
                    Patch.AllCells.Remove(cell.CellId);
                }

                if (Patch.EdgeCells.ContainsKey(cell.CellId))
                {
                    Patch.EdgeCells.Remove(cell.CellId);
                }

                if (
                    TransitionGroup.PatchPrioritization.PatchPrioritizationType == PatchPrioritizationType.LargestEdgesOnly || 
                    TransitionGroup.PatchPrioritization.PatchPrioritizationType == PatchPrioritizationType.SmallestEdgesOnly)
                {
                    if (Patch.EdgeCells.Count() == 0)
                    {
                        TransitionGroup.PatchPrioritization.TransitionPatches.Remove(Patch);
                    }
                }
                else if (Patch.AllCells.Count() == 0)
                {
                    Debug.Assert(Patch.EdgeCells.Count() == 0);
                    TransitionGroup.PatchPrioritization.TransitionPatches.Remove(Patch);
                }
            }
        }

        /// <summary>
        /// Clears the transition patches for the specified transition group
        /// </summary>
        /// <param name="transitionGroup"></param>
        /// <remarks></remarks>
        private void ClearTransitionPatches(TransitionGroup transitionGroup)
        {
            Debug.Assert(this.IsSpatial);

            if (transitionGroup.PatchPrioritization != null)
            {
                transitionGroup.PatchPrioritization.TransitionPatches.Clear();
            }
        }

        /// <summary>
        /// Grows a transition patch
        /// </summary>
        /// <param name="transitionedCells"></param>
        /// <param name="patchCells"></param>
        /// <param name="initiationCell"></param>
        /// <param name="patch"></param>
        /// <param name="transitionGroup"></param>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <remarks></remarks>
        private void GrowTransitionPatch(
            Dictionary<int, Cell> transitionedCells, Dictionary<int, Cell> patchCells, Cell initiationCell, 
            TransitionPatch patch, TransitionGroup transitionGroup, int iteration, int timestep)
        {
            double PatchSize = 0.0;
            Queue<Cell> PatchQueue = new Queue<Cell>();
            Dictionary<int, Cell> QueuedCells = new Dictionary<int, Cell>();

            PatchQueue.Enqueue(initiationCell);
            QueuedCells.Add(initiationCell.CellId, initiationCell);

            while (PatchQueue.Count > 0)
            {
                Cell CurrentCell = PatchQueue.Dequeue();

                Debug.Assert(!patchCells.ContainsKey(CurrentCell.CellId));
                Debug.Assert(!patch.AllCells.ContainsKey(CurrentCell.CellId));

                patchCells.Add(CurrentCell.CellId, CurrentCell);
                patch.AllCells.Add(CurrentCell.CellId, CurrentCell);

                PatchSize += this.m_AmountPerCell;

                this.AddNeighboringPatchCell(transitionedCells, patchCells, QueuedCells, PatchQueue, this.GetCellNorth(CurrentCell), transitionGroup, iteration, timestep);
                this.AddNeighboringPatchCell(transitionedCells, patchCells, QueuedCells, PatchQueue, this.GetCellNortheast(CurrentCell), transitionGroup, iteration, timestep);
                this.AddNeighboringPatchCell(transitionedCells, patchCells, QueuedCells, PatchQueue, this.GetCellEast(CurrentCell), transitionGroup, iteration, timestep);
                this.AddNeighboringPatchCell(transitionedCells, patchCells, QueuedCells, PatchQueue, this.GetCellSoutheast(CurrentCell), transitionGroup, iteration, timestep);
                this.AddNeighboringPatchCell(transitionedCells, patchCells, QueuedCells, PatchQueue, this.GetCellSouth(CurrentCell), transitionGroup, iteration, timestep);
                this.AddNeighboringPatchCell(transitionedCells, patchCells, QueuedCells, PatchQueue, this.GetCellSouthwest(CurrentCell), transitionGroup, iteration, timestep);
                this.AddNeighboringPatchCell(transitionedCells, patchCells, QueuedCells, PatchQueue, this.GetCellWest(CurrentCell), transitionGroup, iteration, timestep);
                this.AddNeighboringPatchCell(transitionedCells, patchCells, QueuedCells, PatchQueue, this.GetCellNorthwest(CurrentCell), transitionGroup, iteration, timestep);
            }

            patch.Size = PatchSize;

            foreach (var PatchCell in patch.AllCells.Values)
            {
                if (this.IsPatchEdgeCell(PatchCell, patch))
                {
                    patch.EdgeCells.Add(PatchCell.CellId, PatchCell);
                }
            }

            Debug.Assert(patch.EdgeCells.Count() > 0);
            Debug.Assert(patch.AllCells.Count() > 0);
            Debug.Assert(patch.AllCells.Count() >= patch.EdgeCells.Count());
        }

        /// <summary>
        /// Adds a neighboring patch cell
        /// </summary>
        /// <param name="transitionedCells"></param>
        /// <param name="patchCells"></param>
        /// <param name="queuedCells"></param>
        /// <param name="patchQueue"></param>
        /// <param name="neighborCell"></param>
        /// <param name="transitionGroup"></param>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <remarks></remarks>
        private void AddNeighboringPatchCell
            (Dictionary<int, Cell> transitionedCells, Dictionary<int, Cell> patchCells, Dictionary<int, Cell> queuedCells, 
            Queue<Cell> patchQueue, Cell neighborCell, TransitionGroup transitionGroup, int iteration, int timestep)
        {
            if (neighborCell != null)
            {
                if (
                    queuedCells.ContainsKey(neighborCell.CellId) || 
                    patchCells.ContainsKey(neighborCell.CellId) || 
                    transitionedCells.ContainsKey(neighborCell.CellId))
                {
                    return;
                }

                if (this.SelectSpatialTransitionPathway(neighborCell, transitionGroup.TransitionGroupId, iteration, timestep) == null)
                {
                    return;
                }

                queuedCells.Add(neighborCell.CellId, neighborCell);
                patchQueue.Enqueue(neighborCell);
            }
        }

        /// <summary>
        /// Fills the transition patches for the specified transition group
        /// </summary>
        /// <param name="transitionedCells"></param>
        /// <param name="stratum"></param>
        /// <param name="transitionGroup"></param>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <remarks></remarks>
        private void FillTransitionPatches(
            Dictionary<int, Cell> transitionedCells, Stratum stratum, 
            TransitionGroup transitionGroup, int iteration, int timestep)
        {
            Debug.Assert(this.IsSpatial);

            if (transitionGroup.PatchPrioritization == null)
            {
                return;
            }

            Debug.Assert(transitionGroup.PatchPrioritization.TransitionPatches.Count == 0);

            Dictionary<int, Cell> patchCells = new Dictionary<int, Cell>();

            foreach (Cell SimulationCell in stratum.Cells.Values)
            {
                Debug.Assert(SimulationCell.StratumId != 0);
                Debug.Assert(SimulationCell.StateClassId != 0);

                if (patchCells.ContainsKey(SimulationCell.CellId) | transitionedCells.ContainsKey(SimulationCell.CellId))
                {
                    continue;
                }

                if (this.SelectSpatialTransitionPathway(SimulationCell, transitionGroup.TransitionGroupId, iteration, timestep) == null)
                {
                    continue;
                }

                TransitionPatch Patch = new TransitionPatch(SimulationCell);

                this.GrowTransitionPatch(transitionedCells, patchCells, SimulationCell, Patch, transitionGroup, iteration, timestep);

                transitionGroup.PatchPrioritization.TransitionPatches.Add(Patch);
            }

            transitionGroup.PatchPrioritization.TransitionPatches.Sort((TransitionPatch p1, TransitionPatch p2) =>
            {
                return (p1.Size.CompareTo(p2.Size));
            });
        }
    }
}
