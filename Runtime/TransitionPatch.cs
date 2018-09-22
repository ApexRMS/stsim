// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System.Collections.Generic;

namespace SyncroSim.STSim
{
    internal class TransitionPatch
    {
        private double m_Size;
        private Cell m_SeedCell;
        private Dictionary<int, Cell> m_EdgeCells = new Dictionary<int, Cell>();
        private Dictionary<int, Cell> m_AllCells = new Dictionary<int, Cell>();

        public TransitionPatch(Cell seedCell)
        {
            this.m_SeedCell = seedCell;
        }

        public double Size
        {
            get
            {
                return this.m_Size;
            }
            set
            {
                this.m_Size = value;
            }
        }

        public Cell SeedCell
        {
            get
            {
                return this.m_SeedCell;
            }
        }

        public Dictionary<int, Cell> EdgeCells
        {
            get
            {
                return this.m_EdgeCells;
            }
        }

        public Dictionary<int, Cell> AllCells
        {
            get
            {
                return this.m_AllCells;
            }
        }
    }
}
