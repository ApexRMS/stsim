'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Imports SyncroSim.Core
Imports System.Globalization
Imports System.Reflection

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class TransitionGroupDataSheet
    Inherits DataSheet

    Public Overrides Sub Validate(proposedValue As Object, columnName As String)

        MyBase.Validate(proposedValue, columnName)

        If (columnName = DATASHEET_NAME_COLUMN_NAME) Then
            ValidateName(CStr(proposedValue))
        End If

    End Sub

    Public Overrides Sub Validate(proposedRow As DataRow, transferMethod As DataTransferMethod)

        MyBase.Validate(proposedRow, transferMethod)
        ValidateName(CStr(proposedRow(DATASHEET_NAME_COLUMN_NAME)))

    End Sub

    Public Overrides Sub Validate(proposedData As DataTable, transferMethod As DataTransferMethod)

        MyBase.Validate(proposedData, transferMethod)

        For Each dr As DataRow In proposedData.Rows

            If (Not DataTableUtilities.GetDataBool(dr, IS_AUTO_COLUMN_NAME)) Then
                ValidateName(CStr(dr(DATASHEET_NAME_COLUMN_NAME)))
            End If

        Next

    End Sub

    Private Shared Sub ValidateName(ByVal name As String)

        If (name.EndsWith(AUTO_COLUMN_SUFFIX, StringComparison.Ordinal)) Then

            Dim msg As String = String.Format(CultureInfo.CurrentCulture,
                "The transition group name cannot have the suffix: '{0}'.", AUTO_COLUMN_SUFFIX)

            Throw New DataException(msg)

        End If

    End Sub

End Class
