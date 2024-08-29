// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;

namespace SyncroSim.STSim
{
    internal class TransitionSpatialMultiplierFineRes
    {
        private int m_TransitionSpatialMultiplierFineResId;
        private int m_TransitionGroupId;
        private int? m_TransitionMultiplierTypeId;
        private int? m_Iteration;
        private int? m_Timestep;
        private string m_Filename;

        public TransitionSpatialMultiplierFineRes(
            int transitionSpatialMultiplierId, int transitionGroupId, int? transitionMultiplierTypeId, 
            int? iteration, int? timestep, string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentException("The fileName parameter is not valid.");
            }

            this.m_TransitionSpatialMultiplierFineResId = transitionSpatialMultiplierId;
            this.m_TransitionGroupId = transitionGroupId;
            this.m_TransitionMultiplierTypeId = transitionMultiplierTypeId;
            this.m_Iteration = iteration;
            this.m_Timestep = timestep;
            this.m_Filename = fileName;
        }

        public int TransitionSpatialMultiplierFineResId
        {
            get
            {
                return this.m_TransitionSpatialMultiplierFineResId;
            }
        }

        public int TransitionGroupId
        {
            get
            {
                return this.m_TransitionGroupId;
            }
        }

        public int? TransitionMultiplierTypeId
        {
            get
            {
                return this.m_TransitionMultiplierTypeId;
            }
        }

        public int? Iteration
        {
            get
            {
                return this.m_Iteration;
            }
        }

        public int? Timestep
        {
            get
            {
                return this.m_Timestep;
            }
        }

        public string FileName
        {
            get
            {
                return this.m_Filename;
            }
        }
    }
}
