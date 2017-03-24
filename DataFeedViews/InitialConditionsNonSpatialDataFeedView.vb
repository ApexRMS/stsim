'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.Core
Imports System.Globalization
Imports SyncroSim.Core.Forms
Imports System.Windows.Forms
Imports System.Reflection

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class InitialConditionsNonSpatialDataFeedView

    Protected Overrides Sub InitializeView()

        MyBase.InitializeView()

        Dim v As DataFeedView = Me.Session.CreateMultiRowDataFeedView(
            Me.Scenario, Me.ControllingScenario)

        Me.PanelDistribution.Controls.Add(v)

    End Sub

    Public Overrides Sub LoadDataFeed(ByVal dataFeed As DataFeed)

        MyBase.LoadDataFeed(dataFeed)

        Me.SetTextBoxBinding(Me.TextBoxTotalAmount, DATASHEET_NSIC_TOTAL_AMOUNT_COLUMN_NAME)
        Me.SetTextBoxBinding(Me.TextBoxNumCells, DATASHEET_NSIC_NUM_CELLS_COLUMN_NAME)
        Me.SetCheckBoxBinding(Me.CheckBoxCalcFromDist, DATASHEET_NSIC_CALC_FROM_DIST_COLUMN_NAME)

        Dim v As DataFeedView = CType(Me.PanelDistribution.Controls(0), DataFeedView)
        v.LoadDataFeed(dataFeed, DATASHEET_NSIC_DISTRIBUTION_NAME)

        Me.RefreshBoundControls()

        If (Me.CheckBoxCalcFromDist.Checked) Then
            Me.SetNumCellsFromDistribution()
        End If

        Me.CalculateCellSize()

    End Sub

    Protected Overrides Sub OnDataSheetChanged(e As DataSheetMonitorEventArgs)

        MyBase.OnDataSheetChanged(e)

        Dim amountlabel As String = Nothing
        Dim units As TerminologyUnit
        Dim unitsLbl As String = Nothing

        GetAmountLabelTerminology(e.DataSheet, amountlabel, units)
        unitsLbl = TerminologyUnitToString(units).ToLower(CultureInfo.CurrentCulture)

        Me.LabelTotalAmount.Text = String.Format(CultureInfo.CurrentCulture, "Total ({0}):", unitsLbl)
        Me.LabelCellSize.Text = String.Format(CultureInfo.CurrentCulture, "Cell size ({0}):", unitsLbl)
        Me.TextBoxNumCells.Enabled = (Me.ShouldEnableView And (Not Me.CheckBoxCalcFromDist.Checked))

    End Sub

    Public Overrides Sub EnableView(enable As Boolean)

        If (Me.PanelDistribution.Controls.Count > 0) Then

            Dim v As DataFeedView = CType(Me.PanelDistribution.Controls(0), DataFeedView)
            v.EnableView(enable)

        End If

        Me.TextBoxTotalAmount.Enabled = enable
        Me.TextBoxNumCells.Enabled = (enable And (Not Me.CheckBoxCalcFromDist.Checked))
        Me.CheckBoxCalcFromDist.Enabled = enable
        Me.ButtonClearAll.Enabled = enable

    End Sub

    Protected Overrides Sub OnRowsAdded(sender As Object, e As Core.DataSheetRowEventArgs)

        MyBase.OnRowsAdded(sender, e)
        Me.RecomputeNumCellsForDistributionChange(sender)

    End Sub

    Protected Overrides Sub OnRowsDeleted(sender As Object, e As Core.DataSheetRowEventArgs)

        MyBase.OnRowsDeleted(sender, e)
        Me.RecomputeNumCellsForDistributionChange(sender)

    End Sub

    Protected Overrides Sub OnRowsModified(sender As Object, e As Core.DataSheetRowEventArgs)

        MyBase.OnRowsModified(sender, e)
        Me.RecomputeNumCellsForDistributionChange(sender)

    End Sub

    Protected Overrides Sub OnBoundTextBoxChanged(textBox As TextBox, columnName As String)

        MyBase.OnBoundTextBoxChanged(textBox, columnName)
        Me.CalculateCellSize()

    End Sub

    Protected Overrides Sub OnBoundCheckBoxChanged(checkBox As CheckBox, columnName As String)

        MyBase.OnBoundCheckBoxChanged(checkBox, columnName)

        If (Me.CheckBoxCalcFromDist.Checked) Then

            Me.TextBoxNumCells.Enabled = False
            Me.SetNumCellsFromDistribution()

        Else

            Me.TextBoxNumCells.Enabled = True
            Me.SetTextBoxData(Me.TextBoxNumCells, Nothing)

        End If

        Me.CalculateCellSize()

    End Sub

    Private Sub RecomputeNumCellsForDistributionChange(ByVal sender As Object)

        Dim ds As DataSheet = Me.DataFeed.GetDataSheet(DATASHEET_NSIC_DISTRIBUTION_NAME)

        If (sender Is ds And Me.Scenario Is Me.ControllingScenario) Then

            If (Me.CheckBoxCalcFromDist.Checked) Then

                Me.SetNumCellsFromDistribution()
                Me.CalculateCellSize()

            End If

        End If

    End Sub

    Private Sub SetNumCellsFromDistribution()

        Debug.Assert(Me.CheckBoxCalcFromDist.Checked)

        Dim NumCells As Integer = Me.CalculateNumCellsFromDistribution()

        If (NumCells > 0) Then
            Me.SetTextBoxData(Me.TextBoxNumCells, NumCells.ToString("N4", CultureInfo.CurrentCulture))
        Else
            Me.SetTextBoxData(Me.TextBoxNumCells, Nothing)
        End If

    End Sub

    Private Function CalculateNumCellsFromDistribution() As Integer

        Dim NumCells As Integer = 0
        Dim dt As DataTable = Me.DataFeed.GetDataSheet(DATASHEET_NSIC_DISTRIBUTION_NAME).GetData()

        ' Use just the lowest(1st iteration) entered to deal with multiple iterations
        Dim minIteration As Integer = 0
        For Each dr As DataRow In dt.Rows

            If (dr.RowState <> DataRowState.Deleted) Then
                If IsDBNull(dr(DATASHEET_ITERATION_COLUMN_NAME)) Then
                    minIteration = 0
                Else
                    minIteration = Math.Min(minIteration, CInt(dr(DATASHEET_ITERATION_COLUMN_NAME)))
                End If

            End If

        Next

        For Each dr As DataRow In dt.Rows

            If (dr.RowState <> DataRowState.Deleted) Then

                Dim iteration = CInt(IIf(IsDBNull(dr(DATASHEET_ITERATION_COLUMN_NAME)), 0, dr(DATASHEET_ITERATION_COLUMN_NAME)))
                If iteration = minIteration Then
                    Dim val As Double = CDbl(dr(DATASHEET_NSIC_DISTRIBUTION_RELATIVE_AMOUNT_COLUMN_NAME))
                    NumCells += CInt(Math.Round(val))
                End If

            End If

        Next

        Return NumCells

    End Function

    Private Sub CalculateCellSize()

        Dim ns As String = Me.TextBoxNumCells.Text.Trim()
        Dim ta As String = Me.TextBoxTotalAmount.Text.Trim()

        If (String.IsNullOrEmpty(ns) Or String.IsNullOrEmpty(ta)) Then
            Me.TextBoxCellSize.Text = Nothing
            Return
        End If

        Dim TotalAmount As Double = 0.0
        Dim NumCells As Integer = 0

        If (Not Double.TryParse(ta, NumberStyles.Any, CultureInfo.InvariantCulture, TotalAmount)) Then
            Me.TextBoxCellSize.Text = Nothing
            Return
        End If

        If (Not Integer.TryParse(ns, NumberStyles.Any, CultureInfo.InvariantCulture, NumCells)) Then
            Me.TextBoxCellSize.Text = Nothing
            Return
        End If

        Dim CellSize As Double

        If (TotalAmount = 0.0 Or NumCells = 0.0) Then
            CellSize = 0.0
        Else
            CellSize = TotalAmount / NumCells
        End If

        Me.TextBoxCellSize.Text = CellSize.ToString("N4", CultureInfo.CurrentCulture)

    End Sub

    Private Sub ButtonClearAll_Click(sender As Object, e As EventArgs) Handles ButtonClearAll.Click

        Me.ResetBoundControls()
        Me.DataFeed.DataSheets(DATASHEET_NSIC_NAME).ClearData()
        Me.TextBoxCellSize.Text = Nothing

    End Sub

End Class
