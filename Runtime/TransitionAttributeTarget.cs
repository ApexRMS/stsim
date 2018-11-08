// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using SyncroSim.Common;
using SyncroSim.StochasticTime;
using System.Diagnostics;
using System.Collections.Generic;

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
        private MultiLevelKeyMap5<TransitionAttributeTargetPrioritization> m_PrioritizationMap;
        private TransitionAttributeTargetPrioritization m_DefaultPrioritization;

        public TransitionAttributeTarget(
            int transitionAttributeTargetId, int? iteration, int? timestep, int? stratumId, int? secondaryStratumId, int? tertiaryStratumId, 
            int transitionAttributeTypeId, double? targetAmount, int? distributionTypeId, DistributionFrequency? distributionFrequency, 
            double? distributionSD, double? distributionMin, double? distributionMax) : base(iteration, timestep, stratumId, secondaryStratumId,
                tertiaryStratumId, targetAmount, distributionTypeId, distributionFrequency, distributionSD, distributionMin, distributionMax)
        {
            this.m_TransitionAttributeTargetId = transitionAttributeTargetId;
            this.m_TransitionAttributeTypeId = transitionAttributeTypeId;
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
                this.CHECK_DISABLED();
                return this.m_TargetRemaining;
            }
            set
            {
                this.CHECK_DISABLED();
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
                this.CHECK_DISABLED();
                return this.m_Multiplier;
            }
            set
            {
                this.CHECK_DISABLED();
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
                this.CHECK_DISABLED();
                return this.m_ExpectedAmount;
            }
            set
            {
                this.CHECK_DISABLED();
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

        public List<TransitionAttributeTargetPrioritization> Prioritizations
        {
            get
            {
                return m_Prioritizations;
            }
        }

        internal TransitionAttributeTargetPrioritization DefaultPrioritization
        {
            get
            {
                return m_DefaultPrioritization;
            }

            set
            {
                m_DefaultPrioritization = value;
            }
        }

        public override STSimDistributionBase Clone()
        {
            TransitionAttributeTarget t = new TransitionAttributeTarget(
                this.TransitionAttributeTargetId, this.Iteration, this.Timestep, this.StratumId, this.SecondaryStratumId, this.TertiaryStratumId, 
                this.TransitionAttributeTypeId, this.DistributionValue, this.DistributionTypeId, this.DistributionFrequency, this.DistributionSD, 
                this.DistributionMin, this.DistributionMax);

            t.TargetRemaining = this.TargetRemainingNoCheck;
            t.Multiplier = this.MultiplierNoCheck;
            t.ExpectedAmount = this.ExpectedAmountNoCheck;
            t.IsDisabled = this.IsDisabled;

            t.SetPrioritizations(this.Prioritizations);
            t.DefaultPrioritization = this.DefaultPrioritization;

            return t;
        }

        public void SetPrioritizations(List<TransitionAttributeTargetPrioritization> prioritizations)
        {
            this.ClonePrioritizationList(prioritizations);
            this.CreatePrioritizationMap();
        }

        public TransitionAttributeTargetPrioritization GetPrioritization(
            int stratumId,
            int? secondaryStratumId,
            int? tertiaryStratumId,
            int? transitionGroupId,
            int stateClassId)
        {
            //Look for a prioritization in the map.  If it is not found, return the default.

            TransitionAttributeTargetPrioritization pri = this.m_PrioritizationMap.GetItem(
                stratumId, secondaryStratumId, tertiaryStratumId, transitionGroupId, stateClassId);

            if (pri == null)
            {
                pri = this.m_DefaultPrioritization;
            }

            return pri;
        }

        private void ClonePrioritizationList(List<TransitionAttributeTargetPrioritization> prioritizations)
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

        private void CreatePrioritizationMap()
        {
            Debug.Assert(this.m_PrioritizationMap == null);
            this.m_PrioritizationMap = new MultiLevelKeyMap5<TransitionAttributeTargetPrioritization>();

            foreach (TransitionAttributeTargetPrioritization pri in this.m_Prioritizations)
            {
                TransitionAttributeTargetPrioritization p = this.m_PrioritizationMap.GetItemExact(
                    pri.StratumId, pri.SecondaryStratumId, pri.TertiaryStratumId, pri.TransitionGroupId, pri.StateClassId);

                if (p == null)
                {
                    this.m_PrioritizationMap.AddItem(
                        pri.StratumId,
                        pri.SecondaryStratumId,
                        pri.TertiaryStratumId,
                        pri.TransitionGroupId,
                        pri.StateClassId,
                        pri);
                }
            }
        }
    }
}
