// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
		internal class FlowMultiplierByStockMap : StockFlowMapBase6<SortedList<double, FlowMultiplierByStock>>
		{
				private readonly STSimDistributionProvider m_DistributionProvider;

				public FlowMultiplierByStockMap(
								Scenario scenario,
								FlowMultiplierByStockCollection items,
								STSimDistributionProvider provider) : base(scenario)
				{
						this.m_DistributionProvider = provider;

						foreach (FlowMultiplierByStock item in items)
						{
								this.TryAddMultiplier(item);
						}
				}

				public double GetFlowMultiplierByStock(
								int stockGroupId, int stratumId, int? secondaryStratumId, int? tertiaryStratumId,
								int stateClassId, int flowGroupId, int iteration, int timestep, double stockValue)
				{
						SortedList<double, FlowMultiplierByStock> lst = this.GetItem(
											stockGroupId, stratumId, secondaryStratumId, tertiaryStratumId,
											stateClassId, flowGroupId, iteration, timestep);

						if (lst == null)
						{
								return 1.0;
						}

						if (lst.Count == 1)
						{
								FlowMultiplierByStock tsm = lst.First().Value;
								tsm.Sample(iteration, timestep, this.m_DistributionProvider, DistributionFrequency.Always);

								return tsm.CurrentValue.Value;
						}

						if (lst.ContainsKey(stockValue))
						{
								FlowMultiplierByStock tsm = lst[stockValue];
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
								FlowMultiplierByStock tsm = lst.First().Value;
								tsm.Sample(iteration, timestep, this.m_DistributionProvider, DistributionFrequency.Always);

								return tsm.CurrentValue.Value;
						}

						if (ThisKey == double.MinValue)
						{
								FlowMultiplierByStock tsm = lst.Last().Value;
								tsm.Sample(iteration, timestep, this.m_DistributionProvider, DistributionFrequency.Always);

								return tsm.CurrentValue.Value;
						}

						FlowMultiplierByStock PrevMult = lst[PrevKey];
						FlowMultiplierByStock ThisMult = lst[ThisKey];

						PrevMult.Sample(iteration, timestep, this.m_DistributionProvider, DistributionFrequency.Always);
						ThisMult.Sample(iteration, timestep, this.m_DistributionProvider, DistributionFrequency.Always);

						return Statistics.Interpolate(PrevKey, PrevMult.CurrentValue.Value, ThisKey, ThisMult.CurrentValue.Value, stockValue);
				}

				private void TryAddMultiplier(FlowMultiplierByStock item)
				{
						SortedList<double, FlowMultiplierByStock> l = this.GetItemExact(
											item.StockGroupId, item.StratumId, item.SecondaryStratumId, item.TertiaryStratumId,
											item.StateClassId, item.FlowGroupId, item.Iteration, item.Timestep);

						if (l == null)
						{
								l = new SortedList<double, FlowMultiplierByStock>();

								this.AddItem(
														item.StockGroupId, item.StratumId, item.SecondaryStratumId, item.TertiaryStratumId,
														item.StateClassId, item.FlowGroupId, item.Iteration, item.Timestep, l);
						}

						l.Add(item.StockValue, item);
						Debug.Assert(this.HasItems);
				}
		}
}