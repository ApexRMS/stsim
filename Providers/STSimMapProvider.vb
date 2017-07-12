'*********************************************************************************************
' ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Imports System.Reflection
Imports System.Globalization
Imports SyncroSim.Core
Imports SyncroSim.StochasticTime.Forms

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class STSimMapProvider
    Inherits MapProvider

    Public Overrides Sub CreateColorMaps(project As Project)

        'STATECLASS Color Map and Legend Map
        Dim dicLegendColors = CreateLegendMap(project, SPATIAL_MAP_STATE_CLASS_VARIABLE_NAME, DATASHEET_STATECLASS_NAME)
        CreateColorMap(project, SPATIAL_MAP_STATE_CLASS_VARIABLE_NAME, DATASHEET_STATECLASS_NAME, dicLegendColors)

        'Primary Stratum Color Map and Legend Map
        dicLegendColors = CreateLegendMap(project, SPATIAL_MAP_STRATUM_VARIABLE_NAME, DATASHEET_STRATA_NAME)
        CreateColorMap(project, SPATIAL_MAP_STRATUM_VARIABLE_NAME, DATASHEET_STRATA_NAME, dicLegendColors)

        'Transition Groups Color Map and Legend Map
        CreateTransitionGroupMaps(project)

        'Age  Color Map 
        CreateAgeColorMap(project)

    End Sub

    Public Overrides Sub RefreshCriteria(layout As SyncroSimLayout, project As Project)

        Using store As DataStore = project.Library.CreateDataStore

            Dim g0 As New SyncroSimLayoutItem("BasicGroup", "Basic", True)
            Dim g1 As New SyncroSimLayoutItem("TransitionsGroup", "Transitions", True)
            Dim g2 As New SyncroSimLayoutItem("AATPGroup", "Average Annual Probability", True)
            Dim g3 As New SyncroSimLayoutItem("StateAttributeGroup", "State Attributes", True)
            Dim g4 As New SyncroSimLayoutItem("TransitionAttributeGroup", "Transition Attributes", True)

            Dim AttrGroupView As DataView = CreateMapAttributeGroupsView(project, store)

            'Basic
            AddBasicVariables(project, g0)

            'Transitions
            AddMapTransitionGroupVariables(project, store, g1.Items,
                "STSim_OutputSpatialTransition", "Filename", "TransitionGroupID", "(Transitions)",
                SPATIAL_MAP_TRANSITION_GROUP_VARIABLE_PREFIX, False)

            'Average Annual Transition Probability
            AddMapTransitionGroupVariables(project, store, g2.Items,
                "STSim_OutputSpatialAverageTransitionProbability", "Filename", "TransitionGroupID", "(Avg. Annual Prob. - All Iterations)",
                SPATIAL_MAP_AVG_ANNUAL_TRANSITION_PROBABILITY_VARIABLE_PREFIX, True)

            'State Attributes
            AddMapStateAttributes(g3.Items, project, store, AttrGroupView)

            'Transition Attributes
            AddMapTransitionAttributes(g4.Items, project, store, AttrGroupView)

            layout.Items.Add(g0)

            If (g1.Items.Count > 0) Then
                layout.Items.Add(g1)
            End If

            If (g2.Items.Count > 0) Then
                layout.Items.Add(g2)
            End If

            If (g3.Items.Count > 0) Then
                layout.Items.Add(g3)
            End If

            If (g4.Items.Count > 0) Then
                layout.Items.Add(g4)
            End If

        End Using

    End Sub

    Private Shared Function CreateMapAttributeGroupsView(ByVal project As Project, ByVal store As DataStore) As DataView

        Dim ds As DataSheet = project.GetDataSheet(DATASHEET_ATTRIBUTE_GROUP_NAME)

        Dim View As New DataView(
            ds.GetData(store),
            Nothing,
            ds.ValidationTable.DisplayMember,
            DataViewRowState.CurrentRows)

        Return View

    End Function

    Private Shared Sub AddBasicVariables(ByVal project As Project, ByVal g0 As SyncroSimLayoutItem)

        Dim psl As String = Nothing
        Dim ssl As String = Nothing
        Dim dsterm As DataSheet = project.GetDataSheet(DATASHEET_TERMINOLOGY_NAME)
        GetStratumLabelTerminology(dsterm, psl, ssl)

        Dim i1 As SyncroSimLayoutItem = New SyncroSimLayoutItem(SPATIAL_MAP_STATE_CLASS_VARIABLE_NAME, "State Class", False)
        Dim i2 As SyncroSimLayoutItem = New SyncroSimLayoutItem(SPATIAL_MAP_AGE_VARIABLE_NAME, "Age", False)
        Dim i3 As SyncroSimLayoutItem = New SyncroSimLayoutItem(SPATIAL_MAP_STRATUM_VARIABLE_NAME, psl, False)

        i1.Properties.Add(New MetaDataProperty("dataSheet", "STSim_OutputSpatialState"))
        i1.Properties.Add(New MetaDataProperty("column", "Filename"))

        i2.Properties.Add(New MetaDataProperty("dataSheet", "STSim_OutputSpatialAge"))
        i2.Properties.Add(New MetaDataProperty("column", "Filename"))

        i3.Properties.Add(New MetaDataProperty("dataSheet", "STSim_OutputSpatialStratum"))
        i3.Properties.Add(New MetaDataProperty("column", "Filename"))

        g0.Items.Add(i1)
        g0.Items.Add(i2)
        g0.Items.Add(i3)

    End Sub

    Private Shared Sub AddMapTransitionGroupVariables(
        ByVal project As Project,
        ByVal store As DataStore,
        ByVal items As SyncroSimLayoutItemCollection,
        ByVal dataSheetName As String,
        ByVal fileColumnName As String,
        ByVal filterColumnName As String,
        ByVal extendedIdentifier As String,
        ByVal prefix As String,
        ByVal userDefinedColorMap As Boolean)

        Dim dstg As DataSheet = project.GetDataSheet(DATASHEET_TRANSITION_GROUP_NAME)
        Dim dsttg As DataSheet = project.GetDataSheet(DATASHEET_TRANSITION_TYPE_GROUP_NAME)
        Dim dttg As DataTable = dstg.GetData(store)
        Dim dtttg As DataTable = dsttg.GetData(store)

        Dim dvtg As New DataView(
            dttg,
            Nothing,
            dstg.ValidationTable.DisplayMember,
            DataViewRowState.CurrentRows)

        For Each drv As DataRowView In dvtg

            Dim tgid As Integer = CInt(drv.Row(dstg.ValidationTable.ValueMember))
            Dim query As String = String.Format(CultureInfo.InvariantCulture, "{0}={1}", DATASHEET_TRANSITION_GROUP_ID_COLUMN_NAME, tgid)
            Dim rows() As DataRow = dtttg.Select(query)

            For Each dr As DataRow In rows

                If (IsPrimaryTypeByGroup(dr)) Then

                    Dim DisplayName As String = CStr(drv.Row(dstg.ValidationTable.DisplayMember))
                    Dim VarName As String = String.Format(CultureInfo.InvariantCulture, "{0}-{1}", prefix, tgid)

                    Dim Item As New SyncroSimLayoutItem(VarName, DisplayName, False)

                    Item.Properties.Add(New MetaDataProperty("dataSheet", dataSheetName))
                    Item.Properties.Add(New MetaDataProperty("column", fileColumnName))
                    Item.Properties.Add(New MetaDataProperty("filter", filterColumnName))
                    Item.Properties.Add(New MetaDataProperty("extendedIdentifier", extendedIdentifier))
                    Item.Properties.Add(New MetaDataProperty("itemId", tgid.ToString(CultureInfo.InvariantCulture)))
                    Item.Properties.Add(New MetaDataProperty("userDefinedColorMap", userDefinedColorMap.ToString(CultureInfo.InvariantCulture)))

                    items.Add(Item)

                    Exit For

                End If

            Next

        Next

    End Sub

    Private Shared Sub AddMapStateAttributes(
        ByVal items As SyncroSimLayoutItemCollection,
        ByVal project As Project,
        ByVal store As DataStore,
        ByVal attrGroupsView As DataView)

        Dim StateAttrsDataSheet As DataSheet =
            project.GetDataSheet(DATASHEET_STATE_ATTRIBUTE_TYPE_NAME)

        AddMapNonGroupedAttributes(
            store,
            items,
            StateAttrsDataSheet,
            "STSim_OutputSpatialStateAttribute",
            "Filename",
            "StateAttributeTypeID",
            SPATIAL_MAP_STATE_ATTRIBUTE_VARIABLE_PREFIX)

        Dim GroupsDict As New Dictionary(Of String, SyncroSimLayoutItem)
        Dim GroupsList As New List(Of SyncroSimLayoutItem)

        For Each drv As DataRowView In attrGroupsView

            Dim GroupName As String = CStr(drv.Row(DATASHEET_NAME_COLUMN_NAME))
            Dim Group As New SyncroSimLayoutItem(GroupName, GroupName, True)

            GroupsDict.Add(GroupName, Group)
            GroupsList.Add(Group)

        Next

        AddMapGroupedAttributes(
            store,
            GroupsDict,
            StateAttrsDataSheet,
            "STSim_OutputSpatialStateAttribute",
            "Filename",
            "StateAttributeTypeID",
            SPATIAL_MAP_STATE_ATTRIBUTE_VARIABLE_PREFIX)

        For Each g As SyncroSimLayoutItem In GroupsList

            If (g.Items.Count > 0) Then
                items.Add(g)
            End If

        Next

    End Sub

    Private Shared Sub AddMapTransitionAttributes(
        ByVal items As SyncroSimLayoutItemCollection,
        ByVal project As Project,
        ByVal store As DataStore,
        ByVal attrGroupsView As DataView)

        Dim TransitionAttrsDataSheet As DataSheet =
            project.GetDataSheet(DATASHEET_TRANSITION_ATTRIBUTE_TYPE_NAME)

        AddMapNonGroupedAttributes(
            store,
            items,
            TransitionAttrsDataSheet,
            "STSim_OutputSpatialTransitionAttribute",
            "Filename",
            "TransitionAttributeTypeID",
            SPATIAL_MAP_TRANSITION_ATTRIBUTE_VARIABLE_PREFIX)

        Dim GroupsDict As New Dictionary(Of String, SyncroSimLayoutItem)
        Dim GroupsList As New List(Of SyncroSimLayoutItem)

        For Each drv As DataRowView In attrGroupsView

            Dim GroupName As String = CStr(drv.Row(DATASHEET_NAME_COLUMN_NAME))
            Dim Group As New SyncroSimLayoutItem(GroupName, GroupName, True)

            GroupsDict.Add(GroupName, Group)
            GroupsList.Add(Group)

        Next

        AddMapGroupedAttributes(
            store,
            GroupsDict,
            TransitionAttrsDataSheet,
            "STSim_OutputSpatialTransitionAttribute",
            "Filename",
            "TransitionAttributeTypeID",
            SPATIAL_MAP_TRANSITION_ATTRIBUTE_VARIABLE_PREFIX)

        For Each g As SyncroSimLayoutItem In GroupsList

            If (g.Items.Count > 0) Then
                items.Add(g)
            End If

        Next

    End Sub

    Private Shared Sub AddMapNonGroupedAttributes(
        ByVal store As DataStore,
        ByVal items As SyncroSimLayoutItemCollection,
        ByVal attrsDataSheet As DataSheet,
        ByVal dataSheetName As String,
        ByVal fileColumnName As String,
        ByVal filterColumnName As String,
        ByVal prefix As String)

        Dim Table As DataTable = attrsDataSheet.GetData(store)

        Dim View As New DataView(
            Table,
            Nothing,
            attrsDataSheet.ValidationTable.DisplayMember,
            DataViewRowState.CurrentRows)

        Debug.Assert(
            DATASHEET_STATE_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME =
            DATASHEET_TRANSITION_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME)

        For Each drv As DataRowView In View

            If (drv.Row(DATASHEET_ATTRIBUTE_GROUP_ID_COLUMN_NAME) Is DBNull.Value) Then

                Dim AttrId As Integer = CInt(drv.Row(attrsDataSheet.ValueMember))
                Dim AttrName As String = String.Format(CultureInfo.InvariantCulture, "{0}-{1}", prefix, AttrId)
                Dim DisplayName As String = CStr(drv.Row(attrsDataSheet.ValidationTable.DisplayMember))

                If (drv.Row(DATASHEET_STATE_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME) IsNot DBNull.Value) Then
                    DisplayName = String.Format(CultureInfo.InvariantCulture, "{0} ({1})", DisplayName, CStr(drv.Row(DATASHEET_STATE_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME)))
                End If

                Dim Item As New SyncroSimLayoutItem(AttrName, DisplayName, False)

                Item.Properties.Add(New MetaDataProperty("dataSheet", dataSheetName))
                Item.Properties.Add(New MetaDataProperty("column", fileColumnName))
                Item.Properties.Add(New MetaDataProperty("filter", filterColumnName))
                Item.Properties.Add(New MetaDataProperty("itemId", AttrId.ToString(CultureInfo.InvariantCulture)))
                Item.Properties.Add(New MetaDataProperty("userDefinedColorMap", "True"))

                items.Add(Item)

            End If

        Next

    End Sub

    Private Shared Sub AddMapGroupedAttributes(
        ByVal store As DataStore,
        ByVal groupsDict As Dictionary(Of String, SyncroSimLayoutItem),
        ByVal attrsDataSheet As DataSheet,
        ByVal dataSheetName As String,
        ByVal fileColumnName As String,
        ByVal filterColumnName As String,
        ByVal prefix As String)

        Dim Table As DataTable = attrsDataSheet.GetData(store)

        Dim View As New DataView(
            Table,
            Nothing,
            attrsDataSheet.ValidationTable.DisplayMember,
            DataViewRowState.CurrentRows)

        Dim GroupsDataSheet As DataSheet =
            attrsDataSheet.Project.GetDataSheet(DATASHEET_ATTRIBUTE_GROUP_NAME)

        Debug.Assert(
            DATASHEET_STATE_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME =
            DATASHEET_TRANSITION_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME)

        For Each drv As DataRowView In View

            If (drv.Row(DATASHEET_ATTRIBUTE_GROUP_ID_COLUMN_NAME) IsNot DBNull.Value) Then

                Dim GroupId As Integer = CType(drv.Row(DATASHEET_ATTRIBUTE_GROUP_ID_COLUMN_NAME), Integer)
                Dim GroupName As String = GroupsDataSheet.ValidationTable.GetDisplayName(GroupId)
                Dim AttrId As Integer = CInt(drv.Row(attrsDataSheet.ValueMember))
                Dim AttrName As String = String.Format(CultureInfo.InvariantCulture, "{0}-{1}", prefix, AttrId)
                Dim DisplayName As String = CStr(drv.Row(attrsDataSheet.ValidationTable.DisplayMember))

                If (drv.Row(DATASHEET_STATE_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME) IsNot DBNull.Value) Then
                    DisplayName = String.Format(CultureInfo.InvariantCulture, "{0} ({1})", DisplayName, CStr(drv.Row(DATASHEET_STATE_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME)))
                End If

                Dim Item As New SyncroSimLayoutItem(AttrName, DisplayName, False)

                Item.Properties.Add(New MetaDataProperty("dataSheet", dataSheetName))
                Item.Properties.Add(New MetaDataProperty("column", fileColumnName))
                Item.Properties.Add(New MetaDataProperty("filter", filterColumnName))
                Item.Properties.Add(New MetaDataProperty("itemId", AttrId.ToString(CultureInfo.InvariantCulture)))
                Item.Properties.Add(New MetaDataProperty("userDefinedColorMap", "True"))

                groupsDict(GroupName).Items.Add(Item)

            End If

        Next

    End Sub

End Class
