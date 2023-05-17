// stsim: A SyncroSim Package for developing state-and-transition simulation models using ST-Sim.
// Copyright © 2007-2023 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Linq;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using SyncroSim.Core;
using SyncroSim.Core.Forms;
using SyncroSim.StochasticTime;

namespace SyncroSim.STSim
{
    internal class STSimChartProvider : ChartProvider
    {
        public override void RefreshCriteria(SyncroSimLayout layout, Project project)
        {
            using (DataStore store = project.Library.CreateDataStore())
            {
                SyncroSimLayoutItem StateClassGroup = new SyncroSimLayoutItem("stsim_StateClassVariableGroup", "State Classes", true);
                SyncroSimLayoutItem TransitionGroup = new SyncroSimLayoutItem("stsim_TransitionVariableGroup", "Transitions", true);
                SyncroSimLayoutItem TSTGroup = new SyncroSimLayoutItem("stsim_TSTGroup", "Time-Since-Transition", true);
                SyncroSimLayoutItem StateAttributeGroup = new SyncroSimLayoutItem("stsim_StateAttributeVariableGroup", "State Attributes", true);
                SyncroSimLayoutItem TransitionAttributeGroup = new SyncroSimLayoutItem("stsim_TransitionAttributeVariableGroup", "Transition Attributes", true);
                SyncroSimLayoutItem ExternalVariableGroup = new SyncroSimLayoutItem("stsim_ExternalVariableGroup", "External Variables", true);

                DataSheet AttrGroupDataSheet = project.GetDataSheet(Strings.DATASHEET_ATTRIBUTE_GROUP_NAME);
                DataView AttrGroupView = CreateChartAttributeGroupsView(project, store);
                DataSheet StateAttrDataSheet = project.GetDataSheet(Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_NAME);
                DataView StateAttrDataView = new DataView(StateAttrDataSheet.GetData(store), null, StateAttrDataSheet.ValidationTable.DisplayMember, DataViewRowState.CurrentRows);
                DataSheet TransitionAttrDataSheet = project.GetDataSheet(Strings.DATASHEET_TRANSITION_ATTRIBUTE_TYPE_NAME);
                DataView TransitionAttrDataView = new DataView(TransitionAttrDataSheet.GetData(store), null, TransitionAttrDataSheet.ValidationTable.DisplayMember, DataViewRowState.CurrentRows);

                RefreshChartAgeClassValidationTable(Strings.DATASHEET_OUTPUT_STRATUM_STATE_NAME, project);
                RefreshChartAgeClassValidationTable(Strings.DATASHEET_OUTPUT_STRATUM_TRANSITION_NAME, project);
                RefreshChartAgeClassValidationTable(Strings.DATASHEET_OUTPUT_STATE_ATTRIBUTE_NAME, project);
                RefreshChartAgeClassValidationTable(Strings.DATASHEET_OUTPUT_TRANSITION_ATTRIBUTE_NAME, project);
                RefreshChartTSTClassValidationTable(Strings.DATASHEET_OUTPUT_TST_NAME, project);

                AddChartStateClassVariables(StateClassGroup.Items, project);
                AddChartTransitionVariables(TransitionGroup.Items, project);
                AddChartTSTVariables(TSTGroup.Items, project);
                AddChartExternalVariables(ExternalVariableGroup.Items, project);

                AddChartAttributeVariables(
                    StateAttributeGroup.Items, AttrGroupView, 
                    AttrGroupDataSheet, StateAttrDataView, StateAttrDataSheet, 
                    Strings.DATASHEET_OUTPUT_STATE_ATTRIBUTE_NAME, 
                    Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_ID_COLUMN_NAME, 
                    false,
                    Constants.STATE_ATTRIBUTE_VARIABLE_NAME, 
                    Constants.STATE_ATTRIBUTE_DENSITY_VARIABLE_NAME);

                AddChartAttributeVariables(
                    TransitionAttributeGroup.Items, AttrGroupView, 
                    AttrGroupDataSheet, TransitionAttrDataView, TransitionAttrDataSheet, 
                    Strings.DATASHEET_OUTPUT_TRANSITION_ATTRIBUTE_NAME, 
                    Strings.DATASHEET_TRANSITION_ATTRIBUTE_TYPE_ID_COLUMN_NAME, 
                    true,
                    Constants.TRANSITION_ATTRIBUTE_VARIABLE_NAME,
                    Constants.TRANSITION_ATTRIBUTE_DENSITY_VARIABLE_NAME);

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

            if (descriptor.VariableName == Constants.STATE_CLASS_PROPORTION_VARIABLE_NAME)
            {
                return ChartingUtilities.CreateProportionChartData(
                    dataSheet.Scenario, descriptor, Strings.DATASHEET_OUTPUT_STRATUM_STATE_NAME, store);
            }
            else if (descriptor.VariableName == Constants.TRANSITION_PROPORTION_VARIABLE_NAME)
            {
                return ChartingUtilities.CreateProportionChartData(
                    dataSheet.Scenario, descriptor, Strings.DATASHEET_OUTPUT_STRATUM_TRANSITION_NAME, store);
            }
            else if (
                descriptor.VariableName.StartsWith(Constants.STATE_ATTRIBUTE_VARIABLE_NAME) || 
                descriptor.VariableName.StartsWith(Constants.STATE_ATTRIBUTE_DENSITY_VARIABLE_NAME))
            {
                string[] s = descriptor.VariableName.Split('-');

                Debug.Assert(s.Count() == 2);
                Debug.Assert(s[0] == Constants.STATE_ATTRIBUTE_VARIABLE_NAME || s[0] == Constants.STATE_ATTRIBUTE_DENSITY_VARIABLE_NAME);

                int AttrId = int.Parse(s[1], CultureInfo.InvariantCulture);
                bool IsDensity = (s[0] == Constants.STATE_ATTRIBUTE_DENSITY_VARIABLE_NAME);
                string ColumnName = Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_ID_COLUMN_NAME;

                return ChartingUtilities.CreateRawAttributeChartData(
                    dataSheet.Scenario, descriptor, dataSheet.Name, ColumnName, AttrId, IsDensity, store);
            }
            else if (
                descriptor.VariableName.StartsWith(Constants.TRANSITION_ATTRIBUTE_VARIABLE_NAME) || 
                descriptor.VariableName.StartsWith(Constants.TRANSITION_ATTRIBUTE_DENSITY_VARIABLE_NAME))
            {
                string[] s = descriptor.VariableName.Split('-');

                Debug.Assert(s.Count() == 2);
                Debug.Assert(s[0] == Constants.TRANSITION_ATTRIBUTE_VARIABLE_NAME || s[0] == Constants.TRANSITION_ATTRIBUTE_DENSITY_VARIABLE_NAME);

                int AttrId = int.Parse(s[1], CultureInfo.InvariantCulture);
                bool IsDensity = (s[0] == Constants.TRANSITION_ATTRIBUTE_DENSITY_VARIABLE_NAME);
                string ColumnName = Strings.DATASHEET_TRANSITION_ATTRIBUTE_TYPE_ID_COLUMN_NAME;

                return ChartingUtilities.CreateRawAttributeChartData(
                    dataSheet.Scenario, descriptor, dataSheet.Name, ColumnName, AttrId, IsDensity, store);
            }
            else if (descriptor.DatasheetName == Strings.OUTPUT_EXTERNAL_VARIABLE_VALUE_DATASHEET_NAME)
            {
                return ChartingUtilities.CreateRawExternalVariableData(
                    dataSheet.Scenario, descriptor, store);
            }

            return null;
        }

        public override ValidationTable GetCustomValidationTable(string dataSheetName, string columnName, Project project)
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
                        Strings.AGE_CLASS_VALIDATION_TABLE_NAME);
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
                    Strings.TST_CLASS_VALIDATION_TABLE_NAME);
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

