'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.Core
Imports System.Windows.Forms

Class PasteStrataForm

    Private m_SelectedStrata As Dictionary(Of String, Boolean)

    Public ReadOnly Property SelectedStrata As Dictionary(Of String, Boolean)
        Get
            Return Me.m_SelectedStrata
        End Get
    End Property

    Public ReadOnly Property MergeDependencies As Boolean
        Get
            Return Me.CheckBoxMergeDeps.Checked
        End Get
    End Property

    Public Sub Initialize(ByVal project As Project, ByVal enableMergeDeps As Boolean)

        Me.DataGridViewStrata.BackgroundColor = Drawing.Color.White
        Me.DataGridViewStrata.PaintSelectionRectangle = False
        Me.DataGridViewStrata.PaintGridBorders = False
        Me.DataGridViewStrata.MultiSelect = True
        Me.DataGridViewStrata.StandardTab = True
        Me.PanelGrid.ShowBorder = True
        Me.CheckBoxMergeDeps.Enabled = enableMergeDeps

        Dim ds As DataSheet = project.GetDataSheet(DATASHEET_STRATA_NAME)
        Dim dv As New DataView(ds.GetData(), Nothing, ds.DisplayMember, DataViewRowState.CurrentRows)
        Dim AtLeastOneDesc As Boolean = False

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

        If (Not AtLeastOneDesc) Then
            Me.ColumnDescription.Visible = False
        End If

    End Sub

    Private Sub SelectStratumAndExit()

        Me.m_SelectedStrata = New Dictionary(Of String, Boolean)

        For Each dgr As DataGridViewRow In Me.DataGridViewStrata.SelectedRows
            SelectedStrata.Add(CStr(dgr.Cells(ColumnName.Name).Value), True)
        Next

        Me.DialogResult = DialogResult.OK

    End Sub

    Private Sub SelectAllStrataAndFocusGrid()

        Me.DataGridViewStrata.SelectAll()
        Me.ActiveControl = Me.DataGridViewStrata

    End Sub

    Private Sub ButtonSelectAllStrata_Click(sender As System.Object, e As System.EventArgs) Handles ButtonSelectAllStrata.Click
        Me.SelectAllStrataAndFocusGrid()
    End Sub

    Private Sub ButtonOK_Click(sender As System.Object, e As System.EventArgs) Handles ButtonOK.Click
        Me.SelectStratumAndExit()
    End Sub

    Private Sub PasteStrataForm_Shown(sender As System.Object, e As System.EventArgs) Handles MyBase.Shown
        Me.SelectAllStrataAndFocusGrid()
    End Sub

    Private Sub DataGridViewStrata_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles DataGridViewStrata.KeyDown

        If (e.KeyCode = Keys.Return) Then
            Me.SelectStratumAndExit()
            e.Handled = True
        End If

    End Sub

End Class







