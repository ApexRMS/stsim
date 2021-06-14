// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2021 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    class AttributeValueAgeBinCollection : Collection<AttributeValueAgeBin>
    {
        private Project m_Project;
        private List<AttributeValueAgeBin> m_Bins = new List<AttributeValueAgeBin>();
        private Dictionary<string, AttributeValueAgeBin> m_Hint = new Dictionary<string, AttributeValueAgeBin>();

        public AttributeValueAgeBinCollection(Project project)
        {
            this.m_Project = project;
        }

        public AttributeValueAgeBin GetAgeBin(int age)
        {
            Debug.Assert(this.m_Bins.Count == this.m_Hint.Count);

            if (this.m_Bins.Count == 0)
            {
                return null;
            }

            AttributeValueAgeBin FinalBin = null;

            foreach (AttributeValueAgeBin bin in this.m_Bins)
            {
                if (age >= bin.AgeMin && age <= bin.AgeMax)
                {
                    if (FinalBin == null)
                    {
                        FinalBin = bin;
                        continue;
                    }

                    if (bin.AgeMin > FinalBin.AgeMin)
                    {
                        FinalBin = bin;
                    }
                }
            }

            return FinalBin;
        }

        public AttributeValueAgeBin GetOrCreateAgeBin(int ageMin, int ageMax)
        {
            string k = CreateBinKey(ageMin, ageMax);

            if (!this.m_Hint.ContainsKey(k))
            {
                AttributeValueAgeBin Bin = new AttributeValueAgeBin(ageMin, ageMax, this.m_Project);

                this.m_Bins.Add(Bin);
                this.m_Hint.Add(k, Bin);
            }

            Debug.Assert(this.m_Bins.Count == this.m_Hint.Count);
            return this.m_Hint[k];
        }

        private static string CreateBinKey(int ageMin, int ageMax)
        {
            return string.Format(CultureInfo.InvariantCulture, 
                "{0}-{1}", 
                ageMin, 
                ageMax);
        }
    }
}
