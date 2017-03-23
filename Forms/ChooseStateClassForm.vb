'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.Core
Imports System.Windows.Forms

''' <summary>
''' Choose State Class form
''' </summary>
''' <remarks></remarks>
Class ChooseStateClassForm

    Private m_Diagram As TransitionDiagram
    Private m_TerminologyDataSheet As DataSheet
    Private m_StateLabelXDataSheet As DataSheet
    Private m_StateLabelYDataSheet As DataSheet
    Private m_ChosenStateLabelX As BaseValueDisplayListItem
    Private m_ChosenStateLabelY As BaseValueDisplayListItem
    Private m_EditMode As Boolean

    ''' <summary>
    ''' Gets the chosen state label x combo item
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ChosenStateLabelX As BaseValueDisplayListItem
        Get
            Return Me.m_ChosenStateLabelX
        End Get
    End Property

    ''' <summary>
    ''' Gets the chosen state label y combo item
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ChosenStateLabelY As BaseValueDisplayListItem
        Get
            Return Me.m_ChosenStateLabelY
        End Get
    End Property

    ''' <summary>
    ''' Initializes this form
    ''' </summary>
    ''' <param name="Diagram"></param>
    ''' <param name="dataFeed"></param>
    ''' <param name="editMode"></param>
    ''' <remarks></remarks>
    Public Function Initialize(
        ByVal diagram As TransitionDiagram,
        ByVal dataFeed As DataFeed,
        ByVal editMode As Boolean) As Boolean

        Me.m_Diagram = diagram

        Me.m_TerminologyDataSheet = dataFeed.Project.GetDataSheet(DATASHEET_TERMINOLOGY_NAME)
        Me.m_StateLabelXDataSheet = dataFeed.Project.GetDataSheet(DATASHEET_STATE_LABEL_X_NAME)
        Me.m_StateLabelYDataSheet = dataFeed.Project.GetDataSheet(DATASHEET_STATE_LABEL_Y_NAME)

        Me.FillComboBoxes()

        If (Me.ComboBoxStateLabelX.Items.Count = 0) Then

            FormsUtilities.ErrorMessageBox(ERROR_DIAGRAM_NO_STATE_LABEL_X_VALUES)
            Return False

        ElseIf (Me.ComboBoxStateLabelY.Items.Count = 0) Then

            FormsUtilities.ErrorMessageBox(ERROR_DIAGRAM_NO_STATE_LABEL_Y_VALUES)
            Return False

        End If

        Me.ResetSlxAndSlyLabels()
        Me.m_EditMode = editMode

        If (Me.m_EditMode) Then

            Me.Text = "Edit State Class"

            Dim EditShape As StateClassShape = CType(diagram.SelectedShapes.First, StateClassShape)
            Me.SelectComboValues(EditShape.StateLabelXId, EditShape.StateLabelYId)

        Else
            Me.Text = "Add State Class"
        End If

        Return True

    End Function

    ''' <summary>
    ''' Fills the combo boxes
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FillComboBoxes()

        Dim dvct As New DataView(Me.m_StateLabelXDataSheet.GetData(), Nothing,
            Me.m_StateLabelXDataSheet.DisplayMember, DataViewRowState.CurrentRows)

        For Each drv As DataRowView In dvct

            Dim Id As Integer = CInt(drv.Row(Me.m_StateLabelXDataSheet.ValueMember))
            Dim Name As String = CStr(drv.Row(Me.m_StateLabelXDataSheet.DisplayMember))

            Me.ComboBoxStateLabelX.Items.Add(New BaseValueDisplayListItem(Id, Name))

        Next

        Dim dvss As New DataView(Me.m_StateLabelYDataSheet.GetData(), Nothing,
            Me.m_StateLabelYDataSheet.DisplayMember, DataViewRowState.CurrentRows)

        For Each drv As DataRowView In dvss

            Dim Id As Integer = CInt(drv.Row(Me.m_StateLabelYDataSheet.ValueMember))
            Dim Name As String = CStr(drv.Row(Me.m_StateLabelYDataSheet.DisplayMember))

            Me.ComboBoxStateLabelY.Items.Add(New BaseValueDisplayListItem(Id, Name))

        Next

        If (Me.ComboBoxStateLabelX.Items.Count > 0) Then
            Me.ComboBoxStateLabelX.SelectedIndex = 0
        End If

        If (Me.ComboBoxStateLabelY.Items.Count > 0) Then
            Me.ComboBoxStateLabelY.SelectedIndex = 0
        End If

    End Sub

    ''' <summary>
    ''' Selects the specified combo values
    ''' </summary>
    ''' <param name="stateLabelXId"></param>
    ''' <param name="stateLabelYId"></param>
    ''' <remarks></remarks>
    Private Sub SelectComboValues(ByVal stateLabelXId As Integer, ByVal stateLabelYId As Integer)

        Debug.Assert(Me.m_EditMode)

        For Each item As BaseValueDisplayListItem In Me.ComboBoxStateLabelX.Items

            If (item.Value = stateLabelXId) Then

                Me.ComboBoxStateLabelX.SelectedItem = item
                Exit For

            End If

        Next

        For Each item As BaseValueDisplayListItem In Me.ComboBoxStateLabelY.Items

            If (item.Value = stateLabelYId) Then

                Me.ComboBoxStateLabelY.SelectedItem = item
                Exit For

            End If

        Next

    End Sub

    ''' <summary>
    ''' Resets the state label x and y form labels
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ResetSlxAndSlyLabels()

        Dim slxlabel As String = Nothing
        Dim slylabel As String = Nothing

        GetStateLabelTerminology(Me.m_TerminologyDataSheet, slxlabel, slylabel)

        Me.LabelStateLabelX.Text = slxlabel & ":"
        Me.LabelStateLabelY.Text = slylabel & ":"

    End Sub

    ''' <summary>
    ''' Determines if the specified state class exists in the diagram
    ''' </summary>
    ''' <param name="slxid"></param>
    ''' <param name="slyid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function StateClassInDiagram(ByVal slxid As Integer, ByVal slyid As Integer) As Boolean

        For Each s As StateClassShape In Me.m_Diagram.Shapes

            If (Me.m_Diagram.StratumId.HasValue) Then

                If (Not s.StratumIdSource.HasValue) Then
                    Continue For
                End If

            End If

            If (s.StateLabelXId = slxid And s.StateLabelYId = slyid) Then
                Return True
            End If

        Next

        Return False

    End Function

    ''' <summary>
    ''' Handles the OK button Clicked event
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ButtonOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOK.Click

        Me.m_ChosenStateLabelX = CType(Me.ComboBoxStateLabelX.Items(
                Me.ComboBoxStateLabelX.SelectedIndex), BaseValueDisplayListItem)

        Me.m_ChosenStateLabelY = CType(Me.ComboBoxStateLabelY.Items(
                Me.ComboBoxStateLabelY.SelectedIndex), BaseValueDisplayListItem)

        If (Not Me.m_EditMode) Then

            If (Me.StateClassInDiagram(Me.m_ChosenStateLabelX.Value, Me.m_ChosenStateLabelY.Value)) Then

                FormsUtilities.ErrorMessageBox(ERROR_DIAGRAM_STATE_CLASS_EXISTS)
                Return

            End If

        End If

        Me.DialogResult = DialogResult.OK
        Me.Close()

    End Sub

    ''' <summary>
    ''' Handles the Cancel button Clicked event
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click

        Me.DialogResult = DialogResult.Cancel
        Me.Close()

    End Sub

End Class



