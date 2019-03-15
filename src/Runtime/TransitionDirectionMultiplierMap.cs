// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;
using SyncroSim.StochasticTime;
using System.Diagnostics;

namespace SyncroSim.STSim
{
    internal class TransitionDirectionMultiplierMap : STSimMapBase5<TransitionDirectionMultiplier>
    {
        private STSimDistributionProvider m_DistributionProvider;

        public TransitionDirectionMultiplierMap(
            Scenario scenario, 
            TransitionDirectionMultiplierCollection multipliers, 
            STSimDistributionProvider distributionProvider) : base(scenario)
        {
            this.m_DistributionProvider = distributionProvider;

            foreach (TransitionDirectionMultiplier Item in multipliers)
            {
                this.TryAddItem(Item);
            }
        }

        public double GetDirectionMultiplier(
            int transitionGroupId, int stratumId, int? secondaryStratumId, int? tertiaryStratumId, 
            CardinalDirection cardinalDirection, int iteration, int timestep)
        {
            TransitionDirectionMultiplier v = this.GetItem(
                transitionGroupId, stratumId, secondaryStratumId, tertiaryStratumId, 
                (int?) cardinalDirection, iteration, timestep);

            if (v == null)
            {
                return 1.0;
            }

            v.Sample(iteration, timestep, this.m_DistributionProvider, DistributionFrequency.Always);
            return v.CurrentValue.Value;
        }

        private void TryAddItem(TransitionDirectionMultiplier item)
        {
            try
            {
                this.AddItem(
                    item.TransitionGroupId, item.StratumId, item.SecondaryStratumId, item.TertiaryStratumId,
                    (int?)item.CardinalDirection, item.Iteration, item.Timestep, item);
            }
            catch (STSimMapDuplicateItemException)
            {
                string template = "A duplicate transition direction multiplier was detected: More information:" + Environment.NewLine + "Transition Group={0}, {1}={2}, {3}={4}, {5}={6}, Iteration={7}, Timestep={8}, Cardinal Direction={9}." + Environment.NewLine + "NOTE: A user defined distribution can result in additional transition direction multipliers when the model is run.";
                ExceptionUtils.ThrowArgumentException(template, this.GetTransitionGroupName(item.TransitionGroupId), this.PrimaryStratumLabel, this.GetStratumName(item.StratumId), this.SecondaryStratumLabel, this.GetSecondaryStratumName(item.SecondaryStratumId), this.TertiaryStratumLabel, this.GetTertiaryStratumName(item.TertiaryStratumId), STSimMapBase.FormatValue(item.Iteration), STSimMapBase.FormatValue(item.Timestep), GetCardinalDirection(item.CardinalDirection));
            }
        }

        private static string GetCardinalDirection(CardinalDirection value)
        {
            if (value == CardinalDirection.N)
            {
                return "N";
            }
            else if (value == CardinalDirection.NE)
            {
                return "NE";
            }
            else if (value == CardinalDirection.E)
            {
                return "E";
            }
            else if (value == CardinalDirection.SE)
            {
                return "SE";
            }
            else if (value == CardinalDirection.S)
            {
                return "S";
            }
            else if (value == CardinalDirection.SW)
            {
                return "SW";
            }
            else if (value == CardinalDirection.W)
            {
                return "W";
            }
            else
            {
                Debug.Assert(value == CardinalDirection.NW);
                return "NW";
            }
        }
    }
}
