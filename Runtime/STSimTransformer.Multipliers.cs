// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using SyncroSim.Common;
using SyncroSim.StochasticTime;

namespace SyncroSim.STSim
{
    public partial class STSimTransformer
    {
        private TransitionMultiplierType GetTransitionMultiplierType(int? id)
        {
            foreach (TransitionMultiplierType t in this.m_TransitionMultiplierTypes)
            {
                if (Nullable.Compare(t.TransitionMultiplierTypeId, id) == 0)
                {
                    return t;
                }
            }

            Debug.Assert(false);
            return null;
        }

        private double GetTransitionTargetMultiplier(
            int transitionGroupId, int stratumId, int? secondaryStratumId, int? tertiaryStratumId, int iteration, int timestep)
        {
            TransitionTarget t = this.m_TransitionTargetMap.GetTransitionTarget(
                transitionGroupId, stratumId, secondaryStratumId, tertiaryStratumId, iteration, timestep);

            if (t == null || t.IsDisabled)
            {
                return 1.0;
            }
            else
            {
                return t.Multiplier;
            }
        }

        private double GetTransitionAttributeTargetMultiplier(
            int transitionAttributeTypeId, int stratumId, int? secondaryStratumId, int? tertiaryStratumId, int iteration, int timestep)
        {
            TransitionAttributeTarget t = this.m_TransitionAttributeTargetMap.GetAttributeTarget(
                transitionAttributeTypeId, stratumId, secondaryStratumId, tertiaryStratumId, iteration, timestep);

            if (t == null || t.IsDisabled)
            {
                return 1.0;
            }
            else
            {
                return t.Multiplier;
            }
        }

        private double GetTransitionAdjacencyMultiplier(int transitionGroupId, int iteration, int timestep, Cell simulationCell)
        {
            double? attrvalue = this.GetNeighborhoodAttributeValue(simulationCell, transitionGroupId);

            if (attrvalue == null)
            {
                attrvalue = 0.0;
            }

            double multiplier = this.m_TransitionAdjacencyMultiplierMap.GetAdjacencyMultiplier(
                transitionGroupId, 
                simulationCell.StratumId, 
                simulationCell.SecondaryStratumId, 
                simulationCell.TertiaryStratumId, 
                iteration, timestep, 
                Convert.ToDouble(attrvalue, CultureInfo.InvariantCulture));

            return multiplier;
        }

        private double GetTransitionMultiplier(int transitionTypeId, int iteration, int timestep, Cell simulationCell)
        {
            if (this.m_TransitionMultiplierValues.Count == 0)
            {
#if DEBUG
                foreach (TransitionMultiplierType tmt in this.m_TransitionMultiplierTypes)
                {
                    Debug.Assert(tmt.TransitionMultiplierValueMap == null);
                }
#endif

                return 1.0;
            }

            Debug.Assert(this.m_TransitionMultiplierTypes.Count > 0);

            double Product = 1.0;
            TransitionType tt = this.m_TransitionTypes[transitionTypeId];

            foreach (TransitionMultiplierType tmt in this.m_TransitionMultiplierTypes)
            {
                if (tmt.TransitionMultiplierValueMap != null)
                {
                    foreach (TransitionGroup tg in tt.TransitionGroups)
                    {
                        TransitionMultiplierValue v = tmt.TransitionMultiplierValueMap.GetTransitionMultiplier(
                            tg.TransitionGroupId, 
                            simulationCell.StratumId, 
                            simulationCell.SecondaryStratumId, 
                            simulationCell.TertiaryStratumId,
                            simulationCell.StateClassId, 
                            iteration, timestep);

                        if (v != null)
                        {
                            Product *= v.CurrentValue.Value;
                        }
                    }
                }
            }

            return Product;
        }

