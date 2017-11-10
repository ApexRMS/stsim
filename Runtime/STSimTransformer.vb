'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core
Imports SyncroSim.Common
Imports SyncroSim.StochasticTime
Imports System.Globalization

Public NotInheritable Class STSimTransformer
    Inherits StochasticTimeTransformer

    Private m_TotalAmount As Double
    Private m_CalcNumCellsFromDist As Boolean
    Private m_RandomGenerator As New RandomGenerator()
    Private m_DistributionProvider As STSimDistributionProvider
    Private m_TimestepUnitsLower As String = "timestep"
    Private m_AgeReportingHelper As AgeHelper
    Private m_AmountPerCell As Double
    Private m_TimestepZero As Integer
    Private m_IsSpatial As Boolean

    Public Event CellInitialized As EventHandler(Of CellEventArgs)
    Public Event CellsInitialized As EventHandler(Of CellEventArgs)
    Public Event ChangingCellProbabilistic As EventHandler(Of CellChangeEventArgs)
    Public Event ChangingCellDeterministic As EventHandler(Of CellChangeEventArgs)
    Public Event CellBeforeTransitions As EventHandler(Of CellChangeEventArgs)
    Public Event ApplyingProbabilisticTransitionsRaster As EventHandler(Of ApplyProbabilisticTransitionsRasterEventArgs)
    Public Event ExternalMultipliersRequested As EventHandler(Of ExternalMultipliersEventArgs)

    ''' <summary>
    ''' Gets whether this should be a spatial run
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsSpatial As Boolean
        Get
            Return Me.m_IsSpatial
        End Get
    End Property

    ''' <summary>
    ''' Gets the value for Timestep Zero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TimestepZero As Integer
        Get
            Return Me.m_TimestepZero
        End Get
    End Property

    ''' <summary>
    ''' Gets the amount per cell
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property AmountPerCell As Double
        Get
            Return Me.m_AmountPerCell
        End Get
    End Property

    ''' <summary>
    ''' Collection of simulation cells
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property Cells As CellCollection
        Get
            Return Me.m_Cells
        End Get
    End Property

    ''' <summary>
    ''' Collection of Transition Groups
    ''' </summary>
    Public ReadOnly Property TransitionGroups As TransitionGroupCollection
        Get
            Return Me.m_TransitionGroups
        End Get
    End Property

    ''' <summary>
    ''' Collection of Transition Types
    ''' </summary>
    Public ReadOnly Property TransitionTypes As TransitionTypeCollection
        Get
            Return Me.m_TransitionTypes
        End Get
    End Property

    ''' <summary>
    ''' Gets the input rasters array
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property InputRasters() As InputRasters
        Get
            Return Me.m_InputRasters
        End Get
    End Property

    ''' <summary>
    ''' Gets the distribution provider
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DistributionProvider As STSimDistributionProvider
        Get
            Return Me.m_DistributionProvider
        End Get
    End Property

    Public Function GetAttributeValueNoAge(
        ByVal stateAttributeTypeId As Integer,
        ByVal stratumId As Integer,
        ByVal secondaryStratumId As Nullable(Of Integer),
        ByVal tertiaryStratumId As Nullable(Of Integer),
        ByVal stateClassId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer) As Nullable(Of Double)

        Return Me.m_StateAttributeValueMapNoAges.GetAttributeValueNoAge(
            stateAttributeTypeId,
            stratumId,
            secondaryStratumId,
            tertiaryStratumId,
            stateClassId,
            iteration,
            timestep)

    End Function

    Public Function GetAttributeValueByAge(
        ByVal stateAttributeTypeId As Integer,
        ByVal stratumId As Integer,
        ByVal secondaryStratumId As Nullable(Of Integer),
        ByVal tertiaryStratumId As Nullable(Of Integer),
        ByVal stateClassId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal age As Integer) As Nullable(Of Double)

        Return Me.m_StateAttributeValueMapAges.GetAttributeValueByAge(
            stateAttributeTypeId,
            stratumId,
            secondaryStratumId,
            tertiaryStratumId,
            stateClassId,
            iteration,
            timestep,
            age)

    End Function

    ''' <summary>
    ''' Determines if the specified attribute type is an age attribute type
    ''' </summary>
    ''' <param name="stateAttributeTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsAgeAttributeType(ByVal stateAttributeTypeId As Integer) As Boolean
        Return Me.m_StateAttributeTypeIdsAges.ContainsKey(stateAttributeTypeId)
    End Function

    ''' <summary>
    ''' Determines if the specified attribute type is a no-age attribute type
    ''' </summary>
    ''' <param name="stateAttributeTypeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsNoAgeAttributeType(ByVal stateAttributeTypeId As Integer) As Boolean
        Return Me.m_StateAttributeTypeIdsNoAges.ContainsKey(stateAttributeTypeId)
    End Function

    ''' <summary>
    ''' Overrides Configure
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub Configure()
        Me.InternalConfigure()
    End Sub

    ''' <summary>
    ''' Overrides Initialize
    ''' </summary>
    Public Overrides Sub Initialize()
        Me.InternalInitialize()
    End Sub

    ''' <summary>
    ''' Overrides Transform
    ''' </summary>
    ''' <remarks></remarks>
    Public Overrides Sub Transform()
        Me.InternalTransform()
    End Sub

    ''' <summary>
    ''' Overrides OnBeforeIteration
    ''' </summary>
    ''' <param name="iteration"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub OnBeforeIteration(iteration As Integer)

        MyBase.OnBeforeIteration(iteration)
        Me.InternalOnBeforeIteration(iteration)

    End Sub

    ''' <summary>
    ''' Overrides OnIteration
    ''' </summary>
    ''' <param name="iteration"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub OnIteration(iteration As Integer)
        Me.InternalOnIteration(iteration)
    End Sub

    ''' <summary>
    ''' Overrides OnBeforeTimestep
    ''' </summary>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub OnBeforeTimestep(iteration As Integer, timestep As Integer)

        MyBase.OnBeforeTimestep(iteration, timestep)
        Me.InternalOnBeforeTimestep(iteration, timestep)

    End Sub

    ''' <summary>
    ''' Overrides OnTimestep
    ''' </summary>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub OnTimestep(iteration As Integer, timestep As Integer)
        Me.InternalOnTimestep(iteration, timestep)
    End Sub

    ''' <summary>
    ''' Called when external data has been appended to the specified data sheet
    ''' </summary>
    ''' <param name="dataSheet"></param>
    ''' <param name="previousData"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub OnExternalDataReady(ByVal dataSheet As DataSheet, ByVal previousData As DataTable)
        Me.STSimExternalDataReady(dataSheet, previousData)
    End Sub

    Private Sub InternalConfigure()

        MyBase.Configure()

        Me.ConfigureTimestepUnits()
        Me.NormalizeRunControl()
        Me.NormalizeOutputOptions()

        ' We need to normalize the Initial Conditions here, so that we can run in Multiprocessor mode 
        ' with the same Input config & raster files

        Me.ConfigureIsSpatialRunFlag()
        Me.NormalizeMapIds()
        Me.NormalizeInitialConditions()

        If (Me.IsSpatial) Then
            Me.NormalizeColorData()
        End If

    End Sub

    Private Sub InternalInitialize()

        Me.SetStatusMessage("Initializing")

        Me.ConfigureTimestepUnits()
        Me.ConfigureIsSpatialRunFlag()
        Me.ConfigureTimestepsAndIterations()

        If (Me.IsSpatial) Then

            Me.FillInitialConditionsSpatialCollectionAndMap()
            Me.InitializeRasterData(Me.MinimumIteration)

        Else
            Me.FillInitialConditionsDistributionCollectionAndMap()
        End If

        Me.InitializeOutputOptions()
        Me.InitializeDistributionProvider()
        Me.InitializeAgeReportingHelper()
        Me.InitializeModelCollections()
        Me.NormalizeForUserDistributions()
        Me.InitializeDistributionValues()
        Me.InitializeOutputDataTables()
        Me.InitializeModel()

    End Sub

    Private Sub InternalTransform()

        MyBase.RunStochasticLoop()

        'We process AATP output after the rest of the model has completed because
        'these calculations must be done across the entire data set.

        Me.ProcessRasterAATPOutput()

    End Sub

    Private Sub InternalOnBeforeIteration(iteration As Integer)

        Me.ResampleExternalVariableValues(iteration, Me.MinimumTimestep, DistributionFrequency.Iteration)
        Me.ResampleDistributionValues(iteration, Me.MinimumTimestep, DistributionFrequency.Iteration)
        Me.ResampleTransitionTargetValues(iteration, Me.MinimumTimestep, DistributionFrequency.Iteration)
        Me.ResampleTransitionAttributeTargetValues(iteration, Me.MinimumTimestep, DistributionFrequency.Iteration)
        Me.ResampleTransitionMultiplierValues(iteration, Me.MinimumTimestep, DistributionFrequency.Iteration)
        Me.ResampleTransitionDirectionMultiplierValues(iteration, Me.MinimumTimestep, DistributionFrequency.Iteration)
        Me.ResampleTransitionSlopeMultiplierValues(iteration, Me.MinimumTimestep, DistributionFrequency.Iteration)
        Me.ResampleTransitionAdjacencyMultiplierValues(iteration, Me.MinimumTimestep, DistributionFrequency.Iteration)

    End Sub

    Private Sub InternalOnBeforeTimestep(iteration As Integer, timestep As Integer)

        Me.ResampleExternalVariableValues(iteration, timestep, DistributionFrequency.Timestep)
        Me.ResampleDistributionValues(iteration, timestep, DistributionFrequency.Timestep)
        Me.ResampleTransitionTargetValues(iteration, timestep, DistributionFrequency.Timestep)
        Me.ResampleTransitionAttributeTargetValues(iteration, timestep, DistributionFrequency.Timestep)
        Me.ResampleTransitionMultiplierValues(iteration, timestep, DistributionFrequency.Timestep)
        Me.ResampleTransitionDirectionMultiplierValues(iteration, timestep, DistributionFrequency.Timestep)
        Me.ResampleTransitionSlopeMultiplierValues(iteration, timestep, DistributionFrequency.Timestep)
        Me.ResampleTransitionAdjacencyMultiplierValues(iteration, timestep, DistributionFrequency.Timestep)

    End Sub

    Private Sub InternalOnIteration(iteration As Integer)

        MyBase.OnIteration(iteration)

        Me.m_ProportionAccumulatorMap = New ProportionAccumulatorMap(Me.m_AmountPerCell)

        If (Me.IsSpatial) Then

            Me.ResetStrataCellCollections()
            Me.InitializeRasterData(iteration)
            Me.InitializeCellsRaster(iteration)
            Me.ResetTransitionSpreadGroupCells()

            'Only Init once, as the maps need to survive entire model run

            If (iteration = Me.MinimumIteration) Then
                Me.InitializeAnnualAvgTransitionProbMaps()
            End If

        Else
            Me.InitializeCellsNonRaster(iteration)
        End If

        Me.ProcessOutputStratumAmounts(iteration, Me.m_TimestepZero)
        Me.ProcessRasterStratumOutput(iteration, Me.m_TimestepZero)
        Me.ProcessRasterStateClassOutput(iteration, Me.m_TimestepZero)
        Me.ProcessRasterAgeOutput(iteration, Me.m_TimestepZero)
        Me.ProcessRasterTSTOutput(iteration, Me.m_TimestepZero)
        Me.ProcessRasterStateAttributeOutput(iteration, Me.m_TimestepZero)
        Me.ProcessTransitionAdjacencyStateAttribute(iteration, Me.m_TimestepZero)

    End Sub

    Private Sub InternalOnTimestep(iteration As Integer, timestep As Integer)

        MyBase.OnTimestep(iteration, timestep)

        Me.Simulate(iteration, timestep)
        Me.GenerateStateClassAttributes()

        Me.ProcessOutputStratumAmounts(iteration, timestep)
        Me.ProcessSummaryStratumStateResults(Me.m_OutputStratumStateTable, iteration, timestep)
        Me.ProcessSummaryStratumTransitionResults(timestep, Me.m_OutputStratumTransitionTable)
        Me.ProcessSummaryStratumTransitionStateResults(Me.m_OutputStratumTransitionStateTable)
        Me.ProcessSummaryStateAttributeResults(Me.m_OutputStateAttributeTable)
        Me.ProcessSummaryTransitionAttributeResults(Me.m_OutputTransitionAttributeTable)
        Me.ProcessRasterStratumOutput(iteration, timestep)
        Me.ProcessRasterStateClassOutput(iteration, timestep)
        Me.ProcessRasterAgeOutput(iteration, timestep)
        Me.ProcessRasterTSTOutput(iteration, timestep)
        Me.ProcessRasterStateAttributeOutput(iteration, timestep)
        Me.ProcessTransitionAdjacencyStateAttribute(iteration, timestep)

        Debug.Assert(Me.m_SummaryTransitionAttributeResults.Count = 0)

    End Sub

    ''' <summary>
    ''' Simulates for the specified iteration and timestep
    ''' </summary>
    Private Sub Simulate(ByVal iteration As Integer, ByVal timestep As Integer)

        Me.ResetTransitionsForCells(iteration, timestep)
        Me.ResetTransitionAttributeRemainingTargetAmounts()
        Me.AssignPatchPrioritizations(iteration, timestep)
        Me.ApplyTransitions(iteration, timestep)

    End Sub

    ''' <summary>
    ''' Applies transitions for all cells
    ''' </summary>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <remarks></remarks>
    Private Sub ApplyTransitions(ByVal iteration As Integer, ByVal timestep As Integer)

        Me.ReorderShufflableTransitionGroups(iteration, timestep)

        For Each simulationCell As Cell In Me.m_Cells

            Dim dt As DeterministicTransition =
                Me.GetDeterministicTransition(simulationCell.StratumId, simulationCell.StateClassId, iteration, timestep)

            If (dt IsNot Nothing) Then

                RaiseEvent CellBeforeTransitions(
                    Me, New CellChangeEventArgs(simulationCell, iteration, timestep, dt, Nothing, Me.m_AmountPerCell))

            End If

        Next

        If (Me.IsSpatial) Then

            Dim RasterTransitionAttrValues As Dictionary(Of Integer, Double()) = CreateRasterTransitionAttributeArrays(timestep)
            Dim dictTransitionedPixels As Dictionary(Of Integer, Integer()) = CreateTransitionGroupTransitionedPixels()

            RaiseEvent ApplyingProbabilisticTransitionsRaster(Me, New ApplyProbabilisticTransitionsRasterEventArgs(iteration, timestep))

            Me.ApplyProbabilisticTransitionsRaster(iteration, timestep, RasterTransitionAttrValues, dictTransitionedPixels)
            Me.ApplyTransitionSpread(iteration, timestep, RasterTransitionAttrValues, dictTransitionedPixels)
            Me.OnRasterTransitionOutput(iteration, timestep, dictTransitionedPixels)

            For Each simulationCell As Cell In Me.m_Cells
                Me.ApplyDeterministicTransitions(simulationCell, iteration, timestep)
            Next

            Me.OnRasterTransitionAttributeOutput(RasterTransitionAttrValues, iteration, timestep)

        Else

            Dim RemainingTransitionGroups As New Dictionary(Of Integer, TransitionGroup)

            For Each tg As TransitionGroup In Me.m_ShufflableTransitionGroups
                RemainingTransitionGroups.Add(tg.TransitionGroupId, tg)
            Next

            For Each TransitionGroup As TransitionGroup In Me.m_ShufflableTransitionGroups

                If (TransitionGroup.PrimaryTransitionTypes.Count = 0) Then
                    Continue For
                End If

                Dim tatMap As New MultiLevelKeyMap1(Of Dictionary(Of Integer, TransitionAttributeTarget))

                Me.ResetTransitionTargetMultipliers(iteration, timestep, TransitionGroup)
                Me.ResetTranstionAttributeTargetMultipliers(iteration, timestep, RemainingTransitionGroups, tatMap, TransitionGroup)

                RemainingTransitionGroups.Remove(TransitionGroup.TransitionGroupId)

                For Each simulationCell As Cell In Me.m_Cells
                    Me.ApplyProbabilisticTransitionsByCell(simulationCell, iteration, timestep, TransitionGroup, Nothing, Nothing)
                Next

            Next

            For Each simulationCell As Cell In Me.m_Cells
                Me.ApplyDeterministicTransitions(simulationCell, iteration, timestep)
            Next

        End If

    End Sub

    ''' <summary>
    ''' Applies deterministic transitions for the specified cell
    ''' </summary>
    ''' <param name="simulationCell">The simulation cell</param>
    ''' <param name="iteration">The current iteration</param>
    ''' <param name="timestep">The current timestep</param>
    ''' <remarks></remarks>
    Private Sub ApplyDeterministicTransitions(ByVal simulationCell As Cell, ByVal iteration As Integer, ByVal timestep As Integer)

        If (simulationCell.StratumId = 0 Or simulationCell.StateClassId = 0) Then
            Return
        End If

        Dim dt As DeterministicTransition =
            Me.GetDeterministicTransition(simulationCell.StratumId, simulationCell.StateClassId, iteration, timestep)

        If (dt IsNot Nothing) Then

            RaiseEvent ChangingCellDeterministic(
                Me, New CellChangeEventArgs(simulationCell, iteration, timestep, dt, Nothing, Me.m_AmountPerCell))

        End If

        Dim IncrementAge As Boolean = True

        If (dt IsNot Nothing) Then

            If (simulationCell.Age >= dt.AgeMaximum) Then

                If Not (dt.StateClassIdSource = dt.StateClassIdDestination) Then
                    Me.ChangeCellForDeterministicTransition(simulationCell, dt, iteration, timestep)
                End If

                IncrementAge = False

            End If

        End If

        If (IncrementAge) Then

            Debug.Assert(simulationCell.Age <> Integer.MaxValue)
            simulationCell.Age += 1

        End If

        'Record output here because Tst should not be incremented until after the output has been recorded.
        Me.OnSummaryStateClassOutput(simulationCell, iteration, timestep)
        Me.OnSummaryStateAttributeOutput(simulationCell, iteration, timestep)

        'If there are Tst values then increment them by one.
        If (simulationCell.TstValues.Count > 0) Then

            Debug.Assert(Me.m_TstTransitionGroupMap.HasItems)

            For Each t As Tst In simulationCell.TstValues
                t.TstValue += 1
            Next

        End If

    End Sub

    ''' <summary>
    ''' Applies probabilistic transitions for the specified cell in non-raster mode
    ''' </summary>
    ''' <param name="simulationCell"></param>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <param name="TransitionGroup"></param>
    ''' <param name="transitionedPixels"></param>
    ''' <param name="rasterTransitionAttrValues"></param>
    ''' <remarks></remarks>
    Private Sub ApplyProbabilisticTransitionsByCell(
        ByVal simulationCell As Cell,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal TransitionGroup As TransitionGroup,
        ByVal transitionedPixels() As Integer,
        ByVal rasterTransitionAttrValues As Dictionary(Of Integer, Double()))

        If (simulationCell.StratumId = 0 Or simulationCell.StateClassId = 0) Then
            Return
        End If

        Dim CumulativeProbability As Double = 0.0
        Dim RandomNextDouble As Double = Me.m_RandomGenerator.GetNextDouble

        For Each tr As Transition In simulationCell.Transitions

            If (TransitionGroup.PrimaryTransitionTypes.Contains(tr.TransitionTypeId)) Then

                Dim multiplier As Double = Me.GetTransitionMultiplier(
                    tr.TransitionTypeId, iteration, timestep,
                    simulationCell.StratumId, simulationCell.SecondaryStratumId, simulationCell.TertiaryStratumId,
                    simulationCell.StateClassId)

                Dim tt As TransitionType = Me.m_TransitionTypes(tr.TransitionTypeId)

                For Each tg As TransitionGroup In tt.TransitionGroups

                    multiplier *= Me.GetTransitionTargetMultiplier(
                        tg.TransitionGroupId, simulationCell.StratumId, simulationCell.SecondaryStratumId, simulationCell.TertiaryStratumId, iteration, timestep)

                Next

                If (Me.m_TransitionAttributeTargets.Count > 0) Then
                    multiplier = Me.ModifyMultiplierForTransitionAttributeTarget(multiplier, tt, simulationCell, iteration, timestep)
                End If

                If (Me.IsSpatial) Then

                    multiplier *= Me.GetTransitionSpatialMultiplier(simulationCell.CellId, tr.TransitionTypeId, iteration, timestep)

                    For Each tg As TransitionGroup In tt.TransitionGroups

                        multiplier *= Me.GetTransitionAdjacencyMultiplier(
                            tg.TransitionGroupId, iteration, timestep, simulationCell.StratumId, simulationCell.SecondaryStratumId, simulationCell.TertiaryStratumId, simulationCell)
                        multiplier *= Me.GetExternalSpatialMultiplier(simulationCell, timestep, tg.TransitionGroupId)

                    Next

                End If

                CumulativeProbability += (tr.Probability * tr.Proportion * multiplier)

                If (CumulativeProbability > RandomNextDouble) Then

                    Me.OnSummaryTransitionOutput(simulationCell, tr, iteration, timestep)
                    Me.OnSummaryTransitionByStateClassOutput(simulationCell, tr, iteration, timestep)

                    Me.ChangeCellForProbabilisticTransition(simulationCell, tr, iteration, timestep, rasterTransitionAttrValues)
                    Me.FillProbabilisticTransitionsForCell(simulationCell, iteration, timestep)

                    If (Me.IsSpatial) Then
                        Me.UpdateTransitionedPixels(simulationCell, tr.TransitionTypeId, transitionedPixels)
                    End If

                    Exit For

                End If

            End If

        Next

    End Sub

    ''' <summary>
    ''' Fills the transition lists with transitions that apply to the specified cell
    ''' </summary>
    ''' <param name="simulationCell"></param>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <remarks></remarks>
    Private Sub FillProbabilisticTransitionsForCell(
        ByVal simulationCell As Cell,
        ByVal iteration As Integer,
        ByVal timestep As Integer)

        'DEVTODO: would it make sense to check for zero multipliers (temporal or spatial or area targets) to 
        'shorten this list based on excluding transitions that are impossible based on these?

        'Always clear the transition list for the cell
        simulationCell.Transitions.Clear()

        'Get the list of transitions that are applicable to this cell.  If there are none then exit now.
        Dim trlist As TransitionCollection = Me.GetTransitionCollection(simulationCell, iteration, timestep)

        If (trlist Is Nothing) Then
            Return
        End If

        'But if there are transitions, add them to the cell's transition list
        For Each tr As Transition In trlist

            If (simulationCell.Age < tr.AgeMinimum) Then
                Continue For
            End If

            If (simulationCell.Age > tr.AgeMaximum) Then
                Continue For
            End If

            If (Not Me.CompareTstValues(simulationCell, tr)) Then
                Continue For
            End If

            Debug.Assert(Not simulationCell.Transitions.Contains(tr))

