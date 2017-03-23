'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.Common.Forms

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
