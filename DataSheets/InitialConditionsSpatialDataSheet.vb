'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.Core
Imports SyncroSim.StochasticTime

Class InitialConditionsSpatialDataSheet
    Inherits DataSheet

    Protected Overrides Sub AfterImportData(ByVal data As DataTable)

        MyBase.AfterImportData(data)

        ' See if ICS Properties set yet. If not, lets load them up.
        Dim dsICS As DataSheet = Me.GetDataSheet(DATASHEET_SPIC_NAME)
        Dim dsProp As DataSheet = Me.GetDataSheet(DATASHEET_SPPIC_NAME)
        Dim drProp As DataRow = dsProp.GetDataRow()
        If drProp Is Nothing Then

            If (drProp Is Nothing) Then
                drProp = dsProp.GetData.NewRow()
                dsProp.GetData.Rows.Add(drProp)
            End If

            ' Fetch the metadata from the 1st row Primary Stratum file
            Dim psFilename As String = data.Rows(0).Item(DATASHEET_SPIC_STRATUM_FILE_COLUMN_NAME).ToString()
            psFilename = RasterFiles.GetInputFileName(dsICS, psFilename, True)

            Dim rast As New ApexRaster

            Try
                RasterFiles.LoadRasterFile(psFilename, rast, RasterDataType.dtInteger)
            Catch e As GdalException
                FormsUtilities.ErrorMessageBox(e.Message)
                Return
            End Try

            drProp(DATASHEET_SPPIC_NUM_ROWS_COLUMN_NAME) = rast.NumberRows
            drProp(DATASHEET_SPPIC_NUM_COLUMNS_COLUMN_NAME) = rast.NumberCols
            drProp(DATASHEET_SPPIC_NUM_CELLS_COLUMN_NAME) = rast.NumberValidCells
            drProp(DATASHEET_SPPIC_XLLCORNER_COLUMN_NAME) = rast.XllCorner
            drProp(DATASHEET_SPPIC_YLLCORNER_COLUMN_NAME) = rast.YllCorner
            drProp(DATASHEET_SPPIC_CELL_SIZE_COLUMN_NAME) = rast.CellSize
            drProp(DATASHEET_SPPIC_CELL_SIZE_UNITS_COLUMN_NAME) = rast.CellSizeUnits
            drProp(DATASHEET_SPPIC_SRS_COLUMN_NAME) = rast.ProjectionString

            drProp(DATASHEET_SPPIC_CELL_AREA_OVERRIDE_COLUMN_NAME) = False

        End If

    End Sub

End Class