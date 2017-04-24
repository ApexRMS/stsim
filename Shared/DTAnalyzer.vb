'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Imports SyncroSim.Core
Imports System.Globalization

Class DTAnalyzer

    Private m_DataSource As DataTable
    Private m_Project As Project
    Private m_RowLookup As New Dictionary(Of String, DataRow)
    Private m_StrataWithData As New Dictionary(Of Integer, Boolean)

    Public Sub New(ByVal dataSource As DataTable, ByVal project As Project)

        Me.m_DataSource = dataSource
        Me.m_Project = project

        For Each dr As DataRow In Me.m_DataSource.Rows

            If (dr.RowState = DataRowState.Deleted) Then
                Continue For
            End If

            Dim StratumId As Nullable(Of Integer) = Nothing
            Dim StateClassId As Integer = CInt(dr(DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME))

            If (dr(DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME) IsNot DBNull.Value) Then
                StratumId = CInt(dr(DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME))
            End If

            Dim StratumKey As Integer = CreateStratumLookupKey(StratumId)
            Dim StateClassKey As String = CreateStateClassLookupKey(StratumId, StateClassId)

            Me.m_RowLookup.Add(StateClassKey, dr)

            If (Not Me.m_StrataWithData.ContainsKey(StratumKey)) Then
                Me.m_StrataWithData.Add(StratumKey, True)
            End If

        Next

    End Sub

    Public Shared Sub GetDTFieldValues(
        ByVal dr As DataRow,
        ByRef stratumIdSource As Nullable(Of Integer),
        ByRef stateClassIdSource As Integer,
        ByRef stratumIdDest As Nullable(Of Integer),
        ByRef stateClassIdDest As Nullable(Of Integer))

        GetCoreFieldValues(
            dr,
            stratumIdSource,
            stateClassIdSource,
            stratumIdDest,
            stateClassIdDest, True)

    End Sub

    Public Shared Sub GetPTFieldValues(
        ByVal dr As DataRow,
        ByRef stratumIdSource As Nullable(Of Integer),
        ByRef stateClassIdSource As Integer,
        ByRef stratumIdDest As Nullable(Of Integer),
        ByRef stateClassIdDest As Nullable(Of Integer))

        GetCoreFieldValues(
            dr,
            stratumIdSource,
            stateClassIdSource,
            stratumIdDest,
            stateClassIdDest, False)

    End Sub

    Public Function CanResolveStateClass(
        ByVal stratumIdSource As Nullable(Of Integer),
        ByVal stratumIdDest As Nullable(Of Integer),
        ByVal stateClassId As Integer) As Boolean

        Return Me.ResolveStateClassStratum(
            stratumIdSource,
            stratumIdDest,
            stateClassId,
            New Nullable(Of Integer))

    End Function

    Public Function ResolveStateClassStratum(
        ByVal stratumIdSource As Nullable(Of Integer),
        ByVal stratumIdDest As Nullable(Of Integer),
        ByVal stateClassId As Integer,
        ByRef outStratumId As Nullable(Of Integer)) As Boolean

        Dim dr As DataRow = Nothing
        outStratumId = Nothing

        If (stratumIdSource.HasValue) Then

            If (stratumIdDest.HasValue) Then
                dr = Me.GetStateClassRow(stratumIdDest.Value, stateClassId)
            Else
                dr = Me.GetStateClassRow(stratumIdSource.Value, stateClassId)
            End If

            If (dr Is Nothing) Then
                dr = Me.GetStateClassRow(Nothing, stateClassId)
            End If

        Else

            If (stratumIdDest.HasValue) Then
                dr = Me.GetStateClassRow(stratumIdDest.Value, stateClassId)
            Else
                dr = Me.GetStateClassRow(Nothing, stateClassId)
            End If

        End If

        If (dr Is Nothing) Then
            Return False
        End If

        If (dr(DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME) IsNot DBNull.Value) Then
            outStratumId = CType(dr(DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME), Integer)
        End If

        Return True

    End Function

    Public Function GetStateClassRow(
        ByVal stratumId As Nullable(Of Integer),
        ByVal stateClassId As Integer) As DataRow

        Dim k As String = CreateStateClassLookupKey(stratumId, stateClassId)

        If (Me.m_RowLookup.ContainsKey(k)) Then
            Return Me.m_RowLookup(k)
        Else
            Return Nothing
        End If

    End Function

    Public Function StateClassExists(
        ByVal stratumId As Nullable(Of Integer),
        ByVal stateClassId As Integer) As Boolean

        Return (Me.GetStateClassRow(stratumId, stateClassId) IsNot Nothing)

    End Function

    Public Function StratumHasData(ByVal stratumId As Nullable(Of Integer)) As Boolean

        Dim StratumKey As Integer = CreateStratumLookupKey(stratumId)
        Return Me.m_StrataWithData.ContainsKey(StratumKey)

    End Function

    Public Sub ThrowDataException(ByVal stateClassId As Integer, ByVal isDestination As Boolean)

        Dim psl As String = Nothing
        Dim ssl As String = Nothing
        Dim StateClassName As String = "NULL"
        Dim StateClassDataSheet As DataSheet = Me.m_Project.GetDataSheet(DATASHEET_STATECLASS_NAME)
        Dim Location As String = "From"

        If (isDestination) Then
            Location = "To"
        End If

        GetStratumLabelTerminology(Me.m_Project.GetDataSheet(DATASHEET_TERMINOLOGY_NAME), psl, ssl)

        StateClassName = CStr(DataTableUtilities.GetTableValue(
            StateClassDataSheet.GetData(),
            StateClassDataSheet.ValueMember,
            stateClassId,
            DATASHEET_NAME_COLUMN_NAME))

        Dim msg As String = String.Format(CultureInfo.CurrentCulture,
            "The state class '{0}' could not be located in '{1} {2}'.", StateClassName, Location, psl)

        Throw New DataException(msg)

    End Sub

    Private Shared Function CreateStratumLookupKey(ByVal stratumId As Nullable(Of Integer)) As Integer

        If (stratumId.HasValue) Then
            Return stratumId.Value
        Else
            Return 0
        End If

    End Function

    Public Shared Function CreateStateClassLookupKey(
        ByVal stratumId As Nullable(Of Integer),
        ByVal stateClassId As Integer) As String

        Dim k1 As String = "NULL"
        Dim k2 As String = CStr(stateClassId)

        If (stratumId.HasValue) Then

            Debug.Assert(CInt(stratumId.Value) > 0)
            k1 = CStr(stratumId.Value)

        End If

        Return String.Format(CultureInfo.InvariantCulture, "{0}-{1}", k1, k2)

    End Function

    Private Shared Sub GetCoreFieldValues(
        ByVal dr As DataRow,
        ByRef stratumIdSource As Nullable(Of Integer),
        ByRef stateClassIdSource As Integer,
        ByRef stratumIdDest As Nullable(Of Integer),
        ByRef stateClassIdDest As Nullable(Of Integer),
        ByVal deterministic As Boolean)

        Dim stsrc As String = DATASHEET_DT_STRATUMIDSOURCE_COLUMN_NAME
        Dim scsrc As String = DATASHEET_DT_STATECLASSIDSOURCE_COLUMN_NAME
        Dim stdst As String = DATASHEET_DT_STRATUMIDDEST_COLUMN_NAME
        Dim scdst As String = DATASHEET_DT_STATECLASSIDDEST_COLUMN_NAME

        If (Not deterministic) Then

            stsrc = DATASHEET_PT_STRATUMIDSOURCE_COLUMN_NAME
            scsrc = DATASHEET_PT_STATECLASSIDSOURCE_COLUMN_NAME
            stdst = DATASHEET_PT_STRATUMIDDEST_COLUMN_NAME
            scdst = DATASHEET_PT_STATECLASSIDDEST_COLUMN_NAME

        End If

        stratumIdSource = Nothing
        stateClassIdSource = CInt(dr(scsrc))
        stratumIdDest = Nothing
        stateClassIdDest = Nothing

        Debug.Assert(stateClassIdSource > 0)

        If (dr(stsrc) IsNot DBNull.Value) Then
            stratumIdSource = CType(dr(stsrc), Integer)
            Debug.Assert(stratumIdSource.Value > 0)
        End If

        If (dr(stdst) IsNot DBNull.Value) Then
            stratumIdDest = CInt(dr(stdst))
            Debug.Assert(stratumIdDest.Value > 0)
        End If

        If (dr(scdst) IsNot DBNull.Value) Then
            stateClassIdDest = CInt(dr(scdst))
            Debug.Assert(stateClassIdDest.Value > 0)
        End If

    End Sub

    Public Shared Function IsValidLocation(ByVal proposedLocation As Object) As Boolean

        If (proposedLocation Is Nothing) Then
            Return False
        End If

        Dim Location As String = CStr(proposedLocation)

        If (String.IsNullOrEmpty(Location)) Then
            Return False
        End If

        Dim LocUpper As String = Location.ToUpper(CultureInfo.CurrentCulture).Trim

        If (String.IsNullOrEmpty(LocUpper)) Then
            Return False
        End If

        If (LocUpper.Length < 2) Then
            Return False
        End If

        Dim CharPart As String = Mid(LocUpper, 1, 1)
        Dim NumPart As String = Mid(LocUpper, 2, LocUpper.Length - 1)

        If (String.IsNullOrEmpty(CharPart) Or String.IsNullOrEmpty(NumPart)) Then
            Return False
        End If

        If (Asc(CharPart) < Asc("A") Or Asc(CharPart) > Asc("Z")) Then
            Return False
        End If

        Dim n As Integer = 0
        If (Not Integer.TryParse(NumPart, n)) Then
            Return False
        End If

        If (n <= 0 Or n > 256) Then
            Return False
        End If

        Return True

    End Function

End Class
