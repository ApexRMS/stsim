// A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System.Diagnostics;
using System.Collections.Generic;
using SyncroSim.Common;
using SyncroSim.StochasticTime;

namespace SyncroSim.STSim
{
    internal class TransitionTarget : STSimDistributionBase
    {
        private int m_TransitionGroupId;
        private double m_ExpectedAmount;
        private double m_Multiplier = 1.0;
        private List<TransitionTargetPrioritization> m_Prioritizations;
        private MultiLevelKeyMap4<TransitionTargetPrioritization> m_PrioritizationMap;

        public TransitionTarget(
            int? iteration, int? timestep, int? stratumId, int? secondaryStratumId, int? tertiaryStratumId, 
            int transitionGroupId, double? targetAmount, int? distributionTypeId, DistributionFrequency? distributionFrequency,
            double? distributionSD, double? distributionMin, double? distributionMax) : base(iteration, timestep, stratumId, secondaryStratumId, 
                tertiaryStratumId, targetAmount, distributionTypeId, distributionFrequency, distributionSD, distributionMin, distributionMax)
        {

            this.m_TransitionGroupId = transitionGroupId;
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
                return this.m_ExpectedAmount;
            }
            set
            {
                this.m_ExpectedAmount = value;
            }
        }

        public double Multiplier
        {
            get
            {
                return this.m_Multiplier;
            }
            set
            {
                this.m_Multiplier = value;
            }
        }

        public List<TransitionTargetPrioritization> Prioritizations
        {
            get
            {
                return m_Prioritizations;
            }
        }

        public override STSimDistributionBase Clone()
        {
            TransitionTarget t = new TransitionTarget(
                this.Iteration, this.Timestep, this.StratumId, 
                this.SecondaryStratumId, this.TertiaryStratumId, this.TransitionGroupId, 
                this.DistributionValue, this.DistributionTypeId, this.DistributionFrequency, 
                this.DistributionSD, this.DistributionMin, this.DistributionMax);

            t.ExpectedAmount = this.ExpectedAmount;
            t.Multiplier = this.Multiplier;

            return t;
        }

        public void SetPrioritizations(List<TransitionTargetPrioritization> prioritizations)
        {
            this.ClonePrioritizationList(prioritizations);
            this.CreatePrioritizationMap();
        }

        public TransitionTargetPrioritization GetPrioritization(
            int stratumId,
            int? secondaryStratumId,
            int? tertiaryStratumId,
            int stateClassId)
        {
            return this.m_PrioritizationMap.GetItem(
                stratumId, secondaryStratumId, tertiaryStratumId, stateClassId);
        }

        private void ClonePrioritizationList(List<TransitionTargetPrioritization> prioritizations)
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

        private void CreatePrioritizationMap()
        {
            Debug.Assert(this.m_PrioritizationMap == null);
            this.m_PrioritizationMap = new MultiLevelKeyMap4<TransitionTargetPrioritization>();

            foreach (TransitionTargetPrioritization pri in this.m_Prioritizations)
            {
                TransitionTargetPrioritization p = this.m_PrioritizationMap.GetItemExact(
                    pri.StratumId, pri.SecondaryStratumId, pri.TertiaryStratumId, pri.StateClassId);

                if (p == null)
                {
                    this.m_PrioritizationMap.AddItem(
                        pri.StratumId,
                        pri.SecondaryStratumId,
                        pri.TertiaryStratumId,
                        pri.StateClassId,
                        pri);
                }
            }
        }
    }
}
