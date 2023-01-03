// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System.Diagnostics;
using System.Collections.Generic;

namespace SyncroSim.STSim
{
    public partial class STSimTransformer
    {
        private TransitionMap m_TransitionMap;
        private DeterministicTransitionMap m_DeterministicTransitionMap;
        private TransitionOrderMap m_TransitionOrderMap;
        private TransitionTargetMap m_TransitionTargetMap;
        private TransitionTargetPrioritizationKeyMap m_TransitionTargetPrioritizationKeyMap;
        private TransitionAttributeTargetMap m_TransitionAttributeTargetMap;
        private TransitionAttributeTargetPrioritizationKeyMap m_TransitionAttributeTargetPrioritizationKeyMap;
        private TransitionAttributeValueMap m_TransitionAttributeValueMap;
        private TransitionSizeDistributionMap m_TransitionSizeDistributionMap;
        private TransitionDirectionMultiplierMap m_TransitionDirectionMultiplierMap;
        private TransitionSlopeMultiplierMap m_TransitionSlopeMultiplierMap;
        private TransitionAdjacencySettingMap m_TransitionAdjacencySettingMap;
        private TransitionAdjacencyMultiplierMap m_TransitionAdjacencyMultiplierMap;
        private TransitionPatchPrioritizationMap m_TransitionPatchPrioritizationMap;
        private TransitionSizePrioritizationMap m_TransitionSizePrioritizationMap;
        private Dictionary<int, double[]> m_TransitionAdjacencyStateAttributeValueMap = new Dictionary<int, double[]>();
        private TransitionPathwayAutoCorrelationMap m_TransitionPathwayAutoCorrelationMap;

        private TstRandomizeMap m_TstRandomizeMap;
        private TstTransitionGroupMap m_TstTransitionGroupMap;
        private InitialTSTSpatialMap m_InitialTstSpatialMap;
        private StateAttributeValueMap m_StateAttributeValueMap;
        private InitialConditionsDistributionMap m_InitialConditionsDistributionMap;
        private InitialConditionsSpatialMap m_InitialConditionsSpatialMap;
        private IntervalMeanTimestepMap m_IntervalMeanTimestepMap;
        private ProportionAccumulatorMap m_ProportionAccumulatorMap;

        private Dictionary<int, bool> m_StateAttributeTypeIds;
        private Dictionary<int, bool> m_TransitionAttributeTypeIds;

        private Dictionary<int, Dictionary<int, double[]>> m_AvgStateClassMap = new Dictionary<int, Dictionary<int, double[]>>();
        private Dictionary<int, double[]> m_AvgAgeMap = new Dictionary<int, double[]>();
        private Dictionary<int, Dictionary<int, double[]>> m_AvgStratumMap = new Dictionary<int, Dictionary<int, double[]>>();
        private Dictionary<int, Dictionary<int, double[]>> m_AvgTransitionProbMap = new Dictionary<int, Dictionary<int, double[]>>();
        private Dictionary<int, Dictionary<int, double[]>> m_AvgTSTMap = new Dictionary<int, Dictionary<int, double[]>>();
        private Dictionary<int, Dictionary<int, double[]>> m_AvgStateAttrMap = new Dictionary<int, Dictionary<int, double[]>>();
        private Dictionary<int, Dictionary<int, double[]>> m_AvgTransitionAttrMap = new Dictionary<int, Dictionary<int, double[]>>();

