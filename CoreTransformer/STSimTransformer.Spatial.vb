'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports System.IO
Imports System.Globalization
Imports SyncroSim.Core
Imports SyncroSim.Common
Imports SyncroSim.StochasticTime

Partial Class STSimTransformer

    ''' <summary>Get the Cell Neighbor at the compass direction North relative to the specified Cell</summary>
    ''' <param name="initiationCell">The cell we're looking for the neighbor of</param>
    ''' <returns>The neighboring Cell at the North compass direction</returns>
    Private Function GetCellNorth(ByVal initiationCell As Cell) As Cell
        Return GetCellByOffset(initiationCell.CellId, -1, 0)
    End Function

    ''' <summary>Get the Cell Neighbor at the compass direction North East relative to the specified Cell</summary>
    ''' <param name="initiationCell">The cell we're looking for the neighbor of</param>
    ''' <returns>The neighboring Cell at the North East compass direction</returns>
    Private Function GetCellNortheast(ByVal initiationCell As Cell) As Cell
        Return GetCellByOffset(initiationCell.CellId, -1, 1)
    End Function

    ''' <summary>Get the Cell Neighbor at the compass direction East relative to the specified Cell</summary>
    ''' <param name="initiationCell">The cell we're looking for the neighbor of</param>
    ''' <returns>The neighboring Cell at the East compass direction</returns>
    Private Function GetCellEast(ByVal initiationCell As Cell) As Cell
        Return GetCellByOffset(initiationCell.CellId, 0, 1)
    End Function

    ''' <summary>Get the Cell Neighbor at the compass direction South East relative to the specified Cell</summary>
    ''' <param name="initiationCell">The cell we're looking for the neighbor of</param>
    ''' <returns>The neighboring Cell at the South East compass direction</returns>
    Private Function GetCellSoutheast(ByVal initiationCell As Cell) As Cell
        Return GetCellByOffset(initiationCell.CellId, 1, 1)
        Return Nothing
    End Function

    ''' <summary>Get the Cell Neighbor at the compass direction South relative to the specified Cell</summary>
    ''' <param name="initiationCell">The cell we're looking for the neighbor of</param>
    ''' <returns>The neighboring Cell at the South compass direction</returns>
    Private Function GetCellSouth(ByVal initiationCell As Cell) As Cell
        Return GetCellByOffset(initiationCell.CellId, 1, 0)
        Return Nothing
    End Function

    ''' <summary>Get the Cell Neighbor at the compass direction South West relative to the specified Cell</summary>
    ''' <param name="initiationCell">The cell we're looking for the neighbor of</param>
    ''' <returns>The neighboring Cell at the South West compass direction</returns>
    Private Function GetCellSouthwest(ByVal initiationCell As Cell) As Cell
        Return GetCellByOffset(initiationCell.CellId, 1, -1)
    End Function

    ''' <summary>Get the Cell Neighbor at the compass direction West relative to the specified Cell</summary>
    ''' <param name="initiationCell">The cell we're looking for the neighbor of</param>
    ''' <returns>The neighboring Cell at the West compass direction</returns>
    Private Function GetCellWest(ByVal initiationCell As Cell) As Cell
        Return GetCellByOffset(initiationCell.CellId, 0, -1)
    End Function

    ''' <summary>Get the Cell Neighbor at the compass direction North West relative to the specified Cell</summary>
    ''' <param name="initiationCell">The cell we're looking for the neighbor of</param>
    ''' <returns>The neighboring Cell at the North West compass direction</returns>
    Private Function GetCellNorthwest(ByVal initiationCell As Cell) As Cell
        Return GetCellByOffset(initiationCell.CellId, -1, -1)
    End Function

    ''' <summary>
    ''' Gets a cell for the specified initiation cell Id and row and column offsets
    ''' </summary>
    ''' <param name="initiationCellId"></param>
    ''' <param name="rowOffset"></param>
    ''' <param name="columnOffset"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCellByOffset(ByVal initiationCellId As Integer, ByVal rowOffset As Integer, ByVal columnOffset As Integer) As Cell

        Dim id As Integer = Me.m_InputRasters.GetCellIdByOffset(initiationCellId, rowOffset, columnOffset)

        If id = -1 Then
            Return Nothing
        Else

            If Me.Cells.Contains(id) Then
                Return Me.Cells(id)
            Else
                Return Nothing
            End If

        End If

    End Function

    Private Function GetNeighboringCells(ByVal c As Cell) As List(Of Cell)

        Dim neighbors As New List(Of Cell)

        Dim addNeighbor = Sub(c1 As Cell)
                              If (c1 IsNot Nothing) Then
                                  neighbors.Add(c1)
                              End If
                          End Sub

        addNeighbor(Me.GetCellNorth(c))
        addNeighbor(Me.GetCellNortheast(c))
        addNeighbor(Me.GetCellEast(c))
        addNeighbor(Me.GetCellSoutheast(c))
        addNeighbor(Me.GetCellSouth(c))
        addNeighbor(Me.GetCellSouthwest(c))
        addNeighbor(Me.GetCellWest(c))
        addNeighbor(Me.GetCellNorthwest(c))

        Return neighbors

    End Function

    ''' <summary>Get the Cell Neighbor at the direction and distance relative to the specified Cell</summary>
    ''' <param name="initiationCell">The cell we're looking for the neighbor of</param>
    ''' <returns>The neighboring Cell at the specified direction and distance </returns>
    Private Function GetCellByDistanceAndDirection(ByVal initiationCell As Cell, ByVal directionDegrees As Integer, ByVal distanceM As Double) As Cell

        Dim id As Integer = Me.m_InputRasters.GetCellIdByDistanceAndDirection(initiationCell.CellId, directionDegrees, distanceM)

        If id = -1 Then
            Return Nothing
        Else

            If Me.Cells.Contains(id) Then
                Return Me.Cells(id)
            Else
                Return Nothing
            End If

        End If

    End Function

    ''' <summary>
    ''' Gets the distance between two neighboring cells
    ''' </summary>
    ''' <param name="direction"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetNeighborCellDistance(ByVal direction As CardinalDirection) As Double

        Dim dist As Double = Me.m_InputRasters.GetCellSizeMeters()

        If (direction = CardinalDirection.NE Or direction = CardinalDirection.SE Or direction = CardinalDirection.SW Or direction = CardinalDirection.NW) Then
            dist = Me.m_InputRasters.GetCellSizeDiagonalMeters
        End If

        Return dist

    End Function

    ''' <summary>
    ''' Gets the slope for the specified cells
    ''' </summary>
    ''' <param name="sourceCell"></param>
    ''' <param name="destinationCell"></param>
    ''' <param name="distance"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSlope(ByVal sourceCell As Cell, ByVal destinationCell As Cell, ByVal distance As Double) As Double

        Dim rise As Double = Me.GetCellElevation(destinationCell) - Me.GetCellElevation(sourceCell)
        Dim radians As Double = Math.Atan(rise / distance)
        Dim degrees As Double = radians * (180 / Math.PI)

        Return degrees

    End Function

    ''' <summary>
    ''' Gets the elevation for the specified cell
    ''' </summary>
    ''' <param name="cell"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCellElevation(ByVal cell As Cell) As Double

        If Me.m_InputRasters.DemCells Is Nothing OrElse (Me.m_InputRasters.DemCells.Count = 0) Then
            Return 1.0
        Else
            Return Me.m_InputRasters.DemCells(cell.CellId)
        End If

    End Function

    ''' <summary>
    ''' Gets the average attribute value for the specified cell's neighborhood and attribute type
    ''' </summary>
    ''' <param name="cell"></param>
    ''' <param name="transitionGroupId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetNeighborhoodAttributeValue(ByVal cell As Cell, ByVal transitionGroupId As Integer) As Nullable(Of Double)

        If Me.m_TransitionAdjacencyStateAttributeValueMap.ContainsKey(transitionGroupId) Then

            Dim attrVals As Double() = Me.m_TransitionAdjacencyStateAttributeValueMap(transitionGroupId)

            If attrVals(cell.CellId) = ApexRaster.DEFAULT_NO_DATA_VALUE Then
                Return Nothing
            Else
                Return attrVals(cell.CellId)
            End If
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Updates the transitioned pixels array for the specified timestep
    ''' </summary>
    ''' <param name="cell"></param>
    ''' <param name="transitionTypeId"></param>
    ''' <param name="transitionedPixels"></param>
    ''' <remarks></remarks>
    Private Sub UpdateTransitionedPixels(
        ByVal cell As Cell,
        ByVal transitionTypeId As Integer,
        ByVal transitionedPixels() As Integer)

        Debug.Assert(Me.IsSpatial)

        If Not (Me.m_CreateRasterTransitionOutput Or Me.m_CreateRasterAATPOutput) Then
            Exit Sub
        End If

        'Dereference to find TT "ID". If blank, dont bother to record transition.
        Dim TransTypeMapId As Nullable(Of Integer) = Me.m_TransitionTypes.Item(transitionTypeId).MapId

        If TransTypeMapId.HasValue Then
            transitionedPixels(cell.CellId) = TransTypeMapId.Value
        End If

    End Sub

    ''' <summary>
    ''' Creates a dictionary of transitioned pixel arrays, with Transition Group Id as the dictionary key
    ''' </summary>
    ''' <returns>Dictionary(Of Integer, Integer())</returns>
    ''' <remarks></remarks>
    Private Function CreateTransitionGroupTransitionedPixels() As Dictionary(Of Integer, Integer())

        Debug.Assert(Me.IsSpatial)

        Dim dictTransitionPixels As New Dictionary(Of Integer, Integer())

        ' Loop thru transition groups. 
        For Each tg As TransitionGroup In Me.m_TransitionGroups

            'Make sure Primary
            If tg.PrimaryTransitionTypes.Count = 0 Then
                Continue For
            End If

            ' Create a transitionPixel array object. If no Transition Output actually configured, economize on memory by not
            ' dimensioning the array

            Dim transitionPixel As Integer() = Nothing

            If (Me.m_CreateRasterTransitionOutput Or Me.m_CreateRasterAATPOutput) Then

                ReDim transitionPixel(Me.m_InputRasters.NumberCells - 1)
                ' initialize to DEFAULT_NO_DATA_VLAUE
                For i = 0 To Me.m_InputRasters.NumberCells - 1
                    transitionPixel(i) = ApexRaster.DEFAULT_NO_DATA_VALUE
                Next

            End If

            dictTransitionPixels.Add(tg.TransitionGroupId, transitionPixel)

        Next

        Return dictTransitionPixels

    End Function

    ''' <summary>
    ''' Creates a dictionary of transition attribute value arrays
    ''' </summary>
    ''' <param name="timestep"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateRasterTransitionAttributeArrays(ByVal timestep As Integer) As Dictionary(Of Integer, Double())

        Debug.Assert(Me.IsSpatial)

        Dim dict As New Dictionary(Of Integer, Double())

        If (Me.IsRasterTransitionAttributeTimestep(timestep)) Then

            For Each id As Integer In Me.m_TransitionAttributeTypeIds.Keys

                Debug.Assert(Me.m_TransitionAttributeTypes.Contains(id))
                Dim arr(Me.m_InputRasters.NumberCells - 1) As Double

                'Initialize array to ApexRaster.DEFAULT_NO_DATA_VALUE

                For i As Integer = 0 To Me.m_InputRasters.NumberCells - 1
                    arr(i) = ApexRaster.DEFAULT_NO_DATA_VALUE
                Next

                dict.Add(id, arr)

            Next

        End If

        Return dict

    End Function

    ''' <summary>
    ''' Applies probabilistic transitions in raster mode
    ''' </summary>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <remarks></remarks>
    Private Sub ApplyProbabilisticTransitionsRaster(
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal rasterTransitionAttrValues As Dictionary(Of Integer, Double()),
        ByVal dictTransitionedPixels As Dictionary(Of Integer, Integer()))

        Debug.Assert(Me.IsSpatial)

        For Each Stratum As Stratum In Me.m_Strata
            Me.ShuffleStratumCells(Stratum)
        Next

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

            'If the transition group has no size distribution or transition patches then call the non-spatial algorithm for this group.

            If ((Not TransitionGroup.HasSizeDistribution) And (TransitionGroup.PatchPrioritization Is Nothing)) Then

                For Each simulationCell As Cell In Me.m_Cells

                    ApplyProbabilisticTransitionsByCell(
                        simulationCell, iteration, timestep, TransitionGroup,
                        dictTransitionedPixels(TransitionGroup.TransitionGroupId),
                        rasterTransitionAttrValues)

                Next

            Else

                Dim TransitionedCells As New Dictionary(Of Integer, Cell)

                For Each Stratum As Stratum In Me.m_Strata

                    Dim ExpectedArea As Double = 0.0
                    Dim MaxCellProbability As Double = 0.0

                    Me.FillTransitionPatches(TransitionedCells, Stratum, TransitionGroup, iteration, timestep)

                    Dim InitiationCells As Dictionary(Of Integer, Cell) =
                        Me.CreateInitiationCellCollection(
                            TransitionedCells,
                            Stratum.StratumId,
                            TransitionGroup.TransitionGroupId,
                            iteration,
                            timestep,
                            ExpectedArea,
                            MaxCellProbability)

                    If (ExpectedArea > 0.0 And MaxCellProbability > 0.0) Then

                        Dim GroupHasTarget As Boolean = TransitionGroupHasTarget(
                            TransitionGroup.TransitionGroupId, Stratum.StratumId, iteration, timestep)

                        Dim MaximizeFidelityToTotalArea As Boolean = Me.MaximizeFidelityToTotalArea(
                            TransitionGroup.TransitionGroupId, Stratum.StratumId, iteration, timestep)

                        Dim rand As Double = Me.m_RandomGenerator.GetNextDouble()

                        While ((MathUtils.CompareDoublesGT(ExpectedArea / Me.m_AmountPerCell, rand, 0.000001)) And (InitiationCells.Count > 0))

                            Dim TransitionEventList As List(Of TransitionEvent) =
                                Me.CreateTransitionEventList(
                                    Stratum.StratumId,
                                    TransitionGroup.TransitionGroupId,
                                    iteration,
                                    timestep,
                                    ExpectedArea)

                            Me.GenerateTransitionEvents(
                                TransitionEventList,
                                TransitionedCells,
                                InitiationCells,
                                Stratum.StratumId,
                                TransitionGroup.TransitionGroupId,
                                iteration,
                                timestep,
                                MaxCellProbability,
                                dictTransitionedPixels(TransitionGroup.TransitionGroupId),
                                ExpectedArea,
                                rasterTransitionAttrValues)

                            If (Not GroupHasTarget) Then

                                If (Not MaximizeFidelityToTotalArea) Then
                                    Exit While
                                End If

                            Else

                                Dim d As Dictionary(Of Integer, TransitionAttributeTarget) = tatMap.GetItem(Stratum.StratumId)

                                If d IsNot Nothing Then

                                    Dim TargetsMet As Boolean = True

                                    For Each tat As TransitionAttributeTarget In d.Values

                                        If tat.TargetRemaining > 0.0 Then
                                            TargetsMet = False
                                            Exit For
                                        End If

                                        If TargetsMet Then
                                            Exit While
                                        End If

                                    Next

                                End If

                            End If

                        End While

                    End If

                    Me.ClearTransitionPatches(TransitionGroup)

                Next

            End If

        Next

    End Sub

    Private Function CreateTransitionEventList(
        ByVal stratumId As Integer,
        ByVal transitionGroupId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal expectedArea As Double) As List(Of TransitionEvent)

        Debug.Assert(Me.IsSpatial)
        Debug.Assert(expectedArea > 0.0)

        Dim AccumulatedArea As Double = 0.0
        Dim TransitionEventList As New List(Of TransitionEvent)

        While (MathUtils.CompareDoublesGT(expectedArea, AccumulatedArea, 0.000001))

            Dim diff As Double = expectedArea - AccumulatedArea

            If (Me.m_AmountPerCell > diff) Then

                Dim rand As Double = Me.m_RandomGenerator.GetNextDouble()
                Dim prob As Double = diff / Me.m_AmountPerCell

                If rand > prob Then
                    Exit While
                End If

            End If

            Dim MinimumSize As Double = Me.m_AmountPerCell
            Dim MaximumSize As Double = (expectedArea - AccumulatedArea)
            Dim TargetSize As Double = Me.m_AmountPerCell
            Dim AreaDifference As Double = (expectedArea - AccumulatedArea)

            Me.GetTargetSizeClass(
                stratumId, transitionGroupId, iteration, timestep,
                AreaDifference, MinimumSize, MaximumSize, TargetSize)

            TransitionEventList.Add(New TransitionEvent(TargetSize))

            AccumulatedArea = AccumulatedArea + TargetSize

            Debug.Assert(MinimumSize >= 0.0)
            Debug.Assert(MaximumSize >= 0.0)
            Debug.Assert(TargetSize >= 0.0)
            Debug.Assert(MinimumSize <= MaximumSize)
            Debug.Assert(TargetSize >= MinimumSize And TargetSize <= MaximumSize)
            Debug.Assert(TransitionEventList.Count < 100000)

        End While

        Me.SortTransitionEventList(
            stratumId, transitionGroupId, iteration, timestep, TransitionEventList)

        Return TransitionEventList

    End Function

    Private Sub GetTargetSizeClass(
        ByVal stratumId As Integer,
        ByVal transitionGroupId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal areaDifference As Double,
        ByRef minimumSizeOut As Double,
        ByRef maximumSizeOut As Double,
        ByRef targetSizeOut As Double)

        Debug.Assert(Me.IsSpatial)

        Dim CumulativeProportion As Double = 0.0
        Dim Rand1 As Double = Me.m_RandomGenerator.GetNextDouble()

        Dim tsdlist As List(Of TransitionSizeDistribution) =
            Me.m_TransitionSizeDistributionMap.GetSizeDistributions(
                transitionGroupId,
                stratumId,
                iteration,
                timestep)

        If (tsdlist Is Nothing) Then

            minimumSizeOut = Me.m_AmountPerCell
            maximumSizeOut = Me.m_AmountPerCell
            targetSizeOut = Me.m_AmountPerCell

            Return

        End If

        For Each tsd As TransitionSizeDistribution In tsdlist

            CumulativeProportion += tsd.Proportion

            If (CumulativeProportion >= Rand1) Then

                minimumSizeOut = tsd.MinimumSize
                maximumSizeOut = tsd.MaximumSize

                Exit For

            End If

        Next

        Debug.Assert(minimumSizeOut <= maximumSizeOut)

        If (maximumSizeOut > areaDifference) Then

            maximumSizeOut = areaDifference
            minimumSizeOut = areaDifference

        End If

        Dim Rand2 As Double = Me.m_RandomGenerator.GetNextDouble()
        Dim Rand3 As Double = (maximumSizeOut - minimumSizeOut) * Rand2

        targetSizeOut = Rand3 + minimumSizeOut

    End Sub

    ''' <summary>
    ''' Determines whether to maximize fidelity to total area
    ''' </summary>
    ''' <param name="transitionGroupId"></param>
    ''' <param name="stratumId"></param>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MaximizeFidelityToTotalArea(
        ByVal transitionGroupId As Integer,
        ByVal stratumId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer) As Boolean

        Dim tsp As TransitionSizePrioritization =
            Me.m_TransitionSizePrioritizationMap.GetSizePrioritization(
                transitionGroupId, stratumId, iteration, timestep)

        If (tsp Is Nothing) Then
            Return False
        Else
            Return tsp.MaximizeFidelityToTotalArea
        End If

    End Function

    ''' <summary>
    ''' Determines whether there are transition targets or transition attribute targets associated with this 
    ''' transition group, stratum, iteration and timestep
    ''' </summary>
    ''' <param name="transitionGroupId"></param>
    ''' <param name="stratumId"></param>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function TransitionGroupHasTarget(
        ByVal transitionGroupId As Integer,
        ByVal stratumId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer) As Boolean

        'Check transition Targets First

        If Me.m_SecondaryStrata.Count > 0 Then

            For Each SecondaryStratum As Stratum In Me.m_SecondaryStrata

                Dim t As TransitionTarget = Me.m_TransitionTargetMap.GetTransitionTarget(
                    transitionGroupId,
                    stratumId,
                    SecondaryStratum.StratumId,
                    iteration,
                    timestep)

                If (t IsNot Nothing) Then
                    Return True
                End If

            Next

        Else

            Dim t As TransitionTarget = Me.m_TransitionTargetMap.GetTransitionTarget(
                transitionGroupId,
                stratumId,
                Nothing,
                iteration,
                timestep)

            If (t IsNot Nothing) Then
                Return True
            End If

        End If

        'Now Check for Transition Attribute Targets

        If Me.m_TransitionAttributeValueMap.TypeGroupMap.ContainsKey(transitionGroupId) Then

            Dim Dict As Dictionary(Of Integer, Boolean) =
                Me.m_TransitionAttributeValueMap.TypeGroupMap(transitionGroupId)

            For Each TransitionAttribute As TransitionAttributeType In Me.m_TransitionAttributeTypes

                If Dict.ContainsKey(TransitionAttribute.TransitionAttributeId) Then

                    If Me.m_SecondaryStrata.Count > 0 Then

                        For Each SecondaryStratum As Stratum In Me.m_SecondaryStrata

                            Dim t As TransitionAttributeTarget = Me.m_TransitionAttributeTargetMap.GetAttributeTarget(
                                TransitionAttribute.TransitionAttributeId,
                                stratumId,
                                SecondaryStratum.StratumId,
                                iteration,
                                timestep)

                            If (t IsNot Nothing) Then
                                Return True
                            End If

                        Next

                    Else

                        Dim t As TransitionAttributeTarget = Me.m_TransitionAttributeTargetMap.GetAttributeTarget(
                            TransitionAttribute.TransitionAttributeId,
                            stratumId,
                            Nothing,
                            iteration,
                            timestep)

                        If (t IsNot Nothing) Then
                            Return True
                        End If

                    End If

                End If

            Next

        End If

        Return False

    End Function

    Private Sub SortTransitionEventList(
        ByVal stratumId As Integer,
        ByVal transitionGroupId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal transitionEventList As List(Of TransitionEvent))

        Dim tsp As TransitionSizePrioritization =
            Me.m_TransitionSizePrioritizationMap.GetSizePrioritization(transitionGroupId, stratumId, iteration, timestep)

        If ((tsp Is Nothing) OrElse (tsp.SizePrioritization = SizePrioritization.None)) Then
            ShuffleUtilities.ShuffleList(transitionEventList, Me.m_RandomGenerator.Random)
        Else

            If (tsp.SizePrioritization = SizePrioritization.Smallest) Then

                transitionEventList.Sort(
                    Function(e1 As TransitionEvent, e2 As TransitionEvent) As Integer
                        Return e1.TargetAmount.CompareTo(e2.TargetAmount)
                    End Function)

            Else

                transitionEventList.Sort(
                    Function(e1 As TransitionEvent, e2 As TransitionEvent) As Integer
                        Return (-(e1.TargetAmount.CompareTo(e2.TargetAmount)))
                    End Function)

            End If

        End If

    End Sub

    Private Function CreateInitiationCellCollection(
        ByVal transitionedCells As Dictionary(Of Integer, Cell),
        ByVal stratumId As Integer,
        ByVal transitionGroupId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByRef expectedAreaOut As Double,
        ByRef maxCellProbabilityOut As Double) As Dictionary(Of Integer, Cell)

        Debug.Assert(Me.IsSpatial)

        Dim ExpectedArea As Double = 0.0
        Dim MaxCellProbability As Double = 0.0
        Dim Stratum As Stratum = Me.m_Strata(stratumId)
        Dim InitiationCells As New Dictionary(Of Integer, Cell)

        For Each SimulationCell As Cell In Stratum.Cells.Values

            Debug.Assert(SimulationCell.StratumId <> 0)
            Debug.Assert(SimulationCell.StateClassId <> 0)

            If (Not transitionedCells.ContainsKey(SimulationCell.CellId)) Then

                Dim CellProbability As Double = Me.SpatialCalculateCellProbabilityNonTruncated(
                    SimulationCell, transitionGroupId, iteration, timestep)

                ExpectedArea += (CellProbability * Me.m_AmountPerCell)

                'Include Initiation Multiplier in the calculation of cell probability once expected area has been calculated

                CellProbability *= Me.GetTransitionSpatialInitiationMultiplier(
                    SimulationCell.CellId, transitionGroupId, iteration, timestep)

                If (CellProbability > MaxCellProbability) Then

                    MaxCellProbability = CellProbability

                    If MaxCellProbability > 1.0 Then
                        MaxCellProbability = 1.0
                    End If

                End If

                If (CellProbability > 0.0) Then
                    InitiationCells.Add(SimulationCell.CellId, SimulationCell)
                End If

            End If

        Next

        expectedAreaOut = ExpectedArea
        maxCellProbabilityOut = MaxCellProbability

        Return InitiationCells

    End Function

    Private Function SelectInitiationCell(
        ByVal initiationCells As Dictionary(Of Integer, Cell),
        ByVal transitionGroupId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal maxCellProbability As Double) As Cell

        Debug.Assert(Me.IsSpatial)

        Dim SimulationCell As Cell = Nothing
        Dim CellProbability As Double = 0.0
        Dim Rand1 As Double = Me.m_RandomGenerator.GetNextDouble
        Dim NumCellsChecked As Integer
        Dim KeepLooping As Boolean = True

        Do

            NumCellsChecked += 1
            Dim Rand2 As Integer = Me.m_RandomGenerator.GetNextInteger(0, (initiationCells.Count - 1))
            SimulationCell = initiationCells.Values.ElementAt(Rand2)

            CellProbability = Me.SpatialCalculateCellProbability(SimulationCell, transitionGroupId, iteration, timestep)
            CellProbability *= Me.GetTransitionSpatialInitiationMultiplier(SimulationCell.CellId, transitionGroupId, iteration, timestep)
            CellProbability = CellProbability / maxCellProbability

            'Increase probability of selection as the number of cells checked increases

            If (CellProbability < (NumCellsChecked / initiationCells.Count)) Then
                CellProbability = NumCellsChecked / initiationCells.Count
            End If

            Rand1 = Me.m_RandomGenerator.GetNextDouble()

            If (Not MathUtils.CompareDoublesGT(Rand1, CellProbability, 0.000001)) Then
                KeepLooping = False
            End If

            If (CellProbability = 0.0) Then
                KeepLooping = True
            End If

            If (initiationCells.Count = 0) Then
                KeepLooping = False
            End If

        Loop While (KeepLooping)

        initiationCells.Remove(SimulationCell.CellId)

        Return SimulationCell

    End Function

    Private Function SelectSpatialTransitionPathway(
        ByVal simulationCell As Cell,
        ByVal transitionGroupId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer) As Transition

        Debug.Assert(Me.IsSpatial)

        Dim SumProbability As Double = 0.0
        Dim Transitions As New TransitionCollection()

        For Each tr As Transition In simulationCell.Transitions

            Dim tt As TransitionType = Me.m_TransitionTypes(tr.TransitionTypeId)

            If (tt.PrimaryTransitionGroups.Contains(transitionGroupId)) Then

                Dim multiplier As Double = Me.GetTransitionMultiplier(
                    tr.TransitionTypeId, iteration, timestep, simulationCell.StratumId, simulationCell.SecondaryStratumId, simulationCell.StateClassId)

                multiplier *= Me.GetTransitionTargetMultiplier(transitionGroupId, simulationCell.StratumId, simulationCell.SecondaryStratumId, iteration, timestep)
                multiplier *= Me.GetTransitionSpatialMultiplier(simulationCell.CellId, tr.TransitionTypeId, iteration, timestep)

                For Each tg As TransitionGroup In tt.TransitionGroups

                    multiplier *= Me.GetTransitionAdjacencyMultiplier(
                        tg.TransitionGroupId, iteration, timestep, simulationCell.StratumId, simulationCell.SecondaryStratumId, simulationCell)
                    multiplier *= Me.GetExternalSpatialMultiplier(simulationCell, timestep, tg.TransitionGroupId)

                Next



                If (Me.m_TransitionAttributeTargets.Count > 0) Then
                    multiplier = Me.ModifyMultiplierForTransitionAttributeTarget(multiplier, tt, simulationCell, iteration, timestep)
                End If

                SumProbability += tr.Probability * tr.Proportion * multiplier
                Transitions.Add(tr)

            End If

        Next

        Dim CumulativeProbability As Double = 0.0
        Dim Rand As Double = Me.m_RandomGenerator.GetNextDouble()

        For Each tr As Transition In Transitions

            Dim tt As TransitionType = Me.m_TransitionTypes(tr.TransitionTypeId)

            Dim multiplier As Double = Me.GetTransitionMultiplier(
                tr.TransitionTypeId, iteration, timestep, simulationCell.StratumId, simulationCell.SecondaryStratumId, simulationCell.StateClassId)

            multiplier *= Me.GetTransitionTargetMultiplier(
                transitionGroupId, simulationCell.StratumId, simulationCell.SecondaryStratumId, iteration, timestep)

            multiplier *= Me.GetTransitionSpatialMultiplier(
                simulationCell.CellId, tr.TransitionTypeId, iteration, timestep)

            For Each tg As TransitionGroup In tt.TransitionGroups

                multiplier *= Me.GetTransitionAdjacencyMultiplier(
                    tg.TransitionGroupId, iteration, timestep, simulationCell.StratumId, simulationCell.SecondaryStratumId, simulationCell)
                multiplier *= Me.GetExternalSpatialMultiplier(simulationCell, timestep, tg.TransitionGroupId)

            Next


            If (Me.m_TransitionAttributeTargets.Count > 0) Then
                multiplier = Me.ModifyMultiplierForTransitionAttributeTarget(multiplier, tt, simulationCell, iteration, timestep)
            End If

            CumulativeProbability += ((tr.Probability * tr.Proportion * multiplier) / SumProbability)

            If (CumulativeProbability > Rand) Then
                Return tr
            End If

        Next

        Return Nothing

    End Function

    Private Sub GenerateTransitionEvents(
        ByVal transitionEventList As List(Of TransitionEvent),
        ByVal transitionedCells As Dictionary(Of Integer, Cell),
        ByVal initiationCells As Dictionary(Of Integer, Cell),
        ByVal stratumId As Integer,
        ByVal transitionGroupId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal maxCellProbability As Double,
        ByVal transitionedPixels() As Integer,
        ByRef expectedArea As Double,
        ByVal rasterTransitionAttrValues As Dictionary(Of Integer, Double()))

