// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Diagnostics;

namespace SyncroSim.STSim
{
    class AttributeValueReference
    {
        private int m_TSTGroupId;
        private int m_TSTMin;
        private int m_TSTMax;
        private STSimDistributionBase m_ClassRef;

        public static int TST_GROUP_WILD = 0;
        public static int TST_VALUE_NULL = -1;

        public AttributeValueReference(
            int? tstGroupId, 
            int? tstMin, 
            int? tstMax, 
            STSimDistributionBase classRef)
        {
            if (!tstGroupId.HasValue && !tstMin.HasValue && !tstMax.HasValue)
            {
                this.m_TSTGroupId = AttributeValueReference.TST_VALUE_NULL;
                this.m_TSTMin = AttributeValueReference.TST_VALUE_NULL;
                this.m_TSTMax = AttributeValueReference.TST_VALUE_NULL;
            }
            else
            {
                this.m_TSTGroupId = tstGroupId.HasValue ? tstGroupId.Value : AttributeValueReference.TST_GROUP_WILD;
                this.m_TSTMin = tstMin.HasValue ? tstMin.Value : 0;
                this.m_TSTMax = tstMax.HasValue ? tstMax.Value : int.MaxValue;
            }

            this.m_ClassRef = classRef;
            Debug.Assert(this.m_TSTMin <= this.m_TSTMax);
        }

        public int TSTGroupId
        {
            get
            {
                return this.m_TSTGroupId;
            }
        }

        public int TSTMin
        {
            get
            {
                return this.m_TSTMin;
            }
        }

        public int TSTMax
        {
            get
            {
                return this.m_TSTMax;
            }
        }

        public STSimDistributionBase ClassRef
        {
            get
            {
                return this.m_ClassRef;
            }
        }
    }
}
