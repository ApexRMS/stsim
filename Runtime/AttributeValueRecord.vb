'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

''' <summary>
''' Attribute Value Record
''' </summary>
''' <remarks>
''' This class is shared by all existing attribute maps.  Members are public for the fastest 
''' possible access, and the constructor is there for convenience.
''' </remarks>
Class AttributeValueRecord

    Public MinimumAge As Integer
    Public MaximumAge As Integer
    Public Value As Double

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="minimumAge"></param>
    ''' <param name="maximumAge"></param>
    ''' <param name="value"></param>
    ''' <remarks></remarks>
    Public Sub New(
        ByVal minimumAge As Integer,
        ByVal maximumAge As Integer,
        ByVal value As Double)

        Me.MinimumAge = minimumAge
        Me.MaximumAge = maximumAge
        Me.Value = value

    End Sub

    ''' <summary>
    ''' Returns an attribute value record from the specified list where ages are not considered
    ''' </summary>
    ''' <param name="records"></param>
    ''' <remarks>
    ''' If there are no records in the list, Null is returned.  But if there are records, the list should have only
    ''' one entry and its value will be returned.
    ''' </remarks>
    Public Shared Function GetAttributeRecordValueNoAge(
        ByVal records As List(Of AttributeValueRecord)) As Nullable(Of Double)

        If (records.Count = 0) Then
            Return Nothing
        Else

            Debug.Assert(records.Count = 1)
            Debug.Assert(records(0).MinimumAge = 0)
            Debug.Assert(records(0).MaximumAge = Integer.MaxValue)

            Return records(0).Value

        End If

    End Function

    ''' <summary>
    ''' Returns an attribute value record from the specified list based on the specified age.
    ''' </summary>
    ''' <param name="records"></param>
    ''' <param name="age"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' The record with the closest matching minimum age is returned if one is found.  But if the specified age
    ''' does not fall within the age ranges of any of the existing records then Null is returned.
    ''' </remarks>
    Public Shared Function GetAttributeRecordValueByAge(
        ByVal records As List(Of AttributeValueRecord),
        ByVal age As Integer) As Nullable(Of Double)

        If (records.Count = 0) Then
            Return Nothing
        End If

        Dim FinalRecord As AttributeValueRecord = Nothing

        For Each record As AttributeValueRecord In records

            If (age >= record.MinimumAge And age <= record.MaximumAge) Then

                If (FinalRecord Is Nothing) Then

                    FinalRecord = record
                    Continue For

                End If

                If (record.MinimumAge > FinalRecord.MinimumAge) Then
                    FinalRecord = record
                End If

            End If

        Next

        If (FinalRecord IsNot Nothing) Then
            Return FinalRecord.Value
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Adds an attribute record to the specified list of records
    ''' </summary>
    ''' <param name="records"></param>
    ''' <param name="minimumAge"></param>
    ''' <param name="maximumAge"></param>
    ''' <param name="value"></param>
    ''' <remarks>
    ''' If the minimum age or maximum age is specified then a new record is added to the list.  Otherwise
    ''' the list should have only one record with no ages specified.
    ''' </remarks>
    Public Shared Sub AddAttributeRecord(
        ByVal records As List(Of AttributeValueRecord),
        ByVal minimumAge As Nullable(Of Integer),
        ByVal maximumAge As Nullable(Of Integer),
        ByVal value As Nullable(Of Double))

        Dim FinalValue As Double = 1.0

        If (value.HasValue) Then
            FinalValue = value.Value
        End If

        If (minimumAge.HasValue Or maximumAge.HasValue) Then

            Dim AgeMin As Integer = 0
            Dim AgeMax As Integer = Integer.MaxValue

            If (minimumAge.HasValue) Then
                AgeMin = minimumAge.Value
            End If

            If (maximumAge.HasValue) Then
                AgeMax = maximumAge.Value
            End If

            records.Add(New AttributeValueRecord(AgeMin, AgeMax, FinalValue))

        Else

            If (records.Count = 0) Then
                records.Add(New AttributeValueRecord(0, Integer.MaxValue, FinalValue))
            Else
                records(0).Value = FinalValue
            End If

            Debug.Assert(records.Count = 1)
            Debug.Assert(records(0).MinimumAge = 0)
            Debug.Assert(records(0).MaximumAge = Integer.MaxValue)

        End If

    End Sub

End Class
