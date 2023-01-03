// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

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
