'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core

Class TransitionAttributeValueMap
    Inherits STSimMapBase6(Of List(Of AttributeValueRecord))

    Private m_TypeGroupMap As New Dictionary(Of Integer, Dictionary(Of Integer, Boolean))

    Public Sub New(
        ByVal scenario As Scenario,
        ByVal transitionAttributes As TransitionAttributeValueCollection)

        MyBase.New(scenario)

        For Each ta As TransitionAttributeValue In transitionAttributes
            Me.AddAttributeValue(ta)
        Next

    End Sub

    Public ReadOnly Property TypeGroupMap As Dictionary(Of Integer, Dictionary(Of Integer, Boolean))
        Get
            Return Me.m_TypeGroupMap
        End Get
    End Property

    Public Function GetAttributeValue(
        ByVal transitionAttributeTypeId As Integer,
        ByVal transitionGroupId As Integer,
        ByVal stratumId As Integer,
        ByVal secondaryStratumId As Nullable(Of Integer),
        ByVal tertiaryStratumId As Nullable(Of Integer),
        ByVal stateClassId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal age As Integer) As Nullable(Of Double)

        Dim cm As List(Of AttributeValueRecord) = Me.GetItem(
            transitionAttributeTypeId,
            transitionGroupId,
            stratumId,
            secondaryStratumId,
            tertiaryStratumId,
            stateClassId,
            iteration,
            timestep)

        If (cm IsNot Nothing) Then
            Return AttributeValueRecord.GetAttributeRecordValueByAge(cm, age)
        Else
            Return Nothing
        End If

    End Function

    Private Sub AddAttributeValue(ByVal item As TransitionAttributeValue)

        Dim l As List(Of AttributeValueRecord) =
            Me.GetItemExact(
                item.AttributeTypeId,
                item.TransitionGroupId,
                item.StratumId,
                item.SecondaryStratumId,
                item.TertiaryStratumId,
                item.StateClassId,
                item.Iteration,
                item.Timestep)

        If (l Is Nothing) Then

            l = New List(Of AttributeValueRecord)

            Me.AddItem(
                item.AttributeTypeId,
                item.TransitionGroupId,
                item.StratumId,
                item.SecondaryStratumId,
                item.TertiaryStratumId,
                item.StateClassId,
                item.Iteration,
                item.Timestep,
                l)

        End If

        AttributeValueRecord.AddAttributeRecord(l, item.MinimumAge, item.MaximumAge, item.Value)

        If (Not Me.m_TypeGroupMap.ContainsKey(item.TransitionGroupId)) Then
            Me.m_TypeGroupMap.Add(item.TransitionGroupId, New Dictionary(Of Integer, Boolean))
        End If

        Dim d As Dictionary(Of Integer, Boolean) = TypeGroupMap(item.TransitionGroupId)

        If Not d.ContainsKey(item.AttributeTypeId) Then
            d.Add(item.AttributeTypeId, True)
        End If

        Debug.Assert(Me.HasItems())

    End Sub

End Class
