// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal class InitialConditionsSpatial
    {
        private int? m_Iteration;
        private string m_PrimaryStratumFileName;
        private string m_SecondaryStratumFileName;
        private string m_TertiaryStratumFileName;
        private string m_StateClassFileName;
        private string m_AgeFileName;

        public InitialConditionsSpatial(
            int? iteration, string primaryStratumName, string secondaryStratumFileName, 
            string tertiaryStratumFileName, string stateClassFileName, string ageFileName)
        {
            this.m_Iteration = iteration;
            this.m_PrimaryStratumFileName = primaryStratumName;
            this.m_SecondaryStratumFileName = secondaryStratumFileName;
            this.m_TertiaryStratumFileName = tertiaryStratumFileName;
            this.m_StateClassFileName = stateClassFileName;
            this.m_AgeFileName = ageFileName;
        }

        public int? Iteration
        {
            get
            {
                return this.m_Iteration;
            }
        }

        public string PrimaryStratumFileName
        {
            get
            {
                return this.m_PrimaryStratumFileName;
            }
        }

        public string SecondaryStratumFileName
        {
            get
            {
                return this.m_SecondaryStratumFileName;
            }
        }

        public string TertiaryStratumFileName
        {
            get
            {
                return this.m_TertiaryStratumFileName;
            }
        }

        public string StateClassFileName
        {
            get
            {
                return this.m_StateClassFileName;
            }
        }

        public string AgeFileName
        {
            get
            {
                return this.m_AgeFileName;
            }
        }
    }
}
