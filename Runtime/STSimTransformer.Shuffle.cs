// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Common;
using System.Diagnostics;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    public partial class STSimTransformer
    {
        /// <summary>
        /// Shuffles the order of the specified stratum's cells
        /// </summary>
        /// <param name="stratum"></param>
        /// <remarks></remarks>
        private void ShuffleStratumCells(Stratum stratum)
        {
            if (stratum.Cells.Count == 0)
            {
                return;
            }

            List<Cell> lst = new List<Cell>();

            foreach (Cell c in stratum.Cells.Values)
            {
                lst.Add(c);
            }

            ShuffleUtilities.ShuffleList(lst, this.m_RandomGenerator.Random);

            stratum.Cells.Clear();

            foreach (Cell c in lst)
            {
                stratum.Cells.Add(c.CellId, c);
            }
        }

        /// <summary>
        /// Reorders the list of shufflable transition groups
        /// </summary>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <param name="groups"></param>
        /// <remarks></remarks>
        private void ReorderShufflableTransitionGroups(int iteration, int timestep)
        {
            TransitionOrderCollection orders = this.m_TransitionOrderMap.GetTransitionOrders(iteration, timestep);

            if (orders == null)
            {
                ShuffleUtilities.ShuffleList(this.m_ShufflableTransitionGroups, this.m_RandomGenerator.Random);
            }
            else
            {
                this.ReorderShufflableTransitionGroups(orders);
            }

#if DEBUG
            this.VALIDATE_SHUFFLABLE_GROUPS();
#endif
        }

        /// <summary>
        /// Reorders the list of shufflable transition groups
        /// </summary>
        /// <param name="orders"></param>
        /// <remarks></remarks>
        private void ReorderShufflableTransitionGroups(TransitionOrderCollection orders)
        {
            //If there are less than two transition groups there is no reason to continue

            if (this.m_ShufflableTransitionGroups.Count <= 1)
            {
                return;
            }

            //Reset all transition group order values

            foreach (TransitionGroup tg in this.m_ShufflableTransitionGroups)
            {
                tg.Order = Constants.DEFAULT_TRANSITION_ORDER;
            }

            //Apply the new ordering from the order collection

            Debug.Assert(this.m_TransitionGroups.Count == this.m_ShufflableTransitionGroups.Count);

            foreach (TransitionOrder order in orders)
            {
                if (this.m_TransitionGroups.Contains(order.TransitionGroupId))
                {
                    Debug.Assert(this.m_ShufflableTransitionGroups.Contains(this.m_TransitionGroups[order.TransitionGroupId]));
                    this.m_TransitionGroups[order.TransitionGroupId].Order = order.Order;
                }
            }

            //Sort by the transition groups by the order value

            this.m_ShufflableTransitionGroups.Sort((TransitionGroup t1, TransitionGroup t2) =>
            {
                return (t1.Order.CompareTo(t2.Order));
            });

            //Find the number of times each order appears.  If it appears more than
            //once then shuffle the subset of transtion groups with this order.

            Dictionary<double, int> OrderCounts = new Dictionary<double, int>();

            foreach (TransitionOrder o in orders)
            {
                if (!OrderCounts.ContainsKey(o.Order))
                {
                    OrderCounts.Add(o.Order, 1);
                }
                else
                {
                    OrderCounts[o.Order] += 1;
                }
            }

            //If any order appears more than once then it is a subset
            //that we need to shuffle.  Note that there may be a subset
            //for the default order.

            foreach (double d in OrderCounts.Keys)
            {
                if (OrderCounts[d] > 1)
                {
                    ShuffleUtilities.ShuffleSubList(
                        this.m_ShufflableTransitionGroups, 
                        this.GetMinOrderIndex(d), 
                        this.GetMaxOrderIndex(d), 
                        this.m_RandomGenerator.Random);
                }
            }

            if (this.DefaultOrderHasSubset())
            {
                ShuffleUtilities.ShuffleSubList(
                    this.m_ShufflableTransitionGroups, 
                    this.GetMinOrderIndex(Constants.DEFAULT_TRANSITION_ORDER), 
                    this.GetMaxOrderIndex(Constants.DEFAULT_TRANSITION_ORDER), 
                    this.m_RandomGenerator.Random);
            }
        }

        private bool DefaultOrderHasSubset()
        {
            int c = 0;

            foreach (TransitionGroup tg in this.m_ShufflableTransitionGroups)
            {
                if (tg.Order == Constants.DEFAULT_TRANSITION_ORDER)
                {
                    c += 1;

                    if (c == 2)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private int GetMinOrderIndex(double order)
        {
            for (int Index = 0; Index < this.m_ShufflableTransitionGroups.Count; Index++)
            {
                TransitionGroup tg = this.m_ShufflableTransitionGroups[Index];

                if (tg.Order == order)
                {
                    return Index;
                }
            }

            throw new InvalidOperationException("Cannot find minimum transition order!");
        }

        private int GetMaxOrderIndex(double order)
        {
            for (int Index = this.m_ShufflableTransitionGroups.Count - 1; Index >= 0; Index--)
            {
                TransitionGroup tg = this.m_ShufflableTransitionGroups[Index];

                if (tg.Order == order)
                {
                    return Index;
                }
            }

            throw new InvalidOperationException("Cannot find maximum transition order!");
        }
    }
}
