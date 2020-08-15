// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Globalization;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    class AttributeValueAgeBinCollection : Collection<AttributeValueAgeBin>
    {
        private Project m_Project;

        private Dictionary<string, AttributeValueAgeBin> m_Hint = 
            new Dictionary<string, AttributeValueAgeBin>();

        public AttributeValueAgeBinCollection(Project project)
        {
            this.m_Project = project;
        }

        public AttributeValueAgeBin GetAgeBin(int ageMin, int ageMax)
        {
            string k = CreateBinKey(ageMin, ageMax);

            if (!this.m_Hint.ContainsKey(k))
            {
                AttributeValueAgeBin Bin = new AttributeValueAgeBin(ageMin, ageMax, this.m_Project);
                this.m_Hint.Add(k, Bin);
            }

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
