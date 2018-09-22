// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

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
