// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal class AttributeValueBase
    {
        private int m_AttributeTypeId;
        private int? m_StratumId;
        private int? m_SecondaryStratumId;
        private int? m_TertiaryStratumId;
        private int? m_Iteration;
        private int? m_Timestep;
        private int? m_StateClassId;
        private int? m_MinimumAge;
        private int? m_MaximumAge;
        private double m_Value;

        public AttributeValueBase(
            int attributeTypeId, int? stratumId, int? secondaryStratumId, int? tertiaryStratumId, 
            int? iteration, int? timestep, int? stateClassId, int? minimumAge, int? maximumAge, double value)
        {
            this.m_AttributeTypeId = attributeTypeId;
            this.m_StratumId = stratumId;
            this.m_SecondaryStratumId = secondaryStratumId;
            this.m_TertiaryStratumId = tertiaryStratumId;
            this.m_Iteration = iteration;
            this.m_Timestep = timestep;
            this.m_StateClassId = stateClassId;
            this.m_MinimumAge = minimumAge;
            this.m_MaximumAge = maximumAge;
            this.m_Value = value;
        }

        public int AttributeTypeId
        {
            get
            {
                return this.m_AttributeTypeId;
            }
        }

        public int? StratumId
        {
            get
            {
                return this.m_StratumId;
            }
        }

        public int? SecondaryStratumId
        {
            get
            {
                return this.m_SecondaryStratumId;
            }
        }

        public int? TertiaryStratumId
        {
            get
            {
                return this.m_TertiaryStratumId;
            }
        }

        public int? Iteration
        {
            get
            {
                return this.m_Iteration;
            }
        }

        public int? Timestep
        {
            get
            {
                return this.m_Timestep;
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

        public double Value
        {
            get
            {
                return this.m_Value;
            }
        }
    }
}
