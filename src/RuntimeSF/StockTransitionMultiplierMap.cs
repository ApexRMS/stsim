// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
	internal class StockTransitionMultiplierMap : StockFlowMapBase6<SortedList<double, StockTransitionMultiplier>>
	{
		private readonly STSimDistributionProvider m_DistributionProvider;

		public StockTransitionMultiplierMap(
            Scenario scenario, 
            StockTransitionMultiplierCollection items, 
            STSimDistributionProvider provider) : base(scenario)
		{
			this.m_DistributionProvider = provider;

			foreach (StockTransitionMultiplier item in items)
			{
				this.TryAddMultiplier(item);
			}
		}

		public double GetStockTransitionMultiplier(
            int stockGroupId, int stratumId, int? secondaryStratumId, int? tertiaryStratumId, 
            int stateClassId, int transitionGroupId, int iteration, int timestep, double stockValue)
		{
			SortedList<double, StockTransitionMultiplier> lst = this.GetItem(
                stockGroupId, stratumId, secondaryStratumId, tertiaryStratumId, 
                stateClassId, transitionGroupId, iteration, timestep);

			if (lst == null)
			{
				return 1.0;
			}

			if (lst.Count == 1)
			{
				StockTransitionMultiplier tsm = lst.First().Value;
				tsm.Sample(iteration, timestep, this.m_DistributionProvider, DistributionFrequency.Always);

				return tsm.CurrentValue.Value;
			}

			if (lst.ContainsKey(stockValue))
			{
				StockTransitionMultiplier tsm = lst[stockValue];
				tsm.Sample(iteration, timestep, this.m_DistributionProvider, DistributionFrequency.Always);

				return tsm.CurrentValue.Value;
			}

			double PrevKey = double.MinValue;
			double ThisKey = double.MinValue;

			foreach (double k in lst.Keys)
			{
				Debug.Assert(k != stockValue);

				if (k > stockValue)
				{
					ThisKey = k;
					break;
				}

				PrevKey = k;
			}

			if (PrevKey == double.MinValue)
			{
				StockTransitionMultiplier tsm = lst.First().Value;
				tsm.Sample(iteration, timestep, this.m_DistributionProvider, DistributionFrequency.Always);

				return tsm.CurrentValue.Value;
			}

			if (ThisKey == double.MinValue)
			{           
				StockTransitionMultiplier tsm = lst.Last().Value;
				tsm.Sample(iteration, timestep, this.m_DistributionProvider, DistributionFrequency.Always);

				return tsm.CurrentValue.Value;
			}

			StockTransitionMultiplier PrevMult = lst[PrevKey];
			StockTransitionMultiplier ThisMult = lst[ThisKey];

			PrevMult.Sample(iteration, timestep, this.m_DistributionProvider, DistributionFrequency.Always);
			ThisMult.Sample(iteration, timestep, this.m_DistributionProvider, DistributionFrequency.Always);

			return Statistics.Interpolate(PrevKey, PrevMult.CurrentValue.Value, ThisKey, ThisMult.CurrentValue.Value, stockValue);
		}

		private void TryAddMultiplier(StockTransitionMultiplier item)
		{
			SortedList<double, StockTransitionMultiplier> l = this.GetItemExact(
                item.StockGroupId, item.StratumId, item.SecondaryStratumId, item.TertiaryStratumId, 
                item.StateClassId, item.TransitionGroupId, item.Iteration, item.Timestep);

			if (l == null)
			{
				l = new SortedList<double, StockTransitionMultiplier>();

				this.AddItem(
                    item.StockGroupId, item.StratumId, item.SecondaryStratumId, item.TertiaryStratumId, 
                    item.StateClassId, item.TransitionGroupId, item.Iteration, item.Timestep, l);
			}

			l.Add(item.StockValue, item);
			Debug.Assert(this.HasItems);
		}
	}
}