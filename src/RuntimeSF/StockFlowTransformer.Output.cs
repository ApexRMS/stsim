// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using SyncroSim.Apex;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
	internal partial class StockFlowTransformer
	{
		private DataTable m_OutputStockTable;
		private bool m_CreateSummaryStockOutput;
		private int m_SummaryStockOutputTimesteps;
		private DataTable m_OutputFlowTable;
		private bool m_CreateSummaryFlowOutput;
		private int m_SummaryFlowOutputTimesteps;
		private bool m_CreateSpatialStockOutput;
		private int m_SpatialStockOutputTimesteps;
		private bool m_CreateSpatialFlowOutput;
		private int m_SpatialFlowOutputTimesteps;
		private bool m_CreateLateralFlowOutput;
		private int m_LateralFlowOutputTimesteps;
		private bool m_CreateAvgSpatialStockOutput;
		private int m_AvgSpatialStockOutputTimesteps;
		private bool m_AvgSpatialStockOutputAcrossTimesteps;
		private bool m_CreateAvgSpatialFlowOutput;
		private int m_AvgSpatialFlowOutputTimesteps;
		private bool m_AvgSpatialFlowOutputAcrossTimesteps;
		private bool m_CreateAvgSpatialLateralFlowOutput;
		private int m_AvgSpatialLateralFlowOutputTimesteps;
		private bool m_AvgSpatialLateralFlowOutputAcrossTimesteps;

		private Dictionary<int, SpatialOutputFlowRecord> m_SpatialOutputFlowDict;
		private Dictionary<int, SpatialOutputFlowRecord> m_LateralOutputFlowDict;
		private readonly OutputFlowCollection m_SummaryOutputFlowRecords = new OutputFlowCollection();
		private readonly OutputStockCollection m_SummaryOutputStockRecords = new OutputStockCollection();

		private void InitializeOutputDataTables()
		{
			Debug.Assert(this.m_OutputStockTable == null);
			Debug.Assert(this.m_OutputFlowTable == null);

			this.m_OutputStockTable = this.ResultScenario.GetDataSheet(Strings.DATASHEET_OUTPUT_STOCK_NAME).GetData();
			this.m_OutputFlowTable = this.ResultScenario.GetDataSheet(Strings.DATASHEET_OUTPUT_FLOW_NAME).GetData();

			Debug.Assert(this.m_OutputStockTable.Rows.Count == 0);
			Debug.Assert(this.m_OutputFlowTable.Rows.Count == 0);
		}

				private void RecordSummaryStockOutputData()
				{
						foreach (Cell c in this.STSimTransformer.Cells)
						{
								Dictionary<int, float> StockAmounts = GetStockAmountDictionary(c);

								foreach (int StockTypeId in StockAmounts.Keys)
								{
										StockType t = this.m_StockTypes[StockTypeId];
										float amount = StockAmounts[StockTypeId];

										foreach (StockGroupLinkage l in t.StockGroupLinkages)                    
										{
												if (!l.StockGroup.OutputFilter.HasFlag(OutputFilter.Tabular))
												{
														continue;
												}

												FiveIntegerLookupKey k = new FiveIntegerLookupKey(
												c.StratumId, GetSecondaryStratumIdKey(c),
												GetTertiaryStratumIdKey(c), c.StateClassId, l.StockGroup.Id);

												if (this.m_SummaryOutputStockRecords.Contains(k))
												{
														OutputStock r = this.m_SummaryOutputStockRecords[k];
														r.Amount += (amount * l.Value);
												}
												else
												{
														OutputStock r = new OutputStock(
																c.StratumId, GetSecondaryStratumIdValue(c),
																GetTertiaryStratumIdValue(c), c.StateClassId, l.StockGroup.Id, amount * l.Value);

														this.m_SummaryOutputStockRecords.Add(r);
												}
										}
								}
						}
				}

				private void RecordSummaryFlowOutputData(
						int timestep,
						Cell cell,
						DeterministicTransition deterministicPathway,
						Transition probabilisticPathway,
						FlowPathway flowPathway,
						float flowAmount)
				{
						int? TransitionTypeId = null;
						int StratumIdDest = cell.StratumId;
						int StateClassIdDest = cell.StateClassId;

						if (probabilisticPathway != null)
						{
								TransitionTypeId = probabilisticPathway.TransitionTypeId;

								if (probabilisticPathway.StratumIdDestination.HasValue)
								{
										StratumIdDest = probabilisticPathway.StratumIdDestination.Value;
								}

								if (probabilisticPathway.StateClassIdDestination.HasValue)
								{
										StateClassIdDest = probabilisticPathway.StateClassIdDestination.Value;
								}
						}
						else
						{
								if (deterministicPathway != null)
								{
										if (deterministicPathway.StratumIdDestination.HasValue)
										{
												StratumIdDest = deterministicPathway.StratumIdDestination.Value;
										}

										if (deterministicPathway.StateClassIdDestination.HasValue)
										{
												StateClassIdDest = deterministicPathway.StateClassIdDestination.Value;
										}
								}
						}

						if (this.m_STSimTransformer.IsOutputTimestep(
								timestep, 
								this.m_SummaryFlowOutputTimesteps, 
								this.m_CreateSummaryFlowOutput))
						{
								FlowType t = this.m_FlowTypes[flowPathway.FlowTypeId];

								foreach (FlowGroupLinkage l in t.FlowGroupLinkages)
								{
										if (!l.FlowGroup.OutputFilter.HasFlag(OutputFilter.Tabular))
										{
												continue;
										}

										FifteenIntegerLookupKey k = new FifteenIntegerLookupKey(
												cell.StratumId,
												GetSecondaryStratumIdKey(cell),
												GetTertiaryStratumIdKey(cell),
												cell.StateClassId,
												LookupKeyUtils.GetOutputCollectionKey(flowPathway.FromStockTypeId),
												LookupKeyUtils.GetOutputCollectionKey(TransitionTypeId),
												StratumIdDest,
												StateClassIdDest,
												LookupKeyUtils.GetOutputCollectionKey(flowPathway.ToStockTypeId),
												l.FlowGroup.Id, 
												LookupKeyUtils.GetOutputCollectionKey(flowPathway.TransferToStratumId),
												LookupKeyUtils.GetOutputCollectionKey(flowPathway.TransferToSecondaryStratumId),
												LookupKeyUtils.GetOutputCollectionKey(flowPathway.TransferToTertiaryStratumId),
												LookupKeyUtils.GetOutputCollectionKey(flowPathway.TransferToStateClassId),
												LookupKeyUtils.GetOutputCollectionKey(flowPathway.TransferToMinimumAge));

										if (this.m_SummaryOutputFlowRecords.Contains(k))
										{
												OutputFlow r = this.m_SummaryOutputFlowRecords[k];
												r.Amount += (flowAmount * l.Value);
										}
										else
										{
												OutputFlow r = new OutputFlow(
														cell.StratumId,
														GetSecondaryStratumIdValue(cell),
														GetTertiaryStratumIdValue(cell),
														cell.StateClassId,
														flowPathway.FromStockTypeId,
														TransitionTypeId,
														StratumIdDest,
														StateClassIdDest,
														flowPathway.ToStockTypeId,
														l.FlowGroup.Id,
														flowPathway.TransferToStratumId,
														flowPathway.TransferToSecondaryStratumId,
														flowPathway.TransferToTertiaryStratumId,
														flowPathway.TransferToStateClassId,
														flowPathway.TransferToMinimumAge,
														flowAmount * l.Value);

												this.m_SummaryOutputFlowRecords.Add(r);
										}
								}
						}
				}

				private void RecordSpatialFlowOutputData(int timestep, Cell cell, int flowTypeId, float flowAmount)
				{
						if (!this.m_IsSpatial)
						{
								return;
						}

						bool IsNormalOutputTimestep = this.m_STSimTransformer.IsOutputTimestep(
								timestep,
								this.m_SpatialFlowOutputTimesteps,
								this.m_CreateSpatialFlowOutput);

						bool IsAverageOutputTimestep = this.m_STSimTransformer.IsOutputTimestep(
								timestep,
								this.m_AvgSpatialFlowOutputTimesteps,
								this.m_CreateAvgSpatialFlowOutput);

						if (!IsNormalOutputTimestep && !IsAverageOutputTimestep)
						{
								return;
						}

						if (GetSpatialOutputFlowDictionary().ContainsKey(flowTypeId))
						{
								SpatialOutputFlowRecord rec = GetSpatialOutputFlowDictionary()[flowTypeId];
								float amt = rec.Data[cell.CollectionIndex];

								if (amt.Equals(Spatial.DefaultNoDataValue) || float.IsNaN(amt))
								{
										amt = 0;
								}

								amt += Convert.ToSingle(flowAmount / this.m_STSimTransformer.AmountPerCell);

								rec.Data[cell.CollectionIndex] = amt;
								rec.HasOutputData = true;
						}
				}

				private void RecordSpatialLateralFlowOutputData(int timestep, Cell cell, int flowTypeId, float flowAmount)
				{
						bool IsLFOutputTimestep = this.m_STSimTransformer.IsOutputTimestep(
								timestep,
								this.m_LateralFlowOutputTimesteps,
								this.m_CreateLateralFlowOutput);

						bool IsLFAverageOutputTimestep = this.m_STSimTransformer.IsOutputTimestep(
								timestep,
								this.m_AvgSpatialLateralFlowOutputTimesteps,
								this.m_CreateAvgSpatialLateralFlowOutput);

						if (!IsLFOutputTimestep && !IsLFAverageOutputTimestep)
						{
								return;
						}

						if (GetLateralOutputFlowDictionary().ContainsKey(flowTypeId))
						{
								SpatialOutputFlowRecord rec = GetLateralOutputFlowDictionary()[flowTypeId];
								float amt = rec.Data[cell.CollectionIndex];

								if (amt.Equals(Spatial.DefaultNoDataValue))
								{
										amt = 0;
								}

								amt += Convert.ToSingle(flowAmount / this.m_STSimTransformer.AmountPerCell);

								rec.Data[cell.CollectionIndex] = amt;
								rec.HasOutputData = true;
						}
				}

				private void WriteTabularSummaryStockOutput(int iteration, int timestep)
				{
						foreach (OutputStock r in this.m_SummaryOutputStockRecords)
						{
								DataRow dr = this.m_OutputStockTable.NewRow();

								dr[Strings.DATASHEET_ITERATION_COLUMN_NAME] = iteration;
								dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME] = timestep;
								dr[Strings.DATASHEET_STRATUM_ID_COLUMN_NAME] = r.StratumId;
								dr[Strings.DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(r.SecondaryStratumId);
								dr[Strings.DATASHEET_TERTIARY_STRATUM_ID_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(r.TertiaryStratumId);
								dr[Strings.DATASHEET_STATECLASS_ID_COLUMN_NAME] = r.StateClassId;
								dr[Strings.STOCK_GROUP_ID_COLUMN_NAME] = r.StockGroupId;
								dr[Strings.DATASHEET_AMOUNT_COLUMN_NAME] = r.Amount;

								this.m_OutputStockTable.Rows.Add(dr);
						}

						this.m_SummaryOutputStockRecords.Clear();
				}

				private void WriteTabularSummaryFlowOutputData(int iteration, int timestep)
		{
			foreach (OutputFlow r in this.m_SummaryOutputFlowRecords)
			{
				DataRow dr = this.m_OutputFlowTable.NewRow();

				dr[Strings.DATASHEET_ITERATION_COLUMN_NAME] = iteration;
				dr[Strings.DATASHEET_TIMESTEP_COLUMN_NAME] = timestep;
				dr[Strings.FROM_STRATUM_ID_COLUMN_NAME] = r.FromStratumId;
				dr[Strings.FROM_SECONDARY_STRATUM_ID_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(r.FromSecondaryStratumId);
				dr[Strings.FROM_TERTIARY_STRATUM_ID_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(r.FromTertiaryStratumId);
				dr[Strings.FROM_STATECLASS_ID_COLUMN_NAME] = r.FromStateClassId;
				dr[Strings.FROM_STOCK_TYPE_ID_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(r.FromStockTypeId);
				dr[Strings.DATASHEET_TRANSITION_TYPE_ID_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(r.TransitionTypeId);
				dr[Strings.TO_STRATUM_ID_COLUMN_NAME] = r.ToStratumId;
				dr[Strings.TO_STATECLASS_ID_COLUMN_NAME] = r.ToStateClassId;
				dr[Strings.TO_STOCK_TYPE_ID_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(r.ToStockTypeId);
				dr[Strings.FLOW_GROUP_ID_COLUMN_NAME] = r.FlowGroupId;
				dr[Strings.END_STRATUM_ID_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(r.TransferToStratumId);
				dr[Strings.END_SECONDARY_STRATUM_ID_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(r.TransferToSecondaryStratumId);
				dr[Strings.END_TERTIARY_STRATUM_ID_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(r.TransferToTertiaryStratumId);
				dr[Strings.END_STATECLASS_ID_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(r.TransferToStateClassId);
				dr[Strings.END_MIN_AGE_COLUMN_NAME] = DataTableUtilities.GetNullableDatabaseValue(r.TransferToMinimumAge);

				dr[Strings.DATASHEET_AMOUNT_COLUMN_NAME] = r.Amount;

				this.m_OutputFlowTable.Rows.Add(dr);
			}

			this.m_SummaryOutputFlowRecords.Clear();
		}

		private void WriteStockGroupRasters(int iteration, int timestep)
		{
			Debug.Assert(this.m_IsSpatial);

						foreach (StockGroup g in this.m_StockGroups)
						{
								if (!g.OutputFilter.HasFlag(OutputFilter.Spatial))
								{
										continue;
								}

								SyncroSimRaster rastOutput = this.STSimTransformer.InputRasters.CreateOutputRaster(RasterDataType.DTFloat);

								foreach (StockTypeLinkage l in g.StockTypeLinkages)
								{
										SyncroSimRaster rastStockType = this.STSimTransformer.InputRasters.CreateOutputRaster(RasterDataType.DTFloat);

										GetStockValues(l.StockType.Id, rastStockType);
										rastStockType.ScaleFloatCells(l.Value);
										rastOutput.AddFloatCells(rastStockType);
								}

								Spatial.WriteRasterData(
										rastOutput,
										this.ResultScenario.GetDataSheet(Strings.DATASHEET_OUTPUT_SPATIAL_STOCK_GROUP),
										iteration,
										timestep,
										g.Id,
										Strings.SPATIAL_MAP_STOCK_GROUP_VARIABLE_PREFIX,
										Strings.DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN);
						}
		}

		private void WriteFlowGroupRasters(int iteration, int timestep)
		{
						Debug.Assert(this.m_IsSpatial);

						foreach (FlowGroup g in this.m_FlowGroups)
						{
								if (!g.OutputFilter.HasFlag(OutputFilter.Spatial))
								{
										continue;
								}

								bool AtLeastOne = false;
								SyncroSimRaster rastOutput = this.STSimTransformer.InputRasters.CreateOutputRaster(RasterDataType.DTFloat);

								foreach (FlowTypeLinkage l in g.FlowTypeLinkages)
								{
										if (GetSpatialOutputFlowDictionary().ContainsKey(l.FlowType.Id))
										{
												SpatialOutputFlowRecord rec = GetSpatialOutputFlowDictionary()[l.FlowType.Id];

												if (rec.HasOutputData)
												{
														SyncroSimRaster rastFlowType = this.STSimTransformer.InputRasters.CreateOutputRaster(RasterDataType.DTFloat);
														float[] arr = rastFlowType.FloatCells;

														foreach (Cell c in this.m_STSimTransformer.Cells)
														{
																if (rec.Data[c.CollectionIndex] == Spatial.DefaultNoDataValue)
																{
																		arr[c.CellId] = 0;
																}
																else
																{
																		arr[c.CellId] = rec.Data[c.CollectionIndex];
																}
														}

														rastFlowType.ScaleFloatCells(l.Value);
														rastOutput.AddFloatCells(rastFlowType);

														AtLeastOne = true;
												}
										}
								}

								if (AtLeastOne)
								{
										Spatial.WriteRasterData(
												rastOutput,
												this.ResultScenario.GetDataSheet(Strings.DATASHEET_OUTPUT_SPATIAL_FLOW_GROUP),
												iteration,
												timestep,
												g.Id,
												Strings.SPATIAL_MAP_FLOW_GROUP_VARIABLE_PREFIX,
												Strings.DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN);
								}
						}
				}

				private void WriteLateralFlowRasters(int iteration, int timestep)
				{
						Debug.Assert(this.m_IsSpatial);

						foreach (FlowGroup g in this.m_FlowGroups)
						{
								if (!g.OutputFilter.HasFlag(OutputFilter.Spatial))
								{
										continue;
								}

								bool AtLeastOne = false;
								SyncroSimRaster rastOutput = this.STSimTransformer.InputRasters.CreateOutputRaster(RasterDataType.DTFloat);

								foreach (FlowTypeLinkage l in g.FlowTypeLinkages)
								{
										if (GetLateralOutputFlowDictionary().ContainsKey(l.FlowType.Id))
										{
												SpatialOutputFlowRecord rec = GetLateralOutputFlowDictionary()[l.FlowType.Id];

												if (rec.HasOutputData)
												{
														SyncroSimRaster rastFlowType = 
																this.STSimTransformer.InputRasters.CreateOutputRaster(RasterDataType.DTFloat);

														float[] arr = rastFlowType.FloatCells;

														foreach (Cell c in this.m_STSimTransformer.Cells)
														{
																if (rec.Data[c.CollectionIndex] == Spatial.DefaultNoDataValue)
																{
																		arr[c.CellId] = 0;
																}
																else
																{
																		arr[c.CellId] = rec.Data[c.CollectionIndex];
																}
														}

														rastFlowType.ScaleFloatCells(l.Value);
														rastOutput.AddFloatCells(rastFlowType);

														AtLeastOne = true;
												}
										}
								}

								if (AtLeastOne)
								{
										Spatial.WriteRasterData(
												rastOutput,
												this.ResultScenario.GetDataSheet(Strings.DATASHEET_OUTPUT_LATERAL_FLOW_GROUP),
												iteration,
												timestep,
												g.Id,
												Strings.SPATIAL_MAP_LATERAL_FLOW_GROUP_VARIABLE_PREFIX,
												Strings.DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN);
								}
						}
				}

				private void WriteAverageStockRasters()
				{
						Debug.Assert(this.STSimTransformer.IsSpatial);
						Debug.Assert(this.m_CreateAvgSpatialStockOutput);

						foreach (int id in this.m_AvgStockMap.Keys)
						{
								StockGroup sg = this.m_StockGroups[id];

								if (!sg.OutputFilter.HasFlag(OutputFilter.AvgSpatial))
								{
										continue;
								}

								Dictionary<int, float[]> dict = this.m_AvgStockMap[id];

								foreach (int timestep in dict.Keys)
								{
										float[] values = dict[timestep];
										var distArray = values.Distinct();

										if (distArray.Count() == 1)
										{
												var el0 = distArray.ElementAt(0);

												if (el0.Equals(Spatial.DefaultNoDataValue))
												{
														continue;
												}
										}

										SyncroSimRaster rast = this.STSimTransformer.InputRasters.CreateOutputRaster(RasterDataType.DTFloat);
										float[] arr = rast.FloatCells;

										foreach (Cell c in this.STSimTransformer.Cells)
										{
												arr[c.CellId] = Convert.ToSingle(values[c.CollectionIndex] / this.STSimTransformer.AmountPerCell);
										}

										Spatial.WriteRasterData(
												rast,
												this.ResultScenario.GetDataSheet(Strings.DATASHEET_OUTPUT_AVG_SPATIAL_STOCK_GROUP),
												0,
												timestep,
												id,
												Strings.SPATIAL_MAP_AVG_STOCK_GROUP_VARIABLE_PREFIX,
												Strings.DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN);
								}
						}
				}

				private void WriteAverageFlowRasters()
				{
						Debug.Assert(this.STSimTransformer.IsSpatial);
						Debug.Assert(this.m_CreateAvgSpatialFlowOutput);

						foreach (int id in this.m_AvgFlowMap.Keys)
						{
								FlowGroup sg = this.m_FlowGroups[id];

								if (!sg.OutputFilter.HasFlag(OutputFilter.AvgSpatial))
								{
										continue;
								}

								Dictionary<int, float[]> dict = this.m_AvgFlowMap[id];

								foreach (int timestep in dict.Keys)
								{
										if (timestep == this.STSimTransformer.TimestepZero)
										{
												continue;
										}

										float[] values = dict[timestep];
										var distArray = values.Distinct();

										if (distArray.Count() == 1)
										{
												var el0 = distArray.ElementAt(0);

												if (el0.Equals(Spatial.DefaultNoDataValue))
												{
														continue;
												}
										}

										SyncroSimRaster rast = this.STSimTransformer.InputRasters.CreateOutputRaster(RasterDataType.DTFloat);
										float[] arr = rast.FloatCells;

										foreach (Cell c in this.STSimTransformer.Cells)
										{
												arr[c.CellId] = Convert.ToSingle(values[c.CollectionIndex]);
										}

										Spatial.WriteRasterData(
												rast,
												this.ResultScenario.GetDataSheet(Strings.DATASHEET_OUTPUT_AVG_SPATIAL_FLOW_GROUP),
												0,
												timestep,
												id,
												Strings.SPATIAL_MAP_AVG_FLOW_GROUP_VARIABLE_PREFIX,
												Strings.DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN);
								}
						}
				}

				private void WriteAverageLateralFlowRasters()
				{
						Debug.Assert(this.STSimTransformer.IsSpatial);
						Debug.Assert(this.m_CreateAvgSpatialLateralFlowOutput);

						foreach (int id in this.m_AvgLateralFlowMap.Keys)
						{
								FlowGroup sg = this.m_FlowGroups[id];

								if (!sg.OutputFilter.HasFlag(OutputFilter.AvgSpatial))
								{
										continue;
								}

								Dictionary<int, float[]> dict = this.m_AvgLateralFlowMap[id];

								foreach (int timestep in dict.Keys)
								{
										if (timestep == this.STSimTransformer.TimestepZero)
										{
												continue;
										}

										float[] values = dict[timestep];
										var distArray = values.Distinct();

										if (distArray.Count() == 1)
										{
												var el0 = distArray.ElementAt(0);

												if (el0.Equals(Spatial.DefaultNoDataValue))
												{
														continue;
												}
										}

										SyncroSimRaster rast = this.STSimTransformer.InputRasters.CreateOutputRaster(RasterDataType.DTFloat);
										float[] arr = rast.FloatCells;

										foreach (Cell c in this.STSimTransformer.Cells)
										{
												arr[c.CellId] = Convert.ToSingle(values[c.CollectionIndex]);
										}

										Spatial.WriteRasterData(
												rast,
												this.ResultScenario.GetDataSheet(Strings.DATASHEET_OUTPUT_AVG_SPATIAL_LATERAL_FLOW_GROUP),
												0,
												timestep,
												id,
												Strings.SPATIAL_MAP_AVG_LATERAL_FLOW_GROUP_VARIABLE_PREFIX,
												Strings.DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN);
								}
						}
				}

				private void RecordAverageStockValues(int timestep)
				{
						if (!this.STSimTransformer.IsSpatial)
						{
								return;
						}

						if (!this.m_CreateAvgSpatialStockOutput)
						{
								return;
						}

						if (this.m_AvgSpatialStockOutputAcrossTimesteps)
						{
								this.RecordAverageStockValuesAcrossTimesteps(timestep);
						}
						else
						{
								if (this.m_STSimTransformer.IsOutputTimestepAverage(
										timestep, 
										this.m_AvgSpatialStockOutputTimesteps, 
										this.m_CreateAvgSpatialStockOutput))
								{
										this.RecordAverageStockValuesNormalMethod(timestep);
								}
						}
				}

				private void RecordAverageStockValuesTimestepZero()
				{
						if (!this.STSimTransformer.IsSpatial)
						{
								return;
						}

						if (!this.m_CreateAvgSpatialStockOutput)
						{
								return;
						}

						this.RecordAverageStockValuesNormalMethod(this.STSimTransformer.TimestepZero);
				}

				private void RecordAverageStockValuesNormalMethod(int timestep)
				{
						Debug.Assert(this.STSimTransformer.IsSpatial);
						Debug.Assert(this.m_CreateAvgSpatialStockOutput);

#if DEBUG
						if (timestep != this.STSimTransformer.TimestepZero) { Debug.Assert(!this.m_AvgSpatialStockOutputAcrossTimesteps); }
#endif

						foreach (StockGroup g in this.m_StockGroups)
						{
								if (!g.OutputFilter.HasFlag(OutputFilter.AvgSpatial))
								{
										continue;
								}

								Dictionary<int, float[]> dict = this.m_AvgStockMap[g.Id];
								float[] Values = dict[timestep];

								foreach (Cell c in this.STSimTransformer.Cells)
								{
										float Amount = 0;
										int i = c.CollectionIndex;
										Dictionary<int, float> StockAmounts = GetStockAmountDictionary(c);

										foreach (StockTypeLinkage l in g.StockTypeLinkages)
										{
												if (!float.IsNaN(StockAmounts[l.StockType.Id]))
												{
														Amount += Convert.ToSingle(StockAmounts[l.StockType.Id] * l.Value);
												}
										}

										Values[i] += Amount / this.m_TotalIterations;
								}               
						}
				}

				private void RecordAverageStockValuesAcrossTimesteps(int timestep)
				{
						Debug.Assert(this.STSimTransformer.IsSpatial);
						Debug.Assert(this.m_CreateAvgSpatialStockOutput);
						Debug.Assert(this.m_AvgSpatialStockOutputAcrossTimesteps);

						int timestepKey = this.GetTimestepKeyForCumulativeAverage(timestep, this.m_AvgSpatialStockOutputTimesteps);

						foreach (StockGroup g in this.m_StockGroups)
						{
								if (!g.OutputFilter.HasFlag(OutputFilter.AvgSpatial))
								{
										continue;
								}

								Dictionary<int, float[]> dict = this.m_AvgStockMap[g.Id];
								float[] Values = dict[timestepKey];

								foreach (Cell c in this.STSimTransformer.Cells)
								{
										float Amount = 0;
										int i = c.CollectionIndex;
										Dictionary<int, float> StockAmounts = GetStockAmountDictionary(c);

										foreach (StockTypeLinkage l in g.StockTypeLinkages)
										{
												if (!float.IsNaN(StockAmounts[l.StockType.Id]))
												{
														Amount += Convert.ToSingle(StockAmounts[l.StockType.Id] * l.Value);
												}
										}

										if ((timestepKey == this.STSimTransformer.MaximumTimestep) && (((timestepKey - this.STSimTransformer.TimestepZero) % this.m_AvgSpatialStockOutputTimesteps) != 0))
										{
												Values[i] += Amount / ((timestepKey - this.STSimTransformer.TimestepZero) % this.m_AvgSpatialStockOutputTimesteps * this.m_TotalIterations);
										}
										else
										{
												Values[i] += Amount / (this.m_AvgSpatialStockOutputTimesteps * this.m_TotalIterations);
										}
								}
						}
				}

				private void RecordAverageFlowValues(int timestep)
				{
						if (!this.STSimTransformer.IsSpatial)
						{
								return;
						}

						if (!this.m_CreateAvgSpatialFlowOutput)
						{
								return;
						}

						if (this.m_AvgSpatialFlowOutputAcrossTimesteps)
						{
								this.RecordAverageFlowValuesAcrossTimesteps(timestep);
						}
						else
						{
								if (this.m_STSimTransformer.IsOutputTimestepAverage(
										timestep, 
										this.m_AvgSpatialFlowOutputTimesteps, 
										this.m_CreateAvgSpatialFlowOutput))
								{
										this.RecordAverageFlowValuesNormalMethod(timestep);
								}
						}
				}

				private void RecordAverageFlowValuesNormalMethod(int timestep)
				{
						Debug.Assert(this.STSimTransformer.IsSpatial);
						Debug.Assert(this.m_CreateAvgSpatialFlowOutput);
						Debug.Assert(!this.m_AvgSpatialFlowOutputAcrossTimesteps);

						foreach (FlowGroup g in this.m_FlowGroups)
						{
								if (!g.OutputFilter.HasFlag(OutputFilter.AvgSpatial))
								{
										continue;
								}

								Dictionary<int, float[]> dict = this.m_AvgFlowMap[g.Id];
								float[] Values = dict[timestep];

								foreach (Cell c in this.m_STSimTransformer.Cells)
								{
										float Amount = 0;
										int i = c.CollectionIndex;

										foreach (FlowTypeLinkage l in g.FlowTypeLinkages)
										{
												SpatialOutputFlowRecord rec = GetSpatialOutputFlowDictionary()[l.FlowType.Id];

												if (rec.HasOutputData)
												{
														if ((rec.Data[i] != Spatial.DefaultNoDataValue) && !float.IsNaN(rec.Data[i]))
														{
																Amount += Convert.ToSingle(rec.Data[i] * l.Value);
														}
												}
										}
										
										Values[i] += Amount / this.m_TotalIterations;
								}
						}
				}

				private void RecordAverageFlowValuesAcrossTimesteps(int timestep)
				{
						Debug.Assert(this.STSimTransformer.IsSpatial);
						Debug.Assert(this.m_CreateAvgSpatialFlowOutput);
						Debug.Assert(this.m_AvgSpatialFlowOutputAcrossTimesteps);

						int timestepKey = this.GetTimestepKeyForCumulativeAverage(timestep, this.m_AvgSpatialFlowOutputTimesteps);

						foreach (FlowGroup g in this.m_FlowGroups)
						{
								if (!g.OutputFilter.HasFlag(OutputFilter.AvgSpatial))
								{
										continue;
								}

								Dictionary<int, float[]> dict = this.m_AvgFlowMap[g.Id];
								float[] Values = dict[timestepKey];

								foreach (Cell c in this.m_STSimTransformer.Cells)
								{
										float Amount = 0;
										int i = c.CollectionIndex;

										foreach (FlowTypeLinkage l in g.FlowTypeLinkages)
										{
												SpatialOutputFlowRecord rec = GetSpatialOutputFlowDictionary()[l.FlowType.Id];

												if (rec.HasOutputData)
												{
														if ((rec.Data[i] != Spatial.DefaultNoDataValue) && !float.IsNaN(rec.Data[i]))
														{
																Amount += Convert.ToSingle(rec.Data[i] * l.Value);
														}
												}
										}

										if ((timestepKey == this.STSimTransformer.MaximumTimestep) && (((timestepKey - this.STSimTransformer.TimestepZero) % this.m_AvgSpatialFlowOutputTimesteps) != 0))
										{
												Values[i] += Amount / ((timestepKey - this.STSimTransformer.TimestepZero) % this.m_AvgSpatialFlowOutputTimesteps * this.m_TotalIterations);
										}
										else
										{
												Values[i] += Amount / (this.m_AvgSpatialFlowOutputTimesteps * this.m_TotalIterations);
										}
								}
						}
				}

				private void RecordAverageLateralFlowValues(int timestep)
				{
						if (!this.STSimTransformer.IsSpatial)
						{
								return;
						}

						if (!this.m_CreateAvgSpatialLateralFlowOutput)
						{
								return;
						}

						if (this.m_AvgSpatialLateralFlowOutputAcrossTimesteps)
						{
								this.RecordAverageLateralFlowValuesAcrossTimesteps(timestep);
						}
						else
						{
								if (this.m_STSimTransformer.IsOutputTimestepAverage(
										timestep,
										this.m_AvgSpatialLateralFlowOutputTimesteps,
										this.m_CreateAvgSpatialLateralFlowOutput))
								{
										this.RecordAverageLateralFlowValuesNormalMethod(timestep);
								}
						}
				}

				private void RecordAverageLateralFlowValuesNormalMethod(int timestep)
				{
						Debug.Assert(this.STSimTransformer.IsSpatial);
						Debug.Assert(this.m_CreateAvgSpatialLateralFlowOutput);
						Debug.Assert(!this.m_AvgSpatialLateralFlowOutputAcrossTimesteps);

						foreach (FlowGroup g in this.m_FlowGroups)
						{
								if (!g.OutputFilter.HasFlag(OutputFilter.AvgSpatial))
								{
										continue;
								}

								Dictionary<int, float[]> dict = this.m_AvgLateralFlowMap[g.Id];
								float[] Values = dict[timestep];

								foreach (Cell c in this.m_STSimTransformer.Cells)
								{
										float Amount = 0;
										int i = c.CollectionIndex;

										foreach (FlowTypeLinkage l in g.FlowTypeLinkages)
										{
												SpatialOutputFlowRecord rec = GetLateralOutputFlowDictionary()[l.FlowType.Id];

												if (rec.HasOutputData)
												{
														if((rec.Data[i] != Spatial.DefaultNoDataValue) && !float.IsNaN(rec.Data[i]))
														{
																Amount += Convert.ToSingle(rec.Data[i] * l.Value);
														}
												}                     
										}

										Values[i] += Amount / this.m_TotalIterations;
								}
						}
				}

				private void RecordAverageLateralFlowValuesAcrossTimesteps(int timestep)
				{
						Debug.Assert(this.STSimTransformer.IsSpatial);
						Debug.Assert(this.m_CreateAvgSpatialLateralFlowOutput);
						Debug.Assert(this.m_AvgSpatialLateralFlowOutputAcrossTimesteps);

						int timestepKey = this.GetTimestepKeyForCumulativeAverage(timestep, this.m_AvgSpatialLateralFlowOutputTimesteps);

						foreach (FlowGroup g in this.m_FlowGroups)
						{
								if (!g.OutputFilter.HasFlag(OutputFilter.AvgSpatial))
								{
										continue;
								}

								Dictionary<int, float[]> dict = this.m_AvgLateralFlowMap[g.Id];
								float[] Values = dict[timestepKey];

								foreach (Cell c in this.m_STSimTransformer.Cells)
								{
										float Amount = 0;
										int i = c.CollectionIndex;

										foreach (FlowTypeLinkage l in g.FlowTypeLinkages)
										{
												SpatialOutputFlowRecord rec = GetLateralOutputFlowDictionary()[l.FlowType.Id];

												if (rec.HasOutputData)
												{
														if ((rec.Data[i] != Spatial.DefaultNoDataValue) && !float.IsNaN(rec.Data[i]))
														{
																Amount += Convert.ToSingle(rec.Data[i] * l.Value);
														}
												}                         
										}

										if ((timestepKey == this.STSimTransformer.MaximumTimestep) && (((timestepKey - this.STSimTransformer.TimestepZero) % this.m_AvgSpatialLateralFlowOutputTimesteps) != 0))
										{
												Values[i] += Amount / ((timestepKey - this.STSimTransformer.TimestepZero) % this.m_AvgSpatialLateralFlowOutputTimesteps * this.m_TotalIterations);
										}
										else
										{
												Values[i] += Amount / (this.m_AvgSpatialLateralFlowOutputTimesteps * this.m_TotalIterations);
										}
								}
						}
				}

				private int GetTimestepKeyForCumulativeAverage(int timestep, int frequency)
				{
						int timestepKey = 0;

						if (timestep == this.STSimTransformer.MaximumTimestep)
						{
								timestepKey = this.STSimTransformer.MaximumTimestep;
						}
						else
						{
								//We're looking for the the timestep which is the first one that is >= to the current timestep

								timestepKey = Convert.ToInt32(Math.Ceiling(
										Convert.ToDouble(timestep - this.STSimTransformer.TimestepZero) / frequency) * frequency) +
												this.STSimTransformer.TimestepZero;

								if (timestepKey > this.STSimTransformer.MaximumTimestep)
								{
										timestepKey = this.STSimTransformer.MaximumTimestep;
								}
						}

						return timestepKey;
				}

				internal int GetSecondaryStratumIdKey(int? value)
		{
			if (this.m_SummaryOmitSecondaryStrata)
			{
				return Constants.OUTPUT_COLLECTION_WILDCARD_KEY;
			}
			else
			{
				return LookupKeyUtils.GetOutputCollectionKey(value);
			}
		}

		private int? GetSecondaryStratumIdValue(int? value)
		{
			if (this.m_SummaryOmitSecondaryStrata)
			{
				return null;
			}
			else
			{
				return value;
			}
		}

		internal int GetTertiaryStratumIdKey(int? value)
		{
			if (this.m_SummaryOmitTertiaryStrata)
			{
				return Constants.OUTPUT_COLLECTION_WILDCARD_KEY;
			}
			else
			{
				return LookupKeyUtils.GetOutputCollectionKey(value);
			}
		}

		private int? GetTertiaryStratumIdValue(int? value)
		{
			if (this.m_SummaryOmitTertiaryStrata)
			{
				return null;
			}
			else
			{
				return value;
			}
		}

		private int GetSecondaryStratumIdKey(Cell simulationCell)
		{
			return GetSecondaryStratumIdKey(simulationCell.SecondaryStratumId);
		}

		private int? GetSecondaryStratumIdValue(Cell simulationCell)
		{
			return GetSecondaryStratumIdValue(simulationCell.SecondaryStratumId);
		}

		private int GetTertiaryStratumIdKey(Cell simulationCell)
		{
			return GetTertiaryStratumIdKey(simulationCell.TertiaryStratumId);
		}

		private int? GetTertiaryStratumIdValue(Cell simulationCell)
		{
			return GetTertiaryStratumIdValue(simulationCell.TertiaryStratumId);
		}
	}
}