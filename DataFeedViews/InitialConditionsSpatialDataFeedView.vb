'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Imports System.IO
Imports System.Text
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Globalization
Imports SyncroSim.Core
Imports SyncroSim.Core.Forms
Imports SyncroSim.StochasticTime
Imports SyncroSim.Common.Forms

Class InitialConditionsSpatialDataFeedView

    Private m_ICSpatialFilesView As DataFeedView
    Private m_ICSpatialFilesDataGrid As DataGridView
    Private Delegate Sub DelegateNoArgs()
    Private m_ColumnsInitialized As Boolean

    Const BROWSE_BUTTON_TEXT As String = "..."
    ' Iteration
    Const ITERATION_COLUMN_INDEX = 0
    ' Strata
    Const PRIMARY_STRATUM_FILE_NAME_COLUMN_INDEX As Integer = 1
    Const PRIMARY_STRATUM_BROWSE_COLUMN_INDEX As Integer = 2
    ' Secondary Strata
    Const SECONDARY_STRATUM_FILE_NAME_COLUMN_INDEX As Integer = 3
    Const SECONDARY_STRATUM_BROWSE_COLUMN_INDEX As Integer = 4
    ' State Class
    Const SCLASS_FILE_NAME_COLUMN_INDEX As Integer = 5
    Const SCLASS_BROWSE_COLUMN_INDEX As Integer = 6
    ' Age 
    Const AGE_FILE_NAME_COLUMN_INDEX As Integer = 7
    Const AGE_BROWSE_COLUMN_INDEX As Integer = 8


    Private m_InRefresh As Boolean
    Private m_CellAreaCalcHasChanges As Boolean

    Protected Overrides Sub InitializeView()

        MyBase.InitializeView()

        Me.m_ICSpatialFilesView = (Me.Session.CreateMultiRowDataFeedView(Me.Scenario, Me.ControllingScenario))
        Me.m_ICSpatialFilesDataGrid = CType(Me.m_ICSpatialFilesView, MultiRowDataFeedView).GridControl
        Me.PanelInitialConditionSpatialFiles.Controls.Add(Me.m_ICSpatialFilesView)

        'TODO:TKR
        '        Me.ConfigureContextMenu()

    End Sub

    Protected Overrides Sub OnRowsDeleted(sender As Object, e As DataSheetRowEventArgs)

        MyBase.OnRowsDeleted(sender, e)

        If CType(sender, DataSheet).Name = DATASHEET_SPIC_NAME Then
            ' Delete the Properties record if all the ICS File records have been deleted
            Dim ds = Me.DataFeed.DataSheets(DATASHEET_SPIC_NAME)
            ' Delete the Properties record.
            If ds.GetData().DefaultView.Count = 0 Then
                Me.DataFeed.DataSheets(DATASHEET_SPPIC_NAME).ClearData()
                'DEVNOTE:TKR - This isnt very elegant, but is a short term fix to getting the Spatial Properties fields to refresh after an Import or a Delete All.
                RefreshControls()
            End If
        End If

    End Sub


    Public Overrides Sub LoadDataFeed(dataFeed As DataFeed)

        MyBase.LoadDataFeed(dataFeed)

        Me.m_ICSpatialFilesView.LoadDataFeed(dataFeed, DATASHEET_SPIC_NAME)

        If (Not Me.m_ColumnsInitialized) Then

            'Add handlers
            AddHandler Me.m_ICSpatialFilesDataGrid.CellEnter, AddressOf Me.OnGridCellEnter
            AddHandler Me.m_ICSpatialFilesDataGrid.CellMouseDown, AddressOf Me.OnGridCellMouseDown
            AddHandler Me.m_ICSpatialFilesDataGrid.DataBindingComplete, AddressOf Me.OnGridBindingComplete
            AddHandler Me.m_ICSpatialFilesDataGrid.KeyDown, AddressOf Me.OnGridKeyDown
            AddHandler Me.m_ICSpatialFilesDataGrid.RowsAdded, AddressOf Me.OnGridRowsAdded

            'Add the browse button columns
            Dim PrimStratumFileBrowseColumn As New DataGridViewButtonColumn()
            PrimStratumFileBrowseColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            PrimStratumFileBrowseColumn.Width = 40
            PrimStratumFileBrowseColumn.MinimumWidth = 40
            Me.m_ICSpatialFilesDataGrid.Columns.Insert(PRIMARY_STRATUM_BROWSE_COLUMN_INDEX, PrimStratumFileBrowseColumn)

            Dim SecStratumFileBrowseColumn As New DataGridViewButtonColumn()
            SecStratumFileBrowseColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            SecStratumFileBrowseColumn.Width = 40
            SecStratumFileBrowseColumn.MinimumWidth = 40
            Me.m_ICSpatialFilesDataGrid.Columns.Insert(SECONDARY_STRATUM_BROWSE_COLUMN_INDEX, SecStratumFileBrowseColumn)

            Dim SClassFileBrowseColumn As New DataGridViewButtonColumn()
            SClassFileBrowseColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            SClassFileBrowseColumn.Width = 40
            SClassFileBrowseColumn.MinimumWidth = 40
            Me.m_ICSpatialFilesDataGrid.Columns.Insert(SCLASS_BROWSE_COLUMN_INDEX, SClassFileBrowseColumn)

            Dim AgeFileBrowseColumn As New DataGridViewButtonColumn()
            AgeFileBrowseColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            AgeFileBrowseColumn.Width = 40
            AgeFileBrowseColumn.MinimumWidth = 40
            Me.m_ICSpatialFilesDataGrid.Columns.Insert(AGE_BROWSE_COLUMN_INDEX, AgeFileBrowseColumn)

            'Configure columns
            Me.m_ICSpatialFilesDataGrid.Columns(PRIMARY_STRATUM_FILE_NAME_COLUMN_INDEX).DefaultCellStyle.BackColor = Color.LightGray
            Me.m_ICSpatialFilesDataGrid.Columns(SECONDARY_STRATUM_FILE_NAME_COLUMN_INDEX).DefaultCellStyle.BackColor = Color.LightGray
            Me.m_ICSpatialFilesDataGrid.Columns(SCLASS_FILE_NAME_COLUMN_INDEX).DefaultCellStyle.BackColor = Color.LightGray
            Me.m_ICSpatialFilesDataGrid.Columns(AGE_FILE_NAME_COLUMN_INDEX).DefaultCellStyle.BackColor = Color.LightGray

            Me.m_ColumnsInitialized = True

        End If

        Me.MonitorDataSheet(
            DATASHEET_TERMINOLOGY_NAME,
            AddressOf Me.OnTerminologyChanged,
            True)

    End Sub

    Private Sub OnGridCellEnter(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs)

        Select Case e.ColumnIndex
            Case PRIMARY_STRATUM_FILE_NAME_COLUMN_INDEX, SECONDARY_STRATUM_FILE_NAME_COLUMN_INDEX, SCLASS_FILE_NAME_COLUMN_INDEX, AGE_FILE_NAME_COLUMN_INDEX
                Me.Session.MainForm.BeginInvoke(New DelegateNoArgs(AddressOf Me.OnNewCellEnterAsync), Nothing)
        End Select

    End Sub

    Private Sub OnGridCellMouseDown(ByVal sender As Object, ByVal e As DataGridViewCellMouseEventArgs)

        If (e.RowIndex >= 0) Then

            Select Case e.ColumnIndex
                Case PRIMARY_STRATUM_BROWSE_COLUMN_INDEX, SECONDARY_STRATUM_BROWSE_COLUMN_INDEX, SCLASS_BROWSE_COLUMN_INDEX, AGE_BROWSE_COLUMN_INDEX
                    ChooseRasterFile(e.RowIndex, e.ColumnIndex - 1)
            End Select

        End If

    End Sub

    Private Sub OnGridKeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)

        If (Me.m_ICSpatialFilesDataGrid.CurrentCell IsNot Nothing) Then

            Select Case Me.m_ICSpatialFilesDataGrid.CurrentCell.ColumnIndex
                Case PRIMARY_STRATUM_BROWSE_COLUMN_INDEX, SECONDARY_STRATUM_BROWSE_COLUMN_INDEX, SCLASS_BROWSE_COLUMN_INDEX, AGE_BROWSE_COLUMN_INDEX
                    If (e.KeyValue = Keys.Enter) Then
                        ChooseRasterFile(Me.m_ICSpatialFilesDataGrid.CurrentCell.RowIndex, Me.m_ICSpatialFilesDataGrid.CurrentCell.ColumnIndex - 1)
                        e.Handled = True
                    End If
            End Select

        End If

    End Sub

    Private Sub OnGridBindingComplete(sender As System.Object, e As System.Windows.Forms.DataGridViewBindingCompleteEventArgs)

        For Each dgr As DataGridViewRow In Me.m_ICSpatialFilesDataGrid.Rows
            dgr.Cells(PRIMARY_STRATUM_BROWSE_COLUMN_INDEX).Value = BROWSE_BUTTON_TEXT
            dgr.Cells(SECONDARY_STRATUM_BROWSE_COLUMN_INDEX).Value = BROWSE_BUTTON_TEXT
            dgr.Cells(SCLASS_BROWSE_COLUMN_INDEX).Value = BROWSE_BUTTON_TEXT
            dgr.Cells(AGE_BROWSE_COLUMN_INDEX).Value = BROWSE_BUTTON_TEXT
        Next

    End Sub

    Private Sub OnNewCellEnterAsync()

        Dim Row As Integer = Me.m_ICSpatialFilesDataGrid.CurrentCell.RowIndex
        Dim Col As Integer = Me.m_ICSpatialFilesDataGrid.CurrentCell.ColumnIndex

        Select Case Col
            Case PRIMARY_STRATUM_FILE_NAME_COLUMN_INDEX, SECONDARY_STRATUM_FILE_NAME_COLUMN_INDEX, SCLASS_FILE_NAME_COLUMN_INDEX, AGE_FILE_NAME_COLUMN_INDEX


                If (ModifierKeys = Keys.Shift) Then

                    Col -= 1

                    While (Not Me.m_ICSpatialFilesDataGrid.Columns(Col).Visible)
                        Col -= -1
                    End While

                Else
                    Col += 1
                End If

                Me.m_ICSpatialFilesDataGrid.CurrentCell = Me.m_ICSpatialFilesDataGrid.Rows(Row).Cells(Col)

        End Select

    End Sub

    Private Sub SetICSpatialFile(ByVal rowIndex As Integer, ByVal colIndex As Integer, rasterFullFilename As String)

        Dim ds As DataSheet = Me.Scenario.GetDataSheet(DATASHEET_SPIC_NAME)

        Dim OldMode As DataGridViewEditMode = Me.m_ICSpatialFilesDataGrid.EditMode
        Me.m_ICSpatialFilesDataGrid.EditMode = DataGridViewEditMode.EditProgrammatically

        Me.m_ICSpatialFilesDataGrid.CurrentCell = Me.m_ICSpatialFilesDataGrid.Rows(rowIndex).Cells(colIndex)
        Me.m_ICSpatialFilesDataGrid.Rows(rowIndex).Cells(colIndex).Value = Path.GetFileName(rasterFullFilename)
        Me.m_ICSpatialFilesDataGrid.NotifyCurrentCellDirty(True)

        Me.m_ICSpatialFilesDataGrid.BeginEdit(False)
        Me.m_ICSpatialFilesDataGrid.EndEdit()

        Me.m_ICSpatialFilesDataGrid.CurrentCell = Me.m_ICSpatialFilesDataGrid.Rows(rowIndex).Cells(colIndex + 1)

        ds.AddExternalInputFile(rasterFullFilename)

        Me.m_ICSpatialFilesDataGrid.EditMode = OldMode

    End Sub


    Public Overrides Sub RefreshControls()

        Me.m_InRefresh = True

        Me.ResetControls()
        Me.RefreshNonCalculatedValues()
        Me.RefreshCalculatedValues()

        Me.m_InRefresh = False

    End Sub

    Public Overrides Sub EnableView(enable As Boolean)

        MyBase.EnableView(enable)

        If (Me.PanelInitialConditionSpatialFiles.Controls.Count > 0) Then

            Dim v As DataFeedView = CType(Me.PanelInitialConditionSpatialFiles.Controls(0), DataFeedView)
            v.EnableView(enable)

        End If

    End Sub

    Private Function GetPropDataSheet() As DataSheet

        If (Me.DataFeed IsNot Nothing) Then
            Return Me.DataFeed.GetDataSheet(DATASHEET_SPPIC_NAME)
        Else
            Return Nothing
        End If

    End Function

    Private Function GetPropDataRow() As DataRow

        Dim ds As DataSheet = Me.GetPropDataSheet()

        If (ds IsNot Nothing) Then
            Return ds.GetDataRow()
        Else
            Return Nothing
        End If

    End Function


    Private Sub OnTerminologyChanged(e As DataSheetMonitorEventArgs)

        Dim Primary As String = Nothing
        Dim Secondary As String = Nothing
        Dim AmountLabel As String = Nothing
        Dim AmountUnits As TerminologyUnit = TerminologyUnit.None

        GetStratumLabelTerminology(e.DataSheet, Primary, Secondary)
        GetAmountLabelTerminology(e.DataSheet, AmountLabel, AmountUnits)

        Me.m_ICSpatialFilesDataGrid.Columns(PRIMARY_STRATUM_FILE_NAME_COLUMN_INDEX).HeaderText = BuildLowerCaseLabel(Primary)
        Me.m_ICSpatialFilesDataGrid.Columns(SECONDARY_STRATUM_FILE_NAME_COLUMN_INDEX).HeaderText = BuildLowerCaseLabel(Secondary)

        Me.LabelCalcTtlAmount.Text = String.Format(CultureInfo.CurrentCulture,
            "Total {0}:", AmountLabel.ToLower(CultureInfo.CurrentCulture))

    End Sub

    Private Sub ResetControls()

        Me.TextBoxNumColumns.Text = Nothing
        Me.TextBoxNumRows.Text = Nothing
        Me.TextBoxCellArea.Text = Nothing
        Me.TextBoxCellAreaCalc.Text = Nothing
        Me.TextBoxCellAreaCalc.ReadOnly = True
        Me.TextBoxNumCells.Text = Nothing
        Me.TextBoxTotalArea.Text = Nothing
        Me.CheckBoxCellSizeOverride.Enabled = False
        Me.CheckBoxCellSizeOverride.AutoCheck = False

    End Sub


    Private Sub RefreshNonCalculatedValues()

        Dim drProp As DataRow = Me.GetPropDataRow()

        If (drProp Is Nothing) Then
            Return
        End If

        Me.CheckBoxCellSizeOverride.Checked = DataTableUtilities.GetDataBool(drProp(DATASHEET_SPPIC_CELL_AREA_OVERRIDE_COLUMN_NAME))
        Me.CheckBoxCellSizeOverride.Enabled = True
        Me.CheckBoxCellSizeOverride.AutoCheck = True

        ' Display non-calculated Raster Metadata
        Dim NumRows As Integer = DataTableUtilities.GetDataInt(drProp(DATASHEET_SPPIC_NUM_ROWS_COLUMN_NAME))
        Dim NumCols As Integer = DataTableUtilities.GetDataInt(drProp(DATASHEET_SPPIC_NUM_COLUMNS_COLUMN_NAME))
        Dim CellArea As Single = DataTableUtilities.GetDataSingle(drProp(DATASHEET_SPPIC_CELL_SIZE_COLUMN_NAME))
        Dim cellAreaCalc As Double = DataTableUtilities.GetDataDbl(drProp(DATASHEET_SPPIC_CELL_AREA_COLUMN_NAME))

        Me.TextBoxNumRows.Text = NumRows.ToString(CultureInfo.InvariantCulture)
        Me.TextBoxNumColumns.Text = NumCols.ToString(CultureInfo.InvariantCulture)
        Me.TextBoxCellArea.Text = CellArea.ToString("N4", CultureInfo.InvariantCulture)
        Me.TextBoxCellAreaCalc.Text = cellAreaCalc.ToString("N4", CultureInfo.InvariantCulture)

    End Sub

    Private Sub RefreshCalculatedValues()

        Dim drProp As DataRow = Me.GetPropDataRow()

        If (drProp Is Nothing) Then
            Return
        End If

        'Num Cells
        Dim NumCells As Integer = DataTableUtilities.GetDataInt(drProp(DATASHEET_SPPIC_NUM_CELLS_COLUMN_NAME))

        Me.TextBoxNumCells.Text = NumCells.ToString(CultureInfo.CurrentCulture)

        'Get the units and refresh the units labels - the default Raster Cell Units is Metres^2
        Dim srcSizeUnits As String = DataTableUtilities.GetDataStr(drProp(DATASHEET_SPPIC_CELL_SIZE_UNITS_COLUMN_NAME))
        Dim srcAreaUnits As String = srcSizeUnits & "^2"
        Dim amountlabel As String = Nothing
        Dim destUnitsVal As TerminologyUnit

        GetAmountLabelTerminology(Me.Project.GetDataSheet(DATASHEET_TERMINOLOGY_NAME), amountlabel, destUnitsVal)
        Dim destAreaLbl As String = TerminologyUnitToString(destUnitsVal)

        srcAreaUnits = srcAreaUnits.ToLower(CultureInfo.CurrentCulture)
        amountlabel = amountlabel.ToLower(CultureInfo.CurrentCulture)
        destAreaLbl = destAreaLbl.ToLower(CultureInfo.CurrentCulture)

        Me.LabelRasterCellArea.Text = String.Format(CultureInfo.CurrentCulture, "Cell size ({0}):", srcAreaUnits)
        Me.LabelCalcCellArea.Text = String.Format(CultureInfo.CurrentCulture, "Cell size ({0}):", destAreaLbl)
        Me.LabelCalcTtlAmount.Text = String.Format(CultureInfo.CurrentCulture, "Total {0} ({1}):", amountlabel, destAreaLbl)

        ' Calculate Cell Area in raster's native units
        Dim cellSize As Single = DataTableUtilities.GetDataSingle(drProp(DATASHEET_SPPIC_CELL_SIZE_COLUMN_NAME))
        Dim cellArea As Double = cellSize ^ 2
        Me.TextBoxCellArea.Text = cellArea.ToString("N4", CultureInfo.CurrentCulture)

        ' Calc Cell Area in terminology units
        Dim cellAreaTU As Double
        If Not CheckBoxCellSizeOverride.Checked Then
            cellAreaTU = CalcCellArea(cellArea, srcSizeUnits, destUnitsVal)
            Me.TextBoxCellAreaCalc.Text = cellAreaTU.ToString("N4", CultureInfo.CurrentCulture)
            drProp(DATASHEET_SPPIC_CELL_AREA_COLUMN_NAME) = cellAreaTU
            TextBoxCellAreaCalc.ReadOnly = True
        Else
            cellAreaTU = DataTableUtilities.GetDataDbl(drProp(DATASHEET_SPPIC_CELL_AREA_COLUMN_NAME))
            TextBoxCellAreaCalc.ReadOnly = False
        End If

        ' Now calculate total area in the specified terminology units
        Dim ttlArea = cellAreaTU * NumCells
        Me.TextBoxTotalArea.Text = ttlArea.ToString("N4", CultureInfo.CurrentCulture)

    End Sub

    ''' <summary>
    ''' Verify the specified instance of class Raster against the raster metadata of any already loaded raster files. If 
    ''' none already loaded, then set the form text boxes with appropriate values from object.
    ''' </summary>
    ''' <param name="dr">The current row of data for this data feed</param>
    ''' <param name="rast">The incoming raster</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function VerifyRaster(ByVal dr As DataRow, ByVal rast As StochasticTimeRaster) As Boolean

        ' Test number of cols. 
        If rast.NumberCols <> DataTableUtilities.GetDataInt(dr(DATASHEET_SPPIC_NUM_COLUMNS_COLUMN_NAME)) Then
            FormsUtilities.ErrorMessageBox("Mismatch on the number of columns in rasters.")
            Return False
        End If

        ' Test number of rows. 
        If rast.NumberRows <> DataTableUtilities.GetDataInt(dr(DATASHEET_SPPIC_NUM_ROWS_COLUMN_NAME)) Then
            FormsUtilities.ErrorMessageBox("Mismatch on the number of rows in rasters.")
            Return False
        End If

        'DEVNOTE: TOM - We've decided only rows and columns are mandatory matches, all other metadata defers to values
        ' contained in Primary Stratum raster.

        Return True

    End Function



    Private Sub EditICSpatialRecord(ByVal rast As StochasticTimeRaster, ByVal fileColumnNumber As Integer, fileRowNumber As Integer, ByVal rasterFileName As String)

        Dim dsICSProp As DataSheet = Me.GetPropDataSheet()
        Dim drProp As DataRow = dsICSProp.GetDataRow()

        If (drProp Is Nothing) Then
            drProp = dsICSProp.GetData.NewRow()
            dsICSProp.GetData.Rows.Add(drProp)
        End If

        ' No raster metadata defined yet ?
        If IsDBNull(drProp(DATASHEET_SPPIC_NUM_ROWS_COLUMN_NAME)) Then
            drProp(DATASHEET_SPPIC_NUM_ROWS_COLUMN_NAME) = rast.NumberRows
            drProp(DATASHEET_SPPIC_NUM_COLUMNS_COLUMN_NAME) = rast.NumberCols
            drProp(DATASHEET_SPPIC_NUM_CELLS_COLUMN_NAME) = rast.NumberValidCells
            drProp(DATASHEET_SPPIC_XLLCORNER_COLUMN_NAME) = rast.XllCorner
            drProp(DATASHEET_SPPIC_YLLCORNER_COLUMN_NAME) = rast.YllCorner
            drProp(DATASHEET_SPPIC_CELL_SIZE_COLUMN_NAME) = rast.CellSize
            drProp(DATASHEET_SPPIC_CELL_SIZE_UNITS_COLUMN_NAME) = rast.CellSizeUnits
            drProp(DATASHEET_SPPIC_SRS_COLUMN_NAME) = rast.ProjectionString

            SetICSpatialFile(fileRowNumber, fileColumnNumber, rasterFileName)
            dsICSProp.Changes.Add(New ChangeRecord(Me, "Added raster metadata"))

        Else
            ' check simple metadata for match
            If VerifyRaster(drProp, rast) Then
                SetICSpatialFile(fileRowNumber, fileColumnNumber, rasterFileName)
            End If
        End If


    End Sub


    ''' <summary>
    ''' Chooses a new raster file
    ''' </summary>
    ''' <remarks>
    ''' Just store the filename. For now, no path required. In the future may want to support absolute path, differentiated by x:\\
    ''' </remarks>
    Private Sub ChooseRasterFile(ByVal rowIndex As Integer, ByVal colIndex As Integer)

        Dim rasterFilename As String = ChooseRasterFileName("Initial Conditions Raster File")

        If (rasterFilename Is Nothing) Then
            Return
        End If

        Using h As New HourGlass

            Dim rast As New StochasticTimeRaster

            Try
                RasterFiles.LoadRasterFile(rasterFilename, rast, RasterDataType.DTInteger)
            Catch e As GdalException
                FormsUtilities.ErrorMessageBox(e.Message)
                Return
            End Try

            EditICSpatialRecord(rast, colIndex, rowIndex, rasterFilename)

            ' Complain to the user if no projection associated with this Primary Stratum file
            If colIndex = PRIMARY_STRATUM_FILE_NAME_COLUMN_INDEX And rast.ProjectionString = "" Then
                Const sMsg As String = "There is no projection associated with this raster file. The model will still run but outputs will also have no projection."
                FormsUtilities.InformationMessageBox(sMsg)
            End If

            Me.RefreshControls()

        End Using

    End Sub

    ''' <summary>
    ''' Convert the Cell Area as specified in the raster units to Cell Area as specified in user-configurable Terminology Units
    ''' </summary>
    ''' <param name="srcCellArea">The Cell Area in the raster files native units</param>
    ''' <param name="srcSizeUnits">The native linear size units of the raster files</param>
    ''' <param name="destAreaUnits">The specified Area Units</param>
    ''' <returns>The calculated Cell Area</returns>
    ''' <remarks></remarks>
    Public Shared Function CalcCellArea(srcCellArea As Double, srcSizeUnits As String, destAreaUnits As TerminologyUnit) As Double

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

    Private Shared Function BuildLowerCaseLabel(ByVal label As String) As String

        Dim sb As New StringBuilder()
        Dim sp() As String = label.Split(CChar(" "))

        If (sp.Count <= 1) Then
            Return label
        Else

            For i As Integer = 0 To sp.Count - 1

                If (i = 0) Then
                    sb.AppendFormat(CultureInfo.CurrentCulture,
                        "{0} ", sp(i))

                Else

                    sb.AppendFormat(CultureInfo.CurrentCulture,
                        "{0} ", sp(i).ToLower(CultureInfo.CurrentCulture))

                End If

            Next

            Return sb.ToString.TrimEnd()

        End If

    End Function



    ''' <summary>
    ''' Handles the CheckChanged event for the CellSize checkbox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CheckBoxCellSizeOverride_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CheckBoxCellSizeOverride.CheckedChanged

        If (Me.m_InRefresh) Then
            Return
        End If

        Dim ds As DataSheet = Me.GetPropDataSheet()
        Dim dr As DataRow = ds.GetDataRow()

        If (dr Is Nothing) Then
            ' Dont allow Overide checkbox to be changed if no underlying record.
            Exit Sub
        End If

        ds.SetSingleRowData(DATASHEET_SPPIC_CELL_AREA_OVERRIDE_COLUMN_NAME, CheckBoxCellSizeOverride.Checked)
        Me.RefreshCalculatedValues()

    End Sub

    Private Sub TextBoxCellAreaCalc_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles TextBoxCellAreaCalc.KeyPress

        ' Only allow digits, single decimal point, and backspace.
        If IsNumeric(e.KeyChar) Then
            e.Handled = False
        ElseIf e.KeyChar = "." Then
            If InStr(CType(sender, System.Windows.Forms.TextBox).Text, ".") > 0 Then
                e.Handled = True
            Else
                e.Handled = False
            End If
        ElseIf Asc(e.KeyChar) = 8 Then
            e.Handled = False
        Else
            e.Handled = True
        End If

    End Sub

    ''' <summary>
    ''' Handles the TextChanged event for the TextBoxCellAreaCalc text box
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' If we are just refreshing the data then we don't want to do anything during this event.
    ''' </remarks>
    Private Sub TextBoxCellAreaCalc_TextChanged(sender As Object, e As System.EventArgs) Handles TextBoxCellAreaCalc.TextChanged

        If (Not Me.m_InRefresh) Then
            Me.m_CellAreaCalcHasChanges = True
        End If

    End Sub

    ''' <summary>
    ''' Handles the Validated event for the TextBoxCellAreaCalc text box
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' If we are just refreshing the data, or the text has not really changed, 
    ''' then we don't want to do anything during this event.
    ''' </remarks>
    Private Sub TextBoxCellAreaCalc_Validated(sender As Object, e As System.EventArgs) Handles TextBoxCellAreaCalc.Validated

        If (Me.m_InRefresh) Then
            Return
        End If

        If (Not Me.m_CellAreaCalcHasChanges) Then
            Return
        End If

        'Save the CellArea value
        Dim ds As DataSheet = Me.GetPropDataSheet()
        Dim dr As DataRow = ds.GetDataRow()
        If (dr Is Nothing) Then
            dr = ds.GetData.NewRow()
            ds.GetData.Rows.Add(dr)
        End If

        ds.BeginModifyRows()
        Dim cellArea As Double
        If Double.TryParse(Me.TextBoxCellAreaCalc.Text, cellArea) Then
            dr(DATASHEET_SPPIC_CELL_AREA_COLUMN_NAME) = cellArea
        Else
            dr(DATASHEET_SPPIC_CELL_AREA_COLUMN_NAME) = DBNull.Value
        End If
        ds.EndModifyRows()

        RefreshCalculatedValues()
        Me.m_CellAreaCalcHasChanges = False

    End Sub


    Private Sub OnGridRowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs)

        'DEVNOTE:TKR - This isnt very elegant, but is a short term fix to getting the Spatial Properties fields to refresh after an Import or a Delete All.
        RefreshControls()
    End Sub

End Class
