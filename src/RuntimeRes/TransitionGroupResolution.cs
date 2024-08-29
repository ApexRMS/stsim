// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    class TransitionGroupResolution
    {
        private int m_Id;
        private Resolution m_Resolution;
        private double m_FFBThresholdProportion;

        public TransitionGroupResolution(int id, Resolution resolution, double ffbThresholdProportion)
        {
            this.m_Id = id;
            this.m_Resolution = resolution;
            this.m_FFBThresholdProportion = ffbThresholdProportion;
        }

        public int Id
        {
            get
            {
                return this.m_Id;
            }
        }

        public Resolution Resolution
        {
            get
            {
                return this.m_Resolution;
            }
        }

        public double FFBThresholdProportion
        {
            get
            {
                return this.m_FFBThresholdProportion;
            }
        }
    }
}