        private static void AddChartStateClassVariables(SyncroSimLayoutItemCollection items, Project project)
        {
            string AmountLabel = null;
            string UnitsLabel;
            TerminologyUnit TermUnit = 0;
            DataSheet dsterm = project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME);

            TerminologyUtilities.GetAmountLabelTerminology(dsterm, ref AmountLabel, ref TermUnit);
            UnitsLabel = TerminologyUtilities.TerminologyUnitToString(TermUnit);

            string disp = string.Format(CultureInfo.InvariantCulture, "{0} ({1})", AmountLabel, UnitsLabel);
            SyncroSimLayoutItem Normal = new SyncroSimLayoutItem(Constants.STATE_CLASS_VARIABLE_NAME, disp, false);
            SyncroSimLayoutItem Proportion = new SyncroSimLayoutItem(Constants.STATE_CLASS_PROPORTION_VARIABLE_NAME, "Proportion", false);

            Normal.Properties.Add(new MetaDataProperty("dataSheet", "stsim_OutputStratumState"));
            Proportion.Properties.Add(new MetaDataProperty("dataSheet", "stsim_OutputStratumState"));

            Normal.Properties.Add(new MetaDataProperty("filter", "StratumID|SecondaryStratumID|TertiaryStratumID|StateClassID|StateLabelXID|StateLabelYID|AgeClass"));
            Proportion.Properties.Add(new MetaDataProperty("filter", "StratumID|SecondaryStratumID|TertiaryStratumID|StateClassID|StateLabelXID|StateLabelYID|AgeClass"));

