// stsim-stockflow: SyncroSim Add-On Package (to stsim) for integrating stocks and flows into state-and-transition simulation models in ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    class LateralFlowCoupletMap
    {
        private readonly List<LateralFlowCouplet> m_Couplets = new List<LateralFlowCouplet>();
        private readonly Dictionary<string, LateralFlowCouplet> m_LookAside = new Dictionary<string, LateralFlowCouplet>();

        public void AddCouplet(int? stockTypeId, int flowTypeId)
        {
#if DEBUG
            if (stockTypeId.HasValue)
            {
                Debug.Assert(stockTypeId.Value > 0);
            }

            Debug.Assert(flowTypeId > 0);
#endif
            string Key = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", LookupKeyUtils.GetOutputCollectionKey(stockTypeId), flowTypeId);

            if (!this.m_LookAside.ContainsKey(Key))
            {
                LateralFlowCouplet couplet = new LateralFlowCouplet();

                couplet.StockTypeId = stockTypeId;
                couplet.FlowTypeId = flowTypeId;

                this.m_Couplets.Add(couplet);
                this.m_LookAside.Add(Key, couplet);
            }

            Debug.Assert(this.m_Couplets.Count == this.m_LookAside.Count);
        }

        public List<LateralFlowCouplet> Couplets
        {
            get
            {
#if DEBUG
                Debug.Assert(this.m_Couplets.Count == this.m_LookAside.Count);

                foreach (LateralFlowCouplet c in this.m_Couplets)
                {
                    string Key = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", c.StockTypeId, c.FlowTypeId);
                    Debug.Assert(this.m_LookAside.ContainsKey(Key));
                }
#endif
                return this.m_Couplets;
            }
        }
    }
}
