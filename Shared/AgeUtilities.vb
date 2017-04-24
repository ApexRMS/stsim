'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Imports SyncroSim.Core

''' <summary>
''' Age Utilities
''' </summary>
''' <remarks></remarks>
Module AgeUtilities

    ''' <summary>
    ''' Gets a collection of current age descriptors
    ''' </summary>
    ''' <param name="project"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAgeDescriptors(ByVal project As Project) As IEnumerable(Of AgeDescriptor)

        Dim e As IEnumerable(Of AgeDescriptor) = GetAgeGroupDescriptors(project)

        If (e Is Nothing) Then
            e = GetAgeTypeDescriptors(project)
        End If

#If DEBUG Then
        If (e IsNot Nothing) Then
            Debug.Assert(e.Count > 0)
        End If
#End If

        If (e Is Nothing) Then
            Return Nothing
        End If

        If (e.Count = 0) Then
            Return Nothing
        End If

        Return e

    End Function

    ''' <summary>
    ''' Gets an enumeration of age descriptors from the age group data sheet
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAgeGroupDescriptors(ByVal project As Project) As IEnumerable(Of AgeDescriptor)

        Dim dt As DataTable = project.GetDataSheet(DATASHEET_AGE_GROUP_NAME).GetData()
        Dim dv As New DataView(dt, Nothing, Nothing, DataViewRowState.CurrentRows)

        If (dv.Count = 0) Then
            Return Nothing
        End If

        Dim lst As New List(Of AgeDescriptor)
        Dim dict As New Dictionary(Of Integer, Boolean)

        For Each drv As DataRowView In dv

            Dim value As Integer = CInt(drv(DATASHEET_AGE_GROUP_MAXIMUM_COLUMN_NAME))

            If (Not dict.ContainsKey(value)) Then

                lst.Add(New AgeDescriptor(value, value))
                dict.Add(value, True)

            End If

        Next

        lst.Sort(Function(ad1 As AgeDescriptor, ad2 As AgeDescriptor) As Integer
                     Return ad1.MinimumAge.CompareTo(ad2.MinimumAge)
                 End Function)

        Dim Prev As Integer = 0

        For Each ad As AgeDescriptor In lst

            Dim t As Integer = ad.MinimumAge

            ad.MinimumAge = Prev
            Prev = t + 1

        Next

        lst.Add(New AgeDescriptor(Prev, Nothing))

#If DEBUG Then

        Debug.Assert(lst.Count > 0)

        For Each ad As AgeDescriptor In lst

            If (ad.MaximumAge.HasValue) Then
                Debug.Assert(ad.MinimumAge <= ad.MaximumAge.Value)
            End If

        Next

#End If

        lst(lst.Count - 1).MaximumAge = Nothing
        Return lst

    End Function

    ''' <summary>
    ''' Gets an enumeration of age descriptors from the age type data sheet
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAgeTypeDescriptors(ByVal project As Project) As IEnumerable(Of AgeDescriptor)

        Dim dr As DataRow = project.GetDataSheet(DATASHEET_AGE_TYPE_NAME).GetDataRow()

        If (dr IsNot Nothing) Then

            If (dr(DATASHEET_AGE_TYPE_FREQUENCY_COLUMN_NAME) IsNot DBNull.Value And
                dr(DATASHEET_AGE_TYPE_MAXIMUM_COLUMN_NAME) IsNot DBNull.Value) Then

                Dim f As Integer = CInt(dr(DATASHEET_AGE_TYPE_FREQUENCY_COLUMN_NAME))
                Dim m As Integer = CInt(dr(DATASHEET_AGE_TYPE_MAXIMUM_COLUMN_NAME))

                If (f <= m) Then

                    Dim h As New AgeHelper(True, f, m)
                    Return h.GetAges()

                End If

            End If

        End If

        Return Nothing

    End Function

End Module
