﻿// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal class TstRandomize
    {
        private int m_MaxInitialTst;
        private int m_MinInitialTst;

        public TstRandomize(int minInitialTst, int maxInitialTst)
        {
            this.m_MinInitialTst = minInitialTst;
            this.m_MaxInitialTst = maxInitialTst;
        }

        public int MaxInitialTst
        {
            get
            {
                return this.m_MaxInitialTst;
            }
        }

        public int MinInitialTst
        {
            get
            {
                return m_MinInitialTst;
            }
        }
    }
}