// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Common;
using System.Diagnostics;

namespace SyncroSim.STSim
{
    internal class ProportionAccumulatorMap
    {
        private MultiLevelKeyMap3<AccumulatedProportion> m_Map = new MultiLevelKeyMap3<AccumulatedProportion>();
        private double m_Amount;

        public ProportionAccumulatorMap(double amount)
        {
            this.m_Amount = amount;
        }

        public void AddOrIncrement(int stratumId, int? secondaryStratumId, int? tertiaryStratumId)
        {
            AccumulatedProportion ap = this.m_Map.GetItemExact(stratumId, secondaryStratumId, tertiaryStratumId);

            if (ap == null)
            {
                this.m_Map.AddItem(stratumId, secondaryStratumId, tertiaryStratumId, new AccumulatedProportion(this.m_Amount));
            }
            else
            {
                Debug.Assert(ap.Amount >= this.m_Amount);
                ap.Amount += this.m_Amount;
            }
        }

        public void Decrement(int stratumId, int? secondaryStratumId, int? tertiaryStratumId)
        {
            AccumulatedProportion ap = this.m_Map.GetItemExact(stratumId, secondaryStratumId, tertiaryStratumId);

            ap.Amount -= this.m_Amount;

            if (ap.Amount < 0.0)
            {
                ap.Amount = 0.0;
            }
        }

        public object GetValue(int stratumId, int? secondaryStratumId, int? tertiaryStratumId)
        {
            AccumulatedProportion ap = this.m_Map.GetItemExact(stratumId, secondaryStratumId, tertiaryStratumId);

            if (ap == null)
            {
                return null;
            }
            else
            {
                return ap.Amount;
            }
        }
    }

    /// <summary>
    /// Accumulated Proportion
    /// </summary>
    /// <remarks>Wrapping the value in a class allows for Null as the default value</remarks>
    internal class AccumulatedProportion
    {
        public double Amount;

        public AccumulatedProportion(double value)
        {
            this.Amount = value;
        }
    }
}
