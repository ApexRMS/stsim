'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Common

Partial Class STSimTransformer

    ''' <summary>
    ''' Initializes the transition spread groups
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTransitionSpreadGroups()

#If DEBUG Then

        Debug.Assert(Me.IsSpatial)
        Debug.Assert(Me.m_TransitionSpreadGroups.Count = 0)

        For Each t As TransitionGroup In Me.m_TransitionGroups
            Debug.Assert(t.TransitionSpreadCells.Count = 0)
        Next

#End If

        'Get a unique list of transition groups from the spread distribution records
        Dim dict As New Dictionary(Of Integer, TransitionSpreadDistribution)

        For Each t As TransitionSpreadDistribution In Me.m_TransitionSpreadDistributions

            If (Not dict.ContainsKey(t.TransitionGroupId)) Then
                dict.Add(t.TransitionGroupId, t)
            End If

        Next

        For Each t As TransitionSpreadDistribution In dict.Values
            Me.m_TransitionSpreadGroups.Add(Me.m_TransitionGroups(t.TransitionGroupId))
        Next

        'Associate each spread distribution with its transition spread group
        For Each t As TransitionSpreadDistribution In Me.m_TransitionSpreadDistributions

            Dim tg As TransitionGroup = Me.m_TransitionGroups(t.TransitionGroupId)
            tg.TransitionSpreadDistributionMap.AddItem(t)

        Next

        For Each tg As TransitionGroup In Me.m_TransitionSpreadGroups
            tg.TransitionSpreadDistributionMap.Normalize()
        Next

    End Sub

    ''' <summary>
    ''' Applies the transition spread for the specified iteration and timestep
    ''' </summary>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <remarks></remarks>
    Private Sub ApplyTransitionSpread(
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal rasterTransitionAttrValues As Dictionary(Of Integer, Double()),
        ByVal dictTransitionedPixels As Dictionary(Of Integer, Integer()))

        Debug.Assert(Me.IsSpatial)

        For Each SpreadGroup As TransitionGroup In Me.m_TransitionSpreadGroups

            If SpreadGroup.TransitionSpreadCells.Count > 0 Then

                Dim ContagiousCellCollection As New CellCollection

                For Each Cell As Cell In SpreadGroup.TransitionSpreadCells.Values
                    ContagiousCellCollection.Add(Cell)
                Next

                For Each ContagionCell As Cell In ContagiousCellCollection

                    Dim ApplyFuncCheckNull = Sub(c1 As Cell, c2 As Cell, tg As TransitionGroup, i As Integer, t As Integer, b As Boolean, d As CardinalDirection)
                                                 If (c2 IsNot Nothing) Then
                                                     Me.ApplyTransitionSpread(c1, c2, tg, i, t, b, d, rasterTransitionAttrValues, dictTransitionedPixels)
                                                 End If
                                             End Sub

                    ApplyFuncCheckNull(ContagionCell, Me.GetCellNorth(ContagionCell), SpreadGroup, iteration, timestep, False, CardinalDirection.N)
                    ApplyFuncCheckNull(ContagionCell, Me.GetCellNortheast(ContagionCell), SpreadGroup, iteration, timestep, True, CardinalDirection.NE)
                    ApplyFuncCheckNull(ContagionCell, Me.GetCellEast(ContagionCell), SpreadGroup, iteration, timestep, False, CardinalDirection.E)
                    ApplyFuncCheckNull(ContagionCell, Me.GetCellSoutheast(ContagionCell), SpreadGroup, iteration, timestep, True, CardinalDirection.SE)
                    ApplyFuncCheckNull(ContagionCell, Me.GetCellSouth(ContagionCell), SpreadGroup, iteration, timestep, False, CardinalDirection.S)
                    ApplyFuncCheckNull(ContagionCell, Me.GetCellSouthwest(ContagionCell), SpreadGroup, iteration, timestep, True, CardinalDirection.SW)
                    ApplyFuncCheckNull(ContagionCell, Me.GetCellWest(ContagionCell), SpreadGroup, iteration, timestep, False, CardinalDirection.W)
                    ApplyFuncCheckNull(ContagionCell, Me.GetCellNorthwest(ContagionCell), SpreadGroup, iteration, timestep, True, CardinalDirection.NW)

                Next

            End If
        Next

    End Sub

    Private Sub ApplyTransitionSpread(
        ByVal contagionCell As Cell,
        ByVal neighboringCell As Cell,
        ByVal spreadGroup As TransitionGroup,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal isDiagonal As Boolean,
        ByVal direction As CardinalDirection,
        ByVal rasterTransitionAttrValues As Dictionary(Of Integer, Double()),
        ByVal dictTransitionedPixels As Dictionary(Of Integer, Integer()))

        Debug.Assert(Me.IsSpatial)

        'Get the cell probability.  If it is less than or equal to zero we don't need to continue
        Dim CellProbability As Double = Me.SpatialCalculateCellProbability(neighboringCell, spreadGroup.TransitionGroupId, iteration, timestep)

        If (CellProbability <= 0.0) Then
            Return
        End If

        'Get the transition pathway.  If there isn't one we don't need to continue
        Dim tr As Transition = Me.SelectSpatialTransitionPathway(neighboringCell, spreadGroup.TransitionGroupId, iteration, timestep)

        If (tr Is Nothing) Then
            Return
        End If

        'Prepare a TST value with a default of 1.  If we can find a TST group for the contagion cell's stratum and transition type, and 
        'the the contagion cells TST values contains that group, then use that TST value.

        Dim tstvalue As Integer = 1

        Dim tstgroup As TstTransitionGroup =
            Me.m_TstTransitionGroupMap.GetGroup(
                tr.TransitionTypeId,
                contagionCell.StratumId,
                contagionCell.SecondaryStratumId)

        If (tstgroup IsNot Nothing) Then

            If (contagionCell.TstValues.Contains(tstgroup.GroupId)) Then
                tstvalue = contagionCell.TstValues(tstgroup.GroupId).TstValue
            End If

        End If

        If (tstvalue > 0) Then

            Dim MinThreshold As Double
            Dim MaxThreshold As Double
            Dim SpreadDistance As Double = Me.CalculateSpreadDistance(contagionCell, tstvalue, spreadGroup, iteration, timestep)
            Dim NeighborDistance As Double = Me.GetNeighborCellDistance(direction)
            Dim Slope As Double = GetSlope(contagionCell, neighboringCell, NeighborDistance)

            SpreadDistance *= Me.m_TransitionDirectionMultiplierMap.GetDirectionMultiplier(
                spreadGroup.TransitionGroupId, contagionCell.StratumId, contagionCell.SecondaryStratumId, direction, iteration, timestep)

            SpreadDistance *= Me.m_TransitionSlopeMultiplierMap.GetSlopeMultiplier(
                spreadGroup.TransitionGroupId, contagionCell.StratumId, contagionCell.SecondaryStratumId, iteration, timestep, Slope)

            If (isDiagonal) Then
                MinThreshold = Me.m_InputRasters.GetCellSizeDiagonalMeters / 2
                MaxThreshold = Me.m_InputRasters.GetCellSizeDiagonalMeters * 1.5
            Else
                MinThreshold = Me.m_InputRasters.GetCellSizeMeters / 2
                MaxThreshold = Me.m_InputRasters.GetCellSizeMeters * 1.5
            End If

            If (SpreadDistance >= MinThreshold And SpreadDistance <= MaxThreshold) Then

                Me.OnSummaryTransitionOutput(neighboringCell, tr, iteration, timestep)
                Me.OnSummaryTransitionByStateClassOutput(neighboringCell, tr, iteration, timestep)

                Me.ChangeCellForProbabilisticTransition(neighboringCell, tr, iteration, timestep, rasterTransitionAttrValues)
                Me.UpdateTransitionedPixels(neighboringCell, tr.TransitionTypeId, dictTransitionedPixels(spreadGroup.TransitionGroupId))
                Me.FillProbabilisticTransitionsForCell(neighboringCell, iteration, timestep)

            ElseIf (SpreadDistance > MaxThreshold) Then

                Dim randdirection As Integer = Me.m_RandomGenerator.GetNextInteger(0, 360)
                Dim DistantCell As Cell = GetCellByDistanceAndDirection(contagionCell, randdirection, SpreadDistance)

                If (DistantCell IsNot Nothing) Then

                    Dim DistantTransition As Transition = Me.SelectSpatialTransitionPathway(DistantCell, spreadGroup.TransitionGroupId, iteration, timestep)

                    If (DistantTransition IsNot Nothing) Then

                        Me.OnSummaryTransitionOutput(DistantCell, DistantTransition, iteration, timestep)
                        Me.OnSummaryTransitionByStateClassOutput(DistantCell, DistantTransition, iteration, timestep)

                        Me.ChangeCellForProbabilisticTransition(DistantCell, DistantTransition, iteration, timestep, rasterTransitionAttrValues)
                        Me.UpdateTransitionedPixels(DistantCell, DistantTransition.TransitionTypeId, dictTransitionedPixels(spreadGroup.TransitionGroupId))
                        Me.FillProbabilisticTransitionsForCell(DistantCell, iteration, timestep)

                    End If

                End If

            End If

        End If

    End Sub

    ''' <summary>
    ''' Calculates the spread distance for the specified criteria
    ''' </summary>
    ''' <param name="contagionCell"></param>
    ''' <param name="tstValue"></param>
    ''' <param name="spreadGroup"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CalculateSpreadDistance(
        ByVal contagionCell As Cell,
        ByVal tstValue As Integer,
        ByVal spreadGroup As TransitionGroup,
        ByVal iteration As Integer,
        ByVal timestep As Integer) As Double

        Debug.Assert(tstValue > 0)
        Debug.Assert(Me.IsSpatial)

        Dim SpreadDistance As Double = 0.0

        For tstval As Integer = 1 To tstValue

            Dim rand As Double = Me.m_RandomGenerator.GetNextDouble
            Dim PreviousCumulativeProportion As Double = 0.0
            Dim CumulativeProportion As Double = 0.0

            Dim lst As List(Of TransitionSpreadDistribution) =
                spreadGroup.TransitionSpreadDistributionMap.GetDistributionList(
                    contagionCell.StratumId, contagionCell.StateClassId, iteration, timestep)

            If (lst Is Nothing) Then
                Continue For
            End If

            For Each tsd As TransitionSpreadDistribution In lst

                PreviousCumulativeProportion = CumulativeProportion
                CumulativeProportion += tsd.Proportion

                If (CumulativeProportion > rand) Then

                    Dim diff1 As Double = (rand - PreviousCumulativeProportion)

                    If (diff1 = 0.0) Then
                        SpreadDistance += tsd.MinimumDistance
                    Else

                        Dim diff2 As Double = (CumulativeProportion - PreviousCumulativeProportion)
                        Debug.Assert(diff2 >= diff1)

                        If (diff1 = diff2) Then
                            SpreadDistance += tsd.MaximumDistance
                        Else

                            Dim diff3 As Double = (tsd.MaximumDistance - tsd.MinimumDistance)
                            SpreadDistance += (tsd.MinimumDistance + ((diff1 / diff2) * diff3))

                        End If

                    End If

                    Exit For

                End If

            Next

        Next

        Return SpreadDistance

    End Function

    ''' <summary>
    ''' Updates the transition spread group membership for the specified cell
    ''' </summary>
    ''' <param name="cell"></param>
    ''' <remarks></remarks>
    Private Sub UpdateTransitionsSpreadGroupMembership(ByVal cell As Cell, ByVal iteration As Integer, ByVal timestep As Integer)

        Debug.Assert(Me.IsSpatial)

        For Each t As TransitionGroup In Me.m_TransitionSpreadGroups

            If (t.TransitionSpreadDistributionMap.GetDistributionList(
                cell.StratumId, cell.StateClassId, iteration, timestep) IsNot Nothing) Then

                If (Not t.TransitionSpreadCells.ContainsKey(cell.CellId)) Then
                    t.TransitionSpreadCells.Add(cell.CellId, cell)
                End If

            Else

                If (t.TransitionSpreadCells.ContainsKey(cell.CellId)) Then
                    t.TransitionSpreadCells.Remove(cell.CellId)
                End If

            End If

        Next

    End Sub

    ''' <summary>
    ''' Resets the transition spread group cells
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ResetTransitionSpreadGroupCells()

        ShuffleUtilities.ShuffleList(Me.m_TransitionSpreadGroups, Me.m_RandomGenerator.Random())

        For Each t As TransitionGroup In Me.m_TransitionSpreadGroups
            t.TransitionSpreadCells.Clear()
        Next

        For Each c As Cell In Me.m_Cells

            For Each t As TransitionGroup In Me.m_TransitionSpreadGroups

                If (t.TransitionSpreadDistributionMap.HasDistributionRecords(c.StratumId, c.StateClassId)) Then
                    t.TransitionSpreadCells.Add(c.CellId, c)
                End If

            Next

        Next

    End Sub

End Class
