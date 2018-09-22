// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;

namespace SyncroSim.STSim
{
    internal class TransitionSpatialMultiplier
    {
        private int m_TransitionSpatialMultiplierId;
        private int m_TransitionGroupId;
        private int? m_TransitionMultiplierTypeId;
        private int? m_Iteration;
        private int? m_Timestep;
        private string m_Filename;

        public TransitionSpatialMultiplier(
            int transitionSpatialMultiplierId, int transitionGroupId, int? transitionMultiplierTypeId, 
            int? iteration, int? timestep, string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentException("The fileName parameter is not valid.");
            }

            this.m_TransitionSpatialMultiplierId = transitionSpatialMultiplierId;
            this.m_TransitionGroupId = transitionGroupId;
            this.m_TransitionMultiplierTypeId = transitionMultiplierTypeId;
            this.m_Iteration = iteration;
            this.m_Timestep = timestep;
            this.m_Filename = fileName;
        }

        public int TransitionSpatialMultiplierId
        {
            get
            {
                return this.m_TransitionSpatialMultiplierId;
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
