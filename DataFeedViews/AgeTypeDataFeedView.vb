'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports System.Reflection
Imports SyncroSim.Common.Forms

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class AgeTypeDataFeedView

    Public Overrides Sub LoadDataFeed(dataFeed As Core.DataFeed)

        MyBase.LoadDataFeed(dataFeed)

        Me.SetTextBoxBinding(Me.TextBoxFrequency, DATASHEET_AGE_TYPE_FREQUENCY_COLUMN_NAME)
        Me.SetTextBoxBinding(Me.TextBoxMaximum, DATASHEET_AGE_TYPE_MAXIMUM_COLUMN_NAME)

        Me.RefreshBoundControls()

    End Sub

    Protected Overrides Sub OnBoundTextBoxValidated(textBox As System.Windows.Forms.TextBox, columnName As String, newValue As String)

        Using h As New HourGlass
            MyBase.OnBoundTextBoxValidated(textBox, columnName, newValue)
        End Using

    End Sub

End Class
