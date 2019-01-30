// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

namespace SyncroSim.STSim
{
    internal class StratumTabStripItem : TransitionDiagramTabStripItem
    {
        private int? m_StratumId;

        public StratumTabStripItem(string stratumName, int? stratumId) : base(stratumName)
        {
            this.m_StratumId = stratumId;
        }

        public int? StratumId
        {
            get
            {
                return this.m_StratumId;
            }
        }
    }
}
