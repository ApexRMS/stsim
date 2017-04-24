'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Partial Class STSimTransformer

    Private m_TransitionMap As TransitionMap
    Private m_DeterministicTransitionMap As DeterministicTransitionMap
    Private m_TransitionOrderMap As TransitionOrderMap
    Private m_TransitionTargetMap As TransitionTargetMap
    Private m_TransitionAttributeTargetMap As TransitionAttributeTargetMap
    Private m_TransitionAttributeValueMap As TransitionAttributeValueMap
    Private m_TransitionSizeDistributionMap As TransitionSizeDistributionMap
    Private m_TransitionDirectionMultiplierMap As TransitionDirectionMultiplierMap
    Private m_TransitionSlopeMultiplierMap As TransitionSlopeMultiplierMap
    Private m_TransitionAdjacencySettingMap As TransitionAdjacencySettingMap
    Private m_TransitionAdjacencyMultiplierMap As TransitionAdjacencyMultiplierMap
    Private m_TransitionPatchPrioritizationMap As TransitionPatchPrioritizationMap
    Private m_TransitionSizePrioritizationMap As TransitionSizePrioritizationMap
    Private m_TransitionAttributeTypeIds As Dictionary(Of Integer, Boolean)
    Private m_TransitionAdjacencyStateAttributeValueMap As New Dictionary(Of Integer, Double())
    Private m_TransitionPathwayAutoCorrelationMap As TransitionPathwayAutoCorrelationMap

    Private m_TstRandomizeMap As TstRandomizeMap
    Private m_TstTransitionGroupMap As TstTransitionGroupMap
    Private m_StateAttributeValueMapAges As StateAttributeValueMap
    Private m_StateAttributeValueMapNoAges As StateAttributeValueMap
    Private m_InitialConditionsDistributionMap As InitialConditionsDistributionMap
    Private m_InitialConditionsSpatialMap As InitialConditionsSpatialMap
    Private m_StateAttributeTypeIdsAges As Dictionary(Of Integer, Boolean)
    Private m_StateAttributeTypeIdsNoAges As Dictionary(Of Integer, Boolean)
    Private m_AnnualAvgTransitionProbMap As New Dictionary(Of Integer, Dictionary(Of Integer, Double()))
    Private m_IntervalMeanTimestepMap As IntervalMeanTimestepMap
    Private m_ProportionAccumulatorMap As ProportionAccumulatorMap

    Private Sub InitializeCollectionMaps()

        'Create the Transition Map
        Debug.Assert(Me.m_TransitionMap Is Nothing)
        Me.m_TransitionMap = New TransitionMap(Me.ResultScenario, Me.m_Transitions)

        'Create the Deterministic Transition Map
        Debug.Assert(Me.m_DeterministicTransitionMap Is Nothing)
        Me.m_DeterministicTransitionMap = New DeterministicTransitionMap(Me.ResultScenario, Me.m_DeterministicTransitions)

        'Create maps associated with Transition Multiplier types
        Me.CreateMultiplierTypeMaps()

        'Create the transition size distribution map
        Me.CreateTransitionSizeDistributionMap()

        'Create the transition direction multipliers map
        Debug.Assert(Me.m_TransitionDirectionMultiplierMap Is Nothing)
        Me.m_TransitionDirectionMultiplierMap = New TransitionDirectionMultiplierMap(Me.ResultScenario, Me.m_TransitionDirectionMultipliers, Me.m_DistributionProvider)

        'Create the transition slope multipliers map
        Debug.Assert(Me.m_TransitionSlopeMultiplierMap Is Nothing)
        Me.m_TransitionSlopeMultiplierMap = New TransitionSlopeMultiplierMap(Me.ResultScenario, Me.m_TransitionSlopeMultipliers, Me.m_DistributionProvider)

        'Create the transition adjacency setting map 
        Debug.Assert(Me.m_TransitionAdjacencySettingMap Is Nothing)
        Me.m_TransitionAdjacencySettingMap = New TransitionAdjacencySettingMap(Me.m_TransitionAdjacencySettings)

        'Create the transition adjacency multiplier map
        Debug.Assert(Me.m_TransitionAdjacencyMultiplierMap Is Nothing)
        Me.m_TransitionAdjacencyMultiplierMap = New TransitionAdjacencyMultiplierMap(Me.ResultScenario, Me.m_TransitionAdjacencyMultipliers, Me.m_DistributionProvider)

        'Create the transition order map
        Debug.Assert(Me.m_TransitionOrderMap Is Nothing)
        Me.m_TransitionOrderMap = New TransitionOrderMap(Me.m_TransitionOrders)

        'Create the transition targets map. 
        Debug.Assert(Me.m_TransitionTargetMap Is Nothing)
        Me.m_TransitionTargetMap = New TransitionTargetMap(Me.ResultScenario, Me.m_TransitionTargets)

        'Create the transition attribute targets map. 
        Debug.Assert(Me.m_TransitionAttributeTargetMap Is Nothing)
        Me.m_TransitionAttributeTargetMap = New TransitionAttributeTargetMap(Me.ResultScenario, Me.m_TransitionAttributeTargets)

        'Create the transition patch prioritization map
        Debug.Assert(Me.m_TransitionPatchPrioritizationMap Is Nothing)
        Me.m_TransitionPatchPrioritizationMap = New TransitionPatchPrioritizationMap(Me.ResultScenario, Me.m_TransitionPatchPrioritizations)

        'Create the transition size prioritization map
        Debug.Assert(Me.m_TransitionSizePrioritizationMap Is Nothing)
        Me.m_TransitionSizePrioritizationMap = New TransitionSizePrioritizationMap(Me.ResultScenario, Me.m_TransitionSizePrioritizations)

        'Create the transition pathway auto-correlation map
        Debug.Assert(Me.m_TransitionPathwayAutoCorrelationMap Is Nothing)
        Me.m_TransitionPathwayAutoCorrelationMap = New TransitionPathwayAutoCorrelationMap(Me.ResultScenario, Me.m_TransitionPathwayAutoCorrelations)

    End Sub

    Private Sub CreateMultiplierTypeMaps()

        For Each tm As TransitionMultiplierValue In Me.m_TransitionMultiplierValues

            Dim mt As TransitionMultiplierType = Me.GetTransitionMultiplierType(tm.TransitionMultiplierTypeId)
            mt.AddTransitionMultiplierValue(tm)

        Next

        For Each sm As TransitionSpatialMultiplier In Me.m_TransitionSpatialMultipliers

            Dim mt As TransitionMultiplierType = Me.GetTransitionMultiplierType(sm.TransitionMultiplierTypeId)
            mt.AddTransitionSpatialMultiplier(sm)

        Next

        For Each sm As TransitionSpatialInitiationMultiplier In Me.m_TransitionSpatialInitiationMultipliers

            Dim mt As TransitionMultiplierType = Me.GetTransitionMultiplierType(sm.TransitionMultiplierTypeId)
            mt.AddTransitionSpatialInitiationMultiplier(sm)

        Next

        For Each tmt As TransitionMultiplierType In Me.m_TransitionMultiplierTypes

            tmt.CreateTransitionMultiplierValueMap()
            tmt.CreateTransitionSpatialMultiplierMap()
            tmt.CreateTransitionSpatialInitiationMultiplierMap()

        Next

    End Sub

    ''' <summary>
    ''' Initializes the transition size distribution map
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateTransitionSizeDistributionMap()

        Debug.Assert(Me.m_TransitionSizeDistributionMap Is Nothing)

        Me.m_TransitionSizeDistributionMap = New TransitionSizeDistributionMap(Me.ResultScenario, Me.m_TransitionSizeDistributions)
        Me.m_TransitionSizeDistributionMap.Normalize()

    End Sub

End Class