#If DEBUG Then

        Debug.Assert(Me.IsSpatial)
        Debug.Assert(maxCellProbability > 0.0)

        For Each c As Cell In initiationCells.Values
            Debug.Assert(c.StratumId = stratumId)
        Next

#End If

        Dim TransitionGroup As TransitionGroup = Me.m_TransitionGroups(transitionGroupId)

        While ((transitionEventList.Count > 0) And (initiationCells.Count > 0) And (expectedArea > 0))

            Dim InitiationCell As Cell = Nothing

            If (TransitionGroup.PatchPrioritization IsNot Nothing) Then

                InitiationCell = Me.SelectPatchInitiationCell(TransitionGroup)

                If (InitiationCell Is Nothing) Then

                    Debug.Assert(TransitionGroup.PatchPrioritization.TransitionPatches.Count = 0)
                    initiationCells.Clear() 'No Patches left.  Clear Initiation Cells.

                    Exit While

                End If

            Else
                InitiationCell = Me.SelectInitiationCell(initiationCells, transitionGroupId, iteration, timestep, maxCellProbability)
            End If

            If (InitiationCell IsNot Nothing) Then

                Dim CellProbability As Double = Me.SpatialCalculateCellProbability(InitiationCell, transitionGroupId, iteration, timestep)

                If (CellProbability > 0.0) Then

                    Dim TransitionEvent As TransitionEvent = transitionEventList(0)

                    Dim tsp As TransitionSizePrioritization =
                        Me.m_TransitionSizePrioritizationMap.GetSizePrioritization(transitionGroupId, stratumId, iteration, timestep)

                    Me.GrowTransitionEvent(
                        transitionEventList,
                        TransitionEvent,
                        transitionedCells,
                        initiationCells,
                        InitiationCell,
                        transitionGroupId,
                        iteration,
                        timestep,
                        transitionedPixels,
                        expectedArea,
                        rasterTransitionAttrValues,
                        tsp)

                End If

            End If

        End While

    End Sub

    Private Sub GrowTransitionEvent(
        ByVal transitionEventList As List(Of TransitionEvent),
        ByVal transitionEvent As TransitionEvent,
        ByVal transitionedCells As Dictionary(Of Integer, Cell),
        ByVal initiationCells As Dictionary(Of Integer, Cell),
        ByVal initiationCell As Cell,
        ByVal transitionGroupId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal transitionedPixels() As Integer,
        ByRef expectedArea As Double,
        ByVal rasterTransitionAttrValues As Dictionary(Of Integer, Double()),
        ByVal tsp As TransitionSizePrioritization)

        Debug.Assert(Me.IsSpatial)

        Dim TotalEventAmount As Double = 0.0
        Dim EventCandidates As New GrowEventRecordCollection(Me.m_RandomGenerator)
        Dim SeenBefore As New Dictionary(Of Integer, Cell)

        EventCandidates.AddRecord(New GrowEventRecord(initiationCell, 0.0, 1.0))
        SeenBefore.Add(initiationCell.CellId, initiationCell)

        Dim transitionDictionary As New Dictionary(Of Integer, Transition)

        While ((EventCandidates.Count > 0) And (TotalEventAmount <= expectedArea))

            Dim Transition As Transition = Nothing
            Dim CurrentRecord As GrowEventRecord = EventCandidates.RemoveRecord()
            Dim neighbors As List(Of Cell) = Me.GetNeighboringCells(CurrentRecord.Cell)

            Dim AutoCorrelation As TransitionPathwayAutoCorrelation =
                        Me.m_TransitionPathwayAutoCorrelationMap.GetCorrelation(
                            transitionGroupId,
                            CurrentRecord.Cell.StratumId,
                            CurrentRecord.Cell.SecondaryStratumId,
                            iteration,
                            timestep)

            If (AutoCorrelation IsNot Nothing) Then
                For Each c As Cell In neighbors
                    If transitionDictionary.ContainsKey(c.CellId) Then
                        Dim neighborTransition As Transition = transitionDictionary(c.CellId)
                        If CurrentRecord.Cell.Transitions.Contains(neighborTransition) Then
                            Transition = neighborTransition
                            Exit For
                        End If

                    End If
                Next
            End If

            If (Transition Is Nothing) Then

                If (AutoCorrelation IsNot Nothing) Then
                    If ((AutoCorrelation.SpreadOnlyToLike) And (transitionDictionary.Count > 0)) Then
                        Continue While
                    End If
                End If

                Transition = Me.SelectSpatialTransitionPathway(CurrentRecord.Cell, transitionGroupId, iteration, timestep)

            Else

                Dim rnd As Double = Me.m_RandomGenerator.GetNextDouble()

                If (AutoCorrelation Is Nothing OrElse rnd > AutoCorrelation.Factor) Then
                    Transition = Me.SelectSpatialTransitionPathway(CurrentRecord.Cell, transitionGroupId, iteration, timestep)
                End If

            End If

            If (Transition Is Nothing) Then
                Continue While
            End If

            If (Me.IsTransitionAttributeTargetExceded(CurrentRecord.Cell, Transition, iteration, timestep)) Then

                initiationCells.Remove(CurrentRecord.Cell.CellId)
                Continue While

            End If

            Me.OnSummaryTransitionOutput(CurrentRecord.Cell, Transition, iteration, timestep)
            Me.OnSummaryTransitionByStateClassOutput(CurrentRecord.Cell, Transition, iteration, timestep)

            Me.ChangeCellForProbabilisticTransition(CurrentRecord.Cell, Transition, iteration, timestep, rasterTransitionAttrValues)

            If Not transitionDictionary.ContainsKey(CurrentRecord.Cell.CellId) Then
                transitionDictionary.Add(CurrentRecord.Cell.CellId, Transition)
            End If

            Me.FillProbabilisticTransitionsForCell(CurrentRecord.Cell, iteration, timestep)

            Me.UpdateCellPatchMembership(transitionGroupId, CurrentRecord.Cell)
            Me.UpdateTransitionedPixels(CurrentRecord.Cell, Transition.TransitionTypeId, transitionedPixels)

            Debug.Assert(Not transitionedCells.ContainsKey(CurrentRecord.Cell.CellId))

            transitionedCells.Add(CurrentRecord.Cell.CellId, CurrentRecord.Cell)
            initiationCells.Remove(CurrentRecord.Cell.CellId)

            TotalEventAmount += Me.m_AmountPerCell

            If ((TotalEventAmount >= (transitionEvent.TargetAmount - (0.5 * Me.m_AmountPerCell))) Or (TotalEventAmount >= expectedArea)) Then
                Exit While
            End If

            Me.AddGrowEventRecords(
                EventCandidates,
                transitionedCells,
                SeenBefore,
                CurrentRecord.Cell,
                transitionGroupId,
                iteration,
                timestep,
                CurrentRecord.TravelTime)

        End While

        expectedArea -= TotalEventAmount

        If expectedArea < 0.0 Then
            expectedArea = 0.0
        End If

        Dim MaximizeFidelityToDistribution As Boolean = True

        If (tsp IsNot Nothing) Then
            MaximizeFidelityToDistribution = tsp.MaximizeFidelityToDistribution
        End If

        If ((Not MaximizeFidelityToDistribution) Or (TotalEventAmount >= transitionEvent.TargetAmount)) Then
            transitionEventList.Remove(transitionEvent)
        Else
            RemoveNearestSizedEvent(transitionEventList, TotalEventAmount)
        End If

    End Sub

    Private Sub AddGrowEventRecords(
        ByVal eventCandidates As GrowEventRecordCollection,
        ByVal transitionedCells As Dictionary(Of Integer, Cell),
        ByVal seenBefore As Dictionary(Of Integer, Cell),
        ByVal initiationCell As Cell,
        ByVal transitionGroupId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByRef travelTime As Double)

        Debug.Assert(Me.IsSpatial)

        Me.AddGrowEventRecord(eventCandidates, transitionedCells, seenBefore, initiationCell, Me.GetCellNorth(initiationCell), transitionGroupId, iteration, timestep, travelTime, CardinalDirection.N)
        Me.AddGrowEventRecord(eventCandidates, transitionedCells, seenBefore, initiationCell, Me.GetCellEast(initiationCell), transitionGroupId, iteration, timestep, travelTime, CardinalDirection.E)
        Me.AddGrowEventRecord(eventCandidates, transitionedCells, seenBefore, initiationCell, Me.GetCellSouth(initiationCell), transitionGroupId, iteration, timestep, travelTime, CardinalDirection.S)
        Me.AddGrowEventRecord(eventCandidates, transitionedCells, seenBefore, initiationCell, Me.GetCellWest(initiationCell), transitionGroupId, iteration, timestep, travelTime, CardinalDirection.W)
        Me.AddGrowEventRecord(eventCandidates, transitionedCells, seenBefore, initiationCell, Me.GetCellNortheast(initiationCell), transitionGroupId, iteration, timestep, travelTime, CardinalDirection.NE)
        Me.AddGrowEventRecord(eventCandidates, transitionedCells, seenBefore, initiationCell, Me.GetCellSoutheast(initiationCell), transitionGroupId, iteration, timestep, travelTime, CardinalDirection.SE)
        Me.AddGrowEventRecord(eventCandidates, transitionedCells, seenBefore, initiationCell, Me.GetCellSouthwest(initiationCell), transitionGroupId, iteration, timestep, travelTime, CardinalDirection.SW)
        Me.AddGrowEventRecord(eventCandidates, transitionedCells, seenBefore, initiationCell, Me.GetCellNorthwest(initiationCell), transitionGroupId, iteration, timestep, travelTime, CardinalDirection.NW)

    End Sub

    Private Sub AddGrowEventRecord(
        ByVal eventCandidates As GrowEventRecordCollection,
        ByVal transitionedCells As Dictionary(Of Integer, Cell),
        ByVal seenBefore As Dictionary(Of Integer, Cell),
        ByVal initiationCell As Cell,
        ByVal simulationCell As Cell,
        ByVal transitionGroupId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal travelTime As Double,
        ByVal direction As CardinalDirection)

        Debug.Assert(Me.IsSpatial)

        If (simulationCell IsNot Nothing) Then

            If ((Not transitionedCells.ContainsKey(simulationCell.CellId)) And
                (Not seenBefore.ContainsKey(simulationCell.CellId))) Then

                Dim tg As TransitionGroup = Me.m_TransitionGroups(transitionGroupId)

                If (tg.PatchPrioritization IsNot Nothing) Then

                    If (tg.PatchPrioritization.PatchPrioritizationType = PatchPrioritizationType.LargestEdgesOnly Or
                        tg.PatchPrioritization.PatchPrioritizationType = PatchPrioritizationType.SmallestEdgesOnly) Then

                        If tg.PatchPrioritization.TransitionPatches.Count() = 0 Then
                            Return
                        End If

                        Dim patch As TransitionPatch = tg.PatchPrioritization.TransitionPatches.First()

                        If (tg.PatchPrioritization.PatchPrioritizationType = PatchPrioritizationType.LargestEdgesOnly) Then
                            patch = tg.PatchPrioritization.TransitionPatches.Last()
                        End If

                        If (Not patch.EdgeCells.ContainsKey(simulationCell.CellId)) Then
                            Return
                        End If

                    End If

                End If

                Dim Probability As Double = Me.SpatialCalculateCellProbability(simulationCell, transitionGroupId, iteration, timestep)

                If (Probability > 0.0) Then

                    Dim dist As Double = Me.GetNeighborCellDistance(direction)
                    Dim slope As Double = GetSlope(initiationCell, simulationCell, dist)

                    Dim dirmult As Double = Me.m_TransitionDirectionMultiplierMap.GetDirectionMultiplier(
                        transitionGroupId, simulationCell.StratumId, simulationCell.SecondaryStratumId, direction, iteration, timestep)

                    Dim slopemult As Double = Me.m_TransitionSlopeMultiplierMap.GetSlopeMultiplier(
                        transitionGroupId, simulationCell.StratumId, simulationCell.SecondaryStratumId, iteration, timestep, slope)

                    Dim rate As Double = slopemult * dirmult

                    Debug.Assert(rate >= 0.0)

                    If (rate > 0.0) Then

                        Dim tt As Double = travelTime + (dist / rate)
                        'DevToDo - Change variable name li to something more understandable.
                        Dim li As Double = Probability / tt

                        Dim Record As New GrowEventRecord(simulationCell, tt, li)

                        eventCandidates.AddRecord(Record)
                        seenBefore.Add(simulationCell.CellId, simulationCell)

                    End If

                End If

            End If

        End If

    End Sub

    ''' <summary>
    ''' Removes the event that is nearest in size to the specified total event amount
    ''' </summary>
    ''' <param name="transitionEvents"></param>
    ''' <param name="totalEventAmount"></param>
    ''' <remarks>
    ''' This function expects the transition events to be sorted in descending order by target amount.
    ''' </remarks>
    Private Shared Sub RemoveNearestSizedEvent(ByVal transitionEvents As List(Of TransitionEvent), ByVal totalEventAmount As Double)

        If (transitionEvents.Count > 0) Then

            Dim RemoveEvent As TransitionEvent = Nothing
            Dim CurrentDifference As Double = Double.MaxValue

            For Each TransitionEvent As TransitionEvent In transitionEvents

                Dim ThisDifference As Double = Math.Abs(totalEventAmount - TransitionEvent.TargetAmount)

                If (ThisDifference <= CurrentDifference) Then

                    RemoveEvent = TransitionEvent
                    CurrentDifference = ThisDifference

                Else
                    Exit For
                End If

            Next

            Debug.Assert(RemoveEvent IsNot Nothing)
            transitionEvents.Remove(RemoveEvent)

        End If

    End Sub

    ''' <summary>
    ''' Create Spatial Initial Condition files and appropriate config based on Non-spatial Initial Condition configuration
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateSpatialICFromNonSpatialIC()

        Dim drta As DataRow = Me.ResultScenario.GetDataSheet(DATASHEET_NSIC_NAME).GetDataRow()
        Dim CalcNumCellsFromDist = DataTableUtilities.GetDataBool(drta, DATASHEET_NSIC_CALC_FROM_DIST_COLUMN_NAME)

        If CalcNumCellsFromDist Then
            Me.CreateRastersFromNonRasterICCalcFromDist()
        Else
            Me.CreateRastersFromNonRasterICNoCalcFromDist()
        End If

    End Sub

    ''' <summary>
    ''' Create Spatial Initial Condition files and appropriate config based on Non-spatial
    ''' Initial Condition configuration. Calculate Cell Area based on Distributtion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateRastersFromNonRasterICCalcFromDist()

        ' Fetch the number of cells from the NS IC setting
        Dim drrc As DataRow = Me.ResultScenario.GetDataSheet(DATASHEET_NSIC_NAME).GetDataRow()
        Dim numCells As Integer = CInt(drrc(DATASHEET_NSIC_NUM_CELLS_COLUMN_NAME))
        Dim ttlArea As Double = CType(drrc(DATASHEET_NSIC_TOTAL_AMOUNT_COLUMN_NAME), Double)

        CreateICSpatialProperties(numCells, ttlArea)

        ' Get a list of the Iterations that are defined in the  InitialConditionsDistribution
        Dim lstIterations = Me.m_InitialConditionsDistributions.GetSortedIterationList()

        Dim cells As New CellCollection

        For CellId As Integer = 0 To numCells - 1
            cells.Add(New Cell(CellId))
        Next

        For Each iteration In lstIterations

            Dim cellIndex As Integer = 0

            Dim icds As InitialConditionsDistributionCollection = Me.m_InitialConditionsDistributions.GetForIteration(iteration)
            Dim sumOfRelativeAmountForIteration As Double = CalcSumOfRelativeAmount(iteration)

            For Each icd As InitialConditionsDistribution In icds

                ' DEVNOTE:To support multiple iterations, use relativeAmount / sum For Iteration as scale of total number of cells. Number of cells determined by 1st iteration specified. 
                ' Otherwise, there's too much likelyhood that Number of cells will vary per iteration, which we cant/wont support.
                Dim NumCellsICD As Integer = CInt(Math.Round(icd.RelativeAmount / sumOfRelativeAmountForIteration * numCells))
                For i As Integer = 0 To NumCellsICD - 1

                    Dim c As Cell = cells(cellIndex)

                    Dim sisagemin As Integer = Math.Min(icd.AgeMin, icd.AgeMax)
                    Dim sisagemax As Integer = Math.Max(icd.AgeMin, icd.AgeMax)

                    Dim Iter As Integer = Me.MinimumIteration

                    If (iteration.HasValue) Then
                        Iter = iteration.Value
                    End If

                    Me.InitializeCellAge(
                        c,
                        icd.StratumId,
                        icd.StateClassId,
                        sisagemin,
                        sisagemax,
                        Iter,
                        Me.m_TimestepZero)

                    c.StratumId = icd.StratumId
                    c.StateClassId = icd.StateClassId
                    c.SecondaryStratumId = icd.SecondaryStratumId

                    cellIndex += 1

                Next

            Next

            ' Randomize the cell distriubtion so we dont get blocks of same  ICD pixels.
            Dim lst As New List(Of Cell)
            For Each c As Cell In cells
                lst.Add(c)
            Next

            ShuffleUtilities.ShuffleList(lst, Me.m_RandomGenerator.Random())
            SaveCellsToUndefinedICRasters(lst, iteration)

        Next

    End Sub

    ''' <summary>
    ''' Create Spatial Initial Condition files and appropriate config based on Non-spatial 
    ''' Initial Condition configuration. Use entered Cell area (don't Calculate Cell Area based on Distributtion)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateRastersFromNonRasterICNoCalcFromDist()

        ' Fetch the number of cells from the NS IC setting
        Dim drrc As DataRow = Me.ResultScenario.GetDataSheet(DATASHEET_NSIC_NAME).GetDataRow()
        Dim numCells As Integer = CInt(drrc(DATASHEET_NSIC_NUM_CELLS_COLUMN_NAME))
        Dim ttlArea As Double = CType(drrc(DATASHEET_NSIC_TOTAL_AMOUNT_COLUMN_NAME), Double)

        CreateICSpatialProperties(numCells, ttlArea)

        ' Get a list of the Iterations that are defined in the  InitialConditionsDistribution
        Dim lstIterations = Me.m_InitialConditionsDistributions.GetSortedIterationList()

        For Each iteration In lstIterations

            Dim sumOfRelativeAmount = CalcSumOfRelativeAmount(iteration)

            Dim icds As InitialConditionsDistributionCollection = Me.m_InitialConditionsDistributions.GetForIteration(iteration)

            Dim cells As New CellCollection

            For CellId As Integer = 0 To numCells - 1
                cells.Add(New Cell(CellId))
            Next

            For Each c As Cell In cells

                Dim Rand As Double = Me.m_RandomGenerator.GetNextDouble()
                Dim CumulativeProportion As Double = 0.0

                For Each icd As InitialConditionsDistribution In icds

                    CumulativeProportion += (icd.RelativeAmount / sumOfRelativeAmount)

                    If (Rand < CumulativeProportion) Then

                        Dim sisagemin As Integer = Math.Min(icd.AgeMin, icd.AgeMax)
                        Dim sisagemax As Integer = Math.Max(icd.AgeMin, icd.AgeMax)

                        Dim Iter As Integer = Me.MinimumIteration

                        If (iteration.HasValue) Then
                            Iter = iteration.Value
                        End If

                        Me.InitializeCellAge(
                            c, icd.StratumId,
                            icd.StateClassId,
                            sisagemin,
                            sisagemax,
                            Iter,
                            Me.m_TimestepZero)

                        c.StratumId = icd.StratumId
                        c.StateClassId = icd.StateClassId
                        c.SecondaryStratumId = icd.SecondaryStratumId

                        Exit For

                    End If
                Next
            Next

            Dim lst As New List(Of Cell)
            For Each c As Cell In cells
                lst.Add(c)
            Next

            SaveCellsToUndefinedICRasters(lst, iteration)

        Next

    End Sub

    ''' <summary>
    ''' Create Spatial Initial Condition files and appropriate config based on a combination of Spatial 
    ''' and Non-spatial Initial Condition configuration. 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateSpatialICFromCombinedIC()

        Dim dsIC As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_SPIC_NAME)

        ' Get a list of the Iterations that are defined in the InitialConditionsSpatials
        Dim lstIterations = Me.m_InitialConditionsSpatials.GetSortedIterationList()
        Dim StateClassDefined As Boolean = False
        Dim SecondaryStratumDefined As Boolean = False
        Dim AgeDefined As Boolean = False
        Dim sMsg As String
        Dim ICFilesCreated As Boolean = False

        For Each iteration In lstIterations

            Dim ics As InitialConditionsSpatial = Me.m_InitialConditionsSpatialMap.GetICS(iteration)
            Dim primary_stratum_cells() As Integer
            Dim ssName As String = ics.SecondaryStratumFileName
            Dim scName As String = ics.StateClassFileName
            Dim ageName As String = ics.AgeFileName
            Dim stateclass_cells(0) As Integer
            Dim age_cells(0) As Integer
            Dim secondary_stratum_cells(0) As Integer
            Dim dsRemap As DataSheet

            If ics.PrimaryStratumFileName.Length = 0 Then
                Throw New ArgumentException(ERROR_SPATIAL_PRIMARY_STRATUM_FILE_NOT_DEFINED)
            End If

            StateClassDefined = (scName <> "")
            SecondaryStratumDefined = (ssName <> "")
            AgeDefined = (ageName <> "")
            If SecondaryStratumDefined And SecondaryStratumDefined And AgeDefined Then
                ' If all the Spatial files are already defined, then we've got nothing to do for this iteration
                Continue For
            End If

            ' So we've got a PS file defined, so lets load it up
            ' Load the Primary Stratum Raster
            Dim rasterFileName As String = ics.PrimaryStratumFileName
            Dim fullFileName As String = RasterFiles.GetInputFileName(dsIC, rasterFileName, False)
            Dim raster As New ApexRaster

            RasterFiles.LoadRasterFile(fullFileName, raster, RasterDataType.dtInteger)

            ' Now lets remap the ID's in the raster to the Stratum PK values
            dsRemap = Me.Project.GetDataSheet(DATASHEET_STRATA_NAME)
            primary_stratum_cells = RasterCells.RemapRasterCells(raster.IntCells, dsRemap, DATASHEET_MAPID_COLUMN_NAME)

            ' Load the State Class Raster, if defined
            If StateClassDefined Then

                fullFileName = RasterFiles.GetInputFileName(dsIC, ics.StateClassFileName, False)
                RasterFiles.LoadRasterFile(fullFileName, raster, RasterDataType.dtInteger)

                ' Now lets remap the ID's in the raster to the State Class PK values
                dsRemap = Me.Project.GetDataSheet(DATASHEET_STATECLASS_NAME)
                stateclass_cells = RasterCells.RemapRasterCells(raster.IntCells, dsRemap, DATASHEET_MAPID_COLUMN_NAME)

                If stateclass_cells.Count() <> primary_stratum_cells.Count() Then
                    Throw New Exception(String.Format(CultureInfo.CurrentCulture, ERROR_SPATIAL_FILE_MISMATCHED_METADATA, fullFileName))
                End If

            End If

            ' Load the Age Raster, if defined
            If AgeDefined Then

                fullFileName = RasterFiles.GetInputFileName(dsIC, ics.AgeFileName, False)
                RasterFiles.LoadRasterFile(fullFileName, raster, RasterDataType.dtInteger)

                age_cells = raster.IntCells

                If age_cells.Count() <> primary_stratum_cells.Count() Then
                    Throw New Exception(String.Format(CultureInfo.CurrentCulture, ERROR_SPATIAL_FILE_MISMATCHED_METADATA, fullFileName))
                End If

            End If

            ' Load the Secondary Stratum Raster, if defined
            If SecondaryStratumDefined Then

                fullFileName = RasterFiles.GetInputFileName(dsIC, ics.SecondaryStratumFileName, False)
                RasterFiles.LoadRasterFile(fullFileName, raster, RasterDataType.dtInteger)

                ' Now lets remap the ID's in the raster to the Secondary Stratum PK values
                dsRemap = Me.Project.GetDataSheet(DATASHEET_SECONDARY_STRATA_NAME)
                secondary_stratum_cells = RasterCells.RemapRasterCells(raster.IntCells, dsRemap, DATASHEET_MAPID_COLUMN_NAME)

                If secondary_stratum_cells.Count() <> primary_stratum_cells.Count() Then
                    Throw New Exception(String.Format(CultureInfo.CurrentCulture, ERROR_SPATIAL_FILE_MISMATCHED_METADATA, fullFileName))
                End If

            End If

            ' Initalize a Cells collection
            Dim cells As New CellCollection
            For CellId As Integer = 0 To primary_stratum_cells.Count() - 1
                Dim c As New Cell(CellId)
                c.StratumId = primary_stratum_cells(CellId)

                If StateClassDefined Then
                    c.StateClassId = stateclass_cells(CellId)
                Else
                    c.StateClassId = ApexRaster.DEFAULT_NO_DATA_VALUE
                End If

                If AgeDefined Then
                    c.Age = age_cells(CellId)
                Else
                    c.Age = ApexRaster.DEFAULT_NO_DATA_VALUE
                End If

                If SecondaryStratumDefined Then
                    c.SecondaryStratumId = secondary_stratum_cells(CellId)
                Else
                    c.SecondaryStratumId = ApexRaster.DEFAULT_NO_DATA_VALUE
                End If

                cells.Add(c)
            Next

            Dim icds As InitialConditionsDistributionCollection = Me.m_InitialConditionsDistributionMap.GetICDs(iteration)
            If icds Is Nothing Then
                sMsg = String.Format(CultureInfo.CurrentCulture, STATUS_SPATIAL_RUN_USING_COMBINED_IC_MISSING_ICD, iteration.GetValueOrDefault())
                Me.AddStatusRecord(StatusRecordType.Warning, sMsg)
            Else

                For Each c As Cell In cells

                    If c.StratumId <> 0 Then

                        ' Now lets filter the ICDs by Primary Stratum, and optionally Age, StateClass, and Secondary Stratum 
                        Dim filteredICDs As InitialConditionsDistributionCollection = icds.GetFiltered(c)

                        Dim sumOfRelativeAmount = filteredICDs.CalcSumOfRelativeAmount()

                        Dim Rand As Double = Me.m_RandomGenerator.GetNextDouble()
                        Dim CumulativeProportion As Double = 0.0

                        For Each icd As InitialConditionsDistribution In filteredICDs

                            CumulativeProportion += (icd.RelativeAmount / sumOfRelativeAmount)

                            If (Rand < CumulativeProportion) Then

                                If Not AgeDefined Then

                                    Dim sisagemin As Integer = Math.Min(icd.AgeMin, icd.AgeMax)
                                    Dim sisagemax As Integer = Math.Max(icd.AgeMin, icd.AgeMax)

                                    Dim Iter As Integer = Me.MinimumIteration

                                    If (iteration.HasValue) Then
                                        Iter = iteration.Value
                                    End If

                                    Me.InitializeCellAge(
                                        c,
                                        icd.StratumId,
                                        icd.StateClassId,
                                        sisagemin,
                                        sisagemax,
                                        Iter,
                                        Me.m_TimestepZero)

                                End If

                                c.StratumId = icd.StratumId
                                c.StateClassId = icd.StateClassId
                                c.SecondaryStratumId = icd.SecondaryStratumId

                                Exit For

                            End If
                        Next
                    End If
                Next
            End If

            Dim lst As New List(Of Cell)
            For Each c As Cell In cells
                lst.Add(c)
            Next

            If SaveCellsToUndefinedICRasters(lst, ics.Iteration) Then
                ICFilesCreated = True
            End If

        Next

        If ICFilesCreated Then
            Me.AddStatusRecord(StatusRecordType.Information, STATUS_SPATIAL_RUN_USING_COMBINED_IC)
        End If

    End Sub

    ''' <summary>
    ''' Create a Initial Condition Spatial Properties record for the current Results Scenario, based on values dervied from Non-Spatial Initial condition settings
    ''' </summary>
    ''' <param name="numberOfCells">The number of cells </param>
    ''' <param name="ttlArea">The total area</param>
    ''' <remarks></remarks>
    Private Sub CreateICSpatialProperties(numberOfCells As Integer, ttlArea As Double)

        ' We want a square raster thats just big enough to accomodate the number of cells specified by user
        Dim numRasterCells As Integer = CInt(Math.Ceiling(Math.Sqrt(numberOfCells)) ^ 2)

        Dim dsSpicProp As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_SPPIC_NAME)
        Dim drSpIcProp As DataRow = dsSpicProp.GetDataRow()
        If (drSpIcProp Is Nothing) Then
            drSpIcProp = dsSpicProp.GetData().NewRow()
            dsSpicProp.GetData().Rows.Add(drSpIcProp)
        Else
            Debug.Assert(False, "We should not be here is there's already a IC Spatial Properties record defined")
        End If

        ' We need convert from Terminalogy Units to M2 for Raster.
        Dim cellSizeTermUnits As Double = ttlArea / numberOfCells

        Dim amountlabel As String = Nothing
        Dim units As TerminologyUnit
        GetAmountLabelTerminology(Me.Project.GetDataSheet(DATASHEET_TERMINOLOGY_NAME), amountlabel, units)

        Dim cellSizeUnits As String = RasterCellSizeUnits.Meter.ToString()
        Dim convFactor As Double = InitialConditionsSpatialDataFeedView.CalcCellArea(1.0, cellSizeUnits, units)
        Dim cellArea As Double = cellSizeTermUnits / convFactor

        drSpIcProp(DATASHEET_SPPIC_NUM_ROWS_COLUMN_NAME) = CInt(Math.Sqrt(numRasterCells))
        drSpIcProp(DATASHEET_SPPIC_NUM_COLUMNS_COLUMN_NAME) = CInt(Math.Sqrt(numRasterCells))
        drSpIcProp(DATASHEET_SPPIC_NUM_CELLS_COLUMN_NAME) = numberOfCells
        drSpIcProp(DATASHEET_SPPIC_CELL_AREA_COLUMN_NAME) = cellSizeTermUnits
        drSpIcProp(DATASHEET_SPPIC_CELL_SIZE_COLUMN_NAME) = CDec(Math.Sqrt(cellArea))

        ' Arbitrary values
        drSpIcProp(DATASHEET_SPPIC_CELL_SIZE_UNITS_COLUMN_NAME) = cellSizeUnits
        drSpIcProp(DATASHEET_SPPIC_YLLCORNER_COLUMN_NAME) = 0
        drSpIcProp(DATASHEET_SPPIC_XLLCORNER_COLUMN_NAME) = 0

        ' DEVNOTE: Set Projection  - Corresponds to NAD83 / UTM zone 12N EPSG:26912. Totally arbitrary, but need something to support units of Meters.
        drSpIcProp(DATASHEET_SPPIC_SRS_COLUMN_NAME) = "+proj=utm +zone=10 +ellps=GRS80 +towgs84=0,0,0,0,0,0,0 +units=m +no_defs"

    End Sub

    ''' <summary>
    ''' Save the values found in the List of Cells to Initial Conditions Spatial Input Raster files, using default naming templates. Note that the file 
    ''' will only be saved in currently unspecified in the Initial Conditions Spatial datasheet. Also, update the appropriate
    ''' file names in the  Initial Conditions Spatial datasheet.  
    ''' </summary>
    ''' <param name="cells">A List of Cell objects</param>
    ''' <param name="iteration">The iteration that we are creating the raster file(s) for.</param>
    ''' <returns>True is a raster file was saved</returns>
    ''' <remarks>Raster files will only be created if not already defined in the Initial Conditions Spatial datasheet.</remarks>
    Private Function SaveCellsToUndefinedICRasters(cells As List(Of Cell), iteration As Integer?) As Boolean

        Dim rasterSaved As Boolean = False

        Dim iterVal As Integer
        If iteration Is Nothing Then
            iterVal = 0
        Else
            iterVal = iteration.Value
        End If

        ' OK, we've got an Initialized cells collection. So now lets create the Initial Condition rasters as required.
        Dim numValidCells As Integer = cells.Count
        Dim rst As New ApexRaster


        ' Get the IC Spatial properties
        Dim dsSpIcProp As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_SPPIC_NAME)
        Dim drProp As DataRow = dsSpIcProp.GetDataRow()

        rst.ProjectionString = drProp(DATASHEET_SPPIC_SRS_COLUMN_NAME).ToString()
        rst.NumberCols = CInt(drProp(DATASHEET_SPPIC_NUM_COLUMNS_COLUMN_NAME))
        rst.NumberRows = CInt(drProp(DATASHEET_SPPIC_NUM_ROWS_COLUMN_NAME))
        rst.CellSize = CDec(drProp(DATASHEET_SPPIC_CELL_SIZE_COLUMN_NAME))
        rst.CellSizeUnits = drProp(DATASHEET_SPPIC_CELL_SIZE_UNITS_COLUMN_NAME).ToString()
        rst.XllCorner = CDec(drProp(DATASHEET_SPPIC_XLLCORNER_COLUMN_NAME))
        rst.YllCorner = CDec(drProp(DATASHEET_SPPIC_YLLCORNER_COLUMN_NAME))

        ' We also need to get the datarow for this InitialConditionSpatial
        Dim filter As String
        Dim dsSpatialIC As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_SPIC_NAME)
        Dim drICS As DataRow
        If IsNothing(iteration) Then
            filter = String.Format(CultureInfo.InvariantCulture, "iteration is null")
        Else
            filter = String.Format(CultureInfo.InvariantCulture, "iteration={0}", iteration.Value)
        End If
        Dim drICSpatials() As DataRow = dsSpatialIC.GetData().Select(filter)
        If (drICSpatials.Count() = 0) Then
            drICS = dsSpatialIC.GetData().NewRow()
            If iteration.HasValue Then
                drICS(DATASHEET_ITERATION_COLUMN_NAME) = iteration
            End If
            dsSpatialIC.GetData().Rows.Add(drICS)
        Else
            drICS = drICSpatials(0)
        End If

        Dim dsRemap As DataSheet
        Dim filename As String

        ' Create Primary Stratum file,  if not already defined
        If drICS(DATASHEET_SPIC_STRATUM_FILE_COLUMN_NAME).ToString() = "" Then

            rst.InitIntCells()
            For i = 0 To numValidCells - 1
                rst.IntCells(i) = cells(i).StratumId
            Next

            ' We need to remap the Primary Stratum PK to the Raster values ( PK - > ID)
            dsRemap = Me.Project.GetDataSheet(DATASHEET_STRATA_NAME)
            rst.IntCells = RasterCells.RemapRasterCells(rst.IntCells, dsRemap, DATASHEET_MAPID_COLUMN_NAME, False, ApexRaster.DEFAULT_NO_DATA_VALUE)

            filename = SavePrimaryStratumInputRaster(rst, Me.ResultScenario, iterVal, 0)
            File.SetAttributes(filename, FileAttributes.Normal)
            drICS(DATASHEET_SPIC_STRATUM_FILE_COLUMN_NAME) = Path.GetFileName(filename)
            dsSpatialIC.AddExternalInputFile(filename)
            rasterSaved = True
        End If


        ' Create State Class IC raster, if not already defined
        If drICS(DATASHEET_SPIC_STATE_CLASS_FILE_COLUMN_NAME).ToString() = "" Then

            rst.InitIntCells()
            For i = 0 To numValidCells - 1
                rst.IntCells(i) = cells(i).StateClassId
            Next

            ' We need to remap the State Class PK to the Raster values ( PK - > ID)
            dsRemap = Me.Project.GetDataSheet(DATASHEET_STATECLASS_NAME)
            rst.IntCells = RasterCells.RemapRasterCells(rst.IntCells, dsRemap, DATASHEET_MAPID_COLUMN_NAME, False, ApexRaster.DEFAULT_NO_DATA_VALUE)

            filename = SaveStateClassInputRaster(rst, Me.ResultScenario, iterVal, 0)
            File.SetAttributes(filename, FileAttributes.Normal)
            drICS(DATASHEET_SPIC_STATE_CLASS_FILE_COLUMN_NAME) = Path.GetFileName(filename)
            dsSpatialIC.AddExternalInputFile(filename)
            rasterSaved = True
        End If

        ' Create Secondary Stratum IC raster , if appropriate and/or not already defined
        If drICS(DATASHEET_SPIC_SECONDARY_STRATUM_FILE_COLUMN_NAME).ToString() = "" Then

            rst.InitIntCells()
            For i = 0 To numValidCells - 1
                If cells(i).SecondaryStratumId.HasValue Then
                    rst.IntCells(i) = cells(i).SecondaryStratumId.Value
                End If
            Next

            ' Test the 2nd stratum has values worth exporting
            If rst.IntCells.Distinct().Count() > 1 Or rst.IntCells(0) <> ApexRaster.DEFAULT_NO_DATA_VALUE Then

                ' We need to remap the Stratum PK to the Raster values ( PK - > ID)
                dsRemap = Me.Project.GetDataSheet(DATASHEET_SECONDARY_STRATA_NAME)
                rst.IntCells = RasterCells.RemapRasterCells(rst.IntCells, dsRemap, DATASHEET_MAPID_COLUMN_NAME, False, ApexRaster.DEFAULT_NO_DATA_VALUE)

                filename = SaveSecondaryStratumInputRaster(rst, Me.ResultScenario, iterVal, 0)
                File.SetAttributes(filename, FileAttributes.Normal)
                drICS(DATASHEET_SPIC_SECONDARY_STRATUM_FILE_COLUMN_NAME) = Path.GetFileName(filename)
                dsSpatialIC.AddExternalInputFile(filename)
                rasterSaved = True

            End If

        End If

        ' Create Age IC raster , if not already defined
        If drICS(DATASHEET_SPIC_AGE_FILE_COLUMN_NAME).ToString() = "" Then

            'Create Age IC Raster
            rst.InitIntCells()
            For i = 0 To numValidCells - 1
                rst.IntCells(i) = cells(i).Age
            Next

            filename = SaveAgeInputRaster(rst, Me.ResultScenario, iterVal, 0)
            File.SetAttributes(filename, FileAttributes.Normal)
            drICS(DATASHEET_SPIC_AGE_FILE_COLUMN_NAME) = Path.GetFileName(filename)
            dsSpatialIC.AddExternalInputFile(filename)
            rasterSaved = True

        End If

        Return rasterSaved

    End Function

    ''' <summary>
    ''' Calculates the cell probability
    ''' </summary>
    ''' <param name="simulationCell"></param>
    ''' <param name="transitionGroupId"></param>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <returns>If the probability excedes 1.0 then it returns 1.0</returns>
    ''' <remarks></remarks>
    Private Function SpatialCalculateCellProbability(
        ByVal simulationCell As Cell,
        ByVal transitionGroupId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer) As Double

        Debug.Assert(Me.IsSpatial)

        Dim CellProbability As Double = Me.SpatialCalculateCellProbabilityNonTruncated(
            simulationCell, transitionGroupId, iteration, timestep)

        If CellProbability > 1.0 Then
            CellProbability = 1.0
        End If

        Return CellProbability

    End Function

    ''' <summary>
    ''' Calculates the cell probability
    ''' </summary>
    ''' <param name="simulationCell"></param>
    ''' <param name="transitionGroupId"></param>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' If the probability excedes 1 it will not be adjusted in any way.
    ''' </remarks>
    Private Function SpatialCalculateCellProbabilityNonTruncated(
        ByVal simulationCell As Cell,
        ByVal transitionGroupId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer) As Double

        Debug.Assert(Me.IsSpatial)

        Dim CellProbability As Double = 0.0
        Dim TransitionGroup As TransitionGroup = Me.m_TransitionGroups(transitionGroupId)

        For Each tr As Transition In simulationCell.Transitions

            If (TransitionGroup.PrimaryTransitionTypes.Contains(tr.TransitionTypeId)) Then

                Dim multiplier As Double = GetTransitionMultiplier(
                    tr.TransitionTypeId, iteration, timestep, simulationCell.StratumId, simulationCell.SecondaryStratumId, simulationCell.StateClassId)

                multiplier *= Me.GetTransitionTargetMultiplier(
                    transitionGroupId, simulationCell.StratumId, simulationCell.SecondaryStratumId, iteration, timestep)

                If (Me.IsSpatial) Then

                    multiplier *= Me.GetTransitionSpatialMultiplier(simulationCell.CellId, tr.TransitionTypeId, iteration, timestep)

                    Dim tt As TransitionType = Me.m_TransitionTypes(tr.TransitionTypeId)

                    For Each tg As TransitionGroup In tt.TransitionGroups

                        multiplier *= Me.GetTransitionAdjacencyMultiplier(
                            tg.TransitionGroupId, iteration, timestep, simulationCell.StratumId, simulationCell.SecondaryStratumId, simulationCell)
                        multiplier *= Me.GetExternalSpatialMultiplier(simulationCell, timestep, tg.TransitionGroupId)

                    Next



                End If

                If (Me.m_TransitionAttributeTargets.Count > 0) Then

                    Dim tt As TransitionType = Me.TransitionTypes.Item(tr.TransitionTypeId)
                    multiplier = Me.ModifyMultiplierForTransitionAttributeTarget(multiplier, tt, simulationCell, iteration, timestep)

                End If

                CellProbability += tr.Probability * tr.Proportion * multiplier

            End If

        Next

        Return CellProbability

    End Function

    ''' <summary>
    ''' Initializes all simulations cells in Raster mode
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeCellsRaster(ByVal iteration As Integer)

        Debug.Assert(Me.IsSpatial)
        Debug.Assert(Me.m_Cells.Count > 0)

        'Loop thru cells and set stratum(s),state class, and age.
        'Note that some cells in the raster don't have a valid state class or stratum.
        'We need to ignore these cells in this routine.

        For i As Integer = 0 To Me.m_InputRasters.NumberCells - 1

            ' Skip a cell that wasnt initially created because of StateClass or Stratum = 0
            If Not Me.m_Cells.Contains(i) Then
                Continue For
            End If

            Dim c As Cell = Me.m_Cells(i)

            c.StateClassId = Me.m_InputRasters.SClassCells(i)
            c.StratumId = Me.m_InputRasters.StratumCells(i)

            Debug.Assert(Not (c.StateClassId = 0 Or c.StratumId = 0),
                "The Cell object should never have been created with StateClass or Stratum = 0")

            If Me.m_InputRasters.SecondaryStratumCells IsNot Nothing Then
                c.SecondaryStratumId = Me.m_InputRasters.SecondaryStratumCells(i)
            End If

            If Me.m_InputRasters.AgeCells Is Nothing Then
                Me.InitializeCellAge(c, c.StratumId, c.StateClassId, 0, Integer.MaxValue, iteration, Me.m_TimestepZero)
            Else

                c.Age = Me.m_InputRasters.AgeCells(i)
                Dim ndv As Integer = Me.m_InputRasters.NoDataValueAsInteger

                If (c.Age = ndv And ndv <> 0) Then
                    Me.InitializeCellAge(c, c.StratumId, c.StateClassId, 0, Integer.MaxValue, iteration, Me.m_TimestepZero)
                Else

                    Dim dt As DeterministicTransition =
                        Me.GetDeterministicTransition(c, iteration, Me.m_TimestepZero)

                    If (dt IsNot Nothing) Then

                        If (c.Age < dt.AgeMinimum Or c.Age > dt.AgeMaximum) Then
                            c.Age = Me.m_RandomGenerator.GetNextInteger(dt.AgeMinimum, dt.AgeMaximum)
                        End If

                    End If

                End If

            End If

            Me.InitializeCellTstValues(c, iteration)

