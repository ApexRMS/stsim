// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System.Diagnostics;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    /// <summary>
    /// Attribute Value Record
    /// </summary>
    /// <remarks>
    /// This class is shared by all existing attribute maps.  Members are public for the fastest 
    /// possible access, and the constructor is there for convenience.
    /// </remarks>
    internal class AttributeValueRecord
    {
        public int MinimumAge;
        public int MaximumAge;
        public double Value;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="minimumAge"></param>
        /// <param name="maximumAge"></param>
        /// <param name="value"></param>
        /// <remarks></remarks>
        public AttributeValueRecord(int minimumAge, int maximumAge, double value)
        {
            this.MinimumAge = minimumAge;
            this.MaximumAge = maximumAge;
            this.Value = value;
        }

        /// <summary>
        /// Returns an attribute value record from the specified list where ages are not considered
        /// </summary>
        /// <param name="records"></param>
        /// <remarks>
        /// If there are no records in the list, Null is returned.  But if there are records, the list should have only
        /// one entry and its value will be returned.
        /// </remarks>
        public static double? GetAttributeRecordValueNoAge(List<AttributeValueRecord> records)
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

                return records[0].Value;
            }
        }

        /// <summary>
        /// Returns an attribute value record from the specified list based on the specified age.
        /// </summary>
        /// <param name="records"></param>
        /// <param name="age"></param>
        /// <returns></returns>
        /// <remarks>
        /// The record with the closest matching minimum age is returned if one is found.  But if the specified age
        /// does not fall within the age ranges of any of the existing records then Null is returned.
        /// </remarks>
        public static double? GetAttributeRecordValueByAge(List<AttributeValueRecord> records, int age)
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
                return FinalRecord.Value;
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
        public static void AddAttributeRecord(List<AttributeValueRecord> records, int? minimumAge, int? maximumAge, double? value)
        {
            double FinalValue = 1.0;

            if (value.HasValue)
            {
                FinalValue = value.Value;
            }

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

                records.Add(new AttributeValueRecord(AgeMin, AgeMax, FinalValue));
            }
            else
            {
                if (records.Count == 0)
                {
                    records.Add(new AttributeValueRecord(0, int.MaxValue, FinalValue));
                }
                else
                {
                    records[0].Value = FinalValue;
                }

                Debug.Assert(records.Count == 1);
                Debug.Assert(records[0].MinimumAge == 0);
                Debug.Assert(records[0].MaximumAge == int.MaxValue);
            }
        }
    }
}
