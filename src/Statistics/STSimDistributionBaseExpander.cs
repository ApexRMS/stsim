// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System.Linq;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using SyncroSim.Common;

namespace SyncroSim.STSim
{
    public class STSimDistributionBaseExpander
    {
        private STSimDistributionProvider m_Provider;
        private MultiLevelKeyMap1<Dictionary<string, STSimDistributionValue>> m_1 = new MultiLevelKeyMap1<Dictionary<string, STSimDistributionValue>>();
        private MultiLevelKeyMap2<Dictionary<string, STSimDistributionValue>> m_2 = new MultiLevelKeyMap2<Dictionary<string, STSimDistributionValue>>();
        private MultiLevelKeyMap2<Dictionary<string, STSimDistributionValue>> m_3 = new MultiLevelKeyMap2<Dictionary<string, STSimDistributionValue>>();

        public STSimDistributionBaseExpander(STSimDistributionProvider provider)
        {
            this.m_Provider = provider;
            this.FillUserDistributionMaps();
        }

        public IEnumerable<STSimDistributionBase> Expand(IEnumerable<STSimDistributionBase> items)
        {
            return this.InternalExpand(items);
        }

        private IEnumerable<STSimDistributionBase> InternalExpand(IEnumerable<STSimDistributionBase> items)
        {
            Debug.Assert(items.Count() > 0);
            Debug.Assert(this.m_Provider.Values.Count > 0);

            if (this.m_Provider.Values.Count == 0 || items.Count() == 0)
            {
                return items;
            }

            List<STSimDistributionBase> Expanded = new List<STSimDistributionBase>();

            foreach (STSimDistributionBase t in items)
            {
                if (!ExpansionRequired(t))
                {
                    Expanded.Add(t);
                    continue;
                }

                Dictionary<string, STSimDistributionValue> l = this.GetValueDictionary(t);

                if (l == null)
                {
                    Expanded.Add(t);
                    continue;
                }

                foreach (STSimDistributionValue v in l.Values)
                {
                    STSimDistributionBase n = t.Clone();

                    n.StratumId = v.StratumId;
                    n.SecondaryStratumId = v.SecondaryStratumId;

                    Expanded.Add(n);
                }
            }

            Debug.Assert(Expanded.Count() >= items.Count());
            return Expanded;
        }

        private static string CreateDistBaseKey(int? stratumId, int? secondaryStratumId)
        {
            string s1 = "NULL";
            string s2 = "NULL";

            if (stratumId.HasValue)
            {
                s1 = stratumId.Value.ToString(CultureInfo.InvariantCulture);
            }

            if (secondaryStratumId.HasValue)
            {
                s2 = secondaryStratumId.Value.ToString(CultureInfo.InvariantCulture);
            }

            return s1 + "-" + s2;
        }

        private static bool ExpansionRequired(STSimDistributionBase t)
        {
            if (!t.DistributionTypeId.HasValue)
            {
                return false;
            }
            else if (t.StratumId.HasValue && t.SecondaryStratumId.HasValue)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private Dictionary<string, STSimDistributionValue> GetValueDictionary(STSimDistributionBase t)
        {
            Dictionary<string, STSimDistributionValue> l = null;

            if (!t.StratumId.HasValue && !t.SecondaryStratumId.HasValue)
            {
                l = this.m_1.GetItemExact(t.DistributionTypeId);
            }
            else if (t.StratumId.HasValue)
            {
                l = this.m_2.GetItemExact(t.DistributionTypeId, t.StratumId.Value);
            }
            else
            {
                l = this.m_3.GetItemExact(t.DistributionTypeId, t.SecondaryStratumId.Value);
            }

#if DEBUG
            if (l != null)
            {
                Debug.Assert(l.Count > 0);
            }
#endif

            return l;
        }

        private void FillUserDistributionMaps()
        {
            foreach (STSimDistributionValue v in this.m_Provider.Values)
            {
                Dictionary<string, STSimDistributionValue> d = this.m_1.GetItemExact(v.DistributionTypeId);

                if (d == null)
                {
                    d = new Dictionary<string, STSimDistributionValue>();
                    this.m_1.AddItem(v.DistributionTypeId, d);
                }

                string k = CreateDistBaseKey(v.StratumId, v.SecondaryStratumId);

                if (!d.ContainsKey(k))
                {
                    d.Add(k, v);
                }
            }

            foreach (STSimDistributionValue v in this.m_Provider.Values)
            {
                if (v.StratumId.HasValue)
                {
                    Dictionary<string, STSimDistributionValue> d = this.m_2.GetItemExact(v.DistributionTypeId, v.StratumId.Value);

                    if (d == null)
                    {
                        d = new Dictionary<string, STSimDistributionValue>();
                        this.m_2.AddItem(v.DistributionTypeId, v.StratumId.Value, d);
                    }

                    string k = CreateDistBaseKey(v.StratumId, v.SecondaryStratumId);

                    if (!d.ContainsKey(k))
                    {
                        d.Add(k, v);
                    }
                }
            }

            foreach (STSimDistributionValue v in this.m_Provider.Values)
            {
                if (v.SecondaryStratumId.HasValue)
                {
                    Dictionary<string, STSimDistributionValue> l = this.m_3.GetItemExact(v.DistributionTypeId, v.SecondaryStratumId.Value);

                    if (l == null)
                    {
                        l = new Dictionary<string, STSimDistributionValue>();
                        this.m_3.AddItem(v.DistributionTypeId, v.SecondaryStratumId.Value, l);
                    }

                    string k = CreateDistBaseKey(v.StratumId, v.SecondaryStratumId);

                    if (!l.ContainsKey(k))
                    {
                        l.Add(k, v);
                    }
                }
            }
        }
    }
}