#If DEBUG Then
            Me.VALIDATE_INITIALIZED_CELL(c, iteration, Me.m_TimestepZero)
#End If

            Me.m_Strata(c.StratumId).Cells.Add(c.CellId, c)
            Me.m_ProportionAccumulatorMap.AddOrIncrement(c.StratumId, c.SecondaryStratumId)

            Me.OnSummaryStateClassOutput(c, iteration, Me.m_TimestepZero)
            Me.OnSummaryStateAttributeOutput(c, iteration, Me.m_TimestepZero)

            RaiseEvent CellInitialized(Me, New CellEventArgs(c, iteration, Me.m_TimestepZero, Me.m_AmountPerCell))

        Next

        RaiseEvent CellsInitialized(Me, New CellEventArgs(Nothing, iteration, Me.m_TimestepZero, Me.m_AmountPerCell))

    End Sub

    ''' <summary>
    ''' Fills the raster data if this is a raster model run
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeRasterData(iteration As Integer)

        Debug.Assert(Me.IsSpatial)

        Dim rastSclass As New ApexRaster
        Dim rastPrimaryStratum As New ApexRaster
        Dim rastSecondaryStratum As New ApexRaster    ' Secondary Stratum
        Dim rastAge As New ApexRaster
        Dim rastDem As New ApexRaster
        Dim inpRasts As InputRasters = Me.m_InputRasters
        Dim sMsg As String

        ' Now import the rasters, if they are configured in the RasterInitialCondition 
        Dim dsIC As DataSheet = Me.ResultScenario.GetDataSheet(DATASHEET_SPIC_NAME)

        Dim ics As InitialConditionsSpatial = Me.m_InitialConditionsSpatialMap.GetICS(iteration)
        If ics Is Nothing Then
            Throw New ArgumentException(ERROR_NO_APPLICABLE_INITIAL_CONDITIONS_SPATIAL_RECORDS)
        End If

        ' Load the State Class Raster
        Dim rasterFileName As String
        Dim fullFileName As String

        rasterFileName = ics.StateClassFileName

        If rasterFileName.Length > 0 Then

            If rasterFileName <> inpRasts.StateClassName Then

                fullFileName = RasterFiles.GetInputFileName(dsIC, rasterFileName, False)
                RasterFiles.LoadRasterFile(fullFileName, rastSclass, RasterDataType.dtInteger)

                inpRasts.StateClassName = rasterFileName
                ' Now lets remap the ID's in the raster to the SClass PK values
                Dim dsRemap As DataSheet = Me.Project.GetDataSheet(DATASHEET_STATECLASS_NAME)
                inpRasts.SClassCells = RasterCells.RemapRasterCells(rastSclass.IntCells, dsRemap, DATASHEET_MAPID_COLUMN_NAME)

            End If

        Else
            ' IC State Class file must be defined
            Throw New ArgumentException(ERROR_SPATIAL_FILE_NOT_DEFINED)
        End If

        ' Load the Primary Stratum Raster
        rasterFileName = ics.PrimaryStratumFileName

        If rasterFileName.Length > 0 Then

            If rasterFileName <> inpRasts.PrimaryStratumName Then

                fullFileName = RasterFiles.GetInputFileName(dsIC, rasterFileName, False)
                RasterFiles.LoadRasterFile(fullFileName, rastPrimaryStratum, RasterDataType.dtInteger)

                ' Only set the metadata the 1st time thru
                If inpRasts.PrimaryStratumName = "" Then
                    inpRasts.SetMetadata(rastPrimaryStratum)
                End If

                inpRasts.PrimaryStratumName = rasterFileName

                ' Now lets remap the ID's in the raster to the Stratum PK values
                Dim dsRemap As DataSheet = Me.Project.GetDataSheet(DATASHEET_STRATA_NAME)
                inpRasts.StratumCells = RasterCells.RemapRasterCells(rastPrimaryStratum.IntCells, dsRemap, DATASHEET_MAPID_COLUMN_NAME)

                ' See if the Primary Stratum has a Projection associated with it
                If rastPrimaryStratum.ProjectionString = "" Then
                    sMsg = String.Format(CultureInfo.CurrentCulture, STATUS_SPATIAL_FILE_MISSING_PROJECTION_WARNING, fullFileName)
                    Me.AddStatusRecord(StatusRecordType.Information, sMsg)
                End If

            End If

        Else
            ' IC Stratum file must be defined
            Throw New ArgumentException(ERROR_SPATIAL_FILE_NOT_DEFINED)
        End If

        ' Load the Secondary Stratum Raster
        rasterFileName = ics.SecondaryStratumFileName

        If rasterFileName.Length > 0 Then

            If rasterFileName <> inpRasts.SecondaryStratumName Then

                fullFileName = RasterFiles.GetInputFileName(dsIC, rasterFileName, False)
                RasterFiles.LoadRasterFile(fullFileName, rastSecondaryStratum, RasterDataType.dtInteger)
                inpRasts.SecondaryStratumName = rasterFileName
                ' Now lets remap the ID's in the raster to the Secondary Stratum PK values
                Dim dsRemap As DataSheet = Me.Project.GetDataSheet(DATASHEET_SECONDARY_STRATA_NAME)
                inpRasts.SecondaryStratumCells = RasterCells.RemapRasterCells(rastSecondaryStratum.IntCells, dsRemap, DATASHEET_MAPID_COLUMN_NAME)

            End If

        Else
            ' IC Secondary Stratum file does not have to be defined
            inpRasts.SecondaryStratumName = ""
        End If

        ' Load the Age Raster
        rasterFileName = ics.AgeFileName

        If rasterFileName.Length > 0 Then

            If rasterFileName <> inpRasts.AgeName Then

                fullFileName = RasterFiles.GetInputFileName(dsIC, rasterFileName, False)
                RasterFiles.LoadRasterFile(fullFileName, rastAge, RasterDataType.dtInteger)
                inpRasts.AgeName = rasterFileName
                inpRasts.AgeCells = rastAge.IntCells

            End If

        Else
            inpRasts.AgeName = ""
        End If

        ' Load the Digital Elevation Model (DEM) Raster
        dsIC = Me.ResultScenario.GetDataSheet(DATASHEET_DIGITAL_ELEVATION_MODEL_NAME)
        Dim drRIS As DataRow = dsIC.GetDataRow()

        If Not drRIS Is Nothing Then

            rasterFileName = drRIS(DATASHEET_DIGITAL_ELEVATION_MODEL_FILE_NAME_COLUMN_NAME).ToString()

            If rasterFileName.Length > 0 Then

                If rasterFileName <> inpRasts.DemName Then

                    fullFileName = RasterFiles.GetInputFileName(dsIC, rasterFileName, False)
                    RasterFiles.LoadRasterFile(fullFileName, rastDem, RasterDataType.dtDouble)

                    inpRasts.DemName = rasterFileName
                    inpRasts.DemCells = rastDem.DblCells

                End If

            Else
                inpRasts.DemName = ""
            End If

        End If

        ' Compare the rasters to make sure meta data matches. Note that we might not have loaded a raster because one of the same name already loaded for a previous iteration.
        Dim cmpResult As InputRasters.CompareMetadataResult
        Dim cmpMsg As String = ""

        ' Primary Stratum
        If rastPrimaryStratum.NumberCells > 0 Then
            cmpResult = inpRasts.CompareMetadata(rastPrimaryStratum, cmpMsg)
            If cmpResult = InputRasters.CompareMetadataResult.ImportantDifferences Then
                sMsg = String.Format(CultureInfo.CurrentCulture, ERROR_SPATIAL_FILE_MISMATCHED_METADATA, inpRasts.PrimaryStratumName, cmpMsg)
                Throw New STSimException(sMsg)
            ElseIf cmpResult = InputRasters.CompareMetadataResult.UnimportantDifferences Then
                sMsg = String.Format(CultureInfo.CurrentCulture, STATUS_SPATIAL_FILE_MISMATCHED_METADATA_INFO, inpRasts.PrimaryStratumName, cmpMsg)
                Me.AddStatusRecord(StatusRecordType.Information, sMsg)
            End If
        End If

        ' SClass is mandatory
        If rastSclass.NumberCells > 0 Then
            cmpResult = inpRasts.CompareMetadata(rastSclass, cmpMsg)
            If cmpResult = InputRasters.CompareMetadataResult.ImportantDifferences Then
                sMsg = String.Format(CultureInfo.CurrentCulture, ERROR_SPATIAL_FILE_MISMATCHED_METADATA, inpRasts.StateClassName, cmpMsg)
                Throw New STSimException(sMsg)
            ElseIf cmpResult = InputRasters.CompareMetadataResult.UnimportantDifferences Then
                sMsg = String.Format(CultureInfo.CurrentCulture, STATUS_SPATIAL_FILE_MISMATCHED_METADATA_INFO, inpRasts.StateClassName, cmpMsg)
                Me.AddStatusRecord(StatusRecordType.Information, sMsg)
            End If
        End If

        ' Age
        If rastAge.NumberCells > 0 Then
            cmpResult = inpRasts.CompareMetadata(rastAge, cmpMsg)
            If cmpResult = InputRasters.CompareMetadataResult.ImportantDifferences Then
                sMsg = String.Format(CultureInfo.CurrentCulture, ERROR_SPATIAL_FILE_MISMATCHED_METADATA, inpRasts.AgeName, cmpMsg)
                Throw New STSimException(sMsg)
            ElseIf cmpResult = InputRasters.CompareMetadataResult.UnimportantDifferences Then
                sMsg = String.Format(CultureInfo.CurrentCulture, STATUS_SPATIAL_FILE_MISMATCHED_METADATA_INFO, inpRasts.AgeName, cmpMsg)
                Me.AddStatusRecord(StatusRecordType.Information, sMsg)
            End If
        End If

        'Secondary Stratum
        If rastSecondaryStratum.NumberCells > 0 Then
            cmpResult = inpRasts.CompareMetadata(rastSecondaryStratum, cmpMsg)
            If cmpResult = InputRasters.CompareMetadataResult.ImportantDifferences Then
                sMsg = String.Format(CultureInfo.CurrentCulture, ERROR_SPATIAL_FILE_MISMATCHED_METADATA, inpRasts.SecondaryStratumName, cmpMsg)
                Throw New STSimException(sMsg)
            ElseIf cmpResult = InputRasters.CompareMetadataResult.UnimportantDifferences Then
                sMsg = String.Format(CultureInfo.CurrentCulture, STATUS_SPATIAL_FILE_MISMATCHED_METADATA_INFO, inpRasts.SecondaryStratumName, cmpMsg)
                Me.AddStatusRecord(StatusRecordType.Information, sMsg)
            End If
        End If

        'DEM 
        If rastDem.NumberCells > 0 Then
            cmpResult = inpRasts.CompareMetadata(rastDem, cmpMsg)
            If cmpResult = InputRasters.CompareMetadataResult.ImportantDifferences Then
                sMsg = String.Format(CultureInfo.CurrentCulture, ERROR_SPATIAL_FILE_MISMATCHED_METADATA, inpRasts.DemName, cmpMsg)
                Throw New STSimException(sMsg)
            ElseIf cmpResult = InputRasters.CompareMetadataResult.UnimportantDifferences Then
                sMsg = String.Format(CultureInfo.CurrentCulture, STATUS_SPATIAL_FILE_MISMATCHED_METADATA_INFO, inpRasts.DemName, cmpMsg)
                Me.AddStatusRecord(StatusRecordType.Information, sMsg)
            End If
        End If

    End Sub

    ''' <summary>
    ''' Initializes the Annual Average Transition Probability Maps
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeAnnualAvgTransitionProbMaps()

        Debug.Assert(Me.IsSpatial)
        Debug.Assert(Me.MinimumTimestep > 0)

        If Not Me.m_CreateRasterAATPOutput Then
            Exit Sub
        End If

        ' Loop thru transition groups. 
        For Each tg As TransitionGroup In Me.m_TransitionGroups

            'Make sure Primary
            If tg.PrimaryTransitionTypes.Count = 0 Then
                Continue For
            End If

            Dim dicTgAATP As New Dictionary(Of Integer, Double())

            ' Loop thru timesteps
            For timestep = Me.MinimumTimestep To Me.MaximumTimestep

                ' Create a dictionary for this transtion group
                ' Create a aatp array object on Maximum Timestep and intervals of user spec'd freq.

                If (timestep = Me.MaximumTimestep) Or ((timestep - Me.TimestepZero) Mod Me.m_RasterAATPTimesteps) = 0 Then

                    Dim aatp As Double()
                    ReDim aatp(Me.m_InputRasters.NumberCells - 1)

                    ' Initialize cells values
                    For i = 0 To Me.m_InputRasters.NumberCells - 1

                        If Not Me.Cells.Contains(i) Then
                            aatp(i) = ApexRaster.DEFAULT_NO_DATA_VALUE
                        Else
                            aatp(i) = 0
                        End If

                    Next

                    dicTgAATP.Add(timestep, aatp)

                End If

            Next

            Me.m_AnnualAvgTransitionProbMap.Add(tg.TransitionGroupId, dicTgAATP)

        Next

    End Sub

End Class
