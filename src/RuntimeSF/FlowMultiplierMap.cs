// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    internal class FlowMultiplierMap : StockFlowMapBase5<List<FlowMultiplier>>
    {
        private readonly STSimDistributionProvider m_DistributionProvider;

        public FlowMultiplierMap(Scenario scenario, FlowMultiplierCollection items, STSimDistributionProvider provider) : base(scenario)
        {
            this.m_DistributionProvider = provider;

            foreach (FlowMultiplier item in items)
            {
                this.TryAddMultiplier(item);
            }
        }

        public FlowMultiplier GetFlowMultiplierClassInstance(
                int flowGroupId,
                int stratumId,
                int? secondaryStratumId,
                int? tertiaryStratumId,
                int stateClassId,
                int iteration,
                int timestep,
                int age)
        {
            List<FlowMultiplier> l = this.GetItem(
                flowGroupId, stratumId, secondaryStratumId, tertiaryStratumId, stateClassId, iteration, timestep);

            if (l == null)
            {
                return null;
            }

            FlowMultiplier m = GetFlowMultiplierByAge(l, age);

            if (m == null)
            {
                return null;
            }

            return m;
        }

        public double GetFlowMultiplier(
            int flowGroupId,
            int stratumId,
            int? secondaryStratumId,
            int? tertiaryStratumId,
            int stateClassId,
            int iteration,
            int timestep,
            int age)
        {
            List<FlowMultiplier> l = this.GetItem(
                flowGroupId, stratumId, secondaryStratumId, tertiaryStratumId, stateClassId, iteration, timestep);

            if (l == null)
            {
                return 1.0;
            }

            FlowMultiplier m = GetFlowMultiplierByAge(l, age);

            if (m == null)
            {
                return 1.0;
            }

            if (m.IsDisabled)
            {
                return 1.0;
            }

            m.Sample(iteration, timestep, this.m_DistributionProvider, DistributionFrequency.Always);
            return m.CurrentValue.Value;
        }

        private static FlowMultiplier GetFlowMultiplierByAge(List<FlowMultiplier> l, int age)
        {
            foreach (FlowMultiplier m in l)
            {
                if (age >= m.AgeMin && age <= m.AgeMax)
                {
                    return m;
                }
            }

            return null;
        }

        private void TryAddMultiplier(FlowMultiplier item)
        {
            try
            {
                List<FlowMultiplier> l = base.GetItemExact(
                    item.FlowGroupId,
                    item.StratumId, item.SecondaryStratumId, item.TertiaryStratumId,
                    item.StateClassId, item.Iteration, item.Timestep);

                if (l == null)
                {
                    l = new List<FlowMultiplier>();

                    base.AddItem(
                        item.FlowGroupId,
                        item.StratumId, item.SecondaryStratumId, item.TertiaryStratumId,
                        item.StateClassId, item.Iteration, item.Timestep, l);
                }

                l.Add(item);
                base.SetHasItems();
            }
            catch (STSimMapDuplicateItemException)
            {
                string template =
                            "A duplicate flow multiplier was detected: More information:" +
                            Environment.NewLine +
                            "Flow Group={0}, {1}={2}, {3}={4}, {5}={6}, State Class={7}, Iteration={8}, Timestep={9}.";

                ExceptionUtils.ThrowArgumentException(template,
                    this.GetFlowGroupName(item.FlowGroupId),
                    this.PrimaryStratumLabel, this.GetStratumName(item.StratumId),
                    this.SecondaryStratumLabel, this.GetSecondaryStratumName(item.SecondaryStratumId),
                    this.TertiaryStratumLabel, this.GetTertiaryStratumName(item.TertiaryStratumId),
                    this.GetStateClassName(item.StateClassId),
                    StockFlowMapBase.FormatValue(item.Iteration),
                    StockFlowMapBase.FormatValue(item.Timestep));
            }
        }
    }
}
