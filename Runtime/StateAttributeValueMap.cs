// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Core;
using System.Diagnostics;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    internal class StateAttributeValueMap : STSimMapBase5<List<AttributeValueRecord>>
    {
        internal StateAttributeValueMap(Scenario scenario, StateAttributeValueCollection items) : base(scenario)
        {

            foreach (StateAttributeValue item in items)
            {
                this.AddAttributeValue(item);
            }
        }

        public double? GetAttributeValueNoAge(
            int stateAttributeTypeId, int stratumId, int? secondaryStratumId, int? tertiaryStratumId, 
            int stateClassId, int iteration, int timestep)
        {
            List<AttributeValueRecord> cm = this.GetItem(
                stateAttributeTypeId, stratumId, secondaryStratumId, tertiaryStratumId, 
                stateClassId, iteration, timestep);

            if (cm != null)
            {
                return AttributeValueRecord.GetAttributeRecordValueNoAge(cm);
            }
            else
            {
                return null;
            }
        }

        public double? GetAttributeValueByAge(
            int stateAttributeTypeId, int stratumId, int? secondaryStratumId, int? tertiaryStratumId, 
            int stateClassId, int iteration, int timestep, int age)
        {
            List<AttributeValueRecord> cm = this.GetItem(
                stateAttributeTypeId, stratumId, secondaryStratumId, tertiaryStratumId, 
                stateClassId, iteration, timestep);

            if (cm != null)
            {
                return AttributeValueRecord.GetAttributeRecordValueByAge(cm, age);
            }
            else
            {
                return null;
            }
        }

        private void AddAttributeValue(StateAttributeValue item)
        {
            List<AttributeValueRecord> l = this.GetItemExact(
                item.AttributeTypeId, item.StratumId, item.SecondaryStratumId, item.TertiaryStratumId,
                item.StateClassId, item.Iteration, item.Timestep);

            if (l == null)
            {
                l = new List<AttributeValueRecord>();

                this.AddItem(
                    item.AttributeTypeId, item.StratumId, item.SecondaryStratumId, item.TertiaryStratumId, 
                    item.StateClassId, item.Iteration, item.Timestep, l);
            }

            AttributeValueRecord.AddAttributeRecord(l, item.MinimumAge, item.MaximumAge, item.Value);
            Debug.Assert(this.HasItems);
        }
    }
}
