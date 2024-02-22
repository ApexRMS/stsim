// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal class TransitionOrder
    {
        private int m_TransitionGroupId;
        private int? m_Iteration;
        private int? m_Timestep;
        private double m_Order = Constants.DEFAULT_TRANSITION_ORDER;

        public TransitionOrder(int transitionGroupId, int? iteration, int? timestep, double? order)
        {
            this.m_TransitionGroupId = transitionGroupId;
            this.m_Iteration = iteration;
            this.m_Timestep = timestep;

            if (order.HasValue)
            {
                this.m_Order = order.Value;
            }
        }

        public int TransitionGroupId
        {
            get
            {
                return this.m_TransitionGroupId;
            }
        }

        public int? Iteration
        {
            get
            {
                return this.m_Iteration;
            }
        }

        public int? Timestep
        {
            get
            {
                return this.m_Timestep;
            }
        }

        public double Order
        {
            get
            {
                return this.m_Order;
            }
        }
    }
}
