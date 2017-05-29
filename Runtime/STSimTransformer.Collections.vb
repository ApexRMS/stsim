'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core
Imports System.Globalization
Imports SyncroSim.StochasticTime

Partial Class STSimTransformer

    Private m_Cells As New CellCollection
    Private m_Strata As New StratumCollection
    Private m_SecondaryStrata As New StratumCollection
    Private m_StateClasses As New StateClassCollection
    Private m_TransitionGroups As New TransitionGroupCollection
    Private m_ShufflableTransitionGroups As New List(Of TransitionGroup)
    Private m_TransitionSpreadGroups As New List(Of TransitionGroup)
    Private m_TransitionTypes As New TransitionTypeCollection
    Private m_TransitionAttributeTypes As New TransitionAttributeTypeCollection
    Private m_TransitionMultiplierTypes As New TransitionMultiplierTypeCollection
    Private m_PatchPrioritizations As New PatchPrioritizationCollection
    Private m_InitialConditionsDistributions As New InitialConditionsDistributionCollection
    Private m_InitialConditionsSpatials As New InitialConditionsSpatialCollection
    Private m_Transitions As New TransitionCollection
    Private m_DeterministicTransitions As New DeterministicTransitionCollection
    Private m_TransitionMultiplierValues As New TransitionMultiplierValueCollection
    Private m_TransitionSpatialMultipliers As New TransitionSpatialMultiplierCollection
    Private m_TransitionSpatialMultiplierRasters As New Dictionary(Of String, StochasticTimeRaster)
    Private m_TransitionSpatialInitiationMultipliers As New TransitionSpatialInitiationMultiplierCollection
    Private m_TransitionSpatialInitiationMultiplierRasters As New Dictionary(Of String, StochasticTimeRaster)
    Private m_TransitionTargets As New TransitionTargetCollection
    Private m_TransitionOrders As New TransitionOrderCollection
    Private m_TransitionSizeDistributions As New TransitionSizeDistributionCollection
    Private m_TransitionSpreadDistributions As New TransitionSpreadDistributionCollection
    Private m_TransitionPatchPrioritizations As New TransitionPatchPrioritizationCollection
    Private m_TransitionSizePrioritizations As New TransitionSizePrioritizationCollection
    Private m_TransitionDirectionMultipliers As New TransitionDirectionMultiplierCollection
    Private m_TransitionSlopeMultipliers As New TransitionSlopeMultiplierCollection
    Private m_TransitionAdjacencySettings As New TransitionAdjacencySettingCollection
    Private m_TransitionAdjacencyMultipliers As New TransitionAdjacencyMultiplierCollection
    Private m_TransitionPathwayAutoCorrelations As New TransitionPathwayAutoCorrelationCollection
    Private m_StateAttributeValues As New StateAttributeValueCollection
    Private m_TransitionAttributeValues As New TransitionAttributeValueCollection
    Private m_TransitionAttributeTargets As New TransitionAttributeTargetCollection
    Private m_InputRasters As New InputRasters
    Private m_TransitionAttributeTypesWithTarget As New Dictionary(Of Integer, Boolean)

    ''' <summary>
    ''' Fills the cell collection
    ''' </summary>
    ''' <param name="numCells">The number of cells to create</param>
    ''' <remarks></remarks>
    Private Sub FillCellCollection(ByVal numCells As Integer)

        Debug.Assert(numCells > 0)
        Dim dstst As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_TST_GROUP_NAME)

        'Create a unique list of transition groups from those found in the Time Since Transition Groups property
        Dim dict As New Dictionary(Of Integer, Boolean)

        For Each dr As DataRow In dstst.GetData().Rows

            Dim id As Integer = CInt(dr(DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME))

            If (Not dict.ContainsKey(id)) Then
                dict.Add(id, True)
            End If

        Next

        'Create all the cells.  If there are Time Since Transition Groups then create the cell's TSTCollection
        'and add a TST for each one.  (We don't allocate the TSTCollection unless there are groups since there
        'can be a large number of cells)

        For CellId As Integer = 0 To numCells - 1

            If (Me.IsSpatial) Then

                'Only create a Cell in the Collection if Stratum or StateClass <>0, to conserve memory.
                If (Me.m_InputRasters.SClassCells(CellId) = 0 Or Me.m_InputRasters.StratumCells(CellId) = 0) Then
                    Continue For
                End If

            End If

            Dim SimulationCell As New Cell(CellId)

            If (dict.Count > 0) Then

                For Each tg As Integer In dict.Keys
                    SimulationCell.TstValues.Add(New Tst(tg))
                Next

            End If

            Me.m_Cells.Add(SimulationCell)

        Next

    End Sub

    ''' <summary>
    ''' Fills the Cell Collection for the model
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillCellCollection()

        Debug.Assert(Me.m_Cells.Count = 0)

        Dim NumCells As Integer

        If (Me.IsSpatial) Then
            NumCells = Me.m_InputRasters.NumberCells
        Else

            Dim drrc As DataRow = Me.ResultScenario.GetDataSheet(DATASHEET_NSIC_NAME).GetDataRow()
            NumCells = CInt(drrc(DATASHEET_NSIC_NUM_CELLS_COLUMN_NAME))

        End If

        Me.FillCellCollection(NumCells)

    End Sub

    ''' <summary>
    ''' Fills the model's stratum collection
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillStratumCollection()

        Debug.Assert(Me.m_Strata.Count = 0)
        Dim ds As DataSheet = Me.Project.GetDataSheet(DATASHEET_STRATA_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            Dim StratumId As Integer = CInt(dr(ds.PrimaryKeyColumn.Name))
            Me.m_Strata.Add(New Stratum(StratumId))

        Next

    End Sub

    ''' <summary>
    ''' Fills the model's secondary stratum collection
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillSecondaryStratumCollection()

        Debug.Assert(Me.m_SecondaryStrata.Count = 0)
        Dim ds As DataSheet = Me.Project.GetDataSheet(DATASHEET_SECONDARY_STRATA_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            Dim SecondaryStratumId As Integer = CInt(dr(ds.PrimaryKeyColumn.Name))
            Me.m_SecondaryStrata.Add(New Stratum(SecondaryStratumId))

        Next

    End Sub

    ''' <summary>
    ''' Fills the model's state class collection
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillStateClassCollection()

        Debug.Assert(Me.m_StateClasses.Count = 0)
        Dim ds As DataSheet = Me.Project.GetDataSheet(DATASHEET_STATECLASS_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            Dim id As Integer = CInt(dr(ds.PrimaryKeyColumn.Name))
            Dim slxid As Integer = CInt(dr(DATASHEET_STATECLASS_STATE_LABEL_X_ID_COLUMN_NAME))
            Dim slyid As Integer = CInt(dr(DATASHEET_STATECLASS_STATE_LABEL_Y_ID_COLUMN_NAME))

            Me.m_StateClasses.Add(New StateClass(id, slxid, slyid))

        Next

    End Sub

#If DEBUG Then
    Private TRANSITION_TYPES_FILLED As Boolean
    Private TRANSITION_GROUPS_FILLED As Boolean
#End If

    ''' <summary>
    ''' Fills the model's transition type collection
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillTransitionTypeCollection()

        Debug.Assert(Me.m_TransitionTypes.Count = 0)
        Dim ds As DataSheet = Me.Project.GetDataSheet(DATASHEET_TRANSITION_TYPE_NAME)

        Dim AtLeastOne As Boolean = False

        For Each dr As DataRow In ds.GetData.Rows

            Dim TransitionTypeId As Integer = CInt(dr(ds.PrimaryKeyColumn.Name))
            Dim DisplayName As String = CStr(dr(ds.DisplayMember))
            Dim MapId As Nullable(Of Integer) = Nothing

            If (dr(DATASHEET_MAPID_COLUMN_NAME) IsNot DBNull.Value) Then
                MapId = CInt(dr(DATASHEET_MAPID_COLUMN_NAME))
            End If

            If (MapId.HasValue) Then
                AtLeastOne = True
            End If

            Me.m_TransitionTypes.Add(
                New TransitionType(TransitionTypeId, DisplayName, MapId))

        Next

        If (Me.m_IsSpatial And (Not AtLeastOne) And Me.m_CreateRasterTransitionOutput) Then
            Me.RecordStatus(StatusType.Warning, "Spatial transition type output requested but no IDs specified for Transition Types.")
        End If

#If DEBUG Then
        TRANSITION_TYPES_FILLED = True
#End If

    End Sub

    ''' <summary>
    ''' Fills the model's transition group collection
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillTransitionGroupCollection()

        Debug.Assert(Me.m_TransitionGroups.Count = 0)
        Dim ds As DataSheet = Me.Project.GetDataSheet(DATASHEET_TRANSITION_GROUP_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            Me.m_TransitionGroups.Add(
                New TransitionGroup(
                    CInt(dr(ds.PrimaryKeyColumn.Name)),
                    CStr(dr("NAME")),
                    DataTableUtilities.GetDataBool(dr, IS_AUTO_COLUMN_NAME)))

        Next

#If DEBUG Then
        TRANSITION_GROUPS_FILLED = True
#End If

    End Sub

    ''' <summary>
    ''' Fills the Transition Type collection of Transition Groups
    ''' </summary>
    ''' <remarks>
    ''' This function must be called after _both_ the transition group and transition type collections have been filled
    ''' </remarks>
    Private Sub FillTransitionTypeGroupCollection()

#If DEBUG Then
        Debug.Assert(TRANSITION_TYPES_FILLED)
        Debug.Assert(TRANSITION_GROUPS_FILLED)
#End If

        Dim ds As DataSheet = Me.Project.GetDataSheet(DATASHEET_TRANSITION_TYPE_GROUP_NAME)

        For Each tt As TransitionType In Me.m_TransitionTypes

            Debug.Assert(tt.TransitionGroups.Count = 0)

            Dim query As String = String.Format(CultureInfo.InvariantCulture,
                    "{0}={1}", DATASHEET_TRANSITION_TYPE_ID_COLUMN_NAME, tt.TransitionTypeId)

            Dim rows() As DataRow = ds.GetData().Select(query)
            Dim HasNonAutoPrimaryGroup As Boolean = False

            'When adding the transition groups, we only want to add an auto-generated
            'primary transition group if there is not already a user-defined one. To ensure
            'this we loop over the groups twice with the user-defined ones taking precedcence.

            'User Defined

            For Each dr As DataRow In rows

                Dim tgid As Integer = CInt(dr(DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME))
                Dim tg As TransitionGroup = Me.m_TransitionGroups(tgid)

                If (Not tg.IsAuto) Then

                    Debug.Assert(Not tt.TransitionGroups.Contains(tgid))
                    tt.TransitionGroups.Add(tg)

                    If (IsPrimaryTypeByGroup(dr)) Then

                        tt.PrimaryTransitionGroups.Add(tg)
                        HasNonAutoPrimaryGroup = True

                    End If

                End If

            Next

            'Auto Generated

            For Each dr As DataRow In rows

                Dim tgid As Integer = CInt(dr(DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME))
                Dim tg As TransitionGroup = Me.m_TransitionGroups(tgid)

                If (tg.IsAuto) Then

                    Debug.Assert(Not tt.TransitionGroups.Contains(tgid))
                    tt.TransitionGroups.Add(tg)

                    If (IsPrimaryTypeByGroup(dr) And (Not HasNonAutoPrimaryGroup)) Then
                        tt.PrimaryTransitionGroups.Add(tg)
                    End If

                End If

            Next

            If (tt.PrimaryTransitionGroups.Count > 1) Then

                Dim msg As String = String.Format(CultureInfo.CurrentCulture,
                    "The transition type '{0}' has more than one primary transition group.", tt.DisplayName)

                Me.RecordStatus(StatusType.Warning, msg)

            End If

        Next

    End Sub

    ''' <summary>
    ''' Fills the Transition Group collection of Transition Types
    ''' </summary>
    ''' <remarks>
    ''' This function must be called after _both_ the transition group and transition type collections have been filled
    ''' </remarks>
    Private Sub FillTransitionGroupTypeCollection()

#If DEBUG Then
        Debug.Assert(TRANSITION_TYPES_FILLED)
        Debug.Assert(TRANSITION_GROUPS_FILLED)
#End If

        Dim ds As DataSheet = Me.Project.GetDataSheet(DATASHEET_TRANSITION_TYPE_GROUP_NAME)

        'We only want to add a type to an auto-generated group if that type is not already
        'a primary type in a user-defined group.  To ensure this, we loop over the groups
        'twice with the user-defined ones taking precedcence.

        Dim d As New Dictionary(Of Integer, Boolean)

        'User defined

        For Each tg As TransitionGroup In Me.m_TransitionGroups

            If (Not tg.IsAuto) Then

                Debug.Assert(tg.TransitionTypes.Count = 0)

                Dim query As String = String.Format(CultureInfo.InvariantCulture,
                    "{0}={1}", DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME, tg.TransitionGroupId)

                Dim rows() As DataRow = ds.GetData().Select(query)

                For Each dr As DataRow In rows

                    Dim ttid As Integer = CInt(dr(DATASHEET_TRANSITION_TYPE_ID_COLUMN_NAME))
                    Dim tt As TransitionType = Me.m_TransitionTypes(ttid)

                    Debug.Assert(Not tg.TransitionTypes.Contains(ttid))
                    Debug.Assert(Not tg.PrimaryTransitionTypes.Contains(ttid))

                    tg.TransitionTypes.Add(tt)

                    If (IsPrimaryTypeByGroup(dr)) Then

                        tg.PrimaryTransitionTypes.Add(tt)

                        If (Not d.ContainsKey(tt.TransitionTypeId)) Then
                            d.Add(tt.TransitionTypeId, True)
                        End If

                    End If

                Next

            End If

        Next

        'Auto Generated

        For Each tg As TransitionGroup In Me.m_TransitionGroups

            If (tg.IsAuto) Then

                Debug.Assert(tg.TransitionTypes.Count = 0)

                Dim query As String = String.Format(CultureInfo.InvariantCulture,
                    "{0}={1}", DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME, tg.TransitionGroupId)

                Dim rows() As DataRow = ds.GetData().Select(query)

                For Each dr As DataRow In rows

                    Dim ttid As Integer = CInt(dr(DATASHEET_TRANSITION_TYPE_ID_COLUMN_NAME))
                    Dim tt As TransitionType = Me.m_TransitionTypes(ttid)

                    Debug.Assert(Not tg.TransitionTypes.Contains(ttid))
                    Debug.Assert(Not tg.PrimaryTransitionTypes.Contains(ttid))

                    tg.TransitionTypes.Add(tt)

                    If (IsPrimaryTypeByGroup(dr) And (Not d.ContainsKey(tt.TransitionTypeId))) Then
                        tg.PrimaryTransitionTypes.Add(tt)
                    End If

                Next

            End If

        Next

    End Sub

    ''' <summary>
    ''' Fills the Transition Multiplier Type Collection
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillTransitionMultiplierTypeCollection()

        Debug.Assert(Me.m_TransitionMultiplierTypes.Count = 0)
        Dim ds As DataSheet = Me.Project.GetDataSheet(DATASHEET_TRANSITION_MULTIPLIER_TYPE_NAME)

        'Always add type with a Null Id because transition multipliers can have null types.
        Me.m_TransitionMultiplierTypes.Add(New TransitionMultiplierType(Nothing, Me.ResultScenario, Me.m_DistributionProvider))

        For Each dr As DataRow In ds.GetData.Rows

            Dim TransitionMultiplierTypeId As Integer = CInt(dr(ds.PrimaryKeyColumn.Name))

            Me.m_TransitionMultiplierTypes.Add(
                New TransitionMultiplierType(
                    TransitionMultiplierTypeId,
                    Me.ResultScenario,
                    Me.m_DistributionProvider))

        Next

    End Sub

    ''' <summary>
    ''' Fills the Transition Attribute Type collection
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillTransitionAttributeTypeCollection()

        Debug.Assert(Me.m_TransitionAttributeTypes.Count = 0)
        Dim ds As DataSheet = Me.Project.GetDataSheet(DATASHEET_TRANSITION_ATTRIBUTE_TYPE_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            Dim TransitionAttributeTypeId As Integer = CInt(dr(ds.PrimaryKeyColumn.Name))
            Me.m_TransitionAttributeTypes.Add(New TransitionAttributeType(TransitionAttributeTypeId))

        Next

    End Sub

    ''' <summary>
    ''' Fills the Patch Prioritization Collection
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillPatchPrioritizationCollection()

        Debug.Assert(Me.IsSpatial)
        Debug.Assert(Me.m_PatchPrioritizations.Count = 0)

        Dim ds As DataSheet = Me.Project.GetDataSheet(DATASHEET_PATCH_PRIORITIZATION_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            Dim Id As Integer = CInt(dr(ds.PrimaryKeyColumn.Name))
            Dim Name As String = CStr(dr(ds.DisplayMember))

            Dim t As PatchPrioritizationType

            If (Name = PATCH_PRIORITIZATION_SMALLEST) Then
                t = PatchPrioritizationType.Smallest
            ElseIf (Name = PATCH_PRIORITIZATION_SMALLEST_EDGES_ONLY) Then
                t = PatchPrioritizationType.SmallestEdgesOnly
            ElseIf (Name = PATCH_PRIORITIZATION_LARGEST) Then
                t = PatchPrioritizationType.Largest
            ElseIf (Name = PATCH_PRIORITIZATION_LARGEST_EDGES_ONLY) Then
                t = PatchPrioritizationType.LargestEdgesOnly
            Else
                ExceptionUtils.ThrowInvalidOperationException("The patch prioritization '{0}' is not valid", Name)
            End If

            Dim pp As New PatchPrioritization(Id, t)
            Me.m_PatchPrioritizations.Add(pp)

        Next

    End Sub

    ''' <summary>
    ''' Fills the initial conditions distribution collection
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillInitialConditionsDistributionMap()

        Me.m_InitialConditionsDistributions.Clear()

        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_NSIC_DISTRIBUTION_NAME)

        If (ds.GetData.Rows.Count = 0) Then
            Throw New ArgumentException(ERROR_NO_INITIAL_CONDITIONS_DISTRIBUTION_RECORDS)
        End If

        For Each dr As DataRow In ds.GetData.Rows

            Dim StateClassId As Integer = CInt(dr(DATASHEET_STATECLASS_ID_COLUMN_NAME))
            Dim StratumId As Integer = CInt(dr(DATASHEET_STRATUM_ID_COLUMN_NAME))
            Dim Iteration As Nullable(Of Integer) = Nothing
            Dim SecondaryStratumId As Nullable(Of Integer) = Nothing
            Dim AgeMin As Integer = 0
            Dim AgeMax As Integer = Integer.MaxValue
            Dim RelativeAmount As Double = CDbl(dr(DATASHEET_NSIC_DISTRIBUTION_RELATIVE_AMOUNT_COLUMN_NAME))

            If (dr(DATASHEET_ITERATION_COLUMN_NAME) IsNot DBNull.Value) Then
                Iteration = CInt(dr(DATASHEET_ITERATION_COLUMN_NAME))
            End If

            If (dr(DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                SecondaryStratumId = CInt(dr(DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_AGE_MIN_COLUMN_NAME) IsNot DBNull.Value) Then
                AgeMin = CInt(dr(DATASHEET_AGE_MIN_COLUMN_NAME))
            End If

            If (dr(DATASHEET_AGE_MAX_COLUMN_NAME) IsNot DBNull.Value) Then
                AgeMax = CInt(dr(DATASHEET_AGE_MAX_COLUMN_NAME))
            End If

            Dim InitialStateRecord As New InitialConditionsDistribution(
                StratumId, Iteration, SecondaryStratumId, StateClassId, AgeMin, AgeMax, RelativeAmount)

            Me.m_InitialConditionsDistributions.Add(InitialStateRecord)
        Next

        Me.m_InitialConditionsDistributionMap = New InitialConditionsDistributionMap(Me.m_InitialConditionsDistributions)

    End Sub

    ''' <summary>
    ''' Fills the initial conditions spatial collection
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillInitialConditionsSpatialMap()

        Me.m_InitialConditionsSpatials.Clear()

        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_SPIC_NAME)

        If (ds.GetData.Rows.Count = 0) Then
            Throw New ArgumentException(ERROR_NO_INITIAL_CONDITIONS_SPATIAL_RECORDS)
        End If

        For Each dr As DataRow In ds.GetData.Rows

            Dim Iteration As Nullable(Of Integer) = Nothing
            Dim PrimaryStratumName As String
            Dim SecondaryStratumName As String = ""
            Dim StateClassName As String
            Dim AgeName As String = ""

            If (dr(DATASHEET_ITERATION_COLUMN_NAME) IsNot DBNull.Value) Then
                Iteration = CInt(dr(DATASHEET_ITERATION_COLUMN_NAME))
            End If

            PrimaryStratumName = dr(DATASHEET_SPIC_STRATUM_FILE_COLUMN_NAME).ToString()
            SecondaryStratumName = dr(DATASHEET_SPIC_SECONDARY_STRATUM_FILE_COLUMN_NAME).ToString()
            StateClassName = dr(DATASHEET_SPIC_STATE_CLASS_FILE_COLUMN_NAME).ToString()
            AgeName = dr(DATASHEET_SPIC_AGE_FILE_COLUMN_NAME).ToString()

            Dim InitialStateRecord As New InitialConditionsSpatial(
                Iteration, PrimaryStratumName, SecondaryStratumName, StateClassName, AgeName)

            Me.m_InitialConditionsSpatials.Add(InitialStateRecord)
        Next

        Me.m_InitialConditionsSpatialMap = New InitialConditionsSpatialMap(Me.m_InitialConditionsSpatials)

    End Sub

    ''' <summary>
    ''' Fills the Deterministic Transitions Collection
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillDeterministicTransitionsCollection()

        Debug.Assert(Me.m_DeterministicTransitions.Count = 0)
        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_DT_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            Dim Iteration As Nullable(Of Integer) = Nothing
            Dim Timestep As Nullable(Of Integer) = Nothing
            Dim StratumIdSource As Nullable(Of Integer) = Nothing
            Dim StateClassIdSource As Integer = CInt(dr(DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME))
            Dim StratumIdDest As Nullable(Of Integer) = Nothing
            Dim StateClassIdDest As Nullable(Of Integer) = StateClassIdSource
            Dim AgeMin As Integer = 0
            Dim AgeMax As Integer = Integer.MaxValue

            If (dr(DATASHEET_ITERATION_COLUMN_NAME) IsNot DBNull.Value) Then
                Iteration = CType(dr(DATASHEET_ITERATION_COLUMN_NAME), Integer)
            End If

            If (dr(DATASHEET_TIMESTEP_COLUMN_NAME) IsNot DBNull.Value) Then
                Timestep = CType(dr(DATASHEET_TIMESTEP_COLUMN_NAME), Integer)
            End If

            If (dr(DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME) IsNot DBNull.Value) Then
                StratumIdSource = CInt(dr(DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME))
            End If

            If (dr(DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME) IsNot DBNull.Value) Then
                StratumIdDest = CInt(dr(DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME))
            End If

            If (dr(DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME) IsNot DBNull.Value) Then
                StateClassIdDest = CInt(dr(DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME))
            End If

            If (dr(DATASHEET_AGE_MIN_COLUMN_NAME) IsNot DBNull.Value) Then
                AgeMin = CInt(dr(DATASHEET_AGE_MIN_COLUMN_NAME))
            End If

            If (dr(DATASHEET_AGE_MAX_COLUMN_NAME) IsNot DBNull.Value) Then
                AgeMax = CInt(dr(DATASHEET_AGE_MAX_COLUMN_NAME))
            End If

            Dim dt As New DeterministicTransition(
                Iteration,
                Timestep,
                StratumIdSource,
                StateClassIdSource,
                StratumIdDest,
                StateClassIdDest,
                AgeMin,
                AgeMax)

            Me.m_DeterministicTransitions.Add(dt)

        Next

    End Sub

    ''' <summary>
    ''' Fills the Probabilistic Transitions Collection for the model
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillProbabilisticTransitionsCollection()

        Debug.Assert(Me.m_Transitions.Count = 0)
        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_PT_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            Dim Iteration As Nullable(Of Integer) = Nothing
            Dim Timestep As Nullable(Of Integer) = Nothing
            Dim StratumIdSource As Nullable(Of Integer) = Nothing
            Dim StateClassIdSource As Integer = CInt(dr(DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME))
            Dim StratumIdDest As Nullable(Of Integer) = Nothing
            Dim StateClassIdDest As Nullable(Of Integer) = StateClassIdSource
            Dim TransitionTypeId As Integer = CInt(dr(DATASHEET_TRANSITION_TYPE_ID_COLUMN_NAME))
            Dim Probability As Double = CDbl(dr(DATASHEET_PT_PROBABILITY_COLUMN_NAME))
            Dim Proportion As Double = 1.0
            Dim AgeMinimum As Integer = 0
            Dim AgeMaximum As Integer = Integer.MaxValue
            Dim AgeRelative As Integer = 0
            Dim AgeReset As Boolean = True
            Dim TstMinimum As Integer = 0
            Dim TstMaximum As Integer = Integer.MaxValue
            Dim TstRelative As Integer = (-Integer.MaxValue)

            If (dr(DATASHEET_ITERATION_COLUMN_NAME) IsNot DBNull.Value) Then
                Iteration = CType(dr(DATASHEET_ITERATION_COLUMN_NAME), Integer)
            End If

            If (dr(DATASHEET_TIMESTEP_COLUMN_NAME) IsNot DBNull.Value) Then
                Timestep = CType(dr(DATASHEET_TIMESTEP_COLUMN_NAME), Integer)
            End If

            If (dr(DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME) IsNot DBNull.Value) Then
                StratumIdSource = CInt(dr(DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME))
            End If

            If (dr(DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME) IsNot DBNull.Value) Then
                StratumIdDest = CInt(dr(DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME))
            End If

            If (dr(DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME) IsNot DBNull.Value) Then
                StateClassIdDest = CInt(dr(DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME))
            End If

            If (dr(DATASHEET_PT_PROPORTION_COLUMN_NAME) IsNot DBNull.Value) Then
                Proportion = CDbl(dr(DATASHEET_PT_PROPORTION_COLUMN_NAME))
            End If

            If (dr(DATASHEET_AGE_MIN_COLUMN_NAME) IsNot DBNull.Value) Then
                AgeMinimum = CInt(dr(DATASHEET_AGE_MIN_COLUMN_NAME))
            End If

            If (dr(DATASHEET_AGE_MAX_COLUMN_NAME) IsNot DBNull.Value) Then
                AgeMaximum = CInt(dr(DATASHEET_AGE_MAX_COLUMN_NAME))
            End If

            If (dr(DATASHEET_PT_AGE_RELATIVE_COLUMN_NAME) IsNot DBNull.Value) Then
                AgeRelative = CInt(dr(DATASHEET_PT_AGE_RELATIVE_COLUMN_NAME))
            End If

            If (dr(DATASHEET_PT_AGE_RESET_COLUMN_NAME) IsNot DBNull.Value) Then
                AgeReset = CBool(dr(DATASHEET_PT_AGE_RESET_COLUMN_NAME))
            End If

            If (dr(DATASHEET_PT_TST_MIN_COLUMN_NAME) IsNot DBNull.Value) Then
                TstMinimum = CInt(dr(DATASHEET_PT_TST_MIN_COLUMN_NAME))
            End If

            If (dr(DATASHEET_PT_TST_MAX_COLUMN_NAME) IsNot DBNull.Value) Then
                TstMaximum = CInt(dr(DATASHEET_PT_TST_MAX_COLUMN_NAME))
            End If

            If (dr(DATASHEET_PT_TST_RELATIVE_COLUMN_NAME) IsNot DBNull.Value) Then
                TstRelative = CInt(dr(DATASHEET_PT_TST_RELATIVE_COLUMN_NAME))
            End If

            Dim pt As New Transition(
                Iteration,
                Timestep,
                StratumIdSource,
                StateClassIdSource,
                StratumIdDest,
                StateClassIdDest,
                TransitionTypeId,
                Probability,
                Proportion,
                AgeMinimum,
                AgeMaximum,
                AgeRelative,
                AgeReset,
                TstMinimum,
                TstMaximum,
                TstRelative)

            Me.m_Transitions.Add(pt)

        Next

    End Sub

    ''' <summary>
    ''' Fills the State Attribute Value Collection for the model
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillStateAttributeValueCollection()

        Debug.Assert(Me.m_StateAttributeValues.Count = 0)
        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_STATE_ATTRIBUTE_VALUE_NAME)
        Dim HasAges As New Dictionary(Of Integer, Boolean)

        'If some attribute types have ages and some don't then we want to configure any that don't 
        'with default values or they will not be included in the calculations.

        For Each dr As DataRow In ds.GetData.Rows

            If (dr(DATASHEET_AGE_MIN_COLUMN_NAME) IsNot DBNull.Value Or
                dr(DATASHEET_AGE_MAX_COLUMN_NAME) IsNot DBNull.Value) Then

                Dim StateAttributeTypeId As Integer = CInt(dr(DATASHEET_STATE_ATTRIBUTE_TYPE_ID_COLUMN_NAME))

                If (Not HasAges.ContainsKey(StateAttributeTypeId)) Then
                    HasAges.Add(StateAttributeTypeId, True)
                End If

            End If

        Next

        For Each dr As DataRow In ds.GetData.Rows

            Dim StateAttributeTypeId As Integer = CInt(dr(DATASHEET_STATE_ATTRIBUTE_TYPE_ID_COLUMN_NAME))

            If (HasAges.ContainsKey(StateAttributeTypeId)) Then

                If (dr(DATASHEET_AGE_MIN_COLUMN_NAME) Is DBNull.Value) Then
                    dr(DATASHEET_AGE_MIN_COLUMN_NAME) = 0
                End If

                If (dr(DATASHEET_AGE_MAX_COLUMN_NAME) Is DBNull.Value) Then
                    dr(DATASHEET_AGE_MAX_COLUMN_NAME) = Integer.MaxValue
                End If

            End If

        Next

        Dim StratumOrStateClassWarningIssued As Boolean = False

        For Each dr As DataRow In ds.GetData.Rows

            Dim StateAttributeTypeId As Integer = CInt(dr(DATASHEET_STATE_ATTRIBUTE_TYPE_ID_COLUMN_NAME))
            Dim StratumId As Nullable(Of Integer) = Nothing
            Dim SecondaryStratumId As Nullable(Of Integer) = Nothing
            Dim Iteration As Nullable(Of Integer) = Nothing
            Dim Timestep As Nullable(Of Integer) = Nothing
            Dim StateClassId As Nullable(Of Integer) = Nothing
            Dim AgeMin As Nullable(Of Integer) = Nothing
            Dim AgeMax As Nullable(Of Integer) = Nothing
            Dim Value As Double = CDbl(dr(DATASHEET_STATE_ATTRIBUTE_VALUE_VALUE_COLUMN_NAME))

            If (dr(DATASHEET_STRATUM_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                StratumId = CInt(dr(DATASHEET_STRATUM_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                SecondaryStratumId = CInt(dr(DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_ITERATION_COLUMN_NAME) IsNot DBNull.Value) Then
                Iteration = CType(dr(DATASHEET_ITERATION_COLUMN_NAME), Integer)
            End If

            If (dr(DATASHEET_TIMESTEP_COLUMN_NAME) IsNot DBNull.Value) Then
                Timestep = CType(dr(DATASHEET_TIMESTEP_COLUMN_NAME), Integer)
            End If

            If (dr(DATASHEET_STATECLASS_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                StateClassId = CInt(dr(DATASHEET_STATECLASS_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_AGE_MIN_COLUMN_NAME) IsNot DBNull.Value) Then
                AgeMin = CInt(dr(DATASHEET_AGE_MIN_COLUMN_NAME))
            End If

            If (dr(DATASHEET_AGE_MAX_COLUMN_NAME) IsNot DBNull.Value) Then
                AgeMax = CInt(dr(DATASHEET_AGE_MAX_COLUMN_NAME))
            End If

            If (Not StratumId.HasValue And Not StateClassId.HasValue) Then

                If (Not StratumOrStateClassWarningIssued) Then

                    Me.RecordStatus(StatusType.Information,
                        "At least one State Attribute Value had neither a stratum nor a state class.")

                    StratumOrStateClassWarningIssued = True

                End If

            End If

            Dim attr As New StateAttributeValue(
                StateAttributeTypeId, StratumId, SecondaryStratumId, Iteration, Timestep,
                StateClassId, AgeMin, AgeMax, Value)

            Me.m_StateAttributeValues.Add(attr)

        Next

    End Sub

    ''' <summary>
    ''' Fills the Transition Attribute Value Collection for the model
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillTransitionAttributeValueCollection()

        Debug.Assert(Me.m_TransitionAttributeValues.Count = 0)
        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_TRANSITION_ATTRIBUTE_VALUE_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            Dim TransitionAttributeTypeId As Integer = CInt(dr(DATASHEET_TRANSITION_ATTRIBUTE_TYPE_ID_COLUMN_NAME))
            Dim StratumId As Nullable(Of Integer) = Nothing
            Dim SecondaryStratumId As Nullable(Of Integer) = Nothing
            Dim Iteration As Nullable(Of Integer) = Nothing
            Dim Timestep As Nullable(Of Integer) = Nothing
            Dim TransitionGroupId As Integer = CInt(dr(DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME))
            Dim StateClassId As Nullable(Of Integer) = Nothing
            Dim AgeMin As Nullable(Of Integer) = Nothing
            Dim AgeMax As Nullable(Of Integer) = Nothing
            Dim Value As Double = CDbl(dr(DATASHEET_STATE_ATTRIBUTE_VALUE_VALUE_COLUMN_NAME))

            If (dr(DATASHEET_STRATUM_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                StratumId = CInt(dr(DATASHEET_STRATUM_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                SecondaryStratumId = CInt(dr(DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_ITERATION_COLUMN_NAME) IsNot DBNull.Value) Then
                Iteration = CType(dr(DATASHEET_ITERATION_COLUMN_NAME), Integer)
            End If

            If (dr(DATASHEET_TIMESTEP_COLUMN_NAME) IsNot DBNull.Value) Then
                Timestep = CType(dr(DATASHEET_TIMESTEP_COLUMN_NAME), Integer)
            End If

            If (dr(DATASHEET_STATECLASS_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                StateClassId = CInt(dr(DATASHEET_STATECLASS_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_AGE_MIN_COLUMN_NAME) IsNot DBNull.Value) Then
                AgeMin = CInt(dr(DATASHEET_AGE_MIN_COLUMN_NAME))
            End If

            If (dr(DATASHEET_AGE_MAX_COLUMN_NAME) IsNot DBNull.Value) Then
                AgeMax = CInt(dr(DATASHEET_AGE_MAX_COLUMN_NAME))
            End If

            Dim attr As New TransitionAttributeValue(
                TransitionAttributeTypeId, StratumId, SecondaryStratumId, Iteration,
                Timestep, TransitionGroupId, StateClassId, AgeMin, AgeMax, Value)

            Me.m_TransitionAttributeValues.Add(attr)

        Next

    End Sub

    ''' <summary>
    ''' Fills the TST Transition Group collection for the model
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillTstTransitionGroupCollection()

        Debug.Assert(Me.m_TstTransitionGroupMap Is Nothing)

        Me.m_TstTransitionGroupMap = New TstTransitionGroupMap(Me.ResultScenario)
        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_TST_GROUP_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            Dim StratumId As Nullable(Of Integer) = Nothing
            Dim SecondaryStratumId As Nullable(Of Integer) = Nothing
            Dim TransitionTypeId As Integer = CInt(dr(DATASHEET_TRANSITION_TYPE_ID_COLUMN_NAME))
            Dim TransitionGroupId As Integer = CInt(dr(DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME))

            If (dr(DATASHEET_STRATUM_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                StratumId = CInt(dr(DATASHEET_STRATUM_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                SecondaryStratumId = CInt(dr(DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME))
            End If

            Me.m_TstTransitionGroupMap.AddGroup(
                TransitionTypeId,
                StratumId,
                SecondaryStratumId,
                New TstTransitionGroup(TransitionGroupId))

        Next

    End Sub

    ''' <summary>
    ''' Fills the Randomize TST collection for the model
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillTstRandomizeCollection()

        Debug.Assert(Me.m_TstRandomizeMap Is Nothing)

        Me.m_TstRandomizeMap = New TstRandomizeMap(Me.ResultScenario)
        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_TST_RANDOMIZE_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            Dim StratumId As Nullable(Of Integer) = Nothing
            Dim SecondaryStratumId As Nullable(Of Integer) = Nothing
            Dim TransitionGroupId As Nullable(Of Integer) = Nothing
            Dim StateClassId As Nullable(Of Integer) = Nothing
            Dim Iteration As Nullable(Of Integer) = Nothing
            Dim MinInitialTST As Integer
            Dim MaxInitialTST As Integer

            If (dr(DATASHEET_ITERATION_COLUMN_NAME) IsNot DBNull.Value) Then
                Iteration = CType(dr(DATASHEET_ITERATION_COLUMN_NAME), Integer)
            End If

            If (dr(DATASHEET_STRATUM_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                StratumId = CInt(dr(DATASHEET_STRATUM_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                SecondaryStratumId = CInt(dr(DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                TransitionGroupId = CInt(dr(DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_STATECLASS_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                StateClassId = CInt(dr(DATASHEET_STATECLASS_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_TST_RANDOMIZE_MIN_INITIAL_TST_COLUMN_NAME) IsNot DBNull.Value) Then
                MinInitialTST = CInt(dr(DATASHEET_TST_RANDOMIZE_MIN_INITIAL_TST_COLUMN_NAME))
            Else
                MinInitialTST = 0
            End If

            If (dr(DATASHEET_TST_RANDOMIZE_MAX_INITIAL_TST_COLUMN_NAME) IsNot DBNull.Value) Then
                MaxInitialTST = CInt(dr(DATASHEET_TST_RANDOMIZE_MAX_INITIAL_TST_COLUMN_NAME))
            Else
                MaxInitialTST = Integer.MaxValue
            End If

            Me.m_TstRandomizeMap.AddTstRandomize(
                TransitionGroupId,
                StratumId,
                SecondaryStratumId,
                StateClassId,
                Iteration,
                New TstRandomize(MinInitialTST, MaxInitialTST))

        Next

    End Sub

    ''' <summary>
    ''' Fills the TransitionOrder collection
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillTransitionOrderCollection()

        Debug.Assert(Me.m_TransitionOrders.Count = 0)
        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_TRANSITION_ORDER_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            Dim TransitionGroupId As Integer = CInt(dr(DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME))
            Dim Iteration As Nullable(Of Integer) = Nothing
            Dim Timestep As Nullable(Of Integer) = Nothing
            Dim Order As Nullable(Of Double) = Nothing

            If (dr(DATASHEET_ITERATION_COLUMN_NAME) IsNot DBNull.Value) Then
                Iteration = CInt(dr(DATASHEET_ITERATION_COLUMN_NAME))
            End If

            If (dr(DATASHEET_TIMESTEP_COLUMN_NAME) IsNot DBNull.Value) Then
                Timestep = CInt(dr(DATASHEET_TIMESTEP_COLUMN_NAME))
            End If

            If (dr(DATASHEET_TRANSITION_ORDER_ORDER_COLUMN_NAME) IsNot DBNull.Value) Then
                Order = CDbl(dr(DATASHEET_TRANSITION_ORDER_ORDER_COLUMN_NAME))
            End If

            Me.m_TransitionOrders.Add(
                New TransitionOrder(TransitionGroupId, Iteration, Timestep, Order))

        Next

    End Sub

    ''' <summary>
    ''' Fills the transition target collection
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillTransitionTargetCollection()

        Debug.Assert(Me.m_TransitionTargets.Count = 0)
        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_TRANSITION_TARGET_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            Dim Iteration As Nullable(Of Integer) = Nothing
            Dim Timestep As Nullable(Of Integer) = Nothing
            Dim StratumId As Nullable(Of Integer) = Nothing
            Dim SecondaryStratumId As Nullable(Of Integer) = Nothing
            Dim TransitionGroupId As Integer = CInt(dr(DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME))
            Dim TargetAmount As Nullable(Of Double) = Nothing
            Dim DistributionTypeId As Nullable(Of Integer) = Nothing
            Dim DistributionFrequency As Nullable(Of DistributionFrequency) = Nothing
            Dim DistributionSD As Nullable(Of Double) = Nothing
            Dim DistributionMin As Nullable(Of Double) = Nothing
            Dim DistributionMax As Nullable(Of Double) = Nothing

            If (dr(DATASHEET_ITERATION_COLUMN_NAME) IsNot DBNull.Value) Then
                Iteration = CInt(dr(DATASHEET_ITERATION_COLUMN_NAME))
            End If

            If (dr(DATASHEET_TIMESTEP_COLUMN_NAME) IsNot DBNull.Value) Then
                Timestep = CInt(dr(DATASHEET_TIMESTEP_COLUMN_NAME))
            End If

            If (dr(DATASHEET_STRATUM_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                StratumId = CInt(dr(DATASHEET_STRATUM_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                SecondaryStratumId = CInt(dr(DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_AMOUNT_COLUMN_NAME) IsNot DBNull.Value) Then
                TargetAmount = CDbl(dr(DATASHEET_AMOUNT_COLUMN_NAME))
            End If

            If (dr(DATASHEET_DISTRIBUTIONTYPE_COLUMN_NAME) IsNot DBNull.Value) Then
                DistributionTypeId = CInt(dr(DATASHEET_DISTRIBUTIONTYPE_COLUMN_NAME))
            End If

            If (dr(DATASHEET_DISTRIBUTION_FREQUENCY_COLUMN_NAME) IsNot DBNull.Value) Then
                DistributionFrequency = CType(dr(DATASHEET_DISTRIBUTION_FREQUENCY_COLUMN_NAME), DistributionFrequency)
            End If

            If (dr(DATASHEET_DISTRIBUTIONSD_COLUMN_NAME) IsNot DBNull.Value) Then
                DistributionSD = CDbl(dr(DATASHEET_DISTRIBUTIONSD_COLUMN_NAME))
            End If

            If (dr(DATASHEET_DISTRIBUTIONMIN_COLUMN_NAME) IsNot DBNull.Value) Then
                DistributionMin = CDbl(dr(DATASHEET_DISTRIBUTIONMIN_COLUMN_NAME))
            End If

            If (dr(DATASHEET_DISTRIBUTIONMAX_COLUMN_NAME) IsNot DBNull.Value) Then
                DistributionMax = CDbl(dr(DATASHEET_DISTRIBUTIONMAX_COLUMN_NAME))
            End If

            Try

                Dim Item As New TransitionTarget(
                    Iteration,
                    Timestep,
                    StratumId,
                    SecondaryStratumId,
                    TransitionGroupId,
                    TargetAmount,
                    DistributionTypeId,
                    DistributionFrequency,
                    DistributionSD,
                    DistributionMin,
                    DistributionMax)

                Me.m_DistributionProvider.Validate(
                    Item.DistributionTypeId,
                    Item.DistributionValue,
                    Item.DistributionSD,
                    Item.DistributionMin,
                    Item.DistributionMax)

                Me.m_TransitionTargets.Add(Item)

            Catch ex As Exception
                Throw New ArgumentException(ds.DisplayName & " -> " & ex.Message)
            End Try

        Next

    End Sub

    ''' <summary>
    ''' Fills the transition attribute target collection model
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillTransitionAttributeTargetCollection()

        Debug.Assert(Me.m_TransitionAttributeTargets.Count = 0)
        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_TRANSITION_ATTRIBUTE_TARGET_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            Dim TransitionAttributeTargetId As Integer = CInt(dr(ds.PrimaryKeyColumn.Name))
            Dim Iteration As Nullable(Of Integer) = Nothing
            Dim Timestep As Nullable(Of Integer) = Nothing
            Dim StratumId As Nullable(Of Integer) = Nothing
            Dim SecondaryStratumId As Nullable(Of Integer) = Nothing
            Dim TransitionAttributeTypeId As Integer = CInt(dr(DATASHEET_TRANSITION_ATTRIBUTE_TYPE_ID_COLUMN_NAME))
            Dim TargetAmount As Nullable(Of Double) = Nothing
            Dim DistributionTypeId As Nullable(Of Integer) = Nothing
            Dim DistributionFrequency As Nullable(Of DistributionFrequency) = Nothing
            Dim DistributionSD As Nullable(Of Double) = Nothing
            Dim DistributionMin As Nullable(Of Double) = Nothing
            Dim DistributionMax As Nullable(Of Double) = Nothing

            If (dr(DATASHEET_ITERATION_COLUMN_NAME) IsNot DBNull.Value) Then
                Iteration = CInt(dr(DATASHEET_ITERATION_COLUMN_NAME))
            End If

            If (dr(DATASHEET_TIMESTEP_COLUMN_NAME) IsNot DBNull.Value) Then
                Timestep = CInt(dr(DATASHEET_TIMESTEP_COLUMN_NAME))
            End If

            If (dr(DATASHEET_STRATUM_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                StratumId = CInt(dr(DATASHEET_STRATUM_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                SecondaryStratumId = CInt(dr(DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_AMOUNT_COLUMN_NAME) IsNot DBNull.Value) Then
                TargetAmount = CDbl(dr(DATASHEET_AMOUNT_COLUMN_NAME))
            End If

            If (dr(DATASHEET_DISTRIBUTIONTYPE_COLUMN_NAME) IsNot DBNull.Value) Then
                DistributionTypeId = CInt(dr(DATASHEET_DISTRIBUTIONTYPE_COLUMN_NAME))
            End If

            If (dr(DATASHEET_DISTRIBUTION_FREQUENCY_COLUMN_NAME) IsNot DBNull.Value) Then
                DistributionFrequency = CType(dr(DATASHEET_DISTRIBUTION_FREQUENCY_COLUMN_NAME), DistributionFrequency)
            End If

            If (dr(DATASHEET_DISTRIBUTIONSD_COLUMN_NAME) IsNot DBNull.Value) Then
                DistributionSD = CDbl(dr(DATASHEET_DISTRIBUTIONSD_COLUMN_NAME))
            End If

            If (dr(DATASHEET_DISTRIBUTIONMIN_COLUMN_NAME) IsNot DBNull.Value) Then
                DistributionMin = CDbl(dr(DATASHEET_DISTRIBUTIONMIN_COLUMN_NAME))
            End If

            If (dr(DATASHEET_DISTRIBUTIONMAX_COLUMN_NAME) IsNot DBNull.Value) Then
                DistributionMax = CDbl(dr(DATASHEET_DISTRIBUTIONMAX_COLUMN_NAME))
            End If

            Try

                Dim Item As New TransitionAttributeTarget(
                    TransitionAttributeTargetId,
                    Iteration,
                    Timestep,
                    StratumId,
                    SecondaryStratumId,
                    TransitionAttributeTypeId,
                    TargetAmount,
                    DistributionTypeId,
                    DistributionFrequency,
                    DistributionSD,
                    DistributionMin,
                    DistributionMax)

                Me.m_DistributionProvider.Validate(
                    Item.DistributionTypeId,
                    Item.DistributionValue,
                    Item.DistributionSD,
                    Item.DistributionMin,
                    Item.DistributionMax)

                Me.m_TransitionAttributeTargets.Add(Item)

                If Not (Me.m_TransitionAttributeTypesWithTarget.ContainsKey(TransitionAttributeTypeId)) Then
                    Me.m_TransitionAttributeTypesWithTarget.Add(TransitionAttributeTypeId, True)
                End If

            Catch ex As Exception
                Throw New ArgumentException(ds.DisplayName & " -> " & ex.Message)
            End Try

        Next

    End Sub

    ''' <summary>
    ''' Fills the Transition Multiplier Value Collection for the model
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillTransitionMultiplierValueCollection()

        Debug.Assert(Me.m_TransitionMultiplierValues.Count = 0)
        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_TRANSITION_MULTIPLIER_VALUE_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            If (dr.RowState = DataRowState.Deleted) Then
                Continue For
            End If

            Dim TransitionGroupId As Integer = CInt(dr(DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME))
            Dim Iteration As Nullable(Of Integer) = Nothing
            Dim Timestep As Nullable(Of Integer) = Nothing
            Dim StratumId As Nullable(Of Integer) = Nothing
            Dim SecondaryStratumId As Nullable(Of Integer) = Nothing
            Dim StateClassId As Nullable(Of Integer) = Nothing
            Dim TransitionMultiplierTypeId As Nullable(Of Integer) = Nothing
            Dim MultiplierAmount As Nullable(Of Double) = Nothing
            Dim DistributionTypeId As Nullable(Of Integer) = Nothing
            Dim DistributionFrequency As Nullable(Of DistributionFrequency) = Nothing
            Dim DistributionSD As Nullable(Of Double) = Nothing
            Dim DistributionMin As Nullable(Of Double) = Nothing
            Dim DistributionMax As Nullable(Of Double) = Nothing

            If (dr(DATASHEET_ITERATION_COLUMN_NAME) IsNot DBNull.Value) Then
                Iteration = CInt(dr(DATASHEET_ITERATION_COLUMN_NAME))
            End If

            If (dr(DATASHEET_TIMESTEP_COLUMN_NAME) IsNot DBNull.Value) Then
                Timestep = CInt(dr(DATASHEET_TIMESTEP_COLUMN_NAME))
            End If

            If (dr(DATASHEET_STRATUM_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                StratumId = CInt(dr(DATASHEET_STRATUM_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                SecondaryStratumId = CInt(dr(DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_STATECLASS_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                StateClassId = CInt(dr(DATASHEET_STATECLASS_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_TRANSITION_MULTIPLIER_TYPE_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                TransitionMultiplierTypeId = CInt(dr(DATASHEET_TRANSITION_MULTIPLIER_TYPE_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_AMOUNT_COLUMN_NAME) IsNot DBNull.Value) Then
                MultiplierAmount = CDbl(dr(DATASHEET_AMOUNT_COLUMN_NAME))
            End If

            If (dr(DATASHEET_DISTRIBUTIONTYPE_COLUMN_NAME) IsNot DBNull.Value) Then
                DistributionTypeId = CInt(dr(DATASHEET_DISTRIBUTIONTYPE_COLUMN_NAME))
            End If

            If (dr(DATASHEET_DISTRIBUTION_FREQUENCY_COLUMN_NAME) IsNot DBNull.Value) Then
                DistributionFrequency = CType(dr(DATASHEET_DISTRIBUTION_FREQUENCY_COLUMN_NAME), DistributionFrequency)
            End If

            If (dr(DATASHEET_DISTRIBUTIONSD_COLUMN_NAME) IsNot DBNull.Value) Then
                DistributionSD = CDbl(dr(DATASHEET_DISTRIBUTIONSD_COLUMN_NAME))
            End If

            If (dr(DATASHEET_DISTRIBUTIONMIN_COLUMN_NAME) IsNot DBNull.Value) Then
                DistributionMin = CDbl(dr(DATASHEET_DISTRIBUTIONMIN_COLUMN_NAME))
            End If

            If (dr(DATASHEET_DISTRIBUTIONMAX_COLUMN_NAME) IsNot DBNull.Value) Then
                DistributionMax = CDbl(dr(DATASHEET_DISTRIBUTIONMAX_COLUMN_NAME))
            End If

            Try

                Dim Item As New TransitionMultiplierValue(
                    TransitionGroupId,
                    Iteration,
                    Timestep,
                    StratumId,
                    SecondaryStratumId,
                    StateClassId,
                    TransitionMultiplierTypeId,
                    MultiplierAmount,
                    DistributionTypeId,
                    DistributionFrequency,
                    DistributionSD,
                    DistributionMin,
                    DistributionMax)

                Me.m_DistributionProvider.Validate(
                    Item.DistributionTypeId,
                    Item.DistributionValue,
                    Item.DistributionSD,
                    Item.DistributionMin,
                    Item.DistributionMax)

                Me.m_TransitionMultiplierValues.Add(Item)

            Catch ex As Exception
                Throw New ArgumentException(ds.DisplayName & " -> " & ex.Message)
            End Try

        Next

    End Sub

    ''' <summary>
    ''' Fills ( or append)  the Transition Spatial Multiplier Collection for the model
    ''' </summary>
    ''' <param name="listPrevDataPK">A list of the primary keys value of existing Transition Spatial Multiplier row. Used to determine new rows to be appended.</param>
    ''' <remarks></remarks>
    Private Sub FillTransitionSpatialMultiplierCollection(Optional listPrevDataPK As List(Of Integer) = Nothing)

        Debug.Assert(Me.IsSpatial)

        If listPrevDataPK Is Nothing Then
            Debug.Assert(Me.m_TransitionSpatialMultipliers.Count = 0)
        End If

        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_TRANSITION_SPATIAL_MULTIPLIER_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            ' If we're in append mode, we're only interested in the newly added rows
            If Not listPrevDataPK Is Nothing Then
                ' Append mode. See if this row was processed in previous timesteps
                If listPrevDataPK.Contains(CInt(dr(ds.PrimaryKeyColumn.Name))) Then
                    Continue For
                End If
            End If

            Dim TransitionSpatialMultiplierId As Integer = CInt(dr(ds.PrimaryKeyColumn.Name))
            Dim TransitionGroupId As Integer = CInt(dr(DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME))
            Dim TransitionMultiplierTypeId As Nullable(Of Integer) = Nothing
            Dim Iteration As Nullable(Of Integer) = Nothing
            Dim Timestep As Nullable(Of Integer) = Nothing
            Dim FileName As String = CStr(dr(DATASHEET_TRANSITION_SPATIAL_MULTIPLIER_FILE_COLUMN_NAME))

            If (dr(DATASHEET_ITERATION_COLUMN_NAME) IsNot DBNull.Value) Then
                Iteration = CInt(dr(DATASHEET_ITERATION_COLUMN_NAME))
            End If

            If (dr(DATASHEET_TIMESTEP_COLUMN_NAME) IsNot DBNull.Value) Then
                Timestep = CInt(dr(DATASHEET_TIMESTEP_COLUMN_NAME))
            End If

            If (dr(DATASHEET_TRANSITION_MULTIPLIER_TYPE_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                TransitionMultiplierTypeId = CInt(dr(DATASHEET_TRANSITION_MULTIPLIER_TYPE_ID_COLUMN_NAME))
            End If

            Dim Multiplier As New TransitionSpatialMultiplier(
                TransitionSpatialMultiplierId, TransitionGroupId, TransitionMultiplierTypeId, Iteration, Timestep, FileName)

            Dim tsmFilename As String = RasterFiles.GetInputFileName(ds, FileName, False)
            Dim rastTSM As New StochasticTimeRaster
            Dim compareMsg As String = ""

            RasterFiles.LoadRasterFile(tsmFilename, rastTSM, RasterDataType.DTDouble)

            'Compare the TSM raster metadata to that of the Initial Condition raster files

            Dim cmpRes = Me.m_InputRasters.CompareMetadata(rastTSM, compareMsg)

            If cmpRes = STSim.CompareMetadataResult.ImportantDifferences Then

                Dim msg As String = String.Format(CultureInfo.CurrentCulture, STATUS_SPATIAL_FILE_TSM_METADATA_WARNING, tsmFilename)
                RecordStatus(StatusType.Warning, msg)

            Else

                If cmpRes = STSim.CompareMetadataResult.UnimportantDifferences Then

                    Dim msg As String = String.Format(CultureInfo.CurrentCulture, STATUS_SPATIAL_FILE_TSM_METADATA_INFO, tsmFilename, compareMsg)
                    RecordStatus(StatusType.Information, msg)

                End If

                Me.m_TransitionSpatialMultipliers.Add(Multiplier)

                'We only want to store a single copy of each unique TSM raster file to conserve memory

                If Not m_TransitionSpatialMultiplierRasters.ContainsKey(FileName) Then
                    m_TransitionSpatialMultiplierRasters.Add(FileName, rastTSM)
                End If

            End If

        Next

    End Sub

    ''' <summary>
    ''' Fills ( or append)  the Transition Spatial Initiation Multiplier Collection for the model
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillTransitionSpatialInitiationMultiplierCollection()

        Debug.Assert(Me.IsSpatial)

        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_TRANSITION_SPATIAL_INITIATION_MULTIPLIER_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            'DEVNOTE: This code is based on FillTransitionSpatialMultiplierCollection, but the ability to "add" new TSM records thru mid-run ImportExternal has been removed. If this feature
            ' is required, refer to the FillTransitionSpatialMultiplierCollection sub for example code

            Dim TransitionSpatialInitiationMultiplierId As Integer = CInt(dr(ds.PrimaryKeyColumn.Name))
            Dim TransitionGroupId As Integer = CInt(dr(DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME))
            Dim TransitionMultiplierTypeId As Nullable(Of Integer) = Nothing
            Dim Iteration As Nullable(Of Integer) = Nothing
            Dim Timestep As Nullable(Of Integer) = Nothing
            Dim FileName As String = CStr(dr(DATASHEET_TRANSITION_SPATIAL_INITIATION_MULTIPLIER_FILE_COLUMN_NAME))

            If (dr(DATASHEET_ITERATION_COLUMN_NAME) IsNot DBNull.Value) Then
                Iteration = CInt(dr(DATASHEET_ITERATION_COLUMN_NAME))
            End If

            If (dr(DATASHEET_TIMESTEP_COLUMN_NAME) IsNot DBNull.Value) Then
                Timestep = CInt(dr(DATASHEET_TIMESTEP_COLUMN_NAME))
            End If

            If (dr(DATASHEET_TRANSITION_MULTIPLIER_TYPE_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                TransitionMultiplierTypeId = CInt(dr(DATASHEET_TRANSITION_MULTIPLIER_TYPE_ID_COLUMN_NAME))
            End If

            Dim Multiplier As New TransitionSpatialInitiationMultiplier(
                TransitionSpatialInitiationMultiplierId, TransitionGroupId, TransitionMultiplierTypeId, Iteration, Timestep, FileName)

            Dim tsimFilename As String = RasterFiles.GetInputFileName(ds, FileName, False)
            Dim rastTSIM As New StochasticTimeRaster
            Dim cmpMsg As String = ""

            RasterFiles.LoadRasterFile(tsimFilename, rastTSIM, RasterDataType.DTDouble)

            'Compare the TSIM raster metadata to that of the Initial Condition raster files
            Dim cmpRes = Me.m_InputRasters.CompareMetadata(rastTSIM, cmpMsg)

            If cmpRes = STSim.CompareMetadataResult.ImportantDifferences Then

                Dim msg As String = String.Format(CultureInfo.CurrentCulture, STATUS_SPATIAL_FILE_TSIM_METADATA_WARNING, tsimFilename)
                RecordStatus(StatusType.Warning, msg)

            Else

                If cmpRes = STSim.CompareMetadataResult.UnimportantDifferences Then

                    Dim msg As String = String.Format(CultureInfo.CurrentCulture, STATUS_SPATIAL_FILE_TSIM_METADATA_INFO, tsimFilename, cmpMsg)
                    RecordStatus(StatusType.Information, msg)

                End If

                Me.m_TransitionSpatialInitiationMultipliers.Add(Multiplier)

                'We only want to store a single copy of each unique TSIM raster file to conserve memory

                If Not m_TransitionSpatialInitiationMultiplierRasters.ContainsKey(FileName) Then
                    m_TransitionSpatialInitiationMultiplierRasters.Add(FileName, rastTSIM)
                End If

            End If

        Next

    End Sub


    ''' <summary>
    ''' Fills the transition size distribution collection
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillTransitionSizeDistributionCollection()

#If DEBUG Then

        Debug.Assert(Me.IsSpatial)
        Debug.Assert(Me.m_TransitionSizeDistributions.Count = 0)

        Debug.Assert(TRANSITION_TYPES_FILLED)
        Debug.Assert(TRANSITION_GROUPS_FILLED)

#End If

        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_TRANSITION_SIZE_DISTRIBUTION_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            Dim TransitionSizeDistributionId As Integer = CInt(dr(ds.PrimaryKeyColumn.Name))
            Dim StratumId As Nullable(Of Integer) = Nothing
            Dim Iteration As Nullable(Of Integer) = Nothing
            Dim Timestep As Nullable(Of Integer) = Nothing
            Dim TransitionGroupId As Integer = CInt(dr(DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME))
            Dim MaximumSize As Double = CDbl(dr(DATASHEET_TRANSITION_SIZE_DISTRIBUTION_MAXIMUM_AREA_COLUMN_NAME))
            Dim RelativeAmount As Double = CDbl(dr(DATASHEET_TRANSITION_SIZE_DISTRIBUTION_RELATIVE_AMOUNT_COLUMN_NAME))

            If (dr(DATASHEET_STRATUM_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                StratumId = CInt(dr(DATASHEET_STRATUM_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_ITERATION_COLUMN_NAME) IsNot DBNull.Value) Then
                Iteration = CInt(dr(DATASHEET_ITERATION_COLUMN_NAME))
            End If

            If (dr(DATASHEET_TIMESTEP_COLUMN_NAME) IsNot DBNull.Value) Then
                Timestep = CInt(dr(DATASHEET_TIMESTEP_COLUMN_NAME))
            End If

            Dim tsd As New TransitionSizeDistribution(
                TransitionSizeDistributionId, StratumId, Iteration, Timestep,
                TransitionGroupId, MaximumSize, RelativeAmount)

            Me.m_TransitionSizeDistributions.Add(tsd)
            Me.m_TransitionGroups(TransitionGroupId).HasSizeDistribution = True

        Next

    End Sub

    ''' <summary>
    ''' Fills the transition spread distribution collection
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillTransitionSpreadDistributionCollection()

        Debug.Assert(Me.IsSpatial)
        Debug.Assert(Me.m_TransitionSpreadDistributions.Count = 0)

        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_TRANSITION_SPREAD_DISTRIBUTION_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            Dim TransitionSpreadDistributionId As Integer = CInt(dr(ds.PrimaryKeyColumn.Name))
            Dim StratumId As Nullable(Of Integer) = Nothing
            Dim Iteration As Nullable(Of Integer) = Nothing
            Dim Timestep As Nullable(Of Integer) = Nothing
            Dim TransitionGroupId As Integer = CInt(dr(DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME))
            Dim StateClassId As Integer = CInt(dr(DATASHEET_STATECLASS_ID_COLUMN_NAME))
            Dim MaximumDistance As Double = CDbl(dr(DATASHEET_TRANSITION_SPREAD_DISTRIBUTION_MAXIMUM_DISTANCE_COLUMN_NAME))
            Dim RelativeAmount As Double = CDbl(dr(DATASHEET_TRANSITION_SPREAD_DISTRIBUTION_RELATIVE_AMOUNT_COLUMN_NAME))

            If (dr(DATASHEET_STRATUM_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                StratumId = CInt(dr(DATASHEET_STRATUM_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_ITERATION_COLUMN_NAME) IsNot DBNull.Value) Then
                Iteration = CInt(dr(DATASHEET_ITERATION_COLUMN_NAME))
            End If

            If (dr(DATASHEET_TIMESTEP_COLUMN_NAME) IsNot DBNull.Value) Then
                Timestep = CInt(dr(DATASHEET_TIMESTEP_COLUMN_NAME))
            End If

            Dim tsd As New TransitionSpreadDistribution(
                TransitionSpreadDistributionId, StratumId, Iteration, Timestep,
                TransitionGroupId, StateClassId, MaximumDistance, RelativeAmount)

            Me.m_TransitionSpreadDistributions.Add(tsd)

        Next

    End Sub

    ''' <summary>
    ''' Fills the Transition Patch Prioritization Collection
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillTransitionPatchPrioritizationCollection()

        Debug.Assert(Me.IsSpatial)
        Debug.Assert(Me.m_TransitionPatchPrioritizations.Count = 0)

        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_TRANSITION_PATCH_PRIORITIZATION_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            Dim TransitionPatchPrioritizationId As Integer = CInt(dr(ds.PrimaryKeyColumn.Name))
            Dim Iteration As Nullable(Of Integer) = Nothing
            Dim Timestep As Nullable(Of Integer) = Nothing
            Dim TransitionGroupId As Integer = CInt(dr(DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME))
            Dim PatchPrioritizationId As Integer = CInt(dr(DATASHEET_TRANSITION_PATCH_PRIORITIZATION_PP_COLUMN_NAME))

            If (dr(DATASHEET_ITERATION_COLUMN_NAME) IsNot DBNull.Value) Then
                Iteration = CInt(dr(DATASHEET_ITERATION_COLUMN_NAME))
            End If

            If (dr(DATASHEET_TIMESTEP_COLUMN_NAME) IsNot DBNull.Value) Then
                Timestep = CInt(dr(DATASHEET_TIMESTEP_COLUMN_NAME))
            End If

            Debug.Assert(Me.m_PatchPrioritizations.Contains(PatchPrioritizationId))

            Dim pp As New TransitionPatchPrioritization(
                TransitionPatchPrioritizationId, Iteration, Timestep, TransitionGroupId, PatchPrioritizationId)

            Me.m_TransitionPatchPrioritizations.Add(pp)

        Next

    End Sub

    ''' <summary>
    ''' Fills the Transition Size Prioritization Collection
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillTransitionSizePrioritizationCollection()

        Debug.Assert(Me.IsSpatial)
        Debug.Assert(Me.m_TransitionSizePrioritizations.Count = 0)

        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_TRANSITION_SIZE_PRIORITIZATION_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            Dim TransitionSizePrioritizationId As Integer = CInt(dr(ds.PrimaryKeyColumn.Name))
            Dim Iteration As Nullable(Of Integer) = Nothing
            Dim Timestep As Nullable(Of Integer) = Nothing
            Dim StratumId As Nullable(Of Integer) = Nothing
            Dim TransitionGroupId As Nullable(Of Integer) = Nothing
            Dim PriorityType As SizePrioritization = SizePrioritization.Largest
            Dim MaximizeFidelityToDistribution As Boolean = True
            Dim MaximizeFidelityToTotalArea As Boolean = False

            If (dr(DATASHEET_ITERATION_COLUMN_NAME) IsNot DBNull.Value) Then
                Iteration = CInt(dr(DATASHEET_ITERATION_COLUMN_NAME))
            End If

            If (dr(DATASHEET_TIMESTEP_COLUMN_NAME) IsNot DBNull.Value) Then
                Timestep = CInt(dr(DATASHEET_TIMESTEP_COLUMN_NAME))
            End If

            If (dr(DATASHEET_STRATUM_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                StratumId = CInt(dr(DATASHEET_STRATUM_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                TransitionGroupId = CInt(dr(DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_TRANSITION_SIZE_PRIORITIZATION_PRIORITY_TYPE_COLUMN_NAME) IsNot DBNull.Value) Then
                PriorityType = CType(dr(DATASHEET_TRANSITION_SIZE_PRIORITIZATION_PRIORITY_TYPE_COLUMN_NAME), SizePrioritization)
            End If

            If (dr(DATASHEET_TRANSITION_SIZE_PRIORITIZATION_MFDIST_COLUMN_NAME) IsNot DBNull.Value) Then
                MaximizeFidelityToDistribution = CBool(dr(DATASHEET_TRANSITION_SIZE_PRIORITIZATION_MFDIST_COLUMN_NAME))
            End If

            If (dr(DATASHEET_TRANSITION_SIZE_PRIORITIZATION_MFAREA_COLUMN_NAME) IsNot DBNull.Value) Then
                MaximizeFidelityToTotalArea = CBool(dr(DATASHEET_TRANSITION_SIZE_PRIORITIZATION_MFAREA_COLUMN_NAME))
            End If

            Dim Item As New TransitionSizePrioritization(
                TransitionSizePrioritizationId,
                Iteration,
                Timestep,
                StratumId,
                TransitionGroupId,
                PriorityType,
                MaximizeFidelityToDistribution,
                MaximizeFidelityToTotalArea)

            Me.m_TransitionSizePrioritizations.Add(Item)

        Next

    End Sub

    ''' <summary>
    ''' Fills the Transition Pathway Auto-Correlation Collection
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillTransitionPathwayAutoCorrelationCollection()

        Debug.Assert(Me.IsSpatial)
        Debug.Assert(Me.m_TransitionPathwayAutoCorrelations.Count = 0)

        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_TRANSITION_PATHWAY_AUTO_CORRELATION_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            Dim Iteration As Nullable(Of Integer) = Nothing
            Dim Timestep As Nullable(Of Integer) = Nothing
            Dim StratumId As Nullable(Of Integer) = Nothing
            Dim SecondaryStratumId As Nullable(Of Integer) = Nothing
            Dim TransitionGroupId As Nullable(Of Integer) = Nothing
            Dim Factor As Double = CDbl(dr(DATASHEET_TRANSITION_PATHWAY_AUTO_CORRELATION_FACTOR_COLUMN_NAME))
            Dim SpreadOnlyToLike As Boolean = False

            If (dr(DATASHEET_ITERATION_COLUMN_NAME) IsNot DBNull.Value) Then
                Iteration = CInt(dr(DATASHEET_ITERATION_COLUMN_NAME))
            End If

            If (dr(DATASHEET_TIMESTEP_COLUMN_NAME) IsNot DBNull.Value) Then
                Timestep = CInt(dr(DATASHEET_TIMESTEP_COLUMN_NAME))
            End If

            If (dr(DATASHEET_STRATUM_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                StratumId = CInt(dr(DATASHEET_STRATUM_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                SecondaryStratumId = CInt(dr(DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                TransitionGroupId = CInt(dr(DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_TRANSITION_PATHWAY_SPREAD_ONLY_TO_LIKE_COLUMN_NAME) IsNot DBNull.Value) Then
                SpreadOnlyToLike = DataTableUtilities.GetDataBool(dr, DATASHEET_TRANSITION_PATHWAY_SPREAD_ONLY_TO_LIKE_COLUMN_NAME)
            End If

            Dim Item As New TransitionPathwayAutoCorrelation(
                 Iteration, Timestep, StratumId, SecondaryStratumId, TransitionGroupId, Factor, SpreadOnlyToLike)

            Me.m_TransitionPathwayAutoCorrelations.Add(Item)

        Next

    End Sub

    ''' <summary>
    ''' Fills the Transition Direction Multiplier Collection
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillTransitionDirectionMultiplierCollection()

        Debug.Assert(Me.m_TransitionDirectionMultipliers.Count = 0)
        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_TRANSITION_DIRECTION_MULTIPLER_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            Dim TransitionGroupId As Integer = CInt(dr(DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME))
            Dim Iteration As Nullable(Of Integer) = Nothing
            Dim Timestep As Nullable(Of Integer) = Nothing
            Dim StratumId As Nullable(Of Integer) = Nothing
            Dim SecondaryStratumId As Nullable(Of Integer) = Nothing
            Dim CardinalDirection As CardinalDirection = CType(CInt(dr(DATASHEET_TRANSITION_DIRECTION_MULTIPLER_CARDINAL_DIRECTION_COLUMN_NAME)), CardinalDirection)
            Dim MultiplierAmount As Nullable(Of Double) = Nothing
            Dim DistributionTypeId As Nullable(Of Integer) = Nothing
            Dim DistributionFrequency As Nullable(Of DistributionFrequency) = Nothing
            Dim DistributionSD As Nullable(Of Double) = Nothing
            Dim DistributionMin As Nullable(Of Double) = Nothing
            Dim DistributionMax As Nullable(Of Double) = Nothing

            If (dr(DATASHEET_ITERATION_COLUMN_NAME) IsNot DBNull.Value) Then
                Iteration = CInt(dr(DATASHEET_ITERATION_COLUMN_NAME))
            End If

            If (dr(DATASHEET_TIMESTEP_COLUMN_NAME) IsNot DBNull.Value) Then
                Timestep = CInt(dr(DATASHEET_TIMESTEP_COLUMN_NAME))
            End If

            If (dr(DATASHEET_STRATUM_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                StratumId = CInt(dr(DATASHEET_STRATUM_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                SecondaryStratumId = CInt(dr(DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_AMOUNT_COLUMN_NAME) IsNot DBNull.Value) Then
                MultiplierAmount = CDbl(dr(DATASHEET_AMOUNT_COLUMN_NAME))
            End If

            If (dr(DATASHEET_DISTRIBUTIONTYPE_COLUMN_NAME) IsNot DBNull.Value) Then
                DistributionTypeId = CInt(dr(DATASHEET_DISTRIBUTIONTYPE_COLUMN_NAME))
            End If

            If (dr(DATASHEET_DISTRIBUTION_FREQUENCY_COLUMN_NAME) IsNot DBNull.Value) Then
                DistributionFrequency = CType(dr(DATASHEET_DISTRIBUTION_FREQUENCY_COLUMN_NAME), DistributionFrequency)
            End If

            If (dr(DATASHEET_DISTRIBUTIONSD_COLUMN_NAME) IsNot DBNull.Value) Then
                DistributionSD = CDbl(dr(DATASHEET_DISTRIBUTIONSD_COLUMN_NAME))
            End If

            If (dr(DATASHEET_DISTRIBUTIONMIN_COLUMN_NAME) IsNot DBNull.Value) Then
                DistributionMin = CDbl(dr(DATASHEET_DISTRIBUTIONMIN_COLUMN_NAME))
            End If

            If (dr(DATASHEET_DISTRIBUTIONMAX_COLUMN_NAME) IsNot DBNull.Value) Then
                DistributionMax = CDbl(dr(DATASHEET_DISTRIBUTIONMAX_COLUMN_NAME))
            End If

            Try

                Dim Item As New TransitionDirectionMultiplier(
                    TransitionGroupId,
                    Iteration,
                    Timestep,
                    StratumId,
                    SecondaryStratumId,
                    CardinalDirection,
                    MultiplierAmount,
                    DistributionTypeId,
                    DistributionFrequency,
                    DistributionSD,
                    DistributionMin,
                    DistributionMax)

                Me.m_DistributionProvider.Validate(
                    Item.DistributionTypeId,
                    Item.DistributionValue,
                    Item.DistributionSD,
                    Item.DistributionMin,
                    Item.DistributionMax)

                Me.m_TransitionDirectionMultipliers.Add(Item)

            Catch ex As Exception
                Throw New ArgumentException(ds.DisplayName & " -> " & ex.Message)
            End Try

        Next

    End Sub

    ''' <summary>
    ''' Fills the Transition Slope Multiplier Collection
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillTransitionSlopeMultiplierCollection()

        Debug.Assert(Me.m_TransitionSlopeMultipliers.Count = 0)
        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_TRANSITION_SLOPE_MULTIPLIER_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            Dim TransitionGroupId As Integer = CInt(dr(DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME))
            Dim Iteration As Nullable(Of Integer) = Nothing
            Dim Timestep As Nullable(Of Integer) = Nothing
            Dim StratumId As Nullable(Of Integer) = Nothing
            Dim SecondaryStratumId As Nullable(Of Integer) = Nothing
            Dim Slope As Integer = CInt(dr(DATASHEET_TRANSITION_SLOPE_MULTIPLIER_SLOPE_COLUMN_NAME))
            Dim MultiplierAmount As Nullable(Of Double) = Nothing
            Dim DistributionTypeId As Nullable(Of Integer) = Nothing
            Dim DistributionFrequency As Nullable(Of DistributionFrequency) = Nothing
            Dim DistributionSD As Nullable(Of Double) = Nothing
            Dim DistributionMin As Nullable(Of Double) = Nothing
            Dim DistributionMax As Nullable(Of Double) = Nothing

            If (dr(DATASHEET_ITERATION_COLUMN_NAME) IsNot DBNull.Value) Then
                Iteration = CInt(dr(DATASHEET_ITERATION_COLUMN_NAME))
            End If

            If (dr(DATASHEET_TIMESTEP_COLUMN_NAME) IsNot DBNull.Value) Then
                Timestep = CInt(dr(DATASHEET_TIMESTEP_COLUMN_NAME))
            End If

            If (dr(DATASHEET_STRATUM_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                StratumId = CInt(dr(DATASHEET_STRATUM_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                SecondaryStratumId = CInt(dr(DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_AMOUNT_COLUMN_NAME) IsNot DBNull.Value) Then
                MultiplierAmount = CDbl(dr(DATASHEET_AMOUNT_COLUMN_NAME))
            End If

            If (dr(DATASHEET_DISTRIBUTIONTYPE_COLUMN_NAME) IsNot DBNull.Value) Then
                DistributionTypeId = CInt(dr(DATASHEET_DISTRIBUTIONTYPE_COLUMN_NAME))
            End If

            If (dr(DATASHEET_DISTRIBUTION_FREQUENCY_COLUMN_NAME) IsNot DBNull.Value) Then
                DistributionFrequency = CType(dr(DATASHEET_DISTRIBUTION_FREQUENCY_COLUMN_NAME), DistributionFrequency)
            End If

            If (dr(DATASHEET_DISTRIBUTIONSD_COLUMN_NAME) IsNot DBNull.Value) Then
                DistributionSD = CDbl(dr(DATASHEET_DISTRIBUTIONSD_COLUMN_NAME))
            End If

            If (dr(DATASHEET_DISTRIBUTIONMIN_COLUMN_NAME) IsNot DBNull.Value) Then
                DistributionMin = CDbl(dr(DATASHEET_DISTRIBUTIONMIN_COLUMN_NAME))
            End If

            If (dr(DATASHEET_DISTRIBUTIONMAX_COLUMN_NAME) IsNot DBNull.Value) Then
                DistributionMax = CDbl(dr(DATASHEET_DISTRIBUTIONMAX_COLUMN_NAME))
            End If

            Try

                Dim Item As New TransitionSlopeMultiplier(
                    TransitionGroupId,
                    Iteration,
                    Timestep,
                    StratumId,
                    SecondaryStratumId,
                    Slope,
                    MultiplierAmount,
                    DistributionTypeId,
                    DistributionFrequency,
                    DistributionSD,
                    DistributionMin,
                    DistributionMax)

                Me.m_DistributionProvider.Validate(
                    Item.DistributionTypeId,
                    Item.DistributionValue,
                    Item.DistributionSD,
                    Item.DistributionMin,
                    Item.DistributionMax)

                Me.m_TransitionSlopeMultipliers.Add(Item)

            Catch ex As Exception
                Throw New ArgumentException(ds.DisplayName & " -> " & ex.Message)
            End Try

        Next

    End Sub

    Private ADJ_MULT_SETTINGS_FILLED As Boolean

    ''' <summary>
    ''' Fills the Transition Adjacency Setting Collection
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillTransitionAdjacencySettingCollection()

        Debug.Assert(Me.m_TransitionAdjacencySettings.Count = 0)
        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_TRANSITION_ADJACENCY_SETTING_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            Dim TransitionGroupId As Integer = CInt(dr(DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME))
            Dim StateAttributeTypeId As Integer = CInt(dr(DATASHEET_STATE_ATTRIBUTE_TYPE_ID_COLUMN_NAME))
            Dim NeighborhoodRadius As Double = CDbl(dr(DATASHEET_TRANSITION_ADJACENCY_SETTING_NR_COLUMN_NAME))
            Dim UpdateFrequency As Integer = CInt(dr(DATASHEET_TRANSITION_ADJACENCY_SETTING_UF_COLUMN_NAME))

            Me.m_TransitionAdjacencySettings.Add(
                New TransitionAdjacencySetting(
                    TransitionGroupId,
                     StateAttributeTypeId,
                     NeighborhoodRadius,
                     UpdateFrequency))

        Next

        Me.ADJ_MULT_SETTINGS_FILLED = True

    End Sub

    ''' <summary>
    ''' Fills the Transition Adjacency Multiplier Collection
    ''' </summary>
    ''' <remarks>
    ''' The Transition Adjacency Setting Collection must be filled before this collection is filled so
    ''' we can validate that they have the same transition groups.
    ''' </remarks>
    Private Sub FillTransitionAdjacencyMultiplierCollection()

        Debug.Assert(Me.m_TransitionAdjacencyMultipliers.Count = 0)
        Dim ds As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_TRANSITION_ADJACENCY_MULTIPLIER_NAME)

        For Each dr As DataRow In ds.GetData.Rows

            Dim TransitionGroupId As Integer = CInt(dr(DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME))
            Dim Iteration As Nullable(Of Integer) = Nothing
            Dim Timestep As Nullable(Of Integer) = Nothing
            Dim StratumId As Nullable(Of Integer) = Nothing
            Dim SecondaryStratumId As Nullable(Of Integer) = Nothing
            Dim AttributeValue As Double = CDbl(dr(DATASHEET_TRANSITION_ADJACENCY_ATTRIBUTE_VALUE_COLUMN_NAME))
            Dim MultiplierAmount As Nullable(Of Double) = Nothing
            Dim DistributionTypeId As Nullable(Of Integer) = Nothing
            Dim DistributionFrequency As Nullable(Of DistributionFrequency) = Nothing
            Dim DistributionSD As Nullable(Of Double) = Nothing
            Dim DistributionMin As Nullable(Of Double) = Nothing
            Dim DistributionMax As Nullable(Of Double) = Nothing

            If (dr(DATASHEET_ITERATION_COLUMN_NAME) IsNot DBNull.Value) Then
                Iteration = CInt(dr(DATASHEET_ITERATION_COLUMN_NAME))
            End If

            If (dr(DATASHEET_TIMESTEP_COLUMN_NAME) IsNot DBNull.Value) Then
                Timestep = CInt(dr(DATASHEET_TIMESTEP_COLUMN_NAME))
            End If

            If (dr(DATASHEET_STRATUM_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                StratumId = CInt(dr(DATASHEET_STRATUM_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME) IsNot DBNull.Value) Then
                SecondaryStratumId = CInt(dr(DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME))
            End If

            If (dr(DATASHEET_AMOUNT_COLUMN_NAME) IsNot DBNull.Value) Then
                MultiplierAmount = CDbl(dr(DATASHEET_AMOUNT_COLUMN_NAME))
            End If

            If (dr(DATASHEET_DISTRIBUTIONTYPE_COLUMN_NAME) IsNot DBNull.Value) Then
                DistributionTypeId = CInt(dr(DATASHEET_DISTRIBUTIONTYPE_COLUMN_NAME))
            End If

            If (dr(DATASHEET_DISTRIBUTION_FREQUENCY_COLUMN_NAME) IsNot DBNull.Value) Then
                DistributionFrequency = CType(dr(DATASHEET_DISTRIBUTION_FREQUENCY_COLUMN_NAME), DistributionFrequency)
            End If

            If (dr(DATASHEET_DISTRIBUTIONSD_COLUMN_NAME) IsNot DBNull.Value) Then
                DistributionSD = CDbl(dr(DATASHEET_DISTRIBUTIONSD_COLUMN_NAME))
            End If

            If (dr(DATASHEET_DISTRIBUTIONMIN_COLUMN_NAME) IsNot DBNull.Value) Then
                DistributionMin = CDbl(dr(DATASHEET_DISTRIBUTIONMIN_COLUMN_NAME))
            End If

            If (dr(DATASHEET_DISTRIBUTIONMAX_COLUMN_NAME) IsNot DBNull.Value) Then
                DistributionMax = CDbl(dr(DATASHEET_DISTRIBUTIONMAX_COLUMN_NAME))
            End If

            Try

                Dim Item As New TransitionAdjacencyMultiplier(
                    TransitionGroupId,
                    Iteration,
                    Timestep,
                    StratumId,
                    SecondaryStratumId,
                    AttributeValue,
                    MultiplierAmount,
                    DistributionTypeId,
                    DistributionFrequency,
                    DistributionSD,
                    DistributionMin,
                    DistributionMax)

                Me.m_DistributionProvider.Validate(
                    Item.DistributionTypeId,
                    Item.DistributionValue,
                    Item.DistributionSD,
                    Item.DistributionMin,
                    Item.DistributionMax)

                Me.m_TransitionAdjacencyMultipliers.Add(Item)

            Catch ex As Exception
                Throw New ArgumentException(ds.DisplayName & " -> " & ex.Message)
            End Try

        Next

        If (Me.AdjacencyMultiplierGroupsIdentical()) Then

            Me.RecordStatus(StatusType.Warning,
                "Transition adjacency settings and multipliers do not have identical transition groups.  Some multipliers may not be applied.")

        End If

    End Sub

    ''' <summary>
    ''' Warns the user if the transition groups are not identical between transition adjacency settings and multipliers
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AdjacencyMultiplierGroupsIdentical() As Boolean

        Debug.Assert(ADJ_MULT_SETTINGS_FILLED = True)

        Dim d1 As New Dictionary(Of Integer, Boolean)
        Dim d2 As New Dictionary(Of Integer, Boolean)

        For Each m As TransitionAdjacencyMultiplier In Me.m_TransitionAdjacencyMultipliers

            If (Not d1.ContainsKey(m.TransitionGroupId)) Then
                d1.Add(m.TransitionGroupId, True)
            End If

        Next

        For Each s As TransitionAdjacencySetting In Me.m_TransitionAdjacencySettings

            If (Not d2.ContainsKey(s.TransitionGroupId)) Then
                d2.Add(s.TransitionGroupId, True)
            End If

        Next

        If (d1.Count <> d2.Count) Then
            Return True
        End If

        For Each tg As Integer In d1.Keys

            If (Not d2.ContainsKey(tg)) Then
                Return True
            End If

        Next

        For Each tg As Integer In d2.Keys

            If (Not d1.ContainsKey(tg)) Then
                Return True
            End If

        Next

        Return False

    End Function

    ''' <summary>
    ''' ValidateSpatialPrimaryGroups
    ''' </summary>
    ''' <remarks>
    ''' If the run is spatial and Area targets, patch prioritization, direction or slope multipliers have been defined
    ''' for groups that have no records in the types by group table where primary = true OR NULL then show a warning.
    ''' </remarks>
    Private Sub ValidateSpatialPrimaryGroups()

#If DEBUG Then
        Debug.Assert(Me.m_IsSpatial)
        Debug.Assert(Me.TRANSITION_TYPES_FILLED)
        Debug.Assert(Me.TRANSITION_GROUPS_FILLED)
#End If

        'First, get all the primary transition groups

        Dim d As New Dictionary(Of Integer, Boolean)
        Dim ds As DataSheet = Me.Project.GetDataSheet(DATASHEET_TRANSITION_TYPE_GROUP_NAME)

        For Each dr As DataRow In ds.GetData().Rows

            If (dr.RowState = DataRowState.Deleted) Then
                Continue For
            End If

            If (IsPrimaryTypeByGroup(dr)) Then

                Dim id As Integer = CInt(dr(DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME))

                If (Not d.ContainsKey(id)) Then
                    d.Add(id, True)
                End If

            End If

        Next

        'Then, verify that each collection has at least one primary transition group

        Dim TransitionTargetsGroupFound As Boolean = True
        Dim TransitionPatchPrioritizationGroupFound As Boolean = True
        Dim TransitionDirectionMultipliersGroupFound As Boolean = True
        Dim TransitionSlopeMultipliersGroupFound As Boolean = True

        For Each t As TransitionTarget In Me.m_TransitionTargets

            If (Not d.ContainsKey(t.TransitionGroupId)) Then

                TransitionTargetsGroupFound = False
                Exit For

            End If

        Next

        For Each t As TransitionPatchPrioritization In Me.m_TransitionPatchPrioritizations

            If (Not d.ContainsKey(t.TransitionGroupId)) Then

                TransitionPatchPrioritizationGroupFound = False
                Exit For

            End If

        Next

        For Each t As TransitionDirectionMultiplier In Me.m_TransitionDirectionMultipliers

            If (Not d.ContainsKey(t.TransitionGroupId)) Then

                TransitionDirectionMultipliersGroupFound = False
                Exit For

            End If

        Next

        For Each t As TransitionSlopeMultiplier In Me.m_TransitionSlopeMultipliers

            If (Not d.ContainsKey(t.TransitionGroupId)) Then

                TransitionSlopeMultipliersGroupFound = False
                Exit For

            End If

        Next

        If (Not TransitionTargetsGroupFound) Then
            Me.RecordStatus(StatusType.Warning, "At least one Transition Target has been defined with a non-primary Transition Group.")
        End If

        If (Not TransitionPatchPrioritizationGroupFound) Then
            Me.RecordStatus(StatusType.Warning, "At least one Transition Patch Prioritization has been defined with a non-primary Transition Group.")
        End If

        If (Not TransitionDirectionMultipliersGroupFound) Then
            Me.RecordStatus(StatusType.Warning, "At least one Transition Direction Multiplier has been defined with a non-primary Transition Group.")
        End If

        If (Not TransitionSlopeMultipliersGroupFound) Then
            Me.RecordStatus(StatusType.Warning, "At least one Transition Slope Multiplier has been defined with a non-primary Transition Group.")
        End If
       
    End Sub

End Class
