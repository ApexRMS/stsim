'*********************************************************************************************
' SyncroSim.Utilities – A class library of general utilities
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports System.Globalization

Module DataTableUtilities

    ''' <summary>
    ''' Sets the specified row value
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <param name="columnName"></param>
    ''' <param name="value"></param>
    Public Sub SetRowValue(dr As DataRow, columnName As [String], value As [Object])
        If Object.ReferenceEquals(value, DBNull.Value) OrElse Object.ReferenceEquals(value, Nothing) Then
            dr(columnName) = DBNull.Value
        Else
            dr(columnName) = value
        End If
    End Sub

    ''' <summary>
    ''' Gets a bool for the specified database object
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDataBool(value As Object) As Boolean
        If (Object.ReferenceEquals(value, DBNull.Value)) Then
            Return False
        Else
            Return Convert.ToBoolean(value, CultureInfo.InvariantCulture)
        End If
    End Function

    ''' <summary>
    ''' Gets a boolean from the specified data row and column name
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <param name="columnName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDataBool(dr As DataRow, columnName As String) As Boolean
        Return GetDataBool(dr(columnName))
    End Function

    ''' <summary>
    ''' Gets a int for the specified database object
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDataInt(value As Object) As Integer
        If (Object.ReferenceEquals(value, DBNull.Value)) Then
            Return 0
        Else
            Return Convert.ToInt32(value, CultureInfo.InvariantCulture)
        End If
    End Function

    ''' <summary>
    ''' Gets a Single for the specified database object
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDataSingle(value As Object) As [Single]
        If (Object.ReferenceEquals(value, DBNull.Value)) Then
            Return 0
        Else
            Return Convert.ToSingle(value, CultureInfo.InvariantCulture)
        End If
    End Function

    ''' <summary>
    ''' Gets a Double for the specified database object
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDataDbl(value As Object) As [Double]
        If (Object.ReferenceEquals(value, DBNull.Value)) Then
            Return 0
        Else
            Return Convert.ToDouble(value, CultureInfo.InvariantCulture)
        End If
    End Function

    ''' <summary>
    ''' Gets a string from the specified database object
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDataStr(value As Object) As String
        If (Object.ReferenceEquals(value, DBNull.Value)) Then
            Return Nothing
        Else
            Return Convert.ToString(value, CultureInfo.InvariantCulture)
        End If
    End Function

    ''' <summary>
    ''' Gets a string from the specified data row and column name
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <param name="columnName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDataStr(dr As DataRow, columnName As String) As String
        Return GetDataStr(dr(columnName))
    End Function

    ''' <summary>
    ''' Gets a database value for the specified nullable boolean
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Function GetNullableDatabaseValue(value As Nullable(Of Boolean)) As Object

        If value.HasValue Then

            If value.Value Then
                Return -1
            Else
                Return 0
            End If

        Else
            Return DBNull.Value
        End If

    End Function

    ''' <summary>
    ''' Gets a database value for the specified nullable integer
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns>If the integer has a value, that value is returned.  Otherwise, DBNull.Value is returned.</returns>
    Public Function GetNullableDatabaseValue(value As Nullable(Of Integer)) As Object

        If value.HasValue Then
            Return value.Value
        Else
            Return DBNull.Value
        End If

    End Function

    ''' <summary>
    ''' Gets a database value for the specified nullable double
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns>If the double has a value, that value is returned.  Otherwise, DBNull.Value is returned.</returns>
    Public Function GetNullableDatabaseValue(value As Nullable(Of Double)) As Object

        If value.HasValue Then
            Return value.Value
        Else
            Return DBNull.Value
        End If

    End Function

    ''' <summary>
    ''' Gets a nullable integer for the specified data row and colum name
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="dr"></param>
    ''' <param name="columnName"></param>
    ''' <returns></returns>
    Public Function GetNullableInt(dr As DataRow, columnName As String) As Nullable(Of Integer)

        Dim value As Object = dr(columnName)

        If Object.ReferenceEquals(value, DBNull.Value) OrElse Object.ReferenceEquals(value, Nothing) Then
            Return Nothing
        Else
            Return Convert.ToInt32(value, CultureInfo.InvariantCulture)
        End If

    End Function

    ''' <summary>
    ''' Gets a nullable double for the specified data row and colum name
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="dr"></param>
    ''' <param name="columnName"></param>
    ''' <returns></returns>
    Public Function GetNullableDouble(dr As DataRow, columnName As String) As Nullable(Of Double)

        Dim value As Object = dr(columnName)

        If Object.ReferenceEquals(value, DBNull.Value) OrElse Object.ReferenceEquals(value, Nothing) Then
            Return Nothing
        Else
            Return Convert.ToDouble(value, CultureInfo.InvariantCulture)
        End If

    End Function

    ''' <summary>
    ''' Deletes the specified table row
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <param name="dr"></param>
    ''' <remarks></remarks>

    Public Sub DeleteTableRow(dt As DataTable, dr As DataRow)

        If (dr.RowState = DataRowState.Added) Then
            dt.Rows.Remove(dr)
        Else
            dr.Delete()
        End If

    End Sub

    ''' <summary>
    ''' Gets the table value for the specified id column and value column
    ''' </summary>
    ''' <param name="table"></param>
    ''' <param name="idColumnName"></param>
    ''' <param name="idColumnValue"></param>
    ''' <param name="valueColumnName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTableValue(table As DataTable, idColumnName As String, idColumnValue As Integer, valueColumnName As String) As Object

        For Each dr As DataRow In table.Rows

            If (dr.RowState = DataRowState.Deleted) Then
                Continue For
            End If

            If (Convert.ToInt32(dr(idColumnName), CultureInfo.InvariantCulture) = idColumnValue) Then
                Return (dr(valueColumnName))
            End If

        Next

        Return DBNull.Value

    End Function

End Module

