'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

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

    Private m_InRefresh As Boolean
    Private m_CellAreaCalcHasChanges As Boolean
    Private m_ICSpatialFilesView As DataFeedView
    Private m_ICSpatialFilesDataGrid As DataGridView
    Private m_ICSpatialFilesDataSheet As InitialConditionsSpatialDataSheet
    Private m_ColumnsInitialized As Boolean
    Private m_HourGlass As HourGlass
    Private m_ReadOnlyColor As Color = Color.FromArgb(232, 232, 232)
    Private Delegate Sub DelegateNoArgs()

    Const ITERATION_COLUMN_INDEX = 0
    Const PRIMARY_STRATUM_FILE_NAME_COLUMN_INDEX As Integer = 1
    Const PRIMARY_STRATUM_BROWSE_COLUMN_INDEX As Integer = 2
    Const SECONDARY_STRATUM_FILE_NAME_COLUMN_INDEX As Integer = 3
    Const SECONDARY_STRATUM_BROWSE_COLUMN_INDEX As Integer = 4
    Const SCLASS_FILE_NAME_COLUMN_INDEX As Integer = 5
    Const SCLASS_BROWSE_COLUMN_INDEX As Integer = 6
    Const AGE_FILE_NAME_COLUMN_INDEX As Integer = 7
    Const AGE_BROWSE_COLUMN_INDEX As Integer = 8
    Const BROWSE_BUTTON_TEXT As String = "..."

    Protected Overrides Sub InitializeView()

        MyBase.InitializeView()

        Me.m_ICSpatialFilesView = (Me.Session.CreateMultiRowDataFeedView(Me.Scenario, Me.ControllingScenario))
        Me.m_ICSpatialFilesDataGrid = CType(Me.m_ICSpatialFilesView, MultiRowDataFeedView).GridControl
        Me.PanelInitialConditionSpatialFiles.Controls.Add(Me.m_ICSpatialFilesView)
        Me.LabelValidate.Visible = False

    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)

        If (disposing And Not Me.IsDisposed) Then

            If (Me.m_ICSpatialFilesDataSheet IsNot Nothing) Then

                RemoveHandler Me.m_ICSpatialFilesDataSheet.ValidatingRasters, AddressOf Me.OnValidatingRasters
                RemoveHandler Me.m_ICSpatialFilesDataSheet.RastersValidated, AddressOf Me.OnRastersValidated

            End If

            If (components IsNot Nothing) Then
                components.Dispose()
            End If

        End If

        MyBase.Dispose(disposing)

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
            Me.m_ICSpatialFilesDataGrid.Columns(PRIMARY_STRATUM_FILE_NAME_COLUMN_INDEX).DefaultCellStyle.BackColor = Me.m_ReadOnlyColor
            Me.m_ICSpatialFilesDataGrid.Columns(SECONDARY_STRATUM_FILE_NAME_COLUMN_INDEX).DefaultCellStyle.BackColor = Me.m_ReadOnlyColor
            Me.m_ICSpatialFilesDataGrid.Columns(SCLASS_FILE_NAME_COLUMN_INDEX).DefaultCellStyle.BackColor = Me.m_ReadOnlyColor
            Me.m_ICSpatialFilesDataGrid.Columns(AGE_FILE_NAME_COLUMN_INDEX).DefaultCellStyle.BackColor = Me.m_ReadOnlyColor

            Me.m_ColumnsInitialized = True

        End If

        Me.MonitorDataSheet(DATASHEET_TERMINOLOGY_NAME, AddressOf Me.OnTerminologyChanged, True)
        Me.m_ICSpatialFilesDataSheet = CType(Me.DataFeed.GetDataSheet(DATASHEET_SPIC_NAME), InitialConditionsSpatialDataSheet)

        AddHandler Me.m_ICSpatialFilesDataSheet.ValidatingRasters, AddressOf Me.OnValidatingRasters
        AddHandler Me.m_ICSpatialFilesDataSheet.RastersValidated, AddressOf Me.OnRastersValidated

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
        Me.m_ICSpatialFilesView.EnableView(enable)

    End Sub

    Private Sub OnValidatingRasters(ByVal sender As Object, ByVal e As EventArgs)

        Me.LabelValidate.Visible = True
        Application.DoEvents()

        Me.m_HourGlass = New HourGlass

        'A slight delay so the user can see this message even if the validation is fast
        Threading.Thread.Sleep(500)

    End Sub

    Private Sub OnRastersValidated(ByVal sender As Object, ByVal e As EventArgs)

        Me.LabelValidate.Visible = False
        Me.m_HourGlass.Dispose()

    End Sub

    Private Sub OnNewCellEnterAsync()

        Dim Row As Integer = Me.m_ICSpatialFilesDataGrid.CurrentCell.RowIndex
        Dim Col As Integer = Me.m_ICSpatialFilesDataGrid.CurrentCell.ColumnIndex

        Select Case Col

            Case PRIMARY_STRATUM_FILE_NAME_COLUMN_INDEX,
                 SECONDARY_STRATUM_FILE_NAME_COLUMN_INDEX,
                 SCLASS_FILE_NAME_COLUMN_INDEX,
                 AGE_FILE_NAME_COLUMN_INDEX

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

    Private Sub OnGridCellEnter(sender As Object, e As DataGridViewCellEventArgs)

        Select Case e.ColumnIndex

            Case PRIMARY_STRATUM_FILE_NAME_COLUMN_INDEX,
                 SECONDARY_STRATUM_FILE_NAME_COLUMN_INDEX,
                 SCLASS_FILE_NAME_COLUMN_INDEX,
                 AGE_FILE_NAME_COLUMN_INDEX

                Me.Session.MainForm.BeginInvoke(New DelegateNoArgs(AddressOf Me.OnNewCellEnterAsync), Nothing)

        End Select

    End Sub

    Private Sub OnGridCellMouseDown(ByVal sender As Object, ByVal e As DataGridViewCellMouseEventArgs)

        If (e.RowIndex >= 0) Then

            Select Case e.ColumnIndex

                Case PRIMARY_STRATUM_BROWSE_COLUMN_INDEX,
                     SECONDARY_STRATUM_BROWSE_COLUMN_INDEX,
                     SCLASS_BROWSE_COLUMN_INDEX,
                     AGE_BROWSE_COLUMN_INDEX

                    ChooseRasterFile(e.RowIndex, e.ColumnIndex - 1)

            End Select

        End If

    End Sub

    Private Sub OnGridKeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)

        If (Me.m_ICSpatialFilesDataGrid.CurrentCell IsNot Nothing) Then

            Select Case Me.m_ICSpatialFilesDataGrid.CurrentCell.ColumnIndex

                Case PRIMARY_STRATUM_BROWSE_COLUMN_INDEX,
                     SECONDARY_STRATUM_BROWSE_COLUMN_INDEX,
                     SCLASS_BROWSE_COLUMN_INDEX,
                     AGE_BROWSE_COLUMN_INDEX

                    If (e.KeyValue = Keys.Enter) Then

                        ChooseRasterFile(
                            Me.m_ICSpatialFilesDataGrid.CurrentCell.RowIndex,
                            Me.m_ICSpatialFilesDataGrid.CurrentCell.ColumnIndex - 1)

                        e.Handled = True

                    End If

            End Select

        End If

    End Sub

    Private Sub OnGridBindingComplete(sender As Object, e As DataGridViewBindingCompleteEventArgs)

        For Each dgr As DataGridViewRow In Me.m_ICSpatialFilesDataGrid.Rows

            dgr.Cells(PRIMARY_STRATUM_BROWSE_COLUMN_INDEX).Value = BROWSE_BUTTON_TEXT
            dgr.Cells(SECONDARY_STRATUM_BROWSE_COLUMN_INDEX).Value = BROWSE_BUTTON_TEXT
            dgr.Cells(SCLASS_BROWSE_COLUMN_INDEX).Value = BROWSE_BUTTON_TEXT
            dgr.Cells(AGE_BROWSE_COLUMN_INDEX).Value = BROWSE_BUTTON_TEXT

        Next

    End Sub

    Private Sub OnTerminologyChanged(e As DataSheetMonitorEventArgs)

        Dim Primary As String = Nothing
        Dim Secondary As String = Nothing
        Dim AmountLabel As String = Nothing
        Dim AmountUnits As TerminologyUnit = TerminologyUnit.None

        GetStratumLabelTerminology(e.DataSheet, Primary, Secondary)
        GetAmountLabelTerminology(e.DataSheet, AmountLabel, AmountUnits)

        Me.m_ICSpatialFilesDataGrid.Columns(PRIMARY_STRATUM_FILE_NAME_COLUMN_INDEX).HeaderText = BuildLowerCaseLabel(Primary)
        Me.m_ICSpatialFilesDataGrid.Columns(SECONDARY_STRATUM_FILE_NAME_COLUMN_INDEX).HeaderText = BuildLowerCaseLabel(Secondary)
        Me.LabelCalcTtlAmount.Text = String.Format(CultureInfo.CurrentCulture, "Total {0}:", AmountLabel.ToLower(CultureInfo.CurrentCulture))

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

        Dim drProp As DataRow = Me.DataFeed.GetDataSheet(DATASHEET_SPPIC_NAME).GetDataRow()

        If (drProp Is Nothing) Then
            Return
        End If

        Me.CheckBoxCellSizeOverride.Checked = DataTableUtilities.GetDataBool(drProp(DATASHEET_SPPIC_CELL_AREA_OVERRIDE_COLUMN_NAME))
        Me.CheckBoxCellSizeOverride.Enabled = True
        Me.CheckBoxCellSizeOverride.AutoCheck = True

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

        Dim drProp As DataRow = Me.DataFeed.GetDataSheet(DATASHEET_SPPIC_NAME).GetDataRow()

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
            cellAreaTU = InitialConditionsSpatialDataSheet.CalcCellArea(cellArea, srcSizeUnits, destUnitsVal)
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
    ''' Chooses a new raster file
    ''' </summary>
    ''' <remarks>
    ''' Just store the filename. For now, no path required. In the future may want to support absolute path, differentiated by x:\\
    ''' </remarks>
    Private Sub ChooseRasterFile(ByVal rowIndex As Integer, ByVal colIndex As Integer)

        Dim rasterFilename As String = ChooseRasterFileName("Initial Conditions Raster File", Me)

        If (rasterFilename Is Nothing) Then
            Return
        End If

        Using h As New HourGlass

            Dim rast As New StochasticTimeRaster

            Try

                RasterFiles.LoadRasterFile(rasterFilename, rast, RasterDataType.DTInteger)

                'Complain to the user if no projection associated with this Primary Stratum file

                If colIndex = PRIMARY_STRATUM_FILE_NAME_COLUMN_INDEX And rast.ProjectionString = "" Then
                    Const sMsg As String = "There is no projection associated with this raster file. The model will still run but outputs will also have no projection."
                    FormsUtilities.InformationMessageBox(sMsg)
                End If

                SetICSpatialFile(rowIndex, colIndex, rasterFilename)

            Catch e As GdalException
                FormsUtilities.ErrorMessageBox(e.Message)
                Return
            End Try

        End Using

    End Sub

    Private Shared Function BuildLowerCaseLabel(ByVal label As String) As String

        Dim sb As New StringBuilder()
        Dim sp() As String = label.Split(CChar(" "))

        If (sp.Count <= 1) Then
            Return label
        Else

            For i As Integer = 0 To sp.Count - 1

                If (i = 0) Then
                    sb.AppendFormat(CultureInfo.CurrentCulture, "{0} ", sp(i))
                Else
                    sb.AppendFormat(CultureInfo.CurrentCulture, "{0} ", sp(i).ToLower(CultureInfo.CurrentCulture))
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
    Private Sub CheckBoxCellSizeOverride_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBoxCellSizeOverride.CheckedChanged

        If (Me.m_InRefresh) Then
            Return
        End If

        Dim ds As DataSheet = Me.DataFeed.GetDataSheet(DATASHEET_SPPIC_NAME)
        Dim dr As DataRow = ds.GetDataRow()

        If (dr Is Nothing) Then
            ' Dont allow Overide checkbox to be changed if no underlying record.
            Exit Sub
        End If

        ds.SetSingleRowData(DATASHEET_SPPIC_CELL_AREA_OVERRIDE_COLUMN_NAME, CheckBoxCellSizeOverride.Checked)
        Me.RefreshCalculatedValues()

    End Sub

    Private Sub TextBoxCellAreaCalc_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBoxCellAreaCalc.KeyPress

        ' Only allow digits, single decimal point, and backspace.
        If IsNumeric(e.KeyChar) Then
            e.Handled = False
        ElseIf e.KeyChar = "." Then
            If InStr(CType(sender, TextBox).Text, ".") > 0 Then
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
        Dim ds As DataSheet = Me.DataFeed.GetDataSheet(DATASHEET_SPPIC_NAME)
        Dim dr As DataRow = ds.GetDataRow()

        If (dr Is Nothing) Then

            ds.BeginAddRows()
            dr = ds.GetData.NewRow()
            ds.GetData.Rows.Add(dr)
            ds.EndAddRows()

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

End Class
