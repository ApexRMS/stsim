// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    internal class TransitionMultiplierValueMap : STSimMapBase5<List<TransitionMultiplierValue>>
    {
        private STSimDistributionProvider m_DistributionProvider;
        private List<List<TransitionMultiplierValue>> m_AllLists = new List<List<TransitionMultiplierValue>>();

        public TransitionMultiplierValueMap(
            Scenario scenario, 
            TransitionMultiplierValueCollection multipliers, 
            STSimDistributionProvider distributionProvider) : base(scenario)
        {
            this.m_DistributionProvider = distributionProvider;

            foreach (TransitionMultiplierValue item in multipliers)
            {
                this.AddMultiplier(item);
            }

            foreach (List<TransitionMultiplierValue> l in this.m_AllLists)
            {
                l.Sort((TransitionMultiplierValue tm1, TransitionMultiplierValue tm2) =>
                {
                    int cmp = tm1.AgeMin.CompareTo(tm2.AgeMin);

                    if (cmp != 0)
                    {
                        return cmp;
                    }

                    cmp = tm1.AgeMax.CompareTo(tm2.AgeMax);

                    if (cmp != 0)
                    {
                        return cmp;
                    }

                    cmp = tm1.TSTMin.CompareTo(tm2.TSTMin);

                    if (cmp != 0)
                    {
                        return cmp;
                    }

                    return tm1.TSTMax.CompareTo(tm2.TSTMax);                   
                });
            }
        }

        public List<TransitionMultiplierValue> GetTransitionMultipliers(
            int transitionGroupId, int stratumId, int? secondaryStratumId, int? tertiaryStratumId, 
            int stateClassId, int iteration, int timestep)
        {
            return this.GetItem(
                transitionGroupId, stratumId, secondaryStratumId, tertiaryStratumId, 
                stateClassId, iteration, timestep);
        }

        private void AddMultiplier(TransitionMultiplierValue item)
        {
            List<TransitionMultiplierValue> Multipliers = this.GetItemExact(
                item.TransitionGroupId, item.StratumId, item.SecondaryStratumId, item.TertiaryStratumId,
                item.StateClassId, item.Iteration, item.Timestep);

            if (Multipliers == null)
            {
                Multipliers = new List<TransitionMultiplierValue>();

                this.AddItem(item.TransitionGroupId, item.StratumId, item.SecondaryStratumId, item.TertiaryStratumId,
                    item.StateClassId, item.Iteration, item.Timestep, Multipliers);

                this.m_AllLists.Add(Multipliers);
            }

            Multipliers.Add(item);
            base.SetHasItems();
        }
    }
}
