// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;
using SyncroSim.STSim;
using System.Diagnostics;

namespace SyncroSim.STSim
{
    class FlowMultiplierType
    {
        private readonly Scenario m_Scenario;
        private readonly STSimDistributionProvider m_Provider;
        private readonly int? m_FlowMultiplierTypeId;
        private readonly FlowMultiplierCollection m_FlowMultipliers = new FlowMultiplierCollection();
        private FlowMultiplierMap m_FlowMultiplierMap;
        private readonly FlowSpatialMultiplierCollection m_FlowSpatialMultipliers = new FlowSpatialMultiplierCollection();
        private FlowSpatialMultiplierMap m_FlowSpatialMultiplierMap;
        private readonly FlowLateralMultiplierCollection m_FlowLateralMultipliers = new FlowLateralMultiplierCollection();
        private FlowLateralMultiplierMap m_FlowLateralMultiplierMap;
        private readonly FlowMultiplierByStockCollection m_FlowMultipliersByStock = new FlowMultiplierByStockCollection();
        private FlowMultiplierByStockMap m_FlowMultiplierByStockMap;

        public FlowMultiplierType(
            int? flowMultiplierTypeId, 
            Scenario scenario, 
            STSimDistributionProvider provider)
        {
            this.m_FlowMultiplierTypeId = flowMultiplierTypeId;
            this.m_Scenario = scenario;
            this.m_Provider = provider;
        }

        internal int? FlowMultiplierTypeId
        {
            get
            {
                return this.m_FlowMultiplierTypeId;
            }
        }

        internal FlowMultiplierMap FlowMultiplierMap
        {
            get
            {
                return this.m_FlowMultiplierMap;
            }
        }

        internal FlowSpatialMultiplierMap FlowSpatialMultiplierMap
        {
            get
            {
                return this.m_FlowSpatialMultiplierMap;
            }
        }

        internal FlowLateralMultiplierMap FlowLateralMultiplierMap
        {
            get
            {
                return this.m_FlowLateralMultiplierMap;
            }
        }

        internal FlowMultiplierByStockMap FlowMultiplierByStockMap
        {
            get
            {
                return this.m_FlowMultiplierByStockMap;
            }
        }

        internal void AddFlowMultiplier(FlowMultiplier multiplier)
        {
            if (multiplier.FlowMultiplierTypeId != this.m_FlowMultiplierTypeId)
            {
                throw new ArgumentException("The flow multiplier type is not correct.");
            }

            this.m_FlowMultipliers.Add(multiplier);
        }

        internal void AddFlowSpatialMultiplier(FlowSpatialMultiplier multiplier)
        {
            if (multiplier.FlowMultiplierTypeId != this.m_FlowMultiplierTypeId)
            {
                throw new ArgumentException("The flow multiplier type is not correct.");
            }

            this.m_FlowSpatialMultipliers.Add(multiplier);
        }

        internal void AddFlowLateralMultiplier(FlowLateralMultiplier multiplier)
        {
            if (multiplier.FlowMultiplierTypeId != this.m_FlowMultiplierTypeId)
            {
                throw new ArgumentException("The flow multiplier type is not correct.");
            }

            this.m_FlowLateralMultipliers.Add(multiplier);
        }

        internal void AddFlowMultiplierByStock(FlowMultiplierByStock multiplier)
        {
            if (multiplier.FlowMultiplierTypeId != this.m_FlowMultiplierTypeId)
            {
                throw new ArgumentException("The flow multiplier type is not correct.");
            }

            this.m_FlowMultipliersByStock.Add(multiplier);
        }

        internal void ClearFlowMultiplierMap()
        {
            this.m_FlowMultipliers.Clear();
            this.m_FlowMultiplierMap = null;
        }

        internal void ClearFlowSpatialMultiplierMap()
        {
            this.m_FlowSpatialMultipliers.Clear();
            this.m_FlowSpatialMultiplierMap = null;
        }

        internal void ClearFlowLateralMultiplierMap()
        {
            this.m_FlowLateralMultipliers.Clear();
            this.m_FlowLateralMultiplierMap = null;
        }

        internal void ClearFlowMultiplierByStockMap()
        {
            this.m_FlowMultipliersByStock.Clear();
            this.m_FlowMultiplierByStockMap = null;
        }

        internal void CreateFlowMultiplierMap()
        {
            if (this.m_FlowMultipliers.Count > 0)
            {
                Debug.Assert(this.m_FlowMultiplierMap == null);

                this.m_FlowMultiplierMap = new FlowMultiplierMap(
                    this.m_Scenario, this.m_FlowMultipliers, this.m_Provider);
            }
        }

        internal void CreateSpatialFlowMultiplierMap()
        {
            if (this.m_FlowSpatialMultipliers.Count > 0)
            {
                Debug.Assert(this.m_FlowSpatialMultiplierMap == null);

                this.m_FlowSpatialMultiplierMap = new FlowSpatialMultiplierMap(
                    this.m_Scenario, this.m_FlowSpatialMultipliers);
            }
        }

        internal void CreateLateralFlowMultiplierMap()
        {
            if (this.m_FlowLateralMultipliers.Count > 0)
            {
                Debug.Assert(this.m_FlowLateralMultiplierMap == null);

                this.m_FlowLateralMultiplierMap = new FlowLateralMultiplierMap(
                    this.m_Scenario, this.m_FlowLateralMultipliers);
            }
        }

        internal void CreateFlowMultiplierByStockMap()
        {
            if (this.m_FlowMultipliersByStock.Count > 0)
            {
                Debug.Assert(this.m_FlowMultiplierByStockMap == null);

                this.m_FlowMultiplierByStockMap = new FlowMultiplierByStockMap(
                    this.m_Scenario, this.m_FlowMultipliersByStock, this.m_Provider);
            }
        }
    }
}
