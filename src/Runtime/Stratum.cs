// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System.Collections.Generic;

namespace SyncroSim.STSim
{
    internal class Stratum
    {
        private int m_StratumId;
        private Dictionary<int, Cell> m_Cells = new Dictionary<int, Cell>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="stratumId">The Id of the stratum</param>
        /// <remarks></remarks>
        public Stratum(int stratumId)
        {
            this.m_StratumId = stratumId;
        }

        /// <summary>
        /// Gets the stratum Id
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int StratumId
        {
            get
            {
                return this.m_StratumId;
            }
        }

        /// <summary>
        /// Gets the cell collection for this stratum
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public Dictionary<int, Cell> Cells
        {
            get
            {
                return this.m_Cells;
            }
        }
    }
}
