// ST-Sim: A SyncroSim Module for the ST-Sim State-and-Transition Model.
// Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.

using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Globalization;
using System.Collections.Generic;
using SyncroSim.Core;
using SyncroSim.Core.Forms;
using SyncroSim.StochasticTime.Forms;

namespace SyncroSim.STSim
{
    [ObfuscationAttribute(Exclude=true, ApplyToMembers=false)]
    internal class STSimChartProvider : ChartProvider
    {
        private const string DENSITY_GROUP_NAME = "stsim_density_group";

        public override void RefreshCriteria(SyncroSimLayout layout, Project project)
        {
            using (DataStore store = project.Library.CreateDataStore())
            {
                SyncroSimLayoutItem g0 = new SyncroSimLayoutItem("StateClassGroup", "State Classes", true);
                SyncroSimLayoutItem g1 = new SyncroSimLayoutItem("TransitionGroup", "Transitions", true);
                SyncroSimLayoutItem g2 = new SyncroSimLayoutItem("StateAttributeGroup", "State Attributes", true);
                SyncroSimLayoutItem g3 = new SyncroSimLayoutItem("TransitionAttributeGroup", "Transition Attributes", true);

                DataSheet AttrGroupDataSheet = project.GetDataSheet(Strings.DATASHEET_ATTRIBUTE_GROUP_NAME);
                DataView AttrGroupView = CreateChartAttributeGroupsView(project, store);
                DataSheet StateAttrDataSheet = project.GetDataSheet(Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_NAME);
                DataView StateAttrDataView = new DataView(StateAttrDataSheet.GetData(store), null, StateAttrDataSheet.ValidationTable.DisplayMember, DataViewRowState.CurrentRows);
                DataSheet TransitionAttrDataSheet = project.GetDataSheet(Strings.DATASHEET_TRANSITION_ATTRIBUTE_TYPE_NAME);

                DataView TransitionAttrDataView = new DataView(
                    TransitionAttrDataSheet.GetData(store), 
                    null, 
                    TransitionAttrDataSheet.ValidationTable.DisplayMember, 
                    DataViewRowState.CurrentRows);

                g0.Properties.Add(new MetaDataProperty("dataSheet", "STSim_OutputStratumState"));
                g0.Properties.Add(new MetaDataProperty("filter", "StratumID|SecondaryStratumID|TertiaryStratumID|StateClassID|StateLabelXID|StateLabelYID|AgeClass"));

                g1.Properties.Add(new MetaDataProperty("dataSheet", "STSim_OutputStratumTransition"));
                g1.Properties.Add(new MetaDataProperty("filter", "StratumID|SecondaryStratumID|TertiaryStratumID|TransitionGroupID|AgeClass"));

                g2.Properties.Add(new MetaDataProperty("dataSheet", "STSim_OutputStateAttribute"));
                g2.Properties.Add(new MetaDataProperty("filter", "StratumID|SecondaryStratumID|TertiaryStratumID|AgeClass"));

                g3.Properties.Add(new MetaDataProperty("dataSheet", "STSim_OutputTransitionAttribute"));
                g3.Properties.Add(new MetaDataProperty("filter", "StratumID|SecondaryStratumID|TertiaryStratumID|AgeClass"));

                RefreshChartAgeClassValidationTable(Strings.DATASHEET_OUTPUT_STRATUM_STATE_NAME, project);
                RefreshChartAgeClassValidationTable(Strings.DATASHEET_OUTPUT_STRATUM_TRANSITION_NAME, project);
                RefreshChartAgeClassValidationTable(Strings.DATASHEET_OUTPUT_STATE_ATTRIBUTE_NAME, project);
                RefreshChartAgeClassValidationTable(Strings.DATASHEET_OUTPUT_TRANSITION_ATTRIBUTE_NAME, project);

                AddChartStateClassVariables(g0.Items, project);
                AddChartTransitionVariables(g1.Items, project);

                AddChartAttributeVariables(
                    g2.Items, AttrGroupView, AttrGroupDataSheet, StateAttrDataView, StateAttrDataSheet, 
                    Strings.DATASHEET_OUTPUT_STATE_ATTRIBUTE_NAME, Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_ID_COLUMN_NAME, false);

                AddChartAttributeVariables(g3.Items, AttrGroupView, AttrGroupDataSheet, TransitionAttrDataView, TransitionAttrDataSheet, 
                    Strings.DATASHEET_OUTPUT_TRANSITION_ATTRIBUTE_NAME, Strings.DATASHEET_TRANSITION_ATTRIBUTE_TYPE_ID_COLUMN_NAME, true);

                layout.Items.Add(g0);
                layout.Items.Add(g1);

                if (g2.Items.Count > 0)
                {
                    layout.Items.Add(g2);
                }

                if (g3.Items.Count > 0)
                {
                    layout.Items.Add(g3);
                }
            }
        }

