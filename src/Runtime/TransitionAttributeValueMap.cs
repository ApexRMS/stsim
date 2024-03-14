// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using SyncroSim.Core;
using System.Diagnostics;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    internal class TransitionAttributeValueMap : STSimMapBase6<AttributeValueAgeBinCollection>
    {
        private Project m_Project;
        private STSimDistributionProvider m_DistributionProvider;

        private Dictionary<int, Dictionary<int, bool>> m_TypeGroupMap = 
            new Dictionary<int, Dictionary<int, bool>>();

        public TransitionAttributeValueMap(
            Scenario scenario, 
            STSimDistributionProvider provider,
            TransitionAttributeValueCollection transitionAttributes) : base(scenario)
        {
            this.m_Project = scenario.Project;
            this.m_DistributionProvider = provider;

            foreach (TransitionAttributeValue ta in transitionAttributes)
            {
                this.AddAttributeValue(ta);
            }
        }

        public Dictionary<int, Dictionary<int, bool>> TypeGroupMap
        {
            get
            {
                return this.m_TypeGroupMap;
            }
        }

        public double? GetAttributeValue(
            int transitionAttributeTypeId, int transitionGroupId, 
            int stratumId, int? secondaryStratumId, int? tertiaryStratumId, 
            int stateClassId, int iteration, int timestep, int age, TstCollection cellTst)
        {
            AttributeValueAgeBinCollection AgeBins = this.GetItem(
                transitionAttributeTypeId, transitionGroupId, 
                stratumId, secondaryStratumId, tertiaryStratumId, 
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
            b.Sample(iteration, timestep, this.m_DistributionProvider, Core.DistributionFrequency.Always);

            return b.CurrentValue.Value;
        }

        private void AddAttributeValue(TransitionAttributeValue item)
        {
            AttributeValueAgeBinCollection AgeBins = this.GetItemExact(
                item.TransitionAttributeTypeId, item.TransitionGroupId, 
                item.StratumId, item.SecondaryStratumId, item.TertiaryStratumId, 
                item.StateClassId, item.Iteration, item.Timestep);

            if (AgeBins == null)
            {
                AgeBins = new AttributeValueAgeBinCollection(this.m_Project);

                this.AddItem(
                    item.TransitionAttributeTypeId, item.TransitionGroupId, 
                    item.StratumId, item.SecondaryStratumId, item.TertiaryStratumId, 
                    item.StateClassId, item.Iteration, item.Timestep, AgeBins);
            }

            try
            {
                AttributeValueAgeBin Bin = AgeBins.GetOrCreateAgeBin(item.MinimumAge, item.MaximumAge);
                Bin.AddReference(new AttributeValueReference(item.TSTGroupId, item.TSTMin, item.TSTMax, item));
            }
            catch (STSimMapDuplicateItemException)
            {
                string template = "A duplicate Transition Attribute value has been created. More information:" +
                    Environment.NewLine + "Transition Attribute={0}, Transition Group={1}, {2}={3}, {4}={5}, {6}={7}, MinAge={8}, MaxAge={9}, TSTMin={10}, TSTMax={11}, TSTGroup={12}, Iteration={13}, Timestep={14}." +
                    Environment.NewLine + "NOTE: A user defined distribution can result in additional Transition Attributes when the model is run.";

                ExceptionUtils.ThrowArgumentException(template,
                    this.GetTransitionAttributeTypeName(item.TransitionAttributeTypeId),
                    this.GetTransitionGroupName(item.TransitionGroupId),
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

            if (!this.m_TypeGroupMap.ContainsKey(item.TransitionGroupId))
            {
                this.m_TypeGroupMap.Add(item.TransitionGroupId, new Dictionary<int, bool>());
            }

            Dictionary<int, bool> d = TypeGroupMap[item.TransitionGroupId];

            if (!d.ContainsKey(item.TransitionAttributeTypeId))
            {
                d.Add(item.TransitionAttributeTypeId, true);
            }

            Debug.Assert(this.HasItems);
        }
    }
}
