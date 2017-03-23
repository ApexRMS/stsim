'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

''' <summary>
''' Lookup Key Utilities 
''' </summary>
''' <remarks></remarks>
Module LookupKeyUtilities

    ''' <summary>
    ''' Gets a secondary stratum Id key for the specified nullable integer
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetNullableKey(ByVal value As Nullable(Of Integer)) As Integer

        If (value.HasValue) Then
            Return value.Value
        Else
            Return SECONDARY_STRATUM_ID_WILDCARD_VALUE
        End If

    End Function

    ''' <summary>
    ''' Gets a secondary stratum Id key for the specified cell
    ''' </summary>
    ''' <param name="simulationCell"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSecondaryStratumIdKey(ByVal simulationCell As Cell) As Integer
        Return GetNullableKey(simulationCell.SecondaryStratumId)
    End Function

End Module