        private double GetTransitionSpatialMultiplier(int cellId, int transitionTypeId, int iteration, int timestep)
        {
            if (this.m_TransitionSpatialMultipliers.Count == 0)
            {
#if DEBUG
                foreach (TransitionMultiplierType tmt in this.m_TransitionMultiplierTypes)
                {
                    Debug.Assert(tmt.TransitionSpatialMultiplierMap == null);
                }
#endif

                return 1.0;
            }

            Debug.Assert(this.m_TransitionMultiplierTypes.Count > 0);

            double Product = 1.0;
            TransitionType tt = this.m_TransitionTypes[transitionTypeId];

            foreach (TransitionMultiplierType tmt in this.m_TransitionMultiplierTypes)
            {
                if (tmt.TransitionSpatialMultiplierMap != null)
                {
                    foreach (TransitionGroup tg in tt.TransitionGroups)
                    {
                        TransitionSpatialMultiplier tsmr = tmt.TransitionSpatialMultiplierMap.GetMultiplierRaster(
                            tg.TransitionGroupId, iteration, timestep);

                        if (tsmr != null)
                        {
                            //Using a single instance of each uniquely named TSM raster

                            Debug.Assert(this.m_TransitionSpatialMultiplierRasters.ContainsKey(tsmr.FileName));

                            if (this.m_TransitionSpatialMultiplierRasters.ContainsKey(tsmr.FileName))
                            {
                                StochasticTimeRaster rastMult = this.m_TransitionSpatialMultiplierRasters[tsmr.FileName];
                                double spatialMult = rastMult.DblCells[cellId];

                                //Test for NODATA_VALUE

                                if (spatialMult < 0.0 | MathUtils.CompareDoublesEqual(spatialMult, rastMult.NoDataValue, double.Epsilon))
                                {
                                    spatialMult = 1.0;
                                }

                                Product *= spatialMult;
                            }
                        }
                    }
                }
            }

            return Product;
        }

        private double GetTransitionSpatialInitiationMultiplier(int cellId, int transitionGroupId, int iteration, int timestep)
        {
            if (this.m_TransitionSpatialInitiationMultipliers.Count == 0)
            {
                return 1.0;
            }

            Debug.Assert(this.m_TransitionMultiplierTypes.Count > 0);

            double Product = 1.0;

            foreach (TransitionMultiplierType tmt in this.m_TransitionMultiplierTypes)
            {
                if (tmt.TransitionSpatialInitiationMultiplierMap != null)
                {
                    TransitionSpatialInitiationMultiplier tsmr = tmt.TransitionSpatialInitiationMultiplierMap.GetMultiplierRaster(
                        transitionGroupId, iteration, timestep);

                    if (tsmr != null)
                    {
                        //Using a single instance of each uniquely named TSIM raster

                        Debug.Assert(this.m_TransitionSpatialInitiationMultiplierRasters.ContainsKey(tsmr.FileName));

                        if (this.m_TransitionSpatialInitiationMultiplierRasters.ContainsKey(tsmr.FileName))
                        {
                            StochasticTimeRaster rastMult = this.m_TransitionSpatialInitiationMultiplierRasters[tsmr.FileName];
                            double spatialMult = rastMult.DblCells[cellId];

                            //Test for NODATA_VALUE

                            if (spatialMult < 0.0 | MathUtils.CompareDoublesEqual(spatialMult, rastMult.NoDataValue, double.Epsilon))
                            {
                                spatialMult = 1.0;
                            }

                            Product *= spatialMult;
                        }
                    }
                }
            }

            return Product;
        }

        /// <summary>
        /// Gets all external transition multipliers
        /// </summary>
        /// <param name="transitionTypeId"></param>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <param name="simulationCell"></param>
        /// <returns></returns>
        private double GetExternalTransitionMultipliers(int transitionTypeId, int iteration, int timestep, Cell simulationCell)
        {
            if (ApplyingTransitionMultipliers == null)
            {
                return 1.0;
            }

            double Product = 1.0;
            TransitionType tt = this.m_TransitionTypes[transitionTypeId];

            foreach (TransitionGroup tg in tt.TransitionGroups)
            {
                MultiplierEventArgs args = new MultiplierEventArgs(simulationCell, iteration, timestep, tg.TransitionGroupId);
                ApplyingTransitionMultipliers?.Invoke(this, args);
                Product *= args.Multiplier;
            }

            return Product;
        }

        /// <summary>
        /// Gets all external spatial multipliers
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <param name="transitionGroupId"></param>
        /// <returns></returns>
        private double GetExternalSpatialMultipliers(Cell simulationCell, int iteration, int timestep, int transitionGroupId)
        {
            MultiplierEventArgs args = new MultiplierEventArgs(simulationCell, iteration, timestep, transitionGroupId);
            ApplyingSpatialMultipliers?.Invoke(this, args);
            return args.Multiplier;
        }

