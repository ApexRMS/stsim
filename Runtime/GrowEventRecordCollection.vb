'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.Core

Class GrowEventRecordCollection

    Private m_Map As New Dictionary(Of Integer, GrowEventRecord)
    Private m_TotalLikelihood As Double
    Private m_RandomGenerator As RandomGenerator

    Public Sub New(ByVal randomGen As RandomGenerator)
        Me.m_RandomGenerator = randomGen
    End Sub

    Public Sub AddRecord(ByVal record As GrowEventRecord)

        Me.m_Map.Add(record.Cell.CellId, record)
        Me.m_TotalLikelihood += record.Likelihood

        Debug.Assert(MathUtils.CompareDoublesGTEqual(Me.m_TotalLikelihood, record.Likelihood, 0.000001))

    End Sub

    Public Function RemoveRecord() As GrowEventRecord

        Dim r As Double = Me.m_RandomGenerator.GetNextDouble
        Dim InverseCumulativeProb As Double = 1.0

        Debug.Assert(Me.m_Map.Count > 0)
        Debug.Assert(Me.m_TotalLikelihood > 0.0)

        For Each v As GrowEventRecord In Me.m_Map.Values

            InverseCumulativeProb -= (v.Likelihood / Me.m_TotalLikelihood)
            Debug.Assert(MathUtils.CompareDoublesGTEqual(InverseCumulativeProb, 0.0, 0.00001))

            If (r >= InverseCumulativeProb) Then

                Me.m_Map.Remove(v.Cell.CellId)

                Debug.Assert(Me.m_TotalLikelihood >= 0.0)
                Me.m_TotalLikelihood -= v.Likelihood

                Return v

            End If

        Next

        Debug.Assert(False)

        Dim first As GrowEventRecord = Me.m_Map.First.Value
        Me.m_Map.Remove(first.Cell.CellId)
        Me.m_TotalLikelihood -= first.Likelihood

        Debug.Assert(Me.m_TotalLikelihood >= 0.0)
        Return first

    End Function

    Public ReadOnly Property Count As Integer
        Get
#If DEBUG Then
            If (Me.m_Map.Count = 0) Then
                Debug.Assert(MathUtils.CompareDoublesEqual(Me.m_TotalLikelihood, 0.0, 0.000001))
            Else
                Debug.Assert(MathUtils.CompareDoublesGTEqual(Me.m_TotalLikelihood, 0.0, 0.000001))
            End If
#End If
            Return Me.m_Map.Count
        End Get
    End Property

End Class
