'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Common
Imports SyncroSim.StochasticTime

Partial Class STSimTransformer

    Private Function GetTransitionMultiplierType(ByVal id As Nullable(Of Integer)) As TransitionMultiplierType

        For Each t As TransitionMultiplierType In Me.m_TransitionMultiplierTypes

            If (Nullable.Compare(t.TransitionMultiplierTypeId, id) = 0) Then
                Return t
            End If

        Next

        Debug.Assert(False)
        Return Nothing

    End Function

    Private Function GetTransitionTargetMultiplier(
        ByVal transitionGroupId As Integer,
        ByVal stratumId As Integer,
        ByVal secondaryStratumId As Nullable(Of Integer),
        ByVal tertiaryStratumId As Nullable(Of Integer),
        ByVal iteration As Integer,
        ByVal timestep As Integer) As Double

        Dim t As TransitionTarget = Me.m_TransitionTargetMap.GetTransitionTarget(
            transitionGroupId,
            stratumId,
            secondaryStratumId,
            tertiaryStratumId,
            iteration,
            timestep)

        If (t Is Nothing) Then
            Return 1.0
        Else
            Return t.Multiplier
        End If

    End Function

    Private Function GetTransitionAttributeTargetMultiplier(
        ByVal transitionAttributeTypeId As Integer,
        ByVal stratumId As Integer,
        ByVal secondaryStratumId As Nullable(Of Integer),
        ByVal tertiaryStratumId As Nullable(Of Integer),
        ByVal iteration As Integer,
        ByVal timestep As Integer) As Double

        Dim t As TransitionAttributeTarget = Me.m_TransitionAttributeTargetMap.GetAttributeTarget(
            transitionAttributeTypeId,
            stratumId,
            secondaryStratumId,
            tertiaryStratumId,
            iteration,
            timestep)

        If (t Is Nothing) Then
            Return 1.0
        Else
            Return t.Multiplier
        End If

    End Function

    Private Function GetTransitionAdjacencyMultiplier(
        ByVal transitionGroupId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal stratumId As Integer,
        ByVal secondaryStratumId As Nullable(Of Integer),
        ByVal tertiaryStratumId As Nullable(Of Integer),
        ByVal cell As Cell) As Double

        Dim attrvalue As Nullable(Of Double) = Me.GetNeighborhoodAttributeValue(cell, transitionGroupId)

        If attrvalue Is Nothing Then
            attrvalue = 0.0
        End If

        Dim multiplier As Double = Me.m_TransitionAdjacencyMultiplierMap.GetAdjacencyMultiplier(
            transitionGroupId, stratumId, secondaryStratumId, tertiaryStratumId, iteration, timestep, CDbl(attrvalue))

        Return multiplier

    End Function

    Private Function GetTransitionMultiplier(
        ByVal transitionTypeId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal stratumId As Integer,
        ByVal secondaryStratumId As Nullable(Of Integer),
        ByVal tertiaryStratumId As Nullable(Of Integer),
        ByVal stateClassId As Integer) As Double

        If (Me.m_TransitionMultiplierValues.Count = 0) Then

#If DEBUG Then
            For Each tmt As TransitionMultiplierType In Me.m_TransitionMultiplierTypes
                Debug.Assert(tmt.TransitionMultiplierValueMap Is Nothing)
            Next
#End If

            Return 1.0

        End If

        Debug.Assert(Me.m_TransitionMultiplierTypes.Count > 0)

        Dim Product As Double = 1.0
        Dim tt As TransitionType = Me.m_TransitionTypes(transitionTypeId)

        For Each tmt As TransitionMultiplierType In Me.m_TransitionMultiplierTypes

            If (tmt.TransitionMultiplierValueMap IsNot Nothing) Then

                For Each tg As TransitionGroup In tt.TransitionGroups

                    Dim v As TransitionMultiplierValue = tmt.TransitionMultiplierValueMap.GetTransitionMultiplier(
                        tg.TransitionGroupId, stratumId, secondaryStratumId, tertiaryStratumId, stateClassId, iteration, timestep)

                    If (v IsNot Nothing) Then
                        Product *= v.CurrentValue.Value
                    End If

                Next

            End If

        Next

        Return Product

    End Function

    Private Function GetTransitionSpatialMultiplier(
        ByVal cellId As Integer,
        ByVal transitionTypeId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer) As Double

        If (Me.m_TransitionSpatialMultipliers.Count = 0) Then

#If DEBUG Then
            For Each tmt As TransitionMultiplierType In Me.m_TransitionMultiplierTypes
                Debug.Assert(tmt.TransitionSpatialMultiplierMap Is Nothing)
            Next
