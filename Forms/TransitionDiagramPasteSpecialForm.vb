'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Imports System.Windows.Forms

Class TransitionDiagramPasteSpecialForm

    Public ReadOnly Property PasteTransitionsAll() As Boolean
        Get
            Return Me.RadioButtonPasteAll.Checked
        End Get
    End Property

    Public ReadOnly Property PasteTransitionsBetween() As Boolean
        Get
            Return Me.RadioButtonPasteBetween.Checked
        End Get
    End Property

    Public ReadOnly Property PasteTransitionsNone() As Boolean
        Get
            Return Me.RadioButtonPasteNone.Checked
        End Get
    End Property

    Public ReadOnly Property PasteTransitionsDeterministic() As Boolean
        Get
            Return Me.CheckboxPasteDeterministic.Checked
        End Get
    End Property

    Public ReadOnly Property PasteTransitionsProbabilistic() As Boolean
        Get
            Return Me.CheckboxPasteProbabilistic.Checked
        End Get
    End Property

    Private Sub OnRadioButtonOptionChanged()

        Me.CheckboxPasteDeterministic.Enabled = (Not Me.RadioButtonPasteNone.Checked)
        Me.CheckboxPasteProbabilistic.Enabled = (Not Me.RadioButtonPasteNone.Checked)

        If (Me.RadioButtonPasteNone.Checked = False) Then

            If (Me.CheckboxPasteDeterministic.Checked = False And _
                Me.CheckboxPasteProbabilistic.Checked = False) Then

                Me.CheckboxPasteDeterministic.Checked = True
                Me.CheckboxPasteProbabilistic.Checked = True

            End If

        End If

    End Sub

    Private Sub OnCheckboxChanged()

        If (Me.CheckboxPasteDeterministic.Checked = False And _
            Me.CheckboxPasteProbabilistic.Checked = False) Then

            Me.RadioButtonPasteNone.Checked = True

        End If

    End Sub

    Private Sub ButtonOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOK.Click

        Me.DialogResult = DialogResult.OK
        Me.Close()

    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click

        Me.DialogResult = DialogResult.Cancel
        Me.Close()

    End Sub

    Private Sub PasteNoneRadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonPasteNone.CheckedChanged
        Me.OnRadioButtonOptionChanged()
    End Sub

    Private Sub PasteBetweenRadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonPasteBetween.CheckedChanged
        Me.OnRadioButtonOptionChanged()
    End Sub

    Private Sub PasteAllRadioButton_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButtonPasteAll.CheckedChanged
        Me.OnRadioButtonOptionChanged()
    End Sub

    Private Sub PasteDeterministicCheckbox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckboxPasteDeterministic.CheckedChanged
        Me.OnCheckboxChanged()
    End Sub

    Private Sub PasteProbabilisticCheckbox_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckboxPasteProbabilistic.CheckedChanged
        Me.OnCheckboxChanged()
    End Sub

End Class