#If DEBUG Then
            If (tr.StratumIdSource.HasValue) Then
                Debug.Assert(Me.m_Strata.Contains(tr.StratumIdSource.Value))
            End If
#End If

            simulationCell.Transitions.Add(tr)

        Next

        'We need to randomize the order of the list in case the sum of the probabilities exceeds 1 so that we don't 
        'bias up the probability of transitions at the top of the list.

        For i As Integer = simulationCell.Transitions.Count - 1 To 0 Step -1

            Dim n As Integer = Me.m_RandomGenerator.GetNextInteger(i + 1)
            Dim t As Transition = simulationCell.Transitions(i)

            simulationCell.Transitions(i) = simulationCell.Transitions(n)
            simulationCell.Transitions(n) = t

        Next

    End Sub

    ''' <summary>
    ''' Changes a simulation cell's for a deterministic transition
    ''' </summary>
    ''' <param name="simulationCell">The simulation cell to change</param>
    ''' <param name="dt">The deterministic transition</param>
    ''' <remarks></remarks>
    Private Sub ChangeCellForDeterministicTransition(
        ByVal simulationCell As Cell,
        ByVal dt As DeterministicTransition,
        ByVal iteration As Integer,
        ByVal timestep As Integer)

        Me.UpdateCellStratum(simulationCell, dt.StratumIdDestination, iteration, timestep)
        Me.UpdateCellStateClass(simulationCell, dt.StateClassIdDestination, iteration, timestep)

        Dim dtdest As DeterministicTransition =
            Me.GetDeterministicTransition(simulationCell.StratumId, simulationCell.StateClassId, iteration, timestep)

        If (dtdest Is Nothing) Then
            simulationCell.Age = 0
        Else

            'DEVTODO: allow to class to be null and in this case set the simulationCell.Age to dtdest.AgeMaximum
            simulationCell.Age = dtdest.AgeMinimum

        End If

        Debug.Assert(Me.m_Strata.Contains(simulationCell.StratumId))

    End Sub

    ''' <summary>
    ''' Changes a simulation cell's for a probabilistic transition
    ''' </summary>
    ''' <param name="simulationCell">The cell that is changing</param>
    ''' <param name="tr">The transition</param>
    ''' <param name="iteration">The iteration</param>
    ''' <param name="timestep">The timestep</param>
    ''' <remarks></remarks>
    Private Sub ChangeCellForProbabilisticTransition(
        ByVal simulationCell As Cell,
        ByVal tr As Transition,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal rasterTransitionAttrValues As Dictionary(Of Integer, Double()))

        RaiseEvent ChangingCellProbabilistic(
            Me, New CellChangeEventArgs(simulationCell, iteration, timestep, Nothing, tr, Me.m_AmountPerCell))

        Me.GenerateTransitionAttributes(simulationCell, tr, iteration, timestep, rasterTransitionAttrValues)

        Me.UpdateCellStratum(simulationCell, tr.StratumIdDestination, iteration, timestep)
        Me.UpdateCellStateClass(simulationCell, tr.StateClassIdDestination, iteration, timestep)

        Me.ChangeCellAgeForProbabilisticTransition(simulationCell, iteration, timestep, tr)
        Me.ChangeCellTstForProbabilisticTransition(simulationCell, tr)

        Debug.Assert(Me.m_Strata.Contains(simulationCell.StratumId))
        Debug.Assert(simulationCell.StateClassId <> 0)

    End Sub

    ''' <summary>
    ''' Updates the stratum for the specified cell using the specified destination stratum
    ''' </summary>
    ''' <param name="simulationCell"></param>
    ''' <param name="destinationStratumId"></param>
    ''' <remarks></remarks>
    Private Sub UpdateCellStratum(
        ByVal simulationCell As Cell,
        ByVal destinationStratumId As Nullable(Of Integer),
        ByVal iteration As Integer,
        ByVal timestep As Integer)

        Debug.Assert(simulationCell.StratumId <> 0)
        Debug.Assert(simulationCell.StateClassId <> 0)

        If (destinationStratumId.HasValue) Then

            If (destinationStratumId.Value <> simulationCell.StratumId) Then

                Dim stold As Stratum = Me.m_Strata(simulationCell.StratumId)
                Dim stnew As Stratum = Me.m_Strata(destinationStratumId.Value)

                If (Me.IsSpatial) Then

                    Debug.Assert(stold.Cells.ContainsKey(simulationCell.CellId))
                    Debug.Assert(Not stnew.Cells.ContainsKey(simulationCell.CellId))

                    stold.Cells.Remove(simulationCell.CellId)
                    stnew.Cells.Add(simulationCell.CellId, simulationCell)

                End If

                simulationCell.StratumId = destinationStratumId.Value

                If (Me.IsSpatial) Then
                    Me.UpdateTransitionsSpreadGroupMembership(simulationCell, iteration, timestep)
                End If

                Me.m_ProportionAccumulatorMap.Decrement(stold.StratumId, simulationCell.SecondaryStratumId, simulationCell.TertiaryStratumId)
                Me.m_ProportionAccumulatorMap.AddOrIncrement(stnew.StratumId, simulationCell.SecondaryStratumId, simulationCell.TertiaryStratumId)

            End If

        End If

    End Sub

    ''' <summary>
    ''' Updates the stratum for the specified cell using the specified destination state class
    ''' </summary>
    ''' <param name="simulationCell"></param>
    ''' <param name="destinationStateClassId"></param>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <remarks></remarks>
    Private Sub UpdateCellStateClass(
        ByVal simulationCell As Cell,
        ByVal destinationStateClassId As Nullable(Of Integer),
        ByVal iteration As Integer,
        ByVal timestep As Integer)

        Debug.Assert(simulationCell.StratumId <> 0)
        Debug.Assert(simulationCell.StateClassId <> 0)

        If (destinationStateClassId.HasValue) Then

            If (destinationStateClassId <> simulationCell.StateClassId) Then

                simulationCell.StateClassId = destinationStateClassId.Value

                If (Me.IsSpatial) Then
                    Me.UpdateTransitionsSpreadGroupMembership(simulationCell, iteration, timestep)
                End If

            End If

        End If

    End Sub

    ''' <summary>
    ''' Resets the applicable transitions for all cells
    ''' </summary>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <remarks></remarks>
    Private Sub ResetTransitionsForCells(ByVal iteration As Integer, ByVal timestep As Integer)

        For Each simulationCell As Cell In Me.m_Cells
            Me.FillProbabilisticTransitionsForCell(simulationCell, iteration, timestep)
        Next

