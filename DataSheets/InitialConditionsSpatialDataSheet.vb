'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports System.Reflection
Imports System.Globalization
Imports SyncroSim.Core
Imports SyncroSim.StochasticTime

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class InitialConditionsSpatialDataSheet
    Inherits DataSheet

    Public Event ValidatingRasters As EventHandler(Of EventArgs)
    Public Event RastersValidated As EventHandler(Of EventArgs)

    Public Overrides Sub Validate(proposedRow As DataRow, transferMethod As DataTransferMethod)

        MyBase.Validate(proposedRow, transferMethod)

        Dim ColNames As New List(Of String)

        ColNames.Add(DATASHEET_SPIC_STRATUM_FILE_COLUMN_NAME)
        ColNames.Add(DATASHEET_SPIC_SECONDARY_STRATUM_FILE_COLUMN_NAME)
        ColNames.Add(DATASHEET_SPIC_STATE_CLASS_FILE_COLUMN_NAME)
        ColNames.Add(DATASHEET_SPIC_AGE_FILE_COLUMN_NAME)

        Dim ThisData As DataTable = Me.GetData()
        Dim FirstRaster As StochasticTimeRaster = Nothing

        If (ThisData.DefaultView.Count = 0) Then
            FirstRaster = Me.LoadRaster(proposedRow, DATASHEET_SPIC_STRATUM_FILE_COLUMN_NAME)
        Else
            FirstRaster = Me.LoadRaster(ThisData.DefaultView(0).Row, DATASHEET_SPIC_STRATUM_FILE_COLUMN_NAME)
        End If

        Try

            RaiseEvent ValidatingRasters(Me, New EventArgs)

            For Each s As String In ColNames

                If (proposedRow(s) IsNot DBNull.Value) Then

                    Dim rast As StochasticTimeRaster = Me.LoadRaster(proposedRow, s)

                    Try
                        Me.ValidateRaster(rast, FirstRaster.NumberRows, FirstRaster.NumberCols, s)
                    Catch ex As Exception
                        proposedRow(s) = DBNull.Value
                        Throw
                    End Try

                End If

            Next

        Finally
            RaiseEvent RastersValidated(Me, New EventArgs)
        End Try

    End Sub

    Protected Overrides Sub BeforeImportData(data As DataTable)

        MyBase.BeforeImportData(data)

        If (data.Rows.Count = 0) Then
            Return
        End If

        Dim ColNames As New List(Of String)

        ColNames.Add(DATASHEET_SPIC_STRATUM_FILE_COLUMN_NAME)
        ColNames.Add(DATASHEET_SPIC_SECONDARY_STRATUM_FILE_COLUMN_NAME)
        ColNames.Add(DATASHEET_SPIC_STATE_CLASS_FILE_COLUMN_NAME)
        ColNames.Add(DATASHEET_SPIC_AGE_FILE_COLUMN_NAME)

        Dim ThisData As DataTable = Me.GetData()
        Dim FirstRaster As StochasticTimeRaster = Nothing

        If (ThisData.DefaultView.Count = 0) Then
            FirstRaster = Me.LoadRaster(data.Rows(0), DATASHEET_SPIC_STRATUM_FILE_COLUMN_NAME)
        Else
            FirstRaster = Me.LoadRaster(ThisData.DefaultView(0).Row, DATASHEET_SPIC_STRATUM_FILE_COLUMN_NAME)
        End If

        Try

            RaiseEvent ValidatingRasters(Me, New EventArgs)

            For Each dr As DataRow In data.Rows

                If (dr.RowState <> DataRowState.Deleted) Then

                    For Each s As String In ColNames

                        If (dr(s) IsNot DBNull.Value) Then

                            Dim rast As StochasticTimeRaster = Me.LoadRaster(dr, s)

                            Try
                                Me.ValidateRaster(rast, FirstRaster.NumberRows, FirstRaster.NumberCols, s)
                            Catch ex As Exception
                                dr(s) = DBNull.Value
                                Throw
                            End Try

                        End If

                    Next

                End If

            Next

        Finally
            RaiseEvent RastersValidated(Me, New EventArgs)
        End Try

    End Sub

    Protected Overrides Sub OnRowsAdded(sender As Object, e As DataSheetRowEventArgs)

        MyBase.OnRowsAdded(sender, e)

        Dim ThisData = Me.GetData()
        Dim dsProp As DataSheet = Me.GetDataSheet(DATASHEET_SPPIC_NAME)
        Dim drProp As DataRow = dsProp.GetDataRow()

        If (drProp Is Nothing And ThisData.DefaultView.Count > 0) Then

            dsProp.BeginAddRows()
            drProp = dsProp.GetData.NewRow()

            Dim FirstRow As DataRow = ThisData.DefaultView(0).Row
            Dim FirstRast As StochasticTimeRaster = Me.LoadRaster(FirstRow, DATASHEET_SPIC_STRATUM_FILE_COLUMN_NAME)

            drProp(DATASHEET_SPPIC_NUM_ROWS_COLUMN_NAME) = FirstRast.NumberRows
            drProp(DATASHEET_SPPIC_NUM_COLUMNS_COLUMN_NAME) = FirstRast.NumberCols
            drProp(DATASHEET_SPPIC_NUM_CELLS_COLUMN_NAME) = FirstRast.NumberValidCells
            drProp(DATASHEET_SPPIC_XLLCORNER_COLUMN_NAME) = FirstRast.XllCorner
            drProp(DATASHEET_SPPIC_YLLCORNER_COLUMN_NAME) = FirstRast.YllCorner
            drProp(DATASHEET_SPPIC_CELL_SIZE_COLUMN_NAME) = FirstRast.CellSize
            drProp(DATASHEET_SPPIC_CELL_SIZE_UNITS_COLUMN_NAME) = FirstRast.CellSizeUnits
            drProp(DATASHEET_SPPIC_SRS_COLUMN_NAME) = FirstRast.ProjectionString
            drProp(DATASHEET_SPPIC_CELL_AREA_OVERRIDE_COLUMN_NAME) = False

            Dim amountlabel As String = Nothing
            Dim destUnitsVal As TerminologyUnit
            Dim cellArea As Double = FirstRast.CellSize ^ 2

            GetAmountLabelTerminology(Me.Project.GetDataSheet(DATASHEET_TERMINOLOGY_NAME), amountlabel, destUnitsVal)
            drProp(DATASHEET_SPPIC_CELL_AREA_COLUMN_NAME) = CalcCellArea(cellArea, FirstRast.CellSizeUnits, destUnitsVal)

            dsProp.Changes.Add(New ChangeRecord(Me, "Added raster metadata"))
            dsProp.GetData.Rows.Add(drProp)
            dsProp.EndAddRows()

        End If

    End Sub

    Protected Overrides Sub OnRowsDeleted(sender As Object, e As DataSheetRowEventArgs)

        MyBase.OnRowsDeleted(sender, e)

        If (Me.GetData().DefaultView.Count = 0) Then

            Dim PropsDataSheet As DataSheet = Me.GetDataSheet(DATASHEET_SPPIC_NAME)

            PropsDataSheet.GetData()
            PropsDataSheet.ClearData()

        End If

    End Sub

    Private Function LoadRaster(ByVal dr As DataRow, ByVal fileNameColumn As String) As StochasticTimeRaster

        Dim FileName As String = CType(dr(fileNameColumn), String)
        Dim psFilename As String = RasterFiles.GetInputFileName(Me, FileName, True)
        Dim rast As New StochasticTimeRaster

        RasterFiles.LoadRasterFile(psFilename, rast, RasterDataType.DTInteger)
        Return rast

    End Function

    Private Sub ValidateRaster(ByVal rast As StochasticTimeRaster, ByVal rows As Integer, ByVal columns As Integer, ByVal columnName As String)

        Dim PrimaryStratumLabel As String = Nothing
        Dim SecondaryStratumLabel As String = Nothing
        Dim TertiaryStratumLabel As String = Nothing
        Dim ColumnDisplayName As String = Me.Columns(columnName).DisplayName

        Dim TerminologySheet As DataSheet = Me.Project.GetDataSheet(DATASHEET_TERMINOLOGY_NAME)
        GetStratumLabelTerminology(TerminologySheet, PrimaryStratumLabel, SecondaryStratumLabel, TertiaryStratumLabel)

        If (columnName = DATASHEET_SPIC_STRATUM_FILE_COLUMN_NAME) Then
            ColumnDisplayName = PrimaryStratumLabel
        ElseIf (columnName = DATASHEET_SPIC_SECONDARY_STRATUM_FILE_COLUMN_NAME) Then
            ColumnDisplayName = SecondaryStratumLabel
        End If

        If rast.NumberRows <> rows Then

            Dim msg As String = Nothing

            If (columnName = DATASHEET_SPIC_STRATUM_FILE_COLUMN_NAME) Then

                msg = String.Format(CultureInfo.InvariantCulture,
                    "The number of rows for the '{0}' raster does not match that of the other '{1}' rasters.",
                    PrimaryStratumLabel, PrimaryStratumLabel)

            Else

                msg = String.Format(CultureInfo.InvariantCulture,
                    "The number of rows for the '{0}' raster does not match that of the '{1}' raster.",
                    ColumnDisplayName, PrimaryStratumLabel)

            End If

            Throw New DataException(msg)

        ElseIf rast.NumberCols <> columns Then

            Dim msg As String = Nothing

            If (columnName = DATASHEET_SPIC_STRATUM_FILE_COLUMN_NAME) Then

                msg = String.Format(CultureInfo.InvariantCulture,
                    "The number of columns for the '{0}' raster does not match that of the other '{1}' rasters.",
                    PrimaryStratumLabel, PrimaryStratumLabel)

            Else

                msg = String.Format(CultureInfo.InvariantCulture,
                    "The number of columns for the '{0}' raster does not match that of the '{1}' raster.",
                    ColumnDisplayName, PrimaryStratumLabel)

            End If

            Throw New DataException(msg)

        End If

    End Sub

    ''' <summary>
    ''' Convert the Cell Area as specified in the raster units to Cell Area as specified in user-configurable Terminology Units
    ''' </summary>
    ''' <param name="srcCellArea">The Cell Area in the raster files native units</param>
    ''' <param name="srcSizeUnits">The native linear size units of the raster files</param>
    ''' <param name="destAreaUnits">The specified Area Units</param>
    ''' <returns>The calculated Cell Area</returns>
    ''' <remarks></remarks>
    Friend Shared Function CalcCellArea(srcCellArea As Double, srcSizeUnits As String, destAreaUnits As TerminologyUnit) As Double

        Dim convFactor As Double

        ' Convert the Source Area to M2
        srcSizeUnits = Replace(srcSizeUnits, " ", "_")    ' replace space with an underscore
        Select Case srcSizeUnits.ToUpper(CultureInfo.InvariantCulture)  ' Use Case insenstive comparison
            ' Convert from ft^2 to M2
            Case RasterCellSizeUnit.Foot.ToString().ToUpper(CultureInfo.InvariantCulture), RasterCellSizeUnit.Foot_US.ToString().ToUpper(CultureInfo.InvariantCulture), RasterCellSizeUnit.US_survey_foot.ToString().ToUpper(CultureInfo.InvariantCulture)
                convFactor = 0.092903
            Case RasterCellSizeUnit.Metre.ToString().ToUpper(CultureInfo.InvariantCulture),
                RasterCellSizeUnit.Meter.ToString().ToUpper(CultureInfo.InvariantCulture),
                RasterCellSizeUnit.Meters.ToString().ToUpper(CultureInfo.InvariantCulture)
                ' No conversion needed for Meters
                convFactor = 1
            Case RasterCellSizeUnit.Undefined.ToString().ToUpper(CultureInfo.InvariantCulture), RasterCellSizeUnit.Undetermined.ToString().ToUpper(CultureInfo.InvariantCulture)
                Return 0
        End Select

        Dim areaM2 = srcCellArea * convFactor

        ' Now lets convert M2 to Terminology Units

        'Calculate the total area and cell size
        Select Case destAreaUnits
            Case TerminologyUnit.Acres
                ' 1m2 = 0.000247105 Acres
                convFactor = 0.000247105

            Case TerminologyUnit.Hectares
                ' 1m2 = 0.0001 Hectares
                convFactor = 0.0001

            Case TerminologyUnit.SquareKilometers
                ' 1m2 = 1e-6 Km2
                convFactor = 0.000001

            Case TerminologyUnit.SquareMiles
                ' 1m2 = 3.861e-7 Mi2
                convFactor = 0.0000003861
            Case Else
                convFactor = 0
        End Select

        Return areaM2 * convFactor

    End Function

End Class