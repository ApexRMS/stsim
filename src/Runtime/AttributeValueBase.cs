// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.StochasticTime;

namespace SyncroSim.STSim
{
    internal class AttributeValueBase :  STSimDistributionBase
    {
        private int m_AttributeTypeId;
        private int? m_StateClassId;
        private int? m_MinimumAge;
        private int? m_MaximumAge;

        public AttributeValueBase(
            int attributeTypeId,
            int? stratumId, 
            int? secondaryStratumId, 
            int? tertiaryStratumId, 
            int? iteration, 
            int? timestep, 
            int? stateClassId, 
            int? minimumAge, 
            int? maximumAge, 
            double? value, 
            int? distributionTypeId, 
            DistributionFrequency? distributionFrequency,
            double? distributionSD, 
            double? distributionMin, 
            double? distributionMax) : base(
                iteration, timestep, stratumId, secondaryStratumId, tertiaryStratumId,
                value, 
                distributionTypeId, distributionFrequency, distributionSD, distributionMin, distributionMax)
        {
            this.m_AttributeTypeId = attributeTypeId;
            this.m_StateClassId = stateClassId;
            this.m_MinimumAge = minimumAge;
            this.m_MaximumAge = maximumAge;
        }

        public int AttributeTypeId
        {
            get
            {
                return this.m_AttributeTypeId;
            }
        }

        public int? StateClassId
        {
            get
            {
                return this.m_StateClassId;
            }
        }

        public int? MinimumAge
        {
            get
            {
                return this.m_MinimumAge;
            }
        }

        public int? MaximumAge
        {
            get
            {
                return this.m_MaximumAge;
            }
        }

        public override STSimDistributionBase Clone()
        {
            throw new NotImplementedException("You must implement the Clone function.");
        }
    }
}
