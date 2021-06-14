// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    internal class TransitionPathwayAutoCorrelationMap : STSimMapBase4<TransitionPathwayAutoCorrelation>
    {
        public TransitionPathwayAutoCorrelationMap(Scenario scenario, TransitionPathwayAutoCorrelationCollection collection) : base(scenario)
        {
            foreach (TransitionPathwayAutoCorrelation Item in collection)
            {
                this.TryAddItem(Item);
            }
        }

        public TransitionPathwayAutoCorrelation GetCorrelation(int transitionGroupId, int stratumId, int? secondaryStratumId, int? tertiaryStratumId, int iteration, int timestep)
        {
            return this.GetItem(transitionGroupId, stratumId, secondaryStratumId, tertiaryStratumId, iteration, timestep);
        }

        private void TryAddItem(TransitionPathwayAutoCorrelation item)
        {
            try
            {
                this.AddItem(item.TransitionGroupId, item.StratumId, item.SecondaryStratumId, item.TertiaryStratumId, item.Iteration, item.Timestep, item);
            }
            catch (STSimMapDuplicateItemException)
            {
                string template = "A duplicate transition pathway auto-correlation was detected: More information:" + Environment.NewLine + "Transition Group={0}, {1}={2}, {3}={4}, {5}={6}, Iteration={7}, Timestep={8}.";
                ExceptionUtils.ThrowArgumentException(template, this.GetTransitionGroupName(item.TransitionGroupId), this.PrimaryStratumLabel, this.GetStratumName(item.StratumId), this.SecondaryStratumLabel, this.GetSecondaryStratumName(item.SecondaryStratumId), this.TertiaryStratumLabel, this.GetTertiaryStratumName(item.TertiaryStratumId), STSimMapBase.FormatValue(item.Iteration), STSimMapBase.FormatValue(item.Timestep));
            }
        }
    }
}
