'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core
Imports SyncroSim.StochasticTime
Imports System.Globalization

Module ValidationTableUtilities

    ''' <summary>
    ''' Creates an age validation table
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CreateAgeValidationTable(ByVal project As Project) As ValidationTable

        Dim dt As New DataTable(AGE_VALIDATION_TABLE_NAME)
        dt.Locale = CultureInfo.InvariantCulture

        dt.Columns.Add(New DataColumn(VALUE_MEMBER_COLUMN_NAME, GetType(Long)))
        dt.Columns.Add(New DataColumn(DISPLAY_MEMBER_COLUMN_NAME, GetType(String)))

        Dim e As IEnumerable(Of AgeDescriptor) = GetAgeGroupDescriptors(project)

        If (e Is Nothing) Then
            e = GetAgeTypeDescriptors(project)
        End If

        If (e IsNot Nothing) Then

            For Each d As AgeDescriptor In e

                Dim Value As Long = CLng(d.MinimumAge)
                Dim Display As String = Nothing

                If (d.MaximumAge.HasValue) Then

                    If (d.MaximumAge.Value = d.MinimumAge) Then
                        Display = String.Format(CultureInfo.InvariantCulture, "{0}", d.MinimumAge)
                    Else
                        Display = String.Format(CultureInfo.InvariantCulture, "{0}-{1}", d.MinimumAge, d.MaximumAge.Value)
                    End If

                Else
                    Display = String.Format(CultureInfo.InvariantCulture, "{0}+", d.MinimumAge)
                End If

                dt.Rows.Add({Value, Display})

            Next

        End If

        Return New ValidationTable(
            dt,
            VALUE_MEMBER_COLUMN_NAME,
            DISPLAY_MEMBER_COLUMN_NAME,
            SortOrder.None)

    End Function

End Module