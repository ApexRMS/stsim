// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using System.Diagnostics;

namespace SyncroSim.STSim
{
    internal class StateAttributeValueMap : STSimMapBase5<AttributeValueAgeBinCollection>
    {
        private Project m_Project;
        private STSimDistributionProvider m_DistributionProvider;

        internal StateAttributeValueMap(
            Scenario scenario, 
            STSimDistributionProvider provider,
            StateAttributeValueCollection items) : base(scenario)
        {
            this.m_Project = scenario.Project;
            this.m_DistributionProvider = provider;

            foreach (StateAttributeValue item in items)
            {
                this.AddAttributeValue(item);
            }
        }

        public double? GetAttributeValue(
            int stateAttributeTypeId, int stratumId, int? secondaryStratumId, int? tertiaryStratumId, 
            int stateClassId, int iteration, int timestep, int age)
        {
            AttributeValueAgeBinCollection AgeBins = this.GetItem(
                stateAttributeTypeId, stratumId, secondaryStratumId, tertiaryStratumId, 
                stateClassId, iteration, timestep);

            Debug.Assert(false);
            return null;
        }

        private void AddAttributeValue(StateAttributeValue item)
        {
            AttributeValueAgeBinCollection AgeBins = this.GetItemExact(
                item.StateAttributeTypeId, item.StratumId, item.SecondaryStratumId, item.TertiaryStratumId,
                item.StateClassId, item.Iteration, item.Timestep);

            if (AgeBins == null)
            {
                AgeBins = new AttributeValueAgeBinCollection(this.m_Project);

                this.AddItem(
                    item.StateAttributeTypeId, item.StratumId, item.SecondaryStratumId, item.TertiaryStratumId, 
                    item.StateClassId, item.Iteration, item.Timestep, AgeBins);
            }

            AttributeValueAgeBin Bin = AgeBins.GetAgeBin(item.MinimumAge, item.MaximumAge);
            Bin.AddReference(new AttributeValueReference(item.TSTGroupId, item.TSTMin, item.TSTMax, item));

            Debug.Assert(this.HasItems);
        }
    }
}
