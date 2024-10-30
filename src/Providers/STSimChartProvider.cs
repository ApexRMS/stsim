// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2024 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Linq;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using SyncroSim.Core;
using SyncroSim.Core.Forms;

namespace SyncroSim.STSim
{
    internal class STSimChartProvider : ChartProvider
    {
        bool ShowMultiResolutionCriteriaNodes = false;

        public override void RefreshCriteria(DataStore store, Layout layout, Project project)
        {
            this.ShowMultiResolutionCriteriaNodes = ShouldShowMultiResolutionCriteriaNodes(project, store);

            LayoutItem StateClassGroup = new LayoutItem("stsim_StateClassVariableGroup", "State Classes", true);
            LayoutItem TransitionGroup = new LayoutItem("stsim_TransitionVariableGroup", "Transitions", true);
            LayoutItem TSTGroup = new LayoutItem("stsim_TSTGroup", "Time-Since-Transition", true);
            LayoutItem StateAttributeGroup = new LayoutItem("stsim_StateAttributeVariableGroup", "State Attributes", true);
            LayoutItem TransitionAttributeGroup = new LayoutItem("stsim_TransitionAttributeVariableGroup", "Transition Attributes", true);
            LayoutItem ExternalVariableGroup = new LayoutItem("stsim_ExternalVariableGroup", "External Variables", true);

            DataSheet AttrGroupDataSheet = project.GetDataSheet(Strings.DATASHEET_ATTRIBUTE_GROUP_NAME);
            DataView AttrGroupView = CreateChartAttributeGroupsView(project, store);
            DataSheet StateAttrDataSheet = project.GetDataSheet(Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_NAME);
            DataView StateAttrDataView = new DataView(StateAttrDataSheet.GetData(store), null, StateAttrDataSheet.ValidationTable.DisplayMember, DataViewRowState.CurrentRows);
            DataSheet TransitionAttrDataSheet = project.GetDataSheet(Strings.DATASHEET_TRANSITION_ATTRIBUTE_TYPE_NAME);
            DataView TransitionAttrDataView = new DataView(TransitionAttrDataSheet.GetData(store), null, TransitionAttrDataSheet.ValidationTable.DisplayMember, DataViewRowState.CurrentRows);

            RefreshChartAgeClassValidationTable(Strings.DATASHEET_OUTPUT_STRATUM_STATE_NAME, project, store);
            RefreshChartAgeClassValidationTable(Strings.DATASHEET_OUTPUT_STRATUM_TRANSITION_NAME, project, store);
            RefreshChartAgeClassValidationTable(Strings.DATASHEET_OUTPUT_STATE_ATTRIBUTE_NAME, project, store);
            RefreshChartAgeClassValidationTable(Strings.DATASHEET_OUTPUT_TRANSITION_ATTRIBUTE_NAME, project, store);
            RefreshChartTSTClassValidationTable(Strings.DATASHEET_OUTPUT_TST_NAME, project, store);

            AddChartStateClassVariables(StateClassGroup.Items, project);
            AddChartTransitionVariables(TransitionGroup.Items, project);
            AddChartTSTVariables(TSTGroup.Items);
            AddChartExternalVariables(store, ExternalVariableGroup.Items, project);

            AddChartAttributeVariables(
                StateAttributeGroup.Items, AttrGroupView, 
                AttrGroupDataSheet, StateAttrDataView, StateAttrDataSheet, 
                Strings.DATASHEET_OUTPUT_STATE_ATTRIBUTE_NAME, 
                Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_ID_COLUMN_NAME, 
                false,
                Strings.STATE_ATTRIBUTE_VARIABLE_NAME,
                Strings.STATE_ATTRIBUTE_DENSITY_VARIABLE_NAME);

            AddChartAttributeVariables(
                TransitionAttributeGroup.Items, AttrGroupView, 
                AttrGroupDataSheet, TransitionAttrDataView, TransitionAttrDataSheet, 
                Strings.DATASHEET_OUTPUT_TRANSITION_ATTRIBUTE_NAME, 
                Strings.DATASHEET_TRANSITION_ATTRIBUTE_TYPE_ID_COLUMN_NAME, 
                true,
                Strings.TRANSITION_ATTRIBUTE_VARIABLE_NAME,
                Strings.TRANSITION_ATTRIBUTE_DENSITY_VARIABLE_NAME);

            layout.Items.Add(StateClassGroup);
            layout.Items.Add(TransitionGroup);
            layout.Items.Add(TSTGroup);

            if (StateAttributeGroup.Items.Count > 0)
            {
                layout.Items.Add(StateAttributeGroup);
            }

            if (TransitionAttributeGroup.Items.Count > 0)
            {
                layout.Items.Add(TransitionAttributeGroup);
            }

            if (ExternalVariableGroup.Items.Count > 0)
            {
                layout.Items.Add(ExternalVariableGroup);
            }

            //Stock Groups
            LayoutItem StockGroupsGroup = new LayoutItem("stsim_Stocks", "Stocks", true);
            StockGroupsGroup.Properties.Add(new MetaDataProperty("dataSheet", Strings.DATASHEET_OUTPUT_STOCK_NAME));
            AddStockGroupChartVariables(store, project, StockGroupsGroup.Items);

            if (StockGroupsGroup.Items.Count > 0)
            {
                layout.Items.Add(StockGroupsGroup);
            }

            //Flow Groups
            LayoutItem FlowGroupsGroup = new LayoutItem("stsim_Flows", "Flows", true);
            FlowGroupsGroup.Properties.Add(new MetaDataProperty("dataSheet", Strings.DATASHEET_OUTPUT_FLOW_NAME));
            AddFlowGroupChartVariables(store, project, FlowGroupsGroup.Items);

            if (FlowGroupsGroup.Items.Count > 0)
            {
                layout.Items.Add(FlowGroupsGroup);
            }
        }

