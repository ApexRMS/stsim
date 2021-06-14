// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Diagnostics;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    internal class ClassBinHelper
    {
        private bool m_IsEnabled;
        private int m_Frequency;
        private int m_Maximum;
        private const int DEFAULT_KEY = 0;
        private const int MAX_CLASSES = 1000;

        public ClassBinHelper(bool isEnabled, int frequency, int maximum)
        {
            if (isEnabled)
            {
                if (frequency <= 0)
                {
                    ExceptionUtils.ThrowArgumentException(
                        "The frequency must be greater than zero.");
                }

                if (maximum < frequency)
                {
                    ExceptionUtils.ThrowArgumentException(
                        "The maximum cannot be less than the frequency.");
                }
            }

            this.m_IsEnabled = isEnabled;
            this.m_Frequency = frequency;
            this.m_Maximum = maximum;

            if ((this.m_Maximum / (double)this.m_Frequency) > MAX_CLASSES)
            {
                this.m_Maximum = (this.m_Frequency * MAX_CLASSES);
            }
        }

        public int GetKey(int value)
        {
            if (!this.m_IsEnabled)
            {
                return DEFAULT_KEY;
            }
            else
            {
                return this.GetMinimum(value).Value;
            }
        }

        public int? GetMinimum(int value)
        {
            if (!this.m_IsEnabled)
            {
                return null;
            }
            else
            {
                if (value == 0)
                {
                    return 0;
                }
                else if (value > this.m_Maximum)
                {
                    return this.m_Maximum + 1;
                }
                else
                {
                    return ((value-1) / this.m_Frequency) * this.m_Frequency + 1;
                }
            }
        }

        public int? GetMaximum(int value)
        {
            if (!this.m_IsEnabled)
            {
                return null;
            }
            else
            {
                if (value == 0)
                {
                    return 0;
                }
                else
                {
                    int? a = this.GetMinimum(value) + (this.m_Frequency - 1);

                    if (a.Value > this.m_Maximum)
                    {
                        a = null;
                    }

                    return a;
                }
            }
        }

        public List<ClassBinDescriptor> GetDescriptors()
        {
            List<ClassBinDescriptor> lst = new List<ClassBinDescriptor>();

            //Add a default 0 bin
            lst.Add(new ClassBinDescriptor(0, 0));

            int min = 1;
            int max = 0;

            while (min <= this.m_Maximum)
            {
                max = min + this.m_Frequency - 1;

                if (max > this.m_Maximum)
                {
                    max = this.m_Maximum;
                }

                Debug.Assert(min <= max);

                lst.Add(new ClassBinDescriptor(min, max));
                min = max + 1;
            }

#if DEBUG
            foreach (ClassBinDescriptor d in lst)
            {
                Debug.Assert(d.Minimum <= this.m_Maximum);

                if (d.Maximum.HasValue)
                {
                    Debug.Assert(d.Maximum.Value <= this.m_Maximum);
                }
            }
#endif

            //Add a final max+1 catch-all bin
            lst.Add(new ClassBinDescriptor(this.m_Maximum + 1, null));

            Debug.Assert(lst.Count > 0);
            Debug.Assert(!(lst[lst.Count - 1].Maximum.HasValue));

            return lst;
        }
    } 
}
