// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Diagnostics;
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

        /// <summary>
        /// Returns an attribute value from the specified list where ages are not considered
        /// </summary>
        /// <param name="records"></param>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <param name="provider"></param>
        /// <returns>
        /// If there are no records in the list, NULL is returned.  But if there are records, 
        /// the list should have only ONE entry and its value will be returned.
        /// </returns>
        public static double? GetAttributeRecordValueNoAge(
            List<AttributeValueRecord> records, 
            int iteration, 
            int timestep, 
            STSimDistributionProvider provider)
        {
            if (records.Count == 0)
            {
                return null;
            }
            else
            {
                Debug.Assert(records.Count == 1);
                Debug.Assert(records[0].MinimumAge == 0);
                Debug.Assert(records[0].MaximumAge == int.MaxValue);

                STSimDistributionBase b = records[0].AttributeClass;
                b.Sample(iteration, timestep, provider, StochasticTime.DistributionFrequency.Always);

                return b.CurrentValue.Value;
            }
        }

        /// <summary>
        /// Returns an attribute value from the specified list based on the specified age.
        /// </summary>
        /// <param name="records"></param>
        /// <param name="iteration"></param>
        /// <param name="timestep"></param>
        /// <param name="provider"></param>
        /// <param name="age"></param>
        /// <returns>
        /// The record with the closest matching minimum age is returned if one is found.  But if the specified 
        /// age does not fall within the age ranges of any of the existing records then Null is returned.
        /// </returns>
        public static double? GetAttributeRecordValueByAge(
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

        /// <summary>
        /// Adds an attribute record to the specified list of records
        /// </summary>
        /// <param name="records"></param>
        /// <param name="minimumAge"></param>
        /// <param name="maximumAge"></param>
        /// <param name="value"></param>
        /// <remarks>
        /// If the minimum age or maximum age is specified then a new record is added to the list.  Otherwise
        /// the list should have only one record with no ages specified.
        /// </remarks>
        public static void AddAttributeRecord(
            List<AttributeValueRecord> records, 
            int? minimumAge, 
            int? maximumAge, 
            STSimDistributionBase attributeClass)
        {
            if (minimumAge.HasValue || maximumAge.HasValue)
            {
                int AgeMin = 0;
                int AgeMax = int.MaxValue;

                if (minimumAge.HasValue)
                {
                    AgeMin = minimumAge.Value;
                }

                if (maximumAge.HasValue)
                {
                    AgeMax = maximumAge.Value;
                }

                records.Add(new AttributeValueRecord(AgeMin, AgeMax, attributeClass));
            }
            else
            {
                if (records.Count == 0)
                {
                    records.Add(new AttributeValueRecord(0, int.MaxValue, attributeClass));
                }
                else
                {
                    records[0].AttributeClass = attributeClass;
                }

                Debug.Assert(records.Count == 1);
                Debug.Assert(records[0].MinimumAge == 0);
                Debug.Assert(records[0].MaximumAge == int.MaxValue);
            }
        }
    }
}