        public override DataTable GetData(DataStore store, ChartDescriptor descriptor, DataSheet dataSheet)
        {
            if (ChartingUtilities.HasAgeClassUpdateTag(dataSheet.Project))
            {
                WinFormSession sess = (WinFormSession) dataSheet.Session;

                sess.SetStatusMessageWithEvents("Updating age related data...");
                dataSheet.Library.Save(store);
                sess.SetStatusMessageWithEvents(string.Empty);

                Debug.Assert(!ChartingUtilities.HasAgeClassUpdateTag(dataSheet.Project));
            }

            if (ChartingUtilities.HasTSTClassUpdateTag(dataSheet.Project))
            {
                WinFormSession sess = (WinFormSession)dataSheet.Session;

                sess.SetStatusMessageWithEvents("Updating TST related data...");
                dataSheet.Library.Save(store);
                sess.SetStatusMessageWithEvents(string.Empty);

                Debug.Assert(!ChartingUtilities.HasTSTClassUpdateTag(dataSheet.Project));
            }

            if (descriptor.VariableName == Strings.STATE_CLASS_PROPORTION_VARIABLE_NAME || descriptor.VariableName == Strings.STATE_CLASS_PROPORTION_VARIABLE_NAME + "-1")
            {
                return ChartingUtilities.CreateProportionChartData(
                    dataSheet.Scenario, descriptor, Strings.DATASHEET_OUTPUT_STRATUM_STATE_NAME, store);
            }
            else if (descriptor.VariableName == Strings.TRANSITION_PROPORTION_VARIABLE_NAME || descriptor.VariableName == Strings.TRANSITION_PROPORTION_VARIABLE_NAME + "-1")
            {
                return ChartingUtilities.CreateProportionChartData(
                    dataSheet.Scenario, descriptor, Strings.DATASHEET_OUTPUT_STRATUM_TRANSITION_NAME, store);
            }
            else if (
                descriptor.VariableName.StartsWith(Strings.STATE_ATTRIBUTE_VARIABLE_NAME) || 
                descriptor.VariableName.StartsWith(Strings.STATE_ATTRIBUTE_DENSITY_VARIABLE_NAME))
            {
                string[] s = descriptor.VariableName.Split('-');

                Debug.Assert(s.Count() == 2);
                Debug.Assert(s[0] == Strings.STATE_ATTRIBUTE_VARIABLE_NAME || s[0] == Strings.STATE_ATTRIBUTE_DENSITY_VARIABLE_NAME);

                int AttrId = int.Parse(s[1], CultureInfo.InvariantCulture);
                bool IsDensity = (s[0] == Strings.STATE_ATTRIBUTE_DENSITY_VARIABLE_NAME);
                string ColumnName = Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_ID_COLUMN_NAME;

                return ChartingUtilities.CreateRawAttributeChartData(
                    dataSheet.Scenario, descriptor, dataSheet.Name, ColumnName, AttrId, IsDensity, store);
            }
            else if (
                descriptor.VariableName.StartsWith(Strings.TRANSITION_ATTRIBUTE_VARIABLE_NAME) || 
                descriptor.VariableName.StartsWith(Strings.TRANSITION_ATTRIBUTE_DENSITY_VARIABLE_NAME))
            {
                string[] s = descriptor.VariableName.Split('-');

                Debug.Assert(s.Count() == 2);
                Debug.Assert(s[0] == Strings.TRANSITION_ATTRIBUTE_VARIABLE_NAME || s[0] == Strings.TRANSITION_ATTRIBUTE_DENSITY_VARIABLE_NAME);

                int AttrId = int.Parse(s[1], CultureInfo.InvariantCulture);
                bool IsDensity = (s[0] == Strings.TRANSITION_ATTRIBUTE_DENSITY_VARIABLE_NAME);
                string ColumnName = Strings.DATASHEET_TRANSITION_ATTRIBUTE_TYPE_ID_COLUMN_NAME;

                return ChartingUtilities.CreateRawAttributeChartData(
                    dataSheet.Scenario, descriptor, dataSheet.Name, ColumnName, AttrId, IsDensity, store);
            }
            else if (descriptor.DatasheetName == Strings.OUTPUT_EXTERNAL_VARIABLE_VALUE_DATASHEET_NAME)
            {
                return ChartingUtilities.CreateRawExternalVariableData(
                    dataSheet.Scenario, descriptor, store);
            }
            else if (
                descriptor.DatasheetName == Strings.DATASHEET_OUTPUT_STOCK_NAME ||
                descriptor.DatasheetName == Strings.DATASHEET_OUTPUT_FLOW_NAME)
            {
                string[] v = descriptor.VariableName.Split('-');
                string VarName = v[0];

                if (
                    VarName == Strings.STOCK_GROUP_VAR_NAME ||
                    VarName == Strings.STOCK_GROUP_DENSITY_VAR_NAME ||
                    VarName == Strings.FLOW_GROUP_VAR_NAME ||
                    VarName == Strings.FLOW_GROUP_DENSITY_VAR_NAME)
                {
                    return ChartingUtilities.CreateRawStockFlowChartData(dataSheet, descriptor, store, VarName);
                }
            }

            return null;
        }

