'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

''' <summary>
''' State attribute value class
''' </summary>
''' <remarks></remarks>
Friend Class StateAttributeValue
    Inherits AttributeValueBase

    Public Sub New(
        ByVal stateAttributeTypeId As Integer,
        ByVal stratumId As Nullable(Of Integer),
        ByVal secondaryStratumId As Nullable(Of Integer),
        ByVal iteration As Nullable(Of Integer),
        ByVal timestep As Nullable(Of Integer),
        ByVal stateClassId As Nullable(Of Integer),
        ByVal minimumAge As Nullable(Of Integer),
        ByVal maximumAge As Nullable(Of Integer),
        ByVal value As Double)

        MyBase.New(
            stateAttributeTypeId,
            stratumId,
            secondaryStratumId,
            iteration,
            timestep,
            StateClassId,
            MinimumAge,
            MaximumAge,
            Value)

    End Sub

End Class