        /// <summary>
        /// Prepares the charting data before the data is actually queried
        /// </summary>
        /// <param name="store"></param>
        /// <param name="descriptors"></param>
        /// <param name="statusEntries"></param>
        /// <param name="project"></param>
        /// <remarks>
        /// If a request is being made for age data we have to update the age class 
        /// </remarks>
        public override void GetStatus(
            DataStore store, 
            ChartDescriptorCollection descriptors, 
            StochasticTimeStatusCollection statusEntries, 
            Project project)
        {
            if (!ChartingUtilities.HasAgeReference(descriptors))
            {
                return;
            }

            List<string> Sheets = new List<string>();

            foreach (ChartDescriptor d in descriptors)
            {
                if (d.DataSheetName == Strings.DATASHEET_OUTPUT_STRATUM_STATE_NAME || 
                    d.DataSheetName == Strings.DATASHEET_OUTPUT_STRATUM_TRANSITION_NAME ||
                    d.DataSheetName == Strings.DATASHEET_OUTPUT_STATE_ATTRIBUTE_NAME || 
                    d.DataSheetName == Strings.DATASHEET_OUTPUT_TRANSITION_ATTRIBUTE_NAME)
                {
                    if (!Sheets.Contains(d.DataSheetName))
                    {
                        Sheets.Add(d.DataSheetName);
                    }
                }
            }

            foreach (Scenario s in project.Results)
            {
                if (s.IsActive)
                {
                    foreach (string n in Sheets)
                    {
                        DataSheet ds = s.GetDataSheet(n);
                        ChartingUtilities.FillAgeRelatedStatusEntries(store, ds, statusEntries);
                    }
                }
            }
        }

        public override DataTable GetData(DataStore store, ChartDescriptor descriptor, DataSheet dataSheet)
        {
            if (AgeUtilities.HasAgeClassUpdateTag(dataSheet.Project))
            {
                WinFormSession sess = (WinFormSession) dataSheet.Session;

                sess.SetStatusMessageWithEvents("Updating age related data...");
                dataSheet.Library.Save(store);
                sess.SetStatusMessageWithEvents(string.Empty);

                Debug.Assert(!AgeUtilities.HasAgeClassUpdateTag(dataSheet.Project));
            }

            if (
                descriptor.DataSheetName == Strings.DATASHEET_OUTPUT_STRATUM_STATE_NAME || 
                descriptor.DataSheetName == Strings.DATASHEET_OUTPUT_STRATUM_TRANSITION_NAME)
            {
                if (descriptor.VariableName == Strings.STATE_CLASS_PROPORTION_VARIABLE_NAME)
                {
                    return ChartingUtilities.CreateProportionChartData(
                        dataSheet.Scenario, descriptor, Strings.DATASHEET_OUTPUT_STRATUM_STATE_NAME, store);
                }
                else if (descriptor.VariableName == Strings.TRANSITION_PROPORTION_VARIABLE_NAME)
                {
                    return ChartingUtilities.CreateProportionChartData(
                        dataSheet.Scenario, descriptor, Strings.DATASHEET_OUTPUT_STRATUM_TRANSITION_NAME, store);
                }
                else
                {
                    return null;
                }
            }
            else if (
                descriptor.DataSheetName == Strings.DATASHEET_OUTPUT_STATE_ATTRIBUTE_NAME || 
                descriptor.DataSheetName == Strings.DATASHEET_OUTPUT_TRANSITION_ATTRIBUTE_NAME)
            {
                string[] s = descriptor.VariableName.Split('-');

                Debug.Assert(s.Count() == 2);
                Debug.Assert(s[0] == "attrnormal" || s[0] == "attrdensity");

                int AttrId = int.Parse(s[1], CultureInfo.InvariantCulture);
                bool IsDensity = (s[0] == "attrdensity");
                string ColumnName = null;

                if (descriptor.DataSheetName == Strings.DATASHEET_OUTPUT_STATE_ATTRIBUTE_NAME)
                {
                    ColumnName = Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_ID_COLUMN_NAME;
                }
                else
                {
                    ColumnName = Strings.DATASHEET_TRANSITION_ATTRIBUTE_TYPE_ID_COLUMN_NAME;
                }

                return ChartingUtilities.CreateRawAttributeChartData(
                    dataSheet.Scenario, descriptor, dataSheet.Name, ColumnName, AttrId, IsDensity, store);
            }

            return null;
        }

