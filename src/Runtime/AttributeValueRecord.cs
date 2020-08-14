// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Collections.Generic;

namespace SyncroSim.STSim
{
    /// <summary>
    /// Attribute Value Record
    /// </summary>
    /// <remarks>
    /// This class is shared by both state and transition attribute maps so
    /// this.m_Value can be either a StateAttributeValue or a TransitionAttributeValue
    /// </remarks>
    internal class AttributeValueRecord
    {
        private int m_MinimumAge;
        private int m_MaximumAge;     
        private STSimDistributionBase m_AttributeClass;

        public AttributeValueRecord(
            int minimumAge, 
            int maximumAge, 
            STSimDistributionBase attributeClass)
        {
            this.m_MinimumAge = minimumAge;
            this.m_MaximumAge = maximumAge;
            this.m_AttributeClass = attributeClass;
        }

        public int MinimumAge
        {
            get
            {
                return m_MinimumAge;
            }
        }

        public int MaximumAge
        {
            get
            {
                return m_MaximumAge;
            }
        }

        public STSimDistributionBase AttributeClass
        {
            get
            {
                return this.m_AttributeClass;
            }
            set
            {
                this.m_AttributeClass = value;
            }
        }

        public static double? GetAttributeRecordValue(
            List<AttributeValueRecord> records,
            int iteration,
            int timestep,
            STSimDistributionProvider provider,
            int age)
        {
            if (records.Count == 0)
            {
                return null;
            }

            AttributeValueRecord FinalRecord = null;

            foreach (AttributeValueRecord record in records)
            {
                if (age >= record.MinimumAge && age <= record.MaximumAge)
                {
                    if (FinalRecord == null)
                    {
                        FinalRecord = record;
                        continue;
                    }

                    if (record.MinimumAge > FinalRecord.MinimumAge)
                    {
                        FinalRecord = record;
                    }
                }
            }

            if (FinalRecord != null)
            {
                STSimDistributionBase b = FinalRecord.AttributeClass;
                b.Sample(iteration, timestep, provider, StochasticTime.DistributionFrequency.Always);

                return FinalRecord.AttributeClass.CurrentValue.Value;
            }
            else
            {
                return null;
            }
        }

        public static void AddAttributeRecord(
            List<AttributeValueRecord> records, 
            int minimumAge, 
            int maximumAge, 
            STSimDistributionBase attributeClass)
        {
            records.Add(new AttributeValueRecord(minimumAge, maximumAge, attributeClass));
        }
    }
}
