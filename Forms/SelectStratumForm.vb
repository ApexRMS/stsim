'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core
Imports System.Windows.Forms

''' <summary>
''' Select Stratum Form
''' </summary>
''' <remarks></remarks>
Class SelectStratumForm

    Private m_SelectedStratum As String

    ''' <summary>
    ''' Gets the name of the selected stratum
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property SelectedStratum As String
        Get
            Return Me.m_SelectedStratum
        End Get
    End Property

    ''' <summary>
    ''' Initializes this control
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Initialize(ByVal project As Project, ByVal selectedStratum As String)

        Me.DataGridViewStrata.BackgroundColor = Drawing.Color.White
        Me.DataGridViewStrata.PaintSelectionRectangle = False
        Me.DataGridViewStrata.PaintGridBorders = False
        Me.PanelGrid.ShowBorder = True

        Dim ds As DataSheet = project.GetDataSheet(DATASHEET_STRATA_NAME)
        Dim dv As New DataView(ds.GetData(), Nothing, ds.DisplayMember, DataViewRowState.CurrentRows)
        Dim AtLeastOneDesc As Boolean = False

        Me.DataGridViewStrata.Rows.Add(DIAGRAM_ALL_STRATA_DISPLAY_NAME, Nothing)

        For Each v As DataRowView In dv

            Dim n As String = CStr(v(ds.DisplayMember))
            Dim d As String = DataTableUtilities.GetDataStr(v(DATASHEET_DESCRIPTION_COLUMN_NAME))

            If (Not String.IsNullOrEmpty(d)) Then
                AtLeastOneDesc = True
            End If

            Me.DataGridViewStrata.Rows.Add(n, d)

        Next

        Me.ButtonOK.Enabled = (Me.DataGridViewStrata.Rows.Count > 0)
        Me.DataGridViewStrata.Enabled = (Me.DataGridViewStrata.Rows.Count > 0)
        Me.m_SelectedStratum = selectedStratum

        If (Not AtLeastOneDesc) Then
            Me.ColumnDescription.Visible = False
        End If

        Me.RefreshTitleBar(project)

    End Sub

    ''' <summary>
    ''' Refreshes the title bar with the correct terminology
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RefreshTitleBar(ByVal project As Project)

        Dim primary As String = Nothing
        Dim secondary As String = Nothing
        Dim ds As DataSheet = project.GetDataSheet(DATASHEET_TERMINOLOGY_NAME)

        GetStratumLabelTerminology(ds, primary, secondary)
        Me.Text = "Select " & primary

    End Sub

    ''' <summary>
    ''' Selects the stratum for the current row in the grid and exits the form
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectStratumAndExit()

        Debug.Assert(Me.DataGridViewStrata.SelectedRows.Count = 1)

        Me.DialogResult = DialogResult.OK
        Me.m_SelectedStratum = CStr(Me.DataGridViewStrata.SelectedRows(0).Cells(ColumnName.Name).Value)

    End Sub

    ''' <summary>
    ''' Handles the OK Button's Clicked event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ButtonOK_Click(sender As System.Object, e As System.EventArgs) Handles ButtonOK.Click
        Me.SelectStratumAndExit()
    End Sub

    ''' <summary>
    ''' Handles the DataGridView's CellDoubleClick event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DataGridViewStrata_CellDoubleClick(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridViewStrata.CellDoubleClick

        If (e.RowIndex >= 0) Then
            Me.SelectStratumAndExit()
        End If

    End Sub

    ''' <summary>
    ''' Handles the KeyDown event for the data grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DataGridViewStrata_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles DataGridViewStrata.KeyDown

        If (e.KeyCode = Keys.Return) Then

            Me.SelectStratumAndExit()
            e.Handled = True

        End If

    End Sub

    ''' <summary>
    ''' Handles the Shown Event for this form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SelectStratumForm_Shown(sender As System.Object, e As System.EventArgs) Handles MyBase.Shown

        Me.ActiveControl = Me.DataGridViewStrata
        Me.DataGridViewStrata.Focus()
        Me.DataGridViewStrata.StandardTab = True

        Me.DataGridViewStrata.ClearSelection()

        For Each dgr As DataGridViewRow In Me.DataGridViewStrata.Rows

            If (CStr(dgr.Cells(Me.ColumnName.Name).Value) = Me.m_SelectedStratum) Then

                dgr.Selected = True
                Me.DataGridViewStrata.CurrentCell = dgr.Cells(0)

                Exit For

            End If

        Next

    End Sub

End Class