        /// <summary>
        /// Modifies a transition multiplier by any transition attribute target multipliers that apply to that transition type
        /// </summary>
        /// <param name="multiplier"></param>
        /// <param name="tt"></param>
        /// <param name="simulationCell"></param>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        private double ModifyMultiplierForTransitionAttributeTarget(double multiplier, TransitionType tt, Cell simulationCell, int iteration, int timestep)
        {
            foreach (int tatId in this.m_TransitionAttributeTypesWithTarget.Keys)
            {
                foreach (TransitionGroup tg in tt.TransitionGroups)
                {
                    double? AttrValue = this.m_TransitionAttributeValueMap.GetAttributeValue(
                        tatId, tg.TransitionGroupId, simulationCell.StratumId, simulationCell.SecondaryStratumId, 
                        simulationCell.TertiaryStratumId, simulationCell.StateClassId, iteration, timestep, simulationCell.Age);

                    if (AttrValue.HasValue)
                    {
                        if (AttrValue.Value > 0.0)
                        {
                            bool TargetPrioritizationMultiplierApplied = false;

                            TransitionAttributeTarget target = this.m_TransitionAttributeTargetMap.GetAttributeTarget(
                                tatId, simulationCell.StratumId, simulationCell.SecondaryStratumId, simulationCell.TertiaryStratumId, 
                                iteration, timestep);

                            if (target != null && !target.IsDisabled && target.HasPrioritizations)
                            {
                                TransitionAttributeTargetPrioritization pri = target.GetPrioritization(
                                    simulationCell.StratumId, simulationCell.SecondaryStratumId,
                                    simulationCell.TertiaryStratumId, tg.TransitionGroupId, simulationCell.StateClassId, 
                                    iteration, timestep);

                                if (pri != null && pri.ProbabilityOverride.HasValue)
                                {
                                    Debug.Assert(pri.ProbabilityOverride.Value == 1.0 || pri.ProbabilityOverride.Value == 0.0);

                                    if (pri.ProbabilityOverride.Value == 1.0)
                                    {
                                        return multiplier;
                                    }
                                    else if (pri.ProbabilityOverride.Value == 0.0)
                                    {
                                        multiplier *= 0.0;
                                        TargetPrioritizationMultiplierApplied = true;
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
                                multiplier *= this.GetTransitionAttributeTargetMultiplier(
                                    tatId, simulationCell.StratumId, simulationCell.SecondaryStratumId, 
                                    simulationCell.TertiaryStratumId, iteration, timestep);

                            }

                            break;
                        }
                    }
                }
            }

            return multiplier;
        }

        private double? GetAttributeTargetProbabilityOverride(TransitionType tt, Cell simulationCell, int iteration, int timestep)
        {
            foreach (int tatId in this.m_TransitionAttributeTypesWithTarget.Keys)
            {
                foreach (TransitionGroup tg in tt.TransitionGroups)
                {
                    double? AttrValue = this.m_TransitionAttributeValueMap.GetAttributeValue(
                        tatId, tg.TransitionGroupId, simulationCell.StratumId, simulationCell.SecondaryStratumId,
                        simulationCell.TertiaryStratumId, simulationCell.StateClassId, iteration, timestep, simulationCell.Age);

                    if (AttrValue.HasValue)
                    {
                        if (AttrValue.Value > 0.0)
                        {
                            TransitionAttributeTarget target = this.m_TransitionAttributeTargetMap.GetAttributeTarget(
                                tatId, simulationCell.StratumId, simulationCell.SecondaryStratumId, simulationCell.TertiaryStratumId,
                                iteration, timestep);

                            if (target != null && !target.IsDisabled && target.HasPrioritizations)
                            {
                                TransitionAttributeTargetPrioritization pri = target.GetPrioritization(
                                    simulationCell.StratumId, simulationCell.SecondaryStratumId,
                                    simulationCell.TertiaryStratumId, tg.TransitionGroupId, simulationCell.StateClassId, 
                                    iteration, timestep);

                                if (pri != null)
                                {
                                    return pri.ProbabilityOverride;
                                }
                            }

                            break;
                        }
                    }
                }
            }

            return null;
        }

        private void ResetTransitionAttributeTargetMultipliers(
            int iteration, int timestep, Dictionary<int, TransitionGroup> remainingTransitionGroups, 
            MultiLevelKeyMap1<Dictionary<int, TransitionAttributeTarget>> tatMap, TransitionGroup currentTransitionGroup)
        {
            if (this.m_TransitionAttributeTargets.Count == 0)
            {
                return;
            }

            foreach (TransitionAttributeTarget tat in this.m_TransitionAttributeTargets)
            {
                if (!tat.IsDisabled)
                {
                    tat.Multiplier = 1.0;
                    tat.ExpectedAmount = 0.0;

                    if (tat.HasPrioritizations)
                    {
                        List<TransitionAttributeTargetPrioritization> pl = tat.GetPrioritizations(iteration, timestep);

                        if (pl != null)
                        {
                            foreach (TransitionAttributeTargetPrioritization pri in pl)
                            {
                                pri.PossibleAmount = 0.0;
                                pri.ExpectedAmount = 0.0;
                                pri.DesiredAmount = null;
                                pri.CumulativePossibleAmount = 0.0;
                                pri.ProbabilityMultiplier = 1.0;
                                pri.ProbabilityOverride = null;
                            }
                        }
                    }
                }
            }

            foreach (Cell simulationCell in this.m_Cells)
            {
                //Only iterate over the transition attribute types that have a target associated with them in this timestep.

                foreach (int tatId in this.m_TransitionAttributeTypesWithTarget.Keys)
                {
                    TransitionAttributeType ta = this.m_TransitionAttributeTypes[tatId];

                    TransitionAttributeTarget Target = this.m_TransitionAttributeTargetMap.GetAttributeTarget(
                        ta.TransitionAttributeId, simulationCell.StratumId, simulationCell.SecondaryStratumId,
                        simulationCell.TertiaryStratumId, iteration, timestep);

                    if (Target == null || Target.IsDisabled)
                    {
                        continue;
                    }

                    foreach (Transition tr in simulationCell.Transitions)
                    {
                        TransitionType tt = this.m_TransitionTypes[tr.TransitionTypeId];
                        bool Found = false;

                        foreach (TransitionGroup rtg in remainingTransitionGroups.Values)
                        {
                            if (tt.PrimaryTransitionGroups.Contains(rtg.TransitionGroupId))
                            {
                                Found = true;
                                break;
                            }
                        }

                        if (!Found)
                        {
                            continue;
                        }

                        if (tt.TransitionGroups.Count == 0)
                        {
                            continue;
                        }

                        if ((tatMap != null) && (currentTransitionGroup != null))
                        {
                            if (tt.TransitionGroups.Contains(currentTransitionGroup.TransitionGroupId))
                            {
                                Dictionary<int, TransitionAttributeTarget> d = tatMap.GetItemExact(Target.StratumId);

                                if (d == null)
                                {
                                    d = new Dictionary<int, TransitionAttributeTarget>();
                                    tatMap.AddItem(Target.StratumId, d);
                                }

                                if (!d.ContainsKey(Target.TransitionAttributeTargetId))
                                {
                                    d.Add(Target.TransitionAttributeTargetId, Target);
                                }
                            }
                        }

                        double TransMult = this.GetTransitionMultiplier(tr.TransitionTypeId, iteration, timestep, simulationCell);
                        TransMult *= this.GetExternalTransitionMultipliers(tr.TransitionTypeId, iteration, timestep, simulationCell);

                        if (this.IsSpatial)
                        {
                            TransMult *= this.GetTransitionSpatialMultiplier(simulationCell.CellId, tr.TransitionTypeId, iteration, timestep);

                            foreach (TransitionGroup tg in tt.TransitionGroups)
                            {
                                TransMult *= this.GetTransitionAdjacencyMultiplier(tg.TransitionGroupId, iteration, timestep, simulationCell);
                                TransMult *= this.GetExternalSpatialMultipliers(simulationCell, iteration, timestep, tg.TransitionGroupId);
                            }

                            Debug.Assert(TransMult >= 0.0);
                        }

                        if (TransMult == 0.0)
                        {
                            continue;
                        }

                        foreach (TransitionGroup tg in tt.TransitionGroups)
                        {
                            if (this.IsSpatial)
                            {
                                if (!remainingTransitionGroups.ContainsKey(tg.TransitionGroupId))
                                {
                                    continue;
                                }
                            }

                            double? AttrValue = this.m_TransitionAttributeValueMap.GetAttributeValue(
                                ta.TransitionAttributeId, tg.TransitionGroupId, simulationCell.StratumId, 
                                simulationCell.SecondaryStratumId, simulationCell.TertiaryStratumId, 
                                simulationCell.StateClassId, iteration, timestep, simulationCell.Age);

                            if (AttrValue.HasValue)
                            {
                                double Expectation = tr.Probability * tr.Proportion * this.m_AmountPerCell * AttrValue.Value * TransMult;
                                Target.ExpectedAmount += Expectation;

                                Debug.Assert(Expectation >= 0.0);
                                Debug.Assert(Target.ExpectedAmount >= 0.0);

                                if (Target.HasPrioritizations)
                                {
                                    TransitionAttributeTargetPrioritization pri = Target.GetPrioritization(
                                        simulationCell.StratumId, simulationCell.SecondaryStratumId, simulationCell.TertiaryStratumId,
                                        tg.TransitionGroupId, simulationCell.StateClassId, iteration, timestep);

                                    if (pri != null)
                                    {
                                        pri.PossibleAmount += this.m_AmountPerCell * AttrValue.Value;
                                        pri.ExpectedAmount += Expectation;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            foreach (TransitionAttributeTarget tat in this.m_TransitionAttributeTargets)
            {
                if (!tat.IsDisabled && tat.ExpectedAmount != 0.0)
                {
                    tat.Multiplier = tat.TargetRemaining / tat.ExpectedAmount;
                    Debug.Assert(tat.Multiplier >= 0.0);

                    if (tat.HasPrioritizations)
                    {
                        double PreviousCumulativeAmount = 0.0;
                        double TotalCumulativePossibleAmount = 0.0;

                        List<TransitionAttributeTargetPrioritization> pl = tat.GetPrioritizations(iteration, timestep);

                        if (pl != null)
                        {
                            foreach (TransitionAttributeTargetPrioritization pri in pl)
                            {
                                TotalCumulativePossibleAmount += pri.PossibleAmount;
                                pri.CumulativePossibleAmount = TotalCumulativePossibleAmount;

                                if (tat.TargetRemaining >= pri.CumulativePossibleAmount)
                                {
                                    pri.ProbabilityOverride = 1.0;
                                }
                                else
                                {
                                    if (tat.TargetRemaining > PreviousCumulativeAmount)
                                    {
                                        pri.DesiredAmount = tat.TargetRemaining - PreviousCumulativeAmount;
                                        pri.ProbabilityMultiplier = pri.DesiredAmount.Value / pri.ExpectedAmount;
                                    }
                                    else
                                    {
                                        pri.ProbabilityOverride = 0.0;
                                    }
                                }

                                PreviousCumulativeAmount = pri.CumulativePossibleAmount;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Resets the transition target multipliers for this cell, iteration, timestep and explicit transition group
        /// </summary>
        /// <param name="iteration">The current iteration</param>
        /// <param name="timestep">The current timestep</param>
        /// <param name="explicitGroup">transition group must be provided if spatial - should be null if non spatial</param>
        /// <remarks></remarks>
        private void ResetTransitionTargetMultipliers(int iteration, int timestep, TransitionGroup explicitGroup)
        {
            Debug.Assert(explicitGroup != null);

            if (this.m_TransitionTargets.Count == 0)
            {
                return;
            }

            foreach (TransitionTarget tt in this.m_TransitionTargets)
            {
                if (!tt.IsDisabled)
                {
                    tt.Multiplier = 1.0;
                    tt.ExpectedAmount = 0.0;

                    if (tt.HasPrioritizations)
                    {
                        List<TransitionTargetPrioritization> pl = tt.GetPrioritizations(iteration, timestep);

                        if (pl != null)
                        {
                            foreach (TransitionTargetPrioritization pri in pl)
                            {
                                pri.PossibleAmount = 0.0;
                                pri.ExpectedAmount = 0.0;
                                pri.DesiredAmount = null;
                                pri.CumulativePossibleAmount = 0.0;
                                pri.ProbabilityMultiplier = 1.0;
                                pri.ProbabilityOverride = null;
                            }
                        }
                    }                    
                }
            }
       
            foreach (Cell simulationCell in this.m_Cells)
            {
                foreach (Transition tr in simulationCell.Transitions)
                {
                    TransitionType ttype = this.m_TransitionTypes[tr.TransitionTypeId];

                    if (this.IsSpatial)
                    {
                        if (!ttype.PrimaryTransitionGroups.Contains(explicitGroup.TransitionGroupId))
                        {
                            continue;
                        }
                    }

                    if (ttype.TransitionGroups.Count == 0)
                    {
                        continue;
                    }

                    double TransMult = this.GetTransitionMultiplier(tr.TransitionTypeId, iteration, timestep, simulationCell);
                    TransMult *= this.GetExternalTransitionMultipliers(tr.TransitionTypeId, iteration, timestep, simulationCell);

                    if (this.IsSpatial)
                    {
                        TransMult *= this.GetTransitionSpatialMultiplier(simulationCell.CellId, tr.TransitionTypeId, iteration, timestep);

                        foreach (TransitionGroup tg in ttype.TransitionGroups)
                        {
                            TransMult *= this.GetTransitionAdjacencyMultiplier(tg.TransitionGroupId, iteration, timestep, simulationCell);
                            TransMult *= this.GetExternalSpatialMultipliers(simulationCell, iteration, timestep, tg.TransitionGroupId);
                        }

                        Debug.Assert(TransMult >= 0.0);
                    }

                    if (TransMult == 0.0)
                    {
                        continue;
                    }

                    foreach (TransitionGroup tgroup in ttype.TransitionGroups)
                    {
                        TransitionTarget tt = this.m_TransitionTargetMap.GetTransitionTarget(
                            tgroup.TransitionGroupId, simulationCell.StratumId, simulationCell.SecondaryStratumId, 
                            simulationCell.TertiaryStratumId, iteration, timestep);

                        if (tt != null && !tt.IsDisabled)
                        {
                            tt.ExpectedAmount += (tr.Probability * tr.Proportion * this.m_AmountPerCell * TransMult);
                            Debug.Assert(tt.ExpectedAmount >= 0.0);

                            if (tt.HasPrioritizations)
                            {
                                TransitionTargetPrioritization pri = tt.GetPrioritization(
                                simulationCell.StratumId, simulationCell.SecondaryStratumId, simulationCell.TertiaryStratumId,
                                simulationCell.StateClassId, iteration, timestep);

                                if (pri != null)
                                {
                                    pri.PossibleAmount += this.m_AmountPerCell;
                                    pri.ExpectedAmount += (tr.Probability * tr.Proportion * this.m_AmountPerCell * TransMult);
                                }
                            }
                        }
                    }
                }
            }

            foreach (TransitionTarget ttarg in this.m_TransitionTargets)
            {
                if (!ttarg.IsDisabled && ttarg.ExpectedAmount != 0)
                {
                    ttarg.Multiplier = ttarg.CurrentValue.Value / ttarg.ExpectedAmount;
                    Debug.Assert(ttarg.Multiplier >= 0.0);

                    if (ttarg.HasPrioritizations)
                    {
                        double PreviousCumulativeAmount = 0.0;
                        double TotalCumulativePossibleAmount = 0.0;

                        List<TransitionTargetPrioritization> pl = ttarg.GetPrioritizations(iteration, timestep);

                        if (pl != null)
                        {
                            foreach (TransitionTargetPrioritization pri in pl)
                            {
                                TotalCumulativePossibleAmount += pri.PossibleAmount;
                                pri.CumulativePossibleAmount = TotalCumulativePossibleAmount;

                                if (ttarg.CurrentValue >= pri.CumulativePossibleAmount)
                                {
                                    pri.ProbabilityOverride = 1.0;
                                }
                                else
                                {
                                    if (ttarg.CurrentValue > PreviousCumulativeAmount)
                                    {
                                        pri.DesiredAmount = ttarg.CurrentValue - PreviousCumulativeAmount;
                                        pri.ProbabilityMultiplier = pri.DesiredAmount.Value / pri.ExpectedAmount;
                                    }
                                    else
                                    {
                                        pri.ProbabilityOverride = 0.0;
                                    }
                                }

                                PreviousCumulativeAmount = pri.CumulativePossibleAmount;
                            }
                        }
                    }                   
                }
            }
        }
    }
}