        private void InitializeCollectionMaps()
        {
            //Create the Transition Map
            Debug.Assert(this.m_TransitionMap == null);
            this.m_TransitionMap = new TransitionMap(this.ResultScenario, this.m_Transitions);

            //Create the Deterministic Transition Map
            Debug.Assert(this.m_DeterministicTransitionMap == null);
            this.m_DeterministicTransitionMap = new DeterministicTransitionMap(this.ResultScenario, this.m_DeterministicTransitions);

            //Create maps associated with Transition Multiplier types
            this.CreateMultiplierTypeMaps();

            //Create the transition size distribution map
            this.CreateTransitionSizeDistributionMap();

            //Create the transition direction multipliers map
            Debug.Assert(this.m_TransitionDirectionMultiplierMap == null);
            this.m_TransitionDirectionMultiplierMap = new TransitionDirectionMultiplierMap(this.ResultScenario, this.m_TransitionDirectionMultipliers, this.m_DistributionProvider);

            //Create the transition slope multipliers map
            Debug.Assert(this.m_TransitionSlopeMultiplierMap == null);
            this.m_TransitionSlopeMultiplierMap = new TransitionSlopeMultiplierMap(this.ResultScenario, this.m_TransitionSlopeMultipliers, this.m_DistributionProvider);

            //Create the transition adjacency setting map 
            Debug.Assert(this.m_TransitionAdjacencySettingMap == null);
            this.m_TransitionAdjacencySettingMap = new TransitionAdjacencySettingMap(this.m_TransitionAdjacencySettings);

            //Create the transition adjacency multiplier map
            Debug.Assert(this.m_TransitionAdjacencyMultiplierMap == null);
            this.m_TransitionAdjacencyMultiplierMap = new TransitionAdjacencyMultiplierMap(this.ResultScenario, this.m_TransitionAdjacencyMultipliers, this.m_DistributionProvider, this.MinimumIteration, this.MinimumTimestep);

            //Create the transition order map
            Debug.Assert(this.m_TransitionOrderMap == null);
            this.m_TransitionOrderMap = new TransitionOrderMap(this.m_TransitionOrders);

            //Create the transition targets map.
            Debug.Assert(this.m_TransitionTargetMap == null);
            this.m_TransitionTargetMap = new TransitionTargetMap(this.ResultScenario, this.m_TransitionTargets);

            //Create the full transition target prioritizations map which will validate there are no duplicates
            TransitionTargetPrioritizationValidationMap ttvalmap = new TransitionTargetPrioritizationValidationMap(this.ResultScenario, this.m_TransitionTargetPrioritizations);

            //Then create the transition target prioritization map which is not a full map - just a map by transition group, iteration, and timestep
            Debug.Assert(this.m_TransitionTargetPrioritizationKeyMap == null);
            this.m_TransitionTargetPrioritizationKeyMap = new TransitionTargetPrioritizationKeyMap(this.m_TransitionTargetPrioritizations);

            //Create the transition attribute target map
            Debug.Assert(this.m_TransitionAttributeTargetMap == null);
            this.m_TransitionAttributeTargetMap = new TransitionAttributeTargetMap(this.ResultScenario, this.m_TransitionAttributeTargets);
        
            //Create the full transition attribute target prioritizations map which will validate there are no duplicates
            TransitionAttributeTargetPrioritizationValidationMap tatvalmap = new TransitionAttributeTargetPrioritizationValidationMap(this.ResultScenario, this.m_TransitionAttributeTargetPrioritizations);        
            
            //Then create the transition attribute target prioritization map which is not a full map - just a map by transition attribute type, iteration, and timestep
            Debug.Assert(this.m_TransitionAttributeTargetPrioritizationKeyMap == null);
            this.m_TransitionAttributeTargetPrioritizationKeyMap = new TransitionAttributeTargetPrioritizationKeyMap(this.m_TransitionAttributeTargetPrioritizations);

            //Create the transition patch prioritization map
            Debug.Assert(this.m_TransitionPatchPrioritizationMap == null);
            this.m_TransitionPatchPrioritizationMap = new TransitionPatchPrioritizationMap(this.ResultScenario, this.m_TransitionPatchPrioritizations);

            //Create the transition size prioritization map
            Debug.Assert(this.m_TransitionSizePrioritizationMap == null);
            this.m_TransitionSizePrioritizationMap = new TransitionSizePrioritizationMap(this.ResultScenario, this.m_TransitionSizePrioritizations);

            //Create the transition pathway auto-correlation map
            Debug.Assert(this.m_TransitionPathwayAutoCorrelationMap == null);
            this.m_TransitionPathwayAutoCorrelationMap = new TransitionPathwayAutoCorrelationMap(this.ResultScenario, this.m_TransitionPathwayAutoCorrelations);
        }

        private void CreateMultiplierTypeMaps()
        {
            foreach (TransitionMultiplierValue tm in this.m_TransitionMultiplierValues)
            {
                TransitionMultiplierType mt = this.GetTransitionMultiplierType(tm.TransitionMultiplierTypeId);
                mt.AddTransitionMultiplierValue(tm);
            }

            foreach (TransitionSpatialMultiplier sm in this.m_TransitionSpatialMultipliers)
            {
                TransitionMultiplierType mt = this.GetTransitionMultiplierType(sm.TransitionMultiplierTypeId);
                mt.AddTransitionSpatialMultiplier(sm);
            }

            foreach (TransitionSpatialInitiationMultiplier sm in this.m_TransitionSpatialInitiationMultipliers)
            {
                TransitionMultiplierType mt = this.GetTransitionMultiplierType(sm.TransitionMultiplierTypeId);
                mt.AddTransitionSpatialInitiationMultiplier(sm);
            }

            foreach (TransitionMultiplierType tmt in this.m_TransitionMultiplierTypes)
            {
                tmt.CreateMultiplierValueMap();
                tmt.CreateSpatialMultiplierMap();
                tmt.CreateSpatialInitiationMultiplierMap();
            }
        }

        /// <summary>
        /// Initializes the transition size distribution map
        /// </summary>
        /// <remarks></remarks>
        private void CreateTransitionSizeDistributionMap()
        {
            Debug.Assert(this.m_TransitionSizeDistributionMap == null);

            this.m_TransitionSizeDistributionMap = new TransitionSizeDistributionMap(this.ResultScenario, this.m_TransitionSizeDistributions);
            this.m_TransitionSizeDistributionMap.Normalize();
        }
    }
}
