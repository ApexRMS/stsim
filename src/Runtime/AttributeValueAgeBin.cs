// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

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

        public int AgeMin
        {
            get
            {
                return this.m_AgeMin;
            }
        }

        public int AgeMax
        {
            get
            {
                return this.m_AgeMax;
            }
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

        public AttributeValueReference GetReference(TstCollection cellTst)
        {
            AttributeValueReference AttrRef = this.GetReferenceWithTST(cellTst);

            if (AttrRef == null)
            {
                AttrRef = this.GetReferenceWithoutTST();
            }
    
            return AttrRef;          
        }

        private AttributeValueReference GetReferenceWithTST(TstCollection cellTst)
        {
            if (this.m_RefsWithTST.Count == 0 || cellTst.Count == 0)
            {
                return null;
            }

            Tst tst = this.GetTstWithSmallestValue(cellTst);

            if (!this.m_TSTGroupHint.ContainsKey(tst.TransitionGroupId) && 
                !this.m_TSTGroupHint.ContainsKey(AttributeValueReference.TST_GROUP_WILD))
            {
                return null;
            }

            AttributeValueReference FinalRef = null;

            foreach (AttributeValueReference attrRef in this.m_RefsWithTST)
            {
                if (attrRef.TSTGroupId != tst.TransitionGroupId && 
                    attrRef.TSTGroupId != AttributeValueReference.TST_GROUP_WILD)
                {
                    continue;
                }

                if (tst.TstValue >= attrRef.TSTMin && tst.TstValue <= attrRef.TSTMax)
                {
                    if (FinalRef == null)
                    {
                        FinalRef = attrRef;
                        continue;
                    }

                    if (attrRef.TSTMin > FinalRef.TSTMin)
                    {
                        FinalRef = attrRef;
                    }
                }
            }

            return FinalRef;
        }

        private AttributeValueReference GetReferenceWithoutTST()
        {
            Debug.Assert(this.m_RefsWithoutTST.Count <= 1);

            if (this.m_RefsWithoutTST.Count == 0)
            {
                return null;
            }

            return this.m_RefsWithoutTST[0];
        }

        private Tst GetTstWithSmallestValue(TstCollection cellTst)
        {
            Tst Smallest = null;

            foreach (Tst tst in cellTst)
            {
                if (this.m_TSTGroupHint.ContainsKey(tst.TransitionGroupId) ||
                    this.m_TSTGroupHint.ContainsKey(AttributeValueReference.TST_GROUP_WILD))
                {
                    if (Smallest == null)
                    {
                        Smallest = tst;
                    }
                    else
                    {
                        if (tst.TstValue < Smallest.TstValue)
                        {
                            Smallest = tst;
                        }
                    }
                }
            }

            return Smallest;
        }

        private void AddReferenceWithTST(AttributeValueReference attrRef)
        {
            Debug.Assert(attrRef.TSTGroupId != AttributeValueReference.TST_VALUE_NULL);
            Debug.Assert(attrRef.TSTMin != AttributeValueReference.TST_VALUE_NULL);
            Debug.Assert(attrRef.TSTMax != AttributeValueReference.TST_VALUE_NULL);

            string k = CreateRefKey(attrRef.TSTGroupId, attrRef.TSTMin, attrRef.TSTMax);

            if (this.m_RefWithTSTSeenBefore.ContainsKey(k))
            {
                throw new STSimMapDuplicateItemException("A duplicate attribute value has been created.");
            }

            this.m_RefsWithTST.Add(attrRef);
            this.m_RefWithTSTSeenBefore.Add(k, true);

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
                throw new STSimMapDuplicateItemException("A duplicate attribute value has been created.");
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
