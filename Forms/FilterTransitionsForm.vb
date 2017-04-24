'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports System.Windows.Forms

Class FilterTransitionsForm

    Private Sub ButtonOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonOK.Click

        Me.DialogResult = DialogResult.OK
        Me.Validate()
        Me.Close()

    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click

        Me.DialogResult = DialogResult.Cancel
        Me.Validate()
        Me.Close()

    End Sub

    Private Sub ProbabilisticTransitionsCheckbox_CheckedChanged(
        ByVal sender As System.Object,
        ByVal e As System.EventArgs) Handles CheckboxProbabilisticTransitions.CheckedChanged

        Me.CheckBoxPanelMain.IsReadOnly = (Not Me.CheckboxProbabilisticTransitions.Checked)

    End Sub

End Class



