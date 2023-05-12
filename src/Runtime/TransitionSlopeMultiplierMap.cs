// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Linq;
using SyncroSim.Core;
using SyncroSim.StochasticTime;
using System.Collections.Generic;
using System.Diagnostics;

namespace SyncroSim.STSim
{
    internal class TransitionSlopeMultiplierMap : STSimMapBase4<SortedList<int, TransitionSlopeMultiplier>>
    {
        private STSimDistributionProvider m_DistributionProvider;

        public TransitionSlopeMultiplierMap(
            Scenario scenario, 
            TransitionSlopeMultiplierCollection multipliers,
            STSimDistributionProvider distributionProvider) : base(scenario)
        {
            this.m_DistributionProvider = distributionProvider;

            foreach (TransitionSlopeMultiplier Item in multipliers)
            {
                this.AddSlopeMultiplier(Item);
            }
        }

        public double GetSlopeMultiplier(
            int transitionGroupId, int stratumId, int? secondaryStratumId, int? tertiaryStratumId, 
            int iteration, int timestep, double slope)
        {
            SortedList<int, TransitionSlopeMultiplier> lst = this.GetItem(
                transitionGroupId, stratumId, secondaryStratumId, tertiaryStratumId, iteration, timestep);

            if (lst == null)
            {
                return 1.0;
            }

            if (lst.Count == 1)
            {
                TransitionSlopeMultiplier tsm = lst.First().Value;
                tsm.Sample(iteration, timestep, this.m_DistributionProvider, DistributionFrequency.Always);

                return tsm.CurrentValue.Value;
            }

            int SlopeInt = Convert.ToInt32(slope);

            if (lst.ContainsKey(SlopeInt))
            {
                TransitionSlopeMultiplier tsm = lst[SlopeInt];
                tsm.Sample(iteration, timestep, this.m_DistributionProvider, DistributionFrequency.Always);

                return tsm.CurrentValue.Value;
            }

            int PrevKey = -91;
            int ThisKey = -91;

            foreach (int k in lst.Keys)
            {
                Debug.Assert(k != SlopeInt);

                if (k > SlopeInt)
                {
                    ThisKey = k;
                    break;
                }

                PrevKey = k;
            }

            if (PrevKey == -91)
            {
                TransitionSlopeMultiplier tsm = lst.First().Value;
                tsm.Sample(iteration, timestep, this.m_DistributionProvider, DistributionFrequency.Always);

                return tsm.CurrentValue.Value;
            }

            if (ThisKey == -91)
            {
                TransitionSlopeMultiplier tsm = lst.Last().Value;
                tsm.Sample(iteration, timestep, this.m_DistributionProvider, DistributionFrequency.Always);

                return tsm.CurrentValue.Value;
            }

            TransitionSlopeMultiplier PrevMult = lst[PrevKey];
            TransitionSlopeMultiplier ThisMult = lst[ThisKey];

            PrevMult.Sample(iteration, timestep, this.m_DistributionProvider, DistributionFrequency.Always);
            ThisMult.Sample(iteration, timestep, this.m_DistributionProvider, DistributionFrequency.Always);

            return Statistics.Interpolate(PrevKey, PrevMult.CurrentValue.Value, ThisKey, ThisMult.CurrentValue.Value, slope);
        }

        private void AddSlopeMultiplier(TransitionSlopeMultiplier item)
        {
            SortedList<int, TransitionSlopeMultiplier> l = this.GetItemExact(item.TransitionGroupId, item.StratumId, item.SecondaryStratumId, item.TertiaryStratumId, item.Iteration, item.Timestep);

            if (l == null)
            {
                l = new SortedList<int, TransitionSlopeMultiplier>();

                this.AddItem(item.TransitionGroupId, item.StratumId, item.SecondaryStratumId, item.TertiaryStratumId, item.Iteration, item.Timestep, l);
            }

            l.Add(Convert.ToInt32(item.Slope), item);
            Debug.Assert(this.HasItems);
        }
    }
}
