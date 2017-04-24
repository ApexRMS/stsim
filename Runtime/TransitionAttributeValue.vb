'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

''' <summary>
''' Transition Attribute Value class
''' </summary>
''' <remarks></remarks>
Class TransitionAttributeValue
    Inherits AttributeValueBase

    Private m_TransitionGroupId As Integer

    Public Sub New(
        ByVal transitionAttributeTypeId As Integer,
        ByVal stratumId As Nullable(Of Integer),
        ByVal secondaryStratumId As Nullable(Of Integer),
        ByVal iteration As Nullable(Of Integer),
        ByVal timestep As Nullable(Of Integer),
        ByVal transitionGroupId As Integer,
        ByVal stateClassId As Nullable(Of Integer),
        ByVal minimumAge As Nullable(Of Integer),
        ByVal maximumAge As Nullable(Of Integer),
        ByVal value As Double)

        MyBase.New(
            transitionAttributeTypeId,
            stratumId,
            secondaryStratumId,
            iteration,
            timestep,
            stateClassId,
            minimumAge,
            maximumAge,
            value)

        Me.m_TransitionGroupId = transitionGroupId

    End Sub

    Public ReadOnly Property TransitionGroupId As Integer
        Get
            Return Me.m_TransitionGroupId
        End Get
    End Property

End Class