        public override ValidationTable GetCustomValidationTable(DataStore store, string dataSheetName, string columnName, Project project)
        {
            if (dataSheetName == Strings.DATASHEET_OUTPUT_STRATUM_STATE_NAME ||
                dataSheetName == Strings.DATASHEET_OUTPUT_STRATUM_TRANSITION_NAME ||
                dataSheetName == Strings.DATASHEET_OUTPUT_STATE_ATTRIBUTE_NAME ||
                dataSheetName == Strings.DATASHEET_OUTPUT_TRANSITION_ATTRIBUTE_NAME)
            {
                if (columnName == Strings.DATASHEET_AGE_CLASS_COLUMN_NAME)
                {
                    return CreateClassBinValidationTable(
                        project,
                        Strings.DATASHEET_AGE_TYPE_NAME,
                        Strings.DATASHEET_AGE_TYPE_FREQUENCY_COLUMN_NAME,
                        Strings.DATASHEET_AGE_TYPE_MAXIMUM_COLUMN_NAME,
                        Strings.DATASHEET_AGE_GROUP_NAME,
                        Strings.DATASHEET_AGE_GROUP_MAXIMUM_COLUMN_NAME,
                        Strings.AGE_CLASS_VALIDATION_TABLE_NAME, 
                        store);
                }
            }

            if (dataSheetName == Strings.DATASHEET_OUTPUT_TST_NAME)
            {
                return CreateClassBinValidationTable(
                    project,
                    Strings.DATASHEET_TST_TYPE_NAME,
                    Strings.DATASHEET_TST_TYPE_FREQUENCY_COLUMN_NAME,
                    Strings.DATASHEET_TST_TYPE_MAXIMUM_COLUMN_NAME,
                    Strings.DATASHEET_TST_GROUP_NAME,
                    Strings.DATASHEET_TST_GROUP_MAXIMUM_COLUMN_NAME,
                    Strings.TST_CLASS_VALIDATION_TABLE_NAME, 
                    store);
            }

            return null;
        }

        public override string GetCacheTag(ChartDescriptor descriptor)
        {
            if (ChartingUtilities.DescriptorHasAgeReference(descriptor))
            {
                return Constants.AGE_QUERY_CACHE_TAG;
            }
            else if (ChartingUtilities.DescriptorHasTSTReference(descriptor))
            {
                return Constants.TST_QUERY_CACHE_TAG;
            }
            else
            {
                return null;
            }
        }

        private static void AddChartStateClassVariables(LayoutItemCollection items, Project project)
        {
            string AmountLabel = null;
            string UnitsLabel;
            TerminologyUnit TermUnit = 0;
            DataSheet dsterm = project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME);

            TerminologyUtilities.GetAmountLabelTerminology(dsterm, ref AmountLabel, ref TermUnit);
            UnitsLabel = TerminologyUtilities.TerminologyUnitToString(TermUnit);

            string disp = string.Format(CultureInfo.InvariantCulture, "{0} ({1})", AmountLabel, UnitsLabel);

            LayoutItem Normal = new LayoutItem(Strings.STATE_CLASS_VARIABLE_NAME, disp, false);
            LayoutItem Proportion = new LayoutItem(Strings.STATE_CLASS_PROPORTION_VARIABLE_NAME, "Proportion", false);

            Normal.Properties.Add(new MetaDataProperty("dataSheet", "stsim_OutputStratumState"));
            Proportion.Properties.Add(new MetaDataProperty("dataSheet", "stsim_OutputStratumState"));

            Normal.Properties.Add(new MetaDataProperty("filter", "StratumId|SecondaryStratumId|TertiaryStratumId|StateClassId|StateLabelXId|StateLabelYId|AgeClass"));
            Proportion.Properties.Add(new MetaDataProperty("filter", "StratumId|SecondaryStratumId|TertiaryStratumId|StateClassId|StateLabelXId|StateLabelYId|AgeClass"));

            Normal.Properties.Add(new MetaDataProperty("column", "Amount"));
            Proportion.Properties.Add(new MetaDataProperty("column", "Amount"));

            Normal.Properties.Add(new MetaDataProperty("defaultValue", "0.0"));
            Proportion.Properties.Add(new MetaDataProperty("defaultValue", "0.0"));

            Normal.Properties.Add(new MetaDataProperty("subsetFilter", "ResolutionId=0"));
            Proportion.Properties.Add(new MetaDataProperty("subsetFilter", "ResolutionId=0"));

            LayoutItem NormalFineRes = new LayoutItem(Strings.STATE_CLASS_VARIABLE_NAME + "-1", disp + " (Fine Resolution)", false);
            LayoutItem ProportionFineRes = new LayoutItem(Strings.STATE_CLASS_PROPORTION_VARIABLE_NAME + "-1", "Proportion (Fine Resolution)", false);

            NormalFineRes.Properties.Add(new MetaDataProperty("dataSheet", "stsim_OutputStratumState"));
            ProportionFineRes.Properties.Add(new MetaDataProperty("dataSheet", "stsim_OutputStratumState"));

            NormalFineRes.Properties.Add(new MetaDataProperty("filter", "StratumId|SecondaryStratumId|TertiaryStratumId|StateClassId|StateLabelXId|StateLabelYId|AgeClass"));
            ProportionFineRes.Properties.Add(new MetaDataProperty("filter", "StratumId|SecondaryStratumId|TertiaryStratumId|StateClassId|StateLabelXId|StateLabelYId|AgeClass"));