        public override string GetCacheTag(ChartDescriptor descriptor)
        {
            if (ChartingUtilities.HasAgeReference(descriptor))
            {
                return Constants.AGE_QUERY_CACHE_TAG;
            }
            else
            {
                return null;
            }
        }

        private static void AddChartStateClassVariables(SyncroSimLayoutItemCollection items, Project project)
        {
            string AmountLabel = null;
            string UnitsLabel = null;
            TerminologyUnit TermUnit = 0;
            DataSheet dsterm = project.GetDataSheet(Strings.DATASHEET_TERMINOLOGY_NAME);

            TerminologyUtilities.GetAmountLabelTerminology(dsterm, ref AmountLabel, ref TermUnit);
            UnitsLabel = TerminologyUtilities.TerminologyUnitToString(TermUnit);

            string disp = string.Format(CultureInfo.InvariantCulture, "{0} ({1})", AmountLabel, UnitsLabel);
            SyncroSimLayoutItem Normal = new SyncroSimLayoutItem(Strings.STATE_CLASS_AMOUNT_VARIABLE_NAME, disp, false);
            SyncroSimLayoutItem Proportion = new SyncroSimLayoutItem(Strings.STATE_CLASS_PROPORTION_VARIABLE_NAME, "Proportion", false);

            Normal.Properties.Add(new MetaDataProperty("dataSheet", "STSim_OutputStratumState"));
            Proportion.Properties.Add(new MetaDataProperty("dataSheet", "STSim_OutputStratumState"));

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
            SyncroSimLayoutItem Normal = new SyncroSimLayoutItem(Strings.TRANSITION_AMOUNT_VARIABLE_NAME, disp, false);
            SyncroSimLayoutItem Proportion = new SyncroSimLayoutItem(Strings.TRANSITION_PROPORTION_VARIABLE_NAME, "Proportion", false);

            Normal.Properties.Add(new MetaDataProperty("dataSheet", "STSim_OutputStratumTransition"));
            Proportion.Properties.Add(new MetaDataProperty("dataSheet", "STSim_OutputStratumTransition"));

            Normal.Properties.Add(new MetaDataProperty("column", "Amount"));
            Proportion.Properties.Add(new MetaDataProperty("column", "Amount"));

            Normal.Properties.Add(new MetaDataProperty("skipTimestepZero", "True"));
            Proportion.Properties.Add(new MetaDataProperty("skipTimestepZero", "True"));

            Normal.Properties.Add(new MetaDataProperty("defaultValue", "0.0"));
            Proportion.Properties.Add(new MetaDataProperty("defaultValue", "0.0"));

            items.Add(Normal);
            items.Add(Proportion);
        }

