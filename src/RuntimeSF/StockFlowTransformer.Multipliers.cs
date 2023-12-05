// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Diagnostics;
using System.Globalization;
using SyncroSim.Core;
using System.Collections.Generic;
using System.Data;

namespace SyncroSim.STSim
{
    partial class StockFlowTransformer
    {
        private FlowMultiplierType GetFlowMultiplierType(int? id)
        {
            foreach (FlowMultiplierType t in this.m_FlowMultiplierTypes)
            {
                if (Nullable.Compare(t.FlowMultiplierTypeId, id) == 0)
                {
                    return t;
                }
            }

            Debug.Assert(false);
            return null;
        }

        private double GetFlowMultiplierByStock(
            int flowGroupId, FlowMultiplierByStockMap map, int iteration, int timestep, Cell simulationCell)
        {
            Debug.Assert(this.m_FlowMultipliersByStock.Count > 0);

            double Multiplier = 1.0;
            DataSheet Groups = this.Project.GetDataSheet(Strings.DATASHEET_STOCK_GROUP_NAME);
            DataSheet TGMembership = this.ResultScenario.GetDataSheet(Strings.DATASHEET_STOCK_TYPE_GROUP_MEMBERSHIP_NAME);
            Dictionary<int, float> StockAmounts = GetStockAmountDictionary(simulationCell);
            var dtgroups = Groups.GetData();
            var dtmembership = TGMembership.GetData();

            foreach (DataRow dr in dtgroups.Rows)
            {
                float StockGroupValue = 0.0F;
                int StockGroupId = Convert.ToInt32(dr[Groups.ValueMember], CultureInfo.InvariantCulture);
                string query = string.Format(CultureInfo.InvariantCulture, "StockGroupID={0}", StockGroupId);
                DataRow[] rows = dtmembership.Select(query);

                foreach (DataRow r in rows)
                {
                    float ValueMultiplier = 1.0F;
                    int StockTypeId = Convert.ToInt32(r[Strings.STOCK_TYPE_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                    float StockTypeAmount = 0.0F;

                    if (StockAmounts.ContainsKey(StockTypeId))
                    {
                        StockTypeAmount = StockAmounts[StockTypeId];
                    }

                    if (!Convert.IsDBNull(r[Strings.DATASHEET_VALUE_COLUMN_NAME]))
                    {
                        ValueMultiplier = Convert.ToSingle(r[Strings.DATASHEET_VALUE_COLUMN_NAME], CultureInfo.InvariantCulture);
                    }

                    StockGroupValue += ((StockTypeAmount * ValueMultiplier) / Convert.ToSingle(this.m_STSimTransformer.AmountPerCell));
                }

                Multiplier *= map.GetFlowMultiplierByStock(
                    StockGroupId, simulationCell.StratumId, simulationCell.SecondaryStratumId, 
                    simulationCell.TertiaryStratumId, simulationCell.StateClassId, flowGroupId, 
                    iteration, timestep, StockGroupValue);
            }

            return Multiplier;
        }

        private double GetFlowSpatialMultiplier(
            Cell cell,
            FlowSpatialMultiplierMap map,
            int flowGroupId,
            int iteration,
            int timestep)
        {
            Debug.Assert(this.m_IsSpatial);
            Debug.Assert(this.m_FlowSpatialMultipliers.Count > 0);

            FlowSpatialMultiplier m = map.GetFlowSpatialMultiplier(flowGroupId, iteration, timestep);

            if (m == null)
            {
                return 1.0;
            }

            if (!this.m_FlowSpatialMultiplierRasters.ContainsKey(m.FileName))
            {
                return 1.0;
            }

            SyncroSimRaster raster = this.m_FlowSpatialMultiplierRasters[m.FileName];
            double v = raster.DblCells[cell.CollectionIndex];

            if (MathUtilities.CompareDoublesEqual(v, raster.NoDataValue, double.Epsilon))
            {
                return 1.0;
            }
            else
            {
                return v;
            }
        }

        private double GetFlowLateralMultiplier(
            Cell cell,
            FlowLateralMultiplierMap map,
            int flowGroupId,
            int iteration,
            int timestep)
        {
            Debug.Assert(this.m_IsSpatial);
            Debug.Assert(this.m_FlowLateralMultipliers.Count > 0);

            FlowLateralMultiplier m = map.GetFlowLateralMultiplier(flowGroupId, iteration, timestep);

            if (m == null)
            {
                return 1.0;
            }

            if (!this.m_FlowLateralMultiplierRasters.ContainsKey(m.FileName))
            {
                return 1.0;
            }

            SyncroSimRaster raster = this.m_FlowLateralMultiplierRasters[m.FileName];
            double v = raster.DblCells[cell.CollectionIndex];

            if ((v < 0.0) || (MathUtilities.CompareDoublesEqual(v, raster.NoDataValue, double.Epsilon)))
            {
                return 1.0;
            }
            else
            {
                return v;
            }
        }

        private double GetFlowLateralMultiplier(int flowTypeId, Cell cell, int iteration, int timestep)
        {
            double Multiplier = 1.0;
            FlowType ft = this.m_FlowTypes[flowTypeId];

            foreach (FlowMultiplierType mt in this.m_FlowMultiplierTypes)
            {
                foreach (FlowGroupLinkage fgl in ft.FlowGroupLinkages)
                {
                    if (this.m_IsSpatial && mt.FlowLateralMultiplierMap != null)
                    {
                        Multiplier *= this.GetFlowLateralMultiplier(
                            cell,
                            mt.FlowLateralMultiplierMap,
                            fgl.FlowGroup.Id,
                            iteration,
                            timestep);
                    }
                }
            }

            Debug.Assert(Multiplier >= 0.0);
            return Multiplier;
        }

        private void ValidateFlowSpatialMultipliers()
        {
            Debug.Assert(this.m_IsSpatial);
            DataSheet ds = this.ResultScenario.GetDataSheet(Strings.DATASHEET_FLOW_SPATIAL_MULTIPLIER_NAME);

            for (int i = this.m_FlowSpatialMultipliers.Count - 1; i >= 0; i--)
            {
                FlowSpatialMultiplier r = this.m_FlowSpatialMultipliers[i];

                if (!this.m_FlowSpatialMultiplierRasters.ContainsKey(r.FileName))
                {
                    string msg = string.Format(CultureInfo.InvariantCulture, Strings.SPATIAL_PROCESS_WARNING, r.FileName);
                    RecordStatus(StatusType.Warning, msg);

                    continue;
                }

                string cmpMsg = "";
                var cmpRes = this.STSimTransformer.InputRasters.CompareMetadata(this.m_FlowSpatialMultiplierRasters[r.FileName], ref cmpMsg);
                string FullFilename = Spatial.GetSpatialDataFileName(ds, r.FileName, false);

                if (cmpRes == CompareMetadataResult.RowColumnMismatch)
                {
                    string msg = string.Format(CultureInfo.InvariantCulture, Strings.SPATIAL_METADATA_ROW_COLUMN_MISMATCH, FullFilename);
                    ExceptionUtils.ThrowArgumentException(msg);
                }
                else
                {
                    if (cmpRes == CompareMetadataResult.UnimportantDifferences)
                    {
                        string msg = string.Format(CultureInfo.InvariantCulture, Strings.SPATIAL_METADATA_INFO, FullFilename, cmpMsg);
                        RecordStatus(StatusType.Information, msg);
                    }
                }
            }
        }

        private void ValidateFlowLateralMultipliers()
        {
            Debug.Assert(this.m_IsSpatial);
            DataSheet ds = this.ResultScenario.GetDataSheet(Strings.DATASHEET_FLOW_LATERAL_MULTIPLIER_NAME);

            for (int i = this.m_FlowLateralMultipliers.Count - 1; i >= 0; i--)
            {
                FlowLateralMultiplier r = this.m_FlowLateralMultipliers[i];

                if (!this.m_FlowLateralMultiplierRasters.ContainsKey(r.FileName))
                {
                    string msg = string.Format(CultureInfo.InvariantCulture, Strings.SPATIAL_PROCESS_WARNING, r.FileName);
                    RecordStatus(StatusType.Warning, msg);

                    continue;
                }

                string cmpMsg = "";
                var cmpRes = this.STSimTransformer.InputRasters.CompareMetadata(this.m_FlowLateralMultiplierRasters[r.FileName], ref cmpMsg);
                string FullFilename = Spatial.GetSpatialDataFileName(ds, r.FileName, false);

                if (cmpRes == CompareMetadataResult.RowColumnMismatch)
                {
                    string msg = string.Format(CultureInfo.InvariantCulture, Strings.SPATIAL_METADATA_ROW_COLUMN_MISMATCH, FullFilename);
                    ExceptionUtils.ThrowArgumentException(msg);
                }
                else
                {
                    if (cmpRes == CompareMetadataResult.UnimportantDifferences)
                    {
                        string msg = string.Format(CultureInfo.InvariantCulture, Strings.SPATIAL_METADATA_INFO, FullFilename, cmpMsg);
                        RecordStatus(StatusType.Information, msg);
                    }
                }
            }
        }
    }
}