            NormalFineRes.Properties.Add(new MetaDataProperty("column", "Amount"));
            ProportionFineRes.Properties.Add(new MetaDataProperty("column", "Amount"));

            NormalFineRes.Properties.Add(new MetaDataProperty("defaultValue", "0.0"));
            ProportionFineRes.Properties.Add(new MetaDataProperty("defaultValue", "0.0"));

            NormalFineRes.Properties.Add(new MetaDataProperty("subsetFilter", "ResolutionId=1"));
            ProportionFineRes.Properties.Add(new MetaDataProperty("subsetFilter", "ResolutionId=1"));

            items.Add(Normal);
            items.Add(NormalFineRes);
            items.Add(Proportion);
            items.Add(ProportionFineRes);
        }

        private static void AddChartTransitionVariables(LayoutItemCollection items, Project project)
        {
            string AmountLabel = null;
            string UnitsLabel;
            TerminologyUnit TermUnit = 0;
            DataSheet dsterm = project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME);

            TerminologyUtilities.GetAmountLabelTerminology(dsterm, ref AmountLabel, ref TermUnit);
            UnitsLabel = TerminologyUtilities.TerminologyUnitToString(TermUnit);

            string disp = string.Format(CultureInfo.InvariantCulture, "{0} ({1})", AmountLabel, UnitsLabel);
            LayoutItem Normal = new LayoutItem(Strings.TRANSITION_VARIABLE_NAME, disp, false);
            LayoutItem Proportion = new LayoutItem(Strings.TRANSITION_PROPORTION_VARIABLE_NAME, "Proportion", false);

            Normal.Properties.Add(new MetaDataProperty("dataSheet", "stsim_OutputStratumTransition"));
            Proportion.Properties.Add(new MetaDataProperty("dataSheet", "stsim_OutputStratumTransition"));

            Normal.Properties.Add(new MetaDataProperty("filter", "StratumId|SecondaryStratumId|TertiaryStratumId|TransitionGroupId|AgeClass|SizeClassId"));
            Proportion.Properties.Add(new MetaDataProperty("filter", "StratumId|SecondaryStratumId|TertiaryStratumId|TransitionGroupId|AgeClass|SizeClassId"));

            Normal.Properties.Add(new MetaDataProperty("column", "Amount"));
            Proportion.Properties.Add(new MetaDataProperty("column", "Amount"));

            Normal.Properties.Add(new MetaDataProperty("skipTimestepZero", "True"));
            Proportion.Properties.Add(new MetaDataProperty("skipTimestepZero", "True"));

            Normal.Properties.Add(new MetaDataProperty("defaultValue", "0.0"));
            Proportion.Properties.Add(new MetaDataProperty("defaultValue", "0.0"));

            Normal.Properties.Add(new MetaDataProperty("subsetFilter", "ResolutionId=0"));
            Proportion.Properties.Add(new MetaDataProperty("subsetFilter", "ResolutionId=0"));

            LayoutItem NormalFineRes = new LayoutItem(Strings.TRANSITION_VARIABLE_NAME + "-1", disp + " (Fine Resolution)", false);
            LayoutItem ProportionFineRes = new LayoutItem(Strings.TRANSITION_PROPORTION_VARIABLE_NAME + "-1", "Proportion (Fine Resolution)", false);

            NormalFineRes.Properties.Add(new MetaDataProperty("dataSheet", "stsim_OutputStratumTransition"));
            ProportionFineRes.Properties.Add(new MetaDataProperty("dataSheet", "stsim_OutputStratumTransition"));

            NormalFineRes.Properties.Add(new MetaDataProperty("filter", "StratumId|SecondaryStratumId|TertiaryStratumId|TransitionGroupId|AgeClass|SizeClassId"));
            ProportionFineRes.Properties.Add(new MetaDataProperty("filter", "StratumId|SecondaryStratumId|TertiaryStratumId|TransitionGroupId|AgeClass|SizeClassId"));

            NormalFineRes.Properties.Add(new MetaDataProperty("column", "Amount"));
            ProportionFineRes.Properties.Add(new MetaDataProperty("column", "Amount"));

            NormalFineRes.Properties.Add(new MetaDataProperty("skipTimestepZero", "True"));
            ProportionFineRes.Properties.Add(new MetaDataProperty("skipTimestepZero", "True"));

            NormalFineRes.Properties.Add(new MetaDataProperty("defaultValue", "0.0"));
            ProportionFineRes.Properties.Add(new MetaDataProperty("defaultValue", "0.0"));

            NormalFineRes.Properties.Add(new MetaDataProperty("subsetFilter", "ResolutionId=1"));
            ProportionFineRes.Properties.Add(new MetaDataProperty("subsetFilter", "ResolutionId=1"));

