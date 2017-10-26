'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core
Imports SyncroSim.Common
Imports SyncroSim.StochasticTime

Partial Class STSimTransformer

    'Output Options
    Private m_CreateSummaryStateClassOutput As Boolean
    Private m_SummaryStateClassOutputTimesteps As Integer
    Private m_SummaryStateClassZeroValues As Boolean
    Private m_CreateSummaryTransitionOutput As Boolean
    Private m_SummaryTransitionOutputTimesteps As Integer
    Private m_SummaryTransitionOutputAsIntervalMean As Boolean
    Private m_CreateSummaryTransitionByStateClassOutput As Boolean
    Private m_SummaryTransitionByStateClassOutputTimesteps As Integer
    Private m_CreateSummaryStateAttributeOutput As Boolean
    Private m_SummaryStateAttributeOutputTimesteps As Integer
    Private m_CreateSummaryTransitionAttributeOutput As Boolean
    Private m_SummaryTransitionAttributeOutputTimesteps As Integer
    Private m_CreateRasterStateClassOutput As Boolean
    Private m_RasterStateClassOutputTimesteps As Integer
    Private m_CreateRasterTransitionOutput As Boolean
    Private m_RasterTransitionOutputTimesteps As Integer
    Private m_CreateRasterAgeOutput As Boolean
    Private m_RasterAgeOutputTimesteps As Integer
    Private m_CreateRasterStratumOutput As Boolean
    Private m_RasterStratumOutputTimesteps As Integer
    Private m_CreateRasterTstOutput As Boolean
    Private m_RasterTstOutputTimesteps As Integer
    Private m_CreateRasterStateAttributeOutput As Boolean
    Private m_RasterStateAttributeOutputTimesteps As Integer
    Private m_CreateRasterTransitionAttributeOutput As Boolean
    Private m_RasterTransitionAttributeOutputTimesteps As Integer
    Private m_CreateRasterAATPOutput As Boolean
    Private m_RasterAATPTimesteps As Integer

    'Output Collections
    Private m_SummaryStratumStateResults As New OutputStratumStateCollection
    Private m_SummaryStratumStateResultsZeroValues As New OutputStratumStateCollectionZeroValues
    Private m_SummaryStratumTransitionResults As New OutputStratumTransitionCollection
    Private m_SummaryStratumTransitionStateResults As New OutputStratumTransitionStateCollection
    Private m_SummaryStateAttributeResults As New OutputStateAttributeCollection
    Private m_SummaryTransitionAttributeResults As New OutputTransitionAttributeCollection

    'Output data tables
    Private m_OutputStratumAmountTable As DataTable
    Private m_OutputStratumStateTable As DataTable
    Private m_OutputStratumTransitionTable As DataTable
    Private m_OutputStratumTransitionStateTable As DataTable
    Private m_OutputStateAttributeTable As DataTable
    Private m_OutputTransitionAttributeTable As DataTable

    ''' <summary>
    ''' Determines whether or not to do summary state class output for the specified timestep
    ''' </summary>
    ''' <param name="timestep">The timestep</param>
    ''' <returns>
    ''' True if the timestep is the first timestep, the last timestep, or the timestep is in the set specified by the user.  
    ''' False will be returned if the user has not specified that this type of output should be generated or if the conditions for True are not met.
    ''' </returns>
    ''' <remarks></remarks>
    Private Function IsSummaryStateClassTimestep(ByVal timestep As Integer) As Boolean
        Return Me.IsOutputTimestep(timestep, Me.m_SummaryStateClassOutputTimesteps, Me.m_CreateSummaryStateClassOutput)
    End Function

    ''' <summary>
    ''' Determines whether or not to do summary transition output for the specified timestep
    ''' </summary>
    ''' <param name="timestep">The timestep</param>
    ''' <returns>
    ''' True if the timestep is the first timestep, the last timestep, or the timestep is in the set specified by the user.  
    ''' False will be returned if the user has not specified that this type of output should be generated or if the conditions for True are not met.
    ''' </returns>
    ''' <remarks></remarks>
    Private Function IsSummaryTransitionTimestep(ByVal timestep As Integer) As Boolean
        Return Me.IsOutputTimestep(timestep, Me.m_SummaryTransitionOutputTimesteps, Me.m_CreateSummaryTransitionOutput)
    End Function

    ''' <summary>
    ''' Determines whether or not to do summary transition by state class output for the specified timestep
    ''' </summary>
    ''' <param name="timestep">The timestep</param>
    ''' <returns>
    ''' True if the timestep is the first timestep, the last timestep, or the timestep is in the set specified by the user.  
    ''' False will be returned if the user has not specified that this type of output should be generated or if the conditions for True are not met.
    ''' </returns>
    ''' <remarks></remarks>
    Private Function IsSummaryTransitionByStateClassTimestep(ByVal timestep As Integer) As Boolean
        Return Me.IsOutputTimestep(timestep, Me.m_SummaryTransitionByStateClassOutputTimesteps, Me.m_CreateSummaryTransitionByStateClassOutput)
    End Function

    ''' <summary>
    ''' Determines whether or not to do summary state attribute output for the specified timestep
    ''' </summary>
    ''' <param name="timestep">The timestep</param>
    ''' <returns>
    ''' True if the timestep is the first timestep, the last timestep, or the timestep is in the set specified by the user.  
    ''' False will be returned if the user has not specified that this type of output should be generated or if the conditions for True are not met.
    ''' </returns>
    ''' <remarks></remarks>
    Private Function IsSummaryStateAttributeTimestep(ByVal timestep As Integer) As Boolean
        Return Me.IsOutputTimestep(timestep, Me.m_SummaryStateAttributeOutputTimesteps, Me.m_CreateSummaryStateAttributeOutput)
    End Function

    ''' <summary>
    ''' Determines whether or not to do summary transition attribute output for the specified timestep
    ''' </summary>
    ''' <param name="timestep">The timestep</param>
    ''' <returns>
    ''' True if the timestep is the first timestep, the last timestep, or the timestep is in the set specified by the user.  
    ''' False will be returned if the user has not specified that this type of output should be generated or if the conditions for True are not met.
    ''' </returns>
    ''' <remarks></remarks>
    Private Function IsSummaryTransitionAttributeTimestep(ByVal timestep As Integer) As Boolean
        Return Me.IsOutputTimestep(timestep, Me.m_SummaryTransitionAttributeOutputTimesteps, Me.m_CreateSummaryTransitionAttributeOutput)
    End Function

    ''' <summary>
    ''' Determines whether or not to do Raster Age output for the specified timestep
    ''' </summary>
    ''' <param name="timestep">The timestep</param>
    ''' <returns>
    ''' True if the timestep is the first timestep, the last timestep, or the timestep is in the set specified by the user.  
    ''' False will be returned if the user has not specified that this type of output should be generated or if the conditions for True are not met.
    ''' </returns>
    ''' <remarks></remarks>
    Private Function IsRasterStateClassTimestep(ByVal timestep As Integer) As Boolean
        Return Me.IsOutputTimestep(timestep, Me.m_RasterStateClassOutputTimesteps, Me.m_CreateRasterStateClassOutput)
    End Function

    ''' <summary>
    ''' Determines whether or not to do Raster Transition output for the specified timestep
    ''' </summary>
    ''' <param name="timestep">The timestep</param>
    ''' <returns>
    ''' True if the timestep is the first timestep, the last timestep, or the timestep is in the set specified by the user.  
    ''' False will be returned if the user has not specified that this type of output should be generated or if the conditions for True are not met.
    ''' </returns>
    ''' <remarks></remarks>
    Private Function IsRasterTransitionTimestep(ByVal timestep As Integer) As Boolean
        Return Me.IsOutputTimestep(timestep, Me.m_RasterTransitionOutputTimesteps, Me.m_CreateRasterTransitionOutput)
    End Function

    ''' <summary>
    ''' Determines whether or not to do Raster Age output for the specified timestep
    ''' </summary>
    ''' <param name="timestep">The timestep</param>
    ''' <returns>
    ''' True if the timestep is the first timestep, the last timestep, or the timestep is in the set specified by the user.  
    ''' False will be returned if the user has not specified that this type of output should be generated or if the conditions for True are not met.
    ''' </returns>
    ''' <remarks></remarks>
    Private Function IsRasterAgeTimestep(ByVal timestep As Integer) As Boolean
        Return Me.IsOutputTimestep(timestep, Me.m_RasterAgeOutputTimesteps, Me.m_CreateRasterAgeOutput)
    End Function

    ''' <summary>
    ''' Determines whether or not to do Raster Tst output for the specified timestep
    ''' </summary>
    ''' <param name="timestep">The timestep</param>
    ''' <returns>
    ''' True if the timestep is the first timestep, the last timestep, or the timestep is in the set specified by the user.  
    ''' False will be returned if the user has not specified that this type of output should be generated or if the conditions for True are not met.
    ''' </returns>
    ''' <remarks></remarks>
    Private Function IsRasterTstTimestep(ByVal timestep As Integer) As Boolean
        Return Me.IsOutputTimestep(timestep, Me.m_RasterTstOutputTimesteps, Me.m_CreateRasterTstOutput)
    End Function

    ''' <summary>
    ''' Determines whether or not to do Raster Age output for the specified timestep
    ''' </summary>
    ''' <param name="timestep">The timestep</param>
    ''' <returns>
    ''' True if the timestep is the first timestep, the last timestep, or the timestep is in the set specified by the user.  
    ''' False will be returned if the user has not specified that this type of output should be generated or if the conditions for True are not met.
    ''' </returns>
    ''' <remarks></remarks>
    Private Function IsRasterStratumTimestep(ByVal timestep As Integer) As Boolean
        Return Me.IsOutputTimestep(timestep, Me.m_RasterStratumOutputTimesteps, Me.m_CreateRasterStratumOutput)
    End Function

    ''' <summary>
    ''' Determines whether or not to do raster state attribute output for the specified timestep
    ''' </summary>
    ''' <param name="timestep">The timestep</param>
    ''' <returns>
    ''' True if the timestep is the first timestep, the last timestep, or the timestep is in the set specified by the user.  
    ''' False will be returned if the user has not specified that this type of output should be generated or if the conditions for True are not met.
    ''' </returns>
    ''' <remarks></remarks>
    Private Function IsRasterStateAttributeTimestep(ByVal timestep As Integer) As Boolean
        Return Me.IsOutputTimestep(timestep, Me.m_RasterStateAttributeOutputTimesteps, Me.m_CreateRasterStateAttributeOutput)
    End Function

    ''' <summary>
    ''' Determines whether or not to do  transition adjacency state attribute calculation for the specified timestep
    ''' </summary>
    ''' <param name="timestep">The timestep</param>
    ''' <returns>
    ''' True if the timestep is the first timestep, the last timestep, or the timestep is in the set specified by the user.  
    ''' False will be returned if the user has not specified that this type of output should be generated or if the 
    ''' conditions for True are not met.
    ''' </returns>
    ''' <remarks></remarks>
    Private Function IsTransitionAdjacencyStateAttributeTimestep(ByVal timestep As Integer, ByVal transitionGroupId As Integer) As Boolean

        Dim setting As TransitionAdjacencySetting = Me.m_TransitionAdjacencySettingMap.GetItem(transitionGroupId)

        If (setting Is Nothing) Then
            Return False
        End If

        Return Me.IsOutputTimestep(timestep, setting.UpdateFrequency, True)

    End Function

    ''' <summary>
    ''' Determines whether or not to do raster transition attribute output for the specified timestep
    ''' </summary>
    ''' <param name="timestep">The timestep</param>
    ''' <returns>
    ''' True if the timestep is the first timestep, the last timestep, or the timestep is in the set specified by the user.  
    ''' False will be returned if the user has not specified that this type of output should be generated or if the conditions for True are not met.
    ''' </returns>
    ''' <remarks></remarks>
    Private Function IsRasterTransitionAttributeTimestep(ByVal timestep As Integer) As Boolean
        Return Me.IsOutputTimestep(timestep, Me.m_RasterTransitionAttributeOutputTimesteps, Me.m_CreateRasterTransitionAttributeOutput)
    End Function

    ''' <summary>
    ''' Determines whether or not the specified timestep is an Output timestep
    ''' </summary>
    ''' <param name="timestep">The timestep to test</param>
    ''' <param name="frequency">The frequency for timestep output</param>
    ''' <param name="shouldCreateOutput">Whether or not to create output</param>
    ''' <returns>
    ''' True if the timestep is the first timestep, the last timestep, or the timestep is within the frequency specified by the user.  
    ''' False will be returned if the user has not specified that this type of output should be generated or if the conditions for True are not met.
    ''' </returns>
    ''' <remarks>
    ''' The frequency of the timestep corresponds to the values that the user has specified for timestep output.  For example, someone
    ''' might specifiy that they only want data every 5 timesteps.  In this case, the frequency will be 5.
    ''' </remarks>
    Public Function IsOutputTimestep(ByVal timestep As Integer, ByVal frequency As Integer, ByVal shouldCreateOutput As Boolean) As Boolean

        If (shouldCreateOutput) Then

            If (timestep = Me.MinimumTimestep Or timestep = Me.MaximumTimestep) Then
                Return True
            End If

            If (((timestep - Me.m_TimestepZero) Mod frequency) = 0) Then
                Return True
            End If

        End If

        Return False

    End Function

    ''' <summary>
    ''' Called to record state class summary output for the specified simulation cell, iteration, and timestep
    ''' </summary>
    ''' <param name="simulationCell">The simulation cell</param>
    ''' <param name="iteration">The current iteration</param>
    ''' <param name="timestep">The current timestep</param>
    ''' <remarks>This function aggregates by stratum, iteration, timestep, and state class.</remarks>
    Private Sub OnSummaryStateClassOutput(
        ByVal simulationCell As Cell,
        ByVal iteration As Integer,
        ByVal timestep As Integer)

        If (simulationCell.StratumId = 0 Or simulationCell.StateClassId = 0) Then
            Return
        End If

        If ((Me.IsSummaryStateClassTimestep(timestep)) Or
            (Me.m_StateAttributeTypeIdsNoAges.Count > 0 And Me.IsSummaryStateAttributeTimestep(timestep))) Then

            Dim AgeKey As Integer = Me.m_AgeReportingHelper.GetKey(simulationCell.Age)

            Dim key As New SevenIntegerLookupKey(
                simulationCell.StratumId,
                GetSecondaryStratumIdKey(simulationCell),
                GetTertiaryStratumIdKey(simulationCell),
                iteration,
                timestep,
                simulationCell.StateClassId,
                AgeKey)

            If (Me.m_SummaryStratumStateResults.Contains(key)) Then

                Dim oss As OutputStratumState = Me.m_SummaryStratumStateResults(key)
                oss.Amount += Me.m_AmountPerCell

            Else

                Dim oss As New OutputStratumState(
                    simulationCell.StratumId,
                    simulationCell.SecondaryStratumId,
                    simulationCell.TertiaryStratumId,
                    iteration,
                    timestep,
                    simulationCell.StateClassId,
                    Me.m_AgeReportingHelper.GetAgeMinimum(simulationCell.Age),
                    Me.m_AgeReportingHelper.GetAgeMaximum(simulationCell.Age),
                    AgeKey,
                    Me.m_AmountPerCell)

                Me.m_SummaryStratumStateResults.Add(oss)

            End If

        End If

    End Sub

    ''' <summary>
    ''' Called to record transition class summary output for the specified simulation cell, iteration, and timestep
    ''' </summary>
    ''' <param name="simulationCell">The simulation cell</param>
    ''' <param name="currentTransition">The current transition</param>
    ''' <param name="iteration">The current iteration</param>
    ''' <param name="timestep">The current timestep</param>
    ''' <remarks>This function aggregates by stratum, iteration, timestep, and transition group.</remarks>
    Private Sub OnSummaryTransitionOutput(
        ByVal simulationCell As Cell,
        ByVal currentTransition As Transition,
        ByVal iteration As Integer,
        ByVal timestep As Integer)

        If (simulationCell.StratumId = 0 Or simulationCell.StateClassId = 0) Then
            Return
        End If

        If (Me.m_SummaryTransitionOutputAsIntervalMean) Then
            Me.RecordTransitionOutputIntervalMeanMethod(simulationCell, currentTransition, iteration, timestep)
        Else
            Me.RecordTransitionOutputNormalMethod(simulationCell, currentTransition, iteration, timestep)
        End If

    End Sub

    ''' <summary>
    ''' Called to record transition by state class summary output for the specified simulation cell, iteration, and timestep
    ''' </summary>
    ''' <param name="simulationCell">The simulation cell</param>
    ''' <param name="currentTransition">The current transition</param>
    ''' <param name="iteration">The current iteration</param>
    ''' <param name="timestep">The current timestep</param>
    ''' <remarks>This function aggregates by stratum, state class source, state class destination, and transition</remarks>
    Private Sub OnSummaryTransitionByStateClassOutput(
        ByVal simulationCell As Cell,
        ByVal currentTransition As Transition,
        ByVal iteration As Integer,
        ByVal timestep As Integer)

        If (simulationCell.StratumId = 0 Or simulationCell.StateClassId = 0) Then
            Return
        End If

        If (Me.IsSummaryTransitionByStateClassTimestep(timestep)) Then

            Dim DestStateClass As Integer = simulationCell.StateClassId

            If (currentTransition.StateClassIdDestination.HasValue) Then
                DestStateClass = currentTransition.StateClassIdDestination.Value
            End If

            Dim key As New EightIntegerLookupKey(
                simulationCell.StratumId,
                GetSecondaryStratumIdKey(simulationCell),
                GetTertiaryStratumIdKey(simulationCell),
                iteration,
                timestep,
                currentTransition.TransitionTypeId,
                currentTransition.StateClassIdSource,
                DestStateClass)

            If (Me.m_SummaryStratumTransitionStateResults.Contains(key)) Then

                Dim osts As OutputStratumTransitionState = Me.m_SummaryStratumTransitionStateResults(key)
                osts.Amount += Me.m_AmountPerCell

            Else

                Dim osts As New OutputStratumTransitionState(
                    simulationCell.StratumId,
                    simulationCell.SecondaryStratumId,
                    simulationCell.TertiaryStratumId,
                    iteration,
                    timestep,
                    currentTransition.TransitionTypeId,
                    currentTransition.StateClassIdSource,
                    DestStateClass,
                    Me.m_AmountPerCell)

                Me.m_SummaryStratumTransitionStateResults.Add(osts)

            End If

        End If

    End Sub

    ''' <summary>
    ''' Called to record attribute summary output for the specified simulation cell, iteration, and timestep
    ''' </summary>
    ''' <param name="simulationCell">The simulation cell</param>
    ''' <param name="iteration">The current iteration</param>
    ''' <param name="timestep">The current timestep</param>
    ''' <remarks>This function aggregates by stratum, iteration, timestep, and attribute type Id.</remarks>
    Private Sub OnSummaryStateAttributeOutput(
        ByVal simulationCell As Cell,
        ByVal iteration As Integer,
        ByVal timestep As Integer)

        If (simulationCell.StratumId = 0 Or simulationCell.StateClassId = 0) Then
            Return
        End If

        If (Me.m_StateAttributeTypeIdsAges.Count = 0) Then
            Debug.Assert(Not Me.m_StateAttributeValueMapAges.HasItems())
            Return
        End If

        If (Not Me.IsSummaryStateAttributeTimestep(timestep)) Then
            Return
        End If

        For Each AttributeTypeId As Integer In Me.m_StateAttributeTypeIdsAges.Keys

            Dim AttrValue As Nullable(Of Double) = Me.m_StateAttributeValueMapAges.GetAttributeValueByAge(
                AttributeTypeId,
                simulationCell.StratumId,
                simulationCell.SecondaryStratumId,
                simulationCell.TertiaryStratumId,
                simulationCell.StateClassId,
                iteration,
                timestep,
                simulationCell.Age)

            If (AttrValue.HasValue) Then

                Dim AgeKey As Integer = Me.m_AgeReportingHelper.GetKey(simulationCell.Age)

                Dim key As New SevenIntegerLookupKey(
                    simulationCell.StratumId,
                    GetSecondaryStratumIdKey(simulationCell),
                    GetTertiaryStratumIdKey(simulationCell),
                    iteration,
                    timestep,
                    AttributeTypeId,
                    AgeKey)

                If (Me.m_SummaryStateAttributeResults.Contains(key)) Then

                    Dim ossa As OutputStateAttribute = Me.m_SummaryStateAttributeResults(key)
                    ossa.Amount += (Me.m_AmountPerCell * AttrValue.Value)

                Else

                    Dim ossa As New OutputStateAttribute(
                        simulationCell.StratumId,
                        simulationCell.SecondaryStratumId,
                        simulationCell.TertiaryStratumId,
                        iteration,
                        timestep,
                        AttributeTypeId,
                        Me.m_AgeReportingHelper.GetAgeMinimum(simulationCell.Age),
                        Me.m_AgeReportingHelper.GetAgeMaximum(simulationCell.Age),
                        AgeKey,
                        (Me.m_AmountPerCell * AttrValue.Value))

                    Me.m_SummaryStateAttributeResults.Add(ossa)

                End If

            End If

        Next

    End Sub

    ''' <summary>
    ''' Record transition type changes for the specified Transition Group.
    ''' </summary>
    ''' <param name="dictTransitionedPixels">A dictionary of arrays of Transition Types 
    ''' which occured during the specified specified Interval / Timstep. Keyed by Transition Group Id.</param>
    ''' <param name="iteration">The current iteration</param>
    ''' <param name="timestep">The current timestep</param>
    ''' <remarks></remarks>
    Private Sub OnRasterTransitionOutput(
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal dictTransitionedPixels As Dictionary(Of Integer, Integer()))

        'Loop thru the Transition Groups found in the dictionary
        For Each transitionGroupId As Integer In dictTransitionedPixels.Keys

            Dim transitionedPixels As Integer() = dictTransitionedPixels(transitionGroupId)

            If (Me.IsRasterTransitionTimestep(timestep)) Then

                'Set up a raster as input to the Raster output function
                Dim rastOP As New StochasticTimeRaster
                Me.m_InputRasters.GetMetadata(rastOP)
                rastOP.IntCells = transitionedPixels

                'Dont bother if there haven't been any transitions
                If transitionedPixels.Distinct().Count() > 1 Then

                    RasterFiles.SaveOutputRaster(
                        rastOP,
                        Me.ResultScenario.GetDataSheet(DATASHEET_OUTPUT_SPATIAL_TRANSITION),
                        RasterDataType.DTInteger,
                        iteration,
                        timestep,
                        SPATIAL_MAP_TRANSITION_GROUP_VARIABLE_PREFIX,
                        transitionGroupId,
                        DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN)

                End If

            End If

            'Transition Summary Rasters
            RecordAnnualAvgTransitionProbabilityOutput(
                Me.MaximumIteration - Me.MinimumIteration + 1,
                timestep,
                transitionGroupId,
                transitionedPixels)

        Next

    End Sub

    ''' <summary>
    ''' Record transition attribute changes for the specified Transition Group.
    ''' </summary>
    ''' <param name="RasterTransitionAttrValues"></param>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <remarks></remarks>
    Private Sub OnRasterTransitionAttributeOutput(
        ByVal RasterTransitionAttrValues As Dictionary(Of Integer, Double()),
        ByVal iteration As Integer,
        ByVal timestep As Integer)

        If (Me.IsRasterTransitionAttributeTimestep(timestep)) Then

            For Each AttributeId As Integer In RasterTransitionAttrValues.Keys

                'Set up a raster as input to the Raster output function
                Dim rastOP As New StochasticTimeRaster
                Me.m_InputRasters.GetMetadata(rastOP)
                rastOP.DblCells = RasterTransitionAttrValues(AttributeId)

                RasterFiles.SaveOutputRaster(
                    rastOP,
                    Me.ResultScenario.GetDataSheet(DATASHEET_OUTPUT_SPATIAL_TRANSITION_ATTRIBUTE),
                    RasterDataType.DTDouble,
                    iteration,
                    timestep,
                    SPATIAL_MAP_TRANSITION_ATTRIBUTE_VARIABLE_PREFIX,
                    AttributeId,
                    DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN)

            Next

        End If

    End Sub

    ''' <summary>
    ''' Records summary transition output using the 'calculate as interval mean values' method
    ''' </summary>
    ''' <param name="simulationCell">The simulation cell</param>
    ''' <param name="currentTransition">The current transition</param>
    ''' <param name="iteration">The current iteration</param>
    ''' <param name="timestep">The current timestep</param>
    ''' <remarks>This function aggregates by stratum, iteration, timestep, and transition group.</remarks>
    Private Sub RecordTransitionOutputIntervalMeanMethod(
        ByVal simulationCell As Cell,
        ByVal currentTransition As Transition,
        ByVal iteration As Integer,
        ByVal timestep As Integer)

        'Look up the output record using the aggregator timestep instead of the actual timestep.
        Dim AggregatorTimestep As Integer = Me.m_IntervalMeanTimestepMap.GetValue(timestep)
        Dim tt As TransitionType = Me.m_TransitionTypes(currentTransition.TransitionTypeId)

        For Each tg As TransitionGroup In tt.TransitionGroups

            Dim AgeKey As Integer = Me.m_AgeReportingHelper.GetKey(simulationCell.Age)

            Dim key As New SevenIntegerLookupKey(
                simulationCell.StratumId,
                GetSecondaryStratumIdKey(simulationCell),
                GetTertiaryStratumIdKey(simulationCell),
                iteration,
                AggregatorTimestep,
                tg.TransitionGroupId,
                AgeKey)

            If (Me.m_SummaryStratumTransitionResults.Contains(key)) Then

                Dim ost As OutputStratumTransition = Me.m_SummaryStratumTransitionResults(key)
                ost.Amount += Me.m_AmountPerCell

            Else

                Dim ost As New OutputStratumTransition(
                    simulationCell.StratumId,
                    simulationCell.SecondaryStratumId,
                    simulationCell.TertiaryStratumId,
                    iteration,
                    AggregatorTimestep,
                    tg.TransitionGroupId,
                    Me.m_AgeReportingHelper.GetAgeMinimum(simulationCell.Age),
                    Me.m_AgeReportingHelper.GetAgeMaximum(simulationCell.Age),
                    AgeKey,
                    Me.m_AmountPerCell)

                Me.m_SummaryStratumTransitionResults.Add(ost)

            End If

        Next

    End Sub

    ''' <summary>
    ''' Records summary transition output using the normal method
    ''' </summary>
    ''' <param name="simulationCell">The simulation cell</param>
    ''' <param name="currentTransition">The current transition</param>
    ''' <param name="iteration">The current iteration</param>
    ''' <param name="timestep">The current timestep</param>
    ''' <remarks>This function aggregates by stratum, iteration, timestep, and transition group.</remarks>
    Private Sub RecordTransitionOutputNormalMethod(
        ByVal simulationCell As Cell,
        ByVal currentTransition As Transition,
        ByVal iteration As Integer,
        ByVal timestep As Integer)

        If (Me.IsSummaryTransitionTimestep(timestep)) Then

            Dim tt As TransitionType = Me.m_TransitionTypes(currentTransition.TransitionTypeId)

            For Each tg As TransitionGroup In tt.TransitionGroups

                Dim AgeKey As Integer = Me.m_AgeReportingHelper.GetKey(simulationCell.Age)

                Dim key As New SevenIntegerLookupKey(
                    simulationCell.StratumId,
                    GetSecondaryStratumIdKey(simulationCell),
                    GetTertiaryStratumIdKey(simulationCell),
                    iteration,
                    timestep,
                    tg.TransitionGroupId,
                    AgeKey)

                If (Me.m_SummaryStratumTransitionResults.Contains(key)) Then

                    Dim ost As OutputStratumTransition = Me.m_SummaryStratumTransitionResults(key)
                    ost.Amount += Me.m_AmountPerCell

                Else

                    Dim ost As New OutputStratumTransition(
                        simulationCell.StratumId,
                        simulationCell.SecondaryStratumId,
                        simulationCell.TertiaryStratumId,
                        iteration,
                        timestep,
                        tg.TransitionGroupId,
                        Me.m_AgeReportingHelper.GetAgeMinimum(simulationCell.Age),
                        Me.m_AgeReportingHelper.GetAgeMaximum(simulationCell.Age),
                        AgeKey,
                        Me.m_AmountPerCell)

                    Me.m_SummaryStratumTransitionResults.Add(ost)

                End If

            Next

        End If

    End Sub

    ''' <summary>
    ''' Generates state class attributes for the current summary state class records
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GenerateStateClassAttributes()

        If (Me.m_StateAttributeTypeIdsNoAges.Count = 0) Then

            Debug.Assert(Not Me.m_StateAttributeValueMapNoAges.HasItems())
            Return

        End If

        For Each oss As OutputStratumState In Me.m_SummaryStratumStateResults

            Dim AgeKey As Integer = AGE_KEY_NO_AGES

            If (oss.AgeMin.HasValue) Then
                AgeKey = Me.m_AgeReportingHelper.GetKey(oss.AgeMin.Value)
            End If

            For Each AttributeTypeId As Integer In Me.m_StateAttributeTypeIdsNoAges.Keys

                Dim AttrValue As Nullable(Of Double) =
                    Me.m_StateAttributeValueMapNoAges.GetAttributeValueNoAge(
                        AttributeTypeId,
                        oss.StratumId,
                        oss.SecondaryStratumId,
                        oss.TertiaryStratumId,
                        oss.StateClassId,
                        oss.Iteration,
                        oss.Timestep)

                If (AttrValue.HasValue) Then

                    Dim key As New SevenIntegerLookupKey(
                        oss.StratumId,
                        GetNullableKey(oss.SecondaryStratumId),
                        GetNullableKey(oss.TertiaryStratumId),
                        oss.Iteration,
                        oss.Timestep,
                        AttributeTypeId,
                        AgeKey)

                    If (Me.m_SummaryStateAttributeResults.Contains(key)) Then

                        Dim osa As OutputStateAttribute = Me.m_SummaryStateAttributeResults(key)
                        osa.Amount += (oss.Amount * AttrValue.Value)

                    Else

                        Dim osa As New OutputStateAttribute(
                            oss.StratumId,
                            oss.SecondaryStratumId,
                            oss.TertiaryStratumId,
                            oss.Iteration,
                            oss.Timestep,
                            AttributeTypeId,
                            oss.AgeMin,
                            oss.AgeMax,
                            AgeKey,
                            (oss.Amount * AttrValue.Value))

                        Me.m_SummaryStateAttributeResults.Add(osa)

                    End If

                End If

            Next

        Next

    End Sub

    ''' <summary>
    ''' Generates state class attributes for the current summary transition records
    ''' </summary>
    ''' <param name="simulationCell"></param>
    ''' <param name="tr"></param>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <remarks></remarks>
    Private Sub GenerateTransitionAttributes(
        ByVal simulationCell As Cell,
        ByVal tr As Transition,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal rasterTransitionAttrValues As Dictionary(Of Integer, Double()))

        If (simulationCell.StratumId = 0 Or simulationCell.StateClassId = 0) Then
            Return
        End If

        If (Not Me.m_TransitionAttributeValueMap.HasItems()) Then
            Return
        End If

        Dim tt As TransitionType = Me.m_TransitionTypes(tr.TransitionTypeId)

        For Each AttributeTypeId As Integer In Me.m_TransitionAttributeTypeIds.Keys

            For Each tg As TransitionGroup In tt.TransitionGroups

                Dim AttrValue As Nullable(Of Double) =
                    Me.m_TransitionAttributeValueMap.GetAttributeValue(
                        AttributeTypeId,
                        tg.TransitionGroupId,
                        simulationCell.StratumId,
                        simulationCell.SecondaryStratumId,
                        simulationCell.TertiaryStratumId,
                        simulationCell.StateClassId,
                        iteration,
                        timestep,
                        simulationCell.Age)

                If (AttrValue.HasValue) Then

                    If (Me.IsSpatial And Me.IsRasterTransitionAttributeTimestep(timestep)) Then

                        Dim arr() As Double = rasterTransitionAttrValues(AttributeTypeId)
                        If arr(simulationCell.CellId) = StochasticTimeRaster.DefaultNoDataValue Then
                            arr(simulationCell.CellId) = AttrValue.Value
                        Else
                            arr(simulationCell.CellId) += AttrValue.Value
                        End If

                    End If

                    Dim Target As TransitionAttributeTarget = Me.m_TransitionAttributeTargetMap.GetAttributeTarget(
                        AttributeTypeId, simulationCell.StratumId, simulationCell.SecondaryStratumId, simulationCell.TertiaryStratumId, iteration, timestep)

                    If (Target IsNot Nothing) Then

                        Target.TargetRemaining -= AttrValue.Value * m_AmountPerCell

                        If Target.TargetRemaining < 0.0 Then
                            Target.TargetRemaining = 0.0
                        End If

                    End If

                    If (Me.IsSummaryTransitionAttributeTimestep(timestep)) Then

                        Dim AgeKey As Integer = Me.m_AgeReportingHelper.GetKey(simulationCell.Age)

                        Dim key As New SixIntegerLookupKey(
                            simulationCell.StratumId,
                            GetSecondaryStratumIdKey(simulationCell),
                            iteration,
                            timestep,
                            AttributeTypeId,
                            AgeKey)

                        If (Me.m_SummaryTransitionAttributeResults.Contains(key)) Then

                            Dim ota As OutputTransitionAttribute = Me.m_SummaryTransitionAttributeResults(key)
                            ota.Amount += (Me.m_AmountPerCell * AttrValue.Value)

                        Else

                            Dim ota As New OutputTransitionAttribute(
                                simulationCell.StratumId,
                                simulationCell.SecondaryStratumId,
                                simulationCell.TertiaryStratumId,
                                iteration,
                                timestep,
                                AttributeTypeId,
                                Me.m_AgeReportingHelper.GetAgeMinimum(simulationCell.Age),
                                Me.m_AgeReportingHelper.GetAgeMaximum(simulationCell.Age),
                                AgeKey,
                                (Me.m_AmountPerCell * AttrValue.Value))

                            Me.m_SummaryTransitionAttributeResults.Add(ota)

                        End If

                    End If

                End If

            Next

        Next

    End Sub

    ''' <summary>
    ''' Processes output stratum amounts
    ''' </summary>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <remarks></remarks>
    Private Sub ProcessOutputStratumAmounts(ByVal iteration As Integer, ByVal timestep As Integer)

        If (Not Me.IsSummaryStateClassTimestep(timestep) And
            Not Me.IsSummaryTransitionTimestep(timestep)) Then

            Return

        End If

        Dim SecondaryStratumIds As New List(Of Nullable(Of Integer))
        Dim TertiaryStratumIds As New List(Of Nullable(Of Integer))

        Dim ssnull As Nullable(Of Integer) = Nothing
        Dim tsnull As Nullable(Of Integer) = Nothing

        SecondaryStratumIds.Add(ssnull)
        TertiaryStratumIds.Add(tsnull)

        For Each s As Stratum In Me.m_SecondaryStrata
            SecondaryStratumIds.Add(s.StratumId)
        Next

        For Each s As Stratum In Me.m_TertiaryStrata
            TertiaryStratumIds.Add(s.StratumId)
        Next

        For Each PrimaryStratum As Stratum In Me.m_Strata

            For Each SecondaryStratumId As Nullable(Of Integer) In SecondaryStratumIds

                For Each TertiaryStratumId As Nullable(Of Integer) In TertiaryStratumIds

                    Dim o As Object = Me.m_ProportionAccumulatorMap.GetValue(PrimaryStratum.StratumId, SecondaryStratumId, TertiaryStratumId)

                    If (o IsNot Nothing) Then

                        Dim dr As DataRow = Me.m_OutputStratumAmountTable.NewRow

                        dr(DATASHEET_ITERATION_COLUMN_NAME) = iteration
                        dr(DATASHEET_TIMESTEP_COLUMN_NAME) = timestep
                        dr(DATASHEET_STRATUM_ID_COLUMN_NAME) = PrimaryStratum.StratumId
                        dr(DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(SecondaryStratumId)
                        dr(DATASHEET_TERTIARY_STRATUM_ID_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(TertiaryStratumId)
                        dr(DATASHEET_AMOUNT_COLUMN_NAME) = CDbl(o)

                        Me.m_OutputStratumAmountTable.Rows.Add(dr)

                    End If

                Next

            Next

        Next

    End Sub

    ''' <summary>
    ''' Processes Summary Stratum State results
    ''' </summary>
    ''' <param name="table"></param>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <remarks></remarks>
    Private Sub ProcessSummaryStratumStateResults(
        ByVal table As DataTable,
        ByVal iteration As Integer,
        ByVal timestep As Integer)

        If (Me.m_CreateSummaryStateClassOutput) Then

            If (Me.m_SummaryStateClassZeroValues) Then

                Dim SSKeys As Dictionary(Of Integer, Boolean) = Me.CreateSecondaryStratumDictionary()
                Dim TSKeys As Dictionary(Of Integer, Boolean) = Me.CreateTertiaryStratumDictionary()

                Debug.Assert(Not SSKeys.Count = 0 And Me.m_SummaryStratumStateResults.Count > 0)
                Debug.Assert(Not TSKeys.Count = 0 And Me.m_SummaryStratumStateResults.Count > 0)

                For Each ss As Integer In SSKeys.Keys

                    For Each ts As Integer In TSKeys.Keys

                        For Each dt As DeterministicTransition In Me.m_DeterministicTransitions

                            Dim key As New SixIntegerLookupKey(
                                GetNullableKey(dt.StratumIdSource), ss, ts, iteration, timestep, dt.StateClassIdSource)

                            If (Not Me.m_SummaryStratumStateResultsZeroValues.Contains(key)) Then

                                Dim oss As New OutputStratumState(
                                     GetNullableKey(dt.StratumIdSource),
                                     ss, ts, iteration, timestep, dt.StateClassIdSource, dt.AgeMinimum, dt.AgeMaximum, 0, 0.0)

                                Dim k2 As New SevenIntegerLookupKey(
                                    GetNullableKey(dt.StratumIdSource),
                                    ss, ts, iteration, timestep, dt.StateClassIdSource, 0)

                                If (Not Me.m_SummaryStratumStateResults.Contains(k2)) Then
                                    Me.m_SummaryStratumStateResults.Add(oss)
                                End If

                                Me.m_SummaryStratumStateResultsZeroValues.Add(oss)

                            End If

                        Next

                    Next

                Next

            End If

            For Each r As OutputStratumState In Me.m_SummaryStratumStateResults

                If (Me.IsSummaryStateClassTimestep(r.Timestep)) Then

                    Dim slxid As Integer = Me.m_StateClasses(r.StateClassId).StateLabelXID
                    Dim slyid As Integer = Me.m_StateClasses(r.StateClassId).StateLabelYID

                    Dim dr As DataRow = table.NewRow

                    dr(DATASHEET_ITERATION_COLUMN_NAME) = r.Iteration
                    dr(DATASHEET_TIMESTEP_COLUMN_NAME) = r.Timestep
                    dr(DATASHEET_STRATUM_ID_COLUMN_NAME) = r.StratumId
                    dr(DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(r.SecondaryStratumId)
                    dr(DATASHEET_TERTIARY_STRATUM_ID_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(r.TertiaryStratumId)
                    dr(DATASHEET_STATECLASS_ID_COLUMN_NAME) = r.StateClassId
                    dr(DATASHEET_STATECLASS_STATE_LABEL_X_ID_COLUMN_NAME) = slxid
                    dr(DATASHEET_STATECLASS_STATE_LABEL_Y_ID_COLUMN_NAME) = slyid
                    dr(DATASHEET_AGE_MIN_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(r.AgeMin)
                    dr(DATASHEET_AGE_MAX_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(r.AgeMax)
                    dr(DATASHEET_AGE_CLASS_COLUMN_NAME) = DBNull.Value
                    dr(DATASHEET_AMOUNT_COLUMN_NAME) = r.Amount

                    table.Rows.Add(dr)

                End If

            Next

        End If

        Me.m_SummaryStratumStateResults.Clear()
        Me.m_SummaryStratumStateResultsZeroValues.Clear()

    End Sub

    ''' <summary>
    ''' Processes Summary Stratum Transition results
    ''' </summary>
    ''' <param name="table"></param>
    ''' <remarks></remarks>
    Private Sub ProcessSummaryStratumTransitionResults(ByVal timestep As Integer, ByVal table As DataTable)

        If (Me.m_CreateSummaryTransitionOutput) Then

            If (Me.IsSummaryTransitionTimestep(timestep)) Then

                For Each r As OutputStratumTransition In Me.m_SummaryStratumTransitionResults

                    Dim AmountToReport As Double

                    If (Me.m_SummaryTransitionOutputAsIntervalMean) Then

                        If timestep = (Me.m_TimestepZero + 1) Then
                            Exit Sub
                        End If

                        Dim divisor As Double = Me.m_SummaryTransitionOutputTimesteps

                        If (r.Timestep = Me.MaximumTimestep) Then

                            If ((r.Timestep Mod Me.m_SummaryTransitionOutputTimesteps) <> 0) Then
                                divisor = ((r.Timestep - Me.m_TimestepZero) Mod Me.m_SummaryTransitionOutputTimesteps)
                            End If

                        End If

                        AmountToReport = r.Amount / divisor

                    Else
                        AmountToReport = r.Amount
                    End If

                    Dim dr As DataRow = table.NewRow

                    dr(DATASHEET_ITERATION_COLUMN_NAME) = r.Iteration
                    dr(DATASHEET_TIMESTEP_COLUMN_NAME) = r.Timestep
                    dr(DATASHEET_STRATUM_ID_COLUMN_NAME) = r.StratumId
                    dr(DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(r.SecondaryStratumId)
                    dr(DATASHEET_TERTIARY_STRATUM_ID_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(r.TertiaryStratumId)
                    dr(DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME) = r.TransitionGroupId
                    dr(DATASHEET_AGE_MIN_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(r.AgeMin)
                    dr(DATASHEET_AGE_MAX_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(r.AgeMax)
                    dr(DATASHEET_AGE_CLASS_COLUMN_NAME) = DBNull.Value
                    dr(DATASHEET_AMOUNT_COLUMN_NAME) = AmountToReport

                    table.Rows.Add(dr)

                Next

                Me.m_SummaryStratumTransitionResults.Clear()

            End If

        End If
    End Sub

    ''' <summary>
    ''' Processes Summary Stratum Transition State results
    ''' </summary>
    ''' <param name="table"></param>
    ''' <remarks></remarks>
    Private Sub ProcessSummaryStratumTransitionStateResults(ByVal table As DataTable)

        If (Me.m_CreateSummaryTransitionByStateClassOutput) Then

            For Each r As OutputStratumTransitionState In Me.m_SummaryStratumTransitionStateResults

                Dim dr As DataRow = table.NewRow

                dr(DATASHEET_ITERATION_COLUMN_NAME) = r.Iteration
                dr(DATASHEET_TIMESTEP_COLUMN_NAME) = r.Timestep
                dr(DATASHEET_STRATUM_ID_COLUMN_NAME) = r.StratumId
                dr(DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(r.SecondaryStratumId)
                dr(DATASHEET_TERTIARY_STRATUM_ID_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(r.TertiaryStratumId)
                dr(DATASHEET_TRANSITION_TYPE_ID_COLUMN_NAME) = r.TransitionTypeId
                dr(DATASHEET_STATECLASS_ID_COLUMN_NAME) = r.StateClassId
                dr(DATASHEET_END_STATECLASS_ID_COLUMN_NAME) = r.EndStateClassId
                dr(DATASHEET_AMOUNT_COLUMN_NAME) = r.Amount

                table.Rows.Add(dr)

            Next

        End If

        Me.m_SummaryStratumTransitionStateResults.Clear()

    End Sub

    ''' <summary>
    ''' Processes Summary State Attribute results
    ''' </summary>
    ''' <param name="table"></param>
    ''' <remarks></remarks>
    Private Sub ProcessSummaryStateAttributeResults(ByVal table As DataTable)

        If (Me.m_CreateSummaryStateAttributeOutput) Then

            For Each r As OutputStateAttribute In Me.m_SummaryStateAttributeResults

                Dim dr As DataRow = table.NewRow

                dr(DATASHEET_ITERATION_COLUMN_NAME) = r.Iteration
                dr(DATASHEET_TIMESTEP_COLUMN_NAME) = r.Timestep
                dr(DATASHEET_STRATUM_ID_COLUMN_NAME) = r.StratumId
                dr(DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(r.SecondaryStratumId)
                dr(DATASHEET_TERTIARY_STRATUM_ID_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(r.TertiaryStratumId)
                dr(DATASHEET_STATE_ATTRIBUTE_TYPE_ID_COLUMN_NAME) = r.StateAttributeTypeId
                dr(DATASHEET_AGE_MIN_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(r.AgeMin)
                dr(DATASHEET_AGE_MAX_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(r.AgeMax)
                dr(DATASHEET_AGE_CLASS_COLUMN_NAME) = DBNull.Value
                dr(DATASHEET_AMOUNT_COLUMN_NAME) = r.Amount

                table.Rows.Add(dr)

            Next

        End If

        Me.m_SummaryStateAttributeResults.Clear()

    End Sub

    ''' <summary>
    ''' Processes Summary Transition Attribute results
    ''' </summary>
    ''' <param name="table"></param>
    ''' <remarks></remarks>
    Private Sub ProcessSummaryTransitionAttributeResults(ByVal table As DataTable)

        If (Me.m_CreateSummaryTransitionAttributeOutput) Then

            For Each r As OutputTransitionAttribute In Me.m_SummaryTransitionAttributeResults

                Dim dr As DataRow = table.NewRow

                dr(DATASHEET_ITERATION_COLUMN_NAME) = r.Iteration
                dr(DATASHEET_TIMESTEP_COLUMN_NAME) = r.Timestep
                dr(DATASHEET_STRATUM_ID_COLUMN_NAME) = r.StratumId
                dr(DATASHEET_SECONDARY_STRATUM_ID_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(r.SecondaryStratumId)
                dr(DATASHEET_TERTIARY_STRATUM_ID_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(r.TertiaryStratumId)
                dr(DATASHEET_TRANSITION_ATTRIBUTE_TYPE_ID_COLUMN_NAME) = r.TransitionAttributeTypeId
                dr(DATASHEET_AGE_MIN_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(r.AgeMin)
                dr(DATASHEET_AGE_MAX_COLUMN_NAME) = DataTableUtilities.GetNullableDatabaseValue(r.AgeMax)
                dr(DATASHEET_AGE_CLASS_COLUMN_NAME) = DBNull.Value
                dr(DATASHEET_AMOUNT_COLUMN_NAME) = r.Amount

                table.Rows.Add(dr)

            Next

        End If

        Me.m_SummaryTransitionAttributeResults.Clear()

    End Sub

    ''' <summary>
    ''' Creates a dictionary of all secondary stratum ids in the current state class summary output
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateSecondaryStratumDictionary() As Dictionary(Of Integer, Boolean)

        Dim d As New Dictionary(Of Integer, Boolean)

        For Each r As OutputStratumState In Me.m_SummaryStratumStateResults

            Dim k As Integer = GetNullableKey(r.SecondaryStratumId)

            If (Not d.ContainsKey(k)) Then
                d.Add(k, True)
            End If

        Next

        Return d

    End Function

    ''' <summary>
    ''' Creates a dictionary of all tertiary stratum ids in the current state class summary output
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateTertiaryStratumDictionary() As Dictionary(Of Integer, Boolean)

        Dim d As New Dictionary(Of Integer, Boolean)

        For Each r As OutputStratumState In Me.m_SummaryStratumStateResults

            Dim k As Integer = GetNullableKey(r.TertiaryStratumId)

            If (Not d.ContainsKey(k)) Then
                d.Add(k, True)
            End If

        Next

        Return d

    End Function

    ''' <summary>
    ''' Process the Raster State Class output. Create a raster file as a snapshot of the current Cell state class values.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ProcessRasterStateClassOutput(ByVal iteration As Integer, ByVal timestep As Integer)

        If (Not Me.IsSpatial) Then
            Debug.Assert(Not Me.IsSpatial)
            Exit Sub
        End If

        If Me.IsRasterStateClassTimestep(timestep) Then

            Dim rastOutput As New StochasticTimeRaster
            ' Fetch the raster metadata from the InpRasters object
            Me.m_InputRasters.GetMetadata(rastOutput)
            rastOutput.InitIntCells()

            ' Fetch the raster data from the Cells collection
            For Each c As Cell In Me.Cells
                rastOutput.IntCells(c.CellId) = c.StateClassId
            Next

            ' We need to remap the State Class values back to the original Raster values ( PK - > ID)
            Dim dsRemap As DataSheet = Me.Project.GetDataSheet(DATASHEET_STATECLASS_NAME)
            'DEVNOTE: Tom - for now use default NoDataValue for remap. Ideally, we would bring the source files NoDataValue thru.
            rastOutput.IntCells = RasterCells.RemapRasterCells(rastOutput.IntCells, dsRemap, DATASHEET_MAPID_COLUMN_NAME, False, StochasticTimeRaster.DefaultNoDataValue)
            RasterFiles.SaveOutputRaster(rastOutput,Me.ResultScenario.GetDataSheet(DATASHEET_OUTPUT_SPATIAL_STATE_CLASS),RasterDataType.DTInteger,iteration,timestep,SPATIAL_MAP_STATE_CLASS_VARIABLE_NAME,Nothing,DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN)

        End If

    End Sub

    ''' <summary>
    ''' Process the Raster Age output. Create a raster file as a snapshot of the current Cell Age values.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ProcessRasterAgeOutput(ByVal iteration As Integer, ByVal timestep As Integer)

        If (Not Me.IsSpatial) Then
            Debug.Assert(Not Me.IsSpatial)
            Exit Sub
        End If

        If Me.IsRasterAgeTimestep(timestep) Then

            Dim rastOutput As New StochasticTimeRaster
            ' Fetch the raster metadata from the InpRasters object
            Me.m_InputRasters.GetMetadata(rastOutput)
            rastOutput.InitIntCells()

            ' Fetch the raster data from the Cells collection
            For Each c As Cell In Me.Cells
                rastOutput.IntCells(c.CellId) = c.Age
            Next

            RasterFiles.SaveOutputRaster(
                rastOutput,
                Me.ResultScenario.GetDataSheet(DATASHEET_OUTPUT_SPATIAL_AGE),
                RasterDataType.DTInteger,
                iteration,
                timestep,
                SPATIAL_MAP_AGE_VARIABLE_NAME,
                Nothing,
                DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN)

        End If

    End Sub

    ''' <summary>
    ''' Process the Raster TST output. Create a raster file as a snapshot of the current Cell Age values.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ProcessRasterTSTOutput(ByVal iteration As Integer, ByVal timestep As Integer)

        If (Not Me.IsSpatial) Then
            Debug.Assert(Not Me.IsSpatial)
            Exit Sub
        End If

        If Me.IsRasterTstTimestep(timestep) Then

            ' Loop thru Transition Groups       
            For Each tg As TransitionGroup In Me.m_TransitionGroups

                Dim rastOutput As New StochasticTimeRaster
                ' Fetch the raster metadata from the InpRasters object
                Me.m_InputRasters.GetMetadata(rastOutput)
                rastOutput.InitIntCells()

                ' Fetch the raster data from the Cells collection
                For Each cell In Me.Cells

                    If cell.TstValues.Count() <> 0 Then

                        ' Make sure the TstValues contains our TransitionGroupId
                        If cell.TstValues.Contains(tg.TransitionGroupId) Then
                            rastOutput.IntCells(cell.CellId) = cell.TstValues(tg.TransitionGroupId).TstValue
                        End If

                    End If

                Next

                ' If no values other than NODATAValue in rastOutput, then supress output for this timestep
                Dim distinctVals = rastOutput.IntCells().Distinct

                If (distinctVals.Count() > 1 Or (distinctVals.Count() = 1 And distinctVals(0) <> StochasticTimeRaster.DefaultNoDataValue)) Then
                    RasterFiles.SaveOutputRaster(rastOutput,Me.ResultScenario.GetDataSheet(DATASHEET_OUTPUT_SPATIAL_TST),RasterDataType.DTInteger,iteration,timestep,SPATIAL_MAP_TST_VARIABLE_NAME, tg.TransitionGroupId,DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN)
                End If

            Next

        End If

    End Sub

    ''' <summary>
    ''' Process the Raster Stratum output. Create a raster file as a snapshot of the current Cell stratum values.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ProcessRasterStratumOutput(ByVal iteration As Integer, ByVal timestep As Integer)

        If (Not Me.IsSpatial) Then
            Debug.Assert(Not Me.IsSpatial)
            Exit Sub
        End If

        If Me.IsRasterStratumTimestep(timestep) Then

            Dim rastOutput As New StochasticTimeRaster
            ' Fetch the raster metadata from the InpRasters object
            Me.m_InputRasters.GetMetadata(rastOutput)
            rastOutput.InitIntCells()

            For Each c As Cell In Me.Cells
                ' Fetch the raster data from the Cells collection
                rastOutput.IntCells(c.CellId) = c.StratumId
            Next

            ' We need to remap the Stratum values back to the original Raster values ( PK - > ID)
            Dim dsRemap As DataSheet = Me.Project.GetDataSheet(DATASHEET_STRATA_NAME)

            'DEVNOTE: Tom - for now use default NoDataValue during remap. Ideally, we would bring the source files NoDataValue thru.
            rastOutput.IntCells = RasterCells.RemapRasterCells(rastOutput.IntCells, dsRemap, DATASHEET_MAPID_COLUMN_NAME, False, StochasticTimeRaster.DefaultNoDataValue)
            RasterFiles.SaveOutputRaster(rastOutput,Me.ResultScenario.GetDataSheet(DATASHEET_OUTPUT_SPATIAL_STRATUM),RasterDataType.DTInteger, iteration,timestep,SPATIAL_MAP_STRATUM_VARIABLE_NAME,Nothing,DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN)

        End If

    End Sub

    ''' <summary>
    ''' Process Raster State Attribute Output
    ''' </summary>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <remarks></remarks>
    Private Sub ProcessRasterStateAttributeOutput(ByVal iteration As Integer, ByVal timestep As Integer)

        If (Not Me.IsSpatial) Then
            Debug.Assert(Not Me.IsSpatial)
            Exit Sub
        End If

        If (Me.IsRasterStateAttributeTimestep(timestep)) Then

            Dim rastOutput As New StochasticTimeRaster
            ' Fetch the raster metadata from the InpRasters object
            Me.m_InputRasters.GetMetadata(rastOutput)

            For Each AttributeTypeId As Integer In Me.m_StateAttributeTypeIdsNoAges.Keys()

                rastOutput.InitDblCells()

                For Each c As Cell In Me.Cells

                    Dim AttrValue As Nullable(Of Double) =
                        Me.m_StateAttributeValueMapNoAges.GetAttributeValueNoAge(
                            AttributeTypeId,
                            c.StratumId,
                            c.SecondaryStratumId,
                            c.TertiaryStratumId,
                            c.StateClassId,
                            iteration,
                            timestep)

                    'If no value, then use NO_DATA (initialized above), otherwise AttrValue

                    If AttrValue IsNot Nothing Then
                        rastOutput.DblCells(c.CellId) = CDbl(AttrValue)
                    End If

                Next

                RasterFiles.SaveOutputRaster(rastOutput,Me.ResultScenario.GetDataSheet(DATASHEET_OUTPUT_SPATIAL_STATE_ATTRIBUTE),RasterDataType.DTDouble, iteration,timestep,SPATIAL_MAP_STATE_ATTRIBUTE_VARIABLE_PREFIX,AttributeTypeId,DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN)

            Next

            For Each AttributeTypeId As Integer In Me.m_StateAttributeTypeIdsAges.Keys()

                rastOutput.InitDblCells()

                For Each c As Cell In Me.Cells

                    Dim AttrValue As Nullable(Of Double) =
                        Me.m_StateAttributeValueMapAges.GetAttributeValueByAge(
                            AttributeTypeId,
                            c.StratumId,
                            c.SecondaryStratumId,
                            c.TertiaryStratumId,
                            c.StateClassId,
                            iteration,
                            timestep,
                            c.Age)

                    'If no value, then use NO_DATA, otherwise AttrValue

                    If AttrValue IsNot Nothing Then
                        rastOutput.DblCells(c.CellId) = CDbl(AttrValue)
                    End If

                Next

                RasterFiles.SaveOutputRaster(
                    rastOutput,
                    Me.ResultScenario.GetDataSheet(DATASHEET_OUTPUT_SPATIAL_STATE_ATTRIBUTE),
                    RasterDataType.DTDouble,
                    iteration,
                    timestep,
                    SPATIAL_MAP_STATE_ATTRIBUTE_VARIABLE_PREFIX,
                    AttributeTypeId,
                    DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN)

            Next

        End If

    End Sub

    ''' <summary>
    ''' Process Transition Adjacency State Attribute Output
    ''' </summary>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <remarks>
    ''' At the Landscape Update Frequency specified above generate a raster of the state attribute in question 
    ''' and then do a moving window analysis of the raster such that for each cell the average value of the state 
    ''' attribute within it’s neighborhood radius is calculated. Create an in memory raster array of the moving 
    ''' window analysis results. Hold on to this raster in memory (as a single dimensional arrary) which can be accessed when needed.
    ''' </remarks>
    Private Sub ProcessTransitionAdjacencyStateAttribute(ByVal iteration As Integer, ByVal timestep As Integer)

        If (Not Me.IsSpatial) Then
            Debug.Assert(Not Me.IsSpatial)
            Exit Sub
        End If

        ' Loop thru all the Transition Adjacency Settings records. Each can have update frequencies.
        For Each setting As TransitionAdjacencySetting In Me.m_TransitionAdjacencySettings

            If (Me.IsTransitionAdjacencyStateAttributeTimestep(timestep, setting.TransitionGroupId)) Then

                ' Determine the relative neighbors of interest for the specified radius
                Dim listNeighbors As IEnumerable(Of CellOffset) = InputRasters.GetCellNeighborOffsetsForRadius(setting.NeighborhoodRadius)

                ' Create one cell array per Transition Adjacency Settings record, indexes by TGId
                Dim stateAttrVals() As Double
                Dim stateAttrAvgs() As Double

                Dim stateAttributeTypeId As Integer = setting.StateAttributeTypeId
                Dim stateAttributeValueMap As StateAttributeValueMap = Nothing
                Dim IsNoAges = False

                ' Extract State Attribute values from StateAttributeValueMaps ( just do it once, to enhance performance)
                ' check whether StateAttributeTypeId is in m_StateAttributeTypeIdsNoAges or m_StateAttributeTypeIdsAges. 

                If Me.m_StateAttributeTypeIdsNoAges.Keys.Contains(stateAttributeTypeId) Then
                    stateAttributeValueMap = Me.m_StateAttributeValueMapNoAges
                    IsNoAges = True
                End If

                If Me.m_StateAttributeTypeIdsAges.Keys.Contains(stateAttributeTypeId) Then
                    stateAttributeValueMap = Me.m_StateAttributeValueMapAges
                End If

                If Not stateAttributeValueMap Is Nothing Then

                    ReDim stateAttrVals(Me.m_InputRasters.NumberCells - 1)

                    For i = 0 To Me.m_InputRasters.NumberCells - 1
                        stateAttrVals(i) = StochasticTimeRaster.DefaultNoDataValue
                    Next

                    ' Loop thru raster and pull out the State Attribute Value for each cell
                    For Each cell In Me.Cells

                        ' Pull out the values 1st, before doing the neighbor averaging, to get our repeated cost if GetValue.
                        Dim attrValue As Nullable(Of Double) = Nothing

                        If (IsNoAges) Then

                            attrValue = stateAttributeValueMap.GetAttributeValueNoAge(
                                stateAttributeTypeId,
                                cell.StratumId,
                                cell.SecondaryStratumId,
                                cell.TertiaryStratumId,
                                cell.StateClassId,
                                iteration,
                                timestep)

                        Else

                            attrValue = stateAttributeValueMap.GetAttributeValueByAge(
                                stateAttributeTypeId,
                                cell.StratumId,
                                cell.SecondaryStratumId,
                                cell.TertiaryStratumId,
                                cell.StateClassId,
                                iteration,
                                timestep,
                                cell.Age)

                        End If

                        If attrValue IsNot Nothing Then
                            stateAttrVals(cell.CellId) = CDbl(attrValue)
                        End If

                    Next

                    ' Now lets loop thru State Attribute array and generate the neighbor average for each cell
                    Dim attrValueTotal As Double
                    Dim attrValueCnt As Integer

                    ReDim stateAttrAvgs(Me.m_InputRasters.NumberCells - 1)

                    For i As Integer = 0 To Me.m_InputRasters.NumberCells - 1

                        attrValueTotal = 0
                        attrValueCnt = 0

                        ' Calculate row/column once, to eek performance ( small improvement) 
                        Dim cellRow As Integer
                        Dim cellColumn As Integer
                        Me.m_InputRasters.GetRowColForId(i, cellRow, cellColumn)


                        For Each offset As CellOffset In listNeighbors
                            ' Convert the relative neighbor into absolute neighbor.
                            Dim neighborCellId As Integer = Me.m_InputRasters.GetCellIdByOffset(cellRow, cellColumn, offset.Row, offset.Column)
                            If neighborCellId <> -1 Then

                                Dim attrValue As Double = stateAttrVals(neighborCellId)

                                ' If NO_DATA, don't include in the averaging
                                If Not attrValue.Equals(StochasticTimeRaster.DefaultNoDataValue) Then
                                    attrValueTotal += CDbl(attrValue)
                                    attrValueCnt += 1
                                End If
                            End If
                        Next

                        If attrValueCnt > 0 Then
                            'Use count of possible neighbors, not actual neghbors (attrValueCnt).
                            stateAttrAvgs(i) = attrValueTotal / listNeighbors.Count

                        Else
                            stateAttrAvgs(i) = StochasticTimeRaster.DefaultNoDataValue
                        End If

                    Next

                    ' Remove the old value array from the map, to be replaced with new array
                    Me.m_TransitionAdjacencyStateAttributeValueMap.Remove(setting.TransitionGroupId)

                    Me.m_TransitionAdjacencyStateAttributeValueMap.Add(setting.TransitionGroupId, stateAttrAvgs)

                End If

            End If

        Next

    End Sub

    ''' <summary>
    ''' Process the Annual Average Transition Probabilities to raster file output.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ProcessRasterAATPOutput()

        If (Not Me.IsSpatial) Then
            Debug.Assert(Not Me.IsSpatial)
            Exit Sub
        End If

        If Not Me.m_CreateRasterAATPOutput Then
            Exit Sub
        End If

        For Each tgId As Integer In Me.m_AnnualAvgTransitionProbMap.Keys

            Dim dictAatp As Dictionary(Of Integer, Double()) = Me.m_AnnualAvgTransitionProbMap(tgId)

            ' Now lets loop thru the timestep arrays in the dictAatp
            For Each timestep As Integer In dictAatp.Keys

                Dim aatp As Double() = dictAatp(timestep)

                Dim rastAatp As New StochasticTimeRaster
                ' Fetch the raster metadata from the InpRasters object
                Me.m_InputRasters.GetMetadata(rastAatp)

                'Dont bother writing out any array thats all DEFAULT_NO_DATA_VALUEs or 0's
                Dim aatpDistinct = aatp.Distinct()

                If (aatpDistinct.Count = 1) Then
                    Debug.Print("Skipping Annual Average Transition Probabilities output for TG {0} / Timestep {1} as no non-DEFAULT_NO_DATA_VALUE values found.", tgId, timestep)
                    Continue For
                ElseIf (aatpDistinct.Count = 2) Then

                    If (aatpDistinct.ElementAt(0) <= 0 And aatpDistinct.ElementAt(1) <= 0) Then
                        Debug.Print("Skipping Annual Average Transition Probabilities output for TG {0} / Timestep {1} as no non-DEFAULT_NO_DATA_VALUE values found.", tgId, timestep)
                        Continue For
                    End If

                End If

                rastAatp.DblCells = aatp

                RasterFiles.SaveOutputRaster(
                    rastAatp,
                    Me.ResultScenario.GetDataSheet(DATASHEET_OUTPUT_SPATIAL_AVERAGE_TRANSITION_PROBABILITY),
                    RasterDataType.DTDouble,
                    0,
                    timestep,
                    SPATIAL_MAP_AVG_ANNUAL_TRANSITION_PROBABILITY_VARIABLE_PREFIX,
                    tgId,
                    DATASHEET_OUTPUT_SPATIAL_FILENAME_COLUMN)

            Next

        Next

    End Sub

    ''' <summary>
    ''' Record the Annual Average Transition Probability output.
    ''' </summary>
    ''' <param name="numIterations">The number of interactions the model will run</param>
    ''' <param name="timestep">The current timestep</param>
    ''' <param name="transitionGroupId">The Transition Group Id</param>
    ''' <param name="cellArray">A cell array containing transition pixels for the specified Transition Group</param>
    ''' <remarks></remarks>
    Private Sub RecordAnnualAvgTransitionProbabilityOutput(
        ByVal numIterations As Integer,
        ByVal timestep As Integer,
        ByVal transitionGroupId As Integer,
        ByVal cellArray() As Integer)

        If (Not Me.IsSpatial) Then
            Debug.Assert(Not Me.IsSpatial)
            Exit Sub
        End If

        If Not Me.m_CreateRasterAATPOutput Then
            Exit Sub
        End If

        'Dont bother if there haven't been any transitions this timestep
        Dim distArray = cellArray.Distinct()
        If distArray.Count() = 1 Then

            Dim el0 = distArray.ElementAt(0)
            If el0.Equals(0.0) Or el0.Equals(StochasticTimeRaster.DefaultNoDataValue) Then
                '  Debug.Print("Found all 0's or NO_DATA_VALUES")
                Exit Sub
            End If

        End If

        ' See if the Dictionary for this Transition Group exists. If not, init routine screwed up.
        If Not Me.m_AnnualAvgTransitionProbMap.ContainsKey(transitionGroupId) Then
            Debug.Assert(False, "Where the heck is the Transition Group in the m_AnnualAvgTransitionProbMap member ?")
        End If

        Dim dictTgAATP As Dictionary(Of Integer, Double()) = Me.m_AnnualAvgTransitionProbMap(transitionGroupId)

        ' We should now have a Dictionary of timestep-keyed Double arrays
        ' See if the specified timestep is a multiple of user timestep specified, or last timestep
        Dim timestepKey As Integer

        If timestep = Me.MaximumTimestep Then
            timestepKey = Me.MaximumTimestep
        Else

            'We're looking for the the raster whose timestep key is the first one that is >= to the current timestep
            timestepKey = CInt(Math.Ceiling(CDbl(timestep - Me.TimestepZero) / Me.m_RasterAATPTimesteps) * Me.m_RasterAATPTimesteps) + Me.TimestepZero

            If timestepKey > Me.MaximumTimestep Then
                timestepKey = Me.MaximumTimestep
            End If

        End If

        ' We should be able to find a dictionary
        Dim aatp As Double() = Nothing

        If dictTgAATP.ContainsKey(timestepKey) Then
            aatp = dictTgAATP(timestepKey)
        Else
            Debug.Assert(False, "Where the heck is the Timestep keyed array in the m_AnnualAvgTransitionProbMap member.")
        End If

        For Each cell In Me.Cells

            Dim i As Integer = cell.CellId

            'Test for > 0 ( and not equal to DEFAULT_NO_DATA_VALUE either )
            If cellArray(i) > 0 Then

                Debug.Assert(aatp(i) >= 0.0, "We shouldn't get a DEFAULT_NO_DATA value here. Init routine Bad!")

                ' Now lets do the probability calculation
                'The value to increment by is 1/(tsf*N) 
                'where tsf is the timestep frequency 
                'N is the number of iterations.
                ' Accomodate last bin, where not multiple of frequency. For instance MaxTS of 8, and freq of 5, would give bins 1-5, and 6-8.

                If ((timestepKey = Me.MaximumTimestep) And (((timestepKey - Me.TimestepZero) Mod Me.m_RasterAATPTimesteps) <> 0)) Then
                    aatp(i) += 1 / ((timestepKey - Me.TimestepZero) Mod Me.m_RasterAATPTimesteps * numIterations)
                Else
                    aatp(i) += 1 / (Me.m_RasterAATPTimesteps * numIterations)
                End If

            End If

        Next

    End Sub

    Private Function IsTransitionAttributeTargetExceded(
        ByVal simulationCell As Cell,
        ByVal tr As Transition,
        ByVal iteration As Integer,
        ByVal timestep As Integer) As Boolean

        If (Not Me.m_TransitionAttributeValueMap.HasItems()) Then
            Return False
        End If

        Dim tt As TransitionType = Me.m_TransitionTypes(tr.TransitionTypeId)

        For Each AttributeTypeId As Integer In Me.m_TransitionAttributeTypeIds.Keys

            For Each tg As TransitionGroup In tt.TransitionGroups

                Dim AttrValue As Nullable(Of Double) =
                    Me.m_TransitionAttributeValueMap.GetAttributeValue(
                        AttributeTypeId,
                        tg.TransitionGroupId,
                        simulationCell.StratumId,
                        simulationCell.SecondaryStratumId,
                        simulationCell.TertiaryStratumId,
                        simulationCell.StateClassId,
                        iteration,
                        timestep,
                        simulationCell.Age)

                If (AttrValue.HasValue) Then

                    Dim Target As TransitionAttributeTarget = Me.m_TransitionAttributeTargetMap.GetAttributeTarget(
                        AttributeTypeId, simulationCell.StratumId, simulationCell.SecondaryStratumId, simulationCell.TertiaryStratumId, iteration, timestep)

                    If (Target IsNot Nothing) Then

                        If (Target.TargetRemaining <= 0.0) Then
                            Return True
                        End If

                    End If

                End If

            Next

        Next

        Return False

    End Function

End Class
