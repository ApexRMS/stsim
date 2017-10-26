'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

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

    ''' <summary>
    ''' Gets a tertiary stratum Id key for the specified cell
    ''' </summary>
    ''' <param name="simulationCell"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTertiaryStratumIdKey(ByVal simulationCell As Cell) As Integer
        Return GetNullableKey(simulationCell.TertiaryStratumId)
    End Function

End Module
