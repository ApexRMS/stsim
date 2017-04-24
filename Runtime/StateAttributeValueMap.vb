'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Imports SyncroSim.Core

Class StateAttributeValueMap
    Inherits STSimMapBase4(Of List(Of AttributeValueRecord))

    Friend Sub New(
        ByVal scenario As Scenario,
        ByVal items As StateAttributeValueCollection)

        MyBase.New(scenario)

        For Each item As StateAttributeValue In items
            Me.AddAttributeValue(item)
        Next

    End Sub

    Public Function GetAttributeValueNoAge(
        ByVal stateAttributeTypeId As Integer,
        ByVal stratumId As Integer,
        ByVal secondaryStratumId As Nullable(Of Integer),
        ByVal stateClassId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer) As Nullable(Of Double)

        Dim cm As List(Of AttributeValueRecord) = Me.GetItem(
            stateAttributeTypeId,
            stratumId,
            secondaryStratumId,
            stateClassId,
            iteration,
            timestep)

        If (cm IsNot Nothing) Then
            Return AttributeValueRecord.GetAttributeRecordValueNoAge(cm)
        Else
            Return Nothing
        End If

    End Function

    Public Function GetAttributeValueByAge(
        ByVal stateAttributeTypeId As Integer,
        ByVal stratumId As Integer,
        ByVal secondaryStratumId As Nullable(Of Integer),
        ByVal stateClassId As Integer,
        ByVal iteration As Integer,
        ByVal timestep As Integer,
        ByVal age As Integer) As Nullable(Of Double)

        Dim cm As List(Of AttributeValueRecord) = Me.GetItem(
            stateAttributeTypeId,
            stratumId,
            secondaryStratumId,
            stateClassId,
            iteration,
            timestep)

        If (cm IsNot Nothing) Then
            Return AttributeValueRecord.GetAttributeRecordValueByAge(cm, age)
        Else
            Return Nothing
        End If

    End Function

    Private Sub AddAttributeValue(ByVal item As StateAttributeValue)

        Dim l As List(Of AttributeValueRecord) =
            Me.GetItemExact(
                item.AttributeTypeId,
                item.StratumId,
                item.SecondaryStratumId,
                item.StateClassId,
                item.Iteration,
                item.Timestep)

        If (l Is Nothing) Then

            l = New List(Of AttributeValueRecord)

            Me.AddItem(
                item.AttributeTypeId,
                item.StratumId,
                item.SecondaryStratumId,
                item.StateClassId,
                item.Iteration,
                item.Timestep,
                l)

        End If

        AttributeValueRecord.AddAttributeRecord(l, item.MinimumAge, item.MaximumAge, item.Value)
        Debug.Assert(Me.HasItems())

    End Sub

End Class



