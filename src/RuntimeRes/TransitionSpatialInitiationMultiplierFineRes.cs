// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;

namespace SyncroSim.STSim
{
    internal class TransitionSpatialInitiationMultiplierFineRes
    {
        private int m_TransitionSpatialInitiationMultiplierId;
        private int m_TransitionGroupId;
        private int? m_TransitionMultiplierTypeId;
        private int? m_Iteration;
        private int? m_Timestep;
        private string m_Filename;

        public TransitionSpatialInitiationMultiplierFineRes(
            int transitionSpatialInitiationMultiplierId, int transitionGroupId, int? transitionMultiplierTypeId, int? 
            iteration, int? timestep, string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentException("The fileName parameter is not valid.");
            }

            this.m_TransitionSpatialInitiationMultiplierId = transitionSpatialInitiationMultiplierId;
            this.m_TransitionGroupId = transitionGroupId;
            this.m_TransitionMultiplierTypeId = transitionMultiplierTypeId;
            this.m_Iteration = iteration;
            this.m_Timestep = timestep;
            this.m_Filename = fileName;
        }

        public int TransitionSpatialInitiationMultiplierId
        {
            get
            {
                return this.m_TransitionSpatialInitiationMultiplierId;
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
