'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Partial Class STSimTransformer

    ''' <summary>
    ''' Selects a patch initiation cell
    ''' </summary>
    ''' <param name="transitionGroup"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectPatchInitiationCell(ByVal transitionGroup As TransitionGroup) As Cell

        Debug.Assert(Me.IsSpatial)

        Dim pp As PatchPrioritization = transitionGroup.PatchPrioritization
        Dim patches As List(Of TransitionPatch) = pp.TransitionPatches

        If (patches.Count = 0) Then
            Return Nothing
        End If

        Dim Patch As TransitionPatch = patches(0)

        If (pp.PatchPrioritizationType = PatchPrioritizationType.Largest Or
            pp.PatchPrioritizationType = PatchPrioritizationType.LargestEdgesOnly) Then

            Patch = patches(patches.Count - 1)

        End If

        If (pp.PatchPrioritizationType = PatchPrioritizationType.SmallestEdgesOnly Or
            pp.PatchPrioritizationType = PatchPrioritizationType.LargestEdgesOnly) Then

            If (Patch.EdgeCells.ContainsKey(Patch.SeedCell.CellId)) Then
                Return Patch.SeedCell
            Else
                Return Patch.EdgeCells.Values.ElementAt(0)
            End If

        Else

            If (Patch.AllCells.ContainsKey(Patch.SeedCell.CellId)) Then
                Return Patch.SeedCell
            Else
                Return Patch.AllCells.Values.ElementAt(0)
            End If

        End If

    End Function

    ''' <summary>
    ''' Assigns patch prioritizations based on the current iteration and timestep
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssignPatchPrioritizations(ByVal iteration As Integer, ByVal timestep As Integer)

        If (Not Me.IsSpatial) Then
            Return
        End If

        For Each tg As TransitionGroup In Me.m_TransitionGroups

            tg.PatchPrioritization = Nothing

            Dim tpp As TransitionPatchPrioritization =
                Me.m_TransitionPatchPrioritizationMap.GetPatchPrioritization(tg.TransitionGroupId, iteration, timestep)

            If (tpp IsNot Nothing) Then
                tg.PatchPrioritization = Me.m_PatchPrioritizations(tpp.PatchPrioritizationId)
            End If

        Next

    End Sub

    ''' <summary>
    ''' Determines if the specified cell is a edge cell in the specified patch
    ''' </summary>
    ''' <param name="cell"></param>
    ''' <param name="patch"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsPatchEdgeCell(ByVal cell As Cell, ByVal patch As TransitionPatch) As Boolean

        If (Not CellInPatch(Me.GetCellNorth(cell), patch)) Then
            Return True
        End If

        If (Not CellInPatch(Me.GetCellNortheast(cell), patch)) Then
            Return True
        End If

        If (Not CellInPatch(Me.GetCellEast(cell), patch)) Then
            Return True
        End If

        If (Not CellInPatch(Me.GetCellSoutheast(cell), patch)) Then
            Return True
        End If

        If (Not CellInPatch(Me.GetCellSouth(cell), patch)) Then
            Return True
        End If

        If (Not CellInPatch(Me.GetCellSouthwest(cell), patch)) Then
            Return True
        End If

        If (Not CellInPatch(Me.GetCellWest(cell), patch)) Then
            Return True
        End If

        If (Not CellInPatch(Me.GetCellNorthwest(cell), patch)) Then
            Return True
        End If

        Return False

    End Function

    ''' <summary>
    ''' Determines if the specified cell is part of the specified patch
    ''' </summary>
    ''' <param name="cell"></param>
    ''' <param name="patch"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function CellInPatch(ByVal cell As Cell, ByVal patch As TransitionPatch) As Boolean

        If (cell Is Nothing) Then
            Return False
        End If

        If (patch.AllCells.ContainsKey(cell.CellId)) Then
            Return True
        End If

        Return False

    End Function

    ''' <summary>
    ''' Updates the patch membership for the specified transition group and cell
    ''' </summary>
    ''' <param name="transitionGroupId"></param>
    ''' <param name="cell"></param>
    ''' <remarks>
    ''' This function will also remove a patch if its cell collection becomes empty.
    ''' </remarks>
    Private Sub UpdateCellPatchMembership(ByVal transitionGroupId As Integer, ByVal cell As Cell)

        Dim TransitionGroup As TransitionGroup = Me.m_TransitionGroups(transitionGroupId)

        If (TransitionGroup.PatchPrioritization IsNot Nothing) Then

            Dim Patch As TransitionPatch = Nothing

            If (TransitionGroup.PatchPrioritization.PatchPrioritizationType = PatchPrioritizationType.Smallest Or
                TransitionGroup.PatchPrioritization.PatchPrioritizationType = PatchPrioritizationType.SmallestEdgesOnly) Then

                Patch = TransitionGroup.PatchPrioritization.TransitionPatches.First

            Else
                Patch = TransitionGroup.PatchPrioritization.TransitionPatches.Last
            End If

            If (Patch.AllCells.ContainsKey(cell.CellId)) Then
                Patch.AllCells.Remove(cell.CellId)
            End If

            If (Patch.EdgeCells.ContainsKey(cell.CellId)) Then
                Patch.EdgeCells.Remove(cell.CellId)
            End If

            If (TransitionGroup.PatchPrioritization.PatchPrioritizationType = PatchPrioritizationType.LargestEdgesOnly Or
                TransitionGroup.PatchPrioritization.PatchPrioritizationType = PatchPrioritizationType.SmallestEdgesOnly) Then

                If (Patch.EdgeCells.Count = 0) Then
                    TransitionGroup.PatchPrioritization.TransitionPatches.Remove(Patch)
                End If

            ElseIf (Patch.AllCells.Count = 0) Then

                Debug.Assert(Patch.EdgeCells.Count = 0)
                TransitionGroup.PatchPrioritization.TransitionPatches.Remove(Patch)

            End If

        End If

    End Sub

    ''' <summary>
    ''' Clears the transition patches for the specified transition group
    ''' </summary>
    ''' <param name="transitionGroup"></param>
    ''' <remarks></remarks>
    Private Sub ClearTransitionPatches(ByVal transitionGroup As TransitionGroup)

        Debug.Assert(Me.IsSpatial)

        If (transitionGroup.PatchPrioritization IsNot Nothing) Then
            transitionGroup.PatchPrioritization.TransitionPatches.Clear()
        End If

    End Sub

    ''' <summary>
    ''' Grows a transition patch
    ''' </summary>
    ''' <param name="transitionedCells"></param>
    ''' <param name="patchCells"></param>
    ''' <param name="initiationCell"></param>
    ''' <param name="patch"></param>
    ''' <param name="transitionGroup"></param>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <remarks></remarks>
    Private Sub GrowTransitionPatch(
        ByVal transitionedCells As Dictionary(Of Integer, Cell),
        ByVal patchCells As Dictionary(Of Integer, Cell),
        ByVal initiationCell As Cell,
        ByVal patch As TransitionPatch,
        ByVal transitionGroup As TransitionGroup,
        ByVal iteration As Integer,
        ByVal timestep As Integer)

        Dim PatchSize As Double = 0.0
        Dim PatchQueue As New Queue(Of Cell)
        Dim QueuedCells As New Dictionary(Of Integer, Cell)

        PatchQueue.Enqueue(initiationCell)
        QueuedCells.Add(initiationCell.CellId, initiationCell)

        While (PatchQueue.Count > 0)

            Dim CurrentCell As Cell = PatchQueue.Dequeue()

            Debug.Assert(Not patchCells.ContainsKey(CurrentCell.CellId))
            Debug.Assert(Not patch.AllCells.ContainsKey(CurrentCell.CellId))

            patchCells.Add(CurrentCell.CellId, CurrentCell)
            patch.AllCells.Add(CurrentCell.CellId, CurrentCell)

            PatchSize += Me.m_AmountPerCell

            Me.AddNeighboringPatchCell(transitionedCells, patchCells, QueuedCells, PatchQueue, Me.GetCellNorth(CurrentCell), transitionGroup, iteration, timestep)
            Me.AddNeighboringPatchCell(transitionedCells, patchCells, QueuedCells, PatchQueue, Me.GetCellNortheast(CurrentCell), transitionGroup, iteration, timestep)
            Me.AddNeighboringPatchCell(transitionedCells, patchCells, QueuedCells, PatchQueue, Me.GetCellEast(CurrentCell), transitionGroup, iteration, timestep)
            Me.AddNeighboringPatchCell(transitionedCells, patchCells, QueuedCells, PatchQueue, Me.GetCellSoutheast(CurrentCell), transitionGroup, iteration, timestep)
            Me.AddNeighboringPatchCell(transitionedCells, patchCells, QueuedCells, PatchQueue, Me.GetCellSouth(CurrentCell), transitionGroup, iteration, timestep)
            Me.AddNeighboringPatchCell(transitionedCells, patchCells, QueuedCells, PatchQueue, Me.GetCellSouthwest(CurrentCell), transitionGroup, iteration, timestep)
            Me.AddNeighboringPatchCell(transitionedCells, patchCells, QueuedCells, PatchQueue, Me.GetCellWest(CurrentCell), transitionGroup, iteration, timestep)
            Me.AddNeighboringPatchCell(transitionedCells, patchCells, QueuedCells, PatchQueue, Me.GetCellNorthwest(CurrentCell), transitionGroup, iteration, timestep)

        End While

        patch.Size = PatchSize

        For Each PatchCell In patch.AllCells.Values

            If (Me.IsPatchEdgeCell(PatchCell, patch)) Then
                patch.EdgeCells.Add(PatchCell.CellId, PatchCell)
            End If

        Next

        Debug.Assert(patch.EdgeCells.Count > 0)
        Debug.Assert(patch.AllCells.Count > 0)
        Debug.Assert(patch.AllCells.Count >= patch.EdgeCells.Count)

    End Sub

    ''' <summary>
    ''' Adds a neighboring patch cell
    ''' </summary>
    ''' <param name="transitionedCells"></param>
    ''' <param name="patchCells"></param>
    ''' <param name="queuedCells"></param>
    ''' <param name="patchQueue"></param>
    ''' <param name="neighborCell"></param>
    ''' <param name="transitionGroup"></param>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <remarks></remarks>
    Private Sub AddNeighboringPatchCell(
        ByVal transitionedCells As Dictionary(Of Integer, Cell),
        ByVal patchCells As Dictionary(Of Integer, Cell),
        ByVal queuedCells As Dictionary(Of Integer, Cell),
        ByVal patchQueue As Queue(Of Cell),
        ByVal neighborCell As Cell,
        ByVal transitionGroup As TransitionGroup,
        ByVal iteration As Integer,
        ByVal timestep As Integer)

        If (neighborCell IsNot Nothing) Then

            If (queuedCells.ContainsKey(neighborCell.CellId) Or
                patchCells.ContainsKey(neighborCell.CellId) Or
                transitionedCells.ContainsKey(neighborCell.CellId)) Then

                Return

            End If

            If (Me.SelectSpatialTransitionPathway(
                neighborCell,
                transitionGroup.TransitionGroupId,
                iteration,
                timestep) Is Nothing) Then

                Return

            End If

            queuedCells.Add(neighborCell.CellId, neighborCell)
            patchQueue.Enqueue(neighborCell)

        End If

    End Sub

    ''' <summary>
    ''' Fills the transition patches for the specified transition group
    ''' </summary>
    ''' <param name="transitionedCells"></param>
    ''' <param name="stratum"></param>
    ''' <param name="transitionGroup"></param>
    ''' <param name="iteration"></param>
    ''' <param name="timestep"></param>
    ''' <remarks></remarks>
    Private Sub FillTransitionPatches(
        ByVal transitionedCells As Dictionary(Of Integer, Cell),
        ByVal stratum As Stratum,
        ByVal transitionGroup As TransitionGroup,
        ByVal iteration As Integer,
        ByVal timestep As Integer)

        Debug.Assert(Me.IsSpatial)

        If (transitionGroup.PatchPrioritization Is Nothing) Then
            Return
        End If

        Debug.Assert(transitionGroup.PatchPrioritization.TransitionPatches.Count = 0)

        Dim patchCells As New Dictionary(Of Integer, Cell)

        For Each SimulationCell As Cell In stratum.Cells.Values

            Debug.Assert(SimulationCell.StratumId <> 0)
            Debug.Assert(SimulationCell.StateClassId <> 0)

            If (patchCells.ContainsKey(SimulationCell.CellId) Or
                transitionedCells.ContainsKey(SimulationCell.CellId)) Then

                Continue For

            End If

            If (Me.SelectSpatialTransitionPathway(
                SimulationCell,
                transitionGroup.TransitionGroupId,
                iteration,
                timestep) Is Nothing) Then

                Continue For

            End If

            Dim Patch As New TransitionPatch(SimulationCell)

            Me.GrowTransitionPatch(
                transitionedCells,
                patchCells,
                SimulationCell,
                Patch,
                transitionGroup,
                iteration,
                timestep)

            transitionGroup.PatchPrioritization.TransitionPatches.Add(Patch)

        Next

        transitionGroup.PatchPrioritization.TransitionPatches.Sort(
            Function(p1 As TransitionPatch, p2 As TransitionPatch) As Integer
                Return (p1.Size.CompareTo(p2.Size))
            End Function)

    End Sub

End Class
