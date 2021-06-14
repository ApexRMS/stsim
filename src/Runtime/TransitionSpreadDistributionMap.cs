// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Common;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    internal class TransitionSpreadDistributionMap
    {
        private MultiLevelKeyMap2<SortedKeyMap2<List<TransitionSpreadDistribution>>> m_Map = new MultiLevelKeyMap2<SortedKeyMap2<List<TransitionSpreadDistribution>>>();
        private List<List<TransitionSpreadDistribution>> m_Lists = new List<List<TransitionSpreadDistribution>>();

        public void AddItem(TransitionSpreadDistribution tsd)
        {
            SortedKeyMap2<List<TransitionSpreadDistribution>> m = this.m_Map.GetItemExact(tsd.StratumId, tsd.StateClassId);

            if (m == null)
            {
                m = new SortedKeyMap2<List<TransitionSpreadDistribution>>(SearchMode.ExactPrev);
                this.m_Map.AddItem(tsd.StratumId, tsd.StateClassId, m);
            }

            List<TransitionSpreadDistribution> l = m.GetItemExact(tsd.Iteration, tsd.Timestep);

            if (l == null)
            {
                l = new List<TransitionSpreadDistribution>();
                m.AddItem(tsd.Iteration, tsd.Timestep, l);
            }

            l.Add(tsd);

            if (!this.m_Lists.Contains(l))
            {
                this.m_Lists.Add(l);
            }
        }

        private static void NormalizeList(List<TransitionSpreadDistribution> tsdList)
        {
            tsdList.Sort((TransitionSpreadDistribution tsd1, TransitionSpreadDistribution tsd2) =>
            {
                         return (tsd1.MaximumDistance.CompareTo(tsd2.MaximumDistance));
            });

            double TotalRelativeAmount = 0.0;

            for (int Index = 0; Index < tsdList.Count; Index++)
            {
                TransitionSpreadDistribution tsd = tsdList[Index];

                if (Index > 0)
                {
                    tsd.MinimumDistance = tsdList[Index - 1].MaximumDistance;
                }

                TotalRelativeAmount += tsd.RelativeAmount;
            }

            foreach (TransitionSpreadDistribution tsd in tsdList)
            {
                tsd.Proportion = tsd.RelativeAmount / TotalRelativeAmount;
            }
        }

        public void Normalize()
        {
            foreach (List<TransitionSpreadDistribution> l in this.m_Lists)
            {
                NormalizeList(l);
            }
        }

        public bool HasDistributionRecords(int stratumId, int stateClassId)
        {
            SortedKeyMap2<List<TransitionSpreadDistribution>> m = this.m_Map.GetItem(stratumId, stateClassId);

            if (m == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public List<TransitionSpreadDistribution> GetDistributionList(int stratumId, int stateClassId, int iteration, int timestep)
        {
            SortedKeyMap2<List<TransitionSpreadDistribution>> m = this.m_Map.GetItem(stratumId, stateClassId);

            if (m == null)
            {
                return null;
            }

            return m.GetItem(iteration, timestep);
        }
    }
}
