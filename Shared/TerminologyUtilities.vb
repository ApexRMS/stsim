'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports SyncroSim.Core

''' <summary>
''' Utilities relating to the terminology data feed
''' </summary>
''' <remarks></remarks>
Module TerminologyUtilities

    Public Function GetTimestepUnits(ByVal project As Project) As String

        Dim dr As DataRow = project.GetDataSheet(DATASHEET_TERMINOLOGY_NAME).GetDataRow()

        If (dr Is Nothing OrElse dr("TimestepUnits") Is DBNull.Value) Then
            Return "Timestep"
        Else
            Return CStr(dr("TimestepUnits"))
        End If

    End Function

    ''' <summary>
    ''' Converts a terminology unit to its corresponding string
    ''' </summary>
    ''' <param name="unit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function TerminologyUnitToString(ByVal unit As TerminologyUnit) As String

        If (unit = TerminologyUnit.Acres) Then
            Return "Acres"
        ElseIf (unit = TerminologyUnit.Hectares) Then
            Return "Hectares"
        ElseIf (unit = TerminologyUnit.SquareKilometers) Then
            Return "Square Kilometers"
        ElseIf (unit = TerminologyUnit.SquareMiles) Then
            Return "Square Miles"
        ElseIf (unit = TerminologyUnit.None) Then
            Return "None"
        End If

        Debug.Assert(False)
        Return Nothing

    End Function

    ''' <summary>
    ''' Gets the amount label terminology
    ''' </summary>
    ''' <param name="store"></param>
    ''' <param name="terminologyDataSheet"></param>
    ''' <param name="amountLabel"></param>
    ''' <param name="amountUnits"></param>
    ''' <remarks></remarks>
    Public Sub GetAmountLabelTerminology(
        ByVal terminologyDataSheet As DataSheet,
        ByRef amountLabel As String,
        ByRef amountUnits As TerminologyUnit)

        Dim dr As DataRow = terminologyDataSheet.GetDataRow()

        amountLabel = "Amount"
        amountUnits = TerminologyUnit.None

        If (dr IsNot Nothing) Then

            If (dr(DATASHEET_TERMINOLOGY_AMOUNT_LABEL_COLUMN_NAME) IsNot DBNull.Value) Then
                amountLabel = CStr(dr(DATASHEET_TERMINOLOGY_AMOUNT_LABEL_COLUMN_NAME))
            End If

            If (dr(DATASHEET_TERMINOLOGY_AMOUNT_UNITS_COLUMN_NAME) IsNot DBNull.Value) Then

                Dim value As Integer = CInt(dr(DATASHEET_TERMINOLOGY_AMOUNT_UNITS_COLUMN_NAME))
                amountUnits = CType(value, TerminologyUnit)

            End If

        End If

    End Sub

    ''' <summary>
    ''' Gets the state label terminology
    ''' </summary>
    ''' <param name="store"></param>
    ''' <param name="terminologyDataSheet"></param>
    ''' <param name="slxlabel"></param>
    ''' <param name="slylabel"></param>
    ''' <remarks></remarks>
    Public Sub GetStateLabelTerminology(
        ByVal terminologyDataSheet As DataSheet,
        ByRef slxlabel As String,
        ByRef slylabel As String)

        Dim dr As DataRow = terminologyDataSheet.GetDataRow

        slxlabel = "State Label X"
        slylabel = "State Label Y"

        If (dr IsNot Nothing) Then

            If (dr(DATASHEET_TERMINOLOGY_STATELABELX_COLUMN_NAME) IsNot DBNull.Value) Then
                slxlabel = CStr(dr(DATASHEET_TERMINOLOGY_STATELABELX_COLUMN_NAME))
            End If

            If (dr(DATASHEET_TERMINOLOGY_STATELABELY_COLUMN_NAME) IsNot DBNull.Value) Then
                slylabel = CStr(dr(DATASHEET_TERMINOLOGY_STATELABELY_COLUMN_NAME))
            End If

        End If

    End Sub

    ''' <summary>
    ''' Gets the stratum label terminology
    ''' </summary>
    ''' <param name="store"></param>
    ''' <param name="terminologyDataSheet"></param>
    ''' <param name="primaryStratumLabel"></param>
    ''' <param name="secondaryStratumLabel"></param>
    ''' <param name="tertiaryStratumLabel"></param>
    ''' <remarks></remarks>
    Public Sub GetStratumLabelTerminology(
        ByVal terminologyDataSheet As DataSheet,
        ByRef primaryStratumLabel As String,
        ByRef secondaryStratumLabel As String,
        ByRef tertiaryStratumLabel As String)

        Dim dr As DataRow = terminologyDataSheet.GetDataRow

        primaryStratumLabel = "Stratum"
        secondaryStratumLabel = "Secondary Stratum"
        tertiaryStratumLabel = "Tertiary Stratum"

        If (dr IsNot Nothing) Then

            If (dr(DATASHEET_TERMINOLOGY_PRIMARY_STRATUM_LABEL_COLUMN_NAME) IsNot DBNull.Value) Then
                primaryStratumLabel = CStr(dr(DATASHEET_TERMINOLOGY_PRIMARY_STRATUM_LABEL_COLUMN_NAME))
            End If

            If (dr(DATASHEET_TERMINOLOGY_SECONDARY_STRATUM_LABEL_COLUMN_NAME) IsNot DBNull.Value) Then
                secondaryStratumLabel = CStr(dr(DATASHEET_TERMINOLOGY_SECONDARY_STRATUM_LABEL_COLUMN_NAME))
            End If

            If (dr(DATASHEET_TERMINOLOGY_TERTIARY_STRATUM_LABEL_COLUMN_NAME) IsNot DBNull.Value) Then
                tertiaryStratumLabel = CStr(dr(DATASHEET_TERMINOLOGY_TERTIARY_STRATUM_LABEL_COLUMN_NAME))
            End If

        End If

    End Sub

End Module