#If DEBUG Then

        For Each c As Cell In Me.m_Cells
            Me.VALIDATE_CELL_TRANSITIONS(c, iteration, timestep)
        Next

#End If

    End Sub

    Private Sub STSimExternalDataReady(ByVal dataSheet As DataSheet, ByVal previousData As DataTable)

        If (dataSheet.Name = DATASHEET_PT_NAME) Then

            Me.m_Transitions.Clear()
            Me.FillProbabilisticTransitionsCollection()
            Me.m_TransitionMap = New TransitionMap(Me.ResultScenario, Me.m_Transitions)

        ElseIf (dataSheet.Name = DATASHEET_TRANSITION_TARGET_NAME) Then

            Me.m_TransitionTargets.Clear()
            Me.FillTransitionTargetCollection()
            Me.InitializeTransitionTargetDistributionValues()
            Me.m_TransitionTargetMap = New TransitionTargetMap(Me.ResultScenario, Me.m_TransitionTargets)

        ElseIf (dataSheet.Name = DATASHEET_TRANSITION_MULTIPLIER_VALUE_NAME) Then

            Me.m_TransitionMultiplierValues.Clear()
            Me.FillTransitionMultiplierValueCollection()

            For Each tmt As TransitionMultiplierType In Me.m_TransitionMultiplierTypes
                tmt.CreateMultiplierValueMap()
                tmt.CreateMultiplierValueMap()
            Next

        ElseIf (dataSheet.Name = DATASHEET_TRANSITION_SPATIAL_MULTIPLIER_NAME) Then

            If (Me.m_IsSpatial) Then

                Me.m_TransitionSpatialMultipliers.Clear()
                Me.m_TransitionSpatialMultiplierRasters.Clear()
                Me.FillTransitionSpatialMultiplierCollection()

                For Each tmt As TransitionMultiplierType In Me.m_TransitionMultiplierTypes
                    tmt.ClearSpatialMultiplierMap()
                Next

                For Each sm As TransitionSpatialMultiplier In Me.m_TransitionSpatialMultipliers
                    Dim mt As TransitionMultiplierType = Me.GetTransitionMultiplierType(sm.TransitionMultiplierTypeId)
                    mt.AddTransitionSpatialMultiplier(sm)
                Next

                For Each tmt As TransitionMultiplierType In Me.m_TransitionMultiplierTypes
                    tmt.CreateSpatialMultiplierMap()
                Next

            End If

        ElseIf (dataSheet.Name = DATASHEET_TRANSITION_SPATIAL_INITIATION_MULTIPLIER_NAME) Then

            If (Me.m_IsSpatial) Then

                Me.m_TransitionSpatialInitiationMultipliers.Clear()
                Me.m_TransitionSpatialInitiationMultiplierRasters.Clear()
                Me.FillTransitionSpatialInitiationMultiplierCollection()

                For Each tmt As TransitionMultiplierType In Me.m_TransitionMultiplierTypes
                    tmt.ClearSpatialInitiationMultiplierMap()
                Next

                For Each sm As TransitionSpatialInitiationMultiplier In Me.m_TransitionSpatialInitiationMultipliers
                    Dim mt As TransitionMultiplierType = Me.GetTransitionMultiplierType(sm.TransitionMultiplierTypeId)
                    mt.AddTransitionSpatialInitiationMultiplier(sm)
                Next

                For Each tmt As TransitionMultiplierType In Me.m_TransitionMultiplierTypes
                    tmt.CreateSpatialInitiationMultiplierMap()
                Next

            End If

        ElseIf (dataSheet.Name = DATASHEET_TRANSITION_ORDER_NAME) Then

            Me.m_TransitionOrders.Clear()
            Me.FillTransitionOrderCollection()
            Me.m_TransitionOrderMap = New TransitionOrderMap(Me.m_TransitionOrders)

        ElseIf (dataSheet.Name = DATASHEET_STATE_ATTRIBUTE_VALUE_NAME) Then

            Me.m_StateAttributeValues.Clear()
            Me.FillStateAttributeValueCollection()
            Me.m_StateAttributeTypeIdsAges = Nothing
            Me.m_StateAttributeTypeIdsNoAges = Nothing
            Me.m_StateAttributeValueMapAges = Nothing
            Me.m_StateAttributeValueMapNoAges = Nothing
            Me.InitializeStateAttributes()

        ElseIf (dataSheet.Name = DATASHEET_TRANSITION_ATTRIBUTE_VALUE_NAME) Then

            Me.m_TransitionAttributeValues.Clear()
            Me.FillTransitionAttributeValueCollection()
            Me.m_TransitionAttributeValueMap = Nothing
            Me.m_TransitionAttributeTypeIds = Nothing
            Me.InitializeTransitionAttributes()

        ElseIf (dataSheet.Name = DATASHEET_TRANSITION_ATTRIBUTE_TARGET_NAME) Then

            Me.m_TransitionAttributeTargets.Clear()
            Me.FillTransitionAttributeTargetCollection()
            Me.InitializeTransitionAttributeTargetDistributionValues()
            Me.m_TransitionAttributeTargetMap = New TransitionAttributeTargetMap(Me.ResultScenario, Me.m_TransitionAttributeTargets)

        Else

            Dim msg As String = String.Format(CultureInfo.InvariantCulture, "External data is not supported for: {0}", dataSheet.Name)
            Throw New TransformerFailedException(msg)

        End If

    End Sub

    ''' <summary>
    ''' Resets the remaining amounts for transition attribute targets
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ResetTransitionAttributeRemainingTargetAmounts()

        For Each t As TransitionAttributeTarget In Me.m_TransitionAttributeTargets
            t.TargetRemaining = t.CurrentValue.Value
        Next

    End Sub

    ''' <summary>
    ''' Resets all strata cell collections
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ResetStrataCellCollections()

        For Each s As Stratum In Me.m_Strata

