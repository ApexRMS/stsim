'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2009-2015 ApexRMS.
'
'************************************************************************************

Module DataSheetUtilities

    Public Function IsPrimaryTypeByGroup(ByVal dr As DataRow) As Boolean

        If (dr(DATASHEET_TRANSITION_TYPE_GROUP_PRIMARY_COLUMN_NAME) Is DBNull.Value) Then
            Return True
        End If

        Return CBool(dr(DATASHEET_TRANSITION_TYPE_GROUP_PRIMARY_COLUMN_NAME))

    End Function

End Module