            items.Add(Normal);
            items.Add(NormalFineRes);
            items.Add(Proportion);
            items.Add(ProportionFineRes);
        }

        private static void AddChartTSTVariables(LayoutItemCollection items)
        {
            LayoutItem Normal = new LayoutItem(Strings.TST_VARIABLE_NAME, "Amount", false);

            Normal.Properties.Add(new MetaDataProperty("dataSheet", "stsim_OutputTST"));
            Normal.Properties.Add(new MetaDataProperty("filter", "StratumId|SecondaryStratumId|TertiaryStratumId|TransitionGroupId|TSTClass"));
            Normal.Properties.Add(new MetaDataProperty("column", "Amount"));
            Normal.Properties.Add(new MetaDataProperty("defaultValue", "0.0"));
            Normal.Properties.Add(new MetaDataProperty("subsetFilter", "ResolutionId=0"));

            LayoutItem NormalFineRes = new LayoutItem(Strings.TST_VARIABLE_NAME + "-1", "Amount (Fine Resolution)", false);

            NormalFineRes.Properties.Add(new MetaDataProperty("dataSheet", "stsim_OutputTST"));
            NormalFineRes.Properties.Add(new MetaDataProperty("filter", "StratumId|SecondaryStratumId|TertiaryStratumId|TransitionGroupId|TSTClass"));
            NormalFineRes.Properties.Add(new MetaDataProperty("column", "Amount"));
            NormalFineRes.Properties.Add(new MetaDataProperty("defaultValue", "0.0"));
            NormalFineRes.Properties.Add(new MetaDataProperty("subsetFilter", "ResolutionId=1"));

            items.Add(Normal);
            items.Add(NormalFineRes);
        }

        private static void AddChartExternalVariables(DataStore store, LayoutItemCollection items, Project project)
        {
            DataSheet ds = project.GetDataSheet(Strings.CORE_EXTERNAL_VAR_TYPE_DATASHEET_NAME);

            foreach (DataRow dr in ds.GetData(store).Rows)
            {
                int Id = Convert.ToInt32(dr[ds.ValueMember], CultureInfo.InvariantCulture);
                string Name = Convert.ToString(dr[ds.DisplayMember], CultureInfo.InvariantCulture);
                string VarName = string.Format(CultureInfo.InvariantCulture, "stsim_ExternalVariable-{0}", Id);
                LayoutItem Item = new LayoutItem(VarName, Name, false);

                Item.Properties.Add(new MetaDataProperty("dataSheet", Strings.OUTPUT_EXTERNAL_VARIABLE_VALUE_DATASHEET_NAME));
                Item.Properties.Add(new MetaDataProperty("column", Strings.OUTPUT_EXTERNAL_VARIABLE_VALUE_VALUE_COLUMN_NAME));
                Item.Properties.Add(new MetaDataProperty("defaultValue", "0.0"));

                items.Add(Item);
            }         
        }

        private static void AddChartAttributeVariables(
            LayoutItemCollection items, 
            DataView attrGroupView, 
            DataSheet attrGroupDataSheet, 
            DataView attrView, 
            DataSheet attrDataSheet, 
            string outputTableName, 
            string attributeTypeColumnName, 
            bool skipTimestepZero,
            string normalAttributePrefix,
            string densityAttributePrefix)
        {
            Debug.Assert(Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME == Strings.DATASHEET_TRANSITION_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME);
            LayoutItem NonGroupedDensityGroup = new LayoutItem(Strings.DENSITY_GROUP_NAME + "STSIM_NON_GROUPED", "Density", true);

            AddChartNonGroupedAttributes(
                items, attrView, attrDataSheet, outputTableName, attributeTypeColumnName, 
                skipTimestepZero, NonGroupedDensityGroup, normalAttributePrefix, densityAttributePrefix);

            if (NonGroupedDensityGroup.Items.Count > 0)
            {
                items.Add(NonGroupedDensityGroup);
            }

            Dictionary<string, LayoutItem> GroupsDict = new Dictionary<string, LayoutItem>();
            List<LayoutItem> GroupsList = new List<LayoutItem>();

            foreach (DataRowView drv in attrGroupView)
            {
                string GroupName = Convert.ToString(drv.Row[Strings.DATASHEET_NAME_COLUMN_NAME], CultureInfo.InvariantCulture);
                string DensityGroupName = Strings.DENSITY_GROUP_NAME + GroupName;
                LayoutItem Group = new LayoutItem(GroupName, GroupName, true);
                LayoutItem DensityGroup = new LayoutItem(DensityGroupName, "Density", true);

                GroupsDict.Add(GroupName, Group);
                GroupsList.Add(Group);

                GroupsDict.Add(DensityGroupName, DensityGroup);
            }

            AddChartGroupedAttributes(
                GroupsDict, attrGroupDataSheet, attrView, attrDataSheet, 
                outputTableName, attributeTypeColumnName, skipTimestepZero, 
                normalAttributePrefix, densityAttributePrefix);

            foreach (LayoutItem g in GroupsList)
            {
                if (g.Items.Count > 0)
                {
                    string DensityGroupName = Strings.DENSITY_GROUP_NAME + g.Name;

                    g.Items.Add(GroupsDict[DensityGroupName]);
                    items.Add(g);
                }
            }
        }

        private static void AddChartNonGroupedAttributes(
            LayoutItemCollection items,
            DataView attrsView, 
            DataSheet attrsDataSheet, 
            string outputDataSheetName, 
            string outputColumnName, 
            bool skipTimestepZero, 
            LayoutItem densityGroup, 
            string normalAttributePrefix, 
            string densityAttributePrefix)
        {
            foreach (DataRowView drv in attrsView)
            {
                if (drv.Row[Strings.DATASHEET_ATTRIBUTE_GROUP_ID_COLUMN_NAME] == DBNull.Value)
                {
                    int AttrId = Convert.ToInt32(drv.Row[attrsDataSheet.ValueMember], CultureInfo.InvariantCulture);
                    string Units = DataTableUtilities.GetDataStr(drv.Row, Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME);

                    //Normal Attribute
                    //----------------

                    string AttrNameNormal = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", normalAttributePrefix, AttrId);
                    string DisplayNameNormal = Convert.ToString(drv.Row[attrsDataSheet.ValidationTable.DisplayMember], CultureInfo.InvariantCulture);

                    if (Units != null)
                    {
                        DisplayNameNormal = string.Format(CultureInfo.InvariantCulture, "{0} ({1})", DisplayNameNormal, Units);
                    }

                    LayoutItem ItemNormal = new LayoutItem(AttrNameNormal, DisplayNameNormal, false);

                    ItemNormal.Properties.Add(new MetaDataProperty("dataSheet", outputDataSheetName));
                    ItemNormal.Properties.Add(new MetaDataProperty("filter", "StratumId|SecondaryStratumId|TertiaryStratumId|AgeClass"));
                    ItemNormal.Properties.Add(new MetaDataProperty("column", outputColumnName));
                    ItemNormal.Properties.Add(new MetaDataProperty("prefixFolderName", "False"));
                    ItemNormal.Properties.Add(new MetaDataProperty("customTitle", DisplayNameNormal));
                    ItemNormal.Properties.Add(new MetaDataProperty("defaultValue", "0.0"));

                    if (skipTimestepZero)
                    {
                        ItemNormal.Properties.Add(new MetaDataProperty("skipTimestepZero", "True"));
                    }

                    items.Add(ItemNormal);

                    //Density Attribute
                    //-----------------

                    string AttrNameDensity = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", densityAttributePrefix, AttrId);
                    string DisplayNameDensity = Convert.ToString(drv.Row[attrsDataSheet.ValidationTable.DisplayMember], CultureInfo.InvariantCulture);

                    if (Units != null)
                    {
                        DisplayNameDensity = string.Format(CultureInfo.InvariantCulture, "{0} ({1})", DisplayNameDensity, Units);
                    }

                    LayoutItem ItemDensity = new LayoutItem(AttrNameDensity, DisplayNameDensity, false);

                    ItemDensity.Properties.Add(new MetaDataProperty("dataSheet", outputDataSheetName));
                    ItemDensity.Properties.Add(new MetaDataProperty("filter", "StratumId|SecondaryStratumId|TertiaryStratumId|AgeClass"));
                    ItemDensity.Properties.Add(new MetaDataProperty("column", outputColumnName));
                    ItemDensity.Properties.Add(new MetaDataProperty("prefixFolderName", "False"));
                    ItemDensity.Properties.Add(new MetaDataProperty("customTitle", "(Density): " + DisplayNameNormal));
                    ItemDensity.Properties.Add(new MetaDataProperty("defaultValue", "0.0"));

                    if (skipTimestepZero)
                    {
                        ItemDensity.Properties.Add(new MetaDataProperty("skipTimestepZero", "True"));
                    }

                    densityGroup.Items.Add(ItemDensity);
                }
            }
        }

        private static void AddChartGroupedAttributes(
            Dictionary<string, LayoutItem> groupsDict, 
            DataSheet groupsDataSheet, 
            DataView attrsView, 
            DataSheet attrsDataSheet, 
            string outputDataSheetName, 
            string outputColumnName, 
            bool skipTimestepZero,
            string normalAttributePrefix,
            string densityAttributePrefix)
        {
            //The density groups have already been created and added to the groups.  Howver, we want the
            //attributes themselves to appear before this group so we must insert them in reverse order.

            for (int i = attrsView.Count - 1; i >= 0; i--)
            {
                DataRowView drv = attrsView[i];

                if (drv.Row[Strings.DATASHEET_ATTRIBUTE_GROUP_ID_COLUMN_NAME] != DBNull.Value)
                {
                    int GroupId = Convert.ToInt32(drv.Row[Strings.DATASHEET_ATTRIBUTE_GROUP_ID_COLUMN_NAME], CultureInfo.InvariantCulture);
                    string GroupName = groupsDataSheet.ValidationTable.GetDisplayName(GroupId);
                    int AttrId = Convert.ToInt32(drv.Row[attrsDataSheet.ValueMember], CultureInfo.InvariantCulture);
                    LayoutItem MainGroup = groupsDict[GroupName];
                    LayoutItem DensityGroup = groupsDict[Strings.DENSITY_GROUP_NAME + GroupName];
                    string Units = DataTableUtilities.GetDataStr(drv.Row, Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME);

                    //Normal Attribute
                    //----------------

                    string AttrNameNormal = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", normalAttributePrefix, AttrId);
                    string DisplayNameNormal = Convert.ToString(drv.Row[attrsDataSheet.ValidationTable.DisplayMember], CultureInfo.InvariantCulture);

                    if (Units != null)
                    {
                        DisplayNameNormal = string.Format(CultureInfo.InvariantCulture, "{0} ({1})", DisplayNameNormal, Units);
                    }

                    LayoutItem ItemNormal = new LayoutItem(AttrNameNormal, DisplayNameNormal, false);

                    ItemNormal.Properties.Add(new MetaDataProperty("dataSheet", outputDataSheetName));
                    ItemNormal.Properties.Add(new MetaDataProperty("filter", "StratumId|SecondaryStratumId|TertiaryStratumId|AgeClass"));
                    ItemNormal.Properties.Add(new MetaDataProperty("column", outputColumnName));
                    ItemNormal.Properties.Add(new MetaDataProperty("prefixFolderName", "False"));
                    ItemNormal.Properties.Add(new MetaDataProperty("customTitle", GroupName + ": " + DisplayNameNormal));
                    ItemNormal.Properties.Add(new MetaDataProperty("defaultValue", "0.0"));

                    if (skipTimestepZero)
                    {
                        ItemNormal.Properties.Add(new MetaDataProperty("skipTimestepZero", "True"));
                    }

                    MainGroup.Items.Insert(0, ItemNormal);

                    //Density Attribute
                    //-----------------

                    string AttrNameDensity = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", densityAttributePrefix, AttrId);
                    string DisplayNameDensity = Convert.ToString(drv.Row[attrsDataSheet.ValidationTable.DisplayMember], CultureInfo.InvariantCulture);

                    if (Units != null)
                    {
                        DisplayNameDensity = string.Format(CultureInfo.InvariantCulture, "{0} ({1})", DisplayNameDensity, Units);
                    }

                    LayoutItem ItemDensity = new LayoutItem(AttrNameDensity, DisplayNameDensity, false);

                    ItemDensity.Properties.Add(new MetaDataProperty("dataSheet", outputDataSheetName));
                    ItemDensity.Properties.Add(new MetaDataProperty("filter", "StratumId|SecondaryStratumId|TertiaryStratumId|AgeClass"));
                    ItemDensity.Properties.Add(new MetaDataProperty("column", outputColumnName));
                    ItemDensity.Properties.Add(new MetaDataProperty("prefixFolderName", "False"));
                    ItemDensity.Properties.Add(new MetaDataProperty("customTitle", GroupName + " (Density): " + DisplayNameNormal));
                    ItemDensity.Properties.Add(new MetaDataProperty("defaultValue", "0.0"));

                    if (skipTimestepZero)
                    {
                        ItemDensity.Properties.Add(new MetaDataProperty("skipTimestepZero", "True"));
                    }

                    DensityGroup.Items.Insert(0, ItemDensity);
                }
            }
        }

        private static void AddStockGroupChartVariables(DataStore store, Project project, LayoutItemCollection items)
        {
            DataSheet ds = project.GetDataSheet(Strings.DATASHEET_STOCK_GROUP_NAME);

            if (ds.HasData(store))
            {
                //Normal
                LayoutItem ItemNormal = new LayoutItem(Strings.STOCK_GROUP_VAR_NAME, "Total", false);

                ItemNormal.Properties.Add(new MetaDataProperty("dataSheet", Strings.DATASHEET_OUTPUT_STOCK_NAME));
                ItemNormal.Properties.Add(new MetaDataProperty("filter", "StratumId|SecondaryStratumId|TertiaryStratumId|StateClassId|StockGroupId"));
                ItemNormal.Properties.Add(new MetaDataProperty("column", "Amount"));
                ItemNormal.Properties.Add(new MetaDataProperty("defaultValue", "0.0"));

                items.Add(ItemNormal);

                //Density
                LayoutItem ItemDensity = new LayoutItem(Strings.STOCK_GROUP_DENSITY_VAR_NAME, "Density", false);

                ItemDensity.Properties.Add(new MetaDataProperty("dataSheet", Strings.DATASHEET_OUTPUT_STOCK_NAME));
                ItemDensity.Properties.Add(new MetaDataProperty("filter", "StratumId|SecondaryStratumId|TertiaryStratumId|StateClassId|StockGroupId"));
                ItemDensity.Properties.Add(new MetaDataProperty("column", "Amount"));
                ItemDensity.Properties.Add(new MetaDataProperty("defaultValue", "0.0"));

                items.Add(ItemDensity);
            }
        }

        private static void AddFlowGroupChartVariables(DataStore store, Project project, LayoutItemCollection items)
        {
            DataSheet ds = project.GetDataSheet(Strings.DATASHEET_FLOW_GROUP_NAME);

            if (ds.HasData(store))
            {
                LayoutItem ItemNormal = new LayoutItem(Strings.FLOW_GROUP_VAR_NAME, "Total", false);

                ItemNormal.Properties.Add(new MetaDataProperty("dataSheet", Strings.DATASHEET_OUTPUT_FLOW_NAME));
                ItemNormal.Properties.Add(new MetaDataProperty("filter", "FromStratumId|FromSecondaryStratumId|FromTertiaryStratumId|FromStateClassId|FromStockTypeId|TransitionTypeId|ToStratumId|ToStateClassId|ToStockTypeId|FlowGroupId|EndStratumId|EndSecondaryStratumId|EndTertiaryStratumId|EndStateClassId"));
                ItemNormal.Properties.Add(new MetaDataProperty("column", "Amount"));
                ItemNormal.Properties.Add(new MetaDataProperty("skipTimestepZero", "True"));
                ItemNormal.Properties.Add(new MetaDataProperty("defaultValue", "0.0"));

                items.Add(ItemNormal);

                //Density
                LayoutItem ItemDensity = new LayoutItem(Strings.FLOW_GROUP_DENSITY_VAR_NAME, "Density", false);

                ItemDensity.Properties.Add(new MetaDataProperty("dataSheet", Strings.DATASHEET_OUTPUT_FLOW_NAME));
                ItemDensity.Properties.Add(new MetaDataProperty("filter", "FromStratumId|FromSecondaryStratumId|FromTertiaryStratumId|FromStateClassId|FromStockTypeId|TransitionTypeId|ToStratumId|ToStateClassId|ToStockTypeId|FlowGroupId|EndStratumId|EndSecondaryStratumId|EndTertiaryStratumId|EndStateClassId"));
                ItemDensity.Properties.Add(new MetaDataProperty("column", "Amount"));
                ItemDensity.Properties.Add(new MetaDataProperty("skipTimestepZero", "True"));
                ItemDensity.Properties.Add(new MetaDataProperty("defaultValue", "0.0"));

                items.Add(ItemDensity);
            }
        }

        private static DataView CreateChartAttributeGroupsView(Project project, DataStore store)
        {
            DataSheet ds = project.GetDataSheet(Strings.DATASHEET_ATTRIBUTE_GROUP_NAME);
            DataView View = new DataView(ds.GetData(store), null, ds.ValidationTable.DisplayMember, DataViewRowState.CurrentRows);

            return View;
        }

        private static void RefreshChartAgeClassValidationTable(string dataSheetName, Project project, DataStore store)
        {
            foreach (Scenario s in project.Results)
            {
                DataSheet ds = s.GetDataSheet(dataSheetName);
                DataSheetColumn col = ds.Columns[Strings.DATASHEET_AGE_CLASS_COLUMN_NAME];

                col.ValidationTable = CreateClassBinValidationTable(
                    project,
                    Strings.DATASHEET_AGE_TYPE_NAME,
                    Strings.DATASHEET_AGE_TYPE_FREQUENCY_COLUMN_NAME,
                    Strings.DATASHEET_AGE_TYPE_MAXIMUM_COLUMN_NAME,
                    Strings.DATASHEET_AGE_GROUP_NAME,
                    Strings.DATASHEET_AGE_GROUP_MAXIMUM_COLUMN_NAME,
                    Strings.AGE_CLASS_VALIDATION_TABLE_NAME,
                    store);
            }
        }

        private static void RefreshChartTSTClassValidationTable(string dataSheetName, Project project, DataStore store)
        {
            foreach (Scenario s in project.Results)
            {
                DataSheet ds = s.GetDataSheet(dataSheetName);
                DataSheetColumn col = ds.Columns[Strings.DATASHEET_TST_CLASS_COLUMN_NAME];

                col.ValidationTable = CreateClassBinValidationTable(
                    project,
                    Strings.DATASHEET_TST_TYPE_NAME,
                    Strings.DATASHEET_TST_TYPE_FREQUENCY_COLUMN_NAME,
                    Strings.DATASHEET_TST_TYPE_MAXIMUM_COLUMN_NAME,
                    Strings.DATASHEET_TST_GROUP_NAME,
                    Strings.DATASHEET_TST_GROUP_MAXIMUM_COLUMN_NAME,
                    Strings.TST_CLASS_VALIDATION_TABLE_NAME, 
                    store);
            }
        }

        private static ValidationTable CreateClassBinValidationTable(
            Project project,
            string classTypeDatasheetName,
            string classTypeFrequencyColumnName,
            string classTypeMaximumColumnName,
            string classGroupDatasheetName,
            string classGroupMaximumColumnName, 
            string validationTableName, 
            DataStore store)
        {
            DataTable dt = new DataTable(validationTableName);
            dt.Locale = CultureInfo.InvariantCulture;

            dt.Columns.Add(new DataColumn(Strings.VALUE_MEMBER_COLUMN_NAME, typeof(long)));
            dt.Columns.Add(new DataColumn(Strings.DISPLAY_MEMBER_COLUMN_NAME, typeof(string)));

            List<ClassBinDescriptor> e = ChartingUtilities.GetClassBinGroupDescriptors(
                project, 
                classGroupDatasheetName, 
                classGroupMaximumColumnName, 
                store);

            if (e == null)
            {
                e = ChartingUtilities.GetClassBinTypeDescriptors(
                    project,
                    classTypeDatasheetName,
                    classTypeFrequencyColumnName,
                    classTypeMaximumColumnName, 
                    store);
            }

            if (e != null)
            {
                foreach (ClassBinDescriptor d in e)
                {
                    long Value = Convert.ToInt64(d.Minimum);
                    string Display;

                    if (d.Maximum.HasValue)
                    {
                        if (d.Maximum.Value == d.Minimum)
                        {
                            Display = string.Format(CultureInfo.InvariantCulture, "{0}", d.Minimum);
                        }
                        else
                        {
                            Display = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", d.Minimum, d.Maximum.Value);
                        }
                    }
                    else
                    {
                        Display = string.Format(CultureInfo.InvariantCulture, "{0}+", d.Minimum);
                    }

                    dt.Rows.Add(new object[] { Value, Display });
                }

                dt.Rows.Add(new object[] { Constants.INCLUDE_DATA_NULL_ID, "(unclassified)" });
            }

            return new ValidationTable(
                dt, 
                Strings.VALUE_MEMBER_COLUMN_NAME, 
                Strings.DISPLAY_MEMBER_COLUMN_NAME, 
                SortOrder.None);
        }

        private static bool DatasheetHasRows(Scenario s, string datasheetName, DataStore store)
        {
            DataSheet ds = s.GetDataSheet(datasheetName);
            return ds != null && ds.GetData(store).Rows.Count > 0;
        }

        private static Func<Scenario, bool> ScenarioIsMultiRes(DataStore store)
        {
            return (Scenario s) =>
            {
                return DatasheetHasRows(s, Strings.DATASHEET_TRG_NAME, store) && DatasheetHasRows(s, Strings.DATASHEET_SPICF_NAME, store);
            };
        }

        private static bool ShouldShowMultiResolutionCriteriaNodes(Project project, DataStore store)
        {
            return project?.Results?.Where(s => s.IsActive)?.Any(ScenarioIsMultiRes(store)) == true;
        }
    }
}
