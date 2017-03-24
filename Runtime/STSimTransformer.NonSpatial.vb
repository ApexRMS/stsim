'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Partial Class STSimTransformer

    ''' <summary>
    ''' Initializes all simulations cells in Non-Raster mode
    ''' </summary>
    ''' <param name="iteration">The current iteration</param>
    ''' <remarks></remarks>
    Private Sub InitializeCellsNonRaster(ByVal iteration As Integer)

        If (Me.m_CalcNumCellsFromDist) Then
            Me.InitializeCellsNonRasterCalcFromDist(iteration)
        Else
            Me.InitializeCellsNonRasterNoCalcFromDist(iteration)
        End If

    End Sub

    ''' <summary>
    ''' Initializes a simulation cell from a initial conditions distribution record
    ''' </summary>
    ''' <param name="simulationCell">The cell to initialize</param>
    ''' <param name="icd">The initial conditions distribution record to use</param>
    ''' <remarks></remarks>
    Private Sub InitializeCellNonRaster(
        ByRef simulationCell As Cell,
        ByVal icd As InitialConditionsDistribution,
        ByVal iteration As Integer)

        Debug.Assert(Not Me.IsSpatial)

        Dim sisagemin As Integer = Math.Min(icd.AgeMin, icd.AgeMax)
        Dim sisagemax As Integer = Math.Max(icd.AgeMin, icd.AgeMax)

        Me.InitializeCellAge(
            simulationCell,
            icd.StratumId,
            icd.StateClassId,
            sisagemin,
            sisagemax,
            iteration,
            Me.m_TimestepZero)

        simulationCell.StratumId = icd.StratumId
        simulationCell.StateClassId = icd.StateClassId
        simulationCell.SecondaryStratumId = icd.SecondaryStratumId

        Me.InitializeCellTstValues(simulationCell, iteration)

#If DEBUG Then
        Me.VALIDATE_INITIALIZED_CELL(simulationCell, iteration, Me.m_TimestepZero)
#End If

    End Sub

    ''' <summary>
    ''' Performs all non initial conditions distribution initialization for the specified cell
    ''' </summary>
    ''' <param name="c"></param>
    ''' <param name="iteration"></param>
    ''' <remarks></remarks>
    Private Sub PostInitializeCellNonRaster(ByVal c As Cell, ByVal iteration As Integer)

        Me.m_ProportionAccumulatorMap.AddOrIncrement(c.StratumId, c.SecondaryStratumId)

        Me.OnSummaryStateClassOutput(c, iteration, Me.m_TimestepZero)
        Me.OnSummaryStateAttributeOutput(c, iteration, Me.m_TimestepZero)

        RaiseEvent CellInitialized(Me, New CellEventArgs(c, iteration, Me.m_TimestepZero, Me.m_AmountPerCell))

    End Sub

    ''' <summary>
    ''' Initializes all simulations cells in Non-Raster mode
    ''' </summary>
    ''' <param name="iteration">The current iteration</param>
    ''' <remarks></remarks>
    Private Sub InitializeCellsNonRasterCalcFromDist(ByVal iteration As Integer)

        ' Fetch the number of cells from the NS IC setting
        Dim drrc As DataRow = Me.ResultScenario.GetDataSheet(DATASHEET_NSIC_NAME).GetDataRow()
        Dim numCells As Integer = CInt(drrc(DATASHEET_NSIC_NUM_CELLS_COLUMN_NAME))

        Debug.Assert(Not Me.IsSpatial)
        Debug.Assert(Me.m_Cells.Count > 0)


        Dim icds As InitialConditionsDistributionCollection = Me.m_InitialConditionsDistributionMap.GetICDs(iteration)
        Dim sumOfRelativeAmountForIteration As Double = CalcSumOfRelativeAmount(iteration)

        Dim CellIndex As Integer = 0

#If DEBUG Then
        Dim dict As New Dictionary(Of Integer, Cell)
#End If

        For Each icd As InitialConditionsDistribution In icds

            ' DEVNOTE:To support multiple iterations, use relativeAmount / sum For Iteration as scale of total number of cells. Number of cells determined by 1st iteration specified. 
            ' Otherwise, there's too much likelyhood that Number of cells will vary per iteration, which we cant/wont support.
            Dim numCellsForICD As Integer = CInt(Math.Round(icd.RelativeAmount / sumOfRelativeAmountForIteration * numCells))
            For i As Integer = 0 To numCellsForICD - 1

                Dim c As Cell = Me.Cells(CellIndex)

#If DEBUG Then
                dict.Add(c.CellId, c)
#End If

                Me.InitializeCellNonRaster(c, icd, iteration)
                Me.PostInitializeCellNonRaster(c, iteration)

                CellIndex += 1

            Next

        Next

#If DEBUG Then
        Debug.Assert(dict.Count = Me.m_Cells.Count)
        Debug.Assert(CellIndex = Me.Cells.Count)
#End If

        RaiseEvent CellsInitialized(Me, New CellEventArgs(Nothing, iteration, Me.m_TimestepZero, Me.m_AmountPerCell))

    End Sub

    ''' <summary>
    ''' Initializes all simulations cells in Non-Raster mode
    ''' </summary>
    ''' <param name="iteration">The current iteration</param>
    ''' <remarks></remarks>
    Private Sub InitializeCellsNonRasterNoCalcFromDist(ByVal iteration As Integer)

        Debug.Assert(Not Me.IsSpatial)
        Debug.Assert(Me.m_Cells.Count > 0)

#If DEBUG Then
        Dim dict As New Dictionary(Of Integer, Cell)
#End If

        Dim icds As InitialConditionsDistributionCollection = Me.m_InitialConditionsDistributionMap.GetICDs(iteration)
        Dim sumOfRelativeAmountForIteration As Double = CalcSumOfRelativeAmount(iteration)

        For Each c As Cell In Me.m_Cells

            Dim Rand As Double = Me.m_RandomGenerator.GetNextDouble()
            Dim CumulativeProportion As Double = 0.0

            For Each icd As InitialConditionsDistribution In icds

                CumulativeProportion += (icd.RelativeAmount / sumOfRelativeAmountForIteration)

                If (Rand < CumulativeProportion) Then

#If DEBUG Then
                    dict.Add(c.CellId, c)
#End If

                    Me.InitializeCellNonRaster(c, icd, iteration)
                    Me.PostInitializeCellNonRaster(c, iteration)

                    Exit For

                End If

            Next

        Next

#If DEBUG Then
        Debug.Assert(dict.Count = Me.m_Cells.Count)
#End If

        RaiseEvent CellsInitialized(Me, New CellEventArgs(Nothing, iteration, Me.m_TimestepZero, Me.m_AmountPerCell))

    End Sub

End Class
