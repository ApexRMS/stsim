'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.Core
Imports SyncroSim.Common

Friend Class TstTransitionGroupMap
    Inherits STSimMapBase

    Private m_Map As New MultiLevelKeyMap3(Of TstTransitionGroup)

    Public Sub New(ByVal scenario As Scenario)
        MyBase.New(scenario)
    End Sub

    Public Function GetGroup(
        ByVal transitionTypeId As Integer,
        ByVal stratumId As Integer,
        ByVal secondaryStratumId As Nullable(Of Integer)) As TstTransitionGroup

        Return Me.m_Map.GetItem(
            transitionTypeId,
            stratumId,
            secondaryStratumId)

    End Function

    Public Sub AddGroup(
        ByVal transitionTypeId As Integer,
        ByVal stratumId As Nullable(Of Integer),
        ByVal secondaryStratumId As Nullable(Of Integer),
        ByVal item As TstTransitionGroup)

        Dim v As TstTransitionGroup = Me.m_Map.GetItemExact(transitionTypeId, stratumId, secondaryStratumId)

        If (v IsNot Nothing) Then

            Dim template As String =
                "A duplicate Time-Since-Transition Group was detected: More information:" & vbCrLf &
                "Transition Type={0}, {1}={2}, {3}={4}."

            ExceptionUtils.ThrowArgumentException(
                template,
                Me.GetTransitionTypeName(transitionTypeId),
                Me.PrimaryStratumLabel,
                Me.GetStratumName(stratumId),
                Me.SecondaryStratumLabel,
                Me.GetSecondaryStratumName(secondaryStratumId))

        End If

        Me.m_Map.AddItem(transitionTypeId, stratumId, secondaryStratumId, item)
        Me.SetHasItems()

    End Sub

End Class
