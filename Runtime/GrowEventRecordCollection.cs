// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace SyncroSim.STSim
{
    internal class GrowEventRecordCollection
    {
        private Dictionary<int, GrowEventRecord> m_Map = new Dictionary<int, GrowEventRecord>();
        private double m_TotalLikelihood;
        private RandomGenerator m_RandomGenerator;

        public GrowEventRecordCollection(RandomGenerator randomGen)
        {
            this.m_RandomGenerator = randomGen;
        }

        public void AddRecord(GrowEventRecord record)
        {
            this.m_Map.Add(record.Cell.CellId, record);
            this.m_TotalLikelihood += record.Likelihood;

            Debug.Assert(MathUtils.CompareDoublesGTEqual(this.m_TotalLikelihood, record.Likelihood, 0.000001));
        }

        public GrowEventRecord RemoveRecord()
        {
            double r = this.m_RandomGenerator.GetNextDouble();
            double InverseCumulativeProb = 1.0;

            Debug.Assert(this.m_Map.Count > 0);
            Debug.Assert(this.m_TotalLikelihood > 0.0);

            foreach (GrowEventRecord v in this.m_Map.Values)
            {
                InverseCumulativeProb -= (v.Likelihood / this.m_TotalLikelihood);
                Debug.Assert(MathUtils.CompareDoublesGTEqual(InverseCumulativeProb, 0.0, 0.00001));

                if (r >= InverseCumulativeProb)
                {
                    this.m_Map.Remove(v.Cell.CellId);

                    Debug.Assert(this.m_TotalLikelihood >= 0.0);
                    this.m_TotalLikelihood -= v.Likelihood;

                    return v;
                }
            }

            Debug.Assert(false);

            GrowEventRecord first = this.m_Map.First().Value;
            this.m_Map.Remove(first.Cell.CellId);
            this.m_TotalLikelihood -= first.Likelihood;

            Debug.Assert(this.m_TotalLikelihood >= 0.0);
            return first;
        }

        public int Count
        {
            get
            {
#if DEBUG
                if (this.m_Map.Count == 0)
                {
                    Debug.Assert(MathUtils.CompareDoublesEqual(this.m_TotalLikelihood, 0.0, 0.000001));
                }
                else
                {
                    Debug.Assert(MathUtils.CompareDoublesGTEqual(this.m_TotalLikelihood, 0.0, 0.000001));
                }
#endif
                return this.m_Map.Count;
            }
        }
    }
}
