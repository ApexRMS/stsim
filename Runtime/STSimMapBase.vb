'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Imports SyncroSim.Core

MustInherit Class STSimMapBase

    Private m_Scenario As Scenario
    Private m_PrimaryStratumLabel As String
    Private m_SecondaryStratumLabel As String
    Private m_HasItems As Boolean

    Protected Sub New(ByVal scenario As Scenario)

        Me.m_Scenario = scenario

        Dim ds As DataSheet = scenario.Project.GetDataSheet(DATASHEET_TERMINOLOGY_NAME)
        GetStratumLabelTerminology(ds, Me.m_PrimaryStratumLabel, Me.m_SecondaryStratumLabel)

    End Sub

    Protected ReadOnly Property PrimaryStratumLabel As String
        Get
            Return Me.m_PrimaryStratumLabel
        End Get
    End Property

    Protected ReadOnly Property SecondaryStratumLabel As String
        Get
            Return Me.m_SecondaryStratumLabel
        End Get
    End Property

    Protected Sub SetHasItems()
        Me.m_HasItems = True
    End Sub

    Public ReadOnly Property HasItems As Boolean
        Get
            Return Me.m_HasItems
        End Get
    End Property

    Protected Shared Sub ThrowDuplicateItemException()
        Throw New STSimMapDuplicateItemException("An item with the same keys has already been added.")
    End Sub

    Protected Shared Function FormatValue(ByVal value As Nullable(Of Integer)) As String

        If (Not value.HasValue) Then
            Return "NULL"
        Else
            Return CStr(value)
        End If

    End Function

    Protected Function GetStratumName(ByVal id As Nullable(Of Integer)) As String
        Return Me.GetDefinitionName(DATASHEET_STRATA_NAME, id)
    End Function

    Protected Function GetSecondaryStratumName(ByVal id As Nullable(Of Integer)) As String
        Return Me.GetDefinitionName(DATASHEET_SECONDARY_STRATA_NAME, id)
    End Function

    Protected Function GetStateClassName(ByVal id As Nullable(Of Integer)) As String
        Return Me.GetDefinitionName(DATASHEET_STATECLASS_NAME, id)
    End Function

    Protected Function GetTransitionGroupName(ByVal id As Nullable(Of Integer)) As String
        Return Me.GetDefinitionName(DATASHEET_TRANSITION_GROUP_NAME, id)
    End Function

    Protected Function GetTransitionTypeName(ByVal id As Nullable(Of Integer)) As String
        Return Me.GetDefinitionName(DATASHEET_TRANSITION_TYPE_NAME, id)
    End Function

    Protected Function GetTransitionAttributeTypeName(ByVal id As Nullable(Of Integer)) As String
        Return Me.GetDefinitionName(DATASHEET_TRANSITION_ATTRIBUTE_TYPE_NAME, id)
    End Function

    Protected Function GetDefinitionName(ByVal dataSheetName As String, ByVal id As Nullable(Of Integer)) As String

        If (Not id.HasValue) Then
            Return "NULL"
        Else

            Dim ds As DataSheet = Me.m_Scenario.Project.GetDataSheet(dataSheetName)
            Return ds.ValidationTable.GetDisplayName(id.Value)

        End If

    End Function

End Class
