// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using SyncroSim.Core;

namespace SyncroSim.STSim
{
    internal class TransitionAttributeTarget : STSimDistributionBase
    {
        private int m_TransitionAttributeTargetId;
        private int m_TransitionAttributeTypeId;
        private double m_TargetRemaining;
        private double m_ExpectedAmount;
        private double m_Multiplier = 1.0;
        private List<TransitionAttributeTargetPrioritization> m_Prioritizations;
        private TransitionAttributeTargetPrioritizationItemMap m_ItemMap;
        private TransitionAttributeTargetPrioritizationListMap m_ListMap;
        private Scenario m_Scenario;

        public TransitionAttributeTarget(
            int transitionAttributeTargetId, int? iteration, int? timestep, int? stratumId, int? secondaryStratumId, int? tertiaryStratumId, 
            int transitionAttributeTypeId, double? targetAmount, int? distributionTypeId, DistributionFrequency? distributionFrequency, 
            double? distributionSD, double? distributionMin, double? distributionMax, Scenario scenario) : base(iteration, timestep, stratumId, secondaryStratumId,
                tertiaryStratumId, targetAmount, distributionTypeId, distributionFrequency, distributionSD, distributionMin, distributionMax)
        {
            this.m_TransitionAttributeTargetId = transitionAttributeTargetId;
            this.m_TransitionAttributeTypeId = transitionAttributeTypeId;
            this.m_Scenario = scenario;
        }

        public int TransitionAttributeTargetId
        {
            get
            {
                return this.m_TransitionAttributeTargetId;
            }
        }

        public int TransitionAttributeTypeId
        {
            get
            {
                return this.m_TransitionAttributeTypeId;
            }
        }

        public double TargetRemaining
        {
            get
            {
                this.CheckDisabled();
                return this.m_TargetRemaining;
            }
            set
            {
                this.CheckDisabled();
                this.m_TargetRemaining = value;
            }
        }

        public double TargetRemainingNoCheck
        {
            get
            {
                return this.m_TargetRemaining;
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

        public bool HasPrioritizations
        {
            get
            {
                return (this.m_Prioritizations != null);
            }
        }

        public override STSimDistributionBase Clone()
        {
            TransitionAttributeTarget t = new TransitionAttributeTarget(
                this.TransitionAttributeTargetId, this.Iteration, this.Timestep, this.StratumId, this.SecondaryStratumId, this.TertiaryStratumId, 
                this.TransitionAttributeTypeId, this.DistributionValue, this.DistributionTypeId, this.DistributionFrequency, this.DistributionSD, 
                this.DistributionMin, this.DistributionMax, this.m_Scenario);

            t.TargetRemaining = this.TargetRemainingNoCheck;
            t.Multiplier = this.MultiplierNoCheck;
            t.ExpectedAmount = this.ExpectedAmountNoCheck;
            t.IsDisabled = this.IsDisabled;

            if (this.m_Prioritizations != null)
            {
                t.SetPrioritizations(this.m_Prioritizations);
            }

            return t;
        }

        public void SetPrioritizations(List<TransitionAttributeTargetPrioritization> prioritizations)
        {
            this.ClonePrioritizations(prioritizations);

            Debug.Assert(this.m_ItemMap == null);
            this.m_ItemMap = new TransitionAttributeTargetPrioritizationItemMap(this.m_Prioritizations, this.m_Scenario);

            Debug.Assert(this.m_ListMap == null);
            this.m_ListMap = new TransitionAttributeTargetPrioritizationListMap(this.m_Prioritizations, this.m_TransitionAttributeTypeId);
        }

        public List<TransitionAttributeTargetPrioritization> GetPrioritizations(int iteration, int timestep)
        {
            return this.m_ListMap.GetPrioritizations(iteration, timestep);
        }

        public TransitionAttributeTargetPrioritization GetPrioritization(
            int? stratumId,
            int? secondaryStratumId,
            int? tertiaryStratumId,
            int? transitionGroupId,
            int? stateClassId, 
            int iteration, 
            int timestep)
        {
            TransitionAttributeTargetPrioritization pri = this.m_ItemMap.GetPrioritization(
                stratumId, secondaryStratumId, tertiaryStratumId, transitionGroupId, stateClassId, iteration, timestep);

            if (pri != null)
            {
                return pri;
            }

            List<TransitionAttributeTargetPrioritization> lst = this.m_ListMap.GetPrioritizations(iteration, timestep);

            if (lst != null)
            {
                pri = lst.Last();
                Debug.Assert(pri.Priority == double.MaxValue);

                return pri;
            }

            return null;
        }

        private void ClonePrioritizations(List<TransitionAttributeTargetPrioritization> prioritizations)
        {
            Debug.Assert(this.m_Prioritizations == null);
            this.m_Prioritizations = new List<TransitionAttributeTargetPrioritization>();

            foreach (TransitionAttributeTargetPrioritization t in prioritizations)
            {
                this.m_Prioritizations.Add(new TransitionAttributeTargetPrioritization(
                    t.Iteration,
                    t.Timestep,
                    t.TransitionAttributeTypeId,
                    t.StratumId,
                    t.SecondaryStratumId,
                    t.TertiaryStratumId,
                    t.TransitionGroupId,
                    t.StateClassId,
                    t.Priority));
            }

            Debug.Assert(this.m_Prioritizations.Count == prioritizations.Count);
        }
    }
}
