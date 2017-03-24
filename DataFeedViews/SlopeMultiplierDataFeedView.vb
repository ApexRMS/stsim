'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports System.IO
Imports SyncroSim.Core
Imports SyncroSim.Core.Forms
Imports SyncroSim.Common.Forms
Imports System.Reflection

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class SlopeMultiplierDataFeedView

    Protected Overrides Sub InitializeView()

        MyBase.InitializeView()

        Dim v As DataFeedView =
            Me.Session.CreateMultiRowDataFeedView(Me.Scenario, Me.ControllingScenario)

        Me.PanelMultipliers.Controls.Add(v)

    End Sub

    Public Overrides Sub LoadDataFeed(dataFeed As Core.DataFeed)

        MyBase.LoadDataFeed(dataFeed)

        Dim v As DataFeedView = CType(Me.PanelMultipliers.Controls(0), DataFeedView)
        v.LoadDataFeed(dataFeed, DATASHEET_TRANSITION_SLOPE_MULTIPLIER_NAME)

    End Sub

    Public Overrides Sub EnableView(enable As Boolean)

        If (Me.PanelMultipliers.Controls.Count > 0) Then

            Dim v As DataFeedView = CType(Me.PanelMultipliers.Controls(0), DataFeedView)
            v.EnableView(enable)

        End If

        Me.TextBoxDEMFilename.Enabled = enable

        If (enable) Then
            Me.EnableButtons()
        Else
            Me.ButtonBrowse.Enabled = False
            Me.ButtonClear.Enabled = False
        End If

        Me.LabelDEM.Enabled = enable
        Me.LabelTMV.Enabled = enable

    End Sub

    Public Overrides Sub RefreshControls()

        MyBase.RefreshControls()

        Me.TextBoxDEMFilename.Text = Nothing

        Dim dr As DataRow = Me.GetDataRow()

        If (dr Is Nothing) Then
            Return
        End If

        Me.TextBoxDEMFilename.Text =
            CStr(dr(DATASHEET_DIGITAL_ELEVATION_MODEL_FILE_NAME_COLUMN_NAME))

    End Sub

    Private Sub EnableButtons()

        Me.ButtonBrowse.Enabled = True
        Me.ButtonClear.Enabled = False

        Dim dr As DataRow = Me.GetDataRow()

        If (dr Is Nothing) Then
            Return
        End If

        If (dr(DATASHEET_DIGITAL_ELEVATION_MODEL_FILE_NAME_COLUMN_NAME) IsNot DBNull.Value) Then

            Me.ButtonBrowse.Enabled = False
            Me.ButtonClear.Enabled = True

        End If

    End Sub

    Private Function GetDataSheet() As DataSheet

        If (Me.DataFeed IsNot Nothing) Then
            Return Me.DataFeed.GetDataSheet(DATASHEET_DIGITAL_ELEVATION_MODEL_NAME)
        Else
            Return Nothing
        End If

    End Function

    Private Function GetDataRow() As DataRow

        Dim ds As DataSheet = Me.GetDataSheet()

        If (ds IsNot Nothing) Then
            Return ds.GetDataRow()
        Else
            Return Nothing
        End If

    End Function

    Private Sub ButtonBrowse_Click(sender As System.Object, e As System.EventArgs) Handles ButtonBrowse.Click

        Dim RasterFile As String = ChooseRasterFileName("Digital Elevation Model File")

        If (RasterFile Is Nothing) Then
            Return
        End If

        Using h As New HourGlass

            Dim ds As DataSheet = Me.GetDataSheet()
            Dim dr As DataRow = ds.GetDataRow()
            Dim RasterFileName As String = Path.GetFileName(RasterFile)

            If (dr Is Nothing) Then

                dr = ds.GetData.NewRow()

                ds.BeginAddRows()
                dr(DATASHEET_DIGITAL_ELEVATION_MODEL_FILE_NAME_COLUMN_NAME) = RasterFileName
                ds.GetData.Rows.Add(dr)
                ds.EndAddRows()

            Else

                ds.BeginModifyRows()
                dr(DATASHEET_DIGITAL_ELEVATION_MODEL_FILE_NAME_COLUMN_NAME) = RasterFileName
                ds.EndModifyRows()

            End If

            ds.AddExternalInputFile(RasterFile)

            Me.RefreshControls()
            Me.EnableButtons()

        End Using

    End Sub

    Private Sub ButtonClear_Click(sender As System.Object, e As System.EventArgs) Handles ButtonClear.Click

        Using h As New HourGlass

            Dim ds As DataSheet = GetDataSheet()

            ds.ClearData()
            Me.RefreshControls()
            Me.EnableButtons()

        End Using

    End Sub

End Class
