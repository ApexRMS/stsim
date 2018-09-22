// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal class TransitionEvent
    {
        private double m_targetAmount;

        public TransitionEvent(double targetAmount)
        {
            this.m_targetAmount = targetAmount;
        }

        public double TargetAmount
        {
            get
            {
                return this.m_targetAmount;
            }
        }
    }
}
