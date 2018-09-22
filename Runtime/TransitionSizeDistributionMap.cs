// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using System.Diagnostics;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    internal class TransitionSizeDistributionMap : STSimMapBase2<List<TransitionSizeDistribution>>
    {
        private List<List<TransitionSizeDistribution>> m_Lists = new List<List<TransitionSizeDistribution>>();

        public TransitionSizeDistributionMap(Scenario scenario, TransitionSizeDistributionCollection collection) : base(scenario)
        {

            foreach (TransitionSizeDistribution Item in collection)
            {
                this.AddSizeDistribution(Item);
            }
        }

        public void Normalize()
        {
            foreach (List<TransitionSizeDistribution> l in this.m_Lists)
            {
                NormalizeList(l);
            }
        }

        public List<TransitionSizeDistribution> GetSizeDistributions(int transitionGroupId, int stratumId, int iteration, int timestep)
        {
            return this.GetItem(transitionGroupId, stratumId, iteration, timestep);
        }

        private void AddSizeDistribution(TransitionSizeDistribution item)
        {
            List<TransitionSizeDistribution> l = this.GetItemExact(item.TransitionGroupId, item.StratumId, item.Iteration, item.Timestep);

            if (l == null)
            {
                l = new List<TransitionSizeDistribution>();

                this.AddItem(item.TransitionGroupId, item.StratumId, item.Iteration, item.Timestep, l);
            }

            l.Add(item);

            if (!this.m_Lists.Contains(l))
            {
                this.m_Lists.Add(l);
            }

            Debug.Assert(this.HasItems);
        }

        private static void NormalizeList(List<TransitionSizeDistribution> tsdList)
        {
            tsdList.Sort((TransitionSizeDistribution tsd1, TransitionSizeDistribution tsd2) =>
            {
                return (tsd1.MaximumSize.CompareTo(tsd2.MaximumSize));
            });

            double TotalRelativeAmount = 0.0;

            for (int Index = 0; Index < tsdList.Count; Index++)
            {
                TransitionSizeDistribution tsd = tsdList[Index];

                if (Index > 0)
                {
                    tsd.MinimumSize = tsdList[Index - 1].MaximumSize;
                }

                TotalRelativeAmount += tsd.RelativeAmount;
            }

            foreach (TransitionSizeDistribution tsd in tsdList)
            {
                tsd.Proportion = tsd.RelativeAmount / TotalRelativeAmount;
            }
        }
    }
}
