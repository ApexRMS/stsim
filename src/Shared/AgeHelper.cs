// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System.Diagnostics;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    internal class AgeHelper
    {
        private bool m_IsEnabled;
        private int m_Frequency;
        private int m_Maximum;
        private const int AGE_KEY_DEFAULT = 0;
        private const int MAX_AGE_CLASSES = 1000;

        public AgeHelper(bool isEnabled, int frequency, int maximum)
        {
            if (isEnabled)
            {
                if (frequency <= 0)
                {
                    ExceptionUtils.ThrowArgumentException("The age reporting frequency must be greater than zero.");
                }

                if (maximum < frequency)
                {
                    ExceptionUtils.ThrowArgumentException("The maximum age cannot be less than the age reporting frequency.");
                }
            }

            this.m_IsEnabled = isEnabled;
            this.m_Frequency = frequency;
            this.m_Maximum = maximum;

            if ((this.m_Maximum / (double)this.m_Frequency) > MAX_AGE_CLASSES)
            {
                this.m_Maximum = (this.m_Frequency * MAX_AGE_CLASSES);
            }
        }

        /// <summary>
        /// Gets a key for the specified age
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        /// <remarks>
        /// If enabled then the key is the minimum age, otherwise it is the default.
        /// </remarks>
        public int GetKey(int age)
        {
            if (!this.m_IsEnabled)
            {
                return AGE_KEY_DEFAULT;
            }
            else
            {
                return this.GetAgeMinimum(age).Value;
            }
        }

        /// <summary>
        /// Gets the minimum age
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int? GetAgeMinimum(int age)
        {
            if (!this.m_IsEnabled)
            {
                return null;
            }
            else
            {
                if (age > this.m_Maximum)
                {
                    return ((this.m_Maximum / this.m_Frequency) * this.m_Frequency);
                }
                else
                {
                    return ((age / this.m_Frequency) * this.m_Frequency);
                }
            }
        }

        /// <summary>
        /// Gets the reporting age maximum for the specified age
        /// </summary>
        /// <param name="age"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public int? GetAgeMaximum(int age)
        {
            if (!this.m_IsEnabled)
            {
                return null;
            }
            else
            {
                int? a = this.GetAgeMinimum(age) + (this.m_Frequency - 1);

                if (a.Value >= this.m_Maximum)
                {
                    a = null;
                }

                return a;
            }
        }

        /// <summary>
        /// Gets an enumeration of age descriptors for the current configuration
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public IEnumerable<AgeDescriptor> GetAges()
        {
            int v = 0;
            List<AgeDescriptor> lst = new List<AgeDescriptor>();

            while (v <= this.m_Maximum)
            {
                int min = this.GetAgeMinimum(v).Value;
                int? max = this.GetAgeMaximum(v);

                lst.Add(new AgeDescriptor(min, max));
                v += this.m_Frequency;
            }

            Debug.Assert(lst.Count > 0);
            Debug.Assert(!(lst[lst.Count - 1].MaximumAge.HasValue));

            return lst;
        }
    }

    /// <summary>
    /// Age Descriptor
    /// </summary>
    /// <remarks></remarks>
    internal class AgeDescriptor
    {
        private int m_MinimumAge;
        private int? m_MaximumAge;

        public AgeDescriptor(int minimumAge, int? maximumAge)
        {
            this.m_MinimumAge = minimumAge;
            this.m_MaximumAge = maximumAge;
        }

        public int MinimumAge
        {
            get
            {
                return this.m_MinimumAge;
            }
            set
            {
                this.m_MinimumAge = value;
            }
        }

        public int? MaximumAge
        {
            get
            {
                return this.m_MaximumAge;
            }
            set
            {
                this.m_MaximumAge = value;
            }
        }
    }
}
