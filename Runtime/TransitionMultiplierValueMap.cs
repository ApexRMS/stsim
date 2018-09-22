// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;
using SyncroSim.StochasticTime;

namespace SyncroSim.STSim
{
    internal class TransitionMultiplierValueMap : STSimMapBase5<TransitionMultiplierValue>
    {
        private STSimDistributionProvider m_DistributionProvider;

        public TransitionMultiplierValueMap(
            Scenario scenario, 
            TransitionMultiplierValueCollection multipliers, 
            STSimDistributionProvider distributionProvider) : base(scenario)
        {

            this.m_DistributionProvider = distributionProvider;

            foreach (TransitionMultiplierValue item in multipliers)
            {
                this.TryAddMultiplier(item);
            }
        }

        public TransitionMultiplierValue GetTransitionMultiplier(
            int transitionGroupId, int stratumId, int? secondaryStratumId, int? tertiaryStratumId, 
            int stateClassId, int iteration, int timestep)
        {
            TransitionMultiplierValue v = this.GetItem(
                transitionGroupId, stratumId, secondaryStratumId, tertiaryStratumId, 
                stateClassId, iteration, timestep);

            if (v != null)
            {
                v.Sample(iteration, timestep, this.m_DistributionProvider, DistributionFrequency.Always);
            }

            return v;
        }

        private void TryAddMultiplier(TransitionMultiplierValue item)
        {
            try
            {
                base.AddItem(
                    item.TransitionGroupId, item.StratumId, item.SecondaryStratumId, item.TertiaryStratumId, 
                    item.StateClassId, item.Iteration, item.Timestep, item);
            }
            catch (STSimMapDuplicateItemException)
            {
                string template = "A duplicate transition multiplier value was detected: More information:" + Environment.NewLine + "Transition Group={0}, {1}={2}, {3}={4}, {5}={6}, State Class={7}, Iteration={8}, Timestep={9}." + Environment.NewLine + "NOTE: A user defined distribution can result in additional transition multiplier values when the model is run.";
                ExceptionUtils.ThrowArgumentException(template, this.GetTransitionGroupName(item.TransitionGroupId), this.PrimaryStratumLabel, this.GetStratumName(item.StratumId), this.SecondaryStratumLabel, this.GetSecondaryStratumName(item.SecondaryStratumId), this.TertiaryStratumLabel, this.GetTertiaryStratumName(item.TertiaryStratumId), this.GetStateClassName(item.StateClassId), STSimMapBase.FormatValue(item.Iteration), STSimMapBase.FormatValue(item.Timestep));
            }
        }
    }
}
