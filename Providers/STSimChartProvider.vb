'************************************************************************************
' ST-Sim: A .NET class library for developing state-and-transition simulation models.
'
' Copyright © 2007-2017 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'************************************************************************************

Imports SyncroSim.Core
Imports SyncroSim.StochasticTime.Forms
Imports System.Globalization
Imports System.Reflection

<ObfuscationAttribute(Exclude:=True, ApplyToMembers:=False)>
Class STSimChartProvider
    Inherits ChartProvider

    Const DENSITY_GROUP_NAME As String = "stsim_density_group"

    Public Overrides Sub RefreshCriteria(layout As SyncroSimLayout, project As Project)

        Using store As DataStore = project.Library.CreateDataStore

            Dim g0 As New SyncroSimLayoutItem("StateClassGroup", "State Classes", True)
            Dim g1 As New SyncroSimLayoutItem("TransitionGroup", "Transitions", True)
            Dim g2 As New SyncroSimLayoutItem("StateAttributeGroup", "State Attributes", True)
            Dim g3 As New SyncroSimLayoutItem("TransitionAttributeGroup", "Transition Attributes", True)

            Dim AttrGroupDataSheet As DataSheet = project.GetDataSheet(DATASHEET_ATTRIBUTE_GROUP_NAME)
            Dim AttrGroupView As DataView = CreateChartAttributeGroupsView(project, store)
            Dim StateAttrDataSheet As DataSheet = project.GetDataSheet(DATASHEET_STATE_ATTRIBUTE_TYPE_NAME)
            Dim StateAttrDataView As New DataView(StateAttrDataSheet.GetData(store), Nothing, StateAttrDataSheet.ValidationTable.DisplayMember, DataViewRowState.CurrentRows)
            Dim TransitionAttrDataSheet As DataSheet = project.GetDataSheet(DATASHEET_TRANSITION_ATTRIBUTE_TYPE_NAME)
            Dim TransitionAttrDataView As New DataView(TransitionAttrDataSheet.GetData(store), Nothing, TransitionAttrDataSheet.ValidationTable.DisplayMember, DataViewRowState.CurrentRows)

            g0.Properties.Add(New MetaDataProperty("dataSheet", "STSim_OutputStratumState"))
            g0.Properties.Add(New MetaDataProperty("column", "Amount"))
            g0.Properties.Add(New MetaDataProperty("filter", "StratumID|SecondaryStratumID|StateClassID|StateLabelXID|StateLabelYID|AgeClass"))

            g1.Properties.Add(New MetaDataProperty("dataSheet", "STSim_OutputStratumTransition"))
            g1.Properties.Add(New MetaDataProperty("column", "Amount"))
            g1.Properties.Add(New MetaDataProperty("filter", "StratumID|SecondaryStratumID|TransitionGroupID|AgeClass"))

            g2.Properties.Add(New MetaDataProperty("dataSheet", "STSim_OutputStateAttribute"))
            g2.Properties.Add(New MetaDataProperty("column", "Amount"))
            g2.Properties.Add(New MetaDataProperty("filter", "StratumID|SecondaryStratumID|AgeClass"))

            g3.Properties.Add(New MetaDataProperty("dataSheet", "STSim_OutputTransitionAttribute"))
            g3.Properties.Add(New MetaDataProperty("column", "Amount"))
            g3.Properties.Add(New MetaDataProperty("filter", "StratumID|SecondaryStratumID|AgeClass"))

            RefreshChartAgeClassValidationTable(DATASHEET_OUTPUT_STRATUM_STATE_NAME, project)
            RefreshChartAgeClassValidationTable(DATASHEET_OUTPUT_STRATUM_TRANSITION_NAME, project)
            RefreshChartAgeClassValidationTable(DATASHEET_OUTPUT_STATE_ATTRIBUTE_NAME, project)
            RefreshChartAgeClassValidationTable(DATASHEET_OUTPUT_TRANSITION_ATTRIBUTE_NAME, project)

            AddChartStateClassVariables(g0.Items, project)
            AddChartTransitionVariables(g1.Items, project)

            AddChartAttributeVariables(
                g2.Items, AttrGroupView, AttrGroupDataSheet, StateAttrDataView,
                StateAttrDataSheet, DATASHEET_OUTPUT_STATE_ATTRIBUTE_NAME, DATASHEET_STATE_ATTRIBUTE_TYPE_ID_COLUMN_NAME, False)

            AddChartAttributeVariables(
                g3.Items, AttrGroupView, AttrGroupDataSheet, TransitionAttrDataView,
                TransitionAttrDataSheet, DATASHEET_OUTPUT_TRANSITION_ATTRIBUTE_NAME, DATASHEET_TRANSITION_ATTRIBUTE_TYPE_ID_COLUMN_NAME, True)

            layout.Items.Add(g0)
            layout.Items.Add(g1)

            If (g2.Items.Count > 0) Then
                layout.Items.Add(g2)
            End If

            If (g3.Items.Count > 0) Then
                layout.Items.Add(g3)
            End If

        End Using

    End Sub

    Public Overrides Sub PrepareData(
        ByVal store As DataStore,
        ByVal descriptors As ChartDescriptorCollection,
        ByVal statusEntries As StochasticTimeStatusCollection,
        ByVal project As Project)

        If (Not HasAgeReference(descriptors)) Then
            Return
        End If

        Dim Sheets As New List(Of String)

        For Each d As ChartDescriptor In descriptors

            If (d.DataSheetName = DATASHEET_OUTPUT_STRATUM_STATE_NAME Or
                d.DataSheetName = DATASHEET_OUTPUT_STRATUM_TRANSITION_NAME Or
                d.DataSheetName = DATASHEET_OUTPUT_STATE_ATTRIBUTE_NAME Or
                d.DataSheetName = DATASHEET_OUTPUT_TRANSITION_ATTRIBUTE_NAME) Then

                If (Not Sheets.Contains(d.DataSheetName)) Then
                    Sheets.Add(d.DataSheetName)
                End If

            End If

        Next

        For Each s As Scenario In project.Results

            If (s.IsActive) Then

                For Each n As String In Sheets

                    Dim ds As DataSheet = s.GetDataSheet(n)

                    UpdateAgeClassColumn(store, ds)
                    GetAgeRelatedStatusEntries(store, ds, statusEntries)

                Next

            End If

        Next

    End Sub

    Public Overrides Function GetData(
        ByVal store As DataStore,
        ByVal descriptor As ChartDescriptor,
        ByVal dataSheet As DataSheet) As DataTable

        If (descriptor.DataSheetName = DATASHEET_OUTPUT_STRATUM_STATE_NAME Or
            descriptor.DataSheetName = DATASHEET_OUTPUT_STRATUM_TRANSITION_NAME) Then

            If (descriptor.VariableName = STATE_CLASS_PROPORTION_VARIABLE_NAME) Then
                Return CreateProportionChartData(dataSheet.Scenario, descriptor, DATASHEET_OUTPUT_STRATUM_STATE_NAME, store)
            ElseIf (descriptor.VariableName = TRANSITION_PROPORTION_VARIABLE_NAME) Then
                Return CreateProportionChartData(dataSheet.Scenario, descriptor, DATASHEET_OUTPUT_STRATUM_TRANSITION_NAME, store)
            Else
                Return Nothing
            End If

        ElseIf (descriptor.DataSheetName = DATASHEET_OUTPUT_STATE_ATTRIBUTE_NAME Or
                descriptor.DataSheetName = DATASHEET_OUTPUT_TRANSITION_ATTRIBUTE_NAME) Then

            Dim s() As String = descriptor.VariableName.Split(CChar("-"))

            Debug.Assert(s.Count = 2)
            Debug.Assert(s(0) = "attrnormal" Or s(0) = "attrdensity")

            Dim AttrId As Integer = CInt(s(1))
            Dim IsDensity As Boolean = (s(0) = "attrdensity")
            Dim ColumnName As String = Nothing

            If (descriptor.DataSheetName = DATASHEET_OUTPUT_STATE_ATTRIBUTE_NAME) Then
                ColumnName = DATASHEET_STATE_ATTRIBUTE_TYPE_ID_COLUMN_NAME
            Else
                ColumnName = DATASHEET_TRANSITION_ATTRIBUTE_TYPE_ID_COLUMN_NAME
            End If

            Return CreateRawAttributeChartData(
                dataSheet.Scenario,
                descriptor,
                dataSheet.Name,
                ColumnName,
                AttrId,
                IsDensity,
                store)

        End If

        Return Nothing

    End Function

    Private Shared Sub AddChartStateClassVariables(ByVal items As SyncroSimLayoutItemCollection, ByVal project As Project)

        Dim AmountLabel As String = Nothing
        Dim UnitsLabel As String = Nothing
        Dim TermUnit As TerminologyUnit
        Dim dsterm As DataSheet = project.GetDataSheet(DATASHEET_TERMINOLOGY_NAME)

        GetAmountLabelTerminology(dsterm, AmountLabel, TermUnit)
        UnitsLabel = TerminologyUnitToString(TermUnit)

        Dim disp As String = String.Format(CultureInfo.CurrentCulture, "{0} ({1})", AmountLabel, UnitsLabel)
        Dim Normal As New SyncroSimLayoutItem(STATE_CLASS_AMOUNT_VARIABLE_NAME, disp, False)
        Dim Proportion As New SyncroSimLayoutItem(STATE_CLASS_PROPORTION_VARIABLE_NAME, "Proportion", False)

        Normal.Properties.Add(New MetaDataProperty("dataSheet", "STSim_OutputStratumState"))
        Proportion.Properties.Add(New MetaDataProperty("dataSheet", "STSim_OutputStratumState"))

        Normal.Properties.Add(New MetaDataProperty("column", "Amount"))
        Proportion.Properties.Add(New MetaDataProperty("column", "Amount"))

        items.Add(Normal)
        items.Add(Proportion)

    End Sub

    Private Shared Sub AddChartTransitionVariables(ByVal items As SyncroSimLayoutItemCollection, ByVal project As Project)

        Dim AmountLabel As String = Nothing
        Dim UnitsLabel As String = Nothing
        Dim TermUnit As TerminologyUnit
        Dim dsterm As DataSheet = project.GetDataSheet(DATASHEET_TERMINOLOGY_NAME)

        GetAmountLabelTerminology(dsterm, AmountLabel, TermUnit)
        UnitsLabel = TerminologyUnitToString(TermUnit)

        Dim disp As String = String.Format(CultureInfo.CurrentCulture, "{0} ({1})", AmountLabel, UnitsLabel)
        Dim Normal As New SyncroSimLayoutItem(TRANSITION_AMOUNT_VARIABLE_NAME, disp, False)
        Dim Proportion As New SyncroSimLayoutItem(TRANSITION_PROPORTION_VARIABLE_NAME, "Proportion", False)

        Normal.Properties.Add(New MetaDataProperty("dataSheet", "STSim_OutputStratumTransition"))
        Proportion.Properties.Add(New MetaDataProperty("dataSheet", "STSim_OutputStratumTransition"))

        Normal.Properties.Add(New MetaDataProperty("column", "Amount"))
        Proportion.Properties.Add(New MetaDataProperty("column", "Amount"))

        Normal.Properties.Add(New MetaDataProperty("skipTimestepZero", "True"))
        Proportion.Properties.Add(New MetaDataProperty("skipTimestepZero", "True"))

        items.Add(Normal)
        items.Add(Proportion)

    End Sub

    Private Shared Sub AddChartAttributeVariables(
        ByVal items As SyncroSimLayoutItemCollection,
        ByVal attrGroupView As DataView,
        ByVal attrGroupDataSheet As DataSheet,
        ByVal attrView As DataView,
        ByVal attrDataSheet As DataSheet,
        ByVal outputTableName As String,
        ByVal attributeTypeColumnName As String,
        ByVal skipTimestepZero As Boolean)

        Debug.Assert(
            DATASHEET_STATE_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME =
            DATASHEET_TRANSITION_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME)

        Dim NonGroupedDensityGroup As New SyncroSimLayoutItem(
            DENSITY_GROUP_NAME & "STSIM_NON_GROUPED", "Density", True)

        AddChartNonGroupedAttributes(
            items,
            attrView,
            attrDataSheet,
            outputTableName,
            attributeTypeColumnName,
            skipTimestepZero,
            NonGroupedDensityGroup)

        If (NonGroupedDensityGroup.Items.Count > 0) Then
            items.Add(NonGroupedDensityGroup)
        End If

        Dim GroupsDict As New Dictionary(Of String, SyncroSimLayoutItem)
        Dim GroupsList As New List(Of SyncroSimLayoutItem)

        For Each drv As DataRowView In attrGroupView

            Dim GroupName As String = CStr(drv.Row(DATASHEET_NAME_COLUMN_NAME))
            Dim DensityGroupName As String = DENSITY_GROUP_NAME & GroupName
            Dim Group As New SyncroSimLayoutItem(GroupName, GroupName, True)
            Dim DensityGroup As New SyncroSimLayoutItem(DensityGroupName, "Density", True)

            GroupsDict.Add(GroupName, Group)
            GroupsList.Add(Group)

            GroupsDict.Add(DensityGroupName, DensityGroup)

        Next

        AddChartGroupedAttributes(
            GroupsDict,
            attrGroupDataSheet,
            attrView,
            attrDataSheet,
            outputTableName,
            attributeTypeColumnName,
            skipTimestepZero)

        For Each g As SyncroSimLayoutItem In GroupsList

            If (g.Items.Count > 0) Then

                Dim DensityGroupName As String = DENSITY_GROUP_NAME & g.Name

                g.Items.Add(GroupsDict(DensityGroupName))
                items.Add(g)

            End If

        Next

    End Sub

    Private Shared Sub AddChartNonGroupedAttributes(
        ByVal items As SyncroSimLayoutItemCollection,
        ByVal attrsView As DataView,
        ByVal attrsDataSheet As DataSheet,
        ByVal outputDataSheetName As String,
        ByVal outputColumnName As String,
        ByVal skipTimestepZero As Boolean,
        ByVal densityGroup As SyncroSimLayoutItem)

        For Each drv As DataRowView In attrsView

            If (drv.Row(DATASHEET_ATTRIBUTE_GROUP_ID_COLUMN_NAME) Is DBNull.Value) Then

                Dim AttrId As Integer = CInt(drv.Row(attrsDataSheet.ValueMember))
                Dim Units As String = DataTableUtilities.GetDataStr(drv.Row, DATASHEET_STATE_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME)

                'Normal Attribute
                '----------------

                Dim AttrNameNormal As String = String.Format(CultureInfo.InvariantCulture, "attrnormal-{0}", AttrId)
                Dim DisplayNameNormal As String = CStr(drv.Row(attrsDataSheet.ValidationTable.DisplayMember))

                If (Units IsNot Nothing) Then
                    DisplayNameNormal = String.Format(CultureInfo.CurrentCulture, "{0} ({1})", DisplayNameNormal, Units)
                End If

                Dim ItemNormal As New SyncroSimLayoutItem(AttrNameNormal, DisplayNameNormal, False)

                ItemNormal.Properties.Add(New MetaDataProperty("dataSheet", outputDataSheetName))
                ItemNormal.Properties.Add(New MetaDataProperty("column", outputColumnName))
                ItemNormal.Properties.Add(New MetaDataProperty("prefixFolderName", "False"))
                ItemNormal.Properties.Add(New MetaDataProperty("customTitle", DisplayNameNormal))

                If (skipTimestepZero) Then
                    ItemNormal.Properties.Add(New MetaDataProperty("skipTimestepZero", "True"))
                End If

                items.Add(ItemNormal)

                'Density Attribute
                '-----------------

                Dim AttrNameDensity As String = String.Format(CultureInfo.InvariantCulture, "attrdensity-{0}", AttrId)
                Dim DisplayNameDensity As String = CStr(drv.Row(attrsDataSheet.ValidationTable.DisplayMember))

                If (Units IsNot Nothing) Then
                    DisplayNameDensity = String.Format(CultureInfo.CurrentCulture, "{0} ({1})", DisplayNameDensity, Units)
                End If

                Dim ItemDensity As New SyncroSimLayoutItem(AttrNameDensity, DisplayNameDensity, False)

                ItemDensity.Properties.Add(New MetaDataProperty("dataSheet", outputDataSheetName))
                ItemDensity.Properties.Add(New MetaDataProperty("column", outputColumnName))
                ItemDensity.Properties.Add(New MetaDataProperty("prefixFolderName", "False"))
                ItemDensity.Properties.Add(New MetaDataProperty("customTitle", "(Density): " & DisplayNameNormal))

                If (skipTimestepZero) Then
                    ItemDensity.Properties.Add(New MetaDataProperty("skipTimestepZero", "True"))
                End If

                densityGroup.Items.Add(ItemDensity)

            End If

        Next

    End Sub

    Private Shared Sub AddChartGroupedAttributes(
        ByVal groupsDict As Dictionary(Of String, SyncroSimLayoutItem),
        ByVal groupsDataSheet As DataSheet,
        ByVal attrsView As DataView,
        ByVal attrsDataSheet As DataSheet,
        ByVal outputDataSheetName As String,
        ByVal outputColumnName As String,
        ByVal skipTimestepZero As Boolean)

        'The density groups have already been created and added to the groups.  Howver, we want the
        'attributes themselves to appear before this group so we must insert them in reverse order.

        For i As Integer = attrsView.Count - 1 To 0 Step -1

            Dim drv As DataRowView = attrsView(i)

            If (drv.Row(DATASHEET_ATTRIBUTE_GROUP_ID_COLUMN_NAME) IsNot DBNull.Value) Then

                Dim GroupId As Integer = CType(drv.Row(DATASHEET_ATTRIBUTE_GROUP_ID_COLUMN_NAME), Integer)
                Dim GroupName As String = groupsDataSheet.ValidationTable.GetDisplayName(GroupId)
                Dim AttrId As Integer = CInt(drv.Row(attrsDataSheet.ValueMember))
                Dim MainGroup As SyncroSimLayoutItem = groupsDict(GroupName)
                Dim DensityGroup As SyncroSimLayoutItem = groupsDict(DENSITY_GROUP_NAME & GroupName)
                Dim Units As String = DataTableUtilities.GetDataStr(drv.Row, DATASHEET_STATE_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME)

                'Normal Attribute
                '----------------

                Dim AttrNameNormal As String = String.Format(CultureInfo.InvariantCulture, "attrnormal-{0}", AttrId)
                Dim DisplayNameNormal As String = CStr(drv.Row(attrsDataSheet.ValidationTable.DisplayMember))

                If (Units IsNot Nothing) Then
                    DisplayNameNormal = String.Format(CultureInfo.CurrentCulture, "{0} ({1})", DisplayNameNormal, Units)
                End If

                Dim ItemNormal As New SyncroSimLayoutItem(AttrNameNormal, DisplayNameNormal, False)

                ItemNormal.Properties.Add(New MetaDataProperty("dataSheet", outputDataSheetName))
                ItemNormal.Properties.Add(New MetaDataProperty("column", outputColumnName))
                ItemNormal.Properties.Add(New MetaDataProperty("prefixFolderName", "False"))
                ItemNormal.Properties.Add(New MetaDataProperty("customTitle", GroupName & ": " & DisplayNameNormal))

                If (skipTimestepZero) Then
                    ItemNormal.Properties.Add(New MetaDataProperty("skipTimestepZero", "True"))
                End If

                MainGroup.Items.Insert(0, ItemNormal)

                'Density Attribute
                '-----------------

                Dim AttrNameDensity As String = String.Format(CultureInfo.InvariantCulture, "attrdensity-{0}", AttrId)
                Dim DisplayNameDensity As String = CStr(drv.Row(attrsDataSheet.ValidationTable.DisplayMember))

                If (Units IsNot Nothing) Then
                    DisplayNameDensity = String.Format(CultureInfo.CurrentCulture, "{0} ({1})", DisplayNameDensity, Units)
                End If

                Dim ItemDensity As New SyncroSimLayoutItem(AttrNameDensity, DisplayNameDensity, False)

                ItemDensity.Properties.Add(New MetaDataProperty("dataSheet", outputDataSheetName))
                ItemDensity.Properties.Add(New MetaDataProperty("column", outputColumnName))
                ItemDensity.Properties.Add(New MetaDataProperty("prefixFolderName", "False"))
                ItemDensity.Properties.Add(New MetaDataProperty("customTitle", GroupName & " (Density): " & DisplayNameNormal))

                If (skipTimestepZero) Then
                    ItemDensity.Properties.Add(New MetaDataProperty("skipTimestepZero", "True"))
                End If

                DensityGroup.Items.Insert(0, ItemDensity)

            End If

        Next

    End Sub

    Private Shared Function CreateChartAttributeGroupsView(ByVal project As Project, ByVal store As DataStore) As DataView

        Dim ds As DataSheet = project.GetDataSheet(DATASHEET_ATTRIBUTE_GROUP_NAME)

        Dim View As New DataView(
            ds.GetData(store),
            Nothing,
            ds.ValidationTable.DisplayMember,
            DataViewRowState.CurrentRows)

        Return View

    End Function

    Private Shared Sub RefreshChartAgeClassValidationTable(ByVal dataSheetName As String, ByVal project As Project)

        For Each s As Scenario In project.Results

            Dim ds As DataSheet = s.GetDataSheet(dataSheetName)
            Dim col As DataSheetColumn = ds.Columns(DATASHEET_AGE_CLASS_COLUMN_NAME)

            col.ValidationTable = CreateAgeValidationTable(project)

        Next

    End Sub

End Class
