// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System.Drawing;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    internal class ProbabilisticTransitionLine : TransitionDiagramLine
    {
        private int m_TransitionTypeId;
        private List<int> m_TransitionGroups = new List<int>();

        public ProbabilisticTransitionLine(int transitionTypeId, Color lineColor) : base(lineColor)
        {
            this.m_TransitionTypeId = transitionTypeId;
        }

        internal int TransitionTypeId
        {
            get
            {
                return this.m_TransitionTypeId;
            }
        }

        internal List<int> TransitionGroups
        {
            get
            {
                return this.m_TransitionGroups;
            }
        }
    }
}
