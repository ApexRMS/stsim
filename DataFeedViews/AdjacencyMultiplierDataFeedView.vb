'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports System.Reflection
Imports SyncroSim.Core.Forms

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class AdjacencyMultiplierDataFeedView

    Protected Overrides Sub InitializeView()

        MyBase.InitializeView()

        Dim v1 As DataFeedView = Me.Session.CreateMultiRowDataFeedView(Me.Scenario, Me.ControllingScenario)
        Me.PanelSettings.Controls.Add(v1)

        Dim v2 As DataFeedView = Me.Session.CreateMultiRowDataFeedView(Me.Scenario, Me.ControllingScenario)
        Me.PanelMultipliers.Controls.Add(v2)

    End Sub

    Public Overrides Sub LoadDataFeed(dataFeed As Core.DataFeed)

        MyBase.LoadDataFeed(dataFeed)

        Dim v1 As DataFeedView = CType(Me.PanelSettings.Controls(0), DataFeedView)
        v1.LoadDataFeed(dataFeed, DATASHEET_TRANSITION_ADJACENCY_SETTING_NAME)

        Dim v2 As DataFeedView = CType(Me.PanelMultipliers.Controls(0), DataFeedView)
        v2.LoadDataFeed(dataFeed, DATASHEET_TRANSITION_ADJACENCY_MULTIPLIER_NAME)

    End Sub

    Public Overrides Sub EnableView(enable As Boolean)

        If (Me.PanelSettings.Controls.Count > 0) Then

            Dim v As DataFeedView = CType(Me.PanelSettings.Controls(0), DataFeedView)
            v.EnableView(enable)

        End If

        If (Me.PanelMultipliers.Controls.Count > 0) Then

            Dim v As DataFeedView = CType(Me.PanelMultipliers.Controls(0), DataFeedView)
            v.EnableView(enable)

        End If

        Me.LabelSettings.Enabled = enable
        Me.LabelMultipliers.Enabled = enable

    End Sub

End Class