        private static void AddChartAttributeVariables(SyncroSimLayoutItemCollection items, DataView attrGroupView, DataSheet attrGroupDataSheet, DataView attrView, DataSheet attrDataSheet, string outputTableName, string attributeTypeColumnName, bool skipTimestepZero)
        {
            Debug.Assert(Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME == Strings.DATASHEET_TRANSITION_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME);
            SyncroSimLayoutItem NonGroupedDensityGroup = new SyncroSimLayoutItem(DENSITY_GROUP_NAME + "STSIM_NON_GROUPED", "Density", true);

            AddChartNonGroupedAttributes(
                items, attrView, attrDataSheet, outputTableName, attributeTypeColumnName, 
                skipTimestepZero, NonGroupedDensityGroup);

            if (NonGroupedDensityGroup.Items.Count > 0)
            {
                items.Add(NonGroupedDensityGroup);
            }

            Dictionary<string, SyncroSimLayoutItem> GroupsDict = new Dictionary<string, SyncroSimLayoutItem>();
            List<SyncroSimLayoutItem> GroupsList = new List<SyncroSimLayoutItem>();

            foreach (DataRowView drv in attrGroupView)
            {
                string GroupName = Convert.ToString(drv.Row[Strings.DATASHEET_NAME_COLUMN_NAME], CultureInfo.InvariantCulture);
                string DensityGroupName = DENSITY_GROUP_NAME + GroupName;
                SyncroSimLayoutItem Group = new SyncroSimLayoutItem(GroupName, GroupName, true);
                SyncroSimLayoutItem DensityGroup = new SyncroSimLayoutItem(DensityGroupName, "Density", true);

                GroupsDict.Add(GroupName, Group);
                GroupsList.Add(Group);

                GroupsDict.Add(DensityGroupName, DensityGroup);
            }

            AddChartGroupedAttributes(
                GroupsDict, attrGroupDataSheet, attrView, attrDataSheet, 
                outputTableName, attributeTypeColumnName, skipTimestepZero);

            foreach (SyncroSimLayoutItem g in GroupsList)
            {
                if (g.Items.Count > 0)
                {
                    string DensityGroupName = DENSITY_GROUP_NAME + g.Name;

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
            SyncroSimLayoutItem densityGroup)
        {
            foreach (DataRowView drv in attrsView)
            {
                if (drv.Row[Strings.DATASHEET_ATTRIBUTE_GROUP_ID_COLUMN_NAME] == DBNull.Value)
                {
                    int AttrId = Convert.ToInt32(drv.Row[attrsDataSheet.ValueMember], CultureInfo.InvariantCulture);
                    string Units = DataTableUtilities.GetDataStr(drv.Row, Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME);

                    //Normal Attribute
                    //----------------

                    string AttrNameNormal = string.Format(CultureInfo.InvariantCulture, "attrnormal-{0}", AttrId);
                    string DisplayNameNormal = Convert.ToString(drv.Row[attrsDataSheet.ValidationTable.DisplayMember], CultureInfo.InvariantCulture);

                    if (Units != null)
                    {
                        DisplayNameNormal = string.Format(CultureInfo.InvariantCulture, "{0} ({1})", DisplayNameNormal, Units);
                    }

                    SyncroSimLayoutItem ItemNormal = new SyncroSimLayoutItem(AttrNameNormal, DisplayNameNormal, false);

                    ItemNormal.Properties.Add(new MetaDataProperty("dataSheet", outputDataSheetName));
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

                    string AttrNameDensity = string.Format(CultureInfo.InvariantCulture, "attrdensity-{0}", AttrId);
                    string DisplayNameDensity = Convert.ToString(drv.Row[attrsDataSheet.ValidationTable.DisplayMember], CultureInfo.InvariantCulture);

                    if (Units != null)
                    {
                        DisplayNameDensity = string.Format(CultureInfo.InvariantCulture, "{0} ({1})", DisplayNameDensity, Units);
                    }

                    SyncroSimLayoutItem ItemDensity = new SyncroSimLayoutItem(AttrNameDensity, DisplayNameDensity, false);

                    ItemDensity.Properties.Add(new MetaDataProperty("dataSheet", outputDataSheetName));
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
            string outputColumnName, bool skipTimestepZero)
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
                    SyncroSimLayoutItem DensityGroup = groupsDict[DENSITY_GROUP_NAME + GroupName];
                    string Units = DataTableUtilities.GetDataStr(drv.Row, Strings.DATASHEET_STATE_ATTRIBUTE_TYPE_UNITS_COLUMN_NAME);

                    //Normal Attribute
                    //----------------

                    string AttrNameNormal = string.Format(CultureInfo.InvariantCulture, "attrnormal-{0}", AttrId);
                    string DisplayNameNormal = Convert.ToString(drv.Row[attrsDataSheet.ValidationTable.DisplayMember], CultureInfo.InvariantCulture);

                    if (Units != null)
                    {
                        DisplayNameNormal = string.Format(CultureInfo.InvariantCulture, "{0} ({1})", DisplayNameNormal, Units);
                    }

                    SyncroSimLayoutItem ItemNormal = new SyncroSimLayoutItem(AttrNameNormal, DisplayNameNormal, false);

                    ItemNormal.Properties.Add(new MetaDataProperty("dataSheet", outputDataSheetName));
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

                    string AttrNameDensity = string.Format(CultureInfo.InvariantCulture, "attrdensity-{0}", AttrId);
                    string DisplayNameDensity = Convert.ToString(drv.Row[attrsDataSheet.ValidationTable.DisplayMember], CultureInfo.InvariantCulture);

                    if (Units != null)
                    {
                        DisplayNameDensity = string.Format(CultureInfo.InvariantCulture, "{0} ({1})", DisplayNameDensity, Units);
                    }

                    SyncroSimLayoutItem ItemDensity = new SyncroSimLayoutItem(AttrNameDensity, DisplayNameDensity, false);

                    ItemDensity.Properties.Add(new MetaDataProperty("dataSheet", outputDataSheetName));
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

                col.ValidationTable = ValidationTableUtilities.CreateAgeValidationTable(project);
            }
        }
    }
}
