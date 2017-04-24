'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Imports SyncroSim.Common

Public Class STSimDistributionBaseExpander

    Private m_Provider As STSimDistributionProvider
    Private m_1 As New MultiLevelKeyMap1(Of Dictionary(Of String, STSimDistributionValue))
    Private m_2 As New MultiLevelKeyMap2(Of Dictionary(Of String, STSimDistributionValue))
    Private m_3 As New MultiLevelKeyMap2(Of Dictionary(Of String, STSimDistributionValue))

    Public Sub New(ByVal provider As STSimDistributionProvider)

        Me.m_Provider = provider
        Me.FillUserDistributionMaps()

    End Sub

    Public Function Expand(ByVal items As IEnumerable(Of STSimDistributionBase)) As IEnumerable(Of STSimDistributionBase)
        Return Me.InternalExpand(items)
    End Function

    Private Function InternalExpand(ByVal items As IEnumerable(Of STSimDistributionBase)) As IEnumerable(Of STSimDistributionBase)

        Debug.Assert(items.Count > 0)
        Debug.Assert(Me.m_Provider.Values.Count > 0)

        If (Me.m_Provider.Values.Count = 0 Or items.Count = 0) Then
            Return items
        End If

        Dim Expanded As New List(Of STSimDistributionBase)

        For Each t As STSimDistributionBase In items

            If (Not ExpansionRequired(t)) Then

                Expanded.Add(t)
                Continue For

            End If

            Dim l As Dictionary(Of String, STSimDistributionValue) = Me.GetValueDictionary(t)

            If (l Is Nothing) Then

                Expanded.Add(t)
                Continue For

            End If

            For Each v As STSimDistributionValue In l.Values

                Dim n As STSimDistributionBase = t.Clone()

                n.StratumId = v.StratumId
                n.SecondaryStratumId = v.SecondaryStratumId

                Expanded.Add(n)

            Next

        Next

        Debug.Assert(Expanded.Count >= items.Count)
        Return Expanded

    End Function

    Private Shared Function CreateDistBaseKey(
        ByVal stratumId As Nullable(Of Integer),
        ByVal secondaryStratumId As Nullable(Of Integer)) As String

        Dim s1 As String = "NULL"
        Dim s2 As String = "NULL"

        If (stratumId.HasValue) Then
            s1 = CStr(stratumId.Value)
        End If

        If (secondaryStratumId.HasValue) Then
            s2 = CStr(secondaryStratumId.Value)
        End If

        Return s1 & "-" & s2

    End Function

    Private Shared Function ExpansionRequired(ByVal t As STSimDistributionBase) As Boolean

        If (Not t.DistributionTypeId.HasValue) Then
            Return False
        ElseIf (t.StratumId.HasValue And t.SecondaryStratumId.HasValue) Then
            Return False
        Else
            Return True
        End If

    End Function

    Private Function GetValueDictionary(ByVal t As STSimDistributionBase) As Dictionary(Of String, STSimDistributionValue)

        Dim l As Dictionary(Of String, STSimDistributionValue) = Nothing

        If (Not t.StratumId.HasValue And Not t.SecondaryStratumId.HasValue) Then
            l = Me.m_1.GetItemExact(t.DistributionTypeId)
        ElseIf (t.StratumId.HasValue) Then
            l = Me.m_2.GetItemExact(t.DistributionTypeId, t.StratumId.Value)
        Else
            l = Me.m_3.GetItemExact(t.DistributionTypeId, t.SecondaryStratumId.Value)
        End If

#If DEBUG Then
        If (l IsNot Nothing) Then
            Debug.Assert(l.Count > 0)
        End If
#End If

        Return l

    End Function

    Private Sub FillUserDistributionMaps()

        For Each v As STSimDistributionValue In Me.m_Provider.Values

            Dim d As Dictionary(Of String, STSimDistributionValue) = Me.m_1.GetItemExact(v.DistributionTypeId)

            If (d Is Nothing) Then
                d = New Dictionary(Of String, STSimDistributionValue)
                Me.m_1.AddItem(v.DistributionTypeId, d)
            End If

            Dim k As String = CreateDistBaseKey(v.StratumId, v.SecondaryStratumId)

            If (Not d.ContainsKey(k)) Then
                d.Add(k, v)
            End If

        Next

        For Each v As STSimDistributionValue In Me.m_Provider.Values

            If (v.StratumId.HasValue) Then

                Dim d As Dictionary(Of String, STSimDistributionValue) =
                    Me.m_2.GetItemExact(v.DistributionTypeId, v.StratumId.Value)

                If (d Is Nothing) Then
                    d = New Dictionary(Of String, STSimDistributionValue)
                    Me.m_2.AddItem(v.DistributionTypeId, v.StratumId.Value, d)
                End If

                Dim k As String = CreateDistBaseKey(v.StratumId, v.SecondaryStratumId)

                If (Not d.ContainsKey(k)) Then
                    d.Add(k, v)
                End If

            End If

        Next

        For Each v As STSimDistributionValue In Me.m_Provider.Values

            If (v.SecondaryStratumId.HasValue) Then

                Dim l As Dictionary(Of String, STSimDistributionValue) =
                    Me.m_3.GetItemExact(v.DistributionTypeId, v.SecondaryStratumId.Value)

                If (l Is Nothing) Then
                    l = New Dictionary(Of String, STSimDistributionValue)
                    Me.m_3.AddItem(v.DistributionTypeId, v.SecondaryStratumId.Value, l)
                End If

                Dim k As String = CreateDistBaseKey(v.StratumId, v.SecondaryStratumId)

                If (Not l.ContainsKey(k)) Then
                    l.Add(k, v)
                End If

            End If

        Next

    End Sub

End Class
