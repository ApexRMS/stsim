// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    class AttributeValueAgeBin
    {
        private int m_AgeMin;
        private int m_AgeMax;
        private Project m_Project;
        private Dictionary<int, bool> m_TSTGroupHint = new Dictionary<int, bool>();
        private Dictionary<string, bool> m_RefWithTSTSeenBefore = new Dictionary<string, bool>();
        private AttributeValueReferenceCollection m_RefsWithTST = new AttributeValueReferenceCollection();
        private AttributeValueReferenceCollection m_RefsWithoutTST = new AttributeValueReferenceCollection();

        public AttributeValueAgeBin(int ageMin, int ageMax, Project project)
        {
            this.m_AgeMin = ageMin;
            this.m_AgeMax = ageMax;
            this.m_Project = project;

            Debug.Assert(this.m_AgeMin <= this.m_AgeMax);
        }

        public void AddReference(AttributeValueReference attrRef)
        {
            if (attrRef.TSTGroupId == AttributeValueReference.TST_VALUE_NULL)
            {
                this.AddReferenceWithoutTST(attrRef);
            }
            else
            {
                this.AddReferenceWithTST(attrRef);
            }

            Debug.Assert(!this.m_TSTGroupHint.ContainsKey(AttributeValueReference.TST_VALUE_NULL));
        }

        private void AddReferenceWithTST(AttributeValueReference attrRef)
        {
            Debug.Assert(attrRef.TSTGroupId != AttributeValueReference.TST_VALUE_NULL);
            Debug.Assert(attrRef.TSTMin != AttributeValueReference.TST_VALUE_NULL);
            Debug.Assert(attrRef.TSTMax != AttributeValueReference.TST_VALUE_NULL);

            string k = CreateRefKey(attrRef.TSTGroupId, attrRef.TSTMin, attrRef.TSTMax);

            if (this.m_RefWithTSTSeenBefore.ContainsKey(k))
            {
                string s = string.Format(CultureInfo.InvariantCulture, 
                    "An attribute with the following TST values has already been added to the age bin {0}-{1}:",
                    this.m_AgeMin, this.m_AgeMax);

                s += Environment.NewLine;
                s += Environment.NewLine;

                string TGName = this.GetTransitionGroupName(attrRef.TSTGroupId);

                s += string.Format(CultureInfo.InvariantCulture, "TST Transition Group: " + TGName);
                s += Environment.NewLine;
                s += string.Format(CultureInfo.InvariantCulture, "TST Min: {0}", attrRef.TSTMin);
                s += Environment.NewLine;
                s += string.Format(CultureInfo.InvariantCulture, "TST Max: {0}", attrRef.TSTMax);
            }

            this.m_RefsWithTST.Add(attrRef);

            if (!this.m_TSTGroupHint.ContainsKey(attrRef.TSTGroupId))
            {
                this.m_TSTGroupHint.Add(attrRef.TSTGroupId, true);
            }
        }

        private void AddReferenceWithoutTST(AttributeValueReference attrRef)
        {
            Debug.Assert(attrRef.TSTGroupId == AttributeValueReference.TST_VALUE_NULL);
            Debug.Assert(attrRef.TSTMin == AttributeValueReference.TST_VALUE_NULL);
            Debug.Assert(attrRef.TSTMax == AttributeValueReference.TST_VALUE_NULL);
            Debug.Assert(this.m_RefsWithoutTST.Count <= 1);

            if (this.m_RefsWithoutTST.Count == 1)
            {
                ExceptionUtils.ThrowArgumentException(
                    "An attribute with no TST values has already been added to the age bin {0}-{1}",
                    this.m_AgeMin, this.m_AgeMax);
            }

            this.m_RefsWithoutTST.Add(attrRef);
        }

        private static string CreateRefKey(int tstGroupId, int tstMin, int tstMax)
        {
            return string.Format(CultureInfo.InvariantCulture,
                "{0}-{1}-{2}",
                tstGroupId,
                tstMin,
                tstMax);
        }

        private string GetTransitionGroupName(int id)
        {
            return this.GetProjectItemName(
                Strings.DATASHEET_TRANSITION_GROUP_NAME, 
                id);
        }

        private string GetProjectItemName(string dataSheetName, int id)
        {
            Debug.Assert(id != AttributeValueReference.TST_VALUE_NULL);

            if (id == AttributeValueReference.TST_GROUP_WILD)
            {
                return "NULL";
            }
            else
            {
                DataSheet ds = this.m_Project.GetDataSheet(dataSheetName);
                return ds.ValidationTable.GetDisplayName(id);
            }
        }
    }
}