#If DEBUG Then
            For Each c As Cell In s.Cells.Values
                Debug.Assert(c.StratumId <> 0)
                Debug.Assert(c.StateClassId <> 0)
            Next
#End If

            s.Cells.Clear()

        Next

    End Sub

    Private Function GetDeterministicTransition(
        ByVal simulationCell As Cell,
        ByVal iteration As Integer,
        ByVal timestep As Integer) As DeterministicTransition

        Return Me.GetDeterministicTransition(
            simulationCell.StratumId,
            simulationCell.StateClassId,
            iteration,
            timestep)

    End Function

    Private Function GetDeterministicTransition(
        ByVal stratumId As Nullable(Of Integer),
        ByVal stateClassId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer) As DeterministicTransition

        Return Me.m_DeterministicTransitionMap.GetDeterministicTransition(
            stratumId,
            stateClassId,
            iteration,
            timestep)

    End Function

    Private Function GetTransitionCollection(
        ByVal simulationCell As Cell,
        ByVal iteration As Integer,
        ByVal timestep As Integer) As TransitionCollection

        Return Me.GetTransitionCollection(
            simulationCell.StratumId,
            simulationCell.StateClassId,
            iteration,
            timestep)

    End Function

    Private Function GetTransitionCollection(
        ByVal stratumId As Integer,
        ByVal stateClassId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer) As TransitionCollection

        Return Me.m_TransitionMap.GetTransitions(
            stratumId,
            stateClassId,
            iteration,
            timestep)

    End Function

End Class


