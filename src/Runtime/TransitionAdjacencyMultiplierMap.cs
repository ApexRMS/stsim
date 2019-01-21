// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System.Linq;
using SyncroSim.Core;
using System.Diagnostics;
using SyncroSim.StochasticTime;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    internal class TransitionAdjacencyMultiplierMapEntry
    {
        public Dictionary<double, TransitionAdjacencyMultiplier> Map = new Dictionary<double, TransitionAdjacencyMultiplier>();
        public SortedList<double, TransitionAdjacencyMultiplier> Items = new SortedList<double, TransitionAdjacencyMultiplier>();
    }

    internal class TransitionAdjacencyMultiplierMap : STSimMapBase4<TransitionAdjacencyMultiplierMapEntry>
    {
        private STSimDistributionProvider m_DistributionProvider;

        public TransitionAdjacencyMultiplierMap(
            Scenario scenario, 
            TransitionAdjacencyMultiplierCollection multipliers,
            STSimDistributionProvider distributionProvider) : base(scenario)
        {

            this.m_DistributionProvider = distributionProvider;

            foreach (TransitionAdjacencyMultiplier Item in multipliers)
            {
                this.AddMultiplier(Item);
            }
        }

        public double GetAdjacencyMultiplier(
            int transitionGroupId, int stratumId, int? secondaryStratumId, 
            int? tertiaryStratumId, int iteration, int timestep, double attributeValue)
        {
            TransitionAdjacencyMultiplierMapEntry e = this.GetItem(
                transitionGroupId, stratumId, secondaryStratumId, tertiaryStratumId, iteration, timestep);

            if (e == null)
            {
                return 1.0;
            }

            Debug.Assert(e.Items.Count == e.Map.Count);

            if (e.Map.Count == 1)
            {
                TransitionAdjacencyMultiplier tam = e.Items.First().Value;
                tam.Sample(iteration, timestep, this.m_DistributionProvider, DistributionFrequency.Always);

                return tam.CurrentValue.Value;
            }

            if (e.Map.ContainsKey(attributeValue))
            {
                TransitionAdjacencyMultiplier tam = e.Map[attributeValue];
                tam.Sample(iteration, timestep, this.m_DistributionProvider, DistributionFrequency.Always);

                return tam.CurrentValue.Value;
            }

            double PrevVal = -1.0;
            double NextVal = -1.0;

            Debug.Assert(attributeValue >= 0.0);

            foreach (double k in e.Items.Keys)
            {
                Debug.Assert(k != attributeValue);

                if (k > attributeValue)
                {
                    NextVal = k;
                    break;
                }

                PrevVal = k;
            }

            if (PrevVal == -1.0)
            {
                PrevVal = e.Items.First().Key;
            }

            if (NextVal == -1.0)
            {
                NextVal = PrevVal;
            }

            TransitionAdjacencyMultiplier PrevMult = e.Map[PrevVal];
            TransitionAdjacencyMultiplier NextMult = e.Map[NextVal];

            PrevMult.Sample(iteration, timestep, this.m_DistributionProvider, DistributionFrequency.Always);
            NextMult.Sample(iteration, timestep, this.m_DistributionProvider, DistributionFrequency.Always);

            if (PrevVal != -1.0)
            {
                if (NextVal != -1.0)
                {
                    if (PrevVal == NextVal)
                    {
                        return PrevMult.CurrentValue.Value;
                    }
                    else
                    {
                        return MathUtils.Interpolate(
                            PrevVal, 
                            PrevMult.CurrentValue.Value, 
                            NextVal, 
                            NextMult.CurrentValue.Value, 
                            attributeValue);
                    }
                }
                else
                {
                    return PrevMult.CurrentValue.Value;
                }
            }
            else
            {
                return 1.0;
            }
        }

        private void AddMultiplier(TransitionAdjacencyMultiplier item)
        {
            TransitionAdjacencyMultiplierMapEntry e = this.GetItemExact(
                item.TransitionGroupId, item.StratumId, item.SecondaryStratumId,
                item.TertiaryStratumId, item.Iteration, item.Timestep);

            if (e == null)
            {
                e = new TransitionAdjacencyMultiplierMapEntry();

                this.AddItem(item.TransitionGroupId, item.StratumId, item.SecondaryStratumId, 
                    item.TertiaryStratumId, item.Iteration, item.Timestep, e);
            }

            if (!e.Map.ContainsKey(item.AttributeValue))
            {
                Debug.Assert(!e.Items.ContainsKey(item.AttributeValue));

                e.Map.Add(item.AttributeValue, item);
                e.Items.Add(item.AttributeValue, item);
            }

            Debug.Assert(this.HasItems);
        }
    }
}
