// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using SyncroSim.Core;
using System.Diagnostics;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
	internal partial class StockFlowTransformer
	{  
		private const string STOCK_AMOUNT_KEY = "stockamountkey";

		/// <summary>
		/// Gets the stock amount dictionary for the specified cell
		/// </summary>
		/// <param name="cell"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		private static Dictionary<int, float> GetStockAmountDictionary(Cell cell)
		{
						Dictionary<int, float> StockAmounts = (Dictionary<int, float>)cell.GetAssociatedObject(STOCK_AMOUNT_KEY);

						if (StockAmounts == null)
						{
								StockAmounts = new Dictionary<int, float>();
								cell.SetAssociatedObject(STOCK_AMOUNT_KEY, StockAmounts);
						}

						return StockAmounts;
				}

				/// <summary>
				/// Gets the spatial output flow dictionary
				/// </summary>
				/// <returns></returns>
				/// <remarks>
				/// We must lazy-load this dictionary because this transformer runs before ST-Sim's
				/// and so the cell data is not there yet.
				/// </remarks>
				private Dictionary<int, SpatialOutputFlowRecord> GetSpatialOutputFlowDictionary()
				{
						if (this.m_SpatialOutputFlowDict == null)
						{
								this.m_SpatialOutputFlowDict = new Dictionary<int, SpatialOutputFlowRecord>();

								foreach (FlowType ft in this.m_FlowTypes)
								{
										if (ft.OutputFilter.HasFlag(OutputFilter.Spatial) ||
												ft.OutputFilter.HasFlag(OutputFilter.AvgSpatial))
										{
												SpatialOutputFlowRecord rec = new SpatialOutputFlowRecord();

												rec.FlowTypeId = ft.Id;
												rec.Data = new float[this.STSimTransformer.Cells.Count];
												rec.HasOutputData = false;

												this.m_SpatialOutputFlowDict.Add(ft.Id, rec);
										}
								}
						}

						return this.m_SpatialOutputFlowDict;
				}

				/// <summary>
				/// Gets the lateral output flow dictionary
				/// </summary>
				/// <returns></returns>
				/// <remarks>
				/// We must lazy-load this dictionary because this transformer runs before ST-Sim's
				/// and so the cell data is not there yet.
				/// </remarks>
				private Dictionary<int, SpatialOutputFlowRecord> GetLateralOutputFlowDictionary()
				{
						if (this.m_LateralOutputFlowDict == null)
						{
								this.m_LateralOutputFlowDict = new Dictionary<int, SpatialOutputFlowRecord>();

								foreach (FlowType ft in this.m_FlowTypes)
								{
										if (ft.OutputFilter.HasFlag(OutputFilter.Spatial) ||
												ft.OutputFilter.HasFlag(OutputFilter.AvgSpatial))
										{
												SpatialOutputFlowRecord rec = new SpatialOutputFlowRecord();

												rec.FlowTypeId = ft.Id;
												rec.Data = new float[this.STSimTransformer.Cells.Count];
												rec.HasOutputData = false;

												this.m_LateralOutputFlowDict.Add(ft.Id, rec);
										}                      
								}
						}

						return this.m_LateralOutputFlowDict;
				}

		/// <summary>
		/// Get Stock Values for the specified Stock Type Id, placing then into the DblCells() in the specified raster.
		/// </summary>
		/// <param name="stockTypeId">The Stock Type Id that we want values for</param>
		/// <param name="rastStockType">An object of type ApexRaster, where we will write the Stock Type values. The raster should be initialized with metadata and appropriate array sizing.</param>
		/// <remarks></remarks>
		private void GetStockValues(int stockTypeId, SyncroSimRaster rastStockType)
				{
						double AmountPerCell = this.m_STSimTransformer.AmountPerCell;

						foreach (Cell c in this.STSimTransformer.Cells)
						{
								Dictionary<int, float> StockAmounts = GetStockAmountDictionary(c);

								if (StockAmounts.Count > 0)
								{
										rastStockType.FloatCells[c.CellId] = Convert.ToSingle(StockAmounts[stockTypeId] / AmountPerCell);
								}
								else
								{
										//I wouldnt expect to get here because of Stratum/StateClass test above
										Debug.Assert(false);
								}
						}
				}

				private float GetAttributeValue(
						int stateAttributeTypeId, int stratumId, int? secondaryStratumId, int? tertiaryStratumId,
						int stateClassId, int iteration, int timestep, int age, TstCollection cellTst)
				{
						float val = 0.0F;

						double? v = this.STSimTransformer.GetAttributeValue(
								stateAttributeTypeId, stratumId, secondaryStratumId, tertiaryStratumId,
								stateClassId, iteration, timestep, age, cellTst);

						if (v.HasValue)
						{
								val = Convert.ToSingle(v.Value);
						}

						return val;
				}

				protected bool AnyOutputOptionsSelected()
		{
			DataRow dr = this.ResultScenario.GetDataSheet(Strings.DATASHEET_STOCKFLOW_OO_NAME).GetDataRow();

			if (dr == null)
			{
				return false;
			}

			if (DataTableUtilities.GetDataBool(
				dr, Strings.DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_ST_COLUMN_NAME) || 
				DataTableUtilities.GetDataBool(dr, Strings.DATASHEET_STOCKFLOW_OO_SUMMARY_OUTPUT_FL_COLUMN_NAME) || 
				DataTableUtilities.GetDataBool(dr, Strings.DATASHEET_STOCKFLOW_OO_SPATIAL_OUTPUT_ST_COLUMN_NAME) ||
				DataTableUtilities.GetDataBool(dr, Strings.DATASHEET_STOCKFLOW_OO_SPATIAL_OUTPUT_FL_COLUMN_NAME) ||
				DataTableUtilities.GetDataBool(dr, Strings.DATASHEET_STOCKFLOW_OO_LATERAL_OUTPUT_FL_COLUMN_NAME))
			{
				return true;
			}

			return false;
		}

		/// <summary>
		/// Determines if it is possible to compute stocks and flows
		/// </summary>
		/// <returns></returns>
		/// <remarks>
		/// (1.) If none of the stock-flow settings are provided then the user is not using the stock-flow feature.
		/// (2.) If the flow pathways are missing then we can't compute stocks and flows.  However, log a message if any of the other settings are specified.
		/// (3.) If flow pathways exist but no initial stocks are specified, log a warning that all stocks will be initialized to zero.
		/// </remarks>
		protected bool CanComputeStocksAndFlows()
		{
			bool OutputOptionsExist = this.AnyOutputOptionsSelected();
			bool ICSpatialRecordsExist = (this.ResultScenario.GetDataSheet(Strings.DATASHEET_INITIAL_STOCK_SPATIAL).GetData().Rows.Count > 0);
			bool ICNonSpatialRecordsExist = (this.ResultScenario.GetDataSheet(Strings.DATASHEET_INITIAL_STOCK_NON_SPATIAL).GetData().Rows.Count > 0);

			if (!OutputOptionsExist && !ICSpatialRecordsExist && !ICNonSpatialRecordsExist)
			{
				return false;
			}

			if (!ICSpatialRecordsExist && !ICNonSpatialRecordsExist)
			{
				this.RecordStatus(StatusType.Information, "No initial stocks have been specified.  All stocks will be initialized to zero.");
			}

			return true;
		}

				protected bool IsStockLimitsOnSourceAndTarget()
				{
						DataTable stockLims = this.ResultScenario.GetDataSheet(Strings.DATASHEET_STOCK_LIMIT_NAME).GetData();
						// this.ResultScenario.DataFeeds.GetDataSheet(Constants.DATASHEET_STOCK_LIMIT_NAME);
						return false;
				}

				private List<List<FlowType>> CreateListOfFlowTypeLists()
				{
						List<List<FlowType>> FlowTypeLists = new List<List<FlowType>>();

						if (this.m_ApplyEquallyRankedSimultaneously)
						{
								SortedDictionary<double, List<FlowType>> FlowTypeOrderDictionary = new SortedDictionary<double, List<FlowType>>();

								foreach (FlowType ft in this.m_ShufflableFlowTypes)
								{
										if (!FlowTypeOrderDictionary.ContainsKey(ft.Order))
										{
												FlowTypeOrderDictionary.Add(ft.Order, new List<FlowType>());
										}

										FlowTypeOrderDictionary[ft.Order].Add(ft);
								}

								foreach (double order in FlowTypeOrderDictionary.Keys)
								{
										List<FlowType> l = FlowTypeOrderDictionary[order];
										FlowTypeLists.Add(l);
								}
						}
						else
						{
								foreach (FlowType ft in this.m_ShufflableFlowTypes)
								{
										List<FlowType> l = new List<FlowType>();
										l.Add(ft);
										FlowTypeLists.Add(l);
								}
						}

						return (FlowTypeLists);
				}

				private bool FilterIncludesTabularDataForStockGroup(int stockGroupId)
				{
						if (!this.m_OutputFilterStocks.HasItems)
						{
								return true;
						}

						OutputFilterStocks flt = (OutputFilterStocks)this.m_OutputFilterStocks.Get(stockGroupId);

						if (flt == null)
						{
								return false;
						}

						return flt.OutputTabularData;
				}

				private bool FilterIncludesSpatialDataForStockGroup(int stockGroupId)
				{
						if (!this.m_OutputFilterStocks.HasItems)
						{
								return true;
						}

						OutputFilterStocks flt = (OutputFilterStocks)this.m_OutputFilterStocks.Get(stockGroupId);

						if (flt == null)
						{
								return false;
						}

						return flt.OutputSpatialData;
				}

				private bool FilterIncludesAvgSpatialDataForStockGroup(int stockGroupId)
				{
						if (!this.m_OutputFilterStocks.HasItems)
						{
								return true;
						}

						OutputFilterStocks flt = (OutputFilterStocks)this.m_OutputFilterStocks.Get(stockGroupId);

						if (flt == null)
						{
								return false;
						}

						return flt.OutputAvgSpatialData;
				}

				private bool FilterIncludesTabularDataForFlowGroup(int flowGroupId)
				{
						if (!this.m_OutputFilterFlows.HasItems)
						{
								return true;
						}

						OutputFilterFlows flt = (OutputFilterFlows)this.m_OutputFilterFlows.Get(flowGroupId);

						if (flt == null)
						{
								return false;
						}

						return flt.OutputTabularData;
				}

				private bool FilterIncludesSpatialDataForFlowGroup(int flowGroupId)
				{
						if (!this.m_OutputFilterFlows.HasItems)
						{
								return true;
						}

						OutputFilterFlows flt = (OutputFilterFlows)this.m_OutputFilterFlows.Get(flowGroupId);

						if (flt == null)
						{
								return false;
						}

						return flt.OutputSpatialData;
				}

				private bool FilterIncludesAvgSpatialDataForFlowGroup(int flowGroupId)
				{
						if (!this.m_OutputFilterFlows.HasItems)
						{
								return true;
						}

						OutputFilterFlows flt = (OutputFilterFlows)this.m_OutputFilterFlows.Get(flowGroupId);

						if (flt == null)
						{
								return false;
						}

						return flt.OutputAvgSpatialData;
				}

				private bool FilterIncludesSpatialDataForFlowType(int flowTypeId)
				{
						FlowType ft = this.m_FlowTypes[flowTypeId];

						foreach (FlowGroupLinkage l in ft.FlowGroupLinkages)
						{
								if (this.FilterIncludesSpatialDataForFlowGroup(l.FlowGroup.Id))
								{
										return true;
								}
						}

						return false;
				}

				private bool FilterIncludesAvgSpatialDataForFlowType(int flowTypeId)
				{
						FlowType ft = this.m_FlowTypes[flowTypeId];

						foreach (FlowGroupLinkage l in ft.FlowGroupLinkages)
						{
								if (this.FilterIncludesAvgSpatialDataForFlowGroup(l.FlowGroup.Id))
								{
										return true;
								}
						}

						return false;
				}

				private int ReturnMinimumNonZeroValue(int value1, int value2)
				{
						int minValue = value1 == 0 && value2 == 0 ? 0
								: value1 == 0 ? value2
								: value2 == 0 ? value1
								: Math.Min(value2, value1);

						return minValue;
				}

				private bool IsValueMultipleOf(int smallValue, int largeValue)
				{
						if (smallValue != 0)
						{
								if (largeValue % smallValue != 0)
								{
										return false;
								}
						}

						return true;
				}
		}
}
