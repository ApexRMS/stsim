'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Module LookupKeyUtils

    Public Function GetOutputCollectionKey(ByVal stratumId As Nullable(Of Integer)) As Integer

        If (stratumId.HasValue) Then
            Return stratumId.Value
        Else
            Return OUTPUT_COLLECTION_WILDCARD_KEY
        End If

    End Function

End Module
