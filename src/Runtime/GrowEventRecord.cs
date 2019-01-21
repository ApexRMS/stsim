// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System.Diagnostics;

namespace SyncroSim.STSim
{
    /// <summary>
    /// Grow event record class
    /// </summary>
    /// <remarks></remarks>
    internal class GrowEventRecord
    {
        private Cell m_Cell;
        private double m_TravelTime;
        private double m_Likelihood;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="travelTime"></param>
        /// <param name="likelihood"></param>
        /// <remarks></remarks>
        public GrowEventRecord(Cell cell, double travelTime, double likelihood)
        {
            this.m_Cell = cell;
            this.m_TravelTime = travelTime;
            this.m_Likelihood = likelihood;

            Debug.Assert(this.m_Likelihood > 0.0);
        }

        /// <summary>
        /// Gets the cell fpr this grow event record
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public Cell Cell
        {
            get
            {
                return this.m_Cell;
            }
        }

        /// <summary>
        /// Gets the travel time for this grow event record
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public double TravelTime
        {
            get
            {
                return this.m_TravelTime;
            }
        }

        /// <summary>
        /// Gets the likelihood this grow event record
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public double Likelihood
        {
            get
            {
                return this.m_Likelihood;
            }
        }
    }
}
