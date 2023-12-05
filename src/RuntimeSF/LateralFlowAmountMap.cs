// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Apex;
using System.Diagnostics;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    class LateralFlowAmountMap
    {
        private readonly List<LateralFlowAmountRecord> m_AllRecords = new List<LateralFlowAmountRecord>();

        private readonly MultiLevelKeyMap6<SortedKeyMap1<LateralFlowAmountRecord>> m_Map = 
            new MultiLevelKeyMap6<SortedKeyMap1<LateralFlowAmountRecord>>();

        public List<LateralFlowAmountRecord> AllRecords
        {
            get
            {
                return this.m_AllRecords;
            }
        }

        public void AddOrUpdate(
            int? stockTypeId,
            int flowTypeId,
            int? primaryStratumId,
            int? secondaryStratumId,
            int? tertiaryStratumId,
            int? stateClassId,
            int? minimumAge, 
            float amount)
        {
            Debug.Assert(
                primaryStratumId.HasValue || secondaryStratumId.HasValue || tertiaryStratumId.HasValue 
                || stateClassId.HasValue || minimumAge.HasValue || stockTypeId.HasValue);

            SortedKeyMap1<LateralFlowAmountRecord> m = this.m_Map.GetItemExact(
                stockTypeId, flowTypeId, primaryStratumId, secondaryStratumId, tertiaryStratumId, stateClassId);

            if (m == null)
            {
                m = new SortedKeyMap1<LateralFlowAmountRecord>(SearchMode.ExactPrev);
                this.m_Map.AddItem(stockTypeId, flowTypeId, primaryStratumId, secondaryStratumId, tertiaryStratumId, stateClassId, m);
            }

            LateralFlowAmountRecord r = m.GetItemExact(minimumAge);

            if (r == null)
            {
                r = new LateralFlowAmountRecord();

                r.StockTypeId = stockTypeId;
                r.FlowTypeId = flowTypeId;
                r.StockAmount = amount;
                m.AddItem(minimumAge, r);

                this.m_AllRecords.Add(r);
            }
            else
            {
                r.StockAmount += amount;
            }
        }

        public LateralFlowAmountRecord GetRecord(
            int? stockTypeId, 
            int flowTypeId, 
            int primaryStratumId, 
            int? secondaryStratumId, 
            int? tertiaryStratumId, 
            int stateClassId, 
            int minimumAge)
        {
            if (this.m_AllRecords.Count == 0)
            {
                return null;
            }

            SortedKeyMap1<LateralFlowAmountRecord> m = this.m_Map.GetItem(
                stockTypeId, flowTypeId, primaryStratumId, secondaryStratumId, tertiaryStratumId, stateClassId);

            if (m == null)
            {
                return null;
            }

            LateralFlowAmountRecord r = m.GetItem(minimumAge);

            if (r != null)
            {
                Debug.Assert(r.StockTypeId == stockTypeId);
                return r;
            }

            return null;
        }
    }
}
