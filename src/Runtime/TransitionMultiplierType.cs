// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Diagnostics;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    internal class TransitionMultiplierType
    {
        private Scenario m_Scenario;
        private STSimDistributionProvider m_Provider;
        private int? m_TransitionMultiplierTypeId;
        private TransitionMultiplierValueCollection m_TransitionMultiplierValues = new TransitionMultiplierValueCollection();
        private TransitionMultiplierValueMap m_TransitionMultiplierValueMap;
        private TransitionSpatialMultiplierCollection m_TransitionSpatialMultipliers = new TransitionSpatialMultiplierCollection();
        private TransitionSpatialMultiplierMap m_TransitionSpatialMultiplierMap;
        private TransitionSpatialInitiationMultiplierCollection m_TransitionSpatialInitiationMultipliers = new TransitionSpatialInitiationMultiplierCollection();
        private TransitionSpatialInitiationMultiplierMap m_TransitionSpatialInitiationMultiplierMap;

        public TransitionMultiplierType(int? transitionMultiplierTypeId, Scenario scenario, STSimDistributionProvider provider)
        {
            this.m_TransitionMultiplierTypeId = transitionMultiplierTypeId;
            this.m_Scenario = scenario;
            this.m_Provider = provider;
        }

        public int? TransitionMultiplierTypeId
        {
            get
            {
                return this.m_TransitionMultiplierTypeId;
            }
        }

        internal TransitionMultiplierValueMap TransitionMultiplierValueMap
        {
            get
            {
                return this.m_TransitionMultiplierValueMap;
            }
        }

        internal TransitionSpatialMultiplierMap TransitionSpatialMultiplierMap
        {
            get
            {
                return this.m_TransitionSpatialMultiplierMap;
            }
        }

        internal TransitionSpatialInitiationMultiplierMap TransitionSpatialInitiationMultiplierMap
        {
            get
            {
                return this.m_TransitionSpatialInitiationMultiplierMap;
            }
        }

        internal void AddTransitionMultiplierValue(TransitionMultiplierValue multiplier)
        {
            if (multiplier.TransitionMultiplierTypeId != this.m_TransitionMultiplierTypeId)
            {
                throw new ArgumentException("The transition multiplier type is not correct.");
            }

            this.m_TransitionMultiplierValues.Add(multiplier);
        }

        internal void AddTransitionSpatialMultiplier(TransitionSpatialMultiplier multiplier)
        {
            if (multiplier.TransitionMultiplierTypeId != this.m_TransitionMultiplierTypeId)
            {
                throw new ArgumentException("The transition multiplier type is not correct.");
            }

            this.m_TransitionSpatialMultipliers.Add(multiplier);
        }

        internal void AddTransitionSpatialInitiationMultiplier(TransitionSpatialInitiationMultiplier multiplier)
        {
            if (multiplier.TransitionMultiplierTypeId != this.m_TransitionMultiplierTypeId)
            {
                throw new ArgumentException("The transition multiplier type is not correct.");
            }

            this.m_TransitionSpatialInitiationMultipliers.Add(multiplier);
        }

        internal void ClearMultiplierValueMap()
        {
            this.m_TransitionMultiplierValues.Clear();
            this.m_TransitionMultiplierValueMap = null;
        }

        internal void ClearSpatialMultiplierMap()
        {
            this.m_TransitionSpatialMultipliers.Clear();
            this.m_TransitionSpatialMultiplierMap = null;
        }

        internal void ClearSpatialInitiationMultiplierMap()
        {
            this.m_TransitionSpatialInitiationMultipliers.Clear();
            this.m_TransitionSpatialInitiationMultiplierMap = null;
        }

        internal void CreateMultiplierValueMap()
        {
            if (this.m_TransitionMultiplierValues.Count > 0)
            {
                Debug.Assert(this.m_TransitionMultiplierValueMap == null);

                this.m_TransitionMultiplierValueMap = new TransitionMultiplierValueMap(
                    this.m_Scenario, this.m_TransitionMultiplierValues, this.m_Provider);
            }
        }

        internal void CreateSpatialMultiplierMap()
        {
            if (this.m_TransitionSpatialMultipliers.Count > 0)
            {
                Debug.Assert(this.m_TransitionSpatialMultiplierMap == null);

                this.m_TransitionSpatialMultiplierMap = new TransitionSpatialMultiplierMap(
                    this.m_Scenario, this.m_TransitionSpatialMultipliers);
            }
        }

        internal void CreateSpatialInitiationMultiplierMap()
        {
            if (this.m_TransitionSpatialInitiationMultipliers.Count > 0)
            {
                Debug.Assert(this.m_TransitionSpatialInitiationMultiplierMap == null);

                this.m_TransitionSpatialInitiationMultiplierMap = new TransitionSpatialInitiationMultiplierMap(
                    this.m_Scenario, this.m_TransitionSpatialInitiationMultipliers);
            }
        }
    }
}