            Normal.Properties.Add(new MetaDataProperty("column", "Amount"));
            Proportion.Properties.Add(new MetaDataProperty("column", "Amount"));

            Normal.Properties.Add(new MetaDataProperty("defaultValue", "0.0"));
            Proportion.Properties.Add(new MetaDataProperty("defaultValue", "0.0"));

            items.Add(Normal);
            items.Add(Proportion);
        }

        private static void AddChartTransitionVariables(SyncroSimLayoutItemCollection items, Project project)
        {
            string AmountLabel = null;
            string UnitsLabel = null;
            TerminologyUnit TermUnit = 0;
            DataSheet dsterm = project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME);

            TerminologyUtilities.GetAmountLabelTerminology(dsterm, ref AmountLabel, ref TermUnit);
            UnitsLabel = TerminologyUtilities.TerminologyUnitToString(TermUnit);

            string disp = string.Format(CultureInfo.InvariantCulture, "{0} ({1})", AmountLabel, UnitsLabel);
            SyncroSimLayoutItem Normal = new SyncroSimLayoutItem(Constants.TRANSITION_VARIABLE_NAME, disp, false);
            SyncroSimLayoutItem Proportion = new SyncroSimLayoutItem(Constants.TRANSITION_PROPORTION_VARIABLE_NAME, "Proportion", false);

            Normal.Properties.Add(new MetaDataProperty("dataSheet", "stsim_OutputStratumTransition"));
            Proportion.Properties.Add(new MetaDataProperty("dataSheet", "stsim_OutputStratumTransition"));

            Normal.Properties.Add(new MetaDataProperty("filter", "StratumID|SecondaryStratumID|TertiaryStratumID|TransitionGroupID|AgeClass|SizeClassID"));
            Proportion.Properties.Add(new MetaDataProperty("filter", "StratumID|SecondaryStratumID|TertiaryStratumID|TransitionGroupID|AgeClass|SizeClassID"));

            Normal.Properties.Add(new MetaDataProperty("column", "Amount"));
            Proportion.Properties.Add(new MetaDataProperty("column", "Amount"));

            Normal.Properties.Add(new MetaDataProperty("skipTimestepZero", "True"));
            Proportion.Properties.Add(new MetaDataProperty("skipTimestepZero", "True"));

            Normal.Properties.Add(new MetaDataProperty("defaultValue", "0.0"));
            Proportion.Properties.Add(new MetaDataProperty("defaultValue", "0.0"));

            items.Add(Normal);
            items.Add(Proportion);
        }

        private static void AddChartTSTVariables(SyncroSimLayoutItemCollection items, Project project)
        {
            SyncroSimLayoutItem v = new SyncroSimLayoutItem(Constants.TST_VARIABLE_NAME, "Amount", false);

            v.Properties.Add(new MetaDataProperty("dataSheet", "stsim_OutputTST"));
            v.Properties.Add(new MetaDataProperty("filter", "StratumID|SecondaryStratumID|TertiaryStratumID|TransitionGroupID|TSTClass"));
            v.Properties.Add(new MetaDataProperty("column", "Amount"));
            v.Properties.Add(new MetaDataProperty("defaultValue", "0.0"));

            items.Add(v);
        }

        private static void AddChartExternalVariables(SyncroSimLayoutItemCollection items, Project project)
        {
            DataSheet ds = project.GetDataSheet(Strings.CORESTIME_EXTERNAL_VAR_TYPE_DATASHEET_NAME);

            foreach (DataRow dr in ds.GetData().Rows)
            {
                int Id = Convert.ToInt32(dr[ds.ValueMember], CultureInfo.InvariantCulture);
                string Name = Convert.ToString(dr[ds.DisplayMember], CultureInfo.InvariantCulture);
                string VarName = string.Format(CultureInfo.InvariantCulture, "stsim_ExternalVariable-{0}", Id);
                SyncroSimLayoutItem Item = new SyncroSimLayoutItem(VarName, Name, false);

                Item.Properties.Add(new MetaDataProperty("dataSheet", Strings.OUTPUT_EXTERNAL_VARIABLE_VALUE_DATASHEET_NAME));
                Item.Properties.Add(new MetaDataProperty("column", Strings.OUTPUT_EXTERNAL_VARIABLE_VALUE_VALUE_COLUMN_NAME));
                Item.Properties.Add(new MetaDataProperty("defaultValue", "0.0"));

                items.Add(Item);
            }         
        }

        private static void AddChartAttributeVariables(
            SyncroSimLayoutItemCollection items, 
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
            SyncroSimLayoutItem NonGroupedDensityGroup = new SyncroSimLayoutItem(Constants.DENSITY_GROUP_NAME + "STSIM_NON_GROUPED", "Density", true);

            AddChartNonGroupedAttributes(
                items, attrView, attrDataSheet, outputTableName, attributeTypeColumnName, 
                skipTimestepZero, NonGroupedDensityGroup, normalAttributePrefix, densityAttributePrefix);

            if (NonGroupedDensityGroup.Items.Count > 0)
            {
                items.Add(NonGroupedDensityGroup);
            }

            Dictionary<string, SyncroSimLayoutItem> GroupsDict = new Dictionary<string, SyncroSimLayoutItem>();
            List<SyncroSimLayoutItem> GroupsList = new List<SyncroSimLayoutItem>();

            foreach (DataRowView drv in attrGroupView)
            {
                string GroupName = Convert.ToString(drv.Row[Strings.DATASHEET_NAME_COLUMN_NAME], CultureInfo.InvariantCulture);
                string DensityGroupName = Constants.DENSITY_GROUP_NAME + GroupName;
                SyncroSimLayoutItem Group = new SyncroSimLayoutItem(GroupName, GroupName, true);
                SyncroSimLayoutItem DensityGroup = new SyncroSimLayoutItem(DensityGroupName, "Density", true);

                GroupsDict.Add(GroupName, Group);
                GroupsList.Add(Group);

                GroupsDict.Add(DensityGroupName, DensityGroup);
            }

            AddChartGroupedAttributes(
                GroupsDict, attrGroupDataSheet, attrView, attrDataSheet, 
                outputTableName, attributeTypeColumnName, skipTimestepZero, 
                normalAttributePrefix, densityAttributePrefix);

            foreach (SyncroSimLayoutItem g in GroupsList)
            {
                if (g.Items.Count > 0)
                {
                    string DensityGroupName = Constants.DENSITY_GROUP_NAME + g.Name;

                    g.Items.Add(GroupsDict[DensityGroupName]);
                    items.Add(g);
                }
            }
        }

        private static void AddChartNonGroupedAttributes(
            SyncroSimLayoutItemCollection items,
            DataView attrsView, 
            DataSheet attrsDataSheet, 
            string outputDataSheetName, 
            string outputColumnName, 
            bool skipTimestepZero, 
            SyncroSimLayoutItem densityGroup, 
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

                    SyncroSimLayoutItem ItemNormal = new SyncroSimLayoutItem(AttrNameNormal, DisplayNameNormal, false);

                    ItemNormal.Properties.Add(new MetaDataProperty("dataSheet", outputDataSheetName));
                    ItemNormal.Properties.Add(new MetaDataProperty("filter", "StratumID|SecondaryStratumID|TertiaryStratumID|AgeClass"));
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

                    SyncroSimLayoutItem ItemDensity = new SyncroSimLayoutItem(AttrNameDensity, DisplayNameDensity, false);

                    ItemDensity.Properties.Add(new MetaDataProperty("dataSheet", outputDataSheetName));
                    ItemDensity.Properties.Add(new MetaDataProperty("filter", "StratumID|SecondaryStratumID|TertiaryStratumID|AgeClass"));
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
            Dictionary<string, SyncroSimLayoutItem> groupsDict, 
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
                    SyncroSimLayoutItem MainGroup = groupsDict[GroupName];
                    SyncroSimLayoutItem DensityGroup = groupsDict[Constants.DENSITY_GROUP_NAME + GroupName];
                    string Units = DataTableUtilities.GetDataStr(drv.Row, Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME);

                    //Normal Attribute
                    //----------------

                    string AttrNameNormal = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", normalAttributePrefix, AttrId);
                    string DisplayNameNormal = Convert.ToString(drv.Row[attrsDataSheet.ValidationTable.DisplayMember], CultureInfo.InvariantCulture);

                    if (Units != null)
                    {
                        DisplayNameNormal = string.Format(CultureInfo.InvariantCulture, "{0} ({1})", DisplayNameNormal, Units);
                    }

                    SyncroSimLayoutItem ItemNormal = new SyncroSimLayoutItem(AttrNameNormal, DisplayNameNormal, false);

                    ItemNormal.Properties.Add(new MetaDataProperty("dataSheet", outputDataSheetName));
                    ItemNormal.Properties.Add(new MetaDataProperty("filter", "StratumID|SecondaryStratumID|TertiaryStratumID|AgeClass"));
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

                    SyncroSimLayoutItem ItemDensity = new SyncroSimLayoutItem(AttrNameDensity, DisplayNameDensity, false);

                    ItemDensity.Properties.Add(new MetaDataProperty("dataSheet", outputDataSheetName));
                    ItemDensity.Properties.Add(new MetaDataProperty("filter", "StratumID|SecondaryStratumID|TertiaryStratumID|AgeClass"));
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

        private static DataView CreateChartAttributeGroupsView(Project project, DataStore store)
        {
            DataSheet ds = project.GetDataSheet(Strings.DATASHEET_ATTRIBUTE_GROUP_NAME);
            DataView View = new DataView(ds.GetData(store), null, ds.ValidationTable.DisplayMember, DataViewRowState.CurrentRows);

            return View;
        }

        private static void RefreshChartAgeClassValidationTable(string dataSheetName, Project project)
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
                    Strings.AGE_CLASS_VALIDATION_TABLE_NAME);
            }
        }

        private static void RefreshChartTSTClassValidationTable(string dataSheetName, Project project)
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
                    Strings.TST_CLASS_VALIDATION_TABLE_NAME);
            }
        }

        private static ValidationTable CreateClassBinValidationTable(
            Project project,
            string classTypeDatasheetName,
            string classTypeFrequencyColumnName,
            string classTypeMaximumColumnName,
            string classGroupDatasheetName,
            string classGroupMaximumColumnName, 
            string validationTableName)
        {
            DataTable dt = new DataTable(validationTableName);
            dt.Locale = CultureInfo.InvariantCulture;

            dt.Columns.Add(new DataColumn(Strings.VALUE_MEMBER_COLUMN_NAME, typeof(long)));
            dt.Columns.Add(new DataColumn(Strings.DISPLAY_MEMBER_COLUMN_NAME, typeof(string)));

            List<ClassBinDescriptor> e = ChartingUtilities.GetClassBinGroupDescriptors(
                project, 
                classGroupDatasheetName, 
                classGroupMaximumColumnName);

            if (e == null)
            {
                e = ChartingUtilities.GetClassBinTypeDescriptors(
                    project,
                    classTypeDatasheetName,
                    classTypeFrequencyColumnName,
                    classTypeMaximumColumnName);
            }

            if (e != null)
            {
                foreach (ClassBinDescriptor d in e)
                {
                    long Value = Convert.ToInt64(d.Minimum);
                    string Display = null;

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
    }
}
