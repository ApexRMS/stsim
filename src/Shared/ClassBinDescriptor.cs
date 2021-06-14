// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Globalization;

namespace SyncroSim.STSim
{
    internal class ClassBinDescriptor
    {
        private int m_Minimum;
        private int? m_Maximum;
        private string m_DisplayName;

        public ClassBinDescriptor(int minimum, int? maximum)
        {
            this.m_Minimum = minimum;
            this.m_Maximum = maximum;

            this.UpdateDisplayName();
        }

        public override string ToString()
        {
            return this.m_DisplayName;
        }

        private void UpdateDisplayName()
        {
            if (this.m_Maximum.HasValue)
            {
                this.m_DisplayName = string.Format(CultureInfo.InvariantCulture,
                    "{0}-{1}", this.m_Minimum, this.m_Maximum);
            }
            else
            {
                this.m_DisplayName = string.Format(CultureInfo.InvariantCulture,
                    "{0}-{1}", this.m_Minimum, "NULL");
            }
        }

        public int Minimum
        {
            get
            {
                return this.m_Minimum;
            }
            set
            {
                this.m_Minimum = value;
                this.UpdateDisplayName();
            }
        }

        public int? Maximum
        {
            get
            {
                return this.m_Maximum;
            }
            set
            {
                this.m_Maximum = value;
                this.UpdateDisplayName();
            }
        }
    }
}