#End If

            Return 1.0

        End If

        Debug.Assert(Me.m_TransitionMultiplierTypes.Count > 0)

        Dim Product As Double = 1.0
        Dim tt As TransitionType = Me.m_TransitionTypes(transitionTypeId)

        For Each tmt As TransitionMultiplierType In Me.m_TransitionMultiplierTypes

            If (tmt.TransitionSpatialMultiplierMap IsNot Nothing) Then

                For Each tg As TransitionGroup In tt.TransitionGroups

                    Dim tsmr As TransitionSpatialMultiplier =
                        tmt.TransitionSpatialMultiplierMap.GetMultiplierRaster(tg.TransitionGroupId, iteration, timestep)

                    If (tsmr IsNot Nothing) Then

                        'Using a single instance of each uniquely named TSM raster

                        Debug.Assert(Me.m_TransitionSpatialMultiplierRasters.ContainsKey(tsmr.FileName))

                        If (Me.m_TransitionSpatialMultiplierRasters.ContainsKey(tsmr.FileName)) Then

                            Dim rastMult As StochasticTimeRaster = Me.m_TransitionSpatialMultiplierRasters(tsmr.FileName)
                            Dim spatialMult As Double = rastMult.DblCells(cellId)

                            'Test for NODATA_VALUE

                            If spatialMult < 0.0 Or MathUtils.CompareDoublesEqual(spatialMult, rastMult.NoDataValue, Double.Epsilon) Then
                                spatialMult = 1.0
                            End If

                            Product *= spatialMult

                        End If

                    End If

                Next

            End If

        Next

        Return Product

    End Function

    Private Function GetTransitionSpatialInitiationMultiplier(
        ByVal cellId As Integer,
        ByVal transitionGroupId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer) As Double

        If (Me.m_TransitionSpatialInitiationMultipliers.Count = 0) Then
            Return 1.0
        End If

        Debug.Assert(Me.m_TransitionMultiplierTypes.Count > 0)

        Dim Product As Double = 1.0

        For Each tmt As TransitionMultiplierType In Me.m_TransitionMultiplierTypes

            If (tmt.TransitionSpatialInitiationMultiplierMap IsNot Nothing) Then

                Dim tsmr As TransitionSpatialInitiationMultiplier =
                    tmt.TransitionSpatialInitiationMultiplierMap.GetMultiplierRaster(transitionGroupId, iteration, timestep)

                If (tsmr IsNot Nothing) Then

                    'Using a single instance of each uniquely named TSIM raster

                    Debug.Assert(Me.m_TransitionSpatialInitiationMultiplierRasters.ContainsKey(tsmr.FileName))

                    If (Me.m_TransitionSpatialInitiationMultiplierRasters.ContainsKey(tsmr.FileName)) Then

                        Dim rastMult As StochasticTimeRaster = Me.m_TransitionSpatialInitiationMultiplierRasters(tsmr.FileName)
                        Dim spatialMult As Double = rastMult.DblCells(cellId)

                        'Test for NODATA_VALUE

                        If spatialMult < 0.0 Or MathUtils.CompareDoublesEqual(spatialMult, rastMult.NoDataValue, Double.Epsilon) Then
                            spatialMult = 1.0
                        End If

                        Product *= spatialMult

                    End If

                End If

            End If

        Next

        Return Product

    End Function

    Private Function GetExternalSpatialMultiplier(
        ByVal cell As Cell,
        ByVal timestep As Integer,
        ByVal transitionGroupId As Integer) As Double

        Dim args As New ExternalMultipliersEventArgs(cell.CellId, timestep, transitionGroupId)
        RaiseEvent ExternalMultipliersRequested(Me, args)

        Return args.Multiplier

    End Function

    ''' <summary>
    ''' Modifies a transition multiplier by any transition attribute target multipliers that apply to that transition type
    ''' </summary>
    ''' <param name="multiplier"></param>
    ''' <param name="tt"></param>
    ''' <param name="simulationCell"></param>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ModifyMultiplierForTransitionAttributeTarget(
        ByVal multiplier As Double,
        ByVal tt As TransitionType,
        ByVal simulationCell As Cell,
        ByVal iteration As Integer,
        ByVal timestep As Integer) As Double

        For Each tatId As Integer In Me.m_TransitionAttributeTypesWithTarget.Keys

            For Each tg As TransitionGroup In tt.TransitionGroups

                Dim AttrValue As Nullable(Of Double) =
                    Me.m_TransitionAttributeValueMap.GetAttributeValue(
                        tatId,
                        tg.TransitionGroupId,
                        simulationCell.StratumId,
                        simulationCell.SecondaryStratumId,
                        simulationCell.TertiaryStratumId,
                        simulationCell.StateClassId,
                        iteration,
                        timestep,
                        simulationCell.Age)

                If (AttrValue.HasValue) Then

                    If (AttrValue.Value > 0.0) Then

                        multiplier *= Me.GetTransitionAttributeTargetMultiplier(
                            tatId, simulationCell.StratumId, simulationCell.SecondaryStratumId, simulationCell.TertiaryStratumId,
                            iteration, timestep)

                        Exit For

                    End If

                End If

            Next

        Next

        Return multiplier

    End Function

    Private Sub ResetTranstionAttributeTargetMultipliers(
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal remainingTransitionGroups As Dictionary(Of Integer, TransitionGroup),
        ByVal tatMap As MultiLevelKeyMap1(Of Dictionary(Of Integer, TransitionAttributeTarget)),
        ByVal currentTg As TransitionGroup)

        If (Me.m_TransitionAttributeTargets.Count = 0) Then
            Return
        End If

        For Each tat As TransitionAttributeTarget In Me.m_TransitionAttributeTargets

            tat.Multiplier = 1.0
            tat.ExpectedAmount = 0.0

        Next

        For Each simulationCell As Cell In Me.m_Cells

            'Only iterate over the transition attribute types that have a target associated with them in this timestep.

            For Each tatId As Integer In Me.m_TransitionAttributeTypesWithTarget.Keys

                Dim ta As TransitionAttributeType = Me.m_TransitionAttributeTypes.Item(tatId)

                Dim Target As TransitionAttributeTarget = Me.m_TransitionAttributeTargetMap.GetAttributeTarget(
                    ta.TransitionAttributeId, simulationCell.StratumId, simulationCell.SecondaryStratumId, simulationCell.TertiaryStratumId,
                    iteration, timestep)

                If (Target Is Nothing) Then
                    Continue For
                End If

                For Each tr As Transition In simulationCell.Transitions

                    Dim tt As TransitionType = Me.m_TransitionTypes(tr.TransitionTypeId)
                    Dim Found As Boolean = False

                    For Each rtg As TransitionGroup In remainingTransitionGroups.Values

                        If (tt.PrimaryTransitionGroups.Contains(rtg.TransitionGroupId)) Then
                            Found = True
                            Exit For
                        End If

                    Next

                    If (Not Found) Then
                        Continue For
                    End If

                    If (tt.TransitionGroups.Count = 0) Then
                        Continue For
                    End If

                    If ((tatMap IsNot Nothing) And (currentTg IsNot Nothing)) Then

                        If tt.TransitionGroups.Contains(currentTg.TransitionGroupId) Then

                            Dim d As Dictionary(Of Integer, TransitionAttributeTarget) = tatMap.GetItemExact(Target.StratumId)

                            If d Is Nothing Then

                                d = New Dictionary(Of Integer, TransitionAttributeTarget)
                                tatMap.AddItem(Target.StratumId, d)

                            End If

                            If Not d.ContainsKey(Target.TransitionAttributeTargetId) Then
                                d.Add(Target.TransitionAttributeTargetId, Target)
                            End If

                        End If

                    End If

                    Dim TransMult As Double = Me.GetTransitionMultiplier(
                        tr.TransitionTypeId,
                        iteration,
                        timestep,
                        simulationCell.StratumId,
                        simulationCell.SecondaryStratumId,
                        simulationCell.TertiaryStratumId,
                        simulationCell.StateClassId)

                    If (Me.IsSpatial) Then

                        TransMult *= Me.GetTransitionSpatialMultiplier(simulationCell.CellId, tr.TransitionTypeId, iteration, timestep)

                        For Each tg As TransitionGroup In tt.TransitionGroups

                            TransMult *= Me.GetTransitionAdjacencyMultiplier(
                                tg.TransitionGroupId, iteration, timestep,
                                simulationCell.StratumId, simulationCell.SecondaryStratumId, simulationCell.TertiaryStratumId,
                                simulationCell)

                            TransMult *= Me.GetExternalSpatialMultiplier(simulationCell, timestep, tg.TransitionGroupId)

                        Next

                        Debug.Assert(TransMult >= 0.0)

                    End If

                    If (TransMult = 0.0) Then
                        Continue For
                    End If

                    For Each tg As TransitionGroup In tt.TransitionGroups

                        If (Me.IsSpatial) Then

                            If (Not remainingTransitionGroups.ContainsKey(tg.TransitionGroupId)) Then
                                Continue For
                            End If

                        End If

                        Dim AttrValue As Nullable(Of Double) =
                            Me.m_TransitionAttributeValueMap.GetAttributeValue(
                                ta.TransitionAttributeId,
                                tg.TransitionGroupId,
                                simulationCell.StratumId,
                                simulationCell.SecondaryStratumId,
                                simulationCell.TertiaryStratumId,
                                simulationCell.StateClassId,
                                iteration,
                                timestep,
                                simulationCell.Age)

                        If (AttrValue.HasValue) Then

                            Dim Expectation As Double = tr.Probability * tr.Proportion * Me.m_AmountPerCell * AttrValue.Value * TransMult
                            Target.ExpectedAmount += Expectation

                            Debug.Assert(Expectation >= 0.0)
                            Debug.Assert(Target.ExpectedAmount >= 0.0)

                        End If

                    Next

                Next

            Next

        Next

        For Each tat As TransitionAttributeTarget In Me.m_TransitionAttributeTargets

            If (tat.ExpectedAmount <> 0.0) Then

                tat.Multiplier = tat.TargetRemaining / tat.ExpectedAmount
                Debug.Assert(tat.Multiplier >= 0.0)

            End If

        Next

    End Sub

    ''' <summary>
    ''' Resets the transition target multipliers for this cell, iteration, timestep and explicit transition group
    ''' </summary>
    ''' <param name="iteration">The current iteration</param>
    ''' <param name="timestep">The current timestep</param>
    ''' <param name="explicitGroup">transition group must be provided if spatial - should be null if non spatial</param>
    ''' <remarks></remarks>
    Private Sub ResetTransitionTargetMultipliers(
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal explicitGroup As TransitionGroup)

        Debug.Assert(explicitGroup IsNot Nothing)

        If (Me.m_TransitionTargets.Count = 0) Then
            Return
        End If

        For Each tt As TransitionTarget In Me.m_TransitionTargets

            tt.Multiplier = 1.0
            tt.ExpectedAmount = 0.0

        Next

        For Each simulationCell As Cell In Me.m_Cells

            For Each tr As Transition In simulationCell.Transitions

                Dim ttype As TransitionType = Me.m_TransitionTypes(tr.TransitionTypeId)

                If Me.IsSpatial Then

                    If Not ttype.PrimaryTransitionGroups.Contains(explicitGroup.TransitionGroupId) Then
                        Continue For
                    End If

                End If

                If (ttype.TransitionGroups.Count = 0) Then
                    Continue For
                End If

                Dim TransMult As Double = Me.GetTransitionMultiplier(
                    tr.TransitionTypeId,
                    iteration,
                    timestep,
                    simulationCell.StratumId,
                    simulationCell.SecondaryStratumId,
                    simulationCell.TertiaryStratumId,
                    simulationCell.StateClassId)

                If (Me.IsSpatial) Then

                    TransMult *= Me.GetTransitionSpatialMultiplier(simulationCell.CellId, tr.TransitionTypeId, iteration, timestep)

                    For Each tg As TransitionGroup In ttype.TransitionGroups

                        TransMult *= Me.GetTransitionAdjacencyMultiplier(
                            tg.TransitionGroupId, iteration, timestep,
                            simulationCell.StratumId, simulationCell.SecondaryStratumId, simulationCell.TertiaryStratumId,
                            simulationCell)

                        TransMult *= Me.GetExternalSpatialMultiplier(simulationCell, timestep, tg.TransitionGroupId)

                    Next

                    Debug.Assert(TransMult >= 0.0)

                End If

                If (TransMult = 0.0) Then
                    Continue For
                End If

                For Each tgroup As TransitionGroup In ttype.TransitionGroups

                    Dim tt As TransitionTarget = Me.m_TransitionTargetMap.GetTransitionTarget(
                        tgroup.TransitionGroupId,
                        simulationCell.StratumId,
                        simulationCell.SecondaryStratumId,
                        simulationCell.TertiaryStratumId,
                        iteration,
                        timestep)

                    If (tt IsNot Nothing) Then

                        tt.ExpectedAmount += (tr.Probability * tr.Proportion * Me.m_AmountPerCell * TransMult)
                        Debug.Assert(tt.ExpectedAmount >= 0.0)

                    End If

                Next

            Next

        Next

        For Each ttarg As TransitionTarget In Me.m_TransitionTargets

            If (ttarg.ExpectedAmount <> 0) Then

                ttarg.Multiplier = ttarg.CurrentValue.Value / ttarg.ExpectedAmount
                Debug.Assert(ttarg.Multiplier >= 0.0)

            End If

        Next

    End Sub

End Class
