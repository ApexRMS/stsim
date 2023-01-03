// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.StochasticTime;

namespace SyncroSim.STSim
{
    public class STSimDistributionValue : DistributionValue
    {
        private int? m_StratumId;
        private int? m_SecondaryStratumId;
        private int? m_TertiaryStratumId;

        public STSimDistributionValue(
            int? iteration, int? timestep, int? stratumId, int? secondaryStratumId, int? tertiaryStratumId, int distributionTypeId, 
            int? externalVariableTypeId, double? externalVariableMin, double? externalVariableMax, double? value, 
            int? valueDistributionTypeId, DistributionFrequency? valueDistributionFrequency, double? valueDistributionSD, 
            double? valueDistributionMin, double? valueDistributionMax, double? valueDistributionRelativeFrequency) : 
            base(iteration, timestep, distributionTypeId, externalVariableTypeId, externalVariableMin, 
                externalVariableMax, value, valueDistributionTypeId, valueDistributionFrequency, valueDistributionSD, 
                valueDistributionMin, valueDistributionMax, valueDistributionRelativeFrequency)
        {
            this.m_StratumId = stratumId;
            this.m_SecondaryStratumId = secondaryStratumId;
            this.m_TertiaryStratumId = tertiaryStratumId;
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
    }
}
