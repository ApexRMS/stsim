// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Diagnostics;
using SyncroSim.Core;

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
            int stateClassId, int iteration, int timestep, int age, TstCollection cellTst)
        {
            AttributeValueAgeBinCollection AgeBins = this.GetItem(
                stateAttributeTypeId, stratumId, secondaryStratumId, tertiaryStratumId, 
                stateClassId, iteration, timestep);

            if (AgeBins == null)
            {
                return null;
            }

            AttributeValueAgeBin Bin = AgeBins.GetAgeBin(age);
            
            if (Bin == null)
            {
                return null;
            }

            AttributeValueReference AttrRef = Bin.GetReference(cellTst);

            if (AttrRef == null)
            {
                return null;
            }

            STSimDistributionBase b = AttrRef.ClassRef;
            b.Sample(iteration, timestep, this.m_DistributionProvider, StochasticTime.DistributionFrequency.Always);

            return b.CurrentValue.Value;
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

            try
            {
                AttributeValueAgeBin Bin = AgeBins.GetOrCreateAgeBin(item.MinimumAge, item.MaximumAge);
                Bin.AddReference(new AttributeValueReference(item.TSTGroupId, item.TSTMin, item.TSTMax, item));
            }
            catch (STSimMapDuplicateItemException)
            {
                string template = "A duplicate State Attribute value has been created. More information:" +
                    Environment.NewLine + "State Attribute={0}, State Class={1}, {2}={3}, {4}={5}, {6}={7}, MinAge={8}, MaxAge={9}, TSTMin={10}, TSTMax={11}, TSTGroup={12}, Iteration={13}, Timestep={14}." + 
                    Environment.NewLine + "NOTE: A user defined distribution can result in additional State Attributes when the model is run.";

                ExceptionUtils.ThrowArgumentException(template, 
                    this.GetStateAttributeTypeName(item.StateAttributeTypeId), 
                    this.GetStateClassName(item.StateClassId),
                    this.PrimaryStratumLabel, 
                    this.GetStratumName(item.StratumId), 
                    this.SecondaryStratumLabel, 
                    this.GetSecondaryStratumName(item.SecondaryStratumId), 
                    this.TertiaryStratumLabel, 
                    this.GetTertiaryStratumName(item.TertiaryStratumId),
                    item.MinimumAge,
                    item.MaximumAge == int.MaxValue ? "NULL" : item.MaximumAge.ToString(),
                    STSimMapBase.FormatValue(item.TSTMin),
                    (!item.TSTMax.HasValue || item.TSTMax.Value == int.MaxValue) ? "NULL" : item.TSTMax.ToString(),
                    this.GetTSTGroupString(item.TSTGroupId),
                    STSimMapBase.FormatValue(item.Iteration), 
                    STSimMapBase.FormatValue(item.Timestep));
            }

            Debug.Assert(this.HasItems);
        }
    }
}
