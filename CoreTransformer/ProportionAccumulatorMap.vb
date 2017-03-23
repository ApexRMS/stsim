'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.Common

''' <summary>
''' Proportion Accumulator Map
''' </summary>
''' <remarks></remarks>
Class ProportionAccumulatorMap

    Private m_Map As New MultiLevelKeyMap2(Of AccumulatedProportion)
    Private m_Amount As Double

    Public Sub New(ByVal amount As Double)
        Me.m_Amount = amount
    End Sub

    Public Sub AddOrIncrement(ByVal stratumId As Integer, ByVal secondaryStratumId As Nullable(Of Integer))

        Dim ap As AccumulatedProportion = Me.m_Map.GetItemExact(stratumId, secondaryStratumId)

        If (ap Is Nothing) Then
            Me.m_Map.AddItem(stratumId, secondaryStratumId, New AccumulatedProportion(Me.m_Amount))
        Else
            Debug.Assert(ap.Amount >= Me.m_Amount)
            ap.Amount += Me.m_Amount
        End If

    End Sub

    Public Sub Decrement(ByVal stratumId As Integer, ByVal secondaryStratumId As Nullable(Of Integer))

        Dim ap As AccumulatedProportion = Me.m_Map.GetItemExact(stratumId, secondaryStratumId)

        ap.Amount -= Me.m_Amount

        If ap.Amount < 0.0 Then
            ap.Amount = 0.0
        End If

    End Sub

    Public Function GetValue(ByVal stratumId As Integer, ByVal secondaryStratumId As Nullable(Of Integer)) As Object

        Dim ap As AccumulatedProportion = Me.m_Map.GetItemExact(stratumId, secondaryStratumId)

        If (ap Is Nothing) Then
            Return Nothing
        Else
            Return ap.Amount
        End If

    End Function

End Class

''' <summary>
''' Accumulated Proportion Class
''' </summary>
''' <remarks>Wrapping the value in a class allows for Null as the default value</remarks>
Class AccumulatedProportion

    Public Amount As Double

    Public Sub New(ByVal value As Double)
        Me.Amount = value
    End Sub

End Class
