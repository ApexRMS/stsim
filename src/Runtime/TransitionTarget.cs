// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using SyncroSim.Core;
using SyncroSim.StochasticTime;

namespace SyncroSim.STSim
{
    internal class TransitionTarget : STSimDistributionBase
    {
        private int m_TransitionGroupId;
        private double m_ExpectedAmount;
        private double m_Multiplier = 1.0;
        private List<TransitionTargetPrioritization> m_Prioritizations;
        private TransitionTargetPrioritizationItemMap m_ItemMap;
        private TransitionTargetPrioritizationListMap m_ListMap;
        private Scenario m_Scenario;

        public TransitionTarget(
            int? iteration, int? timestep, int? stratumId, int? secondaryStratumId, int? tertiaryStratumId, 
            int transitionGroupId, double? targetAmount, int? distributionTypeId, DistributionFrequency? distributionFrequency,
            double? distributionSD, double? distributionMin, double? distributionMax, Scenario scenario) : base(iteration, timestep, stratumId, secondaryStratumId, 
                tertiaryStratumId, targetAmount, distributionTypeId, distributionFrequency, distributionSD, distributionMin, distributionMax)
        {
            this.m_TransitionGroupId = transitionGroupId;
            this.m_Scenario = scenario;
        }

        public int TransitionGroupId
        {
            get
            {
                return this.m_TransitionGroupId;
            }
        }

        public double ExpectedAmount
        {
            get
            {
                this.CheckDisabled();
                return this.m_ExpectedAmount;
            }
            set
            {
                this.CheckDisabled();
                this.m_ExpectedAmount = value;
            }
        }

        public double ExpectedAmountNoCheck
        {
            get
            {
                return this.m_ExpectedAmount;
            }
        }

        public double Multiplier
        {
            get
            {
                this.CheckDisabled();
                return this.m_Multiplier;
            }
            set
            {
                this.CheckDisabled();
                this.m_Multiplier = value;
            }
        }

        public double MultiplierNoCheck
        {
            get
            {
                return this.m_Multiplier;
            }
        }

        public bool HasPrioritizations
        {
            get
            {
                return (this.m_Prioritizations != null);
            }
        }

        public override STSimDistributionBase Clone()
        {
            TransitionTarget t = new TransitionTarget(
                this.Iteration, this.Timestep, this.StratumId, 
                this.SecondaryStratumId, this.TertiaryStratumId, this.TransitionGroupId, 
                this.DistributionValue, this.DistributionTypeId, this.DistributionFrequency, 
                this.DistributionSD, this.DistributionMin, this.DistributionMax, this.m_Scenario);

            t.ExpectedAmount = this.ExpectedAmountNoCheck;
            t.Multiplier = this.MultiplierNoCheck;
            t.IsDisabled = this.IsDisabled;

            if (this.m_Prioritizations != null)
            {
                t.SetPrioritizations(this.m_Prioritizations);
            }
            
            return t;
        }

        public void SetPrioritizations(List<TransitionTargetPrioritization> prioritizations)
        {
            this.ClonePrioritizations(prioritizations);

            Debug.Assert(this.m_ItemMap == null);
            this.m_ItemMap = new TransitionTargetPrioritizationItemMap(this.m_Prioritizations, this.m_Scenario);

            Debug.Assert(this.m_ListMap == null);
            this.m_ListMap = new TransitionTargetPrioritizationListMap(this.m_Prioritizations, this.m_TransitionGroupId);
        }

        public List<TransitionTargetPrioritization> GetPrioritizations(int iteration, int timestep)
        {
            return this.m_ListMap.GetPrioritizations(iteration, timestep);
        }

        public TransitionTargetPrioritization GetPrioritization(
            int? stratumId,
            int? secondaryStratumId,
            int? tertiaryStratumId,
            int? stateClassId,
            int iteration,
            int timestep)
        {
            TransitionTargetPrioritization pri = this.m_ItemMap.GetPrioritization(
                stratumId, secondaryStratumId, tertiaryStratumId, stateClassId, iteration, timestep);

            if (pri != null)
            {
                return pri;
            }

            List<TransitionTargetPrioritization> lst = this.m_ListMap.GetPrioritizations(iteration, timestep);

            if (lst != null)
            {
                pri = lst.Last();
                Debug.Assert(pri.Priority == double.MaxValue);

                return pri;
            }

            return null;
        }

        private void ClonePrioritizations(List<TransitionTargetPrioritization> prioritizations)
        {
            Debug.Assert(this.m_Prioritizations == null);
            this.m_Prioritizations = new List<TransitionTargetPrioritization>();

            foreach (TransitionTargetPrioritization t in prioritizations)
            {
                this.m_Prioritizations.Add(new TransitionTargetPrioritization(
                    t.Iteration,
                    t.Timestep,
                    t.TransitionGroupId,
                    t.StratumId,
                    t.SecondaryStratumId,
                    t.TertiaryStratumId,
                    t.StateClassId,
                    t.Priority));
            }

            Debug.Assert(this.m_Prioritizations.Count == prioritizations.Count);
        }
    }
}
