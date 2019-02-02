// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal class BaseValueDisplayListItem
    {
        private int m_Value;
        private string m_Display;

        public BaseValueDisplayListItem(int value, string display)
        {
            this.m_Value = value;
            this.m_Display = display;
        }

        public int Value
        {
            get
            {
                return this.m_Value;
            }
        }

        public string Display
        {
            get
            {
                return this.m_Display;
            }
        }

        public override string ToString()
        {
            return this.m_Display;
        }
    }
}
