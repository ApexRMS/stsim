// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System.Collections.Generic;

namespace SyncroSim.STSim
{
    /// <summary>
    /// The simulation cell class
    /// </summary>
    /// <remarks>
    /// There can be a huge number of these so:
    /// (1.) Don't add any data members to this class unless you absolutely need to
    /// (2.) Don't make any allocations unless they are required for the model run
    /// </remarks>
    public class Cell
    {
        private int m_CellId;
        private int m_StratumId;
        private int? m_SecondaryStratumId;
        private int? m_TertiaryStratumId;
        private int m_StateClassId;
        private int m_Age;
        private TstCollection m_TstValues = new TstCollection();
        private List<Transition> m_Transitions = new List<Transition>();
        private Dictionary<string, object> m_Keys;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cellID">The ID for this cell</param>
        /// <remarks></remarks>
        public Cell(int cellId)
        {
            this.m_CellId = cellId;
        }

        /// <summary>
        /// Unique integer Id for the cell
        /// </summary>
        public int CellId
        {
            get
            {
                return this.m_CellId;
            }
        }

        /// <summary>
        /// Stratum Id for the cell
        /// </summary>
        public int StratumId
        {
            get
            {
                return this.m_StratumId;
            }
            set
            {
                this.m_StratumId = value;
            }
        }

        /// <summary>
        /// Gets or sets the secondary stratum Id for the cell
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public int? SecondaryStratumId
        {
            get
            {
                return this.m_SecondaryStratumId;
            }
            set
            {
                this.m_SecondaryStratumId = value;
            }
        }

        /// <summary>
        /// Gets or sets the tertiary stratum Id for the cell
        /// </summary>
        /// <returns></returns>
        public int? TertiaryStratumId
        {
            get
            {
                return this.m_TertiaryStratumId;
            }
            set
            {
                this.m_TertiaryStratumId = value;
            }
        }

        /// <summary>
        /// StateClass Id for the cell
        /// </summary>
        public int StateClassId
        {
            get
            {
                return this.m_StateClassId;
            }
            set
            {
                this.m_StateClassId = value;
            }
        }

        /// <summary>
        /// Age of the cell
        /// </summary>
        /// <remarks></remarks>
        public int Age
        {
            get
            {
                return this.m_Age;
            }
            set
            {
                this.m_Age = value;
            }
        }

        /// <summary>
        /// Collection of TST values for the cell
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        internal TstCollection TstValues
        {
            get
            {
                return this.m_TstValues;
            }
        }

        /// <summary>
        /// Collection of transitions for the cell
        /// </summary>
        /// <remarks></remarks>
        internal IList<Transition> Transitions
        {
            get
            {
                return this.m_Transitions;
            }
        }

        /// <summary>
        /// Associates an object with this cell
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <remarks></remarks>
        public void SetAssociatedObject(string key, object value)
        {
            if (this.m_Keys == null)
            {
                this.m_Keys = new Dictionary<string, object>();
            }

            this.m_Keys.Add(key, value);
        }

        /// <summary>
        /// Gets an associated object for this cell
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public object GetAssociatedObject(string key)
        {
            if (this.m_Keys == null)
            {
                return null;
            }

            return this.m_Keys[key];
        }
    }
}
