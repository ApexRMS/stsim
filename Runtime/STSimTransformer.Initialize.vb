'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports System.Globalization

Partial Class STSimTransformer

    ''' <summary>
    ''' Initializes the model
    ''' </summary>
    ''' <remarks>
    ''' This function must be called once the model has been configured
    ''' </remarks>
    Private Sub InitializeModel()

        If (Me.m_Cells.Count = 0) Then
            ExceptionUtils.ThrowArgumentException("You must have at least one cell to run the simulation.")
        End If

        If Not Me.IsSpatial Then

            If (Me.m_InitialConditionsDistributionMap.GetICDs(Me.MinimumIteration).Count = 0) Then
                ExceptionUtils.ThrowArgumentException("The initial conditions distribution collection cannot be empty.")
            End If

        End If

        Debug.Assert(Me.MinimumTimestep > 0)
        Debug.Assert(Me.MinimumIteration > 0)

        Me.InitializeCellArea()
        Me.InitializeCollectionMaps()
        Me.InitializeIntervalMeanTimestepMap()
        Me.InitializeStateAttributes()
        Me.InitializeTransitionAttributes()
        Me.InitializeShufflableTransitionGroups()

        If (Me.IsSpatial) Then
            Me.InitializeTransitionSpreadGroups()
        End If

        Debug.Assert(Me.m_SummaryStratumStateResults.Count = 0)
        Debug.Assert(Me.m_SummaryStratumTransitionStateResults.Count = 0)

    End Sub

    ''' <summary>
    ''' Configures the lower case version of the timestep units
    ''' </summary>
    Private Sub ConfigureTimestepUnits()

        Me.TimestepUnits = GetTimestepUnits(Me.Project)
        Me.m_TimestepUnitsLower = Me.TimestepUnits.ToLower(CultureInfo.InvariantCulture)

    End Sub

    ''' <summary>
    ''' Initializes the IsSpatial run flag
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConfigureIsSpatialRunFlag()

        Dim dr As DataRow = Me.ResultScenario.GetDataSheet(DATASHEET_RUN_CONTROL_NAME).GetDataRow()
        Me.m_IsSpatial = DataTableUtilities.GetDataBool(dr(RUN_CONTROL_IS_SPATIAL_COLUMN_NAME))

    End Sub

    ''' <summary>
    ''' Configures the timesteps and iterations for this model run
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConfigureTimestepsAndIterations()

        Dim dr As DataRow = Me.ResultScenario.GetDataSheet(DATASHEET_RUN_CONTROL_NAME).GetDataRow()

        Me.MinimumIteration = CInt(dr("MinimumIteration"))
        Me.MaximumIteration = CInt(dr("MaximumIteration"))
        Me.MinimumTimestep = CInt(dr("MinimumTimestep"))
        Me.MaximumTimestep = CInt(dr("MaximumTimestep"))

        'We want run control to have the minimum timestep that it is configured with, but we don't want
        'to run this timestep.  Instead, we want to set TimestepZero to the minimum timestep and run the
        'model starting at MinimumTimestep + 1.  We need to configure these values before initializing the
        'rest of the model because some of the initialization routines depend on these values being set.

        If (Me.MinimumTimestep = Me.MaximumTimestep) Then
            ExceptionUtils.ThrowArgumentException("ST-Sim: The start {0} and end {1} cannot be the same.", Me.m_TimestepUnitsLower, Me.m_TimestepUnitsLower)
        End If

        Me.m_TimestepZero = Me.MinimumTimestep
        Me.MinimumTimestep = Me.MinimumTimestep + 1

    End Sub

    ''' <summary>
    ''' Initializes the output data tables
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeOutputDataTables()

        Debug.Assert(Me.m_OutputStratumStateTable Is Nothing)

        Me.m_OutputStratumAmountTable = Me.ResultScenario.GetDataSheet(DATASHEET_OUTPUT_STRATUM_NAME).GetData()
        Me.m_OutputStratumStateTable = Me.ResultScenario.GetDataSheet(DATASHEET_OUTPUT_STRATUM_STATE_NAME).GetData()
        Me.m_OutputStratumTransitionTable = Me.ResultScenario.GetDataSheet(DATASHEET_OUTPUT_STRATUM_TRANSITION_NAME).GetData()
        Me.m_OutputStratumTransitionStateTable = Me.ResultScenario.GetDataSheet(DATASHEET_OUTPUT_STRATUM_TRANSITION_STATE_NAME).GetData()
        Me.m_OutputStateAttributeTable = Me.ResultScenario.GetDataSheet(DATASHEET_OUTPUT_STATE_ATTRIBUTE_NAME).GetData()
        Me.m_OutputTransitionAttributeTable = Me.ResultScenario.GetDataSheet(DATASHEET_OUTPUT_TRANSITION_ATTRIBUTE_NAME).GetData()

        Debug.Assert(m_OutputStratumAmountTable.Rows.Count = 0)
        Debug.Assert(m_OutputStratumStateTable.Rows.Count = 0)
        Debug.Assert(m_OutputStratumTransitionTable.Rows.Count = 0)
        Debug.Assert(m_OutputStratumTransitionStateTable.Rows.Count = 0)
        Debug.Assert(m_OutputStateAttributeTable.Rows.Count = 0)
        Debug.Assert(m_OutputTransitionAttributeTable.Rows.Count = 0)

    End Sub

    ''' <summary>
    ''' Initializes the amount per cell
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeCellArea()

        If (Not Me.IsSpatial) Then

            Dim drta As DataRow = Me.ResultScenario.GetDataSheet(DATASHEET_NSIC_NAME).GetDataRow()

            Me.m_TotalAmount = CDbl(drta(DATASHEET_NSIC_TOTAL_AMOUNT_COLUMN_NAME))
            Me.m_CalcNumCellsFromDist = DataTableUtilities.GetDataBool(drta, DATASHEET_NSIC_CALC_FROM_DIST_COLUMN_NAME)

        Else

            Dim drics As DataRow = Me.ResultScenario.GetDataSheet(DATASHEET_SPPIC_NAME).GetDataRow()
            Dim cellAreaTU As Double = DataTableUtilities.GetDataDbl(drics(DATASHEET_SPPIC_CELL_AREA_COLUMN_NAME))

            If cellAreaTU.Equals(0) Then
                Throw New STSimException(ERROR_SPATIAL_NO_CELL_AREA)
            End If

            Me.m_TotalAmount = cellAreaTU * Me.m_Cells.Count
            Dim drISC As DataRow = Me.ResultScenario.GetDataSheet(DATASHEET_SPPIC_NAME).GetDataRow()

            'Save the Number of Cells count, now that we have a potentially more accurate value than at config time.

            If CInt(drISC(DATASHEET_SPPIC_NUM_CELLS_COLUMN_NAME)) <> Me.m_Cells.Count Then
                drISC(DATASHEET_SPPIC_NUM_CELLS_COLUMN_NAME) = Me.m_Cells.Count
            End If

        End If

        Me.m_AmountPerCell = (Me.m_TotalAmount / CDbl(Me.m_Cells.Count))

    End Sub

    ''' <summary>
    ''' Initializes output options
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeOutputOptions()

        Dim SafeInt = Function(o As Object) As Integer
                          If (o Is DBNull.Value) Then
                              Return 0
                          Else
                              Return CInt(o)
                          End If
                      End Function

        Dim droo As DataRow = Me.ResultScenario.GetDataSheet(DATASHEET_OO_NAME).GetDataRow()

        If (Me.IsSpatial) Then

            Me.m_CreateRasterStateClassOutput = DataTableUtilities.GetDataBool(droo(DATASHEET_OO_RASTER_OUTPUT_SC_COLUMN_NAME))
            Me.m_RasterStateClassOutputTimesteps = SafeInt(droo(DATASHEET_OO_RASTER_OUTPUT_SC_TIMESTEPS_COLUMN_NAME))
            Me.m_CreateRasterTransitionOutput = DataTableUtilities.GetDataBool(droo(DATASHEET_OO_RASTER_OUTPUT_TR_COLUMN_NAME))
            Me.m_RasterTransitionOutputTimesteps = SafeInt(droo(DATASHEET_OO_RASTER_OUTPUT_TR_TIMESTEPS_COLUMN_NAME))
            Me.m_CreateRasterAgeOutput = DataTableUtilities.GetDataBool(droo(DATASHEET_OO_RASTER_OUTPUT_AGE_COLUMN_NAME))
            Me.m_RasterAgeOutputTimesteps = SafeInt(droo(DATASHEET_OO_RASTER_OUTPUT_AGE_TIMESTEPS_COLUMN_NAME))
            Me.m_CreateRasterTstOutput = DataTableUtilities.GetDataBool(droo(DATASHEET_OO_RASTER_OUTPUT_TST_COLUMN_NAME))
            Me.m_RasterTstOutputTimesteps = SafeInt(droo(DATASHEET_OO_RASTER_OUTPUT_TST_TIMESTEPS_COLUMN_NAME))
            Me.m_CreateRasterStratumOutput = DataTableUtilities.GetDataBool(droo(DATASHEET_OO_RASTER_OUTPUT_ST_COLUMN_NAME))
            Me.m_RasterStratumOutputTimesteps = SafeInt(droo(DATASHEET_OO_RASTER_OUTPUT_ST_TIMESTEPS_COLUMN_NAME))
            Me.m_CreateRasterStateAttributeOutput = DataTableUtilities.GetDataBool(droo(DATASHEET_OO_RASTER_OUTPUT_SA_COLUMN_NAME))
            Me.m_RasterStateAttributeOutputTimesteps = SafeInt(droo(DATASHEET_OO_RASTER_OUTPUT_SA_TIMESTEPS_COLUMN_NAME))
            Me.m_CreateRasterTransitionAttributeOutput = DataTableUtilities.GetDataBool(droo(DATASHEET_OO_RASTER_OUTPUT_TA_COLUMN_NAME))
            Me.m_RasterTransitionAttributeOutputTimesteps = SafeInt(droo(DATASHEET_OO_RASTER_OUTPUT_TA_TIMESTEPS_COLUMN_NAME))
            Me.m_CreateRasterAATPOutput = DataTableUtilities.GetDataBool(droo(DATASHEET_OO_RASTER_OUTPUT_AATP_COLUMN_NAME))
            Me.m_RasterAATPTimesteps = SafeInt(droo(DATASHEET_OO_RASTER_OUTPUT_AATP_TIMESTEPS_COLUMN_NAME))

        End If

        Me.m_CreateSummaryStateClassOutput = DataTableUtilities.GetDataBool(droo(DATASHEET_OO_SUMMARY_OUTPUT_SC_COLUMN_NAME))
        Me.m_SummaryStateClassOutputTimesteps = SafeInt(droo(DATASHEET_OO_SUMMARY_OUTPUT_SC_TIMESTEPS_COLUMN_NAME))
        Me.m_SummaryStateClassZeroValues = DataTableUtilities.GetDataBool(droo(DATASHEET_OO_SUMMARY_OUTPUT_SC_ZERO_VALUES_COLUMN_NAME))
        Me.m_CreateSummaryTransitionOutput = DataTableUtilities.GetDataBool(droo(DATASHEET_OO_SUMMARY_OUTPUT_TR_COLUMN_NAME))
        Me.m_SummaryTransitionOutputTimesteps = SafeInt(droo(DATASHEET_OO_SUMMARY_OUTPUT_TR_TIMESTEPS_COLUMN_NAME))
        Me.m_SummaryTransitionOutputAsIntervalMean = DataTableUtilities.GetDataBool(droo(DATASHEET_OO_SUMMARY_OUTPUT_TR_INTERVAL_MEAN_COLUMN_NAME))
        Me.m_CreateSummaryTransitionByStateClassOutput = DataTableUtilities.GetDataBool(droo(DATASHEET_OO_SUMMARY_OUTPUT_TRSC_COLUMN_NAME))
        Me.m_SummaryTransitionByStateClassOutputTimesteps = SafeInt(droo(DATASHEET_OO_SUMMARY_OUTPUT_TRSC_TIMESTEPS_COLUMN_NAME))
        Me.m_CreateSummaryStateAttributeOutput = DataTableUtilities.GetDataBool(droo(DATASHEET_OO_SUMMARY_OUTPUT_SA_COLUMN_NAME))
        Me.m_SummaryStateAttributeOutputTimesteps = SafeInt(droo(DATASHEET_OO_SUMMARY_OUTPUT_SA_TIMESTEPS_COLUMN_NAME))
        Me.m_CreateSummaryTransitionAttributeOutput = DataTableUtilities.GetDataBool(droo(DATASHEET_OO_SUMMARY_OUTPUT_TA_COLUMN_NAME))
        Me.m_SummaryTransitionAttributeOutputTimesteps = SafeInt(droo(DATASHEET_OO_SUMMARY_OUTPUT_TA_TIMESTEPS_COLUMN_NAME))
        Me.m_SummaryOmitSecondaryStrata = DataTableUtilities.GetDataBool(droo(DATASHEET_OO_SUMMARY_OUTPUT_OMIT_SS_COLUMN_NAME))
        Me.m_SummaryOmitTertiaryStrata = DataTableUtilities.GetDataBool(droo(DATASHEET_OO_SUMMARY_OUTPUT_OMIT_TS_COLUMN_NAME))

    End Sub

    ''' <summary>
    ''' Initializes the model collections from the data in the input data feeds
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeModelCollections()

        Me.FillCellCollection()
        Me.FillStratumCollection()
        Me.FillSecondaryStratumCollection()
        Me.FillTertiaryStratumCollection()
        Me.FillStateClassCollection()
        Me.FillTransitionGroupCollection()
        Me.FillTransitionTypeCollection()
        Me.FillTransitionGroupTypeCollection()
        Me.FillTransitionTypeGroupCollection()
        Me.FillTransitionMultiplierTypeCollection()
        Me.FillTransitionAttributeTypeCollection()
        Me.FillDeterministicTransitionsCollection()
        Me.FillProbabilisticTransitionsCollection()
        Me.FillStateAttributeValueCollection()
        Me.FillTransitionAttributeValueCollection()
        Me.FillTransitionAttributeTargetCollection()
        Me.FillTransitionOrderCollection()
        Me.FillTransitionTargetCollection()
        Me.FillTstTransitionGroupCollection()
        Me.FillTstRandomizeCollection()
        Me.FillTransitionMultiplierValueCollection()

        If (Me.IsSpatial) Then

            Me.FillPatchPrioritizationCollection()
            Me.FillTransitionSpatialMultiplierCollection()
            Me.FillTransitionSpatialInitiationMultiplierCollection()
            Me.FillTransitionSizeDistributionCollection()
            Me.FillTransitionSpreadDistributionCollection()
            Me.FillTransitionPatchPrioritizationCollection()
            Me.FillTransitionSizePrioritizationCollection()
            Me.FillTransitionDirectionMultiplierCollection()
            Me.FillTransitionSlopeMultiplierCollection()
            Me.FillTransitionAdjacencySettingCollection()
            Me.FillTransitionAdjacencyMultiplierCollection()
            Me.FillTransitionPathwayAutoCorrelationCollection()

            Me.ValidateSpatialPrimaryGroups()

        End If

    End Sub

    ''' <summary>
    ''' Creates a dictionary that maps all timesteps to another 'aggregator' timestep
    ''' </summary>
    ''' <remarks>This function exists to support the 'calculate as interval mean values' feature for summary transition output.</remarks>
    Friend Sub InitializeIntervalMeanTimestepMap()

        Debug.Assert(Me.m_IntervalMeanTimestepMap Is Nothing)

        If (Not Me.m_SummaryTransitionOutputAsIntervalMean) Then
            Return
        End If

        Debug.Assert(Me.MinimumTimestep > 0)

        Me.m_IntervalMeanTimestepMap = New IntervalMeanTimestepMap(
            Me.MinimumTimestep, Me.MaximumTimestep, Me.m_TimestepZero, Me.m_SummaryTransitionOutputTimesteps)

    End Sub

    ''' <summary>
    ''' Initializes the state attributes map and collections of active attribute type Ids
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeStateAttributes()

        Debug.Assert(Me.m_StateAttributeTypeIdsAges Is Nothing)
        Debug.Assert(Me.m_StateAttributeTypeIdsNoAges Is Nothing)

        Me.m_StateAttributeTypeIdsAges = New Dictionary(Of Integer, Boolean)
        Me.m_StateAttributeTypeIdsNoAges = New Dictionary(Of Integer, Boolean)

        Dim AgesColl As New StateAttributeValueCollection()
        Dim NoAgesColl As New StateAttributeValueCollection()

        For Each attr As StateAttributeValue In Me.m_StateAttributeValues

            If (attr.MinimumAge.HasValue Or attr.MaximumAge.HasValue) Then

                AgesColl.Add(attr)

                If (Not Me.m_StateAttributeTypeIdsAges.ContainsKey(attr.AttributeTypeId)) Then
                    Me.m_StateAttributeTypeIdsAges.Add(attr.AttributeTypeId, True)
                End If

            Else

                NoAgesColl.Add(attr)

                If (Not Me.m_StateAttributeTypeIdsNoAges.ContainsKey(attr.AttributeTypeId)) Then
                    Me.m_StateAttributeTypeIdsNoAges.Add(attr.AttributeTypeId, True)
                End If

            End If

        Next

        Debug.Assert(Me.m_StateAttributeValueMapAges Is Nothing)
        Debug.Assert(Me.m_StateAttributeValueMapNoAges Is Nothing)
        Debug.Assert(AgesColl.Count + NoAgesColl.Count = Me.m_StateAttributeValues.Count)

        Me.m_StateAttributeValueMapAges = New StateAttributeValueMap(Me.ResultScenario, AgesColl)
        Me.m_StateAttributeValueMapNoAges = New StateAttributeValueMap(Me.ResultScenario, NoAgesColl)

    End Sub

    ''' <summary>
    ''' Initializes the transition attribute map and collection of active attribute type Ids
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTransitionAttributes()

        Debug.Assert(Me.m_TransitionAttributeTypeIds Is Nothing)
        Me.m_TransitionAttributeTypeIds = New Dictionary(Of Integer, Boolean)

        For Each attr As TransitionAttributeValue In Me.m_TransitionAttributeValues

            If (Not Me.m_TransitionAttributeTypeIds.ContainsKey(attr.AttributeTypeId)) Then
                Me.m_TransitionAttributeTypeIds.Add(attr.AttributeTypeId, True)
            End If

        Next

        Debug.Assert(Me.m_TransitionAttributeValueMap Is Nothing)
        Me.m_TransitionAttributeValueMap = New TransitionAttributeValueMap(Me.ResultScenario, Me.m_TransitionAttributeValues)

    End Sub

    ''' <summary>
    ''' Initializes a separate list of transition groups that can be shuffled
    ''' </summary>
    ''' <remarks>
    ''' The main list is keyed and cannot be shuffled, but we need a shuffled list for doing raster simulations
    ''' </remarks>
    Private Sub InitializeShufflableTransitionGroups()

        Debug.Assert(Me.m_ShufflableTransitionGroups.Count = 0)

        For Each tg As TransitionGroup In Me.m_TransitionGroups
            Me.m_ShufflableTransitionGroups.Add(tg)
        Next

    End Sub

    ''' <summary>
    ''' Calculates the sum of the Non Spatial Initial Conditions relative amount
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CalcSumOfRelativeAmount(iteration As Integer?) As Double

        Dim sumOfRelativeAmount As Double = 0.0

        Dim icds As InitialConditionsDistributionCollection = Me.m_InitialConditionsDistributionMap.GetICDs(iteration)

        For Each sis As InitialConditionsDistribution In icds
            sumOfRelativeAmount += sis.RelativeAmount
        Next

        If (sumOfRelativeAmount <= 0.0) Then
            ExceptionUtils.ThrowArgumentException("The sum of the relative amount cannot be zero.")
        End If

        Return sumOfRelativeAmount

    End Function

End Class
