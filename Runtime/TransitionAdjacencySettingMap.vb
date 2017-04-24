'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Class TransitionAdjacencySettingMap

    Private m_Map As New Dictionary(Of Integer, TransitionAdjacencySetting)

    Public Sub New(ByVal settings As TransitionAdjacencySettingCollection)

        For Each s As TransitionAdjacencySetting In settings
            Me.AddItem(s)
        Next

    End Sub

    Public Sub AddItem(ByVal setting As TransitionAdjacencySetting)

        If (Not Me.m_Map.ContainsKey(setting.TransitionGroupId)) Then
            Me.m_Map.Add(setting.TransitionGroupId, setting)
        End If

    End Sub

    Public Function GetItem(ByVal transitionGroupId As Integer) As TransitionAdjacencySetting

        If (Me.m_Map.ContainsKey(transitionGroupId)) Then
            Return Me.m_Map(transitionGroupId)
        Else
            Return Nothing
        End If

    End Function

End Class
