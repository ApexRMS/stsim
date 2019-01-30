// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Common;
using System.Diagnostics;

namespace SyncroSim.STSim
{
    internal class TransitionOrderMap
    {
        private bool m_HasItems;
        private SortedKeyMap2<TransitionOrderCollection> m_Map = new SortedKeyMap2<TransitionOrderCollection>(SearchMode.ExactPrev);

        public TransitionOrderMap(TransitionOrderCollection orders)
        {
            foreach (TransitionOrder t in orders)
            {
                this.AddTransitionOrder(t);
            }
        }

        private void AddTransitionOrder(TransitionOrder order)
        {
            TransitionOrderCollection l = this.m_Map.GetItemExact(order.Iteration, order.Timestep);

            if (l == null)
            {
                l = new TransitionOrderCollection();
                this.m_Map.AddItem(order.Iteration, order.Timestep, l);
            }

            l.Add(order);

            this.m_HasItems = true;
        }

        public TransitionOrderCollection GetTransitionOrders(int iteration, int timestep)
        {
            if (!this.m_HasItems)
            {
                return null;
            }

            TransitionOrderCollection l = this.m_Map.GetItem(iteration, timestep);

            if (l == null)
            {
                return null;
            }

            Debug.Assert(l.Count > 0);
            return l;
        }
    }
}
